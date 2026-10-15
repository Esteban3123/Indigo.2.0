




CREATE VIEW [Budget].[ViewListAnnualizedCashFlow]
AS

select 
c.Code
,C.Name
,fs.Code + ' - ' + fs.Name as FinancialSource
,c.ItemType
,c.BudgetaryValidityId
,(select sum(TotalBudget) from Budget.Budget where CategoryId = c.Id) as BudgetValue
,isnull((select sum(InitialValue) from Budget.AnnualizedCashFlow where CategoryId = c.Id),0) as PACValue
,isnull((select top 1 Status from Budget.AnnualizedCashFlow where CategoryId = c.Id),0) as Status

from Budget.Category c with (nolock) 
inner join Budget.FinancialSource fs with (nolock) on fs.Id = c.FinancialSourceId
where c.PAC = 1