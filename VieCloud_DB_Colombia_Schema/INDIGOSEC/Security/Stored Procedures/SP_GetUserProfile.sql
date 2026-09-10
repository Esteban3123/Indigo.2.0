-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetUserProfile]
	-- Add the parameters for the stored procedure here
	@UserCode varchar(20),
	@Container varchar(50),
	@Security varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @sql nvarchar(max)
    -- Insert statements for procedure here
	set @sql = 'SELECT TP.[Name],  E.[Id] as EmployeeId, RPU.[RoleId], P.[IdentificationNumber]
	FROM ' +@Security+'.[SelfService].[PortalUser] PU
	INNER JOIN '+@Security+'.[SelfService].[PortalUserCompany] PUC ON PU.[Id] = PUC.[PortalUserId] 
	INNER JOIN ' +@Container+ '.[Payroll].[Employee] E ON E.[Id] =  PUC.[EmployeeId] 
	INNER JOIN ' +@Container+ '.[Common].[ThirdParty] TP ON  TP.[ID] =  E.[ThirdPartyId]
	INNER JOIN ' +@Security+ '.[SelfService].[RolePortalUser] RPU ON RPU.[PortalUserId] = PU.[Id]
	INNER JOIN ' +@Container+ '.[Common].[Person]  P ON P.[Id] = TP.[PersonId]
	WHERE PU.[UserCode] = '''+ @UserCode +''' AND PUC.TransactionalContainer = '''+ @Container +'''';
	print @sql
	exec(@sql)
END
