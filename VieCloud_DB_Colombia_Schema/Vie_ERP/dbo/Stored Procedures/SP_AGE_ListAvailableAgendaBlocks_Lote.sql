
CREATE OR ALTER PROCEDURE dbo.SP_AGE_ListAvailableAgendaBlocks_Lote
    @IDAGENDAS VARCHAR(MAX)  -- lista de CODAUTONU separados por coma, ej: '123,456,789'
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Ids AS (
        SELECT CAST(VALUE AS INT) AS CODAUTONU FROM dbo.SPLITSTRING(@IDAGENDAS)
    ),
    Agenda AS (
        SELECT
            AGE.CODAUTONU,
            AGE.FECHORAIN AS FechaInicioAgenda,
            AGE.FECHORAFI AS FechaFinAgenda,
            RTRIM(AGE.CODPROSAL) AS CODPROSAL,
            AGE.CODIGOCON,
            AGE.CODCENATE
        FROM AGAGEMEDC AGE
        INNER JOIN Ids ON Ids.CODAUTONU = AGE.CODAUTONU
    ),
    Citas AS (
        SELECT
            AGA.IDAGENDA,
            AGA.FECHORAIN AS FechaInicioCita,
            AGA.FECHORAFI AS FechaFinCita
        FROM AGASICITA AGA
        INNER JOIN Ids ON Ids.CODAUTONU = AGA.IDAGENDA
        WHERE AGA.CODESTCIT <> 4
    ),
    Bloqueos AS (
        SELECT
            B.IDAGENDA,
            B.FECHAINIBLOQUEO AS FechaInicioBloqueo,
            B.FECHAFINBLOQUEO AS FechaFinBloqueo
        FROM AGBLOQUEOPARCIAL B
        INNER JOIN Ids ON Ids.CODAUTONU = B.IDAGENDA
        WHERE B.ESTADO = 1
    ),
    Intervalos AS (
        SELECT
            C.IDAGENDA,
            LAG(C.FechaFinCita, 1, A.FechaInicioAgenda) OVER (PARTITION BY C.IDAGENDA ORDER BY C.FechaInicioCita) AS InicioDisponible,
            C.FechaInicioCita AS FinDisponible
        FROM Citas C
        INNER JOIN Agenda A ON A.CODAUTONU = C.IDAGENDA

        UNION ALL

        SELECT
            C.IDAGENDA,
            C.FechaFinCita AS InicioDisponible,
            LEAD(C.FechaInicioCita, 1, A.FechaFinAgenda) OVER (PARTITION BY C.IDAGENDA ORDER BY C.FechaInicioCita) AS FinDisponible
        FROM Citas C
        INNER JOIN Agenda A ON A.CODAUTONU = C.IDAGENDA
    ),
    BloquesSinCitas AS (
        SELECT IDAGENDA, InicioDisponible, FinDisponible
        FROM Intervalos
        WHERE InicioDisponible < FinDisponible

        UNION ALL

        SELECT A.CODAUTONU, A.FechaInicioAgenda, A.FechaFinAgenda
        FROM Agenda A
        WHERE NOT EXISTS (SELECT 1 FROM Citas C WHERE C.IDAGENDA = A.CODAUTONU)
    ),
    BloquesDisponibles AS (
        SELECT
            BS.IDAGENDA,
            CASE WHEN B.FechaInicioBloqueo > BS.InicioDisponible THEN BS.InicioDisponible ELSE NULL END AS InicioDisponible,
            CASE WHEN B.FechaInicioBloqueo > BS.InicioDisponible THEN B.FechaInicioBloqueo ELSE NULL END AS FinDisponible
        FROM BloquesSinCitas BS
        LEFT JOIN Bloqueos B
            ON B.IDAGENDA = BS.IDAGENDA
            AND B.FechaInicioBloqueo < BS.FinDisponible
            AND B.FechaFinBloqueo > BS.InicioDisponible

        UNION ALL

        SELECT
            BS.IDAGENDA,
            CASE WHEN B.FechaFinBloqueo < BS.FinDisponible THEN B.FechaFinBloqueo ELSE NULL END AS InicioDisponible,
            CASE WHEN B.FechaFinBloqueo < BS.FinDisponible THEN BS.FinDisponible ELSE NULL END AS FinDisponible
        FROM BloquesSinCitas BS
        LEFT JOIN Bloqueos B
            ON B.IDAGENDA = BS.IDAGENDA
            AND B.FechaInicioBloqueo < BS.FinDisponible
            AND B.FechaFinBloqueo > BS.InicioDisponible

        UNION ALL

        SELECT BS.IDAGENDA, BS.InicioDisponible, BS.FinDisponible
        FROM BloquesSinCitas BS
        WHERE NOT EXISTS (
            SELECT 1 FROM Bloqueos B
            WHERE B.IDAGENDA = BS.IDAGENDA
              AND B.FechaInicioBloqueo < BS.FinDisponible
              AND B.FechaFinBloqueo > BS.InicioDisponible
        )
    )
    SELECT DISTINCT
        BD.IDAGENDA AS CODAUTONU,
        BD.InicioDisponible,
        BD.FinDisponible,
        DATEDIFF(MINUTE, BD.InicioDisponible, BD.FinDisponible) AS DuracionDisponible,
        A.CODPROSAL AS ProfesionalDisponible,
        RTRIM(I.NOMMEDICO) AS NombreProfesionalDisponible,
        A.CODIGOCON AS ConsultorioDisponible,
        RTRIM(CO.DESCRICON) AS NombreConsultorioDisponible,
        RTRIM(A.CODCENATE) AS CentroAtencionAgenda
    FROM BloquesDisponibles BD
    INNER JOIN Agenda A ON A.CODAUTONU = BD.IDAGENDA
    LEFT JOIN INPROFSAL I ON I.CODPROSAL = A.CODPROSAL
    LEFT JOIN AGCONSULT CO ON CO.CODIGOCON = A.CODIGOCON AND CO.CODCENATE = A.CODCENATE
    WHERE BD.InicioDisponible IS NOT NULL AND BD.FinDisponible IS NOT NULL
    ORDER BY BD.IDAGENDA, DuracionDisponible DESC, BD.InicioDisponible;
END