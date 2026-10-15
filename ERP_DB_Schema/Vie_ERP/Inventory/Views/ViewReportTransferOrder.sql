





CREATE VIEW [Inventory].[ViewReportTransferOrder]
AS
SELECT
ROW_NUMBER() OVER (ORDER BY tord.Id) AS Id,
tor.Code,
tor.CreationDate,
u.UserCode AS 'codeAuxPharmacy',
per.Fullname,
thi.Nit,
iip.Id As 'IdProduct',
iip.Code AS 'CodeProduct',
iip.Name AS 'NameProduct',
bs.ExpirationDate,
tord.Quantity,
iip.ProductCost,
w.Code AS 'codeWarehouse',
bs.BatchCode,
atc.Concentration,
thi.Name AS 'thirdPartyName',
(iim.Code + ' - ' + iim.Name) AS 'measurementUnit',
fun.Code AS 'codeUnitFunctional',
fun.Name,
w.Name AS 'NameWarehouse',
u.UserCode AS 'UserCodeNameAux'
FROM Inventory.TransferOrder AS tor
LEFT JOIN Inventory.TransferOrderDetail AS tord ON Tord.TransferOrderId = tor.Id
LEFT JOIN Inventory.TransferOrderDetailBatchSerial AS tordb ON tordb.TransferOrderDetailId = tord.Id
LEFT JOIN payroll.FunctionalUnit AS fun ON tor.TargetFunctionalUnitId = fun.Id
LEFT JOIN Inventory.Warehouse AS w ON w.Id = tor.SourceWarehouseId
INNER JOIN Common.ThirdParty AS thi ON tor.ThirdPartyId = thi.Id
INNER JOIN Inventory.InventoryProduct AS iip ON tord.ProductId = iip.Id
LEFT JOIN Inventory.InventoryMeasurementUnit AS iim ON iip.MeasurementUnitId = iim.Id
LEFT JOIN Inventory.ATC AS atc ON iip.ATCId = atc.Id
INNER JOIN Inventory.PhysicalInventory AS phi ON tordb.PhysicalInventoryId = phi.Id
LEFT JOIN Inventory.BatchSerial AS bs ON phi.BatchSerialId = bs.Id
LEFT JOIN Security.[User] AS u ON tor.CreationUser= u.UserCode
INNER JOIN Security.Person AS per ON u.IdPerson = per.Id