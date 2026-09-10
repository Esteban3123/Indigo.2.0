CREATE VIEW MixingStation.ViewSumQuantityByPreparationStatus
AS
SELECT 
    cd.Id,
    SUM(rpds.Status) AS RequestedQuantity,
    SUM(IIF(rpds.Status = 1, rpds.Status, 0)) AS ProductionQuantity,
	SUM(IIF(rpds.Status = 2, rpds.Status, 0)) AS FinishedQuantity,
	SUM(IIF(rpds.Status = 3, rpds.Status, 0)) AS ReleasedQuantity,
	SUM(IIF(rpds.Status = 4, rpds.Status, 0)) AS ReprocessedQuantity,
	SUM(IIF(rpds.Status = 5, rpds.Status, 0)) AS RejectedQuantity,
	SUM(IIF(rpds.Status = 6, rpds.Status, 0)) AS CancelledQuantity
FROM MixingStation.CampaignDetail cd
JOIN MixingStation.RequestMixingStationDetail rmsd ON cd.Id = rmsd.CampaignDetailId
JOIN MixingStation.RequestPackageDetailStatus rpds ON rmsd.Id = rpds.RequestMixingStationDetailId
GROUP BY cd.Id

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resume, por cada detalle de campaña de la estación de mezclas, las cantidades agregadas de paquetes según su estado de preparación (producción, finalizado, liberado, reprocesado, rechazado, cancelado).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByPreparationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir relaciones consistentes entre CampaignDetail, RequestMixingStationDetail y RequestPackageDetailStatus para que un detalle de campaña aparezca en el resultado.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByPreparationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen detalles de campaña que tengan al menos un paquete con estado registrado (debido al INNER JOIN).; Los códigos de estado válidos del paquete son 1=Producción, 2=Finalizado, 3=Liberado, 4=Reprocesado, 5=Rechazado, 6=Cancelado.; Las cantidades por estado se obtienen sumando el valor del propio código de estado (no un conteo puro): cada registro aporta su valor numérico de estado al total correspondiente.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByPreparationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de preparación (CampaignDetail); Solicitud a estación de mezclas; Paquete de preparación farmacéutica; Estados de preparación: producción, finalizado, liberado, reprocesado, rechazado, cancelado', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByPreparationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada CampaignDetail.Id con sumatorias agregadas por estado del paquete tras agrupar por cd.Id.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByPreparationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rpds.Status = 1 → Se contabiliza como ProductionQuantity; si rpds.Status = 2 → Se contabiliza como FinishedQuantity; si rpds.Status = 3 → Se contabiliza como ReleasedQuantity; si rpds.Status = 4 → Se contabiliza como ReprocessedQuantity; si rpds.Status = 5 → Se contabiliza como RejectedQuantity; si rpds.Status = 6 → Se contabiliza como CancelledQuantity', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByPreparationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetail; MixingStation.RequestMixingStationDetail; MixingStation.RequestPackageDetailStatus', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByPreparationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByPreparationStatus';
GO
