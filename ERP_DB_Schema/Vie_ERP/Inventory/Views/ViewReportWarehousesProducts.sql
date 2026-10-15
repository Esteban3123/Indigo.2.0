CREATE VIEW [Inventory].[ViewReportWarehousesProducts]
AS
SELECT ROW_NUMBER() OVER(ORDER BY w.Code ASC) as Row, w.Name, w.Code AS 'WarehouseIdCode'FROM Inventory.[PhysicalInventory] AS pin
inner join Inventory.InventoryProduct iip ON iip.Id =pin.ProductId
INNER JOIN Inventory.Warehouse AS w ON w.Id = pin.WarehouseId
GROUP BY w.Code, w.Name