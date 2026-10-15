

CREATE VIEW [Inventory].[ViewProductHierarchy]
AS

WITH CTE AS (
  SELECT 
	PH.Id, 
	PH.HierarchyProductFinalId, 
	PH.ParentProductId, 
	PH.ProductId, 
	IP.Name, 
	LOG(PH.ConversionUnit) AS LogConversionUnit
  FROM Inventory.ProductHierarchy PH
  JOIN Inventory.InventoryProduct IP ON IP.Id = PH.ParentProductId
)
SELECT 
	CTE.Id, 
	CTE.HierarchyProductFinalId,
	CTE.ParentProductId, 
	CTE.ProductId, 
	CTE.Name,
   EXP(SUM(CTE.LogConversionUnit) OVER (PARTITION BY CTE.HierarchyProductFinalId ORDER BY CTE.Id)) AS Quantity
FROM CTE