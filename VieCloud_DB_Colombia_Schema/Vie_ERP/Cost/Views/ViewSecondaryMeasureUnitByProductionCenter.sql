CREATE view [Cost].[ViewSecondaryMeasureUnitByProductionCenter]
as (
select mu.Id as Id, mu.Code, mu.Name, mu.Abbreviation, mu.UnitType, mu.AllowEditCostValue, mu.CostValue, mu.Status
from Cost.CostDistributionSecondary ds
inner join Cost.CostDistributionSecondaryBase dsb on ds.Id = dsb.DistributionSecondaryId
inner join Cost.CostDistributionSecondaryMeasurementUnit dsm on dsm.DistributionSecondaryBaseId = dsb.Id
inner join Inventory.InventoryMeasurementUnit mu on mu.Id = dsm.MeasurementUnitId
Group by mu.Id, mu.Code, mu.[Name], mu.Abbreviation, mu.UnitType, mu.AllowEditCostValue, mu.CostValue, mu.Status
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las unidades de medida asociadas a las bases de distribución secundaria de costos, mostrando solo aquellas que están efectivamente vinculadas a algún centro de producción a través de los criterios e inductores de reparto secundario. Combina la configuración de distribución secundaria con el catálogo de unidades de medida de inventario para exponer atributos como código, nombre, abreviatura, tipo de unidad y valor de costo. Se utiliza en el proceso de costeo para identificar qué unidades de medida son aplicables al prorratear costos indirectos entre áreas o centros de producción mediante bases secundarias.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewSecondaryMeasureUnitByProductionCenter';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el catálogo de unidades de medida que están asociadas a bases de distribución secundaria de costos por centro de producción.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros relacionados en CostDistributionSecondary, CostDistributionSecondaryBase y CostDistributionSecondaryMeasurementUnit para que la unidad de medida sea visible.; La unidad de medida debe existir en Inventory.InventoryMeasurementUnit y estar referenciada por al menos una unidad de medida de distribución secundaria.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen unidades de medida ligadas por INNER JOIN a una base secundaria de distribución de costos; las no asociadas quedan excluidas.; Los resultados son distintos por unidad de medida gracias al GROUP BY, evitando duplicados aunque una unidad esté asociada a varias bases secundarias.; No se aplica filtro por estado (Status); se devuelven tanto activas como inactivas si están asociadas.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad de medida; Distribución secundaria de costos; Base de distribución de costos; Centro de producción; Inventario', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.InventoryMeasurementUnit: Devuelve unidades de medida únicas (GROUP BY sobre todas sus columnas) que tienen vínculo vía CostDistributionSecondaryMeasurementUnit -> CostDistributionSecondaryBase -> CostDistributionSecondary.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionSecondary; Cost.CostDistributionSecondaryBase; Cost.CostDistributionSecondaryMeasurementUnit; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewSecondaryMeasureUnitByProductionCenter';
GO
