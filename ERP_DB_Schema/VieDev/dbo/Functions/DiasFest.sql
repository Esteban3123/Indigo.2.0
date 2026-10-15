
CREATE FUNCTION [dbo].[DiasFest] (@FechaIngreso as datetime, @FechaCita as datetime,@NumIngreso as char(10))
RETURNS nvarchar (max)
AS
BEGIN

declare @Cant nvarchar (max)

SELECT @Cant = count (diahabil)
from dbo.Tiempo as a,dbo.adingreso as b 
where b.fecha between CONVERT(VARCHAR(11),@FechaIngreso,106) and CONVERT(VARCHAR(11), @FechaCita,106) 
and diahabil='1' and @Numingreso= numingres


RETURN @Cant

END