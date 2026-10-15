-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-27
-- Description:	Procedimiento para el reporte mensual de ejecucion presupuestal de ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportMonthlyIncomeExecution]
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
		RecognitionBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		RecognitionBalanceMonth DECIMAL(18,0) DEFAULT (0),
		CollectionBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		CollectionBalanceMonth DECIMAL(18,0) DEFAULT (0)
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
			AND c.ItemType = 1
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
			WHERE r.Status = 2 AND CAST(r.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
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
			WHERE rm.Status = 2 AND CAST(rm.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
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
			WHERE r.Status = 2 AND CAST(r.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
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
			WHERE rm.Status = 2 AND CAST(rm.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
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
			WHERE c.Status = 2 AND CAST(c.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
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
			WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
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
			WHERE c.Status = 2 AND CAST(c.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
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
			WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY rd.CategoryId, rd.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		-- Retornamos el resultado
		SELECT * 
		FROM @TableExecution 
		WHERE @IncludeZero = 1 OR
		(
			BudgetInitial <> 0 OR BudgetTransferCredit <> 0 OR BudgetTransferDebit <> 0 OR BudgetModificationCredit <> 0 OR BudgetModificationDebit <> 0 OR 
			RecognitionBalanceMonthPrevious <> 0 OR RecognitionBalanceMonth <> 0 OR 
			CollectionBalanceMonthPrevious <> 0 OR CollectionBalanceMonth <> 0
		)
		ORDER BY IIF(@CodeToUse = 1, CategoryCode, AlternativeCode)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END