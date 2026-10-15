
CREATE VIEW [MixingStation].[ViewListMasInfoQuality] 
AS

select CONCAT(v.RequestCode, '-', v.ItemId, '-', ISNULL(a.Id, ISNULL(pd.AtcId, ISNULL(pd.SupplieId, pd.ProductId))), '-', pd.ItemType) Id, 
	v.CampaignDetailId, 
	v.ItemType RowPrincipalItemType, v.ItemTypeName RowPrincipalItemTypeName, v.ItemId RowPrincipalItemId,
	IIF(a.Id is not null, 1, pd.ComponentType) ItemType,
	CASE IIF(a.Id is not null, 1, pd.ComponentType)
		WHEN 1 THEN 'Medicamento'
		WHEN 2 THEN 'Insumo'
		WHEN 3 THEN 'Producto'
		ELSE ''
	END AS ComponentTypeName,
	pd.Quantity ,
	ISNULL(crm.ExpendQuantity,0) ExpendQuantity,
	case 
		when a.Id is not null then a.Id
		when pd.Id is not null and pd.AtcId is not null then pd.AtcId
		when pd.Id is not null and pd.SupplieId is not null then pd.SupplieId
		when pd.Id is not null and pd.ProductId is not null then pd.ProductId
	end ItemId,
	case 
		when a.Id is not null then a.Code 
		when pd.Id is not null and pd.AtcId is not null then a2.Code 
		when pd.Id is not null and pd.SupplieId is not null then s.Code
		when pd.Id is not null and pd.ProductId is not null then prod.Code
	end ItemCode,
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
	CONCAT(imu.Code, ' - ', imu.Name) AS MeasurementUnitCodeName,
	CONCAT(imu1.Code, ' - ', imu1.Name) AS VolumeMeasurementUnitCodeName,
	CONCAT(imu2.Code, ' - ', imu2.Name) AS ConcentrationMeasurementUnitCodeName,
	pd.Concentration,
	pd.VolumeTotalOrder,
	iif(pd.PhotoProtection = 1,'Si','No') PhotoProtectionName,
	case pd.PreparationType
	WHEN 1 THEN 'Reconstitución'
	WHEN 2 THEN 'Dilución'
	WHEN 3 THEN 'No Aplica'
	WHEN 4 THEN 'Reconstitución - Dilución'
	ELSE 'N/A'
	END PreparationTypeName,
	pd.RefrigeratedTerm,
	pd.EnvironmentalTemperatureTerm,
	pd.Purge,
	pd.PreparationInstructions,
	v.RequestType, 
    v.RequestTypeName,
	v.UnitDoseTypeId, v.UnitDoseTypeCodeName,
	v.LabelType, v.LabelTypeName,
	--rdp.Bed, 
	ISNULL(v.StatusHCPRESCRA, 0) HISStateCode,
		case ISNULL(v.StatusHCPRESCRA, 0)
			when 3 then 'Tratamiento Descontinuado'
			when 4 then 'Tratamiento Suspendido'
			when 7 then 'Tratamiento Terminado por Salida del Paciente'
			else ''
		end HISState,
		rpds.BatchCode,
		rpds.Observations
from MixingStation.ViewListCampaignDetailWithRequests v
INNER JOIN MixingStation.RequestPackageDetailStatus rpds on v.RequestMixingStationDetailId = rpds.RequestMixingStationDetailId
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
		P.Concentration,
		P.ConcentrationMeasurementUnitId,
		P.VolumeTotalOrder,
		P.VolumeTotalOrderMeasurementUnitId,
		P.PhotoProtection,
		P.PreparationType,
		P.RefrigeratedTerm,
		P.EnvironmentalTemperatureTerm,
		p.Purge,
		P.PreparationInstructions
	from MixingStation.PackageDetail pd
	INNER JOIN MixingStation.Package p ON pd.PackageId = p.Id

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
		pp.Concentration,
		pp.ConcentrationMeasurementUnitId,
		pp.VolumeTotalOrder,
		pp.VolumeTotalOrderMeasurementUnitId,
		pp.PhotoProtection,
		pp.PreparationType,
		pp.RefrigeratedTerm,
		pp.EnvironmentalTemperatureTerm,
		pp.Purge,
		pp.PreparationInstructions
	from MixingStation.PackagePersonalizedDetail ppd
	INNER JOIN MixingStation.PackagePersonalized pp ON ppd.PackagePersonalizedId = pp.Id
) pd on pd.Id = v.ItemId and pd.ItemType = v.ItemType
left join Inventory.ATC a2 on a2.Id = pd.AtcId
left join Inventory.InventorySupplie s on s.Id = pd.SupplieId
left join Inventory.InventoryProduct prod on prod.Id = pd.ProductId
LEFT JOIN (select	crm.RequestPackageDetailStatusId,
					crm.AtcId,
					sum(crm.ExpendQuantity) ExpendQuantity
					from MixingStation.CampaignRawMaterial crm
					group by crm.RequestPackageDetailStatusId, crm.AtcId
					) crm ON rpds.Id = crm.RequestPackageDetailStatusId and crm.AtcId= pd.AtcId
LEFT JOIN Inventory.InventoryMeasurementUnit imu ON pd.MeasurementUnitId = imu.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu1 ON pd.VolumeTotalOrderMeasurementUnitId = imu1.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu2 ON pd.ConcentrationMeasurementUnitId = imu2.Id