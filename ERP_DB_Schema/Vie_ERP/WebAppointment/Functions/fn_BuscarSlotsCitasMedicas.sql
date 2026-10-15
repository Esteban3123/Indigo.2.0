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