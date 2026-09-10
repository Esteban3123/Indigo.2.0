CREATE VIEW [Inventory].[ViewReportWarehousesProducts]
AS
SELECT ROW_NUMBER() OVER(ORDER BY w.Code ASC) as Row, w.Name, w.Code AS 'WarehouseIdCode'FROM Inventory.[PhysicalInventory] AS pin
inner join Inventory.InventoryProduct iip ON iip.Id =pin.ProductId
INNER JOIN Inventory.Warehouse AS w ON w.Id = pin.WarehouseId
GROUP BY w.Code, w.Name
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de bodegas o almacenes que tienen al menos un producto registrado en el inventario físico. Cruza el inventario físico con el catálogo de productos y el maestro de bodegas para obtener, sin duplicados, cada bodega activa con su código y nombre. Se usa en reportes de inventario para filtrar o visualizar qué bodegas participan en el conteo físico, numerando cada fila para facilitar la paginación o presentación tabular.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportWarehousesProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportWarehousesProducts';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las bodegas que tienen productos con inventario físico registrado, devolviendo su código y nombre numerados para reportes.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportWarehousesProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia referencial entre PhysicalInventory.ProductId e InventoryProduct.Id, y entre PhysicalInventory.WarehouseId y Warehouse.Id para que la bodega aparezca.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportWarehousesProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan bodegas que tienen al menos un registro de inventario físico asociado a un producto válido del catálogo (por el INNER JOIN con PhysicalInventory e InventoryProduct).; Cada bodega aparece una sola vez en el resultado por agrupación de código y nombre.; El resultado se numera secuencialmente ordenado ascendentemente por el código de bodega.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportWarehousesProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario físico; Bodega/Almacén; Producto de inventario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportWarehousesProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.Warehouse: Devuelve una fila por bodega distinta (código y nombre) que tenga al menos un registro en PhysicalInventory cuyo ProductId exista en InventoryProduct, agregando un número de fila ordenado por el código de la bodega.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportWarehousesProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PhysicalInventory; Inventory.InventoryProduct; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportWarehousesProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportWarehousesProducts';
GO
