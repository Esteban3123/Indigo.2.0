
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-15
-- Description:	Procedimiento para el reporte de ejecucion presupuestal de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportBudgetExcutionExpense]
	@xmlCriterias AS XML
AS
BEGIN	
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @ValidityId INT,
			@Year INT,
			@Month INT,
			@CodeToUse INT,
			@FinancialSourceCodeStart VARCHAR(20),
			@FinancialSourceCodeEnd VARCHAR(20),
			@CodeCategoryStart VARCHAR(20),
			@CodeCategoryEnd VARCHAR(20)
	
	DECLARE @TableExecution TABLE
	(
		BudgetId INT,
		CategoryOwnerId INT,
		CategoryId INT,
		CategoryCode VARCHAR(40),
		CategoryName VARCHAR(MAX),
		AlternativeCode VARCHAR(40),
		FinancialSourceCode VARCHAR(20),
		FinancialSourceName VARCHAR(MAX),
		revenueTypeId INT,
		revenueTypeCode VARCHAR(20),
		revenueTypeName VARCHAR(MAX),
		-----------------------------------------------------------------------
		CCPETOwnerId INT,
		CCPETId INT,
		CCPETCode VARCHAR(200),
		CCPETName VARCHAR(MAX),
		-----------------------------------------------------------------------
		BudgetInitial DECIMAL(18,0) DEFAULT (0),
		BudgetTransferDebit DECIMAL(18,0) DEFAULT (0),
		BudgetTransferCredit DECIMAL(18,0) DEFAULT (0),
		BudgetModificationDebit DECIMAL(18,0) DEFAULT (0),
		BudgetModificationCredit DECIMAL(18,0) DEFAULT (0),
		AvailabilityBalanceMonth DECIMAL(18,0) DEFAULT (0),
		AvailabilityBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		CommitmentBalanceMonth DECIMAL(18,0) DEFAULT (0),
		CommitmentBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		ObligationBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		ObligationBalanceMonth DECIMAL(18,0) DEFAULT (0),
		PaymentBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		PaymentBalanceMonth DECIMAL(18,0) DEFAULT (0)
	)

	BEGIN TRY
		
		/************************************************* CRITERIOS *************************************************/

		SELECT	@ValidityId = t.x.value('ValidityId[1]','int'),
				@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@CodeToUse = t.x.value('CodeToUse[1]','int'),
				@FinancialSourceCodeStart = t.x.value('FinancialSourceStart[1]','varchar(20)'),
				@FinancialSourceCodeEnd = t.x.value('FinancialSourceEnd[1]','varchar(20)'),
				@CodeCategoryStart = t.x.value('CategoryStart[1]','varchar(20)'),
				@CodeCategoryEnd = t.x.value('CategoryEnd[1]','varchar(20)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		---------------------------------------------------------------------------------------------------------------

		DECLARE @InitialDate AS DATE = DATEFROMPARTS(@Year, 1, 1)
		DECLARE @InitialDateMonthSelect AS DATE = DATEFROMPARTS(@Year, @Month, 1)
		DECLARE @EndDate AS DATE = DATEADD(DAY, -1, DATEADD(MONTH, 1, @InitialDateMonthSelect))
		DECLARE @EndDateLastMonth AS DATE = DATEADD(DAY, -1, @InitialDateMonthSelect)

		/********************************************  OBTENCION DE DATOS ********************************************/

		-- insertamos en la tabla temporal los datos del presupuesto inicial
		INSERT INTO @TableExecution 
		(
			BudgetId, CategoryOwnerId, CategoryId, CategoryCode, CategoryName, AlternativeCode, FinancialSourceCode, FinancialSourceName, revenueTypeId, revenueTypeCode, revenueTypeName, BudgetInitial
		)
		SELECT 
			b.Id, c.CategoryOwnerId, c.Id, c.Code, c.Name, c.AlternativeCode, fs.Code, fs.Name, rt.Id, rt.Code, rt.Name, b.InitialValue 
		FROM Budget.Budget b 
		JOIN Budget.Category c ON c.Id = b.CategoryId
		JOIN Budget.FinancialSource fs ON fs.Id = c.FinancialSourceId 
		JOIN Budget.RevenueType rt ON rt.Id = b.RevenueTypeId 
		JOIN Budget.BudgetHeader bh ON bh.Id = b.BudgetHeaderId 
		WHERE bh.BudgetaryValidityId = @ValidityId 
			AND c.ItemType = 2
			AND c.Code BETWEEN ISNULL(@CodeCategoryStart,'0') AND ISNULL(@CodeCategoryEnd,'99999999999999999999') 
			AND fs.Code BETWEEN ISNULL(@FinancialSourceCodeStart,'0') AND ISNULL(@FinancialSourceCodeEnd, '99999999999999999999') 

		 -- insertamos en la tabla temporal los datos del traslado de presupuesto 
		 UPDATE te
			SET te.BudgetTransferDebit = data.DebitValue,
				te.BudgetTransferCredit = data.CreditValue
		 FROM @TableExecution te
		 JOIN
		 (
			SELECT 
				btd.BudgetId,			
				SUM(IIF(btd.Nature = 1, btd.Value, 0)) DebitValue,
				SUM(IIF(btd.Nature = 1, 0, btd.Value)) CreditValue
			FROM Budget.BudgetTransfer bt 
			JOIN Budget.BudgetTransferDetail btd ON bt.Id = btd.TransferId
			WHERE CAST(bt.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate AND bt.Status = 2
			GROUP BY btd.BudgetId
		) AS data ON te.BudgetId = data.BudgetId

		 -- insertamos en la tabla temporal los datos de la modificacion del presupuesto
		 UPDATE te
			SET te.BudgetModificationDebit = data.DebitValue,
				te.BudgetModificationCredit = data.CreditValue
		 FROM @TableExecution te
		 JOIN
		 (
			SELECT 
				bmd.BudgetId,
				SUM(IIF(bmd.Nature = 1, bmd.Value, 0)) DebitValue,
				SUM(IIF(bmd.Nature = 1, 0, bmd.Value)) CreditValue 
			FROM Budget.BudgetModification bm
			JOIN Budget.BudgetModificationDetail bmd ON bm.Id = bmd.ModificationId
			WHERE CAST(bm.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate AND bm.Status = 2
			GROUP BY bmd.BudgetId
		) AS data ON data.BudgetId = te.BudgetId 

		-- insertamos en la tabla temporal los datos de los movimientos de disponibilidades en los meses anteriores al seleccionado
		 UPDATE te 
			SET te.AvailabilityBalanceMonthPrevious = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN 
		 (
			 SELECT 
				ad.BudgetId,
				SUM(ad.InitialValue) Value
			 FROM Budget.Availability a 
			 JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId		 
			 WHERE CAST(a.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND a.Status = 2
			 GROUP BY ad.BudgetId
		) AS data ON te.BudgetId = data.BudgetId
		LEFT JOIN
		(
			SELECT 
				ad.BudgetId,
				SUM(IIF(amd.Nature = 1, amd.Value, 0)) DebitValue,
				SUM(IIF(amd.Nature = 1, 0, amd.Value)) CreditValue
			FROM Budget.AvailabilityModification am 
			JOIN Budget.AvailabilityModificationDetail amd ON am.Id = amd.AvailabilityModificationId
			JOIN Budget.AvailabilityDetail ad ON amd.AvailabilityDetailId = ad.Id
			WHERE CAST(am.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND am.Status = 2
			GROUP BY ad.BudgetId
		) AS modification ON te.BudgetId = modification.BudgetId

		--insertamos en la tabla temporal los datos de los movimientos de disponibilidades del mes seleccionado
		 UPDATE te 
			SET te.AvailabilityBalanceMonth = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN
		 (
			SELECT 
				ad.BudgetId,
				SUM(ad.InitialValue) Value
			FROM Budget.Availability a 
			JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
			WHERE CAST(a.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND a.Status = 2 
			GROUP BY ad.BudgetId
		) AS data ON te.BudgetId = data.BudgetId
		LEFT JOIN
		(
			SELECT 
				ad.BudgetId,
				SUM(IIF(amd.Nature = 1, amd.Value, 0)) DebitValue,
				SUM(IIF(amd.Nature = 1, 0, amd.Value)) CreditValue
			FROM Budget.AvailabilityModification am 
			JOIN Budget.AvailabilityModificationDetail amd ON am.Id = amd.AvailabilityModificationId
			JOIN Budget.AvailabilityDetail ad ON amd.AvailabilityDetailId = ad.Id
			WHERE CAST(am.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND am.Status = 2
			GROUP BY ad.BudgetId
		) AS modification ON te.BudgetId = modification.BudgetId

		 -- insertamos en la tabla temporal los datos de los movimientos de compromisos en los meses anteriores al seleccionado
		 UPDATE te 
			SET te.CommitmentBalanceMonthPrevious = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN 
		 (
			 SELECT 
				cd.CategoryId,
				cd.RevenueTypeId,
				SUM(cd.InitialValue) Value
			 FROM Budget.Commitment c 
			 JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
			 WHERE CAST(c.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND c.Status = 2
			 GROUP BY cd.CategoryId, cd.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				cd.CategoryId,
				cd.RevenueTypeId,
				SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue,
				SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
			FROM Budget.CommitmentModification cm 
			JOIN Budget.CommitmentModificationDetail cmd ON cm.Id = cmd.CommitmentModificationId
			JOIN Budget.CommitmentDetail cd ON cmd.CommitmentDetailId = cd.Id
			WHERE CAST(cm.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND cm.Status = 2
			GROUP BY cd.CategoryId, cd.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		 --insertamos en la tabla temporal los datos de los movimientos de compromisos del mes seleccionado
		 UPDATE te 
			SET te.CommitmentBalanceMonth = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN
		 (
			SELECT 
				cd.CategoryId, 
				cd.RevenueTypeId,
				SUM(cd.InitialValue) Value
			FROM Budget.Commitment c
			JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
			WHERE CAST(c.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND c.Status = 2 
			GROUP BY cd.CategoryId, cd.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				cd.CategoryId, 
				cd.RevenueTypeId,
				SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue,
				SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
			FROM Budget.CommitmentModification cm 
			JOIN Budget.CommitmentModificationDetail cmd ON cm.Id = cmd.CommitmentModificationId
			JOIN Budget.CommitmentDetail cd ON cmd.CommitmentDetailId = cd.Id
			WHERE CAST(cm.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND cm.Status = 2
			GROUP BY cd.CategoryId, cd.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		 --insertamos en la tabla temporal los datos de los movimientos de obligaciones en los meses anteriores al seleccionado
		 UPDATE te 
			SET te.ObligationBalanceMonthPrevious = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN 
		 (
			 SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(od.InitialValue) Value
			 FROM Budget.Obligation o 
			 JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
			 WHERE CAST(o.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND o.Status = 2
			 GROUP BY od.CategoryId, od.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(IIF(omd.Nature = 1, omd.Value, 0)) DebitValue,
				SUM(IIF(omd.Nature = 1, 0, omd.Value)) CreditValue
			FROM Budget.ObligationModification om 
			JOIN Budget.ObligationModificationDetail omd ON om.Id = omd.ObligationModificationId
			JOIN Budget.ObligationDetail od ON omd.ObligationDetailId = od.Id
			WHERE CAST(om.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND om.Status = 2
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		 -- insertamos en la tabla temporal los datos de los movimientos de obligaciones en el mes seleccionado
		 UPDATE te 
			SET te.ObligationBalanceMonth = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN
		 (
			SELECT 
				od.CategoryId, 
				od.RevenueTypeId,
				SUM(od.InitialValue) Value
			FROM Budget.Obligation o
			JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
			WHERE CAST(o.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND o.Status = 2 
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				od.CategoryId, 
				od.RevenueTypeId,
				SUM(IIF(omd.Nature = 1, omd.Value, 0)) DebitValue,
				SUM(IIF(omd.Nature = 1, 0, omd.Value)) CreditValue
			FROM Budget.ObligationModification om 
			JOIN Budget.ObligationModificationDetail omd ON om.Id = omd.ObligationModificationId
			JOIN Budget.ObligationDetail od ON omd.ObligationDetailId = od.Id
			WHERE CAST(om.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND om.Status = 2
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		 --insertamos en la tabla temporal los datos de los movimientos de pagos en los meses anteriores al seleccionado
		 UPDATE te 
			SET te.PaymentBalanceMonthPrevious = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN 
		 (
			 SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(pod.InitialValue) Value
			 FROM Budget.PaymentOrder po 
			 JOIN Budget.PaymentOrderDetail pod ON po.Id = pod.PaymentOrderId
			 JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
			 WHERE CAST(po.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND po.Status IN (2, 4)
			 GROUP BY od.CategoryId, od.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			-- Reintegros de meses anteriores
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(rrd.Value) DebitValue,
				0 CreditValue
			FROM Budget.ReimbursementResource rr 
			JOIN Budget.ReimbursementResourceDetaill rrd ON rr.Id = rrd.ReimbursementResourceId
			JOIN Budget.PaymentOrderDetail pod ON rrd.PaymentOrderDetailId = pod.Id
			JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
			WHERE CAST(rr.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND rr.Status IN (2, 4)
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		 -- insertamos en la tabla temporal los datos de los movimientos de pagos en el mes seleccionado
		 UPDATE te 
			SET te.PaymentBalanceMonth = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN
		 (
			SELECT 
				od.CategoryId, 
				od.RevenueTypeId,
				SUM(pod.InitialValue) Value
			FROM Budget.PaymentOrder po 
			 JOIN Budget.PaymentOrderDetail pod ON po.Id = pod.PaymentOrderId
			 JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
			WHERE CAST(po.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND po.Status IN (2, 4)
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			-- insertamos en la tabla temporal los datos de los reintegros del mes seleccionado
			SELECT 
				od.CategoryId, 
				od.RevenueTypeId,
				SUM(rrd.Value) DebitValue,
				0 CreditValue
			FROM Budget.ReimbursementResource rr 
			JOIN Budget.ReimbursementResourceDetaill rrd ON rr.Id = rrd.ReimbursementResourceId
			JOIN Budget.PaymentOrderDetail pod ON rrd.PaymentOrderDetailId = pod.Id
			JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
			WHERE CAST(rr.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND rr.Status IN (2, 4)
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		/**************************************** OBTENCION CATEGORIAS PADRES ****************************************/

		-- Insertamos padres
		DECLARE @Rows INT = 1, 
				@Id INT = 0,
				@RowOwners INT = 1, 
				@OwnerId INT = 0

		IF @CodeToUse = 3
		BEGIN
			UPDATE te
				SET te.CCPETOwnerId = ccpet.CCPETOwnerId,
					te.CCPETId = ccpet.Id,
					te.CCPETCode = CONCAT(ISNULL(ccpet.Code, 'NP: ' + c.Code), ISNULL('.' + cpc.Code, '')),
					te.CCPETName = CONCAT(ISNULL(ccpet.Name, 'NP: ' + c.Name), ISNULL(' - ' + cpc.Name, ''))
			FROM @TableExecution te
			JOIN Budget.Category c ON te.CategoryId = c.Id
			LEFT JOIN Budget.CCPET ccpet ON c.CCPETCodeId = ccpet.Id
			LEFT JOIN Budget.CPCCatalog cpc ON c.CPCCodeId = cpc.Id

			WHILE @Rows > 0
			BEGIN
				SELECT TOP 1
					@Id = te.CCPETOwnerId,
					@RowOwners = 1,
					@OwnerId = te.CCPETOwnerId
				FROM @TableExecution te
				WHERE te.CCPETOwnerId > @Id
				ORDER BY te.CCPETOwnerId

				SET @Rows = @@ROWCOUNT
				IF @Rows = 0 
				BEGIN
					BREAK
				END

				IF NOT EXISTS (SELECT 1 FROM @TableExecution WHERE CCPETId = @Id)
				BEGIN
					INSERT INTO @TableExecution 
					(
						CCPETOwnerId, CCPETId, CCPETCode, CCPETName
					)
					SELECT 
						c.CCPETOwnerId, c.Id, c.Code, c.Name
					FROM Budget.CCPET c
					WHERE c.Id = @Id
				END

				UPDATE te
					SET te.BudgetInitial = teo.BudgetInitial,
						te.BudgetTransferDebit = teo.BudgetTransferDebit,
						te.BudgetTransferCredit = teo.BudgetTransferCredit,
						te.BudgetModificationDebit = teo.BudgetModificationDebit,
						te.BudgetModificationCredit = teo.BudgetModificationCredit,
						te.AvailabilityBalanceMonth = teo.AvailabilityBalanceMonth,
						te.AvailabilityBalanceMonthPrevious = teo.AvailabilityBalanceMonthPrevious,
						te.CommitmentBalanceMonth = teo.CommitmentBalanceMonth,
						te.CommitmentBalanceMonthPrevious = teo.CommitmentBalanceMonthPrevious,
						te.ObligationBalanceMonthPrevious = teo.ObligationBalanceMonthPrevious,
						te.ObligationBalanceMonth = teo.ObligationBalanceMonth,
						te.PaymentBalanceMonthPrevious = teo.PaymentBalanceMonthPrevious,
						te.PaymentBalanceMonth = teo.PaymentBalanceMonth
				FROM @TableExecution te
				JOIN
				(
					SELECT 
						te.CCPETOwnerId,
						SUM(te.BudgetInitial) BudgetInitial,
						SUM(te.BudgetTransferDebit) BudgetTransferDebit,
						SUM(te.BudgetTransferCredit) BudgetTransferCredit,
						SUM(te.BudgetModificationDebit) BudgetModificationDebit,
						SUM(te.BudgetModificationCredit) BudgetModificationCredit,
						SUM(te.AvailabilityBalanceMonth) AvailabilityBalanceMonth,
						SUM(te.AvailabilityBalanceMonthPrevious) AvailabilityBalanceMonthPrevious,
						SUM(te.CommitmentBalanceMonth) CommitmentBalanceMonth,
						SUM(te.CommitmentBalanceMonthPrevious) CommitmentBalanceMonthPrevious,
						SUM(te.ObligationBalanceMonthPrevious) ObligationBalanceMonthPrevious,
						SUM(te.ObligationBalanceMonth) ObligationBalanceMonth,
						SUM(te.PaymentBalanceMonthPrevious) PaymentBalanceMonthPrevious,
						SUM(te.PaymentBalanceMonth) PaymentBalanceMonth
					FROM @TableExecution te
					GROUP BY te.CCPETOwnerId
				) teo ON te.CCPETId = teo.CCPETOwnerId
				WHERE te.CCPETId = @Id

				WHILE @RowOwners > 0
				BEGIN
					SELECT TOP 1
						@OwnerId = c.CCPETOwnerId
					FROM Budget.CCPET c
					WHERE c.Id = @OwnerId
						AND	c.Id <> ISNULL(c.CCPETOwnerId, 0)

					SET @RowOwners = @@ROWCOUNT
					IF @RowOwners = 0 
					BEGIN
						BREAK
					END

					IF NOT EXISTS (SELECT 1 FROM @TableExecution WHERE CCPETId = @OwnerId)
					BEGIN
						INSERT INTO @TableExecution 
						(
							CCPETOwnerId, CCPETId, CCPETCode, CCPETName
						)
						SELECT 
							c.CCPETOwnerId, c.Id, c.Code, c.Name
						FROM Budget.CCPET c
						WHERE c.Id = @OwnerId
					END

					UPDATE te
						SET te.BudgetInitial = teo.BudgetInitial,
							te.BudgetTransferDebit = teo.BudgetTransferDebit,
							te.BudgetTransferCredit = teo.BudgetTransferCredit,
							te.BudgetModificationDebit = teo.BudgetModificationDebit,
							te.BudgetModificationCredit = teo.BudgetModificationCredit,
							te.AvailabilityBalanceMonth = teo.AvailabilityBalanceMonth,
							te.AvailabilityBalanceMonthPrevious = teo.AvailabilityBalanceMonthPrevious,
							te.CommitmentBalanceMonth = teo.CommitmentBalanceMonth,
							te.CommitmentBalanceMonthPrevious = teo.CommitmentBalanceMonthPrevious,
							te.ObligationBalanceMonthPrevious = teo.ObligationBalanceMonthPrevious,
							te.ObligationBalanceMonth = teo.ObligationBalanceMonth,
							te.PaymentBalanceMonthPrevious = teo.PaymentBalanceMonthPrevious,
							te.PaymentBalanceMonth = teo.PaymentBalanceMonth
					FROM @TableExecution te
					JOIN
					(
						SELECT 
							te.CCPETOwnerId,
							SUM(te.BudgetInitial) BudgetInitial,
							SUM(te.BudgetTransferDebit) BudgetTransferDebit,
							SUM(te.BudgetTransferCredit) BudgetTransferCredit,
							SUM(te.BudgetModificationDebit) BudgetModificationDebit,
							SUM(te.BudgetModificationCredit) BudgetModificationCredit,
							SUM(te.AvailabilityBalanceMonth) AvailabilityBalanceMonth,
							SUM(te.AvailabilityBalanceMonthPrevious) AvailabilityBalanceMonthPrevious,
							SUM(te.CommitmentBalanceMonth) CommitmentBalanceMonth,
							SUM(te.CommitmentBalanceMonthPrevious) CommitmentBalanceMonthPrevious,
							SUM(te.ObligationBalanceMonthPrevious) ObligationBalanceMonthPrevious,
							SUM(te.ObligationBalanceMonth) ObligationBalanceMonth,
							SUM(te.PaymentBalanceMonthPrevious) PaymentBalanceMonthPrevious,
							SUM(te.PaymentBalanceMonth) PaymentBalanceMonth
						FROM @TableExecution te
						GROUP BY te.CCPETOwnerId
					) teo ON te.CCPETId = teo.CCPETOwnerId
					WHERE te.CCPETId = @OwnerId
				END
			END
		END
		ELSE
		BEGIN
			WHILE @Rows > 0
			BEGIN
				SELECT TOP 1
					@Id = te.CategoryOwnerId,
					@RowOwners = 1,
					@OwnerId = te.CategoryOwnerId
				FROM @TableExecution te
				WHERE te.CategoryOwnerId > @Id
				ORDER BY te.CategoryOwnerId

				SET @Rows = @@ROWCOUNT
				IF @Rows = 0 
				BEGIN
					BREAK
				END

				IF NOT EXISTS (SELECT 1 FROM @TableExecution WHERE CategoryId = @Id)
				BEGIN
					INSERT INTO @TableExecution 
					(
						CategoryOwnerId, CategoryId, CategoryCode, CategoryName, AlternativeCode
					)
					SELECT 
						c.CategoryOwnerId, c.Id, c.Code, c.Name, c.AlternativeCode
					FROM Budget.Category c
					WHERE c.Id = @Id
				END

				UPDATE te
					SET te.BudgetInitial = teo.BudgetInitial,
						te.BudgetTransferDebit = teo.BudgetTransferDebit,
						te.BudgetTransferCredit = teo.BudgetTransferCredit,
						te.BudgetModificationDebit = teo.BudgetModificationDebit,
						te.BudgetModificationCredit = teo.BudgetModificationCredit,
						te.AvailabilityBalanceMonth = teo.AvailabilityBalanceMonth,
						te.AvailabilityBalanceMonthPrevious = teo.AvailabilityBalanceMonthPrevious,
						te.CommitmentBalanceMonth = teo.CommitmentBalanceMonth,
						te.CommitmentBalanceMonthPrevious = teo.CommitmentBalanceMonthPrevious,
						te.ObligationBalanceMonthPrevious = teo.ObligationBalanceMonthPrevious,
						te.ObligationBalanceMonth = teo.ObligationBalanceMonth,
						te.PaymentBalanceMonthPrevious = teo.PaymentBalanceMonthPrevious,
						te.PaymentBalanceMonth = teo.PaymentBalanceMonth
				FROM @TableExecution te
				JOIN
				(
					SELECT 
						te.CategoryOwnerId,
						SUM(te.BudgetInitial) BudgetInitial,
						SUM(te.BudgetTransferDebit) BudgetTransferDebit,
						SUM(te.BudgetTransferCredit) BudgetTransferCredit,
						SUM(te.BudgetModificationDebit) BudgetModificationDebit,
						SUM(te.BudgetModificationCredit) BudgetModificationCredit,
						SUM(te.AvailabilityBalanceMonth) AvailabilityBalanceMonth,
						SUM(te.AvailabilityBalanceMonthPrevious) AvailabilityBalanceMonthPrevious,
						SUM(te.CommitmentBalanceMonth) CommitmentBalanceMonth,
						SUM(te.CommitmentBalanceMonthPrevious) CommitmentBalanceMonthPrevious,
						SUM(te.ObligationBalanceMonthPrevious) ObligationBalanceMonthPrevious,
						SUM(te.ObligationBalanceMonth) ObligationBalanceMonth,
						SUM(te.PaymentBalanceMonthPrevious) PaymentBalanceMonthPrevious,
						SUM(te.PaymentBalanceMonth) PaymentBalanceMonth
					FROM @TableExecution te
					GROUP BY te.CategoryOwnerId
				) teo ON te.CategoryId = teo.CategoryOwnerId
				WHERE te.CategoryId = @Id

				WHILE @RowOwners > 0
				BEGIN
					SELECT TOP 1
						@OwnerId = c.CategoryOwnerId
					FROM Budget.Category c
					WHERE c.Id = @OwnerId
						AND	c.Id <> ISNULL(c.CategoryOwnerId, 0)

					SET @RowOwners = @@ROWCOUNT
					IF @RowOwners = 0 
					BEGIN
						BREAK
					END

					IF NOT EXISTS (SELECT 1 FROM @TableExecution WHERE CategoryId = @OwnerId)
					BEGIN
						INSERT INTO @TableExecution 
						(
							CategoryOwnerId, CategoryId, CategoryCode, CategoryName, AlternativeCode
						)
						SELECT 
							c.CategoryOwnerId, c.Id, c.Code, c.Name, c.AlternativeCode
						FROM Budget.Category c
						WHERE c.Id = @OwnerId
					END

					UPDATE te
						SET te.BudgetInitial = teo.BudgetInitial,
							te.BudgetTransferDebit = teo.BudgetTransferDebit,
							te.BudgetTransferCredit = teo.BudgetTransferCredit,
							te.BudgetModificationDebit = teo.BudgetModificationDebit,
							te.BudgetModificationCredit = teo.BudgetModificationCredit,
							te.AvailabilityBalanceMonth = teo.AvailabilityBalanceMonth,
							te.AvailabilityBalanceMonthPrevious = teo.AvailabilityBalanceMonthPrevious,
							te.CommitmentBalanceMonth = teo.CommitmentBalanceMonth,
							te.CommitmentBalanceMonthPrevious = teo.CommitmentBalanceMonthPrevious,
							te.ObligationBalanceMonthPrevious = teo.ObligationBalanceMonthPrevious,
							te.ObligationBalanceMonth = teo.ObligationBalanceMonth,
							te.PaymentBalanceMonthPrevious = teo.PaymentBalanceMonthPrevious,
							te.PaymentBalanceMonth = teo.PaymentBalanceMonth
					FROM @TableExecution te
					JOIN
					(
						SELECT 
							te.CategoryOwnerId,
							SUM(te.BudgetInitial) BudgetInitial,
							SUM(te.BudgetTransferDebit) BudgetTransferDebit,
							SUM(te.BudgetTransferCredit) BudgetTransferCredit,
							SUM(te.BudgetModificationDebit) BudgetModificationDebit,
							SUM(te.BudgetModificationCredit) BudgetModificationCredit,
							SUM(te.AvailabilityBalanceMonth) AvailabilityBalanceMonth,
							SUM(te.AvailabilityBalanceMonthPrevious) AvailabilityBalanceMonthPrevious,
							SUM(te.CommitmentBalanceMonth) CommitmentBalanceMonth,
							SUM(te.CommitmentBalanceMonthPrevious) CommitmentBalanceMonthPrevious,
							SUM(te.ObligationBalanceMonthPrevious) ObligationBalanceMonthPrevious,
							SUM(te.ObligationBalanceMonth) ObligationBalanceMonth,
							SUM(te.PaymentBalanceMonthPrevious) PaymentBalanceMonthPrevious,
							SUM(te.PaymentBalanceMonth) PaymentBalanceMonth
						FROM @TableExecution te
						GROUP BY te.CategoryOwnerId
					) teo ON te.CategoryId = teo.CategoryOwnerId
					WHERE te.CategoryId = @OwnerId
				END
			END
		END

		/************************************************* RESULTADO *************************************************/

		SELECT	BudgetId,
				IIF(@CodeToUse = 3, CCPETOwnerId, CategoryOwnerId) CategoryOwnerId,
				IIF(@CodeToUse = 3, CCPETId, CategoryId) CategoryId,
				IIF(@CodeToUse = 3, CCPETCode, CategoryCode) CategoryCode,
				IIF(@CodeToUse = 3, CCPETName, CategoryName) CategoryName,
				IIF(@CodeToUse = 3, NULL, AlternativeCode) AlternativeCode,
				IIF(@CodeToUse = 3, NULL, FinancialSourceCode) FinancialSourceCode,
				IIF(@CodeToUse = 3, NULL, revenueTypeCode) revenueTypeCode,
				IIF(@CodeToUse = 3, NULL, revenueTypeName) revenueTypeName,
				BudgetInitial,
				BudgetTransferDebit,
				BudgetTransferCredit,				
				BudgetModificationDebit,
				BudgetModificationCredit,
				AvailabilityBalanceMonthPrevious,
				AvailabilityBalanceMonth,
				CommitmentBalanceMonthPrevious, 
				CommitmentBalanceMonth,
				ObligationBalanceMonthPrevious,
				ObligationBalanceMonth,
				PaymentBalanceMonthPrevious,
				PaymentBalanceMonth
		FROM @TableExecution
		ORDER BY IIF(@CodeToUse = 3, CCPETCode, CategoryCode)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de ejecución presupuestal de gastos: genera el informe detallado del estado de ejecución del presupuesto de egresos para una vigencia presupuestal y mes determinados. Consolida, para cada rubro de gasto (categoría), los valores de presupuesto inicial, traslados (débitos y créditos), modificaciones presupuestales, y los saldos acumulados de disponibilidades, compromisos, obligaciones y pagos, tanto del mes seleccionado como de los meses anteriores dentro del mismo año. Filtra por vigencia presupuestal, fuente de financiación y rango de códigos de categoría, cruzando las tablas de presupuesto (Budget), categorías de gasto (Category), fuentes financieras (FinancialSource), tipos de ingreso/renta (RevenueType) y encabezado de presupuesto (BudgetHeader). Se utiliza para reportería financiera y control presupuestal institucional, permitiendo hacer seguimiento al comportamiento del gasto en cada etapa del ciclo presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBudgetExcutionExpense';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBudgetExcutionExpense';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de ejecución presupuestal de gastos para una vigencia, año y mes, consolidando presupuesto inicial, traslados, modificaciones, disponibilidades, compromisos, obligaciones, pagos y reintegros, con totales por mes anterior y mes seleccionado, agregados por la jerarquía de Categoría o por el clasificador CCPET.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /Data con elementos: ValidityId, Year, Month, CodeToUse, FinancialSourceStart/End y CategoryStart/End; ValidityId debe corresponder a una vigencia presupuestal existente en Budget.BudgetHeader; Year y Month deben ser válidos para construir fechas con DATEFROMPARTS (Month entre 1 y 12); Las jerarquías Budget.Category (vía CategoryOwnerId) y Budget.CCPET (vía CCPETOwnerId) deben ser acíclicas y terminar en un nodo cuyo Id = ISNULL(OwnerId,0) para que el ascenso jerárquico finalice', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran categorías con ItemType = 2 (rubros de gasto) en el cargue del presupuesto inicial; Solo se incluyen documentos con Status = 2 (estado válido/aprobado) para Transfers, Modifications, Availability, Commitment, Obligation y sus modificaciones; Para órdenes de pago y reembolsos solo se consideran Status IN (2, 4); La naturaleza débito se identifica por Nature = 1; cualquier otro valor se trata como crédito; Los saldos por movimiento se calculan como Valor inicial - Débitos de modificación + Créditos de modificación; Los reintegros (ReimbursementResource) restan al saldo de pagos (se suman como DebitValue, CreditValue=0); El periodo ''mes anterior'' va de 1-ene-@Year a fin del mes previo a @Month; el ''mes seleccionado'' va del primer al último día de @Month/@Year; Los filtros de rangos de Code (Categoría y FinancialSource) usan ISNULL con ''0'' y ''99999999999999999999'' como límites por defecto; Los nodos padre insertados durante la consolidación jerárquica heredan la suma agregada de sus hijos por CCPETOwnerId o CategoryOwnerId; El procedimiento siempre retorna un result set: el reporte completo o, ante error, una fila con CodeResult=''999''', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Vigencia presupuestal; Presupuesto inicial; Traslado presupuestal (débito/crédito); Modificación presupuestal; Disponibilidad presupuestal (CDP); Compromiso presupuestal; Obligación presupuestal; Orden de pago; Reintegro / Reembolso de recursos; Categoría / rubro presupuestal; Fuente de financiación; Tipo de ingreso (RevenueType); Clasificador CCPET; Catálogo CPC; Ejecución presupuestal de gastos; Naturaleza débito/crédito (Nature=1)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodeToUse = 3 → Agrupa y consolida la información usando la jerarquía CCPET (clasificador presupuestal CCPET + CPCCatalog), construyendo códigos compuestos (CCPETCode.CPCCode) y prefijando ''NP: '' cuando la categoría no tiene CCPET asociado else Agrupa y consolida la información usando la jerarquía de Budget.Category (CategoryOwnerId), incluyendo AlternativeCode, FinancialSourceCode y RevenueType en la salida; si NOT EXISTS fila con CCPETId/CategoryId = @Id en la tabla temporal → Inserta una fila padre (sin valores monetarios) con los datos del nodo padre desde Budget.CCPET o Budget.Category; si c.Id <> ISNULL(c.CCPETOwnerId, 0) (o equivalente para Category) → Continúa subiendo en la jerarquía hacia el siguiente padre else Detiene el ascenso jerárquico (raíz alcanzada); si Error en tiempo de ejecución (BEGIN CATCH) → Devuelve un único resultado con CodeResult=''999'' y MessageResult conteniendo ERROR_MESSAGE() y ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Budget; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.BudgetHeader; Budget.BudgetTransfer; Budget.BudgetTransferDetail; Budget.BudgetModification; Budget.BudgetModificationDetail; Budget.Availability; Budget.AvailabilityDetail; Budget.AvailabilityModification; Budget.AvailabilityModificationDetail; Budget.Commitment; Budget.CommitmentDetail; Budget.CommitmentModification; Budget.CommitmentModificationDetail; Budget.Obligation; Budget.ObligationDetail; Budget.ObligationModification; Budget.ObligationModificationDetail; Budget.PaymentOrder; Budget.PaymentOrderDetail; Budget.ReimbursementResource; Budget.ReimbursementResourceDetaill; Budget.CCPET; Budget.CPCCatalog', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionExpense';
-- GO
