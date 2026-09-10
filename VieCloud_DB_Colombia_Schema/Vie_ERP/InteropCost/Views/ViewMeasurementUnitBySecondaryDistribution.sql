

CREATE view [InteropCost].[ViewMeasurementUnitBySecondaryDistribution]
as (

select DistributionSecondaryId, pc.Id, pc.Code, pc.Name, pc.AllowEditCostValue, pc.CostValue
from InteropCost.DistributionSecondaryBase b
inner join InteropCost.DistributionSecondaryMeasurementUnit bd on b.Id = bd.DistributionSecondaryBaseId
inner join Inventory.InventoryMeasurementUnit pc on pc.Id = bd.MeasurementUnitId

)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra las unidades de medida asociadas a cada distribución secundaria de costos. Combina la configuración de bases de distribución secundaria con las unidades de medida definidas en el catálogo de inventario, permitiendo identificar qué unidades (por ejemplo: unidad, caja, miligramo) están vinculadas a cada criterio de reparto de costos indirectos entre unidades funcionales. Expone el identificador de la distribución secundaria junto con el código, nombre, valor de costo y si dicho valor es editable para cada unidad de medida relacionada. Se usa en el módulo de costos para consultar y configurar las unidades de medida que participan en la distribución secundaria de costos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewMeasurementUnitBySecondaryDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewMeasurementUnitBySecondaryDistribution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las unidades de medida (con su código, nombre, valor de costo y permiso de edición) asociadas a cada base secundaria de distribución de costos.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitBySecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las unidades de medida referenciadas por DistributionSecondaryMeasurementUnit deben existir en Inventory.InventoryMeasurementUnit.; Las relaciones en DistributionSecondaryMeasurementUnit deben referenciar bases existentes en DistributionSecondaryBase.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitBySecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen unidades de medida que están efectivamente asociadas a una base de distribución secundaria mediante la tabla puente DistributionSecondaryMeasurementUnit (INNER JOIN).; Solo se exponen bases secundarias que tienen al menos una unidad de medida vinculada y existente en el catálogo de inventario.; Cada fila representa la combinación base secundaria ↔ unidad de medida (una unidad puede aparecer en varias bases distintas).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitBySecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'base de distribución secundaria de costos; unidad de medida; valor de costo', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitBySecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.DistributionSecondaryBase; InteropCost.DistributionSecondaryMeasurementUnit; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitBySecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitBySecondaryDistribution';
GO
