

CREATE VIEW [Inventory].[ViewReportKardex_1]
AS
SELECT	k.Id as KardexId, 
		k.WarehouseId, w.Code as WarehouseCode, w.Name as WarehouseName, w.Code + ' - ' + w.Name as WarehouseDescription, 
		k.ProductId, p.Code as ProductCode, p.Name as ProductName, p.Code + ' - ' + p.Name as ProductDescription, 
		p.ProductControl, p.CodeCUM, p.HealthRegistration, 
		k.BatchSerialId, bs.BatchCode as BatchCode, bs.ExpirationDate, 
		k.DocumentDate, k.CreationDate, k.MovementType, k.Quantity, 
		k.Value, k.PreviousCost, k.AverageCost, k.PreviousAverageCost, 
		k.PreviousAmountProduct, k.PreviousAmountWarehouse, k.PreviousAmountBatch,
		k.EntityId, k.EntityCode, case k.EntityName when 'RemissionEntrance' then case RED.SourceCode when null then 'Traslado Interno' else 'RemissionEntrance' end else k.EntityName end as EntityName, k.ImportedEntityId,k.ImportedEntityCode,k.ImportedEntityName,
		k.AffectInventory, 

		case k.EntityName
			when 'PharmaceuticalDispensing' then (pa.IPCODPACI + ' - ' + pa.IPNOMCOMP)
			when 'PharmaceuticalDispensingDevolution' then (pddpa.IPCODPACI + ' - ' + pddpa.IPNOMCOMP)
			when 'TransferOrder' then iif(tpto.Id is null, (sp.Identification + ' - ' + sp.Fullname), (tpto.Nit + ' - ' + tpto.Name))
			when 'EntranceVoucher' then (spev.Code + ' - ' + spev.Name)
			else ''
		end as NamePatient,


		--IIF
		--(
		--	k.EntityName = 'PharmaceuticalDispensing',
		--	(pa.IPCODPACI + ' - ' + pa.IPNOMCOMP), 
		--	IIF
		--	(
		--		k.EntityName = 'PharmaceuticalDispensingDevolution', 
		--		(pddpa.IPCODPACI + ' - ' + pddpa.IPNOMCOMP),
		--		IIF
		--		(
		--			k.EntityName = 'TransferOrder', 
		--			(tpto.Nit + ' - ' + tpto.Name),
		--			IIF
		--			(
		--				k.EntityName = 'EntranceVoucher', 
		--				(spev.Code + ' - ' + spev.Name),
		--				''
		--			)
		--		)
		--	)
		--) as NamePatient,
		IIF(k.MovementType = 1, k.Quantity, 0) as QuantityEntrance, IIF(k.MovementType = 2, k.Quantity, 0) as QuantityExit, 
		(k.PreviousAverageCost * k.PreviousAmountProduct) as PreviousCostTotalCalculated,
		(k.AverageCost * (IIF(k.AffectInventory = 1, IIF(k.MovementType = 1, k.PreviousAmountProduct + k.Quantity, k.PreviousAmountProduct - k.Quantity), k.PreviousAmountProduct))) as CostTotalCalculated, 
		(IIF(k.AffectInventory = 1, IIF(k.MovementType = 1, k.PreviousAmountProduct + k.Quantity, k.PreviousAmountProduct - k.Quantity), k.PreviousAmountProduct)) as PreviousAmountProductCalculated, 
		(IIF(k.AffectInventory = 1, IIF(k.MovementType = 1, k.PreviousAmountWarehouse + k.Quantity, k.PreviousAmountWarehouse - k.Quantity), k.PreviousAmountWarehouse)) as PreviousAmountWarehouseCalculated, 
		(IIF(k.AffectInventory = 1, IIF(k.MovementType = 1, k.PreviousAmountBatch + k.Quantity, k.PreviousAmountBatch - k.Quantity), k.PreviousAmountBatch)) as PreviousAmountBatchCalculated, 
		case 
			when k.EntityName = 'ConsignmentInventoryRemission' then 'Remision de Inventario en Consignación' 
			when k.EntityName = 'RemissionEntrance' then 'Remision de Entrada'
			when k.EntityName = 'RemissionOutput' then 'Remision de Salida'
			when k.EntityName = 'RemissionDevolution' then 'Devolucion de Remision'
			when k.EntityName = 'EntranceVoucher' then 'Comprobante de Entrada'
			when k.EntityName = 'PharmaceuticalDispensing' then 'Dispensación Farmacéutica'
			when k.EntityName = 'EntranceVoucherDevolution' then 'Devolución de Comprobante de Entrada'
			when k.EntityName = 'InventoryAdjustment' then 'Ajuste de Inventario'
			when k.EntityName = 'LoanMerchandise' then 'Préstamo de Mercancía'
			when k.EntityName = 'LoanMerchandiseDevolution' then 'Devolución de Préstamo de Mercancía'
			when k.EntityName = 'PharmaceuticalDispensingDevolution' then 'Devolución de Dispensación Farmacéutica'
			when k.EntityName = 'TransferOrder' then 'Orden de Traslado'
			when k.EntityName = 'TransferOrderDevolution' then 'Devolución de Orden de Traslado'
			when k.EntityName = 'DocumentInvoiceProductSales' then 'Documento Factura de venta de Producto'
			when k.EntityName = 'InventoryControl' then 'Control De Inventarios'
		end + ' - ' + k.EntityCode as DocumentDescription
FROM 
(
		SELECT	CONCAT('Kardex', k.Id) Id, k.DocumentDate, k.ProductId, k.WarehouseId, k.BatchSerialId,
				k.MovementType, k.Quantity, k.PreviousAmountProduct, k.PreviousAmountWarehouse, k.PreviousAmountBatch,
				k.Value, k.PreviousCost, k.PreviousAverageCost, k.AverageCost,
				k.EntityId, k.EntityCode, k.EntityName, k.ImportedEntityId, k.ImportedEntityCode, k.ImportedEntityName,
				k.CreationUser, k.CreationDate, k.AffectInventory
		FROM Inventory.Kardex k WITH (NOLOCK)
	UNION ALL
		SELECT	CONCAT('KardexControl', k.Id) Id, k.DocumentDate, k.ProductId, k.WarehouseId, k.BatchSerialId,
				k.MovementType, k.Quantity, k.PreviousAmountProduct, k.PreviousAmountWarehouse, k.PreviousAmountBatch,
				0 Value, 0 PreviousCost, 0 PreviousAverageCost, 0 AverageCost,
				k.EntityId, k.EntityCode, k.EntityName, 0 ImportedEntityId, '' ImportedEntityCode, '' ImportedEntityName,
				k.CreationUser, k.CreationDate, 1 AffectInventory
		FROM Inventory.KardexControl k WITH (NOLOCK)
) k
JOIN Inventory.Warehouse w  WITH (NOLOCK)ON W.Id = K.WarehouseId
JOIN Inventory.InventoryProduct p WITH (NOLOCK) ON p.Id = k.ProductId
LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON Bs.Id = k.BatchSerialId
LEFT JOIN Inventory.PharmaceuticalDispensing pd WITH (NOLOCK) on pd.Id = k.EntityId and k.EntityName = 'PharmaceuticalDispensing'
LEFT JOIN dbo.ADINGRESO i  WITH (NOLOCK)on i.NUMINGRES = pd.AdmissionNumber
LEFT JOIN dbo.INPACIENT pa  WITH (NOLOCK) on pa.IPCODPACI = i.IPCODPACI
LEFT JOIN Inventory.PharmaceuticalDispensingDevolution pdd WITH (NOLOCK) on pdd.Id = k.EntityId and k.EntityName = 'PharmaceuticalDispensingDevolution'
LEFT JOIN dbo.ADINGRESO pddi  WITH (NOLOCK)on pddi.NUMINGRES = pdd.AdmissionNumber
LEFT JOIN dbo.INPACIENT pddpa  WITH (NOLOCK)on pddpa.IPCODPACI = pddi.IPCODPACI
LEFT JOIN Inventory.TransferOrder tor  WITH (NOLOCK)on tor.Id = k.EntityId  And k.EntityName = 'TransferOrder'
LEFT JOIN Common.ThirdParty tpto  WITH (NOLOCK) on tpto.Id = tor.ThirdPartyId 

LEFT JOIN [Security].[User] us on tor.ConfirmationUser = us.UserCode
LEFT JOIN [Security].Person sp on us.IdPerson = sp.Id

LEFT JOIN Inventory.EntranceVoucher ev  WITH (NOLOCK) on ev.Id = k.EntityId And k.EntityName = 'EntranceVoucher'
LEFT JOIN Common.Supplier spev WITH (NOLOCK) on spev.Id = ev.SupplierId  
LEFT JOIN 
(
	SELECT RE.Code, RED.ProductId, MAX(RED.SourceCode) SourceCode
	FROM Inventory.RemissionEntrance RE WITH (NOLOCK)
	LEFT JOIN Inventory.RemissionEntranceDetail RED WITH (NOLOCK) ON RE.Id = RED.RemissionEntranceId
	GROUP BY RE.Code, RED.ProductId
) RED ON k.EntityCode = RED.Code AND p.Id = RED.ProductId AND k.EntityName = 'RemissionEntrance'