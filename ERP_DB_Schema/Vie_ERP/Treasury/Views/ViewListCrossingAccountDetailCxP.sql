

CREATE view [Treasury].[ViewListCrossingAccountDetailCxP]
as

select 
	dcp.Id,
	dcp.CrossingAccountId,
	dcp.AccountPayableId, 
	dcp.MainAccountId,
	dcp.CrossingValue, 
	dcp.Detail, 
	dcp.IdCashFlowConcept,
	ma.Number + ' - ' + ma.Name MainAccountDescription,
	ap.BillNumber, ap.Value, ap.Balance, 
	t.Nit + ' - ' + t.Name ThirdPartyDescription, cc.Code + ' - ' + cc.NameConcept CodeNameCashFlowConcept,
	ap.CurrencyId,
	c.Abbreviation AS CurrencyAbbreviation
from Treasury.CrossingAccountDetailCxP dcp
inner join GeneralLedger.MainAccounts ma on ma.Id = dcp.MainAccountId
inner join Payments.AccountPayable ap on ap.Id = dcp.AccountPayableId
inner join Common.ThirdParty t on t.Id = ap.IdThirdParty
left join Treasury.CashFlowConcept cc on cc.Id = dcp.IdCashFlowConcept
LEFT JOIN Common.Currency c ON c.Id = ap.CurrencyId