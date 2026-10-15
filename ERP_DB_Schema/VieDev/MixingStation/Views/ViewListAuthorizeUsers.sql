


CREATE view [MixingStation].[ViewListAuthorizeUsers] 
as 

SELECT 
CM.Id AS 'Id', 
CD.Id AS 'CampaignDetailId', 
CM.UserId, 
CM.UserCode AS 'Codigo', 
RTRIM(P.Fullname) AS 'Nombre',   
RTRIM(P.Identification) + ' - ' + RTRIM(P.Fullname) AS 'Usuario'
FROM MixingStation.CMConfigurationUsers CM
INNER JOIN MixingStation.Campaign C ON C.CMConfigurationId = CM.CMConfigurationId
INNER JOIN MixingStation.CampaignDetail CD ON CD.CampaignId = C.Id
inner join Security.[User] u on cm.UserId = u.Id
inner JOIN [Security].[Person] P ON u.IdPerson = p.Id