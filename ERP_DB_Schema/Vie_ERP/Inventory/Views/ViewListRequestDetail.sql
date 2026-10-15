
CREATE VIEW [Inventory].[ViewListRequestDetail]
AS
	SELECT	CONCAT('InventoryRequestDetail-', ird.Id) ViewKey,
			1 EntitySource,
			ird.Id,
			----------------------------------
			ir.Code,
			ir.DocumentDate,
			ir.TargetWarehouseId,
			ir.TargetFunctionalUnitId,
			ir.Observation,
			ird.Status,
			----------------------------------
			3 AS ComponentType, 					
			'Producto' As ComponentTypeName,
			ip.Id As ItemId,
			CONCAT(ip.Code, ' - ', ip.Name) ItemDescription,
			ird.Quantity AS RequestQuantity,
			ird.OutstandingQuantity
	FROM Inventory.InventoryRequest ir WITH (NOLOCK)
	JOIN Inventory.InventoryRequestDetail ird WITH (NOLOCK) ON ird.InventoryRequestId = ir.Id
	LEFT JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ip.Id = ird.InventoryProductId

UNION ALL

	SELECT	CONCAT('InventoryRequestDetailOther-', irdo.Id) ViewKey,
			2 EntitySource,
			irdo.Id,
			----------------------------------
			ir.Code,
			ir.DocumentDate,
			ir.TargetWarehouseId,
			ir.TargetFunctionalUnitId,
			ir.Observation,
			irdo.Status,
			----------------------------------
			irdo.ComponentType, 					
			CASE irdo.ComponentType 
				WHEN 1 THEN 'Medicamento'
				WHEN 2 THEN 'Insumo'
				WHEN 3 THEN 'Producto'
			END ComponentTypeName,
			CASE irdo.ComponentType 
				WHEN 1 THEN atc.Id
				WHEN 2 THEN isu.Id
				WHEN 3 THEN ip.Id
			END As ItemId,
			CASE irdo.ComponentType 
				WHEN 1 THEN	CONCAT(atc.Code, ' - ', atc.Name)
				WHEN 2 THEN	CONCAT(isu.Code, ' - ', isu.SupplieName)
				WHEN 3 THEN	CONCAT(ip.Code, ' - ', ip.Name)
			END ItemDescription,
			irdo.Quantity AS RequestQuantity,
			irdo.OutstandingQuantity
	FROM Inventory.InventoryRequest ir WITH (NOLOCK)
	JOIN Inventory.InventoryRequestDetailOther irdo  WITH (NOLOCK) ON  irdo.InventoryRequestId = ir.Id
	LEFT JOIN  Inventory.ATC atc  WITH (NOLOCK) ON atc.Id = irdo.ATCId
	LEFT JOIN  Inventory.InventorySupplie isu  WITH (NOLOCK) ON isu.Id = irdo.SupplieId
	LEFT JOIN  Inventory.InventoryProduct ip  WITH (NOLOCK) ON ip.Id = irdo.InventoryProductId