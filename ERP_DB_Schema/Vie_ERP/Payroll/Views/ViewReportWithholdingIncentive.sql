


CREATE VIEW [Payroll].[ViewReportWithholdingIncentive]
AS

select 
ip.Id
,ct.Nit
,ct.Name as ThirdName
,2 as ConceptType
,ip.PeriodInitialDate
,ip.PeriodEndDate
,et.Code as EmployeeTypeCode
,ip.Period
,ip.TotalAccrued as AccruedValue
,ip.TotalDeducted as DeductedValue
,pe.Id as EmployeeId,pg.Id as GroupId,pg.Code as GroupCode,pg.Name as GroupName
,ip.ProcedureTypeRTF, pe.DeclarantType,ip.TypeArticleRTF,ip.RetentionBase  , (ip.RetentionBase / 3) AS ExemptValueRetention
,(select sum(ipd.DeductedValue) from Payroll.IncentivePaymentDetail ipd inner join Payroll.Concept con on con.Id = ipd.ConceptId where ipd.IncentivePaymentId = ip.Id and con.ConceptClass = '020') as RetentionValue
,fUnit.BranchOfficeId
from Payroll.IncentivePayment as ip 
inner join Payroll.[Group] as pg on pg.Id = ip.GroupId
inner join Payroll.[Contract] as c on c.Id = ip.ContractId
inner join Payroll.Employee as pe on pe.Id = c.EmployeeId
inner join Common.ThirdParty as ct on ct.Id = pe.ThirdPartyId
inner join Payroll.EmployeeType as et on et.Id = pe.EmployeeTypeId
OUTER APPLY(
	SELECT TOP 1 fUnit.BranchOfficeId
	FROM Payroll.[Contract] cntrc
	JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = cntrc.FunctionalUnitId
	WHERE cntrc.EmployeeId = pe.Id
	ORDER BY cntrc.Id DESC
) fUnit
where (select sum(ipd.DeductedValue) from Payroll.IncentivePaymentDetail ipd inner join Payroll.Concept con on con.Id = ipd.ConceptId where ipd.IncentivePaymentId = ip.Id and con.ConceptClass = '020' and con.ConceptType <> 3) > 0