



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