
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista unificada del detalle de solicitudes de inventario (requisiciones), que consolida en un único resultado todos los ítems pedidos sin importar su tipo: productos de inventario (renglones estándar) y componentes adicionales como medicamentos por clasificación ATC, insumos o productos de otras solicitudes. Para cada renglón expone el código y fecha de la solicitud, la bodega o unidad funcional destino, el tipo de componente (Medicamento, Insumo o Producto), la descripción del ítem solicitado, las cantidades pedidas y las cantidades pendientes de despacho, junto con el estado del renglón. Se utiliza para consultar y reportar el detalle completo de pedidos de traslado o despacho de insumos, medicamentos y suministros entre bodegas y unidades funcionales del hospital.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListRequestDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListRequestDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en un único listado el detalle de las solicitudes de inventario, combinando los ítems de productos estándar con los ítems "otros" (medicamentos ATC, insumos y productos), normalizando su descripción y tipo de componente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Inventory.InventoryRequest relacionados por Id con sus detalles; Los detalles ''Other'' deben tener un ComponentType en {1,2,3} para que se resuelva su nombre e ítem', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los detalles provenientes de InventoryRequestDetail siempre se clasifican como ComponentType=3 (''Producto''); ViewKey es único por origen: prefijo ''InventoryRequestDetail-'' para EntitySource=1 e ''InventoryRequestDetailOther-'' para EntitySource=2; EntitySource=1 corresponde al detalle estándar y EntitySource=2 al detalle ''Other''; ItemDescription se construye siempre como ''Code - Nombre'' del catálogo correspondiente; Las cabeceras de solicitud (Code, DocumentDate, TargetWarehouseId, TargetFunctionalUnitId, Observation) se replican desde InventoryRequest en cada línea; Solo se incluyen detalles cuya solicitud exista en InventoryRequest (JOIN interno); los catálogos de ítems se resuelven con LEFT JOIN, permitiendo descripciones nulas si no hay coincidencia', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de inventario; Detalle de solicitud; Producto; Medicamento (ATC); Insumo; Almacén destino; Unidad funcional destino; Cantidad solicitada; Cantidad pendiente', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve la unión (UNION ALL) de los detalles de InventoryRequestDetail (EntitySource=1, ComponentType fijo=3 ''Producto'') con los de InventoryRequestDetailOther (EntitySource=2, ComponentType dinámico)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si irdo.ComponentType = 1 → El ítem se interpreta como Medicamento; ItemId e ItemDescription se obtienen de Inventory.ATC; si irdo.ComponentType = 2 → El ítem se interpreta como Insumo; ItemId e ItemDescription se obtienen de Inventory.InventorySupplie; si irdo.ComponentType = 3 → El ítem se interpreta como Producto; ItemId e ItemDescription se obtienen de Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryRequest; Inventory.InventoryRequestDetail; Inventory.InventoryProduct; Inventory.InventoryRequestDetailOther; Inventory.ATC; Inventory.InventorySupplie', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListRequestDetail';
GO
