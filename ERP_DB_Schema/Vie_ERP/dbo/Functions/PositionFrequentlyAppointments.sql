


CREATE FUNCTION [dbo].[PositionFrequentlyAppointments] (@Codigo varchar(10), @IDAgrupacion varchar(100))
RETURNS varchar (10)
AS
BEGIN

DECLARE @Resultado VARCHAR(10)

    IF @IDAgrupacion IS NULL
        RETURN NULL

    ;WITH Citas AS (
        SELECT 
            CODAUTONU,
            FECHORAIN,
            Posicion = ROW_NUMBER() OVER (PARTITION BY IdFrequentlyAppointment ORDER BY FECHORAIN),
            Total = COUNT(*) OVER (PARTITION BY IdFrequentlyAppointment)
        FROM AGASICITA
        WHERE IdFrequentlyAppointment = @IDAgrupacion
    )
    SELECT @Resultado = CAST(Posicion AS VARCHAR) + '/' + CAST(Total AS VARCHAR)
    FROM Citas
    WHERE CODAUTONU = @Codigo

    RETURN @Resultado

END