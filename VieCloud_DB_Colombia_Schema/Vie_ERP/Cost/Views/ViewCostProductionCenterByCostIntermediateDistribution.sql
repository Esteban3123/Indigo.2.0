
CREATE view [Cost].[ViewCostProductionCenterByCostIntermediateDistribution]
as (

	select b.IntermediateDistributionId, pc.Id, pc.Code, pc.Name
	from Cost.CostIntermediateDistributionBase b
	inner join Cost.CostIntermediateDistributionBaseDetail bd on b.Id = bd.IntermediateDistributionBaseId
	INNER join Cost.CostProductionCenter pc on pc.Id = bd.ProductionCenterId
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Muestra los centros de producción asociados a cada distribución intermedia de costos. Combina la base de distribución intermedia con su detalle para identificar qué centros de producción (unidades funcionales o centros de costo) participan en cada esquema de prorrateo de costos indirectos. Es útil para consultar, por distribución intermedia, cuáles son los centros receptores del reparto de costos, mostrando su código y nombre para facilitar la configuración y revisión del modelo de costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostProductionCenterByCostIntermediateDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostProductionCenterByCostIntermediateDistribution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los centros de producción asociados a cada base de distribución intermedia de costos, mediante el cruce de la base con su detalle.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostIntermediateDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación entre la base de distribución intermedia y su detalle (IntermediateDistributionBaseId); El detalle debe referenciar un centro de producción existente', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostIntermediateDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan centros de producción que están efectivamente vinculados a alguna base de distribución intermedia vía su detalle; Un mismo centro de producción puede aparecer en múltiples bases de distribución intermedia', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostIntermediateDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución intermedia de costos; Centro de producción; Base de distribución de costos; Contabilidad de costos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostIntermediateDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve únicamente combinaciones donde existe coincidencia en ambos JOINs (INNER JOIN), excluyendo bases sin detalle o detalles sin centro de producción válido', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostIntermediateDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostIntermediateDistributionBase; Cost.CostIntermediateDistributionBaseDetail; Cost.CostProductionCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostIntermediateDistribution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostProductionCenterByCostIntermediateDistribution';
GO
