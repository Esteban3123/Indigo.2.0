

CREATE view [MixingStation].[ViewListAuthorizeUsers] 
as 

SELECT 
CM.Id AS 'Id', 
CD.Id AS 'CampaignDetailId', 
CM.UserId, 
CM.UserCode AS 'Codigo', 
RTRIM(P.Fullname) AS 'Nombre',   
RTRIM(P.Identification) + ' - ' + RTRIM(P.Fullname) AS 'Usuario'
FROM MixingStation.CMConfigurationUsers CM
INNER JOIN MixingStation.Campaign C ON C.CMConfigurationId = CM.CMConfigurationId
INNER JOIN MixingStation.CampaignDetail CD ON CD.CampaignId = C.Id
inner join Security.[User] u on cm.UserId = u.Id
inner JOIN [Security].[Person] P ON u.IdPerson = p.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los usuarios autorizados para operar en cada lote de preparación (campaña) de la estación de mezclas farmacéuticas. Combina la configuración de usuarios habilitados (CMConfigurationUsers) con las campañas y sus detalles de lote, enriqueciendo cada registro con el nombre completo y la identificación (cédula) del profesional obtenidos desde el módulo de seguridad. Sirve para controlar y auditar qué personas están habilitadas para intervenir en un detalle de campaña específico, mostrando su código de usuario, nombre y una etiqueta combinada de identificación más nombre útil para selección en formularios o reportes de trazabilidad farmacéutica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListAuthorizeUsers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListAuthorizeUsers';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los usuarios autorizados para cada detalle de campaña de la estación de mezcla, mostrando su identificación y nombre completo.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListAuthorizeUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe estar registrado en MixingStation.CMConfigurationUsers asociado a una configuración de mezcla; La configuración debe tener al menos una Campaign con CampaignDetail para que el usuario aparezca; El usuario debe existir en Security.User y tener una Persona vinculada en Security.Person', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListAuthorizeUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen usuarios cuya configuración de mezcla tiene campañas con detalle (INNER JOIN en cadena Campaign→CampaignDetail); Solo se exponen usuarios con persona asociada en Security.Person (INNER JOIN obligatorio); El campo ''Usuario'' siempre concatena identificación y nombre con separador '' - '' aplicando RTRIM a ambos lados; Un mismo usuario aparecerá tantas veces como CampaignDetail existan bajo su configuración asignada', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListAuthorizeUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezcla; Configuración de mezcla; Campaña de preparación; Detalle de campaña; Usuario autorizado; Persona/identificación', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListAuthorizeUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewListAuthorizeUsers: Devuelve una fila por cada combinación CMConfigurationUsers × CampaignDetail, exponiendo Id de la asignación, CampaignDetailId, UserId, código y nombre formateado como ''Identificación - Nombre completo''', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListAuthorizeUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CMConfigurationUsers; MixingStation.Campaign; MixingStation.CampaignDetail; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListAuthorizeUsers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListAuthorizeUsers';
GO
