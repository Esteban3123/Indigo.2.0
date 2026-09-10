

CREATE view [Report].[UploadCubeVieSCMPurchaseOrdersVSEntryReceiptsSummarized]
AS

SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		oc.code AS 'NRO DE ORDEN',--[NroOrden], 
		oc.creationdate AS 'FECHA DE ORDEN',-- [FechaOrden],
		oc.delivereddate AS 'FECHA VENCIMIENTO ORDEN',--[FechaVencimientoOrden], 
		prov.code AS 'NIT',--[NIT], 
		prov.name AS 'PROVEEDOR',--[Proveedor], 
		al.name AS 'ALMACEN',--[Almacen], 
		CASE
			WHEN pd.producttypeid = '1' THEN 'Medicamento' 
			WHEN pd.producttypeid = '2'THEN 'Insumos' 
			WHEN pd.producttypeid = '6'THEN 'Nutriciones'  
			ELSE 'Otro' END AS 'TIPO PRODUCTO',--[TipoProducto], 
		pd.code AS 'CODIGO PRODUCTO',--[CodigoProducto], 
		pd.name 'PRODUCTO',--[Producto], 
		pd.healthregistration As 'INVIMA',--[INVIMA], 
		ocd.quantity AS 'CANTIDAD ORDENADA',--[CantidadOrdenada], 
		SUM(cedl.quantity) AS 'CANTIDAD RECIBIDA',--[CantidadRecibida], 
		ocd.value AS 'VALOR UNITARIO ORDEN',--[ValorUnitarioOrden], 
		ced.unitvalue AS 'VALOR UNITARIO FACTURA',--[ValUnitarioFactura],
		ocd.totalvalue AS 'VALOR TOTAL ORDEN',--[ValTotalOrden], 
		SUM(ced.totalvalue) AS 'VALOR TOTAL FACTURA',--[ValTotalFactura], 
		ocd.ivavalue AS 'VALOR IVA ORDEN',--[ValIVAOrden],
		SUM(ced.ivavalue) AS 'VALOR IVA FACTURA',--[ValIVAFactura],
		ce.totalvalue AS 'VALOR TOTAL GENERAL FACTURA',--[ValTotalGeneralFactura]
		CAST(oc.creationdate AS DATE) AS 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM inventory.purchaseorder AS oc WITH (NOLOCK)
	INNER JOIN inventory.purchaseorderdetail AS ocd WITH (NOLOCK) ON ocd.purchaseorderid = oc.id
	INNER JOIN inventory.warehouse AS al WITH (NOLOCK) ON  oc.warehouseid = al.id
	INNER JOIN common.supplier AS prov WITH (NOLOCK) ON prov.id = oc.supplierid
	INNER JOIN inventory.inventoryproduct AS pd WITH (NOLOCK) ON pd.id = ocd.productid
	LEFT JOIN inventory.entrancevoucherdetail AS ced WITH (NOLOCK) ON ocd.id = ced.purchaseorderdetailid
	LEFT JOIN inventory.entrancevoucher AS ce WITH (NOLOCK) ON ce.id = ced.entrancevoucherid
	LEFT JOIN inventory.entrancevoucherdetailbatchserial AS cedl WITH (NOLOCK) ON ced.id = cedl.entrancevoucherdetailid
	--WHERE CAST(oc.creationdate AS DATE) >= @ini_date
	where CAST(oc.creationdate AS DATE) BETWEEN CAST(common.getdate()-35 AS DATE) AND CAST(COMMON.GETDATE() AS DATE)
	GROUP BY oc.code, oc.creationdate, oc.delivereddate, prov.code, prov.name, al.name, pd.producttypeid, pd.code, pd.name, pd.healthregistration, ocd.quantity, ocd.value, ced.unitvalue, ocd.totalvalue, ocd.ivavalue, ce.totalvalue
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de carga para cubo OLAP que consolida, para los últimos 35 días, las órdenes de compra de inventario frente a sus comprobantes de entrada (recepciones), agrupando cantidades y valores ordenados versus recibidos/facturados por proveedor, almacén y producto (clasificado en Medicamento, Insumos, Nutriciones u Otro). Permite analizar diferencias entre lo pactado en la orden y lo efectivamente recibido, incluyendo IVA y registro sanitario (INVIMA).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceiptsSummarized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceiptsSummarized';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida para carga a cubo BI la comparación entre órdenes de compra y sus recepciones (entrance vouchers) de los últimos 35 días, sumarizando cantidades y valores por orden/producto.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceiptsSummarized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas de inventory (purchaseorder, purchaseorderdetail, warehouse, inventoryproduct, entrancevoucher*) y common.supplier deben existir y estar accesibles.; La función common.getdate() debe estar disponible para acotar la ventana temporal.; El servidor debe soportar la zona horaria ''Pakistan Standard Time'' usada para timestamp de actualización.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceiptsSummarized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes cuya creationdate esté entre common.getdate()-35 y common.getdate() (ventana móvil de 35 días).; El identificador de compañía expuesto se trunca a VARCHAR(9) sobre DB_NAME().; ULT_ACTUAL siempre se calcula con GETDATE() convertido a la zona horaria ''Pakistan Standard Time''.; Las recepciones (entrancevoucher*) se incluyen vía LEFT JOIN: una orden sin recepciones aún aparece con cantidades/valores de factura en NULL.; Todas las lecturas se hacen con WITH (NOLOCK), permitiendo lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceiptsSummarized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de compra; Proveedor (NIT); Almacén; Producto (medicamento, insumo, nutrición); Registro sanitario INVIMA; Comprobante de entrada (entrance voucher); Lote/Serial de recepción; IVA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceiptsSummarized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por orden de compra y detalle de producto, sumando quantity, totalvalue e ivavalue de los entrance vouchers asociados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceiptsSummarized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pd.producttypeid = ''1'' → Clasifica TIPO PRODUCTO como ''Medicamento'' else evalúa siguientes valores de producttypeid; si pd.producttypeid = ''2'' → Clasifica TIPO PRODUCTO como ''Insumos''; si pd.producttypeid = ''6'' → Clasifica TIPO PRODUCTO como ''Nutriciones''; si pd.producttypeid no está en (1,2,6) → Clasifica TIPO PRODUCTO como ''Otro''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceiptsSummarized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'inventory.purchaseorder; inventory.purchaseorderdetail; inventory.warehouse; common.supplier; inventory.inventoryproduct; inventory.entrancevoucherdetail; inventory.entrancevoucher; inventory.entrancevoucherdetailbatchserial', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceiptsSummarized';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPurchaseOrdersVSEntryReceiptsSummarized';
GO
