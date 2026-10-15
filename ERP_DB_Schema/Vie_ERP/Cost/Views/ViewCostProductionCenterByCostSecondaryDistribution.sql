




CREATE view [Cost].[ViewCostProductionCenterByCostSecondaryDistribution]
as (

select DistributionSecondaryId, pc.Id, pc.Code, pc.Name
from Cost.CostDistributionSecondaryBase b
inner join Cost.CostDistributionSecondaryBaseDetail bd on b.Id = bd.DistributionSecondaryBaseId
inner join Cost.CostProductionCenter pc on pc.Id = bd.ProductionCenterId

)