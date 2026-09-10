-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 04/11/2016
-- Description:	Store para el reporte de resultado de operaciones
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_ReportOperatingResultGrouped]
	@InitialMonth int,
	@EndMonth int,
	@Year int,
	@Container varchar(20),
	@CodePCenterIni varchar(50),
    @CodePCenterFin varchar(50),
    @StructureOfCostId int
AS
BEGIN

	if @CodePCenterFin = ''
		Begin
			set @CodePCenterFin = 'zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz'
		End
	
	declare @StringSelect nvarchar(max)
	declare @TableBillingValue table(ProductionCenterId int, Value numeric(18,0))

	set @StringSelect = 'select hm.ProductionCenterId,abs(sum(CSCDEBITO - CSCCREDITO)) from '+ @Container +'.dbo.CTNSAL'+ cast(@Year as varchar(20)) +' sal
	inner join '+ @Container +'.dbo.CTNCUENTA cue on cue.OID = sal.CTNCUENTA
	inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = cue.CUECODIGO and hm.HomologationType = 6
	where CSCMES >= '+ cast(@InitialMonth as varchar(20)) +' and CSCMES <= '+ CAST(@EndMonth as varchar(20)) +'
	group by hm.ProductionCenterId'
	
	insert into @TableBillingValue
	exec sp_executesql @StringSelect

	select 
	pc.Code + ' - ' + pc.Name as CostCenter,
	pc.Code as CostCenterCode,
	pc.Name as CostCenterName,
	cast(ce.SecondaryDistribution  as decimal (20,2) ) as TotalCost,
	cast(isnull(bv.Value,0) as decimal (20,2) ) as BillingValue,
	cast(isnull(bv.Value,0) - ce.SecondaryDistribution  as decimal (20,2) ) as Diference,
	cast((isnull(bv.Value,0) - ce.SecondaryDistribution) * 100 / case isnull(ce.SecondaryDistribution,0) when 0 then 1 else isnull(ce.SecondaryDistribution,0) end as decimal (20,2) ) as Margin,
	cast((isnull(bv.Value,0) - ce.SecondaryDistribution) * 100 / case isnull(bv.Value,0) when 0 then 1 else isnull(bv.Value,0) end as decimal (20,2) ) as Utility
	from InteropCost.ProductionCenter pc
	inner join InteropCost.CostEstimation ce on ce.ProductionCenterId = pc.Id
	left join @TableBillingValue bv on bv.ProductionCenterId = pc.Id
	where ce.[Month] >= @InitialMonth and ce.[Month] <= @EndMonth and ce.[Year] = @Year and pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin
	--and pc.OrganizationalStructureOfCostId In (select id from [InteropCost].[OrganizationalStructureOfCosts] as oec where oec.Id = @StructureOfCostId or oec.[ParentId] = @StructureOfCostId)
	and pc.OrganizationalStructureOfCostId In (select id from [InteropCost].[OrganizationalStructureOfCosts] as oec where @StructureOfCostId = case @StructureOfCostId when 0 then @StructureOfCostId else oec.Id end or @StructureOfCostId = case @StructureOfCostId when 0 then @StructureOfCostId else oec.[ParentId] end)
	order by (isnull(bv.Value,0) - ce.SecondaryDistribution) * 100 / case isnull(bv.Value,0) when 0 then 1 else isnull(bv.Value,0) end desc

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de resultado operativo agrupado por centro de costo: calcula y compara, para un rango de meses y año seleccionado, el costo total distribuido (distribución secundaria) contra el valor facturado de cada centro de producción, obteniendo la diferencia, el margen y la utilidad porcentual. Cruza la estimación de costos del módulo InteropCost con los valores de facturación extraídos dinámicamente de la contabilidad del contenedor (empresa) indicado, permitiendo filtrar por rango de código de centro de costo y por estructura organizacional de costos. Sirve para que la dirección financiera evalúe la rentabilidad operativa de cada unidad o área de la institución en un período determinado.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportOperatingResultGrouped';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportOperatingResultGrouped';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de resultado operativo por centro de producción comparando el costo distribuido (CostEstimation.SecondaryDistribution) contra el valor facturado obtenido de la contabilidad externa, calculando diferencia, margen y utilidad.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la base de datos contable indicada en @Container con las tablas dbo.CTNSAL{Year} y dbo.CTNCUENTA.; Debe existir la tabla CTNSAL correspondiente al año @Year (concatenación dinámica del nombre).; Los centros de producción deben tener homologación de tipo 6 para poder asociar cuentas contables con valor de facturación.; Debe existir CostEstimation para el rango de meses y año consultados para que el centro aparezca en el reporte.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor de facturación se obtiene como el valor absoluto de la suma (DEBITO - CREDITO) de los movimientos contables del año y rango de meses indicado.; Solo se consideran cuentas contables con homologación tipo 6 para obtener el valor de facturación por centro de producción.; La tabla de saldos contables consultada se determina dinámicamente por año (CTNSAL + @Year), por lo que cada año tiene su propia tabla física.; Cuando un centro de producción no tiene movimiento de facturación, se asume BillingValue = 0 (ISNULL).; El reporte solo incluye centros que tienen registro de CostEstimation dentro del rango de mes/año solicitado.; El resultado se ordena descendentemente por el porcentaje de utilidad.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'centro de producción; centro de costo; estructura organizacional de costos; homologación contable; cuenta contable; distribución secundaria de costos; valor de facturación; margen; utilidad; estimación de costos', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableBillingValue: Se inserta el valor absoluto de (CSCDEBITO - CSCCREDITO) agrupado por ProductionCenterId, tomado de CTNSAL{Year} unido con CTNCUENTA y ProductionCenterHomologation (HomologationType=6) filtrando CSCMES entre @InitialMonth y @EndMonth.; [RETURN_RESULT] InteropCost.ProductionCenter: Devuelve por centro de producción: CostCenter (Code+Name), CostCenterCode, CostCenterName, TotalCost, BillingValue, Diference, Margin y Utility, filtrado por rango de código de centro, mes/año de CostEstimation y estructura organizacional (con soporte de un nivel padre).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodePCenterFin viene vacío ('''') → Se reemplaza por una cadena de ''z'' de máxima longitud para que actúe como cota superior abierta en el filtro pc.Code <= @CodePCenterFin; si @StructureOfCostId = 0 → La condición sobre OrganizationalStructureOfCostId se neutraliza (0=0) y no filtra por estructura organizacional else Filtra centros cuya OrganizationalStructureOfCostId coincida con @StructureOfCostId o cuyo ParentId sea @StructureOfCostId (incluye un nivel de jerarquía); si SecondaryDistribution = 0 o NULL al calcular Margin → Se usa 1 como divisor para evitar división por cero; si BillingValue (bv.Value) = 0 o NULL al calcular Utility → Se usa 1 como divisor para evitar división por cero', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.ProductionCenter; InteropCost.CostEstimation; InteropCost.ProductionCenterHomologation; InteropCost.OrganizationalStructureOfCosts; CTNSAL{Year}; CTNCUENTA', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultGrouped';
-- GO
