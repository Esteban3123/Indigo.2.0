CREATE PROCEDURE[InteropCost].[SP_ReportGeneralProfitabilityTotalCostB]
@InitialMonth int,
@InitialYear int,
@LastMonth int,
@LastYear int
AS
SELECT
--ce.Id As Consecutive,
pc.Code, 
pc.Name, 
pc.OrganizationalStructureOfCostId, 
pc.Area, 
pc.CenterType, 
pc.[Status],
SUM(ce.[AutoCostDistribution]) AS AutoCostDistribution ,
SUM(ce.[DirectCostDistribution])AS DirectCostDistribution,
SUM(ce.[ManPowerDistributionDirect]) AS ManPowerDistributionDirect,
SUM(ce.[DispensingDistribution]) AS DispensingDistribution,
SUM(ce.[ManPowerDistributionInDirect]) AS ManPowerDistributionInDirect,
SUM(ce.[TransferDistribution]) AS TransferDistribution,
SUM(ce.[DirectCostDistribution] + ce.[AutoCostDistribution]) as GeneralExpensesDirect,
SUM(ce.[FixedAssetDistribution]) AS FixedAssetDistribution,
SUM(ce.[InitialDistribution]) AS InitialDistribution,
SUM(ce.[IntermediateDistribution]) AS IntermediateDistribution,
SUM(ce.[SecondaryDirectCostDistribution]) AS SecondaryDirectCostDistribution,
SUM(ce.[SecondaryAutoCostDistribution]) AS SecondaryAutoCostDistribution,
SUM(ce.[SecondaryManPowerDistributionDirect]) AS SecondaryManPowerDistributionDirect,
SUM(ce.[SecondaryManPowerDistributionInDirect]) AS SecondaryManPowerDistributionInDirect, 
SUM(ce.[SecondaryFixedAssetDistribution]) AS SecondaryFixedAssetDistribution,
SUM(ce.[SecondaryDispensingDistribution]) AS SecondaryDispensingDistribution,
SUM(ce.[SecondaryTransferDistribution]) AS SecondaryTransferDistribution,
SUM(ce.[SecondaryDistribution]) AS SecondaryDistribution,
SUM(ce.[DirectCostDistribution] + ce.[AutoCostDistribution] + ce.[SecondaryDirectCostDistribution] + ce.[SecondaryAutoCostDistribution] + ce.[FixedAssetDistribution] + ce.[SecondaryFixedAssetDistribution]) as TotalGeneralExpenses,
SUM(ce.[ManPowerDistributionDirect] + ce.[SecondaryManPowerDistributionDirect] + ce.[DispensingDistribution] + ce.[SecondaryDispensingDistribution]) as TotalPrimaryCost,
SUM(ce.[ManPowerDistributionInDirect] + ce.[SecondaryManPowerDistributionInDirect] + ce.[TransferDistribution] + ce.[SecondaryTransferDistribution]) as TotalConvergenceCost
FROM InteropCost.CostEstimation AS ce
INNER JOIN InteropCost.ProductionCenter AS pc ON pc.Id = ce.ProductionCenterId
WHERE CE.[Year] BETWEEN @InitialYear AND @LastYear AND CE.[Month] BETWEEN @InitialMonth AND @LastMonth
GROUP BY pc.Code, pc.Name, pc.OrganizationalStructureOfCostId, pc.Area, pc.CenterType, pc.[Status]--, ce.Id