

CREATE VIEW [Billing].[ViewDocumentInvoiceProductSalesDevolutionDetails]
AS

select ROW_NUMBER() OVER (ORDER BY d.Id) RowId,
pro.Id ProductId, pro.Code ProductCode, pro.Name ProductName, pro.Code + ' - ' + pro.Name ProductDescription,
phy.BatchSerialId, bs.BatchCode, 
psdbs.Quantity, dd.Quantity QuantityDevolution,
d.Id DocumentInvoiceProductSalesDevolutionId, psdbs.Id DocumentInvoiceProductSalesDetailBatchSerialId,
psd.SalePrice, psd.IvaPercentage, psd.DiscountPercentage, psd.RTFPercentage,
dd.Id DocumentInvoiceProductSalesDevolutionDetailId,psd.ImportSource,psd.SourceCode
from Inventory.DocumentInvoiceProductSalesDevolution d
inner join Inventory.DocumentInvoiceProductSalesDevolutionDetail dd on dd.DocumentInvoiceProductSalesDevolutionId = d.Id
inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = dd.DocumentInvoiceProductSalesDetailBatchSerialId
inner join Inventory.DocumentInvoiceProductSalesDetail psd on psd.Id = psdbs.DocumentInvoiceProductSalesDetailId
inner join Inventory.PhysicalInventory phy on phy.Id = psdbs.PhysicalInventoryId
inner join Inventory.InventoryProduct pro on pro.Id = psd.ProductId
left join Inventory.BatchSerial bs on bs.Id = phy.BatchSerialId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle completo de las devoluciones de facturas de venta de productos de inventario (notas crédito), integrando cada documento de devolución con los productos devueltos, sus lotes o seriales, las cantidades originalmente despachadas y las cantidades devueltas, junto con el precio de venta, porcentajes de IVA, descuento y retención en la fuente de cada línea. Combina las tablas de devolución, detalle de devolución, detalle de lote/serial de la venta original, inventario físico, catálogo de productos y lotes para ofrecer una vista trazable producto a producto de qué artículos fueron retornados en cada nota crédito. Se usa para reportería de devoluciones de ventas, conciliación de inventario y auditoría de movimientos de salida y reingreso de medicamentos, insumos y dispositivos médicos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewDocumentInvoiceProductSalesDevolutionDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewDocumentInvoiceProductSalesDevolutionDetails';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle consolidado de productos devueltos en facturas de venta, combinando información de la devolución, el lote/serial despachado, el producto y los valores comerciales de la línea original.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesDevolutionDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el encabezado de devolución (DocumentInvoiceProductSalesDevolution) con al menos un detalle (DocumentInvoiceProductSalesDevolutionDetail).; Cada detalle de devolución debe estar vinculado a un registro existente de lote/serial de venta (DocumentInvoiceProductSalesDetailBatchSerial) y este a su línea de factura (DocumentInvoiceProductSalesDetail).; El lote/serial despachado debe tener inventario físico asociado (PhysicalInventory) y el producto debe existir en el catálogo maestro (InventoryProduct).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesDevolutionDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa un detalle de devolución único (DocumentInvoiceProductSalesDevolutionDetailId).; La cantidad original despachada (psdbs.Quantity) y la cantidad devuelta (dd.Quantity QuantityDevolution) se exponen lado a lado para permitir comparación.; Los valores comerciales (SalePrice, IvaPercentage, DiscountPercentage, RTFPercentage, ImportSource, SourceCode) provienen siempre de la línea original de la factura de venta, no de la devolución.; ProductDescription se construye concatenando Code + '' - '' + Name del producto.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesDevolutionDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de factura de venta; Lote y serial de producto; Inventario físico; Precio de venta; IVA; Descuento; Retención en la fuente (RTF); Trazabilidad de producto', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesDevolutionDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un row por cada detalle de devolución (Inventory.DocumentInvoiceProductSalesDevolutionDetail), numerado mediante ROW_NUMBER() ordenado por DocumentInvoiceProductSalesDevolution.Id.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesDevolutionDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LEFT JOIN sobre Inventory.BatchSerial vía PhysicalInventory.BatchSerialId → Si el inventario físico tiene lote/serial asociado, se expone su BatchCode; en caso contrario se devuelve NULL, permitiendo productos sin trazabilidad por lote.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesDevolutionDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.DocumentInvoiceProductSalesDevolution; Inventory.DocumentInvoiceProductSalesDevolutionDetail; Inventory.DocumentInvoiceProductSalesDetailBatchSerial; Inventory.DocumentInvoiceProductSalesDetail; Inventory.PhysicalInventory; Inventory.InventoryProduct; Inventory.BatchSerial', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesDevolutionDetails';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesDevolutionDetails';
GO
