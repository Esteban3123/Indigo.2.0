

create FUNCTION [dbo].[Quirurgicos] (@Ingreso as char(10))
RETURNS nvarchar (1000)
AS
BEGIN

declare @Quirurgicos varchar (max)
 SELECT @Quirurgicos =  COALESCE(rtrim(@Quirurgicos) + ' ;*' + rtrim(b.DESSERIPS), rtrim(b.DESSERIPS))
FROM  .HCORDPROQ as a INNER JOIN
     .INCUPSIPS AS b on b.CODSERIPS = a.CODSERIPS
	 where @Ingreso=NUMINGRES and ESTSERIPS NOT IN ('3','4') AND UFUCODIGO='N01'
RETURN @Quirurgicos

END