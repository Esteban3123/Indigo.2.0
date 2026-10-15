

create FUNCTION [dbo].[Laboratorio] (@Ingreso as char(10))
RETURNS nvarchar (1000)
AS
BEGIN

declare @Laboratorio varchar (max)
 SELECT @Laboratorio =  COALESCE(rtrim(@Laboratorio) + ' ;*' + rtrim(b.DESSERIPS), rtrim(b.DESSERIPS))
FROM  .HCORDLABO as a INNER JOIN
     .INCUPSIPS AS b on b.CODSERIPS = a.CODSERIPS
	 where @Ingreso=NUMINGRES and ESTSERIPS NOT IN ('3','6','4','7') 
RETURN @Laboratorio

END