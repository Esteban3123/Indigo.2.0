CREATE VIEW [dbo].[ViewDashBoardPharmacyMixingSationPatient]
AS
SELECT
    CONCAT(
        LTRIM(RTRIM(A.IPCODPACI)),
        ' - ',
        A.ORDTRANUE,
        '-',
        A.NUMINGRES
    ) AS Id,
    0 AS SelectOption,
    A.IPCODPACI AS CodigoPaciente,
    A.CODCENATE AS CodigoCentroAtencion,
    RTRIM(PAC.IPNOMCOMP) AS NombrePaciente,
    A.NUMINGRES AS Ingreso,
    CASE A.ORDTRANUE
        WHEN 1 THEN 'Tratamiento nuevo'
        WHEN 2 THEN 'Tratamiento antiguo'
        WHEN 3 THEN 'Código azul'
        WHEN 5 THEN 'Código azul'
        ELSE ''
    END AS Tipo,
    CAST(0 AS bit) AS Impresion,
    PAC.IPFECNACI AS BirthDay
FROM dbo.ViewPharmacyMixingStationWithOncology AS A WITH (NOLOCK)
INNER JOIN dbo.INPACIENT AS PAC WITH (NOLOCK) ON PAC.IPCODPACI = A.IPCODPACI
WHERE  A.RoutingMP = 1
GROUP BY A.IPCODPACI, A.CODCENATE, PAC.IPNOMCOMP, A.NUMINGRES, A.ORDTRANUE, PAC.IPFECNACI;
GO