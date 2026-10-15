

CREATE VIEW [InteropCost].[ViewReportGeneralProfitabilityTotalCost]
AS
SELECT
ce.Id As Consecutive,
pc.Code, 
pc.Name, 
pc.OrganizationalStructureOfCostId, 
pc.Area, 
pc.CenterType, 
pc.[Status],
ce.[Year],
ce.[Month],
ce.[AutoCostDistribution],
ce.[DirectCostDistribution],
ce.[ManPowerDistributionDirect],
ce.[DispensingDistribution],
ce.[ManPowerDistributionInDirect],
ce.[TransferDistribution],
ce.[DirectCostDistribution] + ce.[AutoCostDistribution] as GeneralExpensesDirect,
ce.[FixedAssetDistribution],
ce.[InitialDistribution],
ce.[IntermediateDistribution] ,
ce.[SecondaryDirectCostDistribution] ,
ce.[SecondaryAutoCostDistribution] ,
ce.[SecondaryManPowerDistributionDirect] ,
ce.[SecondaryManPowerDistributionInDirect], 
ce.[SecondaryFixedAssetDistribution] ,
ce.[SecondaryDispensingDistribution] ,
ce.[SecondaryTransferDistribution] ,
ce.[SecondaryDistribution] ,
ce.[DirectCostDistribution] + ce.[AutoCostDistribution] + ce.[SecondaryDirectCostDistribution] + ce.[SecondaryAutoCostDistribution] + ce.[FixedAssetDistribution] + ce.[SecondaryFixedAssetDistribution] as TotalGeneralExpenses,
ce.[ManPowerDistributionDirect] + ce.[SecondaryManPowerDistributionDirect] + ce.[DispensingDistribution] + ce.[SecondaryDispensingDistribution] as TotalPrimaryCost,
ce.[ManPowerDistributionInDirect] + ce.[SecondaryManPowerDistributionInDirect] + ce.[TransferDistribution] + ce.[SecondaryTransferDistribution] as TotalConvergenceCost
FROM InteropCost.CostEstimation AS ce
INNER JOIN InteropCost.ProductionCenter AS pc ON pc.Id = ce.ProductionCenterId