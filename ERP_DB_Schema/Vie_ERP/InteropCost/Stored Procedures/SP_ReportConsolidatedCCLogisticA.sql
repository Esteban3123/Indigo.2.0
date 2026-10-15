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