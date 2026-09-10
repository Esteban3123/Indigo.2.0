-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-11-24
-- Description:	Procedimiento que se encarga de obtener los pagos relacionados a una factura
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[SP_GetPaymentMethodsByInvoiceId]
	@InvoiceId INT
AS
BEGIN
	
	DECLARE @IncoiceCopayId INT = NULL
	DECLARE @ConceptCollection varchar(2) = NULL

	--verifica si la fact salud tiene asociado fact copago
	IF EXISTS(SELECT 1 FROM Billing.InvoiceCopay WHERE InvoiceId = @InvoiceId) BEGIN
		
		SELECT top 1 @IncoiceCopayId = bb.InvoiceId
		FROM Billing.InvoiceCopay ic WITH(NOLOCK)
		JOIN Billing.BasicBilling bb WITH(NOLOCK) on ic.BasicBillingId = bb.Id
		WHERE ic.InvoiceId = @InvoiceId	
	END

	--Valida que la factura sea de tipo SALUD para detectar el tipo de recaudo
	IF EXISTS(SELECT 1 
				FROM Billing.Invoice WITH(NOLOCK) where Id =@InvoiceId and DocumentType in (1,2))
	BEGIN
			SELECT TOP 1 @ConceptCollection = CASE id.RecoveryFeeType 
													WHEN 2 THEN '02'
													WHEN 3 THEN '01'
													WHEN 4 THEN '03'
													ELSE '05'
												END
			FROM Billing.Invoice i with(NOLOCK)
			JOIN Billing.InvoiceDetail id WITH(NOLOCK) on i.Id =id.InvoiceId
			where i.Id =@InvoiceId
	END

	SELECT	pm.AccountReceivableType,
			pm.Code,
			pm.DocumentDate,
			pm.PaymentMethodTypes,
			pm.Value,
			CASE pm.PaymentMethodTypes
				WHEN 1 THEN '10'
				WHEN 2 THEN '20'
				WHEN 3 THEN CASE WHEN pm.CardType = 1 THEN '49' ELSE '48' END
				WHEN 4 THEN '42'
				ELSE 'ZZZ'
			END MethodTypeCode,
			@ConceptCollection as ConceptCollection
	FROM
	(
		SELECT	v.AccountReceivableType,
				v.Code, 
				v.DocumentDate,
				v.PaymentMethodTypes,
				v.Value,
				v.CardType
		FROM [Billing].[ViewPaymentMethods] v
		WHERE v.InvoiceId =COALESCE(@IncoiceCopayId,@InvoiceId) 
		
		UNION ALL

		SELECT	ar.AccountReceivableType,
				ar.Code, 
				ar.AccountReceivableDate DocumentDate,
				CAST(99 AS TINYINT) PaymentMethodTypes, 
				(ar.Value - ISNULL(v.Value, 0)) Value,
				CAST(NULL AS TINYINT) CardType
		FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
		LEFT JOIN 
		(
			SELECT v.InvoiceId, v.AccountReceivableType, SUM(v.Value) Value
			FROM [Billing].[ViewPaymentMethods] v
			GROUP BY v.InvoiceId, v.AccountReceivableType
		) v ON ar.InvoiceId = v.InvoiceId AND ar.AccountReceivableType = v.AccountReceivableType
		WHERE ar.InvoiceId = COALESCE(@IncoiceCopayId,@InvoiceId)
			AND ar.AccountReceivableType IN (4, 6)
			AND ar.Value > ISNULL(v.Value, 0)
	) pm
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene todos los métodos de pago registrados para una factura específica, identificada por su ID. Verifica si la factura de salud tiene una factura de copago asociada (cuota moderadora) y, en ese caso, consulta los pagos vinculados a esa factura de copago en lugar de la original. Determina el tipo de recaudo (concepto de cobro) según el tipo de cuota moderadora registrado en el detalle de la factura, aplicable a facturas de tipo salud. Consolida los pagos desde la vista de métodos de pago y los cruza con las cuentas por cobrar de cartera (para tipos crédito y convenio), calculando el saldo pendiente no cubierto por pagos ya registrados, y devuelve cada método de pago con su código estándar de forma de pago y el concepto de recaudo correspondiente, información utilizada principalmente para la generación de documentos electrónicos y reportes RIPS de facturación en salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetPaymentMethodsByInvoiceId';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetPaymentMethodsByInvoiceId';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los métodos de pago aplicados a una factura (incluyendo saldos pendientes en cartera) junto con sus códigos DIAN y el concepto de recaudo cuando aplica a facturas de salud.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodsByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir en Billing.Invoice para obtener concepto de recaudo.; Si la factura tiene copago asociado en Billing.InvoiceCopay, debe existir el BasicBilling vinculado para resolver la factura del copago.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodsByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El saldo pendiente solo se reporta para AccountReceivableType 4 y 6.; Cuando la factura tiene copago vinculado, los pagos se consultan sobre la factura del copago y no sobre la original.; El ConceptCollection solo aplica a facturas con DocumentType 1 o 2 (salud).; El valor de PaymentMethodTypes=99 representa saldo pendiente (cartera), no un pago efectivamente recibido.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodsByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'factura de salud; copago; método de pago; concepto de recaudo; cuota de recuperación (RecoveryFeeType); cuenta por cobrar; saldo pendiente', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodsByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewPaymentMethods: Retorna los métodos de pago registrados para la factura (o su factura de copago si existe), mapeando PaymentMethodTypes a códigos DIAN: 1→''10'', 2→''20'', 3→''48'', 4→''42'', otros→''ZZZ''.; [RETURN_RESULT] Portfolio.AccountReceivable: Adiciona (UNION ALL) por cada cuenta por cobrar con AccountReceivableType IN (4,6) cuyo Value supere lo ya pagado en ViewPaymentMethods, una fila con PaymentMethodTypes=99 y Value = ar.Value - SUM(pagos).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodsByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EXISTS en Billing.InvoiceCopay para @InvoiceId → Resuelve @IncoiceCopayId con la factura asociada vía BasicBilling y la usa como factura efectiva para consultar pagos y cuentas por cobrar. else Usa @InvoiceId directamente para consultar pagos.; si Billing.Invoice.DocumentType IN (1,2) (factura de salud) → Calcula @ConceptCollection según InvoiceDetail.RecoveryFeeType: 2→''02'', 3→''01'', 4→''03'', otros→''05''. else ConceptCollection queda NULL.; si ar.AccountReceivableType IN (4,6) AND ar.Value > ISNULL(pagos,0) → Incluye el saldo pendiente como un método de pago tipo 99.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodsByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.InvoiceCopay; Billing.BasicBilling; Billing.Invoice; Billing.InvoiceDetail; Billing.ViewPaymentMethods; Portfolio.AccountReceivable', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodsByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetPaymentMethodsByInvoiceId';
-- GO
