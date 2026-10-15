
CREATE FUNCTION [dbo].[TipoAislamiento] (@CodigoAislamiento as int)
RETURNS nvarchar (60)
AS
BEGIN

declare @Aislamiento nvarchar(60)


--SELECT @Aislamiento = CASE @CodigoAislamiento WHEN '1' THEN 'Aerosol' WHEN '2' THEN 'Contacto' WHEN '3' THEN 'Estandar' WHEN '4' THEN 'Gota' WHEN '5' THEN 'Protector' ELSE ' ' END

	SELECT @Aislamiento  =  Nombre from CHTIPOSAISLAMIENTOS WHERE Id = @CodigoAislamiento 
	

RETURN @Aislamiento

END