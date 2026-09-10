-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de reintegros del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpenseReimbursementResource]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@ReimbursementResourceCode VARCHAR(20),
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
				@ReimbursementResourceCode = t.x.value('ReimbursementResourceCode[1]','varchar(20)'),
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
				rr.Code,
				rr.DocumentDate,
				rr.Document,
				rr.Observations,
				po.Code PaymentOrderCode,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE rr.Status 
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
				'(-) Débito' NatureName,
				rrd.Value
		FROM Budget.ReimbursementResource rr WITH (NOLOCK)
		JOIN Budget.ReimbursementResourceDetaill rrd WITH (NOLOCK) ON rr.Id = rrd.ReimbursementResourceId
		/************************************  RECONOCIMIENTO ************************************/
		JOIN Budget.PaymentOrderDetail pod WITH (NOLOCK) ON rrd.PaymentOrderDetailId = pod.Id
		JOIN Budget.ObligationDetail od WITH (NOLOCK) ON pod.ObligationDetailId = od.Id
		JOIN Budget.Category cat WITH (NOLOCK) ON od.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON od.RevenueTypeId = rt.Id
		JOIN Budget.PaymentOrder po WITH (NOLOCK) ON pod.PaymentOrderId = po.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON po.ThirdPartyId = tp.Id
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(rr.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@ReimbursementResourceCode, '') = '' OR rr.Code = @ReimbursementResourceCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY rr.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de documentos de reintegro de recursos del presupuesto de gastos, filtrando por rango de fechas, vigencia presupuestal, código de reintegro y terceros seleccionados. Recorre los reintegros registrados (ReimbursementResource) junto con su detalle (ReimbursementResourceDetaill), enlazando cada renglón con el detalle de la orden de pago, la obligación presupuestal, la categoría, la fuente de financiación y el tipo de renta, para obtener el contexto presupuestal completo de cada movimiento. Incluye datos del tercero beneficiario (NIT y nombre), el código de la orden de pago y el estado del reintegro (Registrado, Confirmado o Anulado). Permite agrupar los resultados por tercero, categoría presupuestal, fuente de financiación o tipo de renta, recibiendo todos los criterios de búsqueda a través de un parámetro XML.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseReimbursementResource';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseReimbursementResource';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el listado de reintegros de recursos del presupuesto de gastos, agrupado por tercero, categoría, fuente de financiación o tipo de renta, con filtros por fechas, código y terceros.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener DateStart y DateEnd válidos para filtrar el rango de DocumentDate.; Si se envía ThirdParties, debe ser una lista de IDs enteros separados por coma.; GroupBy debe tomar un valor entre 1 y 4 para producir agrupación significativa (1=Tercero, 2=Categoría, 3=Fuente Financiera, 4=Tipo de Renta).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango de fechas se aplica sobre rr.DocumentDate convertido a DATE (BETWEEN @DateStart AND @DateEnd).; Todos los registros retornados representan movimientos de naturaleza débito (''(-) Débito'').; Las lecturas se realizan con WITH (NOLOCK) en todas las tablas, permitiendo lectura sucia.; La consulta principal se compila con OPTION (RECOMPILE) por la variabilidad de filtros.; Cualquier error en el bloque TRY se captura y se devuelve como resultset con código ''999'', sin lanzar excepción al cliente.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reintegro de recursos presupuestales; Presupuesto de gastos; Vigencia presupuestal; Orden de pago; Obligación presupuestal; Categoría presupuestal; Fuente de financiación; Tipo de renta/ingreso; Tercero (NIT); Naturaleza débito', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve un conjunto con GroupId/GroupName según @GroupBy, datos del reintegro, orden de pago, tercero, categoría, fuente y tipo de renta, con naturaleza fija ''(-) Débito'' y el Value del detalle.; [RAISERROR] Resultset: Ante excepción, retorna fila con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y línea del error en lugar de propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@ThirdParties,'''') <> '''' → Activa @FilterByThirdParties=1 y carga la tabla temporal con los IDs parseados por dbo.Split, restringiendo el resultado a esos terceros (ttp.Id IS NOT NULL). else No filtra por terceros (LEFT JOIN no condiciona).; si @GroupBy = 1/2/3/4 → Define GroupId y GroupName a partir de Tercero (Nit-Name), Categoría (Code-Name), Fuente Financiera (Code-Name) o Tipo de Renta (Code-Name) respectivamente. else GroupId=0 y GroupName='''' cuando @GroupBy no coincide con 1-4.; si rr.Status IN (1,2,3) → Traduce Status a ''Registrado'', ''Confirmado'' o ''Anulado''. else StatusName=''N/A''.; si ISNULL(@ReimbursementResourceCode,'''') = '''' → No filtra por código del reintegro. else Sólo incluye registros donde rr.Code = @ReimbursementResourceCode.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.ReimbursementResource; Budget.ReimbursementResourceDetaill; Budget.PaymentOrderDetail; Budget.ObligationDetail; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.PaymentOrder; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseReimbursementResource';
-- GO
