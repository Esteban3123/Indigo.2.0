-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de modificación de compromisos del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpenseCommitmentModification]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@CommitmentModificationCode VARCHAR(20),
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
				@CommitmentModificationCode = t.x.value('CommitmentModificationCode[1]','varchar(20)'),
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
				cm.Code,
				cm.DocumentDate,
				cm.Document,
				cm.Observations,
				c.Code CommitmentCode,
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
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				CASE cmd.Nature
					WHEN 1 THEN '(-) Débito'
					WHEN 2 THEN '(+) Crédito'
					ELSE 'N/A'
				END NatureName,
				cmd.Value
		FROM Budget.CommitmentModification cm WITH (NOLOCK)
		JOIN Budget.CommitmentModificationDetail cmd WITH (NOLOCK) ON cm.Id = cmd.CommitmentModificationId
		/************************************  RECONOCIMIENTO ************************************/
		JOIN Budget.CommitmentDetail cd WITH (NOLOCK) ON cmd.CommitmentDetailId = cd.Id
		JOIN Budget.Category cat WITH (NOLOCK) ON cd.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON cd.RevenueTypeId = rt.Id
		JOIN Budget.Commitment c WITH (NOLOCK) ON cd.CommitmentId = c.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON c.ThirdPartyId = tp.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON cm.EntityName = gend.EntityName
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(cm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@CommitmentModificationCode, '') = '' OR cm.Code = @CommitmentModificationCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY cm.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de modificaciones a compromisos del presupuesto de gastos, permitiendo consultar los ajustes (débitos y créditos) realizados sobre compromisos existentes dentro de una vigencia presupuestal. Filtra por rango de fechas, código de modificación y terceros (proveedores o contratistas), y permite agrupar los resultados por tercero, categoría presupuestal, fuente de financiación o tipo de ingreso. Integra datos de las modificaciones y su detalle con la información del compromiso original, la categoría, la fuente financiera, el tipo de renta y el tercero involucrado, mostrando el estado del trámite (Registrado, Confirmado, Anulado) y el origen del documento. Es utilizado para auditoría, control presupuestal y seguimiento de cambios en los compromisos de gasto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseCommitmentModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseCommitmentModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte agrupable de modificaciones a compromisos del presupuesto de gastos, filtrado por rango de fechas, código, terceros y agrupado por tercero, categoría, fuente o tipo de ingreso.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener nodo /Data con DateStart, DateEnd y GroupBy válidos; Si se envía ThirdParties debe ser una lista de IDs enteros separados por comas convertibles a INT', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte solo incluye modificaciones cuyo compromiso tenga detalle con categoría, fuente financiera, tipo de ingreso, compromiso padre y tercero existentes (INNER JOIN en cadena); El nombre del origen prioriza la descripción de Common.GetEntityNameDescriptions sobre cm.EntityName; Los estados de modificación se interpretan como 1=Registrado, 2=Confirmado, 3=Anulado; La naturaleza del movimiento se interpreta como 1=Débito (resta) y 2=Crédito (suma); El filtrado por fecha se hace sobre la parte DATE de cm.DocumentDate; Se usa OPTION (RECOMPILE) y NOLOCK en todas las tablas leídas', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de compromiso presupuestal; Compromiso de gasto; Categoría presupuestal; Fuente de financiación; Tipo de ingreso/renta; Tercero; Vigencia presupuestal; Naturaleza débito/crédito; Estado de documento (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas de modificaciones de compromiso cuyo cm.DocumentDate (cast a DATE) esté entre @DateStart y @DateEnd, filtrado opcionalmente por código de modificación y por terceros; [RETURN_RESULT] Resultset: Ante excepción retorna un único resultado con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y línea', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@ThirdParties,'''') <> '''' → Activa @FilterByThirdParties=1 e inserta los IDs parseados en @Table_ThirdParties para filtrar por INNER MATCH efectivo (ttp.Id IS NOT NULL) else No se aplica filtro por terceros (LEFT JOIN no restringe); si @GroupBy = 1/2/3/4 → Define GroupId y GroupName desde Tercero (1), Categoría (2), Fuente Financiera (3) o Tipo de Ingreso (4) respectivamente else GroupId=0 y GroupName='''' cuando @GroupBy no coincide con 1-4; si cm.Status IN (1,2,3) → Mapea Status a ''Registrado'' (1), ''Confirmado'' (2), ''Anulado'' (3) else StatusName=''N/A''; si cmd.Nature IN (1,2) → Mapea Nature a ''(-) Débito'' (1) o ''(+) Crédito'' (2) else NatureName=''N/A''; si ISNULL(@CommitmentModificationCode,'''') = '''' → No aplica filtro por código else Restringe a cm.Code = @CommitmentModificationCode', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.CommitmentModification; Budget.CommitmentModificationDetail; Budget.CommitmentDetail; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.Commitment; Common.ThirdParty; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitmentModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitmentModification';
-- GO
