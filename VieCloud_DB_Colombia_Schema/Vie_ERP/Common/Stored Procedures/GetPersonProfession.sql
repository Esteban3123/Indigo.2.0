-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [Common].[GetPersonProfession]  
-- Add the parameters for the stored procedure here  
@IdentificationNumber INT
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        SELECT [Common].[PersonProfession].[Id], 
               [Payroll].[Profession].[Name]
        FROM [Common].[PersonProfession]
             INNER JOIN [Common].[Person] ON [Common].[Person].[Id] = [Common].[PersonProfession].[PersonId]
             INNER JOIN [Payroll].[Profession] ON [Payroll].[Profession].[Id] = [Common].[PersonProfession].[IdProfession]
        WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las profesiones u ocupaciones registradas para una persona (profesional de la salud o empleado) a partir de su número de identificación o cédula. Recibe el número de documento como parámetro, busca a la persona en el registro maestro de personas, y retorna el identificador del vínculo y el nombre de cada profesión asociada según el catálogo de profesiones de nómina. Se utiliza para conocer la formación académica o perfil profesional de un individuo, por ejemplo al validar habilitaciones, asignar roles clínicos o gestionar contratación de personal.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonProfession';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonProfession';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene las profesiones registradas de una persona identificándola por su número de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en Common.Person con el número de identificación indicado; Debe existir relación en Common.PersonProfession asociada a la persona; La profesión referenciada debe existir en Payroll.Profession', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna profesiones cuya persona, relación y profesión existan simultáneamente (INNER JOIN); El filtro siempre se aplica por número de identificación de la persona', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Profesión; Identificación de persona; Relación persona-profesión', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.PersonProfession: Retorna Id de PersonProfession y Name de la profesión cuando Common.Person.IdentificationNumber coincide con el parámetro de entrada', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonProfession; Common.Person; Payroll.Profession', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonProfession';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonProfession';
-- GO
