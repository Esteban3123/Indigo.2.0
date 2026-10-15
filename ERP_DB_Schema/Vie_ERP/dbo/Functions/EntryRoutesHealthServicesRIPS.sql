
CREATE FUNCTION [dbo].[EntryRoutesHealthServicesRIPS] (@Id as int)
RETURNS varchar (100)
AS
BEGIN


declare @EntryRoutesHealthServices varchar(100)
	
 SET @EntryRoutesHealthServices  = (SELECT Name FROM EntryRoutesHealthServices WHERE Id = @Id)
/*
Función que me va a listar la via de ingreso.
*/

RETURN @EntryRoutesHealthServices

END