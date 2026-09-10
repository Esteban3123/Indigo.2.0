-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de modificación de disponibilidades del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpenseAvailabilityModification]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@AvailabilityModificationCode VARCHAR(20)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@GroupBy = t.x.value('GroupBy[1]','tinyint'),
				@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@AvailabilityModificationCode = t.x.value('AvailabilityModificationCode[1]','varchar(20)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT	CASE @GroupBy
					WHEN 2 THEN cat.Id
					WHEN 3 THEN fs.Id
					WHEN 4 THEN rt.Id
					ELSE 0
				END GroupId,
				CASE @GroupBy
					WHEN 2 THEN cat.Code + ' - ' + cat.Name
					WHEN 3 THEN fs.Code + ' - ' + fs.Name
					WHEN 4 THEN rt.Code + ' - ' + rt.Name
					ELSE ''
				END GroupName,
				am.Code,
				am.DocumentDate,
				am.Document,
				am.Observations,
				a.Code AvailabilityCode,
				CASE am.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				CASE amd.Nature
					WHEN 1 THEN '(-) Débito'
					WHEN 2 THEN '(+) Crédito'
					ELSE 'N/A'
				END NatureName,
				amd.Value
		FROM Budget.AvailabilityModification am WITH (NOLOCK)
		JOIN Budget.AvailabilityModificationDetail amd WITH (NOLOCK) ON am.Id = amd.AvailabilityModificationId
		/************************************  RECONOCIMIENTO ************************************/
		JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON amd.AvailabilityDetailId = ad.Id
		JOIN Budget.Budget b WITH (NOLOCK) ON ad.BudgetId = b.Id
		JOIN Budget.Category cat WITH (NOLOCK) ON b.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON b.RevenueTypeId = rt.Id
		JOIN Budget.Availability a WITH (NOLOCK) ON ad.AvailabilityId = a.Id/**************************************** FILTROS ****************************************/
		WHERE CAST(am.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@AvailabilityModificationCode, '') = '' OR am.Code = @AvailabilityModificationCode)
		ORDER BY am.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de modificaciones a las disponibilidades presupuestales de gastos (CDP). Recibe como parámetros un rango de fechas, una vigencia presupuestaria y opcionalmente el código de una modificación específica, además de un criterio de agrupación (por categoría de gasto, fuente financiera o tipo de ingreso/renta). Cruza las tablas de modificaciones de disponibilidad (AvailabilityModification y AvailabilityModificationDetail) con el detalle del presupuesto (AvailabilityDetail, Budget, Category, FinancialSource, RevenueType y Availability) para devolver cada línea de ajuste (débito o crédito) con su código de CDP, fecha del documento, estado (Registrado, Confirmado o Anulado), clasificación presupuestal y valor modificado. Se usa para auditar y controlar los cambios realizados sobre los certificados de disponibilidad presupuestal en un período de vigencia determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseAvailabilityModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseAvailabilityModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de modificaciones de disponibilidad del presupuesto de gastos, agrupable por categoría, fuente financiera o tipo de ingreso, filtrando por rango de fechas y código de modificación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro @xmlCriterias debe contener un nodo /Data con DateStart, DateEnd, GroupBy, BudgetaryValidityId y AvailabilityModificationCode.; Deben existir relaciones íntegras entre AvailabilityModification, AvailabilityModificationDetail, AvailabilityDetail, Budget, Category, FinancialSource, RevenueType y Availability.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado de la modificación se interpreta exclusivamente como 1=Registrado, 2=Confirmado, 3=Anulado.; La naturaleza del detalle se interpreta exclusivamente como 1=Débito y 2=Crédito.; El reporte siempre cruza la modificación con su disponibilidad y el presupuesto base (Budget→Category→FinancialSource y Budget→RevenueType).; Las consultas se realizan con NOLOCK, permitiendo lecturas sucias.; El parámetro @BudgetaryValidityId se lee del XML pero no se utiliza en el filtrado del query.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de disponibilidad presupuestal; Disponibilidad presupuestal (CDP); Presupuesto de gastos; Categoría presupuestal; Fuente de financiación; Tipo de ingreso (rubro); Débito/Crédito presupuestal; Vigencia presupuestal; Estado del documento (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.AvailabilityModification: Devuelve filas de modificaciones cuyo DocumentDate está entre @DateStart y @DateEnd y, si se proporciona, cuyo Code coincide con @AvailabilityModificationCode.; [RETURN_RESULT] (error): Ante excepción retorna un resultset con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @GroupBy = 2 → Agrupa/identifica por Category (Id, Code, Name).; si @GroupBy = 3 → Agrupa/identifica por FinancialSource (Id, Code, Name).; si @GroupBy = 4 → Agrupa/identifica por RevenueType (Id, Code, Name). else GroupId=0 y GroupName='''' (sin agrupación).; si am.Status IN (1,2,3) → Traduce a ''Registrado'', ''Confirmado'' o ''Anulado'' respectivamente. else StatusName=''N/A''.; si amd.Nature IN (1,2) → Traduce a ''(-) Débito'' o ''(+) Crédito'' respectivamente. else NatureName=''N/A''.; si ISNULL(@AvailabilityModificationCode,'''')='''' → No aplica filtro por código de modificación. else Filtra estrictamente por am.Code = @AvailabilityModificationCode.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.AvailabilityModification; Budget.AvailabilityModificationDetail; Budget.AvailabilityDetail; Budget.Budget; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.Availability', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailabilityModification';
-- GO
