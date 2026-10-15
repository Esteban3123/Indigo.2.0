
CREATE FUNCTION [dbo].[AdmissionModalities](@Id as int)
RETURNS varchar(100)
AS
BEGIN
    
	declare @AdmissionModalities varchar(100)

	SET @AdmissionModalities = (SELECT Name FROM Admissions.AdmissionModalities WHERE Id = @Id)
	/*
	Función que me va a retornar el nombre de la modalidad de atención.
	*/

    RETURN @AdmissionModalities
END