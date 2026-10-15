
CREATE FUNCTION [dbo].[FactorRiesgoSivigila560] (@IdFichaSivigila as int,@CodigoAntecedentes as int)
RETURNS varchar (1)
AS
BEGIN

declare @TipoAntecedentenMarcada varchar (1)

IF (Select COUNT(*) from HCFICHARIESGOS where IDFICHANOTIFICACION = @IdFichaSivigila AND CODRIESGOCOMPLI = @CodigoAntecedentes) > 0 BEGIN
	set @TipoAntecedentenMarcada  = 'X'
END 

RETURN @TipoAntecedentenMarcada  

END