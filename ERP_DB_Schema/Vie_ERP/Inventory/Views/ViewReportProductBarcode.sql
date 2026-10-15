

CREATE VIEW [Inventory].[ViewReportProductBarcode]
AS
SELECT	CONCAT(phy.Id, '-') UUID,
			w.Id WarehouseId,
			CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName,
			ip.Id ProductId,
			ip.Code ProductCode,
			ip.Name ProducName,
			bs.Id BatchSerialId,
			bs.BatchCode,
			bs.ExpirationDate,
			pbc.Barcode,
			IIF(pbc.Barcode IS NULL,CONCAT(ip.Code,'*IND*', bs.BatchCode), pbc.Barcode) CalculatedBarCode,
			phy.Quantity
	FROM Inventory.PhysicalInventory phy
	JOIN Inventory.Warehouse w ON phy.WarehouseId = w.Id
	JOIN Inventory.InventoryProduct ip ON phy.ProductId = ip.Id
	LEFT JOIN Inventory.BatchSerial bs ON phy.BatchSerialId = bs.Id
	OUTER APPLY (
		Select top 1  pbcc.ProductId , pbcc.Barcode
		from Inventory.ProductBarcode pbcc
		where ip.Id = pbcc.ProductId
		ORDER by pbcc.CreationDate desc
	) as pbc