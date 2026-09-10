CREATE VIEW   Inventory.ViewClosedMonthValuedInventory as 
SELECT 
	cmi.Id,
	cm.Year,
	cm.Month,
    w.Code + ' - ' + w.Name AS CodeNameWarehouse,
    ip.Code AS CodeProduct,
    ip.Name AS NameProduct,
    cmi.Quantity,
    cmi.FinalProductCost,
    cmi.Quantity * cmi.FinalProductCost AS TotalProductCost
FROM 
    Inventory.ClosedMonthInventory AS cmi
INNER JOIN  
    Inventory.ClosedMonth AS cm ON cm.Id = cmi.ClosedMonthId
LEFT JOIN 
    Inventory.Warehouse AS w ON w.Id = cmi.WareHouseId
LEFT JOIN 
    Inventory.InventoryProduct AS ip ON ip.Id = cmi.ProductId;

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el inventario valorizado de los meses cerrados, mostrando por producto y bodega la cantidad, costo unitario final y costo total al cierre.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthValuedInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir cierres mensuales registrados en Inventory.ClosedMonth con detalle en Inventory.ClosedMonthInventory.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthValuedInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El costo total expuesto siempre es el producto de la cantidad por el costo final del producto al cierre.; La bodega y el producto se muestran aunque falten en sus catálogos (LEFT JOIN), preservando el detalle del cierre.; Solo se exponen registros asociados a un mes cerrado existente (INNER JOIN con ClosedMonth).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthValuedInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario valorizado; Cierre mensual de inventario; Bodega/almacén; Producto de inventario; Costo final del producto', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthValuedInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ClosedMonthInventory: Retorna una fila por cada registro de inventario de mes cerrado, calculando TotalProductCost = Quantity * FinalProductCost.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthValuedInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ClosedMonthInventory; Inventory.ClosedMonth; Inventory.Warehouse; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthValuedInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthValuedInventory';
GO
