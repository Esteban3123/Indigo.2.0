-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [Common].[GetPersonFreeTimeUse]  
-- Add the parameters for the stored procedure here  
@IdentificationNumber VARCHAR(20)
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        SELECT [Common].[PersonFreeTimeUse].[Id], 
               [Payroll].[FreeTimeUse].[Code], 
               [Payroll].[FreeTimeUse].[Name] AS FreeTimeUseName
        FROM [Payroll].[FreeTimeUse]
             INNER JOIN [Common].[PersonFreeTimeUse] ON [Payroll].[FreeTimeUse].[Id] = [Common].[PersonFreeTimeUse].[FreeTimeUseId]
             INNER JOIN [Common].[person] ON [Common].[person].[Id] = [Common].[PersonFreeTimeUse].[PersonId]
        WHERE [Common].[person].[IdentificationNumber] = @IdentificationNumber;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los usos o motivos de tiempo libre (vacaciones, licencias, permisos, compensatorios) asociados a una persona, buscándola por su número de identificación o cédula. Recibe como parámetro el número de documento del empleado o persona, cruza la tabla de personas con la relación de usos de tiempo libre y el catálogo de conceptos de nómina, y devuelve el identificador del registro, el código y el nombre de cada uso de tiempo libre asignado. Se utiliza para consultar desde nómina o recursos humanos qué conceptos de ausencia o tiempo no laborado tiene registrados un empleado específico.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonFreeTimeUse';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonFreeTimeUse';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene los usos de tiempo libre asociados a una persona identificada por su número de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una persona con el número de identificación suministrado.; Deben existir asignaciones en PersonFreeTimeUse vinculadas a esa persona y a un FreeTimeUse válido del catálogo.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven usos de tiempo libre que estén efectivamente vinculados a la persona vía PersonFreeTimeUse (INNER JOIN).; La persona se identifica unívocamente por su número de identificación.; Solo se devuelven registros donde existe correspondencia entre el catálogo de usos de tiempo libre y la asignación a la persona.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Uso de tiempo libre; Nómina; Identificación de persona', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando la persona coincide por IdentificationNumber, se retornan sus usos de tiempo libre con Id de la asignación, código y nombre del uso.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.FreeTimeUse; Common.PersonFreeTimeUse; Common.person', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonFreeTimeUse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonFreeTimeUse';
-- GO
