



CREATE view [MixingStation].[ViewListCampaignDetailWithRequests] 
as 

select rd.Id Id, 
	rd.Id as RequestMixingStationDetailId,
	r.CMConfigurationId, 
	rd.ProductionLineId,
	r.Code RequestCode, 
	rd.Source RequestType, 	
	case rd.Source 
		when 1 then 'Orden Médica'
		when 2 then 'Solicitud Externa Paciente'
		when 3 then 'Solicitud Externa Maquila'
		when 4 then 'Solicitud Inventario'
	end RequestTypeName, 
	r.RequestDate, 
	r.RequestUser,
	IIF(pp.Id is not null, 3, IIF(p.Id is not null, 1, 2)) ItemType, 
	IIF(pp.Id is not null, 'Paquete Personalizado', IIF(p.Id is not null, 'Paquete', 'Medicamento')) ItemTypeName, 
	ISNULL(pp.Id, ISNULL(p.Id, a.Id)) ItemId, 
	IIF(udt.MSClass <> 2,(ISNULL(pp.Code + ' - ' + pp.Description, ISNULL(p.Code + ' - ' + p.Description, a.Code + ' - ' + a.Name))),p.Code + ' - ' + p.Name) ItemCodeName,
	0 ExistenceQuantity,
	ISNULL(rdp.Quantity, rd.Quantity) RequestQuantity, 0 ProduceQuantity, '' ControlNumber,
	rd.CareCenterCode, 
	IIF(cce.Id is not null, cce.Code + ' - ' + cce.Description, rtrim(ltrim(cci.CODCENATE)) + ' - ' + rtrim(ltrim(cci.NOMCENATE))) CareCenterCodeName,
	ISNULL(rdp.StringIds, rd.Id) StringIds,
	IIF(pl.Id is not null, pl.Code + ' - ' + pl.Name, 'Sin Asignar') ProductionLineCodeName,
	rd.UnitDoseTypeId, udt.Code + ' - ' + udt.Description UnitDoseTypeCodeName,
	ISNULL(rdp.CampaignDetailId, rd.CampaignDetailId) CampaignDetailId,
	ISNULL(d.PREESTADO, 0) StatusHCPRESCRA,
	IsNull(rd.LabelType, p.LabelType) as LabelType,
	Case IsNull(rd.LabelType, p.LabelType) 
		When 1 Then 'Bolsa'
		When 2 Then 'Mediana'
		When 3 Then 'Jeringa'
		Else NULL
	End As LabelTypeName
	,cd.CampaignStatus,
	rdp.Bed,
	(
		SELECT COUNT(*) 
		FROM MixingStation.RequestPackageDetailStatus rpds
		where rpds.RequestMixingStationDetailId = rd.Id AND rpds.Status NOT IN (5,6)
	) As ManageQuantity
    , ISNULL(re.IsReadjustment,0) IsReadjustment
	,iif(rd.RequestPackageDetailStatusId is null,0,1) HasReadjustment
from MixingStation.RequestMixingStationDetail rd
inner join MixingStation.RequestMixingStation r on r.Id = rd.RequestMixingStationId
inner join MixingStation.UnitDoseType udt on udt.Id = rd.UnitDoseTypeId
left join (
	select RequestMixingStationDetailId, SUM(Quantity) Quantity, CampaignDetailId, string_agg(Id, ', ') StringIds, Bed
	from MixingStation.RequestMixingStationDetailPatients
	where CampaignDetailId is not null and Status <> 3
	group by RequestMixingStationDetailId, CampaignDetailId, Bed
) rdp on rdp.RequestMixingStationDetailId = rd.Id
inner join MixingStation.CampaignDetail cd on ISNULL(rd.CampaignDetailId,rdp.CampaignDetailId) =cd.Id
left join MixingStation.Package p on p.Id = rd.PackageId
left join Inventory.ATC a on a.Id = rd.ATCId
left join MixingStation.PackagePersonalized pp on pp.Id = rd.PackagePersonalizedId
left join .ADCENATEN cci on cci.CODCENATE = rd.CareCenterCode and rd.Source in (1, 4)
left join MixingStation.ExternalCareCenter cce on cce.Code = rd.CareCenterCode and rd.Source in (2, 3)
left join MixingStation.ProductionLine pl on pl.Id = rd.ProductionLineId
left join (
	select p.RequestMixingStationDetailId, MAX(a.ID) HCPRESCRAId, p.CampaignDetailId
	from MixingStation.RequestMixingStationDetailPatients p
	inner join .HCFARMEPD h on h.ID = p.EntityId
	inner join .HCPRESCRA a on h.IdSourceTable = a.ID
	where 
		p.EntityName = 'HCFARMEPD' And h.SourceTable = 'HCPRESCRA' And p.Status <> 3
	group by p.RequestMixingStationDetailId, p.CampaignDetailId
) data on data.RequestMixingStationDetailId = rd.Id and data.CampaignDetailId = rdp.CampaignDetailId
left join .HCPRESCRA d on d.ID = data.HCPRESCRAId
left join(
			Select rpd.CampaignDetailId
				, Iif(r.IsReadjustment =1,1 ,0) as IsReadjustment
				,r.RequestPackageDetailStatusId
				,rpds.RequestMixingStationDetailId
			From MixingStation.RequestPackageDetailStatus rpds
			Inner Join MixingStation.RequestMixingStationDetail rpd On rpd.id = rpds.RequestMixingStationDetailId 
			inner join  MixingStation.Readjustments r on r.RequestPackageDetailStatusId = rpds.Id and r.IsReadjustment=1
			Group By  rpd.CampaignDetailId, r.IsReadjustment, r.RequestPackageDetailStatusId, rpds.RequestMixingStationDetailId
) as re on re.RequestMixingStationDetailId = rd.Id
where rdp.CampaignDetailId is not null or rd.CampaignDetailId is not null