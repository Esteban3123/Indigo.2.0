

CREATE view [Cost].[ViewMeasurementUnitByCostSecondaryDistribution]
as (

select DistributionSecondaryId, pc.Id, pc.Code, pc.Name, pc.AllowEditCostValue, pc.CostValue
from Cost.CostDistributionSecondaryBase b
inner join Cost.CostDistributionSecondaryMeasurementUnit bd on b.Id = bd.DistributionSecondaryBaseId
inner join Inventory.InventoryMeasurementUnit pc on pc.Id = bd.MeasurementUnitId

)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra las unidades de medida disponibles para cada distribución secundaria de costos. Integra las bases secundarias de distribución, su relación con unidades de medida y el catálogo de unidades del inventario, consolidando en un solo resultado el identificador de la distribución secundaria junto con el código, nombre, valor de costo y si el valor es editable para cada unidad de medida asociada. Se usa para parametrizar y visualizar qué unidades de medida se aplican al prorratear costos indirectos entre áreas o centros de costo en el proceso de distribución secundaria.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewMeasurementUnitByCostSecondaryDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewMeasurementUnitByCostSecondaryDistribution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las unidades de medida del inventario asociadas a cada base de distribución secundaria de costos, junto con sus atributos de código, nombre, permiso de edición y valor de costo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre la base de distribución secundaria, su asociación de unidad de medida y el catálogo de unidades de medida del inventario.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen unidades de medida que están efectivamente vinculadas a una base de distribución secundaria de costos (INNER JOIN entre base, asociación y catálogo).; Cada fila representa la relación entre una distribución secundaria y una unidad de medida del inventario, conservando atributos del catálogo como código, nombre, permiso de edición y valor de costo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución secundaria de costos; Unidad de medida; Base de distribución de costos; Valor de costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.ViewMeasurementUnitByCostSecondaryDistribution: Devuelve una fila por cada vínculo existente entre una base de distribución secundaria de costos y una unidad de medida del inventario, exponiendo el identificador de la distribución secundaria y los datos de la unidad de medida.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionSecondaryBase; Cost.CostDistributionSecondaryMeasurementUnit; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMeasurementUnitByCostSecondaryDistribution';
GO
