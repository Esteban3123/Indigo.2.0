


CREATE VIEW [Payroll].[ViewReportExpeditionCDPAndRP]
AS
SELECT	
	ROW_NUMBER() OVER(ORDER BY C.Id ASC) AS [Row],  
	C.Id, C.Code + ' - ' + C.[Name] AS [Name], 
	SUM(LD.ConceptTotalValue) AS 'ConceptTotalValue', 
	ET.Code, 
	C.ConceptType, 
	L.PayrollDateLiquidated,
	bOffice.Id AS BranchOfficeID,
	bOffice.Code AS BranchOfficeCode,
	bOffice.[Name] AS BranchOffice
FROM Payroll.Concept AS C
JOIN Payroll.LiquidationDetail AS LD 
JOIN Payroll.Employee AS E 
JOIN Payroll.Liquidation AS L ON E.Id = L.EmployeeId ON LD.PayrollId = L.Id ON C.Id = LD.ConceptId 
JOIN Payroll.EmployeeType AS ET ON E.EmployeeTypeId = ET.Id
JOIN Payroll.[Contract] cntrc ON cntrc.EmployeeId = E.Id
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = cntrc.FunctionalUnitId
JOIN Payroll.BranchOffice bOffice ON bOffice.Id = fUnit.BranchOfficeId
WHERE cntrc.Id = L.ContractId
GROUP BY 
	C.Code,
	C.[Name], 
	C.Id, 
	ET.Code, 
	C.ConceptType, 
	L.PayrollDateLiquidated,
	bOffice.Id,
	bOffice.Code,
	bOffice.[Name]