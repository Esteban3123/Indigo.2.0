
CREATE PROCEDURE [Scheduling].[SP_AGE_GetRenalTherapySchedulesAvailableTimes]

@FechaInicio DATETIME,
@FechaFin DATETIME,
@IdSala VARCHAR(10),
@CodigoActividad VARCHAR(10),
@DuracionMinutos INT

AS
BEGIN
    SET NOCOUNT ON;
       
     -- Hora actual del servidor
    DECLARE @Ahora DATETIME = COmmon.GETDATE();

    -- Validar rango
    IF @FechaFin <= @FechaInicio
    BEGIN
        SELECT CAST(NULL AS VARCHAR(10)) AS IDEQUIPO, CAST(NULL AS VARCHAR(100)) AS DESCREQUI WHERE 1=0;
        RETURN;
    END;

    ;WITH Agendas AS (
        SELECT A.IDEQUIPO, A.FECHORAIN, A.FECHORAFI, E.DESCREQUI, S.CODCONCEC AS IDSALA, S.DESCRIPSAL, E.CODCENATE, D.CODACTMED
        FROM AGDISPONEQUIPO A
        INNER JOIN AGENSALAEQU SE ON SE.IDAGEQUIPTRA = A.IDEQUIPO
        INNER JOIN AGEQUIPTRA E ON E.ID = SE.IDAGEQUIPTRA
        INNER JOIN AGEQUIPTRAD D ON D.IDAGEQUIPTRA = E.ID
        INNER JOIN AGENSALAC S ON S.CODCONCEC = SE.CODCONCEC
        WHERE S.CODCONCEC = @IdSala AND E.ESTEQUIP = 1 AND D.CODACTMED = @CodigoActividad AND A.TURNOBLOQ = 0 AND A.FECHORAFI >= @FechaInicio AND A.FECHORAIN < @FechaFin
    ),
    Citas AS (
        SELECT AG.IDEQUIPOTRA AS IDEQUIPO, AG.FECHORAIN, AG.FECHORAFI
        FROM AGASICITA AG
        WHERE AG.CODESTCIT NOT IN (4,5)
    ),
    Turnos AS (
        SELECT A.IDEQUIPO, A.FECHORAIN, A.FECHORAFI, C.FECHORAIN AS CITAINICIO, C.FECHORAFI AS CITAFFIN
        FROM Agendas A
        LEFT JOIN Citas C ON A.IDEQUIPO = C.IDEQUIPO AND C.FECHORAIN < A.FECHORAFI AND C.FECHORAFI > A.FECHORAIN
    ),
    Espacios AS (        
        SELECT T.IDEQUIPO, T.FECHORAIN AS InicioDisponible, T.FECHORAFI AS FinOcupado
        FROM Turnos T
        WHERE T.CITAINICIO IS NULL

        UNION ALL

        SELECT T.IDEQUIPO, T.CITAFFIN AS InicioDisponible, LEAD(T.CITAINICIO, 1, T.FECHORAFI) OVER (PARTITION BY T.IDEQUIPO, T.FECHORAIN, T.FECHORAFI ORDER BY T.CITAINICIO) AS FinOcupado
        FROM Turnos T
        WHERE T.CITAINICIO IS NOT NULL
    ),
    EspaciosAjustados AS (

        SELECT E.IDEQUIPO, 
            CASE 
                WHEN E.InicioDisponible < @Ahora THEN @Ahora 
                WHEN E.InicioDisponible < @FechaInicio THEN @FechaInicio 
                ELSE E.InicioDisponible 
            END AS InicioAjustado,
            CASE 
                WHEN E.FinOcupado > @FechaFin THEN @FechaFin 
                ELSE E.FinOcupado 
            END AS FinAjustado
        FROM Espacios E
        WHERE E.FinOcupado > @FechaInicio
    )
    SELECT DISTINCT EA.IDEQUIPO, Ag.DESCREQUI, Ag.IDSALA, Ag.DESCRIPSAL, EA.InicioAjustado AS FECHAHORAINICIO, EA.FinAjustado AS FECHAHORAFIN
    FROM EspaciosAjustados EA
    INNER JOIN Agendas Ag ON EA.IDEQUIPO = Ag.IDEQUIPO
    WHERE DATEDIFF(MINUTE, EA.InicioAjustado, EA.FinAjustado) >= @DuracionMinutos AND EA.FinAjustado > @Ahora
    ORDER BY EA.InicioAjustado ASC;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los horarios disponibles para agendar sesiones de terapia renal (diálisis u otros tratamientos renales) en una sala y actividad médica específicas, dentro de un rango de fechas dado. Evalúa la disponibilidad real de cada equipo o recurso físico (máquinas, camillas, sillas) cruzando sus franjas de disponibilidad registradas en AGDISPONEQUIPO con las citas ya agendadas en AGASICITA, descontando los turnos bloqueados y las citas activas (excluyendo canceladas/anuladas). Devuelve únicamente los espacios libres cuya duración es suficiente para la sesión solicitada (según @DuracionMinutos), ajustando los tiempos para que no queden en el pasado ni fuera del rango pedido. Se utiliza en el módulo de agendamiento de terapia renal para presentarle al operador los cupos reales disponibles por equipo, sala y actividad antes de confirmar una nueva cita.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_GetRenalTherapySchedulesAvailableTimes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_GetRenalTherapySchedulesAvailableTimes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula los espacios libres de tiempo en equipos de una sala asignados a una actividad médica (terapia renal), considerando agendas, citas vigentes y duración mínima requerida.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetRenalTherapySchedulesAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas debe ser válido: FechaFin > FechaInicio (de lo contrario retorna resultado vacío).; La sala (CODCONCEC) debe existir y tener equipos asociados con ESTEQUIP=1 (equipo activo).; Los equipos deben estar configurados para la actividad médica indicada (CODACTMED).; Debe existir disponibilidad en AGDISPONEQUIPO con TURNOBLOQ=0 (turno no bloqueado) que intersecte el rango.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetRenalTherapySchedulesAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se excluyen citas con CODESTCIT en (4,5) (estados cancelado/anulado u equivalentes), no bloquean disponibilidad.; Solo se consideran equipos activos (ESTEQUIP=1).; Solo se consideran turnos no bloqueados (TURNOBLOQ=0).; Nunca se devuelven espacios pasados: FinAjustado debe ser mayor que la hora actual del servidor.; Los espacios devueltos siempre cumplen la duración mínima solicitada.; Los espacios disponibles quedan acotados al rango [FechaInicio, FechaFin] mediante ajuste de extremos.; El filtrado se restringe a una sala y una actividad médica específica.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetRenalTherapySchedulesAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Terapia renal; Agenda de equipos; Sala de atención; Actividad médica; Cita; Turno; Disponibilidad de equipo; Estado de cita; Equipo de tratamiento', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetRenalTherapySchedulesAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando FechaFin <= FechaInicio, retorna un resultset vacío con columnas IDEQUIPO y DESCREQUI nulas (WHERE 1=0).; [RETURN_RESULT] resultset: Devuelve los espacios disponibles por equipo cuya duración (DATEDIFF MINUTE entre InicioAjustado y FinAjustado) sea >= DuracionMinutos y cuyo FinAjustado sea mayor a la hora actual.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetRenalTherapySchedulesAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FechaFin <= FechaInicio → Retorna resultset vacío y termina el procedimiento. else Procede al cálculo de espacios disponibles.; si InicioDisponible < hora actual del servidor → Ajusta el inicio del espacio a la hora actual. else Si InicioDisponible < FechaInicio se ajusta a FechaInicio; en otro caso se mantiene el InicioDisponible original.; si FinOcupado > FechaFin → Ajusta el fin del espacio a FechaFin. else Mantiene el FinOcupado original.; si Turno sin cita superpuesta (CITAINICIO IS NULL) → El turno completo se considera espacio disponible. else Se generan espacios entre el fin de cada cita y el inicio de la siguiente (o el fin del turno) usando LEAD.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetRenalTherapySchedulesAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'COmmon.GETDATE', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetRenalTherapySchedulesAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AGDISPONEQUIPO; AGENSALAEQU; AGEQUIPTRA; AGEQUIPTRAD; AGENSALAC; AGASICITA', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetRenalTherapySchedulesAvailableTimes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_GetRenalTherapySchedulesAvailableTimes';
-- GO
