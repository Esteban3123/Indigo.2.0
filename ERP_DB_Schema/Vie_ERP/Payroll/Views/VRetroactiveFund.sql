

CREATE VIEW [Payroll].[VRetroactiveFund]
AS
SELECT 
	rd.Id,
	G.Id AS GroupId, 
	G.Code AS GroupCode, 
	G.[Name] AS GroupName, 
	TP.Id AS ThirdPartyId, 
	TP.Nit AS NitEmployee, 
	TP.[Name] AS EmployeeName, 
	CONT.BasicSalary AS BasicSalary,
	30 AS PayrollDays, 
	RD.ValueConcept AS IBC,
	emp.Id AS IdEmployee,
	RD.ValueConceptWithRetroactive AS FundValue,
	rc.InitialDateRetroactive AS RetroactiveDate,
	CASE rc.[Status] 
		WHEN 1 THEN '' WHEN 2 THEN 'C' 
	END AS RegisterStatus,
	pc.ConceptClass,
	(SELECT ISNULL(RD.ValueConceptWithRetroactive,0) FROM Payroll.RetroactiveC RC1, Payroll.RetroactiveD RD1, Payroll.Concept CONC WHERE RC1.Id = RC.Id AND RD1.IdRetroactiveC = RC1.Id AND CONC.Id = RD1.IdConcept AND CONC.ConceptClass = '014') as FundValuePension,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 1 AND VoluntaryContribution = 0) AS HealthFund,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 2 AND VoluntaryContribution = 0) AS PensionFund,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 1 AND VoluntaryContribution = 1) AS VoluntaryHealthFund,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 2 AND VoluntaryContribution = 1) AS VoluntaryPensionFund,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 4 AND VoluntaryContribution = 0) AS RiskFund,
	(SELECT TOP 1 F.[Name] FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.FundId = F.Id AND FC.ContractId = RC.IdContract AND FC.FundType = 3 AND VoluntaryContribution = 0) AS UnemploymentFund,
	fUnit.BranchOfficeId
FROM Payroll.RetroactiveD AS rd
JOIN Payroll.RetroactiveC AS rc ON rc.Id = rd.IdRetroactiveC
JOIN Payroll.Concept AS pc ON pc.Id = rd.IdConcept
JOIN Payroll.Employee AS Emp ON Emp.Id = rc.IdEmployee
JOIN Payroll.[Contract] AS CONT ON CONT.Id = RC.IdContract
JOIN Common.ThirdParty AS TP ON TP.Id = Emp.ThirdPartyId
JOIN Payroll.[Group] AS G ON G.Id = CONT.GroupId
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = CONT.FunctionalUnitId