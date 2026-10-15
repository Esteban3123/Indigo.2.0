
CREATE VIEW [Maintenance].[ViewMaintenanceWithOutProgramming]
as
(
	select
		erg.Id as Id,		
		erg.FixedAssetPhysicalAssetId as PhysetAssetId,
		lo.Id as LocationId,
		pai.Id as ItemId,
		pait.InventoryTypeId as InventoryTypeId,
		pai.ItemTypeId as ItemTypeId,
		pa.Plate,
		pai.Code as ArticleCode, 
		pai.[Description] as ArticleName, 
		concat(tm.Code, ' - ', tm.[Name]) as TrademarkCodeName,
		pa.Serie, 
		pa.Model, 
		lo.Code as LocationCode, 
		lo.[Name] as LocationName,
		concat(lo.code, ' - ', lo.[Name]) as LocationCodeName		
	from FixedAsset.FixedAssetPhysicalAsset pa with(nolock)
	inner join FixedAsset.FixedAssetItem pai with(nolock) on pai.Id = pa.ItemId
	inner join FixedAsset.FixedAssetItemType pait with(nolock) on pai.[ItemTypeId] = pait.Id	
	inner join FixedAsset.FixedAssetLocation lo with(nolock) on pa.LocationId = lo.Id
	inner join Maintenance.EquipmentRegistration er with(nolock) on er.FixedAssetPhysicalAssetId = pa.Id
	inner join FixedAsset.FixedAssetTrademark tm with(nolock) on pa.TrademarkId = tm.Id
	inner join Maintenance.EquipmentRegistration erg with(nolock) on erg.FixedAssetPhysicalAssetId = pa.Id
)