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
    LEFT JOIN Inventory.ATC a WITH(NOLOCK) ON pd.AtcId = a.Id
    LEFT JOIN Inventory.InventorySupplie su WITH(NOLOCK) ON pd.SupplieId = su.Id
    LEFT JOIN Inventory.InventoryProduct ipr WITH(NOLOCK) ON pd.ProductId = ipr.Id
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
	FROM MixingStation.RequestMixingStationDetailPatients rmsdp WITH(NOLOCK)
	JOIN INPACIENT inp WITH(NOLOCK) ON rmsdp.PatientCode = inp.IPCODPACI
	JOIN Payroll.FunctionalUnit fu WITH(NOLOCK) ON fu.Code = rmsdp.FunctionalUnitCode
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
	FROM MedicalHistory.PharmaDose pd WITH(NOLOCK)
	JOIN MedicalHistory.ProductSusceptibleMixingStation psms WITH(NOLOCK) ON pd.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation
	JOIN HCFARMEPD hpd WITH(NOLOCK) ON psms.CodeSusceptibleMixingStation = hpd.CodeSusceptibleMixingStation
	JOIN MixingStation.UnitDoseType udt WITH(NOLOCK) ON pd.UnitDoseTypeId = udt.Id 
	JOIN HCNUTPAREC np WITH(NOLOCK) ON hpd.IdSourceTable = np.ID
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
		FROM MixingStation.CampaignDetailUsers cdu WITH(NOLOCK)
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
	JOIN MedicalHistory.PharmaDose pd WITH(NOLOCK)
		ON rpds.GroupingCodeDose = pd.GroupingCodeDose
	LEFT JOIN Inventory.InventoryMeasurementUnit mu WITH(NOLOCK)
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
		a.CODICAMHO Bed,
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
			FROM MixingStation.PackageDetail pd WITH(NOLOCK)
			LEFT JOIN Inventory.InventoryProduct ipr WITH(NOLOCK) ON pd.ProductId = ipr.Id
			LEFT JOIN Inventory.InventorySupplie su WITH(NOLOCK) ON pd.SupplieId = su.Id
			LEFT JOIN Inventory.ATC a WITH(NOLOCK) ON pd.AtcId = a.Id
			WHERE pd.PackageId = p.Id And pd.MainMedicine = 1
		) AS MainMedicines,
		CASE 
			WHEN Msclass IN (3, 4, 10) THEN ISNULL(pd.Dose, 0)
			ELSE ISNULL(dfpd.Doses, 0)
		END AS Dose,
		ISNULL((
			SELECT TOP 1 atc.AbbreviationName
			FROM MixingStation.PackageDetail pd WITH(NOLOCK)
			JOIN Inventory.ATC atc WITH(NOLOCK) ON pd.AtcId = atc.Id
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
FROM MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) 
INNER JOIN MixingStation.PackagePersonalized pp WITH(NOLOCK) on rpds.PackagePersonalizedId = pp.Id
INNER JOIN MixingStation.Package p WITH(NOLOCK) ON p.Id = rpds.PackageId
INNER JOIN Inventory.InventoryRiskLevel irl ON irl.Id = p.RiskLevelId
INNER JOIN MixingStation.RequestMixingStationDetail rd WITH(NOLOCK) ON rpds.RequestMixingStationDetailId = rd.Id
INNER JOIN MixingStation.CampaignDetail as cd WITH(NOLOCK) ON cd.Id = rd.CampaignDetailId
INNER JOIN MixingStation.UnitDoseType ut WITH(NOLOCK) ON rd.UnitDoseTypeId = ut.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) ON pp.VolumeTotalOrderMeasurementUnitId= imu.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu2 WITH(NOLOCK) ON pp.ConcentrationMeasurementUnitId =imu2.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu3 WITH(NOLOCK) ON pp.MeasurementPreparedId = imu3.Id
LEFT JOIN MixingStation.PackageDetail pkd WITH(NOLOCK) ON pkd.PackageId = p.Id and pkd.MainMedicine = 1
LEFT JOIN Inventory.ATC atc WITH(NOLOCK) ON pkd.AtcId = atc.Id
LEFT JOIN MedicalHistory.PharmaDose  pd with(NOLOCK) ON rpds.GroupingCodeDose = pd.GroupingCodeDose and pd.ProductCode =  atc.Code
LEFT JOIN MedicalHistory.ProductSusceptibleMixingStation psms WITH(NOLOCK) ON pd.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation
LEFT JOIN HCFARMEPC hc WITH(NOLOCK) ON hc.CODCONCEC = pd.IDHCFARMEPC
LEFT JOIN Payroll.FunctionalUnit fu WITH(NOLOCK) ON fu.Code = hc.UFUCODIGO
LEFT JOIN INPACIENT it WITH(NOLOCK) ON hc.IPCODPACI=it.IPCODPACI
LEFT JOIN ADINGRESO a WITH(NOLOCK) ON hc.NUMINGRES =a.NUMINGRES
LEFT JOIN Inventory.InventoryMeasurementUnit mu WITH(NOLOCK) ON pd.MeasurementUnitCode = mu.Code
--- Preescripcion de solicitudes instrahospitalarias
LEFT JOIN HCPRESCRA hcp WITH (NOLOCK) ON psms.Origin ='HCPRESCRA' and psms.IdOrigin=hcp.ID
--- Prescripción desde esquemas
LEFT JOIN [EHR].HCORDMEDICAM hcm WITH (NOLOCK) ON psms.Origin ='HCORDMEDICAM' AND psms.IdOrigin = hcm.ID
LEFT JOIN [EHR].SchemesDrugs sd ON sd.SchemesId = hcm.SchemesId AND sd.DrugCode = psms.MainDrugCode
-- Preescripcion desde mezclas y liquidos
LEFT JOIN HCINFLIQA hca WITH(NOLOCK) ON psms.Origin = 'HCINFLIQA' AND hca.CONSECUTI = psms.IdOrigin
LEFT JOIN HCINFLIQD hcd WITH(NOLOCK) ON hcd.CODCONCEC = hca.CODCONCEC
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
FROM MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) 
JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rpds.RequestMixingStationDetailId = rmsd.Id
JOIN MixingStation.CampaignDetail cd WITH(NOLOCK) ON cd.Id = rmsd.CampaignDetailId 
JOIN MixingStation.PackagePersonalized pp WITH(NOLOCK) on rpds.PackagePersonalizedId = pp.Id
JOIN MixingStation.Package p WITH(NOLOCK) ON p.Id = rpds.PackageId
JOIN MixingStation.UnitDoseType ut WITH(NOLOCK) ON rmsd.UnitDoseTypeId = ut.Id
JOIN CTE_UserByCampaign qc ON rmsd.CampaignDetailId = qc.CampaignDetailId AND qc.UserRole = 1 
JOIN CTE_UserByCampaign pc ON rmsd.CampaignDetailId = pc.CampaignDetailId AND pc.UserRole = 2
LEFT JOIN Cte_InfoPatient cte_ip ON rmsd.Id = cte_ip.RequestMixingStationDetailId
LEFT JOIN Inventory.InventoryRiskLevel irl WITH(NOLOCK) ON COALESCE(pp.RiskLevelId, p.RiskLevelId, 0) = irl.Id
LEFT JOIN Cte_NptIndications cte_npti ON rpds.GroupingCodeDose = cte_npti.GroupingCodeDose
LEFT JOIN Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) ON ISNULL(pp.VolumeTotalOrderMeasurementUnitId, p.VolumeTotalOrderMeasurementUnitId) = imu.Id
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
FROM MixingStation.CampaignDetail cd WITH(NOLOCK)
INNER JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON cd.Id = rmsd.CampaignDetailId
INNER JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
INNER JOIN MixingStation.UnitDoseType udt WITH(NOLOCK) ON rmsd.UnitDoseTypeId = udt.Id
INNER JOIN MixingStation.Package p WITH(NOLOCK) ON rmsd.PackageId = p.Id
INNER JOIN CTE_InfoDetail CteMainMedicine ON p.Id = CteMainMedicine.PackageId AND CteMainMedicine.MainMedicine = 1
INNER JOIN Inventory.InventoryRiskLevel irl WITH(NOLOCK) ON irl.Id = p.RiskLevelId
INNER JOIN Inventory.InventoryMeasurementUnit imup WITH(NOLOCK) ON CteMainMedicine.MeasurementUnitId = imup.Id 
INNER JOIN Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) ON ISNULL(p.VolumeTotalOrderMeasurementUnitId, MeasurementPreparedId) = imu.Id
INNER JOIN CTE_UserByCampaign qc ON rmsd.CampaignDetailId = qc.CampaignDetailId AND qc.UserRole = 1 
INNER JOIN CTE_UserByCampaign pc ON rmsd.CampaignDetailId = pc.CampaignDetailId AND pc.UserRole = 2
INNER JOIN MixingStation.RequestUnitDoseInventoryDetail rudic WITH(NOLOCK) ON rmsd.EntityName = 'RequestUnitDoseInventoryDetail' AND rmsd.EntityId = rudic.Id
LEFT JOIN Inventory.AdministrationRoute ar WITH(NOLOCK) ON rudic.AdministrationRouteId = ar.Id
LEFT JOIN CTE_InfoDetail CteVehicle ON p.Id = CteVehicle.PackageId AND CteVehicle.Vehicle = 1 
LEFT JOIN Inventory.InventoryMeasurementUnit imu2 WITH(NOLOCK) ON p.ConcentrationMeasurementUnitId = imu2.Id
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
			FROM MixingStation.PackageDetail pd WITH(NOLOCK)
			LEFT JOIN Inventory.InventoryProduct ipr WITH(NOLOCK) ON pd.ProductId = ipr.Id
			LEFT JOIN Inventory.InventorySupplie su WITH(NOLOCK) ON pd.SupplieId = su.Id
			LEFT JOIN Inventory.ATC a WITH(NOLOCK) ON pd.AtcId = a.Id
			WHERE pd.PackageId = p.Id And pd.MainMedicine = 1
		) AS MainMedicines,
		NULL Dose,
		ISNULL((
			SELECT TOP 1 atc.AbbreviationName
			FROM MixingStation.PackageDetail pd WITH(NOLOCK)
			JOIN Inventory.ATC atc WITH(NOLOCK) ON pd.AtcId = atc.Id
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
FROM MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) 
INNER JOIN MixingStation.Package p WITH(NOLOCK) ON p.Id = rpds.PackageId
INNER JOIN Inventory.InventoryRiskLevel irl ON irl.Id = p.RiskLevelId
INNER JOIN MixingStation.RequestMixingStationDetail rd WITH(NOLOCK) ON rpds.RequestMixingStationDetailId = rd.Id
INNER JOIN MixingStation.CampaignDetail as cd WITH(NOLOCK) ON cd.Id = rd.CampaignDetailId
INNER JOIN MixingStation.UnitDoseType ut WITH(NOLOCK) ON rd.UnitDoseTypeId = ut.Id
INNER JOIN MixingStation.PackageDetail pkd WITH(NOLOCK) ON pkd.PackageId = p.Id and pkd.MainMedicine = 1
INNER JOIN Inventory.ATC atc WITH(NOLOCK) ON pkd.AtcId = atc.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) ON p.VolumeTotalOrderMeasurementUnitId= imu.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu2 WITH(NOLOCK) ON p.ConcentrationMeasurementUnitId =imu2.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imu3 WITH(NOLOCK) ON p.MeasurementPreparedId = imu3.Id
WHERE rpds.Status NOT IN (5,6) AND rd.Source = 3
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida toda la información necesaria para imprimir las etiquetas de preparados magistrales y mezclas generadas en la estación de mezclas (farmacia). Integra datos del paquete o fórmula preparada (medicamento principal, vehículo, concentración, volumen total, almacenamiento, nivel de riesgo), datos del paciente (nombre, cédula o código, cama, unidad funcional), datos del lote de producción (fecha y hora de elaboración, fecha de vencimiento, código de lote), indicaciones de administración y vía de administración (línea central, periférica u otras), y los usuarios responsables del proceso (químico de calidad y químico de producción). Combina los catálogos de medicamentos ATC, insumos y productos del inventario para resolver los nombres abreviados de cada componente, y cruza con la historia clínica para obtener las instrucciones de administración específicas del médico. Se utiliza principalmente en el módulo de farmacia hospitalaria para generar e imprimir las etiquetas identificativas que acompañan cada preparación individualizada por paciente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListPrintLabels';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListPrintLabels';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información necesaria para imprimir las etiquetas de los preparados magistrales (NPT, estériles, citostáticos/oncológicos y maquila) elaborados en la estación de mezclas, unificando datos de paquete, paciente, dosis, vía, almacenamiento y químicos responsables.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPrintLabels';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las solicitudes deben tener un RequestPackageDetailStatus cuyo Status no sea 5 ni 6 (excluye estados anulados/finalizados de impresión); Cada CampaignDetail debe tener usuarios CampaignDetailUsers con UserRole 1 (químico de calidad) y 2 (químico de producción) para los flujos de NPT e inventario; Los paquetes/ítems deben estar asociados a un UnitDoseType cuya MSClass clasifica el tipo de preparación (2=NPT, 3=estéril, 4=oncológico, 10=otros); Para el bloque de inventario, rmsd.EntityName debe ser ''RequestUnitDoseInventoryDetail'' y existir el detalle correspondiente', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPrintLabels';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListPrintLabels: Devuelve etiquetas de preparados intrahospitalarios no NPT cuando rd.Source=1, ut.MSClass<>2, status NOT IN (5,6) y PreparationType<>4; [RETURN_RESULT] MixingStation.ViewListPrintLabels: Devuelve etiquetas de NPT cuando rmsd.Source=1, ut.MSClass=2 y status NOT IN (5,6), usando indicaciones desde HCNUTPAREC; [RETURN_RESULT] MixingStation.ViewListPrintLabels: Devuelve etiquetas de inventario (estériles/citostáticos/oncológicos) cuando rmsd.Source=4, udt.MSClass IN (3,4,10) y p.PreparationType<>4, con datos de paciente fijados a ''NO APLICA''; [RETURN_RESULT] MixingStation.ViewListPrintLabels: Devuelve etiquetas de maquila externa cuando rd.Source=3 y status NOT IN (5,6), con campos de paciente, dosis, vía e indicaciones en NULL', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPrintLabels';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rd.Source = 1 AND ut.MSClass <> 2 AND ISNULL(pp.PreparationType, p.PreparationType) <> 4 → Bloque de solicitudes intrahospitalarias no-NPT: arma etiqueta con datos de paciente desde HCFARMEPC/INPACIENT/ADINGRESO y vía desde HCVIAADMI según origen (HCPRESCRA, HCORDMEDICAM, HCINFLIQA); si rd.Source = 1 AND ut.MSClass = 2 → Bloque NPT: toma indicaciones (administración, adicional, vía central/periférica) desde HCNUTPAREC vía CTE_NptIndications; si rmsd.Source = 4 AND udt.MSClass IN (3,4,10) AND p.PreparationType <> 4 → Bloque de inventario: usa CTE_InfoDetail para medicamento principal y vehículo, fija paciente/cama/UF como ''NO APLICA'' e Indications=''Según protocolo institucional''; si rd.Source = 3 → Bloque maquila externa: anula campos de paciente y dosis, conserva concentración y volumen del paquete; si ut.MSClass IN (3,4,10) → Usa VolumeTotalPrepared y ConcentrationAntibiotic del PackagePersonalized/Package; en otros casos usa VolumeTotalOrder y Concentration con su unidad; si ut.MSClass IN (4,10) → Toma instrucciones desde EHR.HCORDMEDICAM.INSTRUADMINIS (citostático/estéril); de lo contrario toma HCPRESCRA.DESADMINI else Indications = HCPRESCRA.DESADMINI; si MsClass IN (3,4,10) → Dose = ISNULL(pd.Dose,0) (dosis individual de PharmaDose) else Dose = suma de pd.Dose agrupada por RequestPackageDetailStatusId (CTE_DosesFromPharmaDose); si ut.MSClass = 3 → SpecialIndications = ''Medicamento estéril en dosis unitaria''; si ut.MSClass = 4 → SpecialIndications = ''Medicamento oncológico de manipulación riesgosa''; si p.Storage IN (1..5) → Traduce el código a texto de condiciones de almacenamiento (ambiente, ambiente controlado, frío, refrigerador, congelador); si psms.Origin = ''HCPRESCRA'' / ''HCORDMEDICAM'' / ''HCINFLIQA'' → Resuelve la vía de administración consultando HCPRESCRA, EHR.SchemesDrugs (vía esquema) o HCINFLIQD respectivamente, usando COALESCE sobre HCVIAADMI; si cdu.UserRole = 1 → El usuario se reporta como NameQualityChemical (químico de calidad); si cdu.UserRole = 2 → El usuario se reporta como NameProductionChemical (químico de producción); si pd.MainMedicine = 1 → Componente se concatena como MainMedicines de la etiqueta; si pd.Vehicle = 1 → Componente se reporta como VehicleName (con TOP 1 ATC); si no existe se imprime ''N/A''', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPrintLabels';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPrintLabels';
GO
