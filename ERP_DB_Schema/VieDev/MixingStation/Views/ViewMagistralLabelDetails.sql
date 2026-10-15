
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