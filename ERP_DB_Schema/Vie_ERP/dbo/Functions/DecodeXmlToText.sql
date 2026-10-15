
-- =============================================
-- Author:		Juan F. Tamayo
-- Create date: 2017-02-10
-- Description:	Convierte los caracteres prohibidos
-- en el formato Xml a su correspondiente en texto normal
-- =============================================
create FUNCTION [dbo].[DecodeXmlToText] 
(
	@text varchar(max)
)
RETURNS varchar(max)
AS
BEGIN
	return replace(replace(replace(replace(replace(@text, '&quot;', '"'), '&apos;', ''''), '&amp;', '&'), '&lt;', '<'), '&gt;', '>')
END