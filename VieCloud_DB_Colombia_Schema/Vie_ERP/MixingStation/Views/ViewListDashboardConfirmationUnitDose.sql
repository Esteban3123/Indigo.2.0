

CREATE VIEW [MixingStation].[ViewListDashboardConfirmationUnitDose] 
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
			CONCAT(imu.Code, ' - ', imu.Name) MeasurementUnitDescription,
			Ep.Id EntityId,
			'ExternalPatientPreparation' EntityName
	FROM MixingStation.ExternalPatientPreparation ep
	JOIN MixingStation.ExternalPatientPreparationDetail epd ON ep.Id = epd.ExternalPatientPreparationId
	JOIN Inventory.ATC a ON epd.AtcId = a.Id
	JOIN Inventory.AdministrationRoute ar ON ep.AdministrationRouteId = ar.Id
	JOIN Inventory.InventoryMeasurementUnit imu ON ISNULL(epd.MeasurementUnitId, epd.VolumeMeasureUnitId) = imu.Id 
	WHERE epd.ComponentType = 1
), 

CTE_MedicinesProduction AS (
		SELECT  mp.Id,
				mp.CMConfigurationId,
				mp.CenterAttentionId,
				mp.UnitDoseTypeId,
				a.Id AtcId,
				a.Code AtcCode,
				a.Name AtcName,
				a.AbbreviationName AbbreviationName,
				a.ProductNPT
		FROM MixingStation.MedicinesProduction mp
		JOIN Inventory.ATC a ON mp.ATCId = a.Id 
),

CTE_ConfirmationUnitDose AS(
		SELECT cud.Id ConfirmationUnitDoseId,
				cud.GroupingCodeDose,
				cud.CMConfigurationId,
				cud.KeyView,
				cud.RequestMixingStationDetailId,
				pl.Id ProductionLineId,
				pl.Code ProductionLineCode,
				pl.Name ProductionLineName,
				 COALESCE(
					CONCAT_WS(' - ', pl.Code, pl.Name), 'Sin asignar'
				) AS ProductionLineCodeName,
				p.Id PackageId,
				p.Code PackageCode,
				p.Name PackageName,
				CONCAT(p.Code, ' - ', p.Description) PackageDescription,
				pp.Id PackagePersonalizedId,
				pp.Description PackagePersonalizedCodeName
		FROM MixingStation.ConfirmationUnitDose cud
		INNER JOIN MixingStation.ProductionLine pl ON cud.ProductionLineId = pl.Id
		INNER JOIN MixingStation.Package p ON cud.PackageId = p.Id
		LEFT JOIN MixingStation.PackagePersonalized pp ON cud.PersonalizedMasterPreparationPackageId = pp.Id
),

Numbers AS (
        SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
		FROM (VALUES (1),(2),(3),(4),(5),(6),(7),(8),(9),(10)) AS t1(v)
		CROSS JOIN (VALUES (1),(2),(3),(4),(5),(6),(7),(8),(9),(10)) AS t2(v))

---------------------------------------------------------------------------------------------------
 
	SELECT  DISTINCT
		CONCAT(
		  CAST(phd.CodeSusceptibleMixingStation AS varchar(36)), '-',
		  CAST(phd.GroupingCodeDose AS varchar(36))
		) AS Id
		, RTRIM(LTRIM(adc.CODCENATE)) CareCenterCode
		, CONCAT(RTRIM(LTRIM(adc.CODCENATE)), ' - ', RTRIM(LTRIM(adc.NOMCENATE))) CareCenterDescription
		, mp.AtcId ServiceId
		, mp.AtcCode ServiceCode
		, mp.AtcName ServiceName
		, CASE 
			WHEN phd.Origin IN ('HCPRESCRA', 'HCORDMEDICAM') THEN CONCAT(mp.AtcCode, ' - ', mp.AbbreviationName)
			WHEN phd.Origin IN ('HCINFLIQA') THEN phd.FullProductName
			ELSE ''
		END AS ServiceDescription
		, 1 TotalQuantity
		, mp.CMConfigurationId
		, phd.Dose As Dosage
		, cud.PackageId
		, cud.PackageCode
		, cud.PackageName
		, cud.PackageDescription
		, udt.Id UnitDoseTypeId
		, udt.Code UnitDoseTypeCode
		, udt.Description UnitDoseTypeName
		, CONCAT(udt.Code, ' - ', udt.Description) UnitDoseTypeDescription
		, mu.Id MeasurementUnitId
		, mu.Code MeasurementUnitCode
		, mu.Name MeasurementUnitName
		, CONCAT(mu.Code, ' - ', mu.Name) MeasurementUnitDescription
		, cud.ConfirmationUnitDoseId
		, cud.ProductionLineId
		, cud.ProductionLineCode
		, cud.ProductionLineName
		, cud.ProductionLineCodeName
		, 1 SourceType
		, 'Orden médica' SourceName
		, cud.PackagePersonalizedId
		, cud.PackagePersonalizedCodeName
		, CAST(phd.GroupingCodeDose As Varchar(36)) AGRUPAQUETE
		, pac.IPCODPACI as PatientCode	
		, CONCAT(LTRIM(RTRIM(pac.IPCODPACI)), ' - ', LTRIM(RTRIM(pac.IPNOMCOMP))) As PatientCodeName
		, ISNULL(cma.NUMCAMHOS, '') As Bed
		, hcv.DESVIAADM AdministrationRoute
		, mp.ProductNPT
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
		END AS OriginOrder
		, CAST(phd.CodeSusceptibleMixingStation AS VARCHAR(36)) CodeSusceptibleMixingStation
		, phd.RequestDate RequestDate
		, phd.NUMINGRES AdmissionCode
		, udt.MSClass
		, hce.FECALTPAC
		, ISNULL(cuv.SafeStatus, 1) SafeStatus
		, ISNULL(cuv.NPTVerified, 0) NPTVerified,
		NULL EntityId,
		NULL EntityName,
		IIF(udt.MSClass IN (3, 9, 10) AND phd.IDETIPHIS = 'ENFERMER1', CAST(1 AS BIT), CAST(0 AS BIT)) AS AllowPharmaceuticalCareRouting
	From (
			SELECT DISTINCT
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
				psms.CreationDate as RequestDate,
				phd.MixingStationId,
				hcf.IDETIPHIS,
				hcf.IDCITA
			FROM MedicalHistory.PharmaDose phd
			INNER JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.CodeSusceptibleMixingStation = phd.CodeSusceptibleMixingStation AND phd.ProductCode = psms.MainDrugCode
			INNER JOIN HCFARMEPD hcf on psms.CodeSusceptibleMixingStation = hcf.CodeSusceptibleMixingStation AND hcf.CODPRODUC = psms.MainDrugCode
			WHERE hcf.SENDTO = 2 And phd.DeliveryStatus <> 3 AND hcf.SourceTable NOT IN ('HCNUTPAREC')
			GROUP BY psms.Id,
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
					psms.CreationDate,
					phd.MixingStationId,
					hcf.IDETIPHIS,
					hcf.IDCITA
	) AS phd
	INNER JOIN CTE_MedicinesProduction mp ON phd.ProductCode = mp.AtcCode AND phd.CenterAttentionCode = mp.CenterAttentionId
												AND mp.UnitDoseTypeId = phd.UnitDoseTypeId AND phd.MixingStationId = mp.CMConfigurationId
	INNER JOIN ADCENATEN adc ON adc.CODCENATE = phd.CenterAttentionCode
	LEFT JOIN CTE_ConfirmationUnitDose cud ON phd.GroupingCodeDose = cud.GroupingCodeDose AND cud.CMConfigurationId = mp.CMConfigurationId 
												AND cud.KeyView = CONCAT(
																			CAST(phd.CodeSusceptibleMixingStation AS varchar(36)), '-',
																			CAST(phd.GroupingCodeDose AS varchar(36))
																		)
	LEFT JOIN HCREGEGRE hce ON hce.NUMINGRES = phd.NUMINGRES
	LEFT JOIN Inventory.InventoryMeasurementUnit mu ON mu.Code = phd.MeasurementUnitCode
	LEFT JOIN MixingStation.UnitDoseType udt ON udt.Id = phd.UnitDoseTypeId
	LEFT JOIN INUNIFUNC fu ON fu.UFUCODIGO = phd.FunctionalUnitCode
	LEFT JOIN HCPRESCRA hcp ON phd.IdOrigin = hcp.ID And phd.Origin = 'HCPRESCRA'
	LEFT JOIN HCINFLIQA hcl ON phd.IdOrigin = hcl.CONSECUTI And phd.Origin = 'HCINFLIQA'
	LEFT JOIN EHR.HCORDMEDICAM hcm ON hcm.ID = phd.IdOrigin AND phd.Origin = 'HCORDMEDICAM'
	LEFT JOIN HCINFLIQD hcd ON hcd.CODCONCEC = hcl.CODCONCEC
	LEFT JOIN HCVIAADMI hcv ON hcv.CODVIAADM = COALESCE(hcp.CODVIAADM, hcd.VIAADMDIL, hcm.CODVIAADM)
	LEFT JOIN INPACIENT pac ON pac.IPCODPACI = phd.IPCODPACI
	LEFT JOIN ADINGRESO ing ON phd.NUMINGRES = ing.NUMINGRES
	LEFT JOIN CHCAMASHO cma ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE Cast(ing.CODCAMACT AS VARCHAR(15)) END, '') = cma.CODICAMAS
	LEFT JOIN MixingStation.ConfirmationUnitDoseValidations cuv ON cuv.Id = CONCAT(
																								  CAST(phd.CodeSusceptibleMixingStation AS varchar(36)), '-',
																								  CAST(phd.GroupingCodeDose AS varchar(36))
																								)
	WHERE cud.RequestMixingStationDetailId IS NULL 

	------------------------------------------------------------------------------------
	UNION ALL
	------------------------------------------------------------------------------------
	-- NUTRICIONES PARENTERALES
	SELECT  DISTINCT
		CONCAT(
		  CAST(phd.CodeSusceptibleMixingStation AS varchar(36)), '-',
		  CAST(phd.GroupingCodeDose AS varchar(36))
		) AS Id
		, RTRIM(LTRIM(adc.CODCENATE)) CareCenterCode
		, CONCAT(RTRIM(LTRIM(adc.CODCENATE)), ' - ', RTRIM(LTRIM(adc.NOMCENATE))) CareCenterDescription
		, mp.AtcId ServiceId
		, mp.AtcCode ServiceCode
		, mp.AtcName ServiceName
		, phd.FullProductName AS ServiceDescription
		, 1 TotalQuantity
		, mp.CMConfigurationId
		, HTC.VOLUTOTAL AS Dosage
		, cud.PackageId
		, cud.PackageCode
		, cud.PackageName
		, cud.PackageDescription
		, udt.Id UnitDoseTypeId
		, udt.Code UnitDoseTypeCode
		, udt.Description UnitDoseTypeName
		, CONCAT(udt.Code, ' - ', udt.Description) UnitDoseTypeDescription
		, mu.Id MeasurementUnitId
		, mu.Code MeasurementUnitCode
		, mu.Name MeasurementUnitName
		, CONCAT(mu.Code, ' - ', mu.Name) MeasurementUnitDescription
		, cud.ConfirmationUnitDoseId
		, cud.ProductionLineId
		, cud.ProductionLineCode
		, cud.ProductionLineName
		, cud.ProductionLineCodeName
		, 1 SourceType
		, 'Orden médica' SourceName
		, cud.PackagePersonalizedId
		, cud.PackagePersonalizedCodeName
		, CAST(phd.GroupingCodeDose As Varchar(36)) AGRUPAQUETE
		, pac.IPCODPACI as PatientCode	
		, CONCAT(LTRIM(RTRIM(pac.IPCODPACI)), ' - ', LTRIM(RTRIM(pac.IPNOMCOMP))) As PatientCodeName
		, ISNULL(cma.NUMCAMHOS, '') As Bed
		, IIF(HTC.VIADMIN = '2','LÍNEA PERIFÉRICA' ,'LÍNEA CENTRAL') AS AdministrationRoute
		, MP.ProductNPT
		, fu.UFUCODIGO FunctionalUnitCode	
		, CONCAT(fu.UFUCODIGO, ' - ', fu.UFUDESCRI) FuncionalUnitCodeName
		, 0 StatusHCPRESCRA
		, '' AS StatusNameHCPRESCRA
		, 4 AS OriginOrder
		, CAST(phd.CodeSusceptibleMixingStation AS VARCHAR(36)) CodeSusceptibleMixingStation
		, phd.RequestDate
		, phd.NUMINGRES AdmissionCode
		, udt.MSClass
		, hce.FECALTPAC
		, ISNULL(cuv.SafeStatus, 1) SafeStatus
		, ISNULL(cuv.NPTVerified, 0) NPTVerified,
		NULL EntityId,
		NULL EntityName,
		CAST(0 AS BIT) AS AllowPharmaceuticalCareRouting
	From (
			SELECT DISTINCT
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
				psms.CreationDate as RequestDate,
				phd.MixingStationId
			FROM HCFARMEPD hcf
			INNER JOIN HCNUTPAREC n ON hcf.IdSourceTable = n.ID
			INNER JOIN HCPARNUTC nc ON n.IDHCPARNUTC = nc.Id
			INNER JOIN MedicalHistory.ProductSusceptibleMixingStation psms
				ON hcf.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation
					AND nc.FinishedProductCode = psms.MainDrugCode
			CROSS APPLY (
				SELECT TOP 1
					phd.UnitDoseTypeId,
					phd.Dose,
					phd.MeasurementUnitCode,
					phd.GroupingCodeDose,
					phd.DeliveryStatus,
					phd.mixingStationId
				FROM MedicalHistory.PharmaDose phd
				WHERE psms.CodeSusceptibleMixingStation = phd.CodeSusceptibleMixingStation
			) phd
			WHERE hcf.SENDTO = 2 And phd.DeliveryStatus <> 3 AND hcf.SourceTable IN ('HCNUTPAREC')
			GROUP BY psms.Id,
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
					psms.CreationDate,
					phd.MixingStationId
	) AS phd
	INNER JOIN CTE_MedicinesProduction mp ON phd.ProductCode = mp.AtcCode AND phd.CenterAttentionCode = mp.CenterAttentionId
												AND phd.UnitDoseTypeId = mp.UnitDoseTypeId AND mp.CMConfigurationId = phd.MixingStationId
	INNER JOIN ADCENATEN adc ON adc.CODCENATE = phd.CenterAttentionCode
	LEFT JOIN CTE_ConfirmationUnitDose cud ON phd.GroupingCodeDose = cud.GroupingCodeDose AND cud.CMConfigurationId = mp.CMConfigurationId 
												AND cud.KeyView = CONCAT(
																			CAST(phd.CodeSusceptibleMixingStation AS varchar(36)), '-',
																			CAST(phd.GroupingCodeDose AS varchar(36))
																		)
	LEFT JOIN HCREGEGRE hce ON hce.NUMINGRES = phd.NUMINGRES
	LEFT JOIN Inventory.InventoryMeasurementUnit mu ON mu.Code = phd.MeasurementUnitCode
	LEFT JOIN MixingStation.UnitDoseType udt ON udt.Id = phd.UnitDoseTypeId
	LEFT JOIN INUNIFUNC fu ON fu.UFUCODIGO = phd.FunctionalUnitCode
	LEFT JOIN HCNUTPAREC HTC  ON phd.IdOrigin = HTC.ID AND phd.Origin = 'HCNUTPAREC'
	LEFT JOIN INPACIENT pac ON pac.IPCODPACI = phd.IPCODPACI
	LEFT JOIN ADINGRESO ing ON phd.NUMINGRES = ing.NUMINGRES
	LEFT JOIN CHCAMASHO cma ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE Cast(ing.CODCAMACT AS VARCHAR(15)) END, '') = cma.CODICAMAS
	LEFT JOIN MixingStation.ConfirmationUnitDoseValidations cuv ON cuv.Id = CONCAT(
																								  CAST(phd.CodeSusceptibleMixingStation AS varchar(36)), '-',
																								  CAST(phd.GroupingCodeDose AS varchar(36))
																								)
	WHERE cud.RequestMixingStationDetailId IS NULL

	------------------------------------------------------------------------------------
	UNION ALL
	------------------------------------------------------------------------------------
	
	----SOLICITUDES EXTERNAS PERSONALIZADAS (PACIENTES EXTERNOS)----
	SELECT  
		CONCAT(rudecc.Id, '-', rudeccp.Id, '-', cte_epd.Id, n.n) AS Id,
		ecc.Code AS CareCenterCode,
		CONCAT(ecc.Code, ' - ', ecc.Description) AS CareCenterDescription,
		cte_epd.AtcId AS ServiceId, 
		cte_epd.AtcCode AS ServiceCode, 
		cte_epd.AtcName AS ServiceName,
		cte_epd.ServiceDescription,
		cte_epd.TotalQuantity, 
		rudecc.CMConfigurationId,
		cte_epd.Dosage, 
		cud.PackageId, 
		cud.PackageCode, 
		cud.PackageName,
		cud.PackageDescription,
		udt.Id AS UnitDoseTypeId, 
		udt.Code AS UnitDoseTypeCode, 
		udt.[Description] AS UnitDoseTypeName, 
		CONCAT (udt.Code, ' - ', udt.[Description]) AS UnitDoseTypeDescription,
		cte_epd.MeasurementUnitId, 
		cte_epd.MeasurementUnitCode, 
		cte_epd.MeasurementUnitName, 
		cte_epd.MeasurementUnitDescription,
		cud.ConfirmationUnitDoseId,
		cud.ProductionLineId,
		cud.ProductionLineCode,
		cud.ProductionLineName,
		cud.ProductionLineCodeName,
		2 AS SourceType, 
		'Solicitud externa' AS SourceName,
		cud.PackagePersonalizedId,
		cud.PackagePersonalizedCodeName,
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
		NULL AS AdmissionCode,
		udt.MSClass,
		NULL AS FECALTPAC,
		ISNULL(cuv.SafeStatus, 1) SafeStatus,
		ISNULL(cuv.NPTVerified, 0) NPTVerified,
		cte_epd.EntityId,
		cte_epd.EntityName,
		CAST(0 AS BIT) AS AllowPharmaceuticalCareRouting
	FROM MixingStation.RequestUnitDoseExternalCareCenter rudecc
	JOIN MixingStation.RequestUnitDoseExternalCareCenterPatient rudeccp ON rudecc.Id = rudeccp.RequestUnitDoseExternalCareCenterId
	JOIN MixingStation.ExternalCareCenter ecc ON rudecc.ExternalCareCenterId = ecc.Id
	JOIN Cte_ExternalPreparationDetail cte_epd ON rudeccp.Id = cte_epd.RequestUnitDoseExternalCareCenterPatientId
	JOIN Numbers AS n ON n.n <= cte_epd.TotalQuantity
	JOIN MixingStation.UnitDoseType udt ON rudeccp.UnitDoseTypeId = udt.Id
	JOIN MixingStation.PatientExternalCareCenter pecc ON rudeccp.PatientExternalCareCenterId = pecc.Id
	LEFt JOIN CTE_ConfirmationUnitDose cud ON cud.KeyView = CONCAT(rudecc.Id, '-', rudeccp.Id, '-', cte_epd.Id, n.n)
	LEFT JOIN MixingStation.ConfirmationUnitDoseValidations cuv ON cuv.Id = CONCAT(rudecc.Id, '-', rudeccp.Id, '-', cte_epd.Id)
	WHERE rudecc.[Status] = 2 
		AND rudecc.RequestType = 1   --Tipo solicitud:  1. Solicitud de dosis personalizada  2. Solicitud de dosis estándar  3. Solicitud de Nutrición
		AND cud.RequestMixingStationDetailId IS NULL
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
		ISNULL(cuv.NPTVerified, 0) NPTVerified,
		NULL EntityId,
		NULL EntityName,
		CAST(0 AS BIT) AS AllowPharmaceuticalCareRouting
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
	left join MixingStation.ProductionLine pl on pl.Id = cud.ProductionLineId
	left join MixingStation.UnitDoseType udt on udt.Id = p.UnitDoseTypeId
	left join HCFARMEPD d on data.StringIds = d.ID
	left outer join dbo.INUNIFUNC fu on fu.UFUCODIGO = d.UFUCODIGO
	left join (
		select a.IPCODPACI, a.NUMINGRES, a.CODPRODUC, MAX(a.ID) HCPRESCRAId
		from .HCPRESCRA a
		inner join .HCPRESCRD d on d.IPCODPACI = a.IPCODPACI and d.CODPRODUC = a.CODPRODUC and d.NUMINGRES = a.NUMINGRES
		where d.TRATMODIF = 1
		group by a.IPCODPACI, a.NUMINGRES, a.CODPRODUC
	) res on res.IPCODPACI = d.IPCODPACI and res.NUMINGRES = d.NUMINGRES and res.CODPRODUC = d.CODPRODUC
	left join .HCPRESCRA h on h.ID = res.HCPRESCRAId
	LEFT JOIN MixingStation.ConfirmationUnitDoseValidations cuv ON cuv.Id = CONCAT(ecc.Id, '-', a.Id, '-', RTRIM(LTRIM(ecc.Code)), '-', ISNULL(data.Dosage, 0))
	WHERE cud.RequestMixingStationDetailId IS NULL
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del dashboard de confirmación de dosis unitaria para la estación de mezclas. Consolida en una sola consulta las preparaciones de pacientes externos (mezclas magistrales, NPT y otros preparados especiales) junto con las órdenes médicas internas (prescripciones, infusiones y órdenes de medicamentos) que están pendientes o en proceso de preparación en la estación de mezclas. Integra información del paciente (cédula, nombre), ingreso hospitalario, cama, unidad funcional, centro de atención, medicamento (código ATC, nombre, abreviatura), dosis, vía de administración, unidad de medida, tipo de dosis unitaria, línea de producción asignada, empaque (estándar o personalizado), estado del tratamiento y si la preparación es NPT verificada o segura. Está diseñada para alimentar el tablero de control (dashboard) del farmacéutico o técnico de mezclas, permitiendo visualizar y gestionar en tiempo real todas las preparaciones que deben confirmarse, agrupadas por código de agrupación de dosis, origen de la orden y estado clínico del paciente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListDashboardConfirmationUnitDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListDashboardConfirmationUnitDose';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista las dosis unitarias pendientes de confirmación en la estación de mezclas, integrando órdenes médicas internas, nutriciones parenterales y solicitudes externas (personalizadas y de maquila) para alimentar el dashboard de confirmación.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los productos a producir deben existir en MixingStation.MedicinesProduction emparejados por código ATC y centro de atención (join INNER).; Para órdenes internas: el documento HCFARMEPD debe tener SENDTO=2 y la PharmaDose asociada DeliveryStatus distinto de 3.; Para nutriciones parenterales: HCFARMEPD.SourceTable debe ser ''HCNUTPAREC''; para el resto de órdenes médicas se excluye ''HCNUTPAREC''.; Para solicitudes externas personalizadas: RequestUnitDoseExternalCareCenter.Status=2 y RequestType=1.; Para maquila externa: RequestUnitDoseExternalCareCenter.RequestType debe ser 1 o 3.; En todas las ramas, el ConfirmationUnitDose enlazado debe tener RequestMixingStationDetailId IS NULL (dosis aún no enlazada a una solicitud de mezcla).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen dosis cuyo ConfirmationUnitDose asociado aún no está vinculado a una solicitud de estación de mezclas (RequestMixingStationDetailId IS NULL) en todas las ramas.; SafeStatus por defecto es 1 y NPTVerified por defecto es 0 cuando no existe registro en ConfirmationUnitDoseValidations.; ProductionLineCodeName toma ''Sin asignar'' cuando no hay línea de producción asignada.; Las solicitudes externas personalizadas se expanden en N filas (una por unidad solicitada) usando un generador de números 1..100.; Las dosis con DeliveryStatus=3 (entregadas/canceladas) nunca aparecen en el dashboard.; Solo documentos farmacéuticos con SENDTO=2 (enrutados a la estación de mezclas) son considerados en las ramas internas.; Las filas externas no tienen AdmissionCode, CodeSusceptibleMixingStation ni OriginOrder (NULL); las internas no tienen EntityId/EntityName.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListDashboardConfirmationUnitDose: Devuelve filas unificadas (UNION ALL de 4 fuentes) con identificador compuesto por Id de PharmaDose/solicitud, psmsId, GroupingCodeDose y PackagePersonalizedId, marcadas con SourceType=1 ''Orden médica'' o SourceType=2 ''Solicitud externa''/''Solicitud dosis unitaria centro atención externo''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si phd.Origin IN (''HCPRESCRA'',''HCORDMEDICAM'') → ServiceDescription se compone como ''{AtcCode} - {AbbreviationName}'' else Si Origin=''HCINFLIQA'' usa phd.FullProductName; en otros casos cadena vacía; si Estado clínico (PREESTADO de HCPRESCRA o HCINFLIQA según Origin) → Se traduce a texto: 3=''Tratamiento descontinuado'', 4=''Tratamiento suspendido'', 7=''Tratamiento terminado por salida del paciente'' else Cadena vacía para otros valores; si phd.Origin → Determina OriginOrder: ''HCPRESCRA''=1, ''HCINFLIQA''=2, ''HCORDMEDICAM''=3; rama de nutrición parenteral asigna fijo OriginOrder=4; si HCNUTPAREC.VIADMIN = ''2'' → AdministrationRoute = ''LÍNEA PERIFÉRICA'' else AdministrationRoute = ''LÍNEA CENTRAL''; si HCFARMEPD.SourceTable IN (''HCNUTPAREC'') → La fila se procesa en la rama de nutriciones parenterales con dosis = HCNUTPAREC.VOLUTOTAL else Se procesa como orden médica regular usando phd.Dose; si RequestUnitDoseExternalCareCenter.RequestType → =1 con Status=2 alimenta solicitudes externas personalizadas; IN (1,3) alimenta la rama de maquila externa; si ExternalPatientPreparationDetail.ComponentType = 1 → Solo estos componentes se incluyen en el CTE de preparaciones externas (filtro WHERE en CTE); si ing.CODCAMACT = 0 → Se reemplaza por cadena vacía antes de buscar la cama en CHCAMASHO else Se castea a VARCHAR(15) para hacer match con CODICAMAS', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.ExternalPatientPreparation; MixingStation.ExternalPatientPreparationDetail; Inventory.ATC; Inventory.AdministrationRoute; Inventory.InventoryMeasurementUnit; MixingStation.MedicinesProduction; MixingStation.ConfirmationUnitDose; MixingStation.ProductionLine; MixingStation.Package; MixingStation.PackagePersonalized; MedicalHistory.PharmaDose; MedicalHistory.ProductSusceptibleMixingStation; dbo.HCFARMEPD; dbo.ADCENATEN; dbo.HCREGEGRE; MixingStation.UnitDoseType; dbo.INUNIFUNC; dbo.HCPRESCRA; dbo.HCINFLIQA; EHR.HCORDMEDICAM; dbo.HCINFLIQD; dbo.HCVIAADMI; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHCAMASHO; MixingStation.ConfirmationUnitDoseValidations; dbo.HCNUTPAREC; dbo.HCPARNUTC; MixingStation.RequestUnitDoseExternalCareCenter (+5 adicionales)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListDashboardConfirmationUnitDose';
GO
