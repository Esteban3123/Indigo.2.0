
CREATE view [Cost].[ViewMeasurementUnitByCostIntermediateDistribution]
as (
	select IntermediateDistributionId, pc.Id, pc.Code, pc.Name, pc.AllowEditCostValue, pc.CostValue
	from Cost.CostIntermediateDistributionBase b
	inner join Cost.CostIntermediateDistributionMeasurementUnit bd on b.Id = bd.IntermediateDistributionBaseId
	inner join Inventory.InventoryMeasurementUnit pc on pc.Id = bd.MeasurementUnitId
)