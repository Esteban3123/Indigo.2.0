

CREATE view [MixingStation].[ViewListDashboardRequestMixingStationDetail] 
as 

--Solicitudes que vienen de confirmación dosis unitarias
	select CONCAT(rdp.EntityName, '-', rdp.Id) Id,
	r.Id RequestMixingStationId,
	rdp.Id RequestMixingStationDetailPatientsId,
	rd.Id RequestMixingStationDetailId,
	1 ItemType,
	'Paquete' ItemTypeName,
	p.Id ItemId,
	p.Code + ' - ' + p.Name ItemCodeName,
	udt.Id UnitDoseTypeId,
	udt.Code + ' - ' + udt.Description UnitDoseTypeCodeName,
	rdp.Bed,
	rdp.Quantity,
	rdp.PatientCode, 
	rdp.PatientCode + ' - ' + ISNULL(pecc.Name, RTRIM(ltrim(pat.IPNOMCOMP))) PatientCodeName,
	rdp.FunctionalUnitCode,
	IIF(fu.UFUCODIGO is not null, rtrim(ltrim(fu.UFUCODIGO)) + ' - ' + rtrim(ltrim(fu.UFUDESCRI)), rdp.FunctionalUnitCode + ' Externa') FunctionalUnitCodeName,
	rdp.AdministrationRouteId AdministrationRouteId,
		CASE WHEN udt.MSClass = 2 AND hnpt.VIADMIN is not null THEN -- se evalua si es de tipo NPT obtiene la via de administracion de la tabla del EHR
		CASE WHEN hnpt.VIADMIN = 1 THEN 'Línea central'
			 ELSE
			  CASE WHEN  hnpt.VIADMIN = 2 THEN 'Línea periférica'
			  ELSE 'No encontrado'
			  END
		END
	  ELSE ar.[Name]
	  END AS AdministrationRouteCodeName,
	--ar.[Name] as AdministrationRouteCodeName, -- Actualizar los datos correctamente
	CAST((	SELECT TOP 1 hcd.CodeSusceptibleMixingStation
		FROM  HCFARMEPD hcd
		where rd.Source=1 and rdp.EntityName= 'HCFARMEPD' and hcd.ID= rdp.EntityId) as VARCHAR(36)) CodeSusceptibleMixingStation
		, rd.SendTo
	from MixingStation.RequestMixingStationDetailPatients rdp
	inner join MixingStation.RequestMixingStationDetail rd on rd.Id = rdp.RequestMixingStationDetailId
	inner join MixingStation.RequestMixingStation r on r.Id = rd.RequestMixingStationId
	inner join MixingStation.UnitDoseType udt on udt.Id = rd.UnitDoseTypeId
	inner join MixingStation.Package p on p.Id = rd.PackageId
	left join Inventory.AdministrationRoute ar on ar.Id = rdp.AdministrationRouteId
	left join dbo.INPACIENT pat on pat.IPCODPACI = rdp.PatientCode and rdp.EntityName = 'ProductSusceptibleMixingStation'
	left join MixingStation.PatientExternalCareCenter pecc on pecc.IdentificationNumber = rdp.PatientCode and rdp.EntityName = 'RequestUnitDoseExternalCareCenterPatientDetails'
	left join dbo.INUNIFUNC fu on fu.UFUCODIGO = rdp.FunctionalUnitCode and rdp.EntityName = 'ProductSusceptibleMixingStation'

	left join MedicalHistory.ProductSusceptibleMixingStation psms (nolock) on rdp.EntityId = psms.Id and rdp.EntityName = 'ProductSusceptibleMixingStation'
	left join HCNUTPAREC hnpt(nolock) on psms.IdOrigin = hnpt.ID and psms.Origin = 'HCNUTPAREC'
	where rdp.Status = 1 And rd.SendTo = 0

union all

	--Solicitudes que vienen de solicitudes inventario o solicitudes centros de atención externo tipo maquila
	select CONCAT(rd.EntityName, '-', rd.Id) Id,
	r.Id RequestMixingStationId,
	0 RequestMixingStationDetailPatientsId,
	rd.Id RequestMixingStationDetailId,
	IIF(p.Id is not null, 1, 2) ItemType,
	IIF(p.Id is not null, 'Paquete', 'Medicamento') ItemTypeName,
	ISNULL(p.Id, a.Id) ItemId, 
	IIF(p.Id is not null, p.Code + ' - ' + p.Name, a.Code + ' - ' + a.Name) ItemCodeName,
	udt.Id UnitDoseTypeId,
	udt.Code + ' - ' + udt.Description UnitDoseTypeCodeName,
	'' Bed, rd.Quantity,
	'' PatientCode,
	'' PatientCodeName,
	'' FunctionalUnitCode,
	'Interna' FunctionalUnitCodeName,
	null AdministrationRouteId,
	'' AdministrationRouteCodeName,
	null CodeSusceptibleMixingStation
	, rd.SendTo
	from MixingStation.RequestMixingStationDetail rd 
	inner join MixingStation.RequestMixingStation r on r.Id = rd.RequestMixingStationId
	inner join MixingStation.UnitDoseType udt on udt.Id = rd.UnitDoseTypeId
	left join MixingStation.Package p on p.Id = rd.PackageId
	left join Inventory.ATC a on a.Id = rd.ATCId
	where rd.Status = 1 And rd.EntityName <> 'ConfirmationUnitDose' And rd.SendTo = 0 And rd.EntityName <> 'RequestPackageDetailStatus'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del dashboard de la estación de mezclas (farmacia) que consolida en una sola consulta todos los ítems pendientes de preparación, sin importar su origen. Combina dos fuentes: (1) solicitudes que provienen de confirmación de dosis unitarias, mostrando el paciente (con su código/cédula, nombre completo y cama), la unidad funcional donde está internado, la vía de administración, el paquete asignado y el tipo de dosis unitaria; y (2) solicitudes que provienen de inventario interno o de centros de atención externos tipo maquila, donde no hay paciente asociado sino una cantidad global de medicamento o paquete a preparar. Permite al operador de la estación de mezclas visualizar en tiempo real qué preparados magistrales o mezclas están pendientes de despacho (SendTo = 0), identificando si el ítem corresponde a un paquete o a un medicamento suelto, y resolviendo la vía de administración según el tipo de dosis (incluyendo el caso especial de Nutrición Parenteral Total donde la vía se obtiene del historial clínico).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListDashboardRequestMixingStationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListDashboardRequestMixingStationDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista las solicitudes pendientes de despacho en la estación de mezclas, unificando los pedidos con paciente (dosis unitarias) y los pedidos sin paciente (inventario interno o maquila externa) para el tablero de control.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las solicitudes deben tener detalle activo (rd.Status = 1 en el segundo bloque; rdp.Status = 1 en el primero); Solo se incluyen detalles aún no despachados (SendTo = 0); Para resolver vía de administración de NPT, debe existir el producto en MedicalHistory.ProductSusceptibleMixingStation con Origin = ''HCNUTPAREC'' y registro en HCNUTPAREC', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan ítems pendientes de envío (SendTo = 0); Los ítems del segundo bloque siempre se reportan como unidad funcional ''Interna'' y sin paciente, cama ni vía de administración; El identificador compuesto Id concatena EntityName y el Id del registro origen para garantizar unicidad entre los dos bloques; Las solicitudes con EntityName ''ConfirmationUnitDose'' o ''RequestPackageDetailStatus'' nunca aparecen en el bloque sin paciente (se enrutan por el bloque de pacientes o se excluyen); Para NPT (MSClass=2) la vía de administración nunca se toma del catálogo estándar si existe valor en HCNUTPAREC.VIADMIN', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas; Preparado magistral; Dosis unitaria; Nutrición Parenteral Total (NPT); Vía de administración (línea central / línea periférica); Paquete farmacéutico; Medicamento (ATC); Paciente externo (centro de atención externo / maquila); Unidad funcional; Cama; Despacho/envío de solicitud', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListDashboardRequestMixingStationDetail: Devuelve filas tipo ''Paquete'' (ItemType=1) cuando provienen de RequestMixingStationDetailPatients con rdp.Status=1 y rd.SendTo=0; [RETURN_RESULT] MixingStation.ViewListDashboardRequestMixingStationDetail: Devuelve filas tipo ''Paquete'' (ItemType=1) si rd.PackageId no es nulo, o ''Medicamento'' (ItemType=2) si solo hay ATCId, para solicitudes sin paciente con rd.Status=1, rd.SendTo=0 y EntityName distinto de ''ConfirmationUnitDose'' y ''RequestPackageDetailStatus''', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si udt.MSClass = 2 AND hnpt.VIADMIN IS NOT NULL (preparación tipo NPT con vía registrada en historia clínica) → La vía de administración se obtiene de HCNUTPAREC: 1=''Línea central'', 2=''Línea periférica'', otros=''No encontrado'' else Se usa el nombre de la vía de Inventory.AdministrationRoute (ar.Name); si rdp.EntityName = ''ProductSusceptibleMixingStation'' (paciente interno) → El nombre del paciente se obtiene de dbo.INPACIENT (IPNOMCOMP) y la unidad funcional de dbo.INUNIFUNC else Si rdp.EntityName = ''RequestUnitDoseExternalCareCenterPatientDetails'', el nombre se toma de MixingStation.PatientExternalCareCenter; si fu.UFUCODIGO IS NULL (unidad funcional no encontrada en el maestro interno) → Se etiqueta como ''<código> Externa'' else Se muestra ''UFUCODIGO - UFUDESCRI''; si rd.Source = 1 AND rdp.EntityName = ''HCFARMEPD'' → Se obtiene CodeSusceptibleMixingStation desde HCFARMEPD por rdp.EntityId else CodeSusceptibleMixingStation queda nulo; si p.Id IS NOT NULL en bloque sin paciente → ItemType=1 ''Paquete'' con datos del catálogo Package else ItemType=2 ''Medicamento'' con datos del catálogo ATC', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStationDetailPatients; MixingStation.RequestMixingStationDetail; MixingStation.RequestMixingStation; MixingStation.UnitDoseType; MixingStation.Package; Inventory.AdministrationRoute; dbo.INPACIENT; MixingStation.PatientExternalCareCenter; dbo.INUNIFUNC; MedicalHistory.ProductSusceptibleMixingStation; dbo.HCNUTPAREC; dbo.HCFARMEPD; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardRequestMixingStationDetail';
GO
