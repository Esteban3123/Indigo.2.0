
CREATE FUNCTION [dbo].[TipoPoblacionSivigila] (@IdFichaSivigila as int,@CodigoTipoPoblacion as int)
RETURNS varchar (1)
AS
BEGIN

declare @TipoPoblacionMarcada varchar (1)

IF (Select COUNT(*) from HCFICHAPOBLACIONAL where IDFICHANOTIFICACION = @IdFichaSivigila AND CODPOBLACION = @CodigoTipoPoblacion) > 0 BEGIN
	set @TipoPoblacionMarcada = 'X'
END 

RETURN @TipoPoblacionMarcada 

END