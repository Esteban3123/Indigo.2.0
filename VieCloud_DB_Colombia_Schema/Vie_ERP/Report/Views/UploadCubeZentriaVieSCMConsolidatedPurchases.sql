
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting destinada a alimentar un cubo analítico (Zentria) con las compras consolidadas de inventario SCM. Combina mediante UNION ALL los comprobantes de entrada confirmados (status=2) y sus devoluciones, calculando valores de IVA, descuento y retención a nivel de lote/serial por producto. Identifica dinámicamente la unidad de negocio (Oncólogos del Occidente, Unión de Cirujanos o Clínica Cancerologica de Boyacá) según la base de datos activa, permitiendo consolidar múltiples compañías del grupo Zentria en un único dataset de compras.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeZentriaVieSCMConsolidatedPurchases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeZentriaVieSCMConsolidatedPurchases';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida para cubo de reporting las compras (comprobantes de entrada) y sus devoluciones del módulo SCM/Inventario por unidad de negocio Zentria, calculando IVA, descuento, retención y totales por producto, lote y proveedor.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeZentriaVieSCMConsolidatedPurchases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La base de datos debe ser una de las esperadas (INDIGO045, INDIGO046 u otra) ya que el mapeo de unidad de negocio depende de DB_NAME(); Los comprobantes y devoluciones deben encontrarse en estado status=2 para ser considerados; Deben existir relaciones íntegras entre entrancevoucher, su detalle y el detalle batch/serial (INNER JOIN); settinginventory debe tener associatecostcenter en {2,3} para resolver el centro de costo, en otro caso queda NULL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeZentriaVieSCMConsolidatedPurchases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen comprobantes y devoluciones con status = 2 (confirmados); En devoluciones se exige cantidad > 0 (evdevd.quantity > 0); ValIVA se calcula siempre sobre la base (ValSubTotal - ValDescuento) aplicando PorcentajeIVA; ValTotal = (ValSubTotal - ValDescuento) + IVA calculado, redondeado a DECIMAL(20,2); ValRetencion se calcula sobre (ValSubTotal - ValDescuento) aplicando PorcentajeRetencion; ValDescuento = cantidad * unitvalue * discountpercentage / 100, redondeado a DECIMAL(20,2); Las devoluciones se reportan con Operation=''Resta'' para que al consolidarse netifiquen las compras (Operation=''Suma''); Los comprobantes de entrada se identifican por la unidad de negocio derivada únicamente del nombre de la base de datos (DB_NAME); ULT_ACTUAL se entrega convertido a zona horaria ''Pakistan Standard Time''; FECHA BUSQUEDA corresponde al documentdate del comprobante o de la devolución (no a la fecha de confirmación)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeZentriaVieSCMConsolidatedPurchases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de entrada; Devolución de comprobante de entrada; Unidad de negocio; Unidad operativa; Centro de costo; Bodega; Proveedor (NIT); Producto de inventario; ATC; Insumo; CUM; Registro sanitario; Lote y fecha de expiración; IVA; Retención en la fuente (RTF); Descuento; Flete; Compras consolidadas SCM', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeZentriaVieSCMConsolidatedPurchases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DB_NAME() = ''INDIGO045'' → Identifica la unidad de negocio como ONCOLOGOS DEL OCCIDENTE con código 8010007139 else Si DB_NAME()=''INDIGO046'' → UNION DE CIRUJANOS (900377863); en cualquier otro caso → CLINICA CANCEROLOGICA DE BOYACA (820001277); si Bloque UNION ALL: primer SELECT con TipoOperacion=''Ingreso Comprobante'' y Operation=''Suma'' → Reporta los comprobantes de entrada (compras) sumando inventario else Segundo SELECT marca TipoOperacion=''Devolucion Comprobante'' y Operation=''Resta'' para devoluciones; si CASE si.associatecostcenter WHEN 2 THEN pg.costcenterid WHEN 3 THEN w.costcenterid END → Asocia el centro de costo según configuración: si associatecostcenter=2 toma el del grupo de producto; si =3 toma el de la bodega else Para otros valores el centro de costo queda NULL; si ISNULL(atc.code, iss.code) / ISNULL(atc.name, iss.suppliename) → Usa el código/nombre ATC del producto como ''padre''; si no existe, recurre al insumo (inventorysupplie)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeZentriaVieSCMConsolidatedPurchases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'common.operatingunit; inventory.entrancevoucher; inventory.entrancevoucherdetail; inventory.entrancevoucherdetailbatchserial; common.supplier; inventory.warehouse; inventory.inventoryproduct; inventory.producttype; inventory.atc; inventory.inventorysupplie; inventory.batchserial; inventory.settinginventory; inventory.productgroup; payroll.costcenter; inventory.entrancevoucherdevolution; inventory.entrancevoucherdevolutiondetail', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeZentriaVieSCMConsolidatedPurchases';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeZentriaVieSCMConsolidatedPurchases';
GO
