

CREATE VIEW [Payroll].[ViewReportExpeditionCDPAndRPIncentivePayment]
AS
SELECT 
	ROW_NUMBER() OVER(ORDER BY C.Id ASC) AS [Row],
	C.Id, 
	C.Name,
	SUM(ipd.AccruedValue + ipd.DeductedValue) AS 'ConceptTotalValue', 
	ET.Code, 
	C.ConceptType, 
	[ip].PeriodInitialDate AS PayrollDateLiquidated,
	bOffice.Id AS BranchOfficeID,
	bOffice.Code AS BranchOfficeCode,
	bOffice.[Name] AS BranchOffice
FROM Payroll.IncentivePayment [ip]
JOIN Payroll.IncentivePaymentDetail ipd on [ip].Id = ipd.IncentivePaymentId
JOIN Payroll.Concept c on c.Id = ipd.ConceptId
JOIN Payroll.[Contract] con on con.Id = [ip].ContractId
JOIN Payroll.Employee e on e.Id = con.EmployeeId
JOIN Payroll.EmployeeType et on et.Id = e.EmployeeTypeId
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = con.FunctionalUnitId
JOIN Payroll.BranchOffice bOffice ON bOffice.Id = fUnit.BranchOfficeId
WHERE c.ConceptType <> 3
GROUP BY 
	C.[Name], 
	C.Id,
	ET.Code, 
	C.ConceptType, 
	[ip].PeriodInitialDate,
	bOffice.Id,
	bOffice.Code,
	bOffice.[Name]