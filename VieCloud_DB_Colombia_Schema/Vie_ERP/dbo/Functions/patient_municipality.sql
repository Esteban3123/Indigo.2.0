

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que retorna el nombre del municipio de residencia principal de un paciente, dado su código o cédula. Consulta la dirección marcada como principal en el registro de direcciones del paciente, la cruza con el catálogo de ubicaciones geográficas y el maestro de municipios de Colombia para obtener el nombre oficial del municipio. Se utiliza para mostrar el lugar de residencia del paciente en reportes, formularios de admisión y generación de RIPS, evitando repetir esta lógica en múltiples consultas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'patient_municipality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'patient_municipality';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el nombre del municipio de residencia principal asociado a un paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una dirección del paciente con IsMain = 1 vinculada a una ubicación válida en INUBICACI y a un municipio existente en INMUNICIP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la dirección marcada como principal (IsMain = 1) del paciente.; El nombre del municipio se devuelve sin espacios a la derecha (RTRIM).; Si el paciente no tiene dirección principal o no resuelve el join con ubicación/municipio, el resultado es NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Dirección de residencia; Municipio; Ubicación geográfica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Admissions.PatientAddress: Cuando existe una dirección del paciente con IsMain = 1, retorna el nombre del municipio (RTRIM) resultante de unir la dirección con su ubicación y el municipio correspondiente; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.PatientAddress; dbo.INUBICACI; dbo.INMUNICIP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipality';
GO
