
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