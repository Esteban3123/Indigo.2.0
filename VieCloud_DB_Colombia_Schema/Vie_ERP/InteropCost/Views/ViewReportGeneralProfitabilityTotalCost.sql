

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de rentabilidad general que consolida el costo total por centro de producción (centro de costo) para cada período mensual (año/mes). Combina la información de estimación de costos con los datos del centro de producción para presentar, en una sola fila, todos los componentes del costo distribuido: costos directos, gastos generales, mano de obra directa e indirecta, activos fijos, dispensación, traslados y distribuciones secundarias. Calcula tres totales clave de negocio: gastos generales directos (DirectCostDistribution + AutoCostDistribution), total de gastos generales (suma de costos directos, automáticos y activos fijos primarios y secundarios), costo primario total (mano de obra directa + dispensación primaria y secundaria) y costo de convergencia total (mano de obra indirecta + traslados primarios y secundarios). Se utiliza para reportería de rentabilidad operativa y análisis de costos por unidad productiva en el módulo de interoperabilidad de costos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewReportGeneralProfitabilityTotalCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewReportGeneralProfitabilityTotalCost';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por período (año/mes) y centro de producción la estimación de costos con sus totales calculados (gastos generales directos, totales generales, costo primario y costo de convergencia) para reportes de rentabilidad.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitabilityTotalCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre CostEstimation.ProductionCenterId y ProductionCenter.Id para que la fila aparezca en el reporte.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitabilityTotalCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila de estimación de costos siempre se asocia a un centro de producción existente vía INNER JOIN (estimaciones sin centro válido quedan excluidas).; GeneralExpensesDirect = DirectCostDistribution + AutoCostDistribution.; TotalGeneralExpenses agrega distribuciones directas y automáticas tanto primarias como secundarias junto con activos fijos primarios y secundarios.; TotalPrimaryCost suma mano de obra directa y dispensación, primarias y secundarias.; TotalConvergenceCost suma mano de obra indirecta y traslado, primarias y secundarias.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitabilityTotalCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'centro de producción; estimación de costos; distribución de costos; costos directos; costos secundarios; mano de obra directa e indirecta; dispensación; traslado; activos fijos; gastos generales; rentabilidad', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitabilityTotalCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] InteropCost.ViewReportGeneralProfitabilityTotalCost: Devuelve una fila por cada CostEstimation que tenga ProductionCenter asociado, exponiendo atributos del centro (Código, Nombre, Área, Tipo, Estado) junto con las distribuciones de costos y tres agregados calculados: TotalGeneralExpenses, TotalPrimaryCost y TotalConvergenceCost.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitabilityTotalCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.CostEstimation; InteropCost.ProductionCenter', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitabilityTotalCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitabilityTotalCost';
GO
