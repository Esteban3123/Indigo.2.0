CREATE PROCEDURE[InteropCost].[SP_ReportGeneralProfitabilityTotalCostCx]
@InitialMonth int,
@InitialYear int,
@LastMonth int,
@LastYear int,
@CenterType int,
@CodePCenterIni varchar(50),
@CodePCenterFin varchar(50),
@Status int
AS
BEGIN

if @CodePCenterFin = ''
	Begin
		set @CodePCenterFin = 'z'
	End

SELECT
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
SUM(ce.[ManPowerDistributionDirect] + ce.[ManPowerDistributionInDirect]) AS LaborIndirectAndDirect,
SUM(ce.[DispensingDistribution] + ce.[TransferDistribution]) AS DispensationsAndConsumption,
SUM(ce.[DirectCostDistribution] + ce.[AutoCostDistribution] + ce.[FixedAssetDistribution]) AS GeneralExpenses,
SUM(ce.[DirectCostDistribution] + ce.[AutoCostDistribution]) AS GeneralExpensesDirect1,
SUM(ce.[ManPowerDistributionDirect] + ce.[ManPowerDistributionInDirect] + ce.[SecondaryManPowerDistributionDirect] + ce.[SecondaryManPowerDistributionInDirect]) AS LaborIndirectAndDirectFinal,
SUM(ce.[DispensingDistribution] + ce.[TransferDistribution] + ce.[SecondaryDispensingDistribution] + ce.[SecondaryTransferDistribution]) AS DispensationsAndConsumptionFinal,
SUM(ce.[DirectCostDistribution] + ce.[AutoCostDistribution] + ce.[FixedAssetDistribution] + ce.[SecondaryDirectCostDistribution] + ce.[SecondaryAutoCostDistribution] + ce.[SecondaryFixedAssetDistribution]) AS GeneralExpensesFinal,
SUM(ce.[ManPowerDistributionDirect] + ce.[SecondaryManPowerDistributionDirect]) AS ManPowerDirectFinalDetailed,
SUM(ce.[DispensingDistribution] + ce.[SecondaryDispensingDistribution]) AS DispensingDirectFinalDetailed,
SUM(ce.[ManPowerDistributionInDirect] + ce.[SecondaryManPowerDistributionInDirect]) AS ManPowerIndirectFinalDetailed,
SUM(ce.[TransferDistribution] + ce.[SecondaryTransferDistribution]) AS DispensingIndirectFinalDetailed,
SUM(ce.[DirectCostDistribution] + ce.[SecondaryDirectCostDistribution]+ ce.[AutoCostDistribution] + ce.[SecondaryAutoCostDistribution]) AS GeneralExpensesDirectFinalDetailed,
SUM(ce.[FixedAssetDistribution] + ce.[SecondaryFixedAssetDistribution]) AS WearFixedAssetFinalDetailed,
SUM(ce.[DirectCostDistribution] + ce.[AutoCostDistribution] + ce.[SecondaryDirectCostDistribution] + ce.[SecondaryAutoCostDistribution] + ce.[FixedAssetDistribution] + ce.[SecondaryFixedAssetDistribution]) as TotalGeneralExpenses,
SUM(ce.[ManPowerDistributionDirect] + ce.[SecondaryManPowerDistributionDirect] + ce.[DispensingDistribution] + ce.[SecondaryDispensingDistribution]) as TotalPrimaryCost,
SUM(ce.[ManPowerDistributionInDirect] + ce.[SecondaryManPowerDistributionInDirect] + ce.[TransferDistribution] + ce.[SecondaryTransferDistribution]) as TotalConvergenceCost
FROM InteropCost.CostEstimation AS ce
INNER JOIN InteropCost.ProductionCenter AS pc ON pc.Id = ce.ProductionCenterId
WHERE pc.Status = 1 and CE.[Year] BETWEEN @InitialYear AND @LastYear AND CE.[Month] BETWEEN @InitialMonth AND @LastMonth
and @CenterType = case @CenterType when 0 then @CenterType else pc.CenterType end
and pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin
GROUP BY pc.Code, pc.Name, pc.OrganizationalStructureOfCostId, pc.Area, pc.CenterType, pc.[Status]
END