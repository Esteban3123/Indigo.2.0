CREATE PROCEDURE [InteropCost].[SP_ReportConsolidatedCCLogistic]
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
(select Code from InteropCost.OrganizationalStructureOfCosts where Id = pcDetail.OrganizationalStructureOfCostId) as ParentCode,
(select Name from [InteropCost].[fnRecursiveStructureFather](pcDetail.OrganizationalStructureOfCostId,0) where NumberLevel = @OrganizationalStructureLevel) as ParentName,
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte consolidado de consumos logísticos por centro de costo: genera un informe agrupado por día que muestra las cantidades de productos/insumos despachados o distribuidos desde un centro logístico hacia los centros de producción destino, en un rango de fechas determinado. Cruza los registros de actas logísticas (LogisticsProductionCenterRecord) con su detalle de cantidades (LogisticsProductionCenterRecordDetail), los centros de producción origen y destino, las unidades de medida de inventario, y la estructura organizacional de costos, permitiendo filtrar por centro logístico, rango de códigos de centro de producción destino, rango de unidades de medida y nivel jerárquico de la estructura de costos. Utiliza la función recursiva fnRecursiveStructureFather para obtener el nombre del nodo padre correspondiente al nivel organizacional solicitado, facilitando la reportería financiera y de control de costos de insumos y medicamentos distribuidos internamente.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportConsolidatedCCLogistic';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportConsolidatedCCLogistic';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte consolidado diario de cantidades despachadas por un centro logístico hacia centros de costo destino, agrupado por día, unidad de medida y nodo padre de la estructura organizacional de costos.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogistic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@FechaInicial y @FechaFinal definen el rango de RecordDate (inclusive en ambos extremos).; @ProductionCenterIdLogistic identifica el centro logístico emisor y es obligatorio (filtro por igualdad).; @OrganizationalStructureLevel indica el nivel jerárquico del padre a obtener vía fnRecursiveStructureFather.; Los códigos de centros de producción destino deben estar en el rango [@ProductionCenterIdIni, @ProductionCenterIdFin].', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogistic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros logísticos cuyo centro de producción emisor coincide exactamente con @ProductionCenterIdLogistic.; El rango de fechas se evalúa sobre CAST(RecordDate AS DATE), por lo que la hora de RecordDate se ignora.; El nombre del padre organizacional (ParentName) se obtiene del nivel jerárquico exacto solicitado (@OrganizationalStructureLevel) mediante fnRecursiveStructureFather.; Las consultas usan WITH (NOLOCK) en todas las tablas, permitiendo lecturas sucias.; La cantidad reportada se totaliza por día calendario (day(RecordDate)), no por fecha completa.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogistic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción / centro de costo; Centro logístico (despacho/alistamiento); Estructura organizacional de costos jerárquica; Unidad de medida de inventario; Registro logístico de producción; Consolidado diario de consumo/despacho', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogistic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por (centro destino, día de RecordDate, centro logístico, unidad de medida) con SUM(lpcd.[Count]) como Cantidad, ordenadas por InventoryMeasurementUnitId.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogistic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ProductionCenterIdFin = 0 → Se reasigna @ProductionCenterIdFin = ''z'' para no acotar el extremo superior del rango de códigos de centro destino (filtro abierto al final alfabético). else Se conserva el valor recibido como tope superior del rango de códigos.; si @InventoryMeasurementUnitIdIni = 0 → El límite inferior de unidad de medida se fija en 0 (sin filtro efectivo por inferior). else Se aplica @InventoryMeasurementUnitIdIni como límite inferior.; si @InventoryMeasurementUnitIdFin = 0 → El límite superior de unidad de medida se fija en 9999999999999999999 (sin filtro efectivo por superior). else Se aplica @InventoryMeasurementUnitIdFin como límite superior.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogistic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'InteropCost.fnRecursiveStructureFather', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogistic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.LogisticsProductionCenterRecord; InteropCost.LogisticsProductionCenterRecordDetail; InteropCost.ProductionCenter; Inventory.InventoryMeasurementUnit; InteropCost.OrganizationalStructureOfCosts; InteropCost.fnRecursiveStructureFather', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogistic';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportConsolidatedCCLogistic';
-- GO
