



CREATE view [MixingStation].[ViewListProductionLine] 
as 

select CONCAT(1, '-', cm.Id, '-', ca.Id, '-', pludt.Id) Id,
cm.Id CMConfigurationId, ca.CodeCenterAttention CareCenterCode, 
pl.Id ProductionLineId, pl.Code ProductionLineCode, pl.Name ProductionLineName, pl.Code + ' - ' + pl.Name ProductionLineCodeName,
pludt.Id_UnitDoseType UnitDoseTypeId, 1 SourceType
from MixingStation.CMConfiguration cm
inner join MixingStation.CMCenterAttention ca on ca.IdMixingStation = cm.Id
inner join MixingStation.ProductionLine pl on pl.Id = ca.IdProductionLine
inner join MixingStation.ProductionLineUnitDoseType pludt on pludt.Id_ProductionLine = pl.Id

union all

select CONCAT(2, '-', cm.Id, '-', ca.Id, '-', pludt.Id) Id,
cm.Id CMConfigurationId, ecc.Code CareCenterCode, 
pl.Id ProductionLineId, pl.Code ProductionLineCode, pl.Name ProductionLineName, pl.Code + ' - ' + pl.Name ProductionLineCodeName,
pludt.Id_UnitDoseType UnitDoseTypeId, 2 SourceType
from MixingStation.CMConfiguration cm
inner join MixingStation.CMExternalCareCenter ca on ca.CMConfigurationId = cm.Id
inner join MixingStation.ExternalCareCenter ecc on ecc.Id = ca.ExternalCareCenterId
inner join MixingStation.ProductionLine pl on pl.Id = ca.ProductionLineId
inner join MixingStation.ProductionLineUnitDoseType pludt on pludt.Id_ProductionLine = pl.Id