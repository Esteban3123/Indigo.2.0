-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetRecoveryAccountEmail]
	-- Add the parameters for the stored procedure here
	 @UserCode varchar(20),
	 @Container varchar(50),
	 @Security varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @sql nvarchar(max)

    -- Insert statements for procedure here
	set @sql = 'SELECT '+@Container+ '.[Common].[Email].[Email], '+@Container+'.[Common].[ThirdParty].[Name],'+@Security+'.[SelfService].[PortalUser].[UserCode], 
	'+@Security+'.[SelfService].[PortalUser].[Password]
	FROM '+@Security+'.[SelfService].[PortalUser]
	INNER JOIN '+@Security+'.[SelfService].[PortalUserCompany] 
	ON '+@Security+'.[SelfService].[PortalUser].[Id] = '+@Security+'.[SelfService].[PortalUserCompany].[PortalUserId] 
	INNER JOIN '+@Container+'.[Payroll].[Employee]  
	ON '+@Container+'.[Payroll].[Employee].[Id] = '+@Security+'.[SelfService].[PortalUserCompany].[EmployeeId] 	
	INNER JOIN '+@Container+'.[Common].[ThirdParty]
	ON '+@Container+'.[Common].[ThirdParty].[ID] = '+@Container+'.[Payroll].[Employee].[ThirdPartyId]
	INNER JOIN '+@Container+'.[Common].[Person]
	ON '+@Container+'.[Common].[Person].[ID] = '+@Container+'.[Common].[ThirdParty].[PersonId]
	INNER JOIN '+@Container+'.[Common].[Email]
	ON '+@Container+'.[Common].[Email].[IdPerson]= '+@Container+'.[Common].[Person].[Id] 
	WHERE '+@Security+'.[SelfService].[PortalUser].[UserCode] = '''+@UserCode+''';';

	exec(@sql)
END
