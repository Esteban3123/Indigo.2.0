








CREATE VIEW [Budget].[ViewListBudgetAvailabilityOfFixedAssetItem]
AS

select ROW_NUMBER() OVER (ORDER BY i.Id) Id,
a.Code AvailabilityCode, c.Name CategoryName, fs.Code + ' - ' + fs.Name FinancialSourceDescription, rt.Code + ' - ' + rt.Name RevenueTypeDescription,
ad.Balance AvailabilityBalance, i.Id ItemId, ic.Id ItemCatalogId, a.Id AvailabilityId, ad.Id AvailabilityDetailId,
a.Code + ' - ' + c.Name CodeName
from FixedAsset.FixedAssetItem i with(nolock)
inner join FixedAsset.FixedAssetItemCatalog ic with(nolock) on ic.Id = i.ItemCatalogId
inner join Budget.AvailabilityDetail ad with(nolock) on ad.BudgetId = ic.BudgetId
inner join Budget.Availability a with(nolock) on a.Id = ad.AvailabilityId
inner join Budget.Budget b with(nolock) on b.Id = ad.BudgetId
inner join Budget.Category c with(nolock) on c.Id = b.CategoryId
inner join Budget.RevenueType rt with(nolock) on rt.Id = b.RevenueTypeId
left join Budget.FinancialSource fs with(nolock) on fs.Id = c.FinancialSourceId