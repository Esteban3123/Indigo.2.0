
CREATE view [MixingStation].[ViewReleaseLineToReport]
As

WITH last_release_line(CampaignDetailId, ReleaseLineId, UnitDoseCodeName, WorkingAreaId, BatchesProcessed) as (
	select top 1 lcd.Id
		, lrl.Id as ReleaseLineId
		, concat(lud.Code, ' - ', lud.Description) as UnitDoseCodeName		
		, lrl.WorkingAreaId
		, COUNT(rpds.Id) BatchesProcessed
	from MixingStation.CampaignDetail lcd
	INNER join MixingStation.ReleaseLine lrl ON lrl.CampaignDetailId = lcd.Id
	INNER join MixingStation.UnitDoseType lud ON lcd.UnitDoseTypeId = lud.Id
	INNER JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.CampaignDetailId = lcd.Id
	INNER JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.RequestMixingStationDetailId = rmsd.Id AND rpds.Status = 3
	GROUP BY lcd.Id, lrl.Id, lud.Code, lud.Description, lrl.WorkingAreaId, lcd.ModificationDate
	ORDER BY lcd.ModificationDate DESC
)
SELECT rl.Id
		, rl.CampaignDetailId
		, rl.WorkingAreaId
		, rl.AirIgnitionTime
		, rl.CPIIgnitionTime
		, rl.EntryMixingStationTime
		, rl.PreparationStartTime
		, rl.IsSterile
		, rl.AdequacyItem1
		, rl.AdequacyItem2
		, rl.AdequacyItem3
		, rl.AdequacyItem4
		, rl.AdequacyItem5
		, rl.AdequacyItem6
		, rl.AdequacyItem7
		, rl.AdequacyItem8
		, rl.ConditioningItem1
		, rl.ConditioningItem2
		, rl.ConditioningItem3
		, rl.ConditioningItem4
		, rl.ConditioningItem5
		, rl.ConditioningItem6
		, rl.ConditioningItem7
		, rl.ConditioningItem8
		, rl.ApplyItem9
		, rl.ApplyItem11
		, rl.ValueItem9
		, rl.ValueItem11
		, rl.Observation
		, COUNT(rpds.Id) BatchesProcess
		, cd.ProductionLineId
		, cd.UnitDoseTypeId
		, cd.CampaignStatus
		, pl.Code as ProductionLineCode
		, pl.Name as ProductionLineName
		, ud.Code as UnitDoseCode
		, ud.Description as UnitDoseName
		, wa.Code as WorkingAreaCode
		, wa.Description as WorkingAreaName
		, rl.CreationUser as CreationUser
		, rl.CreationDate
		, lrl.UnitDoseCodeName as LastUnitDoseCodeName
		, lrl.BatchesProcessed LastCampaignDetailBatchesProcessed
		, cdu.UserCode As UserSupervisor
from MixingStation.ReleaseLine rl
INNER JOIN MixingStation.CampaignDetail cd on rl.campaignDetailid = cd.Id
INNER JOIN MixingStation.UnitDoseType ud on cd.UnitDoseTypeId = ud.Id
INNER JOIN MixingStation.ProductionLine pl on cd.ProductionLineId = pl.Id
INNER JOIN MixingStation.WorkingArea wa on rl.WorkingAreaId = wa.Id
INNER JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.CampaignDetailId = cd.Id
INNER JOIN MixingStation.RequestPackageDetailStatus rpds ON rpds.RequestMixingStationDetailId = rmsd.Id AND rpds.Status = 3

LEFT JOIN MixingStation.CampaignDetailUsers cdu on cdu.CampaignDetailId = cd.Id and cdu.UserRole = 1
LEFT JOIN last_release_line lrl on lrl.WorkingAreaId = rl.WorkingAreaId And lrl.CampaignDetailId <> rl.CampaignDetailId
GROUP BY rl.Id, rl.CampaignDetailId, rl.WorkingAreaId, rl.AirIgnitionTime, rl.CPIIgnitionTime, rl.EntryMixingStationTime,rl.PreparationStartTime,
rl.IsSterile, rl.AdequacyItem1, rl.AdequacyItem2, rl.AdequacyItem3, rl.AdequacyItem4, rl.AdequacyItem5, rl.AdequacyItem6, rl.AdequacyItem7,
rl.AdequacyItem8, rl.ConditioningItem1, rl.ConditioningItem2, rl.ConditioningItem3, rl.ConditioningItem4, rl.ConditioningItem5, rl.ConditioningItem6,
rl.ConditioningItem7, rl.ConditioningItem8, rl.ApplyItem9, rl.ApplyItem11, rl.ValueItem9, rl.ValueItem11, rl.Observation, cd.ProductionLineId,
cd.UnitDoseTypeId, cd.CampaignStatus, pl.Code, pl.Name, ud.Code, ud.Description, wa.Code, wa.Description, rl.CreationUser, rl.CreationDate, lrl.UnitDoseCodeName,
lrl.BatchesProcessed, cdu.UserCode
GO