
CREATE VIEW  [MixingStation].[ViewQuantityRemainingLow]
AS
 SELECT 
    ROW_NUMBER() OVER(ORDER BY qr.Id) AS RowNumber,
    qr.Id,
    qr.CampaignDetailId,
    ip.Id AS ProductId,
    ip.Code + ' - ' + ip.Name AS ProductFullName,
    bs.Id AS BatchSerialId,
    bs.BatchCode AS BatchSerialCode,
    CONVERT(VARCHAR, qr.OpeningDate, 103) AS OpeningDate,
    CONVERT(VARCHAR, 
        CASE 
            WHEN qr.UnitTimeStability = 1 THEN DATEADD(HOUR, qr.Stability, qr.OpeningDate)
            ELSE DATEADD(DAY, qr.Stability, qr.OpeningDate)
        END, 
    103) AS VctoStability,
    qr.Stability,
    CONVERT(VARCHAR, 
        CASE 
            WHEN bs.ExpirationDate IS NULL THEN ip.ExpirationDate 
            ELSE bs.ExpirationDate 
        END, 
    103) AS ExpirationDate,
    FORMAT(qr.Concentration, 'N2') AS Concentration,
	--CONVERT(VARCHAR,FORMAT(qr.Concentration, 'N2')) + ' ' + imum.Abbreviation + '/' + imuv.Abbreviation AS ConcentrationWithMeasurementUnit,
    CONVERT(VARCHAR, FORMAT(qr.Concentration, 'N2')) + ' mg/ml' AS ConcentrationWithMeasurementUnit,
    FORMAT(qr.RemnantVolume, 'N2') AS RemnantVolume,
    FORMAT(ROUND(qr.Quantity, 2), 'N2') AS Quantity,
    crr.Id AS CauseReprocessingRejectionId,
    crr.Code AS CauseReprocessingRejectionCode,
    crr.Name AS CauseReprocessingRejectionName,
    qr.Observations,
    w1.Code + ' - ' + w1.Name AS WareHouseRemaining,
    c1.Name AS MixingStation,
    w.IdMixingStation,
    w.IdWarehouse,
    qr.Status,
    qp.Fullname AS QP,
    qc.Fullname AS QC
FROM MixingStation.QuantityRemaining qr WITH (NOLOCK)
JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON qr.BatchSerialId = bs.Id AND qr.Status = 2
JOIN Inventory.InventoryMeasurementUnit imum WITH (NOLOCK) ON qr.UnitQuantity = imum.Id
JOIN Inventory.InventoryMeasurementUnit imuv WITH (NOLOCK) ON qr.UnitRemnantVolume = imuv.Id
JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON qr.ProductId = ip.Id
LEFT JOIN MixingStation.CauseReprocessingRejection crr WITH (NOLOCK) ON qr.CauseReprocessingRejectionId = crr.Id
JOIN MixingStation.CampaignDetail cd WITH (NOLOCK) ON qr.CampaignDetailId = cd.Id
JOIN MixingStation.Campaign c WITH (NOLOCK) ON cd.CampaignId = c.Id
JOIN MixingStation.CMConfiguration c1 WITH (NOLOCK) ON c.CMConfigurationId = c1.Id
JOIN MixingStation.CMWarehouse w WITH (NOLOCK) ON qr.WareHouseId = w.IdWarehouse AND c1.Id = w.IdMixingStation
JOIN Inventory.Warehouse w1 WITH (NOLOCK) ON w.IdWarehouse = w1.Id
JOIN MixingStation.CampaignDetailUsers cdu_qp WITH (NOLOCK) ON cd.Id = cdu_qp.CampaignDetailId AND cdu_qp.UserRole = 1
JOIN [Security].[User] su_qp ON cdu_qp.UserId = su_qp.Id
JOIN [Security].Person qp ON su_qp.IdPerson = qp.Id
JOIN MixingStation.CampaignDetailUsers cdu_qc WITH (NOLOCK) ON cd.Id = cdu_qc.CampaignDetailId AND cdu_qc.UserRole = 2
JOIN [Security].[User] su_qc ON cdu_qc.UserId = su_qc.Id
JOIN [Security].Person qc ON su_qc.IdPerson = qc.Id