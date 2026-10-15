







CREATE VIEW [Budget].[ViewListBudgetAvailabilityOfProducts]
AS

select ROW_NUMBER() OVER (ORDER BY ipr.Id) Id,
a.Code AvailabilityCode, c.Name CategoryName, fs.Code + ' - ' + fs.Name FinancialSourceDescription, rt.Code + ' - ' + rt.Name RevenueTypeDescription,
ad.Balance AvailabilityBalance, ipr.Id ProductId, pg.Id ProductGroupId, a.Id AvailabilityId, ad.Id AvailabilityDetailId,
a.Code + ' - ' + c.Name CodeName
from Inventory.InventoryProduct ipr with(nolock)
inner join Inventory.ProductGroup pg with(nolock) on pg.Id = ipr.ProductGroupId
inner join Budget.AvailabilityDetail ad with(nolock) on ad.BudgetId = pg.BudgetId
inner join Budget.Availability a with(nolock) on a.Id = ad.AvailabilityId
inner join Budget.Budget b with(nolock) on b.Id = ad.BudgetId
inner join Budget.Category c with(nolock) on c.Id = b.CategoryId
inner join Budget.RevenueType rt with(nolock) on rt.Id = b.RevenueTypeId
left join Budget.FinancialSource fs with(nolock) on fs.Id = c.FinancialSourceId