

CREATE   VIEW [Payroll].[ViewReportIncomeTax]
AS

SELECT	
	pl.Id,
    pl.PayrollDateLiquidated,
    tp.Nit AS IdentificationNumber,
    tp.Name AS EmployeeName,
    SUM(pl.TotalBaseRetention) AS TotalBaseRetention,
    SUM(CASE WHEN c.Code = 'DQ97' THEN 
		pld.ConceptTotalValue 
		ELSE 0 END) AS ConceptRentPaid,
    SUM(CASE WHEN c.Code = 'BQ41' THEN
		pld.ConceptTotalValue 
		ELSE 0 END) AS ConceptWithholding,
    SUM(pl.DependentsSupplementary) AS SupplementaryPension,
    SUM(pl.DependentsDeduction) AS TaxCredits,
    'SL' AS Code
FROM Payroll.Liquidation pl
JOIN Payroll.Employee e ON e.Id = pl.EmployeeId
JOIN Common.ThirdParty tp ON tp.Id = e.ThirdPartyId
LEFT JOIN Payroll.LiquidationDetail pld ON pld.PayrollId = pl.Id
LEFT JOIN Payroll.Concept c ON c.Id = pld.ConceptId
GROUP BY pl.Id, pl.PayrollDateLiquidated, tp.Nit, tp.Name