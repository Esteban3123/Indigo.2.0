

create view [InteropCost].[ViewSecondaryMeasureUnitByProductionCenter]
as (
select mu.Id, mu.Code, mu.Name, mu.Abbreviation, mu.UnitType, mu.AllowEditCostValue, mu.CostValue, mu.Status, ds.ProductionCenterId
from InteropCost.DistributionSecondary ds
inner join InteropCost.DistributionSecondaryBase dsb on ds.Id = dsb.DistributionSecondaryId
inner join InteropCost.DistributionSecondaryMeasurementUnit dsm on dsm.DistributionSecondaryBaseId = dsb.Id
inner join Inventory.InventoryMeasurementUnit mu on mu.Id = dsm.MeasurementUnitId
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Muestra las unidades de medida secundarias asociadas a cada centro de producción, combinando la configuración de distribución secundaria de costos con el catálogo de unidades de medida de inventario. Cruza la distribución secundaria, sus bases de distribución y las unidades de medida vinculadas, para exponer atributos como código, nombre, abreviatura, tipo de unidad y valor de costo junto con el identificador del centro de producción al que pertenecen. Sirve como fuente de consulta en el módulo de costos para identificar qué unidades de medida (por ejemplo: unidad, caja, miligramo) están disponibles o configuradas para repartir costos indirectos en cada centro de producción.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewSecondaryMeasureUnitByProductionCenter';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las unidades de medida secundarias asociadas a cada centro de producción a través de la cadena de distribución secundaria de costos.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en DistributionSecondary con ProductionCenterId asignado; Debe existir vínculo DistributionSecondaryBase ligado a DistributionSecondary; Debe existir DistributionSecondaryMeasurementUnit que referencie a la base; La unidad de medida referenciada debe existir en Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen unidades de medida que estén efectivamente vinculadas a una base de distribución secundaria con centro de producción (uso de INNER JOIN en toda la cadena); Una misma unidad de medida puede aparecer múltiples veces si está asociada a varios centros de producción mediante distintas distribuciones secundarias; El ProductionCenterId proviene exclusivamente de DistributionSecondary, no de la unidad de medida', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Distribución secundaria de costos; Unidad de medida secundaria; Base de distribución de costos; Interoperabilidad contable', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] InteropCost.ViewSecondaryMeasureUnitByProductionCenter: Devuelve datos de la unidad de medida (Id, Code, Name, Abbreviation, UnitType, AllowEditCostValue, CostValue, Status) junto al ProductionCenterId proveniente de DistributionSecondary, solo cuando existe la cadena completa de joins entre DistributionSecondary → DistributionSecondaryBase → DistributionSecondaryMeasurementUnit → InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.DistributionSecondary; InteropCost.DistributionSecondaryBase; InteropCost.DistributionSecondaryMeasurementUnit; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
