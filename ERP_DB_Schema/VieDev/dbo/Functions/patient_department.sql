

CREATE FUNCTION [dbo].[patient_department] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN


declare @department varchar(300)
	
 SET @department  = (select Rtrim(D.nomdepart)
					from Admissions.PatientAddress A WITH(NOLOCK) 
					inner join INUBICACI B  WITH(NOLOCK) on A.IdUbication = B.ID
					inner join INMUNICIP C  WITH(NOLOCK) on B.DEPMUNCOD = C.DEPMUNCOD
					inner join INDEPARTA D  WITH(NOLOCK) on C.DEPCODIGO = D.depcodigo
					where A.IPCODPACI = @IPCODPACI and IsMain = 1
				  )

/*
Función que me va a listar el departamento de la direccion principal del paciente.
*/

RETURN @department

END