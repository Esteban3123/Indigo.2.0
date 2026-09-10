

---*************************************************************INFORME DE COMPROBANTES DE ENTRADA VS ORDENES DE COMPRA****************************************************************--

CREATE view [Report].[UploadCubeVieSCMEntryReceiptsVsPurchaseOrders]

as
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CE.Code AS 'NRO COMPROBANTE DE ENTRADA',--[NroComprobanteEntrada], 
		CE.CreationDate AS 'FECHA', --[Fecha],
		CE.InvoiceNumber AS 'NRO FACTURA',-- [NroFactura], 
		CE.InvoiceDate AS 'FECHA FACTURA',--[FechaFactura], 
		OC.CODE AS 'NRO ORDEN DE COMPRA', --[NroOrdenCompra],
		PROV.Code AS 'NIT', --[NIT], 
		PROV.Name AS 'PROVEEDOR' ,--[Proveedor], 
		al.name AS 'ALMACEN' ,--[Almacen], 
		(case when PD.ProductTypeId = '1'THEN 'MEDICAMENTO' when PD.ProductTypeId = '2'THEN 'INSUMOS'when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  ELSE 'OTR0'END) AS 'TIPO PRODUCTO',--[TipoProducto], 
		PD.CODE AS 'CODIGO PRODUCTO',--[CodigoProducto], 
		PD.Name AS 'DESCRIPCION PRODUCTO',--[DescripcionProducto], 
		OCD.Quantity AS 'CANTIDAD ORDENADA',--[CantidadOrdenada], 
		CED.Quantity AS 'CANTIDAD RECIBIDA',--[CantidadRecibida], 
		OCD.Value AS 'VALOR UNITARIO OC' ,--[ValUnitarioOC], 
		CED.UnitValue AS 'VALOR UNITARIO FACTURA',--[ValUnitarioFactura],
		Ocd.TotalValue AS 'VALOR TOTAL OC' ,--[ValTotalOC], 
		CED.TotalValue AS 'VALOR TOTAL FACTURA',--[ValTotalFactura], 
		ocd.IvaValue AS 'VALOR IMPUESTO OC',--[ValImpuestoOC],
		CED.IvaValue AS 'VALOR IMPUESTO FACTURA',--[ValImpuestoFactura],
		CE.TotalValue AS 'VALOR TOTAL',--[ValorTotal]
		CAST(CE.InvoiceDate AS DATE) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM Inventory.EntranceVoucher as CE
	inner join Inventory.EntranceVoucherDetail CED WITH (nolock) on CE.Id = CED.EntranceVoucherId
	inner join Inventory.Warehouse al WITH (nolock) on CE.WarehouseId = al.Id
	inner join Common.Supplier PROV WITH (nolock) on PROV.Id = CE.SupplierId
	inner join Inventory.InventoryProduct PD WITH (nolock) on Pd.Id = CED.ProductId
	LEFT JOIN Inventory.PurchaseOrderDetail OCD WITH (nolock) on CED.PurchaseOrderDetailId = OCD.ID
	LEFT JOIN Inventory.PurchaseOrder OC WITH (nolock) on OCD.PurchaseOrderId = OC.ID
	where CAST(CE.CreationDate AS DATE) BETWEEN CAST(common.getdate()-35 AS DATE) AND CAST(COMMON.GETDATE() AS DATE)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para cubo analítico que cruza comprobantes de entrada de mercancía con sus órdenes de compra asociadas en los últimos 35 días. Consolida por ítem recibido: proveedor, almacén, producto (clasificado como medicamento, insumo, nutrición u otro), cantidades ordenadas vs. recibidas, y valores unitarios y totales tanto de la orden de compra como de la factura del proveedor, facilitando análisis de cumplimiento y conciliación de compras en SCM.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsVsPurchaseOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsVsPurchaseOrders';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los comprobantes de entrada de inventario de los últimos 35 días junto con sus órdenes de compra asociadas, factura y proveedor, para alimentar un cubo analítico de compras vs recepciones.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsVsPurchaseOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de comprobantes de entrada (Inventory.EntranceVoucher) con su detalle, almacén, proveedor y producto válidos referenciados.; La función common.getdate() debe estar disponible para calcular la ventana temporal.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsVsPurchaseOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen comprobantes de entrada cuya fecha de creación esté dentro de los 35 días previos a la fecha actual del sistema (common.getdate()).; El comprobante de entrada, su detalle, almacén, proveedor y producto son obligatorios (INNER JOIN); la orden de compra y su detalle son opcionales (LEFT JOIN), permitiendo recepciones sin OC.; ID_COMPANY se deriva del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres, identificando la compañía en escenarios multi-BD.; La columna ULT_ACTUAL refleja la marca temporal de ejecución convertida a la zona horaria ''Pakistan Standard Time''.; El campo ''FECHA BUSQUEDA'' corresponde a la fecha de la factura (InvoiceDate) truncada a DATE.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsVsPurchaseOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'comprobante de entrada; orden de compra; factura de proveedor; proveedor; almacén; medicamento; insumo; nutrición; IVA / impuestos; cantidad ordenada vs recibida; valor unitario y total', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsVsPurchaseOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieSCMEntryReceiptsVsPurchaseOrders: Devuelve una fila por cada detalle de comprobante de entrada cuya CreationDate esté en los últimos 35 días (BETWEEN common.getdate()-35 AND common.getdate()), enlazando opcionalmente con la orden de compra origen.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsVsPurchaseOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PD.ProductTypeId = ''1'' → Clasifica el ítem como ''MEDICAMENTO''; si PD.ProductTypeId = ''2'' → Clasifica el ítem como ''INSUMOS''; si PD.ProductTypeId = ''6'' → Clasifica el ítem como ''NUTRICIONES'' else Cualquier otro ProductTypeId se etiqueta como ''OTR0''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsVsPurchaseOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'common.getdate', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsVsPurchaseOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.Warehouse; Common.Supplier; Inventory.InventoryProduct; Inventory.PurchaseOrderDetail; Inventory.PurchaseOrder', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsVsPurchaseOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsVsPurchaseOrders';
GO
