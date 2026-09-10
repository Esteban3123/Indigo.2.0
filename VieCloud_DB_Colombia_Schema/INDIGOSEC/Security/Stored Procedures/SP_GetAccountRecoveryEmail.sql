-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetAccountRecoveryEmail]
	-- Add the parameters for the stored procedure here
	@UserCode varchar(20)  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Common].[Email].[Email], [Common].[ThirdParty].[Name],[SelfService].[PortalUser].[UserCode], 
	[SelfService].[PortalUser].[Password]
	FROM [SelfService].[PortalUser]
	INNER JOIN [SelfService].[PortalUserCompany] 
	ON [SelfService].[PortalUser].[Id] = [SelfService].[PortalUserCompany].[PortalUserId] 
	INNER JOIN [Payroll].[Employee]  
	ON [Payroll].[Employee].[Id] = [SelfService].[PortalUserCompany].[EmployeeId] 	
	INNER JOIN [Common].[ThirdParty]
	ON [Common].[ThirdParty].[ID] = [Payroll].[Employee].[ThirdPartyId]
	INNER JOIN [Common].[Person]
	ON [Common].[Person].[ID] = [Common].[ThirdParty].[PersonId]
	INNER JOIN [Common].[Email]
	ON [Common].[Email].[IdPerson]= [Common].[Person].[Id] 
	WHERE [SelfService].[PortalUser].[UserCode] = @UserCode;

END
