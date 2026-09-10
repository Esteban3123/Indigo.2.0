

CREATE FUNCTION [dbo].[Especialidades] (@Especialidad as nvarchar(3))
RETURNS nvarchar (60)
AS
BEGIN

declare @NomEspecialidad nvarchar(60)

SELECT @NomEspecialidad=DESESPECI FROM INESPECIA WHERE CODESPECI=@Especialidad

RETURN @NomEspecialidad

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe el código de una especialidad médica y devuelve su nombre o descripción completa. Consulta el catálogo maestro de especialidades (INESPECIA) para traducir códigos internos de especialidad a texto legible por el usuario. Se usa típicamente en reportes, listados y pantallas donde se necesita mostrar el nombre de la especialidad en lugar de su código, por ejemplo en agendamientos, historias clínicas o admisiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Especialidades';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Especialidades';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve la descripción/nombre de una especialidad médica a partir de su código consultando el catálogo de especialidades.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Especialidades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el catálogo de especialidades con el código buscado para obtener una descripción no nula', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Especialidades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve NULL si no existe el código de especialidad en el catálogo; Resuelve un único nombre de especialidad por código (asume unicidad del código en el catálogo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Especialidades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Especialidad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Especialidades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INESPECIA: Cuando CODESPECI coincide con el código recibido, retorna DESESPECI; si no hay coincidencia, retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Especialidades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Especialidades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Especialidades';
GO
