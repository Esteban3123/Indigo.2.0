



CREATE VIEW [Inventory].[ViewListConsignmentWarehouseProducts]
AS

SELECT 
    w.Id WarehouseId,
	p.Id ProductId,
	Concat(p.Code, ' - ', p.Name) CodeName, 
	sub.HandlesBatch,
	 SUM(cird.Quantity) - SUM(cirdbs.DecreaseQuantity) QuantityMax,
	(k.QuantityEntrance - k.QuantityOut) QuantityKardex,
	0 DecreaseQuantity,
	'' Justificaton
FROM Inventory.ConsignmentInventoryRemission cir
JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
JOIN Inventory.Warehouse w ON w.Id = cir.WarehouseId
JOIN Inventory.InventoryProduct p ON p.Id = cird.ProductId
JOIN Inventory.ProductSubGroup sub ON sub.Id = p.ProductSubGroupId
LEFT JOIN 
(
	SELECT 		
		SUM(IIF(MovementType = 1, Quantity, 0)) QuantityEntrance,
		SUM(IIF(MovementType = 2, Quantity, 0)) QuantityOut,
		ProductId, 
		WarehouseId		
	FROM Inventory.Kardex 
	GROUP BY ProductId, WarehouseId
) k ON	
	K.ProductId = p.Id AND 
	k.WarehouseId = w.Id 
WHERE cir.MovementType IN (1, 2)
GROUP BY w.Id, p.Id, p.Code, p.Name, k.QuantityEntrance, k.QuantityOut, HandlesBatch