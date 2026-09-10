-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-30
-- Description:	Procedimiento para el reporte de listado de modificación de reconocimientos del presupuesto de ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentIncomeRecognitionModification]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@RecognitionModificationCode VARCHAR(20),
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
				@RecognitionModificationCode = t.x.value('RecognitionModificationCode[1]','varchar(20)'),
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
				rm.Code,
				rm.DocumentDate,
				rm.Document,
				rm.Observations,
				r.Code RecognitionCode,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE rm.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				rm.EntityCode OriginCode,
				ISNULL(gend.Description, rm.EntityName) OriginName,
				c.Code CategoryCode,
				c.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				CASE rmd.Nature
					WHEN 1 THEN '(-) Débito'
					WHEN 2 THEN '(+) Crédito'
					ELSE 'N/A'
				END NatureName,
				rmd.Value,
				rd.TotalRecognition RecognitionTotal,
				rd.Balance RecognitionBalance,
				b.ExecutedValue BudgetExecutedValue,
				b.Balance BudgetBalance
		FROM Budget.RecognitionModification rm WITH (NOLOCK)
		JOIN Budget.RecognitionModificationDetail rmd WITH (NOLOCK) ON rm.Id = rmd.RecognitionModificationId
		/************************************  RECONOCIMIENTO ************************************/
		JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON rmd.RecognitionDetailId = rd.Id				
		JOIN Budget.Category c WITH (NOLOCK) ON rd.CategoryId = c.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON c.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON rd.RevenueTypeId = rt.Id
		JOIN Budget.Recognition r WITH (NOLOCK) ON rd.RecognitionId = r.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON r.ThirdPartyId = tp.Id
		/************************************** ADICIONALES **************************************/
		LEFT JOIN Budget.Budget b WITH (NOLOCK) ON rd.CategoryId = b.CategoryId AND rd.RevenueTypeId = b.RevenueTypeId
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON rm.EntityName = gend.EntityName
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(rm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@RecognitionModificationCode, '') = '' OR rm.Code = @RecognitionModificationCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY rm.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de listado de documentos de modificación de reconocimientos del presupuesto de ingresos. Dado un rango de fechas, una vigencia presupuestal y filtros opcionales por código de modificación y terceros, devuelve el detalle de cada modificación (débito o crédito) aplicada sobre reconocimientos de ingresos, mostrando el documento soporte, el estado (Registrado, Confirmado, Anulado), el tercero (NIT y nombre), la categoría presupuestal, la fuente de financiación, el tipo de renta, los valores ejecutados y saldos tanto del reconocimiento como del presupuesto. Permite agrupar los resultados por tercero, categoría, fuente de financiación o tipo de ingreso, y combina las tablas RecognitionModification, RecognitionModificationDetail, RecognitionDetail, Recognition, Category, FinancialSource, RevenueType, ThirdParty y Budget para entregar una vista consolidada de los ajustes al presupuesto de ingresos en un periodo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentIncomeRecognitionModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentIncomeRecognitionModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de modificaciones a reconocimientos del presupuesto de ingresos, agrupable por tercero, categoría, fuente financiera o tipo de ingreso, con filtros por fecha, código y terceros.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener un nodo /Data con DateStart, DateEnd, GroupBy, BudgetaryValidityId, RecognitionModificationCode y ThirdParties.; Si ThirdParties viene informado, debe ser una lista de IDs enteros separados por coma (CAST a INT en dbo.Split).; GroupBy debe ser 1 (tercero), 2 (categoría), 3 (fuente financiera) o 4 (tipo de ingreso); cualquier otro valor produce GroupId=0 y GroupName vacío.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen modificaciones que tengan al menos un detalle (RecognitionModificationDetail) y un detalle de reconocimiento asociado (INNER JOINs sobre rmd, rd, c, fs, rt, r, tp).; El filtrado de fechas siempre se aplica sobre rm.DocumentDate convertida a DATE.; El parámetro @BudgetaryValidityId se lee del XML pero no se usa para filtrar el resultado.; Todas las lecturas se hacen WITH (NOLOCK) y la consulta principal se compila con OPTION (RECOMPILE).; El resultado se ordena por rm.Id ascendente.; Los errores nunca se propagan: se capturan y se devuelven como fila con CodeResult=''999''.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reconocimiento presupuestal; Modificación de reconocimiento; Vigencia presupuestal; Categoría presupuestal; Fuente financiera; Tipo de ingreso (renta); Tercero; Naturaleza débito/crédito; Saldo y ejecución presupuestal; Estados del documento (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve el listado de modificaciones de reconocimiento cuya rm.DocumentDate (cast a DATE) esté entre @DateStart y @DateEnd, opcionalmente filtrado por código exacto y por terceros.; [RAISERROR] ResultSet: Ante cualquier excepción, retorna una fila con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() + número de línea, en lugar del set de datos.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@ThirdParties,'''') <> '''' → Activa @FilterByThirdParties=1 y carga la tabla temporal con los IDs de terceros, restringiendo el resultado a esos terceros vía LEFT JOIN + ttp.Id IS NOT NULL. else No filtra por terceros (@FilterByThirdParties=0).; si @GroupBy IN (1,2,3,4) → GroupId y GroupName se calculan sobre tercero / categoría / fuente financiera / tipo de ingreso respectivamente. else GroupId=0 y GroupName='''' (sin agrupación efectiva).; si ISNULL(@RecognitionModificationCode,'''') = '''' → No filtra por código de modificación. else Restringe a rm.Code = @RecognitionModificationCode.; si rm.Status WHEN 1/2/3 → Traduce el estado a ''Registrado'' / ''Confirmado'' / ''Anulado'' respectivamente. else Muestra ''N/A''.; si rmd.Nature WHEN 1/2 → Etiqueta el movimiento como ''(-) Débito'' o ''(+) Crédito''. else Muestra ''N/A''.; si Existe descripción en Common.GetEntityNameDescriptions para rm.EntityName → Usa gend.Description como OriginName. else Usa rm.EntityName como OriginName (ISNULL).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.RecognitionModification; Budget.RecognitionModificationDetail; Budget.RecognitionDetail; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.Recognition; Common.ThirdParty; Budget.Budget; Common.GetEntityNameDescriptions; dbo.Split', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognitionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognitionModification';
-- GO
