CREATE VIEW [MixingStation].[ViewCampaignMainInfo]
AS 
	SELECT cd.Id,
			cd.CampaignId,
			cd.CampaignNumber,
			utd.MSClass,
			cd.UnitDoseTypeId,
			utd.[Description] UnitDoseTypeDescription,
			cd.CampaignStatus,
			CASE cd.CampaignStatus
				WHEN 1 THEN 'ABIERTA'
				WHEN 2 THEN 'CERRADA'
				WHEN 3 THEN 'BLOQUEADA'
				WHEN 4 THEN 'ANULADA'
				WHEN 5 THEN 'PROCESADA'
				WHEN 6 THEN 'TERMINADA'
				ELSE 'SIN ESTADO'
			END DescriptionStatus,
			CONCAT(vur.QFQualityCode, ' - ', vur.QFQualityName) QFQualityCodeName,
			CONCAT(vur.QFProductionCode, ' - ', vur.QFProductionName) QFProductionCodeName,
			CASE cd.CampaignStatus
				WHEN 1 THEN cd.CreationDate
				WHEN 2 THEN cd.ModificationDate 
				WHEN 3 THEN cd.ModificationDate
				WHEN 4 THEN cd.ModificationDate
				WHEN 5 THEN cd.ProcessingDate
				WHEN 6 THEN cd.FinishDate
				ELSE cd.CreationDate 
			END ModificationStatusDate,
			cd.CreationDate,
			cd.ProcessingDate
	FROM MixingStation.CampaignDetail cd WITH (NOLOCK)
	JOIN MixingStation.UnitDoseType utd WITH (NOLOCK) ON cd.UnitDoseTypeId = utd.Id
	JOIN MixingStation.ViewCampaignDetailUser vur ON cd.Id = vur.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información principal de cada campaña (lote) de preparación farmacéutica en la estación de mezclas. Combina el detalle de la campaña con el tipo de dosis unitaria y los usuarios responsables de calidad y producción asignados al lote. Traduce el estado numérico de la campaña a su descripción legible (Abierta, Cerrada, Bloqueada, Anulada, Procesada, Terminada) y calcula la fecha de referencia correspondiente a cada estado del ciclo de vida del lote. Se usa para consultar y reportar el estado actual, el tipo de preparación, la clase de estación de mezclas y los responsables de calidad y producción de cada campaña farmacéutica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignMainInfo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignMainInfo';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información principal de cada campaña de la estación de mezclas con su tipo de dosis, responsables de calidad y producción, y la fecha relevante según el estado actual.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignMainInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada CampaignDetail debe tener un UnitDoseType asociado (JOIN obligatorio); Cada CampaignDetail debe tener registro correspondiente en ViewCampaignDetailUser (JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignMainInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado numérico de la campaña se mapea a un único literal: 1=ABIERTA, 2=CERRADA, 3=BLOQUEADA, 4=ANULADA, 5=PROCESADA, 6=TERMINADA; cualquier otro valor se reporta como ''SIN ESTADO''; La fecha de modificación de estado depende del estado: estados intermedios (2,3,4) usan ModificationDate; estado 5 usa ProcessingDate; estado 6 usa FinishDate; estado 1 y desconocidos usan CreationDate; Los códigos y nombres de QF (calidad y producción) se exponen concatenados con el separador '' - ''; Solo se exponen campañas que tengan tanto tipo de dosis como usuarios QF asignados (debido a los INNER JOIN); Las lecturas se realizan con NOLOCK sobre CampaignDetail y UnitDoseType, permitiendo lecturas sucias', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignMainInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de mezclas; Dosis unitaria; Estado de campaña (abierta/cerrada/bloqueada/anulada/procesada/terminada); Químico Farmacéutico de Calidad (QF Quality); Químico Farmacéutico de Producción (QF Production); Clase de estación de mezclas (MSClass)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignMainInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.CampaignDetail: Devuelve una fila por cada campaña con su estado traducido a etiqueta legible y la fecha de cambio de estado correspondiente', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignMainInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CampaignStatus = 1 → DescriptionStatus=''ABIERTA'' y ModificationStatusDate = CreationDate; si CampaignStatus = 2 → DescriptionStatus=''CERRADA'' y ModificationStatusDate = ModificationDate; si CampaignStatus = 3 → DescriptionStatus=''BLOQUEADA'' y ModificationStatusDate = ModificationDate; si CampaignStatus = 4 → DescriptionStatus=''ANULADA'' y ModificationStatusDate = ModificationDate; si CampaignStatus = 5 → DescriptionStatus=''PROCESADA'' y ModificationStatusDate = ProcessingDate; si CampaignStatus = 6 → DescriptionStatus=''TERMINADA'' y ModificationStatusDate = FinishDate; si CampaignStatus fuera de 1..6 → DescriptionStatus=''SIN ESTADO'' y ModificationStatusDate = CreationDate', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignMainInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetail; MixingStation.UnitDoseType; MixingStation.ViewCampaignDetailUser', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignMainInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignMainInfo';
GO
