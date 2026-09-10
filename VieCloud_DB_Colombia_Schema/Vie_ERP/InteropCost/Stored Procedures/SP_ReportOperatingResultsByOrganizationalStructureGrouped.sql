
-- Author:		Cristhian Mauricio Salazar
-- Create date: 04/11/2016
-- Description:	Store para el reporte de resultado de operaciones
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_ReportOperatingResultsByOrganizationalStructureGrouped]
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

--- Total de valor facturado
declare @TotalBillingValue numeric(20,0) = (
select sum(ISNULL(b.Value,0))
from InteropCost.ProductionCenter pc
inner join InteropCost.CostEstimation ce on ce.ProductionCenterId = pc.Id
left join @TableBillingValue b on b.ProductionCenterId = pc.Id
where ce.[Month] >= @InitialMonth and ce.[Month] <= @EndMonth and ce.[Year] = @Year and pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin
and (pc.OrganizationalStructureOfCostId In (select id from [InteropCost].[OrganizationalStructureOfCosts] as oec where oec.[ParentId] = @StructureOfCostId) OR pc.OrganizationalStructureOfCostId = @StructureOfCostId)
)

if @TotalBillingValue = 0 begin
	set @TotalBillingValue = 1
end

--- Total de costos 
declare @TotalCost numeric(20,0) = (
select sum(ISNULL(ce.SecondaryDistribution,0))
from InteropCost.ProductionCenter pc
inner join InteropCost.CostEstimation ce on ce.ProductionCenterId = pc.Id
where ce.[Month] >= @InitialMonth and ce.[Month] <= @EndMonth and ce.[Year] = @Year and pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin
and (pc.OrganizationalStructureOfCostId In (select id from [InteropCost].[OrganizationalStructureOfCosts] as oec where oec.[ParentId] = @StructureOfCostId) OR pc.OrganizationalStructureOfCostId = @StructureOfCostId)
)

if @TotalCost = 0 begin
	set @TotalCost = 1
end

select pc.Code + ' - ' + pc.Name as CostCenter,
pc.Code as CostCenterCode,
pc.Name as CostCenterName,
b.Value as BillingValue,
b.Value * 100 / @TotalBillingValue as BillingPercentage,
ce.SecondaryDistribution as CostValue,
ce.SecondaryDistribution * 100 / @TotalCost as CostPercentage,
b.Value - ce.SecondaryDistribution as Diference,
(b.Value - ce.SecondaryDistribution) * 100 / b.Value as Utility
from InteropCost.ProductionCenter pc
inner join InteropCost.CostEstimation ce on ce.ProductionCenterId = pc.Id
left join @TableBillingValue b on b.ProductionCenterId = pc.Id
where ce.[Month] >= @InitialMonth and ce.[Month] <= @EndMonth and ce.[Year] = @Year and pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin
--and pc.OrganizationalStructureOfCostId In (select id from [InteropCost].[OrganizationalStructureOfCosts] as oec where oec.Id = @StructureOfCostId or oec.[ParentId] = @StructureOfCostId)
and pc.OrganizationalStructureOfCostId In (select * from InteropCost.fnRecursiveStructure(case @StructureOfCostId when 0 then 1 else @StructureOfCostId end))

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de resultados operativos (ingresos vs. costos) por centro de producción, agrupado según la estructura organizacional de costos de la institución. Cruza el valor facturado —obtenido dinámicamente desde la contabilidad del contenedor indicado— con el costo distribuido (distribución secundaria) de cada centro de producción para un rango de meses y año seleccionados. Para cada centro de costo dentro de la jerarquía organizacional solicitada, calcula el valor facturado, el porcentaje sobre el total facturado, el costo, el porcentaje sobre el total de costos, la diferencia (utilidad bruta) y el margen de utilidad porcentual. Es el procedimiento principal del módulo de costos para el análisis de rentabilidad y resultados financieros por unidad productiva o área de la organización.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportOperatingResultsByOrganizationalStructureGrouped';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportOperatingResultsByOrganizationalStructureGrouped';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de resultados de operación (facturación vs costos, diferencia y utilidad %) por centro de costo, agrupado bajo una estructura organizacional de costos y su jerarquía descendente, para un rango de meses y centros.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructureGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la base de datos contable indicada por @Container con las tablas CTNSAL{Año} y CTNCUENTA accesibles vía SQL dinámico.; @Year se concatena al nombre de la tabla CTNSAL para apuntar al año contable correspondiente.; Deben existir homologaciones en InteropCost.ProductionCenterHomologation con HomologationType=6 que vinculen cuentas contables (CUECODIGO) con centros de producción.; Debe existir información en InteropCost.CostEstimation para el período (mes/año) y centros consultados.; La función InteropCost.fnRecursiveStructure debe poder resolver la jerarquía descendente desde el nodo raíz.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructureGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los totales (facturación y costo) se calculan únicamente sobre centros cuya OrganizationalStructureOfCostId coincide con @StructureOfCostId o tiene a éste como ParentId (un solo nivel), mientras que el detalle final usa la jerarquía recursiva completa vía fnRecursiveStructure.; Sólo se consideran homologaciones con HomologationType = 6 para vincular cuentas contables con centros de producción.; El valor facturado se obtiene como valor absoluto del saldo contable (|débito - crédito|).; El rango de códigos de centro de producción es inclusivo (>= @CodePCenterIni y <= @CodePCenterFin).; Si @StructureOfCostId = 0 el reporte se calcula desde la estructura raíz (Id=1).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructureGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción / centro de costo; Estructura organizacional de costos jerárquica; Homologación contable de cuentas a centros de costo; Distribución secundaria de costos; Valor facturado vs costo; Utilidad operacional; Saldo contable (débito-crédito) por mes', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructureGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un conjunto con CostCenter (código+nombre), valor facturado, % de facturación sobre el total, costo (SecondaryDistribution), % de costo sobre el total, diferencia (facturado-costo) y utilidad % ((facturado-costo)/facturado*100).; [INSERT] @TableBillingValue: Inserta por cada ProductionCenterId el valor absoluto de SUM(CSCDEBITO-CSCCREDITO) de CTNSAL{Year} filtrado por CSCMES entre @InitialMonth y @EndMonth, uniendo CTNCUENTA con homologaciones de tipo 6.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructureGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodePCenterFin = '''' → Se reemplaza por una cadena de ''z'' de longitud máxima para actuar como cota superior abierta en el rango de códigos de centro.; si @TotalBillingValue = 0 → Se fuerza @TotalBillingValue = 1 para evitar división por cero en BillingPercentage.; si @TotalCost = 0 → Se fuerza @TotalCost = 1 para evitar división por cero en CostPercentage.; si @StructureOfCostId = 0 al invocar fnRecursiveStructure → Se sustituye por 1 (estructura raíz por defecto) para obtener la jerarquía completa. else Se usa el @StructureOfCostId recibido como nodo raíz de la recursión.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructureGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'InteropCost.fnRecursiveStructure', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructureGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.ProductionCenterHomologation; InteropCost.ProductionCenter; InteropCost.CostEstimation; InteropCost.OrganizationalStructureOfCosts; InteropCost.fnRecursiveStructure; CTNSAL{Year}; CTNCUENTA', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructureGrouped';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructureGrouped';
-- GO
