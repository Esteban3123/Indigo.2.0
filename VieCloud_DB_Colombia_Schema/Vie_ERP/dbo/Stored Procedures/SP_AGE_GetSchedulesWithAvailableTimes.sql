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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de agendamiento que busca bloques de tiempo disponibles para agendar una cita médica, dado un rango de fechas, una actividad médica (procedimiento o consulta), una especialidad clínica y una duración mínima en minutos. Cruza las agendas de los profesionales de salud (AGAGEMEDC y AGAGEMEDD) con las citas ya asignadas (AGASICITA) para calcular los espacios libres reales, excluyendo los turnos activos y respetando la hora actual si la fecha de inicio es hoy. Retorna los bloques disponibles por profesional, con nombre del médico, horario de inicio y fin ajustados, y un código compuesto profesional-contrato, ordenados cronológicamente; sirve para que el sistema de agendamiento web o masivo ofrezca al usuario únicamente los turnos con suficiente tiempo libre para atender la actividad solicitada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_GetSchedulesWithAvailableTimes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_GetSchedulesWithAvailableTimes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los profesionales y franjas horarias libres dentro de un rango, para una actividad y especialidad, descontando citas vigentes y filtrando huecos cuya duración cubra los minutos requeridos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetSchedulesWithAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la función Common.GETDATE() para obtener la fecha/hora actual del sistema.; Las agendas en AGAGEMEDC/AGAGEMEDD deben coincidir con la actividad y especialidad solicitadas y solaparse con el rango [FechaInicio, FechaFin).; Los profesionales deben existir en INPROFSAL para poder retornar su nombre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetSchedulesWithAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se ofrecen turnos cuyo inicio sea anterior a la hora actual del sistema.; Las citas con estado 4 o 5 no bloquean disponibilidad (se tratan como inexistentes).; Solo se devuelven huecos cuya duración en minutos sea mayor o igual a los minutos requeridos.; Los huecos se acotan dentro del rango [FechaInicio, FechaFin].; Cuando un hueco comienza antes del rango solicitado, su inicio se desplaza 5 minutos después de FechaInicio.; Los códigos de profesional y consultorio se entregan sin espacios en blanco a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetSchedulesWithAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agenda médica; Profesional de salud; Especialidad; Actividad médica; Cita; Estado de cita; Disponibilidad horaria; Consultorio; Turno', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetSchedulesWithAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Si FechaFin <= FechaInicio (tras ajuste), retorna un resultset vacío con columnas CODPROSAL y NOMMEDICO y termina.; [RETURN_RESULT] resultset: Retorna huecos disponibles (CODAUTONU, CODPROSAL, NOMMEDICO, FECHAHORAINICIO, FECHAHORAFIN, CodigoCompuesto) solo cuando DATEDIFF(MINUTE, InicioAjustado, FinAjustado) >= MinutosDisponibles, ordenados por InicioAjustado ascendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetSchedulesWithAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FechaInicio < Common.GETDATE() → Se reemplaza FechaInicio por la fecha/hora actual para no ofrecer turnos en el pasado. else Se conserva la FechaInicio recibida.; si FechaFin <= FechaInicio (tras ajuste) → Se devuelve resultset vacío y termina la ejecución. else Continúa el cálculo de espacios disponibles.; si Cita en AGASICITA con CODESTCIT IN (4,5) → La cita se ignora (se considera no vigente, p.ej. cancelada/anulada) al calcular ocupación. else La cita se considera ocupada y se excluye su franja del tiempo disponible.; si Agenda sin ninguna cita asociada (LEFT JOIN nulo) → Toda la franja de la agenda se considera disponible. else Se calculan huecos antes, entre y después de las citas existentes.; si InicioDisponible < FechaInicio → Se ajusta el inicio al FechaInicio + 5 minutos. else Se mantiene el InicioDisponible original.; si FinOcupado > FechaFin → Se trunca el fin del hueco a FechaFin. else Se mantiene el FinOcupado original.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetSchedulesWithAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGAGEMEDC; dbo.AGAGEMEDD; dbo.AGASICITA; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetSchedulesWithAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetSchedulesWithAvailableTimes';
-- GO
