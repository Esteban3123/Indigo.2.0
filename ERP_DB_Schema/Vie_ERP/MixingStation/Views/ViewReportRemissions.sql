CREATE VIEW [MixingStation].[ViewReportRemissions] 
AS
WITH temp_Medical as (
	SELECT	ip.Id,
			ip.Name,
			ip.Abbreviation ShortName,
			3 type
	FROM  Inventory.InventoryProduct ip
	---------------------
	UNION ALL
	SELECT	atc.Id,
			atc.Name,
			atc.AbbreviationName ShortName,
			1 type 
	FROM Inventory.ATC atc
	---------------------
	UNION ALL
	SELECT	iss.Id,
			iss.SupplieName Name,
			iss.SupplieName ShortName,
			2 type
	FROM Inventory.InventorySupplie iss
),
--====================================================================================================
temp_Package AS (	
		
	SELECT	pd.Id,
			pd.PackageId PackageId,
			p.storage,
			pd.MainMedicine,
			pd.Quantity,
			pd.MeasurementUnitId,
			pd.Volume,
			pd.VolumeMeasureUnit,
			pd.Thinner,
			pd.Vehicle,
			pd.AtcId,
			pd.SupplieId,
			pd.ProductId,
			pd.ComponentType,
			p.VolumeTotalPrepared,
			p.VolumeTotalOrder,
			'S' Type, 
			p.PreparationType
	FROM MixingStation.PackageDetail pd
	JOIN MixingStation.Package p ON p.Id = pd.PackageId
						
	UNION ALL
						
	SELECT	ppd.Id,
			ppd.PackagePersonalizedId PackageId,
			pp.storage,
			ppd.MainMedicine,
			ppd.Quantity,
			ppd.MeasurementUnitId,
			ppd.Volume,
			ppd.VolumeMeasureUnit,
			ppd.Thinner,
			ppd.Vehicle,
			ppd.AtcId,
			ppd.SupplieId,
			ppd.ProductId,
			ppd.ComponentType,
			pp.VolumeTotalPrepared,
			pp.VolumeTotalOrder,
			'P' Type,
			pp.PreparationType
	FROM MixingStation.PackagePersonalizedDetail ppd
	JOIN MixingStation.PackagePersonalized pp ON pp.Id = ppd.PackagePersonalizedId
),
--====================================================================================================
Cte_mainDataInternal AS (
	SELECT 
		pd.GroupingCodeDose,
		psms.CodeSusceptibleMixingStation,
		psms.FunctionalUnitCode CodeFunctionalUnit,
		psms.FullProductName,
		psms.MainDrugCode,
		fu.UFUDESCRI FunctionalUnitName,
		hd.IPCODPACI PatientCode,
		hd.NUMINGRES AdmissionNumber,
		hd.SourceTable,
		hd.IdSourceTable,
		mun.MUNNOMBRE MunicipalityName,
		ca.NOMCENATE CareCenterName,
		udt.MSClass,
		pd.UnitDoseTypeId,
		pd.ProductCode,
		pd.Dose,
		pd.MeasurementUnitCode
	FROM MedicalHistory.ProductSusceptibleMixingStation psms
	JOIN MedicalHistory.PharmaDose pd ON psms.CodeSusceptibleMixingStation = pd.CodeSusceptibleMixingStation
	JOIN MixingStation.UnitDoseType udt ON pd.UnitDoseTypeId = udt.Id
	JOIN HCFARMEPD hd ON psms.CodeSusceptibleMixingStation = hd.CodeSusceptibleMixingStation AND hd.SENDTO = 2
	JOIN INUNIFUNC fu ON psms.FunctionalUnitCode = fu.UFUCODIGO
	JOIN ADCENATEN ca ON psms.CenterAttentionCode = ca.CODCENATE
	JOIN INMUNICIP mun ON ca.DEPMUNCOD = mun.DEPMUNCOD
),
Cte_MainDataOthers AS (
	SELECT DISTINCT cte_mdi.GroupingCodeDose,
			cte_mdi.CodeSusceptibleMixingStation,
			cte_mdi.CodeFunctionalUnit,
			cte_mdi.FullProductName,
			cte_mdi.FunctionalUnitName,
			cte_mdi.PatientCode,
			cte_mdi.AdmissionNumber,
			cte_mdi.SourceTable,
			cte_mdi.IdSourceTable,
			cte_mdi.MunicipalityName,
			cte_mdi.CareCenterName,
			cte_mdi.MSClass,
			cte_mdi.UnitDoseTypeId,
			cte_mdi.ProductCode,
			cte_mdi.Dose,
			cte_mdi.MeasurementUnitCode,
			a.Id AtcId
	FROM Cte_mainDataInternal cte_mdi
	JOIN Inventory.ATC a ON cte_mdi.ProductCode = a.Code
	WHERE cte_mdi.MSClass <> 2 AND cte_mdi.MainDrugCode = cte_mdi.ProductCode
),
Cte_MainDataNPT AS(
	SELECT 
			cte_mdi.GroupingCodeDose,
			cte_mdi.CodeSusceptibleMixingStation,
			cte_mdi.CodeFunctionalUnit,
			cte_mdi.FullProductName,
			cte_mdi.FunctionalUnitName,
			cte_mdi.PatientCode,
			cte_mdi.AdmissionNumber,
			cte_mdi.SourceTable,
			cte_mdi.IdSourceTable,
			cte_mdi.MunicipalityName,
			cte_mdi.CareCenterName,
			cte_mdi.MSClass,
			cte_mdi.UnitDoseTypeId,
			cte_mdi.MainDrugCode ProductCode,
			n.VOLUTOTAL Dose,
			'mL' MeasurementUnitCode,
			a.Id AtcId
	FROM Cte_mainDataInternal cte_mdi
	JOIN HCNUTPAREC n ON cte_mdi.SourceTable = 'HCNUTPAREC' AND cte_mdi.IdSourceTable = n.ID
	JOIN HCPARNUTC nc ON n.IDHCPARNUTC = nc.Id
	JOIN Inventory.ATC a ON cte_mdi.MainDrugCode = a.Code
	LEFT JOIN Inventory.InventoryMeasurementUnit imu ON COALESCE(a.VolumeMeasureUnit, a.WeightMeasureUnit) = imu.Id
	WHERE cte_mdi.MSClass = 2 AND cte_mdi.MainDrugCode = nc.FinishedProductCode
	GROUP BY cte_mdi.GroupingCodeDose,
			cte_mdi.CodeSusceptibleMixingStation,
			cte_mdi.CodeFunctionalUnit,
			cte_mdi.FullProductName,
			cte_mdi.FunctionalUnitName,
			cte_mdi.PatientCode,
			cte_mdi.AdmissionNumber,
			cte_mdi.SourceTable,
			cte_mdi.IdSourceTable,
			cte_mdi.MunicipalityName,
			cte_mdi.CareCenterName,
			cte_mdi.MSClass,
			cte_mdi.UnitDoseTypeId,
			cte_mdi.MainDrugCode,
			n.VOLUTOTAL,
			a.Id
),
--Suma cantidades como dosis para Paquetes oncólogicos intratecales)
Cte_PackageQuantitySum AS ( 
	SELECT 
		ppd.PackagePersonalizedId AS PackageId,
		'P' AS PackageType,
		SUM(ppd.Quantity) AS TotalQuantity,
		MAX(ppd.MeasurementUnitId) AS MeasurementUnitId
	FROM MixingStation.PackagePersonalizedDetail ppd
	WHERE ppd.MainMedicine = 1
		AND ppd.Quantity IS NOT NULL
	GROUP BY ppd.PackagePersonalizedId
	
	UNION ALL
	
	SELECT 
		pd.PackageId,
		'S' AS PackageType,
		SUM(pd.Quantity) AS TotalQuantity,
		MAX(pd.MeasurementUnitId) AS MeasurementUnitId
	FROM MixingStation.PackageDetail pd
	WHERE pd.MainMedicine = 1
		AND pd.Quantity IS NOT NULL
	GROUP BY pd.PackageId
)
--================================
SELECT  
		rpds.Id,
		cmc.Name MixingStationName, 
		cd.ProcessingDate, 
		mdiO.FunctionalUnitName,
		Concat(LTRIM(RTRIM(pac.IPCODPACI)), ' - ', LTRIM(RTRIM(pac.IPNOMCOMP))) AS PatientCodeName,
		rmsdp.Bed
		------------------------------------------------------------------------------
		, CASE 
			WHEN udt.MSClass IN (9) THEN CONCAT(p2.Code, ' - ', p2.Name)  -- Magistrales
			ELSE med1.ShortName
		END AS ProductName, 
		CASE 
		    WHEN udt.MSClass = 9 
				THEN mdiO.FullProductName 
			WHEN udt.MSClass = 4 AND p.PreparationType = 4 -- (Oncológicos Intratecales)
				THEN CONCAT(COALESCE(pqsP.TotalQuantity, pqsS.TotalQuantity, 0), ' ', COALESCE(imuSum.Abbreviation, imu.Abbreviation))
			ELSE CONCAT(ISNULL(pp.Quantity, p.Quantity),' ',imu.Abbreviation)
		END ProductConcentration,
		med2.ShortName VehicleName,
		CASE 
			WHEN udt.MSClass = 4 AND p.PreparationType = 4 -- (Oncológicos Intratecales)
				THEN CONCAT(p.VolumeTotalPrepared, ' ', imu.Abbreviation)
			WHEN udt.MSClass IN (3, 10, 4)  -- 3 - Antibioticoterapia, 10 - Otros estériles , 4 - oncologico -- Se usa el total del preparado
				THEN CONCAT(pp1.VolumeTotalPrepared, ' ', imu1.Abbreviation)  
			ELSE 
				CONCAT(pp1.VolumeTotalOrder, ' - ', imu1.Abbreviation) -- total de la mezcla
		END AS VehicleConcentration,
		--------------------------------------------------------------------------------
		rpds.BatchCode,
		1 Quantity,
		p.Storage,
		rmsd.CampaignDetailId,
		mdiO.CodeFunctionalUnit,
		------------------------------------------------------------------------------
		mdiO.MunicipalityName,
		cd.CampaignNumber,
		ps.Code as ProductionScheduleCode,
		mdiO.CareCenterName  CenterAttention, 
		udt.Description AS UnitDoseTypeName,
		udt.MSClass AS UnitDoseTypeMsClass,
		rpds.GroupingCodeDose
FROM MixingStation.RequestMixingStationDetail rmsd
JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.UnitDoseType udt ON rmsd.UnitDoseTypeId = udt.Id
JOIN MixingStation.CampaignDetail cd ON rmsd.CampaignDetailId = cd.Id
JOIN MixingStation.Campaign c ON cd.CampaignId = c.Id
JOIN MixingStation.CMConfiguration cmc ON c.CMConfigurationId = cmc.Id
JOIN Cte_MainDataOthers mdiO ON rpds.GroupingCodeDose = mdiO.GroupingCodeDose 
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON rmsd.Id = rmsdp.RequestMixingStationDetailId
JOIN INPACIENT pac ON mdiO.PatientCode = pac.IPCODPACI
JOIN MixingStation.Package p2 ON p2.Id = rmsd.PackageId
JOIN temp_Package p ON rpds.PackageId = p.PackageId and p.Type ='S' AND p.MainMedicine = 1 AND p.AtcId = mdiO.AtcId
LEFT JOIN temp_Package pp ON  rpds.PackagePersonalizedId = pp.PackageId AND pp.Type ='P' AND pp.MainMedicine=1 AND mdiO.AtcId = pp.AtcId AND pp.Quantity = mdiO.Dose
LEFT JOIN temp_Package p1 ON rpds.PackageId = p1.PackageId and p1.Type ='S' AND p1.Vehicle = 1
LEFT JOIN temp_Package pp1 ON rpds.PackagePersonalizedId = pp1.PackageId and pp1.Type ='P' AND pp1.Vehicle = 1 
LEFT JOIN temp_Medical med1 ON med1.type= ISNULL(pp.ComponentType, p.ComponentType) AND med1.Id = COALESCE(pp.ProductId, p.ProductId, pp.AtcId, p.AtcId, pp.SupplieId, p.SupplieId)
LEFT JOIN temp_Medical med2 ON med2.type= ISNULL(pp1.ComponentType, p1.ComponentType) AND med2.Id = COALESCE(pp1.ProductId, p1.ProductId , pp1.AtcId, p1.AtcId, pp1.SupplieId, p1.SupplieId)
LEFT JOIN Inventory.InventoryMeasurementUnit imu ON imu.Id = COALESCE(pp.MeasurementUnitId, p.MeasurementUnitId, pp.VolumeMeasureUnit, p.VolumeMeasureUnit)
LEFT JOIN Inventory.InventoryMeasurementUnit imu1 ON imu1.Id = COALESCE(pp1.MeasurementUnitId, p1.MeasurementUnitId, pp1.VolumeMeasureUnit, p1.VolumeMeasureUnit)
LEFT JOIN Cte_PackageQuantitySum pqsP ON pqsP.PackageId = rpds.PackagePersonalizedId AND pqsP.PackageType = 'P'
LEFT JOIN Cte_PackageQuantitySum pqsS ON pqsS.PackageId = rpds.PackageId AND pqsS.PackageType = 'S' AND rpds.PackagePersonalizedId IS NULL
LEFT JOIN Inventory.InventoryMeasurementUnit imuSum ON imuSum.Id = COALESCE(pqsP.MeasurementUnitId, pqsS.MeasurementUnitId)
LEFT JOIN MixingStation.ProductionScheduleDetail psd (NOLOCK) ON cd.Id = psd.CampaignDetailId
LEFT JOIN MixingStation.ProductionSchedule ps (NOLOCK) ON psd.ProductionScheduleId = ps.Id
where udt.MSClass <> 2

UNION ALL

SELECT 
		rpds.Id,
		cmc.Name MixingStationName, 
		cd.ProcessingDate, 
		mdiN.FunctionalUnitName,
		Concat(LTRIM(RTRIM(pac.IPCODPACI)), ' - ', LTRIM(RTRIM(pac.IPNOMCOMP))) AS PatientCodeName,
		rmsdp.Bed,
		------------------------------------------------------------------------------
		COALESCE(pp.Name, p.Name, '') ProductName, 
		CONCAT(mdiN.Dose, ' ', mdiN.MeasurementUnitCode) ProductConcentration,
		'' VehicleName,
		CONCAT(mdiN.Dose, ' ', mdiN.MeasurementUnitCode) VehicleConcentration,
		--------------------------------------------------------------------------------
		rpds.BatchCode,
		1 Quantity,
		p.Storage,
		rmsd.CampaignDetailId,
		mdiN.CodeFunctionalUnit,
		------------------------------------------------------------------------------
		mdiN.MunicipalityName,
		cd.CampaignNumber,
		ps.Code as ProductionScheduleCode,
		mdiN.CareCenterName  CenterAttention, 
		udt.Description AS UnitDoseTypeName,
		udt.MSClass AS UnitDoseTypeMsClass,
		rpds.GroupingCodeDose
FROM MixingStation.RequestMixingStationDetail rmsd
JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.UnitDoseType udt ON rmsd.UnitDoseTypeId = udt.Id
JOIN MixingStation.CampaignDetail cd ON rmsd.CampaignDetailId = cd.Id
JOIN MixingStation.Campaign c ON cd.CampaignId = c.Id
JOIN MixingStation.CMConfiguration cmc ON c.CMConfigurationId = cmc.Id
JOIN Cte_MainDataNPT mdiN ON rpds.GroupingCodeDose = mdiN.GroupingCodeDose 
JOIN MixingStation.Package p ON rmsd.PackageId = p.Id
JOIN INPACIENT pac ON mdiN.PatientCode = pac.IPCODPACI
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON rmsd.Id = rmsdp.RequestMixingStationDetailId
------
LEFT JOIN MixingStation.PackagePersonalized pp ON rmsd.PackagePersonalizedId = pp.Id
LEFT JOIN MixingStation.ProductionScheduleDetail psd (NOLOCK) ON cd.Id = psd.CampaignDetailId
LEFT JOIN MixingStation.ProductionSchedule ps (NOLOCK) ON psd.ProductionScheduleId = ps.Id
JOIN MixingStation.UnitDoseType udt1 ON cd.UnitDoseTypeId = udt1.Id
WHERE udt1.MSClass = 2