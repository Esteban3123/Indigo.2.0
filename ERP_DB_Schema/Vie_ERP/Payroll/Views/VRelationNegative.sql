
Create VIEW [Payroll].[VRelationNegative]
AS
select PayrollDateLiquidated,t.Nit, t.Name, SUM(ld.AccruedValue) as TotalDevengados, SUM(DeductedValue) as TotalDeducidos, SUM(ld.AccruedValue) - SUM(DeductedValue) as Diferencia
from Payroll.Liquidation l
inner join Payroll.Employee e on e.Id = l.EmployeeId
inner join Common.ThirdParty t on t.Id = e.ThirdPartyId
inner join Payroll.LiquidationDetail ld on ld.PayrollId = l.Id
where ld.ConceptType in (1,2)
group by PayrollDateLiquidated,e.Id,t.Nit, t.Name
having (SUM(ld.AccruedValue) - SUM(DeductedValue)) <0