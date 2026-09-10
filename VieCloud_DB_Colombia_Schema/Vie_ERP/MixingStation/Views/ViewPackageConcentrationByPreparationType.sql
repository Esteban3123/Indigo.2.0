CREATE VIEW  [MixingStation].[ViewPackageConcentrationByPreparationType]
AS
WITH Cte_DetailByStandarPackage AS (
	SELECT 	pd.PackageId,
			pd.MainMedicine,
			pd.Thinner,
			pd.Vehicle,
			ipr.Id ProductId,
			a.Id AtcId,
			a.Code AtcCode,
			a.Name AtcName,
			pd.AmountTime Stability,
			pd.TimeUnit,
			pd.Concentration,
			imu.Abbreviation MeasurementUnitAbbreviation,
			imu.Id MeasurementUnitId,
			pd.Quantity
	FROM MixingStation.PackageDetail pd WITH(NOLOCK)
	JOIN Inventory.ATC a WITH(NOLOCK) ON pd.AtcId = a.Id
	JOIN Inventory.InventoryProduct ipr WITH(NOLOCK) ON a.Id = ipr.ATCId
	JOIN Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) ON ipr.MeasurementUnitId = imu.Id
) ,

Cte_DetailByPackagePersonalized AS (
	SELECT 	ppd.PackagePersonalizedId PackageId,
			ppd.MainMedicine,
			ppd.Thinner,
			ppd.Vehicle,
			ipr.Id ProductId,
			a.Id AtcId,
			a.Code AtcCode,
			a.Name AtcName,
			ppd.AmountTime Stability,
			ppd.TimeUnit,
			ppd.Concentration,
			imu.Abbreviation MeasurementUnitAbbreviation,
			imu.Id MeasurementUnitId,
			ppd.Quantity
	FROM MixingStation.PackagePersonalizedDetail ppd WITH(NOLOCK)
	JOIN Inventory.ATC a WITH(NOLOCK) ON ppd.AtcId = a.Id
	JOIN Inventory.InventoryProduct ipr WITH(NOLOCK) ON a.Id = ipr.ATCId
	JOIN Inventory.InventoryMeasurementUnit imu WITH(NOLOCK) ON ipr.MeasurementUnitId = imu.Id
)

SELECT
		CONCAT(rmsd.CampaignDetailId, '-', 1, '-', ISNULL(pmm.AtcId, mm.AtcId), '-', ISNULL(pmm.ProductId, mm.ProductId)) AS Id,
		rmsd.CampaignDetailId,
		1 PreparationType, --Reconstitución
		ISNULL(pmm.ProductId ,mm.ProductId) AS ProductId,
		ISNULL(pmm.AtcId, mm.AtcId) AS MainMedicineId,
		ISNULL(pmm.AtcCode, mm.AtcCode) AS MainMedicineCode,
		ISNULL(pmm.AtcName, mm.AtcName) AS MainMedicineName,
		-------------------------
		ISNULL(pt.AtcId, t.AtcId) AtcIdReconstituent,
		NULL AtcIdVehicle,
		-------------------------
		ISNULL(pt.Stability, t.Stability) Stability,
		ISNULL(pt.TimeUnit, t.TimeUnit) TimeUnit,
		IIF(ISNULL(pt.TimeUnit, t.TimeUnit) = 1, 'HORA(S)', 'DIA(S)') TimeUnitNameStability,
		-- Para medicamentos tipo Peso (FormulationType=1): concentración desde Factor de Dilución por defecto
		-- Para otros tipos: concentración del componente Thinner del paquete
		CASE
			WHEN a.FormulationType = 1 THEN (
				SELECT TOP 1 dfd.Concentration
				FROM MixingStation.DilutionFactors df
				JOIN MixingStation.DilutionFactorsDetail dfd ON dfd.DilutionFactorsId = df.Id
				WHERE df.ATCId = a.Id AND dfd.ByDefault = 1
			)
			ELSE ISNULL(pt.Concentration, t.Concentration)
		END AS Concentration,
		-------------------------
		ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation) AS MeasurementUnitMainMedicine,
		ISNULL(pmm.MeasurementUnitId, mm.MeasurementUnitId) AS MeasurementUnitMainMedicineId,
		ISNULL(pt.MeasurementUnitAbbreviation, t.MeasurementUnitAbbreviation) AS MeasurementUnitVolume,
		ISNULL(pt.MeasurementUnitId, t.MeasurementUnitId) AS MeasurementUnitVolumeId,
		-------------------------
		ISNULL(ISNULL(pt.Quantity, t.Quantity), 0) AS MaximumVolume
FROM MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK)
LEFT JOIN Cte_DetailByStandarPackage mm ON rmsd.PackageId = mm.PackageId AND mm.MainMedicine = 1 AND rmsd.PackagePersonalizedId IS NULL
LEFT JOIN Cte_DetailByStandarPackage t ON rmsd.PackageId = t.PackageId AND t.Thinner = 1 AND rmsd.PackagePersonalizedId IS NULL
LEFT JOIN Cte_DetailByPackagePersonalized pmm ON rmsd.PackagePersonalizedId = pmm.PackageId	AND pmm.MainMedicine = 1
LEFT JOIN Cte_DetailByPackagePersonalized pt ON rmsd.PackagePersonalizedId = pt.PackageId AND pt.Thinner = 1
JOIN Inventory.ATC a ON ISNULL(pmm.AtcId, mm.AtcId) = a.Id
GROUP BY rmsd.CampaignDetailId,
		ISNULL(pmm.AtcId, mm.AtcId),
		ISNULL(pmm.AtcCode, mm.AtcCode),
		ISNULL(pmm.AtcName, mm.AtcName),
		ISNULL(pmm.ProductId ,mm.ProductId),
		ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation),
		ISNULL(pmm.MeasurementUnitId, mm.MeasurementUnitId),
		ISNULL(pt.Stability, t.Stability),
		ISNULL(pt.TimeUnit, t.TimeUnit),
		ISNULL(pt.Concentration, t.Concentration),
		ISNULL(pt.Quantity, t.Quantity),
		ISNULL(pt.MeasurementUnitAbbreviation, t.MeasurementUnitAbbreviation),
		ISNULL(pt.MeasurementUnitId, t.MeasurementUnitId),
		ISNULL(pt.AtcId, t.AtcId),
		a.FormulationType,
		a.Id

UNION ALL

SELECT
	CONCAT(rmsd.CampaignDetailId, '-', 3, '-', ISNULL(pmm.AtcId, mm.AtcId), '-', ISNULL(pmm.ProductId, mm.ProductId)) AS Id,
	rmsd.CampaignDetailId,
	3 PreparationType, --Soluciones Inyectables
	ISNULL(pmm.ProductId ,mm.ProductId) AS ProdutId,
	ISNULL(pmm.AtcId, mm.AtcId) AS MainMedicineId,
	ISNULL(pmm.AtcCode, mm.AtcCode) AS MainMedicineCode,
	ISNULL(pmm.AtcName, mm.AtcName) AS MainMedicineName,
	-------------------------
	NULL AtcIdReconstituent,
	ISNULL(pv.AtcId, v.AtcId) AtcIdVehicle,
	-------------------------
	a.StabilityMaximumHours AS Stability,
	1 AS TimeUnit,
	'HORA(S)'AS TimeUnitNameStability,
	MixingStation.CalculateConcentration(a.FormulationType, a.Weight, a.Volume) AS Concentration,
	-------------------------
	ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation) AS MeasurementUnitMainMedicine,
	ISNULL(pmm.MeasurementUnitId, mm.MeasurementUnitId) AS MeasurementUnitMainMedicineId,
	IIF(a.VolumeMeasureUnit IS NOT NULL, ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation), 'mL') AS MeasurementUnitVolume,
	IIF(a.VolumeMeasureUnit IS NOT NULL, a.VolumeMeasureUnit, 19) AS MeasurementUnitVolumeId,
	-------------------------
	CAST(ISNULL(a.Volume, 0) AS DECIMAL(18,2)) AS MaximumVolume
FROM MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK)
LEFT JOIN Cte_DetailByStandarPackage mm ON rmsd.PackageId = mm.PackageId AND mm.MainMedicine = 1 AND rmsd.PackagePersonalizedId IS NULL
LEFT JOIN Cte_DetailByStandarPackage v ON rmsd.PackageId = v.PackageId AND v.Vehicle = 1 AND rmsd.PackagePersonalizedId IS NULL
LEFT JOIN Cte_DetailByPackagePersonalized pmm ON rmsd.PackagePersonalizedId = pmm.PackageId	AND pmm.MainMedicine = 1
LEFT JOIN Cte_DetailByPackagePersonalized pv ON rmsd.PackagePersonalizedId = pv.PackageId AND pv.Vehicle = 1
JOIN Inventory.ATC a WITH(NOLOCK) ON ISNULL(pmm.AtcId, mm.AtcId)= a.Id
GROUP BY rmsd.CampaignDetailId, 
		ISNULL(pmm.AtcId, mm.AtcId),
		ISNULL(pmm.AtcCode, mm.AtcCode),
		ISNULL(pmm.AtcName, mm.AtcName),
		ISNULL(pmm.ProductId ,mm.ProductId),
		ISNULL(pmm.MeasurementUnitAbbreviation, mm.MeasurementUnitAbbreviation),
		ISNULL(pmm.MeasurementUnitId, mm.MeasurementUnitId),
		ISNULL(pv.AtcId, v.AtcId),
		a.StabilityMaximumHours, 
		a.FormulationType, 
		a.Weight, 
		a.Volume,
		a.VolumeMeasureUnit

GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la concentración y estabilidad de los medicamentos contenidos en paquetes de preparación farmacéutica (estándar y personalizados) según el tipo de preparación: reconstitución simple (tipo 1) o reconstitución con dilución (tipo 2). Para cada solicitud de mezcla farmacéutica (campaña), identifica el medicamento principal, el reconstituyente y el vehículo, junto con su código ATC, concentración, estabilidad expresada en horas o días, unidad de medida y volumen máximo. Integra los catálogos de medicamentos (ATC), productos de inventario y unidades de medida para entregar información técnica de preparación lista para ser usada por la estación de mezclas en el proceso de elaboración y validación de fórmulas magistrales o preparaciones parenterales.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewPackageConcentrationByPreparationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewPackageConcentrationByPreparationType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida, por cada detalle de solicitud a la estación de mezclas, la información de concentración, estabilidad, volumen máximo y unidades para los tres tipos de preparación (Reconstitución, Reconstitución y dilución, Soluciones Inyectables), tomando datos del paquete estándar o del paquete personalizado según corresponda.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPackageConcentrationByPreparationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de solicitud (RequestMixingStationDetail) debe estar asociado a un PackageId estándar o a un PackagePersonalizedId; si tiene PackagePersonalizedId, los componentes se toman del paquete personalizado, en caso contrario del paquete estándar.; Los componentes del paquete deben estar marcados con los flags MainMedicine=1, Thinner=1 o Vehicle=1 para identificar su rol (medicamento principal, reconstituyente o vehículo).; Para el tipo de preparación 3 (Soluciones Inyectables), el medicamento principal debe existir en Inventory.ATC (JOIN obligatorio) con datos de FormulationType, Weight, Volume y StabilityMaximumHours.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPackageConcentrationByPreparationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo Id se construye como CONCAT(CampaignDetailId, ''-'', PreparationType, ''-'', MainMedicineId, ''-'', ProductId), garantizando unicidad por combinación de detalle de campaña, tipo de preparación, medicamento principal y producto.; PreparationType siempre es 1, 2 o 3 (literales fijos en cada bloque del UNION ALL).; Para PreparationType=1, AtcIdVehicle siempre es NULL; para PreparationType=3, AtcIdReconstituent siempre es NULL.; MaximumVolume nunca es NULL: se aplica ISNULL(...,0) o CAST(ISNULL(a.Volume,0) AS DECIMAL(18,2)).; La unidad de volumen por defecto cuando no está definida en ATC es ''mL'' con Id=19.; Los datos del paquete personalizado tienen prioridad sobre los del paquete estándar (ISNULL(personalizado, estándar) en todos los campos).; Todas las consultas usan WITH(NOLOCK), aceptando lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPackageConcentrationByPreparationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas (MixingStation); Preparación farmacéutica por reconstitución; Reconstitución y dilución; Soluciones inyectables; Medicamento principal (MainMedicine); Diluyente/Reconstituyente (Thinner); Vehículo de dilución (Vehicle); Clasificación ATC; Estabilidad de la preparación (horas/días); Concentración de la preparación; Volumen máximo de preparación; Paquete estándar vs paquete personalizado; Detalle de campaña (CampaignDetail)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPackageConcentrationByPreparationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewPackageConcentrationByPreparationType: Devuelve tres bloques unidos por UNION ALL: PreparationType=1 (Reconstitución) con AtcIdReconstituent y AtcIdVehicle=NULL; PreparationType=2 (Reconstitución y dilución) con ambos AtcId; PreparationType=3 (Soluciones Inyectables) con AtcIdReconstituent=NULL y concentración calculada vía MixingStation.CalculateConcentration.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPackageConcentrationByPreparationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rmsd.PackagePersonalizedId IS NOT NULL (se resuelve vía ISNULL(pXX.*, XX.*)) → Se usan los componentes del paquete personalizado (Cte_DetailByPackagePersonalized: pmm/pt/pv) else Se usan los componentes del paquete estándar (Cte_DetailByStandarPackage: mm/t/v) siempre que rmsd.PackagePersonalizedId IS NULL; si TimeUnit = 1 → TimeUnitNameStability = ''HORA(S)'' else TimeUnitNameStability = ''DIA(S)''; si PreparationType=3 y a.VolumeMeasureUnit IS NOT NULL → MeasurementUnitVolume toma la abreviatura del medicamento principal y MeasurementUnitVolumeId = a.VolumeMeasureUnit else MeasurementUnitVolume=''mL'' y MeasurementUnitVolumeId=19 (mL por defecto); si PreparationType=3 → Stability se toma de a.StabilityMaximumHours con TimeUnit=1 (horas) y Concentration se calcula con MixingStation.CalculateConcentration(FormulationType, Weight, Volume); MaximumVolume = ISNULL(a.Volume,0) else Para PreparationType 1 y 2, Stability/TimeUnit/Concentration provienen del componente Thinner (tipo 1) o Vehicle (tipo 2) del paquete', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPackageConcentrationByPreparationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'MixingStation.CalculateConcentration', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPackageConcentrationByPreparationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.PackageDetail; MixingStation.PackagePersonalizedDetail; MixingStation.RequestMixingStationDetail; Inventory.ATC; Inventory.InventoryProduct; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPackageConcentrationByPreparationType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewPackageConcentrationByPreparationType';
GO
