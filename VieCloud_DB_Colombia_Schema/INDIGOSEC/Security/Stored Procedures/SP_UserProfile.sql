-- =============================================
-- Author:		<Sebastian Martinez,,Name>
-- Create date: <03-09-2019,,>
-- Description:	<This Store Procedure return the profle information of an user,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_UserProfile]
	-- Add the parameters for the stored procedure here
	@UserCode int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/*SELECT <@Param1, sysname, @p1>, <@Param2, sysname, @p2>*/
	SELECT [Common].[ThirdParty].[Name], [Payroll].[Employee].[Id] as EmployeeId,
	[SelfService].[RolePortalUser].[RoleId], [Common].[Person].[IdentificationNumber]
	FROM [SelfService].[PortalUser]
	INNER JOIN [SelfService].[PortalUserCompany] 
	ON [SelfService].[PortalUser].[Id] = [SelfService].[PortalUserCompany].[PortalUserId] 
	INNER JOIN [Payroll].[Employee]  
	ON [Payroll].[Employee].[Id] = [SelfService].[PortalUserCompany].[EmployeeId] 	
	INNER JOIN [Common].[ThirdParty]
	ON [Common].[ThirdParty].[ID] = [Payroll].[Employee].[ThirdPartyId]
	INNER JOIN [SelfService].[RolePortalUser]
	ON [SelfService].[PortalUser].[Id] = [SelfService].[RolePortalUser].[PortalUserId] 
	INNER JOIN [Common].[Person]
	ON [Common].[Person].[Id] = [Common].[ThirdParty].[PersonId]
	WHERE [SelfService].[PortalUser].[UserCode] = @UserCode;

END
