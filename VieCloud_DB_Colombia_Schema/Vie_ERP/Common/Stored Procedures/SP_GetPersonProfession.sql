-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonProfession]
	-- Add the parameters for the stored procedure here
	@IdentificationNumber int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Common].[PersonProfession].[Id]      
	,[Payroll].[Profession].[Name]
	FROM [Common].[PersonProfession]
	INNER JOIN [Common].[Person]
	ON [Common].[Person].[Id] = [Common].[PersonProfession].[PersonId]
	INNER JOIN [Payroll].[Profession]
	ON [Payroll].[Profession].[Id] = [Common].[PersonProfession].[IdProfession]
	Where [Common].[Person].[IdentificationNumber] = @IdentificationNumber

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las profesiones u ocupaciones registradas de un profesional de la salud o empleado, a partir de su número de documento (cédula o identificación). Recibe el número de identificación como parámetro, busca a la persona en el registro maestro, cruza con la relación de profesiones asignadas y retorna el identificador del vínculo junto con el nombre de cada profesión según el catálogo de nómina. Se utiliza para conocer la formación académica o perfil profesional de una persona registrada en el sistema, por ejemplo al validar habilitaciones de un profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonProfession';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonProfession';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene las profesiones registradas de una persona identificada por su número de identificación, devolviendo el id de la relación y el nombre de cada profesión.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en Common.Person con el número de identificación proporcionado; Debe existir al menos un vínculo en Common.PersonProfession con una profesión válida en Payroll.Profession para retornar resultados', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna profesiones que tengan correspondencia tanto en Common.Person como en Payroll.Profession (INNER JOIN); El filtro se aplica exclusivamente por número de identificación de la persona', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Profesión; Identificación de persona; Profesional de la salud / nómina', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.PersonProfession: Cuando Person.IdentificationNumber coincide con el parámetro, se retornan las profesiones asociadas vía INNER JOIN con Person y Profession', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonProfession; Common.Person; Payroll.Profession', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonProfession';
-- GO
