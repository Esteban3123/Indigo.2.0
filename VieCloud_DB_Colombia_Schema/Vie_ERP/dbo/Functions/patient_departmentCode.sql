

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que retorna el código del departamento de residencia principal de un paciente, dado su código o cédula. Consulta la dirección principal registrada del paciente, la cruza con el catálogo de ubicaciones, municipios y departamentos para resolver el código departamental correspondiente. Se utiliza para identificar la región geográfica del paciente en procesos de reportería, RIPS, segmentación territorial y validaciones de cobertura. Recibe como parámetro la cédula o identificación del paciente (IPCODPACI) y devuelve el código del departamento asociado a su dirección principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'patient_departmentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'patient_departmentCode';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el código del departamento geográfico correspondiente a la dirección principal registrada de un paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_departmentCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener registrada al menos una dirección con IsMain = 1.; La ubicación de la dirección debe estar vinculada a un municipio existente y este a un departamento existente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_departmentCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la dirección marcada como principal (IsMain = 1) del paciente.; El código de departamento se devuelve sin espacios en blanco a la derecha (RTRIM).; La resolución del departamento se hace encadenando ubicación → municipio → departamento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_departmentCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; dirección principal del paciente; departamento geográfico; municipio; ubicación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_departmentCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Admissions.PatientAddress: Cuando existe dirección con IsMain = 1 para el paciente, retorna el código de departamento (RTRIM) asociado mediante ubicación-municipio-departamento; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_departmentCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.PatientAddress; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_departmentCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_departmentCode';
GO
