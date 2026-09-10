
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
			ird.Status,
			ir.MovementType
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
			irdo.Status,
			ir.MovementType
	FROM Inventory.InventoryRequest ir WITH (NOLOCK)
	-----------------------
	JOIN Inventory.InventoryRequestDetailOther irdo  WITH (NOLOCK) ON  irdo.InventoryRequestId = ir.Id
	LEFT JOIN  Inventory.ATC atc  WITH (NOLOCK) ON atc.Id = irdo.ATCId
	LEFT JOIN  Inventory.InventorySupplie isu  WITH (NOLOCK) ON isu.Id = irdo.SupplieId
	LEFT JOIN  Inventory.InventoryProduct ip  WITH (NOLOCK) ON ip.Id = irdo.InventoryProductId
	LEFT JOIN  Inventory.Warehouse wh WITH (NOLOCK) ON wh.Id = ir.SourceWarehouseId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle completo de solicitudes de inventario (requisiciones) para procesos de importación o carga masiva, uniendo dos tipos de ítems: productos directos del catálogo de inventario y componentes adicionales que pueden ser medicamentos clasificados por código ATC, insumos o productos. Para cada renglón de solicitud expone el código y fecha del documento, el tipo de componente solicitado (medicamento, insumo o producto), el artículo con su código y nombre, las cantidades solicitadas y pendientes de despacho, la bodega de origen, la bodega o unidad funcional destino, el código CUM del producto cuando aplica, el estado del renglón y el tipo de movimiento. Sirve como fuente unificada para reportería de requisiciones, trazabilidad de pedidos entre bodegas y unidades funcionales, y validación de cantidades despachadas versus solicitadas en la gestión de almacenes y farmacia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListRequestDetailImport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListRequestDetailImport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica los detalles de solicitudes de inventario (productos y otros componentes como medicamentos ATC, insumos y productos) en una vista única para importación/listado, normalizando códigos, nombres y cantidades pendientes.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'QuantityDelivered siempre se expone como 0 (la vista no calcula entregas reales).; CUMSourceCodeName usa ISNULL(ip.CodeCUM,''-''); cuando no hay producto asociado retorna ''-''.; En el primer bloque el ComponentType es siempre 3 (''Producto'').; InventoryRequestDetailType=1 corresponde a detalle estándar; =2 corresponde a ''otros'' componentes.; La OutstandingQuantity se toma directamente del detalle origen sin recálculo.; Todas las consultas usan WITH (NOLOCK), permitiendo lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de inventario; Detalle de requisición; Producto; Medicamento (ATC); Insumo; Bodega/Almacén origen y destino; Unidad funcional destino; Cantidad solicitada; Cantidad pendiente; Código CUM; Tipo de movimiento', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve dos conjuntos unidos (UNION ALL): InventoryRequestDetailType=1 desde InventoryRequestDetail y InventoryRequestDetailType=2 desde InventoryRequestDetailOther.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si irdo.ComponentType = 1 → Se trata como ''Medicamento''; EntityId/SourceCode/SourceCodeName se toman desde ATC; si irdo.ComponentType = 2 → Se trata como ''Insumo''; EntityId/SourceCode/SourceCodeName se toman desde InventorySupplie (Code + SupplieName); si irdo.ComponentType = 3 → Se trata como ''Producto''; EntityId/SourceCode/SourceCodeName se toman desde InventoryProduct (Code + Name); si Origen InventoryRequestDetail (primer SELECT) → Se fija ComponentType=3 (''Producto'') e InventoryRequestDetailType=1, ligando siempre a InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryRequest; Inventory.InventoryRequestDetail; Inventory.InventoryProduct; Inventory.Warehouse; Inventory.InventoryRequestDetailOther; Inventory.ATC; Inventory.InventorySupplie', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailImport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetailImport';
GO
