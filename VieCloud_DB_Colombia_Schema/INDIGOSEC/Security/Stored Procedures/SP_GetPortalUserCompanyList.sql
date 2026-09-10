-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetPortalUserCompanyList]
	-- Add the parameters for the stored procedure here
	 @Container varchar(50),
	 @Security varchar(50)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @sql nvarchar(max)
    -- Insert statements for procedure here
	set @sql = 'SELECT '
	+@Security+ '.[SelfService].[PortalUser].[Id], ' 
	+@Security+ '.[SelfService].[Role].[Id]  as RoleId, '
	+@Security+ '.[SelfService].[PortalUser].[UserCode], ' 
	+@Security+ '.[SelfService].[PortalUser].[Status], ' 
	+@Container+ '.Common.ThirdParty.Name
	FROM ' +@Security+ '.[SelfService].[PortalUser] 
	INNER JOIN ' +@Security+ '.[SelfService].[RolePortalUser] 
	on ' +@Security+ '.[SelfService].[RolePortalUser].[PortalUserId] = ' +@Security+ '.[SelfService].[PortalUser].[Id]  
	INNER JOIN ' +@Security+ '.[SelfService].[Role]
	on ' +@Security+ '.[SelfService].[RolePortalUser].[RoleId] = ' +@Security+ '.[SelfService].[Role].[Id]  
	INNER JOIN ' +@Security+ '.[SelfService].[PortalUserCompany] 
	ON ' +@Security+ '.[SelfService].[PortalUser].[Id] = ' +@Security+ '.[SelfService].[PortalUserCompany].[PortalUserId] 
	INNER JOIN ' + @Container+ '.[Payroll].[Employee]
	on ' +@Container+ '.[Payroll].[Employee].[Id] = ' +@Security+ '.[SelfService].[PortalUserCompany].[EmployeeId] 
	INNER JOIN ' +@Container+ '.[Common].[ThirdParty] 
	on ' +@Container+ '.[Payroll].[Employee].[ThirdPartyId] =' +@Container+ '.[Common].[ThirdParty].[Id]'

	--PRINT @sql

	exec(@sql)
	
END

--exec [Security].[SP_GetPortalUserCompanyList] 'VIE08'