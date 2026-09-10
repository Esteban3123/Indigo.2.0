-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[patient_DepartmentAndMunicipality] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN

declare @municipality varchar(300)
	
 SET @municipality  = (select (C.DEPMUNCOD)
							from Admissions.PatientAddress A WITH(NOLOCK) 
							inner join INUBICACI B  WITH(NOLOCK) on A.IdUbication = B.ID
							inner join INMUNICIP C  WITH(NOLOCK) on B.DEPMUNCOD = C.DEPMUNCOD
							where A.IPCODPACI = @IPCODPACI and IsMain = 1
						  )

/*
Función que me va a listar el codigo del municipio de la direccion principal del paciente.
*/

RETURN @municipality
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado el código o cédula de un paciente, retorna el código del municipio correspondiente a su dirección principal de residencia. Consulta la dirección marcada como principal del paciente, la relaciona con el catálogo de ubicaciones y luego con el catálogo maestro de municipios de Colombia para obtener el código compuesto departamento-municipio. Es útil para reportes de georeferenciación, RIPS, estadísticas de cobertura geográfica y cualquier proceso que requiera conocer el municipio y departamento de domicilio del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'patient_DepartmentAndMunicipality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'patient_DepartmentAndMunicipality';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el código de departamento-municipio (DEPMUNCOD) correspondiente a la dirección principal registrada del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_DepartmentAndMunicipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir con al menos una dirección marcada como principal (IsMain = 1) en Admissions.PatientAddress.; La ubicación asociada (IdUbication) debe existir en INUBICACI y su DEPMUNCOD debe estar en el catálogo INMUNICIP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_DepartmentAndMunicipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la dirección marcada como principal (IsMain = 1) del paciente.; El código de municipio retornado proviene del catálogo INMUNICIP, garantizando consistencia referencial vía INUBICACI.; Si el paciente no tiene dirección principal registrada o no se resuelve la ubicación/municipio, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_DepartmentAndMunicipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; dirección principal del paciente; municipio; departamento; ubicación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_DepartmentAndMunicipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar varchar(300)): Cuando existe una PatientAddress con IsMain = 1 para el paciente, se retorna el DEPMUNCOD del municipio asociado mediante INUBICACI → INMUNICIP; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_DepartmentAndMunicipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.PatientAddress; dbo.INUBICACI; dbo.INMUNICIP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_DepartmentAndMunicipality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'patient_DepartmentAndMunicipality';
GO
