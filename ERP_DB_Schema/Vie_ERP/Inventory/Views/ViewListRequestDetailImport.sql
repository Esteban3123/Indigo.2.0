CREATE VIEW [Inventory].[ViewListRequestDetailImport]
AS
	SELECT	ir.Code,
			ir.DocumentDate,
			1 As InventoryRequestDetailType,
			ird.Id As Row,
			3 AS ComponentType, 					
			'Producto' As ComponentTypeName,
			ip.Id As EntityId,
			ip.Code As SourceCode,
			ip.Code + ' - ' + ip.Name As SourceCodeName,
			ird.Quantity AS QuantityRequested,
			0 AS QuantityDelivered,
			ird.OutstandingQuantity AS OutstandingQuantity, 
			ir.Observation As DescriptionProduct,
			ir.TargetWarehouseId,
			ir.TargetFunctionalUnitId,
			wh.Id AS SourceWarehouseId,
			ISNULL(ip.CodeCUM,'-') As CUMSourceCodeName,
			ird.Status
	FROM Inventory.InventoryRequest ir WITH (NOLOCK)
	-----------------------
	JOIN Inventory.InventoryRequestDetail ird  WITH (NOLOCK) ON  ird.InventoryRequestId = ir.Id
	LEFT JOIN  Inventory.InventoryProduct ip  WITH (NOLOCK) ON ip.Id = ird.InventoryProductId
	LEFT JOIN  Inventory.Warehouse wh WITH (NOLOCK) ON wh.Id = ir.SourceWarehouseId

	UNION ALL

		SELECT	
			ir.Code,	
			ir.DocumentDate,
			2 As InventoryRequestDetailType,
			irdo.Id As Row,
			irdo.ComponentType, 			
			CASE irdo.ComponentType 
				WHEN 1 THEN 'Medicamento'
				WHEN 2 THEN 'Insumo'
				WHEN 3 THEN 'Producto'
			END As ComponentTypeName,
			CASE irdo.ComponentType 
				WHEN 1 THEN atc.Id
				WHEN 2 THEN isu.Id
				WHEN 3 THEN ip.Id
			END As EntityId,
			CASE irdo.ComponentType 
				WHEN 1 THEN	 ATC.Code
				WHEN 2 THEN	 isu.Code
				WHEN 3 THEN	 ip.Code
			END As SourceCode,
			CASE irdo.ComponentType 
				WHEN 1 THEN	 ATC.Code + ' - ' + atc.Name
				WHEN 2 THEN	 isu.Code + ' - ' + isu.SupplieName
				WHEN 3 THEN	 ip.Code + ' - ' + ip.Name
			END As SourceCodeName,
			irdo.Quantity AS QuantityRequested,
			0 AS QuantityDelivered,
			irdo.OutstandingQuantity AS OutstandingQuantity,
			ir.Observation As DescriptionProduct,
			ir.TargetWarehouseId,
			ir.TargetFunctionalUnitId,
			wh.Id AS SourceWarehouseId,
			ISNULL(ip.CodeCUM,'-') As CUMSourceCodeName,
			irdo.Status
	FROM Inventory.InventoryRequest ir WITH (NOLOCK)
	-----------------------
	JOIN Inventory.InventoryRequestDetailOther irdo  WITH (NOLOCK) ON  irdo.InventoryRequestId = ir.Id
	LEFT JOIN  Inventory.ATC atc  WITH (NOLOCK) ON atc.Id = irdo.ATCId
	LEFT JOIN  Inventory.InventorySupplie isu  WITH (NOLOCK) ON isu.Id = irdo.SupplieId
	LEFT JOIN  Inventory.InventoryProduct ip  WITH (NOLOCK) ON ip.Id = irdo.InventoryProductId
	LEFT JOIN  Inventory.Warehouse wh WITH (NOLOCK) ON wh.Id = ir.SourceWarehouseId