

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de usuarios del sistema clasificados por su tipo de acceso y asociados al contenedor o empresa (tenant/container) al que pertenecen, utilizada por la estación de mezclas (MixingStation). Integra tres segmentos: usuarios globales (tipo 3), usuarios con permisos explícitos por empresa/contenedor (con Permission=1), y usuarios asociados a un tenant específico (tipo 2). Para cada usuario expone su código de usuario, rol, nombre completo y cargo laboral vigente, obteniendo este último cruzando la identidad del usuario con el tercero en nómina y su contrato activo. Sirve como fuente de consulta para asignar o filtrar usuarios operativos según el contexto de empresa o sede en la que deben operar.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListUserByContainerCM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListUserByContainerCM';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la lista de usuarios internos asociados a contenedores/empresas combinando superusuarios, usuarios con permiso por compañía y usuarios de tenant, enriquecidos con su cargo laboral.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListUserByContainerCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las personas deben estar registradas en Security.PersonInt vinculadas al usuario por IdPerson; Para obtener cargo, la identificación de la persona debe coincidir con el NIT en Common.ThirdParty y existir un empleado con contrato vigente (Valid=1 y Status=1)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListUserByContainerCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un usuario UserType=''3'' siempre se reporta con IdContainer=0 y TenantId=0; Un usuario UserType=''2'' siempre se reporta con IdContainer=0 y el TenantId asociado; Usuarios con UserType distinto de ''2'' y ''3'' solo aparecen si tienen Permission=1 en PermissionCompanyInt; Solo se considera el cargo derivado de contratos con Valid=1 y Status=1; El vínculo persona-empleado requiere igualdad entre PersonInt.Identification y ThirdParty.Nit', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListUserByContainerCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario interno; Tipo de usuario; Permiso por contenedor/empresa; Tenant; Empleado; Contrato laboral vigente; Cargo/Posición', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListUserByContainerCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas de usuarios con Id, IdPerson, UserCode, RollCode, Fullname, PositionName, TenantId e IdContainer combinando tres orígenes según el tipo de usuario', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListUserByContainerCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UserType = ''3'' (superusuario/global) → Incluye al usuario con TenantId=0 e IdContainer=0 (sin asociación a contenedor ni tenant específico); si UserType NOT IN (''2'',''3'') AND PermissionCompanyInt.Permission=1 → Incluye una fila por cada contenedor sobre el que tiene permiso, fijando TenantId=0 e IdContainer = c.IdContainer; si UserType = ''2'' (usuario de tenant) → Incluye una fila por cada tenant asociado en TenantUsersInt, fijando TenantId = t.TenantId e IdContainer=0; si Contrato con Valid=1 AND Status=1 → Toma el cargo (Position.Name) del contrato vigente; en caso contrario PositionName queda nulo else PositionName nulo por LEFT JOIN sin coincidencia', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListUserByContainerCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.UserInt; Security.PersonInt; Common.ThirdParty; Payroll.Employee; Payroll.Contract; Payroll.Position; Security.PermissionCompanyInt; Security.TenantUsersInt', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListUserByContainerCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListUserByContainerCM';
GO
