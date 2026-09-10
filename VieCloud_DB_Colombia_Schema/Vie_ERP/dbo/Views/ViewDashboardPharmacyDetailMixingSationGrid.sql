

CREATE VIEW [dbo].[ViewDashboardPharmacyDetailMixingSationGrid]
AS
WITH BasePS AS (
    SELECT
        ps.Id,
        ps.CodeSusceptibleMixingStation,
        ps.Origin,
        ps.IdOrigin,
        ps.FullProductName,
        ps.MainDrugCode,
        ps.ApplicationsNumber
    FROM MedicalHistory.ProductSusceptibleMixingStation AS ps 
    WHERE ps.ApplicationsNumber > 0
),
EligiblePSCodes AS (
    SELECT DISTINCT
        CodeSusceptibleMixingStation
    FROM BasePS
),
BasePCD AS (
    SELECT DISTINCT
        pcd.CODCONCEC,
        pcd.CodeSusceptibleMixingStation
    FROM dbo.HCFARMEPD AS pcd 
    WHERE pcd.SENDTO = 2 AND pcd.CodeSusceptibleMixingStation IS NOT NULL
),
BasePharmaDose AS (
    SELECT DISTINCT
        pd.GroupingCodeDose,
        pd.CodeSusceptibleMixingStation,
        pd.IDHCFARMEPC,
        pd.IsDispensed,
        pd.UnitDoseTypeId
    FROM MedicalHistory.PharmaDose AS pd 
    INNER JOIN EligiblePSCodes AS eps ON eps.CodeSusceptibleMixingStation = pd.CodeSusceptibleMixingStation
    INNER JOIN BasePCD AS pcd ON pcd.CODCONCEC = pd.IDHCFARMEPC AND pcd.CodeSusceptibleMixingStation = pd.CodeSusceptibleMixingStation
    WHERE pd.DeliveryStatus <> 3 -- 0 'Sin Entregar', 1 'Entregado', 2 'Generado', 3 'Anulado'
),
DoseAgg AS (
    SELECT
        pd.CodeSusceptibleMixingStation,
        pd.IDHCFARMEPC,
        ud.MSClass,
        COUNT(pd.GroupingCodeDose) AS TotalGeneratedDose,
        SUM(CASE WHEN pd.IsDispensed = 1 THEN 1 ELSE 0 END) AS TotalDispensedDose,
        COUNT(DISTINCT CASE WHEN pd.IsDispensed = 1 THEN 1 END) AS DispensedFlagCount
    FROM BasePharmaDose AS pd
    INNER JOIN MixingStation.UnitDoseType AS ud  ON ud.Id = pd.UnitDoseTypeId
    GROUP BY
        pd.CodeSusceptibleMixingStation,
        pd.IDHCFARMEPC,
        ud.MSClass
    HAVING COUNT(pd.GroupingCodeDose) - SUM(CASE WHEN pd.IsDispensed = 1 THEN 1 ELSE 0 END) > 0
)
SELECT
    ps.Id,
    '0' AS Opcion,
    '' AS Procedimiento,
    pc.CODCONCEC AS Consecutivo,
    CAST(ps.CodeSusceptibleMixingStation AS varchar(36)) AS CodeSusceptibleMixingStation,
    ps.Origin,
    ps.IdOrigin,
    ps.FullProductName AS Producto,
    '' AS CodProducto,
    ps.MainDrugCode,
    ps.ApplicationsNumber AS CantidadSolicitada,
    0 AS CantidadEntregada,
    pc.NUMINGRES AS Ingreso,
    pc.IPCODPACI AS CodigoPaciente,
    ps.ApplicationsNumber - da.DispensedFlagCount AS CantidadPendiente,
    da.MSClass
FROM DoseAgg AS da
INNER JOIN BasePS AS ps ON ps.CodeSusceptibleMixingStation = da.CodeSusceptibleMixingStation
INNER JOIN dbo.HCFARMEPC AS pc  ON pc.CODCONCEC = da.IDHCFARMEPC
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del dashboard de farmacia que muestra el detalle de productos pendientes de dispensación en la estación de mezclas (mixing station). Combina los productos susceptibles de mezcla con las dosis farmacéuticas activas (excluyendo anuladas) y la orden médica de farmacia del paciente, calculando la cantidad solicitada versus la cantidad ya dispensada para exponer únicamente los productos que aún tienen unidades pendientes de entrega. Sirve para que el personal de farmacia monitoree en tiempo real qué medicamentos o preparaciones para mezcla están pendientes de despachar, asociando cada producto al número de ingreso y cédula del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashboardPharmacyDetailMixingSationGrid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashboardPharmacyDetailMixingSationGrid';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para el dashboard de la estación de mezclas, los productos susceptibles de mezcla pendientes de dispensar por paciente/ingreso, con cantidades solicitadas, entregadas y pendientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSationGrid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen productos susceptibles de mezcla con ApplicationsNumber > 0; Las dosis farmacéuticas asociadas no están anuladas (DeliveryStatus <> 3); El detalle en HCFARMEPD debe tener SENDTO = 2 (envío a estación de mezclas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSationGrid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan productos con cantidad pendiente mayor que cero; Las dosis anuladas (DeliveryStatus=3) nunca se cuentan ni como solicitadas ni como entregadas; Solo se consideran detalles con destino estación de mezclas (SENDTO=2); La cantidad solicitada se toma de ApplicationsNumber del producto, no del conteo de dosis; CantidadPendiente = ApplicationsNumber - dosis marcadas como dispensadas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSationGrid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas; Dosis farmacéutica; Dispensación; Producto susceptible de mezcla; Tipo de dosis unitaria; Paciente; Ingreso hospitalario; Anulación de dosis', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSationGrid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo filas donde (total dosis agrupadas - dosis dispensadas) > 0, es decir, productos con cantidad pendiente real.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSationGrid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PharmaDose.DeliveryStatus <> 3 → Se incluye la dosis en el cálculo (se excluyen anuladas) else Dosis anulada se excluye del conteo; si PharmaDose.IsDispensed = 1 → Cuenta como dosis entregada y reduce la cantidad pendiente else Permanece como pendiente; si HCFARMEPD.SENDTO = 2 → El detalle se considera dirigido a la estación de mezclas y entra al resultado else Se excluye; si ProductSusceptibleMixingStation.ApplicationsNumber > 0 → El producto se incluye en el dashboard else Se omite', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSationGrid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalHistory.PharmaDose; MedicalHistory.ProductSusceptibleMixingStation; MixingStation.UnitDoseType; dbo.HCFARMEPC; dbo.HCFARMEPD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSationGrid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailMixingSationGrid';
GO
