-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [Common].[GetPersonDiagnosedDesease]  
-- Add the parameters for the stored procedure here  
@IdentificationNumber VARCHAR(20)
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;
        SELECT [Common].[PersonDiagnosedDisease].[Id], 
               [Payroll].[DiagnosedDisease].[Name] AS DiagnosedDiseaseName, 
               [Payroll].[DiagnosedDisease].[Code]
        FROM [Common].[PersonDiagnosedDisease]
             INNER JOIN [Common].[Person] ON [Common].[Person].[Id] = [Common].[PersonDiagnosedDisease].[PersonId]
             INNER JOIN [Payroll].[DiagnosedDisease] ON [Common].[PersonDiagnosedDisease].[DiagnosedDiseaseId] = [Payroll].[DiagnosedDisease].[Id]
        WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber;
    END;