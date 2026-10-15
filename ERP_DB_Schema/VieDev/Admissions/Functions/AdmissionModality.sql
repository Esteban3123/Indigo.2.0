
CREATE FUNCTION [Admissions].[AdmissionModality] (@Id as int)
RETURNS varchar (300)
AS
BEGIN


declare @AdmissionModality varchar(300)
	
 SET @AdmissionModality  = (SELECT Name FROM Admissions.AdmissionModalities WHERE Id = @Id)
/*
Función que me va a listar la modalidad de atención.
*/

RETURN @AdmissionModality

END