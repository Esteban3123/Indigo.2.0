

CREATE FUNCTION [dbo].[patient_municipality] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN


declare @municipality varchar(300)
	
 SET @municipality  = (select Rtrim(C.MUNNOMBRE)
					from Admissions.PatientAddress A WITH(NOLOCK) 
					inner join INUBICACI B  WITH(NOLOCK) on A.IdUbication = B.ID
					inner join INMUNICIP C  WITH(NOLOCK) on B.DEPMUNCOD = C.DEPMUNCOD
					where A.IPCODPACI = @IPCODPACI and IsMain = 1
				  )

/*
Función que me va a listar el municipio de recidencia principal del paciente.
*/

RETURN @municipality

END