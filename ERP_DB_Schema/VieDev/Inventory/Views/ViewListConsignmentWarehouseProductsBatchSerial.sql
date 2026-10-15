


CREATE VIEW [Inventory].[ViewListConsignmentWarehouseProductsBatchSerial]
AS

SELECT 
   w.id WarehouseId,
    ser.BatchCode BatchCode,
	p.Id ProductId,
	p.Code Code, 
	cirdbs.BatchSerialId BatchSerialId,	
	k.QuantityEntrance - k.QuantityOut QuantityKardex,
	k.QuantityOut QuantityDecrease
FROM Inventory.ConsignmentInventoryRemission cir
JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
JOIN Inventory.Warehouse w ON w.Id = cir.WarehouseId
JOIN Inventory.InventoryProduct p ON p.Id = cird.ProductId
join Inventory.BatchSerial ser on cirdbs.BatchSerialId = ser.Id
LEFT JOIN 
(
	SELECT 
		SUM(IIF(MovementType = 1, Quantity, 0)) QuantityEntrance,
		SUM(IIF(MovementType = 2, Quantity, 0)) QuantityOut,
		ProductId, 
		WarehouseId,
		BatchSerialId
	FROM Inventory.Kardex 
	GROUP BY ProductId, WarehouseId, BatchSerialId
) k ON	
	K.ProductId = p.Id AND 
	k.WarehouseId = w.Id AND
	ISNULL(k.BatchSerialId, 0) = ISNULL(cirdbs.BatchSerialId, 0)
--
WHERE cir.MovementType IN (1, 2)
    
GROUP BY w.id, ser.BatchCode, p.Id, p.Code, cirdbs.BatchSerialId, k.QuantityEntrance, k.QuantityOut