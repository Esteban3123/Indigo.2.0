
CREATE view [Cost].[ViewCostProductionCenterByCostIntermediateDistribution]
as (

	select b.IntermediateDistributionId, pc.Id, pc.Code, pc.Name
	from Cost.CostIntermediateDistributionBase b
	inner join Cost.CostIntermediateDistributionBaseDetail bd on b.Id = bd.IntermediateDistributionBaseId
	INNER join Cost.CostProductionCenter pc on pc.Id = bd.ProductionCenterId
)