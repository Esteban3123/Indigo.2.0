
CREATE view [Cost].[ViewMeasurementUnitByCostIntermediateDistribution]
as (
	select IntermediateDistributionId, pc.Id, pc.Code, pc.Name, pc.AllowEditCostValue, pc.CostValue
	from Cost.CostIntermediateDistributionBase b
	inner join Cost.CostIntermediateDistributionMeasurementUnit bd on b.Id = bd.IntermediateDistributionBaseId
	inner join Inventory.InventoryMeasurementUnit pc on pc.Id = bd.MeasurementUnitId
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Muestra las unidades de medida asociadas a cada distribución intermedia de costos, combinando las bases de distribución intermedia con sus unidades de medida configuradas y los datos del catálogo de unidades de inventario. Para cada distribución intermedia expone el identificador de la distribución, el código, nombre, valor de costo y si el valor de costo es editable para cada unidad de medida vinculada. Se utiliza en el proceso de costeo para consultar qué unidades de medida aplican a cada base de distribución intermedia, facilitando la parametrización y el cálculo de costos por unidad.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewMeasurementUnitByCostIntermediateDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewMeasurementUnitByCostIntermediateDistribution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las unidades de medida asociadas a cada base de distribución intermedia de costos, junto con su configuración de valor de costo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostIntermediateDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa una relación existente base-unidad: si no hay vínculo en CostIntermediateDistributionMeasurementUnit, no aparece en el resultado.; Las unidades de medida expuestas siempre existen en el catálogo de inventario (Inventory.InventoryMeasurementUnit).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostIntermediateDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'distribución intermedia de costos; base de distribución de costos; unidad de medida; valor de costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostIntermediateDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve solo unidades de medida que estén vinculadas a una base de distribución intermedia mediante la tabla puente (INNER JOIN), excluyendo bases sin unidades asociadas y unidades no relacionadas.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostIntermediateDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostIntermediateDistributionBase; Cost.CostIntermediateDistributionMeasurementUnit; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostIntermediateDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostIntermediateDistribution';
GO
