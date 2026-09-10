-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-30
-- Description:	Procedimiento para el reporte de listado de reconocimientos del presupuesto de ingresos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentIncomeRecognition]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@RecognitionCode VARCHAR(20),
			@ThirdParties VARCHAR(MAX),
			-------------
			@FilterByThirdParties BIT = 0

	DECLARE @Table_ThirdParties AS TABLE(Id INT)

	BEGIN TRY
		
		/*************************************** CRITERIOS ***************************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@GroupBy = t.x.value('GroupBy[1]','tinyint'),
				@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@RecognitionCode = t.x.value('RecognitionCode[1]','varchar(20)'),
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
				r.Code,
				r.DocumentDate,
				r.Document,
				r.Observations,
				CASE r.RecognitonType 
					WHEN 1 THEN 'Reconocimiento' 
					WHEN 2 THEN 'Cuenta x Cobrar' 
					WHEN 3 THEN 'Vigencia Anterior' 
					ELSE 'N/A'
				END RecognitionTypeName,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE r.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				r.EntityCode OriginCode,
				ISNULL(gend.Description, r.EntityName) OriginName,
				c.Code CategoryCode,
				c.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				rd.InitialValue,
				ISNULL(rm.DebitValue, 0) DebitValue,
				ISNULL(rm.CreditValue, 0) CreditValue,
				rd.InitialValue - ISNULL(rm.DebitValue, 0) + ISNULL(rm.CreditValue, 0) TotalValue,
				ISNULL(cn.ExecutedValue, 0) ExecutedValue,
				rd.InitialValue - ISNULL(rm.DebitValue, 0) + ISNULL(rm.CreditValue, 0) - ISNULL(cn.ExecutedValue, 0) Balance
		FROM Budget.Recognition r WITH (NOLOCK)
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON r.ThirdPartyId = tp.Id
		JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON r.Id = rd.RecognitionId
		JOIN Budget.Category c WITH (NOLOCK) ON rd.CategoryId = c.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON c.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON rd.RevenueTypeId = rt.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON r.EntityName = gend.EntityName
		/************************************  MODIFICACIONES ************************************/
		LEFT JOIN 
		(
			SELECT	rmd.RecognitionDetailId, 
					SUM(IIF(rmd.Nature = 1, rmd.Value, 0)) DebitValue, 
					SUM(IIF(rmd.Nature = 1, 0, rmd.Value)) CreditValue
			FROM Budget.RecognitionModification rm WITH (NOLOCK)
			JOIN Budget.RecognitionModificationDetail rmd WITH (NOLOCK) ON rm.Id = rmd.RecognitionModificationId
			WHERE rm.Status = 2 AND CAST(rm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY rmd.RecognitionDetailId
		) rm ON rd.Id = rm.RecognitionDetailId 
		/***************************************  RECAUDOS ***************************************/
		LEFT JOIN 
		(
			SELECT	cnd.RecognitionDetailId, 
					SUM(cnd.InitialValue + ISNULL(cm.CreditValue, 0) - ISNULL(cm.DebitValue, 0)) ExecutedValue
			FROM Budget.Collection cn WITH (NOLOCK)
			JOIN Budget.CollectionDetail cnd WITH (NOLOCK) ON cn.Id = cnd.CollectionId
			LEFT JOIN 
			(
				SELECT cmd.CollectionDetailId, SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue, SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
				FROM Budget.CollectionModification cm WITH (NOLOCK)
				JOIN Budget.CollectionModificationDetail cmd WITH (NOLOCK) ON cm.Id = cmd.CollectionModificationId
				WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY cmd.CollectionDetailId
			) cm ON cnd.Id = cm.CollectionDetailId
			WHERE cn.Status = 2 AND CAST(cn.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY cnd.RecognitionDetailId
		) cn ON rd.Id = cn.RecognitionDetailId 
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(r.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@RecognitionCode, '') = '' OR r.Code = @RecognitionCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY r.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de reconocimientos del presupuesto de ingresos para un rango de fechas y vigencia presupuestal determinados. Consolida información de los reconocimientos (tipo: reconocimiento, cuenta por cobrar o vigencia anterior) con sus detalles por categoría, fuente de financiación y tipo de ingreso/renta, junto al tercero (proveedor, aseguradora o entidad) asociado a cada documento. Para cada reconocimiento calcula el valor inicial, las modificaciones aprobadas (débitos y créditos), el valor total ajustado, los recaudos ejecutados en el período y el saldo pendiente por recaudar. Permite filtrar por fechas, código de reconocimiento y uno o varios terceros, y agrupar los resultados por tercero, categoría, fuente de financiación o tipo de ingreso.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentIncomeRecognition';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentIncomeRecognition';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de listado de reconocimientos del presupuesto de ingresos, calculando valor inicial, modificaciones (débitos/créditos), total reconocido, valor ejecutado por recaudos y saldo, agrupado según criterio seleccionado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener nodo /Data con DateStart, DateEnd y GroupBy; Si se envía ThirdParties debe ser una lista separada por comas de IDs enteros válidos; GroupBy debe estar entre 1 y 4 para obtener agrupación válida (1=Tercero, 2=Categoría, 3=Fuente Financiera, 4=Tipo de Ingreso)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran modificaciones de reconocimiento con Status=2 (Confirmado) y dentro del rango de fechas del reporte; Solo se consideran recaudos (Collection) y sus modificaciones con Status=2 (Confirmado) y DocumentDate entre @DateStart y @DateEnd; TotalValue se calcula como InitialValue - DebitValue + CreditValue de las modificaciones confirmadas; Balance = TotalValue - ExecutedValue (valor ejecutado por recaudos); ExecutedValue por detalle = suma de (InitialValue de recaudo + CreditValue - DebitValue de modificaciones de recaudo confirmadas); El reporte siempre filtra por DocumentDate del reconocimiento dentro del rango [@DateStart, @DateEnd]; Si @RecognitionCode es vacío o NULL no aplica filtro por código; si tiene valor exige coincidencia exacta; El resultado se ordena por r.Id', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reconocimiento presupuestal; Vigencia presupuestal; Tercero; Categoría presupuestal; Fuente de financiación; Tipo de ingreso; Modificación presupuestal (débito/crédito); Recaudo; Cuenta por cobrar; Vigencia anterior; Saldo de ejecución', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_ThirdParties: Cuando @ThirdParties no es vacío, se parsea con dbo.Split('','') y se insertan los IDs como INT para filtrar por terceros; [RETURN_RESULT] ResultSet: Devuelve el listado de reconocimientos cuyo DocumentDate está entre @DateStart y @DateEnd, opcionalmente filtrado por código de reconocimiento y/o lista de terceros; [RETURN_RESULT] ResultSet: En caso de error en el TRY, retorna una fila con CodeResult=''999'' y MessageResult con el mensaje y línea del error', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@ThirdParties,'''') <> '''' → Activa @FilterByThirdParties=1 e inserta los IDs en la tabla temporal para restringir resultados a esos terceros else No se aplica filtro por terceros (se traen todos); si @GroupBy = 1/2/3/4 → Define GroupId y GroupName usando ThirdParty / Category / FinancialSource / RevenueType respectivamente else GroupId=0 y GroupName='''' (sin agrupación); si r.RecognitonType IN (1,2,3) → Mapea a ''Reconocimiento'', ''Cuenta x Cobrar'' o ''Vigencia Anterior'' else Etiqueta como ''N/A''; si r.Status IN (1,2,3) → Mapea a ''Registrado'', ''Confirmado'' o ''Anulado'' else Etiqueta como ''N/A''; si rmd.Nature = 1 → El valor de la modificación se suma como DebitValue else El valor de la modificación se suma como CreditValue; si cmd.Nature = 1 → El valor de la modificación de recaudo se suma como DebitValue else El valor se suma como CreditValue', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Recognition; Common.ThirdParty; Budget.RecognitionDetail; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Common.GetEntityNameDescriptions; Budget.RecognitionModification; Budget.RecognitionModificationDetail; Budget.Collection; Budget.CollectionDetail; Budget.CollectionModification; Budget.CollectionModificationDetail; dbo.Split', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentIncomeRecognition';
-- GO
