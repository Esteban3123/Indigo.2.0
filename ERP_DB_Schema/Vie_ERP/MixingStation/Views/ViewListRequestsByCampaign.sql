

CREATE view [MixingStation].[ViewListRequestsByCampaign] 
as 

	select CONCAT(r.Code, '-', IIF(rd.PackageId is not null, 'P', 'M'), '-', ISNULL(rd.PackageId, rd.ATCId), '-',rdp.StringIds) Id, 
		r.CMConfigurationId, 
		rd.ProductionLineId,
		r.Code as RequestCode, 
		rd.[Source] as RequestType, 
		rd.PackageId,
		rd.PackagePersonalizedId,
		case rd.[Source] 
			when 1 then 'Orden Médica'
			when 2 then 'Solicitud Externa Paciente'
			when 3 then 'Solicitud Externa Maquila'
			when 4 then 'Solicitud Inventario'
		end as RequestTypeName, 
		r.RequestDate, 
		r.RequestUser,
		IIF(pp.Id is not null, 3, IIF(p.Id is not null, 1, 2)) as ItemType, 
		IIF(pp.Id is not null, 'Paquete Personalizado', IIF(p.Id is not null, 'Paquete', 'Medicamento')) as ItemTypeName, 
		ISNULL(pp.Id, ISNULL(p.Id, a.Id)) as ItemId,  
		CASE 
			WHEN udt.MSClass = 2 THEN
				p.Code + ' - ' + p.Name
			ELSE 
				ISNULL(pp.Code + ' - ' + pp.Description, ISNULL(p.Code + ' - ' + p.Description, a.Code + ' - ' + a.Name))
		END ItemCodeName,
		0 as ExistenceQuantity, 
		ISNULL(rdp.Quantity, rd.Quantity) as RequestQuantity, 
		0 as ProduceQuantity, 
		'' as ControlNumber,
		rd.CareCenterCode, 
		IIF(cce.Id is not null, cce.Code + ' - ' + cce.[Description], rtrim(ltrim(cci.CODCENATE)) + ' - ' + rtrim(ltrim(cci.NOMCENATE))) as CareCenterCodeName,
		ISNULL(rdp.StringIds, rd.Id) StringIds,
		IIF(pl.Id is not null, pl.Code + ' - ' + pl.[Name], 'Sin Asignar') ProductionLineCodeName,
		rd.UnitDoseTypeId, 
		udt.Code + ' - ' + udt.[Description] as UnitDoseTypeCodeName,
		rd.RequestMixingStationId, 
		rd.Id as RequestMixingStationDetailId,
		cce.ContractExternalClientsId,
		ISNULL(cec.ManagesMaquila,0) as ManagesMaquila, 
		rdp.PatientCode,
		concat(ltrim(rtrim(pac.IPCODPACI)), ' - ', ltrim(rtrim(pac.IPNOMCOMP))) as PatientFullName
	from MixingStation.RequestMixingStationDetail rd
	inner join MixingStation.RequestMixingStation r on r.Id = rd.RequestMixingStationId
	left join (
		select RequestMixingStationDetailId, SUM(Quantity) Quantity, CampaignDetailId, string_agg(Id, ', ') StringIds, PatientCode
		from MixingStation.RequestMixingStationDetailPatients
		where CampaignDetailId is null and [Status] <> 3
		group by RequestMixingStationDetailId, CampaignDetailId, PatientCode
	) rdp on rdp.RequestMixingStationDetailId = rd.Id
	inner join MixingStation.UnitDoseType udt on udt.Id = rd.UnitDoseTypeId
	left join MixingStation.Package p on p.Id = rd.PackageId
	left join Inventory.ATC a on a.Id = rd.ATCId
	left join MixingStation.PackagePersonalized pp on pp.Id = rd.PackagePersonalizedId
	left join .ADCENATEN cci on cci.CODCENATE = rd.CareCenterCode and rd.Source in (1, 4)
	left join MixingStation.ExternalCareCenter cce on cce.Code = rd.CareCenterCode and rd.Source in (2, 3)
	left join MixingStation.ContractExternalClients cec ON cce.ContractExternalClientsId= cec.Id
	left join MixingStation.ProductionLine pl on pl.Id = rd.ProductionLineId
	left join ..INPACIENT pac on rdp.PatientCode = pac.IPCODPACI
	where rdp.CampaignDetailId is null and rd.CampaignDetailId is null  and rd.SendTo = 1 and rd.[Status] <> 3 --No mostrar anulados