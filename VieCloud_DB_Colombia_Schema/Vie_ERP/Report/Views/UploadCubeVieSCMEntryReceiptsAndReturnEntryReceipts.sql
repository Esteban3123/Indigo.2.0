
CREATE view [Report].[UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts] as

	SELECT	CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		ev.DOCUMENTO 'NRO DOCUMENTO',--[NroDocumento],
		ev.DocumentDate 'FECHA DOCUMENTO',--[FechaDocumento],
		ev.InvoiceNumber 'FACTURA',--[Factura],
		ev.UnitName 'SEDE',--[Sede],
		ev.Warehouse 'BODEGA',--[Bodega],
		ev.Operation 'OPERACION',--[Operacion],
		ev.ProductType 'TIPO',--[Tipo],
		ev.CODIGO_PADRE 'CODIGO PADRE',--[CodigoPadre],
		ev.NOMBRE_PADRE 'DESCRIPCION PADRE',--[DescripcionPadre],
		ev.Code AS 'CODIGO PRODUCTO',--[CodigoProducto],
		ev.ProductName 'PRODUCTO',--[Producto],
		ev.HealthRegistration 'REGISTRO SANITARIO',-- [RegistroSanitario],
		ev.BatchCode 'LOTE',--[Lote],
		ev.ExpirationDate 'FECHA VENCIMIENTO',--[FechaVencimiento],
		ev.Quantity 'CANTIDAD',--[Cantidad],
		ev.UnitValue 'VALOR UNITARIO',--[ValorUnitario],
		(ev.Quantity * ev.UnitValue) 'VALOR TOTAL',--[ValorTotal],
		ROUND(ev.SubTotalValue * ev.DiscountPercentage / 100, 2) 'DESCUENTO',--[Descuento],
		ROUND(ev.SubTotalValue * ev.IvaPercentage / 100, 2) 'IVA',--[IVA],
		ROUND(ev.SubTotalValue * ev.RTFPercentage / 100, 2) 'RETENCION',--[Retencion],
		ev.CostCenter 'CENTRO COSTO',--[CentroCosto],
		ev.FreightValue 'VALOR FLETE',--[ValorFlete],
		ev.FreightIVAValue 'VALOR IVA FLETE',--[ValorIVAFlete],
		ev.NIT AS 'NIT',--[NIT], 
		ev.RAZON AS 'RAZON SOCIAL',--[RazonSocial],
		ev.UnitValue + ((ROUND(ev.SubTotalValue * ev.IvaPercentage / 100, 2)/ev.Quantity )) 'VALOR UNITARIO IVA',--[ValorUnitarioIVA],
		((ev.Quantity * ev.UnitValue) + (ROUND(ev.SubTotalValue * ev.IvaPercentage / 100, 2))) 'VALOR TOTAL IVA',--[ValorTotalIVA],
		'COMPROBANTES DE ENTRADA' 'TIPO MOVIMIENTO',--[TipoMovimiento],
		ev.origen AS 'ORIGEN',--[Origen],
		ev.[DocumentoOrigen] AS 'DOCUMENTO ORIGEN',--[DocumentoOrigen],
		ev.[FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM
	(
		SELECT	
			ev.Code DOCUMENTO,ev.DocumentDate,ev.InvoiceNumber,ou.UnitName,CONCAT(w.Code, ' - ', w.Name) Warehouse,'Suma' Operation,
			ISNULL(ATC.Code,ISS.Code ) AS CODIGO_PADRE,ISNULL(ATC.Name,ISS.SupplieName ) AS NOMBRE_PADRE,IP.Code,
			pt.Name ProductType,ip.HealthRegistration ,
			ip.Name ProductName,bs.BatchCode,bs.ExpirationDate,evdbs.Quantity,evd.UnitValue,evd.DiscountPercentage,ROUND((evdbs.Quantity * evd.UnitValue) * (100 - evd.DiscountPercentage) / 100, 2) SubTotalValue,			
			evd.IvaPercentage,evd.RTFPercentage,cc.Name CostCenter,ev.FreightValue,ev.FreightIVAValue,S.Code AS NIT ,S.Name AS RAZON,
			ISNULL(pur.code, ISNULL(rem.code, cir.code)) AS [DocumentoOrigen],
			CASE evd.entrancesource
				WHEN 1 THEN 'Ninguno'
				WHEN 2 THEN 'Orden de compra'
				WHEN 3 THEN 'Contrato (Fijo)'
				WHEN 4 THEN 'Remision de entrada'
				WHEN 5 THEN 'Remision de inventario en consignacion'
				ELSE 'NaN' END AS [Origen],
				CAST(EV.DocumentDate  AS DATE) 'FECHA BUSQUEDA'

		FROM Common.OperatingUnit ou WITH (NOLOCK)
		JOIN Inventory.EntranceVoucher ev WITH (NOLOCK) ON ou.Id = ev.OperatingUnitId
		JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
		JOIN Inventory.EntranceVoucherDetailBatchSerial evdbs WITH (NOLOCK) ON evd.Id = evdbs.EntranceVoucherDetailId
		JOIN Common .Supplier AS S WITH (NOLOCK) ON ev.SupplierId =S.Id
		---------------------------------------------------------------------------------------------------------------
		JOIN Inventory.Warehouse w WITH (NOLOCK) ON ev.WarehouseId = w.Id
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON evd.ProductId = ip.Id
		JOIN Inventory.ProductType pt WITH (NOLOCK) ON ip.ProductTypeId = pt.Id
		LEFT JOIN Inventory .ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
		LEFT JOIN Inventory .InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId
		LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON evdbs.BatchSerialId = bs.Id
		---------------------------------------------------------------------------------------------------------------
		LEFT JOIN Inventory.SettingInventory si WITH (NOLOCK) ON ev.OperatingUnitId = si.OperatingUnitId
		LEFT JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
		LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = CASE si.AssociateCostCenter WHEN 2 THEN pg.CostCenterId WHEN 3 THEN w.CostCenterId	END

		-- _______________________________________________________________________________________________________
		LEFT JOIN inventory.purchaseorderdetail AS purd ON evd.purchaseorderdetailid = purd.id
		LEFT JOIN inventory.purchaseorder AS pur ON purd.purchaseorderid = pur.id

		LEFT JOIN inventory.remissionentrancedetailbatchserial AS remb ON evd.remissionentrancedetailbatchserialid = remb.id
		LEFT JOIN inventory.remissionentrancedetail AS remd ON remb.remissionentrancedetailid = remd.id
		LEFT JOIN inventory.remissionentrance AS rem ON remd.remissionentranceid = rem.id

		LEFT JOIN inventory.consignmentinventoryremissiondetailbatchserial AS cirb ON evd.consignmentinventoryremissiondetailbatchserialid = cirb.id
		LEFT JOIN inventory.consignmentinventoryremissiondetail AS cird ON cirb.consignmentinventoryremissiondetailid = cird.id
		LEFT JOIN inventory.consignmentinventoryremission AS cir ON cird.consignmentinventoryremissionid = cir.id
		WHERE ev.Status = 2 AND YEAR(EV.DocumentDate)>=2022
		--CAST(EV.DocumentDate  AS DATE) BETWEEN @FECINI AND @FECFIN
	) ev

	UNION ALL

	SELECT	CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		ev.DOCUMENTO AS 'DOCUMENTO',--[DOCUMENTO],
		ev.DocumentDate 'FECHA DOCUMENTO',--[FECHA DOCUMENTO],
		ev.InvoiceNumber 'FACTURA',--[FACTURA],
		ev.UnitName 'SEDE',--[SEDE],
		ev.Warehouse 'BODEGA',--[BODEGA],
		ev.Operation 'OPERACION',--[OPERACION],
		ev.ProductType 'TIPO',--[TIPO],
		EV.CODIGO_PADRE 'CODIGO PADRE',--[CODIGO PADRE],
		EV.NOMBRE_PADRE 'NOMBRE PADRE',--[NOMBRE PADRE],
		EV.Code 'CODIGO PRODUCTO',--[CODIGO PRODUCTO],
		ev.ProductName 'PRODUCTO',--[PRODUCTO],
		ev.HealthRegistration 'REGISTRO SANITARIO',--[REGISTRO SANITARIO],
		ev.BatchCode 'LOTE',--[LOTE],
		ev.ExpirationDate 'FECHA VENCIMIENTO',--[FECHA VENCIMIENTO],
		ev.Quantity 'CANTIDAD',--[CANTIDAD],
		ev.UnitValue 'VALOR UNITARIO',--[VALOR UNITARIO],
		(ev.Quantity * ev.UnitValue) 'VALOR TOTAL',--[VALOR TOTAL],
		ROUND(ev.SubTotalValue * ev.DiscountPercentage / 100, 2) 'DESCUENTO',--[DESCUENTO],
		ROUND(ev.SubTotalValue * ev.IvaPercentage / 100, 2) 'IVA',--[IVA],
		ROUND(ev.SubTotalValue * ev.RTFPercentage / 100, 2) 'RETENCION',--[RETENCION],
		ev.CostCenter 'CENTRO DE COSTO',--[CENTRO DE COSTO],
		ev.FreightValue 'VALOR FLETE',--[VALOR FLETE],
		ev.FreightIVAValue 'VALOR IVA FLETE',--[VALOR IVA FLETE],
		ev.NIT AS 'NIT',--[NIT], 
		ev.RAZON AS 'RAZON SOCIAL',--[RAZON SOCIAL],
		ev.UnitValue + (ROUND(ev.SubTotalValue * ev.IvaPercentage / 100, 2)/ev.Quantity)  'VALOR UNITARIO MAS IVA',--[VALOR UNITARIO MAS IVA],
		((ev.Quantity * ev.UnitValue)+ROUND(ev.SubTotalValue * ev.IvaPercentage / 100, 2)) 'VALOR TOTAL MAS IVA',-- [VALOR TOTAL MAS IVA],
		'DEVOLUCION COMPROBANTES DE ENTRADA' 'TIPO MOVIMIENTO',--[TIPO MOVIMIENTO],
		ev.[Origen] 'ORIGEN',--,
		ev.[DocumentoOrigen] 'DOCUMENTO ORIGEN',--,
		ev.[FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
		--INTO INDIGODWH.PBI.STG_DEVOLUCION_ORDENES_COMPRA
	FROM
	(
		SELECT	
			evdev.Code as DOCUMENTO,evdev.DocumentDate,ev.InvoiceNumber,ou.UnitName,CONCAT(w.Code, ' - ', w.Name) Warehouse,'Resta' Operation,
			ISNULL(ATC.Code,ISS.Code ) AS CODIGO_PADRE,ISNULL(ATC.Name,ISS.SupplieName ) AS NOMBRE_PADRE,
			pt.Name ProductType,ip.HealthRegistration,ip.Code ,ip.Name ProductName,
			bs.BatchCode,bs.ExpirationDate,evdevd.Quantity,evd.UnitValue,evd.DiscountPercentage,ROUND((evdevd.Quantity * evd.UnitValue) * (100 - evd.DiscountPercentage) / 100, 2) SubTotalValue,			
			evd.IvaPercentage,evd.RTFPercentage,cc.Name CostCenter,ev.FreightValue,ev.FreightIVAValue,S.Code AS NIT ,S.Name AS RAZON,
			'Comprobante de entrada' AS [Origen],
			ev.code AS [DocumentoOrigen],CAST(evdev.DocumentDate  AS DATE) 'FECHA BUSQUEDA'
		FROM Inventory.EntranceVoucherDevolution evdev WITH (NOLOCK)
		JOIN Inventory.EntranceVoucherDevolutionDetail evdevd WITH (NOLOCK) ON evdev.Id = evdevd.EntranceVoucherDevolutionId
		JOIN Inventory.EntranceVoucherDetailBatchSerial evdbs WITH (NOLOCK) ON evdevd.EntranceVoucherDetailBatchSerialId = evdbs.Id
		JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON evdbs.EntranceVoucherDetailId = evd.Id
		JOIN Inventory.EntranceVoucher ev WITH (NOLOCK) ON evd.EntranceVoucherId = ev.Id
		JOIN Common.OperatingUnit ou WITH (NOLOCK) ON ev.OperatingUnitId = ou.Id
		JOIN Common .Supplier AS S WITH (NOLOCK) ON ev.SupplierId =S.Id
		---------------------------------------------------------------------------------------------------------------
		JOIN Inventory.Warehouse w WITH (NOLOCK) ON ev.WarehouseId = w.Id
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON evd.ProductId = ip.Id
		JOIN Inventory.ProductType pt WITH (NOLOCK) ON ip.ProductTypeId = pt.Id
		LEFT JOIN Inventory .ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
		LEFT JOIN Inventory .InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId
		LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON evdbs.BatchSerialId = bs.Id
		---------------------------------------------------------------------------------------------------------------
		LEFT JOIN Inventory.SettingInventory si WITH (NOLOCK) ON ev.OperatingUnitId = si.OperatingUnitId
		LEFT JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
		LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = CASE si.AssociateCostCenter WHEN 2 THEN pg.CostCenterId WHEN 3 THEN w.CostCenterId END 
		WHERE evdev.Status = 2 AND evdevd.Quantity>0 AND YEAR(evdev.DocumentDate)>=2022
		--CAST(evdev.DocumentDate  AS DATE)  BETWEEN @FECINI AND @FECFIN 
	) ev
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a la carga de un cubo analítico (BI/DWH) que consolida, mediante UNION ALL, dos flujos de movimientos de inventario: comprobantes de entrada de mercancía (estado=2, desde 2022) y sus devoluciones a proveedor, ambos a nivel de lote/serial. Expone valores unitarios, totales, descuentos, IVA y retención por producto, bodega, sede y proveedor (NIT/razón social), identificando el documento origen (orden de compra, remisión o consignación). Incluye marca de última actualización en zona horaria PKT.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada para cubo BI que unifica los comprobantes de entrada al inventario y sus devoluciones a proveedor, calculando valores con IVA, descuentos y retenciones, y enriqueciendo con datos de origen (orden de compra, remisión, consignación).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas Inventory.EntranceVoucher e Inventory.EntranceVoucherDevolution deben tener registros con Status = 2 (estado considerado válido/aprobado).; Los registros deben tener DocumentDate con año >= 2022.; Para devoluciones, la cantidad devuelta (evdevd.Quantity) debe ser mayor a 0.; Debe existir relación válida entre comprobante, detalle y lote/serial (joins INNER obligatorios).; Debe existir un Supplier asociado al comprobante de entrada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen comprobantes con Status=2 (aprobados/válidos), excluyendo borradores o anulados.; Solo se incluyen movimientos con DocumentDate de año 2022 en adelante.; Las devoluciones solo se reportan cuando la cantidad devuelta es estrictamente positiva.; El SubTotalValue se calcula como (Cantidad * ValorUnitario) * (100 - DiscountPercentage) / 100, redondeado a 2 decimales.; DESCUENTO, IVA y RETENCION se calculan como porcentaje sobre SubTotalValue redondeados a 2 decimales.; VALOR UNITARIO IVA = UnitValue + (IVA/Quantity); VALOR TOTAL IVA = (Quantity*UnitValue) + IVA.; Las entradas se etiquetan con Operation=''Suma'' y las devoluciones con Operation=''Resta'', señalando el sentido del movimiento de inventario.; Todas las consultas usan WITH (NOLOCK), aceptando lecturas sucias para favorecer el desempeño en cargas analíticas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de entrada de inventario; Devolución a proveedor; Orden de compra; Remisión de entrada; Inventario en consignación; Lote y serial (trazabilidad); Registro sanitario; Clasificación ATC de medicamentos; IVA / Retención en la fuente / Descuento comercial; Centro de costo; Bodega / Sede / Unidad operativa; Flete y IVA de flete; Proveedor (NIT, razón social); Tipo de producto; Factura de compra', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts: Devuelve dos conjuntos unidos por UNION ALL: TIPO MOVIMIENTO=''COMPROBANTES DE ENTRADA'' (Operation=''Suma'') desde EntranceVoucher y TIPO MOVIMIENTO=''DEVOLUCION COMPROBANTES DE ENTRADA'' (Operation=''Resta'') desde EntranceVoucherDevolution.; [RETURN_RESULT] Report.UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts: ID_COMPANY se obtiene de DB_NAME() truncado a VARCHAR(9), permitiendo identificar la base/empresa origen al consolidar en el cubo.; [RETURN_RESULT] Report.UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts: ULT_ACTUAL se calcula con GETDATE() AT TIME ZONE ''Pakistan Standard Time'' convertido a DATETIME para registrar la última actualización del dato.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si evd.entrancesource (origen del comprobante de entrada) → Mapea a etiqueta de origen: 1=''Ninguno'', 2=''Orden de compra'', 3=''Contrato (Fijo)'', 4=''Remision de entrada'', 5=''Remision de inventario en consignacion'' else ''NaN''; si si.AssociateCostCenter en la configuración de inventario → Si =2 toma el centro de costo del grupo de producto (pg.CostCenterId); si =3 toma el centro de costo de la bodega (w.CostCenterId) else NULL (sin centro de costo asociado); si Selección del documento de origen para entradas → Usa COALESCE: primero pur.code (orden de compra), luego rem.code (remisión de entrada), luego cir.code (remisión de inventario en consignación); si Determinación del CODIGO_PADRE / NOMBRE_PADRE del producto → Usa ISNULL(ATC.Code, ISS.Code): prioriza clasificación ATC; si no existe usa el insumo (InventorySupplie); si Para devoluciones, el origen siempre es fijo → Origen=''Comprobante de entrada'' y DocumentoOrigen=ev.code (código del comprobante de entrada original)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.OperatingUnit; Common.Supplier; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucherDetailBatchSerial; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ProductType; Inventory.ATC; Inventory.InventorySupplie; Inventory.BatchSerial; Inventory.SettingInventory; Inventory.ProductGroup; Payroll.CostCenter; Inventory.PurchaseOrderDetail; Inventory.PurchaseOrder; Inventory.RemissionEntranceDetailBatchSerial; Inventory.RemissionEntranceDetail; Inventory.RemissionEntrance; Inventory.ConsignmentInventoryRemissionDetailBatchSerial; Inventory.ConsignmentInventoryRemissionDetail; Inventory.ConsignmentInventoryRemission; Inventory.EntranceVoucherDevolution; Inventory.EntranceVoucherDevolutionDetail', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMEntryReceiptsAndReturnEntryReceipts';
GO
