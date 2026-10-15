CREATE FUNCTION [dbo].[AdmissionModalitiesCodeRIPS] (@Id as int)
RETURNS varchar(100)
AS
BEGIN
    
	declare @CodeRIPS varchar(100)

	SET @CodeRIPS = (SELECT CodeRIPS FROM Admissions.AdmissionModalities WHERE Id = @Id)
	/*
	Función que me va a retornar el código RIPS de la modalidad de atención.
	*/

    RETURN @CodeRIPS
END