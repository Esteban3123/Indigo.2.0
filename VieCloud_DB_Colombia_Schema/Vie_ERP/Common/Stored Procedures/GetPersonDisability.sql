-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [Common].[GetPersonDisability]  
-- Add the parameters for the stored procedure here  
@IdentificationNumber VARCHAR(20)
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        SELECT [Common].[PersonDisability].[Id], 
               [DisabilityId], 
               [Common].[Disability].[Description], 
               [PersonId], 
               [Common].[Person].[FirstName], 
               [Percentage]
        FROM [Common].[PersonDisability]
             INNER JOIN [Common].[Disability] ON [Common].[PersonDisability].[DisabilityId] = [Common].[Disability].[Id]
             INNER JOIN [Common].[Person] ON [Common].[PersonDisability].[PersonId] = [Common].[Person].[Id]
        WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las discapacidades registradas de una persona (paciente u otro actor del sistema) a partir de su número de identificación o cédula. Combina el registro de discapacidades asociadas a la persona, el catálogo de tipos de discapacidad y los datos básicos de la persona para devolver el detalle de cada discapacidad: tipo, descripción oficial, nombre de la persona y porcentaje de afectación reconocido. Se utiliza para consultar el perfil de discapacidad de un paciente o profesional en el EHR, por ejemplo al gestionar admisiones, historia clínica o beneficios según condición de discapacidad.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonDisability';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonDisability';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta las discapacidades registradas de una persona junto con su tipo y porcentaje de afectación, identificándola por su número de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en el registro maestro y estar identificada por un número de identificación válido; Deben existir registros de discapacidad asociados a la persona para retornar filas; Cada registro de discapacidad debe referenciar un tipo de discapacidad existente en el catálogo', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan discapacidades cuyo tipo exista en el catálogo Disability (INNER JOIN excluye huérfanos); Solo se retornan discapacidades cuya persona exista en Person (INNER JOIN excluye huérfanos); El filtrado se realiza siempre por número de identificación de la persona, no por su Id interno', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Discapacidad; Porcentaje de discapacidad; Identificación de persona; Catálogo de discapacidades', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando la persona coincide por IdentificationNumber y tiene discapacidades registradas, retorna las discapacidades con su descripción y porcentaje vía INNER JOIN entre PersonDisability, Disability y Person', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonDisability; Common.Disability; Common.Person', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDisability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonDisability';
-- GO
