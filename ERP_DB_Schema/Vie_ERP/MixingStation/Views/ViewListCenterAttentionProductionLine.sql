CREATE view [MixingStation].[ViewListCenterAttentionProductionLine] 
as select c.CODCENATE, c.CODCENATE as item2, c.NOMCENATE, c.DEPMUNCOD, i.MUNNOMBRE, a.IdProductionLine, p.Name, a.IdMixingStation
from dbo.ADCENATEN c, dbo.INMUNICIP i, MixingStation.CMCenterAttention a, MixingStation.ProductionLine p
where c.CODCENATE = a.IdCenterAttention
and p.Id = a.IdProductionLine
and c.DEPMUNCOD = i.DEPMUNCOD