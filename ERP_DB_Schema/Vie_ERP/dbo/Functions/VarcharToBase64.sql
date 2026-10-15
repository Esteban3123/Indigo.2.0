
-- =============================================
-- Author:		Juan F. Tamayo Puertas
-- Create date: 2017-01-25
-- Description:	Convierte una cadena de caracteres
--				a su correspondiente en base 64
-- =============================================
create FUNCTION [dbo].[VarcharToBase64] 
(
	@text varchar(max)
)
RETURNS varchar(max)
AS
BEGIN
	
	DECLARE @encoded varchar(max), @source varbinary(max)

	set @source = convert(varbinary(max), @text)

	set @encoded = cast('' as xml).value('xs:base64Binary(sql:variable("@source"))', 'varchar(max)')

	RETURN @encoded
END