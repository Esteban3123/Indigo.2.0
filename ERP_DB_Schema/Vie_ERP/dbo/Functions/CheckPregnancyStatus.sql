
CREATE FUNCTION [dbo].[CheckPregnancyStatus](@IPCODPACI as varchar(25), @Ingreso as varchar(10) )

RETURNS bit
AS
BEGIN

declare @retorno as bit

IF EXISTS (SELECT TOP 1 Id FROM HCRIESGOSP WHERE IPCODPACI = @IPCODPACI and NUMINGRCES = @Ingreso and GESTACION = 1) or EXISTS (SELECT top 1 ID FROM ADTRIAGEU WHERE IPCODPACI = @IPCODPACI and NUMINGRES = @Ingreso and PregnancyStatus = 1 ORDER BY TRIAFECHA DESC)
	set @retorno  = 1 
else
	set @retorno  = 0

return @retorno  

end