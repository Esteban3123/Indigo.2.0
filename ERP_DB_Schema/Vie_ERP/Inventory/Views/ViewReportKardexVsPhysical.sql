
CREATE VIEW [Inventory].[ViewReportKardexVsPhysical]
AS
SELECT 
	CONCAT(w.Id, '-', ip.Id) Id,
	w.Id as WarehouseId, 
	w.Code as WarehouseCode, 
	w.Name as WarehouseName,
	ip.Id ProductId,
	ip.Code as ProductCode, 
	ip.Name as ProductName, 
	SUM(phy_k.PhysicalQuantity) as PhysicalQuantity, 
	SUM(phy_k.KardexQuantity) as KardexQuantity
FROM Inventory.InventoryProduct ip WITH (NOLOCK)
JOIN
(
	SELECT phy.ProductId, phy.WarehouseId, SUM(phy.Quantity) PhysicalQuantity, 0 KardexQuantity
	FROM Inventory.PhysicalInventory phy WITH (NOLOCK)
	GROUP BY phy.ProductId, phy.WarehouseId

	UNION ALL

	SELECT
		k.ProductId,
		k.WarehouseId,
		0 PhysicalQuantity,
		SUM(k.Quantity * IIF(k.MovementType = 1, 1, -1)) KardexQuantity
	FROM Inventory.Kardex k WITH (NOLOCK)
	WHERE k.AffectInventory = 1
	GROUP BY k.ProductId, k.WarehouseId

	UNION ALL

	SELECT
		k.ProductId,
		k.WarehouseId,
		0 PhysicalQuantity,
		SUM(k.Quantity * IIF(k.MovementType = 1, 1, -1)) KardexQuantity
	FROM Inventory.KardexControl k WITH (NOLOCK)
	GROUP BY k.ProductId, k.WarehouseId
) phy_k ON ip.Id = phy_k.ProductId
JOIN Inventory.Warehouse w WITH (NOLOCK) ON phy_k.WarehouseId = w.Id
GROUP BY 
	w.Id, w.Code, w.Name,
	ip.Id, ip.Code, ip.Name
HAVING SUM(phy_k.PhysicalQuantity) <> SUM(phy_k.KardexQuantity)