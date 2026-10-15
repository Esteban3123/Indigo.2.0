

CREATE VIEW [InteropCost].[ViewReportGeneralProfitability]
AS
SELECT
ROW_NUMBER() OVER(ORDER BY ce.Id ASC) as Row,
pc.Code, 
pc.Name, 
pc.OrganizationalStructureOfCostId, 
pc.Area, 
pc.CenterType, 
pc.[Status],
ce.Id,
ce.[Year],
ce.[Month],
ce.DirectCostDistribution,
ce.AutoCostDistribution,
ce.ManPowerDistributionDirect,
ce.FixedAssetDistribution,
ce.DispensingDistribution,
ce.TransferDistribution,
ce.InitialDistribution,
ce.IntermediateDistribution,
ce.SecondaryDistribution
FROM InteropCost.CostEstimation AS ce
INNER JOIN InteropCost.ProductionCenter AS pc ON pc.Id = ce.ProductionCenterId
Where pc.[Status] = 1