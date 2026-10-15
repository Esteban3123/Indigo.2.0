

create FUNCTION [dbo].[Imagenes] (@Ingreso as char(10))
RETURNS nvarchar (1000)
AS
BEGIN

declare @Imagenes varchar (max)
 SELECT @Imagenes =  COALESCE(rtrim(@Imagenes) + ' ;*' + rtrim(b.DESSERIPS), rtrim(b.DESSERIPS))
FROM  .HCORDIMAG as a INNER JOIN
     .INCUPSIPS AS b on b.CODSERIPS = a.CODSERIPS
	 where @Ingreso=NUMINGRES and ESTSERIPS NOT IN ('3','4','6','7') 
RETURN @Imagenes

END