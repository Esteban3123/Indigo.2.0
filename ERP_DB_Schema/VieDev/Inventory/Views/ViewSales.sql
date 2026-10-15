



CREATE VIEW [Inventory].[ViewSales]
AS
SELECT
ROW_NUMBER() OVER (ORDER BY psd.Id) AS Id,
ps.Code AS 'CodeProductSales',
thi.Nit,
ps.CreationDate,
iip.Id As 'IdProduct',
iip.Code AS 'CodeProduct', 
psd.Quantity, 
psd.TotalValue,
psd.SalePrice,
case when psd.HandlesBatch = 0 then NULL when psd.HandlesBatch = 1 then bs.ExpirationDate end AS 'vtoProduct',
per.Fullname,
thi.Name AS 'NameThirdParty',
pg.Name,
iim.Code,
iw.Code AS 'IdAlmacen',
iw.Name AS 'Almacen',
iip.Name AS 'NameProduct',
u.UserCode AS 'UserCodeNameAux'
FROM Inventory.DocumentInvoiceProductSales AS ps
LEFT JOIN Inventory.DocumentInvoiceProductSalesDetail AS psd ON psd.DocumentInvoiceProductSalesId = ps.Id
LEFT JOIN Inventory.DocumentInvoiceProductSalesDetailBatchSerial AS psdb ON PSDB.DocumentInvoiceProductSalesDetailId = psd.Id
INNER JOIN Inventory.Warehouse AS iw ON ps.WarehouseId = iw.Id
INNER JOIN Inventory.PhysicalInventory AS phi ON psdb.PhysicalInventoryId = phi.Id
LEFT JOIN Inventory.BatchSerial AS bs ON phi.BatchSerialId = bs.Id
INNER JOIN Common.ThirdParty AS thi ON ps.ThirdPartyId = thi.Id
INNER JOIN Inventory.InventoryProduct AS iip ON psd.ProductId = iip.Id
LEFT JOIN Inventory.ATC AS atc ON iip.ATCId = atc.Id
INNER JOIN Inventory.PharmacologicalGroup AS pg ON atc.PharmacologicalGroupId = pg.Id
LEFT JOIN Inventory.InventoryMeasurementUnit AS iim ON iip.MeasurementUnitId = iim.Id
LEFT JOIN Security.[User] AS u ON ps.CreationUser= u.UserCode
INNER JOIN Security.Person AS per ON u.IdPerson = per.Id