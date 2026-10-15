CREATE VIEW MixingStation.ViewSumQuantityByPreparationStatus
AS
SELECT 
    cd.Id,
    SUM(rpds.Status) AS RequestedQuantity,
    SUM(IIF(rpds.Status = 1, rpds.Status, 0)) AS ProductionQuantity,
	SUM(IIF(rpds.Status = 2, rpds.Status, 0)) AS FinishedQuantity,
	SUM(IIF(rpds.Status = 3, rpds.Status, 0)) AS ReleasedQuantity,
	SUM(IIF(rpds.Status = 4, rpds.Status, 0)) AS ReprocessedQuantity,
	SUM(IIF(rpds.Status = 5, rpds.Status, 0)) AS RejectedQuantity,
	SUM(IIF(rpds.Status = 6, rpds.Status, 0)) AS CancelledQuantity
FROM MixingStation.CampaignDetail cd
JOIN MixingStation.RequestMixingStationDetail rmsd ON cd.Id = rmsd.CampaignDetailId
JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsd.Id = rpds.RequestMixingStationDetailId
GROUP BY cd.Id