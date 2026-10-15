








CREATE VIEW [Authorization].[ViewListUsersForSchedule]
AS

	select stu.Id, st.Id AuthorizationScheduleTemplateId,
	stu.UserId, stu.UserCode, stu.UserCode + ' - ' + p.Fullname UserCodeName
	from [Authorization].AuthorizationScheduleTemplateUsers stu
	inner join [Authorization].AuthorizationScheduleTemplate st on st.Id = stu.AuthorizationScheduleTemplateId
	inner join Security.[UserInt] u on u.Id = stu.UserId
	inner join Security.PersonInt p on p.Id = u.IdPerson