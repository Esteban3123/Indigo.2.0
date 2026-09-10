CREATE VIEW [MixingStation].[ViewParenteralNutritionLabelCampaign]
AS
	SELECT 
		CONCAT(CampaignDetailId, '-', rpds.Id) KeyView
		, cd.Id CampaignDetailId
		, rpds.Id RequestPackageDetailStatusId
	FROM MixingStation.CampaignDetail cd
	JOIN MixingStation.RequestMixingStationDetail rmsd ON cd.Id = rmsd.CampaignDetailId
	JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsd.Id = rpds.RequestMixingStationDetailId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que relaciona las campañas de preparación farmacéutica (lotes) con el estado de cada paquete o bolsa generado en la estación de mezclas para nutrición parenteral. Cruza el detalle de campaña (CampaignDetail) con los detalles de solicitud de preparación (RequestMixingStationDetail) y el historial de estados de paquetes (RequestPackageDetailStatus), generando una clave compuesta única por campaña-paquete. Sirve como base para la impresión y consulta de etiquetas de nutrición parenteral asociadas a una campaña de producción farmacéutica, permitiendo identificar cada bolsa dentro de su lote de origen.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewParenteralNutritionLabelCampaign';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewParenteralNutritionLabelCampaign';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la relación entre cada detalle de campaña de mezclas y los estados de paquete generados, con una clave compuesta para identificar de forma única cada vínculo destinado a la emisión de etiquetas de nutrición parenteral.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros relacionados en CampaignDetail, RequestMixingStationDetail y RequestPackageDetailStatus enlazados por CampaignDetailId y RequestMixingStationDetailId; de lo contrario, la fila no aparece en la vista.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen combinaciones donde el detalle de campaña tiene un detalle de solicitud asociado (INNER JOIN sobre CampaignDetailId) y dicho detalle tiene al menos un estado de paquete (INNER JOIN sobre RequestMixingStationDetailId).; La clave KeyView es única por par (CampaignDetailId, RequestPackageDetailStatusId) al construirse como concatenación de ambos identificadores.; No se filtra por estado ni tipo: la vista devuelve todos los vínculos existentes entre campaña, detalle de mezcla y estado de paquete.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de preparación (mezclas); Detalle de campaña; Solicitud a estación de mezclas; Paquete/estado de paquete; Nutrición parenteral; Etiqueta (label)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve una fila por cada combinación CampaignDetail → RequestMixingStationDetail → RequestPackageDetailStatus, con KeyView = CONCAT(CampaignDetailId,''-'',RequestPackageDetailStatusId).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetail; MixingStation.RequestMixingStationDetail; MixingStation.RequestPackageDetailStatus', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelCampaign';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewParenteralNutritionLabelCampaign';
GO
