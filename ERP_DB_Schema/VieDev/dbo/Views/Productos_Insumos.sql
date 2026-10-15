

CREATE VIEW [dbo].[Productos_Insumos]
AS
SELECT        t.Name AS Tipo, ISU.Code AS CodigoInsumo, ISU.SupplieName AS NombreInsumo, P.Code AS CodigoProducto, P.CodeAlternative AS CodigoAlternativo, P.CodeAlternativeTwo AS CodigoAlternativo2, P.Name, 
                         P.Description AS NombreProducto, PG.Code AS CodigoGrupo, PG.Name AS NombreGrupo, PSG.Code AS CodigoSubGrupo, PSG.Name AS NombreSubGrupo, PU.Code AS CodigoUnidadEmpaque, 
                         PU.Name AS NombreUnidadEmpaque, M.Code AS CodigoProveedor, M.Name AS Proveedor, IVA.Code AS CodigoIva, IVA.Name AS NombreIva, 
                         CASE HandlesHealthRegistration WHEN 1 THEN 'Si' ELSE 'No' END AS ManejaRegistro, P.HealthRegistration AS Registro, P.ExpirationDate AS FechaVencimiento, BG.Code AS CodigoGrupoFac, BG.Name AS NombreGrupoFac, 
                         CASE ProductControl WHEN 1 THEN 'Si' ELSE 'No' END AS InsumoControl, CASE P.POSProduct WHEN 1 THEN 'Si' ELSE 'no' END AS ProductoPos, P.ProductCost AS Costo, P.FinalProductCost AS UltimoCosto, 
                         P.SellingPrice AS PrecioVenta, P.ControlCostPercentage AS PorcentajeControl, R.Code AS CodigoRiesgo, R.Name AS NombreRiesgo, P.SerialNumber, 
                         CASE P.Consumption WHEN 1 THEN 'Si' ELSE 'No' END AS ProductoComoConsumo,ISNULL(STUFF((SELECT DISTINCT ', ' + CONCAT(bs.BatchCode, ' - ', bs.ExpirationDate) FROM Inventory.BatchSerial bs WHERE bs.productId = p.Id AND CAST(ISNULL(bs.ExpirationDate, GETDATE()) AS DATE) >= CAST(GETDATE() AS DATE) FOR XML PATH ('')), 1,2, ''), '') LoteFechaVencimiento

FROM            Inventory.InventoryProduct AS P LEFT OUTER JOIN
                         Inventory.InventorySupplie AS ISU ON ISU.Id = P.SupplieId LEFT OUTER JOIN
                         Inventory.ProductGroup AS PG ON P.ProductGroupId = PG.Id LEFT OUTER JOIN
                         Inventory.ProductSubGroup AS PSG ON P.ProductSubGroupId = PSG.Id LEFT OUTER JOIN
                         Inventory.PackagingUnit AS PU ON P.PackagingUnitId = PU.Id LEFT OUTER JOIN
                         Inventory.Manufacturer AS M ON P.ManufacturerId = M.Id LEFT OUTER JOIN
                         GeneralLedger.GeneralLedgerIVA AS IVA ON P.IVAId = IVA.Id LEFT OUTER JOIN
                         Billing.BillingGroup AS BG ON P.BillingGroupId = BG.Id LEFT OUTER JOIN
                         Inventory.InventoryRiskLevel AS R ON P.InventoryRiskLevelId = R.Id LEFT OUTER JOIN
                         Inventory.ProductType AS t ON P.ProductTypeId = t.Id 
					
WHERE        (P.ProductTypeId NOT IN (1)) AND (P.Status = 1)