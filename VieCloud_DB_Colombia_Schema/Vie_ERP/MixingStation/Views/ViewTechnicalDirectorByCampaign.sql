CREATE VIEW [MixingStation].[ViewTechnicalDirectorByCampaign]
AS
	SELECT	CONCAT(cmc.Id, '-', cd.Id, '-', u.Id) KeyView,
			cd.Id CampaignDetailId,
			u.Id UserId,
			u.UserCode,
			p.Fullname FullName
	FROM MixingStation.CampaignDetail cd
	JOIN MixingStation.Campaign c ON cd.CampaignId = c.Id
	JOIN MixingStation.CMConfiguration cmc ON c.CMConfigurationId = cmc.Id
	JOIN Security.[User] u ON cmc.IdDirector = u.Id
	JOIN Security.Person p ON u.IdPerson = p.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Muestra el director técnico responsable de cada lote de preparación (campaña) en la estación de mezclas farmacéuticas. Para cada detalle de campaña obtiene, a través de la configuración de la estación de mezclas (CMConfiguration), el usuario designado como director técnico junto con su código de usuario y nombre completo. Sirve para identificar qué profesional dirige o supervisa cada lote de preparación, siendo útil en reportes de trazabilidad, auditoría de responsabilidades y validación del director técnico por campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewTechnicalDirectorByCampaign';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewTechnicalDirectorByCampaign';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, por cada detalle de campaña de la estación de mezclas, el director técnico responsable obtenido a partir de la configuración de la estación, junto con su código de usuario y nombre completo.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTechnicalDirectorByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La configuración de la estación (CMConfiguration) debe tener asignado un IdDirector que exista en Security.User.; El usuario director debe estar asociado a una persona en Security.Person mediante IdPerson.; La campaña debe estar vinculada a una CMConfiguration vía CampaignId → Campaign.CMConfigurationId.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTechnicalDirectorByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los JOINs son INNER: solo se listan detalles de campaña cuya configuración tenga un director técnico válido con persona asociada; se excluyen campañas sin director o sin persona vinculada.; El director técnico de un detalle de campaña proviene siempre de CMConfiguration.IdDirector (no de la campaña ni del detalle directamente).; KeyView combina configuración de mezcla, detalle de campaña y usuario, garantizando unicidad por esos tres elementos.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTechnicalDirectorByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Director técnico; Campaña de mezcla; Detalle de campaña; Configuración de estación de mezcla; Usuario; Persona', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTechnicalDirectorByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por combinación CampaignDetail-Director, con KeyView = CONCAT(CMConfiguration.Id, ''-'', CampaignDetail.Id, ''-'', User.Id) como identificador único de la vista.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTechnicalDirectorByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetail; MixingStation.Campaign; MixingStation.CMConfiguration; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTechnicalDirectorByCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewTechnicalDirectorByCampaign';
GO
