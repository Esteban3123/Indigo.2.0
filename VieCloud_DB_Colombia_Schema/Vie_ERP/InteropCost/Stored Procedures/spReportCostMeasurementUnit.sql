
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el informe de costos por unidad de medida logística para un centro de producción en un rango de períodos (mes/año). Calcula la distribución proporcional del costo total del centro de producción (obtenido de la estimación de costos CostEstimation) entre cada unidad de medida, según la cantidad de actividad registrada en los registros logísticos y los costos directos distribuidos. Para cada unidad de medida devuelve: cantidad ejecutada, valor total, porcentaje de participación, costo distribuido total y costo unitario; permitiendo filtrar por rango de código de unidad de medida y acotando el período de consulta entre mes/año inicial y final.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'spReportCostMeasurementUnit';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'spReportCostMeasurementUnit';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el informe de costos por unidad de medida de un centro de producción, prorrateando el costo total estimado del período según la participación de cada unidad en los consumos logísticos y costos directos distribuidos.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'spReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de producción referenciado debe existir y tener Status = 1 (activo) para que sus registros sean considerados.; Los parámetros de mes/año inicial y final deben permitir construir fechas válidas (día 01 del mes); el rango efectivo cubre desde el primer día del mes inicial hasta el último día del mes final.; Debe existir una base de datos cuyo nombre se pasa en @Container y que contenga la tabla dbo.CTNCUENTA (catálogo contable externo).; Debe haber registros en CostEstimation, LogisticsProductionCenterRecord(Detail) o DistributionDirectCost(Detail) en el período; de lo contrario @TotalValue puede ser 0/NULL y provocar división por cero al calcular Percentage/UnitValueCost.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'spReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran centros de producción con Status=1 (activos) tanto en el flujo logístico como en el de costos directos.; El rango de fechas se normaliza al primer día del mes inicial y al último día del mes final (DATEADD(day,-1,DATEADD(MONTH,1,...))).; El costo total del centro a prorratear proviene exclusivamente de InteropCost.CostEstimation.InitialDistribution para el centro y período indicados.; El reparto del costo total entre unidades de medida es proporcional al TotalValue de cada fila respecto al TotalValue global (sum(TotalValue)).; Las filas provenientes de costos directos siempre llevan DistributedCosts=1 y cuenta contable; las logísticas siempre DistributedCosts=0 y cuenta contable nula.; El nombre de base de datos contable es dinámico (@Container) y se concatena directamente en el SQL ejecutado vía sp_executesql.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'spReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Costos por centro de producción; Unidad de medida de inventario; Estimación de costos mensual; Distribución de costos directos; Cuenta contable principal; Registro logístico de producción; Prorrateo de costo unitario', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'spReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableDataMeasurement (tabla variable): Inserta una fila por unidad de medida proveniente de LogisticsProductionCenterRecordDetail (con DistributedCosts=0, sin cuenta contable) y otra por unidad de medida proveniente de DistributionDirectCostDetail (con DistributedCosts=1 y CUECODIGO/CUENOMBRE de la cuenta contable principal), filtrando por centro de producción activo y rango de fechas/meses.; [UPDATE] @TableDataMeasurement (tabla variable): Para cada fila: Percentage = round(TotalValue*100/@TotalValue,1); TotalCostValue = (TotalValue/@TotalValue)*@TotalCostProductionCenter; UnitValueCost = TotalCostValue/Quantity, donde @TotalCostProductionCenter es la suma de InitialDistribution de CostEstimation del centro en el período.; [RETURN_RESULT] @TableDataMeasurement (resultado): Devuelve las filas de @TableDataMeasurement cuyo Code esté entre ISNULL(@InitialMeasurementUnitCode,''0'') y ISNULL(@EndMeasurementUnitCode,''Z''), ordenadas por Code.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'spReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen de datos: registros logísticos del centro (LogisticsProductionCenterRecord/Detail) en el rango de fechas → Suma cantidades y calcula TotalValue como sum(Count)*CostValue de la unidad; marca DistributedCosts=0 y deja MainAccountCode/Name en NULL; si Origen de datos: distribución de costos directos (DistributionDirectCost/Detail) en el rango de meses/años → Suma cantidades y TotalValue=SUM(Value); marca DistributedCosts=1 y trae CUECODIGO/CUENOMBRE desde [Container].dbo.CTNCUENTA vía MainAccountId; si Filtro final por código de unidad de medida con ISNULL(@InitialMeasurementUnitCode,''0'') y ISNULL(@EndMeasurementUnitCode,''Z'') → Si los parámetros vienen NULL se usa el rango ''0''..''Z'' como cota lexicográfica por defecto', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'spReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'spReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.CostEstimation; InteropCost.LogisticsProductionCenterRecordDetail; InteropCost.LogisticsProductionCenterRecord; InteropCost.ProductionCenter; Inventory.InventoryMeasurementUnit; InteropCost.DistributionDirectCost; InteropCost.DistributionDirectCostDetail; [@Container].dbo.CTNCUENTA', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'spReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'spReportCostMeasurementUnit';
-- GO
