-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de obligaciones del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpenseObligation]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@ObligationCode VARCHAR(20),
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
				@ObligationCode = t.x.value('ObligationCode[1]','varchar(20)'),
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
				o.Code,
				o.DocumentDate,
				od.ExpiredDate ExpirationDate,
				o.Document,
				o.Observations,
				CASE o.ObligationType
					WHEN 1 THEN 'Obligación' 
					WHEN 2 THEN 'Cuenta por Pagar' 
					ELSE 'N/A'
				END ObligationTypeName,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE o.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				od.EntityCode OriginCode,
				ISNULL(gend.Description, od.EntityName) OriginName,
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				od.InitialValue,
				ISNULL(om.DebitValue, 0) DebitValue,
				ISNULL(om.CreditValue, 0) CreditValue,
				od.InitialValue - ISNULL(om.DebitValue, 0) + ISNULL(om.CreditValue, 0) TotalValue,
				ISNULL(po.ExecutedValue, 0) ExecutedValue,
				od.InitialValue - ISNULL(om.DebitValue, 0) + ISNULL(om.CreditValue, 0) - ISNULL(po.ExecutedValue, 0) Balance
		FROM Budget.Obligation o WITH (NOLOCK)
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON o.ThirdPartyId = tp.Id
		JOIN Budget.ObligationDetail od WITH (NOLOCK) ON o.Id = od.ObligationId
		JOIN Budget.Category cat WITH (NOLOCK) ON od.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON od.RevenueTypeId = rt.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON od.EntityName = gend.EntityName
		/************************************  MODIFICACIONES ************************************/
		LEFT JOIN 
		(
			SELECT	omd.ObligationDetailId, SUM(IIF(omd.Nature = 1, omd.Value, 0)) DebitValue, SUM(IIF(omd.Nature = 1, 0, omd.Value)) CreditValue
			FROM Budget.ObligationModification om WITH (NOLOCK)
			JOIN Budget.ObligationModificationDetail omd WITH (NOLOCK) ON om.Id = omd.ObligationModificationId
			WHERE om.Status = 2 AND CAST(om.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY omd.ObligationDetailId
		) om ON od.Id = om.ObligationDetailId 
		/************************************ ORDENES DE PAGO ************************************/
		LEFT JOIN 
		(
			SELECT	pod.ObligationDetailId, SUM(pod.InitialValue + ISNULL(rr.CreditValue, 0) - ISNULL(rr.DebitValue, 0)) ExecutedValue
			FROM Budget.PaymentOrder po WITH (NOLOCK)
			JOIN Budget.PaymentOrderDetail pod WITH (NOLOCK) ON po.Id = pod.PaymentOrderId
			LEFT JOIN 
			(
				SELECT rrd.PaymentOrderDetailId, SUM(rrd.Value) DebitValue, 0 CreditValue
				FROM Budget.ReimbursementResource rr WITH (NOLOCK)
				JOIN Budget.ReimbursementResourceDetaill rrd WITH (NOLOCK) ON rr.Id = rrd.ReimbursementResourceId
				WHERE rr.Status = 2 AND CAST(rr.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY rrd.PaymentOrderDetailId
			) rr ON pod.Id = rr.PaymentOrderDetailId
			WHERE po.Status = 2 AND CAST(po.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY pod.ObligationDetailId
		) po ON od.Id = po.ObligationDetailId 
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(o.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@ObligationCode, '') = '' OR o.Code = @ObligationCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY o.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de obligaciones del presupuesto de gastos para un rango de fechas y vigencia presupuestal determinados. Consolida información de obligaciones (compromisos formales de gasto) junto con sus terceros (proveedores o contratistas), categorías presupuestales, fuentes de financiación y tipos de ingreso, calculando para cada detalle de obligación los valores iniciales, modificaciones aprobadas (débitos y créditos) y el valor ejecutado mediante órdenes de pago, para obtener el saldo pendiente por pagar. Permite filtrar por rango de fechas, código de obligación y uno o varios terceros específicos, y agrupar los resultados por tercero, categoría, fuente financiera o tipo de renta, siendo útil para el seguimiento y control del gasto comprometido versus ejecutado en la gestión presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseObligation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseObligation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de listado de obligaciones del presupuesto de gastos en un rango de fechas, mostrando valores iniciales, modificaciones (débitos/créditos), valor ejecutado y saldo, agrupable por tercero, categoría, fuente de financiación o tipo de renta.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener el nodo /Data con DateStart y DateEnd para acotar el rango (se filtra por o.DocumentDate entre @DateStart y @DateEnd).; Si se envía ThirdParties, debe ser una lista de IDs separados por coma convertibles a INT.; El criterio GroupBy debe ser 1 (Tercero), 2 (Categoría), 3 (Fuente Financiera) o 4 (Tipo de Renta); cualquier otro valor retorna GroupId=0 y GroupName vacío.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera modificaciones, órdenes de pago y reembolsos con Status=2 (Confirmado) para los cálculos de valores ejecutados y modificados.; Tanto las modificaciones como las órdenes de pago y reembolsos se filtran por su DocumentDate dentro del mismo rango [@DateStart, @DateEnd] que las obligaciones.; El TotalValue de una obligación se calcula como InitialValue - DebitValue + CreditValue de sus modificaciones confirmadas.; El Balance disponible es TotalValue - ExecutedValue (lo ya pagado mediante órdenes de pago netas de reembolsos).; Los nombres de ObligationType (Obligación/Cuenta por Pagar) y Status (Registrado/Confirmado/Anulado) se traducen desde códigos numéricos; valores fuera del dominio se muestran como ''N/A''.; El nombre de origen (OriginName) prioriza la descripción de Common.GetEntityNameDescriptions sobre od.EntityName.; Los errores no se propagan: se capturan en CATCH y se devuelven como resultset con CodeResult=''999''.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Obligación presupuestal; Cuenta por Pagar; Vigencia presupuestal; Tercero (proveedor/contratista); Categoría presupuestal; Fuente de financiación; Tipo de renta; Modificación de obligación (débito/crédito); Orden de pago; Reembolso de recursos; Saldo disponible; Valor ejecutado', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve el listado de obligaciones cuyo o.DocumentDate está entre @DateStart y @DateEnd, con totales calculados como InitialValue - DebitValue + CreditValue y Balance = TotalValue - ExecutedValue.; [RETURN_RESULT] ResultSet: En caso de error, retorna una fila con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() + número de línea.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@ThirdParties,'''') <> '''' → Activa @FilterByThirdParties=1 y carga IDs en @Table_ThirdParties para restringir el reporte a esos terceros (ttp.Id IS NOT NULL). else No filtra por terceros; incluye todas las obligaciones del rango.; si @GroupBy = 1/2/3/4 → Define GroupId y GroupName usando ThirdParty (Nit+Name), Category (Code+Name), FinancialSource (Code+Name) o RevenueType (Code+Name) respectivamente. else GroupId=0 y GroupName=''''.; si ISNULL(@ObligationCode,'''') = '''' → No filtra por código de obligación. else Filtra exactamente por o.Code = @ObligationCode.; si om.Status = 2 AND modif.DocumentDate en rango → Suma como DebitValue cuando omd.Nature=1 y como CreditValue en caso contrario, agrupado por ObligationDetailId.; si po.Status = 2 AND po.DocumentDate en rango → Calcula ExecutedValue = SUM(InitialValue + reembolsos.CreditValue - reembolsos.DebitValue) por detalle de obligación.; si rr.Status = 2 AND rr.DocumentDate en rango → Suma rrd.Value como DebitValue del reembolso (CreditValue forzado a 0).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Obligation; Common.ThirdParty; Budget.ObligationDetail; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.ObligationModification; Budget.ObligationModificationDetail; Budget.PaymentOrder; Budget.PaymentOrderDetail; Budget.ReimbursementResource; Budget.ReimbursementResourceDetaill', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseObligation';
-- GO
