-- =============================================
-- Author: Emanuel Olaya Penagos
-- Create Date: 01/10/2024
-- Description:
-- Este procedimiento se encarga de agendar una cita médica en la tabla AGASICITA. 
-- Primero, verifica si el horario propuesto para la cita entra en conflicto con otras citas ya existentes.
-- Si no hay conflicto, se agenda la cita en el horario solicitado.
-- En caso de que el horario propuesto esté ocupado, el procedimiento busca el siguiente hueco disponible
-- y agenda la cita en ese nuevo horario.
-- =============================================
CREATE PROCEDURE [WebAppointment].[AgendarCita]
    @AgendaCode VARCHAR(50),
    @ActivityCode VARCHAR(50),
    @StartDatetime DATETIME,
    @PatientCode VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @ActivityDuration INT;
    DECLARE @EndDateTime DATETIME;
    DECLARE @AvailableStartDatetime DATETIME;

    -- Obtener la duración de la actividad
    SELECT @ActivityDuration = DURAACTIV
    FROM AGACTIMED 
    WHERE CODACTMED = @ActivityCode;

    -- Calcular la hora de finalización de la cita
    SET @EndDateTime = DATEADD(MINUTE, @ActivityDuration, @StartDatetime);

    -- Validar si existe una cita que interfiera con la nueva cita
    IF EXISTS (
        SELECT 1 
        FROM AGASICITA 
        WHERE AGASICITA.IDAGENDA = @AgendaCode AND AGASICITA.CODESTCIT IN (0, 3, 6)
          AND (
              (@StartDatetime >= AGASICITA.FECHORAIN AND @StartDatetime < AGASICITA.FECHORAFI)
              OR (@EndDateTime > AGASICITA.FECHORAIN AND @EndDateTime < AGASICITA.FECHORAFI)
          )
    )
    BEGIN
        -- Si existe interferencia, buscar la siguiente hora disponible
        SELECT TOP 1 @AvailableStartDatetime = GapStart
        FROM WebAppointment.fn_BuscarSlotsCitasMedicas(
            --SWITCHOFFSET(SYSDATETIMEOFFSET(), '-05:00'),
			@StartDatetime,
            @ActivityCode,
            NULL, -- Year
            NULL, -- Month
            NULL, -- Day
            NULL, -- AttentionCenterCode
            NULL, -- DoctorCode
            @AgendaCode
        );

        -- Si no se encuentra una nueva hora disponible, retornar NULL
        IF @AvailableStartDatetime IS NULL
        BEGIN
            SELECT NULL AS NoDisponible;
            RETURN;
        END

        -- Actualizar @StartDatetime y recalcular @EndDateTime con la nueva hora disponible
        SET @StartDatetime = @AvailableStartDatetime;
        SET @EndDateTime = DATEADD(MINUTE, @ActivityDuration, @StartDatetime);
    END

    -- Insertar la nueva cita en AGASICITA
    INSERT INTO AGASICITA (
        CODESPECI, CODCENATE, CODPROSAL, CODIGOCON, ESTENVSMS, CODUSUASI, 
        CODSERIPS, CODACTMED, IDAGENDA, IPCODPACI, GENCAREGROUP, GENCONENTITY,
        MODALIDAD, CODTIPSOL, CODTIPCIT, CODESTCIT, CITAEXTRA, TIPSOLICITU, 
        RELCITAINGRE, CONFASIST, FECHORAIN, FECHORAFI, FECREGSIS, FECHAOFERTADA, FECITADES
    )
    SELECT 
        -- Datos de AGAGEMEDC y AGAGEMEDD
        AGAGEMEDD.CODESPECI, AGAGEMEDC.CODCENATE, AGAGEMEDC.CODPROSAL, 
        AGAGEMEDC.CODIGOCON, AGPARAMET.NOTIFISMS, AGPARAMET.DefaultUserWebAppointment, 
        AGACTIMED.CODSERIPS, AGACTIMED.CODACTMED, AGAGEMEDC.CODAUTONU, 
        -- Datos del paciente desde INPACIENT
        INPACIENT.IPCODPACI, INPACIENT.GENCAREGROUP, INPACIENT.GENCONENTITY, 
        -- Valores estáticos
        0, 0, 3, 6, 0, 1, 0, 3, 
        @StartDatetime, @EndDateTime, SWITCHOFFSET(SYSDATETIMEOFFSET(), '-05:00'), 
        DATETRUNC(day, @StartDatetime), NULL
    FROM AGAGEMEDC
    INNER JOIN AGAGEMEDD ON AGAGEMEDC.CODAUTONU = AGAGEMEDD.CODAUTONU
    INNER JOIN AGPARAMET ON AGPARAMET.CODCENATE = AGAGEMEDC.CODCENATE
    INNER JOIN AGACTIMED ON AGACTIMED.CODACTMED = AGAGEMEDD.CODACTMED
    CROSS JOIN INPACIENT
    WHERE AGAGEMEDD.CODAUTONU = @AgendaCode
      AND AGAGEMEDD.CODACTMED = @ActivityCode
      AND INPACIENT.IPCODPACI = @PatientCode;

	DECLARE @NuevoCodigoAgenda INT;
    SET @NuevoCodigoAgenda = SCOPE_IDENTITY();

	select @NuevoCodigoAgenda
END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agenda una cita médica para un paciente en el sistema de agendamiento (tabla AGASICITA). Primero verifica si el horario solicitado está disponible para la agenda del profesional; si hay conflicto con una cita existente, busca automáticamente el siguiente slot libre usando la función fn_BuscarSlotsCitasMedicas y reprograma la cita en ese horario. Registra la cita con todos los datos del médico, centro de atención, contrato, actividad médica (CUPS) y datos del paciente (cédula/código), retornando el identificador de la nueva cita creada. Se usa en el portal de agendamiento web para permitir que pacientes o asesores reserven citas médicas sin solapamientos de horario.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'PROCEDURE', @level1name = N'AgendarCita';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'PROCEDURE', @level1name = N'AgendarCita';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Agenda una cita médica validando solapamientos con citas existentes y, si hay conflicto, la reubica automáticamente en el siguiente slot disponible.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'AgendarCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La actividad médica debe existir en AGACTIMED para obtener su duración; Debe existir la agenda (CODAUTONU) en AGAGEMEDC/AGAGEMEDD con la actividad solicitada; Debe existir el paciente en INPACIENT; Debe existir parametrización en AGPARAMET para el centro de atención de la agenda', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'AgendarCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La duración de la cita siempre se calcula como DURAACTIV minutos sumados al inicio (EndDateTime = StartDatetime + duración); Solo se consideran como bloqueantes para solapamiento las citas con estado 0, 3 o 6; Las citas creadas se registran con estado 6 y tipo 3 por defecto; La fecha de registro se almacena con offset horario -05:00; Nunca se inserta una cita si existe solapamiento y no hay slot alternativo disponible', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'AgendarCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cita médica; agenda médica; actividad médica; paciente; especialidad; centro de atención; profesional de salud; contrato; slot de disponibilidad; notificación SMS; servicio IPS', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'AgendarCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] AGASICITA: Si no hay solapamiento (o se encontró un nuevo slot), inserta la cita con CODTIPCIT=3, CODESTCIT=6, MODALIDAD=0, CODTIPSOL=0, CITAEXTRA=0, TIPSOLICITU=1, CONFASIST=3, RELCITAINGRE=0, FECREGSIS=hora actual con offset -05:00 y FECHAOFERTADA truncada al día del inicio.; [RETURN_RESULT] Result: Si se detecta conflicto y fn_BuscarSlotsCitasMedicas no retorna slot disponible, devuelve un resultset con NULL como NoDisponible y termina sin insertar.; [RETURN_RESULT] Result: Tras insertar, retorna el identificador autogenerado (SCOPE_IDENTITY) de la cita creada.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'AgendarCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe en AGASICITA una cita de la misma agenda con CODESTCIT IN (0,3,6) cuyo rango [FECHORAIN, FECHORAFI) se solapa con [@StartDatetime, @EndDateTime) → Buscar siguiente slot libre vía fn_BuscarSlotsCitasMedicas y reasignar el inicio/fin de la cita else Conservar el horario solicitado e insertar; si No se encontró un slot disponible al reubicar → Retornar NULL (NoDisponible) y abortar el agendamiento else Continuar con el INSERT en la nueva hora', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'AgendarCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'WebAppointment.fn_BuscarSlotsCitasMedicas', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'AgendarCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AGACTIMED; AGASICITA; AGAGEMEDC; AGAGEMEDD; AGPARAMET; INPACIENT; WebAppointment.fn_BuscarSlotsCitasMedicas', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'AgendarCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'AgendarCita';
-- GO
