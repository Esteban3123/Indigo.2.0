

---*************************************************************INFORME DE ORDENES DE COMPRA VS COMPROBANTES DE ENTRADA****************************************************************--

CREATE view [Report].[UploadCubeVieSCMPurchaseOrdersVSEntryReceipts]
AS

	SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		OC.CODE AS 'NRO ORDEN DE COMPRA',--[NroOC], 
		OC.CreationDate AS 'FECHA ORDEN DE COMPRA',--[FechaOC],
		OC.DeliveredDate 'FECHA VENCIMIENTO ORDEN',--[FechaVencimientoOC], 
		CE.Code as 'NRO COMPROBANTE DE ENTRADA',--[NroCE], 
		CE.CreationDate AS 'FECHA COMPROBANTE DE ENTRADA',--[FechaCE], 
		CE.InvoiceNumber AS 'NRO DE FACTURA',--[NroFactura], 
		CE.InvoiceDate AS 'FECHA DE FACTURA',--[FechaFactura],
		PROV.Code AS 'NIT',--[NIT], 
		PROV.Name AS 'PROVEEDOR',--[Proveedor], 
		al.name AS 'ALMACEN',--[Almacen], 
		case 
			when PD.ProductTypeId = '1' THEN 'MEDICAMENTO' 
			when PD.ProductTypeId = '2'THEN 'INSUMOS' 
			when PD.ProductTypeId = '6'THEN 'NUTRICIONES'  
			ELSE 'OTR0' END AS 'TIPO DE PRODUCTO',--[TipoProducto], 
		PD.CODE as 'CODIGO DE PRODUCTO',--[CodigoProducto], 
		PD.Name AS 'DESCRIPCION PRODUCTO',--[DescripcionProducto], 
		PD.HealthRegistration AS 'CODIGO INVIMA',--[CodigoINVIMA], 
		OCD.Quantity AS 'CANTIDAD ORDENADA',--[CantidadOrdenada], 
		SUM(CEDL.Quantity) AS 'CANTIDAD RECIBIDA',--[CantidadRecibida], 
		--LT.BatchCode Lote, 
		--LT.ExpirationDate, 
		OCD.Value AS 'VALOR UNITARIO ORDEN DE COMPRA',--[ValUnitarioOC], 
		CED.UnitValue AS 'VALOR UNITARIO FACTURA',--[ValUnitarioFactura],
		Ocd.TotalValue AS 'VALOR TOTAL ORDEN DE COMPRA',--[ValTotalOC], 
		CED.TotalValue AS 'VALOR TOTAL FACTURA',--[ValTotalFactura], 
		ocd.IvaValue AS 'VALOR IMPUESTO ORDEN DE COMPRA',--[ValImpuestoOC],
		CED.IvaValue AS 'VALOR IMPUESTO FACTURA',--[ValImpuestoFactura],
		CE.TotalValue as 'VALOR TOTAL',--[ValorTotal]
		CAST(OC.CreationDate AS DATE) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM Inventory.PurchaseOrder OC WITH (nolock) 
	Inner JOIN  Inventory.PurchaseOrderDetail OCD WITH (nolock) on OCD.PurchaseOrderId = OC.ID
	inner join  Inventory.Warehouse al WITH (nolock) on oc.WarehouseId = al.Id
	inner join  Common.Supplier PROV WITH (nolock) on PROV.Id = oc.SupplierId
	inner join  Inventory.InventoryProduct PD WITH (nolock) on Pd.Id = ocd.ProductId
	left join  Inventory.EntranceVoucherDetail CED WITH (nolock) on ocd.Id = CED.PurchaseOrderDetailId
	LEFT JOIN  Inventory.EntranceVoucher as CE WITH (nolock) on ce.id = CED.EntranceVoucherId
	left join  Inventory.EntranceVoucherDetailBatchSerial CEDL WITH (nolock) on CED.Id = CEDL.EntranceVoucherDetailId
	--left join Inventory.BatchSerial LT WITH (nolock) on CEDL.BatchSerialId = LT.Id
	--WHERE CAST(oc.creationDate AS DATE) BETWEEN @ini_date AND @end_date
	WHERE CAST(oc.creationDate AS DATE) BETWEEN CAST(common.getdate()-35 AS DATE) AND CAST(COMMON.GETDATE() AS DATE)
	GROUP BY OC.CODE, OC.CreationDate, OC.DeliveredDate, CE.Code, CE.CreationDate, CE.InvoiceNumber, CE.InvoiceDate, PROV.Code, PROV.Name, al.name, PD.ProductTypeId, PD.CODE, PD.Name, 
	PD.HealthRegistration, OCD.Quantity, OCD.Value, CED.UnitValue, Ocd.TotalValue, CED.TotalValue, ocd.IvaValue, CED.IvaValue, CE.TotalValue
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a la carga de cubos analíticos que cruza órdenes de compra con sus comprobantes de entrada en un horizonte móvil de 35 días. Consolida por producto (medicamento, insumo, nutrición) las cantidades ordenadas vs. recibidas por lote, comparando valores unitarios, totales e IVA tanto de la orden como de la factura del proveedor. Permite auditar discrepancias entre lo pactado con el proveedor y lo efectivamente recibido en bodega, incluyendo datos fiscales del proveedor y registro INVIMA del producto.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceipts';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que cruza órdenes de compra con sus comprobantes de entrada (recepciones) de los últimos 35 días, consolidando cantidades pedidas vs recibidas, valores e impuestos por producto y proveedor para carga a cubo analítico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La función common.getdate() debe estar disponible para calcular el rango de fechas (hoy y hoy-35); Las órdenes de compra deben tener al menos un detalle, almacén, proveedor y producto válidos (joins INNER); El servidor debe soportar la zona horaria ''Pakistan Standard Time'' para el cálculo de ULT_ACTUAL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte siempre se limita a órdenes de compra cuya CreationDate esté entre hoy-35 y hoy (ventana móvil de 35 días); Una OC sin comprobante de entrada aún se reporta (LEFT JOIN sobre EntranceVoucherDetail/EntranceVoucher/BatchSerial); Una OC sin almacén, proveedor o producto nunca aparece (INNER JOIN obligatorio); El ID_COMPANY siempre corresponde al nombre de la base de datos actual truncado a 9 caracteres; La marca temporal ULT_ACTUAL se calcula convirtiendo GETDATE() a zona horaria ''Pakistan Standard Time''; Las cantidades recibidas se agregan a nivel de lote/serial mediante SUM(CEDL.Quantity), por lo que pueden duplicar si hay múltiples lotes; Todas las consultas a tablas usan WITH (NOLOCK), permitiendo lecturas sucias', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de compra; Comprobante de entrada; Proveedor; NIT; Almacén/bodega; Medicamento; Insumo; Nutrición; Registro INVIMA; Factura del proveedor; IVA/Impuesto; Cantidad ordenada vs recibida; Lote/Serial', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieSCMPurchaseOrdersVSEntryReceipts: Devuelve filas agrupadas por OC, comprobante de entrada, proveedor, almacén y producto sumando CEDL.Quantity como ''CANTIDAD RECIBIDA''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PD.ProductTypeId = ''1'' → Clasifica el producto como ''MEDICAMENTO''; si PD.ProductTypeId = ''2'' → Clasifica el producto como ''INSUMOS''; si PD.ProductTypeId = ''6'' → Clasifica el producto como ''NUTRICIONES''; si PD.ProductTypeId distinto de 1, 2 o 6 → Clasifica el producto como ''OTR0'' (categoría residual)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PurchaseOrder; Inventory.PurchaseOrderDetail; Inventory.Warehouse; Common.Supplier; Inventory.InventoryProduct; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetailBatchSerial', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceipts';
GO
