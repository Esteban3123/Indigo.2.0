

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de solicitudes de mezclas y preparados magistrales en estado aprobado (listo para campaña de producción) que deben ser procesadas por la estación de mezclas de farmacia. Integra dos grupos de solicitudes: las que tienen pacientes específicamente asignados (órdenes médicas y solicitudes externas por paciente) y las que no tienen paciente individual sino una cantidad global (solicitudes de maquila e inventario). Para cada línea expone el código y fecha de la solicitud, el tipo de solicitud (orden médica, solicitud externa paciente, maquila o inventario), el ítem solicitado (medicamento, paquete o paquete personalizado), el centro de atención con su nombre, la cantidad total requerida, la línea de producción asignada, el tipo de unidad de dosis, y si el contrato del cliente externo gestiona maquila. Sirve como fuente principal para la pantalla de agrupación y lanzamiento de campañas de producción en la estación de mezclas, permitiendo al farmacéutico ver qué mezclas están pendientes de fabricar, para qué paciente o cliente, y en qué línea de producción deben elaborarse.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListRequestsProcessToCampaign';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListRequestsProcessToCampaign';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de la estación de mezclas en estado "enviado" listas para ser procesadas en una campaña, unificando las que tienen pacientes asignados y las que no, con su tipo de ítem (medicamento, paquete o paquete personalizado) y centro de atención.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsProcessToCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La solicitud (RequestMixingStation) debe estar en Status = 2 (enviada/lista para procesar).; El detalle (RequestMixingStationDetail) debe tener SendTo = 1 (dirigido a la estación de mezclas).; Para la rama con pacientes: Source ∈ (1,2) — Orden Médica o Solicitud Externa Paciente — y existir RequestMixingStationDetailPatients con Status = 1.; Para la rama sin pacientes: Source ∈ (3,4) — Solicitud Externa Maquila o Solicitud Inventario — y detalle con Status = 1.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsProcessToCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se exponen solicitudes con RequestMixingStation.Status = 2 y detalle con SendTo = 1.; El origen Source determina exclusivamente la rama: 1-2 generan filas por paciente, 3-4 generan filas agregadas por solicitud; no hay solapamiento entre ramas.; El tipo de ítem es mutuamente excluyente y se decide en orden: PackagePersonalized > Package > ATC (medicamento).; RequestQuantity prioriza la cantidad agregada del subquery; si es nula, cae a rd.Quantity.; ManagesMaquila se asume 0 cuando no existe contrato externo asociado (ISNULL).; Sólo se consideran pacientes activos del detalle (Status = 1) en la rama con pacientes.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsProcessToCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas (farmacia); Solicitud de preparación; Orden Médica; Solicitud Externa Paciente; Solicitud Externa Maquila; Solicitud Inventario; Paquete personalizado; Paquete; Medicamento (ATC); Dosis unitaria; Línea de producción; Centro de atención externo; Contrato con cliente externo; Maquila; Campaña de producción', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsProcessToCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve una fila por combinación solicitud-detalle-paciente (rama 1) o por solicitud-detalle (rama 2), con Id compuesto CONCAT(rms.Id,''-'',rms.Code,''-'',1|2,''-'',PatientCode|rd.Id).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsProcessToCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rd.Source IN (1,2) y existen pacientes en RequestMixingStationDetailPatients con Status=1 → Agrupa cantidades por paciente (SUM Quantity, string_agg de Ids) y emite filas con discriminador 1 en el Id; cruza ExternalCareCenter sólo cuando Source ∈ (1,2).; si rd.Source IN (3,4) (Maquila/Inventario) → Agrupa por solicitud sumando cantidades del detalle con Status=1, emite filas con discriminador 2 en el Id y cruza ExternalCareCenter sólo cuando Source ∈ (3,4).; si rd.PackagePersonalizedId no es nulo (pp.Id no nulo) → ItemType = 3 (Paquete Personalizado). else Si PackageId no nulo → ItemType = 1 (Paquete); en caso contrario ItemType = 2 (Medicamento, vía Inventory.ATC).; si Existe coincidencia en MixingStation.ExternalCareCenter (cce.Id no nulo) → CareCenterCodeName se arma con cce.Code + '' - '' + cce.Description. else Se usa el centro asistencial interno ADCENATEN (CODCENATE + NOMCENATE).; si rd.ProductionLineId tiene línea asociada (pl.Id no nulo) → Muestra Code + '' - '' + Name de la línea. else Muestra ''Sin Asignar''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsProcessToCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStation; MixingStation.RequestMixingStationDetail; MixingStation.RequestMixingStationDetailPatients; MixingStation.UnitDoseType; MixingStation.ProductionLine; MixingStation.ExternalCareCenter; MixingStation.ContractExternalClients; MixingStation.Package; MixingStation.PackagePersonalized; Inventory.ATC; ADCENATEN', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsProcessToCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsProcessToCampaign';
GO
