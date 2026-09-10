-- =============================================
-- Author:		Emanuel Olaya
-- Create date: 30/09/2024
-- Description:	Funcion para obtener slots para citas medicas
-- =============================================
CREATE FUNCTION [WebAppointment].[fn_BuscarSlotsCitasMedicas]
(	
	@SearchAftert datetime,
    @ActivityCode varchar(50),
    @Year INT = NULL,
    @Month INT = NULL,
    @Day INT = NULL,
    @AttentionCenterCode varchar(50) = NULL,
    @DoctorCode varchar(50) = NULL,
	@AgendaCode varchar(50) = NULL
)
RETURNS TABLE 
AS
RETURN 
(
	-- Definimos la fecha actual con el desfase de zona horaria
    WITH CitasConHuecos AS (
        -- Obtenemos los huecos entre las citas y también entre los límites de la agenda
        SELECT
            AGAGEMEDC.CODAUTONU AS AgendaCode,
            GREATEST(ISNULL(LAG(AGASICITA.FECHORAFI) OVER (PARTITION BY AGAGEMEDC.CODAUTONU ORDER BY AGASICITA.FECHORAIN), AGAGEMEDC.FECHORAIN), @SearchAftert) AS GapStart,
            AGASICITA.FECHORAIN AS GapEnd
        FROM AGAGEMEDC
        LEFT JOIN AGASICITA ON AGAGEMEDC.CODAUTONU = AGASICITA.IDAGENDA
        INNER JOIN AGAGEMEDD ON AGAGEMEDC.CODAUTONU = AGAGEMEDD.CODAUTONU
        INNER JOIN AGACTIMED ON AGAGEMEDD.CODACTMED = AGACTIMED.CODACTMED
        WHERE 
            AGAGEMEDD.CODACTMED = @ActivityCode
            AND AGAGEMEDC.MOSTRARWEB = 1
            AND AGAGEMEDC.FECHORAFI > @SearchAftert
			-- AND AGASICITA.CODESTCIT IN (0, 3, 6)
            AND (@Year IS NULL OR YEAR(AGAGEMEDC.FECHORAIN) = @Year)
            AND (@Month IS NULL OR MONTH(AGAGEMEDC.FECHORAIN) = @Month)
            AND (@Day IS NULL OR DAY(AGAGEMEDC.FECHORAIN) = @Day)
            AND (@AttentionCenterCode IS NULL OR AGAGEMEDC.CODCENATE = @AttentionCenterCode)
            AND (@DoctorCode IS NULL OR AGAGEMEDC.CODPROSAL = @DoctorCode)
			AND (@AgendaCode IS NULL OR AGAGEMEDC.CODAUTONU = @AgendaCode)

        UNION

        -- Incluimos el hueco entre la última cita y el fin de la agenda
        SELECT 
            AGAGEMEDC.CODAUTONU AS AgendaCode,
            GREATEST(ISNULL(MAX(AGASICITA.FECHORAFI), AGAGEMEDC.FECHORAIN), @SearchAftert) AS GapStart,
            AGAGEMEDC.FECHORAFI AS GapEnd
        FROM AGAGEMEDC
        LEFT JOIN AGASICITA ON AGAGEMEDC.CODAUTONU = AGASICITA.IDAGENDA
        INNER JOIN AGAGEMEDD ON AGAGEMEDC.CODAUTONU = AGAGEMEDD.CODAUTONU
        INNER JOIN AGACTIMED ON AGAGEMEDD.CODACTMED = AGACTIMED.CODACTMED
        WHERE 
            AGAGEMEDD.CODACTMED = @ActivityCode
            AND AGAGEMEDC.MOSTRARWEB = 1
            AND AGAGEMEDC.FECHORAFI > @SearchAftert
			-- AND AGASICITA.CODESTCIT IN (0, 3, 6)
            AND (@Year IS NULL OR YEAR(AGAGEMEDC.FECHORAIN) = @Year)
            AND (@Month IS NULL OR MONTH(AGAGEMEDC.FECHORAIN) = @Month)
            AND (@Day IS NULL OR DAY(AGAGEMEDC.FECHORAIN) = @Day)
            AND (@AttentionCenterCode IS NULL OR AGAGEMEDC.CODCENATE = @AttentionCenterCode)
            AND (@DoctorCode IS NULL OR AGAGEMEDC.CODPROSAL = @DoctorCode)
			AND (@AgendaCode IS NULL OR AGAGEMEDC.CODAUTONU = @AgendaCode)
        GROUP BY 
            AGAGEMEDC.CODAUTONU, AGAGEMEDC.FECHORAIN, AGAGEMEDC.FECHORAFI
    ),
    HuecosFiltrados AS (
        -- Filtramos solo los huecos que son mayores o iguales a la duración de la nueva cita
        SELECT *, DATEDIFF(MINUTE, gapStart, gapEnd) AS DuracionHueco
        FROM 
            CitasConHuecos
        WHERE
            gapStart < gapEnd -- Aseguramos que el hueco tiene sentido
            AND gapStart >= @SearchAftert
            AND DATEDIFF(MINUTE, gapStart, gapEnd) >= (SELECT CONVERT(INT, DURAACTIV) FROM AGACTIMED WHERE CODACTMED = @ActivityCode) -- Solo huecos que soporten la duración de la cita
    )
    -- Devolvemos el resultado de la tabla filtrada
    SELECT *
    FROM HuecosFiltrados
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula y devuelve los slots de tiempo disponibles para agendar una cita médica a través del portal web. Analiza los bloques de agenda del médico (AGAGEMEDC) y las citas ya asignadas (AGASICITA) para identificar los huecos libres entre citas, verificando que cada hueco sea suficientemente amplio para acomodar la duración del tipo de actividad médica solicitada (AGACTIMED/AGAGEMEDD). Permite filtrar la búsqueda por tipo de actividad o procedimiento, año, mes, día específico, centro de atención, código del médico o bloque de agenda, considerando únicamente agendas habilitadas para visualización web. Es el motor de disponibilidad de turnos para el agendamiento en línea de consultas, procedimientos y controles médicos.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'FUNCTION', @level1name = N'fn_BuscarSlotsCitasMedicas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'FUNCTION', @level1name = N'fn_BuscarSlotsCitasMedicas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula los huecos disponibles dentro de las agendas médicas publicadas en web que tengan duración suficiente para alojar una nueva cita de la actividad médica indicada.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'FUNCTION', @level1name=N'fn_BuscarSlotsCitasMedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La actividad médica debe existir en AGACTIMED con una duración (DURAACTIV) convertible a INT.; Las agendas deben estar marcadas como visibles en web (MOSTRARWEB = 1).; La fecha fin de la agenda debe ser posterior a la fecha de búsqueda.; La agenda debe estar asociada a la actividad médica solicitada vía AGAGEMEDD.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'FUNCTION', @level1name=N'fn_BuscarSlotsCitasMedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran agendas con MOSTRARWEB = 1 (publicadas en portal web).; Solo se consideran agendas cuya fecha fin sea posterior a la fecha de búsqueda.; Nunca devuelve huecos cuyo inicio sea posterior o igual a su fin (gapStart < gapEnd).; Nunca devuelve huecos anteriores a la fecha de búsqueda (gapStart >= @SearchAftert).; Nunca devuelve huecos con duración menor a la duración configurada de la actividad médica.; Los huecos se calculan por agenda (particionado por CODAUTONU) en orden cronológico de citas.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'FUNCTION', @level1name=N'fn_BuscarSlotsCitasMedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agenda médica; Cita médica; Slot/hueco disponible; Actividad médica; Duración de actividad; Centro de atención; Profesional/doctor; Publicación web de agendas', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'FUNCTION', @level1name=N'fn_BuscarSlotsCitasMedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado tabular: Devuelve los huecos (GapStart, GapEnd, AgendaCode, DuracionHueco) cuya duración en minutos sea >= DURAACTIV de la actividad y cuyo GapStart sea >= a la fecha de búsqueda.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'FUNCTION', @level1name=N'fn_BuscarSlotsCitasMedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe una cita previa en la agenda (LAG(FECHORAFI) no nulo) → El inicio del hueco es el mayor entre el fin de la cita anterior y la fecha de búsqueda else El inicio del hueco es el mayor entre el inicio de la agenda (FECHORAIN) y la fecha de búsqueda; si Existen citas en la agenda (MAX(FECHORAFI) no nulo) al calcular el hueco final → El inicio del último hueco es el mayor entre el fin de la última cita y la fecha de búsqueda, y termina en FECHORAFI de la agenda else El inicio del último hueco es el mayor entre el inicio de la agenda y la fecha de búsqueda; si Cualquiera de los filtros opcionales (@Year, @Month, @Day, @AttentionCenterCode, @DoctorCode, @AgendaCode) es NULL → Se omite ese filtro y no restringe la búsqueda else Se aplica como condición de igualdad sobre la agenda', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'FUNCTION', @level1name=N'fn_BuscarSlotsCitasMedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AGAGEMEDC; AGASICITA; AGAGEMEDD; AGACTIMED', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'FUNCTION', @level1name=N'fn_BuscarSlotsCitasMedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'FUNCTION', @level1name=N'fn_BuscarSlotsCitasMedicas';
GO
