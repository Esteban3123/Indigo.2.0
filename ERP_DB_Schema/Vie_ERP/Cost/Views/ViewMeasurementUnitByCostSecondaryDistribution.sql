


CREATE view [Cost].[ViewMeasurementUnitByCostSecondaryDistribution]
as (

select DistributionSecondaryId, pc.Id, pc.Code, pc.Name, pc.AllowEditCostValue, pc.CostValue
from Cost.CostDistributionSecondaryBase b
inner join Cost.CostDistributionSecondaryMeasurementUnit bd on b.Id = bd.DistributionSecondaryBaseId
inner join Inventory.InventoryMeasurementUnit pc on pc.Id = bd.MeasurementUnitId

)