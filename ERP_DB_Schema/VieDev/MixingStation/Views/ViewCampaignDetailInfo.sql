
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