
CREATE FUNCTION [dbo].[patient_municipalityCode] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN


declare @municipalityCode varchar(300)
	
 SET @municipalityCode  = (select (C.MUNCODIGO)
							from Admissions.PatientAddress A WITH(NOLOCK) 
							inner join INUBICACI B  WITH(NOLOCK) on A.IdUbication = B.ID
							inner join INMUNICIP C  WITH(NOLOCK) on B.DEPMUNCOD = C.DEPMUNCOD
							where A.IPCODPACI = @IPCODPACI and IsMain = 1
						  )

/*
Función que me va a listar el codigo del municipio de la direccion principal del paciente.
*/

RETURN @municipalityCode

END