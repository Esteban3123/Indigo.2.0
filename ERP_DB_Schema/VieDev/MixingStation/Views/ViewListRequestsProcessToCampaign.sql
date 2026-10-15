


CREATE view [MixingStation].[ViewListRequestsProcessToCampaign] 
as 


--Solicitudes que SI tengan pacientes
select 
CONCAT(rms.Id, '-', rms.Code, '-', 1,'-',temp.PatientCode) Id,
rms.CMConfigurationId,
rd.ProductionLineId,
rms.Code RequestCode,
rd.Source RequestType,
rd.PackageId,
rd.PackagePersonalizedId,
case rd.Source 
	when 1 then 'Orden Médica'
	when 2 then 'Solicitud Externa Paciente'
	when 3 then 'Solicitud Externa Maquila'
	when 4 then 'Solicitud Inventario'
end RequestTypeName,
rms.RequestDate,
IIF(pp.Id is not null, 3, IIF(p.Id is not null, 1, 2)) ItemType, 
IIF(pp.Id is not null, 'Paquete Personalizado', IIF(p.Id is not null, 'Paquete', 'Medicamento')) ItemTypeName,
ISNULL(pp.Code + ' - ' + pp.Name, ISNULL(p.Code + ' - ' + p.Name, a.Code + ' - ' + a.Name)) ItemCodeName,
IIF(cce.Id is not null, cce.Code + ' - ' + cce.Description, rtrim(ltrim(cci.CODCENATE)) + ' - ' + rtrim(ltrim(cci.NOMCENATE))) CareCenterCodeName,
ISNULL(temp.Quantity, rd.Quantity) RequestQuantity,
temp.StringIds,
IIF(pl.Id is not null, pl.Code + ' - ' + pl.Name, 'Sin Asignar') ProductionLineCodeName,
rd.UnitDoseTypeId, udt.Code + ' - ' + udt.Description UnitDoseTypeCodeName,
rms.Id RequestMixingStationId, 
temp.Id RequestMixingStationDetailId,
cce.ContractExternalClientsId,
ISNULL(cec.ManagesMaquila,0) ManagesMaquila
from MixingStation.RequestMixingStation rms 
inner join (
	select d.id, d.RequestMixingStationId, p.PatientCode, SUM(p.Quantity) Quantity, string_agg(p.Id, ', ') StringIds
	from MixingStation.RequestMixingStationDetailPatients p
	inner join MixingStation.RequestMixingStationDetail d on d.Id = p.RequestMixingStationDetailId
	where p.Status = 1
	group by d.Id, d.RequestMixingStationId, p.PatientCode
) temp on temp.RequestMixingStationId = rms.Id
inner join MixingStation.RequestMixingStationDetail rd on rms.Id = rd.RequestMixingStationId
inner join MixingStation.UnitDoseType udt on udt.Id = rd.UnitDoseTypeId
left join MixingStation.ProductionLine pl on pl.Id = rd.ProductionLineId
left join MixingStation.ExternalCareCenter cce on cce.Code = rd.CareCenterCode and rd.Source in (1, 2)
Left join MixingStation.ContractExternalClients cec WITH(NOLOCK) ON cce.ContractExternalClientsId= cec.Id
left join .ADCENATEN cci on cci.CODCENATE = rd.CareCenterCode
left join MixingStation.Package p on p.Id = rd.PackageId
left join Inventory.ATC a on a.Id = rd.ATCId
left join MixingStation.PackagePersonalized pp on pp.Id = rd.PackagePersonalizedId
where rms.Status = 2 and rd.Source in (1, 2) and rd.SendTo = 1

union all

--Solicitudes que NO tengan pacientes
select 
CONCAT(rms.Id, '-', rms.Code, '-', 2,'-',rd.Id) Id,
rms.CMConfigurationId,
rd.ProductionLineId,
rms.Code RequestCode,
rd.Source RequestType,
rd.PackageId,
rd.PackagePersonalizedId,
case rd.Source 
	when 1 then 'Orden Médica'
	when 2 then 'Solicitud Externa Paciente'
	when 3 then 'Solicitud Externa Maquila'
	when 4 then 'Solicitud Inventario'
end RequestTypeName,
rms.RequestDate,
IIF(pp.Id is not null, 3, IIF(p.Id is not null, 1, 2)) ItemType, 
IIF(pp.Id is not null, 'Paquete Personalizado', IIF(p.Id is not null, 'Paquete', 'Medicamento')) ItemTypeName,
ISNULL(pp.Code + ' - ' + pp.Name, ISNULL(p.Code + ' - ' + p.Name, a.Code + ' - ' + a.Name)) ItemCodeName,
IIF(cce.Id is not null, cce.Code + ' - ' + cce.Description, rtrim(ltrim(cci.CODCENATE)) + ' - ' + rtrim(ltrim(cci.NOMCENATE))) CareCenterCodeName,
ISNULL(temp.Quantity, rd.Quantity) RequestQuantity,
temp.StringIds,
IIF(pl.Id is not null, pl.Code + ' - ' + pl.Name, 'Sin Asignar') ProductionLineCodeName,
rd.UnitDoseTypeId, udt.Code + ' - ' + udt.Description UnitDoseTypeCodeName,
rms.Id RequestMixingStationId, 
rd.Id RequestMixingStationDetailId,
cce.ContractExternalClientsId,
ISNULL(cec.ManagesMaquila,0) ManagesMaquila
from MixingStation.RequestMixingStation rms 
inner join (
select d.RequestMixingStationId, SUM(d.Quantity) Quantity, string_agg(d.Id, ', ') StringIds
	from MixingStation.RequestMixingStationDetail d 
	where d.Status = 1
	group by d.RequestMixingStationId
) temp on temp.RequestMixingStationId = rms.Id
inner join MixingStation.RequestMixingStationDetail rd on rms.Id = rd.RequestMixingStationId
inner join MixingStation.UnitDoseType udt on udt.Id = rd.UnitDoseTypeId
left join MixingStation.ProductionLine pl on pl.Id = rd.ProductionLineId
left join MixingStation.ExternalCareCenter cce on cce.Code = rd.CareCenterCode and rd.Source in (3,4)
Left join MixingStation.ContractExternalClients cec WITH(NOLOCK) ON cce.ContractExternalClientsId= cec.Id
left join .ADCENATEN cci on cci.CODCENATE = rd.CareCenterCode
left join MixingStation.Package p on p.Id = rd.PackageId
left join Inventory.ATC a on a.Id = rd.ATCId
left join MixingStation.PackagePersonalized pp on pp.Id = rd.PackagePersonalizedId
where rms.Status = 2 and rd.Source in (3,4) and rd.SendTo = 1