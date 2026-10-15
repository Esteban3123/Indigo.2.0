
CREATE VIEW [Payroll].[ViewReportIncentivePayment]
AS
SELECT
	ipd.Id,
	ct.Nit,
	ct.[Name] as ThirdName,
	pc.ConceptType,
	pc.Code,
	pc.[Name],
	[ip].PeriodInitialDate,
	[ip].PeriodEndDate,
	et.Code AS EmployeeTypeCode,
	[ip].[Period],
	ipd.AccruedValue,
	ipd.DeductedValue,
	c.JobBondingDate,
	c.BasicSalary,
	pe.Id AS EmployeeId,
	pg.Id AS GroupId,
	pg.Code AS GroupCode,
	pg.[Name] AS GroupName,
	pco.Id AS CostCenetrId,
	pco.Code AS CostCenetrCode, 
	pco.[Name] AS CostCenetrName,
	fu.Id AS FunctionalId,
	fu.Code AS FunctionalCode,
	fu.[Name] AS FunctiomalName,
	bOffice.Id AS BranchOfficeID,
	bOffice.Code AS BranchOfficeCode,
	bOffice.[Name] AS BranchOffice
FROM Payroll.IncentivePaymentDetail as ipd 
JOIN Payroll.IncentivePayment as [ip] on [ip].Id = ipd.IncentivePaymentId
JOIN Payroll.[Group] as pg on pg.Id = [ip].GroupId
JOIN Payroll.Concept as pc on pc.Id = ipd.ConceptId
JOIN Payroll.[Contract] as c on c.Id = [ip].ContractId
JOIN Payroll.FunctionalUnit as fu on fu.id = c.FunctionalUnitId
JOIN Payroll.Employee as pe on pe.Id = c.EmployeeId
JOIN Payroll.CostCenter as pco on pco.Id = pe.CostCenterId
JOIN Common.ThirdParty as ct on ct.Id = pe.ThirdPartyId
JOIN Payroll.EmployeeType as et on et.Id = pe.EmployeeTypeId
JOIN Payroll.BranchOffice bOffice ON bOffice.Id = fu.BranchOfficeId
WHERE pc.ConceptType <> 3