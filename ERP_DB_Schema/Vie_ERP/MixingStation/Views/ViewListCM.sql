


CREATE view [MixingStation].[ViewListCM] 
as 

select CONCAT(cm.Id, '-', cmca.Id, '-', cmu.Id) Id,
cm.Code CMCode, cm.Name CMName, cm.Code + ' - ' + cm.Name CMCodeName,
RTRIM(LTRIM(ca.CODCENATE)) CareCenterCode, RTRIM(LTRIM(ca.NOMCENATE)) CareCenterName, RTRIM(LTRIM(ca.CODCENATE)) + ' - ' + RTRIM(LTRIM(ca.NOMCENATE)) CareCenterCodeName,
cmu.UserCode, cmu.UserId,
cm.Code + ' - ' + cm.Name + '   ' + RTRIM(LTRIM(ca.CODCENATE)) + ' - ' + RTRIM(LTRIM(ca.NOMCENATE)) Description,
cm.Id CMConfigurationId
from MixingStation.CMConfiguration cm
inner join MixingStation.CMCenterAttention cmca on cmca.IdMixingStation = cm.Id
inner join .ADCENATEN ca on ca.CODCENATE = cmca.CodeCenterAttention
inner join MixingStation.CMConfigurationUsers cmu on cmu.CMConfigurationId = cm.Id