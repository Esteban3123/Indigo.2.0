


CREATE VIEW [Portfolio].[ViewPortfolioReclasificationDetail]
AS
SELECT  pr.Id,
		pr.Code,
		ar.InvoiceNumber AS AccountReceivableCode,
		mas.Number + ' - ' + mas.Name AS AccoutSource,
		mat.Number + ' - ' + mat.Name AS TargetAccount, 
		pr.Value,
		cu.Abbreviation AS CurrencyAbbreviation
FROM Portfolio.PortfolioReclassification AS pr  with (nolock) 
INNER JOIN Portfolio.AccountReceivable AS ar  with (nolock) ON pr.AccountReceivableId = ar.Id
INNER JOIN GeneralLedger.MainAccounts AS mas with (nolock) ON pr.SourceAccountId = mas.Id 
INNER JOIN GeneralLedger.MainAccounts AS mat with (nolock) ON pr.TargetAccountId = mat.Id
INNER JOIN (SELECT TOP 1 cs.OfficialCurrencyId
			FROM GeneralLedger.CompanySettings cs WITH(NOLOCK)) cs on 1=1
INNER JOIN Common.Currency cu WITH(NOLOCK) on cu.Id = ISNULL(ar.CurrencyId,cs.OfficialCurrencyId)