

CREATE FUNCTION [dbo].[calcularTiempo]
(
    @Date1 Datetime,
    @Date2 Datetime,
    @TYPE VARCHAR(MAX)  -- Forma en la que se va a transformar
)                       -- 1: Formato String x horas x minutos x segundos
                        -- 2: Formato Number HH:MM:SS
 
RETURNS VARCHAR(MAX)
AS 
BEGIN
    DECLARE @temp VARCHAR(100)
    DECLARE @horas INT
    DECLARE @minutos INT
    DECLARE @tempMINUTOS INT
    DECLARE @segundos BIGINT
    
    SET @segundos = (
        SELECT
            datediff(SECOND,@Date1, @Date2) AS Segundos
    )
 
    SET @temp ='...'
 
    IF (@segundos < 3600) BEGIN
    
        SET @minutos =  FLOOR(@minutos / 60)
        SET @segundos = @minutos % 60
            --Según el tipo recibido lo formateo de una forma u otra
            IF @TYPE = 1
                SET @temp = '0 Horas ' + CONVERT(VARCHAR, @minutos) + ' Minutos ' + CONVERT(VARCHAR, @segundos) + ' Segundos'
            ELSE            
                SET @temp = '00:' + CONVERT(VARCHAR, @minutos) + ':' +  CONVERT(VARCHAR, @segundos)
    END ELSE 
BEGIN 
    SET @horas = FLOOR(@segundos / 3600)
    SET @tempMINUTOS = @segundos % 3600
    SET @minutos = FLOOR(@tempMINUTOS / 60) --MINUTOS FINALES
    SET @segundos = @tempMINUTOS % 60
        --Según el tipo recibido lo formateo de una forma u otra
        IF @TYPE = 1
            SET @temp = CONVERT(VARCHAR, @horas) + ' Horas ' + CONVERT(VARCHAR, @minutos) + ' Minutos ' + CONVERT(VARCHAR, @segundos) + ' Segundos'
        ELSE            
            SET @temp = CONVERT(VARCHAR, @horas) + ':' + CONVERT(VARCHAR, @minutos) + ':' + CONVERT(VARCHAR, @segundos) 
END 
    RETURN @temp
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la diferencia de tiempo entre dos fechas y la devuelve formateada en horas, minutos y segundos. Recibe dos fechas (@Date1 y @Date2) y un tipo de formato (@TYPE): tipo 1 retorna texto legible como ''2 Horas 30 Minutos 15 Segundos'', tipo 2 retorna formato numérico estilo ''02:30:15''. Se usa para calcular duraciones en procesos clínicos o administrativos, como tiempo de espera del paciente, duración de una atención, tiempo entre ingreso y egreso, o tiempo de respuesta en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'calcularTiempo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'calcularTiempo';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la diferencia entre dos fechas y la devuelve formateada como texto en horas/minutos/segundos, ya sea en formato descriptivo o estilo HH:MM:SS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'calcularTiempo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las dos fechas deben permitir el cálculo de DATEDIFF en segundos sin overflow.; El indicador de formato debe interpretarse como ''1'' para formato descriptivo; cualquier otro valor produce formato HH:MM:SS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'calcularTiempo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre es una cadena de texto con la duración formateada.; El formato descriptivo se aplica únicamente cuando el tipo es ''1''; en cualquier otro caso se aplica el formato con dos puntos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'calcularTiempo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Duración entre fechas; Formato de tiempo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'calcularTiempo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando la diferencia en segundos es < 3600, retorna la duración con 0 horas en formato descriptivo o ''00:MM:SS''.; [RETURN_RESULT] N/A: Cuando la diferencia en segundos es >= 3600, retorna la duración descompuesta en horas, minutos y segundos según el formato indicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'calcularTiempo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Diferencia en segundos < 3600 → Calcula sólo minutos y segundos, fijando horas en 0. else Calcula horas, minutos y segundos completos a partir de los segundos totales.; si Tipo de formato = 1 → Devuelve cadena descriptiva ''X Horas Y Minutos Z Segundos''. else Devuelve cadena en formato ''HH:MM:SS''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'calcularTiempo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'calcularTiempo';
GO
