CREATE FUNCTION [dbo].[EntryRoutesHealthServicesRIPSCode] (@Id as int)
RETURNS varchar (5)
AS
BEGIN


declare @EntryRoutesHealthServicesRIPSCode varchar(5)
	
 SET @EntryRoutesHealthServicesRIPSCode  = (SELECT RIPSCode FROM EntryRoutesHealthServices WHERE Id = @Id)
/*
Función que me va a retornar el código RIPS.
*/

RETURN @EntryRoutesHealthServicesRIPSCode

END