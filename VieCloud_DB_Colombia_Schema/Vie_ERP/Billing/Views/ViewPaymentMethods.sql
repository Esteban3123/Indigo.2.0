

CREATE VIEW [Billing].[ViewPaymentMethods]
AS
SELECT	ar.InvoiceId, ar.AccountReceivableType, ar.Code, ar.DocumentDate,
		MAX(ar.PaymentMethodTypes) PaymentMethodTypes,
		MAX(ar.CardType) CardType,
		SUM(ar.Value) Value, ar.CurrencyId
FROM
(
	SELECT	ar.InvoiceId,
			ar.AccountReceivableType,
			cr.Code, 
			cr.DocumentDate,
			pm.PaymentMethodTypes,
			pm.CardType,
			CAST(SUM(ptd.Value) AS DECIMAL(18,2)) Value,
			pa.CurrencyId
	FROM Portfolio.AccountReceivable ar WITH(NOLOCK)
	JOIN Portfolio.PortfolioTransferDetail ptd WITH(NOLOCK) ON ar.Id = ptd.AccountReceivableId
	JOIN Portfolio.PortfolioTransfer pt WITH(NOLOCK) ON ptd.PortfolioTrasferId = pt.Id
	JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pt.PortfolioAdvanceId = pa.Id
	JOIN Treasury.CashReceipts cr WITH(NOLOCK) ON pa.CashReceiptId = cr.Id
	JOIN 
	(
		SELECT
			IdCashReceipt,
			MAX(PaymentMethodTypes) PaymentMethodTypes,
			MAX(CardType) CardType
		FROM Treasury.PaymentMethods WITH(NOLOCK)
		GROUP BY IdCashReceipt
	) pm ON cr.Id = pm.IdCashReceipt
	WHERE (pt.Status = 2 AND cr.Status = 2) OR (ar.status = cr.status)  -- por anulación la factura y el recibo deben estar anulados, pero se necesita el registro para el xml de las notas
	GROUP BY ar.InvoiceId, ar.AccountReceivableType, cr.Code, cr.DocumentDate, pm.PaymentMethodTypes, pm.CardType, pa.CurrencyId

	UNION ALL

	SELECT	ar.InvoiceId,
			ar.AccountReceivableType,
			cr.Code, 
			cr.DocumentDate,
			pm.PaymentMethodTypes,
			pm.CardType,
			CAST(SUM(crar.Value) AS DECIMAL(18,2)) Value,
			ar.CurrencyId
	FROM Portfolio.AccountReceivable ar WITH(NOLOCK)
	JOIN Treasury.CashReceiptAccountReceivable crar WITH(NOLOCK) ON ar.Id = crar.AccountReceivableId
	JOIN Treasury.CashReceiptDetails crd WITH(NOLOCK) ON crar.CashReceiptDetailId = crd.Id
	JOIN Treasury.CashReceipts cr WITH(NOLOCK) ON crd.IdCashReceipt = cr.Id
	JOIN 
	(
		SELECT
			IdCashReceipt,
			MAX(PaymentMethodTypes) PaymentMethodTypes,
			MAX(CardType) CardType
		FROM Treasury.PaymentMethods WITH(NOLOCK)
		GROUP BY IdCashReceipt
	) pm ON cr.Id = pm.IdCashReceipt
	WHERE cr.Status = 2  OR (ar.status = cr.status) -- por anulación la factura y el recibo deben estar anulados, pero se necesita el registro para el xml de las notas
	GROUP BY ar.InvoiceId, ar.AccountReceivableType, cr.Code, cr.DocumentDate, pm.PaymentMethodTypes,pm.CardType,ar.CurrencyId
) ar
GROUP BY ar.InvoiceId, ar.AccountReceivableType, ar.Code, ar.DocumentDate,ar.CurrencyId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los métodos de pago aplicados a cada factura o cuenta por cobrar. Integra dos flujos de recaudo: pagos canalizados mediante anticipos y traslados de cartera, y pagos aplicados directamente a cuentas por cobrar a través de recibos de caja. Para cada factura muestra el recibo de caja asociado, su fecha, el tipo de medio de pago utilizado (efectivo, cheque, tarjeta, depósito u otro), el valor total abonado y la moneda. Sirve para reportería de tesorería y cartera que necesite conocer cómo se pagó una factura: qué medio de pago se usó, en qué recibo de caja quedó registrado y por cuánto valor.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewPaymentMethods';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewPaymentMethods';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por factura y moneda los pagos aplicados a cuentas por cobrar, sumando valores y reportando el método de pago predominante, tanto vía transferencias de cartera como aplicaciones directas de recibos de caja.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las transferencias de cartera (PortfolioTransfer) deben estar en Status = 2 para ser consideradas.; Los recibos de caja (CashReceipts) deben estar en Status = 2 (estado válido/aplicado) para ser considerados.; Debe existir al menos un registro en Treasury.PaymentMethods asociado al recibo de caja para que se obtenga el método de pago.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consolidan pagos cuyo recibo de caja esté en Status = 2.; Solo se consideran transferencias de cartera con Status = 2.; Cuando un recibo tiene varios métodos de pago, se reporta únicamente el de mayor código (MAX).; Los valores se redondean/castean a DECIMAL(18,2) antes de la consolidación final.; La vista combina dos orígenes de pago (transferencias de cartera y aplicación directa por recibo de caja) mediante UNION ALL, por lo que un mismo pago aplicado por ambas vías se sumaría dos veces.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por cobrar; Factura; Recibo de caja; Método de pago; Anticipo de cartera; Transferencia de cartera; Moneda; Tesorería', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewPaymentMethods: Devuelve una fila por combinación de InvoiceId, AccountReceivableType, Code, DocumentDate y CurrencyId, con la SUMA de Value y el MAX de PaymentMethodTypes.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen del pago vía transferencia de cartera (PortfolioTransfer.Status=2 y CashReceipts.Status=2) → Suma ptd.Value desde PortfolioTransferDetail y toma CurrencyId de PortfolioAdvance. else En la rama UNION ALL, cuando el pago se aplica directamente desde un recibo de caja a la cuenta por cobrar (CashReceipts.Status=2), suma crar.Value desde CashReceiptAccountReceivable y toma CurrencyId de AccountReceivable.; si Un recibo de caja tiene múltiples métodos de pago en Treasury.PaymentMethods → Se selecciona el MAX(PaymentMethodTypes) por IdCashReceipt como método representativo.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioTransfer; Portfolio.PortfolioAdvance; Treasury.CashReceipts; Treasury.PaymentMethods; Treasury.CashReceiptAccountReceivable; Treasury.CashReceiptDetails', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewPaymentMethods';
GO
