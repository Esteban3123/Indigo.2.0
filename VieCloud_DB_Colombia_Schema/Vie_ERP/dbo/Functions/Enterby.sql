
CREATE FUNCTION [dbo].[Enterby] (@Id as int)
RETURNS varchar (300)
AS
BEGIN

declare @Enterby varchar(300)
	
 SET @Enterby  = (SELECT Name FROM EntryRoutesHealthServices WHERE Id = @Id)
/*
Función que me va a listar el campo ingresa por.
*/

RETURN @Enterby

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe el identificador numérico de una vía de ingreso y devuelve su nombre descriptivo consultando el catálogo de rutas de entrada a los servicios de salud. Se utiliza para resolver el texto legible del campo ''ingresa por'' (por ejemplo: urgencias, consulta externa, hospitalización, remisión) a partir de su código interno. Es útil en reportes, vistas e interfaces donde se necesita mostrar la forma en que el paciente accedió al servicio de salud sin hacer un JOIN explícito a la tabla EntryRoutesHealthServices.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Enterby';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Enterby';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve el nombre descriptivo de la vía de ingreso a servicios de salud a partir de su identificador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Enterby';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en EntryRoutesHealthServices cuyo Id coincida con el parámetro; de lo contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Enterby';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna un único valor (asume unicidad de Id en EntryRoutesHealthServices).; No modifica datos; función de solo lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Enterby';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Vía de ingreso a servicios de salud; Ingresa por', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Enterby';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] EntryRoutesHealthServices: Cuando Id = @Id, retorna el valor de Name como varchar(300).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Enterby';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.EntryRoutesHealthServices', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Enterby';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Enterby';
GO
