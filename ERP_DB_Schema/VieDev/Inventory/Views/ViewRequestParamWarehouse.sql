


CREATE view [Inventory].[ViewRequestParamWarehouse]
as

SELECT 
  wa.Id, 
  wa.Id AS WarehouseId, 
  wa.Code AS WarehouseCode, 
  wa.Name AS WarehouseName, 
  rw.Id AS RequestParamWarehouseId, 
  rp.Id AS RequestParamId,
  rp.RequiredAuthorization,
  wa.status as StatusWarehouse,
  CAST(
    CASE WHEN rw.Id IS NULL THEN 0 ELSE 1 END AS bit
  ) AS IsSelected 
FROM 
  Inventory.Warehouse AS wa 
  LEFT OUTER JOIN Inventory.RequestParamWarehouse AS rw ON rw.WarehouseId = wa.Id 
  LEFT OUTER JOIN Inventory.RequestParam AS rp ON rw.RequestParamId = rp.Id