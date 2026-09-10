
-- =============================================
-- Author:		Cristhian Salazar
-- Create date: 29/12/2016
-- Description:	 Store para el informe de costos de las unidades de medida
-- =============================================
CREATE PROCEDURE [Cost].[spCostReportCostMeasurementUnit]
	@InitialMonth int,
	@InitialYear int,
	@EndMonth int,
	@EndYear int,
	@InitialMeasurementUnitCode varchar(20),
	@EndMeasurementUnitCode varchar(20),
	@ProductionCenterId int
AS
BEGIN
	declare @TotalCostProductionCenter numeric(18,2) = 0
	declare @InitialDate date = '01/' + RIGHT('0'+cast(@InitialMonth as varchar(20)),2)+ '/' + cast(@InitialYear as varchar(20))
	declare @EndDate date = '01/' + RIGHT('0'+cast(@EndMonth as varchar(20)),2)+ '/' + cast(@EndYear as varchar(20))
	set @EndDate = DATEADD(day,-1,DATEADD(MONTH, 1, @EndDate))
	---Obtengo el costo total de las estimacion del centro de produccion logistico
	set @TotalCostProductionCenter = (select sum(InitialDistribution) from Cost.CostEstimationNative where ProductionCenterId = @ProductionCenterId and ('01/' + RIGHT('0'+cast([Month] as varchar(20)),2)+ '/' + cast([Year] as varchar(20))) between @InitialDate and @EndDate)
	---Tabla para alamcenar las cantidades reealizadas por la unidad de medida
	declare @TableDataMeasurement table(Id int, Code varchar(20), [Name] varchar(300), ValueCost numeric(18,2), Quantity int, TotalValue numeric(18,2), [Percentage] decimal, TotalCostValue numeric(18,2), UnitValueCost numeric(18,2), MainAccountCode varchar(20), MainAccountName varchar(120), DistributedCosts bit)

	--Agrego el centro de Produccion para reflejarlo en el reporte
	--insert into @TableDataMeasurement(Id, Code, [Name], ValueCost, Quantity, TotalValue, [Percentage], TotalCostValue, UnitValueCost)
	insert into @TableDataMeasurement
	select lrd.InventoryMeasurementUnitId, mu.Code, mu.[Name], mu.CostValue, sum(lrd.[Count]), sum(lrd.[Count]) * mu.CostValue, 0, 0, 0, NULL, NULL, 0
	from Cost.CostLogisticsProductionCenterRecordDetail lrd
	inner join Cost.CostLogisticsProductionCenterRecord lr on lr.Id = lrd.LogisticsProductionCenterRecordId
	inner join Cost.CostProductionCenter as pc on pc.Id = lr.ProductionCenterId and pc.[Status] = 1
	inner join Inventory.InventoryMeasurementUnit mu on mu.Id = lrd.InventoryMeasurementUnitId
	where cast(lr.RecordDate as date) between cast(@InitialDate as varchar(20)) and cast(@EndDate as varchar(20))  and lr.ProductionCenterId = cast(@ProductionCenterId as varchar(10))
	group by lrd.InventoryMeasurementUnitId, mu.CostValue, mu.Code, mu.[Name]
	union All
	select ddcd.MeasurementUnitId, mu.Code, mu.[Name], ddcd.CostValue, sum(ddcd.[Count]),SUM( ddcd.[Value]), 0, 0, 0, Cue.Number, Cue.[Name], 1
	from Cost.CostDistributionDirectCost ddc
	inner join Cost.CostDistributionDirectCostDetail as ddcd on ddcd.DistributionDirectCostId = ddc.Id
	inner join Cost.CostProductionCenter as pc on pc.Id = ddcd.ProductionCenterId and pc.[Status] = 1
	inner join GeneralLedger.MainAccounts as cue on cue.Id = ddcd.MainAccountId
	inner join Inventory.InventoryMeasurementUnit as mu on mu.Id = ddcd.MeasurementUnitId
	where ddc.Year between cast(@InitialYear as varchar(4)) and cast(@EndYear as varchar(4)) and ddc.[Month] between cast(@InitialMonth as varchar(2)) and cast(@EndMonth as varchar(2)) and pc.Id = cast(@ProductionCenterId as varchar(10))
	group by ddcd.MeasurementUnitId, ddcd.CostValue, mu.Code, mu.[Name], Cue.Number, Cue.[Name]

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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el informe de costos por unidad de medida para un centro de producción logístico en un rango de meses y años determinado. Consolida dos fuentes de información: los movimientos logísticos registrados en los actas del centro de producción (insumos consumidos con su unidad de medida y cantidad) y los costos directos distribuidos por cuenta contable y unidad de medida. Para cada unidad de medida calcula la cantidad total, el valor total, el porcentaje de participación sobre el total, el costo distribuido proporcional (tomando como base la estimación nativa del centro de producción) y el costo unitario resultante. Permite filtrar por rango de códigos de unidad de medida y es utilizado en la reportería de costos para analizar cuánto representa cada unidad de medida (unidad, caja, frasco, etc.) dentro del costo total del centro de producción en el período seleccionado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'spCostReportCostMeasurementUnit';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'spCostReportCostMeasurementUnit';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un informe de costos por unidad de medida para un centro de producción en un rango de meses, distribuyendo el costo total estimado proporcionalmente según el valor consumido.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'spCostReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de producción debe existir y tener estado activo (Status = 1).; Debe existir al menos un registro en Cost.CostEstimationNative para el centro de producción y rango de fechas para que el costo total no sea NULL.; Las unidades de medida y cuentas contables referenciadas deben existir en sus catálogos.; El rango de meses/años debe ser válido para construir fechas (día 01 + mes + año).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'spCostReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El costo total a distribuir proviene exclusivamente de la suma de InitialDistribution en CostEstimationNative para el centro y rango.; La fecha final se ajusta al último día del mes indicado (DATEADD día -1 al primer día del mes siguiente).; Solo se incluyen centros de producción con Status = 1.; El porcentaje de cada unidad de medida es proporcional a su TotalValue respecto al TotalValue global del periodo.; El costo unitario se obtiene dividiendo el costo distribuido entre la cantidad consumida.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'spCostReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Unidad de medida; Costo logístico; Distribución de costos directos; Estimación de costos; Cuenta contable principal; Costo unitario; Porcentaje de participación', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'spCostReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableDataMeasurement: Inserta consumos logísticos agrupados por unidad de medida cuando lr.RecordDate está entre InitialDate y EndDate y el centro de producción coincide y está activo, con DistributedCosts=0.; [INSERT] @TableDataMeasurement: Inserta costos directos distribuidos agrupados por unidad de medida y cuenta contable cuando Year y Month están en el rango y el centro de producción coincide y está activo, con DistributedCosts=1.; [UPDATE] @TableDataMeasurement: Calcula Percentage = round(TotalValue*100/SUM(TotalValue),1), TotalCostValue = (TotalValue/SUM(TotalValue))*TotalCostProductionCenter y UnitValueCost = TotalCostValue/Quantity para cada unidad de medida.; [RETURN_RESULT] @TableDataMeasurement: Devuelve los registros cuyo Code esté entre InitialMeasurementUnitCode (default ''0'') y EndMeasurementUnitCode (default ''Z''), ordenados por Code.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'spCostReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen del registro: detalle logístico vs distribución de costos directos → Si proviene de CostLogisticsProductionCenterRecordDetail se marca DistributedCosts=0 y sin cuenta contable; si proviene de CostDistributionDirectCostDetail se marca DistributedCosts=1 y se asocia cuenta contable principal.; si Códigos de unidad de medida no proporcionados → Aplica límites por defecto ''0'' y ''Z'' usando ISNULL para no filtrar resultados.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'spCostReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostEstimationNative; Cost.CostLogisticsProductionCenterRecordDetail; Cost.CostLogisticsProductionCenterRecord; Cost.CostProductionCenter; Inventory.InventoryMeasurementUnit; Cost.CostDistributionDirectCost; Cost.CostDistributionDirectCostDetail; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'spCostReportCostMeasurementUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'spCostReportCostMeasurementUnit';
-- GO
