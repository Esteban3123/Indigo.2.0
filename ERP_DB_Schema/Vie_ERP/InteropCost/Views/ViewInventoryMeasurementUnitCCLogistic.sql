create view [InteropCost].[ViewInventoryMeasurementUnitCCLogistic]
	as
select distinct(mu.Id), mu.Code, mu.Name from [InteropCost].[LogisticsProductionCenterRecordDetail] as lpc inner join
[Inventory].[InventoryMeasurementUnit] as mu on lpc.InventoryMeasurementUnitId = mu.Id