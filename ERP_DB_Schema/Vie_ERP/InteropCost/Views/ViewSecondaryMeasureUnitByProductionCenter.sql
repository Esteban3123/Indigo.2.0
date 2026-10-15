

create view [InteropCost].[ViewSecondaryMeasureUnitByProductionCenter]
as (
select mu.Id, mu.Code, mu.Name, mu.Abbreviation, mu.UnitType, mu.AllowEditCostValue, mu.CostValue, mu.Status, ds.ProductionCenterId
from InteropCost.DistributionSecondary ds
inner join InteropCost.DistributionSecondaryBase dsb on ds.Id = dsb.DistributionSecondaryId
inner join InteropCost.DistributionSecondaryMeasurementUnit dsm on dsm.DistributionSecondaryBaseId = dsb.Id
inner join Inventory.InventoryMeasurementUnit mu on mu.Id = dsm.MeasurementUnitId
)