
CREATE FUNCTION [dbo].[EntryRoutesHealthServicesRIPS] (@Id as int)
RETURNS varchar (100)
AS
BEGIN

declare @EntryRoutesHealthServices varchar(100)
	
 SET @EntryRoutesHealthServices  = (SELECT Name FROM EntryRoutesHealthServices WHERE Id = @Id)
/*
Función que me va a listar la via de ingreso.
*/

RETURN @EntryRoutesHealthServices

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado un identificador numérico de vía de ingreso, devuelve el nombre descriptivo de esa vía de entrada al servicio de salud (por ejemplo: urgencias, consulta externa, hospitalización, remisión). Consulta el catálogo EntryRoutesHealthServices para traducir el código interno al nombre legible. Se utiliza principalmente en la generación de reportes RIPS para describir la ruta o vía de ingreso del paciente a la atención en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EntryRoutesHealthServicesRIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EntryRoutesHealthServicesRIPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve y devuelve el nombre descriptivo de la vía de ingreso del servicio de salud a partir de su identificador, para uso en reportes RIPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en EntryRoutesHealthServices con el Id proporcionado; de lo contrario, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre corresponde al campo Name del registro cuyo Id coincide con el parámetro recibido.; La función no modifica datos; es de solo lectura.; El resultado se trunca al tipo varchar(100).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Vía de ingreso; Servicios de salud; RIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.EntryRoutesHealthServices', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPS';
GO
