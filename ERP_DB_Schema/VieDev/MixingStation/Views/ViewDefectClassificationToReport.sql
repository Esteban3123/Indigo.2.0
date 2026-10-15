





CREATE view [MixingStation].[ViewDefectClassificationToReport]
as

WITH cte_Details AS (
    SELECT 
        STRING_AGG(CAST(rpdsdc.Observation AS VARCHAR(MAX)), ', ') AS Observations,
        STRING_AGG(CAST(rpds.BatchCode AS VARCHAR(MAX)), ', ') AS BatchCodes,
        rmsd.CampaignDetailId
    FROM MixingStation.RequestPackageDetailStatusDefectClassification rpdsdc
    JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.Id = rpdsdc.RequestPackageDetailStatusId
    JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
    GROUP BY rmsd.CampaignDetailId
)

SELECT dci.Id
	, dcg.Id DefectClassificationGroupId
	, dcg.Description DefectClassificationGroupName
	, dcg.Weight DefectClassificationGroupWeight
	, dci.Code ItemCode
	, dci.Description ItemDescription
	, dci.Weight ItemWeight
	, dci.Critical
	, dci.Less
	, SUM(IIF(rpdsdcd.Production = 1, 1, 0)) Production
	, SUM(IIF(rpdsdcd.Quality = 1, 1, 0)) Quality
	, cte.CampaignDetailId
	, cdu.UserCode UserSupervisor
	, cte.Observations
	, NULL TheoreticalWeight
	, NULL InputsWeight
	, NULL TheoreticalAndInputsWeight
	, NULL MaximunWeight
	, NULL MinimunWeight
	, NULL ActualWeight
	, NULL ValidationResult
	, udt.MSClass UnitDoseTypeMSClass
	, cte.BatchCodes
	, cd.CampaignNumber
FROM MixingStation.RequestPackageDetailStatusDefectClassificationDetail rpdsdcd   
JOIN MixingStation.RequestPackageDetailStatusDefectClassification rpdsdc ON rpdsdc.Id = rpdsdcd.RequestPackageDetailStatusDefectClassificationId
JOIN MixingStation.DefectClassificationItem dci ON dci.Id = rpdsdcd.DefectClassificationItemId
JOIN MixingStation.DefectClassificationGroup dcg ON dcg.Id = dci.DefectClassificationGroupId
JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.Id = rpdsdc.RequestPackageDetailStatusId
JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.CampaignDetail cd ON cd.Id = rmsd.CampaignDetailId
JOIN MixingStation.UnitDoseType udt ON udt.Id = rmsd.UnitDoseTypeId
JOIN cte_Details cte ON cte.CampaignDetailId = rmsd.CampaignDetailId
LEFT JOIN MixingStation.CampaignDetailUsers cdu  ON cdu.CampaignDetailId = rmsd.CampaignDetailId AND cdu.UserRole = 1
WHERE udt.MSClass <> 2
GROUP BY dci.Id, dcg.Id, dcg.Description, dcg.Weight, dci.Code, dci.Description, dci.Weight, dci.Critical,
dci.Less, cte.CampaignDetailId, cdu.UserCode, cte.Observations, udt.MSClass, cte.BatchCodes, cd.CampaignNumber

UNION ALL 

SELECT rpdsdcd.Id
	, dcg.Id DefectClassificationGroupId
	, dcg.Description DefectClassificationGroupName
	, dcg.Weight DefectClassificationGroupWeight
	, dci.Code ItemCode
	, dci.Description ItemDescription
	, dci.Weight ItemWeight
	, dci.Critical
	, dci.Less
	, SUM(IIF(rpdsdcd.Production = 1, 1, 0)) Production
	, SUM(IIF(rpdsdcd.Quality = 1, 1, 0)) Quality
	, rmsd.CampaignDetailId
	, cdu.UserCode UserSupervisor
	, rpdsdc.Observation Observations
	, vwn.TheoreticalWeight
	, vwn.InputsWeight
	, vwn.TheoreticalAndInputsWeight
	, vwn.MaximunWeight
	, vwn.MinimunWeight
	, rpdsdc.ActualWeight
	, IIF(rpdsdc.ActualWeight BETWEEN vwn.MinimunWeight AND vwn.MaximunWeight, 1, 0) AS ValidationResult
	, udt.MSClass UnitDoseTypeMSClass
	, rpds.BatchCode BatchCodes
	, cd.CampaignNumber
FROM MixingStation.RequestPackageDetailStatusDefectClassificationDetail rpdsdcd   
JOIN MixingStation.RequestPackageDetailStatusDefectClassification rpdsdc ON rpdsdc.Id = rpdsdcd.RequestPackageDetailStatusDefectClassificationId
JOIN MixingStation.DefectClassificationItem dci ON dci.Id = rpdsdcd.DefectClassificationItemId
JOIN MixingStation.DefectClassificationGroup dcg ON dcg.Id = dci.DefectClassificationGroupId
JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.Id = rpdsdc.RequestPackageDetailStatusId
JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.CampaignDetail cd ON cd.Id = rmsd.CampaignDetailId
JOIN MixingStation.UnitDoseType udt ON udt.Id = rmsd.UnitDoseTypeId
JOIN MixingStation.ViewListValidationWeightNPT vwn ON vwn.RequestPackageDetailStatusId = rpds.Id	
JOIN MixingStation.CampaignDetailUsers cdu  ON cdu.CampaignDetailId = rmsd.CampaignDetailId AND cdu.UserRole = 1
WHERE udt.MSClass = 2
GROUP BY  rpdsdcd.Id, dcg.Id, dcg.Description, dcg.Weight, dci.Code, dci.Description, dci.Weight, dci.Critical, dci.Less,
rmsd.CampaignDetailId, cdu.UserCode, rpdsdc.Observation, vwn.TheoreticalWeight, vwn.InputsWeight, vwn.TheoreticalAndInputsWeight,
vwn.MaximunWeight, vwn.MinimunWeight, rpdsdc.ActualWeight, udt.MSClass, rpds.BatchCode, cd.CampaignNumber