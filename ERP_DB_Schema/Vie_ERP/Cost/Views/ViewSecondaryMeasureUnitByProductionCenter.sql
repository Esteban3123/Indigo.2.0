CREATE view [Cost].[ViewSecondaryMeasureUnitByProductionCenter]
as (
select mu.Id as Id, mu.Code, mu.Name, mu.Abbreviation, mu.UnitType, mu.AllowEditCostValue, mu.CostValue, mu.Status
from Cost.CostDistributionSecondary ds
inner join Cost.CostDistributionSecondaryBase dsb on ds.Id = dsb.DistributionSecondaryId
inner join Cost.CostDistributionSecondaryMeasurementUnit dsm on dsm.DistributionSecondaryBaseId = dsb.Id
inner join Inventory.InventoryMeasurementUnit mu on mu.Id = dsm.MeasurementUnitId
Group by mu.Id, mu.Code, mu.[Name], mu.Abbreviation, mu.UnitType, mu.AllowEditCostValue, mu.CostValue, mu.Status
)