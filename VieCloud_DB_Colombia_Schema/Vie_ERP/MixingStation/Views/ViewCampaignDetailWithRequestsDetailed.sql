
CREATE view [MixingStation].[ViewCampaignDetailWithRequestsDetailed]
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
		FROM MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK)
		where rpds.RequestMixingStationDetailId = rd.Id AND rpds.Status NOT IN (5,6)
	) As ManageQuantity
    , ISNULL(re.IsReadjustment,0) IsReadjustment
	,iif(rd.RequestPackageDetailStatusId is null,0,1) HasReadjustment
	, coalesce(pd.ATCId, pp.ATCId) as PackageATCId
from MixingStation.RequestMixingStationDetail rd
inner join MixingStation.RequestMixingStation r on r.Id = rd.RequestMixingStationId
inner join MixingStation.UnitDoseType udt on udt.Id = rd.UnitDoseTypeId
left join (
	select RequestMixingStationDetailId, SUM(Quantity) Quantity, CampaignDetailId, string_agg(Id, ', ') StringIds, Bed
	from MixingStation.RequestMixingStationDetailPatients
	where CampaignDetailId is not null and Status <> 3
	group by RequestMixingStationDetailId, CampaignDetailId, Bed
) rdp on rdp.RequestMixingStationDetailId = rd.Id
inner join MixingStation.CampaignDetail cd WITH(NOLOCK) on ISNULL(rd.CampaignDetailId,rdp.CampaignDetailId) =cd.Id
left join MixingStation.Package p on p.Id = rd.PackageId
left join Inventory.ATC a on a.Id = rd.ATCId
left join MixingStation.PackagePersonalized pp on pp.Id = rd.PackagePersonalizedId
left join MixingStation.PackageDetail pd on pd.PackageId = coalesce(p.Id, pp.Id)
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
			From MixingStation.RequestPackageDetailStatus rpds with(nolock)
			Inner Join MixingStation.RequestMixingStationDetail rpd with(nolock) On rpd.id = rpds.RequestMixingStationDetailId 
			inner join  MixingStation.Readjustments r WITH(NOLOCK) on r.RequestPackageDetailStatusId = rpds.Id and r.IsReadjustment=1
			Group By  rpd.CampaignDetailId, r.IsReadjustment, r.RequestPackageDetailStatusId, rpds.RequestMixingStationDetailId
) as re on re.RequestMixingStationDetailId = rd.Id
where rdp.CampaignDetailId is not null or rd.CampaignDetailId is not null
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle completo de las solicitudes de mezcla (órdenes médicas, solicitudes externas de paciente, maquila e inventario) asociadas a cada lote de campaña farmacéutica en la estación de mezclas. Integra el ítem solicitado (medicamento ATC, paquete estándar o paquete personalizado), la cantidad pedida por paciente o por solicitud, la cama y unidad asignada, el centro de atención (interno o externo), la línea de producción, el tipo de dosis unitaria y el estado de la prescripción en historia clínica (HCPRESCRA). También expone el tipo de etiqueta de preparación (bolsa, mediana o jeringa), si el ítem tiene reajuste de producción, la cantidad en gestión activa (excluyendo estados cancelados o finalizados), y el identificador de campaña que agrupa cada lote, sirviendo como fuente principal para los módulos de seguimiento de producción, dispensación y trazabilidad de preparados magistrales en farmacia.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignDetailWithRequestsDetailed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignDetailWithRequestsDetailed';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida el detalle de campañas de la estación de mezclas con sus solicitudes asociadas, enriqueciendo cada ítem con tipo (paquete, paquete personalizado o medicamento), centro de atención, línea de producción, estado clínico y reajustes.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsDetailed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de la solicitud (RequestMixingStationDetail) debe estar asociado a una RequestMixingStation, a un UnitDoseType y a un CampaignDetail (vía rd.CampaignDetailId o vía pacientes en RequestMixingStationDetailPatients).; Los pacientes considerados en la agrupación deben tener Status <> 3 y CampaignDetailId no nulo.; Para resolver el centro de atención interno (ADCENATEN) la solicitud debe ser Source 1 o 4; para externo (ExternalCareCenter) debe ser Source 2 o 3.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsDetailed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ManageQuantity cuenta solo registros de RequestPackageDetailStatus cuyo Status NO esté en (5,6) para el detalle de solicitud.; La cantidad solicitada (RequestQuantity) prioriza la suma agrupada por paciente/campaña (rdp.Quantity) sobre rd.Quantity.; El CampaignDetailId resultante prioriza rdp.CampaignDetailId (de pacientes) sobre rd.CampaignDetailId.; La asociación a HCPRESCRA solo considera registros de pacientes con EntityName=''HCFARMEPD'', SourceTable=''HCPRESCRA'' y Status <> 3, tomando el MAX(HCPRESCRA.ID) por detalle/campaña.; IsReadjustment proviene exclusivamente de Readjustments con IsReadjustment=1; cuando no existe, se devuelve 0.; Los pacientes con Status=3 quedan excluidos tanto del cálculo de cantidad/camas como del enlace con prescripciones (HCPRESCRA).; ExistenceQuantity, ProduceQuantity y ControlNumber siempre se devuelven como literales fijos (0, 0 y '''').', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsDetailed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas; Campaña de preparación; Solicitud de preparación farmacéutica; Orden médica; Solicitud externa paciente; Solicitud externa maquila; Solicitud de inventario; Paquete personalizado (mezcla magistral); Medicamento ATC; Centro de atención (interno/externo); Línea de producción; Dosis unitaria; Tipo de etiqueta (Bolsa/Mediana/Jeringa); Cama del paciente; Prescripción (HCPRESCRA); Reajuste de preparación; Clasificación ATC del paquete', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsDetailed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewCampaignDetailWithRequestsDetailed: Devuelve filas solo cuando rdp.CampaignDetailId IS NOT NULL OR rd.CampaignDetailId IS NOT NULL (filtra ítems sin asociación a campaña).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsDetailed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rd.Source IN (1,2,3,4) → Asigna RequestTypeName: 1=''Orden Médica'', 2=''Solicitud Externa Paciente'', 3=''Solicitud Externa Maquila'', 4=''Solicitud Inventario''.; si pp.Id IS NOT NULL → ItemType=3 e ItemTypeName=''Paquete Personalizado''. else Si p.Id IS NOT NULL → ItemType=1 (''Paquete''); en otro caso ItemType=2 (''Medicamento'').; si udt.MSClass <> 2 → ItemCodeName se arma con Code+Description del paquete personalizado, paquete o ATC. else Cuando MSClass = 2, ItemCodeName se construye como p.Code + '' - '' + p.Name (forzando uso del paquete).; si IsNull(rd.LabelType, p.LabelType) IN (1,2,3) → LabelTypeName = ''Bolsa'' (1), ''Mediana'' (2) o ''Jeringa'' (3). else NULL para cualquier otro valor.; si cce.Id IS NOT NULL → CareCenterCodeName se toma de ExternalCareCenter (Code+Description). else Se toma de ADCENATEN (CODCENATE+NOMCENATE) con trims.; si pl.Id IS NOT NULL → ProductionLineCodeName = pl.Code+'' - ''+pl.Name. else ''Sin Asignar''.; si rd.RequestPackageDetailStatusId IS NULL → HasReadjustment = 0. else HasReadjustment = 1.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsDetailed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStationDetail; MixingStation.RequestMixingStation; MixingStation.UnitDoseType; MixingStation.RequestMixingStationDetailPatients; MixingStation.CampaignDetail; MixingStation.Package; Inventory.ATC; MixingStation.PackagePersonalized; MixingStation.PackageDetail; ADCENATEN; MixingStation.ExternalCareCenter; MixingStation.ProductionLine; HCFARMEPD; HCPRESCRA; MixingStation.RequestPackageDetailStatus; MixingStation.Readjustments', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsDetailed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailWithRequestsDetailed';
GO
