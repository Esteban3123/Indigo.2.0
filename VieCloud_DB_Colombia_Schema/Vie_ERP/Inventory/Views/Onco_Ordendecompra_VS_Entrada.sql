

CREATE       VIEW [Inventory].[Onco_Ordendecompra_VS_Entrada] 

AS

select  OC.CODE NroOrdenCompra, OC.CreationDate AS FechaOC,OC.DeliveredDate FechaVencOC, CE.Code as NroCEntrada, CE.CreationDate Fecha, CE.InvoiceNumber NroFactura, CE.InvoiceDate FechaFactura,PROV.Code Nit, PROV.Name NombreProveedor, 
al.name NombAlmacen, (case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END) TIPO, 
PD.CODE as CodProd, PD.Name Nomproducto, 
PD.HealthRegistration IVIMA, OCD.Quantity CantOrdenada, CEDL.Quantity CantRecibida, LT.BatchCode Lote, LT.ExpirationDate, OCD.Value VlUnitOrdenC , CED.UnitValue VlUnitFact,
Ocd.TotalValue VlTotalOrdenC, CED.TotalValue VlTotFact, ocd.IvaValue VlImpOC,CED.IvaValue Impfact,CE.TotalValue as VlTotalFact

from Inventory.PurchaseOrder OC WITH (nolock) 
Inner JOIN Inventory.PurchaseOrderDetail OCD WITH (nolock) on OCD.PurchaseOrderId = OC.ID
inner join Inventory.Warehouse al WITH (nolock) on oc.WarehouseId = al.Id
inner join Common.Supplier PROV WITH (nolock) on PROV.Id = oc.SupplierId
inner join Inventory.InventoryProduct PD WITH (nolock) on Pd.Id = ocd.ProductId
left join Inventory.EntranceVoucherDetail CED WITH (nolock) on ocd.Id = CED.PurchaseOrderDetailId
LEFT JOIN Inventory.EntranceVoucher as CE WITH (nolock) on ce.id = CED.EntranceVoucherId
left join Inventory.EntranceVoucherDetailBatchSerial CEDL WITH (nolock) on CED.Id = CEDL.EntranceVoucherDetailId
left join Inventory.BatchSerial LT WITH (nolock) on CEDL.BatchSerialId = LT.Id

---ORDER BY  OC.CODE 

---where ce.code ='ACBA0000000001'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de oncología que cruza las órdenes de compra contra los comprobantes de entrada de mercancía al inventario, permitiendo comparar lo que se pidió al proveedor versus lo que realmente se recibió en bodega. Integra datos de la orden de compra (número, fechas, proveedor, NIT), el comprobante de entrada (número, factura del proveedor, fecha), el producto (código, nombre, tipo: medicamento, insumo o nutrición, registro INVIMA), las cantidades ordenadas y recibidas, los valores unitarios y totales tanto de la orden como de la factura, el IVA, y la trazabilidad por lote y fecha de vencimiento. Sirve principalmente para auditoría y control de compras oncológicas, conciliación de facturas de proveedores, seguimiento de entregas pendientes y verificación de precios pactados versus facturados.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_Ordendecompra_VS_Entrada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_Ordendecompra_VS_Entrada';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de conciliación entre órdenes de compra y comprobantes de entrada de inventario, mostrando cantidades, valores y lotes recibidos frente a lo ordenado por producto y proveedor.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Ordendecompra_VS_Entrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de órdenes de compra con su detalle (PurchaseOrder/PurchaseOrderDetail).; El producto del detalle debe existir en el catálogo InventoryProduct.; La orden debe tener bodega (Warehouse) y proveedor (Supplier) válidos.; Para mostrar datos de recepción, debe existir EntranceVoucherDetail enlazado al PurchaseOrderDetail.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Ordendecompra_VS_Entrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fila de la vista corresponde a un detalle de orden de compra existente (INNER JOIN con PurchaseOrderDetail).; El producto, proveedor y bodega de la OC siempre existen (INNER JOIN obligatorios).; La recepción (comprobante de entrada, lotes y seriales) es opcional respecto a la OC (LEFT JOIN).; Las lecturas se realizan con NOLOCK, por lo que pueden incluir datos no confirmados (lecturas sucias).; El tipo de producto se normaliza a un dominio fijo: MEDICAMENTO, INSUMOS, NUTRICIONES u OTR0.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Ordendecompra_VS_Entrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de compra; Comprobante de entrada; Proveedor; Almacén/Bodega; Producto de inventario; Medicamento; Insumo; Nutrición; Lote; Fecha de vencimiento; Registro sanitario (INVIMA); Factura de proveedor; IVA; Cantidad ordenada vs cantidad recibida', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Ordendecompra_VS_Entrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por combinación de detalle de OC con su(s) detalle(s) de comprobante de entrada y lote/serial; si no hay recepción, las columnas de entrada/factura/lote vienen en NULL por los LEFT JOIN.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Ordendecompra_VS_Entrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PD.ProductTypeId = ''1'' → Clasifica el producto como ''MEDICAMENTO''; si PD.ProductTypeId = ''2'' → Clasifica el producto como ''INSUMOS''; si PD.ProductTypeId = ''6'' → Clasifica el producto como ''NUTRICIONES''; si PD.ProductTypeId no está en (1,2,6) → Clasifica el producto como ''OTR0''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Ordendecompra_VS_Entrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PurchaseOrder; Inventory.PurchaseOrderDetail; Inventory.Warehouse; Common.Supplier; Inventory.InventoryProduct; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetailBatchSerial; Inventory.BatchSerial', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Ordendecompra_VS_Entrada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Ordendecompra_VS_Entrada';
GO
