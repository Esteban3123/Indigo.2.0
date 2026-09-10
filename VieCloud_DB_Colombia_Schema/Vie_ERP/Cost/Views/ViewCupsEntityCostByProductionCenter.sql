

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el costo total por servicio CUPS, entidad, centro de producción y contrato para cada mes contable cerrado. Cruza los costos unitarios por actividad (de ClosedMonthCostActivity) con las cantidades facturadas por centro de costos (de ClosedMonthCUPSEntityByCostCenter), calculando la cantidad total de prestaciones, el valor unitario y el valor total (cantidad × valor unitario). Se usa para reportería de costos de producción: permite saber cuánto costó cada servicio o procedimiento CUPS en un centro de producción durante un período cerrado, desglosado por contrato o convenio.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCupsEntityCostByProductionCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCupsEntityCostByProductionCenter';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida cantidad, valor unitario y valor total de servicios CUPS por entidad y centro de producción dentro de un mes cerrado, cruzando actividades de costo con el cierre mensual de CUPS por centro de costos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCupsEntityCostByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir información en Cost.ClosedMonthCostActivity para el mes cerrado consultado.; Debe existir información correspondiente en Cost.ClosedMonthCUPSEntityByCostCenter con el mismo ClosedMonthId y CUPSEntityId.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCupsEntityCostByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El cruce entre actividad de costo y cierre CUPS siempre exige coincidencia de ClosedMonthId y CUPSEntityId.; Las actividades sin centro de costo asignado heredan el centro de costo del cierre CUPS para efectos de emparejamiento.; Las cantidades provienen exclusivamente de ClosedMonthCUPSEntityByCostCenter y los valores unitarios de ClosedMonthCostActivity.; El TotalValue se calcula multiplicando cantidad por valor unitario antes de agregar.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCupsEntityCostByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Mes cerrado contable (ClosedMonth); Centro de producción de costos; Centro de costos; Entidad CUPS (servicio); Valor unitario; Cantidad facturada; Costeo por actividad', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCupsEntityCostByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve por (ClosedMonthId, CostProductionCenterId, CUPSEntityId) la suma de cantidades, suma de valores unitarios y el total calculado como SUM(Quantity * UnitValue).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCupsEntityCostByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(cmca.CostCenterId, cmcupsbcc.CostCenterId) = cmcupsbcc.CostCenterId en el JOIN → Si la actividad de costo no tiene CostCenterId definido, se empareja con cualquier CostCenterId del cierre CUPS; si lo tiene, debe coincidir exactamente.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCupsEntityCostByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.ClosedMonthCostActivity; Cost.ClosedMonthCUPSEntityByCostCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCupsEntityCostByProductionCenter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCupsEntityCostByProductionCenter';
GO
