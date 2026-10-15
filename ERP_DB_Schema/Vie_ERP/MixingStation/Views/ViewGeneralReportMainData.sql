CREATE VIEW [MixingStation].[ViewGeneralReportMainData] 
AS
	WITH CampaignQuantityTemp AS
	(
		SELECT data.CampaignDetailId, SUM(data.Quantity) Quantity
		FROM
		(
			SELECT rd.CampaignDetailId, SUM(rd.Quantity) Quantity
			FROM MixingStation.RequestMixingStationDetail rd
			GROUP BY rd.CampaignDetailId
		) data 
		GROUP BY data.CampaignDetailId
	)

	SELECT 
		CONCAT(c.Id, ' - ', cd.Id ) KeyView,
		cd.Id CampaignDetailId,
		cc.Name MixingStationName,
		cd.CampaignNumber,
		CONCAT (ud.Code, ' - ', ud.Description) UnitDoseType,
		CONCAT (pl.Code, ' - ', pl.Name) ProductionLine,
		wa.Description WorkArea,
		'Terminada' CampaignStatus,
		cd.ProcessingDate,
		cd.FinishDate,
		CONCAT(
			IIF(DATEDIFF(DAY, cd.ProcessingDate, cd.FinishDate) > 0, 
				CONCAT(DATEDIFF(DAY, cd.ProcessingDate, cd.FinishDate), ' días con '), ''
			), 
			CONVERT(varchar, DATEADD(second, 
					DATEDIFF(SECOND, cd.ProcessingDate, cd.FinishDate), 0), 108)
		) Duration,
		cqt.Quantity,
		cduTD.Usuario TechnicalDirector,
		cduQC.Usuario QualityChemical,
		cduPC.Usuario ProductionChemical,
		ISNULL(cduPA.Usuario, '') ProductionAssistant
	FROM MixingStation.CMConfiguration cc
	JOIN MixingStation.Campaign c (NOLOCK) ON cc.Id = c.CMConfigurationId
	JOIN MixingStation.CampaignDetail cd (NOLOCK) ON c.Id = cd.CampaignId
	JOIN MixingStation.UnitDoseType ud (NOLOCK) ON cd.UnitDoseTypeId = ud.Id
	JOIN MixingStation.ProductionLine pl (NOLOCK) ON cd.ProductionLineId = pl.Id
	JOIN MixingStation.ReleaseLine rl (NOLOCK) ON cd.Id = rl.CampaignDetailId
	JOIN MixingStation.WorkingArea wa (NOLOCK) ON rl.WorkingAreaId = wa.Id
	JOIN CampaignQuantityTemp cqt (NOLOCK) ON cd.Id = cqt.CampaignDetailId
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduQC (NOLOCK) ON cd.Id = cduQC.CampaignDetailId AND cduQC.UserRole = 1 --QF Calidad
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPC (NOLOCK) ON cd.Id = cduPC.CampaignDetailId AND cduPC.UserRole = 2 --QF Producción 
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduPA (NOLOCK) ON cd.Id = cduPA.CampaignDetailId AND cduPA.UserRole = 3 --Auxiliar de Central de Mezclas
	LEFT JOIN [MixingStation].[ViewUsersRolsCM] cduTD (NOLOCK) ON cd.Id = cduTD.CampaignDetailId AND cduTD.UserRole = 4 --Director técnico
	WHERE cd.CampaignStatus = 6