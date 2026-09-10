-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [Common].[GetPersonRelationship]  
-- Add the parameters for the stored procedure here  
@IdentificationNumber VARCHAR(20)
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        SELECT [Payroll].[Relationship].[Id], 
               [Payroll].[Kinship].[Name] AS KinshipName, 
               [Payroll].[Relationship].[Name], 
               [DependsValue]
        FROM [Payroll].[Relationship]
             INNER JOIN [Payroll].[Employee] ON [Payroll].[Employee].[Id] = [Payroll].[Relationship].[EmployeeId]
             INNER JOIN [Payroll].[Kinship] ON [Payroll].[Kinship].[Id] = [Payroll].[Relationship].[KinshipId]
             INNER JOIN [Common].[ThirdParty] ON [Common].[ThirdParty].[Id] = [Payroll].[Employee].[ThirdPartyId]
             INNER JOIN [Common].[Person] ON [Common].[Person].[Id] = [Common].[ThirdParty].[PersonId]
        WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los familiares y beneficiarios a cargo de un empleado a partir de su número de identificación (cédula o documento). Recibe el número de identificación como parámetro, lo cruza con la persona registrada en el sistema, llega hasta el empleado de nómina correspondiente y retorna todos sus vínculos familiares: el identificador del beneficiario, el tipo de parentesco (cónyuge, hijo, padre, etc.), el nombre del familiar y el valor o porcentaje del beneficio que genera. Es utilizado para consultar la carga familiar de un empleado en procesos de nómina, beneficios o deducciones.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonRelationship';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonRelationship';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los familiares/beneficiarios registrados de un empleado a partir del número de identificación de la persona, devolviendo el parentesco y si genera dependencia.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una Persona con el número de identificación recibido.; La Persona debe estar enlazada a un ThirdParty y este a un Employee con relaciones familiares registradas para que se devuelvan filas.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan relaciones familiares cuyo empleado esté vinculado a un tercero y este a su vez a una persona registrada (encadenamiento obligatorio mediante INNER JOIN).; La identificación se busca a nivel de Persona, no del empleado directamente.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Parentesco; Familiar/Beneficiario; Tercero; Persona; Identificación', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Relationship: Cuando Common.Person.IdentificationNumber coincide con el parámetro, se retorna el listado de relaciones familiares (Id, nombre del parentesco, nombre y DependsValue) del empleado asociado a esa persona vía ThirdParty.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Relationship; Payroll.Employee; Payroll.Kinship; Common.ThirdParty; Common.Person', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationship';
-- GO
