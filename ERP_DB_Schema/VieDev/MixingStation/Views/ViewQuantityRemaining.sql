


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