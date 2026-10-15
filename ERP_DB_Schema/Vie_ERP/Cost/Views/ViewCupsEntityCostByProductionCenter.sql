

CREATE VIEW [Cost].[ViewCupsEntityCostByProductionCenter]
AS
SELECT
	cmca.ClosedMonthId,
	cmca.CostProductionCenterId, 
	cmca.CUPSEntityId,
	SUM(cmcupsbcc.Quantity) Quantity,
	SUM(cmca.UnitValue) UnitValue,
	SUM(cmcupsbcc.Quantity * cmca.UnitValue) TotalValue
FROM 
(
	SELECT
		cmca.ClosedMonthId,
		cmca.CostProductionCenterId, 
		cmca.CostCenterId, 
		cmca.CUPSEntityId, 
		cmca.UnitValue
	FROM Cost.ClosedMonthCostActivity cmca 
	GROUP BY cmca.ClosedMonthId, cmca.CostProductionCenterId, cmca.CostCenterId, cmca.CUPSEntityId, cmca.UnitValue
) cmca
JOIN Cost.ClosedMonthCUPSEntityByCostCenter cmcupsbcc 
	ON cmca.ClosedMonthId = cmcupsbcc.ClosedMonthId		
		AND cmca.CUPSEntityId = cmcupsbcc.CUPSEntityId
		AND ISNULL(cmca.CostCenterId, cmcupsbcc.CostCenterId) = cmcupsbcc.CostCenterId
GROUP BY cmca.ClosedMonthId, cmca.CostProductionCenterId, cmca.CUPSEntityId