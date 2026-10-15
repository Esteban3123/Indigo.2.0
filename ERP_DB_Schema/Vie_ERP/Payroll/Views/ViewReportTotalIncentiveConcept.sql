CREATE VIEW [Payroll].[ViewReportTotalIncentiveConcept]
AS
SELECT 
	ipd.Id,
	pc.ConceptType,
	pc.Code,
	pc.[Name],
	[ip].PeriodInitialDate,
	[ip].PeriodEndDate,
	et.Code AS EmployeeTypeCode,
	[ip].[Period],
	ipd.AccruedValue,
	ipd.DeductedValue,
	pe.Id AS EmployeeId,
	pg.Id AS GroupId,
	pg.Code AS GroupCode,
	pg.[Name] AS GroupName,
	fu.BranchOfficeId
FROM Payroll.IncentivePaymentDetail AS ipd 
JOIN Payroll.IncentivePayment AS [ip] ON [ip].Id = ipd.IncentivePaymentId
JOIN Payroll.[Group] AS pg ON pg.Id = [ip].GroupId
JOIN Payroll.Concept AS pc ON pc.Id = ipd.ConceptId
JOIN Payroll.[Contract] AS c ON c.Id = [ip].ContractId
JOIN Payroll.FunctionalUnit AS fu ON fu.id = c.FunctionalUnitId
JOIN Payroll.Employee AS pe ON pe.Id = c.EmployeeId
JOIN Payroll.CostCenter AS pco ON pco.Id = pe.CostCenterId
JOIN Common.ThirdParty AS ct ON ct.Id = pe.ThirdPartyId
JOIN Payroll.EmployeeType AS et ON et.Id = pe.EmployeeTypeId
WHERE pc.ConceptType <> 3