
CREATE VIEW [MixingStation].[ViewRequestDetailStatus] 
AS
	
			WITH dataTemp AS
	(
		SELECT	
			data.Status, 
			data.CampaignDetailId
		FROM
		(
			SELECT	
				rpds.Status,		
				rmsd.CampaignDetailId 
			FROM MixingStation.RequestPackageDetailStatus rpds
			INNER JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
		WHERE rmsd.CampaignDetailId IS NOT NULL
		GROUP BY rpds.Status, rmsd.CampaignDetailId
		) data 
		GROUP BY data.Status, data.CampaignDetailId
	)

		select 
			concat(dt.Status, '' ) Id
			,cd.Id as CampaignDetailId
			,(	SELECT COUNT(rpds.Id) 
				FROM MixingStation.RequestPackageDetailStatus rpds
				JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = rpds.RequestMixingStationDetailId
				WHERE rmsd.CampaignDetailId = cd.Id AND dt.Status = rpds.Status) AS Quantity
			, CASE dt.Status
			WHEN 1 THEN 'Producción'
			WHEN 2 THEN 'Terminado'
			WHEN 3 THEN 'Liberado'
			WHEN 4 THEN 'Reproceso'
			WHEN 5 THEN 'Rechazado'
			WHEN 6 THEN 'Anulado'
			END StatusName
		FROM MixingStation.Campaign c WITH(NOLOCK)
		JOIN MixingStation.CampaignDetail cd WITH(NOLOCK) on c.Id = cd.CampaignId
		JOIN dataTemp dt WITH(NOLOCK) on dt.CampaignDetailId = cd.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el conteo de paquetes (bolsas/envases) de preparación farmacéutica agrupados por estado y por detalle de campaña (lote) en la estación de mezclas. Para cada detalle de campaña muestra cuántos paquetes se encuentran en cada estado del ciclo de vida: Producción, Terminado, Liberado, Reproceso, Rechazado o Anulado. Integra las tablas de campañas, detalles de campaña, solicitudes de preparación y el historial de estados de paquetes para ofrecer un resumen operativo del avance de cada lote farmacéutico. Sirve para reportería de seguimiento de producción y control de calidad en la farmacia de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewRequestDetailStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewRequestDetailStatus';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resume por cada detalle de campaña de mezcla cuántos paquetes existen en cada estado del ciclo de vida (Producción, Terminado, Liberado, Reproceso, Rechazado, Anulado), traduciendo el código de estado a su nombre.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestDetailStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'RequestMixingStationDetail debe tener CampaignDetailId no nulo para ser considerado; Debe existir relación entre RequestPackageDetailStatus.RequestMixingStationDetailId y RequestMixingStationDetail.Id; El CampaignDetail debe pertenecer a una Campaign existente', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestDetailStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se incluyen detalles de solicitud de mezcla cuyo CampaignDetailId no es nulo (WHERE rmsd.CampaignDetailId IS NOT NULL); Los estados reconocidos del ciclo de vida del paquete son exactamente 1..6; otros valores quedan sin nombre; Quantity es siempre el COUNT de RequestPackageDetailStatus.Id correspondientes al par (CampaignDetailId, Status); Una misma combinación (Status, CampaignDetailId) aparece una sola vez gracias al doble GROUP BY del CTE dataTemp', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestDetailStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas (MixingStation); Campaña de preparación farmacéutica; Detalle de campaña; Paquete/bolsa de preparación; Estados del paquete: Producción, Terminado, Liberado, Reproceso, Rechazado, Anulado', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestDetailStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas (Id=Status como texto, CampaignDetailId, Quantity=conteo de paquetes en ese estado para ese detalle de campaña, StatusName) sólo para combinaciones (Status, CampaignDetailId) que existan en RequestPackageDetailStatus con RequestMixingStationDetail.CampaignDetailId no nulo', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestDetailStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si dt.Status = 1 → StatusName = ''Producción''; si dt.Status = 2 → StatusName = ''Terminado''; si dt.Status = 3 → StatusName = ''Liberado''; si dt.Status = 4 → StatusName = ''Reproceso''; si dt.Status = 5 → StatusName = ''Rechazado''; si dt.Status = 6 → StatusName = ''Anulado'' else StatusName = NULL para cualquier otro valor de Status', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestDetailStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetail; MixingStation.Campaign; MixingStation.CampaignDetail', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestDetailStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestDetailStatus';
GO
