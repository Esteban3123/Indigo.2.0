
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