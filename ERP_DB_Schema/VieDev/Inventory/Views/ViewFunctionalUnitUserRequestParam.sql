
CREATE VIEW [Inventory].[ViewFunctionalUnitUserRequestParam] AS

select 
	
	u.Id,
	fu.Code as CodeFunctionalUnit,
	u.UserCode  + ' - ' + p.Fullname  as CodeName
from [Payroll].[FunctionalUnit] AS fu
INNER JOIN Payroll.FunctionalUnitUserAuthorizationRequest AS fusar ON fusar.FunctionalUnitId = fu.Id
inner join Security.[User] as u on u.Id = fusar.UserId 
inner join Security.Person as p on p.Id = u.IdPerson