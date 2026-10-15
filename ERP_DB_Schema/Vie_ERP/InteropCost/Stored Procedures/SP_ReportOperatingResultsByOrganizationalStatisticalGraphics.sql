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