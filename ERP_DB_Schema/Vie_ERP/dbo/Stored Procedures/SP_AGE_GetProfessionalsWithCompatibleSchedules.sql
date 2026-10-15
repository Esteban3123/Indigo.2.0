CREATE PROCEDURE [dbo].[SP_AGE_GetProfessionalsWithCompatibleSchedules]

@MinutosDisponibles INT,
@CodigoActividad varchar(10),
@CodigoEspecialidad varchar(10)

AS
BEGIN
    SET NOCOUNT ON;
       
    WITH Agendas AS (
        SELECT
            A.CODPROSAL,
            A.CODAUTONU,
            A.FECHORAIN,
            A.FECHORAFI
        FROM AGAGEMEDC A
        INNER JOIN AGAGEMEDD B ON A.CODAUTONU = B.CODAUTONU
        WHERE B.CODACTMED = @CodigoActividad 
          AND B.CODESPECI = @CodigoEspecialidad
          AND A.FECHORAFI >= Common.GETDATE()
    ),
    Turnos AS (
        SELECT
            A.CODPROSAL,
            A.CODAUTONU,
            A.FECHORAIN,
            A.FECHORAFI,
            COALESCE(T.FECHORAIN, A.FECHORAIN) AS TurnoInicio,
            COALESCE(T.FECHORAFI, A.FECHORAFI) AS TurnoFin
        FROM Agendas A
        LEFT JOIN AGASICITA T ON A.CODAUTONU = T.IDAGENDA AND T.CODESTCIT not in(4,5)
    ),
    Espacios AS (
         --Caso donde la agenda está completamente libre
        SELECT
            A.CODPROSAL,
            A.CODAUTONU,
            A.FECHORAIN AS InicioDisponible,
            A.FECHORAFI AS FinOcupado
        FROM Agendas A
    	LEFT JOIN AGASICITA T ON A.CODAUTONU = T.IDAGENDA
        WHERE T.IDAGENDA IS NULL
        
        UNION ALL
        
        -- Espacios antes del primer turno ocupado
        SELECT
            T.CODPROSAL,
            T.CODAUTONU,
            T.FECHORAIN AS InicioDisponible,
            COALESCE(MIN(T.TurnoInicio), T.FECHORAFI) AS FinOcupado
        FROM Turnos T
        GROUP BY T.CODPROSAL, T.CODAUTONU, T.FECHORAIN, T.FECHORAFI
        
        UNION ALL
        
        -- Espacios entre turnos ocupados
        SELECT
            T.CODPROSAL,
            T.CODAUTONU,
            T.TurnoFin AS InicioDisponible,
            LEAD(T.TurnoInicio, 1, T.FECHORAIN) OVER (PARTITION BY T.CODAUTONU ORDER BY T.TurnoInicio) AS FinOcupado
        FROM Turnos T
        
        UNION ALL
        
        -- Espacios después del último turno ocupado
        SELECT
            T.CODPROSAL,
            T.CODAUTONU,
            COALESCE(MAX(T.TurnoFin), T.FECHORAIN) AS InicioDisponible,
            T.FECHORAFI AS FinOcupado
        FROM Turnos T
        GROUP BY T.CODPROSAL, T.CODAUTONU, T.FECHORAIN, T.FECHORAFI
    )
    SELECT DISTINCT RTRIM(A.CODPROSAL) AS CODPROSAL, RTRIM(I.NOMMEDICO) AS NOMMEDICO
    FROM Espacios A
    INNER JOIN INPROFSAL I ON A.CODPROSAL = I.CODPROSAL
    WHERE DATEDIFF(MINUTE, InicioDisponible, FinOcupado) >= @MinutosDisponibles;
END