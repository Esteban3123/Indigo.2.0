CREATE FUNCTION [dbo].[EntryRoutesHealthServicesRIPSCode] (@Id as int)
RETURNS varchar (5)
AS
BEGIN

declare @EntryRoutesHealthServicesRIPSCode varchar(5)
	
 SET @EntryRoutesHealthServicesRIPSCode  = (SELECT RIPSCode FROM EntryRoutesHealthServices WHERE Id = @Id)
/*
Función que me va a retornar el código RIPS.
*/

RETURN @EntryRoutesHealthServicesRIPSCode

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que, dado el identificador interno de una vía de entrada a los servicios de salud (urgencias, consulta externa, hospitalización, remisión, entre otras), devuelve el código RIPS correspondiente a esa vía de ingreso. Consulta el catálogo de rutas de entrada (EntryRoutesHealthServices) para obtener el código requerido en los reportes al Ministerio de Salud. Se utiliza para garantizar que los registros individuales de prestación de servicios (RIPS) incluyan el código oficial de la vía de ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EntryRoutesHealthServicesRIPSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EntryRoutesHealthServicesRIPSCode';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el código RIPS asociado a una vía de ingreso de servicios de salud a partir de su identificador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPSCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en la tabla de vías de ingreso de servicios de salud cuyo identificador coincida con el valor proporcionado; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPSCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre es de tipo varchar(5).; Solo se considera un único registro coincidente por Id (clave única implícita).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPSCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Código RIPS; Vías de ingreso de servicios de salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPSCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] EntryRoutesHealthServices: Cuando se consulta por Id, retorna el RIPSCode correspondiente al registro encontrado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPSCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.EntryRoutesHealthServices', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPSCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EntryRoutesHealthServicesRIPSCode';
GO
