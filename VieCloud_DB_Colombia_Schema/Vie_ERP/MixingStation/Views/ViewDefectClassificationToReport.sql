

CREATE view [MixingStation].[ViewDefectClassificationToReport]
as

WITH DefectClassificationBase AS (
    SELECT
        rpdsdc.Id AS RequestPackageDetailStatusDefectClassificationId,
        rpdsdc.Observation,
        rpdsdc.ActualWeight,
        rpds.Id AS RequestPackageDetailStatusId,
        rpds.BatchCode,
        rmsd.CampaignDetailId,
        udt.MSClass AS UnitDoseTypeMSClass,
        rmsd.Source
    FROM MixingStation.RequestPackageDetailStatusDefectClassification rpdsdc
    JOIN MixingStation.RequestPackageDetailStatus rpds
      ON rpds.Id = rpdsdc.RequestPackageDetailStatusId
    JOIN MixingStation.RequestMixingStationDetail rmsd
      ON rmsd.Id = rpds.RequestMixingStationDetailId
    JOIN MixingStation.UnitDoseType udt
      ON udt.Id = rmsd.UnitDoseTypeId
),
InfoDistinct AS (
    SELECT DISTINCT Observation, CampaignDetailId, BatchCode
    FROM DefectClassificationBase
    WHERE UnitDoseTypeMSClass IN (5, 7)
        OR Source = 4
), Details AS (
    SELECT 
        STRING_AGG(CAST(Observation AS VARCHAR(MAX)), ', ') AS Observations,
        STRING_AGG(CAST(BatchCode AS VARCHAR(MAX)), ', ') AS BatchCodes,
        CampaignDetailId
    FROM DefectClassificationBase
    WHERE UnitDoseTypeMSClass NOT IN (5, 7)
        AND Source <> 4
    GROUP BY CampaignDetailId

	UNION ALL

	SELECT
		STRING_AGG(Observation, ',') AS Observations,
		STRING_AGG(BatchCode, ',') AS BatchCodes,
		CampaignDetailId
	FROM InfoDistinct
	GROUP BY CampaignDetailId
), DefectClassificationRows AS (
    SELECT
        rpdsdcd.Id AS RequestPackageDetailStatusDefectClassificationDetailId,
        dci.Id,
        dcg.Id AS DefectClassificationGroupId,
        dcg.Description AS DefectClassificationGroupName,
        dcg.Weight AS DefectClassificationGroupWeight,
        dci.Code AS ItemCode,
        dci.Description AS ItemDescription,
        dci.Weight AS ItemWeight,
        dci.Critical,
        dci.Less,
        rpdsdcd.Production,
        rpdsdcd.Quality,
        base.CampaignDetailId,
        cdu.UserCode AS UserSupervisor,
        base.Observation,
        base.ActualWeight,
        base.UnitDoseTypeMSClass,
        base.BatchCode,
        base.RequestPackageDetailStatusId,
        cd.CampaignNumber
    FROM DefectClassificationBase base
    JOIN MixingStation.RequestPackageDetailStatusDefectClassificationDetail rpdsdcd
      ON rpdsdcd.RequestPackageDetailStatusDefectClassificationId = base.RequestPackageDetailStatusDefectClassificationId
    JOIN MixingStation.DefectClassificationItem dci
      ON dci.Id = rpdsdcd.DefectClassificationItemId
    JOIN MixingStation.DefectClassificationGroup dcg
      ON dcg.Id = dci.DefectClassificationGroupId
    JOIN MixingStation.CampaignDetail cd
      ON cd.Id = base.CampaignDetailId
    LEFT JOIN MixingStation.CampaignDetailUsers cdu
      ON cdu.CampaignDetailId = base.CampaignDetailId
     AND cdu.UserRole = 1
)

SELECT dcr.Id
	, dcr.DefectClassificationGroupId
	, dcr.DefectClassificationGroupName
	, dcr.DefectClassificationGroupWeight
	, dcr.ItemCode
	, dcr.ItemDescription
	, dcr.ItemWeight
	, dcr.Critical
	, dcr.Less
	, SUM(CASE WHEN dcr.Production = 1 THEN 1 ELSE 0 END) Production
	, SUM(CASE WHEN dcr.Quality = 1 THEN 1 ELSE 0 END) Quality
	, details.CampaignDetailId
	, dcr.UserSupervisor
	, details.Observations
	, NULL TheoreticalWeight
	, NULL InputsWeight
	, NULL TheoreticalAndInputsWeight
	, NULL MaximunWeight
	, NULL MinimunWeight
	, NULL ActualWeight
	, NULL ValidationResult
	, dcr.UnitDoseTypeMSClass
	, details.BatchCodes
	, dcr.CampaignNumber
FROM DefectClassificationRows dcr
JOIN Details details
  ON details.CampaignDetailId = dcr.CampaignDetailId
WHERE dcr.UnitDoseTypeMSClass <> 2
GROUP BY dcr.Id, dcr.DefectClassificationGroupId, dcr.DefectClassificationGroupName, dcr.DefectClassificationGroupWeight,
dcr.ItemCode, dcr.ItemDescription, dcr.ItemWeight, dcr.Critical, dcr.Less, details.CampaignDetailId, dcr.UserSupervisor,
details.Observations, dcr.UnitDoseTypeMSClass, details.BatchCodes, dcr.CampaignNumber

UNION ALL 

SELECT dcr.RequestPackageDetailStatusDefectClassificationDetailId
	, dcr.DefectClassificationGroupId
	, dcr.DefectClassificationGroupName
	, dcr.DefectClassificationGroupWeight
	, dcr.ItemCode
	, dcr.ItemDescription
	, dcr.ItemWeight
	, dcr.Critical
	, dcr.Less
	, SUM(CASE WHEN dcr.Production = 1 THEN 1 ELSE 0 END) Production
	, SUM(CASE WHEN dcr.Quality = 1 THEN 1 ELSE 0 END) Quality
	, dcr.CampaignDetailId
	, dcr.UserSupervisor
	, dcr.Observation Observations
	, vwn.TheoreticalWeight
	, vwn.InputsWeight
	, vwn.TheoreticalAndInputsWeight
	, vwn.MaximunWeight
	, vwn.MinimunWeight
	, dcr.ActualWeight
	, IIF(dcr.ActualWeight BETWEEN vwn.MinimunWeight AND vwn.MaximunWeight, 1, 0) AS ValidationResult
	, dcr.UnitDoseTypeMSClass
	, dcr.BatchCode BatchCodes
	, dcr.CampaignNumber
FROM DefectClassificationRows dcr
JOIN MixingStation.ViewListValidationWeightNPT vwn
  ON vwn.RequestPackageDetailStatusId = dcr.RequestPackageDetailStatusId
WHERE dcr.UnitDoseTypeMSClass = 2
    AND dcr.UserSupervisor IS NOT NULL
GROUP BY dcr.RequestPackageDetailStatusDefectClassificationDetailId, dcr.DefectClassificationGroupId,
dcr.DefectClassificationGroupName, dcr.DefectClassificationGroupWeight, dcr.ItemCode, dcr.ItemDescription,
dcr.ItemWeight, dcr.Critical, dcr.Less, dcr.CampaignDetailId, dcr.UserSupervisor, dcr.Observation,
vwn.TheoreticalWeight, vwn.InputsWeight, vwn.TheoreticalAndInputsWeight, vwn.MaximunWeight, vwn.MinimunWeight,
dcr.ActualWeight, dcr.UnitDoseTypeMSClass, dcr.BatchCode, dcr.CampaignNumber

GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la clasificación de defectos detectados durante el control de calidad en la estación de mezclas farmacéuticas, para su uso en reportes de campaña. Combina los grupos y ítems de defectos (con sus pesos y criticidad) con los resultados de inspección por producción y calidad, las observaciones registradas, los códigos de lote y el número de campaña. Para preparaciones de tipo NPT (nutrición parenteral, MSClass = 2) incluye además la validación de peso real versus rangos teóricos (mínimo, máximo, insumos), mientras que para los demás tipos de dosis unitaria agrupa observaciones y lotes por detalle de campaña. Sirve como fuente de datos para reportes de trazabilidad y calidad de paquetes (bolsas/envases) preparados en la estación de mezclas, incluyendo el usuario supervisor responsable de cada campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewDefectClassificationToReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewDefectClassificationToReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los defectos clasificados por ítem y campaña para reportes de calidad en la estación de mezclas, separando preparaciones estándar (con observaciones/lotes agregados) de preparaciones NPT (clase 2) que añaden validación de pesos teóricos vs reales.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDefectClassificationToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en RequestPackageDetailStatusDefectClassificationDetail vinculados a un ítem y grupo de clasificación de defectos; Cada CampaignDetail tiene asociado un usuario con UserRole = 1 (supervisor) en CampaignDetailUsers para el segundo bloque (INNER JOIN); Para preparaciones con MSClass = 2 (NPT) debe existir registro correspondiente en ViewListValidationWeightNPT por RequestPackageDetailStatusId', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDefectClassificationToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El supervisor reportado siempre corresponde a CampaignDetailUsers.UserRole = 1; Las preparaciones NPT (MSClass=2) siempre traen información de validación de peso; las no-NPT nunca la traen; La vista nunca incluye filas cuya UnitDoseType no exista (todos los JOINs a UnitDoseType son INNER); El ValidationResult solo es 1 cuando ActualWeight cae dentro del intervalo cerrado [MinimunWeight, MaximunWeight]', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDefectClassificationToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas (farmacia); Clasificación de defectos (grupos e ítems con peso/severidad); Defecto crítico y menor (Critical/Less); Campaña de preparación farmacéutica; Lote (BatchCode); Dosis unitaria y su clase (MSClass); Preparación NPT (Nutrición Parenteral Total, MSClass=2) con validación de peso; Supervisor de campaña (UserRole=1); Defecto detectado por Producción vs Calidad', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDefectClassificationToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewDefectClassificationToReport: Para UnitDoseType.MSClass IN (5,7) o RequestMixingStationDetail.Source = 4, las observaciones y BatchCodes se agregan tras eliminar duplicados (DISTINCT por Observation, CampaignDetailId, BatchCode) antes del STRING_AGG; [RETURN_RESULT] MixingStation.ViewDefectClassificationToReport: Para UnitDoseType.MSClass NOT IN (5,7) AND Source <> 4, las observaciones y BatchCodes se concatenan directamente con STRING_AGG por CampaignDetailId sin deduplicación; [RETURN_RESULT] MixingStation.ViewDefectClassificationToReport: Cuando UnitDoseType.MSClass <> 2 los campos de pesos (Theoretical/Inputs/Maximun/Minimun/Actual/ValidationResult) se devuelven como NULL; [RETURN_RESULT] MixingStation.ViewDefectClassificationToReport: Cuando UnitDoseType.MSClass = 2 (NPT) se calcula ValidationResult = 1 si rpdsdc.ActualWeight BETWEEN vwn.MinimunWeight AND vwn.MaximunWeight, sino 0; [RETURN_RESULT] MixingStation.ViewDefectClassificationToReport: Production y Quality se cuentan como SUM(IIF(=1,1,0)) por agrupación de ítem/grupo/campaña, totalizando ocurrencias marcadas como producción o calidad', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDefectClassificationToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si udt.MSClass <> 2 → Primer bloque: agrega observaciones y BatchCodes por CampaignDetailId (con o sin DISTINCT según MSClass IN (5,7) o Source=4) y devuelve los campos de peso como NULL; si udt.MSClass = 2 → Segundo bloque: une con ViewListValidationWeightNPT para incorporar pesos teóricos, mínimos y máximos y calcular ValidationResult comparando ActualWeight contra el rango permitido else Excluido del segundo bloque; si ud.MSClass IN (5,7) OR rmsd.Source = 4 → Las observaciones se deduplican (DISTINCT) antes de concatenarse else Se concatenan tal cual sin DISTINCT', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDefectClassificationToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestPackageDetailStatusDefectClassification; MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetail; MixingStation.UnitDoseType; MixingStation.RequestPackageDetailStatusDefectClassificationDetail; MixingStation.DefectClassificationItem; MixingStation.DefectClassificationGroup; MixingStation.CampaignDetail; MixingStation.CampaignDetailUsers; MixingStation.ViewListValidationWeightNPT', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDefectClassificationToReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewDefectClassificationToReport';
GO
