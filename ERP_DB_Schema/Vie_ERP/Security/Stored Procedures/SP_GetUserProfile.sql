-- Stored Procedure

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
	set @sql = 'SELECT ' +@Container+ '.[Common].[ThirdParty].[Name], ' +@Container+ '.[Payroll].[Employee].[Id] as EmployeeId,'
	+@Security+'.[SelfService].[RolePortalUser].[RoleId], ' +@Container+ '.[Common].[Person].[IdentificationNumber]
	FROM ' +@Security+'.[SelfService].[PortalUser]
	INNER JOIN '+@Security+'.[SelfService].[PortalUserCompany] 
	ON '+@Security+'.[SelfService].[PortalUser].[Id] = '+@Security+'.[SelfService].[PortalUserCompany].[PortalUserId] 
	INNER JOIN ' +@Container+ '.[Payroll].[Employee]  
	ON '+@Container+'.[Payroll].[Employee].[Id] = ' +@Security+ '.[SelfService].[PortalUserCompany].[EmployeeId] 	
	INNER JOIN ' +@Container+ '.[Common].[ThirdParty]
	ON ' +@Container+'.[Common].[ThirdParty].[ID] = ' +@Container+'.[Payroll].[Employee].[ThirdPartyId]
	INNER JOIN ' +@Security+ '.[SelfService].[RolePortalUser]
	ON ' +@Security+ '.[SelfService].[PortalUser].[Id] = '+@Security+'.[SelfService].[RolePortalUser].[PortalUserId] 
	INNER JOIN ' +@Container+ '.[Common].[Person]
	ON ' +@Container+ '.[Common].[Person].[Id] = ' +@Container+ '.[Common].[ThirdParty].[PersonId]
	WHERE '+@Security+'.[SelfService].[PortalUser].[UserCode] = '''+@UserCode+''';';
	print @sql
	exec(@sql)
END