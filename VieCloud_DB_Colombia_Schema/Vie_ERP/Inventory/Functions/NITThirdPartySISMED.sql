CREATE FUNCTION [Inventory].[NITThirdPartySISMED] 
(
	@Type varchar(2),
	@Year INT, 
	@Month INT, 
	@ProductId INT, 	
	@Value DECIMAL(18,2)
)
RETURNS VARCHAR(20)
AS
BEGIN
	DECLARE @NIT VARCHAR(20)

	IF @Type = 'VN' 
	BEGIN
		SELECT TOP 1 @NIT = t.Nit
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
		JOIN Common.ThirdParty t WITH(NOLOCK) ON t.Id = i.ThirdPartyId
		WHERE i.Status = 1 
			AND YEAR(i.InvoiceDate) = @Year
			AND MONTH(i.InvoiceDate) = @Month
	END
	ElSE
	BEGIN

		SELECT TOP 1 @NIT = t.Nit
				FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
				JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
				JOIN Common.Supplier s WITH(NOLOCK) ON s.Id = ev.SupplierId
				JOIN Common.ThirdParty t WITH(NOLOCK) ON t.Id = s.IdThirdParty
				WHERE ev.Status = 2 
					AND YEAR(ev.DocumentDate) = @Year
					AND MONTH(ev.DocumentDate) = @Month
					AND evd.ProductId = @ProductId 
					AND evd.UnitValue = @Value
	END
 
	RETURN @NIT
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que obtiene el NIT (número de identificación tributaria) del tercero asociado a una transacción de inventario, dado un producto, un valor monetario, un año y un mes específicos. Cuando el tipo es ''VN'' (venta), busca la factura activa que contenga ese producto con ese valor exacto consultando tres fuentes posibles: el detalle de facturas de servicios clínicos, el detalle de facturas de venta de inventario (farmacia/insumos) y el detalle de facturación básica; luego recupera el NIT del tercero pagador asociado a esa factura. Para cualquier otro tipo (compras/entradas), busca el NIT del proveedor en los comprobantes de entrada de inventario que coincidan con el producto, valor, año y mes indicados. Se utiliza principalmente en la generación de reportes SISMED, donde es obligatorio identificar el NIT del comprador o proveedor por cada movimiento de producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'NITThirdPartySISMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'NITThirdPartySISMED';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el NIT del tercero (pagador o proveedor) asociado a un movimiento de producto, según tipo de operación (venta o entrada), para reportes SISMED.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'NITThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Para tipo ''VN'', deben existir facturas con Status=1 en el año y mes indicados que incluyan el producto y valor buscados en cualquiera de los detalles de venta (servicio, inventario o facturación básica).; Para tipos distintos a ''VN'', deben existir comprobantes de entrada con Status=2 en el año y mes indicados, vinculados a un proveedor con tercero asociado, que coincidan en producto y valor unitario.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'NITThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera facturas de venta con Status=1 (activas/emitidas).; Solo considera comprobantes de entrada con Status=2.; Filtra siempre por año y mes de la fecha del documento (InvoiceDate o DocumentDate).; Devuelve un único NIT (TOP 1); si no hay coincidencias, retorna NULL.; El emparejamiento por valor se hace contra SubTotalSalesPrice, SalePrice, Price o UnitValue según la fuente del detalle.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'NITThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'NIT; tercero pagador; proveedor; factura de venta; comprobante de entrada de inventario; reporte SISMED; producto', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'NITThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.ThirdParty: Cuando tipo=''VN'' y existe factura con Status=1 cuyo detalle (servicio, inventario o básica) coincide con producto y valor en el año/mes, retorna el NIT del tercero pagador de la factura.; [RETURN_RESULT] Common.ThirdParty: Cuando tipo distinto de ''VN'' y existe comprobante de entrada con Status=2 en el año/mes con detalle que coincide en producto y UnitValue, retorna el NIT del tercero asociado al proveedor.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'NITThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Type = ''VN'' → Busca el NIT del tercero pagador en facturas de venta (Billing.Invoice) cuyo detalle por servicio, venta de inventario o facturación básica coincida con el producto y valor. else Busca el NIT del tercero asociado al proveedor en los comprobantes de entrada de inventario que coincidan con producto, valor unitario, año y mes.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'NITThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail; Billing.BasicBilling; Billing.BasicBillingDetail; Common.ThirdParty; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Common.Supplier', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'NITThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'NITThirdPartySISMED';
GO
