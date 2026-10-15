


CREATE VIEW [Inventory].[ViewReportKardexForPharmacy]
AS
SELECT
ik.Id,
ik.EntityCode,
iip.Id As 'IdProduct',
iip.Code AS 'CodeProduct',
ibs.ExpirationDate,
ik.DocumentDate,
ik.AverageCost,
case when ik.MovementType = 1 then '1' end AS 'income',
case when ik.MovementType = 2 then '2' end AS 'expenses',
ik.PreviousAmountWarehouse AS 'openingbalance',
case when ik.MovementType = 1 then (ik.PreviousAmountWarehouse + ik.Quantity) when ik.MovementType = 2 then (ik.PreviousAmountWarehouse - ik.Quantity) end AS 'finalBalance',
w.Name AS 'nameWarehouse',
thi.Name AS 'nameThirdParty',
iip.Name AS 'NameProduct',
atc.Concentration,
iim.Name,
ik.CreationUser AS 'UserCodeNameAux',
w.Code AS 'codeWarehouse'
FROM
Inventory.Kardex AS ik
LEFT JOIN Common.ThirdParty AS thi ON ik.ThirdPartyId = thi.Id
INNER JOIN Inventory.InventoryProduct AS iip ON ik.ProductId = iip.Id
LEFT JOIN Inventory.InventoryMeasurementUnit AS iim ON iip.MeasurementUnitId = iim.Id
LEFT JOIN Inventory.ATC AS atc ON iip.ATCId = atc.Id
LEFT JOIN Inventory.BatchSerial AS ibs ON ik.BatchSerialId = ibs.Id
INNER JOIN Inventory.Warehouse AS w ON ik.WarehouseId = w.Id