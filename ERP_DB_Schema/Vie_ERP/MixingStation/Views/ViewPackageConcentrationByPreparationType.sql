CREATE VIEW  [MixingStation].[ViewPackageConcentrationByPreparationType]
AS
WITH Cte_DetailByStandarPackage AS (
	SELECT 	pd.PackageId,
			pd.MainMedicine,
			pd.Thinner,
			pd.Vehicle,
			ipr.Id ProductId,
			a.Id AtcId,
			a.Code AtcCode,
			a.Name AtcName,
			pd.AmountTime Stability,
			pd.TimeUnit,
			pd.Concentration,
			-------------------------
			CASE pt.Class
				WHEN 2 THEN	imua.Abbreviation
				ELSE imu.Abbreviation
			END AS MeasurementUnitAbbreviation,
			CASE pt.Class
				WHEN 2 THEN	imua.Id	-- Para productos de tipo Item Medicamento
				ELSE imu.Id
			END AS MeasurementUnitId,
			-------------------------
			pd.Quantity
	FROM MixingStation.PackageDetail pd
	JOIN Inventory.ATC a ON pd.AtcId = a.Id
	JOIN Inventory.InventoryProduct ipr ON a.Id = ipr.ATCId
	JOIN Inventory.ProductType pt ON ipr.ProductTypeId = pt.Id
	JOIN Inventory.InventoryMeasurementUnit imua ON ISNULL(a.VolumeMeasureUnit, a.WeightMeasureUnit) = imua.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu ON ipr.MeasurementUnitId = imu.Id
) ,

Cte_DetailByPackagePersonalized AS (
	SELECT 	ppd.PackagePersonalizedId PackageId,
			ppd.MainMedicine,
			ppd.Thinner,
			ppd.Vehicle,
			ipr.Id ProductId,
			a.Id AtcId,
			a.Code AtcCode,
			a.Name AtcName,
			ppd.AmountTime Stability,
			ppd.TimeUnit,
			ppd.Concentration,
			-------------------------
			CASE pt.Class
				WHEN 2 THEN	imua.Abbreviation
				ELSE imu.Abbreviation
			END AS MeasurementUnitAbbreviation,
			CASE pt.Class
				WHEN 2 THEN	imua.Id	-- Para productos de tipo Item Medicamento
				ELSE imu.Id
			END AS MeasurementUnitId,
			-------------------------
			ppd.Quantity
	FROM MixingStation.PackagePersonalizedDetail ppd
	JOIN Inventory.ATC a ON ppd.AtcId = a.Id
	JOIN Inventory.InventoryProduct ipr ON a.Id = ipr.ATCId
	JOIN Inventory.ProductType pt ON ipr.ProductTypeId = pt.Id
	JOIN Inventory.InventoryMeasurementUnit imua ON ISNULL(a.VolumeMeasureUnit, a.WeightMeasureUnit) = imua.Id
	LEFT JOIN Inventory.InventoryMeasurementUnit imu ON ipr.MeasurementUnitId = imu.Id
)

SELECT 
		CONCAT(rmsd.CampaignDetailId, '-', 1, '-', ISNULL(pmm.AtcId, mm.AtcId), '-', ISNULL(pmm.ProductId, mm.ProductId)) AS Id,
		rmsd.CampaignDetailId,
		1 PreparationType, --Reconstitución
		ISNULL(pmm.ProductId ,mm.ProductId) AS ProductId,
		ISNULL(pmm.AtcId, mm.AtcId) AS MainMedicineId,
		ISNULL(pmm.AtcCode, mm.AtcCode) AS MainMedicineCode,
		ISNULL(pmm.AtcName, mm.AtcName) AS MainMedicineName,
		-------------------------
		ISNULL(pt.AtcId, t.AtcId) AtcIdReconstituent,
		NULL AtcIdVehicle,
		-------------------------
		ISNULL(pt.Stability, t.Stability) Stability,
		ISNULL(pt.TimeUnit, t.TimeUnit) TimeUnit,
		IIF(ISNULL(pt.TimeUnit, t.TimeUnit) = 1, 'HORA(S)', 'DIA(S)') TimeUnitNameStability,
		ISNULL(pt.Concentration, t.Concentration) Concentration,
		-------------------------
		ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation) AS MeasurementUnitMainMedicine,
		ISNULL(pmm.MeasurementUnitId, mm.MeasurementUnitId) AS MeasurementUnitMainMedicineId,
		ISNULL(pt.MeasurementUnitAbbreviation, t.MeasurementUnitAbbreviation) AS MeasurementUnitVolume,
		ISNULL(pt.MeasurementUnitId, t.MeasurementUnitId) AS MeasurementUnitVolumeId,
		-------------------------
		ISNULL(ISNULL(pt.Quantity, t.Quantity), 0) AS MaximumVolume
FROM MixingStation.RequestMixingStationDetail rmsd
LEFT JOIN Cte_DetailByStandarPackage mm ON rmsd.PackageId = mm.PackageId AND mm.MainMedicine = 1 AND rmsd.PackagePersonalizedId IS NULL
LEFT JOIN Cte_DetailByStandarPackage t ON rmsd.PackageId = t.PackageId AND t.Thinner = 1 AND rmsd.PackagePersonalizedId IS NULL
LEFT JOIN Cte_DetailByPackagePersonalized pmm ON rmsd.PackagePersonalizedId = pmm.PackageId	AND pmm.MainMedicine = 1
LEFT JOIN Cte_DetailByPackagePersonalized pt ON rmsd.PackagePersonalizedId = pt.PackageId AND pt.Thinner = 1
GROUP BY rmsd.CampaignDetailId, 
		ISNULL(pmm.AtcId, mm.AtcId),
		ISNULL(pmm.AtcCode, mm.AtcCode),
		ISNULL(pmm.AtcName, mm.AtcName),
		ISNULL(pmm.ProductId ,mm.ProductId),
		ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation),
		ISNULL(pmm.MeasurementUnitId, mm.MeasurementUnitId),
		ISNULL(pt.Stability, t.Stability),
		ISNULL(pt.TimeUnit, t.TimeUnit),
		ISNULL(pt.Concentration, t.Concentration),
		ISNULL(pt.Quantity, t.Quantity),
		ISNULL(pt.MeasurementUnitAbbreviation, t.MeasurementUnitAbbreviation),
		ISNULL(pt.MeasurementUnitId, t.MeasurementUnitId),
		ISNULL(pt.AtcId, t.AtcId)

UNION ALL

SELECT
	CONCAT(rmsd.CampaignDetailId, '-', 2, '-', ISNULL(pmm.AtcId, mm.AtcId), '-', ISNULL(pmm.ProductId, mm.ProductId)) AS Id,
	rmsd.CampaignDetailId,
	2 PreparationType, --Reconstitución y dilución
	ISNULL(pmm.ProductId ,mm.ProductId) AS ProdutId,
	ISNULL(pmm.AtcId, mm.AtcId) AS MainMedicineId,
	ISNULL(pmm.AtcCode, mm.AtcCode) AS MainMedicineCode,
	ISNULL(pmm.AtcName, mm.AtcName) AS MainMedicineName,
	-------------------------
	ISNULL(pt.AtcId, t.AtcId) AtcIdReconstituent,
	ISNULL(pv.AtcId, v.AtcId) AtcIdVehicle,
	-------------------------
	ISNULL(pv.Stability, v.Stability) Stability,
	ISNULL(pv.TimeUnit, v.TimeUnit) TimeUnit,
	IIF(ISNULL(pv.TimeUnit, v.TimeUnit) = 1, 'HORA(S)', 'DIA(S)') TimeUnitNameStability,
	ISNULL(pv.Concentration, v.Concentration) Concentration,
	-------------------------
	ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation) AS MeasurementUnitMainMedicine,
	ISNULL(pmm.MeasurementUnitId, mm.MeasurementUnitId) AS MeasurementUnitMainMedicineId,
	ISNULL(pv.MeasurementUnitAbbreviation, v.MeasurementUnitAbbreviation) AS MeasurementUnitVolume,
	ISNULL(pv.MeasurementUnitId, v.MeasurementUnitId) AS MeasurementUnitVolumeId,
	-------------------------
	ISNULL(ISNULL(pv.Quantity, v.Quantity), 0) AS MaximumVolume
FROM MixingStation.RequestMixingStationDetail rmsd
LEFT JOIN Cte_DetailByStandarPackage mm ON rmsd.PackageId = mm.PackageId AND mm.MainMedicine = 1 AND rmsd.PackagePersonalizedId IS NULL
LEFT JOIN Cte_DetailByStandarPackage t ON rmsd.PackageId = t.PackageId AND t.Thinner = 1 AND rmsd.PackagePersonalizedId IS NULL
LEFT JOIN Cte_DetailByStandarPackage v ON rmsd.PackageId = v.PackageId AND v.Vehicle = 1 AND rmsd.PackagePersonalizedId IS NULL
LEFT JOIN Cte_DetailByPackagePersonalized pmm ON rmsd.PackagePersonalizedId = pmm.PackageId	AND pmm.MainMedicine = 1
LEFT JOIN Cte_DetailByPackagePersonalized pt ON rmsd.PackagePersonalizedId = pt.PackageId AND pt.Thinner = 1
LEFT JOIN Cte_DetailByPackagePersonalized pv ON rmsd.PackagePersonalizedId = pv.PackageId AND pv.Vehicle = 1
GROUP BY rmsd.CampaignDetailId, 
		ISNULL(pmm.AtcId, mm.AtcId),
		ISNULL(pmm.AtcCode, mm.AtcCode),
		ISNULL(pmm.AtcName, mm.AtcName),
		ISNULL(pmm.ProductId ,mm.ProductId),
		ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation),
		ISNULL(pmm.MeasurementUnitId, mm.MeasurementUnitId),
		ISNULL(pv.Stability, v.Stability),
		ISNULL(pv.TimeUnit, v.TimeUnit),
		ISNULL(pv.Concentration, v.Concentration),
		ISNULL(pv.Quantity, v.Quantity),
		ISNULL(pv.MeasurementUnitAbbreviation, v.MeasurementUnitAbbreviation),
		ISNULL(pv.MeasurementUnitId, v.MeasurementUnitId),
		ISNULL(pv.AtcId, v.AtcId),
		ISNULL(pt.AtcId, t.AtcId)

UNION ALL

SELECT
	CONCAT(rmsd.CampaignDetailId, '-', 3, '-', ISNULL(pmm.AtcId, mm.AtcId), '-', ISNULL(pmm.ProductId, mm.ProductId)) AS Id,
	rmsd.CampaignDetailId,
	3 PreparationType, --Soluciones Inyectables
	ISNULL(pmm.ProductId ,mm.ProductId) AS ProdutId,
	ISNULL(pmm.AtcId, mm.AtcId) AS MainMedicineId,
	ISNULL(pmm.AtcCode, mm.AtcCode) AS MainMedicineCode,
	ISNULL(pmm.AtcName, mm.AtcName) AS MainMedicineName,
	-------------------------
	NULL AtcIdReconstituent,
	ISNULL(pv.AtcId, v.AtcId) AtcIdVehicle,
	-------------------------
	a.StabilityMaximumHours AS Stability,
	1 AS TimeUnit,
	'HORA(S)'AS TimeUnitNameStability,
	MixingStation.CalculateConcentration(a.FormulationType, a.Weight, a.Volume) AS Concentration,
	-------------------------
	ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation) AS MeasurementUnitMainMedicine,
	ISNULL(pmm.MeasurementUnitId, mm.MeasurementUnitId) AS MeasurementUnitMainMedicineId,
	IIF(a.VolumeMeasureUnit IS NOT NULL, ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation), 'mL') AS MeasurementUnitVolume,
	IIF(a.VolumeMeasureUnit IS NOT NULL, a.VolumeMeasureUnit, 19) AS MeasurementUnitVolumeId,
	-------------------------
	CAST(ISNULL(a.Volume, 0) AS DECIMAL(18,2)) AS MaximumVolume
FROM MixingStation.RequestMixingStationDetail rmsd
LEFT JOIN Cte_DetailByStandarPackage mm ON rmsd.PackageId = mm.PackageId AND mm.MainMedicine = 1 AND rmsd.PackagePersonalizedId IS NULL
LEFT JOIN Cte_DetailByStandarPackage v ON rmsd.PackageId = v.PackageId AND v.Vehicle = 1 AND rmsd.PackagePersonalizedId IS NULL
LEFT JOIN Cte_DetailByPackagePersonalized pmm ON rmsd.PackagePersonalizedId = pmm.PackageId	AND pmm.MainMedicine = 1
LEFT JOIN Cte_DetailByPackagePersonalized pv ON rmsd.PackagePersonalizedId = pv.PackageId AND pv.Vehicle = 1
JOIN Inventory.ATC a ON ISNULL(pmm.AtcId, mm.AtcId)= a.Id
GROUP BY rmsd.CampaignDetailId, 
		ISNULL(pmm.AtcId, mm.AtcId),
		ISNULL(pmm.AtcCode, mm.AtcCode),
		ISNULL(pmm.AtcName, mm.AtcName),
		ISNULL(pmm.ProductId ,mm.ProductId),
		ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation),
		ISNULL(pmm.MeasurementUnitId, mm.MeasurementUnitId),
		ISNULL(pv.AtcId, v.AtcId),
		a.StabilityMaximumHours, 
		a.FormulationType, 
		a.Weight, 
		a.Volume,
		a.VolumeMeasureUnit

GO