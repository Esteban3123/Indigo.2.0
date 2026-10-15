CREATE view [Payroll].[ViewReportTotalPaidEmployeeIncentiveConcept]
AS 
SELECT
	ipd.Id 
	,g.Code AS GroupCode
	,g.Code + ' ' + g.[Name] AS GroupName
	,e.Id AS EmployeeId
	,t.Nit
	,t.[Name]
	,[ip].PeriodInitialDate
	,[ip].PeriodEndDate
	,[ip].[Period]
	,c.BasicSalary
	,ipd.AccruedValue TotalAccrued
	,isnull(ipd.DeductedValue,0) AS TotalDeducted
	,ipd.AccruedValue + isnull(ipd.DeductedValue,0) AS Total
	,[ip].[RegisterStatus]
	,fu.Code AS FunctionalCode
	,fu.[Name] AS FunctionalName
	,cost.Code AS CostCenterCode
	,cost.[Name] AS CostCenterName
	,con.Id AS ConceptId
	,con.Code AS ConceptCode
	,con.[Name] AS ConceptName
	,fu.BranchOfficeId
FROM Payroll.IncentivePayment [ip]
JOIN Payroll.IncentivePaymentDetail ipd ON [ip].Id = ipd.IncentivePaymentId
JOIN Payroll.Concept con ON con.Id = ipd.ConceptId
JOIN Payroll.[Group] g ON g.Id = [ip].GroupId
JOIN Payroll.[Contract] c ON c.Id = [ip].ContractId
JOIN Payroll.FunctionalUnit fu ON fu.Id = c.FunctionalUnitId
JOIN Payroll.CostCenter cost ON cost.Id = fu.CostCenterId
JOIN Payroll.Employee e ON e.Id = c.EmployeeId
JOIN Common.ThirdParty t ON t.Id = e.ThirdPartyId
WHERE con.ConceptType <> 3