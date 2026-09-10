-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-30
-- Description:	Procedimiento para el reporte de listado de modificación de recaudos del presupuesto de ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentIncomeCollectionModification]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@CollectionModificationCode VARCHAR(20),
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
				@CollectionModificationCode = t.x.value('CollectionModificationCode[1]','varchar(20)'),
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
				cm.Code,
				cm.DocumentDate,
				cm.Document,
				cm.Observations,
				cn.Code CollectionCode,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE cm.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				cm.EntityCode OriginCode,
				ISNULL(gend.Description, cm.EntityName) OriginName,
				c.Code CategoryCode,
				c.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				CASE cmd.Nature
					WHEN 1 THEN '(-) Débito'
					WHEN 2 THEN '(+) Crédito'
					ELSE 'N/A'
				END NatureName,
				cmd.Value,
				cnd.Balance CollectionTotal,
				rd.ExecutedValue RecognitionExecutedValue,
				rd.Balance RecognitionBalance
		FROM Budget.CollectionModification cm WITH (NOLOCK)
		JOIN Budget.CollectionModificationDetail cmd WITH (NOLOCK) ON cm.Id = cmd.CollectionModificationId
		/************************************  RECONOCIMIENTO ************************************/
		JOIN Budget.CollectionDetail cnd WITH (NOLOCK) ON cmd.CollectionDetailId = cnd.Id
		JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON cnd.RecognitionDetailId = rd.Id
		JOIN Budget.Category c WITH (NOLOCK) ON rd.CategoryId = c.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON c.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON rd.RevenueTypeId = rt.Id
		JOIN Budget.Collection cn WITH (NOLOCK) ON cnd.CollectionId = cn.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON cn.ThirdPartyId = tp.Id
		/************************************** ADICIONALES **************************************/
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON cm.EntityName = gend.EntityName
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(cm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@CollectionModificationCode, '') = '' OR cm.Code = @CollectionModificationCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY cm.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de modificaciones de recaudos del presupuesto de ingresos. Consolida información de cada modificación de cobro (CollectionModification) con su detalle de ajuste (débito o crédito), vinculando el recaudo original, la categoría presupuestal, la fuente de financiamiento, el tipo de ingreso y el tercero asociado. Permite filtrar por rango de fechas, vigencia presupuestaria, código de modificación y uno o varios terceros, agrupando los resultados por tercero, categoría, fuente financiera o tipo de ingreso según el criterio solicitado. Se usa para auditar y controlar los ajustes realizados sobre los cobros presupuestales, mostrando el estado de cada modificación (Registrado, Confirmado o Anulado) y los saldos de reconocimiento y recaudo afectados.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentIncomeCollectionModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentIncomeCollectionModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el listado de modificaciones de recaudo del presupuesto de ingresos en un rango de fechas, agrupable por tercero, categoría, fuente o tipo de ingreso, con filtros opcionales por código y terceros.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener los nodos DateStart, DateEnd y GroupBy bajo /Data.; Si se envía ThirdParties, debe ser una lista de IDs enteros separados por comas convertibles a INT vía dbo.Split.; Las modificaciones consultadas deben tener detalle vinculado a CollectionDetail, RecognitionDetail, Category, FinancialSource, RevenueType, Collection y ThirdParty existentes (uniones internas).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nombre del origen (OriginName) usa la descripción de Common.GetEntityNameDescriptions cuando exista; si no, conserva cm.EntityName.; Cada fila representa un detalle de modificación (CollectionModificationDetail) y reporta el saldo del recaudo asociado y los valores ejecutado/saldo del reconocimiento padre.; El filtro por fechas siempre se aplica sobre cm.DocumentDate convertido a DATE (ignora la hora).; Toda la consulta usa NOLOCK, por lo que admite lecturas sucias sobre las tablas presupuestales.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de recaudo presupuestal; Recaudo / Colección presupuestaria; Reconocimiento presupuestal; Categoría presupuestal; Fuente de financiación; Tipo de ingreso (renta); Tercero; Vigencia presupuestal; Naturaleza débito/crédito; Estado del documento (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve el listado de modificaciones cuyo cm.DocumentDate (cast a DATE) esté entre @DateStart y @DateEnd, opcionalmente filtrado por @CollectionModificationCode y por la lista de terceros, ordenado por cm.Id.; [RETURN_RESULT] ResultSet: En caso de excepción, retorna una fila con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() concatenado al número de línea.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@ThirdParties,'''') <> '''' → Activa @FilterByThirdParties=1 y carga la tabla temporal @Table_ThirdParties con los IDs derivados de dbo.Split, restringiendo el resultado a terceros presentes en esa lista. else No se aplica filtro por terceros y se devuelven todas las modificaciones del rango.; si @GroupBy = 1/2/3/4 → Define GroupId y GroupName tomando respectivamente Tercero (Nit-Name), Categoría (Code-Name), FinancialSource (Code-Name) o RevenueType (Code-Name). else GroupId=0 y GroupName=''''.; si cm.Status = 1/2/3 → Etiqueta el estado como ''Registrado'', ''Confirmado'' o ''Anulado'' respectivamente. else Etiqueta el estado como ''N/A''.; si cmd.Nature = 1/2 → Etiqueta la naturaleza del movimiento como ''(-) Débito'' o ''(+) Crédito''. else Etiqueta la naturaleza como ''N/A''.; si ISNULL(@CollectionModificationCode,'''') = '''' → No se filtra por código. else Solo se incluyen modificaciones cuyo Code coincide exactamente con @CollectionModificationCode.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.CollectionModification; Budget.CollectionModificationDetail; Budget.CollectionDetail; Budget.RecognitionDetail; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.Collection; Common.ThirdParty; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeCollectionModification';
-- GO
