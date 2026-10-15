CREATE VIEW [Inventory].[ViewReportPhysicalInventoryByColumns]
AS
SELECT 
ROW_NUMBER() OVER (ORDER BY pin.ProductId) AS Id,
iip.Code + ' - ' + iip.Name AS 'Product',
bs.BatchCode,
pf.Name AS 'PharmaceuticalFormIdName',
w.Name, 
w.Code AS 'WarehouseIdCode',
SUM(pin.Quantity) AS 'Quantity',
iip.Code AS 'ProductIdCode',
iip.Name AS 'ProductIdName',
mu.Name AS 'MeasurementUnitIdName',
pin.ProductId
FROM Inventory.[PhysicalInventory] AS pin
inner join Inventory.InventoryProduct iip ON iip.Id =pin.ProductId
INNER JOIN Inventory.Warehouse AS w ON w.Id = pin.WarehouseId
LEFT JOIN Inventory.BatchSerial AS bs ON bs.Id = pin.BatchSerialId
LEFT JOIN Inventory.InventoryMeasurementUnit AS mu ON mu.Id = iip.MeasurementUnitId
LEFT JOIN Inventory.ATC AS atc ON atc.Id = iip.ATCId
LEFT JOIN Inventory.AdministrationRoute AS ar ON ar.Id = atc.AdministrationRouteId
LEFT JOIN Inventory.PharmaceuticalForm AS pf ON pf.Id = ar.PharmaceuticalFormId
GROUP BY w.Code, iip.Code, bs.BatchCode, iip.Name, mu.Name, pf.Name, pin.ProductId, w.Name