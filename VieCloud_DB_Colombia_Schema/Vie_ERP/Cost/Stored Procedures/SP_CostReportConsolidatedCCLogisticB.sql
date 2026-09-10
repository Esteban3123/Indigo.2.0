CREATE PROCEDURE [Cost].[SP_CostReportConsolidatedCCLogisticB]
	@FechaInicial as DateTime,
	@FechaFinal as DateTime,
	@ProductionCenterIdLogistic Int,
	@ProductionCenterIdIni Int,
	@ProductionCenterIdFin Int,
	@InventoryMeasurementUnitIdIni Int,
	@InventoryMeasurementUnitIdFin Int
AS
BEGIN
	declare @ProductionCentIdIni as varchar(20) = CAST(@ProductionCenterIdIni as varchar(20))
	declare @ProductionCentIdFin as varchar(20) = CAST(@ProductionCenterIdFin as varchar(20))

	if @ProductionCentIdFin = 0
	Begin
		set @ProductionCentIdFin = 'z'
	End

	SELECT	lpcd.InventoryMeasurementUnitId,
			imu.Code as MeasurementUnitCode,
			imu.Name as MeasurementUnitName,
			sum(lpcd.[Count]) Cantidad,
			lpcd.ProductionCenterId as DetailProductionCenterId,
			pcDetail.Code as DetailProductionCenterCode,
			pcDetail.Name as DetailProductionCenterName,  
			lpc.ProductionCenterId as ProductionCenterIdLogistic,
			pcLogistic.Code as ProductionCenterCodeLogistic,
			pcLogistic.Name as ProductionCenterNameLogistic
	FROM [Cost].[CostLogisticsProductionCenterRecord] as lpc with (nolock) 
	inner join [Cost].[CostLogisticsProductionCenterRecordDetail] as lpcd with (nolock) on lpc.Id = lpcd.LogisticsProductionCenterRecordId 
	inner join [Cost].[CostProductionCenter] as pcDetail with (nolock) on lpcd.ProductionCenterId = pcDetail.Id 
	inner join [Cost].[CostProductionCenter] as pcLogistic with (nolock) on lpc.ProductionCenterId = pcLogistic.Id 
	inner join [Inventory].[InventoryMeasurementUnit] as imu with (nolock) on lpcd.InventoryMeasurementUnitId = imu.Id
	where cast(lpc.RecordDate as date) >= @FechaInicial and cast(lpc.RecordDate as date) <= @FechaFinal 
		AND lpc.Status = 2
		and lpc.ProductionCenterId = @ProductionCenterIdLogistic 
		and pcDetail.Code >= @ProductionCentIdIni and pcDetail.Code <= @ProductionCentIdFin 
		and lpcd.InventoryMeasurementUnitId >= case @InventoryMeasurementUnitIdIni when 0 then 0 else @InventoryMeasurementUnitIdIni end  and lpcd.InventoryMeasurementUnitId <= case @InventoryMeasurementUnitIdFin when 0 then 9999999999999999999 else @InventoryMeasurementUnitIdFin end
	group by lpcd.ProductionCenterId, pcDetail.Code,
	pcDetail.Name, 
	lpc.ProductionCenterId,
	pcLogistic.Code,
	pcLogistic.Name,
	lpcd.InventoryMeasurementUnitId,
	imu.Code,
	imu.Name
	order by  lpcd.InventoryMeasurementUnitId
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte consolidado de movimientos logísticos entre centros de producción (costos), filtrado por rango de fechas, centro logístico origen, rango de centros de producción destino y rango de unidades de medida. Consolida los registros logísticos confirmados (estado 2) cruzando el acta de movimiento logístico con su detalle de insumos/productos, el catálogo de centros de producción (tanto el centro logístico que despacha como el centro destino que recibe) y el catálogo de unidades de medida de inventario. El resultado muestra la cantidad total consumida o distribuida por unidad de medida y por centro de producción destino, agrupada y ordenada por unidad de medida, permitiendo analizar la distribución de insumos desde un centro logístico hacia otros centros en un período determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportConsolidatedCCLogisticB';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportConsolidatedCCLogisticB';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el consolidado de cantidades de insumos por unidad de medida y centro de producción de detalle, asociados a un centro logístico, dentro de un rango de fechas, considerando solo registros aprobados (Status=2).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de producción logístico debe existir en Cost.CostProductionCenter; Las fechas inicial y final deben definir un rango válido sobre RecordDate; Existir registros logísticos en estado 2 (aprobado/cerrado) para devolver datos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran registros logísticos con Status = 2; El filtro por rango de centros de producción se realiza sobre el Code (texto) del centro de detalle, no sobre el Id; Las fechas se comparan truncadas a DATE, ignorando la hora de RecordDate; Los valores 0 en parámetros de rango se interpretan como ''sin límite'' en ese extremo; El resultado siempre se ordena por InventoryMeasurementUnitId', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción logístico; Centro de costo/producción de detalle; Unidad de medida de inventario; Registro logístico de costos; Consolidado de costos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.CostLogisticsProductionCenterRecord: Devuelve sumatoria de Count agrupada por unidad de medida y centro de producción de detalle, filtrando lpc.Status = 2 y lpc.ProductionCenterId igual al centro logístico recibido', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Parámetro de centro de producción final (texto) = ''0'' → Se reemplaza por ''z'' para que el filtro de Code <= valor abarque todos los códigos alfanuméricos posibles (sin tope superior efectivo); si InventoryMeasurementUnitIdIni = 0 → Se aplica límite inferior 0 (sin restricción inferior efectiva) else Se usa el valor recibido como límite inferior; si InventoryMeasurementUnitIdFin = 0 → Se aplica límite superior 9999999999999999999 (sin restricción superior efectiva) else Se usa el valor recibido como límite superior', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostLogisticsProductionCenterRecord; Cost.CostLogisticsProductionCenterRecordDetail; Cost.CostProductionCenter; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticB';
-- GO
