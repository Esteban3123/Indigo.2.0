-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SelfService].[AuthenticateUser]
	-- Add the parameters for the stored procedure here
	@UserCode Varchar(20)
AS
BEGIN
	DECLARE @QueryResult int;
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	  SET @QueryResult = (SELECT COUNT(*)
	  FROM [SelfService].[PortalUser]
	  INNER JOIN [SelfService].[PortalUserCompany]
	  ON [SelfService].[PortalUser].[Id] = [SelfService].[PortalUserCompany].[PortalUserId]
	  INNER JOIN [Payroll].[Contract]
	  ON  [SelfService].[PortalUserCompany].[EmployeeId] = [Payroll].[Contract].[EmployeeId]
	  WHERE (UserCode = @UserCode AND [Payroll].[Contract].[Valid] = 1 AND [Payroll].[Contract].[Status] = 1))
	  print @QueryResult

END
