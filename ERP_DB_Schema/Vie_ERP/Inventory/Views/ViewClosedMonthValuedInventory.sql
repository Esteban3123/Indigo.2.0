CREATE VIEW   Inventory.ViewClosedMonthValuedInventory as 
SELECT 
	cmi.Id,
	cm.Year,
	cm.Month,
    w.Code + ' - ' + w.Name AS CodeNameWarehouse,
    ip.Code AS CodeProduct,
    ip.Name AS NameProduct,
    cmi.Quantity,
    cmi.FinalProductCost,
    cmi.Quantity * cmi.FinalProductCost AS TotalProductCost
FROM 
    Inventory.ClosedMonthInventory AS cmi
INNER JOIN  
    Inventory.ClosedMonth AS cm ON cm.Id = cmi.ClosedMonthId
LEFT JOIN 
    Inventory.Warehouse AS w ON w.Id = cmi.WareHouseId
LEFT JOIN 
    Inventory.InventoryProduct AS ip ON ip.Id = cmi.ProductId;