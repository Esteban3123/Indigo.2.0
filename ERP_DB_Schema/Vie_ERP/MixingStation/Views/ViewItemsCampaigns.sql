











CREATE view [MixingStation].[ViewItemsCampaigns] 
as 

with dataPatients as (
	select rdp.CampaignDetailId, rdp.RequestMixingStationDetailId, SUM(rdp.Quantity) Quantity
	from MixingStation.RequestMixingStationDetailPatients rdp
	where rdp.CampaignDetailId is not null
	group by rdp.CampaignDetailId, rdp.RequestMixingStationDetailId
), 
dataDetail as (
	select ISNULL(dataPatients.CampaignDetailId, rd.CampaignDetailId) CampaignDetailId,
	ISNULL(dataPatients.Quantity, rd.Quantity) Quantity,
	rd.ATCId, rd.PackageId, rd.PackagePersonalizedId
	from MixingStation.RequestMixingStationDetail rd
	left join dataPatients on dataPatients.RequestMixingStationDetailId = rd.Id
	where rd.CampaignDetailId is not null or dataPatients.CampaignDetailId is not null
)

select CONCAT(cd.Id, '') Id,
dataDetail.CampaignDetailId,
IIF(pp.Id is not null, 3, IIF(p.Id is not null, 1, 2)) ItemType, 
IIF(pp.Id is not null, 'Paquete Personalizado', IIF(p.Id is not null, 'Paquete', 'Medicamento')) ItemTypeName, 
ISNULL(pp.Id, ISNULL(p.Id, a.Id)) ItemId, 
ISNULL(pp.Code + ' - ' + pp.Name, ISNULL(p.Code + ' - ' + p.Name, a.Code + ' - ' + a.Name)) ItemCodeName,
dataDetail.Quantity
from MixingStation.CampaignDetail cd
inner join dataDetail on dataDetail.CampaignDetailId = cd.Id
left join Inventory.ATC a on a.Id = dataDetail.ATCId
left join MixingStation.Package p on p.Id = dataDetail.PackageId
left join MixingStation.PackagePersonalized pp on pp.Id = dataDetail.PackagePersonalizedId