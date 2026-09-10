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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula y devuelve los bloques de tiempo libre disponibles dentro de una agenda médica específica, identificada por su ID de agenda. Para ello, cruza tres fuentes: los horarios definidos en la agenda del profesional (AGAGEMEDC), las citas ya asignadas y activas (AGASICITA, excluyendo canceladas), y los bloqueos parciales vigentes (AGBLOQUEOPARCIAL), restando de la franja total de la agenda los intervalos ocupados por citas y bloqueos activos. El resultado lista los espacios libres ordenados por duración descendente, indicando hora de inicio y fin disponible, duración en minutos, código y nombre del profesional de salud, código y nombre del consultorio, y centro de atención; es utilizado para el agendamiento de citas, ya sea manual, web o masivo, permitiendo identificar cuándo y dónde hay espacio real para agendar un paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListAvailableAgendaBlocks';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListAvailableAgendaBlocks';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula y lista los intervalos de tiempo libres dentro de una agenda médica, descontando citas existentes y bloqueos parciales activos, para determinar cupos disponibles de agendamiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListAvailableAgendaBlocks';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una agenda en AGAGEMEDC cuyo CODAUTONU coincida con el identificador recibido; La agenda debe tener definidas fecha/hora de inicio y fin (FECHORAIN, FECHORAFI); Los bloqueos parciales considerados deben tener ESTADO = 1 (activos)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListAvailableAgendaBlocks';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran bloqueos parciales con ESTADO = 1; Un intervalo se reporta solo si InicioDisponible < FinDisponible (duración positiva); Los bloques disponibles nunca se solapan con citas existentes ni con bloqueos activos; Los bloques disponibles están acotados por la ventana FECHORAIN–FECHORAFI de la agenda; Se eliminan duplicados con SELECT DISTINCT y se excluyen filas con extremos nulos; La duración disponible se expresa en minutos vía DATEDIFF(MINUTE, ...)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListAvailableAgendaBlocks';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agenda médica; Cita; Bloqueo parcial de agenda; Profesional de salud; Consultorio; Centro de atención; Disponibilidad de cupos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListAvailableAgendaBlocks';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve los bloques con InicioDisponible y FinDisponible no nulos, junto con duración en minutos, profesional, consultorio y centro de atención asociados a la agenda, ordenados por mayor duración disponible y luego por hora de inicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListAvailableAgendaBlocks';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existen citas (NOT EXISTS en Citas) para la agenda → El bloque disponible es la ventana completa de la agenda (FechaInicioAgenda a FechaFinAgenda) else Se construyen intervalos entre citas usando LAG/LEAD, tomando inicio/fin de agenda como bordes; si Existe un bloqueo que solapa el bloque libre y FechaInicioBloqueo > InicioDisponible → Se genera un sub-bloque desde InicioDisponible hasta FechaInicioBloqueo else Se descarta ese fragmento (NULL); si Existe un bloqueo que solapa el bloque libre y FechaFinBloqueo < FinDisponible → Se genera un sub-bloque desde FechaFinBloqueo hasta FinDisponible else Se descarta ese fragmento (NULL); si No existe ningún bloqueo que solape el bloque libre → El bloque libre se mantiene íntegro como disponible', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListAvailableAgendaBlocks';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGAGEMEDC; dbo.AGASICITA; dbo.AGBLOQUEOPARCIAL; dbo.INPROFSAL; dbo.AGCONSULT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListAvailableAgendaBlocks';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListAvailableAgendaBlocks';
-- GO
