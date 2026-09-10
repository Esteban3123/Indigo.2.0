CREATE VIEW [Inventory].[InvoicesForSISMED]
AS
SELECT 
	YEAR(i.InvoiceDate) Year
	,MONTH(i.InvoiceDate) Month
	,ISNULL(ip.CodeCUM, '') CodeCUM
	,ISNULL(sod.SubTotalSalesPrice, dipsd.SalePrice) Price
	,MIN(i.InvoiceNumber) InvoiceNumber
FROM Billing.Invoice i
LEFT JOIN Billing.InvoiceDetail AS id WITH (NOLOCK) ON i.Id = id.InvoiceId
LEFT JOIN Billing.ServiceOrderDetail AS sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id AND sod.GrandTotalSalesPrice > 0
LEFT JOIN Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK) ON i.Id = dips.InvoiceId
LEFT JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
LEFT JOIN Inventory.InventoryProduct ip ON ISNULL(sod.ProductId, dipsd.ProductId) = ip.Id
WHERE ip.ATCId IS NOT NULL --AND ip.CodeCUM IS NOT NULL
GROUP BY YEAR(i.InvoiceDate), MONTH(i.InvoiceDate), ip.CodeCUM, ISNULL(sod.SubTotalSalesPrice, dipsd.SalePrice)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las facturas de venta de medicamentos e insumos con clasificación ATC (código anatómico-terapéutico-química) para el reporte SISMED (Sistema de Información de Precios de Medicamentos). Integra las facturas de cobro (Billing.Invoice) con sus detalles de servicios (ServiceOrderDetail) y las ventas directas de inventario (DocumentInvoiceProductSalesDetail), obteniendo para cada producto el código CUM (Código Único de Medicamento), el precio de venta y el número mínimo de factura, agrupados por año y mes. Solo incluye productos que tengan clasificación ATC asignada, garantizando que el reporte contenga únicamente medicamentos regulados. Se usa para generar los informes periódicos de precios de medicamentos que las instituciones deben reportar al Ministerio de Salud a través del SISMED.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'InvoicesForSISMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'InvoicesForSISMED';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida ventas mensuales de medicamentos con código CUM y clasificación ATC, agregando facturas de servicios y de productos de inventario para reporte regulatorio SISMED.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'InvoicesForSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los productos deben tener ATCId asignado en Inventory.InventoryProduct para ser incluidos; Las facturas deben estar registradas en Billing.Invoice con fecha válida', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'InvoicesForSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan productos con clasificación ATC asignada; Únicamente se consideran ventas de servicios cuyo GrandTotalSalesPrice sea mayor a cero; El CodeCUM nulo se sustituye por cadena vacía en la salida; El precio se obtiene preferentemente del detalle de orden de servicio y, en su defecto, del detalle de factura de inventario; Cuando hay múltiples facturas para la misma combinación año/mes/CUM/precio se reporta solo el InvoiceNumber mínimo', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'InvoicesForSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; CUM (Código Único de Medicamento); ATC (clasificación anatómica terapéutica química); SISMED (Sistema de Información de Precios de Medicamentos); Orden de servicio; Venta de productos de inventario; Precio de venta', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'InvoicesForSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultado_consulta: Devuelve año, mes, CodeCUM, precio y número mínimo de factura agrupados por periodo, CUM y precio, solo para productos con ATCId IS NOT NULL', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'InvoicesForSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ip.ATCId IS NOT NULL → Incluye el registro en el resultado else Excluye productos sin clasificación ATC; si sod.SubTotalSalesPrice disponible (vía ServiceOrderDetail con GrandTotalSalesPrice > 0) → Usa el precio de la orden de servicio else Usa dipsd.SalePrice del detalle de venta de inventario; si sod.GrandTotalSalesPrice > 0 → Considera el detalle de orden de servicio en el JOIN else Descarta líneas de orden de servicio sin valor positivo', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'InvoicesForSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'InvoicesForSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'InvoicesForSISMED';
GO
