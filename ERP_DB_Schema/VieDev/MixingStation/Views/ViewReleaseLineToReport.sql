
CREATE view [MixingStation].[ViewReleaseLineToReport]
As

with last_release_line(CampaignDetailId, CampaignDetailNumber, ReleaseLineId, UnitDoseCodeName, WorkingAreaId) as (
	select top 1 lcd.Id
		, lcd.CampaignNumber
		, lrl.Id as ReleaseLineId
		, concat(lud.Code, ' - ', lud.Description) as UnitDoseCodeName		
		, lrl.WorkingAreaId
	from MixingStation.CampaignDetail lcd with(nolock)
	inner join MixingStation.ReleaseLine lrl with(nolock) on lrl.CampaignDetailId = lcd.Id
	inner join MixingStation.UnitDoseType lud with(nolock) on lcd.UnitDoseTypeId = lud.Id
	order by lcd.ModificationDate desc
)
select rl.Id, rl.CampaignDetailId, rl.WorkingAreaId, rl.AirIgnitionTime, rl.CPIIgnitionTime
	, rl.EntryMixingStationTime, rl.PreparationStartTime, rl.IsSterile, rl.AdequacyItem1, rl.AdequacyItem2
	, rl.AdequacyItem3, rl.AdequacyItem4, rl.AdequacyItem5, rl.AdequacyItem6, rl.AdequacyItem7, rl.AdequacyItem8
	, rl.ConditioningItem1, rl.ConditioningItem2, rl.ConditioningItem3, rl.ConditioningItem4, rl.ConditioningItem5
	, rl.ConditioningItem6, rl.ConditioningItem7, rl.ConditioningItem8, rl.ApplyItem9, rl.ApplyItem11
	, rl.ValueItem9, rl.ValueItem11, rl.Observation
	, cd.CampaignNumber, cd.ProductionLineId, cd.UnitDoseTypeId, cd.CampaignStatus
	, pl.Code as ProductionLineCode, pl.Name as ProductionLineName, ud.Code as UnitDoseCode, ud.Description as UnitDoseName
	, wa.Code as WorkingAreaCode, wa.Description as WorkingAreaName
	, rl.CreationUser as CreationUser
	, rl.CreationDate
	--, (
	--	select top 1 concat(lud.Code, ' - ', lud.Description)
	--	from MixingStation.CampaignDetail lcd with(nolock)
	--	inner join MixingStation.ReleaseLine lrl with(nolock) on lrl.CampaignDetailId = lcd.Id
	--	inner join MixingStation.UnitDoseType lud with(nolock) on lcd.UnitDoseTypeId = lud.Id
	--	where lrl.WorkingAreaId = rl.WorkingAreaId And rl.CampaignDetailId <> lrl.CampaignDetailId
	--	order by lcd.ModificationDate desc
	--) as LastUnitDoseReleaseLine
	, lrl.UnitDoseCodeName as LastUnitDoseCodeName
	, lrl.CampaignDetailNumber as LastCampaignDetailNumber
	, cdu.UserCode As UserSupervisor
from MixingStation.ReleaseLine rl with(nolock)
inner join MixingStation.CampaignDetail cd with(nolock) on rl.campaignDetailid = cd.Id
inner join MixingStation.UnitDoseType ud with(nolock) on cd.UnitDoseTypeId = ud.Id
inner join MixingStation.ProductionLine pl with(nolock) on cd.ProductionLineId = pl.Id
inner join MixingStation.WorkingArea wa with(nolock) on rl.WorkingAreaId = wa.Id

left join MixingStation.CampaignDetailUsers cdu with(nolock) on cdu.CampaignDetailId = cd.Id and cdu.UserRole = 1
--left join last_release_line lrl with(nolock) on lrl.WorkingAreaId = rl.WorkingAreaId And lrl.CampaignDetailId = rl.CampaignDetailId And lrl.ReleaseLineId <> rl.Id
left join last_release_line lrl with(nolock) on lrl.WorkingAreaId = rl.WorkingAreaId And lrl.CampaignDetailId <> rl.CampaignDetailId