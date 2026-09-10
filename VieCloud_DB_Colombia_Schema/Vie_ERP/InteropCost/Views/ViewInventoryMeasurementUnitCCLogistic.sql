create view [InteropCost].[ViewInventoryMeasurementUnitCCLogistic]
	as
select distinct(mu.Id), mu.Code, mu.Name from [InteropCost].[LogisticsProductionCenterRecordDetail] as lpc inner join
[Inventory].[InventoryMeasurementUnit] as mu on lpc.InventoryMeasurementUnitId = mu.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidades de medida de inventario utilizadas en los registros logísticos de centros de producción. Combina el catálogo general de unidades de medida (por ejemplo: unidad, caja, frasco, miligramo) con los registros de producción logística, devolviendo únicamente las unidades que están efectivamente en uso en algún centro de producción. Sirve como lista filtrada de unidades de medida relevantes para la logística de costos, evitando mostrar unidades que no tengan movimiento en los centros de producción registrados.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewInventoryMeasurementUnitCCLogistic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewInventoryMeasurementUnitCCLogistic';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las unidades de medida de inventario (Id, Code, Name) que están en uso en los detalles de registros de producción logística por centro, sin duplicados.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre LogisticsProductionCenterRecordDetail.InventoryMeasurementUnitId e Inventory.InventoryMeasurementUnit.Id.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen unidades de medida que estén efectivamente referenciadas en al menos un detalle de registro de producción logística (INNER JOIN sobre InventoryMeasurementUnitId).; Aplica DISTINCT sobre Id para no repetir unidades de medida aunque sean usadas en múltiples detalles logísticos.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'unidad de medida de inventario; centro de producción logística; registro de producción logística', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.InventoryMeasurementUnit: Devuelve únicamente unidades de medida (Id, Code, Name) cuyo Id aparece en LogisticsProductionCenterRecordDetail.InventoryMeasurementUnitId, usando INNER JOIN y DISTINCT.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.LogisticsProductionCenterRecordDetail; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewInventoryMeasurementUnitCCLogistic';
GO
