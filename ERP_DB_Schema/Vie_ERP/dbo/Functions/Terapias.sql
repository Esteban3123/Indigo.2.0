
create FUNCTION [dbo].[Terapias] (@Ingreso as char(10))
RETURNS nvarchar (1000)
AS
BEGIN

declare @Terapias varchar (max)
 SELECT @Terapias =  COALESCE(rtrim(@Terapias) + ' ;*' + rtrim(b.DESSERIPS), rtrim(b.DESSERIPS))
FROM  .HCORDPRON as a INNER JOIN
     .INCUPSIPS AS b on b.CODSERIPS = a.CODSERIPS
	 where @Ingreso=NUMINGRES and ESTSERIPS NOT IN ('4') 
RETURN @Terapias

END