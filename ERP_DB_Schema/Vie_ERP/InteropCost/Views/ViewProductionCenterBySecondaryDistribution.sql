


CREATE view [InteropCost].[ViewProductionCenterBySecondaryDistribution]
as (

select DistributionSecondaryId, pc.Id, pc.Code, pc.Name
from InteropCost.DistributionSecondaryBase b
inner join InteropCost.DistributionSecondaryBaseDetail bd on b.Id = bd.DistributionSecondaryBaseId
inner join InteropCost.ProductionCenter pc on pc.Id = bd.ProductionCenterId

)