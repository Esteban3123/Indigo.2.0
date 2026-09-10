-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetFormActions]
	-- Add the parameters for the stored procedure here
	@FormId INT,
	@RoleId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT [SelfService].[RolFormAction].[Id] as RolFormActionId
	,[SelfService].[FormAction].[FormId]
	,[SelfService].[FormAction].[ActionId]
	,[SelfService].[RolFormAction].[Status]
	,[SelfService].[Action].[Id]
	,[SelfService].[Action].[Name]
	,[SelfService].[Action].[Code]
	,[SelfService].[Action].[CreationUser]
	,[SelfService].[Action].[CreationDate]
	,[SelfService].[Action].[ModificationUser]
	,[SelfService].[Action].[ModificationDate]
	FROM [SelfService].[FormAction]
	INNER JOIN [SelfService].[Action]
	ON [SelfService].[FormAction].[ActionId] = [SelfService].[Action].[Id]
	INNER JOIN [SelfService].[RolFormAction] 
	ON [SelfService].[RolFormAction].[FormActionId] = [SelfService].[FormAction].[Id] 
	WHERE ([SelfService].[FormAction].[FormId] = @FormId AND [SelfService].[RolFormAction].[RoleId] = @RoleId)
END
