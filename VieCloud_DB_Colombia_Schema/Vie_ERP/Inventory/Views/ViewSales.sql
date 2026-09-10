

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las ventas de inventario (farmacia, insumos y productos comerciales) combinando las facturas de venta con su detalle de líneas, lotes o seriales, bodega de despacho, tercero comprador (cliente o aseguradora), producto vendido, grupo farmacológico, unidad de medida y usuario que registró la transacción. Integra información de facturas, inventario físico, clasificación ATC y datos del tercero para ofrecer una visión completa de cada venta por producto, permitiendo reportería de ventas, trazabilidad de lotes, control de vencimientos y análisis comercial o farmacéutico por bodega y cliente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewSales';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada de ventas de productos del inventario que integra cabecera y detalle de factura, lote/serial, bodega, tercero comprador, producto con clasificación ATC y grupo farmacológico, unidad de medida y usuario que creó la venta.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada venta debe estar asociada a una bodega existente (INNER JOIN Warehouse); Cada venta debe tener tercero registrado en Common.ThirdParty (INNER JOIN); Cada detalle debe referenciar un producto existente en InventoryProduct (INNER JOIN); Existe un registro de PhysicalInventory para el lote/serial del detalle (INNER JOIN); El producto debe tener ATC asociado a un PharmacologicalGroup (INNER JOIN PharmacologicalGroup); El usuario creador de la venta debe tener una persona asociada en Security.Person (INNER JOIN per)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ventas cuyo producto tenga clasificación ATC vinculada a un grupo farmacológico (por el INNER JOIN a PharmacologicalGroup mediante atc.PharmacologicalGroupId); Solo se exponen ventas cuyo usuario creador esté registrado en Security.User y tenga Persona asociada; Solo se exponen ventas con al menos un registro de PhysicalInventory referenciado desde el detalle de lote/serial (INNER JOIN PhysicalInventory); La fecha de vencimiento solo se reporta para productos que manejan lote', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Venta de productos de inventario; Factura de venta; Lote y serial; Fecha de vencimiento; Bodega/Almacén; Tercero (comprador); Producto / medicamento; Clasificación ATC; Grupo farmacológico; Unidad de medida; Inventario físico; Usuario creador de la venta', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por cada combinación de detalle de venta y registro en DocumentInvoiceProductSalesDetailBatchSerial; el Id se genera con ROW_NUMBER ordenado por el Id del detalle; [RETURN_RESULT] : Cuando psd.HandlesBatch = 1 se expone la fecha de vencimiento del lote (bs.ExpirationDate); cuando psd.HandlesBatch = 0 se devuelve NULL en vtoProduct', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si psd.HandlesBatch = 0 → vtoProduct = NULL (el producto no maneja lote, no aplica fecha de vencimiento) else Si HandlesBatch = 1, vtoProduct = bs.ExpirationDate del lote asociado', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail; Inventory.DocumentInvoiceProductSalesDetailBatchSerial; Inventory.Warehouse; Inventory.PhysicalInventory; Inventory.BatchSerial; Common.ThirdParty; Inventory.InventoryProduct; Inventory.ATC; Inventory.PharmacologicalGroup; Inventory.InventoryMeasurementUnit; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewSales';
GO
