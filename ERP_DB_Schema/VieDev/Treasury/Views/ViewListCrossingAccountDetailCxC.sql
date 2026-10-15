
CREATE VIEW [Treasury].[ViewListCrossingAccountDetailCxC]
AS
SELECT	dcc.Id, 
		dcc.CrossingAccountId, 
		dcc.AccountReceivableId, 
		dcc.AccountReceivableAccountingId, 
		ar.InvoiceNumber BillNumber, 
		dcc.MainAccountId, 
		ma.Number + ' - ' + ma.Name MainAccountDescription, 
		t.Nit + ' - ' + t.Name ThirdPartyDescription, 
		ar.Value, ar.Balance, 
		dcc.CrossingValue, 
		dcc.Detail, 
		dcc.IdCashFlowConcept,
		cc.Code + ' - ' + cc.NameConcept CodeNameCashFlowConcept,
		ar.CurrencyId,
		c.Abbreviation AS CurrencyAbbreviation
FROM Treasury.CrossingAccountDetailCxC dcc
JOIN GeneralLedger.MainAccounts ma on ma.Id = dcc.MainAccountId
JOIN Portfolio.AccountReceivableAccounting ara on ara.Id = dcc.AccountReceivableAccountingId
JOIN Portfolio.AccountReceivable ar on ar.Id = ara.AccountReceivableId
JOIN Common.ThirdParty t on t.Id = ar.ThirdPartyId
LEFT JOIN Treasury.CashFlowConcept cc on cc.Id = dcc.IdCashFlowConcept
LEFT JOIN Common.Currency c ON c.Id = ar.CurrencyId