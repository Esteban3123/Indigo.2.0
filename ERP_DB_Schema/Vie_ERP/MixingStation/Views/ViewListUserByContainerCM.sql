




CREATE VIEW [MixingStation].[ViewListUserByContainerCM]
as
(
	Select t.Id, t.IdPerson, t.UserCode, t.RollCode, t.Fullname, t.PositionName, t.TenantId, t.IdContainer  from(
		SELECT u.Id, u.IdPerson, u.UserCode, u.RollCode, p.Fullname, po.Name as PositionName,0 as TenantId,0 as IdContainer
		from Security.[UserInt] u 
		join Security.[PersonInt] p on p.Id = u.IdPerson
		left join Common.ThirdParty tp on tp.Nit = p.Identification
		left join Payroll.Employee e on e.ThirdPartyId = tp.Id
		left join Payroll.Contract c on c.EmployeeId = e.Id  and c.Valid=1 and c.Status=1
		left join Payroll.Position po on po.Id = c.PositionId
		where UserType='3'
		UNION all
		SELECT  u.Id, u.IdPerson, u.UserCode, u.RollCode, p.Fullname, po.Name as PositionName, 0 as TenantId,c.IdContainer
		from Security.[UserInt] u
		join Security.PermissionCompanyInt c on u.Id= c.IdUser
		join Security.[PersonInt] p on p.Id = u.IdPerson
		left join Common.ThirdParty tp on tp.Nit = p.Identification
		left join Payroll.Employee e on e.ThirdPartyId = tp.Id
		left join Payroll.Contract co on co.EmployeeId = e.Id  and co.Valid=1 and co.Status=1
		left join Payroll.Position po on po.Id = co.PositionId
		WHERE u.UserType not in('2','3') and c.Permission=1
		union all
		SELECT  u.Id, u.IdPerson, u.UserCode, u.RollCode, p.Fullname, po.Name as PositionName, t.TenantId as TenantId,0 as IdContainer
		FROM Security.[UserInt] u
		join Security.TenantUsersInt t on u.Id=t.UserId
		join Security.[PersonInt] p on p.Id = u.IdPerson
		left join Common.ThirdParty tp on tp.Nit = p.Identification
		left join Payroll.Employee e on e.ThirdPartyId = tp.Id
		left join Payroll.Contract c on c.EmployeeId = e.Id  and c.Valid=1 and c.Status=1
		left join Payroll.Position po on po.Id = c.PositionId
		where u.UserType='2' 
	)as t
)