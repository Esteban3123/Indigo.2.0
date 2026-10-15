CREATE VIEW [Inventory].[ViewPurchaseRequestToOrder]
AS
SELECT	prd.Id, 
		pr.Id PurchaseRequestId, 
		pr.Code PurchaseRequestCode, 		
		prd.InventoryProductId, 
		ip.Code ProductCode,
		ip.Name ProductName, 
		prd.OutstandingQuantity,		
		pr.CreationDate
FROM Inventory.PurchaseRequest pr
JOIN Inventory.PurchaseRequestDetail prd ON pr.Id = prd.PurchaseRequestId
JOIN Inventory.InventoryProduct ip ON prd.InventoryProductId = ip.Id
WHERE pr.Status = 2 AND pr.RequestTypeId = 1
	AND prd.Status = 1 AND prd.OutstandingQuantity > 0