

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de materias primas (insumos, medicamentos y productos) requeridos para la preparación de mezclas en la estación de mezclas. Integra las solicitudes de campaña (pedidos activos de preparación) con el detalle de componentes de paquetes estándar y paquetes personalizados, resolviendo para cada ítem su nombre, código, tipo de componente (medicamento, insumo o producto), rol dentro de la fórmula (principio activo principal, diluyente o vehículo), cantidad, unidades de medida, concentración, volumen total a preparar, tipo de preparación (reconstitución, dilución o ambas), condiciones de almacenamiento (refrigeración, temperatura ambiente), fotosensibilidad, purga, instrucciones de preparación y consideraciones especiales. Sirve como base para que el farmacéutico o técnico de la estación de mezclas visualice y gestione todos los componentes necesarios para cada orden de preparación magistral o citostática.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListRawMaterial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListRawMaterial';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado las materias primas (medicamentos, insumos y productos) que componen cada paquete estándar o personalizado vinculado a las solicitudes/campañas de la estación de mezclas, junto con sus unidades, concentración y condiciones de preparación.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRawMaterial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las solicitudes/campañas deben estar disponibles vía MixingStation.ViewListCampaignDetailWithRequests.; Los detalles de paquete (PackageDetail) o paquete personalizado (PackagePersonalizedDetail) deben estar enlazados al Package/PackagePersonalized correspondiente por Id.; Las unidades de medida referenciadas (MeasurementUnitId, VolumeTotalOrderMeasurementUnitId, ConcentrationMeasurementUnitId, MeasurementPreparedId) deben existir en Inventory.InventoryMeasurementUnit para mostrar código y nombre.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRawMaterial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ItemType=1 representa ''Medicamento'', 2 ''Insumo'' y 3 ''Producto'' en la columna ComponentTypeName.; Los paquetes estándar se identifican con ItemType=1 y los personalizados con ItemType=3 en el subquery unificado.; Cuando el ítem principal es un ATC directo, siempre se considera medicamento principal (MainMedicine=1, GroupName=''Principal'').; El Id de cada fila combina RequestCode, ItemId, el identificador resuelto (ATC/Supplie/Product) y el ItemType, garantizando unicidad por componente dentro de la solicitud.; Thinner y Vehicle se exponen como 0 cuando son nulos (ISNULL a 0).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRawMaterial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas; Paquete (preparación farmacéutica); Paquete personalizado; Medicamento principal; Diluyente (Thinner); Vehículo; Concentración; Volumen total de orden; Fotoprotección; Tipo de preparación (Reconstitución/Dilución); Término refrigerado / temperatura ambiental; Purga; Instrucciones de preparación; Clasificación ATC; Insumo; Producto', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRawMaterial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListRawMaterial: Devuelve una fila por cada componente de paquete o paquete personalizado, o por cada ATC directamente referenciado cuando v.ItemType=2 (medicamento), con un Id compuesto por RequestCode-ItemId-(ATC/Supplie/Product)-ItemType.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRawMaterial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si v.ItemType = 2 (Medicamento) y existe coincidencia en Inventory.ATC por v.ItemId → Marca ItemType=1 (''Medicamento''), MainMedicine=1, GroupName=''Principal'' y toma código/nombre desde ATC.; si Existe registro en PackageDetail (ItemType=1, paquete) o PackagePersonalizedDetail (ItemType=3, paquete personalizado) que coincida con v.ItemId y v.ItemType → Usa pd.ComponentType (1=Medicamento, 2=Insumo, 3=Producto) para determinar el tipo de componente y resuelve el ItemId/ItemCodeName desde ATC, InventorySupplie o InventoryProduct según corresponda.; si pd.MainMedicine = 1 → GroupName=''Principal'' else GroupName=''Otro''; si pd.PhotoProtection = 1 → PhotoProtectionName=''Si'' else PhotoProtectionName=''No''; si pd.PreparationType ∈ {0,1,2,3} → Mapea a ''No Aplica'', ''Reconstitución'', ''Dilución'' o ''Reconstitución - Dilución'' respectivamente. else PreparationTypeName=''N/A''; si En PackagePersonalized: pp.ConcentrationAntibiotic IS NOT NULL → Toma ConcentrationAntibiotic como Concentration mostrada. else Construye la concentración como FORMAT(pp.Concentration,''N2'') + abreviatura unidad / abreviatura unidad de volumen.; si En PackagePersonalized: pp.MeasurementPreparedId IS NOT NULL → Usa MeasurementPreparedId como unidad de volumen total. else Usa pp.VolumeTotalOrderMeasurementUnitId.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRawMaterial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.ViewListCampaignDetailWithRequests; Inventory.ATC; MixingStation.PackageDetail; MixingStation.Package; Inventory.InventoryMeasurementUnit; MixingStation.PackagePersonalizedDetail; MixingStation.PackagePersonalized; Inventory.InventorySupplie; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRawMaterial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRawMaterial';
GO
