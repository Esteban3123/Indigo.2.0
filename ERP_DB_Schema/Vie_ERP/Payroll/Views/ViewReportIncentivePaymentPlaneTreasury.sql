


CREATE VIEW [Payroll].[ViewReportIncentivePaymentPlaneTreasury]
AS
select ip.Id
,ct.Nit
,ct.Name as ThirdName
,ip.PeriodInitialDate
,ip.PeriodEndDate
,ip.Period
,pg.Id as GroupId
,pg.Code as GroupCode
,pg.Name as GroupName
,c.BankAccountNumber as BankAccountNumber
,b.Name as BankName
, isNull(ip.TotalAccrued,0) - isNull(ip.TotalDeducted,0) as TotalPaid
from Payroll.IncentivePayment as ip
inner join Payroll.[Group] as pg on pg.Id = ip.GroupId
inner join Payroll.[Contract] as c on c.Id = ip.ContractId
inner join Payroll.Employee as pe on pe.Id = c.EmployeeId
inner join Common.ThirdParty as ct on ct.Id = pe.ThirdPartyId
left join Payroll.Bank as b on b.Id = c.BankId