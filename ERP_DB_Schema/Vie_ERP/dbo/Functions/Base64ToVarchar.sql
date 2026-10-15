
-- =============================================
-- Author:		Juan F. Tamayo Puertas
-- Create date: 2017-01-25
-- Description:	Convierte una cadena de caracteres
--				en base 64 a su correspondiente
-- =============================================
create FUNCTION [dbo].[Base64ToVarchar] 
(
	@text varchar(max)
)
RETURNS varchar(max)
AS
BEGIN
	
	DECLARE @decoded varbinary(max)

	set @decoded = cast('' as xml).value('xs:base64Binary(sql:variable("@text"))', 'varbinary(max)')

	RETURN convert(varchar(max), @decoded)
END