-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-30
-- Description:	Procedimiento para el reporte de listado de recaudos del presupuesto de ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentIncomeCollection]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@CollectionCode VARCHAR(20),
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
				@CollectionCode = t.x.value('CollectionCode[1]','varchar(20)'),
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
					WHEN 2 THEN c.Id
					WHEN 3 THEN fs.Id
					WHEN 4 THEN rt.Id
					ELSE 0
				END GroupId,
				CASE @GroupBy
					WHEN 1 THEN tp.Nit + ' - ' + tp.Name
					WHEN 2 THEN c.Code + ' - ' + c.Name
					WHEN 3 THEN fs.Code + ' - ' + fs.Name
					WHEN 4 THEN rt.Code + ' - ' + rt.Name
					ELSE ''
				END GroupName,
				cn.Code,
				cn.DocumentDate,
				cn.Observations,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE cn.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				cn.EntityCode OriginCode,
				ISNULL(gend.Description, cn.EntityName) OriginName,
				c.Code CategoryCode,
				c.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				cnd.InitialValue,
				ISNULL(cm.DebitValue, 0) DebitValue,
				ISNULL(cm.CreditValue, 0) CreditValue,
				cnd.InitialValue - ISNULL(cm.DebitValue, 0) + ISNULL(cm.CreditValue, 0) TotalValue
		FROM Budget.Collection cn WITH (NOLOCK)
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON cn.ThirdPartyId = tp.Id
		JOIN Budget.CollectionDetail cnd WITH (NOLOCK) ON cn.Id = cnd.CollectionId
		JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON cnd.RecognitionDetailId = rd.Id
		JOIN Budget.Category c WITH (NOLOCK) ON rd.CategoryId = c.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON c.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON rd.RevenueTypeId = rt.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON cn.EntityName = gend.EntityName
		/************************************  MODIFICACIONES ************************************/
		LEFT JOIN 
		(
			SELECT	cmd.CollectionDetailId, 
					SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue, 
					SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
			FROM Budget.CollectionModification cm WITH (NOLOCK)
			JOIN Budget.CollectionModificationDetail cmd WITH (NOLOCK) ON cm.Id = cmd.CollectionModificationId
			WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY cmd.CollectionDetailId
		) cm ON cnd.Id = cm.CollectionDetailId 
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(cn.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@CollectionCode, '') = '' OR cn.Code = @CollectionCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY cn.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de documentos de recaudo del presupuesto de ingresos, mostrando cada cobro o recaudo registrado junto con su tercero (NIT y nombre), categoría presupuestal, fuente financiera, tipo de ingreso o renta, estado del documento (registrado, confirmado o anulado) y los valores iniciales con sus respectivos débitos y créditos por modificaciones confirmadas. Recibe criterios de búsqueda en formato XML, como rango de fechas, vigencia presupuestal, código de recaudo y filtro por terceros; admite agrupación por tercero, categoría, fuente financiera o tipo de ingreso. Integra las tablas de recaudos (Collection), detalles de recaudo (CollectionDetail), reconocimientos (RecognitionDetail), categorías (Category), fuentes financieras (FinancialSource), tipos de ingreso (RevenueType), terceros (ThirdParty) y las modificaciones confirmadas (CollectionModification / CollectionModificationDetail) para calcular el valor total vigente de cada línea de recaudo.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentIncomeCollection';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentIncomeCollection';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte tabular de recaudos presupuestales en un rango de fechas, agrupado dinámicamente por tercero, categoría, fuente financiera o tipo de ingreso, calculando el valor total vigente tras modificaciones confirmadas.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener al menos DateStart y DateEnd válidos para el filtro BETWEEN sobre cn.DocumentDate; Si se proporciona ThirdParties, debe ser una lista de enteros separados por coma convertibles vía dbo.Split; GroupBy debe estar entre 1 y 4 para producir un GroupId/GroupName significativo (en otro caso devuelve 0/'''')', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran modificaciones cuyo cm.Status = 2 (Confirmado) y cuyo DocumentDate cae dentro del rango [@DateStart, @DateEnd]; Los recaudos incluidos son aquellos cuyo cn.DocumentDate (casteado a DATE) está entre @DateStart y @DateEnd; TotalValue siempre se calcula como InitialValue - DebitValue + CreditValue, tratando ausencia de modificaciones como 0; OriginName usa la descripción amigable de Common.GetEntityNameDescriptions cuando existe; en su defecto usa cn.EntityName; El resultado se ordena por cn.Id (orden de creación del recaudo)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recaudo presupuestal; Reconocimiento presupuestal; Vigencia presupuestaria; Categoría presupuestal; Fuente financiera; Tipo de ingreso; Tercero; Modificación de recaudo (débito/crédito); Estado del documento (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas con GroupId/GroupName según @GroupBy y métricas InitialValue, DebitValue, CreditValue y TotalValue = InitialValue - DebitValue + CreditValue; [RAISERROR] Resultset: En caso de excepción, devuelve un resultset con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() + número de línea', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@ThirdParties,'''') <> '''' → Activa @FilterByThirdParties=1 y carga la tabla @Table_ThirdParties con los IDs parseados, restringiendo el resultado a esos terceros else No filtra por terceros (LEFT JOIN no excluye filas); si @GroupBy IN (1,2,3,4) → Selecciona el GroupId/GroupName correspondiente: 1=Tercero (Nit+Name), 2=Categoría (Code+Name), 3=Fuente Financiera, 4=Tipo de Ingreso else Retorna GroupId=0 y GroupName=''''; si cn.Status WHEN 1/2/3 → Mapea a ''Registrado''/''Confirmado''/''Anulado'' respectivamente else ''N/A''; si cmd.Nature = 1 → El valor se acumula como DebitValue else El valor se acumula como CreditValue; si ISNULL(@CollectionCode,'''') <> '''' → Filtra el resultset por cn.Code = @CollectionCode', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Collection; Budget.CollectionDetail; Budget.RecognitionDetail; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Common.ThirdParty; Budget.CollectionModification; Budget.CollectionModificationDetail; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollection';
-- GO
