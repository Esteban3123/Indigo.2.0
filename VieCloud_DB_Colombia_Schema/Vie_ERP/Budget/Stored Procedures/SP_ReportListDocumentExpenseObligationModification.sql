-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de modificación de obligaciones del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpenseObligationModification]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@ObligationModificationCode VARCHAR(20),
			@ThirdParties VARCHAR(MAX),
			-------------
			@FilterByThirdParties BIT = 0

	DECLARE @Table_ThirdParties AS TABLE(Id INT)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@GroupBy = t.x.value('GroupBy[1]','tinyint'),
				@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@ObligationModificationCode = t.x.value('ObligationModificationCode[1]','varchar(20)'),
				@ThirdParties = t.x.value('ThirdParties[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		IF ISNULL(@ThirdParties, '') <> ''
		BEGIN
			SET @FilterByThirdParties = 1

			INSERT INTO @Table_ThirdParties
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@ThirdParties, ',')
		END

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT	CASE @GroupBy
					WHEN 1 THEN tp.Id
					WHEN 2 THEN cat.Id
					WHEN 3 THEN fs.Id
					WHEN 4 THEN rt.Id
					ELSE 0
				END GroupId,
				CASE @GroupBy
					WHEN 1 THEN tp.Nit + ' - ' + tp.Name
					WHEN 2 THEN cat.Code + ' - ' + cat.Name
					WHEN 3 THEN fs.Code + ' - ' + fs.Name
					WHEN 4 THEN rt.Code + ' - ' + rt.Name
					ELSE ''
				END GroupName,
				om.Code,
				om.DocumentDate,
				om.Document,
				om.Observations,
				o.Code ObligationCode,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE om.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				om.EntityCode OriginCode,
				ISNULL(gend.Description, om.EntityName) OriginName,
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				CASE omd.Nature
					WHEN 1 THEN '(-) Débito'
					WHEN 2 THEN '(+) Crédito'
					ELSE 'N/A'
				END NatureName,
				omd.Value
		FROM Budget.ObligationModification om WITH (NOLOCK)
		JOIN Budget.ObligationModificationDetail omd WITH (NOLOCK) ON om.Id = omd.ObligationModificationId
		/************************************  RECONOCIMIENTO ************************************/
		JOIN Budget.ObligationDetail od WITH (NOLOCK) ON omd.ObligationDetailId = od.Id
		JOIN Budget.Category cat WITH (NOLOCK) ON od.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON od.RevenueTypeId = rt.Id
		JOIN Budget.Obligation o WITH (NOLOCK) ON od.ObligationId = o.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON o.ThirdPartyId = tp.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON om.EntityName = gend.EntityName
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(om.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@ObligationModificationCode, '') = '' OR om.Code = @ObligationModificationCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY om.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de modificaciones de obligaciones presupuestarias de gasto, permitiendo consultar los ajustes, correcciones o anulaciones aplicados a obligaciones dentro de una vigencia presupuestal. Recibe criterios de búsqueda en formato XML (rango de fechas, vigencia presupuestal, código de modificación y lista de terceros) y cruza las tablas de modificaciones, detalles de obligación, categorías, fuentes financieras, tipos de ingreso, obligaciones y terceros para producir un listado completo. Cada fila muestra el código y estado de la modificación (Registrado, Confirmado, Anulado), el documento soporte, el tercero (NIT y nombre), la categoría presupuestal, la fuente financiera, el tipo de renta y la naturaleza del movimiento (débito o crédito) con su valor. Permite agrupar los resultados por tercero, categoría, fuente financiera o tipo de ingreso, y es usado para auditoría, seguimiento y control del ciclo de vida de las obligaciones presupuestarias de gastos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseObligationModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseObligationModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un listado tabular de modificaciones a obligaciones del presupuesto de gastos, agrupable por tercero, categoría, fuente financiera o tipo de renta, filtrando por rango de fechas, código de modificación y terceros.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener el nodo /Data con DateStart, DateEnd, GroupBy, BudgetaryValidityId, ObligationModificationCode y ThirdParties.; @GroupBy debe estar en {1,2,3,4} para producir agrupación válida; otros valores devuelven GroupId=0 y GroupName vacío.; @ThirdParties, si se envía, debe ser una lista de enteros separados por coma convertibles a INT (CAST a INT en dbo.Split).; @DateStart y @DateEnd deben ser fechas válidas para acotar DocumentDate.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte solo retorna modificaciones cuya DocumentDate cae en el rango [@DateStart, @DateEnd] (inclusive).; Si se especifica @ObligationModificationCode, se restringe a esa modificación exacta; si viene vacío/NULL no se aplica el filtro.; Si se especifica @ThirdParties, solo se incluyen obligaciones cuyos terceros estén en la lista.; El nombre del origen prioriza la descripción de Common.GetEntityNameDescriptions; si no hay match, usa om.EntityName.; Cualquier excepción se captura y retorna un único resultset con CodeResult=''999'' y mensaje + línea de error, en lugar de propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de obligación presupuestal; Vigencia presupuestal; Tercero (proveedor/contratista); Categoría presupuestal; Fuente de financiación; Tipo de renta; Naturaleza débito/crédito; Estado de obligación (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas de modificaciones de obligaciones uniendo ObligationModification, ObligationModificationDetail, ObligationDetail, Category, FinancialSource, RevenueType, Obligation y ThirdParty, ordenadas por om.Id, cuando CAST(om.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd y se cumplen los filtros opcionales.; [RETURN_RESULT] Resultset: En caso de error en TRY, devuelve un resultset con columnas CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@ThirdParties,'''') <> '''' → Activa el filtro por terceros y carga la lista de IDs separados por coma en una tabla temporal usando dbo.Split else No se filtra por terceros (@FilterByThirdParties=0); si @GroupBy IN (1,2,3,4) → Define GroupId/GroupName según agrupación: 1=Tercero (Nit-Nombre), 2=Categoría, 3=Fuente Financiera, 4=Tipo de Renta else GroupId=0 y GroupName vacío; si om.Status IN (1,2,3) → Traduce a ''Registrado'' (1), ''Confirmado'' (2) o ''Anulado'' (3) else ''N/A''; si omd.Nature = 1 ó 2 → Etiqueta el movimiento como ''(-) Débito'' (1) o ''(+) Crédito'' (2) else ''N/A''', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.ObligationModification; Budget.ObligationModificationDetail; Budget.ObligationDetail; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.Obligation; Common.ThirdParty; Common.GetEntityNameDescriptions; dbo.Split', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligationModification';
-- GO
