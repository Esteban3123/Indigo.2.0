

CREATE view [MixingStation].[ViewListWarehouseTypes] 
as 
	SELECT c.id Id,
		   c.IdMixingStation,
		   C.WarehouseType,
		   w.Code, 
		   w.Name,
		   CONCAT(W.Code,' - ', w.Name) As CodeName,
		   w.Id as Id_Warehouse,
		   Case  C.WarehouseType
				when 1 then 'Materia Prima Stock'
				when 2 then 'Almacén'
				when 3 then 'En Proceso'
				when 4 then 'Inventario de Control'
				else 'Sin asignar'
		   end WarehouseTypeName
	FROM MixingStation.CMWarehouse C WITH(NOLOCK)
	INNER JOIN Inventory.Warehouse w WITH(NOLOCK) ON c.IdWarehouse = w.id
	INNER JOIN MixingStation.CMConfiguration s WITH(NOLOCK) ON s.id = c.IdMixingStation
	WHERE C.StateWH = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las bodegas o almacenes asociados a las estaciones de mezcla (MixingStation) que están activas, clasificadas por su tipo de uso dentro del proceso de preparación de medicamentos o fórmulas. Combina la configuración de bodegas de la estación de mezcla (CMWarehouse) con el catálogo general de almacenes del inventario (Inventory.Warehouse), mostrando el código, nombre y una etiqueta descriptiva del tipo de bodega: Materia Prima Stock, Almacén, En Proceso, Inventario de Control o Sin asignar. Solo incluye bodegas en estado activo (StateWH = 1). Sirve para que los módulos de mezcla y dispensación identifiquen rápidamente qué almacenes están habilitados para cada etapa del proceso productivo en cada estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListWarehouseTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListWarehouseTypes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los almacenes activos asociados a cada estación de mezcla, con su tipo de bodega traducido a etiqueta legible.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListWarehouseTypes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en CMWarehouse con StateWH = 1 (almacenes activos); Cada CMWarehouse referencia un Warehouse válido en Inventory.Warehouse; Cada CMWarehouse referencia una configuración existente en CMConfiguration', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListWarehouseTypes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los tipos de almacén se mapean a un dominio fijo de 4 categorías (1..4); cualquier otro valor se rotula como ''Sin asignar''; Solo expone almacenes activos (StateWH = 1); Cada fila vincula obligatoriamente una estación de mezcla, una bodega de inventario y una configuración de estación (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListWarehouseTypes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezcla (Mixing Station); Almacén / bodega; Materia prima; Inventario de control; Almacén en proceso; Configuración de estación', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListWarehouseTypes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.CMWarehouse: Solo se exponen filas donde StateWH = 1 (excluye almacenes inactivos); [RETURN_RESULT] Inventory.Warehouse: Devuelve campo CodeName concatenando Code y Name con separador '' - ''', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListWarehouseTypes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.WarehouseType = 1 → WarehouseTypeName = ''Materia Prima Stock''; si C.WarehouseType = 2 → WarehouseTypeName = ''Almacén''; si C.WarehouseType = 3 → WarehouseTypeName = ''En Proceso''; si C.WarehouseType = 4 → WarehouseTypeName = ''Inventario de Control'' else WarehouseTypeName = ''Sin asignar''', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListWarehouseTypes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CMWarehouse; Inventory.Warehouse; MixingStation.CMConfiguration', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListWarehouseTypes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListWarehouseTypes';
GO
