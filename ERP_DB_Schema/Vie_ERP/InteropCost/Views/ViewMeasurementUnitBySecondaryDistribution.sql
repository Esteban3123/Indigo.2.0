

CREATE view [InteropCost].[ViewMeasurementUnitBySecondaryDistribution]
as (

select DistributionSecondaryId, pc.Id, pc.Code, pc.Name, pc.AllowEditCostValue, pc.CostValue
from InteropCost.DistributionSecondaryBase b
inner join InteropCost.DistributionSecondaryMeasurementUnit bd on b.Id = bd.DistributionSecondaryBaseId
inner join Inventory.InventoryMeasurementUnit pc on pc.Id = bd.MeasurementUnitId

)