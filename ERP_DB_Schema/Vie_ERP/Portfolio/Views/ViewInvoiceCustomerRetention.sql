CREATE VIEW [Portfolio].[ViewInvoiceCustomerRetention]
AS
SELECT	icr.Id, 
		i.Id InvoiceId,
		i.InvoiceNumber, 
		--------------------  RETENCION --------------------
		rc.Name RetentionName,
		icr.RetentionRate,
		IIF(i.Status = 1, icr.BaseValue, 0) + ISNULL(pnarr.BaseValue, 0) BaseValue,
		IIF(i.Status = 1, icr.Value, 0) + ISNULL(pnarr.Value, 0) Value
FROM Billing.Invoice i WITH (NOLOCK) 
JOIN Billing.InvoiceCustomerRetention icr WITH (NOLOCK) ON i.Id = icr.InvoiceId
JOIN Common.CustomerRetention cc WITH (NOLOCK) ON icr.CustomerRetentionId = cc.Id
JOIN GeneralLedger.RetentionConcepts rc WITH (NOLOCK) ON cc.RetentionConceptId = rc.Id
LEFT JOIN
(
	SELECT	ar.InvoiceId, 
			pnarr.InvoiceCustomerRetentionId, 
			SUM(pnarr.BaseValue * IIF(pnarr.Nature = 1, 1, -1)) BaseValue,
			SUM(pnarr.Value * IIF(pnarr.Nature = 1, 1, -1)) Value
	FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
	JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK) ON ar.Id = pnara.AccountReceivableId
	JOIN Portfolio.PortfolioNoteAccountReceivableRetention pnarr WITH (NOLOCK) ON pnara.Id = pnarr.PortfolioNoteAccountReceivableId
	GROUP BY ar.InvoiceId, pnarr.InvoiceCustomerRetentionId
) pnarr ON icr.InvoiceId = pnarr.InvoiceId AND icr.Id = pnarr.InvoiceCustomerRetentionId