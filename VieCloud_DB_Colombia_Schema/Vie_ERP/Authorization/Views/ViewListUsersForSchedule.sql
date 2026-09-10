

CREATE VIEW [Authorization].[ViewListUsersForSchedule]
AS

	select stu.Id, st.Id AuthorizationScheduleTemplateId,
	stu.UserId, stu.UserCode, stu.UserCode + ' - ' + p.Fullname UserCodeName
	from [Authorization].AuthorizationScheduleTemplateUsers stu
	inner join [Authorization].AuthorizationScheduleTemplate st on st.Id = stu.AuthorizationScheduleTemplateId
	inner join Security.[UserInt] u on u.Id = stu.UserId
	inner join Security.PersonInt p on p.Id = u.IdPerson
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de usuarios habilitados para participar en agendas de autorización de servicios de salud, vinculando cada usuario a su plantilla de programación correspondiente. Combina la asignación de usuarios por plantilla, los datos de seguridad del usuario y el nombre completo de la persona, ofreciendo una vista consolidada con el código y nombre del autorizador para cada esquema de agenda. Se usa en procesos de configuración y consulta de quiénes pueden gestionar o están asignados a una plantilla de cronograma de autorizaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListUsersForSchedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListUsersForSchedule';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la lista de usuarios asociados a cada plantilla de agenda de autorización, mostrando su código y nombre completo combinados para selección en interfaces.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListUsersForSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario asignado a la plantilla debe existir en Security.UserInt y tener una persona vinculada en Security.PersonInt (IdPerson válido).; La plantilla referenciada debe existir en Authorization.AuthorizationScheduleTemplate.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListUsersForSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan asignaciones cuya plantilla, usuario y persona existen (INNER JOIN en las cuatro tablas).; El campo UserCodeName siempre se construye como ''UserCode - Fullname'' concatenando código del usuario y nombre completo de la persona.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListUsersForSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'plantilla de agenda de autorización; usuario interno; persona interna; asignación de usuarios a plantilla', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListUsersForSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationScheduleTemplateUsers; Authorization.AuthorizationScheduleTemplate; Security.UserInt; Security.PersonInt', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListUsersForSchedule';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListUsersForSchedule';
GO
