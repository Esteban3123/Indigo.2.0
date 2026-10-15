





CREATE VIEW [Payroll].[ViewReportContractLiquidation] 
AS


select
cast(cld.Id as varchar(50)) as Id,
cl.Id as contractLiquidationId,
cs.[Name] as employeeCostCenterName,
p.FirstName + ' ' + p.SecondName + ' ' + p.FirstLastName + ' ' + p.SecondLastName as employeeName, 
p.IdentificationNumber as employeeIdentificationNumber, 
pos.[Name] as employeePosition, 
rr.[Name] as employeeRetirementReason,
c.JobBondingDate as contractInitialDate,
c.RetirementDate as contractEndingDate,
datediff(day, c.JobBondingDate, c.RetirementDate) as workedDays,
c.ContractInitialDate as initialBenefitsLiquidation,
c.BasicSalary as basicSalary,
CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END AS incentiveLiquidationDate  ,
--cld.initialdate as incentiveLiquidationDate,
cld.endingdate as incentiveCourtDate,
datediff(day, CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END, cld.endingdate) as incentiveDays,
cld.initialdate as christmasLiquidationDate,
cld.endingdate as christmasCourtDate,
datediff(day, cld.initialdate, cld.endingdate) as christmasDays,
SUM(vp.TakenDays) as vacationTakenDays,
IIF((datediff(day, cld.initialdate, cld.endingdate) - SUM(vp.TakenDays)) > 0, (datediff(day, cld.initialdate, cld.endingdate) - SUM(vp.TakenDays)), 0) as vacationPendingDays,
CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END AS unemployedInitialDate,
--cld.initialdate as unemployedInitialDate,
cld.endingdate as unemployedEndingDate,
datediff(day, CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END, cld.endingdate) as unemployedDays,
CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END AS unemployedInterestInitialDate  ,
--cld.initialdate as unemployedInterestInitialDate,
cld.endingdate as unemployedInterestEndingDate,
datediff(day, CASE WHEN C.JobBondingDate <=  CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) THEN CAST(CONVERT(VARCHAR(4),YEAR(C.RetirementDate)) + '01' + '01' AS DATE) ELSE C.JobBondingDate END, cld.endingdate) as unemployedInterestDays,
SUM(vp.PendingDays) as sumaryVacationAccured,
cld.Accrued as sumaryAccured,
cld.Deducted as sumaryDeducted,
cld.[Description] as sumaryDescription,
cl.TotalPaid as totalPaidLiquidation,
cl.TotalAccrued as totalAccured,
cl.TotalDeducted as totalDeducted,
--IIF(c.BasicSalary <= (pp.LegalSalaryMinimum * 2), pp.TransportHelpValue, 0) AS transportHelpValue
case when c.BasicSalary <= (pp.LegalSalaryMinimum * 2) then pp.TransportHelpValue else 0 end AS transportHelpValue
from Payroll.ContractLiquidation cl
inner join Payroll.Employee e on e.Id = cl.EmployeeId
inner join Common.ThirdParty tp on tp.Id = e.ThirdPartyId
inner join Common.Person p on p.Id = tp.PersonId
inner join Payroll.[Contract] c on c.Id = cl.ContractId
inner join Payroll.Position pos on pos.Id = c.PositionId
inner join Payroll.RetirementReason rr on rr.Id = cl.RetirementReasonId
left join Payroll.VacationPeriod vp on vp.EmployeeId = e.Id
inner join Payroll.CostCenter cs on cs.Id = e.CostCenterId
inner join payroll.contractliquidationdetail cld on cld.contractliquidationid = cl.id
inner join payroll.[Group] g on g.Id = c.GroupId
inner join Payroll.PayrollParameter pp on pp.Id = g.PayrollParameterId
group by cld.id, cl.Id, cs.[Name], p.FirstName, p.SecondName, p.FirstLastName, p.SecondLastName, p.IdentificationNumber, pos.[Name], rr.[Name], c.JobBondingDate, c.RetirementDate, c.ContractInitialDate,
c.JobBondingDate,c.BasicSalary,cld.InitialDate,cld.EndingDate,
cl.TotalPaid,
cld.Accrued,
cld.Deducted,
cld.[Description],
cl.TotalAccrued,
cl.TotalDeducted,
pp.TransportHelpValue,
pp.LegalSalaryMinimum