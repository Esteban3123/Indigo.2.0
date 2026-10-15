

CREATE view [MixingStation].[ViewListPreferenceWarehouse] 
as 

select 
	phy.Id,
	atc.Id AtcId,
	isnull(phy.Quantity,0) as ExistenceQuantity,
	atc.Code, 
	atc.Name,
	bs.Id BatchSerialId,
	bs.ExpirationDate,
	E.Id WarehouseId,
	E.Code WarehouseCode
	from  Inventory.ATC atc
	JOIN Inventory.InventoryProduct B on B.Status = 1 AND B.ATCId = atc.Id
	JOIN Inventory.PhysicalInventory phy on phy.ProductId = B.Id AND phy.Quantity > 0 
	JOIN Inventory.BatchSerial bs ON bs.Id = phy.BatchSerialId
	JOIN Inventory.Warehouse AS E ON phy.WarehouseId = E.Id