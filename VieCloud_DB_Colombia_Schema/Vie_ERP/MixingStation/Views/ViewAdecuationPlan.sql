
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del plan de adecuación farmacéutica en la estación de mezclas. Consolida, para cada campaña de preparación (lote farmacéutico), los componentes detallados de cada fórmula magistral o mezcla a preparar: medicamento principal, vehículo, diluyente, insumos y productos, junto con sus cantidades requeridas, unidades de medida y concentración. Combina la solicitud de preparación (RequestMixingStationDetail) con su estado de paquete (RequestPackageDetailStatus) y el detalle del lote de campaña (CampaignDetail), resolviendo automáticamente si la fórmula corresponde a un paquete estándar (Package/PackageDetail) o a un paquete personalizado (PackagePersonalized/PackagePersonalizedDetail). Sirve como fuente principal para la planificación y ejecución de la preparación de mezclas endovenosas, nutrición parenteral y otras preparaciones magistrales, permitiendo visualizar ingrediente por ingrediente el plan de adecuación de cada campaña farmacéutica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewAdecuationPlan';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewAdecuationPlan';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el plan de adecuación (preparación) farmacéutica unificando los componentes de paquetes estándar y personalizados de cada solicitud, con su ítem (ATC, insumo o producto), cantidad requerida y concentración por campaña.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewAdecuationPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las solicitudes en RequestMixingStationDetail deben tener un RequestPackageDetailStatus asociado.; Cada solicitud debe pertenecer a un CampaignDetail existente.; El RequestPackageDetailStatus debe referenciar un PackageId (paquete estándar) o un PackagePersonalizedId (paquete personalizado).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewAdecuationPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de fila se construye concatenando RequestPackageDetailStatus.Id + Package(op).Id + AtcId, garantizando unicidad por componente y solicitud.; La concentración expuesta es Weight si existe; en caso contrario Volume; si ninguno existe, 0 (isnull(aa.Weight, isnull(aa.Volume, 0))).; Un mismo RequestPackageDetailStatus solo aporta componentes desde una de las dos fuentes (estándar o personalizada), nunca ambas, por las condiciones mutuamente excluyentes del UNION ALL.; El nombre y código del ítem (ItemCode/ItemName) provienen de un único catálogo determinado por ComponentType (1=ATC, 2=Insumo, 3=Producto).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewAdecuationPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas (preparación farmacéutica); Plan de adecuación; Paquete estándar y paquete personalizado; Campaña de preparación (lote); Componentes de mezcla: medicamento principal, vehículo y diluyente; Clasificación ATC; Concentración (peso/volumen); Lote (BatchCode); Dosis unitaria', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewAdecuationPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewAdecuationPlan: Devuelve una fila por componente del paquete (estándar o personalizado) asociado a cada solicitud, con datos del ítem resuelto según ComponentType.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewAdecuationPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rpds.PackagePersonalizedId IS NULL y rpds.PackageId = p.Id → Toma los componentes desde Package/PackageDetail (paquete estándar).; si rpds.PackagePersonalizedId IS NOT NULL y coincide con pp.Id → Toma los componentes desde PackagePersonalized/PackagePersonalizedDetail (paquete personalizado).; si op.ComponentType = 1 → Resuelve el ítem desde Inventory.ATC usando AtcId (medicamento ATC).; si op.ComponentType = 2 → Resuelve el ítem desde Inventory.InventorySupplie usando SupplieId (insumo).; si op.ComponentType = 3 → Resuelve el ítem desde Inventory.InventoryProduct usando ProductId (producto).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewAdecuationPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStationDetail; MixingStation.RequestPackageDetailStatus; MixingStation.CampaignDetail; MixingStation.Package; MixingStation.PackageDetail; MixingStation.PackagePersonalized; MixingStation.PackagePersonalizedDetail; Inventory.ATC; Inventory.InventoryMeasurementUnit; Inventory.InventorySupplie; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewAdecuationPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewAdecuationPlan';
GO
