



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