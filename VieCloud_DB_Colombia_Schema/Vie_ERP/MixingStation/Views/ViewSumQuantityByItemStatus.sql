CREATE VIEW MixingStation.ViewSumQuantityByItemStatus
AS
SELECT 
    cd.Id,
    SUM(rpds.Status) AS TotalQuantity,
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
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Agrega por detalle de campaña los conteos de paquetes preparados en la estación de mezclas según su estado (producción, terminado, liberado, reprocesado, rechazado, cancelado).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByItemStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros enlazados entre CampaignDetail, RequestMixingStationDetail y RequestPackageDetailStatus mediante sus llaves foráneas', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByItemStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los estados manejados son los valores 1 a 6 (Producción, Terminado, Liberado, Reprocesado, Rechazado, Cancelado); Solo se incluyen detalles de campaña que tengan al menos una solicitud y un estado de paquete asociados (JOIN interno); El resultado se agrupa siempre por el identificador del detalle de campaña', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByItemStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas; Campaña de preparación; Solicitud de preparación farmacéutica; Paquete (bolsa/envase); Estados de paquete: Producción, Terminado, Liberado, Reprocesado, Rechazado, Cancelado', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByItemStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve por cada CampaignDetail.Id la suma del campo Status y sumas condicionales por valor de Status (1..6) que corresponden a estados de paquete', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByItemStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rpds.Status = 1 → Suma como ProductionQuantity; si rpds.Status = 2 → Suma como FinishedQuantity; si rpds.Status = 3 → Suma como ReleasedQuantity; si rpds.Status = 4 → Suma como ReprocessedQuantity; si rpds.Status = 5 → Suma como RejectedQuantity; si rpds.Status = 6 → Suma como CancelledQuantity', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByItemStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetail; MixingStation.RequestMixingStationDetail; MixingStation.RequestPackageDetailStatus', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByItemStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewSumQuantityByItemStatus';
GO
