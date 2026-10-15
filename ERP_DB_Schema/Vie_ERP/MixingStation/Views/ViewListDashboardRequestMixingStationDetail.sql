


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