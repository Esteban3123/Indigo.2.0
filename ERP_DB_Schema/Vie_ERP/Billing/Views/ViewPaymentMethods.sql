

CREATE VIEW [Billing].[ViewPaymentMethods]
AS
SELECT	ar.InvoiceId, ar.AccountReceivableType, ar.Code, ar.DocumentDate,
		MAX(ar.PaymentMethodTypes) PaymentMethodTypes,
		SUM(ar.Value) Value, ar.CurrencyId
FROM
(
	SELECT	ar.InvoiceId,
			ar.AccountReceivableType,
			cr.Code, 
			cr.DocumentDate,
			pm.PaymentMethodTypes, 
			CAST(SUM(ptd.Value) AS DECIMAL(18,2)) Value,
			pa.CurrencyId
	FROM Portfolio.AccountReceivable ar WITH(NOLOCK)
	JOIN Portfolio.PortfolioTransferDetail ptd WITH(NOLOCK) ON ar.Id = ptd.AccountReceivableId
	JOIN Portfolio.PortfolioTransfer pt WITH(NOLOCK) ON ptd.PortfolioTrasferId = pt.Id
	JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pt.PortfolioAdvanceId = pa.Id
	JOIN Treasury.CashReceipts cr WITH(NOLOCK) ON pa.CashReceiptId = cr.Id
	JOIN 
	(
		SELECT IdCashReceipt, MAX(PaymentMethodTypes) PaymentMethodTypes
		FROM Treasury.PaymentMethods WITH(NOLOCK)
		GROUP BY IdCashReceipt
	) pm ON cr.Id = pm.IdCashReceipt
	WHERE pt.Status = 2 AND cr.Status = 2
	GROUP BY ar.InvoiceId, ar.AccountReceivableType, cr.Code, cr.DocumentDate, pm.PaymentMethodTypes,pa.CurrencyId

	UNION ALL

	SELECT	ar.InvoiceId,
			ar.AccountReceivableType,
			cr.Code, 
			cr.DocumentDate,
			pm.PaymentMethodTypes, 
			CAST(SUM(crar.Value) AS DECIMAL(18,2)) Value,
			ar.CurrencyId
	FROM Portfolio.AccountReceivable ar WITH(NOLOCK)
	JOIN Treasury.CashReceiptAccountReceivable crar WITH(NOLOCK) ON ar.Id = crar.AccountReceivableId
	JOIN Treasury.CashReceiptDetails crd WITH(NOLOCK) ON crar.CashReceiptDetailId = crd.Id
	JOIN Treasury.CashReceipts cr WITH(NOLOCK) ON crd.IdCashReceipt = cr.Id
	JOIN 
	(
		SELECT IdCashReceipt, MAX(PaymentMethodTypes) PaymentMethodTypes
		FROM Treasury.PaymentMethods WITH(NOLOCK)
		GROUP BY IdCashReceipt
	) pm ON cr.Id = pm.IdCashReceipt
	WHERE cr.Status = 2
	GROUP BY ar.InvoiceId, ar.AccountReceivableType, cr.Code, cr.DocumentDate, pm.PaymentMethodTypes,ar.CurrencyId
) ar
GROUP BY ar.InvoiceId, ar.AccountReceivableType, ar.Code, ar.DocumentDate,ar.CurrencyId