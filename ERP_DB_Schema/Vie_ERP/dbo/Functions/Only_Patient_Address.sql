

CREATE FUNCTION [dbo].[Only_Patient_Address] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN


declare @Address varchar(300)
	
 SET @Address  = (select Address
					from Admissions.PatientAddress A WITH(NOLOCK) 
					where A.IPCODPACI = @IPCODPACI and IsMain = 1
				  )

/*
Función que me va a listar la direccion del paciente.
*/

RETURN @Address

END