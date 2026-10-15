
CREATE FUNCTION [dbo].[Enterby] (@Id as int)
RETURNS varchar (300)
AS
BEGIN


declare @Enterby varchar(300)
	
 SET @Enterby  = (SELECT Name FROM EntryRoutesHealthServices WHERE Id = @Id)
/*
Función que me va a listar el campo ingresa por.
*/

RETURN @Enterby

END