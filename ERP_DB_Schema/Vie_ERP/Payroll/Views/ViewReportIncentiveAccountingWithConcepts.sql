


CREATE VIEW [Payroll].[ViewReportIncentiveAccountingWithConcepts]
as
SELECT 
	ipd.Id as IncentivePaymentDetailId,
	ip.Id as IncentivePaymentId,
	thi.Nit, 
	thi.Name, 
	ip.BasicSalary, 
	ip.RetentionValue, 
	ip.TotalAccrued, 
	ip.TotalDeducted, 
	ip.PaidValue,
	conc.Code as ConceptCode,
	conc.Name as ConceptName,
	iif(ipd.AccruedValue = 0, ipd.DeductedValue, ipd.AccruedValue) as ValConceptAccruDeduc,
	gro.Code as GroupCode,
	gro.Name as GroupName,
	ipd.AccruedValue,
	ipd.DeductedValue,
	ip.PeriodInitialDate,
	ip.PeriodEndDate,
	em.Id as EmployeeeId,
	ip.Period,
	bOffice.Id AS BranchOfficeID
FROM 
[Payroll].[IncentivePayment] as ip with (nolock) inner join
[Payroll].[IncentivePaymentDetail] as ipd with (nolock) on ip.id = ipd.IncentivePaymentId inner join
[Payroll].[Contract] as con with (nolock) on ip.ContractId = con.Id inner join
[Payroll].[Employee] as em with (nolock) on con.EmployeeId = em.Id inner join
[Common].[ThirdParty] as thi with (nolock) on em.ThirdPartyId = thi.Id inner join
[Payroll].[Group] as gro with (nolock) on ip.GroupId = gro.Id inner join
[Payroll].[Concept] as conc with (nolock) on ipd.ConceptId = conc.Id
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = con.FunctionalUnitId
JOIN Payroll.BranchOffice bOffice ON bOffice.Id = fUnit.BranchOfficeId
Where conc.ConceptType <> 3