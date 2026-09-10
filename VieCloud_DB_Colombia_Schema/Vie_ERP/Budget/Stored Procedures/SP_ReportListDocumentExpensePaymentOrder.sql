-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de ordenes de pago del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpensePaymentOrder]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@PaymentOrderCode VARCHAR(20),
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
				@PaymentOrderCode = t.x.value('PaymentOrderCode[1]','varchar(20)'),
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
				po.Code,
				po.DocumentDate,
				pod.ExpiredDate ExpirationDate,
				po.Document,
				po.Observations,
				CASE po.PaymentOrderType
					WHEN 1 THEN 'Orden de Pago' 
					ELSE 'N/A'
				END PaymentOrderTypeName,
				tp.Nit ThirdPartyNit,
				tp.Name ThirdPartyName,
				CASE po.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				po.EntityCode OriginCode,
				ISNULL(gend.Description, po.EntityName) OriginName,
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				pod.InitialValue,
				ISNULL(rr.DebitValue, 0) DebitValue,
				ISNULL(rr.CreditValue, 0) CreditValue,
				pod.InitialValue - ISNULL(rr.DebitValue, 0) + ISNULL(rr.CreditValue, 0) TotalValue
		FROM Budget.PaymentOrder po WITH (NOLOCK)
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON po.ThirdPartyId = tp.Id
		JOIN Budget.PaymentOrderDetail pod WITH (NOLOCK) ON po.Id = pod.PaymentOrderId
		JOIN Budget.ObligationDetail od WITH (NOLOCK) ON pod.ObligationDetailId = od.Id
		JOIN Budget.Category cat WITH (NOLOCK) ON od.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON od.RevenueTypeId = rt.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON po.EntityName = gend.EntityName
		/************************************  MODIFICACIONES ************************************/
		LEFT JOIN 
		(
			SELECT rrd.PaymentOrderDetailId, SUM(rrd.Value) DebitValue, 0 CreditValue
			FROM Budget.ReimbursementResource rr WITH (NOLOCK)
			JOIN Budget.ReimbursementResourceDetaill rrd WITH (NOLOCK) ON rr.Id = rrd.ReimbursementResourceId
			WHERE rr.Status = 2 AND CAST(rr.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY rrd.PaymentOrderDetailId
		) rr ON pod.Id = rr.PaymentOrderDetailId 
		/**************************************** FILTROS ****************************************/
		LEFT JOIN @Table_ThirdParties ttp ON tp.Id = ttp.Id
		WHERE CAST(po.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@PaymentOrderCode, '') = '' OR po.Code = @PaymentOrderCode)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
		ORDER BY po.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de órdenes de pago del presupuesto de gastos para un rango de fechas y vigencia presupuestal determinados. Cruza las órdenes de pago con sus detalles, la obligación presupuestal, la categoría del gasto, la fuente de financiación, el tipo de renta y el tercero beneficiario (proveedor o contratista), permitiendo filtrar por código de orden de pago y por uno o varios terceros. Calcula el valor inicial de cada renglón y le aplica los movimientos de débito y crédito provenientes de los recursos de reembolso confirmados, para mostrar el valor total actualizado. Admite agrupación de resultados por tercero, categoría, fuente financiera o tipo de renta, facilitando el análisis y seguimiento de la ejecución presupuestal de egresos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpensePaymentOrder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpensePaymentOrder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de listado de órdenes de pago del presupuesto de gastos, agrupable por tercero, categoría, fuente de financiación o tipo de renta, con cálculo de débitos, créditos y valor total.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpensePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener al menos DateStart y DateEnd para acotar el rango de DocumentDate.; Si se envía ThirdParties, debe ser una lista de IDs separados por coma convertibles a INT.; GroupBy debe estar en {1,2,3,4} para producir GroupId/GroupName con valor; otro valor produce 0/cadena vacía.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpensePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El TotalValue se calcula siempre como InitialValue - DebitValue + CreditValue, tratando como 0 los valores nulos del subquery de reembolsos.; Solo se consideran reembolsos con Status=2 (confirmados) y cuya DocumentDate esté dentro del mismo rango [@DateStart,@DateEnd] del reporte.; El nombre de origen (OriginName) prefiere la descripción provista por Common.GetEntityNameDescriptions sobre po.EntityName.; El reporte recorre únicamente órdenes con detalle (PaymentOrderDetail), obligación, categoría, fuente y tipo de renta existentes (INNER JOIN).; El resultado se ordena siempre por po.Id ascendente.; Toda la consulta usa NOLOCK; no realiza modificaciones a tablas persistentes.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpensePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de pago presupuestal; Tercero/proveedor; Vigencia presupuestal; Categoría presupuestal; Fuente de financiación; Tipo de renta/ingreso; Obligación presupuestal; Reembolso de recursos; Débito y crédito presupuestal; Estado de orden (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpensePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve una fila por cada detalle de orden de pago cuyo po.DocumentDate esté entre @DateStart y @DateEnd, opcionalmente filtrado por código de orden y por terceros.; [RETURN_RESULT] ResultSet: En caso de error captura ERROR_MESSAGE y ERROR_LINE y devuelve un único registro con CodeResult=''999'' y MessageResult.; [INSERT] @Table_ThirdParties: Cuando @ThirdParties no es vacío, se hace split por coma e inserta cada Id en la tabla temporal y se activa @FilterByThirdParties=1.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpensePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@ThirdParties,'''') <> '''' → Activa el filtro por terceros (@FilterByThirdParties=1) y carga @Table_ThirdParties con los IDs. else No se filtra por terceros (todas las órdenes pasan el filtro).; si @GroupBy = 1/2/3/4 → Define GroupId y GroupName a partir de Tercero (Nit-Name), Categoría (Code-Name), Fuente Financiera (Code-Name) o Tipo de Renta (Code-Name) respectivamente. else GroupId=0 y GroupName='''' (sin agrupación efectiva).; si po.PaymentOrderType = 1 → PaymentOrderTypeName = ''Orden de Pago''. else ''N/A''.; si po.Status IN (1,2,3) → StatusName = ''Registrado'' / ''Confirmado'' / ''Anulado'' respectivamente. else ''N/A''.; si ISNULL(@PaymentOrderCode,'''') = '''' OR po.Code = @PaymentOrderCode → Se incluye la orden si no se filtra por código o coincide exactamente con el código indicado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpensePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.PaymentOrder; Common.ThirdParty; Budget.PaymentOrderDetail; Budget.ObligationDetail; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Common.GetEntityNameDescriptions; Budget.ReimbursementResource; Budget.ReimbursementResourceDetaill; dbo.Split', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpensePaymentOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpensePaymentOrder';
-- GO
