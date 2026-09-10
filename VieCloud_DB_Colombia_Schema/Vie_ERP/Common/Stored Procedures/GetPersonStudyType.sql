-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [Common].[GetPersonStudyType]   
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
        WHERE [Common].[Person].[IdentificationNumber] = '11436094';
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los estudios académicos y tipos de formación registrados para una persona, identificada por su número de cédula o documento. Combina la información de estudios individuales (títulos, nombres del estudio) con la clasificación del nivel educativo (bachillerato, técnico, universitario, posgrado, etc.) definida en la tabla de tipos de estudio de nómina. Se utiliza en procesos de recursos humanos para verificar el perfil educativo de empleados o profesionales de la salud. Nota: aunque recibe el parámetro @IdentificationNumber, el filtro en el código está fijo con un número de identificación hardcodeado, lo que puede ser un error de implementación pendiente de corregir.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonStudyType';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonStudyType';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los estudios académicos y su tipo de formación asociados a una persona identificada por su número de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la persona en Common.Person con el IdentificationNumber buscado; La persona debe tener registros en Common.PersonStudy con StudyTypeId válido en Payroll.StudyType', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna estudios de personas existentes (INNER JOIN con Person); Solo retorna estudios cuyo tipo está catalogado en StudyType (INNER JOIN); El filtro de identificación está hardcodeado al valor ''11436094'', ignorando el parámetro de entrada', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Estudios académicos de la persona; Tipo de estudio/formación académica; Número de identificación', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.PersonStudy: Devuelve Id, nombre del estudio y nombre del tipo de estudio para los registros cuya persona coincide con el IdentificationNumber filtrado', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonStudy; Common.Person; Payroll.StudyType', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudyType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonStudyType';
-- GO
