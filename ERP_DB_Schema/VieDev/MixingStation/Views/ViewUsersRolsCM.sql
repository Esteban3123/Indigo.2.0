



CREATE VIEW [MixingStation].[ViewUsersRolsCM] 
AS
		Select CONCAT(cdu.Id, ' ') Id
		, cdu.CampaignDetailId
		, cdu.UserId
		, cdu.UserRole
		, RTRIM(P.Fullname) AS 'Nombre'   
		, RTRIM(P.Identification) + ' - ' + RTRIM(P.Fullname) AS 'Usuario'
		From MixingStation.CampaignDetailUsers cdu with(nolock)
		inner join Security.[User] u on cdu.UserId = u.Id
		inner JOIN [Security].[Person] P ON u.IdPerson = p.Id