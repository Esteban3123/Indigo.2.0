
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del dashboard de programación de producción de la estación de mezclas (farmacia). Consolida las solicitudes de mezcla aprobadas (estado 2) junto con sus detalles e ítems por paciente, agrupando cantidades y generando identificadores únicos por combinación de paciente, solicitud, campaña y tipo de ítem (paquete personalizado, paquete estándar o medicamento ATC). Integra información del paciente (cédula y nombre completo), centro de atención (hospitalario o externo de maquila), tipo de solicitud (orden médica, solicitud externa paciente, maquila o inventario) y campaña de producción, para alimentar el tablero de control donde el personal de farmacia planifica y ejecuta la producción de preparados magistrales y mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListDashboardProductionSchedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListDashboardProductionSchedule';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en el dashboard de programación de producción los ítems pendientes de la estación de mezclas, agrupando solicitudes aprobadas con su tipo (medicamento, paquete o paquete personalizado), centro de atención, paciente y campaña.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardProductionSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las solicitudes deben estar en estado 2 (r.Status = 2) para considerarse en el dashboard.; La cantidad agregada por ítem (Quantity) debe ser mayor que 0.; Los códigos de centro de atención deben existir en ADCENATEN para Source 1/4 (origen interno) o en ExternalCareCenter para Source 2/3 (origen externo) para mostrar nombre completo.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardProductionSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen solicitudes con Status = 2.; Se excluyen filas con cantidad agregada <= 0 (where data.Quantity > 0).; La cantidad efectiva por ítem-paciente prioriza la cantidad por paciente (rdp.Quantity) sobre la cantidad del detalle (rd.Quantity) mediante ISNULL.; El CampaignDetailId efectivo proviene del paciente si existe, si no del detalle (ISNULL(rdp.CampaignDetailId, rd.CampaignDetailId)).; ExistenceQuantity, ProduceQuantity y ControlNumber se devuelven siempre como 0/'''' (no se calculan en la vista).; La jerarquía de identificación del ítem es: PackagePersonalized > Package > ATC.; Los IDs originales de los detalles agrupados se conservan concatenados en StringIds vía STRING_AGG.; Un centro de atención interno (Source 1/4) y uno externo (Source 2/3) son mutuamente excluyentes por construcción del JOIN.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardProductionSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas; Solicitud de preparación; Paquete farmacéutico; Paquete personalizado; Medicamento (ATC); Paciente; Centro de atención (interno/externo); Campaña de producción; Línea de producción; Orden médica; Solicitud externa (paciente/maquila); Solicitud de inventario', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardProductionSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListDashboardProductionSchedule: Devuelve un Id sintético construido como CONCAT(PatientCode,''-'',Code,''-'',CampaignDetailId, sufijo de tipo (''PP''|''P''|''M''), Id de paquete/ATC e Id del ítem) para identificar de forma única cada fila del dashboard.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardProductionSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si data.Source = 1 → RequestTypeName = ''Orden Médica''; si data.Source = 2 → RequestTypeName = ''Solicitud Externa Paciente''; si data.Source = 3 → RequestTypeName = ''Solicitud Externa Maquila''; si data.Source = 4 → RequestTypeName = ''Solicitud Inventario''; si PackagePersonalized.Id no es nulo → ItemType=3, ItemTypeName=''Paquete Personalizado'' y se usa pp como ítem else Si Package.Id no es nulo → ItemType=1 ''Paquete''; en caso contrario ItemType=2 ''Medicamento'' usando ATC; si data.Source IN (1,4) → Se resuelve el centro de atención contra ADCENATEN (centros internos) else Si Source IN (2,3) se resuelve contra MixingStation.ExternalCareCenter (centros externos); si ExternalCareCenter.Id no es nulo → CareCenterCodeName = cce.Code + '' - '' + cce.Description else Se usa rtrim/ltrim(cci.CODCENATE) + '' - '' + rtrim/ltrim(cci.NOMCENATE) desde ADCENATEN', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardProductionSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStation; MixingStation.RequestMixingStationDetail; MixingStation.RequestMixingStationDetailPatients; MixingStation.Package; Inventory.ATC; MixingStation.PackagePersonalized; MixingStation.ExternalCareCenter; MixingStation.CampaignDetail; INPACIENT; ADCENATEN', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardProductionSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardProductionSchedule';
GO
