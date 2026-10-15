create view [Cost].[ViewCostInventoryMeasurementUnitCCLogistic]
	as
select
 distinct(mu.Id), mu.Code, mu.Name 
 from
    Cost.CostLogisticsProductionCenterRecordDetail as lpc inner join
    [Inventory].[InventoryMeasurementUnit] as mu on lpc.InventoryMeasurementUnitId = mu.Id