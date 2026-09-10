

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
		FROM MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK)
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
inner join MixingStation.CampaignDetail cd WITH(NOLOCK) on ISNULL(rd.CampaignDetailId,rdp.CampaignDetailId) =cd.Id
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
			From MixingStation.RequestPackageDetailStatus rpds with(nolock)
			Inner Join MixingStation.RequestMixingStationDetail rpd with(nolock) On rpd.id = rpds.RequestMixingStationDetailId 
			inner join  MixingStation.Readjustments r WITH(NOLOCK) on r.RequestPackageDetailStatusId = rpds.Id and r.IsReadjustment=1
			Group By  rpd.CampaignDetailId, r.IsReadjustment, r.RequestPackageDetailStatusId, rpds.RequestMixingStationDetailId
) as re on re.RequestMixingStationDetailId = rd.Id
where rdp.CampaignDetailId is not null or rd.CampaignDetailId is not null
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los ítems de solicitudes de mezcla (órdenes médicas, solicitudes externas de paciente, maquila e inventario) asociados a un lote de campaña de producción farmacéutica. Para cada ítem muestra el tipo de elemento solicitado (medicamento, paquete estándar o paquete personalizado), la cantidad solicitada, el centro de atención (sede hospitalaria o externa), la línea de producción asignada, el tipo de dosis unitaria y el estado de la prescripción en historia clínica. Integra datos de pacientes por cama asignada a cada detalle de solicitud, indica si el ítem tiene reajuste de producción y contabiliza las unidades en gestión activa, excluyendo registros cancelados. Se usa para alimentar el panel de trabajo de la estación de mezclas y el seguimiento del ciclo de vida de cada campaña de preparación magistral.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListCampaignDetailWithRequests';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListCampaignDetailWithRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida los detalles de solicitudes de preparación farmacéutica asociadas a campañas (lotes) de la estación de mezclas, integrando ítem, paciente, centro de atención, línea de producción, estado de campaña y reajustes.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCampaignDetailWithRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de solicitud (RequestMixingStationDetail) debe estar asociado a una RequestMixingStation y a un UnitDoseType existentes (INNER JOIN).; El detalle debe estar vinculado a una CampaignDetail existente, ya sea directamente (rd.CampaignDetailId) o vía la asignación de pacientes (rdp.CampaignDetailId).; Solo se consideran asignaciones de pacientes con CampaignDetailId no nulo y Status <> 3.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCampaignDetailWithRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles que tengan al menos una CampaignDetail asociada (directa o vía pacientes).; La cantidad solicitada (RequestQuantity) prioriza la suma agrupada por paciente (rdp.Quantity) y solo si es nula se usa rd.Quantity.; El CampaignDetailId expuesto prioriza rdp.CampaignDetailId si existe; si no, usa rd.CampaignDetailId.; ManageQuantity cuenta solo los RequestPackageDetailStatus cuyo Status NO esté en (5,6).; Las asignaciones de pacientes con Status=3 se excluyen de los agregados (cantidad y referencias HCPRESCRA).; El estado de prescripción (StatusHCPRESCRA) se obtiene del HCPRESCRA con MAX(ID) ligado vía HCFARMEPD donde SourceTable=''HCPRESCRA'' y EntityName=''HCFARMEPD''.; El ItemType jerarquiza: Paquete Personalizado > Paquete > Medicamento (ATC).; ExistenceQuantity y ProduceQuantity se exponen siempre como 0 (placeholders), y ControlNumber siempre como cadena vacía.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCampaignDetailWithRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas (farmacia); Solicitud de preparación; Campaña / lote de preparación; Orden médica; Solicitud externa de paciente; Maquila; Solicitud de inventario; Paquete personalizado / Paquete / Medicamento (ATC); Dosis unitaria; Centro de atención (interno y externo); Línea de producción; Tipo de etiqueta (Bolsa/Mediana/Jeringa); Cama del paciente; Reajuste de preparación; Prescripción (HCPRESCRA)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCampaignDetailWithRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListCampaignDetailWithRequests: Devuelve un renglón por detalle de solicitud cuando existe una campaña asociada (rdp.CampaignDetailId IS NOT NULL OR rd.CampaignDetailId IS NOT NULL).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCampaignDetailWithRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rd.Source = 1 / 2 / 3 / 4 → Asigna RequestTypeName: ''Orden Médica'', ''Solicitud Externa Paciente'', ''Solicitud Externa Maquila'' o ''Solicitud Inventario'' respectivamente.; si pp.Id IS NOT NULL → ItemType=3, ItemTypeName=''Paquete Personalizado'' y se toma pp.Id como ItemId. else Si p.Id IS NOT NULL → ItemType=1, ''Paquete''; en otro caso → ItemType=2, ''Medicamento'' (a.Id).; si udt.MSClass <> 2 → ItemCodeName concatena Code+Description del paquete personalizado/paquete/ATC. else Cuando MSClass = 2 se fuerza el nombre a partir del Package (p.Code + '' - '' + p.Name).; si cce.Id IS NOT NULL → CareCenterCodeName se forma con cce.Code+Description (centro de atención externo). else Se usa CODCENATE+NOMCENATE de ADCENATEN (centro interno).; si rd.Source IN (1,4) → El centro de atención se resuelve contra ADCENATEN (interno). else Si Source IN (2,3) se resuelve contra ExternalCareCenter.; si pl.Id IS NOT NULL → ProductionLineCodeName=pl.Code+pl.Name. else ''Sin Asignar''.; si ISNULL(rd.LabelType, p.LabelType) = 1/2/3 → LabelTypeName=''Bolsa''/''Mediana''/''Jeringa''. else NULL.; si rd.RequestPackageDetailStatusId IS NULL → HasReadjustment=0. else HasReadjustment=1.; si Existe Readjustments con IsReadjustment=1 ligado al RequestPackageDetailStatus del detalle → IsReadjustment=1; en otro caso 0 (ISNULL).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCampaignDetailWithRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStationDetail; MixingStation.RequestMixingStation; MixingStation.UnitDoseType; MixingStation.RequestMixingStationDetailPatients; MixingStation.CampaignDetail; MixingStation.Package; Inventory.ATC; MixingStation.PackagePersonalized; ADCENATEN; MixingStation.ExternalCareCenter; MixingStation.ProductionLine; HCFARMEPD; HCPRESCRA; MixingStation.RequestPackageDetailStatus; MixingStation.Readjustments', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCampaignDetailWithRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListCampaignDetailWithRequests';
GO
