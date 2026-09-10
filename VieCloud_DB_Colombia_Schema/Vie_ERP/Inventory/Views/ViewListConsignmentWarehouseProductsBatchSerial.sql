

CREATE VIEW [Inventory].[ViewListConsignmentWarehouseProductsBatchSerial]
AS

SELECT 
   w.id WarehouseId,
    ser.BatchCode BatchCode,
	p.Id ProductId,
	p.Code Code, 
	cirdbs.BatchSerialId BatchSerialId,	
	k.QuantityEntrance - k.QuantityOut QuantityKardex,
	k.QuantityOut QuantityDecrease
FROM Inventory.ConsignmentInventoryRemission cir
JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
JOIN Inventory.Warehouse w ON w.Id = cir.WarehouseId
JOIN Inventory.InventoryProduct p ON p.Id = cird.ProductId
join Inventory.BatchSerial ser on cirdbs.BatchSerialId = ser.Id
LEFT JOIN 
(
	SELECT 
		SUM(IIF(MovementType = 1, Quantity, 0)) QuantityEntrance,
		SUM(IIF(MovementType = 2, Quantity, 0)) QuantityOut,
		ProductId, 
		WarehouseId,
		BatchSerialId
	FROM Inventory.Kardex 
	GROUP BY ProductId, WarehouseId, BatchSerialId
) k ON	
	K.ProductId = p.Id AND 
	k.WarehouseId = w.Id AND
	ISNULL(k.BatchSerialId, 0) = ISNULL(cirdbs.BatchSerialId, 0)
--
WHERE cir.MovementType IN (1, 2)
    
GROUP BY w.id, ser.BatchCode, p.Id, p.Code, cirdbs.BatchSerialId, k.QuantityEntrance, k.QuantityOut
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los productos en consignación por bodega, mostrando para cada combinación de almacén, producto y lote/serial: el código del lote, el identificador del producto, su código interno y las cantidades disponibles según el kardex (entradas menos salidas) junto con las cantidades despachadas o consumidas. Integra las remisiones de consignación con su detalle de productos y lotes/seriales, cruza contra el kardex de movimientos para calcular el saldo real por lote en cada bodega, y se limita a remisiones de entrada y salida (tipos 1 y 2). Sirve para consultar el inventario disponible en consignación por bodega y lote, útil en procesos de legalización, devolución y control de stock de mercancía consignada por proveedores.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListConsignmentWarehouseProductsBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListConsignmentWarehouseProductsBatchSerial';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos con lote/serial existentes en bodegas de consignación, mostrando cantidad neta en kardex (entradas menos salidas) y cantidad de salidas por combinación bodega-producto-lote.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProductsBatchSerial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen remisiones de inventario en consignación con MovementType 1 o 2; Cada remisión tiene detalle con productos y lotes/seriales asociados; El kardex registra movimientos clasificados como entrada (MovementType=1) o salida (MovementType=2)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProductsBatchSerial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'QuantityKardex se calcula como entradas menos salidas (MovementType=1 - MovementType=2) agrupadas por producto, bodega y lote/serial; El emparejamiento con kardex usa ISNULL(BatchSerialId,0) para tratar lotes nulos como equivalentes; Solo se consideran remisiones de consignación con MovementType 1 o 2; El LEFT JOIN al kardex permite mostrar productos de remisión aunque no tengan movimientos registrados', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProductsBatchSerial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario en consignación; Remisión de inventario; Bodega/almacén; Producto; Lote/Serial; Kardex; Movimientos de entrada y salida', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProductsBatchSerial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas solo cuando cir.MovementType IN (1,2); excluye otros tipos de movimiento de remisión', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProductsBatchSerial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Kardex.MovementType = 1 → La cantidad se acumula como QuantityEntrance (entradas) else Si MovementType = 2 se acumula como QuantityOut (salidas); si cir.MovementType IN (1,2) → La remisión se incluye en el resultado else Se excluye del listado', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProductsBatchSerial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ConsignmentInventoryRemission; Inventory.ConsignmentInventoryRemissionDetail; Inventory.ConsignmentInventoryRemissionDetailBatchSerial; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.BatchSerial; Inventory.Kardex', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProductsBatchSerial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProductsBatchSerial';
GO
