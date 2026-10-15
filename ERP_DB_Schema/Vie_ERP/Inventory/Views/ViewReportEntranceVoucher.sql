

CREATE VIEW [Inventory].[ViewReportEntranceVoucher]
AS
SELECT
ROW_NUMBER() OVER (ORDER BY ievd.Id) AS Id,
(DATEADD(DAY,iev.DayPeriod,iev.InvoiceDate)) AS 'invoiceVtoDate',
iev.InvoiceNumber,
iev.InvoiceDate,
thi.Nit,
thi.Name AS 'NameThirdParty',
(SELECT TOP 1 Addresss FROM Common.[Address] a WHERE a.IdPerson = p.id) AS 'Addresss',
city.Name,
iip.Id As 'IdProduct',
iip.Code AS 'CodeProduct',
bs.ExpirationDate,
bs.BatchCode,
w.Code AS 'codeWarehouse',
ievdb.Quantity, 
(iimu.Code + ' - ' + iimu.Name) AS 'measurementUnit',
(((ievdb.Quantity * ievd.UnitValue) * (100 - ievd.DiscountPercentage) / 100) * (100 + ievd.IvaPercentage) / 100) TotalValue,
iev.CreationUser AS 'UserCodeNameAux', 
atc.Concentration,
iev.TotalValue AS 'totalValuePurchase',
iip.Name AS 'NameProduct',
w.Name AS 'NameWarehouse'
FROM 
Inventory.EntranceVoucher AS iev
LEFT JOIN Inventory.EntranceVoucherDetail AS ievd ON ievd.EntranceVoucherId = iev.Id
LEFT JOIN Inventory.EntranceVoucherDetailBatchSerial AS ievdb ON ievdb.EntranceVoucherDetailId = ievd.Id
INNER JOIN Inventory.Warehouse AS w ON iev.WarehouseId = w.Id
INNER JOIN Inventory.InventoryProduct AS iip ON ievd.ProductId = iip.Id
LEFT JOIN Inventory.ATC AS atc ON iip.ATCId = atc.Id
LEFT JOIN Inventory.InventoryMeasurementUnit AS iimu ON iip.MeasurementUnitId = iimu.Id 
LEFT JOIN Inventory.BatchSerial AS bs ON ievdb.BatchSerialId = bs.Id
INNER JOIN Common.Supplier AS cs ON iev.SupplierId = cs.Id
INNER JOIN Common.City AS city ON cs.IdCity = city.Id
INNER JOIN Common.ThirdParty AS thi ON cs.IdThirdParty = thi.Id
INNER JOIN Common.Person p on p.Id = thi.PersonId