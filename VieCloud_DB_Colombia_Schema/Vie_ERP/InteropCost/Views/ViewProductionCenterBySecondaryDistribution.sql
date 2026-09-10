

CREATE view [InteropCost].[ViewProductionCenterBySecondaryDistribution]
as (

select DistributionSecondaryId, pc.Id, pc.Code, pc.Name
from InteropCost.DistributionSecondaryBase b
inner join InteropCost.DistributionSecondaryBaseDetail bd on b.Id = bd.DistributionSecondaryBaseId
inner join InteropCost.ProductionCenter pc on pc.Id = bd.ProductionCenterId

)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los centros de producción (centros de costo) asociados a cada proceso de distribución secundaria de costos. Combina las bases secundarias de distribución con su detalle de reparto para identificar qué unidades productivas participan en cada proceso de distribución indirecta de costos. Es útil para reportería de costeo, permitiendo saber qué centros de costo reciben imputaciones dentro de un proceso específico de distribución secundaria.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewProductionCenterBySecondaryDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewProductionCenterBySecondaryDistribution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, por cada base secundaria de distribución de costos, los centros de producción asociados a través de su detalle de reparto.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterBySecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en DistributionSecondaryBase con detalles en DistributionSecondaryBaseDetail vinculados a ProductionCenter.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterBySecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen centros de producción que tengan un detalle asociado a una base secundaria (INNER JOIN), excluyendo bases sin detalle o detalles sin centro válido.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterBySecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'distribución secundaria de costos; base de distribución; centro de producción; detalle de reparto', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterBySecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve DistributionSecondaryId junto con Id, Code y Name del centro de producción para cada detalle de la base secundaria.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterBySecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.DistributionSecondaryBase; InteropCost.DistributionSecondaryBaseDetail; InteropCost.ProductionCenter', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterBySecondaryDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewProductionCenterBySecondaryDistribution';
GO
