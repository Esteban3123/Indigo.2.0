
CREATE View [MixingStation].[ViewAdecuationPlan]
As

--select rpds.Id, rpds.BatchCode, p.Code, a.Code, a.Name, ae.Code, ae.Name, pd.*
--from MixingStation.RequestMixingStationDetail rmsd
--join MixingStation.RequestPackageDetailStatus rpds on rpds.RequestMixingStationDetailId = rmsd.Id
--left join MixingStation.Package p on rmsd.PackageId = p.Id
--left join MixingStation.PackageDetail pd on pd.PackageId = p.Id
--left join Inventory.ATC a on pd.AtcId = a.Id
--left join Inventory.ATCEntity ae on a.ATCEntityId = ae.Id
--left join [MixingStation].[CampaignRawMaterial] crm on crm.RequestPackageDetailStatusId = rpds.Id
--where rmsd.CampaignDetailId = 435

--select * from MixingStation.CampaignDetail where CampaignNumber = 140
--select * from [MixingStation].[CampaignRawMaterial]
select concat(rpds.Id, op.Id, op.AtcId) as Id
	, cd.CampaignNumber
	, rpds.BatchCode
	, cd.UnitDoseTypeId 
	, it.Code as ItemCode
	, it.[Name] as ItemName
	, mu.Abbreviation as MeasurementUnitAbbreviation
	, rmsd.Quantity as RequiredQuantity
	, op.Code as PackageCode
	, op.AtcId
	, op.SupplieId
	, op.ProductId
	, op.Quantity
	, op.MeasurementUnitId
	, op.ComponentType
	, op.MainMedicine
	, op.Vehicle
	, op.Thinner
	, isnull(aa.Weight, isnull(aa.Volume, 0)) as Concentration
	, rmsd.CampaignDetailId
from MixingStation.RequestMixingStationDetail rmsd
join MixingStation.RequestPackageDetailStatus rpds on rpds.RequestMixingStationDetailId = rmsd.Id
join MixingStation.CampaignDetail cd (nolock) on rmsd.CampaignDetailId = cd.Id
outer apply (
	select p.Id
		, p.Code
		, pd.AtcId
		, pd.SupplieId
		, pd.ProductId
		, pd.Quantity
		, pd.MeasurementUnitId
		, pd.ComponentType
		, pd.MainMedicine
		, pd.Vehicle
		, pd.Thinner
	from MixingStation.Package p
	join MixingStation.PackageDetail pd on pd.PackageId = p.Id
	where rpds.PackagePersonalizedId is null and rpds.PackageId = p.Id

	union ALL

	select pp.Id
		, pp.Code
		, ppd.AtcId
		, ppd.SupplieId
		, ppd.ProductId
		, ppd.Quantity
		, ppd.MeasurementUnitId
		, ppd.ComponentType
		, ppd.MainMedicine
		, ppd.Vehicle
		, ppd.Thinner
	from MixingStation.PackagePersonalized pp
	join MixingStation.PackagePersonalizedDetail ppd on ppd.PackagePersonalizedId = pp.Id
	where rpds.PackagePersonalizedId is not null and rpds.PackagePersonalizedId = pp.Id
) op
left join Inventory.ATC aa (nolock) on op.AtcId = aa.Id
left join Inventory.InventoryMeasurementUnit mu (nolock) on op.MeasurementUnitId = mu.Id
outer apply (
	select a.Code, a.[Name]
	from Inventory.ATC a
	where op.ComponentType = 1 and op.AtcId = a.Id

	union ALL

	select s.Code, s.SupplieName as [Name]
	from Inventory.InventorySupplie s
	where op.ComponentType = 2 and op.SupplieId = s.Id

	union ALL

	select p.Code, p.[Name]
	from Inventory.InventoryProduct p
	where op.ComponentType = 3 and op.ProductId = p.Id
) it
--where rmsd.CampaignDetailId = 437 --and op.MainMedicine = 1