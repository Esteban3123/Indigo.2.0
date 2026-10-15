CREATE view [Payroll].[ViewReportTotalPaidEmployeeIncentive]
AS 
SELECT
	[ip].Id 
	,g.Code AS GroupCode
	,g.Code + ' ' + g.[Name] AS GroupName
	,e.Id AS EmployeeId
	,t.Nit
	,t.[Name]
	,[ip].PeriodInitialDate
	,[ip].PeriodEndDate
	,[ip].[Period]
	,c.BasicSalary
	,[ip].TotalAccrued
	,isnull([ip].TotalDeducted,0) AS TotalDeducted
	,[ip].TotalAccrued - isnull([ip].TotalDeducted,0) AS Total
	,[ip].[RegisterStatus]
	,fu.Code AS FunctionalCode
	,fu.[Name] AS FunctionalName
	,cost.Code AS CostCenterCode
	,cost.[Name] AS CostCenterName
	,fu.BranchOfficeId
FROM Payroll.IncentivePayment [ip]
JOIN Payroll.[Group] g ON g.Id = [ip].GroupId
JOIN Payroll.[Contract] c ON c.Id = [ip].ContractId
JOIN Payroll.FunctionalUnit fu ON fu.Id = c.FunctionalUnitId
JOIN Payroll.CostCenter cost ON cost.Id = fu.CostCenterId
JOIN Payroll.Employee e ON e.Id = c.EmployeeId
JOIN Common.ThirdParty t ON t.Id = e.ThirdPartyId