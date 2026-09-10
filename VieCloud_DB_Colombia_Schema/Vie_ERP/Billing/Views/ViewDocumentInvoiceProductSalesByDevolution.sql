

CREATE VIEW [Billing].[ViewDocumentInvoiceProductSalesByDevolution]
AS

select ROW_NUMBER() OVER (ORDER BY ps.Id) RowId,
pro.Id ProductId, pro.Code ProductCode, pro.Name ProductName, pro.Code + ' - ' + pro.Name ProductDescription,
phy.BatchSerialId, bs.BatchCode, 
psdbs.OutstandingQuantity Quantity, psdbs.OutstandingQuantity QuantityDevolution,
ps.Id DocumentInvoiceProductSalesId, psdbs.Id DocumentInvoiceProductSalesDetailBatchSerialId,
psd.SalePrice, psd.IvaPercentage, psd.DiscountPercentage, psd.RTFPercentage,
ps.ContractExternalClientsId,
CONCAT(cec.Code,' - ',cec.ContractNumber) CECcode,
psd.ImportSource,
psd.SourceCode
from Inventory.DocumentInvoiceProductSales ps
inner join Inventory.DocumentInvoiceProductSalesDetail psd with(NOLOCK) on psd.DocumentInvoiceProductSalesId = ps.Id
inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs with(NOLOCK) on psdbs.DocumentInvoiceProductSalesDetailId = psd.Id
inner join Inventory.PhysicalInventory phy with(NOLOCK) on phy.Id = psdbs.PhysicalInventoryId
inner join Inventory.InventoryProduct pro with(NOLOCK) on pro.Id = psd.ProductId
left join Inventory.BatchSerial bs with(NOLOCK) on bs.Id = phy.BatchSerialId
left join MixingStation.ContractExternalClients cec  with(NOLOCK) on ps.ContractExternalClientsId = cec.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de devoluciones de productos vendidos en facturación de inventario (farmacia, insumos, dispositivos médicos). Consolida, por cada lote o serial despachado, la cantidad pendiente de devolución junto con el precio de venta, descuentos e impuestos aplicados en la factura original. Integra las facturas de venta, el detalle por línea de producto, el control de lotes/seriales, el inventario físico y el contrato del cliente externo, permitiendo identificar qué productos (con su código, nombre y lote) están disponibles para ser devueltos y a qué contrato o cliente corresponden. Se usa en el proceso de gestión de devoluciones comerciales y de farmacia para calcular valores a revertir y mantener la trazabilidad del inventario devuelto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewDocumentInvoiceProductSalesByDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewDocumentInvoiceProductSalesByDevolution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las líneas de factura de venta de inventario con saldo pendiente por lote/serial, listando cantidades susceptibles de devolución junto con producto, lote, precios, impuestos y contrato externo asociado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesByDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada línea de factura (DocumentInvoiceProductSalesDetail) debe tener al menos un registro en DocumentInvoiceProductSalesDetailBatchSerial con su PhysicalInventoryId.; El PhysicalInventoryId del detalle batch/serial debe existir en Inventory.PhysicalInventory.; El producto referenciado en el detalle debe existir en Inventory.InventoryProduct.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesByDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cantidad reportada como devolución coincide siempre con la cantidad pendiente (OutstandingQuantity) del detalle por lote/serial.; ProductDescription siempre se construye como ''Code - Name'' del producto.; Solo se incluyen líneas con detalle de lote/serial existente (INNER JOIN sobre DocumentInvoiceProductSalesDetailBatchSerial y PhysicalInventory).; RowId se asigna secuencialmente ordenado por el Id de la factura de venta (ps.Id).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesByDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura de venta de inventario; Devolución de productos; Lote y serial; Inventario físico; Producto de inventario; Contrato con cliente externo; IVA; RTF (retención en la fuente); Descuento; Precio de venta', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesByDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewDocumentInvoiceProductSalesByDevolution: Devuelve una fila por cada combinación factura-detalle-lote/serial, usando OutstandingQuantity como cantidad disponible para devolución (Quantity y QuantityDevolution se alimentan ambos de psdbs.OutstandingQuantity).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesByDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si BatchSerial (bs) y ContractExternalClients (cec) se enlazan con LEFT JOIN → Las facturas sin lote/serial o sin contrato externo siguen apareciendo en el resultado, con BatchCode o CECcode en NULL. else Cuando existe lote, se muestra su BatchCode; cuando existe contrato, se concatena ''Code - ContractNumber'' como CECcode.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesByDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail; Inventory.DocumentInvoiceProductSalesDetailBatchSerial; Inventory.PhysicalInventory; Inventory.InventoryProduct; Inventory.BatchSerial; MixingStation.ContractExternalClients', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesByDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewDocumentInvoiceProductSalesByDevolution';
GO
