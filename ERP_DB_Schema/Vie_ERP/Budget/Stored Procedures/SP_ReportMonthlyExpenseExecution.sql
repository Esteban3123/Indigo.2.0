-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-03-01
-- Description:	Procedimiento para el reporte mensual de ejecucion presupuestal de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportMonthlyExpenseExecution]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE	@BudgetaryValidityId INT,
			@Year INT,
			@Month INT,
			@CodeToUse TINYINT,
			@IncludeZero BIT,
			@Categories VARCHAR(MAX),
			@FinancialSources VARCHAR(MAX),
			@RevenueTypes VARCHAR(MAX),
			-------------
			@FilterByCategories BIT = 0,
			@FilterByFinancialSources BIT = 0,
			@FilterByRevenueTypes BIT = 0

	DECLARE @Table_Categories AS TABLE(Id INT)
	DECLARE @Table_FinancialSources AS TABLE(Id INT)
	DECLARE @Table_RevenueTypes AS TABLE(Id INT)

	DECLARE @TableExecution TABLE
	(
		BudgetId INT,
		CategoryId INT,
		CategoryCode VARCHAR(40),
		CategoryName VARCHAR(MAX),
		AlternativeCode VARCHAR(40),
		FinancialSourceCode VARCHAR(20),
		FinancialSourceName VARCHAR(MAX),
		RevenueTypeId INT,
		RevenueTypeCode VARCHAR(20),
		RevenueTypeName VARCHAR(MAX),
		BudgetInitial DECIMAL(18,0) DEFAULT (0),
		BudgetTransferCredit DECIMAL(18,0) DEFAULT (0),
		BudgetTransferDebit DECIMAL(18,0) DEFAULT (0),
		BudgetModificationCredit DECIMAL(18,0) DEFAULT (0),
		BudgetModificationDebit DECIMAL(18,0) DEFAULT (0),
		AvailabilityBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		AvailabilityBalanceMonth DECIMAL(18,0) DEFAULT (0),
		CommitmentBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		CommitmentBalanceMonth DECIMAL(18,0) DEFAULT (0),
		ObligationBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		ObligationBalanceMonth DECIMAL(18,0) DEFAULT (0),
		PaymentOrderBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		PaymentOrderBalanceMonth DECIMAL(18,0) DEFAULT (0)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@CodeToUse = t.x.value('CodeToUse[1]','tinyint'),
				@IncludeZero = t.x.value('IncludeZero[1]','bit'),
				@Categories = t.x.value('Categories[1]','varchar(max)'),
				@FinancialSources = t.x.value('FinancialSources[1]','varchar(max)'),
				@RevenueTypes = t.x.value('RevenueTypes[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		DECLARE @InitialDate AS DATE = DATEFROMPARTS(@Year,1,1)
		DECLARE @EndDate AS DATE = DATEADD(DAY,-1,DATEFROMPARTS(@Year,@Month,1))
		DECLARE @InitialDateMonthSelect AS DATE = DATEFROMPARTS(@Year,@Month,1)
		DECLARE @EndDateMonthSelect AS DATE = DATEADD(DAY,-1,DATEADD(MONTH,1,@InitialDateMonthSelect))

		IF ISNULL(@Categories, '') <> ''
		BEGIN
			SET @FilterByCategories = 1

			INSERT INTO @Table_Categories
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Categories, ',')
		END

		IF ISNULL(@FinancialSources, '') <> ''
		BEGIN
			SET @FilterByFinancialSources = 1

			INSERT INTO @Table_FinancialSources
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@FinancialSources, ',')
		END

		IF ISNULL(@RevenueTypes, '') <> ''
		BEGIN
			SET @FilterByRevenueTypes = 1

			INSERT INTO @Table_RevenueTypes
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@RevenueTypes, ',')
		END

		/********************************** OBTENCION DE DATOS **********************************/

		-- insertamos en la tabla temporal los datos del presupuesto inicial
		INSERT INTO @TableExecution 
		(
			BudgetId, CategoryId, CategoryCode, CategoryName, AlternativeCode, FinancialSourceCode, FinancialSourceName, RevenueTypeId, RevenueTypeCode, RevenueTypeName, BudgetInitial
		)
		SELECT 
			b.Id, c.Id, c.Code, c.Name, AlternativeCode, fs.Code, fs.Name, rt.Id, rt.Code, rt.Name, b.InitialValue 
		FROM Budget.Budget b 
		JOIN Budget.Category c ON c.Id = b.CategoryId
		JOIN Budget.FinancialSource fs ON fs.Id = c.FinancialSourceId 
		JOIN Budget.RevenueType rt ON rt.Id = b.RevenueTypeId 
		JOIN Budget.BudgetHeader bh ON bh.Id = b.BudgetHeaderId 
		LEFT JOIN @Table_Categories tc ON c.Id = tc.Id
		LEFT JOIN @Table_FinancialSources tfs ON fs.Id = tfs.Id
		LEFT JOIN @Table_RevenueTypes trt ON rt.Id = trt.Id
		WHERE bh.BudgetaryValidityId = @BudgetaryValidityId 
			AND c.ItemType = 2
			AND (@FilterByCategories = 0 OR tc.Id IS NOT NULL)
			AND (@FilterByFinancialSources = 0 OR tfs.Id IS NOT NULL)
			AND (@FilterByRevenueTypes = 0 OR trt.Id IS NOT NULL)

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
			WHERE bt.Status = 2 AND CAST(bt.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateMonthSelect
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
			WHERE bm.Status = 2 AND CAST(bm.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateMonthSelect
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
			WHERE a.Status = 2 AND CAST(a.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
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
			WHERE am.Status = 2 AND CAST(am.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
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
			WHERE a.Status = 2 AND CAST(a.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
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
			WHERE am.Status = 2 AND CAST(am.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
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
			WHERE c.Status = 2 AND CAST(c.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
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
			WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
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
			WHERE c.Status = 2 AND CAST(c.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
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
			WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY cd.CategoryId, cd.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		-- insertamos en la tabla temporal los datos de los movimientos de obligaciones en los meses anteriores al seleccionado
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
			WHERE o.Status = 2 AND CAST(o.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
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
			WHERE om.Status = 2 AND CAST(om.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		--insertamos en la tabla temporal los datos de los movimientos de obligaciones del mes seleccionado
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
			WHERE o.Status = 2 AND CAST(o.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
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
			WHERE om.Status = 2 AND CAST(om.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		-- insertamos en la tabla temporal los datos de los movimientos de ordenes de pago en los meses anteriores al seleccionado
		UPDATE te 
			SET te.PaymentOrderBalanceMonthPrevious = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
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
			WHERE po.Status = 2 AND CAST(po.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(rrd.Value) DebitValue,
				0 CreditValue
			FROM Budget.ReimbursementResource rr
			JOIN Budget.ReimbursementResourceDetaill rrd ON rr.Id = rrd.ReimbursementResourceId
			JOIN Budget.PaymentOrderDetail pod ON rrd.PaymentOrderDetailId = pod.Id
			JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
			WHERE rr.Status = 2 AND CAST(rr.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		--insertamos en la tabla temporal los datos de los movimientos de ordenes de pago del mes seleccionado
		UPDATE te 
			SET te.PaymentOrderBalanceMonth = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
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
			WHERE po.Status = 2 AND CAST(po.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(rrd.Value) DebitValue,
				0 CreditValue
			FROM Budget.ReimbursementResource rr
			JOIN Budget.ReimbursementResourceDetaill rrd ON rr.Id = rrd.ReimbursementResourceId
			JOIN Budget.PaymentOrderDetail pod ON rrd.PaymentOrderDetailId = pod.Id
			JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
			WHERE rr.Status = 2 AND CAST(rr.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		-- Retornamos el resultado
		SELECT * 
		FROM @TableExecution 
		WHERE @IncludeZero = 1 OR
		(
			BudgetInitial <> 0 OR BudgetTransferCredit <> 0 OR BudgetTransferDebit <> 0 OR BudgetModificationCredit <> 0 OR BudgetModificationDebit <> 0 OR 
			AvailabilityBalanceMonthPrevious <> 0 OR AvailabilityBalanceMonth <> 0 OR 
			CommitmentBalanceMonthPrevious <> 0 OR CommitmentBalanceMonth <> 0 OR
			ObligationBalanceMonthPrevious <> 0 OR ObligationBalanceMonth <> 0 OR
			PaymentOrderBalanceMonthPrevious <> 0 OR PaymentOrderBalanceMonth <> 0
		)
		ORDER BY IIF(@CodeToUse = 1, CategoryCode, AlternativeCode)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END