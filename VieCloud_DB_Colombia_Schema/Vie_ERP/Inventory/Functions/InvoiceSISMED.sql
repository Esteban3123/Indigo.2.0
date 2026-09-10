CREATE FUNCTION [Inventory].[InvoiceSISMED] 
(
	@Type INT,
	@Year INT, 
	@Month INT, 
	@ProductId INT, 	
	@Value DECIMAL(18,2)
)
RETURNS VARCHAR(100)
AS
BEGIN
	DECLARE @InvoiceNumber VARCHAR(100)

	IF @Type = 1 
	BEGIN
		SELECT TOP 1 @InvoiceNumber = i.InvoiceNumber 
		FROM Billing.Invoice i WITH (NOLOCK) 
		JOIN
		(
				SELECT id.InvoiceId
				FROM Billing.InvoiceDetail id WITH (NOLOCK)
				JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id
				WHERE sod.ProductId = @ProductId 
					AND sod.SubTotalSalesPrice = @Value 
			UNION ALL
				SELECT dips.InvoiceId
				FROM Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK)
				JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
				WHERE dipsd.ProductId = @ProductId 
					AND dipsd.SalePrice = @Value 

			UNION ALL
				SELECT bb.InvoiceId
				FROM Billing.BasicBilling bb WITH (NOLOCK)
				JOIN Billing.BasicBillingDetail bbd WITH (NOLOCK) ON bb.Id = bbd.BasicBillingId
				WHERE bbd.ProductId = @ProductId 
					AND bbd.Price = @Value

		) id ON i.Id = id.InvoiceId
		WHERE i.Status = 1 
			AND YEAR(i.InvoiceDate) = @Year
			AND MONTH(i.InvoiceDate) = @Month
	END
	ElSE
	BEGIN
		SELECT TOP 1 @InvoiceNumber = ev.InvoiceNumber 
		FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
		JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
		WHERE ev.Status = 2 
			AND YEAR(ev.DocumentDate) = @Year
			AND MONTH(ev.DocumentDate) = @Month
			AND evd.ProductId = @ProductId 
			AND evd.UnitValue = @Value
	END
 
	RETURN @InvoiceNumber
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que busca y retorna el número de factura o documento asociado a un producto y un valor económico específico, dentro de un mes y año determinados. Según el tipo indicado (@Type): si es tipo 1, consulta facturas de venta activas (facturas clínicas de orden de servicio, facturas de venta de inventario y facturas básicas) que contengan el producto y el precio exacto; si es otro tipo, busca en comprobantes de entrada de inventario aprobados con el mismo criterio de producto, valor y período. Se usa principalmente para el reporte SISMED (Sistema de Información de Precios de Medicamentos) del Ministerio de Salud, donde se requiere identificar el número de factura de compra o venta asociado a cada movimiento de un medicamento o insumo en un período contable.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'InvoiceSISMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'InvoiceSISMED';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el número de factura (de venta o de entrada de inventario) asociado a un producto, valor y período contable, para soportar el reporte SISMED de medicamentos e insumos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El producto, año, mes y valor deben corresponder a un movimiento real registrado en facturación o en comprobantes de entrada; Para tipo=1 deben existir facturas en estado activo (Status=1) en el período; Para otros tipos deben existir comprobantes de entrada aprobados (Status=2) en el período', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera facturas de venta con Status=1 (activas); Solo considera comprobantes de entrada con Status=2 (aprobados); El filtrado de período se hace por año y mes de la fecha del documento; El emparejamiento exige coincidencia exacta de producto y valor unitario/subtotal/precio; Devuelve un único número de factura (TOP 1), aunque existan múltiples coincidencias', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reporte SISMED; Sistema de Información de Precios de Medicamentos; Ministerio de Salud; factura de venta; comprobante de entrada de inventario; medicamentos e insumos; período contable', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.Invoice: Cuando Type=1, retorna el InvoiceNumber de una factura con Status=1 cuyo año/mes de InvoiceDate coincide y cuyo detalle (InvoiceDetail/ServiceOrderDetail, DocumentInvoiceProductSales o BasicBilling) tiene el ProductId y valor solicitados; [RETURN_RESULT] Inventory.EntranceVoucher: Cuando Type<>1, retorna el InvoiceNumber de un comprobante de entrada con Status=2, año/mes de DocumentDate coincidente, y detalle con ProductId y UnitValue iguales a los parámetros', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Type = 1 → Busca en facturas de venta (Billing.Invoice) consolidando tres orígenes de detalle: InvoiceDetail vía ServiceOrderDetail, DocumentInvoiceProductSales y BasicBilling else Busca en comprobantes de entrada de inventario (Inventory.EntranceVoucher) aprobados', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail; Billing.BasicBilling; Billing.BasicBillingDetail; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'InvoiceSISMED';
GO
