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