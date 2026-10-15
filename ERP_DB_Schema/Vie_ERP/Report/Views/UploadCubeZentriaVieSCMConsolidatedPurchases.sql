
CREATE view [Report].[UploadCubeZentriaVieSCMConsolidatedPurchases] as

	-- Comprobantes de Entrada Oncologos Del Occidente 
	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		--GETDATE() AS ''InsertionDate,
		IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO045','8010007139',
		  IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO046','900377863','820001277')) AS 'CODIGO UNIDAD NEGOCIO',--[CodUnidadNegocio], --  --  
        IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO045','ONCOLOGOS DEL OCCIDENTE',
		  IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO046','UNION DE CIRUJANOS','CLINICA CANCEROLOGICA DE BOYACA')) AS 'NOMBRE UNIDAD NEGOCIO',--[NomUnidadNegocio], --  -- 
		NroDocumento 'NRO DOCUMENTO',--,
		CAST(FecDocumento AS DATE) AS 'FECHA DOCUMENTO',--[FecDocumento],
		CAST(FecConfirmacion AS DATE) AS 'FECHA CONFIRMACION',--[FecConfirmacion],
		NroFactura 'NRO FACTURA',--,
		UnidadOperativa 'UNIDAD OPERATIVA',--,
		CentroCosto 'CENTRO COSTO',--,
		Bodega 'BODEGA',--,
		TipoOperacion 'TIPO OPERACION',--,
		Operation 'OPERACION',--,
		TipoProducto 'TIPO PRODUCTO',--,
		CodPadre 'CODIGO PADRE',--,
		NomPadre 'NOMBRE PADRE',--,
		CodProducto 'CODIGO PRODUCTO',--,
		NomProducto 'NOMBRE PRODUTO',--,
		CUM 'CUM',--,
		RegSanitario 'REGISTRO SANITARIO',--,
		Lote 'LOTE',--,
		FecExpiracion 'FECHA EXPIRACION',--,
		Cantidad 'CANTIDAD',--,
		ValUnitario 'VALOR UNITARIO',--,
		ValSubTotal 'VALOR SUB TOTAL',--,  
		PorcentajeDescuento 'PORCENTAJE DESCUENTO',--,
		ValDescuento 'VALOR DESCUENTO',--,
		PorcentajeIVA 'PORCENTAJE IVA',--,
		CAST((((ValSubTotal - ValDescuento) * PorcentajeIVA) / 100) AS DECIMAL(20, 2)) AS 'VALOR IVA',--[ValIVA],
		CAST((ValSubTotal - ValDescuento) + (((ValSubTotal - ValDescuento) * PorcentajeIVA) / 100) AS DECIMAL(20, 2)) AS 'VALOR TOTAL',--[ValTotal],
		PorcentajeRetencion 'PORCENTAJE RETENCION',--,
		CAST((((ValSubTotal - ValDescuento) * PorcentajeRetencion) / 100) AS DECIMAL(20, 2)) AS 'VALOR RETENCION',--[ValRetencion],
		ValFlete 'VALOR FLETE',--,
		ValIVAFlete 'VALOR IVA FLETE',--,
		NIT 'NIT',--,
		NombreProveedor 'NOMBRE PROVEEDOR',--,
		[FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM (
		-- Comprobantes de Entrada 
		SELECT	
			ev.code AS [NroDocumento],
			ev.documentdate AS [FecDocumento],
			ev.confirmationdate AS [FecConfirmacion],
			ev.invoicenumber [NroFactura],
			ou.unitname AS [UnidadOperativa],
			cc.name AS [CentroCosto],
			CONCAT(w.code, ' - ', w.name) AS [Bodega],
			'Ingreso Comprobante' AS [TipoOperacion],
			'Suma' AS [Operation],
			pt.name AS [TipoProducto],
			ISNULL(atc.code, iss.code) AS [CodPadre],
			ISNULL(atc.name, iss.suppliename) AS [NomPadre],
			ip.code AS [CodProducto],
			ip.name AS [NomProducto],
			ip.codecum AS [CUM],
			ip.healthregistration AS [RegSanitario],
			bs.batchcode AS [Lote],
			bs.expirationdate AS [FecExpiracion],
			evdbs.quantity AS [Cantidad],
			evd.unitvalue AS [ValUnitario],
			evdbs.quantity * evd.unitvalue AS [ValSubTotal],  
			evd.discountpercentage AS [PorcentajeDescuento],
			CAST(evdbs.quantity * evd.unitvalue * evd.discountpercentage / 100 AS DECIMAL(20, 2)) AS [ValDescuento],
			evd.ivapercentage AS [PorcentajeIVA],
			evd.rtfpercentage AS [PorcentajeRetencion],
			ev.freightvalue AS [ValFlete],
			ev.freightivavalue AS [ValIVAFlete],
			s.code AS [NIT],
			s.name AS [NombreProveedor],
			CAST(ev.documentdate AS DATE) 'FECHA BUSQUEDA'
	    FROM common.operatingunit ou WITH(NOLOCK)
		INNER JOIN inventory.entrancevoucher ev WITH(NOLOCK) ON ou.id = ev.operatingunitid
		INNER JOIN inventory.entrancevoucherdetail evd WITH(NOLOCK) ON ev.id = evd.entrancevoucherid
		INNER JOIN inventory.entrancevoucherdetailbatchserial evdbs WITH(NOLOCK) ON evd.id = evdbs.entrancevoucherdetailid
		INNER JOIN common.supplier AS s WITH(NOLOCK) ON ev.supplierid = s.id
		INNER JOIN inventory.warehouse w WITH(NOLOCK) ON ev.warehouseid = w.id
		INNER JOIN inventory.inventoryproduct ip WITH(NOLOCK) ON evd.productid = ip.id
		INNER JOIN inventory.producttype pt WITH(NOLOCK) ON ip.producttypeid = pt.id
		LEFT JOIN inventory.atc AS atc WITH(NOLOCK) ON atc.id = ip.atcid 
		LEFT JOIN inventory.inventorysupplie AS iss WITH(NOLOCK) ON iss.id = ip.supplieid
		LEFT JOIN inventory.batchserial bs WITH(NOLOCK) ON evdbs.batchserialid = bs.id
		LEFT JOIN inventory.settinginventory si WITH(NOLOCK) ON ev.operatingunitid = si.operatingunitid
		LEFT JOIN inventory.productgroup pg WITH(NOLOCK) ON ip.productgroupid = pg.id
		LEFT JOIN payroll.costcenter cc WITH(NOLOCK) ON cc.id = CASE si.associatecostcenter WHEN 2 THEN pg.costcenterid WHEN 3 THEN w.costcenterid END
		WHERE ev.status = 2 
		--AND CAST(ev.documentdate  AS DATE) BETWEEN @ini_date AND @end_date 

		UNION ALL
		-- Devolucion Comprobantes de Entrada
		SELECT 
			evdev.code AS [NroDocumento],
			evdev.documentdate [FecDocumento],
			ev.confirmationdate AS [FecConfirmacion],
			ev.invoicenumber AS [NroFactura],
			ou.unitname AS [UnidadOperativa],
			cc.name AS [CentroCosto],
			CONCAT(w.code, ' - ', w.name) AS [Bodega],
			'Devolucion Comprobante' AS [TipoOperacion],
			'Resta' AS [Operation],
			pt.name AS [TipoProducto],
			ISNULL(atc.code, iss.code) AS [CodPadre],
			ISNULL(atc.name, iss.suppliename) AS [NomPadre],
			ip.code AS [CodProducto],
			ip.name AS [NomProducto],
			ip.codecum AS [CUM],
			ip.healthregistration AS [RegSanitario],
			bs.batchcode AS [Lote],
			bs.expirationdate AS [FecExpiracion],
			evdevd.quantity AS [Cantidad],
			evd.unitvalue AS [ValUnitario],
			evdevd.quantity * evd.unitvalue AS [ValSubTotal],
			evd.discountpercentage AS [PorcentajeDescuento],
			CAST(evdevd.quantity * evd.unitvalue * evd.discountpercentage / 100 AS DECIMAL(20, 2)) AS [ValDescuento],
			evd.ivapercentage AS [PorcentajeIVA],
			evd.rtfpercentage AS [PorcentajeRetencion],
			ev.freightvalue AS [ValFlete],
			ev.freightivavalue AS [ValIVAFlete],
			s.code AS [NIT],
			s.name AS [NombreProveedor],
			CAST(evdev.documentdate AS DATE) 'FECHA BUSQUEDA'
		FROM inventory.entrancevoucherdevolution evdev WITH(NOLOCK)
		INNER JOIN inventory.entrancevoucherdevolutiondetail evdevd WITH(NOLOCK) ON evdev.id = evdevd.entrancevoucherdevolutionid
		INNER JOIN inventory.entrancevoucherdetailbatchserial evdbs WITH(NOLOCK) ON evdevd.entrancevoucherdetailbatchserialid = evdbs.id
		INNER JOIN inventory.entrancevoucherdetail evd WITH(NOLOCK) ON evdbs.entrancevoucherdetailid = evd.id
		INNER JOIN inventory.entrancevoucher ev WITH(NOLOCK) ON evd.entrancevoucherid = ev.id
		INNER JOIN common.operatingunit ou WITH(NOLOCK) ON ev.operatingunitid = ou.id
		INNER JOIN common.supplier AS s WITH(NOLOCK) ON ev.supplierid = s.id
		INNER JOIN inventory.warehouse w WITH(NOLOCK) ON ev.warehouseid = w.id
		INNER JOIN inventory.inventoryproduct ip WITH(NOLOCK) ON evd.productid = ip.id
		INNER JOIN inventory.producttype pt WITH(NOLOCK) ON ip.producttypeid = pt.id
		LEFT JOIN inventory.atc AS atc WITH(NOLOCK) ON atc.id = ip.atcid 
		LEFT JOIN inventory.inventorysupplie AS iss WITH(NOLOCK) ON iss.id = ip.supplieid
		LEFT JOIN inventory.batchserial bs WITH(NOLOCK) ON evdbs.batchserialid = bs.id
		LEFT JOIN inventory.settinginventory si WITH(NOLOCK) ON ev.operatingunitid = si.operatingunitid
		LEFT JOIN inventory.productgroup pg WITH(NOLOCK) ON ip.productgroupid = pg.id
		LEFT JOIN payroll.costcenter cc WITH(NOLOCK) ON cc.id = CASE si.associatecostcenter WHEN 2 THEN pg.costcenterid WHEN 3 THEN w.costcenterid END
		WHERE evdev.status = 2 AND evdevd.quantity > 0
		--AND CAST(evdev.documentdate AS DATE) BETWEEN @ini_date AND @end_date

	) AS comprobantes