CREATE VIEW [MixingStation].[ViewQualityControl]
AS

WITH CTE_ExpirationDays AS (
    SELECT TOP 1 ExpirationDays
    FROM MixingStation.MixingStationSetting
),
-- Pre-filtra los IDs activos para reducir el scope de los CTEs de clasificación
CTE_RelevantStandardIds AS (
    SELECT rpds.Id AS RequestPackageDetailStatusId
    FROM MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK)
    JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
    JOIN MixingStation.CampaignDetail c WITH(NOLOCK) ON c.Id = rmsd.CampaignDetailId AND c.CampaignStatus IN (5,6)
    JOIN MixingStation.UnitDoseType ud WITH(NOLOCK) ON rmsd.UnitDoseTypeId = ud.Id
    WHERE rpds.Status = 2 AND rpds.BatchCode IS NOT NULL AND ud.MSClass NOT IN (5,7)
),
-- Garantiza una sola fila por RequestPackageDetailStatusId (MIN en ValidateWeigthNPT = peor caso)
-- BIT no admite MIN/MAX directamente: se castea a INT antes de agregar y se devuelve como BIT
CTE_CriticalClasification AS (
    SELECT RequestPackageDetailStatusId,
        CAST(MAX(CAST([Critical] AS INT)) AS BIT) AS [Critical],
        CAST(MIN(CAST(ValidateWeigthNPT AS INT)) AS BIT) AS ValidateWeigthNPT
    FROM (
        SELECT rpdsdc.RequestPackageDetailStatusId,
            CAST(rpdsdcd.Quality AS INT) AS [Critical],
            rpdsdc.ValidateWeigthNPT
        FROM MixingStation.RequestPackageDetailStatusDefectClassification rpdsdc WITH(NOLOCK)
        JOIN CTE_RelevantStandardIds r ON rpdsdc.RequestPackageDetailStatusId = r.RequestPackageDetailStatusId
        JOIN MixingStation.RequestPackageDetailStatusDefectClassificationDetail rpdsdcd WITH(NOLOCK)
            ON rpdsdc.Id = rpdsdcd.RequestPackageDetailStatusDefectClassificationId
        JOIN MixingStation.DefectClassificationItem dci WITH(NOLOCK)
            ON rpdsdcd.DefectClassificationItemId = dci.Id
        WHERE rpdsdcd.Quality IS NOT NULL AND dci.[Critical] = 1
    ) AS t
    GROUP BY RequestPackageDetailStatusId
),
CTE_LessClasification AS (
    SELECT rpdsdc.RequestPackageDetailStatusId,
        CAST(1 AS BIT) AS [Less]
    FROM MixingStation.RequestPackageDetailStatusDefectClassification rpdsdc WITH(NOLOCK)
    JOIN CTE_RelevantStandardIds r ON rpdsdc.RequestPackageDetailStatusId = r.RequestPackageDetailStatusId
    JOIN MixingStation.RequestPackageDetailStatusDefectClassificationDetail rpdsdcd WITH(NOLOCK)
        ON rpdsdc.Id = rpdsdcd.RequestPackageDetailStatusDefectClassificationId
    JOIN MixingStation.DefectClassificationItem dci WITH(NOLOCK)
        ON rpdsdcd.DefectClassificationItemId = dci.Id
    WHERE rpdsdcd.Quality = 1 AND dci.[Less] = 1
    GROUP BY rpdsdc.RequestPackageDetailStatusId
),
DetailsWithDefect AS (
    SELECT rpdsdcd.RequestPackageDetailStatusDefectClassificationId,
        MAX(IIF(dci.Critical = 1 AND rpdsdcd.Quality = 1, 1, 0)) CriticalDefects
    FROM MixingStation.RequestPackageDetailStatusDefectClassificationDetail rpdsdcd WITH(NOLOCK)
    INNER JOIN MixingStation.DefectClassificationItem dci WITH(NOLOCK) ON rpdsdcd.DefectClassificationItemId = dci.Id
    GROUP BY rpdsdcd.RequestPackageDetailStatusDefectClassificationId
),
-- Acota CTE_Defects solo a reempaques activos (MSClass IN (5,7))
CTE_Defects AS (
    SELECT rmsd.Id AS RequestMixingStationDetailId,
        SUM(CASE WHEN dwd.CriticalDefects = 1 THEN 1 ELSE 0 END) AS CriticalDefects,
        SUM(CASE WHEN dwd.CriticalDefects = 0 THEN 1 ELSE 0 END) AS WhitoutDefects
    FROM MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK)
    JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
    JOIN MixingStation.CampaignDetail c WITH(NOLOCK) ON c.Id = rmsd.CampaignDetailId AND c.CampaignStatus IN (5,6)
    JOIN MixingStation.UnitDoseType ud WITH(NOLOCK) ON rmsd.UnitDoseTypeId = ud.Id
    JOIN MixingStation.RequestPackageDetailStatusDefectClassification rpdsdc WITH(NOLOCK) ON rpds.Id = rpdsdc.RequestPackageDetailStatusId
    JOIN DetailsWithDefect dwd ON rpdsdc.Id = dwd.RequestPackageDetailStatusDefectClassificationId
    WHERE rpds.Status = 2 AND rpds.BatchCode IS NOT NULL AND ud.MSClass IN (5,7)
    GROUP BY rmsd.Id
)

SELECT
    rpds.Id,
    NULL AS RequestPackageDetailStatusIds,
    c.CampaignNumber,
    c.ProcessingDate AS DateCampaign,
    rpds.BatchCode AS Lot,
    -- Rama MSClass IN (5,7) es código muerto aquí (WHERE filtra NOT IN (5,7))
    CASE WHEN inp.Code IS NULL THEN pk.Code ELSE inp.Code END AS FinishedProductCode,
    CASE WHEN inp.Name IS NULL THEN pk.Name ELSE inp.Name END AS FinishedProductName,
    DATEADD(DAY, ed.ExpirationDays, GETDATE()) AS ExpirationDate,
    RTRIM(p.Fullname) AS QualitySupervisor,
    'ProductoTerminado' AS [State],  -- WHERE garantiza Status=2
    rmsd.ProductionLineId,
    CASE rpds.QualityStatus
        WHEN 0 THEN 'Pendiente'
        WHEN 1 THEN 'Liberado'
        WHEN 2 THEN 'Rechazado'
        WHEN 3 THEN 'Reprocesado'
    END AS QualityStatus,
    rmsd.CampaignDetailId,
    rpds.RequestMixingStationDetailId,
    CONCAT(pl.Code,' - ',pl.Name) AS ProductionLineCodeName,
    CONCAT(ud.Code,' - ',ud.Description) AS UnitDoseTypeCodeName,
    ud.MSClass AS UnitDoseClass,
    pk.Code AS PackageDescription,
    pt.PatientCodeName,
    crt.[Critical] AS FlagDefectClasificationCritical,
    rmsd.LabelType,
    CASE rmsd.LabelType
        WHEN 1 THEN 'Bolsa'
        WHEN 2 THEN 'Nutriciones parenterales'
        WHEN 3 THEN 'Jeringa'
        ELSE ''
    END AS LabelTypeName,
    c.[Status],
    c.CampaignStatus,
    rpds.[Status] AS RequestPackageDetailStatusState,
    CASE
        WHEN crt.Critical = 1 OR crt.ValidateWeigthNPT = 0 THEN 3
        WHEN lss.Less = 1 THEN 2
        WHEN crt.Critical = 0 THEN 1
        ELSE 0
    END AS FlagQualification,
    NULL AS CriticalDefects,
    NULL AS WhitoutDefects
FROM MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK)
JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN MixingStation.CampaignDetail c WITH(NOLOCK) ON c.Id = rmsd.CampaignDetailId AND c.CampaignStatus IN (5,6)
JOIN MixingStation.CampaignDetailUsers cdu WITH(NOLOCK) ON c.Id = cdu.CampaignDetailId AND cdu.UserRole = 1
JOIN MixingStation.ProductionLine pl WITH(NOLOCK) ON c.ProductionLineId = pl.Id
JOIN MixingStation.UnitDoseType ud WITH(NOLOCK) ON rmsd.UnitDoseTypeId = ud.Id
JOIN Security.[User] us ON cdu.UserId = us.Id
JOIN Security.Person p ON us.IdPerson = p.Id
CROSS JOIN CTE_ExpirationDays ed
LEFT JOIN Inventory.ATC a WITH(NOLOCK) ON a.Id = rmsd.ATCId
LEFT JOIN MixingStation.Package pk WITH(NOLOCK) ON rpds.PackageId = pk.Id
LEFT JOIN Inventory.InventoryProduct inp WITH(NOLOCK) ON inp.Id = pk.ProductId
-- OUTER APPLY elimina el SELECT DISTINCT del original: evita el sort sobre todo el dataset
OUTER APPLY (
    SELECT TOP 1 CONCAT(LTRIM(RTRIM(pac.IPCODPACI)),' - ',pac.IPNOMCOMP) AS PatientCodeName
    FROM MixingStation.RequestMixingStationDetailPatients rmsdp WITH(NOLOCK)
    LEFT JOIN INPACIENT pac ON rmsdp.PatientCode = pac.IPCODPACI
    WHERE rmsdp.RequestMixingStationDetailId = rpds.RequestMixingStationDetailId
      AND rmsdp.CampaignDetailId = c.Id
) AS pt
LEFT JOIN CTE_CriticalClasification crt ON rpds.Id = crt.RequestPackageDetailStatusId
LEFT JOIN CTE_LessClasification lss ON rpds.Id = lss.RequestPackageDetailStatusId
WHERE rpds.Status = 2 AND rpds.BatchCode IS NOT NULL AND ud.MSClass NOT IN (5,7)

UNION ALL

-- Reempaque
SELECT
    rmsd.Id,
    STRING_AGG(TRY_CAST(rpds.Id AS VARCHAR(MAX)), ',') AS RequestPackageDetailStatusIds,
    c.CampaignNumber,
    c.ProcessingDate AS DateCampaign,
    rpds.BatchCode AS Lot,
    -- MSClass IN (5,7) siempre verdadero aquí; rama ELSE pk.Code es código muerto
    CASE WHEN inp.Code IS NULL THEN a.Code ELSE inp.Code END AS FinishedProductCode,
    CASE WHEN inp.Name IS NULL THEN a.AbbreviationName ELSE inp.Name END AS FinishedProductName,
    DATEADD(DAY, ed.ExpirationDays, GETDATE()) AS ExpirationDate,
    RTRIM(p.Fullname) AS QualitySupervisor,
    'ProductoTerminado' AS [State],
    rmsd.ProductionLineId,
    CASE rpds.QualityStatus
        WHEN 0 THEN 'Pendiente'
        WHEN 1 THEN 'Liberado'
        WHEN 2 THEN 'Rechazado'
        WHEN 3 THEN 'Reprocesado'
    END AS QualityStatus,
    rmsd.CampaignDetailId,
    rpds.RequestMixingStationDetailId,
    CONCAT(pl.Code,' - ',pl.Name) AS ProductionLineCodeName,
    CONCAT(ud.Code,' - ',ud.Description) AS UnitDoseTypeCodeName,
    ud.MSClass AS UnitDoseClass,
    NULL AS PackageDescription,
    NULL AS PatientCodeName,
    NULL AS FlagDefectClasificationCritical,
    rmsd.LabelType,
    CASE rmsd.LabelType
        WHEN 1 THEN 'Bolsa'
        WHEN 2 THEN 'Nutriciones parenterales'
        WHEN 3 THEN 'Jeringa'
        ELSE ''
    END AS LabelTypeName,
    c.[Status],
    c.CampaignStatus,
    rpds.[Status] AS RequestPackageDetailStatusState,
    NULL AS FlagQualification,
    cte.CriticalDefects AS CriticalDefects,
    cte.WhitoutDefects AS WhitoutDefects
FROM MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK)
JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
JOIN Inventory.ATC a WITH(NOLOCK) ON a.Id = rmsd.ATCId
JOIN MixingStation.CampaignDetail c WITH(NOLOCK) ON c.Id = rmsd.CampaignDetailId AND c.CampaignStatus IN (5,6)
JOIN MixingStation.CampaignDetailUsers cdu WITH(NOLOCK) ON c.Id = cdu.CampaignDetailId AND cdu.UserRole = 1
JOIN MixingStation.ProductionLine pl WITH(NOLOCK) ON c.ProductionLineId = pl.Id
JOIN MixingStation.UnitDoseType ud WITH(NOLOCK) ON rmsd.UnitDoseTypeId = ud.Id
JOIN Security.[User] us ON cdu.UserId = us.Id
JOIN Security.Person p ON us.IdPerson = p.Id
CROSS JOIN CTE_ExpirationDays ed
LEFT JOIN CTE_Defects cte ON cte.RequestMixingStationDetailId = rmsd.Id
LEFT JOIN MixingStation.Package pk WITH(NOLOCK) ON rpds.PackageId = pk.Id
LEFT JOIN Inventory.InventoryProduct inp WITH(NOLOCK) ON inp.Id = pk.ProductId
WHERE rpds.Status = 2 AND rpds.BatchCode IS NOT NULL AND ud.MSClass IN (5,7)
GROUP BY rmsd.Id, c.CampaignNumber, c.ProcessingDate, rpds.BatchCode, inp.Code, ud.MSClass, a.Code,
    pk.Code, inp.Name, a.AbbreviationName, pk.Name, p.Fullname, rpds.Status, rmsd.ProductionLineId,
    rpds.QualityStatus, rmsd.CampaignDetailId, rpds.RequestMixingStationDetailId, pl.Code, pl.Name,
    ud.Code, ud.Description, rmsd.LabelType, c.Status, c.CampaignStatus, cte.CriticalDefects, cte.WhitoutDefects,
    ed.ExpirationDays
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de control de calidad para los paquetes (bolsas, nutriciones parenterales, jeringas y reempaques) producidos en la estación de mezclas farmacéuticas. Consolida, para cada unidad producida con estado ''Producto Terminado'' y lote asignado, la información de la campaña de producción, el producto terminado, la fecha de vencimiento calculada según los días de expiración configurados en MixingStationSetting, el supervisor de calidad responsable, y el paciente asociado. Integra la clasificación de defectos (defectos críticos, menores y validación de peso NPT) a partir de RequestPackageDetailStatusDefectClassification y DefectClassificationItem para calcular un indicador de calificación (FlagQualification) que refleja si el paquete está aprobado, tiene observaciones menores, defectos críticos o falló validación de peso. Sirve como fuente principal para los reportes y módulos de revisión, liberación o rechazo de calidad en el proceso de preparación de mezclas, permitiendo consultar el estado de calidad (Pendiente, Liberado, Rechazado, Reprocesado) por lote, campaña, línea de producción, tipo de dosis y paciente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewQualityControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewQualityControl';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para control de calidad en la estación de mezclas, los paquetes terminados de campañas en curso/cerradas, calificándolos según defectos críticos/menores y separando productos estándar de reempaques (ATC clase 5/7).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQualityControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe al menos una fila en MixingStation.MixingStationSetting con ExpirationDays para calcular la fecha de vencimiento.; El paquete (RequestPackageDetailStatus) debe estar en Status=2 (producto terminado) y tener BatchCode no nulo.; La campaña asociada (CampaignDetail) debe estar en CampaignStatus 5 o 6.; Debe existir al menos un usuario asignado al CampaignDetail con UserRole=1 (supervisor de calidad).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQualityControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen paquetes cuyo Status=2 (producto terminado) y con BatchCode informado.; Solo se consideran campañas con CampaignStatus 5 o 6.; El supervisor de calidad mostrado siempre proviene de CampaignDetailUsers con UserRole=1.; ExpirationDate siempre se calcula como GETDATE() + ExpirationDays tomado del primer registro de MixingStationSetting.; El State siempre se etiqueta como ''ProductoTerminado'' (única rama de CASE para Status=2).; Para clasificación crítica solo se cuentan ítems con DefectClassificationItem.Critical=1 y Quality no nulo; para clasificación menor (Less) requiere Quality=1 y DefectClassificationItem.Less=1.; Los reempaques (MSClass 5/7) no exponen información de paciente ni descripción de paquete (campos forzados a NULL).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQualityControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Control de calidad farmacéutico; Estación de mezclas; Producto terminado; Lote (BatchCode); Fecha de vencimiento; Supervisor de calidad; Clasificación de defectos críticos y menores; Nutrición parenteral; Reempaque; Campaña de preparación; Línea de producción; Dosis unitaria; Paciente; Liberación/Rechazo/Reproceso de calidad', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQualityControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewQualityControl: Devuelve dos conjuntos unidos con UNION ALL: (1) paquetes con UnitDoseType.MSClass NOT IN (5,7) a nivel de RequestPackageDetailStatus; (2) reempaques con MSClass IN (5,7) agregados por RequestMixingStationDetail con STRING_AGG de los Ids.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQualityControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ud.MSClass NOT IN (5,7) y rpds.Status=2 con BatchCode no nulo → Se emite la fila como producto estándar mostrando datos de paciente, Package y FlagDefectClasificationCritical/FlagQualification calculados por defectos. else Se delega al segundo SELECT (reempaque); si ud.MSClass IN (5,7) y rpds.Status=2 con BatchCode no nulo → Se trata como reempaque: se agregan IDs por RequestMixingStationDetail (STRING_AGG), se anulan PackageDescription, PatientCodeName y FlagQualification, y se reportan CriticalDefects/WhitoutDefects desde CTE_Defects.; si inp.Code/Name IS NULL (no hay InventoryProduct) → FinishedProductCode/Name se toma de ATC (a) cuando MSClass IN (5,7); en caso contrario, de Package (pk). else Se usa el código/nombre del InventoryProduct (inp).; si Cálculo de FlagQualification (solo primer SELECT) → 3 si crt.Critical=1 o crt.ValidateWeigthNPT=0; 2 si lss.Less=1; 1 si crt.Critical=0; 0 en otro caso.; si Mapeo de rpds.QualityStatus → 0→Pendiente, 1→Liberado, 2→Rechazado, 3→Reprocesado.; si Mapeo de rmsd.LabelType → 1→Bolsa, 2→Nutriciones parenterales, 3→Jeringa. else Cadena vacía.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQualityControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.MixingStationSetting; MixingStation.RequestPackageDetailStatusDefectClassification; MixingStation.RequestPackageDetailStatusDefectClassificationDetail; MixingStation.DefectClassificationItem; MixingStation.RequestMixingStationDetail; MixingStation.RequestPackageDetailStatus; MixingStation.CampaignDetail; MixingStation.CampaignDetailUsers; MixingStation.ProductionLine; MixingStation.UnitDoseType; Security.User; Security.Person; Inventory.ATC; MixingStation.Package; Inventory.InventoryProduct; MixingStation.RequestMixingStationDetailPatients; INPACIENT', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQualityControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'optimized_performance_2026-05-13', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQualityControl';
GO
