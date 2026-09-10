
-- Author:		Cristhian Mauricio Salazar
-- Create date: 04/11/2016
-- Description:	Store para el reporte de resultado de operaciones
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_ReportOperatingResultsByOrganizationalStructure]
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

set @StringSelect = 'select hm.ProductionCenterId, pccc.CostCenterId,isnull(case cla.CLANATURA when 1 then sum(isnull(sal.CSCDEBITO,0)) - sum(isnull(sal.CSCCREDITO,0)) else sum(isnull(sal.CSCCREDITO,0)) - sum(isnull(sal.CSCDEBITO,0)) end,0) from '+ @Container +'.dbo.CTNSAL'+ cast(@Year as varchar(20)) +' sal
inner join '+ @Container +'.dbo.CTNCUENTA cue on cue.OID = sal.CTNCUENTA
inner join ' + @Container + '.dbo.CTNCLASE cla on cla.OID = cue.CTNCLASE
inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = cue.CUECODIGO and hm.HomologationType = 6 
inner join InteropCost.ProductionCenter pc on pc.Id = hm.ProductionCenterId 
inner join InteropCost.ProductionCenterCostCenter pccc on pccc.ProductionCenterId = hm.ProductionCenterId and pccc.CostCenterId = sal.CTNCENCOS
where CSCMES >= '+ cast(@InitialMonth as varchar(20)) +' and CSCMES <= '+ CAST(@EndMonth as varchar(20)) +' and pc.CenterType = 1 
group by hm.ProductionCenterId, pccc.CostCenterId, cla.CLANATURA'
	
insert into @TableBillingValue
exec sp_executesql @StringSelect

--- Total de valor facturado
declare @TotalBillingValue numeric(20,0) = (
select sum(ISNULL(b.Value,0))
from InteropCost.ProductionCenter pc
left join (select ProductionCenterId, sum(Value) as Value from @TableBillingValue group by ProductionCenterId) b on b.ProductionCenterId = pc.Id
where pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin and pc.CenterType = 1
and pc.OrganizationalStructureOfCostId In (select * from InteropCost.fnRecursiveStructure(case @StructureOfCostId when 0 then 1 else @StructureOfCostId end))
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
and pc.OrganizationalStructureOfCostId In (select * from InteropCost.fnRecursiveStructure(case @StructureOfCostId when 0 then 1 else @StructureOfCostId end))
)

if @TotalCost = 0 begin
	set @TotalCost = 1
end
print ':P'
print @TotalBillingValue
select pc.Code + ' - ' + pc.Name as CostCenter,
b.Value as BillingValue,
round((isnull(b.Value,0) * 100.00 / @TotalBillingValue), 3) as BillingPercentage,
cast(ce.SecondaryDistribution as decimal (20,2)) as CostValue,
round((ce.SecondaryDistribution * 100 / @TotalCost), 3) as CostPercentage,
isnull(b.Value,0) - isnull(ce.SecondaryDistribution,0) as Diference,
(isnull(b.Value,0) - isnull(ce.SecondaryDistribution,0)) * 100 / b.Value as Utility
from InteropCost.ProductionCenter pc
inner join (select ProductionCenterId, sum(SecondaryDistribution) as SecondaryDistribution from InteropCost.CostEstimation where [Month] >= @InitialMonth and [Month] <= @EndMonth and [Year] = @Year group by ProductionCenterId) ce on ce.ProductionCenterId = pc.Id
left join (select ProductionCenterId, sum(Value) as Value from @TableBillingValue group by ProductionCenterId) b on b.ProductionCenterId = pc.Id
where pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin and pc.CenterType = 1
--and pc.OrganizationalStructureOfCostId In (select id from [InteropCost].[OrganizationalStructureOfCosts] as oec where oec.Id = @StructureOfCostId or oec.[ParentId] = @StructureOfCostId)
and pc.OrganizationalStructureOfCostId In (select * from InteropCost.fnRecursiveStructure(case @StructureOfCostId when 0 then 1 else @StructureOfCostId end))

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de resultados operativos por estructura organizacional de costos. Compara, para un rango de meses y año indicados, el valor facturado (obtenido de las cuentas contables homologadas a centros de producción) versus el costo total distribuido (tomado de la estimación de costos con distribución secundaria), calculando para cada centro de producción el porcentaje de facturación, el porcentaje de costo, la diferencia y la utilidad. Permite filtrar por rango de códigos de centro de producción y por nodo de estructura organizacional de costos, recorriendo la jerarquía de forma recursiva mediante la función fnRecursiveStructure. Sirve para el análisis financiero y de rentabilidad operativa por unidad productiva dentro del módulo de interoperabilidad de costos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportOperatingResultsByOrganizationalStructure';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de resultados de operación por estructura organizacional comparando el valor facturado (desde el módulo contable externo) contra los costos distribuidos, calculando porcentajes de participación, diferencia y utilidad por centro de producción.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la tabla CTNSAL{Year} en la base contable externa indicada por @Container (e.g. CTNSAL2016); Debe existir homologación en InteropCost.ProductionCenterHomologation con HomologationType = 6 entre cuentas contables (CUECODIGO) y centros de producción; Los centros de producción considerados deben tener CenterType = 1; La función InteropCost.fnRecursiveStructure debe poder resolver la jerarquía a partir de @StructureOfCostId (o de 1 si es 0); Debe existir información en InteropCost.CostEstimation para el rango Month/Year solicitado', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran centros de producción con CenterType = 1; La homologación contable usada es exclusivamente la de tipo 6 (HomologationType = 6); El rango de meses aplica tanto a la facturación (CSCMES) como a los costos (CostEstimation.Month) usando los mismos límites @InitialMonth/@EndMonth; El nombre de la tabla contable se construye dinámicamente como CTNSAL concatenado con @Year; Los porcentajes se redondean a 3 decimales; Los totales usados como denominadores nunca son 0 (se sustituyen por 1)', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Estructura organizacional de costos; Homologación contable; Valor facturado; Distribución secundaria de costos; Naturaleza contable (débito/crédito); Utilidad operativa; Resultado de operaciones', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableBillingValue: Se inserta el valor facturado por (ProductionCenterId, CostCenterId) calculado desde CTNSAL{Year}: si CLANATURA=1 entonces (DEBITO - CREDITO), de lo contrario (CREDITO - DEBITO), filtrado por CSCMES entre @InitialMonth y @EndMonth y CenterType=1; [RETURN_RESULT] ResultSet: Devuelve por centro de producción: Code+Name, BillingValue, BillingPercentage (Value*100/TotalBillingValue), CostValue, CostPercentage, Diference (Value - SecondaryDistribution) y Utility ((Value - SecondaryDistribution)*100/Value), filtrando pc.Code entre @CodePCenterIni y @CodePCenterFin, CenterType=1 y dentro de la estructura recursiva', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodePCenterFin = '''' → Se asigna ''z'' como límite superior del rango de códigos para incluir todos los centros desde @CodePCenterIni; si @StructureOfCostId = 0 → Se usa 1 como raíz de la jerarquía al invocar fnRecursiveStructure else Se usa @StructureOfCostId tal cual; si cla.CLANATURA = 1 → El valor facturado se calcula como SUM(DEBITO) - SUM(CREDITO) else El valor facturado se calcula como SUM(CREDITO) - SUM(DEBITO); si @TotalBillingValue = 0 → Se fuerza a 1 para evitar división por cero en el cálculo de BillingPercentage; si @TotalCost = 0 → Se fuerza a 1 para evitar división por cero en el cálculo de CostPercentage', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'InteropCost.fnRecursiveStructure', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.ProductionCenterHomologation; InteropCost.ProductionCenter; InteropCost.ProductionCenterCostCenter; InteropCost.CostEstimation; InteropCost.fnRecursiveStructure', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultsByOrganizationalStructure';
-- GO
