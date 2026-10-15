


CREATE VIEW [Billing].[ViewBillingNoteInvoice]
AS
SELECT	bnd.Id,
		bnd.BillingNoteId,
		bnd.InvoiceNumber,
		bnd.DocumentDate,
		bnd.BillingValue,
		bnd.AdjusmentValue,
		ISNULL(bndt.TaxValue, 0) TaxValue,
		CONCAT(ma.Number,' - ',ma.Name) AccountNumberName,
		i.InvoiceDate,
		i.InvoiceExpirationDate
FROM Billing.BillingNoteDetail bnd WITH (NOLOCK)
JOIN Billing.BillingNote bn WITH(NOLOCK) on bnd.BillingNoteId = bn.Id
JOIN Billing.Invoice i WITH (NOLOCK) ON bnd.InvoiceId = i.Id
JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on i.Id =ar.InvoiceId AND ar.ThirdPartyId =i.ThirdPartyId
LEFT JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) 
			ON pnara.AccountReceivableId =ar.Id 
			and pnara.PortfolioNoteId =  bn.EntityId 
			and bn.EntityName = 'PortfolioNote'
LEFT JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = pnara.MainAccountId
LEFT JOIN
(
	SELECT BillingNoteDetailId, SUM(TaxValue) TaxValue
	FROM Billing.BillingNoteDetailTax WITH (NOLOCK)
	GROUP BY BillingNoteDetailId
) bndt ON bnd.Id = bndt.BillingNoteDetailId