CREATE VIEW [Cost].[InventoryConsumedByCostCenter]
AS
SELECT 
	k.Year, 
	k.Month, 
	k.ProductId, 
	k.CostCenterId
FROM
(
	SELECT 
		YEAR(k.DocumentDate) Year, 
		MONTH(k.DocumentDate) Month, 
		k.ProductId, 
		fu.CostCenterId
	FROM Inventory.Kardex k WITH (NOLOCK)
	JOIN Inventory.PharmaceuticalDispensingDetail pdd 
		ON k.EntityName = 'PharmaceuticalDispensing' 
			AND k.EntityId = pdd.PharmaceuticalDispensingId
			AND k.ProductId = pdd.ProductId
	JOIN Payroll.FunctionalUnit fu ON pdd.FunctionalUnitId = fu.Id
	WHERE pdd.Quantity > pdd.ReturnedQuantity
	GROUP BY YEAR(k.DocumentDate), MONTH(k.DocumentDate), k.ProductId, fu.CostCenterId

	UNION ALL

	SELECT 
		YEAR([to].DocumentDate) Year, 
		MONTH([to].DocumentDate) Month, 
		k.ProductId, 
		IIF([to].DispatchTo = 1, w.CostCenterId, fu.CostCenterId) CostCenterId
	FROM Inventory.Kardex k WITH (NOLOCK)
	JOIN Inventory.TransferOrder [to] 
		ON k.EntityName = 'TransferOrder' 
			AND k.EntityId = [to].Id
	JOIN Inventory.TransferOrderDetail tod 
		ON [to].Id = tod.TransferOrderId 
			AND k.ProductId = tod.ProductId
	JOIN Inventory.TransferOrderDetailBatchSerial todbs 
		ON tod.Id = todbs.TransferOrderDetailId
	LEFT JOIN Inventory.Warehouse w ON [to].TargetWarehouseId = w.Id
	LEFT JOIN Payroll.FunctionalUnit fu ON [to].TargetFunctionalUnitId = fu.Id
	WHERE [to].OrderType = 2
		AND todbs.OutstandingQuantity > 0
	GROUP BY YEAR([to].DocumentDate), MONTH([to].DocumentDate), k.ProductId, IIF([to].DispatchTo = 1, w.CostCenterId, fu.CostCenterId)
) k
GROUP BY k.Year, k.Month, k.ProductId, k.CostCenterId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de costos que consolida los productos de inventario consumidos por centro de costo, agrupados por año, mes y producto. Combina dos fuentes de consumo: las dispensaciones farmacéuticas (medicamentos entregados a pacientes y asociados a una unidad funcional) y los traslados internos de inventario entre bodegas o unidades funcionales (órdenes de transferencia de tipo consumo con cantidades pendientes por procesar). Cruza el kardex de movimientos con el detalle de dispensación farmacéutica, las órdenes de traslado y las unidades funcionales de nómina para determinar a qué centro de costo se imputa cada consumo. Sirve como base para reportes de costos por área, análisis de consumo de insumos y medicamentos por centro de costo, y distribución contable del gasto de inventario.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'InventoryConsumedByCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'InventoryConsumedByCostCenter';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida mensualmente el inventario consumido por centro de costo, combinando dispensaciones farmacéuticas efectivas y traslados de inventario tipo despacho con saldos pendientes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'InventoryConsumedByCostCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen movimientos en Kardex asociados a entidades ''PharmaceuticalDispensing'' o ''TransferOrder''; Las unidades funcionales y bodegas referenciadas deben existir para resolver el centro de costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'InventoryConsumedByCostCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen dispensaciones farmacéuticas con cantidad efectivamente consumida (Quantity > ReturnedQuantity), excluyendo lo devuelto; Solo se incluyen traslados de tipo OrderType=2 (despacho) y con saldo pendiente (OutstandingQuantity>0); La asignación del centro de costo en traslados depende del tipo de destino (DispatchTo): bodega vs. unidad funcional; La granularidad del resultado siempre es Año + Mes + Producto + Centro de Costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'InventoryConsumedByCostCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex de inventario; Dispensación farmacéutica; Devolución de medicamentos; Orden de traslado; Despacho de inventario; Lote/serial; Unidad funcional; Bodega; Centro de costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'InventoryConsumedByCostCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.InventoryConsumedByCostCenter: Devuelve agregación por año, mes, producto y centro de costo uniendo consumos por dispensación farmacéutica y por órdenes de traslado de tipo despacho (OrderType=2)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'InventoryConsumedByCostCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Movimiento Kardex con EntityName=''PharmaceuticalDispensing'' y Quantity > ReturnedQuantity en el detalle de dispensación → Se considera consumo y se asigna al CostCenterId de la unidad funcional (FunctionalUnit) asociada al detalle dispensado; si Movimiento Kardex con EntityName=''TransferOrder'', OrderType=2 y OutstandingQuantity>0 en el lote/serial → Se considera consumo por traslado; si DispatchTo=1 se asigna al CostCenterId de la bodega destino, en caso contrario al CostCenterId de la unidad funcional destino else Se asigna al CostCenterId de la TargetFunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'InventoryConsumedByCostCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.PharmaceuticalDispensingDetail; Payroll.FunctionalUnit; Inventory.TransferOrder; Inventory.TransferOrderDetail; Inventory.TransferOrderDetailBatchSerial; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'InventoryConsumedByCostCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'InventoryConsumedByCostCenter';
GO
