
CREATE view [MixingStation].[ViewListDashboardProductionSchedule] 
as 

with dataTemp as (
	select r.CMConfigurationId, 
		rd.ProductionLineId, 
		rd.PackageId, 
		rd.ATCId, 
		rd.PackagePersonalizedId, 
		r.Code, 
		r.RequestDate, 
		r.RequestUser, 
		rd.CareCenterCode, 
		rd.Source, 
		rdp.PatientCode,
		SUM(ISNULL(rdp.Quantity, rd.Quantity)) Quantity, string_agg(ISNULL(rdp.Id, rd.Id), ', ') StringIds, ISNULL(rdp.CampaignDetailId, rd.CampaignDetailId) CampaignDetailId
	from MixingStation.RequestMixingStation r
	inner join MixingStation.RequestMixingStationDetail rd on rd.RequestMixingStationId = r.Id
	left join MixingStation.RequestMixingStationDetailPatients rdp on rdp.RequestMixingStationDetailId = rd.Id
	where r.Status = 2
	group by r.CMConfigurationId, rd.ProductionLineId, rd.PackageId, rd.ATCId, rd.PackagePersonalizedId, r.Code, r.RequestDate, 
		r.RequestUser, rd.CareCenterCode, rd.Source, rdp.CampaignDetailId, rd.CampaignDetailId, rdp.PatientCode
)

select CONCAT(data.PatientCode, '-', data.Code, '-',cd.Id, IIF(pp.Id is not null, 'PP', IIF(data.PackageId is not null, 'P', 'M')), '-', ISNULL(data.PackageId, data.ATCId),ISNULL(pp.Id, ISNULL(p.Id, a.Id))) Id, 
	data.CMConfigurationId, data.ProductionLineId,
	data.Code RequestCode, data.Source RequestType, 
	case data.Source 
		when 1 then 'Orden Médica'
		when 2 then 'Solicitud Externa Paciente'
		when 3 then 'Solicitud Externa Maquila'
		when 4 then 'Solicitud Inventario'
	end RequestTypeName, 
	data.RequestDate, data.RequestUser,
	IIF(pp.Id is not null, 3, IIF(p.Id is not null, 1, 2)) ItemType, IIF(pp.Id is not null, 'Paquete Personalizado', IIF(p.Id is not null, 'Paquete', 'Medicamento')) ItemTypeName, 
	ISNULL(pp.Id, ISNULL(p.Id, a.Id)) ItemId, ISNULL(pp.Code + ' - ' + pp.Description, ISNULL(p.Code + ' - ' + p.Description, a.Code + ' - ' + a.Name)) ItemCodeName,
	0 ExistenceQuantity, data.Quantity RequestQuantity, 0 ProduceQuantity, '' ControlNumber,
	data.CareCenterCode, IIF(cce.Id is not null, cce.Code + ' - ' + cce.Description, rtrim(ltrim(cci.CODCENATE)) + ' - ' + rtrim(ltrim(cci.NOMCENATE))) CareCenterCodeName,
	data.StringIds,
	data.CampaignDetailId, cd.CampaignNumber, 'Campaña # ' + cast(cd.CampaignNumber as varchar(10)) CampaignDescription,
	concat(ltrim(rtrim(pac.IPCODPACI)), ' - ', pac.IPNOMCOMP) as PatientCodeName
from dataTemp data
left join INPACIENT pac on data.PatientCode = pac.IPCODPACI
left join MixingStation.Package p on p.Id = data.PackageId
left join Inventory.ATC a on a.Id = data.ATCId
left join MixingStation.PackagePersonalized pp on pp.Id = data.PackagePersonalizedId
left join .ADCENATEN cci on cci.CODCENATE = data.CareCenterCode and data.Source in (1, 4)
left join MixingStation.ExternalCareCenter cce on cce.Code = data.CareCenterCode and data.Source in (2, 3)
left join MixingStation.CampaignDetail cd on cd.Id = data.CampaignDetailId
where data.Quantity > 0