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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que retorna la dirección principal completa de un paciente a partir de su cédula o código de identificación. Construye una cadena de texto concatenando la dirección registrada, el nombre del barrio o ubicación, el municipio, el departamento y el país, consultando los catálogos maestros de ubicaciones, municipios, departamentos y países. Solo considera la dirección marcada como principal (domicilio principal) del paciente. Útil para mostrar la dirección del paciente en documentos, reportes, historia clínica y comunicaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Patient_Address';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Patient_Address';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la dirección principal del paciente concatenada con su ubicación, municipio, departamento y país.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener un registro de dirección marcado como principal (IsMain = 1); Las claves de ubicación, municipio, departamento y país deben existir en sus catálogos para que el JOIN retorne datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la dirección marcada como principal (IsMain = 1); La dirección retornada siempre incluye jerarquía geográfica completa: ubicación, municipio, departamento y país; Se aplican RTRIM a los nombres de ubicación/municipio/departamento/país eliminando espacios finales; Si el paciente no tiene dirección principal o falta algún nivel jerárquico en los catálogos, el resultado es NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Dirección del paciente; Ubicación; Municipio; Departamento; País; Dirección principal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Cuando existe una dirección con IsMain = 1 para el paciente, retorna la concatenación ''Address - Ubicación - Municipio - Departamento - País''; de lo contrario retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.PatientAddress; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; Common.Country', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Address';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Address';
GO
