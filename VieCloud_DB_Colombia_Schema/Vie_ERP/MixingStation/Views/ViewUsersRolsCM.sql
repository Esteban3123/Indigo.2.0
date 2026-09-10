

CREATE VIEW [MixingStation].[ViewUsersRolsCM] 
AS
		Select CONCAT(cdu.Id, ' ') Id
		, cdu.CampaignDetailId
		, cdu.UserId
		, cdu.UserRole
		, RTRIM(P.Fullname) AS 'Nombre'   
		, RTRIM(P.Identification) + ' - ' + RTRIM(P.Fullname) AS 'Usuario'
		From MixingStation.CampaignDetailUsers cdu with(nolock)
		inner join Security.[User] u on cdu.UserId = u.Id
		inner JOIN [Security].[Person] P ON u.IdPerson = p.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra los usuarios asignados a los detalles de campaña en la estación de mezcla (MixingStation), junto con su rol dentro de cada campaña. Combina la asignación usuario-campaña con los datos de seguridad del sistema para exponer el nombre completo y la identificación (cédula o documento) de cada usuario. Se utiliza para reportería y consulta de los participantes o responsables por detalle de campaña, mostrando quién tiene qué rol en cada segmento de campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewUsersRolsCM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewUsersRolsCM';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los usuarios asignados a detalles de campaña junto con su rol y datos de identificación/nombre completo de la persona vinculada.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewUsersRolsCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada usuario en CampaignDetailUsers debe existir en Security.User; Cada usuario debe tener una persona asociada (IdPerson) registrada en Security.Person', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewUsersRolsCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Id se devuelve concatenado con un espacio (CONCAT(cdu.Id,'' '')), forzando salida tipo texto; Se aplica RTRIM a Fullname e Identification para eliminar espacios sobrantes a la derecha; El campo ''Usuario'' siempre tiene formato ''Identificación - Nombre Completo''; El INNER JOIN con Security.User y Security.Person excluye asignaciones sin usuario o sin persona vinculada; Lectura con NOLOCK sobre CampaignDetailUsers: permite lecturas sucias sin bloqueos', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewUsersRolsCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña; Detalle de campaña; Usuario; Rol de usuario; Persona; Identificación', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewUsersRolsCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.CampaignDetailUsers: Devuelve una fila por cada asignación usuario-detalle de campaña, enriquecida con nombre e identificación de la persona.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewUsersRolsCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetailUsers; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewUsersRolsCM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewUsersRolsCM';
GO
