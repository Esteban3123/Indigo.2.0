



--ItemType 1 : 'Paquete'
--ItemType 2 : 'Medicamento'
--ItemType 3 : 'Paquete Personalizado'



CREATE view [MixingStation].[ViewListRawMaterial] 
as 

select CONCAT(v.RequestCode, '-', v.ItemId, '-', ISNULL(a.Id, ISNULL(pd.AtcId, ISNULL(pd.SupplieId, pd.ProductId))), '-', pd.ItemType) Id, 
	v.CampaignDetailId, 
	v.ItemType RowPrincipalItemType, 
	v.ItemTypeName RowPrincipalItemTypeName, 
	v.ItemId RowPrincipalItemId,
	IIF(a.Id is not null, 1, pd.ComponentType) ItemType,
	CASE IIF(a.Id is not null, 1, pd.ComponentType)
		WHEN 1 THEN 'Medicamento'
		WHEN 2 THEN 'Insumo'
		WHEN 3 THEN 'Producto'
		ELSE ''
	END AS ComponentTypeName,
	pd.Quantity,
	case 
		when a.Id is not null then a.Id
		when pd.Id is not null and pd.AtcId is not null then pd.AtcId
		when pd.Id is not null and pd.SupplieId is not null then pd.SupplieId
		when pd.Id is not null and pd.ProductId is not null then pd.ProductId
	end ItemId,
	case 
		when a.Id is not null then a.Code + ' - ' + a.Name 
		when pd.Id is not null and pd.AtcId is not null then a2.Code + ' - ' + a2.Name
		when pd.Id is not null and pd.SupplieId is not null then s.Code + ' - ' + s.SupplieName
		when pd.Id is not null and pd.ProductId is not null then prod.Code + ' - ' + prod.Name
	end ItemCodeName,
	IIF(a.Id is not null, 1, ISNULL(pd.MainMedicine, 0)) MainMedicine,
	ISNULL(pd.Thinner, 0) Thinner,
	ISNULL(pd.Vehicle, 0) Vehicle,
	IIF(a.Id is not null, 'Principal', IIF(ISNULL(pd.MainMedicine, 0) = 1, 'Principal', 'Otro')) GroupName,
	v.RequestMixingStationDetailId,
	pd.MeasurementUnitCodeName,
	pd.VolumeMeasurementUnitCodeName,
	pd.ConcentrationMeasurementUnitCodeName,
	pd.Concentration,
	pd.VolumeTotalOrder,
	iif(pd.PhotoProtection = 1,'Si','No') PhotoProtectionName,
	case pd.PreparationType
	WHEN 0 THEN 'No Aplica'
	WHEN 1 THEN 'Reconstitución'
	WHEN 2 THEN 'Dilución'
	WHEN 3 THEN 'Reconstitución - Dilución'
	ELSE 'N/A'
	END PreparationTypeName,
	pd.RefrigeratedTerm,
	pd.EnvironmentalTemperatureTerm,
	pd.Purge,
	pd.PreparationInstructions,
	pd.SpecialConsiderations
from MixingStation.ViewListCampaignDetailWithRequests v
left join Inventory.ATC a on a.Id = v.ItemId and v.ItemType = 2
left join (
	select 1 ItemType, 
		pd.AtcId, 
		pd.Quantity, 
		pd.SupplieId, 
		pd.ProductId, 
		pd.PackageId Id, 
		pd.ComponentType, 
		pd.MainMedicine, 
		pd.Thinner, 
		pd.Vehicle, 
		pd.MeasurementUnitId,
		CONCAT(FORMAT(P.Concentration, 'N2'), ' ', imu.Abbreviation, '/', imu1.Abbreviation) Concentration,
		P.ConcentrationMeasurementUnitId,
		P.VolumeTotalOrder,
		P.VolumeTotalOrderMeasurementUnitId,
		P.PhotoProtection,
		P.PreparationType,
		P.RefrigeratedTerm,
		P.EnvironmentalTemperatureTerm,
		p.Purge,
		P.PreparationInstructions,
		p.SpecialConsiderations,
		CONCAT(imu.Code, ' - ', imu.Name) AS MeasurementUnitCodeName,
		CONCAT(imu1.Code, ' - ', imu1.Name) AS VolumeMeasurementUnitCodeName,
		CONCAT(imu2.Code, ' - ', imu2.Name) AS ConcentrationMeasurementUnitCodeName
	from MixingStation.PackageDetail pd
	INNER JOIN MixingStation.Package p ON pd.PackageId = p.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu ON pd.MeasurementUnitId = imu.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu1 ON p.VolumeTotalOrderMeasurementUnitId = imu1.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu2 ON p.ConcentrationMeasurementUnitId = imu2.Id

	union all

	select 3 ItemType, 
		ppd.AtcId, 
		ppd.Quantity, 
		ppd.SupplieId, 
		ppd.ProductId, 
		ppd.PackagePersonalizedId Id, 
		ppd.ComponentType, 
		ppd.MainMedicine, 
		ppd.Thinner,
		ppd.Vehicle, 
		ppd.MeasurementUnitId,
		IIF(pp.ConcentrationAntibiotic IS NULL, CONCAT(FORMAT(pp.Concentration, 'N2'), ' ', imu.Abbreviation, '/', imu1.Abbreviation), pp.ConcentrationAntibiotic) Concentration,
		pp.ConcentrationMeasurementUnitId,
		pp.VolumeTotalOrder,
		ISNULL(pp.MeasurementPreparedId,pp.VolumeTotalOrderMeasurementUnitId) VolumeTotalOrderMeasurementUnitId,
		pp.PhotoProtection,
		pp.PreparationType,
		pp.RefrigeratedTerm,
		pp.EnvironmentalTemperatureTerm,
		pp.Purge,
		pp.PreparationInstructions,
		pp.SpecialConsiderations,
		CONCAT(imu.Code, ' - ', imu.Name) AS MeasurementUnitCodeName,
		CONCAT(imu1.Code, ' - ', imu1.Name) AS VolumeMeasurementUnitCodeName,
		CONCAT(imu2.Code, ' - ', imu2.Name) AS ConcentrationMeasurementUnitCodeName
	from MixingStation.PackagePersonalizedDetail ppd
	INNER JOIN MixingStation.PackagePersonalized pp ON ppd.PackagePersonalizedId = pp.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu ON ppd.MeasurementUnitId = imu.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu1 ON ISNULL(pp.MeasurementPreparedId,pp.VolumeTotalOrderMeasurementUnitId) = imu1.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu2 ON pp.ConcentrationMeasurementUnitId = imu2.Id
) pd on pd.Id = v.ItemId and pd.ItemType = v.ItemType
left join Inventory.ATC a2 on a2.Id = pd.AtcId
left join Inventory.InventorySupplie s on s.Id = pd.SupplieId
left join Inventory.InventoryProduct prod on prod.Id = pd.ProductId