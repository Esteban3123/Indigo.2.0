

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
		udt.MSClass as UnitDoseTypeMSClass,
		udt.Code + ' - ' + udt.[Description] as UnitDoseTypeCodeName,
		rd.RequestMixingStationId, 
		rd.Id as RequestMixingStationDetailId,
		cce.ContractExternalClientsId,
		ISNULL(cec.ManagesMaquila,0) as ManagesMaquila, 
		rdp.PatientCode,
		concat(ltrim(rtrim(pac.IPCODPACI)), ' - ', ltrim(rtrim(pac.IPNOMCOMP))) as PatientFullName
	from MixingStation.RequestMixingStationDetail rd with(nolock)
	inner join MixingStation.RequestMixingStation r with(nolock) on r.Id = rd.RequestMixingStationId
	left join (
		select RequestMixingStationDetailId, SUM(Quantity) Quantity, CampaignDetailId, string_agg(Id, ', ') StringIds, PatientCode
		from MixingStation.RequestMixingStationDetailPatients with(nolock)
		where CampaignDetailId is null and [Status] <> 3
		group by RequestMixingStationDetailId, CampaignDetailId, PatientCode
	) rdp on rdp.RequestMixingStationDetailId = rd.Id
	inner join MixingStation.UnitDoseType udt with(nolock) on udt.Id = rd.UnitDoseTypeId
	left join MixingStation.Package p with(nolock) on p.Id = rd.PackageId
	left join Inventory.ATC a with(nolock) on a.Id = rd.ATCId
	left join MixingStation.PackagePersonalized pp with(nolock) on pp.Id = rd.PackagePersonalizedId
	left join .ADCENATEN cci with(nolock) on cci.CODCENATE = rd.CareCenterCode and rd.Source in (1, 4)
	left join MixingStation.ExternalCareCenter cce with(nolock) on cce.Code = rd.CareCenterCode and rd.Source in (2, 3)
	left join MixingStation.ContractExternalClients cec with(nolock) ON cce.ContractExternalClientsId= cec.Id
	left join MixingStation.ProductionLine pl with(nolock) on pl.Id = rd.ProductionLineId
	left join ..INPACIENT pac with(nolock) on rdp.PatientCode = pac.IPCODPACI
	where rdp.CampaignDetailId is null and rd.CampaignDetailId is null  and rd.SendTo = 1 and rd.[Status] <> 3 --No mostrar anulados
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las solicitudes de preparación de mezclas (magistrales, paquetes y medicamentos) pendientes de campaña en la estación de mezclas de farmacia. Consolida datos del encabezado de la solicitud, el detalle del ítem solicitado (ya sea un medicamento ATC, un paquete estándar o un paquete personalizado), el tipo de dosis, la línea de producción asignada y el centro de atención (tanto interno como externo/maquila). Incluye la cantidad solicitada por paciente agrupando los registros de pacientes asignados al detalle que aún no pertenecen a ninguna campaña y no están anulados, mostrando además el nombre completo del paciente a partir de su código o cédula. Sirve como fuente de datos para el módulo de programación y producción de mezclas, permitiendo visualizar qué preparados deben fabricarse, para qué sede o cliente externo y para qué paciente, filtrando únicamente los ítems activos y enviados a producción que todavía no han sido agrupados en una campaña de fabricación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListRequestsByCampaign';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListRequestsByCampaign';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes pendientes de la estación de mezclas que aún no han sido asignadas a una campaña, consolidando información de ítems (paquete, paquete personalizado o medicamento), pacientes, centro de atención y línea de producción.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las solicitudes deben tener SendTo = 1 (dirigidas a la estación de mezclas); Los detalles no deben estar anulados (Status <> 3); Ni el detalle (rd.CampaignDetailId) ni la asignación de paciente (rdp.CampaignDetailId) deben estar ya vinculados a una campaña (ambos IS NULL); Los pacientes considerados en el detalle no deben estar en estado 3 (anulados)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se muestran solicitudes anuladas (Status = 3) ni pacientes anulados; Solo expone solicitudes aún no vinculadas a una campaña (CampaignDetailId NULL en detalle y en pacientes); Solo expone solicitudes con destino estación de mezclas (SendTo = 1); RequestQuantity prioriza la cantidad agregada por pacientes (rdp.Quantity); si no hay pacientes asignados, usa rd.Quantity; ExistenceQuantity y ProduceQuantity siempre se devuelven en 0 (campos placeholder no calculados aquí); ControlNumber siempre se devuelve vacío; El centro de atención se resuelve por una sola fuente según el origen de la solicitud (interno vs externo); ManagesMaquila por defecto es 0 cuando no hay contrato externo asociado', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de estación de mezclas; Orden Médica; Solicitud Externa Paciente; Solicitud Externa Maquila; Solicitud Inventario; Paquete; Paquete Personalizado; Medicamento (ATC); Dosis unitaria; Centro de atención (interno/externo); Contrato con cliente externo; Maquila; Línea de producción; Paciente; Campaña (CampaignDetail)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas representando ítems de solicitudes de mezcla no asociadas a campañas; el Id se compone como Code-(P|M)-(PackageId|ATCId)-StringIds para identificar de forma única la combinación detalle+pacientes.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rd.Source IN (1,4) (Orden Médica o Solicitud Inventario) → El centro de atención se busca en la tabla interna ADCENATEN (cci) else Si rd.Source IN (2,3) (Solicitud Externa Paciente o Maquila), se busca en MixingStation.ExternalCareCenter (cce); si pp.Id IS NOT NULL → ItemType=3 ''Paquete Personalizado'' else Si p.Id IS NOT NULL → ItemType=1 ''Paquete''; en caso contrario ItemType=2 ''Medicamento'' (a.Id de ATC); si udt.MSClass = 2 → ItemCodeName se arma con p.Code + '' - '' + p.Name (nombre del paquete) else Se usa Description del paquete personalizado, paquete o ATC según disponibilidad; si rd.Source → Traduce a nombre legible: 1=''Orden Médica'', 2=''Solicitud Externa Paciente'', 3=''Solicitud Externa Maquila'', 4=''Solicitud Inventario''; si pl.Id IS NULL → ProductionLineCodeName = ''Sin Asignar'' else Se concatena pl.Code + '' - '' + pl.Name', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStationDetail; MixingStation.RequestMixingStation; MixingStation.RequestMixingStationDetailPatients; MixingStation.UnitDoseType; MixingStation.Package; Inventory.ATC; MixingStation.PackagePersonalized; ADCENATEN; MixingStation.ExternalCareCenter; MixingStation.ContractExternalClients; MixingStation.ProductionLine; INPACIENT', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListRequestsByCampaign';
GO
