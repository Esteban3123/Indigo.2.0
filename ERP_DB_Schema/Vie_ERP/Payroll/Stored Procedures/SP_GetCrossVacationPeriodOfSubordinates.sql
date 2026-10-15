-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Payroll].[SP_GetCrossVacationPeriodOfSubordinates]
	-- Add the parameters for the stored procedure here
	@InitialVacationDate DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @InputDate DATE;
	DECLARE @InitialDatePeriod DATE;
	DECLARE @EndDatePeriod DATE;

    -- Insert statements for procedure here
	--SELECT @InitialDatePeriod = InitialDatePeriod, @EndDatePeriod = EndDatePeriod FROM Payroll.VacationPeriod WHERE EmployeeId = 1 ORDER BY InitialDatePeriod; 
	SELECT Payroll.VacationPeriod.Id
	,Payroll.VacationPeriod.InitialDatePeriod
	,Payroll.VacationPeriod.EndDatePeriod
	,Payroll.Employee.Id AS EmployeeId
	,Common.ThirdParty.Name as ThirdPartyName
	,Payroll.Position.Name as PositionName
	FROM Payroll.VacationPeriod 
	INNER JOIN Payroll.Employee
	ON Payroll.Employee.Id =  Payroll.VacationPeriod.EmployeeId 
	INNER JOIN Common.ThirdParty
	ON Payroll.Employee.ThirdPartyId = Common.ThirdParty.Id
	INNER JOIN Payroll.Contract
	ON Payroll.Contract.EmployeeId = Payroll.Employee.Id
	INNER JOIN Payroll.Position
	ON Payroll.Position.Id = Payroll.Contract.PositionId
	WHERE (EndDatePeriod > @InitialVacationDate AND Payroll.Contract.Status = 1 AND Payroll.Contract.Valid= 1)
	ORDER BY Payroll.Employee.Id;
END