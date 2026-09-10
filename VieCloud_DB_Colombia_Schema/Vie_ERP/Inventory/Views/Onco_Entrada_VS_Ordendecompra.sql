

CREATE     VIEW [Inventory].[Onco_Entrada_VS_Ordendecompra] 

AS

select CE.Code as NroCEntrada, CE.CreationDate Fecha, CE.InvoiceNumber NroFactura, CE.InvoiceDate FechaFactura, OC.CODE NroOrdenCompra,PROV.Code Nit, PROV.Name NombreProveedor, 
al.name NombAlmacen, (case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END) TIPO, PD.CODE as CodProd, PD.Name Nomproducto, OCD.Quantity CantOrdenada, CED.Quantity CantRecibida, OCD.Value VlUnitOrdenC , CED.UnitValue VlUnitFact,
Ocd.TotalValue VlTotalOrdenC, CED.TotalValue VlTotFact, ocd.IvaValue VlImpOC,CED.IvaValue Impfact,CE.TotalValue
from Inventory.EntranceVoucher as CE
inner join Inventory.EntranceVoucherDetail CED WITH (nolock) on CE.Id = CED.EntranceVoucherId
inner join Inventory.Warehouse al WITH (nolock) on CE.WarehouseId = al.Id
inner join Common.Supplier PROV WITH (nolock) on PROV.Id = CE.SupplierId
inner join Inventory.InventoryProduct PD WITH (nolock) on Pd.Id = CED.ProductId
LEFT JOIN Inventory.PurchaseOrderDetail OCD WITH (nolock) on CED.PurchaseOrderDetailId = OCD.ID
LEFT JOIN Inventory.PurchaseOrder OC WITH (nolock) on OCD.PurchaseOrderId = OC.ID

---where ce.code ='ACBA0000000001'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que cruza las entradas de mercancía al inventario (comprobantes de entrada) con sus respectivas órdenes de compra, permitiendo comparar lo que se ordenó al proveedor versus lo que efectivamente se recibió en bodega. Integra datos del comprobante de entrada, el detalle por producto (medicamentos, insumos, nutricciones u otros), el proveedor (NIT y nombre), el almacén receptor y la orden de compra origen, mostrando cantidades ordenadas y recibidas, valores unitarios y totales tanto de la orden como de la factura del proveedor, e impuestos (IVA). Está orientada al módulo de oncología y sirve para auditoría, conciliación de compras y control de recepciones: detectar diferencias de cantidad o precio entre lo pactado en la orden de compra y lo facturado/recibido por el proveedor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_Entrada_VS_Ordendecompra';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_Entrada_VS_Ordendecompra';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone una vista comparativa entre las entradas de mercancía al inventario (vale de entrada y factura) y la orden de compra que las originó, con datos de proveedor, almacén, producto, cantidades y valores.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Entrada_VS_Ordendecompra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El comprobante de entrada debe tener almacén, proveedor y al menos un detalle con producto válido.; Para mostrar datos de orden de compra, el detalle de entrada debe estar enlazado a un PurchaseOrderDetail existente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Entrada_VS_Ordendecompra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan entradas que tengan al menos un detalle (EntranceVoucherDetail), un almacén, un proveedor y un producto válidos (joins INNER).; La relación con la orden de compra es opcional: una entrada puede existir sin orden de compra asociada (LEFT JOIN).; El tipo de producto se traduce siempre a una etiqueta textual fija (''MEDICAMENTO'', ''INSUMOS'', ''NUTRICIONES'' u ''OTR0'').; Las cantidades y valores ordenados provienen de la orden de compra; las cantidades y valores recibidos/facturados provienen del detalle del vale de entrada, permitiendo comparar lo ordenado contra lo recibido/facturado.; Las consultas sobre tablas relacionadas se realizan con NOLOCK, asumiendo lecturas sin bloqueo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Entrada_VS_Ordendecompra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante/vale de entrada de inventario; Orden de compra; Proveedor; Almacén/bodega; Producto de inventario (medicamento, insumo, nutrición); Factura de proveedor; IVA / impuestos; Cantidad ordenada vs cantidad recibida; Valor unitario y total de orden de compra y factura', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Entrada_VS_Ordendecompra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve una fila por cada detalle de comprobante de entrada cruzado opcionalmente con su detalle de orden de compra, mostrando cantidades ordenadas vs recibidas y valores unitarios/totales/IVA de orden de compra y factura.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Entrada_VS_Ordendecompra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ProductTypeId = 1 → Clasifica el producto como ''MEDICAMENTO''; si ProductTypeId = 2 → Clasifica el producto como ''INSUMOS''; si ProductTypeId = 6 → Clasifica el producto como ''NUTRICIONES'' else Clasifica el producto como ''OTR0'' (otros)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Entrada_VS_Ordendecompra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.Warehouse; Common.Supplier; Inventory.InventoryProduct; Inventory.PurchaseOrderDetail; Inventory.PurchaseOrder', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Entrada_VS_Ordendecompra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Entrada_VS_Ordendecompra';
GO
