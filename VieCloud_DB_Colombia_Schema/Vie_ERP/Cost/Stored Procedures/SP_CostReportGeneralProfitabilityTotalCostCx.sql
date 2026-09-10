CREATE PROCEDURE [Cost].[SP_CostReportGeneralProfitabilityTotalCostCx]
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
FROM [Cost].[CostEstimationNative]  AS ce
INNER JOIN [Cost].[CostProductionCenter] AS pc ON pc.Id = ce.ProductionCenterId
WHERE CE.[Year] BETWEEN @InitialYear AND @LastYear AND CE.[Month] BETWEEN @InitialMonth AND @LastMonth
and @CenterType = case @CenterType when 0 then @CenterType else pc.CenterType end
and pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin
GROUP BY pc.Code, pc.Name, pc.OrganizationalStructureOfCostId, pc.Area, pc.CenterType, pc.[Status]
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de rentabilidad general y costo total por centro de producción para cirugía (Cx). Consolida, para un rango de meses y años definido, todos los componentes de costo distribuido (costos directos, mano de obra directa e indirecta, dispensación, transferencias, activos fijos y ajustes de autocosto) en sus etapas inicial, intermedia y secundaria, agrupados por centro de producción. Permite filtrar por tipo de centro, estado y rango de códigos de centro de producción, calculando además totales agrupados como costo laboral final, consumos y dispensaciones, gastos generales finales y costos primarios y de convergencia. Se utiliza en los informes de rentabilidad del módulo de costos para evaluar la estructura de costos de los centros de producción en un período determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportGeneralProfitabilityTotalCostCx';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportGeneralProfitabilityTotalCostCx';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte agregado de rentabilidad y costo total por centro de producción, sumando todas las distribuciones de costo (directos, indirectos, mano de obra, dispensación, traslado, activo fijo y secundarios) en un rango de meses/años.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportGeneralProfitabilityTotalCostCx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en CostEstimationNative dentro del rango de Year y Month indicados.; Cada registro de CostEstimationNative debe tener un ProductionCenterId válido en CostProductionCenter.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportGeneralProfitabilityTotalCostCx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango de centros de producción se evalúa siempre por código alfanumérico (>= inicial y <= final).; Las sumas siempre agrupan por Code, Name, OrganizationalStructureOfCostId, Area, CenterType y Status del centro de producción.; Los costos generales se calculan como suma de costo directo + autocosto (+ activo fijo en variantes ''General'').; El costo total combina componentes primarios (directos) y secundarios (de convergencia) de cada concepto.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportGeneralProfitabilityTotalCostCx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Distribución de costo directo; Distribución de autocosto; Mano de obra directa; Mano de obra indirecta; Dispensación; Traslado; Activo fijo; Distribución secundaria; Costo primario; Costo de convergencia; Rentabilidad general', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportGeneralProfitabilityTotalCostCx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve agregaciones SUM por centro de producción filtradas por rango de año/mes, tipo de centro y rango de códigos de centro.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportGeneralProfitabilityTotalCostCx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Código final de centro de producción viene vacío ('''') → Se reemplaza por ''z'' para abrir el rango superior de búsqueda por código else Se respeta el código final recibido como límite superior; si Tipo de centro = 0 → Se ignora el filtro por CenterType (se autoiguala) else Se filtra por el CenterType indicado contra pc.CenterType', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportGeneralProfitabilityTotalCostCx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostEstimationNative; Cost.CostProductionCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportGeneralProfitabilityTotalCostCx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportGeneralProfitabilityTotalCostCx';
-- GO
