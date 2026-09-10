-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetRolePortalUser]
	-- Add the parameters for the stored procedure here
	@UserCode Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [SelfService].[PortalUser].[Id]
	  ,[SelfService].[PortalUser].[UserCode]
      ,[SelfService].[RolePortalUser].[PortalUserId]
      ,[SelfService].[RolePortalUser].[RoleId]
	  ,[SelfService].[Role].[Name]
	  FROM [SelfService].[RolePortalUser]
	  INNER JOIN [SelfService].[PortalUser]
	  ON [SelfService].[PortalUser].[Id] = [SelfService].[RolePortalUser].[PortalUserId]
	  INNER JOIN [SelfService].[Role]
	  ON [SelfService].[Role].[Id] = [SelfService].[RolePortalUser].[RoleId]
	  where PortalUser.UserCode = @UserCode  
END
