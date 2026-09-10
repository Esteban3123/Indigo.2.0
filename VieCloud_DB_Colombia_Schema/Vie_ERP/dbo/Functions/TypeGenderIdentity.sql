
CREATE FUNCTION [dbo].[TypeGenderIdentity] (@IdTipoIdentidadGenero AS int)
RETURNS varchar(50)
AS
BEGIN

DECLARE @NombreIdentidadGenero varchar(50) 	
 SET @NombreIdentidadGenero  = (SELECT TOP 1 Name FROM Admissions.GenderTypes WHERE Id = @IdTipoIdentidadGenero)	
RETURN @NombreIdentidadGenero

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe el identificador numérico de un tipo de identidad de género y devuelve su nombre descriptivo (por ejemplo: masculino, femenino, otro). Consulta el catálogo de tipos de género del módulo de admisiones para traducir el código interno al texto legible. Se usa para mostrar en formularios e informes el nombre del género o identidad de género del paciente a partir de su código de clasificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TypeGenderIdentity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TypeGenderIdentity';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve el nombre descriptivo de un tipo de identidad de género a partir de su identificador, consultando el catálogo de tipos de género de admisiones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TypeGenderIdentity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador recibido debe corresponder a un registro existente en el catálogo de tipos de género; de lo contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TypeGenderIdentity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve únicamente un nombre (TOP 1) del catálogo de tipos de género para el identificador recibido.; Si el identificador no existe en el catálogo, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TypeGenderIdentity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Identidad de género; Catálogo de tipos de género; Admisiones', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TypeGenderIdentity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Admissions.GenderTypes: Cuando existe un registro en Admissions.GenderTypes cuyo Id coincide con el parámetro, retorna el campo Name (primer match); en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TypeGenderIdentity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.GenderTypes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TypeGenderIdentity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TypeGenderIdentity';
GO
