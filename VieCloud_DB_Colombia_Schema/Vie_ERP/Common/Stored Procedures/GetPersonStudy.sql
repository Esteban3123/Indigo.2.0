-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [Common].[GetPersonStudy]   
-- Add the parameters for the stored procedure here  
@IdentificationNumber VARCHAR(20)
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        SELECT [Common].[PersonStudy].[Id], 
               [Common].[PersonStudy].[Name] AS PersonStudyName, 
               [Payroll].[StudyType].[Name] AS PersonStudyTypeName
        FROM [Common].[PersonStudy]
             INNER JOIN [Common].[Person] ON [Common].[PersonStudy].[PersonId] = [Common].[Person].[Id]
             INNER JOIN [Payroll].[StudyType] ON [Payroll].[StudyType].[Id] = [Common].[PersonStudy].[StudyTypeId]
        WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y retorna los estudios académicos y de formación registrados para una persona (empleado o profesional de la salud), identificada por su número de cédula o documento. Combina los datos de estudios de la persona con el tipo o nivel educativo correspondiente (bachillerato, técnico, universitario, posgrado, etc.), permitiendo conocer el historial formativo y los títulos obtenidos. Se utiliza en procesos de recursos humanos, verificación de credenciales y gestión de personal, recibiendo como parámetro el número de identificación del individuo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonStudy';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonStudy';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los estudios académicos de una persona identificada por su número de documento, retornando el nombre del estudio y el tipo de formación asociado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en Common.Person con el IdentificationNumber recibido; El estudio debe tener un PersonId válido vinculado a Common.Person; El estudio debe tener un StudyTypeId válido vinculado a Payroll.StudyType', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna estudios cuyas relaciones con Person y StudyType existen (INNER JOIN excluye huérfanos); Filtra estrictamente por número de identificación exacto, sin transformación', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Estudio académico de la persona; Tipo de estudio/formación; Número de identificación', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.PersonStudy: Cuando Person.IdentificationNumber coincide con el parámetro, retorna los estudios de la persona junto con el nombre del tipo de estudio', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonStudy; Common.Person; Payroll.StudyType', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudy';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudy';
-- GO
