
CREATE FUNCTION [dbo].[ClaseCama] (@CodigoClaseCama as int)
RETURNS nvarchar (30)
AS
BEGIN

declare @NomClaseCama nvarchar(30)

SELECT @NomClaseCama = CASE @CodigoClaseCama WHEN '1' THEN 'Observacion Urgencias' WHEN '2' THEN 'Recuperacion Post-Quirurgico' WHEN '3' THEN 'Hospitalaria' WHEN '4' THEN 'Cuna de Observación' END


RETURN @NomClaseCama

END