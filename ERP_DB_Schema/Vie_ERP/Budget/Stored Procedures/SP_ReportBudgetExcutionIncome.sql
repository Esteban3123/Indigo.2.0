-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-15
-- Description:	Procedimiento para el reporte de ejecucion presupuestal de ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportBudgetExcutionIncome]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/
	
	DECLARE @ValidityId INT,
			@Year INT,
			@Month INT,
			@TypeValidity INT,
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
		BudgetTransferCredit DECIMAL(18,0) DEFAULT (0),
		BudgetTransferDebit DECIMAL(18,0) DEFAULT (0),
		BudgetModificationCredit DECIMAL(18,0) DEFAULT (0),
		BudgetModificationDebit DECIMAL(18,0) DEFAULT (0),
		RecognitionBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		RecognitionBalanceMonth DECIMAL(18,0) DEFAULT (0),
		CollectionBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		CollectionBalanceMonth DECIMAL(18,0) DEFAULT (0)
	)

	BEGIN TRY
		
		/************************************************* CRITERIOS *************************************************/

		SELECT	@ValidityId = t.x.value('ValidityId[1]','int'),
				@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@TypeValidity = t.x.value('TypeValidity[1]','int'),
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
			b.Id, c.CategoryOwnerId, c.Id, c.Code, c.Name, AlternativeCode, fs.Code, fs.Name, rt.Id, rt.Code, rt.Name, b.InitialValue 
		FROM Budget.Budget b 
		JOIN Budget.Category c ON c.Id = b.CategoryId
		JOIN Budget.FinancialSource fs ON fs.Id = c.FinancialSourceId 
		JOIN Budget.RevenueType rt ON rt.Id = b.RevenueTypeId 
		JOIN Budget.BudgetHeader bh ON bh.Id = b.BudgetHeaderId 
		WHERE bh.BudgetaryValidityId = @ValidityId 
			AND c.ItemType = 1
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
			SET te.RecognitionBalanceMonthPrevious = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN 
		 (
			 SELECT 
				rd.CategoryId,
				rd.RevenueTypeId,
				SUM(rd.InitialValue) Value
			 FROM Budget.Recognition r 
			 JOIN Budget.RecognitionDetail rd ON r.Id = rd.RecognitionId
			 WHERE r.Status = 2 AND CAST(r.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND 
			 (
				(@TypeValidity = 1 AND r.RecognitonType <> 3)
				OR
				(@TypeValidity = 2 AND r.RecognitonType = 3)
			 )
			 GROUP BY rd.CategoryId, rd.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				rd.CategoryId,
				rd.RevenueTypeId,
				SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue,
				SUM(IIF(rmd.Nature = 1, 0, rmd.Value)) CreditValue
			FROM Budget.RecognitionModification rm 
			JOIN Budget.RecognitionModificationDetail rmd ON rm.Id = rmd.RecognitionModificationId
			JOIN Budget.RecognitionDetail rd ON rmd.RecognitionDetailId = rd.Id
			JOIN Budget.Recognition r ON rd.RecognitionId = r.Id
			WHERE rm.Status = 2 AND CAST(rm.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND
			(
				(@TypeValidity = 1 AND r.RecognitonType <> 3)
				OR
				(@TypeValidity = 2 AND r.RecognitonType = 3)
			)
			GROUP BY rd.CategoryId, rd.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId



		 --insertamos en la tabla temporal los datos de los movimientos de compromisos del mes seleccionado
		 UPDATE te 
			SET te.RecognitionBalanceMonth = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN
		 (
			SELECT 
				rd.CategoryId, 
				rd.RevenueTypeId,
				SUM(rd.InitialValue) Value
			FROM Budget.Recognition r
			JOIN Budget.RecognitionDetail rd ON r.Id = rd.RecognitionId
			WHERE r.Status = 2 AND CAST(r.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND
			(
				(@TypeValidity = 1 AND r.RecognitonType <> 3)
				OR
				(@TypeValidity = 2 AND r.RecognitonType = 3)
			)
			GROUP BY rd.CategoryId, rd.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				rd.CategoryId, 
				rd.RevenueTypeId,
				SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue,
				SUM(IIF(rmd.Nature = 1, 0, rmd.Value)) CreditValue
			FROM Budget.RecognitionModification rm 
			JOIN Budget.RecognitionModificationDetail rmd ON rm.Id = rmd.RecognitionModificationId
			JOIN Budget.RecognitionDetail rd ON rmd.RecognitionDetailId = rd.Id
			JOIN Budget.Recognition r ON rd.RecognitionId = r.Id
			WHERE rm.Status = 2 AND CAST(rm.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND
			(
				(@TypeValidity = 1 AND r.RecognitonType <> 3)
				OR
				(@TypeValidity = 2 AND r.RecognitonType = 3)
			)
			GROUP BY rd.CategoryId, rd.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId


		-- insertamos en la tabla temporal los datos de los movimientos de compromisos en los meses anteriores al seleccionado
		 UPDATE te 
			SET te.CollectionBalanceMonthPrevious = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN 
		 (
			 SELECT 
				rd.CategoryId,
				rd.RevenueTypeId,
				SUM(cd.InitialValue) Value
			 FROM Budget.Collection c 
			 JOIN Budget.CollectionDetail cd ON c.Id = cd.CollectionId
			 JOIN Budget.RecognitionDetail rd ON cd.RecognitionDetailId = rd.Id
			 JOIN Budget.Recognition r ON rd.RecognitionId = r.Id
			 WHERE c.Status = 2 AND CAST(c.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND
			 (
				(@TypeValidity = 1 AND r.RecognitonType <> 3)
				OR
				(@TypeValidity = 2 AND r.RecognitonType = 3)
			 )
			 GROUP BY rd.CategoryId, rd.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				rd.CategoryId,
				rd.RevenueTypeId,
				SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue,
				SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
			FROM Budget.CollectionModification cm 
			JOIN Budget.CollectionModificationDetail cmd ON cm.Id = cmd.CollectionModificationId
			JOIN Budget.CollectionDetail cd ON cmd.CollectionDetailId = cd.Id
			JOIN Budget.RecognitionDetail rd ON cd.RecognitionDetailId = rd.Id
			JOIN Budget.Recognition r ON rd.RecognitionId = r.Id
			WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateLastMonth AND
			(
				(@TypeValidity = 1 AND r.RecognitonType <> 3)
				OR
				(@TypeValidity = 2 AND r.RecognitonType = 3)
			)
			GROUP BY rd.CategoryId, rd.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		 --insertamos en la tabla temporal los datos de los movimientos de compromisos del mes seleccionado
		  UPDATE te 
			SET te.CollectionBalanceMonth = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		 FROM @TableExecution te
		 LEFT JOIN 
		 (
			 SELECT 
				rd.CategoryId,
				rd.RevenueTypeId,
				SUM(cd.InitialValue) Value
			 FROM Budget.Collection c 
			 JOIN Budget.CollectionDetail cd ON c.Id = cd.CollectionId
			 JOIN Budget.RecognitionDetail rd ON cd.RecognitionDetailId = rd.Id
			 JOIN Budget.Recognition r ON rd.RecognitionId = r.Id
			 WHERE c.Status = 2 AND CAST(c.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND
			 (
				(@TypeValidity = 1 AND r.RecognitonType <> 3)
				OR
				(@TypeValidity = 2 AND r.RecognitonType = 3)
			 )
			 GROUP BY rd.CategoryId, rd.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				rd.CategoryId,
				rd.RevenueTypeId,
				SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue,
				SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
			FROM Budget.CollectionModification cm 
			JOIN Budget.CollectionModificationDetail cmd ON cm.Id = cmd.CollectionModificationId
			JOIN Budget.CollectionDetail cd ON cmd.CollectionDetailId = cd.Id
			JOIN Budget.RecognitionDetail rd ON cd.RecognitionDetailId = rd.Id
			JOIN Budget.Recognition r ON rd.RecognitionId = r.Id
			WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDate AND
			(
				(@TypeValidity = 1 AND r.RecognitonType <> 3)
				OR
				(@TypeValidity = 2 AND r.RecognitonType = 3)
			)
			GROUP BY rd.CategoryId, rd.RevenueTypeId
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
						te.RecognitionBalanceMonthPrevious = teo.RecognitionBalanceMonthPrevious,
						te.RecognitionBalanceMonth = teo.RecognitionBalanceMonth,
						te.CollectionBalanceMonthPrevious = teo.CollectionBalanceMonthPrevious,
						te.CollectionBalanceMonth = teo.CollectionBalanceMonth
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
						SUM(te.RecognitionBalanceMonthPrevious) RecognitionBalanceMonthPrevious,
						SUM(te.RecognitionBalanceMonth) RecognitionBalanceMonth,				
						SUM(te.CollectionBalanceMonthPrevious) CollectionBalanceMonthPrevious,
						SUM(te.CollectionBalanceMonth) CollectionBalanceMonth
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
							te.RecognitionBalanceMonthPrevious = teo.RecognitionBalanceMonthPrevious,
							te.RecognitionBalanceMonth = teo.RecognitionBalanceMonth,
							te.CollectionBalanceMonthPrevious = teo.CollectionBalanceMonthPrevious,
							te.CollectionBalanceMonth = teo.CollectionBalanceMonth
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
							SUM(te.RecognitionBalanceMonthPrevious) RecognitionBalanceMonthPrevious,
							SUM(te.RecognitionBalanceMonth) RecognitionBalanceMonth,
							SUM(te.CollectionBalanceMonthPrevious) CollectionBalanceMonthPrevious,
							SUM(te.CollectionBalanceMonth) CollectionBalanceMonth
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
						te.RecognitionBalanceMonthPrevious = teo.RecognitionBalanceMonthPrevious,
						te.RecognitionBalanceMonth = teo.RecognitionBalanceMonth,
						te.CollectionBalanceMonthPrevious = teo.CollectionBalanceMonthPrevious,
						te.CollectionBalanceMonth = teo.CollectionBalanceMonth
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
						SUM(te.RecognitionBalanceMonthPrevious) RecognitionBalanceMonthPrevious,
						SUM(te.RecognitionBalanceMonth) RecognitionBalanceMonth,				
						SUM(te.CollectionBalanceMonthPrevious) CollectionBalanceMonthPrevious,
						SUM(te.CollectionBalanceMonth) CollectionBalanceMonth
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
							te.RecognitionBalanceMonthPrevious = teo.RecognitionBalanceMonthPrevious,
							te.RecognitionBalanceMonth = teo.RecognitionBalanceMonth,
							te.CollectionBalanceMonthPrevious = teo.CollectionBalanceMonthPrevious,
							te.CollectionBalanceMonth = teo.CollectionBalanceMonth
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
							SUM(te.RecognitionBalanceMonthPrevious) RecognitionBalanceMonthPrevious,
							SUM(te.RecognitionBalanceMonth) RecognitionBalanceMonth,
							SUM(te.CollectionBalanceMonthPrevious) CollectionBalanceMonthPrevious,
							SUM(te.CollectionBalanceMonth) CollectionBalanceMonth
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
				RecognitionBalanceMonthPrevious,
				RecognitionBalanceMonth,
				CollectionBalanceMonthPrevious, 
				CollectionBalanceMonth
		FROM @TableExecution
		ORDER BY IIF(@CodeToUse = 3, CCPETCode, CategoryCode)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END