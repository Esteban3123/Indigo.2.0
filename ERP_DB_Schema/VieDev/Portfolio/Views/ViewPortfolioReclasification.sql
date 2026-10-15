
CREATE VIEW [Portfolio].[ViewPortfolioReclasification]
AS
SELECT	pr.Code, 
		SUM(pr.Value) Value, 
		pr.DocumentType, 
		pr.Status, 
		ar.InvoiceNumber,
		prt.Total,
		cu.Abbreviation CurrencyAbbreviation
FROM Portfolio.PortfolioReclassification AS pr 
JOIN Portfolio.AccountReceivable AS ar ON pr.AccountReceivableId = ar.Id
JOIN
(
	SELECT Code, SUM(VALUE) Total
	FROM Portfolio.PortfolioReclassification
	GROUP BY Code
) prt ON pr.Code = prt.Code
JOIN (SELECT top 1 cs.OfficialCurrencyId from GeneralLedger.CompanySettings cs WITH(NOLOCK)) cs on 1=1
JOIN Common.Currency cu WITH(NOLOCK) on cu.Id =  ISNULL(ar.CurrencyId,cs.OfficialCurrencyId)
GROUP BY  pr.Code, pr.Status, pr.DocumentType, ar.InvoiceNumber, prt.Total,cu.Abbreviation