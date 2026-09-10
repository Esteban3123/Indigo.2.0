-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de compromisos del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpenseCommitment]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@CommitmentCode VARCHAR(20),
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
				@CommitmentCode = t.x.value('CommitmentCode[1]','varchar(20)'),
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
				c.Code,
				c.DocumentDate,
				cd.ExpiredDate ExpirationDate,
				c.Document,
				c.Observations,
				CASE c.CommitmentType
					WHEN 1 THEN 'Compromiso' 
					WHEN 2 THEN 'Reserva' 
					ELSE 'N/A'
				END CommitmentTypeName,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE c.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				c.EntityCode OriginCode,
				ISNULL(gend.Description, c.EntityName) OriginName,
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				cd.InitialValue,
				ISNULL(cm.DebitValue, 0) DebitValue,
				ISNULL(cm.CreditValue, 0) CreditValue,
				cd.InitialValue - ISNULL(cm.DebitValue, 0) + ISNULL(cm.CreditValue, 0) TotalValue,
				ISNULL(o.ExecutedValue, 0) ExecutedValue,
				cd.InitialValue - ISNULL(cm.DebitValue, 0) + ISNULL(cm.CreditValue, 0) - ISNULL(o.ExecutedValue, 0) Balance
		FROM Budget.Commitment c WITH (NOLOCK)
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON c.ThirdPartyId = tp.Id
		JOIN Budget.CommitmentDetail cd WITH (NOLOCK) ON c.Id = cd.CommitmentId
		JOIN Budget.Category cat WITH (NOLOCK) ON cd.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON cd.RevenueTypeId = rt.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON c.EntityName = gend.EntityName
		/************************************  MODIFICACIONES ************************************/
		LEFT JOIN 
		(
			SELECT	cmd.CommitmentDetailId, SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue, SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
			FROM Budget.CommitmentModification cm WITH (NOLOCK)
			JOIN Budget.CommitmentModificationDetail cmd WITH (NOLOCK) ON cm.Id = cmd.CommitmentModificationId
			WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY cmd.CommitmentDetailId
		) cm ON cd.Id = cm.CommitmentDetailId 
		/*************************************  OBLIGACIONES *************************************/
		LEFT JOIN 
		(
			SELECT	od.CommitmentDetailId, SUM(od.InitialValue + ISNULL(om.CreditValue, 0) - ISNULL(om.DebitValue, 0)) ExecutedValue
			FROM Budget.Obligation o WITH (NOLOCK)
			JOIN Budget.ObligationDetail od WITH (NOLOCK) ON o.Id = od.ObligationId
			LEFT JOIN 
			(
				SELECT omd.ObligationDetailId, SUM(IIF(omd.Nature = 1, omd.Value, 0)) DebitValue, SUM(IIF(omd.Nature = 1, 0, omd.Value)) CreditValue
				FROM Budget.ObligationModification om WITH (NOLOCK)
				JOIN Budget.ObligationModificationDetail omd WITH (NOLOCK) ON om.Id = omd.ObligationModificationId
				WHERE om.Status = 2 AND CAST(om.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY omd.ObligationDetailId
			) om ON od.Id = om.ObligationDetailId
			WHERE o.Status = 2 AND CAST(o.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY od.CommitmentDetailId
		) o ON cd.Id = o.CommitmentDetailId 
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(c.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@CommitmentCode, '') = '' OR c.Code = @CommitmentCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY c.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de compromisos del presupuesto de gastos para un rango de fechas y una vigencia presupuestal. Consolida datos de compromisos (tipo compromiso o reserva) con su tercero (proveedor o contratista), categoría presupuestal, fuente de financiación y tipo de ingreso, calculando para cada detalle el valor inicial, las modificaciones aprobadas (débitos y créditos), el valor total comprometido, el valor ejecutado a través de obligaciones y el saldo disponible. Permite filtrar por código de compromiso y por terceros específicos, y agrupar los resultados por tercero, categoría, fuente de financiación o tipo de ingreso para facilitar el análisis presupuestal de egresos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseCommitment';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseCommitment';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de listado de compromisos del presupuesto de gastos, agrupado por tercero, categoría, fuente de financiación o tipo de renta, con sus modificaciones, obligaciones ejecutadas y saldo disponible.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener nodo /Data con DateStart, DateEnd y GroupBy.; Si se envía ThirdParties, debe ser una lista de IDs separados por coma convertibles a INT.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran modificaciones de compromiso con Status=2 (confirmadas) y DocumentDate dentro del rango.; Solo se consideran obligaciones con Status=2 y DocumentDate dentro del rango, así como sus modificaciones con Status=2 dentro del rango.; El valor ejecutado de una obligación se calcula como InitialValue + CreditValue - DebitValue de sus modificaciones.; El saldo del compromiso es InitialValue del detalle ajustado por modificaciones (débitos/créditos) menos el valor ejecutado por obligaciones.; Si EntityName tiene descripción en GetEntityNameDescriptions se usa esa; si no, se usa el EntityName del compromiso.; Todas las lecturas se hacen con NOLOCK.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Compromiso presupuestal; Reserva presupuestal; Obligación presupuestal; Modificación presupuestal (débito/crédito); Vigencia presupuestaria; Categoría presupuestal; Fuente de financiación; Tipo de renta; Tercero (NIT); Saldo de compromiso; Valor ejecutado', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve el listado de compromisos cuyo DocumentDate está entre @DateStart y @DateEnd, opcionalmente filtrado por código de compromiso y/o lista de terceros, calculando TotalValue = InitialValue - DebitValue + CreditValue y Balance = TotalValue - ExecutedValue.; [RAISERROR] Resultset: Ante error en TRY, retorna fila con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y línea.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@ThirdParties,'''') <> '''' → Activa @FilterByThirdParties=1 y carga los IDs en @Table_ThirdParties; el resultado se restringe a esos terceros. else No se filtra por terceros (ttp.Id puede ser NULL en el LEFT JOIN).; si @GroupBy IN (1,2,3,4) → Define GroupId/GroupName por Tercero (1), Categoría (2), Fuente Financiera (3) o Tipo de Renta (4). else GroupId=0 y GroupName=''''.; si c.CommitmentType = 1 / 2 / otro → Etiqueta como ''Compromiso'', ''Reserva'' o ''N/A''.; si c.Status = 1 / 2 / 3 / otro → Etiqueta como ''Registrado'', ''Confirmado'', ''Anulado'' o ''N/A''.; si cmd.Nature = 1 (en modificaciones de compromiso y obligación) → El valor se suma como DebitValue; en caso contrario se suma como CreditValue.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Commitment; Common.ThirdParty; Budget.CommitmentDetail; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.CommitmentModification; Budget.CommitmentModificationDetail; Budget.Obligation; Budget.ObligationDetail; Budget.ObligationModification; Budget.ObligationModificationDetail', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseCommitment';
-- GO
