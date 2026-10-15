









CREATE VIEW [Authorization].[ViewListScheduleTemplateUsers]
AS

	select distinct stu.UserId, stu.UserCode, p.Fullname FullName, stu.UserCode + ' - ' + p.Fullname UserCodeName
	from [Authorization].AuthorizationScheduleTemplateUsers stu
	inner join Security.[UserInt] u on u.Id = stu.UserId
	inner join Security.PersonInt p on p.Id = u.IdPerson