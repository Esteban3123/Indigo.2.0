CREATE FUNCTION [Inventory].[InvoiceSISMEDProductSales] (@ProductID as int, @Value as decimal(18,2), @month as int)
RETURNS varchar(100)
AS
BEGIN
	declare @invoiceNumber varchar(100)

select top 1 @invoiceNumber = i.InvoiceNumber 
from Billing.Invoice as i WITH (NOLOCK) 
inner join Inventory.DocumentInvoiceProductSales dips with(nolock) on dips.InvoiceId = i.Id
inner join Inventory.DocumentInvoiceProductSalesDetail dipsd with(nolock) on dipsd.DocumentInvoiceProductSalesId = dips.Id
where i.Status = 1 and dipsd.ProductId = @ProductID and dipsd.SubTotalValue = @value and MONTH(i.InvoiceDate) = @month
 
RETURN @invoiceNumber

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que busca y retorna el número de factura activa (no anulada) asociada a la venta de un producto específico del inventario, filtrando por el identificador del producto, el valor subtotal de la línea de venta y el mes de la factura. Cruza las facturas de cobro (Billing.Invoice), los documentos de venta de inventario (DocumentInvoiceProductSales) y el detalle de productos vendidos (DocumentInvoiceProductSalesDetail) para localizar el comprobante exacto. Se utiliza principalmente en procesos de conciliación o auditoría de ventas de farmacia e insumos con el sistema SISMED, donde se necesita identificar qué número de factura respalda la comercialización de un medicamento o insumo en un mes determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'InvoiceSISMEDProductSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'InvoiceSISMEDProductSales';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el número de factura activa asociada a una venta de producto del inventario, identificada por producto, subtotal y mes de emisión, para reportes SISMED.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMEDProductSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una factura en Billing.Invoice con Status = 1 (activa/vigente); La factura debe estar enlazada a un documento de venta de productos en Inventory.DocumentInvoiceProductSales; Debe existir un detalle en DocumentInvoiceProductSalesDetail cuyo ProductId y SubTotalValue coincidan exactamente con los parámetros; El mes de InvoiceDate debe coincidir con el mes recibido', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMEDProductSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera facturas con Status = 1 (excluye anuladas u otros estados); Filtra por mes calendario sin considerar año, lo que puede generar coincidencias entre años distintos; Devuelve únicamente un resultado (TOP 1) aunque existan múltiples coincidencias; Lecturas con NOLOCK: tolera lecturas sucias en aras de rendimiento', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMEDProductSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura de venta; Producto de inventario; Venta de productos (farmacia/insumos); SISMED (Sistema de Información de Precios de Medicamentos); Subtotal de línea de factura', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMEDProductSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.Invoice: Cuando existe coincidencia de Status=1, ProductId, SubTotalValue y MONTH(InvoiceDate)=@month, retorna el InvoiceNumber del primer registro encontrado; si no hay coincidencia, retorna NULL', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMEDProductSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMEDProductSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMEDProductSales';
GO
