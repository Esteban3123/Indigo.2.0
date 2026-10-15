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