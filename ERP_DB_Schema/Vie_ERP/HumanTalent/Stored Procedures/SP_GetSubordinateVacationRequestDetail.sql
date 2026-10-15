-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [HumanTalent].[SP_GetSubordinateVacationRequestDetail]
	-- Add the parameters for the stored procedure here
	@VacationRequestDetailId INT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [HumanTalent].[VacationRequestDetail].[Id]
	,[HumanTalent].[VacationRequestDetail].[Comments]
	,[HumanTalent].[VacationRequest].[Id] AS VacationRequestId
	,[HumanTalent].[VacationRequest].[InitialDateVacation]
	,[HumanTalent].[VacationRequest].[EndDateVacation]
	,[HumanTalent].[VacationRequest].[DaysRequest]
	,[HumanTalent].[VacationRequest].[CreationDate]
	,[Common].[ThirdParty].[Name]
	,[Payroll].[Position].[Name] AS PositionName
	,[Common].[Person].[IdentificationNumber]
	FROM [HumanTalent].[VacationRequestDetail]
	INNER JOIN [HumanTalent].[VacationRequest]
	ON [HumanTalent].[VacationRequestDetail].[IdVacationRequest] = [HumanTalent].[VacationRequest].[Id]
	INNER JOIN [Payroll].[Employee]
	ON [HumanTalent].[VacationRequest].[EmployeeId] = [Payroll].[Employee].[Id]
	INNER JOIN [Common].[ThirdParty]
	ON [Payroll].[Employee].[ThirdPartyId] = [Common].[ThirdParty].[Id]
	INNER JOIN [Payroll].[Contract]
	ON [Payroll].[Contract].[EmployeeId] = [Payroll].[Employee].[Id]
	INNER JOIN [Payroll].[Position]
	ON [Payroll].[Position].[Id] = [Payroll].[Contract].[PositionId]
	INNER JOIN Common.Person 
	ON [Common].[ThirdParty].[PersonId] = [Common].[Person].[Id]
	WHERE ([HumanTalent].[VacationRequestDetail].[Id] = @VacationRequestDetailId AND [Payroll].[Contract].[Status] = 1 AND [Payroll].[Contract].[Valid] = 1);
    
END