-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[Patient_Address] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN


declare @Address varchar(300)
	
 SET @Address  = (select Concat(Address, ' - ', Rtrim(B.UBINOMBRE), ' - ', Rtrim(C.MUNNOMBRE), ' - ', Rtrim(D.nomdepart), ' - ', Rtrim(E.Name))
					from Admissions.PatientAddress A WITH(NOLOCK) 
					inner join INUBICACI B  WITH(NOLOCK) on A.IdUbication = B.ID
					inner join INMUNICIP C  WITH(NOLOCK) on B.DEPMUNCOD = C.DEPMUNCOD
					inner join INDEPARTA D  WITH(NOLOCK) on C.DEPCODIGO = D.depcodigo
					inner join Common.Country E  WITH(NOLOCK) on D.IDPAIS = E.ID
					where A.IPCODPACI = @IPCODPACI and IsMain = 1
				  )

/*
Función que me va a listar la direccion del paciente.
*/

RETURN @Address

END