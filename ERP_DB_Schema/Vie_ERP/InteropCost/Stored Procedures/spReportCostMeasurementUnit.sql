
-- =============================================
-- Author:		Cristhian Salazar
-- Create date: 29/12/2016
-- Description:	 Store para el informe de costos de las unidades de medida
-- =============================================
CREATE PROCEDURE [InteropCost].[spReportCostMeasurementUnit]
	@InitialMonth int,
	@InitialYear int,
	@EndMonth int,
	@EndYear int,
	@InitialMeasurementUnitCode varchar(20),
	@EndMeasurementUnitCode varchar(20),
	@ProductionCenterId int,
	@Container varchar(20)
AS
BEGIN
	declare @TotalCostProductionCenter numeric(18,2) = 0
	declare @InitialDate date = '01/' + RIGHT('0'+cast(@InitialMonth as varchar(20)),2)+ '/' + cast(@InitialYear as varchar(20))
	declare @EndDate date = '01/' + RIGHT('0'+cast(@EndMonth as varchar(20)),2)+ '/' + cast(@EndYear as varchar(20))
	set @EndDate = DATEADD(day,-1,DATEADD(MONTH, 1, @EndDate))
	---Obtengo el costo total de las estimacion del centro de produccion logistico
	set @TotalCostProductionCenter = (select sum(InitialDistribution) from InteropCost.CostEstimation where ProductionCenterId = @ProductionCenterId and ('01/' + RIGHT('0'+cast([Month] as varchar(20)),2)+ '/' + cast([Year] as varchar(20))) between @InitialDate and @EndDate)
	---Tabla para alamcenar las cantidades reealizadas por la unidad de medida
	declare @TableDataMeasurement table(Id int, Code varchar(20), [Name] varchar(300), ValueCost numeric(18,2), Quantity int, TotalValue numeric(18,2), [Percentage] decimal, TotalCostValue numeric(18,2), UnitValueCost numeric(18,2), MainAccountCode varchar(20), MainAccountName varchar(120), DistributedCosts bit)

	--Se declara el String para construir la consulta a las dieferentes bases de datos 
	declare @StringSelect nvarchar(max)

	--Agrego el centro de Produccion para reflejarlo en el reporte
	--insert into @TableDataMeasurement(Id, Code, [Name], ValueCost, Quantity, TotalValue, [Percentage], TotalCostValue, UnitValueCost)
	set @StringSelect = 'select lrd.InventoryMeasurementUnitId, mu.Code, mu.[Name], mu.CostValue, sum(lrd.[Count]), sum(lrd.[Count]) * mu.CostValue, 0, 0, 0, NULL, NULL, 0
	from InteropCost.LogisticsProductionCenterRecordDetail lrd
	inner join InteropCost.LogisticsProductionCenterRecord lr on lr.Id = lrd.LogisticsProductionCenterRecordId
	inner join InteropCost.ProductionCenter as pc on pc.Id = lr.ProductionCenterId and pc.[Status] = 1
	inner join Inventory.InventoryMeasurementUnit mu on mu.Id = lrd.InventoryMeasurementUnitId
	where cast(lr.RecordDate as date) between ''' + cast(@InitialDate as varchar(20)) + ''' and ''' + cast(@EndDate as varchar(20)) + ''' and lr.ProductionCenterId = ' + cast(@ProductionCenterId as varchar(10))  + '
	group by lrd.InventoryMeasurementUnitId, mu.CostValue, mu.Code, mu.[Name]
	union All
	select ddcd.MeasurementUnitId, mu.Code, mu.[Name], ddcd.CostValue, sum(ddcd.[Count]),SUM( ddcd.[Value]), 0, 0, 0, Cue.CUECODIGO, Cue.CUENOMBRE, 1
	from InteropCost.DistributionDirectCost ddc
	inner join InteropCost.DistributionDirectCostDetail as ddcd on ddcd.DistributionDirectCostId = ddc.Id
	inner join InteropCost.ProductionCenter as pc on pc.Id = ddcd.ProductionCenterId and pc.[Status] = 1
	inner join '+ @Container +'.dbo.CTNCUENTA as cue on cue.OID = ddcd.MainAccountId
	inner join Inventory.InventoryMeasurementUnit as mu on mu.Id = ddcd.MeasurementUnitId
	where ddc.Year between ' + cast(@InitialYear as varchar(4)) + ' and ' + cast(@EndYear as varchar(4)) + ' and ddc.Month between ' + cast(@InitialMonth as varchar(2)) + ' and ' + cast(@EndMonth as varchar(2)) +  ' and pc.Id = ' + cast(@ProductionCenterId as varchar(10))  + '
	group by ddcd.MeasurementUnitId, ddcd.CostValue, mu.Code, mu.[Name], Cue.CUECODIGO, Cue.CUENOMBRE'
	
	insert into @TableDataMeasurement
	exec sp_executesql @StringSelect

	declare @TotalValue numeric(18,2) = (select sum(TotalValue) from @TableDataMeasurement)
	update @TableDataMeasurement set [Percentage] = round(TotalValue * 100 / @TotalValue,1), TotalCostValue = (TotalValue / @TotalValue) * @TotalCostProductionCenter, UnitValueCost = ((TotalValue / @TotalValue) * @TotalCostProductionCenter) / Quantity
	
	--declare @TotalValue2 numeric(18,2) = (select round(sum(Percentage),1) from @TableDataMeasurement)

	--declare @TotalValue3 numeric(18,2) = (select round(avg(Percentage),1) from @TableDataMeasurement)

	select *
	from @TableDataMeasurement
	where Code >= ISNULL(@InitialMeasurementUnitCode,'0') AND Code <= ISNULL(@EndMeasurementUnitCode,'Z')
	order by Code
END