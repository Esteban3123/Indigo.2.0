-- Author:		Cristhian Mauricio Salazar
-- Create date: 08/11/2016
-- Description:	Store para el reporte de resultado de operaciones
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_ReportOperatingResultsByOrganizationalStatisticalGraphics]
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
		set @CodePCenterFin = 'z'
	End
	
declare @StringSelect nvarchar(max)
declare @TableBillingValue table(ProductionCenterId int, CostCenterId int, Value numeric(18,0))

set @StringSelect = 'select hm.ProductionCenterId, pccc.CostCenterId,abs(sum(CSCDEBITO - CSCCREDITO)) from '+ @Container +'.dbo.CTNSAL'+ cast(@Year as varchar(20)) +' sal
inner join '+ @Container +'.dbo.CTNCUENTA cue on cue.OID = sal.CTNCUENTA
inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = cue.CUECODIGO and hm.HomologationType = 6
inner join InteropCost.ProductionCenterCostCenter pccc on pccc.ProductionCenterId = hm.ProductionCenterId and pccc.CostCenterId = sal.CTNCENCOS
where CSCMES >= '+ cast(@InitialMonth as varchar(20)) +' and CSCMES <= '+ CAST(@EndMonth as varchar(20)) +'
group by hm.ProductionCenterId, pccc.CostCenterId'
	
insert into @TableBillingValue
exec sp_executesql @StringSelect

--- Total de valor facturado
declare @TotalBillingValue numeric(20,0) = (
select sum(ISNULL(b.Value,0))
from InteropCost.ProductionCenter pc
inner join InteropCost.CostEstimation ce on ce.ProductionCenterId = pc.Id
left join (select ProductionCenterId, sum(Value) as Value from @TableBillingValue group by ProductionCenterId) b on b.ProductionCenterId = pc.Id
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

select 
null as CostCenter,
osc.Code + ' - ' + osc.Name as Name,
b.Value as BillingValue,
b.Value * 100 / @TotalBillingValue as BillingPercentage,
ce.SecondaryDistribution as CostValue,
ce.SecondaryDistribution * 100 / @TotalCost as CostPercentage,
b.Value - ce.SecondaryDistribution as Diference,
(b.Value - ce.SecondaryDistribution) * 100 / b.Value as Utility
from InteropCost.ProductionCenter pc
inner join InteropCost.CostEstimation ce on ce.ProductionCenterId = pc.Id
inner join InteropCost.OrganizationalStructureOfCosts osc on osc.Id = pc.OrganizationalStructureOfCostId 
left join (select ProductionCenterId, sum(Value) as Value from @TableBillingValue group by ProductionCenterId) b on b.ProductionCenterId = pc.Id
where ce.[Month] >= @InitialMonth and ce.[Month] <= @EndMonth and ce.[Year] = @Year and pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin
--and (pc.OrganizationalStructureOfCostId In (select id from [InteropCost].[OrganizationalStructureOfCosts] as oec where oec.[ParentId] = @StructureOfCostId) OR pc.OrganizationalStructureOfCostId = @StructureOfCostId)
and (pc.OrganizationalStructureOfCostId In (select * from InteropCost.fnRecursiveStructure(case @StructureOfCostId when 0 then 1 else @StructureOfCostId end)) OR pc.OrganizationalStructureOfCostId = @StructureOfCostId)
group by osc.Code, osc.Name, b.Value, ce.SecondaryDistribution

End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte gráfico de resultados operativos por estructura organizacional de costos. Calcula y compara, para un rango de meses y año dados, el valor facturado versus el costo total (distribución secundaria) por centro de producción, agrupado según la jerarquía organizacional de costos. Permite visualizar el margen operativo (diferencia y porcentaje de utilidad) de cada unidad productiva, filtrando por rango de códigos de centro de producción y por nodo de estructura organizacional (incluyendo sus hijos de forma recursiva). Compone los datos cruzando la facturación real obtenida dinámicamente desde tablas contables del contenedor indicado, con las estimaciones de costo registradas en CostEstimation y la jerarquía de OrganizationalStructureOfCosts.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera datos para gráfico estadístico de resultado de operaciones por estructura organizacional de costos, comparando valores facturados (desde la contabilidad externa) con costos distribuidos por centro de producción en un rango de meses.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La base de datos contable externa identificada por @Container debe existir y contener las tablas CTNSAL{Year} y CTNCUENTA.; Debe existir homologación en InteropCost.ProductionCenterHomologation con HomologationType = 6 para mapear cuentas contables a centros de producción.; @Year debe corresponder al sufijo de una tabla CTNSAL existente.; Debe existir información en InteropCost.CostEstimation para el rango de meses y año solicitados.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor facturado se obtiene siempre como valor absoluto de (débito - crédito) contable.; Solo se consideran cuentas con HomologationType = 6 al homologar cuentas contables con centros de producción.; Los porcentajes (BillingPercentage, CostPercentage) nunca producen división por cero por la sustitución de 0 a 1 en los totales.; El filtro de centros de producción se restringe al rango pc.Code entre @CodePCenterIni y @CodePCenterFin.; La estructura organizacional considerada incluye al @StructureOfCostId y todos sus descendientes vía fnRecursiveStructure (excepto en el cálculo de @TotalBillingValue y @TotalCost donde solo se consideran hijos directos vía ParentId).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Resultado de operaciones; Valor facturado; Distribución secundaria de costos; Estructura organizacional de costos; Homologación contable; Utilidad; Período mensual contable', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado tabular: Devuelve por cada estructura organizacional de costos: nombre, valor facturado, % de facturación sobre el total, costo (SecondaryDistribution), % de costo, diferencia (facturado - costo) y utilidad porcentual ((facturado - costo) * 100 / facturado).; [INSERT] @TableBillingValue: Inserta el valor facturado por centro de producción y centro de costo calculado como abs(sum(CSCDEBITO - CSCCREDITO)) de CTNSAL{Year} para los meses CSCMES entre @InitialMonth y @EndMonth, agrupado por ProductionCenterId y CostCenterId.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodePCenterFin = '''' → Asigna @CodePCenterFin = ''z'' para que el filtro pc.Code <= @CodePCenterFin no excluya registros.; si @TotalBillingValue = 0 → Se reasigna a 1 para evitar división por cero en el cálculo de BillingPercentage.; si @TotalCost = 0 → Se reasigna a 1 para evitar división por cero en el cálculo de CostPercentage.; si @StructureOfCostId = 0 (en la consulta final) → Se invoca fnRecursiveStructure(1) como raíz de la jerarquía. else Se invoca fnRecursiveStructure(@StructureOfCostId) para obtener todos los descendientes en la jerarquía.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'InteropCost.fnRecursiveStructure', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.ProductionCenterHomologation; InteropCost.ProductionCenterCostCenter; InteropCost.ProductionCenter; InteropCost.CostEstimation; InteropCost.OrganizationalStructureOfCosts; InteropCost.fnRecursiveStructure', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
