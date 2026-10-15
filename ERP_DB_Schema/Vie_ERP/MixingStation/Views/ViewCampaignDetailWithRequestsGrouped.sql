


CREATE VIEW [MixingStation].[ViewCampaignDetailWithRequestsGrouped]
AS

WITH packagedetail AS (
	SELECT pdw.Id AS PackageDetailId
		, pdw.PackageId
		, atc.Id AS ATCId
		, atc.Code
		, atc.AbbreviationName AS Name
	FROM MixingStation.PackageDetail pdw
	JOIN Inventory.ATC atc ON pdw.AtcId = atc.Id
	WHERE pdw.MainMedicine = 1
),
packagePdetail AS (
	SELECT pdw.Id AS PackagePersonalizedDetailId
		, pdw.PackagePersonalizedId
		, atc.Id AS ATCId
		, atc.Code
		, atc.AbbreviationName AS Name
	FROM MixingStation.PackagePersonalizedDetail pdw
	JOIN Inventory.ATC atc ON pdw.AtcId = atc.Id
	WHERE pdw.MainMedicine = 1
) 
select t.Id, t.Code, t.Name, t.CampaignDetailId, count(1) as Quantity
from (
SELECT 
	COALESCE(ppd.AtcId, pda.AtcId, a.Id) AS Id
	, COALESCE(ppd.Code, pda.Code, a.Code) AS Code
	, COALESCE(ppd.Name, pda.Name, a.Name) AS Name
	, cd.Id AS CampaignDetailId
FROM MixingStation.RequestMixingStationDetail rd
JOIN MixingStation.RequestMixingStation r on r.Id = rd.RequestMixingStationId
JOIN MixingStation.UnitDoseType udt on udt.Id = rd.UnitDoseTypeId
LEFT JOIN (
	select RequestMixingStationDetailId, SUM(Quantity) Quantity, CampaignDetailId, string_agg(Id, ', ') StringIds, Bed
	from MixingStation.RequestMixingStationDetailPatients
	where CampaignDetailId is not null and Status <> 3
	group by RequestMixingStationDetailId, CampaignDetailId, Bed
) rdp on rdp.RequestMixingStationDetailId = rd.Id
JOIN MixingStation.CampaignDetail cd on ISNULL(rd.CampaignDetailId,rdp.CampaignDetailId) =cd.Id
LEFT JOIN MixingStation.Package p on p.Id = rd.PackageId
LEFT JOIN Inventory.ATC a on a.Id = rd.ATCId
LEFT JOIN MixingStation.PackagePersonalized pp on pp.Id = rd.PackagePersonalizedId
LEFT JOIN MixingStation.ProductionLine pl on pl.Id = rd.ProductionLineId
LEFT JOIN packagedetail pda on pda.PackageId = p.Id
LEFT JOIN packagePdetail ppd on ppd.PackagePersonalizedId = pp.Id
WHERE rdp.CampaignDetailId IS NOT NULL OR rd.CampaignDetailId IS NOT NULL
) as t
group by t.Id, t.Code, t.Name, t.CampaignDetailId
