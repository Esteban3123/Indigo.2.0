CREATE VIEW [Payroll].[ViewReportLiquidationDetaill]
as
select 
l.BasicSalary, 
thi.CreationDate, 
thi.Nit, 
thi.Name as NameThi, 
ld.Id, 
c.Name
from 
Payroll.LiquidationDetail as ld
INNER JOIN Payroll.Liquidation as l on l.Id = ld.PayrollId
INNER JOIN Payroll.Employee as em on em.Id = l.EmployeeId
INNER JOIN Common.ThirdParty as thi on thi.Id = em.ThirdPartyId
INNER JOIN Payroll.Concept as c on c.Id = ld.ConceptId