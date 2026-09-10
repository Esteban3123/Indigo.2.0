

CREATE VIEW [Authorization].[ViewListScheduleTemplateUsers]
AS

	select distinct stu.UserId, stu.UserCode, p.Fullname FullName, stu.UserCode + ' - ' + p.Fullname UserCodeName
	from [Authorization].AuthorizationScheduleTemplateUsers stu
	inner join Security.[UserInt] u on u.Id = stu.UserId
	inner join Security.PersonInt p on p.Id = u.IdPerson
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de usuarios vinculados a plantillas de agenda de autorización. Combina los datos de asignación de usuarios a plantillas con la información de seguridad e identidad de cada usuario, mostrando su código, nombre completo y una etiqueta combinada código-nombre. Se usa para poblar selectores o filtros en la gestión de plantillas de agendamiento de autorizaciones, permitiendo identificar rápidamente qué usuarios (por código y nombre) están asociados a cada plantilla.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListScheduleTemplateUsers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListScheduleTemplateUsers';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la lista única de usuarios asignados a plantillas de agenda de autorización con su código y nombre completo, formateados para selección/visualización.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListScheduleTemplateUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada UserId en AuthorizationScheduleTemplateUsers debe existir en Security.UserInt.; Cada UserInt debe tener un IdPerson asociado en Security.PersonInt.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListScheduleTemplateUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan usuarios que tienen vínculo válido en AuthorizationScheduleTemplateUsers, UserInt y PersonInt (los INNER JOIN excluyen huérfanos).; Los resultados son únicos por usuario gracias al SELECT DISTINCT, evitando duplicados cuando un usuario está asignado a múltiples plantillas.; El campo UserCodeName se construye siempre como ''UserCode - Fullname''.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListScheduleTemplateUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'plantilla de agenda de autorización; usuario interno; persona interna', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListScheduleTemplateUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewListScheduleTemplateUsers: Devuelve filas distintas con UserId, UserCode, FullName y la concatenación ''UserCode - Fullname'' solo para usuarios presentes en AuthorizationScheduleTemplateUsers que tengan correspondencia en UserInt y PersonInt.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListScheduleTemplateUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationScheduleTemplateUsers; Security.UserInt; Security.PersonInt', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListScheduleTemplateUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListScheduleTemplateUsers';
GO
