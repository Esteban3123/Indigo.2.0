

CREATE VIEW [Inventory].[ViewReportKardexForPharmacy]
AS
SELECT
ik.Id,
ik.EntityCode,
iip.Id As 'IdProduct',
iip.Code AS 'CodeProduct',
ibs.ExpirationDate,
ik.DocumentDate,
ik.AverageCost,
case when ik.MovementType = 1 then '1' end AS 'income',
case when ik.MovementType = 2 then '2' end AS 'expenses',
ik.PreviousAmountWarehouse AS 'openingbalance',
case when ik.MovementType = 1 then (ik.PreviousAmountWarehouse + ik.Quantity) when ik.MovementType = 2 then (ik.PreviousAmountWarehouse - ik.Quantity) end AS 'finalBalance',
w.Name AS 'nameWarehouse',
thi.Name AS 'nameThirdParty',
iip.Name AS 'NameProduct',
atc.Concentration,
iim.Name,
ik.CreationUser AS 'UserCodeNameAux',
w.Code AS 'codeWarehouse'
FROM
Inventory.Kardex AS ik
LEFT JOIN Common.ThirdParty AS thi ON ik.ThirdPartyId = thi.Id
INNER JOIN Inventory.InventoryProduct AS iip ON ik.ProductId = iip.Id
LEFT JOIN Inventory.InventoryMeasurementUnit AS iim ON iip.MeasurementUnitId = iim.Id
LEFT JOIN Inventory.ATC AS atc ON iip.ATCId = atc.Id
LEFT JOIN Inventory.BatchSerial AS ibs ON ik.BatchSerialId = ibs.Id
INNER JOIN Inventory.Warehouse AS w ON ik.WarehouseId = w.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del kardex de farmacia e inventario que consolida todos los movimientos de entradas y salidas de medicamentos e insumos por bodega. Integra información del producto (nombre, código, concentración ATC, unidad de medida), lote con su fecha de vencimiento, bodega, tercero relacionado (proveedor o entidad), costo promedio, saldo anterior y saldo final calculado según el tipo de movimiento (entrada o salida). Está diseñada para reportería y auditoría del kardex en el módulo de farmacia, permitiendo trazabilidad completa del inventario por producto, lote, almacén y fecha de documento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportKardexForPharmacy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportKardexForPharmacy';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el kardex de farmacia con saldos inicial y final calculados según tipo de movimiento, junto con datos del producto, lote, bodega y tercero asociados.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexForPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada movimiento de kardex debe tener producto y bodega asociados (INNER JOIN con InventoryProduct y Warehouse).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexForPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan movimientos cuyo producto exista en InventoryProduct y cuya bodega exista en Warehouse.; El saldo final siempre se deriva del saldo previo aplicando la cantidad según el tipo de movimiento (suma en ingreso, resta en egreso).; Tercero, unidad de medida, clasificación ATC y lote/serial son opcionales (LEFT JOIN); su ausencia no excluye al movimiento.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexForPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex de farmacia; Movimiento de inventario (ingreso/egreso); Saldo inicial y final de bodega; Costo promedio; Lote y fecha de vencimiento; Clasificación ATC del medicamento; Bodega/almacén; Tercero; Unidad de medida', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexForPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.Kardex: Devuelve un registro por movimiento de kardex con saldo final calculado: si MovementType=1 (ingreso) suma la cantidad al saldo previo; si MovementType=2 (egreso) la resta.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexForPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ik.MovementType = 1 → Marca el movimiento como ingreso (''income''=1) y calcula saldo final = PreviousAmountWarehouse + Quantity.; si ik.MovementType = 2 → Marca el movimiento como egreso (''expenses''=2) y calcula saldo final = PreviousAmountWarehouse - Quantity. else Si MovementType no es 1 ni 2, finalBalance queda NULL e income/expenses quedan NULL.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexForPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Common.ThirdParty; Inventory.InventoryProduct; Inventory.InventoryMeasurementUnit; Inventory.ATC; Inventory.BatchSerial; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexForPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportKardexForPharmacy';
GO
