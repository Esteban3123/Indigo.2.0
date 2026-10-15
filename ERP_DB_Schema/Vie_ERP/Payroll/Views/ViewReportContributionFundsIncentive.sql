



CREATE view [Payroll].[ViewReportContributionFundsIncentive]
AS
SELECT
	ipd.Id
	,pc.Code AS ConceptCode
	,pc.ConceptClass
	,pg.Code AS GroupCode
	,pg.[Name] AS GroupName
	,e.Id AS EmployeeId
	,t.Nit
	,t.[Name]
	,[ip].PeriodInitialDate
	,[ip].PeriodEndDate
	,[ip].[Period]
	,[ip].BasicSalary
	,[ip].WorkingDays
	,ISNULL(IBCUnemployment,0) AS IBCUnemployment
	,ISNULL(ipd.AccruedValue,0) AS TotalAccrued
	,ISNULL(ipd.DeductedValue,0) AS TotalDeducted
	,ISNULL([ip].TotalAccrued,0) AS Total
	,CASE 
		WHEN [ip].RegisterStatus=1 THEN '' 
		WHEN [ip].RegisterStatus=2 THEN 'C' 
	END AS RegisterStatus
	,pf.ThirdPartyId AS IdThirdParty
	,pf.Code AS FundCode
	,pf.[Name] AS FundName
	,fUnit.BranchOfficeId
FROM Payroll.IncentivePaymentDetail AS ipd 
JOIN Payroll.IncentivePayment AS [ip] ON [ip].Id = ipd.IncentivePaymentId
JOIN Payroll.[Group] pg ON pg.Id = [ip].GroupId
JOIN Payroll.Concept AS pc ON pc.Id = ipd.ConceptId
JOIN Payroll.[Contract] c ON c.Id = [ip].ContractId
JOIN Payroll.Employee e ON e.Id = c.EmployeeId
JOIN Common.ThirdParty t ON t.Id = e.ThirdPartyId
LEFT JOIN Payroll.Fund AS pf ON pf.Id = [ip].UnemployementFundId
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = c.FunctionalUnitId