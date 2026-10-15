CREATE VIEW [MixingStation].[ViewParenteralNutritionLabelCampaign]
AS
	SELECT 
		CONCAT(CampaignDetailId, '-', rpds.Id) KeyView
		, cd.Id CampaignDetailId
		, rpds.Id RequestPackageDetailStatusId
	FROM MixingStation.CampaignDetail cd
	JOIN MixingStation.RequestMixingStationDetail rmsd ON cd.Id = rmsd.CampaignDetailId
	JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsd.Id = rpds.RequestMixingStationDetailId