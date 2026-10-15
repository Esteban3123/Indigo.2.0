CREATE VIEW [dbo].[ViewPharmacyMixingStationWithOncology]
AS
WITH MainData AS
(
    SELECT
        HC.CODCONCEC,
        HC.CODCONCEP,
        HC.CODCONCES,
        HC.FECHAORDE,
        HC.CODCENATE,
        HC.UFUCODIGO,
        HC.CODCENCOS,
        HC.IPCODPACI,
        HC.NUMINGRES,
        HC.NUMEFOLIO,
        HC.IDETIPHIS,
        HC.ORDTRANUE,
        ISNULL(HC.TIPOSOLICITUD, 0) AS TIPOSOLICITUD,
        HC.MEDICAMENTOVALIDADO,
        HC.CODBODEGA,
        HC.CODPROSAL,
        HC.ORDESTADO,
        ISNULL(HC.ORDENQUIMIO, 0) AS ORDENQUIMIO
    FROM dbo.HCFARMEPC AS HC WITH (NOLOCK)
    WHERE HC.ORDESTADO = '1' AND HC.IDAGEPROGQX IS NULL
),
MixingStationDetail AS
(
    SELECT
        HD.CODCONCEC,
        HD.CodeSusceptibleMixingStation,
        PSMS.ApplicationsNumber AS RequestedQuantity,
        COUNT
        (
            DISTINCT CASE
                WHEN PD.IsDispensed = 1 THEN PD.GroupingCodeDose
            END
        ) AS DispensedQuantity
    FROM dbo.HCFARMEPD AS HD WITH (NOLOCK)
    INNER JOIN MedicalHistory.ProductSusceptibleMixingStation AS PSMS WITH (NOLOCK) ON PSMS.CodeSusceptibleMixingStation = HD.CodeSusceptibleMixingStation
    INNER JOIN MedicalHistory.PharmaDose AS PD WITH (NOLOCK) ON PD.CodeSusceptibleMixingStation = PSMS.CodeSusceptibleMixingStation
    WHERE HD.SENDTO IN (0, 2)
    GROUP BY HD.CODCONCEC, HD.CodeSusceptibleMixingStation, PSMS.ApplicationsNumber
    HAVING PSMS.ApplicationsNumber >
            COUNT
            (
                DISTINCT CASE
                    WHEN PD.IsDispensed = 1 THEN PD.GroupingCodeDose
                END
            )
),
PendingMixingStation AS
(
    SELECT MSD.CODCONCEC
    FROM MixingStationDetail AS MSD
    GROUP BY MSD.CODCONCEC
    HAVING SUM(MSD.RequestedQuantity) > SUM(MSD.DispensedQuantity)
)
SELECT
    HC.CODCONCEC,
    HC.CODCONCEP,
    HC.CODCONCES,
    HC.FECHAORDE,
    HC.CODCENATE,
    HC.UFUCODIGO,
    HC.CODCENCOS,
    HC.IPCODPACI,
    HC.NUMINGRES,
    HC.NUMEFOLIO,
    HC.IDETIPHIS,
    HC.ORDTRANUE,
    HC.TIPOSOLICITUD,
    HC.MEDICAMENTOVALIDADO,
    HC.CODBODEGA,
    HC.CODPROSAL,
    ING.CODCAMACT,
    HC.ORDESTADO,
    ISNULL(CG.ExtramuralPharmaceuticalDispensing, 0) AS TIPOSOLICITUD1,
    CG.ExtramuralPharmaceuticalDispensing AS PERMITEEXTRA,
    1 AS RoutingMP
FROM MainData AS HC
INNER JOIN PendingMixingStation AS PMS ON PMS.CODCONCEC = HC.CODCONCEC
INNER JOIN dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES = HC.NUMINGRES
LEFT JOIN Contract.CareGroup AS CG WITH (NOLOCK) ON CG.Id = ING.GENCAREGROUP
WHERE
    (
        HC.ORDENQUIMIO = 0
        AND
        (
            ING.IESTADOIN NOT IN ('F', 'C')
            OR
            (
                HC.TIPOSOLICITUD = 2
                AND ISNULL(ING.TRATAESPECIA, 0) <> 3
            )
        )
    )
    OR
    (
        HC.ORDENQUIMIO = 1
        AND ING.IESTADOIN NOT IN ('F', 'C')
    );
GO