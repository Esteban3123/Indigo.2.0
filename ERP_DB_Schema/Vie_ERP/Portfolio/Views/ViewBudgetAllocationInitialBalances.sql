

CREATE VIEW [Portfolio].[ViewBudgetAllocationInitialBalances]
AS
select ar.Id as Id, ar.PortfolioInitialBalanceId as PortfolioInitialBalanceId,ar.InvoiceNumber as InvoiceNumber , CONCAT(tp.nit,' - ' ,tp.Name) as NitNameThird ,ar.AccountReceivableDate as AccountReceivableDate , CONCAT(ma.Number,' - ' ,ma.Name) as NumberNameAccount ,ara.Value as Value   
from Portfolio.PortfolioInitialBalanceAccountReceivable ar with (nolock)
inner join Common.ThirdParty as tp with (nolock) on tp.Id = ar.ThirdPartyId 
inner join Portfolio.PortfolioInitialBalanceAccountReceivableAccounting as ara with (nolock) on ara.PortfolioInitialBalanceAccountReceivableId =ar.id 
inner join GeneralLedger.MainAccounts as ma  with (nolock) on ara.MainAccountId = ma.Id 
where ar.AffectBudget = 0