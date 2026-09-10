-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonFreeTimeUse]
-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 

	[Common].[PersonFreeTimeUse].[Id], 
	[Payroll].[FreeTimeUse].[Code], 
	[Payroll].[FreeTimeUse].[Name] as FreeTimeUseName                            
	from [Payroll].[FreeTimeUse]                        
	inner join [Common].[PersonFreeTimeUse] 
	on [Payroll].[FreeTimeUse].[Id] = [Common].[PersonFreeTimeUse].[FreeTimeUseId] 
	inner join [Common].[person] 
	on [Common].[person].[Id] = [Common].[PersonFreeTimeUse].[PersonId] 
	where [Common].[person].[IdentificationNumber] = @IdentificationNumber;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los usos o motivos de tiempo libre (vacaciones, licencias, permisos, compensatorios) registrados para una persona específica, identificada por su número de cédula o documento de identidad. Cruza el catálogo de conceptos de tiempo libre de nómina con la relación persona-tiempo libre y los datos maestros de la persona para filtrar únicamente los registros correspondientes al número de identificación recibido como parámetro. Devuelve el identificador del registro, el código y el nombre de cada uso de tiempo libre asociado a esa persona. Se utiliza para consultar el historial de ausencias o permisos de un empleado desde otros módulos del sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonFreeTimeUse';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonFreeTimeUse';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene los usos de tiempo libre asociados a una persona identificada por su número de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en Common.person con el número de identificación recibido; Deben existir registros en Common.PersonFreeTimeUse vinculados a esa persona; Cada PersonFreeTimeUse debe referenciar un FreeTimeUse válido en Payroll.FreeTimeUse', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan usos de tiempo libre que tengan correspondencia (INNER JOIN) en las tres tablas: persona, relación y catálogo; El filtrado se realiza exclusivamente por número de identificación exacto de la persona', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Uso de tiempo libre; Identificación de persona; Nómina', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve Id, Code y Name de los usos de tiempo libre de la persona cuyo IdentificationNumber coincide con el parámetro, uniendo PersonFreeTimeUse con FreeTimeUse y Person.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.FreeTimeUse; Common.PersonFreeTimeUse; Common.person', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonFreeTimeUse';
-- GO
