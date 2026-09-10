-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<This Sp inserts the forms in RolesForms table and adter insert the correspondent action to that form in RolFormAction,>
-- =============================================
CREATE PROCEDURE [Security].[SP_InsertRoleAndActionsByRole]
	-- Add the parameters for the stored procedure here
	@RoleId INT,
	@FormCode INT,
	@CreationUser VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	-- Insert the RolesForms
	INSERT INTO [SelfService].[RolesForms] (FormId, RoleId, Status,CreationUser, CreationDate) VALUES ((Select [Form].[Id] from [SelfService].[Form] WHERE [SelfService].[Form].[Code] = @FormCode) ,@RoleId,1,@CreationUser, GETDATE())
	
	INSERT INTO SelfService.RolFormAction(RoleId, FormActionId, [Status])
	SELECT @RoleId,  [SelfService].[FormAction].[Id], 0
	FROM [SelfService].[FormAction]
	INNER JOIN [SelfService].[Form]
	ON [SelfService].[Form].[Id] = [SelfService].[FormAction].[FormId]
	WHERE [SelfService].[Form].[Code] = @FormCode;

	-- Insert the formActions in RoleFormAction table with the parameter of role
	
  
END
