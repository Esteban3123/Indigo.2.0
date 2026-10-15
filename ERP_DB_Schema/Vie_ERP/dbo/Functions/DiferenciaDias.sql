
create FUNCTION [dbo].[DiferenciaDias] (@Fecha as datetime)
RETURNS int
AS
BEGIN

declare @Dias int

SELECT @Dias = DATEDIFF(DAY, @Fecha, GETDATE()) + 1 


RETURN @Dias

END