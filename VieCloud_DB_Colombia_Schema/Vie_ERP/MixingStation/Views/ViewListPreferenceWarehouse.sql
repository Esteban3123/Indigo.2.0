

CREATE view [MixingStation].[ViewListPreferenceWarehouse] 
as 

select 
	phy.Id,
	atc.Id AtcId,
	isnull(phy.Quantity,0) as ExistenceQuantity,
	atc.Code, 
	atc.Name,
	bs.Id BatchSerialId,
	bs.ExpirationDate,
	E.Id WarehouseId,
	E.Code WarehouseCode
	from  Inventory.ATC atc
	JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = atc.Id
	JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id AND phy.Quantity > 0 
	JOIN Inventory.BatchSerial bs ON bs.Id = phy.BatchSerialId
	JOIN Inventory.Warehouse AS E ON phy.WarehouseId = E.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de medicamentos e insumos disponibles en bodega para la estación de mezclas, mostrando únicamente productos activos con existencias físicas mayores a cero. Integra el catálogo ATC (código y nombre del medicamento), el inventario físico real por bodega, el lote o serial con su fecha de vencimiento, y la bodega donde se encuentra almacenado cada producto. Sirve para que el módulo de estación de mezclas consulte rápidamente qué medicamentos hay disponibles, en qué lote y en cuál bodega, facilitando la preparación y dispensación de mezclas farmacéuticas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListPreferenceWarehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListPreferenceWarehouse';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las existencias disponibles por producto, lote/serial y bodega, basándose en el catálogo ATC, para alimentar la selección de preferencias de bodega en la estación de mezclas.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPreferenceWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El producto debe estar vinculado a un código ATC vigente en Inventory.ATC.; Debe existir un registro en InventoryProduct con Status=1 enlazando ATC con el producto físico.; Debe existir lote/serial (BatchSerial) y bodega (Warehouse) asociados al inventario físico.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPreferenceWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen productos de inventario activos (InventoryProduct.Status = 1).; Solo se exponen registros con existencia positiva (PhysicalInventory.Quantity > 0).; Cada fila resultante está asociada obligatoriamente a un lote/serial (JOIN no opcional con BatchSerial) y a una bodega (JOIN con Warehouse).; Si la cantidad física es nula, se reporta como 0 (ISNULL sobre Quantity).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPreferenceWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'ATC (clasificación de medicamentos); producto de inventario; inventario físico; lote/serial; fecha de vencimiento; bodega/almacén; existencias', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPreferenceWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PhysicalInventory: Devuelve únicamente existencias con Quantity > 0 y producto activo (InventoryProduct.Status = 1), incluyendo ATC, lote/serial con fecha de vencimiento y bodega.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPreferenceWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.BatchSerial; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPreferenceWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListPreferenceWarehouse';
GO
