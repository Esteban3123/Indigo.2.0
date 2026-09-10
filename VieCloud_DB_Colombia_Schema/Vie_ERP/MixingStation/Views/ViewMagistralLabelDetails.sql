
CREATE VIEW [MixingStation].[ViewMagistralLabelDetails]	
AS
SELECT 	
		pd.Id ,
		rpds.Id RequestPackageDetailStatusId,
		p.Id PackageId,
		ISNULL(a.Id, 0) AtcId,
		a.AbbreviationName AbbreviationNameAtc,
		COALESCE(pd.Quantity, pd.Volume, 0) Quantity,
		imu.Abbreviation MeasurementUnit,
		CONCAT(COALESCE(pd.Quantity, pd.Volume, 0), ' ', imu.Abbreviation) QuantityWithUnitMeasurement,
		CAST(ISNULL(pd.MainMedicine, 0) AS BIT) MainMedicine,
		pd.Vehicle,
		'Paquete Estándar' AS TypePackage
FROM MixingStation.RequestPackageDetailStatus rpds
JOIN MixingStation.RequestMixingStationDetail rmsd ON rpds.RequestMixingStationDetailId = rmsd.Id
JOIN MixingStation.UnitDoseType udt ON rmsd.UnitDoseTypeId = udt.Id
JOIN MixingStation.Package p ON rmsd.PackageId = p.Id
JOIN MixingStation.PackageDetail pd ON p.Id = pd.PackageId
JOIN Inventory.ATC a ON pd.AtcId = a.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu ON ISNULL(pd.MeasurementUnitId, pd.VolumeMeasureUnit) = imu.Id
WHERE udt.MSClass = 9 
		AND rmsd.PackagePersonalizedId IS NULL
		AND (pd.MainMedicine = 1 OR pd.Vehicle = 1)

UNION ALL

SELECT	
		ppd.Id,
		rpds.Id RequestPackageDetailStatusId,
		pp.Id PackageId,
		ISNULL(a.Id, 0) AtcId,
		a.AbbreviationName AbbreviationNameAtc,
		COALESCE(ppd.Quantity, ppd.Volume, 0) Quantity,
		imu.Abbreviation MeasurementUnit,
		CONCAT(COALESCE(ppd.Quantity, ppd.Volume, 0), ' ', imu.Abbreviation) QuantityWithUnitMeasurement,
		CAST(ISNULL(ppd.MainMedicine, 0) AS BIT) MainMedicine,
		ppd.Vehicle,
		'Paquete personalizado' AS TypePackage
FROM MixingStation.RequestPackageDetailStatus rpds
JOIN MixingStation.RequestMixingStationDetail rmsd ON rpds.RequestMixingStationDetailId = rmsd.Id
JOIN MixingStation.UnitDoseType udt ON rmsd.UnitDoseTypeId = udt.Id
JOIN MixingStation.PackagePersonalized pp ON rmsd.PackagePersonalizedId = pp.Id
JOIN MixingStation.PackagePersonalizedDetail ppd ON pp.Id = ppd.PackagePersonalizedId
JOIN Inventory.ATC a ON ppd.AtcId = a.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu ON ISNULL(ppd.MeasurementUnitId, ppd.VolumeMeasureUnit) = imu.Id
WHERE udt.MSClass = 9 
		AND rmsd.PackagePersonalizedId IS NOT NULL
		AND (ppd.MainMedicine = 1 OR ppd.Vehicle = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que reúne el detalle de los componentes (medicamento principal y vehículo/diluyente) de cada bolsa o envase preparado en la estación de mezclas magistrales, tanto para paquetes estándar como para paquetes personalizados. Combina el historial de estados del paquete (RequestPackageDetailStatus), la solicitud de mezcla, el tipo de dosis unitaria y el catálogo de medicamentos ATC para obtener el nombre abreviado del principio activo, la cantidad o volumen con su unidad de medida, y si el componente es el medicamento principal o el vehículo portador. Sirve principalmente para imprimir la etiqueta magistral de cada preparación farmacéutica, mostrando con claridad qué medicamentos y en qué dosis componen la fórmula, diferenciando si el envase fue preparado con una fórmula estándar o una personalizada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewMagistralLabelDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewMagistralLabelDetails';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los componentes principales (medicamento principal o vehículo) de los paquetes estándar y personalizados asociados a solicitudes de mezclas magistrales, para imprimir/visualizar la información de la etiqueta.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabelDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tipo de dosis unitaria (UnitDoseType) debe tener MSClass = 9 (clase correspondiente a preparaciones magistrales).; Para paquetes estándar: RequestMixingStationDetail.PackagePersonalizedId debe ser NULL.; Para paquetes personalizados: RequestMixingStationDetail.PackagePersonalizedId debe ser NOT NULL.; El detalle del paquete debe tener un ATC asociado (JOIN obligatorio con Inventory.ATC).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabelDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un mismo RequestMixingStationDetail aporta filas exclusivamente desde un paquete estándar o desde un paquete personalizado, nunca de ambos (las ramas son mutuamente excluyentes por el filtro de PackagePersonalizedId).; Sólo se incluyen preparaciones cuya clase de dosis unitaria es MSClass = 9.; Cada fila representa el medicamento principal o el vehículo del paquete; cantidades NULL se normalizan a 0 y MainMedicine NULL se normaliza a 0 (BIT).; AtcId nunca es NULL en la salida (se aplica ISNULL(a.Id, 0)), aunque el JOIN con Inventory.ATC es obligatorio.; La cadena QuantityWithUnitMeasurement concatena cantidad y abreviatura de unidad para presentación en etiqueta.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabelDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas; Preparación magistral; Paquete estándar; Paquete personalizado; Medicamento principal; Vehículo (diluyente); Clasificación ATC; Dosis unitaria; Unidad de medida; Etiqueta de preparación', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabelDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewMagistralLabelDetails: Devuelve sólo componentes con MainMedicine = 1 o Vehicle = 1; los demás insumos del paquete son excluidos del resultado.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabelDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rmsd.PackagePersonalizedId IS NULL y udt.MSClass = 9 → Toma componentes desde Package/PackageDetail y etiqueta TypePackage = ''Paquete Estándar''. else Si PackagePersonalizedId IS NOT NULL, toma componentes desde PackagePersonalized/PackagePersonalizedDetail y etiqueta TypePackage = ''Paquete personalizado''.; si pd.Quantity (o ppd.Quantity) es NULL → Usa Volume como cantidad mediante COALESCE(Quantity, Volume, 0). else Usa Quantity.; si pd.MeasurementUnitId (o ppd.MeasurementUnitId) es NULL → Usa VolumeMeasureUnit para resolver la unidad de medida vía ISNULL(MeasurementUnitId, VolumeMeasureUnit). else Usa MeasurementUnitId.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabelDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetail; MixingStation.UnitDoseType; MixingStation.Package; MixingStation.PackageDetail; MixingStation.PackagePersonalized; MixingStation.PackagePersonalizedDetail; Inventory.ATC; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabelDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewMagistralLabelDetails';
GO
