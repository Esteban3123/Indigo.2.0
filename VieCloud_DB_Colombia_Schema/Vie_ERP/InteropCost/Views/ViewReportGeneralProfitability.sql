

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de rentabilidad general por centro de producción. Combina la información de centros de producción activos con sus estimaciones de costos mensuales, mostrando para cada centro su código, nombre, área, tipo y estructura organizativa, junto con todos los montos distribuidos por categoría: costos directos, mano de obra, activos fijos, dispensación, traslados, distribuciones iniciales, intermedias y secundarias. Sirve para analizar la rentabilidad y comportamiento de costos por unidad productiva en cada período mensual, siendo la fuente principal para reportes gerenciales y de control financiero de costos operativos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewReportGeneralProfitability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'VIEW', @level1name = N'ViewReportGeneralProfitability';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un reporte consolidado de rentabilidad general que combina estimaciones mensuales de costos con datos del centro de producción, restringido a centros activos.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros relacionados entre CostEstimation y ProductionCenter mediante ProductionCenterId; El centro de producción debe encontrarse en estado activo (Status = 1)', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen filas cuyo centro de producción tenga Status = 1 (excluye centros inactivos); El INNER JOIN garantiza que únicamente se devuelven estimaciones con centro de producción existente; La numeración Row se asigna globalmente sobre el resultado ordenado por ce.Id ascendente', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Estimación de costos; Distribución de costo directo; Distribución de mano de obra; Distribución de activos fijos; Distribución de dispensación; Distribución de traslado; Distribución inicial / intermedia / secundaria; Rentabilidad general; Período (Año/Mes)', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por cada estimación de costo asociada a un centro de producción activo, numerada secuencialmente vía ROW_NUMBER ordenado por CostEstimation.Id ascendente', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.CostEstimation; InteropCost.ProductionCenter', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'VIEW', @level1name=N'ViewReportGeneralProfitability';
GO
