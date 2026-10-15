CREATE VIEW [Inventory].[ViewReportKardex]
AS
WITH K AS (
   SELECT
       CAST(CONCAT('Kardex', k.Id) AS nvarchar(50))            AS Id,
       k.DocumentDate, k.ProductId, k.WarehouseId, k.BatchSerialId,
       k.MovementType, k.Quantity, k.PreviousAmountProduct, k.PreviousAmountWarehouse, k.PreviousAmountBatch,
       k.Value, k.PreviousCost, k.PreviousAverageCost, k.AverageCost,
       k.EntityId, k.EntityCode, k.EntityName,
       k.ImportedEntityId, k.ImportedEntityCode, k.ImportedEntityName,
       k.CreationUser, k.CreationDate, k.AffectInventory
   FROM Inventory.Kardex AS k
   UNION ALL
   SELECT
       CAST(CONCAT('KardexControl', k.Id) AS nvarchar(50))     AS Id,
       k.DocumentDate, k.ProductId, k.WarehouseId, k.BatchSerialId,
       k.MovementType, k.Quantity, k.PreviousAmountProduct, k.PreviousAmountWarehouse, k.PreviousAmountBatch,
       0 AS Value, 0 AS PreviousCost, 0 AS PreviousAverageCost, 0 AS AverageCost,
       k.EntityId, k.EntityCode, k.EntityName,
       CAST(0 AS int) AS ImportedEntityId, CAST('' AS nvarchar(50)) AS ImportedEntityCode, CAST('' AS nvarchar(200)) AS ImportedEntityName,
       k.CreationUser, k.CreationDate, 1 AS AffectInventory
   FROM Inventory.KardexControl AS k
),
REOA AS (
   SELECT
       re.Id              AS RemissionEntranceId,
       re.Code            AS RemissionEntranceCode,
       s.Code             AS SupplierCode,
       s.Name             AS SupplierName,
       red.ProductId,
       MAX(red.SourceCode) AS SourceCode
   FROM Inventory.RemissionEntrance AS re
   LEFT JOIN Inventory.RemissionEntranceDetail AS red  ON red.RemissionEntranceId = re.Id
   LEFT JOIN Common.Supplier AS s ON s.Id = re.SupplierId
   GROUP BY re.Id, re.Code, s.Code, s.Name, red.ProductId
)
SELECT
   k.Id AS KardexId,
   k.WarehouseId,
   w.Code AS WarehouseCode, w.Name AS WarehouseName,
   w.Code + ' - ' + w.Name AS WarehouseDescription,
   k.ProductId,
   p.Code AS ProductCode, p.Name AS ProductName,
   p.Code + ' - ' + p.Name AS ProductDescription,
   p.ProductControl, p.CodeCUM, p.HealthRegistration,
   k.BatchSerialId,
   bs.BatchCode AS BatchCode, bs.ExpirationDate,
   k.DocumentDate, k.CreationDate,
   k.MovementType, k.Quantity,
   k.Value, k.PreviousCost, k.AverageCost, k.PreviousAverageCost,
   k.PreviousAmountProduct, k.PreviousAmountWarehouse, k.PreviousAmountBatch,
   k.EntityId, k.EntityCode,
   CASE
       WHEN k.EntityName = 'RemissionEntrance'
            THEN CASE WHEN reo.SourceCode IS NULL THEN 'Traslado Interno' ELSE 'RemissionEntrance' END
       ELSE k.EntityName
   END AS EntityName,
   k.ImportedEntityId, k.ImportedEntityCode, k.ImportedEntityName,
   k.AffectInventory,
   CASE k.EntityName
       WHEN 'PharmaceuticalDispensing'
           THEN CONCAT(pa.IPCODPACI, ' - ', pa.IPNOMCOMP)
       WHEN 'PharmaceuticalDispensingDevolution'
           THEN CONCAT(pddpa.IPCODPACI, ' - ', pddpa.IPNOMCOMP)
       WHEN 'TransferOrder'
           THEN COALESCE(CONCAT(tpto.Nit, ' - ', tpto.Name),
                         CONCAT(sp.Identification, ' - ', sp.FullName))
       WHEN 'EntranceVoucher'
           THEN CONCAT(spev.Code, ' - ', spev.Name)
       WHEN 'RemissionEntrance'
           THEN CONCAT(reo.SupplierCode, ' - ', reo.SupplierName)
       WHEN 'BasicBilling'
           THEN CONCAT(bb.Nit, ' - ', bb.Name)
       WHEN 'BasicBillingDevolution'
           THEN CONCAT(bb.Nit, ' - ', bb.Name)
       ELSE ''
   END AS NamePatient,
   IIF(k.MovementType = 1, k.Quantity, 0) AS QuantityEntrance,
   IIF(k.MovementType = 2, k.Quantity, 0) AS QuantityExit,
   (k.PreviousAverageCost * k.PreviousAmountProduct) AS PreviousCostTotalCalculated,
   (k.AverageCost * IIF(k.AffectInventory = 1,
                        IIF(k.MovementType = 1, k.PreviousAmountProduct + k.Quantity,
                                             k.PreviousAmountProduct - k.Quantity),
                        k.PreviousAmountProduct)) AS CostTotalCalculated,
   IIF(k.AffectInventory = 1,
       IIF(k.MovementType = 1, k.PreviousAmountProduct + k.Quantity,
                            k.PreviousAmountProduct - k.Quantity),
       k.PreviousAmountProduct) AS PreviousAmountProductCalculated,
   IIF(k.AffectInventory = 1,
       IIF(k.MovementType = 1, k.PreviousAmountWarehouse + k.Quantity,
                            k.PreviousAmountWarehouse - k.Quantity),
       k.PreviousAmountWarehouse) AS PreviousAmountWarehouseCalculated,
   IIF(k.AffectInventory = 1,
       IIF(k.MovementType = 1, k.PreviousAmountBatch + k.Quantity,
                            k.PreviousAmountBatch - k.Quantity),
       k.PreviousAmountBatch) AS PreviousAmountBatchCalculated,
   CASE
       WHEN k.EntityName = 'ConsignmentInventoryRemission'     THEN 'Remision de Inventario en Consignación'
       WHEN k.EntityName = 'RemissionEntrance'                 THEN 'Remision de Entrada'
       WHEN k.EntityName = 'RemissionOutput'                   THEN 'Remision de Salida'
       WHEN k.EntityName = 'RemissionDevolution'               THEN 'Devolucion de Remision'
       WHEN k.EntityName = 'EntranceVoucher'                   THEN 'Comprobante de Entrada'
       WHEN k.EntityName = 'PharmaceuticalDispensing'          THEN CONCAT('Dispensación Farmacéutica',' - ','( ', IIF(pd.EntityName <> 'SavePharmaceuticalDispensing','Dashboard','Manual'),' ) ')
       WHEN k.EntityName = 'EntranceVoucherDevolution'         THEN 'Devolución de Comprobante de Entrada'
       WHEN k.EntityName = 'InventoryAdjustment'               THEN 'Ajuste de Inventario'
       WHEN k.EntityName = 'LoanMerchandise'                   THEN 'Préstamo de Mercancía'
       WHEN k.EntityName = 'LoanMerchandiseDevolution'         THEN 'Devolución de Préstamo de Mercancía'
       WHEN k.EntityName = 'PharmaceuticalDispensingDevolution'THEN CONCAT('Devolución de Dispensación Farmacéutica',' - ','( ', IIF(pdd.EntityName IS NULL,'Manual','Dashboard'),' ) ')
       WHEN k.EntityName = 'TransferOrder'                     THEN 'Orden de Traslado'
       WHEN k.EntityName = 'TransferOrderDevolution'           THEN 'Devolución de Orden de Traslado'
       WHEN k.EntityName = 'DocumentInvoiceProductSales'       THEN 'Documento Factura de venta de Producto'
       WHEN k.EntityName = 'InventoryControl'                  THEN 'Control De Inventarios'
       WHEN k.EntityName = 'BasicBilling'                      THEN 'Facturación Basica'
       WHEN k.EntityName = 'BasicBillingDevolution'            THEN 'Anulación Factura Basica'
   END + ' - ' + k.EntityCode AS DocumentDescription
FROM K
JOIN Inventory.Warehouse								AS w    ON w.Id = k.WarehouseId
JOIN Inventory.InventoryProduct							AS p    ON p.Id = k.ProductId
LEFT JOIN Inventory.BatchSerial							AS bs   ON bs.Id = k.BatchSerialId
LEFT JOIN Inventory.PharmaceuticalDispensing            AS pd    ON pd.Id  = k.EntityId AND k.EntityName = 'PharmaceuticalDispensing'
LEFT JOIN dbo.ADINGRESO                                 AS i     ON i.NUMINGRES = pd.AdmissionNumber
LEFT JOIN dbo.INPACIENT                                 AS pa    ON pa.IPCODPACI = i.IPCODPACI
LEFT JOIN Inventory.PharmaceuticalDispensingDevolution  AS pdd   ON pdd.Id = k.EntityId AND k.EntityName = 'PharmaceuticalDispensingDevolution'
LEFT JOIN dbo.ADINGRESO                                 AS pddi  ON pddi.NUMINGRES = pdd.AdmissionNumber
LEFT JOIN dbo.INPACIENT                                 AS pddpa ON pddpa.IPCODPACI = pddi.IPCODPACI
LEFT JOIN Inventory.TransferOrder                       AS tor   ON tor.Id = k.EntityId  AND k.EntityName = 'TransferOrder'
LEFT JOIN [Security].[User]                             AS us    ON us.UserCode = tor.ConfirmationUser
LEFT JOIN [Security].Person                             AS sp    ON sp.Id = us.IdPerson
LEFT JOIN Common.ThirdParty                             AS tpto  ON tpto.Id = tor.ThirdPartyId
LEFT JOIN Inventory.EntranceVoucher                     AS ev    ON ev.Id = k.EntityId   AND k.EntityName = 'EntranceVoucher'
LEFT JOIN Common.Supplier                               AS spev  ON spev.Id = ev.SupplierId
LEFT JOIN REOA                                          AS reo   ON reo.RemissionEntranceId = k.EntityId AND reo.ProductId = k.ProductId AND k.EntityName = 'RemissionEntrance'
LEFT JOIN Billing.BasicBilling                          AS bba   ON bba.Id = k.EntityId AND k.EntityName IN ('BasicBilling','BasicBillingDevolution')
LEFT JOIN Common.Customer                               AS bb    ON bb.Id = bba.CustomerId;
GO