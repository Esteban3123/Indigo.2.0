-- =============================================
-- Author:		Andrea Pahola Coqueco Cuellar
-- Create date: 2025-09-29
-- Description:	Vista encargada de establecer los métodos de pago para los documentos electrónicos
-- =============================================

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