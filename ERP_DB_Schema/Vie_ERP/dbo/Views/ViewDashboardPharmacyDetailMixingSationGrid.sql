



CREATE VIEW [dbo].[ViewDashboardPharmacyDetailMixingSationGrid]
AS
WITH Cte_PharmaDose AS (
    SELECT 
        pd.GroupingCodeDose, 
        pd.CodeSusceptibleMixingStation, 
        pd.IDHCFARMEPC,
		pd.IsDispensed,
		pd.UnitDoseTypeId
    FROM MedicalHistory.PharmaDose pd WITH(NOLOCK)
    WHERE pd.DeliveryStatus <> 3  -- 0 'Sin Entregar'   1 'Entregado'   2 'Generado  3 Anulado
    GROUP BY 
        pd.GroupingCodeDose, 
        pd.CodeSusceptibleMixingStation, 
        pd.IDHCFARMEPC,
		pd.IsDispensed,
		pd.UnitDoseTypeId
)

SELECT
    ps.Id,
    '0' AS Opcion,
    '' AS Procedimiento,
    pc.CODCONCEC AS Consecutivo,
    CAST(ps.CodeSusceptibleMixingStation AS VARCHAR(36)) AS CodeSusceptibleMixingStation,
    ps.Origin,
    ps.IdOrigin,
    ps.FullProductName AS Producto,
    '' AS CodProducto,
    ps.MainDrugCode,
    --COUNT(psd.GroupingCodeDose) AS CantidadSolicitada,
	ps.ApplicationsNumber AS CantidadSolicitada,
    0 AS CantidadEntregada,
    pc.NUMINGRES AS Ingreso,
    pc.IPCODPACI AS CodigoPaciente,
	--COUNT(psd.GroupingCodeDose) - COUNT(CASE WHEN psd.IsDispensed = 1 THEN 1 END) AS CantidadPendiente,
	ps.ApplicationsNumber - COUNT(DISTINCT CASE WHEN psd.IsDispensed = 1 THEN 1 END) AS CantidadPendiente,
	ud.MSClass
FROM MedicalHistory.ProductSusceptibleMixingStation ps WITH(NOLOCK)
JOIN Cte_PharmaDose psd ON psd.CodeSusceptibleMixingStation = ps.CodeSusceptibleMixingStation
JOIN MixingStation.UnitDoseType ud on ud.Id = psd.UnitDoseTypeId
JOIN HCFARMEPC pc WITH(NOLOCK) ON psd.IDHCFARMEPC = pc.CODCONCEC
JOIN HCFARMEPD pcd ON pcd.CODCONCEC = pc.CODCONCEC AND ps.CodeSusceptibleMixingStation = pcd.CodeSusceptibleMixingStation
WHERE ps.ApplicationsNumber > 0 AND pcd.SENDTO = 2
GROUP BY 
    ps.CodeSusceptibleMixingStation,
    ps.Id,
    ps.Origin,
    ps.IdOrigin,
    ps.FullProductName,
    ps.MainDrugCode,
    pc.NUMINGRES,
    pc.CODCONCEC,
    pc.IPCODPACI,
	ps.ApplicationsNumber,
	ud.MSClass
HAVING (COUNT(psd.GroupingCodeDose) - COUNT(CASE WHEN psd.IsDispensed = 1 THEN 1 END)) > 0