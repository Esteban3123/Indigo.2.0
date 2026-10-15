



CREATE VIEW [MixingStation].[ViewProductionOrder] 
AS
	WITH dataTemp AS
	(
		SELECT data.CampaignDetailId, SUM(data.Quantity) Quantity
		FROM
		(
			SELECT rd.CampaignDetailId, SUM(rd.Quantity) Quantity
			FROM MixingStation.RequestMixingStationDetail rd
			WHERE rd.CampaignDetailId IS NOT NULL
			GROUP BY rd.CampaignDetailId
		) data 
		GROUP BY data.CampaignDetailId
	)

	SELECT	CONCAT(cd.Id, '') as Id, 
			cd.Id as CampaignDetailId, 
			c.Id as CampaignId,
			cd.CampaignNumber, 
			CONCAT('Campaña # ', cd.CampaignNumber) as CampaignDescription,
			cd.ProductionLineId, 
			c.CMConfigurationId, 
			dt.Quantity,
			ps.Code as ProductionScheduleCode,
			cd.CampaignStatus,
			CASE cd.CampaignStatus
			WHEN 1 THEN 'Abierta'
			WHEN 2 THEN 'Cerrada'
			WHEN 3 THEN 'Bloqueada'
			WHEN 4 THEN 'Anulada'
			WHEN 5 THEN 'Procesada'
			WHEN 6 THEN 'Terminada'
			END CampaignStatusName,
			cd.Status,
			cd.ProcessingDate,
			cd.LabelConfirmationDate,
			cd.CreationDate as CampaignCreationDate,
			rl.WorkingAreaId,
			wa.Description as WorkingAreaName,
			ud.MSClass as UnitDoseTypeClass,
			cast(Iif(cd.LabelConfirmationDate is not null, 1, 0) as bit) as IsManagedLabel
	FROM MixingStation.Campaign c (NOLOCK)
	JOIN MixingStation.CampaignDetail cd (NOLOCK) ON c.Id = cd.CampaignId
	JOIN MixingStation.UnitDoseType ud (NOLOCK) on cd.UnitDoseTypeId = ud.Id
	LEFT JOIN MixingStation.ReleaseLine rl (NOLOCK) on cd.Id = rl.CampaignDetailId
	LEFT JOIN MixingStation.WorkingArea wa (NOLOCK) on wa.Id = rl.WorkingAreaId
	LEFT JOIN dataTemp as dt (NOLOCK) ON cd.Id = dt.CampaignDetailId
	LEFT JOIN MixingStation.ProductionScheduleDetail psd (NOLOCK) ON cd.Id = psd.CampaignDetailId
	LEFT JOIN MixingStation.ProductionSchedule ps (NOLOCK) ON psd.ProductionScheduleId = ps.Id
	WHERE cd.CampaignStatus <> 6