
CREATE VIEW [MixingStation].[ViewReportRemissions] 
AS
WITH temp_Medical as (
	SELECT	ip.Id,
			ip.Name,
			ip.Abbreviation ShortName,
			3 type
	FROM  Inventory.InventoryProduct ip WITH(NOLOCK)
	---------------------
	UNION ALL
	SELECT	atc.Id,
			atc.Name,
			atc.AbbreviationName ShortName,
			1 type 
	FROM Inventory.ATC atc WITH(NOLOCK)
	---------------------
	UNION ALL
	SELECT	iss.Id,
			iss.SupplieName Name,
			iss.SupplieName ShortName,
			2 type
	FROM Inventory.InventorySupplie iss WITH(NOLOCK)
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
	FROM MixingStation.PackageDetail pd WITH(NOLOCK)
	JOIN MixingStation.Package p WITH(NOLOCK) ON p.Id = pd.PackageId
						
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
	FROM MixingStation.PackagePersonalizedDetail ppd WITH(NOLOCK)
	JOIN MixingStation.PackagePersonalized pp WITH(NOLOCK) ON pp.Id = ppd.PackagePersonalizedId
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
	FROM MedicalHistory.ProductSusceptibleMixingStation psms WITH(NOLOCK)
	JOIN MedicalHistory.PharmaDose pd WITH(NOLOCK) ON psms.CodeSusceptibleMixingStation = pd.CodeSusceptibleMixingStation
	JOIN MixingStation.UnitDoseType udt WITH(NOLOCK) ON pd.UnitDoseTypeId = udt.Id
	JOIN HCFARMEPD hd WITH(NOLOCK) ON psms.CodeSusceptibleMixingStation = hd.CodeSusceptibleMixingStation AND hd.SENDTO = 2
	JOIN INUNIFUNC fu WITH(NOLOCK) ON psms.FunctionalUnitCode = fu.UFUCODIGO
	JOIN ADCENATEN ca WITH(NOLOCK) ON psms.CenterAttentionCode = ca.CODCENATE
	JOIN INMUNICIP mun WITH(NOLOCK) ON ca.DEPMUNCOD = mun.DEPMUNCOD
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
	JOIN HCNUTPAREC n WITH (NOLOCK) ON cte_mdi.SourceTable = 'HCNUTPAREC' AND cte_mdi.IdSourceTable = n.ID
	JOIN HCPARNUTC nc WITH (NOLOCK) ON n.IDHCPARNUTC = nc.Id
	JOIN Inventory.ATC a WITH(NOLOCK) ON cte_mdi.MainDrugCode = a.Code
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
	FROM MixingStation.PackagePersonalizedDetail ppd WITH(NOLOCK)
	WHERE ppd.MainMedicine = 1
		AND ppd.Quantity IS NOT NULL
	GROUP BY ppd.PackagePersonalizedId
	
	UNION ALL
	
	SELECT 
		pd.PackageId,
		'S' AS PackageType,
		SUM(pd.Quantity) AS TotalQuantity,
		MAX(pd.MeasurementUnitId) AS MeasurementUnitId
	FROM MixingStation.PackageDetail pd WITH(NOLOCK)
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
FROM MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK)
JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.UnitDoseType udt	WITH(NOLOCK) ON rmsd.UnitDoseTypeId = udt.Id
JOIN MixingStation.CampaignDetail cd WITH(NOLOCK) ON rmsd.CampaignDetailId = cd.Id
JOIN MixingStation.Campaign c WITH(NOLOCK) ON cd.CampaignId = c.Id
JOIN MixingStation.CMConfiguration cmc WITH(NOLOCK) ON c.CMConfigurationId = cmc.Id
JOIN Cte_MainDataOthers mdiO ON rpds.GroupingCodeDose = mdiO.GroupingCodeDose 
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON rmsd.Id = rmsdp.RequestMixingStationDetailId
JOIN INPACIENT pac WITH(NOLOCK) ON mdiO.PatientCode = pac.IPCODPACI
JOIN MixingStation.Package p2 ON p2.Id = rmsd.PackageId
JOIN temp_Package p ON rpds.PackageId = p.PackageId and p.Type ='S' AND p.MainMedicine = 1 AND p.AtcId = mdiO.AtcId
LEFT JOIN temp_Package pp ON  rpds.PackagePersonalizedId = pp.PackageId AND pp.Type ='P' AND pp.MainMedicine=1 AND mdiO.AtcId = pp.AtcId AND pp.Quantity = mdiO.Dose
LEFT JOIN temp_Package p1 ON rpds.PackageId = p1.PackageId and p1.Type ='S' AND p1.Vehicle = 1
LEFT JOIN temp_Package pp1 ON rpds.PackagePersonalizedId = pp1.PackageId and pp1.Type ='P' AND pp1.Vehicle = 1 
LEFT JOIN temp_Medical med1 ON med1.type= ISNULL(pp.ComponentType, p.ComponentType) AND med1.Id = COALESCE(pp.ProductId, p.ProductId, pp.AtcId, p.AtcId, pp.SupplieId, p.SupplieId)
LEFT JOIN temp_Medical med2 ON med2.type= ISNULL(pp1.ComponentType, p1.ComponentType) AND med2.Id = COALESCE(pp1.ProductId, p1.ProductId , pp1.AtcId, p1.AtcId, pp1.SupplieId, p1.SupplieId)
LEFT JOIN Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) ON imu.Id = COALESCE(pp.MeasurementUnitId, p.MeasurementUnitId, pp.VolumeMeasureUnit, p.VolumeMeasureUnit)
LEFT JOIN Inventory.InventoryMeasurementUnit imu1 WITH(NOLOCK) ON imu1.Id = COALESCE(pp1.MeasurementUnitId, p1.MeasurementUnitId, pp1.VolumeMeasureUnit, p1.VolumeMeasureUnit)
LEFT JOIN Cte_PackageQuantitySum pqsP ON pqsP.PackageId = rpds.PackagePersonalizedId AND pqsP.PackageType = 'P'
LEFT JOIN Cte_PackageQuantitySum pqsS ON pqsS.PackageId = rpds.PackageId AND pqsS.PackageType = 'S' AND rpds.PackagePersonalizedId IS NULL
LEFT JOIN Inventory.InventoryMeasurementUnit imuSum WITH(NOLOCK) ON imuSum.Id = COALESCE(pqsP.MeasurementUnitId, pqsS.MeasurementUnitId)
LEFT JOIN MixingStation.ProductionScheduleDetail psd (NOLOCK) ON cd.Id = psd.CampaignDetailId
LEFT JOIN MixingStation.ProductionSchedule ps (NOLOCK) ON psd.ProductionScheduleId = ps.Id
where udt.MSClass <> 2
	AND ISNULL(rmsd.Status, 0) <> 3
	AND rmsdp.Status <> 3
	AND rpds.Status <> 6

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
FROM MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK)
JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.UnitDoseType udt	WITH(NOLOCK) ON rmsd.UnitDoseTypeId = udt.Id
JOIN MixingStation.CampaignDetail cd WITH(NOLOCK) ON rmsd.CampaignDetailId = cd.Id
JOIN MixingStation.Campaign c WITH(NOLOCK) ON cd.CampaignId = c.Id
JOIN MixingStation.CMConfiguration cmc WITH(NOLOCK) ON c.CMConfigurationId = cmc.Id
JOIN Cte_MainDataNPT mdiN ON rpds.GroupingCodeDose = mdiN.GroupingCodeDose 
JOIN MixingStation.Package p ON rmsd.PackageId = p.Id
JOIN INPACIENT pac WITH(NOLOCK) ON mdiN.PatientCode = pac.IPCODPACI
JOIN MixingStation.RequestMixingStationDetailPatients rmsdp ON rmsd.Id = rmsdp.RequestMixingStationDetailId
------
LEFT JOIN MixingStation.PackagePersonalized pp ON rmsd.PackagePersonalizedId = pp.Id
LEFT JOIN MixingStation.ProductionScheduleDetail psd (NOLOCK) ON cd.Id = psd.CampaignDetailId
LEFT JOIN MixingStation.ProductionSchedule ps (NOLOCK) ON psd.ProductionScheduleId = ps.Id
JOIN MixingStation.UnitDoseType udt1 ON cd.UnitDoseTypeId = udt1.Id
WHERE udt1.MSClass = 2
	AND ISNULL(rmsd.Status, 0) <> 3
	AND rmsdp.Status <> 3
	AND rpds.Status <> 6
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de remisiones de la estación de mezclas (farmacia oncológica/preparaciones especiales). Integra y consolida la información de preparaciones estándar y personalizadas (paquetes oncológicos, mezclas intratecales, nutrición parenteral total) con los datos del paciente, su número de ingreso, cédula, unidad funcional, centro de atención y municipio. Combina el catálogo de medicamentos ATC, productos del inventario e insumos médicos para identificar y describir los componentes de cada preparación (medicamento principal, diluyente, vehículo, cantidad, volumen y unidad de medida). Sirve como fuente principal para los reportes de remisiones y trazabilidad de preparaciones farmacéuticas especiales, permitiendo consultar qué se preparó, para qué paciente, en qué servicio y bajo qué tipo de preparación (estándar, personalizada, NPT, magistral, etc.).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewReportRemissions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewReportRemissions';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en MedicalHistory.ProductSusceptibleMixingStation enlazados con PharmaDose y HCFARMEPD (campo SENDTO=2).; El producto principal (MainDrugCode) debe existir en Inventory.ATC por Code.; Las unidades funcionales, centros de atención y municipios deben estar parametrizados en INUNIFUNC, ADCENATEN e INMUNICIP.; Las solicitudes (RequestMixingStationDetail) deben tener al menos un paquete (Package) asociado y un estado en RequestPackageDetailStatus.; Para NPT (MSClass=2) debe existir el registro padre en HCNUTPAREC con su nutrición parenteral en HCPARNUTC y FinishedProductCode coincidente con MainDrugCode.; Los pacientes deben existir en INPACIENT con código IPCODPACI.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReportRemissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La rama principal excluye explícitamente NPT (udt.MSClass <> 2) y la rama NPT solo incluye MSClass = 2; En el join con temp_Package siempre se exige MainMedicine=1 para el producto principal y Vehicle=1 para el vehículo; Solo se reportan productos susceptibles de mezcla cuyo destino sea la central de mezclas (HCFARMEPD.SENDTO = 2); Se excluyen solicitudes anuladas (RequestMixingStationDetail.Status = 3), pacientes anulados (RequestMixingStationDetailPatients.Status = 3) y paquetes anulados (RequestPackageDetailStatus.Status = 6); Para NPT la unidad de medida de dosis siempre se reporta como ''mL''; Cuando hay PackagePersonalized, sus valores tienen prioridad sobre los del Package estándar (vía COALESCE/ISNULL); La cantidad reportada (Quantity) siempre es 1 por registro de remisión; Para no-NPT, el medicamento principal se identifica por coincidencia de AtcId con el ATC del MainDrugCode', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReportRemissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas (Mixing Station); Paciente; Cama / Unidad funcional; Centro de atención; Municipio; Campaña de preparación / lote; Dosis unitaria (Unit Dose); Mezcla magistral; Antibioticoterapia; Oncológico / Quimioterapia intratecal; Nutrición parenteral total (NPT); Otros estériles; Vehículo / diluyente; Medicamento principal (MainMedicine); Clasificación ATC; Insumo médico; Programación de producción; Lote / BatchCode; Historia clínica farmacéutica (HCFARMEPD); Producto susceptible de mezcla', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReportRemissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si udt.MSClass <> 2 (no es nutrición parenteral / NPT) → Construye la fila desde Cte_MainDataOthers usando Package/PackagePersonalized y aplica reglas de ProductName y concentraciones según MSClass (9=Magistral, 4+PreparationType=4=Oncológicos Intratecales, 3/10/4=usa volumen preparado) else Se evalúa la rama UNION ALL para MSClass=2 (NPT); si udt1.MSClass = 2 (NPT) → Construye la fila desde Cte_MainDataNPT con dosis tomada de HCNUTPAREC.VOLUTOTAL y unidad ''mL'', usando MainDrugCode como ProductCode; si udt.MSClass = 9 (Magistrales) → ProductName = CONCAT(Package.Code,'' - '',Package.Name) y ProductConcentration = FullProductName de ProductSusceptibleMixingStation else ProductName = abreviatura del producto/ATC/insumo según ComponentType; si udt.MSClass = 4 AND p.PreparationType = 4 (Oncológicos Intratecales) → ProductConcentration se calcula sumando Quantity de los detalles con MainMedicine=1 (Cte_PackageQuantitySum) y VehicleConcentration usa VolumeTotalPrepared del paquete; si udt.MSClass IN (3, 10, 4) (Antibioticoterapia, Otros estériles, Oncológico) → VehicleConcentration = VolumeTotalPrepared (volumen total preparado de la mezcla) else VehicleConcentration = VolumeTotalOrder (volumen total de la orden); si cte_mdi.MSClass <> 2 AND MainDrugCode = ProductCode (en Cte_MainDataOthers) → Se considera el producto como medicamento principal de la mezcla y se enlaza a Inventory.ATC por Code; si cte_mdi.MSClass = 2 AND MainDrugCode = nc.FinishedProductCode (en Cte_MainDataNPT) → Se trata de NPT y se vincula con HCNUTPAREC/HCPARNUTC para obtener el volumen total como dosis', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReportRemissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.ATC; Inventory.InventorySupplie; MixingStation.PackageDetail; MixingStation.Package; MixingStation.PackagePersonalizedDetail; MixingStation.PackagePersonalized; MedicalHistory.ProductSusceptibleMixingStation; MedicalHistory.PharmaDose; MixingStation.UnitDoseType; HCFARMEPD; INUNIFUNC; ADCENATEN; INMUNICIP; HCNUTPAREC; HCPARNUTC; Inventory.InventoryMeasurementUnit; MixingStation.RequestMixingStationDetail; MixingStation.RequestPackageDetailStatus; MixingStation.CampaignDetail; MixingStation.Campaign; MixingStation.CMConfiguration; MixingStation.RequestMixingStationDetailPatients; INPACIENT; MixingStation.ProductionScheduleDetail; MixingStation.ProductionSchedule', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReportRemissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReportRemissions';
GO
