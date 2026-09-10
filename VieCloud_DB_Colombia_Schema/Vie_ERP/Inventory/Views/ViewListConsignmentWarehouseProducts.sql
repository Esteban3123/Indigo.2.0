

CREATE VIEW [Inventory].[ViewListConsignmentWarehouseProducts]
AS

SELECT 
    w.Id WarehouseId,
	p.Id ProductId,
	Concat(p.Code, ' - ', p.Name) CodeName, 
	sub.HandlesBatch,
	 SUM(cird.Quantity) - SUM(cirdbs.DecreaseQuantity) QuantityMax,
	(k.QuantityEntrance - k.QuantityOut) QuantityKardex,
	0 DecreaseQuantity,
	'' Justificaton
FROM Inventory.ConsignmentInventoryRemission cir
JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
JOIN Inventory.Warehouse w ON w.Id = cir.WarehouseId
JOIN Inventory.InventoryProduct p ON p.Id = cird.ProductId
JOIN Inventory.ProductSubGroup sub ON sub.Id = p.ProductSubGroupId
LEFT JOIN 
(
	SELECT 		
		SUM(IIF(MovementType = 1, Quantity, 0)) QuantityEntrance,
		SUM(IIF(MovementType = 2, Quantity, 0)) QuantityOut,
		ProductId, 
		WarehouseId		
	FROM Inventory.Kardex 
	GROUP BY ProductId, WarehouseId
) k ON	
	K.ProductId = p.Id AND 
	k.WarehouseId = w.Id 
WHERE cir.MovementType IN (1, 2)
GROUP BY w.Id, p.Id, p.Code, p.Name, k.QuantityEntrance, k.QuantityOut, HandlesBatch
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de productos en consignación disponibles por bodega, con sus saldos actuales. Cruza las remisiones de consignación (entradas de mercancía enviada por proveedores) con el detalle de productos y lotes/seriales para calcular la cantidad máxima disponible (total remisionado menos lo ya consumido o decrementado). Complementa este saldo con el movimiento real del kardex (entradas menos salidas) para mostrar el inventario vigente de cada producto en cada almacén. Incluye el código y nombre del producto, si maneja lotes/vencimiento, y campos base para registrar consumos y justificaciones, siendo útil para consultar el stock de consignación disponible en cada bodega antes de legalizar o devolver mercancía al proveedor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListConsignmentWarehouseProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListConsignmentWarehouseProducts';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista, por bodega y producto en consignación, la cantidad máxima disponible (remitida menos disminuida) y el saldo en kardex, para soportar operaciones de salida o ajuste sobre inventario consignado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir remisiones de consignación con detalle y lotes/seriales asociados.; El producto debe estar vinculado a un subgrupo que defina si maneja lote (HandlesBatch).; Solo se consideran remisiones cuyo MovementType sea 1 o 2.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'QuantityMax representa lo remitido neto de las disminuciones registradas en lotes/seriales.; QuantityKardex se calcula como entradas (tipo 1) menos salidas (tipo 2) por producto y bodega.; DecreaseQuantity y Justificaton se exponen siempre como 0 y cadena vacía respectivamente (valores por defecto para edición posterior).; Solo se exponen productos que tengan al menos una remisión de consignación con detalle y lote/serial.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario en consignación; Remisión de inventario; Bodega/almacén; Producto; Lote y serial; Kardex (entradas/salidas); Subgrupo de producto; Manejo por lote', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas agrupadas por bodega y producto con QuantityMax = SUM(cird.Quantity) - SUM(cirdbs.DecreaseQuantity) y QuantityKardex = QuantityEntrance - QuantityOut.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Kardex.MovementType = 1 → La cantidad se acumula como QuantityEntrance (entrada). else Si MovementType = 2 se acumula como QuantityOut (salida); otros valores no suman.; si ConsignmentInventoryRemission.MovementType IN (1,2) → La remisión se incluye en el cálculo de QuantityMax. else Cualquier otro MovementType de remisión se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ConsignmentInventoryRemission; Inventory.ConsignmentInventoryRemissionDetail; Inventory.ConsignmentInventoryRemissionDetailBatchSerial; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ProductSubGroup; Inventory.Kardex', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListConsignmentWarehouseProducts';
GO
