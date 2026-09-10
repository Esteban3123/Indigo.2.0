

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe la cédula o código del paciente y devuelve el nombre del departamento (estado/provincia) correspondiente a su dirección principal de residencia. Para obtener este dato, encadena la dirección principal del paciente con la tabla de ubicaciones, el catálogo de municipios y el catálogo de departamentos del país. Es útil en reportes, formularios y validaciones donde se necesita conocer el departamento de domicilio del paciente a partir de su identificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'patient_department';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'patient_department';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el nombre del departamento geográfico correspondiente a la dirección principal registrada de un paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_department';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener registrada una dirección marcada como principal con una ubicación válida vinculada a un municipio y departamento existentes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_department';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la dirección marcada como principal (IsMain = 1) del paciente.; El nombre del departamento se devuelve sin espacios a la derecha (RTRIM).; Si el paciente no tiene dirección principal o falta encadenamiento ubicación→municipio→departamento, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_department';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; dirección principal; departamento geográfico; municipio; ubicación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_department';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Cuando existe una dirección con IsMain = 1 para el paciente, retorna el nombre del departamento (RTRIM) resultante de unir PatientAddress → INUBICACI → INMUNICIP → INDEPARTA; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_department';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.PatientAddress; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_department';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_department';
GO
