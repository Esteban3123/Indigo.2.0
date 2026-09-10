-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetFormFolder]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	[SelfService].[FormFolder].[Id] as FormFolderId, 
	[SelfService].[FormFolder].[Name] as FormFolderName, 
	[SelfService].[FormFolder].[Code] as FormFolderCode, 
	[SelfService].[Form].[Id] as FormId, 
	[SelfService].[Form].[Name] as FormName, 
	[SelfService].[Form].[Code] as FormCode, 
	[SelfService].[Action].[Id] as ActionId, 
	[SelfService].[Action].[Name] as ActionName, 
	[SelfService].[Action].[Code] as ActionCode 
	FROM[SelfService].[FormFolder] 
	INNER JOIN[SelfService].[Form] 
	ON[SelfService].[FormFolder].[Id] = [SelfService].[Form].[FormFolderId] 
	INNER JOIN[SelfService].[FormAction] 
	ON[SelfService].[FormAction].[FormId] = [SelfService].[Form].[Id] 
	INNER JOIN[SelfService].[Action] 
	ON[SelfService].[FormAction].[ActionId] = [SelfService].[Action].[Id];	
END
