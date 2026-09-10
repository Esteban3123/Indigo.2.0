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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte mensual de ejecución presupuestal de ingresos para una vigencia presupuestal determinada. Consolida en una tabla temporal el presupuesto inicial de ingresos, los traslados presupuestales (débitos y créditos), las modificaciones presupuestales, y los saldos de reconocimiento y recaudo acumulados hasta el mes anterior y del mes seleccionado. Recibe criterios de filtro en formato XML (vigencia, año, mes, categorías, fuentes financieras y tipos de ingreso/renta) y utiliza la función Split para descomponer listas separadas por comas. Compone información de las tablas Budget, Category, FinancialSource, RevenueType y BudgetHeader para producir el informe de ejecución presupuestal de ingresos por categoría, fuente financiera y tipo de renta.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportMonthlyIncomeExecution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportMonthlyIncomeExecution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Generar el reporte de ejecución presupuestal mensual de ingresos consolidando, por rubro/categoría, el presupuesto inicial, traslados, modificaciones, reconocimientos y recaudos del mes seleccionado y de los meses previos de la vigencia.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyIncomeExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener un nodo /Data con BudgetaryValidityId, Year, Month, CodeToUse e IncludeZero válidos.; @Year y @Month deben permitir construir fechas válidas con DATEFROMPARTS (Month entre 1 y 12).; Los parámetros Categories, FinancialSources y RevenueTypes, si vienen, deben ser cadenas de IDs enteros separados por coma compatibles con dbo.Split.; Debe existir un BudgetHeader para la vigencia (@BudgetaryValidityId) con líneas en Budget.Budget cuya Category tenga ItemType=1 para producir filas.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyIncomeExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran documentos en estado 2 (aprobado/confirmado) en BudgetTransfer, BudgetModification, Recognition, RecognitionModification, Collection y CollectionModification.; Solo se incluyen categorías cuyo ItemType = 1 (rubros de ingreso).; El presupuesto base se obtiene únicamente para BudgetHeader cuya BudgetaryValidityId coincide con el parámetro.; El periodo ''meses anteriores'' va del 1-ene del año al día anterior al primer día del mes seleccionado; el periodo ''mes seleccionado'' cubre desde el primer al último día del mes.; El cálculo de saldos de reconocimiento y recaudo se realiza como InitialValue - DébitosModificación + CréditosModificación.; La naturaleza 1 corresponde a Débito y cualquier otro valor a Crédito en todos los detalles (transfer, modificación, reconocimiento y recaudo).; Los filtros opcionales (categorías, fuentes, tipos de ingreso) solo se aplican si el parámetro respectivo no es nulo ni vacío.; Cualquier excepción durante la ejecución se captura y se devuelve como un resultset con CodeResult=''999'' y el mensaje + número de línea.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyIncomeExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Vigencia presupuestal; Presupuesto inicial de ingresos; Traslados presupuestales; Modificaciones presupuestales; Reconocimientos de ingresos; Modificaciones de reconocimientos; Recaudos (collection); Modificaciones de recaudos; Categoría presupuestal; Fuente de financiación; Tipo de ingreso (RevenueType); Naturaleza débito/crédito; Ejecución presupuestal mensual', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyIncomeExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve el contenido de @TableExecution filtrado: si @IncludeZero=0 omite filas con todos los valores en cero, ordenado por CategoryCode o AlternativeCode según @CodeToUse.; [RETURN_RESULT] Resultset: En caso de error en TRY/CATCH retorna un resultset alterno con columnas CodeResult=''999'' y MessageResult = ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyIncomeExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@Categories,'''') <> '''' → Activa filtro por categorías y carga el listado en tabla temporal con dbo.Split else No se filtra por categoría; si ISNULL(@FinancialSources,'''') <> '''' → Activa filtro por fuentes de financiación y carga el listado else No se filtra por fuente de financiación; si ISNULL(@RevenueTypes,'''') <> '''' → Activa filtro por tipos de ingreso y carga el listado else No se filtra por tipo de ingreso; si @IncludeZero = 1 → Incluye en el resultado todas las filas, incluso aquellas con todos los valores en cero else Solo se retornan filas con al menos un valor (inicial, traslado, modificación, reconocimiento o recaudo) distinto de cero; si @CodeToUse = 1 → Ordena el resultado por CategoryCode else Ordena el resultado por AlternativeCode; si btd.Nature = 1 (y análogos bmd/rmd/cmd) → El valor del detalle se acumula como Débito else El valor se acumula como Crédito', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyIncomeExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyIncomeExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Budget; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.BudgetHeader; Budget.BudgetTransfer; Budget.BudgetTransferDetail; Budget.BudgetModification; Budget.BudgetModificationDetail; Budget.Recognition; Budget.RecognitionDetail; Budget.RecognitionModification; Budget.RecognitionModificationDetail; Budget.Collection; Budget.CollectionDetail; Budget.CollectionModification; Budget.CollectionModificationDetail', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyIncomeExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyIncomeExecution';
-- GO
