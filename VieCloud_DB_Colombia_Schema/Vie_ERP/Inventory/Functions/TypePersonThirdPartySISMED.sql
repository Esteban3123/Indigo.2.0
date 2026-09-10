CREATE FUNCTION [Inventory].[TypePersonThirdPartySISMED] 
(
	@Type varchar(2),
	@Year INT, 
	@Month INT, 
	@ProductId INT, 	
	@Value DECIMAL(18,2)
)
RETURNS VARCHAR(2)
AS
BEGIN
	DECLARE @TypePerson VARCHAR(2)

	IF @Type = 'VN' 
	BEGIN
		SELECT TOP 1 @TypePerson = IIF(t.PersonType = 1, 'PN','NI')
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

		SELECT TOP 1 @TypePerson = IIF(t.PersonType = 1, 'PN','NI')
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
 
	RETURN @TypePerson
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina el tipo de persona (natural ''PN'' o jurídica/no identificada ''NI'') del tercero asociado a una transacción de inventario o facturación para los reportes SISMED (Sistema de Información de Precios de Medicamentos). Recibe como parámetros el tipo de movimiento (''VN'' para ventas o cualquier otro tipo para compras/entradas), el año y mes de la transacción, el producto (medicamento o insumo) y su valor unitario. Para ventas (''VN''), busca en las facturas de cobro (Invoice), sus detalles de órdenes de servicio, facturas de inventario y facturación básica el tercero pagador vinculado al producto y valor indicados; para compras, consulta los comprobantes de entrada de inventario y su proveedor asociado. En ambos casos, clasifica al tercero según su tipo de persona registrado en el maestro de terceros (ThirdParty), permitiendo generar correctamente los archivos de reporte SISMED exigidos por las autoridades sanitarias colombianas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'TypePersonThirdPartySISMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'TypePersonThirdPartySISMED';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el tipo de persona (Natural ''PN'' o Jurídica/No identificada ''NI'') del tercero asociado a una transacción de venta o compra de un producto en un periodo, para clasificación en reportes SISMED.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'TypePersonThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de tipo debe ser ''VN'' (ventas) o cualquier otro valor se interpreta como compras; Deben existir transacciones (facturas o comprobantes de entrada) en el año y mes indicados, para el producto y valor unitario especificados', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'TypePersonThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera facturas de venta con Status = 1 (activas/vigentes); Solo considera comprobantes de entrada con Status = 2; El filtro temporal se aplica sobre InvoiceDate (ventas) o DocumentDate (compras), por año y mes; El match de transacción exige coincidencia exacta de ProductId y valor (SubTotalSalesPrice/SalePrice/Price/UnitValue); Solo retorna el tipo de persona de UN tercero (TOP 1), aunque haya múltiples coincidencias; En ventas se unifican tres orígenes documentales: facturas de servicios, facturas de productos de inventario y facturación básica', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'TypePersonThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reporte SISMED; Tipo de persona (Natural/Jurídica); Tercero; Proveedor; Factura de venta; Comprobante de entrada de inventario; Facturación básica; Producto/medicamento', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'TypePersonThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.ThirdParty: Cuando PersonType = 1 retorna ''PN'' (Persona Natural); en caso contrario retorna ''NI''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'TypePersonThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo = ''VN'' (ventas) → Busca el tercero en facturas de venta (Billing.Invoice con Status=1) cruzando con InvoiceDetail+ServiceOrderDetail, DocumentInvoiceProductSales(Detail) o BasicBilling(Detail) que coincidan en producto y precio else Busca el tercero (proveedor) en comprobantes de entrada de inventario (EntranceVoucher con Status=2) que coincidan en producto y valor unitario; si ThirdParty.PersonType = 1 → Clasifica como ''PN'' (Persona Natural) else Clasifica como ''NI'' (No Identificada / Jurídica)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'TypePersonThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail; Billing.BasicBilling; Billing.BasicBillingDetail; Common.ThirdParty; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Common.Supplier', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'TypePersonThirdPartySISMED';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'TypePersonThirdPartySISMED';
GO
