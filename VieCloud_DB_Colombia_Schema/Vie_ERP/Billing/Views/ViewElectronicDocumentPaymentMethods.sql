CREATE VIEW Billing.ViewElectronicDocumentPaymentMethods
AS
WITH cte_Header as	(	
	SELECT	i.Id as EntityId,
			ed.EntityName,
			i.InvoiceValue Value,
			i.TotalValue,
			i.Observation,
			i.TaxDevolutionValue,
			i.InvoiceNumber as InternalInvoice,
			i.CurrencyId,
			i.ThirdPartyId,
			i.CareGroupId,
			i.AdmissionNumber,
			ed.id,
			ar.InvoiceId,
			ar.Term,
			ar.Id as  AccountReceivableId,
			ar.Balance,
			i.RevenueControlDetailId
	FROM Billing.Invoice i WITH (NOLOCK)
	JOIN Billing.ElectronicDocument ed WITH (NOLOCK) ON i.Id = ed.EntityId AND 'Invoice' = ed.EntityName
	JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON i.Id = ar.InvoiceId
)
, cte_InvoicePaymentMethods AS (
	SELECT	
		ar.InvoiceId,
		mc.PaymentMethodTypes,
		ptd.Value Value
	FROM cte_Header ar 
	JOIN Portfolio.PortfolioTransferDetail ptd WITH(NOLOCK) ON ar.AccountReceivableId = ptd.AccountReceivableId
	JOIN Portfolio.PortfolioTransfer pt WITH(NOLOCK) ON ptd.PortfolioTrasferId = pt.Id
	JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pt.PortfolioAdvanceId = pa.Id
	JOIN Treasury.CashReceipts cr WITH(NOLOCK) ON pa.CashReceiptId = cr.Id
	LEFT JOIN  Treasury.PaymentMethods mc WITH(NOLOCK) ON mc.IdCashReceipt = cr.Id  
	WHERE pt.Status = 2 AND cr.Status = 2 AND ROUND(ar.Balance, -1) = 0
	GROUP BY ar.InvoiceId, mc.PaymentMethodTypes, ptd.Value

	UNION ALL

	SELECT	ar.InvoiceId,
			mc.PaymentMethodTypes,
			crar.Value Value
	FROM cte_Header ar WITH(NOLOCK)
	JOIN Treasury.CashReceiptAccountReceivable crar WITH(NOLOCK) ON ar.AccountReceivableId = crar.AccountReceivableId
	JOIN Treasury.CashReceiptDetails crd WITH(NOLOCK) ON crar.CashReceiptDetailId = crd.Id
	JOIN Treasury.CashReceipts cr WITH(NOLOCK) ON crd.IdCashReceipt = cr.Id
	LEFT JOIN  Treasury.PaymentMethods mc WITH(NOLOCK) ON mc.IdCashReceipt = cr.Id
	WHERE cr.Status = 2 AND ROUND(ar.Balance,-1) = 0
	GROUP BY ar.InvoiceId, mc.PaymentMethodTypes, crar.Value

	UNION ALL
	--cuando el folio es tipo particular
	SELECT	ar.InvoiceId,
			99 AS PaymentMethodTypes,
			ar.Balance Value
	FROM cte_Header ar WITH(NOLOCK)
	JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.id = ar.RevenueControlDetailId
	WHERE rcd.FolioType = 3 AND ROUND(ar.Balance,-1) <> 0
)

----------------------------------------

	SELECT
		CONCAT('Invoice-',InvoiceId,'-',PaymentMethodTypes) Id,
		'Invoice' EntityName,
		InvoiceId EntityId,
		CASE PaymentMethodTypes
			WHEN 1 THEN '01'
			WHEN 2 THEN '03'
			WHEN 3 THEN '02'
			WHEN 4 THEN '04'
			ELSE '99'
		END PaymentMethodTypes,
		'' PaymentMethodOthers,
		SUM(Value) Value
	FROM cte_InvoicePaymentMethods
	GROUP BY InvoiceId, PaymentMethodTypes
UNION ALL
	SELECT
		CONCAT(EntityName,'-',EntityId) Id,
		EntityName,
		EntityId,
		'04' PaymentMethodTypes,
		'' PaymentMethodOthers,
		CAST(0 AS DECIMAL(18,2)) Value
	FROM Billing.ElectronicDocument WITH (NOLOCK)
	WHERE EntityName = 'BillingNote'
	GROUP BY EntityName, EntityId

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los métodos de pago aplicados a facturas electrónicas (vía transferencias de cartera, recibos de caja o folio particular) para reportarlos en el documento electrónico DIAN.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe tener un documento electrónico asociado en Billing.ElectronicDocument con EntityName=''Invoice''.; La factura debe tener una cuenta por cobrar en Portfolio.AccountReceivable.; Para considerar pagos por transferencia: la PortfolioTransfer y el CashReceipt deben tener Status=2 y el saldo de la cuenta por cobrar (redondeado a la decena) debe ser 0.; Para considerar pagos por aplicación directa de recibo: el CashReceipt debe tener Status=2 y el saldo (redondeado a la decena) debe ser 0.; Para incluir el saldo como pago particular: el RevenueControlDetail asociado debe tener FolioType=3 y el saldo (redondeado a la decena) distinto de 0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El saldo se considera saldado cuando ROUND(Balance,-1)=0 (tolerancia a la decena más cercana).; Solo se reconocen pagos de transferencias y recibos de caja en estado 2 (confirmado/aplicado).; Códigos de método de pago se normalizan al estándar DIAN (01, 02, 03, 04, 99).; Los folios tipo particular (FolioType=3) con saldo pendiente se reportan con método 99.; Las notas de facturación (BillingNote) siempre se reportan con método ''04'' y valor 0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura electrónica; Documento electrónico DIAN; Cuenta por cobrar; Transferencia de cartera; Anticipo de cartera; Recibo de caja; Método de pago; Folio particular; Nota de facturación (BillingNote); Saldo de factura', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve una fila por (Invoice, PaymentMethodTypes) con el valor sumado y el código DIAN mapeado; adicionalmente una fila por cada BillingNote con método ''04'' y valor 0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Pagos vía PortfolioTransfer con pt.Status=2 y cr.Status=2 y ROUND(Balance,-1)=0 → Toma método de pago de Treasury.PaymentMethods y valor desde PortfolioTransferDetail.; si Pagos vía CashReceiptAccountReceivable con cr.Status=2 y ROUND(Balance,-1)=0 → Toma método de pago de Treasury.PaymentMethods y valor desde CashReceiptAccountReceivable.; si RevenueControlDetail.FolioType=3 y ROUND(Balance,-1)<>0 (folio tipo particular con saldo) → Asigna PaymentMethodTypes=99 y reporta el saldo como valor.; si Mapeo de PaymentMethodTypes a código DIAN → 1→''01'', 2→''03'', 3→''02'', 4→''04'' else ''99''; si EntityName=''BillingNote'' en ElectronicDocument → Genera fila con método ''04'' y valor 0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.ElectronicDocument; Portfolio.AccountReceivable; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioTransfer; Portfolio.PortfolioAdvance; Treasury.CashReceipts; Treasury.PaymentMethods; Treasury.CashReceiptAccountReceivable; Treasury.CashReceiptDetails; Billing.RevenueControlDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentPaymentMethods';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentPaymentMethods';
GO
