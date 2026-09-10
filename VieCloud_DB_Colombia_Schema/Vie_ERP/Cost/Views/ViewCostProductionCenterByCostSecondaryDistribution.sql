

CREATE view [Cost].[ViewCostProductionCenterByCostSecondaryDistribution]
as (

select DistributionSecondaryId, pc.Id, pc.Code, pc.Name
from Cost.CostDistributionSecondaryBase b
inner join Cost.CostDistributionSecondaryBaseDetail bd on b.Id = bd.DistributionSecondaryBaseId
inner join Cost.CostProductionCenter pc on pc.Id = bd.ProductionCenterId

)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra los centros de producción asociados a cada distribución secundaria de costos. Integra las bases secundarias de distribución con su detalle de participación y los datos maestros del centro de producción (código y nombre), permitiendo identificar qué áreas o centros de costo reciben costos indirectos en una distribución secundaria específica. Es útil para reportería de costeo indirecto, análisis de reparto de costos entre centros y auditoría del proceso de distribución secundaria.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostProductionCenterByCostSecondaryDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostProductionCenterByCostSecondaryDistribution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los centros de producción asociados a cada base secundaria de distribución de costos, vinculando la base con sus centros participantes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en la base secundaria de distribución con detalle asociado y los detalles deben referenciar centros de producción válidos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen bases secundarias que tengan al menos un detalle con centro de producción existente (INNER JOIN excluye huérfanos).; Un mismo centro de producción puede aparecer asociado a múltiples bases secundarias.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución secundaria de costos; Base de distribución de costos; Centro de producción; Centro de costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.CostDistributionSecondaryBase: Por cada detalle de base secundaria que cruce con un centro de producción (INNER JOINs), se retorna una fila con la base y los datos identificadores del centro.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionSecondaryBase; Cost.CostDistributionSecondaryBaseDetail; Cost.CostProductionCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostSecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostSecondaryDistribution';
GO
