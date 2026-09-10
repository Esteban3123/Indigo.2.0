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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que identifica qué profesionales de la salud tienen disponibilidad suficiente en su agenda para atender una actividad médica y especialidad específicas. Recibe como parámetros el código de actividad médica, el código de especialidad y los minutos mínimos de disponibilidad requeridos. Consulta los bloques de agenda (AGAGEMEDC) filtrados por actividad y especialidad (AGAGEMEDD), descuenta los turnos ya ocupados con citas vigentes (AGASICITA, excluyendo estados cancelado/anulado), y calcula los espacios libres entre citas para determinar si algún hueco supera el mínimo de minutos solicitado. Devuelve el código y nombre de cada profesional (INPROFSAL) que cuente con al menos un espacio libre compatible, siendo útil para agendamiento de citas, asignación automática de profesional disponible y validación de capacidad antes de reservar un turno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_GetProfessionalsWithCompatibleSchedules';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_GetProfessionalsWithCompatibleSchedules';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los profesionales de salud que tienen al menos un hueco libre en su agenda con duración suficiente, para una actividad y especialidad determinadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetProfessionalsWithCompatibleSchedules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir agendas vigentes (FECHORAFI >= fecha actual) asociadas a la actividad y especialidad solicitadas.; Las citas en AGASICITA con estado 4 o 5 se consideran no ocupantes (canceladas/anuladas) y se excluyen del cálculo de ocupación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetProfessionalsWithCompatibleSchedules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se evalúan agendas cuya fecha-hora final aún no ha pasado.; El filtro por actividad y especialidad es obligatorio y simultáneo.; Las citas en estado 4 o 5 nunca bloquean disponibilidad.; El resultado nunca incluye profesionales repetidos (DISTINCT).; Solo se reportan profesionales con al menos un espacio cuya duración alcance los minutos requeridos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetProfessionalsWithCompatibleSchedules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agenda médica; Profesional de salud; Especialidad; Actividad médica; Cita; Estado de cita; Disponibilidad / huecos de agenda; Turno', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetProfessionalsWithCompatibleSchedules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] INPROFSAL: Cuando DATEDIFF(MINUTE, InicioDisponible, FinOcupado) >= minutos requeridos, se retorna el código y nombre del profesional, sin duplicados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetProfessionalsWithCompatibleSchedules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Estado de cita (CODESTCIT) está en (4,5) → Se ignora la cita al construir los turnos ocupados, tratando ese tiempo como disponible. else La cita se considera ocupante y delimita los espacios libres.; si La agenda no tiene ninguna cita asociada (T.IDAGENDA IS NULL) → Se considera que la agenda está completamente libre desde FECHORAIN hasta FECHORAFI. else Se calculan huecos antes del primer turno, entre turnos y después del último turno.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetProfessionalsWithCompatibleSchedules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGAGEMEDC; dbo.AGAGEMEDD; dbo.AGASICITA; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetProfessionalsWithCompatibleSchedules';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetProfessionalsWithCompatibleSchedules';
-- GO
