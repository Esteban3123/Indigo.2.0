create view [Cost].[ViewCostInventoryMeasurementUnitCCLogistic]
	as
select
 distinct(mu.Id), mu.Code, mu.Name 
 from
    Cost.CostLogisticsProductionCenterRecordDetail as lpc inner join
    [Inventory].[InventoryMeasurementUnit] as mu on lpc.InventoryMeasurementUnitId = mu.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidades de medida distintas que están siendo utilizadas en los registros logísticos de centros de producción, dentro del módulo de costos. Combina el detalle de movimientos logísticos por centro de producción con el catálogo de unidades de medida de inventario, devolviendo únicamente las unidades (código y nombre) que tienen al menos un registro logístico asociado. Sirve como lista de referencia filtrada para reportes y análisis de costos logísticos, evitando mostrar unidades de medida que no tienen actividad en el contexto de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostInventoryMeasurementUnitCCLogistic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostInventoryMeasurementUnitCCLogistic';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el catálogo distinto de unidades de medida de inventario que efectivamente han sido utilizadas en los detalles logísticos por centro de producción del módulo de costos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación válida entre el detalle logístico de centro de producción y el catálogo de unidades de medida de inventario', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se retornan unidades de medida que no estén referenciadas en el detalle logístico de centros de producción; Cada unidad de medida aparece una sola vez en el resultado por efecto del DISTINCT', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'unidad de medida de inventario; centro de producción; registro logístico; costos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve únicamente unidades de medida que tienen al menos un registro asociado en el detalle logístico (INNER JOIN), eliminando duplicados con DISTINCT', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostLogisticsProductionCenterRecordDetail; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostInventoryMeasurementUnitCCLogistic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostInventoryMeasurementUnitCCLogistic';
GO
