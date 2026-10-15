





CREATE VIEW [Budget].[VReportBudgetExpense]
as

select 
c.BudgetaryValidityId,
c.id as idCategory,
c.CategoryOwnerId,
c.Code as CategoryCode,
c.Name as CategoryName,
c.Auxiliary,
'' as RevenueTypeCode,
'' as RevenueTypeName,
0 as InitialValue,
0 as CreditValueModification,
0 as DebitValueModification,
0 as CreditValueTransfer,
0 as DebitValueTransfer,
0 as TotalBudget,
0 as ExecutedValue
,0 as Balance
from 
Budget.Category c
where 
c.ItemType = 2 and c.Auxiliary = 0

union all

select 
c.BudgetaryValidityId,
c.id as idCategory,
c.CategoryOwnerId,
c.Code as CategoryCode,
c.Name as CategoryName,
c.Auxiliary,
rt.Code as RevenueTypeCode,
rt.Name as RevenueTypeName,
b.InitialValue,
b.CreditValueModification,
b.DebitValueModification,
b.CreditValueTransfer,
b.DebitValueTransfer,
b.TotalBudget,
b.ExecutedValue
,b.Balance
from 
Budget.Category c with (nolock)
inner join Budget.RevenueType rt with (nolock) on c.BudgetaryValidityId = rt.BudgetaryValidityId 
left join Budget.Budget b  with (nolock) on b.CategoryId = c.Id and b.RevenueTypeId = rt.Id
where 
c.ItemType = 2 and c.Auxiliary = 1 and rt.Type = 2