
CREATE VIEW [Inventory].[ViewReportKardexVsPhysical]
AS
SELECT 
	CONCAT(w.Id, '-', ip.Id) Id,
	w.Id as WarehouseId, 
	w.Code as WarehouseCode, 
	w.Name as WarehouseName,
	ip.Id ProductId,
	ip.Code as ProductCode, 
	ip.Name as ProductName, 
	SUM(phy_k.PhysicalQuantity) as PhysicalQuantity, 
	SUM(phy_k.KardexQuantity) as KardexQuantity
FROM Inventory.InventoryProduct ip WITH (NOLOCK)
JOIN
(
	SELECT phy.ProductId, phy.WarehouseId, SUM(phy.Quantity) PhysicalQuantity, 0 KardexQuantity
	FROM Inventory.PhysicalInventory phy WITH (NOLOCK)
	GROUP BY phy.ProductId, phy.WarehouseId

	UNION ALL

	SELECT
		k.ProductId,
		k.WarehouseId,
		0 PhysicalQuantity,
		SUM(k.Quantity * IIF(k.MovementType = 1, 1, -1)) KardexQuantity
	FROM Inventory.Kardex k WITH (NOLOCK)
	WHERE k.AffectInventory = 1
	GROUP BY k.ProductId, k.WarehouseId

	UNION ALL

	SELECT
		k.ProductId,
		k.WarehouseId,
		0 PhysicalQuantity,
		SUM(k.Quantity * IIF(k.MovementType = 1, 1, -1)) KardexQuantity
	FROM Inventory.KardexControl k WITH (NOLOCK)
	GROUP BY k.ProductId, k.WarehouseId
) phy_k ON ip.Id = phy_k.ProductId
JOIN Inventory.Warehouse w WITH (NOLOCK) ON phy_k.WarehouseId = w.Id
GROUP BY 
	w.Id, w.Code, w.Name,
	ip.Id, ip.Code, ip.Name
HAVING SUM(phy_k.PhysicalQuantity) <> SUM(phy_k.KardexQuantity)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de conciliación entre el inventario físico contado en bodega y el saldo teórico del kardex (movimientos registrados en sistema) por producto y bodega. Compara la cantidad real ingresada en el inventario físico contra la cantidad calculada sumando entradas y restando salidas del Kardex y KardexControl, filtrando únicamente los registros donde ambas cifras NO coinciden. Es la base del reporte de diferencias de inventario, utilizada para detectar faltantes, sobrantes o errores de registro de medicamentos e insumos en cada almacén o bodega.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportKardexVsPhysical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportKardexVsPhysical';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta, por bodega y producto, las diferencias entre el stock físico contado y el saldo teórico calculado a partir del kardex y kardex de control, listando solo los casos donde ambas cantidades no coinciden.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexVsPhysical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en InventoryProduct y Warehouse asociados a los movimientos.; Los movimientos de Kardex tienen MovementType definido (1 para sumar, otro valor para restar).; Para considerar movimientos de Kardex en el saldo teórico, deben tener AffectInventory = 1.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexVsPhysical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen combinaciones bodega-producto con discrepancia entre físico y kardex (HAVING SUM(physical) <> SUM(kardex)).; El saldo teórico combina movimientos de Kardex (filtrados por AffectInventory=1) y de KardexControl (sin filtro de afectación).; Las entradas (MovementType=1) suman y cualquier otro tipo resta al saldo teórico.; El identificador de fila se construye como WarehouseId-ProductId, garantizando unicidad por bodega/producto.; Solo aparecen productos que tengan al menos un registro en PhysicalInventory, Kardex o KardexControl (JOIN sobre phy_k).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexVsPhysical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario físico; Kardex; Kardex de control; Bodega/almacén; Producto de inventario; Movimiento de entrada/salida; Conciliación físico vs kardex', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexVsPhysical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve filas por bodega+producto solo cuando SUM(PhysicalQuantity) <> SUM(KardexQuantity), es decir, cuando hay descuadre entre físico y kardex.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexVsPhysical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si k.MovementType = 1 en Kardex/KardexControl → La cantidad suma al saldo teórico (signo +1). else La cantidad resta al saldo teórico (signo -1).; si k.AffectInventory = 1 en Inventory.Kardex → El movimiento se incluye en el cálculo del saldo teórico (KardexQuantity). else El movimiento de Kardex se ignora en el cálculo (los movimientos de KardexControl no aplican este filtro).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexVsPhysical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.Kardex; Inventory.KardexControl; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexVsPhysical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexVsPhysical';
GO
