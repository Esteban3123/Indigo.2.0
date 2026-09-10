

CREATE View [MixingStation].[ViewListDashboardConfirmationUnitDose_Refactor]
AS
WITH Cte_ExternalPreparationDetail AS(
	SELECT 	 
			CONCAT(ep.RequestUnitDoseExternalCareCenterPatientId, '-', ep.Id) Id, 
			ep.Id ExternalPatientPreparationId,
			CONCAT('ExternalPreparation - ', ep.Id) ComponentBundler,
			ep.RequestUnitDoseExternalCareCenterPatientId,
			ep.PreparationsRequested TotalQuantity,
			ep.Description ServiceDescription,
			ep.PreparationTypeId,
			a.Id AtcId,
			a.Code AtcCode,
			a.Name AtcName,
			a.ProductNPT,
			ISNULL(epd.Quantity, epd.Volume) Dosage,
			epd.ComponentType,
			ar.Name AdministrationRouteName,
			imu.Id MeasurementUnitId,
			imu.Code MeasurementUnitCode,
			imu.Name MeasurementUnitName,
			CONCAT(imu.Code, ' - ', imu.Name) MeasurementUnitDescription
	FROM MixingStation.ExternalPatientPreparation ep WITH(NOLOCK)
	JOIN MixingStation.ExternalPatientPreparationDetail epd WITH(NOLOCK) ON ep.Id = epd.ExternalPatientPreparationId
	JOIN Inventory.ATC a WITH(NOLOCK) ON epd.AtcId = a.Id
	JOIN Inventory.AdministrationRoute ar WITH(NOLOCK) ON ep.AdministrationRouteId = ar.Id
	JOIN Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) ON ISNULL(epd.MeasurementUnitId, epd.VolumeMeasureUnitId) = imu.Id 
)

---------------------------------------------------------------------------------------------------

	SELECT  DISTINCT
		CONCAT(phd.Id, '-',psmsId,'-' ,Cast(phd.GroupingCodeDose as Varchar(50)), pp.Id) Id
		, RTRIM(LTRIM(adc.CODCENATE)) CareCenterCode
		, CONCAT(RTRIM(LTRIM(adc.CODCENATE)), ' - ', RTRIM(LTRIM(adc.NOMCENATE))) CareCenterDescription
		, atc.Id ServiceId
		, atc.Code ServiceCode
		, atc.Name ServiceName
		, CASE 
			WHEN phd.Origin IN ('HCPRESCRA', 'HCORDMEDICAM') THEN CONCAT(atc.code, ' - ', atc.AbbreviationName)
			WHEN phd.Origin IN ('HCINFLIQA', 'HCNUTPAREC') THEN phd.FullProductName
			ELSE ''
		END AS ServiceDescription
		, 1 TotalQuantity
		, mp.CMConfigurationId
		, IIF(phd.Origin = 'HCNUTPAREC', HTC.VOLUTOTAL, phd.Dose) As Dosage
		, p.Id PackageId
		, p.Code PackageCode
		, p.Name PackageName
		, CONCAT(p.Code, ' - ', p.Description) PackageDescription
		, udt.Id UnitDoseTypeId
		, udt.Code UnitDoseTypeCode
		, udt.Description UnitDoseTypeName
		, CONCAT(udt.Code, ' - ', udt.Description) UnitDoseTypeDescription
		, mu.Id MeasurementUnitId
		, mu.Code MeasurementUnitCode
		, mu.Name MeasurementUnitName
		, CONCAT(mu.Code, ' - ', mu.Name) MeasurementUnitDescription
		, cud.Id ConfirmationUnitDoseId
		, pl.Id ProductionLineId
		, pl.Code ProductionLineCode
		, pl.Name ProductionLineName
		, IIF(pl.Id IS NULL, 'Sin asignar', CONCAT(pl.Code, ' - ', pl.Name)) ProductionLineCodeName
		, 1 SourceType
		, 'Orden médica' SourceName
		, pp.Id PackagePersonalizedId
		, pp.Description PackagePersonalizedCodeName
		, CAST(phd.GroupingCodeDose As Varchar(36)) AGRUPAQUETE
		, pac.IPCODPACI as PatientCode	
		, CONCAT(LTRIM(RTRIM(pac.IPCODPACI)), ' - ', LTRIM(RTRIM(pac.IPNOMCOMP))) As PatientCodeName
		, ISNULL(cma.NUMCAMHOS, '') As Bed
		,CASE phd.Origin
			WHEN 'HCNUTPAREC' THEN IIF(HTC.VIADMIN = '2' AND atc.ProductNPT = 1 AND phd.Origin = 'HCNUTPAREC','LÍNEA PERIFÉRICA' ,'INTRAVENOSA')
			ELSE hcv.DESVIAADM
		END AS AdministrationRoute
		, atc.ProductNPT
		, fu.UFUCODIGO FunctionalUnitCode	
		, CONCAT(fu.UFUCODIGO, ' - ', fu.UFUDESCRI) FuncionalUnitCodeName
		, ISNULL(hcp.PREESTADO, 0) StatusHCPRESCRA
		, CASE ISNULL(CASE phd.Origin WHEN 'HCPRESCRA' THEN hcp.PREESTADO WHEN 'HCINFLIQA' THEN hcl.PREESTADO END, 0)
			WHEN 3 THEN 'Tratamiento descontinuado'
			WHEN 4 THEN 'Tratamiento suspendido'
			WHEN 7 THEN 'Tratamiento terminado por salida del paciente'
			ELSE ''
		END AS StatusNameHCPRESCRA
		, CASE phd.Origin
				WHEN 'HCPRESCRA' THEN 1
				WHEN 'HCINFLIQA' THEN 2
				WHEN 'HCORDMEDICAM' THEN 3
				WHEN 'HCNUTPAREC' THEN 4
		END AS OriginOrder
		, CAST(phd.CodeSusceptibleMixingStation AS VARCHAR(36)) CodeSusceptibleMixingStation
		, phd.RequestDate
		, phd.NUMINGRES AdmissionCode
		, udt.MSClass
		, hce.FECALTPAC
		, ISNULL(cuv.SafeStatus, 1) SafeStatus
		, ISNULL(cuv.NPTVerified, 0) NPTVerified
	From (
			SELECT DISTINCT(SELECT TOP 1 ph.Id
							FROM MedicalHistory.PharmaDose ph
							WHERE ph.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation) Id,
				psms.Id as psmsId,
				hcf.NUMINGRES,
				hcf.IPCODPACI,
				psms.FullProductName, 
				phd.UnitDoseTypeId,
				phd.Dose,
				psms.MainDrugCode ProductCode,
				psms.CenterAttentionCode, 
				psms.FunctionalUnitCode,
				phd.MeasurementUnitCode,
				phd.GroupingCodeDose,
				psms.IdOrigin,
				psms.Origin,
				psms.CodeSusceptibleMixingStation,
				ISNULL(rlg.CreationDate,psms.CreationDate) as RequestDate
			FROM MedicalHistory.PharmaDose phd WITH(NOLOCK)
			INNER JOIN MedicalHistory.ProductSusceptibleMixingStation psms WITH(NOLOCK) ON psms.CodeSusceptibleMixingStation = phd.CodeSusceptibleMixingStation AND phd.ProductCode = psms.MainDrugCode
			INNER JOIN HCFARMEPD hcf on psms.CodeSusceptibleMixingStation = hcf.CodeSusceptibleMixingStation AND hcf.CODPRODUC = psms.MainDrugCode
			LEFT JOIN MedicalHistory.RoutingLog rlg ON rlg.IDHCFARMEPD = hcf.ID
			WHERE hcf.SENDTO = 2 And phd.DeliveryStatus <> 3
			GROUP BY phd.Id,
					psms.Id,
					hcf.NUMINGRES,
					hcf.IPCODPACI,
					phd.UnitDoseTypeId,
					psms.FullProductName, 
					psms.MainDrugCode, 
					phd.Dose,
					psms.CenterAttentionCode, 
					psms.FunctionalUnitCode,
					phd.MeasurementUnitCode,
					phd.GroupingCodeDose,
					psms.IdOrigin,
					psms.Origin,
					psms.CodeSusceptibleMixingStation,
					rlg.CreationDate,
					psms.CreationDate
	) AS phd
	INNER JOIN Inventory.ATC atc WITH(NOLOCK) ON atc.Code = phd.ProductCode
	INNER JOIN ADCENATEN adc ON adc.CODCENATE = phd.CenterAttentionCode
	INNER JOIN MixingStation.MedicinesProduction mp WITH(NOLOCK) ON mp.ATCId = atc.Id And phd.CenterAttentionCode = mp.CenterAttentionId 
	LEFT JOIN HCREGEGRE hce WITH(NOLOCK) ON hce.NUMINGRES = phd.NUMINGRES
	LEFT JOIN Inventory.InventoryMeasurementUnit mu ON mu.Code = phd.MeasurementUnitCode
	LEFT JOIN MixingStation.ConfirmationUnitDose cud WITH(NOLOCK) ON phd.GroupingCodeDose = cud.GroupingCodeDose AND cud.CMConfigurationId = mp.CMConfigurationId
	LEFT JOIN MixingStation.Package p WITH(NOLOCK) ON p.Id = cud.PackageId
	LEFT JOIN MixingStation.PackagePersonalized pp WITH(NOLOCK) ON pp.Id = cud.PersonalizedMasterPreparationPackageId
	LEFT JOIN MixingStation.UnitDoseType udt ON udt.Id = phd.UnitDoseTypeId
	LEFT JOIN MixingStation.ProductionLine pl WITH(NOLOCK) ON pl.Id = cud.ProductionLineId
	LEFT JOIN INUNIFUNC fu ON fu.UFUCODIGO = phd.FunctionalUnitCode
	LEFT JOIN HCPRESCRA hcp WITH(NOLOCK) ON phd.IdOrigin = hcp.ID And phd.Origin = 'HCPRESCRA'
	LEFT JOIN HCINFLIQA hcl WITH(NOLOCK) ON phd.IdOrigin = hcl.CONSECUTI And phd.Origin = 'HCINFLIQA'
	LEFT JOIN HCNUTPAREC HTC WITH(NOLOCK)  ON phd.IdOrigin = HTC.ID AND phd.Origin = 'HCNUTPAREC'
	LEFT JOIN EHR.HCORDMEDICAM hcm WITH(NOLOCK) ON hcm.ID = phd.IdOrigin AND phd.Origin = 'HCORDMEDICAM'
	LEFT JOIN HCINFLIQD hcd WITH(NOLOCK) ON hcd.CODCONCEC = hcl.CODCONCEC
	LEFT JOIN HCVIAADMI hcv ON hcv.CODVIAADM = COALESCE(hcp.CODVIAADM, hcd.VIAADMDIL, hcm.CODVIAADM)
	LEFT JOIN INPACIENT pac WITH(NOLOCK) ON pac.IPCODPACI = phd.IPCODPACI
	LEFT JOIN ADINGRESO ing ON phd.NUMINGRES = ing.NUMINGRES
	LEFT JOIN CHCAMASHO cma WITH(NOLOCK) ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE Cast(ing.CODCAMACT AS VARCHAR(15)) END, '') = cma.CODICAMAS
	LEFT JOIN MixingStation.ConfirmationUnitDoseValidations cuv ON cuv.Id = CONCAT(phd.Id, '-',psmsId,'-' ,Cast(phd.GroupingCodeDose AS VARCHAR(50)), pp.Id)
	WHERE cud.RequestMixingStationDetailId IS NULL

	------------------------------------------------------------------------------------
	UNION ALL
	------------------------------------------------------------------------------------
	
	----SOLICITUDES EXTERNAS PERSONALIZADAS (PACIENTES EXTERNOS)----
	SELECT  
		CONCAT(rudecc.Id, '-', rudeccp.Id, '-', cte_epd.Id) AS Id,
		ecc.Code AS CareCenterCode,
		CONCAT(ecc.Code, ' - ', ecc.Description) AS CareCenterDescription,
		cte_epd.AtcId AS ServiceId, 
		cte_epd.AtcCode AS ServiceCode, 
		cte_epd.AtcName AS ServiceName,
		cte_epd.ServiceDescription,
		cte_epd.TotalQuantity, 
		rudecc.CMConfigurationId,
		cte_epd.Dosage, 
		p.Id AS PackageId, 
		p.Code AS PackageCode, 
		p.[Name] AS PackageName,
		CONCAT(p.Code, ' - ', p.[Name]) AS PackageDescription,
		udt.Id AS UnitDoseTypeId, 
		udt.Code AS UnitDoseTypeCode, 
		udt.[Description] AS UnitDoseTypeName, 
		CONCAT (udt.Code, ' - ', udt.[Description]) AS UnitDoseTypeDescription,
		cte_epd.MeasurementUnitId, 
		cte_epd.MeasurementUnitCode, 
		cte_epd.MeasurementUnitName, 
		cte_epd.MeasurementUnitDescription,
		cud.Id AS ConfirmationUnitDoseId,
		pl.Id AS ProductionLineId,
		pl.Code AS ProductionLineCode,
		pl.[Name] AS ProductionLineName,
		CONCAT(pl.Code, ' - ', pl.[Name]) AS ProductionLineCodeName,
		2 AS SourceType, 
		'Solicitud externa' AS SourceName,
		pp.Id AS PackagePersonalizedId,
		CONCAT(pp.Code, ' - ', pp.[Name]) AS PackagePersonalizedCodeName,
		cte_epd.ComponentBundler AS AGRUPAQUETE, 
		pecc.IdentificationNumber AS PatientCode,
		CONCAT(pecc.IdentificationNumber, ' - ', pecc.Name, ' ', pecc.LastName) AS PatientCodeName,
		pecc.PatientBed AS Bed,
		cte_epd.AdministrationRouteName AS  AdministrationRoute,
		cte_epd.ProductNPT,
		NULL AS FunctionalUnitCode, 
		pecc.ExternalFunctionalUnit AS FuncionalUnitCodeName,
		0 AS StatusHCPRESCRA,
		'' AS StatusNameHCPRESCRA,
		NULL AS OriginOrder,
		NULL AS CodeSusceptibleMixingStation,
		rudecc.DocumentDate AS RequestDate,
		rudecc.Code AS AdmissionCode,
		udt.MSClass,
		NULL AS FECALTPAC,
		ISNULL(cuv.SafeStatus, 1) SafeStatus,
		ISNULL(cuv.NPTVerified, 0) NPTVerified
	FROM MixingStation.RequestUnitDoseExternalCareCenter rudecc	WITH(NOLOCK)
	JOIN MixingStation.RequestUnitDoseExternalCareCenterPatient rudeccp WITH(NOLOCK) ON rudecc.Id = rudeccp.RequestUnitDoseExternalCareCenterId
	JOIN MixingStation.ExternalCareCenter ecc WITH(NOLOCK) ON rudecc.ExternalCareCenterId = ecc.Id
	JOIN Cte_ExternalPreparationDetail cte_epd ON rudeccp.Id = cte_epd.RequestUnitDoseExternalCareCenterPatientId AND cte_epd.ComponentType = 1
	JOIN MixingStation.UnitDoseType udt WITH(NOLOCK) ON rudeccp.UnitDoseTypeId = udt.Id
	JOIN MixingStation.PatientExternalCareCenter pecc WITH(NOLOCK) ON rudeccp.PatientExternalCareCenterId = pecc.Id
	LEFT JOIN MixingStation.ConfirmationUnitDose cud WITH(NOLOCK) ON cte_epd.AtcCode = cud.ServiceCode AND ecc.Code = cud.CareCenterCode AND cte_epd.Dosage = cud.Dosage AND cud.RequestMixingStationDetailId IS NULL  
	LEFT JOIN MixingStation.Package	p WITH(NOLOCK) ON cud.PackageId = p.Id
	lEFT JOIN MixingStation.PackagePersonalized pp WITH(NOLOCK) ON cud.PersonalizedMasterPreparationPackageId = pp.Id
	LEFT JOIN MixingStation.ProductionLine pl WITH(NOLOCK) ON cud.ProductionLineId = pl.Id
	LEFT JOIN MixingStation.ConfirmationUnitDoseValidations cuv ON cuv.Id = CONCAT(rudecc.Id, '-', rudeccp.Id, '-', cte_epd.Id)
	WHERE rudecc.[Status] = 2 
		AND rudecc.RequestType = 1   --Tipo solicitud:  1. Solicitud de dosis personalizada  2. Solicitud de dosis estándar  3. Solicitud de Nutrición

	------------------------------------------------------------------------------------
	UNION ALL
	------------------------------------------------------------------------------------

	select  CONCAT(ecc.Id, '-', a.Id, '-', RTRIM(LTRIM(ecc.Code)), '-', ISNULL(data.Dosage, 0)) Id,
		ecc.Code CareCenterCode, ecc.Code + ' - ' + ecc.Description CareCenterDescription,
		NULL ServiceId, a.Code ServiceCode, a.Name ServiceName, a.Code + ' - ' + a.Name ServiceDescription,
		data.TotalQuantity, data.CMConfigurationId, data.Dosage,
		p.Id PackageId, p.Code PackageCode, p.Name PackageName, p.Code + ' - ' + p.Name PackageDescription,
		udt.Id UnitDoseTypeId, udt.Code UnitDoseTypeCode, udt.Description UnitDoseTypeName, udt.Code + ' - ' + udt.Description UnitDoseTypeDescription,
		null	as MeasurementUnitId, Null as  MeasurementUnitCode, Null as MeasurementUnitName, null as MeasurementUnitDescription,
		cud.Id ConfirmationUnitDoseId,
		pl.Id ProductionLineId, pl.Code ProductionLineCode, pl.Name ProductionLineName, IIF(pl.Id is null, 'Sin asignar', pl.Code + ' - ' + pl.Name) ProductionLineCodeName,
		2 SourceType, 'Solicitud dosis unitaria centro atención externo' SourceName,
		pp.Id PackagePersonalizedId, pp.Code + ' - ' + pp.Description PackagePersonalizedCodeName, '' AGRUPAQUETE, null PatientCodeName,NULL PatientCode , NULL Bed, 'INTRAVENOSA' AdministrationRoute,
		a.ProductNPT
		, fu.UFUCODIGO AS FunctionalUnitCodek
		, concat(fu.UFUCODIGO, ' - ', fu.UFUDESCRI) as FuncionalUnitCodeName
		, ISNULL(h.PREESTADO, 0) StatusHCPRESCRA,
		case ISNULL(h.PREESTADO, 0)
			when 3 then 'Tratamiento descontinuado'
			when 4 then 'Tratamiento suspendido'
			when 7 then 'Tratamiento terminado por salida del paciente'
			else ''
		end StatusNameHCPRESCRA
		, NULL As CodeSusceptibleMixingStation
		, null as OriginOrder
		, data.DocumentDate as RequestDate
		, d.NUMINGRES as AdmissionCode
		, udt.MSClass
		, (select top 1 FECALTPAC from HCREGEGRE e (nolock) where e.NUMINGRES = d.NUMINGRES order by FECALTPAC desc) as FECALTPAC,
		ISNULL(cuv.SafeStatus, 1) SafeStatus,
		ISNULL(cuv.NPTVerified, 0) NPTVerified
	from (
		select rpd.ATCId
			, 0 as Dosage
			, 0 as MeasurementUnitId
			, SUM(rpd.Quantity) TotalQuantity
			, rcc.ExternalCareCenterId
			, rcc.CMConfigurationId
			, string_agg(rpd.Id, ', ') StringIds
			, rcc.DocumentDate
		from MixingStation.[RequestUnitDoseExternalCareCenterMaquila] rpd
		inner join MixingStation.RequestUnitDoseExternalCareCenter rcc on rcc.Id = rpd.RequestUnitDoseExternalCareCenterId 
		Where rcc.RequestType in (1,3)
		group by rpd.ATCId, rcc.ExternalCareCenterId, rcc.CMConfigurationId, rcc.DocumentDate
	) data
	inner join Inventory.ATC a on a.Id = data.ATCId
	inner join MixingStation.ExternalCareCenter ecc on ecc.Id = data.ExternalCareCenterId
	left join MixingStation.ConfirmationUnitDose cud on cud.ServiceCode = a.Code and cud.CareCenterCode = ecc.Code and cud.Dosage = ISNULL(data.Dosage, 0) and cud.RequestMixingStationDetailId is null
	left join MixingStation.Package p on p.Id = cud.PackageId
	left join MixingStation.PackagePersonalized pp on pp.Id = cud.PersonalizedMasterPreparationPackageId
	left join MixingStation.UnitDoseType udt on udt.Id = p.UnitDoseTypeId
	left join MixingStation.ProductionLine pl on pl.Id = cud.ProductionLineId
	left join HCFARMEPD d with(nolock) on data.StringIds = d.ID
	left outer join dbo.INUNIFUNC fu with(nolock) on fu.UFUCODIGO = d.UFUCODIGO
	left join (
		select a.IPCODPACI, a.NUMINGRES, a.CODPRODUC, MAX(a.ID) HCPRESCRAId
		from .HCPRESCRA a 
		inner join .HCPRESCRD d on d.IPCODPACI = a.IPCODPACI and d.CODPRODUC = a.CODPRODUC and d.NUMINGRES = a.NUMINGRES
		where d.TRATMODIF = 1
		group by a.IPCODPACI, a.NUMINGRES, a.CODPRODUC
	) res on res.IPCODPACI = d.IPCODPACI and d.NUMINGRES = d.NUMINGRES and res.CODPRODUC = d.CODPRODUC
	left join .HCPRESCRA h WITH(NOLOCK) on h.ID = res.HCPRESCRAId
	LEFT JOIN MixingStation.ConfirmationUnitDoseValidations cuv ON cuv.Id = CONCAT(ecc.Id, '-', a.Id, '-', RTRIM(LTRIM(ecc.Code)), '-', ISNULL(data.Dosage, 0))
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del dashboard de confirmación de dosis unitaria en la estación de mezclas. Consolida en una sola consulta las órdenes médicas activas pendientes de preparación (prescripciones, infusiones, nutrición parenteral y órdenes de medicamentos) junto con las preparaciones para pacientes externos, mostrando para cada ítem: datos del paciente (cédula, nombre), número de ingreso, centro de atención, unidad funcional, cama, medicamento con su código ATC y clasificación farmacológica, tipo de dosis unitaria, vía de administración, unidad de medida, dosis, línea de producción asignada, estado del tratamiento y estado de verificación NPT. Integra información del módulo de historia clínica (MedicalHistory), el catálogo de inventario (Inventory.ATC, vías de administración, unidades de medida) y las tablas de pacientes/admisiones del ERP (ADCENATEN, HCREGEGRE, IPPACIENT), además de incorporar mediante un CTE las preparaciones de pacientes externos (ExternalPatientPreparation) para unificar en un único listado todos los productos susceptibles de preparación en farmacia. Es utilizada para el monitoreo y control operativo del proceso de dispensación de dosis unitaria en la estación de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListDashboardConfirmationUnitDose_Refactor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListDashboardConfirmationUnitDose_Refactor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado las dosis unitarias pendientes de confirmar en la estación de mezclas, combinando órdenes médicas internas, solicitudes externas personalizadas y solicitudes externas de maquila, con su estado, paquete, línea de producción y validaciones asociadas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose_Refactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las prescripciones internas (HCFARMEPD) deben tener SENDTO = 2 y PharmaDose.DeliveryStatus distinto de 3 para ser incluidas; Debe existir correspondencia ATC y centro de atención en MixingStation.MedicinesProduction (mp.ATCId = atc.Id AND mp.CenterAttentionId = phd.CenterAttentionCode); Para el bloque interno, la ConfirmationUnitDose enlazada debe tener RequestMixingStationDetailId IS NULL (dosis aún no confirmada/asignada a una solicitud); Para solicitudes externas personalizadas: rudecc.Status = 2 y RequestType = 1 (dosis personalizada); Para solicitudes externas tipo maquila: RequestType en (1,3) (personalizada o nutrición); Para externas personalizadas, los componentes considerados son ComponentType = 1', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose_Refactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan confirmaciones de dosis unitaria que aún no han sido asociadas a una solicitud (cud.RequestMixingStationDetailId IS NULL); SafeStatus por defecto es 1 y NPTVerified por defecto es 0 cuando no hay registro en ConfirmationUnitDoseValidations; El Id del resultado se construye uniformemente con CONCAT para servir de clave única al cruce con ConfirmationUnitDoseValidations; Las solicitudes maquila no exponen paciente ni cama (PatientCode, PatientCodeName y Bed = NULL) y la vía siempre se reporta como ''INTRAVENOSA''; Para órdenes internas se elige TOP 1 de PharmaDose por CodeSusceptibleMixingStation, garantizando una sola fila por código susceptible; El bloque externo personalizado solo considera componentes principales (ComponentType = 1) de la preparación externa', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose_Refactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un UNION ALL de tres orígenes: (1) órdenes internas con SourceType=1 ''Orden médica'', (2) solicitudes externas personalizadas con SourceType=2 ''Solicitud externa'', (3) solicitudes externas de maquila con SourceType=2 ''Solicitud dosis unitaria centro atención externo''', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose_Refactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si phd.Origin IN (''HCPRESCRA'',''HCORDMEDICAM'') → ServiceDescription = atc.Code + '' - '' + atc.AbbreviationName else Si Origin IN (''HCINFLIQA'',''HCNUTPAREC'') usa phd.FullProductName; en otro caso cadena vacía; si phd.Origin = ''HCNUTPAREC'' AND HTC.VIADMIN=''2'' AND atc.ProductNPT=1 → AdministrationRoute = ''LÍNEA PERIFÉRICA'' else Si Origin=''HCNUTPAREC'' pero no cumple, ''INTRAVENOSA''; en otros casos hcv.DESVIAADM; si phd.Origin = ''HCNUTPAREC'' → Dosage = HTC.VOLUTOTAL (volumen total de la nutrición parenteral) else Dosage = phd.Dose; si Estado de prescripción (PREESTADO) = 3 / 4 / 7 → StatusNameHCPRESCRA = ''Tratamiento descontinuado'' / ''Tratamiento suspendido'' / ''Tratamiento terminado por salida del paciente'' else Cadena vacía; si phd.Origin = ''HCPRESCRA'' | ''HCINFLIQA'' | ''HCORDMEDICAM'' | ''HCNUTPAREC'' → OriginOrder = 1 | 2 | 3 | 4 respectivamente para clasificar el tipo de origen clínico; si pl.Id IS NULL → ProductionLineCodeName = ''Sin asignar'' else Concat(Code,'' - '',Name) de la línea de producción; si ing.CODCAMACT = 0 o NULL → No se vincula cama (cadena vacía) en lugar del código de cama else Se hace JOIN a CHCAMASHO por el código de cama; si En el bloque maquila: HCPRESCRD.TRATMODIF = 1 → Se selecciona el MAX(ID) de HCPRESCRA por (paciente, ingreso, producto) para reflejar la última modificación de tratamiento', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose_Refactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.ExternalPatientPreparation; MixingStation.ExternalPatientPreparationDetail; Inventory.ATC; Inventory.AdministrationRoute; Inventory.InventoryMeasurementUnit; MedicalHistory.PharmaDose; MedicalHistory.ProductSusceptibleMixingStation; MedicalHistory.RoutingLog; HCFARMEPD; ADCENATEN; MixingStation.MedicinesProduction; HCREGEGRE; MixingStation.ConfirmationUnitDose; MixingStation.Package; MixingStation.PackagePersonalized; MixingStation.UnitDoseType; MixingStation.ProductionLine; dbo.INUNIFUNC; HCPRESCRA; HCINFLIQA; HCNUTPAREC; EHR.HCORDMEDICAM; HCINFLIQD; HCVIAADMI; INPACIENT; ADINGRESO; CHCAMASHO; MixingStation.ConfirmationUnitDoseValidations; MixingStation.RequestUnitDoseExternalCareCenter; MixingStation.RequestUnitDoseExternalCareCenterPatient (+4 adicionales)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose_Refactor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose_Refactor';
GO
