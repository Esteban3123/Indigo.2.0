

CREATE FUNCTION [dbo].[patient_departmentCode] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN


declare @departmentCode varchar(300)
	
 SET @departmentCode  = (select Rtrim(D.depcodigo)
					from Admissions.PatientAddress A WITH(NOLOCK) 
					inner join INUBICACI B  WITH(NOLOCK) on A.IdUbication = B.ID
					inner join INMUNICIP C  WITH(NOLOCK) on B.DEPMUNCOD = C.DEPMUNCOD
					inner join INDEPARTA D  WITH(NOLOCK) on C.DEPCODIGO = D.depcodigo
					where A.IPCODPACI = @IPCODPACI and IsMain = 1
				  )

/*
Función que me va a listar el Codigo del departamento de la direccion principal del paciente.
*/

RETURN @departmentCode

END