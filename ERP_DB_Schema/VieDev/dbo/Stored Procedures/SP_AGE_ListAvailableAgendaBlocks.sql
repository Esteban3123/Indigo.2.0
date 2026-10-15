--SP para listar los bloques de espacio disponibles en una agenda en específico
CREATE PROCEDURE [dbo].[SP_AGE_ListAvailableAgendaBlocks]
@IDAGENDA int
AS
BEGIN
    SET NOCOUNT ON;

WITH Agenda AS (
    SELECT 
        AGE.FECHORAIN AS FechaInicioAgenda,
        AGE.FECHORAFI AS FechaFinAgenda
    FROM AGAGEMEDC AGE
    WHERE AGE.CODAUTONU = @IDAGENDA
),
Citas AS (
    SELECT 
        AGA.FECHORAIN AS FechaInicioCita,
        AGA.FECHORAFI AS FechaFinCita
    FROM AGASICITA AGA
    WHERE AGA.IDAGENDA = @IDAGENDA
),
Bloqueos AS (
    SELECT 
        B.FECHAINIBLOQUEO AS FechaInicioBloqueo,
        B.FECHAFINBLOQUEO AS FechaFinBloqueo
    FROM AGBLOQUEOPARCIAL B
    WHERE B.IDAGENDA = @IDAGENDA AND B.ESTADO = 1
),
Intervalos AS (
    SELECT 
        LAG(FechaFinCita, 1, (SELECT FechaInicioAgenda FROM Agenda)) OVER (ORDER BY FechaInicioCita) AS InicioDisponible,
        FechaInicioCita AS FinDisponible
    FROM Citas
    UNION ALL
    SELECT 
        FechaFinCita AS InicioDisponible,
        LEAD(FechaInicioCita, 1, (SELECT FechaFinAgenda FROM Agenda)) OVER (ORDER BY FechaInicioCita) AS FinDisponible
    FROM Citas
),
BloquesSinCitas AS (
    SELECT 
        InicioDisponible, 
        FinDisponible
    FROM Intervalos 
    WHERE InicioDisponible < FinDisponible 
    UNION ALL 
    SELECT 
        FechaInicioAgenda, 
        FechaFinAgenda
    FROM Agenda 
    WHERE NOT EXISTS (SELECT 1 FROM Citas)
),
BloquesDisponibles AS (
    SELECT 
        CASE 
            WHEN B.FechaInicioBloqueo > BS.InicioDisponible THEN BS.InicioDisponible
            ELSE NULL
        END AS InicioDisponible, 
        CASE 
            WHEN B.FechaInicioBloqueo > BS.InicioDisponible THEN B.FechaInicioBloqueo
            ELSE NULL
        END AS FinDisponible
    FROM BloquesSinCitas BS
    LEFT JOIN Bloqueos B 
        ON B.FechaInicioBloqueo < BS.FinDisponible 
        AND B.FechaFinBloqueo > BS.InicioDisponible
    
    UNION ALL 

    SELECT 
        CASE 
            WHEN B.FechaFinBloqueo < BS.FinDisponible THEN B.FechaFinBloqueo
            ELSE NULL
        END AS InicioDisponible, 
        CASE 
            WHEN B.FechaFinBloqueo < BS.FinDisponible THEN BS.FinDisponible
            ELSE NULL
        END AS FinDisponible
    FROM BloquesSinCitas BS
    LEFT JOIN Bloqueos B 
        ON B.FechaInicioBloqueo < BS.FinDisponible 
        AND B.FechaFinBloqueo > BS.InicioDisponible
    
    UNION ALL 

    SELECT InicioDisponible, FinDisponible
    FROM BloquesSinCitas
    WHERE NOT EXISTS (
        SELECT 1 FROM Bloqueos B 
        WHERE B.FechaInicioBloqueo < BloquesSinCitas.FinDisponible 
        AND B.FechaFinBloqueo > BloquesSinCitas.InicioDisponible
    )
)
SELECT DISTINCT 
    InicioDisponible, 
    FinDisponible, 
    DATEDIFF(MINUTE, InicioDisponible, FinDisponible) AS DuracionDisponible, 
    (SELECT RTRIM(CODPROSAL) FROM AGAGEMEDC WHERE CODAUTONU = @IDAGENDA) AS ProfesionalDisponible, 
    (SELECT RTRIM(NOMMEDICO) FROM AGAGEMEDC A INNER JOIN INPROFSAL I ON A.CODPROSAL = I.CODPROSAL WHERE CODAUTONU = @IDAGENDA) AS NombreProfesionalDisponible, 
    (SELECT CODIGOCON FROM AGAGEMEDC WHERE CODAUTONU = @IDAGENDA) AS ConsultorioDisponible, 
    (SELECT RTRIM(DESCRICON) FROM AGAGEMEDC A INNER JOIN AGCONSULT B ON A.CODIGOCON = B.CODIGOCON AND A.CODCENATE = B.CODCENATE  WHERE CODAUTONU = @IDAGENDA) AS NombreConsultorioDisponible,
	(SELECT RTRIM(CODCENATE) FROM AGAGEMEDC WHERE CODAUTONU = @IDAGENDA) AS CentroAtencionAgenda
FROM BloquesDisponibles 
WHERE InicioDisponible IS NOT NULL AND FinDisponible IS NOT NULL 
ORDER BY DuracionDisponible DESC, InicioDisponible

END