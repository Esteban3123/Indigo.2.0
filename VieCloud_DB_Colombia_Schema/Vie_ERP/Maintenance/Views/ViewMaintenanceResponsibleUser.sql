CREATE VIEW [Maintenance].[ViewMaintenanceResponsibleUser]
AS
SELECT	mr.Id,
		mr.Code,
		u.UserCode,
		mr.ResponsibleRole
FROM Maintenance.MaintenanceResponsible mr WITH (NOLOCK)
JOIN Common.ThirdParty tp WITH (NOLOCK) ON mr.ThirdPartyId = tp.Id
JOIN Security.Person sp ON tp.Nit = sp.Identification
JOIN Security.[User] u ON sp.Id = u.IdPerson
WHERE mr.Status = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los responsables de mantenimiento activos que tienen un usuario registrado en el sistema de seguridad. Cruza los responsables de mantenimiento con sus terceros asociados, y a partir del NIT del tercero identifica a la persona y su usuario de acceso, devolviendo el identificador interno del responsable, su código, el código de usuario del sistema y el rol que desempeña en mantenimiento. Sirve para consultas y controles donde se necesita vincular a un técnico o contratista de mantenimiento con su cuenta de usuario activa en la plataforma.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewMaintenanceResponsibleUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'VIEW', @level1name = N'ViewMaintenanceResponsibleUser';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los responsables de mantenimiento activos junto con el código de usuario del sistema asociado, enlazando tercero–persona–usuario por identificación.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe correspondencia entre Common.ThirdParty.Nit y Security.Person.Identification para poder enlazar el tercero con la persona.; La persona enlazada debe tener un usuario registrado en Security.User.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen responsables de mantenimiento activos (Status = 1).; Cada responsable expuesto debe estar vinculado a un Tercero cuyo NIT coincida con la Identificación de una Persona registrada en Seguridad.; Cada responsable expuesto debe tener un Usuario asociado a la Persona (existe registro en Security.User para esa persona).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Responsable de mantenimiento; Tercero; Persona; Usuario del sistema; Rol de responsable', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Maintenance.MaintenanceResponsible: Devuelve únicamente filas de Maintenance.MaintenanceResponsible con Status = 1, descartando responsables inactivos.; [RETURN_RESULT] Common.ThirdParty: El emparejamiento tercero↔persona se realiza por igualdad ThirdParty.Nit = Person.Identification; si no existe coincidencia, el responsable no aparece en la vista.; [RETURN_RESULT] Security.User: Solo se incluyen responsables cuya persona asociada (por identificación) tenga un usuario en Security.User (sp.Id = u.IdPerson).', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.MaintenanceResponsible; Common.ThirdParty; Security.Person; Security.User', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'VIEW', @level1name=N'ViewMaintenanceResponsibleUser';
GO
