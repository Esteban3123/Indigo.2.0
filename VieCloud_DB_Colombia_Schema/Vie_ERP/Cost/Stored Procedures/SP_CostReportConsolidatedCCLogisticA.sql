CREATE PROCEDURE [Cost].[SP_CostReportConsolidatedCCLogisticA]
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

if @ProductionCenterIdFin = ''
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
[Cost].[CostLogisticsProductionCenterRecord] as lpc with (nolock) inner join 
[Cost].[CostLogisticsProductionCenterRecordDetail] as lpcd with (nolock) on lpc.Id = lpcd.LogisticsProductionCenterRecordId inner join 
[Cost].[CostProductionCenter] as pcDetail with (nolock) on lpcd.ProductionCenterId = pcDetail.Id inner join
[Cost].[CostProductionCenter] as pcLogistic with (nolock) on lpc.ProductionCenterId = pcLogistic.Id inner join
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte consolidado de costos logísticos por centro de producción. Genera un informe agrupado por día que muestra las cantidades de insumos o productos movidos logísticamente entre centros de producción, dentro de un rango de fechas, un centro logístico específico, un rango de códigos de centros de producción destino y un rango de unidades de medida. Cruza los registros logísticos (actas de movimiento) con su detalle, los centros de producción involucrados y el catálogo de unidades de medida, e incorpora la jerarquía organizacional de costos mediante una función recursiva que permite identificar el nodo padre en el nivel estructural indicado. Se usa para reportería contable y de control de costos logísticos internos, permitiendo filtrar y analizar los consumos o distribuciones de insumos entre áreas o dependencias de la organización.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportConsolidatedCCLogisticA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportConsolidatedCCLogisticA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte consolidado diario de cantidades de insumos/productos despachados por un centro de producción logístico hacia centros de producción de detalle, agrupado por día, unidad de medida y nivel jerárquico de la estructura organizacional de costos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere un rango de fechas válido (FechaInicial y FechaFinal).; Debe existir el centro de producción logístico indicado en Cost.CostProductionCenter.; La función InteropCost.fnRecursiveStructureFather debe estar disponible para resolver la jerarquía de estructura organizacional de costos.; Las unidades de medida de inventario deben existir en Inventory.InventoryMeasurementUnit.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran registros cuyo RecordDate, casteado a date, esté dentro del rango [FechaInicial, FechaFinal].; Solo se incluyen movimientos del centro de producción logístico especificado.; El filtro de códigos de centro de detalle es inclusivo en ambos extremos y de tipo alfabético (varchar).; La cantidad reportada es la suma agrupada por día calendario (DAY(RecordDate)), perdiendo el mes/año en la agrupación.; El nivel jerárquico para obtener el código padre proviene de la función recursiva de estructura organizacional según el nivel parametrizado.; Todas las lecturas se realizan con NOLOCK, permitiendo lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción / centro de costo; Centro de producción logístico; Estructura organizacional de costos; Jerarquía de centros (nivel padre); Unidad de medida de inventario; Movimiento/registro logístico de insumos; Consolidación diaria de consumos/despachos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve el resultado consolidado agrupado por centro de producción de detalle, día, centro logístico y unidad de medida, sumando la cantidad (SUM(lpcd.Count)) y resolviendo el código padre según el nivel organizacional indicado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El parámetro de código final de centro de producción viene vacío (''''). → Se reemplaza por ''z'' para actuar como límite superior abierto en el filtro alfabético de códigos. else Se respeta el valor recibido como límite superior.; si El parámetro inicial de unidad de medida es 0. → Se usa 0 como límite inferior (no filtra por unidad mínima). else Se filtra desde el valor recibido.; si El parámetro final de unidad de medida es 0. → Se usa 9999999999999999999 como límite superior (no filtra por unidad máxima). else Se filtra hasta el valor recibido.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'InteropCost.fnRecursiveStructureFather', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostLogisticsProductionCenterRecord; Cost.CostLogisticsProductionCenterRecordDetail; Cost.CostProductionCenter; Inventory.InventoryMeasurementUnit; InteropCost.fnRecursiveStructureFather', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportConsolidatedCCLogisticA';
-- GO
