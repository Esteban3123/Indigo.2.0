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