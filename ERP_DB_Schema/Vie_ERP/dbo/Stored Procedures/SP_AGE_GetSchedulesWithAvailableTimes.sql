CREATE PROCEDURE [dbo].[SP_AGE_GetSchedulesWithAvailableTimes]

@MinutosDisponibles INT,
@CodigoActividad VARCHAR(10),
@CodigoEspecialidad VARCHAR(10),
@FechaInicio DATETIME,
@FechaFin DATETIME

AS
BEGIN
    SET NOCOUNT ON;
       
    -- Ajustar @FechaInicio si corresponde al dia de hoy pero es anterior a la hora actual
    IF @FechaInicio < Common.GETDATE()
    BEGIN
        SET @FechaInicio = Common.GETDATE();
    END;

    IF @FechaFin <= @FechaInicio
    BEGIN
        -- rango vacío
        SELECT CAST(NULL AS VARCHAR(10)) AS CODPROSAL, CAST(NULL AS VARCHAR(100)) AS NOMMEDICO WHERE 1 = 0; 
		RETURN;
    END;

    WITH Agendas AS (
        SELECT
            A.CODPROSAL, A.CODAUTONU, A.FECHORAIN, A.FECHORAFI, A.CODIGOCON
        FROM AGAGEMEDC A
        INNER JOIN AGAGEMEDD B ON A.CODAUTONU = B.CODAUTONU
        WHERE B.CODACTMED = @CodigoActividad AND B.CODESPECI = @CodigoEspecialidad AND A.FECHORAFI >= @FechaInicio AND A.FECHORAIN < @FechaFin
    ),
    Turnos AS (
        SELECT
            A.CODPROSAL, A.CODAUTONU, A.FECHORAIN, A.FECHORAFI, A.CODIGOCON, COALESCE(T.FECHORAIN, A.FECHORAIN) AS TurnoInicio, COALESCE(T.FECHORAFI, A.FECHORAFI) AS TurnoFin
        FROM Agendas A
        LEFT JOIN AGASICITA T ON A.CODAUTONU = T.IDAGENDA AND T.CODESTCIT NOT IN (4,5)
    ),
    Espacios AS (
        -- Agendas sin citas
        SELECT
            A.CODPROSAL, A.CODAUTONU, A.CODIGOCON, A.FECHORAIN AS InicioDisponible, A.FECHORAFI AS FinOcupado
        FROM Agendas A
        LEFT JOIN AGASICITA T ON A.CODAUTONU = T.IDAGENDA
        WHERE T.IDAGENDA IS NULL

        UNION ALL

        -- Antes del primer ocupado
        SELECT
            T.CODPROSAL, T.CODAUTONU, T.CODIGOCON, T.FECHORAIN AS InicioDisponible, COALESCE(MIN(T.TurnoInicio), T.FECHORAFI) AS FinOcupado
        FROM Turnos T
        GROUP BY T.CODPROSAL, T.CODAUTONU, T.CODIGOCON, T.FECHORAIN, T.FECHORAFI

        UNION ALL

        -- Entre dos
        SELECT
            T.CODPROSAL, T.CODAUTONU, T.CODIGOCON, T.TurnoFin AS InicioDisponible, LEAD(T.TurnoInicio, 1, T.FECHORAIN) OVER (PARTITION BY T.CODAUTONU ORDER BY T.TurnoInicio) AS FinOcupado
        FROM Turnos T

        UNION ALL

        -- Después del último
        SELECT
            T.CODPROSAL, T.CODAUTONU, T.CODIGOCON, COALESCE(MAX(T.TurnoFin), T.FECHORAIN) AS InicioDisponible, T.FECHORAFI AS FinOcupado
        FROM Turnos T
        GROUP BY T.CODPROSAL, T.CODAUTONU, T.CODIGOCON, T.FECHORAIN, T.FECHORAFI
    ),
    EspaciosAjustados AS (
        SELECT
            CODPROSAL, CODAUTONU, CODIGOCON, CASE WHEN InicioDisponible < @FechaInicio THEN DATEADD(MINUTE, 5, @FechaInicio) ELSE InicioDisponible END AS InicioAjustado, FinOcupado
        FROM Espacios
    ),
    ResultadoFinal AS (
        SELECT
            CODPROSAL, CODAUTONU, CODIGOCON, InicioAjustado, CASE WHEN FinOcupado > @FechaFin THEN @FechaFin ELSE FinOcupado END AS FinAjustado
        FROM EspaciosAjustados
        WHERE 
            CASE WHEN FinOcupado > @FechaFin THEN @FechaFin ELSE FinOcupado END > @FechaInicio
    )
    SELECT DISTINCT A.CODAUTONU, RTRIM(A.CODPROSAL) AS CODPROSAL, RTRIM(I.NOMMEDICO) AS NOMMEDICO, InicioAjustado AS FECHAHORAINICIO, FinAjustado AS FECHAHORAFIN, CONCAT(RTRIM(A.CODPROSAL), '-', RTRIM(CODIGOCON)) AS CodigoCompuesto
    FROM ResultadoFinal A
    INNER JOIN INPROFSAL I ON A.CODPROSAL = I.CODPROSAL
    WHERE DATEDIFF(MINUTE, InicioAjustado, FinAjustado) >= @MinutosDisponibles
    ORDER BY InicioAjustado ASC;
END