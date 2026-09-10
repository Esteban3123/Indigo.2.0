
CREATE PROCEDURE [InteropCost].[SP_ReportConsolidatedCCLogisticB]
@FechaInicial as Date,
@FechaFinal as Date,
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

SELECT
lpcd.InventoryMeasurementUnitId,
imu.Code as MeasurementUnitCode,
imu.Name as MeasurementUnitName,
sum(lpcd.[Count]) Cantidad,
lpcd.ProductionCenterId as DetailProductionCenterId,
pcDetail.Code as DetailProductionCenterCode,
pcDetail.Name as DetailProductionCenterName,  
lpc.ProductionCenterId as ProductionCenterIdLogistic,
pcLogistic.Code as ProductionCenterCodeLogistic,
pcLogistic.Name as ProductionCenterNameLogistic

FROM 
[InteropCost].[LogisticsProductionCenterRecord] as lpc with (nolock) inner join 
[InteropCost].[LogisticsProductionCenterRecordDetail] as lpcd with (nolock) on lpc.Id = lpcd.LogisticsProductionCenterRecordId inner join 
[InteropCost].[ProductionCenter] as pcDetail with (nolock) on lpcd.ProductionCenterId = pcDetail.Id inner join
[InteropCost].[ProductionCenter] as pcLogistic with (nolock) on lpc.ProductionCenterId = pcLogistic.Id inner join
[Inventory].[InventoryMeasurementUnit] as imu with (nolock) on lpcd.InventoryMeasurementUnitId = imu.Id
where 
cast(lpc.RecordDate as date) >= @FechaInicial and cast(lpc.RecordDate as date) <= @FechaFinal and
pcDetail.[Status] = 1 and pcLogistic.[Status] = 1 and 
lpc.ProductionCenterId = @ProductionCenterIdLogistic and
pcDetail.Code >= @ProductionCentIdIni and pcDetail.Code <= @ProductionCentIdFin and
lpcd.InventoryMeasurementUnitId >= case @InventoryMeasurementUnitIdIni when 0 then 0 else @InventoryMeasurementUnitIdIni end  and lpcd.InventoryMeasurementUnitId <= case @InventoryMeasurementUnitIdFin when 0 then 9999999999999999999 else @InventoryMeasurementUnitIdFin end
group by 
lpcd.ProductionCenterId,
pcDetail.Code,
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte consolidado de consumos logísticos por centro de producción (centro de costo): dado un rango de fechas, un centro logístico origen y rangos opcionales de centros de producción destino y unidades de medida, totaliza las cantidades de insumos o medicamentos despachados desde el centro logístico hacia cada centro de costo receptor. Cruza los registros de producción logística (actas de despacho) con su detalle de cantidades, los centros de producción (origen y destino) y el catálogo de unidades de medida de inventario, devolviendo la suma de unidades agrupada por centro destino, unidad de medida y centro logístico. Se usa para control de costos y trazabilidad de distribución de insumos entre centros operativos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportConsolidatedCCLogisticB';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportConsolidatedCCLogisticB';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte consolidado de cantidades registradas por un centro de producción logístico hacia centros de producción de detalle, agrupadas por unidad de medida de inventario, dentro de un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (@FechaInicial, @FechaFinal) debe estar definido y se compara contra RecordDate convertido a date.; Debe existir un ProductionCenterIdLogistic válido que filtra los registros de LogisticsProductionCenterRecord.; Los centros de producción (detalle y logístico) deben tener Status = 1 (activos) para ser incluidos.; Los códigos de centro de producción y los IDs de unidad de medida pueden recibirse como ''0'' para indicar ''sin límite'' (se reemplazan por ''z'' o 9999999999999999999).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen centros de producción (detalle y logístico) con Status = 1.; Las cantidades reportadas siempre corresponden a registros cuyo RecordDate cae dentro del rango [@FechaInicial, @FechaFinal].; El centro logístico del reporte siempre coincide con @ProductionCenterIdLogistic.; El filtro por código de centro de detalle se realiza por comparación de cadena (varchar), no numérica, lo que permite el uso del centinela ''z''.; Las consultas se ejecutan con WITH (NOLOCK), por lo que pueden producir lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción logístico; Centro de producción de detalle (centro de costo); Registro de producción logística; Unidad de medida de inventario; Consolidado de cantidades despachadas/alistadas', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve cantidades sumadas (SUM(lpcd.Count)) agrupadas por centro de producción de detalle, centro logístico y unidad de medida, ordenadas por InventoryMeasurementUnitId.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ProductionCenterIdFin = 0 → Se reemplaza el límite superior de código de centro por ''z'' para no acotar el rango por arriba. else Se conserva el valor recibido como límite superior del código de centro de detalle.; si @InventoryMeasurementUnitIdIni = 0 → El límite inferior de unidad de medida se fija en 0 (sin filtro inferior efectivo). else Se aplica @InventoryMeasurementUnitIdIni como límite inferior.; si @InventoryMeasurementUnitIdFin = 0 → El límite superior de unidad de medida se fija en 9999999999999999999 (sin filtro superior efectivo). else Se aplica @InventoryMeasurementUnitIdFin como límite superior.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.LogisticsProductionCenterRecord; InteropCost.LogisticsProductionCenterRecordDetail; InteropCost.ProductionCenter; Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticB';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticB';
-- GO
