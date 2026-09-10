CREATE PROCEDURE [InteropCost].[SP_ReportConsolidatedCCLogisticA]
@FechaInicial as Date,
@FechaFinal as Date,
@ProductionCenterIdLogistic Int,
@ProductionCenterIdIni varchar(20),
@ProductionCenterIdFin varchar(20),
@InventoryMeasurementUnitIdIni Int,
@InventoryMeasurementUnitIdFin Int,
@OrganizationalStructureLevel Int
As
begin

if @ProductionCenterIdFin = 0
		Begin
			set @ProductionCenterIdFin = 'z'
		End
SELECT
lpcd.ProductionCenterId as DetailProductionCenterId,
pcDetail.Code as DetailProductionCenterCode,
pcDetail.Name as DetailProductionCenterName,
pcDetail.OrganizationalStructureOfCostId as PCDetailOrganizationalStructureOfCostId,
(select Name from [InteropCost].[fnRecursiveStructureFather](pcDetail.OrganizationalStructureOfCostId,0) where NumberLevel = @OrganizationalStructureLevel)  as ParentCode,
lpcd.ProductionCenterId as ParentName,
day(lpc.RecordDate)[day],
sum(lpcd.[Count]) Cantidad,
lpc.ProductionCenterId as ProductionCenterIdLogistic,
pcLogistic.Code as ProductionCenterCodeLogistic,
pcLogistic.Name as ProductionCenterNameLogistic,
lpcd.InventoryMeasurementUnitId,
imu.Code as MeasurementUnitCode,
imu.Name as MeasurementUnitName
FROM 
[InteropCost].[LogisticsProductionCenterRecord] as lpc with (nolock) inner join 
[InteropCost].[LogisticsProductionCenterRecordDetail] as lpcd with (nolock) on lpc.Id = lpcd.LogisticsProductionCenterRecordId inner join 
[InteropCost].[ProductionCenter] as pcDetail with (nolock) on lpcd.ProductionCenterId = pcDetail.Id inner join
[InteropCost].[ProductionCenter] as pcLogistic with (nolock) on lpc.ProductionCenterId = pcLogistic.Id inner join
[Inventory].[InventoryMeasurementUnit] as imu with (nolock) on lpcd.InventoryMeasurementUnitId = imu.Id
where 
cast(lpc.RecordDate as date) >= @FechaInicial and cast(lpc.RecordDate as date) <= @FechaFinal and 
lpc.ProductionCenterId = @ProductionCenterIdLogistic and
pcDetail.Code >= @ProductionCenterIdIni and pcDetail.Code <= @ProductionCenterIdFin and
lpcd.InventoryMeasurementUnitId >= case @InventoryMeasurementUnitIdIni when 0 then 0 else @InventoryMeasurementUnitIdIni end  and lpcd.InventoryMeasurementUnitId <= case @InventoryMeasurementUnitIdFin when 0 then 9999999999999999999 else @InventoryMeasurementUnitIdFin end
--pcDetail.OrganizationalStructureOfCostId = (select * from [InteropCost].[fnRecursiveStructureFather](pcDetail.OrganizationalStructureOfCostId,0) where NumberLevel = 1)
group by 
lpcd.ProductionCenterId,
pcDetail.Code,
pcDetail.Name, 
pcDetail.OrganizationalStructureOfCostId,
day(lpc.RecordDate),
lpc.ProductionCenterId,
pcLogistic.Code,
pcLogistic.Name,
lpcd.InventoryMeasurementUnitId,
imu.Code,
imu.Name
order by  lpcd.InventoryMeasurementUnitId

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte consolidado de consumos logísticos por centro de costo y centro de producción: genera un resumen agrupado por día de las cantidades de insumos o medicamentos despachados/asignados desde un centro logístico hacia los centros de producción destino, dentro de un rango de fechas, centros de producción y unidades de medida de inventario. Combina los registros de actas logísticas (LogisticsProductionCenterRecord) con su detalle de cantidades por producto (LogisticsProductionCenterRecordDetail), cruzando información del centro de producción destino (código, nombre y estructura organizacional de costos obtenida recursivamente mediante fnRecursiveStructureFather según el nivel jerárquico indicado), el centro logístico origen y la unidad de medida (InventoryMeasurementUnit). Se usa para la reportería de distribución y control de costos operativos de farmacia, insumos o logística interna entre centros de la organización.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportConsolidatedCCLogisticA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportConsolidatedCCLogisticA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un consolidado diario de cantidades despachadas/registradas por un centro logístico, desglosado por centro de producción de detalle, unidad de medida y día, incluyendo el centro padre en un nivel jerárquico configurable.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los rangos de fechas (@FechaInicial, @FechaFinal) deben ser válidos y consistentes.; @ProductionCenterIdLogistic debe corresponder a un ProductionCenter existente que actúe como centro logístico.; Los códigos de centro de producción @ProductionCenterIdIni/@ProductionCenterIdFin son comparados como cadenas (varchar) sobre ProductionCenter.Code.; @OrganizationalStructureLevel debe existir como NumberLevel en la jerarquía retornada por fnRecursiveStructureFather; en caso contrario ParentCode será NULL.; Los IDs de unidad de medida usan 0 como sentinela para ''sin límite''.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros cuyo RecordDate (truncado a fecha) esté dentro del rango [@FechaInicial, @FechaFinal].; Solo se consolidan registros cuyo centro logístico coincida exactamente con @ProductionCenterIdLogistic.; El centro de costo padre reportado corresponde al nivel @OrganizationalStructureLevel dentro de la jerarquía recursiva de OrganizationalStructureOfCost del centro de detalle.; Las cantidades se agregan (SUM) por combinación de centro de detalle, día del RecordDate, centro logístico y unidad de medida.; El resultado se ordena por InventoryMeasurementUnitId.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'centro de producción; centro logístico; registro de producción logística; estructura organizacional de costos; unidad de medida de inventario; consolidado por día', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por centro de detalle, día (DAY(RecordDate)), centro logístico y unidad de medida, con SUM(lpcd.Count) como Cantidad, filtradas por rango de fechas, centro logístico, rango de códigos de centro de detalle y rango de unidades de medida.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ProductionCenterIdFin = 0 → Se reemplaza por ''z'' para usarlo como límite superior abierto en el filtro pcDetail.Code <= @ProductionCenterIdFin (alfabéticamente cubre todos los códigos).; si @InventoryMeasurementUnitIdIni = 0 → Se usa 0 como límite inferior efectivo (sin filtro inferior) else Se usa el valor recibido como límite inferior de InventoryMeasurementUnitId; si @InventoryMeasurementUnitIdFin = 0 → Se usa 9999999999999999999 como límite superior efectivo (sin filtro superior) else Se usa el valor recibido como límite superior de InventoryMeasurementUnitId', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'InteropCost.fnRecursiveStructureFather', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.LogisticsProductionCenterRecord; InteropCost.LogisticsProductionCenterRecordDetail; InteropCost.ProductionCenter; Inventory.InventoryMeasurementUnit; InteropCost.fnRecursiveStructureFather', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogisticA';
-- GO
