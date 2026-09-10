

CREATE VIEW  [MixingStation].[ViewQuantityRemaining]
AS
SELECT 
	qr.Id,
	qr.CampaignDetailId,
	qr.ProductId,
	qr.BatchSerialId,
	qr.PreparationType,
	qr.Stability,
	qr.UnitTimeStability,
	IIF(qr.UnitTimeStability = 1, 'HORA(S)', 'DIA(S)') TimeUnitNameStability,
	qr.Concentration,
	qr.RemnantVolume,
	imuv.Abbreviation AS MeasurementUnitVolume,
	qr.UnitRemnantVolume AS MeasurementUnitVolumeId,
	qr.Quantity,
	imum.Abbreviation AS MeasurementUnitMainMedicine,
	qr.UnitQuantity AS MeasurementUnitMainMedicineId,
	-------------------------
	ISNULL(CASE qr.PreparationType
		WHEN 1 THEN (SELECT  ppdt.Quantity * COUNT(1) Quantity
					 FROM MixingStation.RequestMixingStationDetail rmsd
					 JOIN MixingStation.PackageDetail ppdm ON rmsd.PackageId = ppdm.PackageId AND ppdm.MainMedicine = 1
					 JOIN MixingStation.PackageDetail ppdt ON rmsd.PackageId = ppdt.PackageId AND ppdt.Thinner = 1					 
					 WHERE rmsd.CampaignDetailId = qr.CampaignDetailId AND ppdm.AtcId = ip.ATCId
					 GROUP BY ppdt.Quantity)
		WHEN 2 THEN (SELECT ppdv.Quantity * COUNT(1) Quantity
					 FROM MixingStation.RequestMixingStationDetail rmsd
					 JOIN MixingStation.PackageDetail ppdm ON rmsd.PackageId = ppdm.PackageId AND ppdm.MainMedicine = 1
					 JOIN MixingStation.PackageDetail ppdv ON rmsd.PackageId = ppdv.PackageId AND ppdv.Vehicle = 1
					 WHERE rmsd.CampaignDetailId = qr.CampaignDetailId AND ppdm.AtcId = ip.ATCId
					 GROUP BY ppdv.Quantity)
		WHEN 3 THEN (SELECT a.Volume * COUNT(1) Quantity
					 FROM MixingStation.RequestMixingStationDetail rmsd
					 JOIN MixingStation.PackageDetail ppdm ON rmsd.PackageId = ppdm.PackageId AND ppdm.MainMedicine = 1
					 JOIN Inventory.ATC a ON ppdm.AtcId = a.Id
					 WHERE a.Id = ip.ATCId
					 GROUP BY a.Volume)
	END, 0) AS MaximumVolume
FROM MixingStation.QuantityRemaining qr
JOIN Inventory.InventoryMeasurementUnit imum ON qr.UnitQuantity = imum.Id
JOIN Inventory.InventoryMeasurementUnit imuv ON qr.UnitRemnantVolume = imuv.Id
JOIN Inventory.InventoryProduct ip ON qr.ProductId = ip.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra el inventario de sobrantes o remanentes de medicamentos en la estación de mezclas, integrando información del producto (medicamento o insumo), la cantidad restante con su unidad de medida y el volumen remanente con su propia unidad. Calcula el volumen máximo utilizable del sobrante según el tipo de preparación: diluyente (tipo 1), vehículo (tipo 2) o volumen ATC (tipo 3), cruzando con las solicitudes de mezcla y el detalle de paquetes de preparación asociados a la misma campaña. Incluye además la estabilidad del remanente expresada en horas o días, y la concentración del preparado. Sirve para consultar cuánto material sobrante de mezclas oncológicas o parenterales está disponible para reutilizar en nuevas preparaciones, apoyando el control de desperdicios y la trazabilidad de insumos en la farmacia de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewQuantityRemaining';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewQuantityRemaining';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los remanentes de productos en la estación de mezclas con sus unidades de medida y calcula el volumen máximo disponible según el tipo de preparación (diluyente, vehículo o volumen del ATC).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemaining';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada remanente debe tener UnitQuantity y UnitRemnantVolume válidos en Inventory.InventoryMeasurementUnit (JOIN inner).; El ProductId del remanente debe existir en Inventory.InventoryProduct (JOIN inner).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemaining';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'MaximumVolume nunca es NULL: se fuerza a 0 cuando no aplica el CASE o no hay datos.; El cálculo de MaximumVolume solo considera componentes del paquete marcados como MainMedicine=1 para identificar el ATC, y como Thinner=1 o Vehicle=1 según el tipo de preparación.; El emparejamiento entre el remanente y los detalles de la campaña se hace por CampaignDetailId del remanente y por coincidencia del ATC del medicamento principal con el ATC del producto del remanente.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemaining';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'estación de mezclas; remanente de producto; campaña de preparación; tipo de preparación; estabilidad (horas/días); medicamento principal; diluyente (Thinner); vehículo (Vehicle); clasificación ATC; unidad de medida; concentración; volumen máximo', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemaining';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.QuantityRemaining: Devuelve un registro por cada remanente con sus unidades de medida resueltas y un MaximumVolume calculado según PreparationType.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemaining';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si qr.UnitTimeStability = 1 → TimeUnitNameStability = ''HORA(S)'' else TimeUnitNameStability = ''DIA(S)''; si qr.PreparationType = 1 → MaximumVolume = (Quantity del componente Thinner del paquete) * COUNT(detalles de la campaña cuyo MainMedicine.AtcId coincide con el ATC del producto), agrupado por la cantidad del diluyente.; si qr.PreparationType = 2 → MaximumVolume = (Quantity del componente Vehicle del paquete) * COUNT(detalles de la campaña cuyo MainMedicine.AtcId coincide con el ATC del producto), agrupado por la cantidad del vehículo.; si qr.PreparationType = 3 → MaximumVolume = (Volume del ATC del medicamento principal) * COUNT(detalles de la campaña con ese ATC).; si PreparationType no es 1, 2 ni 3, o el subquery devuelve NULL → MaximumVolume = 0 (vía ISNULL).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemaining';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.QuantityRemaining; MixingStation.RequestMixingStationDetail; MixingStation.PackageDetail; Inventory.ATC; Inventory.InventoryMeasurementUnit; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemaining';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemaining';
GO
