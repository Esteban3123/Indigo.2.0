CREATE VIEW [dbo].[ViewDashBoardPharmacyMixingSation]
AS
SELECT
    A.CODCONCEC AS [Row],
    0 AS SelectOption,
    A.CODCENATE AS CodigoCentroAtencion,
    A.CODCONCEC AS ConsecutivoFarmacia,
    A.FECHAORDE AS FechaOrden,
    A.IPCODPACI AS CodigoPaciente,
    RTRIM(PAC.IPNOMCOMP) AS NombrePaciente,
    RTRIM(UF.UFUDESCRI) AS UnidadFuncional,
    A.ORDESTADO AS Estado,
    RTRIM(PROF.NOMMEDICO) AS NombreMedico,
    dbo.GetProfessionType(PROF.TIPPROFES) AS Profesion,
    CONCAT(RTRIM(BOD.CODBODEGA), ' - ', RTRIM(BOD.DESBODEGA)) AS Bodega,
    A.NUMINGRES AS Ingreso,
    A.UFUCODIGO,
    COALESCE(NULLIF(A.CODCONCEP, ''), '') AS ConsecutivoPrescripcion,
    COALESCE(NULLIF(A.CODCONCES, ''), '') AS ConsecutivoInsumos,
    RTRIM(A.CODCENCOS) AS CentroCostos,
    A.ORDTRANUE AS Tipo,
    A.NUMEFOLIO AS Folio,
    A.IDETIPHIS,
    CAST(0 AS bit) AS Impresion,
    CASE
        WHEN A.MEDICAMENTOVALIDADO IS NULL OR A.MEDICAMENTOVALIDADO = 0
            THEN CAST(0 AS bit)
        ELSE CAST(1 AS bit)
    END AS Validado,
    ISNULL(A.TIPOSOLICITUD, 0) AS TIPOSOLICITUD,
    ISNULL(CAMA.NUMCAMHOS, '') AS CodigoCama,
    A.TIPOSOLICITUD1,
    A.PERMITEEXTRA,
    RTRIM(LTRIM(A.CODCENATE)) + ' - ' + RTRIM(LTRIM(CENT.NOMCENATE)) AS CareCenterDescription,
    PAC.IPFECNACI AS BirthDay
FROM dbo.ViewPharmacyMixingStationWithOncology AS A WITH (NOLOCK)
INNER JOIN dbo.IHBODEGAS AS BOD WITH (NOLOCK) ON BOD.CODBODEGA = A.CODBODEGA
INNER JOIN dbo.INPACIENT AS PAC WITH (NOLOCK) ON PAC.IPCODPACI = A.IPCODPACI
INNER JOIN dbo.INPROFSAL AS PROF WITH (NOLOCK) ON PROF.CODPROSAL = A.CODPROSAL
INNER JOIN dbo.INUNIFUNC AS UF WITH (NOLOCK) ON UF.UFUCODIGO = A.UFUCODIGO
INNER JOIN dbo.ADCENATEN AS CENT WITH (NOLOCK) ON CENT.CODCENATE = A.CODCENATE
LEFT JOIN dbo.CHCAMASHO AS CAMA WITH (NOLOCK) ON CAMA.CODICAMAS = CASE
                                                                        WHEN A.CODCAMACT = 0 THEN ''
                                                                        ELSE CAST(A.CODCAMACT AS varchar(15))
                                                                    END
WHERE A.RoutingMP = 1;
GO