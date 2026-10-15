
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