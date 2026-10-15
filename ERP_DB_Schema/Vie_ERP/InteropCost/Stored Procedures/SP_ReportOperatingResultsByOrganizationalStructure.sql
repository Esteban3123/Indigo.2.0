
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