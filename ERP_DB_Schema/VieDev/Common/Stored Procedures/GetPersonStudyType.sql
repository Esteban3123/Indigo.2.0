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