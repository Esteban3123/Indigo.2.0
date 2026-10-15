



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