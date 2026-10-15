
CREATE view [Inventory].[ViewRequestParamFunctionalUnit]
as
select 
	fu.Id
	, fu.Id as FunctionalUnitId
	, fu.Code as FunctionalUnitCode
	, fu.[Name] as FunctionalUnitName
	, rf.Id as RequestParamFuncionalUnitId
	, rp.Id as RequestParamId
	, cast(case when rf.Id is null then 0 else 1 end as bit) as IsSelected
	, rp.RequiredAuthorization
from Payroll.FunctionalUnit fu
left join Inventory.RequestParamFuncionalUnit rf on rf.FunctionalUnitId = fu.Id
left join Inventory.RequestParam rp on rf.RequestParamId = rp.Id