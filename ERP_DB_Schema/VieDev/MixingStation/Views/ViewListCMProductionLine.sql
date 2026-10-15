









CREATE view [MixingStation].[ViewListCMProductionLine] 
as 

select CONCAT(cm.Id, '-', cmPl.Id, '-', cmU.Id) Id,
cm.Id CMConfigurationId, 
cm.Code + ' - ' + cm.Name CMConfigurationCodeName, 
pl.Id ProductionLineId,
pl.Code + ' - ' + pl.Name ProductionLineCodeName, 
cmU.UserCode,
'CM: ' + cm.Code + ' - ' + cm.Name + ' LP: ' + pl.Code + ' - ' + pl.Name CodeName
from MixingStation.CMConfiguration cm
INNER JOIN MixingStation.CMMixingProducitonLine cmPl on cmPl.Id_CMConfiguration = cm.Id
INNER JOIN MixingStation.ProductionLine pl on pl.Id = cmPl.Id_ProductionLine
INNER JOIN MixingStation.CMConfigurationUsers cmU on cmU.CMConfigurationId = cm.Id