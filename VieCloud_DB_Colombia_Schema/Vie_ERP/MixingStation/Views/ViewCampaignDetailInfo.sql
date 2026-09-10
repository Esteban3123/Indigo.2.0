
CREATE VIEW  [MixingStation].[ViewCampaignDetailInfo]
AS
SELECT cd.Id,
c.Id  AS CampaingId,
c1.Id AS IdMixingStation,
c1.Name AS MixingStation,
w.Id AS IdWarehouse,
w.Code + ' - ' + w.Name AS WareHouseRemaining
FROM MixingStation.CampaignDetail cd  WITH (NOLOCK)
INNER JOIN MixingStation.Campaign c WITH (NOLOCK) ON cd.CampaignId = c.Id
INNER JOIN MixingStation.CMConfiguration c1 WITH (NOLOCK) on c.CMConfigurationId = c1.Id
INNER JOIN MixingStation.CMWarehouse c2 WITH (NOLOCK) ON c2.IdMixingStation = c1.Id AND c2.WarehouseType = 6
INNER JOIN inventory.Warehouse w WITH (NOLOCK) ON c2.IdWarehouse = w.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información de detalle de cada campaña de preparación farmacéutica en la estación de mezclas, combinando el detalle de lote (CampaignDetail) con su campaña padre, la configuración de la estación de mezclas asociada y la bodega de remanentes (tipo 6) donde quedan los sobrantes del proceso. Permite identificar, para cada registro de detalle de campaña, cuál es la estación de mezclas responsable y qué bodega de inventario recibe los excedentes, mostrando el código y nombre de dicha bodega en un formato legible. Se usa en reportería y consultas operativas del ciclo de preparación de medicamentos en dosis unitaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignDetailInfo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewCampaignDetailInfo';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, por cada detalle de campaña de la estación de mezclas, la estación asociada y el almacén de tipo "remanente" (WarehouseType=6) configurado para esa estación.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada CampaignDetail debe tener una Campaign válida (INNER JOIN por CampaignId).; La Campaign debe referenciar una CMConfiguration existente vía CMConfigurationId.; La estación de mezcla (CMConfiguration) debe tener al menos un registro en CMWarehouse con WarehouseType = 6.; El almacén referenciado en CMWarehouse debe existir en inventory.Warehouse.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles de campaña cuya estación de mezcla tenga configurada una bodega de tipo 6 (remanente); los detalles sin esa configuración quedan excluidos por los INNER JOIN.; El campo WareHouseRemaining siempre se construye como ''Code - Name'' del almacén de inventory.Warehouse.; Todas las lecturas usan WITH (NOLOCK), por lo que pueden leerse filas no confirmadas (lecturas sucias).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de mezcla; Detalle de campaña; Estación de mezcla (Mixing Station); Configuración de estación de mezcla; Bodega/Almacén asociado a estación; Bodega de remanente (WarehouseType=6)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve una fila por cada combinación CampaignDetail × CMWarehouse(WarehouseType=6); si una estación tiene múltiples bodegas tipo 6, el detalle se duplicará.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c2.WarehouseType = 6 → Solo se incluyen las relaciones CMWarehouse cuyo tipo de bodega sea 6 (bodega remanente), excluyendo cualquier otro tipo de almacén configurado en la estación.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetail; MixingStation.Campaign; MixingStation.CMConfiguration; MixingStation.CMWarehouse; inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailInfo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewCampaignDetailInfo';
GO
