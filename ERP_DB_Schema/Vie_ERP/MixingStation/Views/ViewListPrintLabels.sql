CREATE view [MixingStation].[ViewListPrintLabels]  
AS

WITH CTE_InfoDetail AS (
    SELECT  
        pd.PackageId,
        pd.ComponentType,
        pd.MainMedicine,
        pd.Vehicle,
        pd.Quantity,
        COALESCE(pd.MeasurementUnitId, pd.VolumeMeasureUnit) AS MeasurementUnitId,
        STRING_AGG(
            CASE pd.ComponentType
                WHEN 1 THEN a.AbbreviationName
                WHEN 2 THEN su.SupplieName
                WHEN 3 THEN ipr.Abbreviation
                ELSE ''
            END, ', '
        ) AS ComponentName
    FROM MixingStation.PackageDetail pd
    LEFT JOIN Inventory.ATC a ON pd.AtcId = a.Id
    LEFT JOIN Inventory.InventorySupplie su ON pd.SupplieId = su.Id
    LEFT JOIN Inventory.InventoryProduct ipr ON pd.ProductId = ipr.Id
    WHERE pd.MainMedicine = 1 OR pd.Vehicle = 1
    GROUP BY pd.PackageId, pd.ComponentType, pd.MainMedicine, pd.Vehicle, pd.Quantity,
             pd.MeasurementUnitId, pd.VolumeMeasureUnit
)
-----------------------------------------------------------------------------------
, Cte_InfoPatient AS(
	SELECT	rmsdp.RequestMixingStationDetailId,
			inp.IPNOMCOMP AS PatientName, 
			inp.IPCODPACI AS PatientCode,
			rmsdp.Bed, 
			fu.[Name] AS FunctionalUnitName
	FROM MixingStation.RequestMixingStationDetailPatients rmsdp
	JOIN INPACIENT inp ON rmsdp.PatientCode = inp.IPCODPACI
	JOIN Payroll.FunctionalUnit fu ON fu.Code = rmsdp.FunctionalUnitCode
)
, Cte_NptIndications AS( 
	SELECT	pd.GroupingCodeDose,
			np.INDICACIONADM AS AdministrationIndications, 
			np.INDICACIONADI AS AditionalIndications,
			CASE np.VIADMIN 
				WHEN 1 THEN 'Línea central'
				WHEN 2 THEN 'Línea periférica'
				ELSE ''
			END AS AdministrationRoute 
	FROM MedicalHistory.PharmaDose pd
	JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON pd.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation
	JOIN HCFARMEPD hpd ON psms.CodeSusceptibleMixingStation = hpd.CodeSusceptibleMixingStation
	JOIN MixingStation.UnitDoseType udt ON pd.UnitDoseTypeId = udt.Id 
	JOIN HCNUTPAREC np ON hpd.IdSourceTable = np.ID
	WHERE hpd.SourceTable = 'HCNUTPAREC'AND udt.MSClass = 2
	GROUP BY pd.GroupingCodeDose, np.VIADMIN, np.INDICACIONADM, np.INDICACIONADI
)
-------------------------------------------------------------------------------
, CTE_UserByCampaign AS (
    SELECT cdu.Id Id
		, cdu.CampaignDetailId
		, cdu.UserId
		, cdu.UserRole
		, RTRIM(p.Fullname) AS UserName   
		FROM MixingStation.CampaignDetailUsers cdu
		JOIN Security.[User] u ON cdu.UserId = u.Id
		JOIN [Security].[Person] p ON u.IdPerson = p.Id
		WHERE cdu.UserRole IN (1, 2)	-- Químico de calidad y químico de producción
)
---------------------------------------------------------------------------------
,CTE_DosesFromPharmaDose AS (
	SELECT
		rpds.Id AS RequestPackageDetailStatusId,
		sum(pd.Dose)  AS Doses
	FROM MixingStation.RequestPackageDetailStatus rpds
	JOIN MedicalHistory.PharmaDose pd
		ON rpds.GroupingCodeDose = pd.GroupingCodeDose
	LEFT JOIN Inventory.InventoryMeasurementUnit mu
		ON pd.MeasurementUnitCode = mu.Code
	WHERE pd.Dose IS NOT NULL
	GROUP BY rpds.Id
)
-------------------------------------------------------------------------------
SELECT 	DISTINCT 
		rpds.Id,
		fu.Name FunctionalUnitName,
		it.IPNOMCOMP PatientName,
		it.IPCODPACI PatientCode,
		ISNULL(chc.NUMCAMHOS, a.CODICAMHO) Bed,
		p.Name PackageName,
		(
			SELECT STRING_AGG(
				CASE pd.ComponentType
					WHEN 1 THEN a.AbbreviationName
					WHEN 2 THEN su.SupplieName
					WHEN 3 THEN ipr.Abbreviation
					ELSE ''
				END
			, ', ') 
			FROM MixingStation.PackageDetail pd
			LEFT JOIN Inventory.InventoryProduct ipr ON pd.ProductId = ipr.Id
			LEFT JOIN Inventory.InventorySupplie su ON pd.SupplieId = su.Id
			LEFT JOIN Inventory.ATC a ON pd.AtcId = a.Id
			WHERE pd.PackageId = p.Id And pd.MainMedicine = 1
		) AS MainMedicines,
		CASE 
			WHEN Msclass IN (3, 4, 10) THEN ISNULL(pd.Dose, 0)
			ELSE ISNULL(dfpd.Doses, 0)
		END AS Dose,
		ISNULL((
			SELECT TOP 1 atc.AbbreviationName
			FROM MixingStation.PackageDetail pd
			JOIN Inventory.ATC atc ON pd.AtcId = atc.Id
			WHERE pd.PackageId = p.Id AND pd.Vehicle = 1
		), 'N/A') AS VehicleName,
		CASE 
			WHEN ut.MSClass IN (3, 4, 10) THEN  CONCAT(pp.VolumeTotalPrepared,'-', imu3.Abbreviation) 
			ELSE CONCAT(p.VolumeTotalOrder,'-', imu.Abbreviation) 
		END VolumeTotalOrder,
		CASE 
			WHEN ut.MSClass IN (3, 4, 10) THEN COALESCE(pp.ConcentrationAntibiotic, p.ConcentrationAntibiotic, '0')
			ELSE CONCAT(COALESCE(pp.Concentration, p.Concentration, 0), '-',ISNULL(imu2.Abbreviation, imu3.Abbreviation))
		END Concentration,
		rpds.BatchCode,
		CAST(cd.ProcessingDate AS DATE) ElaborationDate,
		CONVERT(VARCHAR(20), rpds.PreparationTime, 108)  ElaborationHour,
		rpds.BatchExpirationDate ExpirationDate,
		hcva.DESVIAADM AdministrationRoute,
		CASE 
			WHEN ut.MSClass IN (4,10) THEN hcm.INSTRUADMINIS --Citostatico, Esteril
			ELSE hcp.DESADMINI 
		END AS Indications,
		CASE ut.MSClass
			WHEN 3 THEN 'Medicamento estéril en dosis unitaria'
			WHEN 4 THEN 'Medicamento oncológico de manipulación riesgosa'
		END SpecialIndications,
		CASE p.Storage
			WHEN 1 THEN 'Ambiente: 20 ° C - 25 ° C (Permitida 15 ° C y 30 ° C)'
			WHEN 2 THEN 'Ambiente controlada: 20 ° C - 25 ° C'
			WHEN 3 THEN 'En frío: 8 ° C - 15 ° C'
			WHEN 4 THEN 'Refrigerador: 2 ° C - 8 ° C'
			WHEN 5 THEN 'Congelador: -25 ° C - 10 ° C'
		END Storage,
		irl.Name NameRiskLevel,
		ut.Description UnitDoseTypeDes,
		rd.CampaignDetailId,
		mu.Abbreviation MeasurementUnitAbbreviation,
		(SELECT TOP 1 
			vau.Nombre
			FROM MixingStation.CampaignDetailUsers cdu
			JOIN MixingStation.ViewListAuthorizeUsers vau ON vau.UserId = cdu.UserId
			WHERE cdu.CampaignDetailId = rd.CampaignDetailId AND cdu.UserRole = 1) NameQualityChemical,
		(SELECT TOP 1 
			vau.Nombre
			FROM MixingStation.CampaignDetailUsers cdu
			JOIN MixingStation.ViewListAuthorizeUsers vau ON vau.UserId = cdu.UserId
			WHERE cdu.CampaignDetailId = rd.CampaignDetailId AND cdu.UserRole = 2) NameProductionChemical,
		rd.LabelType
FROM MixingStation.RequestPackageDetailStatus rpds 
INNER JOIN MixingStation.PackagePersonalized pp on rpds.PackagePersonalizedId = pp.Id
INNER JOIN MixingStation.Package p ON p.Id = rpds.PackageId
INNER JOIN Inventory.InventoryRiskLevel irl ON irl.Id = p.RiskLevelId
INNER JOIN MixingStation.RequestMixingStationDetail rd ON rpds.RequestMixingStationDetailId = rd.Id
INNER JOIN MixingStation.CampaignDetail as cd ON cd.Id = rd.CampaignDetailId
INNER JOIN MixingStation.UnitDoseType ut ON rd.UnitDoseTypeId = ut.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu ON pp.VolumeTotalOrderMeasurementUnitId= imu.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu2 ON pp.ConcentrationMeasurementUnitId =imu2.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu3 ON pp.MeasurementPreparedId = imu3.Id
LEFT JOIN MixingStation.PackageDetail pkd ON pkd.PackageId = p.Id and pkd.MainMedicine = 1
LEFT JOIN Inventory.ATC atc ON pkd.AtcId = atc.Id
LEFT JOIN MedicalHistory.PharmaDose  pd ON rpds.GroupingCodeDose = pd.GroupingCodeDose and pd.ProductCode =  atc.Code
LEFT JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON pd.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation
LEFT JOIN HCFARMEPC hc ON hc.CODCONCEC = pd.IDHCFARMEPC
LEFT JOIN Payroll.FunctionalUnit fu ON fu.Code = hc.UFUCODIGO
LEFT JOIN INPACIENT it ON hc.IPCODPACI=it.IPCODPACI
LEFT JOIN ADINGRESO a ON hc.NUMINGRES =a.NUMINGRES
LEFT JOIN CHREGESTA chr WITH(NOLOCK) ON hc.NUMINGRES = chr.NUMINGRES AND chr.REGESTADO = 1
LEFT JOIN CHCAMASHO chc WITH(NOLOCK) ON chr.CODICAMAS = chc.CODICAMAS
LEFT JOIN Inventory.InventoryMeasurementUnit mu ON pd.MeasurementUnitCode = mu.Code
--- Preescripcion de solicitudes instrahospitalarias
LEFT JOIN HCPRESCRA hcp ON psms.Origin ='HCPRESCRA' and psms.IdOrigin=hcp.ID
--- Prescripción desde esquemas
LEFT JOIN [EHR].HCORDMEDICAM hcm ON psms.Origin ='HCORDMEDICAM' AND psms.IdOrigin = hcm.ID
LEFT JOIN [EHR].SchemesDrugs sd ON sd.SchemesId = hcm.SchemesId AND sd.DrugCode = psms.MainDrugCode
-- Preescripcion desde mezclas y liquidos
LEFT JOIN HCINFLIQA hca ON psms.Origin = 'HCINFLIQA' AND hca.CONSECUTI = psms.IdOrigin
LEFT JOIN HCINFLIQD hcd ON hcd.CODCONCEC = hca.CODCONCEC
-- Via de administracion para los diferentes origenes
LEFT JOIN HCVIAADMI hcva ON hcva.CODVIAADM = COALESCE(hcd.VIAADMDIL,sd.RouteOfAdministration, hcp.CODVIAADM)
--Relacion dosis
LEFT JOIN CTE_DosesFromPharmaDose dfpd ON dfpd.RequestPackageDetailStatusId = rpds.Id
WHERE rpds.Status NOT IN (5,6) AND rd.Source = 1 AND ut.MSClass <> 2 AND ISNULL(pp.PreparationType, p.PreparationType) <> 4

UNION ALL
-------- Solicitudes intrahospitalarias de nutriciones parenterales --------
SELECT CAST(CONCAT(rpds.Id, pp.Id) AS BIGINT) Id,
		cte_ip.FunctionalUnitName,
		cte_ip.PatientName,
		cte_ip.PatientCode,
		cte_ip.Bed,
		p.Name AS PackageName,
		p.Name AS MainMedicines,
		0 AS Dose,
		'' AS VehicleName,
		CONCAT(COALESCE(pp.VolumeTotalOrder, p.VolumeTotalOrder, ''),'-', imu.Abbreviation)  AS VolumeTotalOrder,
		'' Concentration,
		rpds.BatchCode AS BatchCode,
		CAST(cd.ProcessingDate AS DATE) ElaborationDate,
		CONVERT(VARCHAR(20), rpds.PreparationTime, 100) AS ElaborationHour,
		rpds.BatchExpirationDate AS ExpirationDate,
		cte_npti.AdministrationRoute AS AdministrationRoute,
		cte_npti.AdministrationIndications AS Indications,
		cte_npti.AditionalIndications AS SpecialIndications,
		CASE ISNULL(pp.Storage, p.Storage)
			WHEN 1 THEN 'Ambiente: 20 ° C - 25 ° C (Permitida 15 ° C y 30 ° C)'
			WHEN 2 THEN 'Ambiente controlada: 20 ° C - 25 ° C'
			WHEN 3 THEN 'En frío: 8 ° C - 15 ° C'
			WHEN 4 THEN 'Refrigerador: 2 ° C - 8 ° C'
			WHEN 5 THEN 'Congelador: -25 ° C - 10 ° C'
			ELSE 'No posee almacenamiento parametrizado'
		END Storage,
		ISNULL(irl.[Name], '') AS NameRiskLevel,
		ut.[Description] AS UnitDoseTypeDes,
		rmsd.CampaignDetailId,
		imu.Abbreviation AS MeasurementUnitAbbreviation,
		qc.UserName AS NameQualityChemical,
		pc.UserName AS NameProductionChemical,
		rmsd.LabelType
FROM MixingStation.RequestPackageDetailStatus rpds 
JOIN MixingStation.RequestMixingStationDetail rmsd ON rpds.RequestMixingStationDetailId = rmsd.Id
JOIN MixingStation.CampaignDetail cd ON cd.Id = rmsd.CampaignDetailId 
JOIN MixingStation.PackagePersonalized pp on rpds.PackagePersonalizedId = pp.Id
JOIN MixingStation.Package p ON p.Id = rpds.PackageId
JOIN MixingStation.UnitDoseType ut ON rmsd.UnitDoseTypeId = ut.Id
JOIN CTE_UserByCampaign qc ON rmsd.CampaignDetailId = qc.CampaignDetailId AND qc.UserRole = 1 
JOIN CTE_UserByCampaign pc ON rmsd.CampaignDetailId = pc.CampaignDetailId AND pc.UserRole = 2
LEFT JOIN Cte_InfoPatient cte_ip ON rmsd.Id = cte_ip.RequestMixingStationDetailId
LEFT JOIN Inventory.InventoryRiskLevel irl ON COALESCE(pp.RiskLevelId, p.RiskLevelId, 0) = irl.Id
LEFT JOIN Cte_NptIndications cte_npti ON rpds.GroupingCodeDose = cte_npti.GroupingCodeDose
LEFT JOIN Inventory.InventoryMeasurementUnit imu ON ISNULL(pp.VolumeTotalOrderMeasurementUnitId, p.VolumeTotalOrderMeasurementUnitId) = imu.Id
WHERE rpds.Status NOT IN (5,6) AND rmsd.Source = 1 AND ut.MSClass = 2 

UNION ALL
-------- Solicitudes de inventario: Estériles, citostáticos y Oncológicos --------
SELECT  
    CAST(CONCAT(rpds.Id, p.Id) AS BIGINT) Id,
    'NO APLICA' AS FunctionalUnitName,
    'NO APLICA' AS PatientName,
    'NO APLICA' AS PatientCode,
    'NO APLICA' AS Bed,
    p.Name AS PackageName,
    CteMainMedicine.ComponentName AS MainMedicines,
    CteMainMedicine.Quantity AS Dose,
    COALESCE(CteVehicle.ComponentName, 'NO APLICA') AS VehicleName,
    CONCAT(COALESCE(NULLIF(p.VolumeTotalOrder, 0), p.VolumeTotalPrepared), ' ', imu.Abbreviation) AS VolumeTotalOrder,
    CASE 
        WHEN udt.MSClass IN (3, 4, 10) THEN ISNULL(p.ConcentrationAntibiotic, '0')
        ELSE CONCAT(ISNULL(p.Concentration, 0), '-', ISNULL(imu2.Abbreviation, imu.Abbreviation))
    END AS Concentration,
    rpds.BatchCode,
    CAST(cd.ProcessingDate AS DATE) AS ElaborationDate,
    CONVERT(VARCHAR(20), rpds.PreparationTime, 108) AS ElaborationHour,
    rpds.BatchExpirationDate AS ExpirationDate,
    ar.Name AS AdministrationRoute,
    'Según protocolo institucional' AS Indications,
    'NO APLICA' AS SpecialIndications,
    CASE p.Storage
        WHEN 1 THEN 'Ambiente: 20 ° C - 25 ° C (Permitida 15 ° C y 30 ° C)'
        WHEN 2 THEN 'Ambiente controlada: 20 ° C - 25 ° C'
        WHEN 3 THEN 'En frío: 8 ° C - 15 ° C'
        WHEN 4 THEN 'Refrigerador: 2 ° C - 8 ° C'
        WHEN 5 THEN 'Congelador: -25 ° C - 10 ° C'
    END AS Storage,
    irl.[Name] AS NameRiskLevel,
    udt.Description AS UnitDoseTypeDes,
    cd.Id AS CampaignDetailId,
    imup.Abbreviation AS MeasurementUnitAbbreviation,
    qc.UserName AS NameQualityChemical,
    pc.UserName AS NameProductionChemical,
    rmsd.LabelType
FROM MixingStation.CampaignDetail cd
INNER JOIN MixingStation.RequestMixingStationDetail rmsd ON cd.Id = rmsd.CampaignDetailId
INNER JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsd.Id = rpds.RequestMixingStationDetailId
INNER JOIN MixingStation.UnitDoseType udt ON rmsd.UnitDoseTypeId = udt.Id
INNER JOIN MixingStation.Package p ON rmsd.PackageId = p.Id
INNER JOIN CTE_InfoDetail CteMainMedicine ON p.Id = CteMainMedicine.PackageId AND CteMainMedicine.MainMedicine = 1
INNER JOIN Inventory.InventoryRiskLevel irl ON irl.Id = p.RiskLevelId
INNER JOIN Inventory.InventoryMeasurementUnit imup ON CteMainMedicine.MeasurementUnitId = imup.Id 
INNER JOIN Inventory.InventoryMeasurementUnit imu ON ISNULL(p.VolumeTotalOrderMeasurementUnitId, MeasurementPreparedId) = imu.Id
INNER JOIN CTE_UserByCampaign qc ON rmsd.CampaignDetailId = qc.CampaignDetailId AND qc.UserRole = 1 
INNER JOIN CTE_UserByCampaign pc ON rmsd.CampaignDetailId = pc.CampaignDetailId AND pc.UserRole = 2
INNER JOIN MixingStation.RequestUnitDoseInventoryDetail rudic ON rmsd.EntityName = 'RequestUnitDoseInventoryDetail' AND rmsd.EntityId = rudic.Id
LEFT JOIN Inventory.AdministrationRoute ar ON rudic.AdministrationRouteId = ar.Id
LEFT JOIN CTE_InfoDetail CteVehicle ON p.Id = CteVehicle.PackageId AND CteVehicle.Vehicle = 1 
LEFT JOIN Inventory.InventoryMeasurementUnit imu2 ON p.ConcentrationMeasurementUnitId = imu2.Id
WHERE rmsd.Source = 4 AND udt.MSClass IN (3, 4, 10) AND p.PreparationType <> 4 

UNION ALL

-- Solicitudes externa Maquila
SELECT 	DISTINCT 
		rpds.Id,
		NULL FunctionalUnitName,
		NULL PatientName,
		NULL PatientCode,
		NULL Bed,
		p.Name PackageName,
		(
			SELECT STRING_AGG(
				CASE pd.ComponentType
					WHEN 1 THEN a.AbbreviationName
					WHEN 2 THEN su.SupplieName
					WHEN 3 THEN ipr.Abbreviation
					ELSE ''
				END
			, ', ') 
			FROM MixingStation.PackageDetail pd
			LEFT JOIN Inventory.InventoryProduct ipr ON pd.ProductId = ipr.Id
			LEFT JOIN Inventory.InventorySupplie su ON pd.SupplieId = su.Id
			LEFT JOIN Inventory.ATC a ON pd.AtcId = a.Id
			WHERE pd.PackageId = p.Id And pd.MainMedicine = 1
		) AS MainMedicines,
		NULL Dose,
		ISNULL((
			SELECT TOP 1 atc.AbbreviationName
			FROM MixingStation.PackageDetail pd
			JOIN Inventory.ATC atc ON pd.AtcId = atc.Id
			WHERE pd.PackageId = p.Id AND pd.Vehicle = 1
		), 'N/A') AS VehicleName,
		CASE 
			WHEN ut.MSClass IN (3, 4, 10) THEN  CONCAT(p.VolumeTotalPrepared,'-', imu3.Abbreviation) 
			ELSE CONCAT(p.VolumeTotalOrder,'-', imu.Abbreviation) 
		END VolumeTotalOrder,
		CASE 
			WHEN ut.MSClass IN (3, 4, 10) THEN p.ConcentrationAntibiotic
			ELSE CONCAT(p.Concentration, '-',ISNULL(imu2.Abbreviation, imu3.Abbreviation))
		END Concentration,
		rpds.BatchCode,
		CAST(cd.ProcessingDate AS DATE) ElaborationDate,
		CONVERT(VARCHAR(20), rpds.PreparationTime, 108)  ElaborationHour,
		rpds.BatchExpirationDate ExpirationDate,
		NULL AdministrationRoute,
		NULL Indications,
		CASE ut.MSClass
			WHEN 3 THEN 'Medicamento estéril en dosis unitaria'
			WHEN 4 THEN 'Medicamento oncológico de manipulación riesgosa'
		END SpecialIndications,
		CASE p.Storage
			WHEN 1 THEN 'Ambiente: 20 ° C - 25 ° C (Permitida 15 ° C y 30 ° C)'
			WHEN 2 THEN 'Ambiente controlada: 20 ° C - 25 ° C'
			WHEN 3 THEN 'En frío: 8 ° C - 15 ° C'
			WHEN 4 THEN 'Refrigerador: 2 ° C - 8 ° C'
			WHEN 5 THEN 'Congelador: -25 ° C - 10 ° C'
		END Storage,
		irl.Name NameRiskLevel,
		ut.Description UnitDoseTypeDes,
		rd.CampaignDetailId,
		NULL MeasurementUnitAbbreviation,
		(SELECT TOP 1 
			vau.Nombre
			FROM MixingStation.CampaignDetailUsers cdu
			JOIN MixingStation.ViewListAuthorizeUsers vau ON vau.UserId = cdu.UserId
			WHERE cdu.CampaignDetailId = rd.CampaignDetailId AND cdu.UserRole = 1) NameQualityChemical,
		(SELECT TOP 1 
			vau.Nombre
			FROM MixingStation.CampaignDetailUsers cdu
			JOIN MixingStation.ViewListAuthorizeUsers vau ON vau.UserId = cdu.UserId
			WHERE cdu.CampaignDetailId = rd.CampaignDetailId AND cdu.UserRole = 2) NameProductionChemical,
		rd.LabelType
FROM MixingStation.RequestPackageDetailStatus rpds 
INNER JOIN MixingStation.Package p ON p.Id = rpds.PackageId
INNER JOIN Inventory.InventoryRiskLevel irl ON irl.Id = p.RiskLevelId
INNER JOIN MixingStation.RequestMixingStationDetail rd ON rpds.RequestMixingStationDetailId = rd.Id
INNER JOIN MixingStation.CampaignDetail as cd ON cd.Id = rd.CampaignDetailId
INNER JOIN MixingStation.UnitDoseType ut ON rd.UnitDoseTypeId = ut.Id
INNER JOIN MixingStation.PackageDetail pkd ON pkd.PackageId = p.Id and pkd.MainMedicine = 1
INNER JOIN Inventory.ATC atc ON pkd.AtcId = atc.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu ON p.VolumeTotalOrderMeasurementUnitId= imu.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu2 ON p.ConcentrationMeasurementUnitId =imu2.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu3 ON p.MeasurementPreparedId = imu3.Id
WHERE rpds.Status NOT IN (5,6) AND rd.Source = 3