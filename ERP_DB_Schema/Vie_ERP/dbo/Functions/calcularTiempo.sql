

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