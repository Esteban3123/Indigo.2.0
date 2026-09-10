
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
INNER JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) on v.RequestMixingStationDetailId = rpds.RequestMixingStationDetailId
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
					from MixingStation.CampaignRawMaterial crm WITH(NOLOCK)
					group by crm.RequestPackageDetailStatusId, crm.AtcId
					) crm ON rpds.Id = crm.RequestPackageDetailStatusId and crm.AtcId= pd.AtcId
LEFT JOIN Inventory.InventoryMeasurementUnit imu ON pd.MeasurementUnitId = imu.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu1 ON pd.VolumeTotalOrderMeasurementUnitId = imu1.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu2 ON pd.ConcentrationMeasurementUnitId = imu2.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información detallada de calidad para las solicitudes de preparación farmacéutica en la estación de mezclas. Integra los datos de campaña y solicitudes (fórmulas magistrales, dosis unitarias, preparaciones estándar y personalizadas) con el historial de estados de cada paquete (bolsa/envase), mostrando para cada componente su tipo (medicamento ATC, insumo o producto), cantidad prescrita, cantidad consumida de materia prima, concentración, volumen total, tipo de preparación (reconstitución, dilución), condiciones de almacenamiento (refrigeración, temperatura ambiente), protección fotosensible, instrucciones de preparación y código de lote. Identifica además el rol de cada componente dentro de la fórmula: medicamento principal, diluyente o vehículo, y expone el estado clínico del tratamiento en el HIS (suspendido, descontinuado, terminado por salida del paciente). Es utilizada por el proceso de control de calidad de la farmacéutica para verificar y auditar cada paquete preparado en la estación de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListMasInfoQuality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListMasInfoQuality';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola fila por componente la información detallada de calidad de cada preparación farmacéutica de la estación de mezclas, combinando datos del paquete (estándar o personalizado), su ítem ATC/insumo/producto, unidades de medida, consumos de materia prima y estado HIS.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMasInfoQuality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada RequestMixingStationDetailId debe tener un registro en MixingStation.RequestPackageDetailStatus (INNER JOIN) para aparecer en la vista.; El ItemType del request principal debe ser 1 (Package) o 3 (PackagePersonalized) para resolver detalle vía PackageDetail/PackagePersonalizedDetail; el valor 2 indica que el ítem principal es un ATC directo.; Las unidades de medida (MeasurementUnitId, VolumeTotalOrderMeasurementUnitId, ConcentrationMeasurementUnitId) deben existir en Inventory.InventoryMeasurementUnit para mostrar nombre legible (LEFT JOIN tolera ausencia).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMasInfoQuality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Id de salida es siempre la concatenación RequestCode-ItemId-<ATC|Supplie|Product>-ItemType, garantizando trazabilidad única por componente.; Cuando el ítem principal es un ATC (a.Id no nulo), siempre se considera Medicamento Principal (ItemType=1, MainMedicine=1, GroupName=''Principal'').; ExpendQuantity nunca es NULL: se devuelve 0 si no existen consumos en CampaignRawMaterial para el RequestPackageDetailStatusId+AtcId.; Thinner, Vehicle y MainMedicine se devuelven como 0 cuando no hay valor en el detalle del paquete (ISNULL).; El consumo de materia prima se agrega por RequestPackageDetailStatusId y AtcId (SUM de ExpendQuantity).; Sólo se consideran consumos cuyo AtcId coincide con el AtcId del componente (crm.AtcId = pd.AtcId).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMasInfoQuality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas; Preparación farmacéutica (reconstitución/dilución); Paquete estándar y paquete personalizado; Componentes: medicamento, insumo, producto; Medicamento principal vs diluyente/vehículo; Clasificación ATC; Fotoprotección; Términos de conservación (refrigerado/ambiental); Purga; Consumo de materia prima por campaña; Estados HIS de prescripción (descontinuado, suspendido, terminado por salida del paciente); Lote (BatchCode) y observaciones de calidad', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMasInfoQuality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListMasInfoQuality: Devuelve una fila por cada combinación RequestCode-ItemId-(ATC/Insumo/Producto)-ItemType, con Id concatenado como clave lógica de despliegue.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMasInfoQuality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si a.Id IS NOT NULL (el ítem principal es un ATC directo, ItemType=2 en la vista origen) → ItemType se fija en 1 (Medicamento), MainMedicine=1, GroupName=''Principal'' y se toman Code/Name desde Inventory.ATC a else Se toma ComponentType desde PackageDetail/PackagePersonalizedDetail y se resuelve ítem por AtcId, SupplieId o ProductId en ese orden de prioridad; si ComponentType resultante → Se mapea a ComponentTypeName: 1=''Medicamento'', 2=''Insumo'', 3=''Producto'', otro=''''; si pd.AtcId IS NOT NULL (componente es un principio activo) → ItemId/Code/Name se toman desde Inventory.ATC a2 else Si SupplieId no es null se usa Inventory.InventorySupplie; si ProductId no es null se usa Inventory.InventoryProduct; si pd.MainMedicine = 1 → GroupName=''Principal'' else GroupName=''Otro'' (cuando no es ATC directo y MainMedicine<>1); si pd.PreparationType → Se traduce a nombre: 1=''Reconstitución'', 2=''Dilución'', 3=''No Aplica'', 4=''Reconstitución - Dilución'', otro=''N/A''; si pd.PhotoProtection = 1 → PhotoProtectionName=''Si'' else PhotoProtectionName=''No''; si v.StatusHCPRESCRA → Se traduce a HISState: 3=''Tratamiento Descontinuado'', 4=''Tratamiento Suspendido'', 7=''Tratamiento Terminado por Salida del Paciente'', otro=''''; si ItemType de origen del request → Si =1 se une con PackageDetail+Package; si =3 se une con PackagePersonalizedDetail+PackagePersonalized (UNION ALL en subconsulta pd)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMasInfoQuality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.ViewListCampaignDetailWithRequests; MixingStation.RequestPackageDetailStatus; Inventory.ATC; MixingStation.PackageDetail; MixingStation.Package; MixingStation.PackagePersonalizedDetail; MixingStation.PackagePersonalized; Inventory.InventorySupplie; Inventory.InventoryProduct; MixingStation.CampaignRawMaterial; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMasInfoQuality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListMasInfoQuality';
GO
