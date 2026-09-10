
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que retorna el código del municipio correspondiente a la dirección principal registrada de un paciente, identificado por su cédula o código de paciente. Combina la dirección principal del paciente (PatientAddress), la tabla de ubicaciones geográficas (INUBICACI) y el catálogo maestro de municipios de Colombia (INMUNICIP) para obtener el código oficial del municipio de residencia. Se utiliza para procesos de reportería, RIPS, facturación y cualquier consulta que requiera conocer el municipio de domicilio del paciente a partir de su identificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'patient_municipalityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'patient_municipalityCode';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el código de municipio asociado a la dirección principal registrada del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipalityCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener al menos una dirección con IsMain = 1 en Admissions.PatientAddress para obtener resultado.; La ubicación referenciada debe existir en INUBICACI y su DEPMUNCOD debe corresponder a un registro en INMUNICIP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipalityCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la dirección marcada como principal (IsMain = 1) del paciente.; El código del municipio se obtiene cruzando ubicación → municipio mediante DEPMUNCOD.; Si el paciente no tiene dirección principal registrada, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipalityCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Dirección principal del paciente; Municipio; Ubicación geográfica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipalityCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Admissions.PatientAddress: Cuando existe una dirección con IsMain=1 para el paciente, retorna MUNCODIGO del municipio vinculado vía INUBICACI/INMUNICIP; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipalityCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.PatientAddress; dbo.INUBICACI; dbo.INMUNICIP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipalityCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_municipalityCode';
GO
