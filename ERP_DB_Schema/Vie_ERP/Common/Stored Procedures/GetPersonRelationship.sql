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