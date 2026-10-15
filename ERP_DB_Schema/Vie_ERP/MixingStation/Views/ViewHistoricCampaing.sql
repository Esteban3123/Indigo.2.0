



CREATE VIEW [MixingStation].[ViewHistoricCampaing] 
AS
WITH dataTemp
AS
(SELECT data.CampaignDetailId
	   ,SUM(data.Quantity) Quantity
	FROM (SELECT
			rd.CampaignDetailId
		   ,SUM(rd.Quantity) Quantity
		FROM MixingStation.RequestMixingStationDetail rd
		WHERE rd.CampaignDetailId IS NOT NULL
		GROUP BY rd.CampaignDetailId) data
	GROUP BY data.CampaignDetailId)

SELECT
	CONCAT(cd.Id, '') Id
   ,cd.Id CampaignDetailId
   ,c.Id CampaignId
   ,cd.CampaignNumber
   ,CONCAT('Campaña # ', cd.CampaignNumber) CampaignDescription
   ,cd.ProductionLineId
   ,c.CMConfigurationId
   ,dataTemp.Quantity
   ,ps.Code ProductionScheduleCode
   ,cd.CampaignStatus
   ,CASE cd.CampaignStatus
		WHEN 1 THEN 'Abierta'
		WHEN 2 THEN 'Cerrada'
		WHEN 3 THEN 'Bloqueada'
		WHEN 4 THEN 'Anulada'
		WHEN 5 THEN 'Procesada'
		WHEN 6 THEN 'Terminada'
	END CampaignStatusName
   ,cd.ProcessingDate
   ,cd.LabelConfirmationDate
   ,cd.CreationDate AS CampaignCreationDate
   ,rl.WorkingAreaId
   ,wa.Description AS WorkingAreaName
   ,(SELECT TOP 1 ce.IdWarehouse
		FROM MixingStation.CMWarehouse ce 
		LEFT JOIN MixingStation.CMConfiguration cn ON c.CMConfigurationId = cn.Id
		WHERE ce.WarehouseType = 1 AND ce.IdMixingStation = cn.Id)
	AS MateriaPrimaStock
   ,(SELECT TOP 1 ce.IdWarehouse
		FROM MixingStation.CMWarehouse ce
		LEFT JOIN MixingStation.CMConfiguration cn ON c.CMConfigurationId = cn.Id
		WHERE ce.WarehouseType = 2 AND ce.IdMixingStation = cn.Id)
	AS Almacen
   ,(SELECT TOP 1 ce.IdWarehouse
		FROM MixingStation.CMWarehouse ce
		LEFT JOIN MixingStation.CMConfiguration cn ON c.CMConfigurationId = cn.Id
		WHERE ce.WarehouseType = 6 AND ce.IdMixingStation = cn.Id)
	AS Remanente
   ,ut.Description AS NameUnitDoseType
   , ISNULL((SELECT TOP 1  cr.EntityId 
			FROM MixingStation.CampaignReports cr
			WHERE cd.Id = cr.campaignDetailId)
			, 0) IdTransferOrder
	, ut.MSClass
FROM MixingStation.Campaign c
JOIN MixingStation.CampaignDetail cd ON c.Id = cd.CampaignId
LEFT JOIN MixingStation.UnitDoseType ut ON ut.Id = cd.UnitDoseTypeId
LEFT JOIN dataTemp ON cd.Id = dataTemp.CampaignDetailId
LEFT JOIN MixingStation.ProductionScheduleDetail psd ON cd.Id = psd.CampaignDetailId
LEFT JOIN MixingStation.ProductionSchedule ps ON psd.ProductionScheduleId = ps.Id
LEFT JOIN MixingStation.ReleaseLine rl (NOLOCK) ON cd.Id = rl.CampaignDetailId
LEFT JOIN MixingStation.WorkingArea wa (NOLOCK)ON wa.Id = rl.WorkingAreaId
WHERE cd.CampaignStatus = 6