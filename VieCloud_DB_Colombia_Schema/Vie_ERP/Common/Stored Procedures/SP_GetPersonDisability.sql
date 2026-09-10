-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonDisability]
	-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Common].[PersonDisability].[Id]
      ,[DisabilityId]
	  ,[Common].[Disability].[Description]
      ,[PersonId]
	  ,[Common].[Person].[FirstName]
      ,[Percentage]
	  FROM [Common].[PersonDisability]
	  INNER JOIN [Common].[Disability]
	  ON [Common].[PersonDisability].[DisabilityId] = [Common].[Disability].[Id]
	  INNER JOIN[Common].[Person]
	  ON [Common].[PersonDisability].[PersonId] = [Common].[Person].[Id]
	  WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las discapacidades registradas de una persona (paciente, profesional u otro actor) a partir de su número de identificación o cédula. Combina el registro de discapacidades de la persona con el catálogo de tipos de discapacidad para obtener la descripción oficial de cada una, junto con el porcentaje de afectación reconocido y el nombre de la persona. Se utiliza para visualizar el perfil de discapacidad de un individuo en el sistema, siendo útil en procesos de admisión, historia clínica y caracterización socioeconómica del paciente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonDisability';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonDisability';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta las discapacidades registradas de una persona junto con su descripción y porcentaje, identificando a la persona por su número de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en Common.Person con el número de identificación recibido; Las discapacidades de la persona deben estar referenciadas a un tipo válido en Common.Disability', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registros con coincidencia entre PersonDisability, Disability y Person (INNER JOIN), excluyendo discapacidades huérfanas o personas sin discapacidad registrada', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Discapacidad; Porcentaje de discapacidad; Identificación de persona', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.PersonDisability: Devuelve las discapacidades de la persona cuyo IdentificationNumber coincide con el parámetro, incluyendo descripción del tipo y porcentaje.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonDisability; Common.Disability; Common.Person', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonDisability';
-- GO
