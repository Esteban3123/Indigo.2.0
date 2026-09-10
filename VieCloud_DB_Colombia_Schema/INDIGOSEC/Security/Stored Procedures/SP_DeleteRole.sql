-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_DeleteRole]
	-- Add the parameters for the stored procedure here
	@RoleId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	IF (NOT EXISTS (SELECT * FROM [SelfService].[RolFormAction] where RoleId = @RoleId) AND NOT EXISTS (SELECT * FROM [SelfService].[RolePortalUser] where RoleId = @RoleId))
	BEGIN
		DELETE FROM [SelfService].[Role] WHERE Id = @RoleId;
		--return 1;
	END
END
