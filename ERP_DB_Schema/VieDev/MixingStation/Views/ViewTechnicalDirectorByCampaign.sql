CREATE VIEW [MixingStation].[ViewTechnicalDirectorByCampaign]
AS
	SELECT	CONCAT(cmc.Id, '-', cd.Id, '-', u.Id) KeyView,
			cd.Id CampaignDetailId,
			u.Id UserId,
			u.UserCode,
			p.Fullname FullName
	FROM MixingStation.CampaignDetail cd
	JOIN MixingStation.Campaign c ON cd.CampaignId = c.Id
	JOIN MixingStation.CMConfiguration cmc ON c.CMConfigurationId = cmc.Id
	JOIN Security.[User] u ON cmc.IdDirector = u.Id
	JOIN Security.Person p ON u.IdPerson = p.Id