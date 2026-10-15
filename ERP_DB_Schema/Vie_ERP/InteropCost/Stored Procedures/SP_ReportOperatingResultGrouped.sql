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