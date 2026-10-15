
-- =============================================
-- Author:		Carlos Jhefersson Muñoz Ramirez
-- Create date: 06/05/2017
-- Description:	Store para el reporte de resultado de operaciones 
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_ReportOperatingResultProductionCenter]
	@InitialMonth int,
	@EndMonth int,
	@Year int,
	@Container varchar(20),
	@CodePCenterIni varchar(50),
    @CodePCenterFin varchar(50)
AS
BEGIN

	if @CodePCenterFin = ''
		Begin
			set @CodePCenterFin = 'z'
		End
	
	declare @StringSelect nvarchar(max)
	declare @StringSelect12 nvarchar(max)
	declare @StringSelect12AND nvarchar(max)
	declare @TableBillingValue table(ProductionCenterId int, CostCenterId int, [Value] numeric(18,0))

	set @StringSelect = 'select hm.ProductionCenterId,pccc.CostCenterId,abs(sum(CSCCREDITO - CSCDEBITO)) from '+ @Container +'.dbo.CTNSAL'+ cast(@Year as varchar(20)) +' sal
	inner join '+ @Container +'.dbo.CTNCUENTA cue on cue.OID = sal.CTNCUENTA
	inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = cue.CUECODIGO and hm.HomologationType = 6
	inner join InteropCost.ProductionCenterCostCenter pccc on pccc.ProductionCenterId = hm.ProductionCenterId and pccc.CostCenterId = sal.CTNCENCOS
	where CSCMES >= '+ cast(@InitialMonth as varchar(20)) +' and CSCMES <= '+ CAST(@EndMonth as varchar(20)) +'
	group by hm.ProductionCenterId, pccc.CostCenterId'


	set @StringSelect12 = 'select hm.ProductionCenterId,pccc.CostCenterId,sum(MOV.CMMVALCRE -MOV.CMMVALDEB) from '+ @Container +'.dbo.CTNCOMD'+ cast(@Year as varchar(20)) +' as Mov 
	inner join '+ @Container +'.dbo.CTNCOM'+ cast(@Year as varchar(20)) +' as CMov on Cmov.OID = Mov.CtNCOMCONC
	inner join '+ @Container +'.dbo.CTNCUENTA as MA on MA.OID = Mov.CTNCUENTA
	inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = MA.CUECODIGO and hm.HomologationType = 6
	inner join InteropCost.ProductionCenterCostCenter pccc on pccc.ProductionCenterId = hm.ProductionCenterId and pccc.CostCenterId = MOV.CTNCENCOS
	where CMov.CTNTIPCOM <> 67 and Month(COMFECCOM) = 12 and MA.CTNCLASE = 5
	group by hm.ProductionCenterId, pccc.CostCenterId'

	set @StringSelect12AND = 'select hm.ProductionCenterId,pccc.CostCenterId,abs(sum(CSCDEBITO - CSCCREDITO)) from '+ @Container +'.dbo.CTNSAL'+ cast(@Year as varchar(20)) +' sal
	inner join '+ @Container +'.dbo.CTNCUENTA cue on cue.OID = sal.CTNCUENTA
	inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = cue.CUECODIGO and hm.HomologationType = 6
	inner join InteropCost.ProductionCenterCostCenter pccc on pccc.ProductionCenterId = hm.ProductionCenterId and pccc.CostCenterId = sal.CTNCENCOS
	where CSCMES >= '+ cast(@InitialMonth as varchar(20)) +' and CSCMES <= 11
	group by hm.ProductionCenterId, pccc.CostCenterId
	UNION ALL
	select hm.ProductionCenterId,pccc.CostCenterId,sum(MOV.CMMVALCRE -MOV.CMMVALDEB) from '+ @Container +'.dbo.CTNCOMD'+ cast(@Year as varchar(20)) +' as Mov 
	inner join '+ @Container +'.dbo.CTNCOM'+ cast(@Year as varchar(20)) +' as CMov on Cmov.OID = Mov.CtNCOMCONC
	inner join '+ @Container +'.dbo.CTNCUENTA as MA on MA.OID = Mov.CTNCUENTA
	inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = MA.CUECODIGO and hm.HomologationType = 6
	inner join InteropCost.ProductionCenterCostCenter pccc on pccc.ProductionCenterId = hm.ProductionCenterId and pccc.CostCenterId = MOV.CTNCENCOS
	where CMov.CTNTIPCOM <> 67 and Month(COMFECCOM) = 12 and MA.CTNCLASE = 5
	group by hm.ProductionCenterId, pccc.CostCenterId'
		

	if @EndMonth = 12 and @InitialMonth = 12 begin
		insert into @TableBillingValue
		exec sp_executesql @StringSelect12
	end
	else if @EndMonth = 12 and @InitialMonth <= 11 begin
		insert into @TableBillingValue
		exec sp_executesql @StringSelect12AND
	--Sobraria solo puede ir el else...	
	end 
	else if @EndMonth <> 12 and @InitialMonth <= 11 begin
		insert into @TableBillingValue
		exec sp_executesql @StringSelect
	end
	
	select 
	pc.Code + ' - ' + pc.[Name] as ProductionCenter,
	sum(cast(ce.SecondaryDistribution  as decimal (20,2) )) as TotalCost,
	sum(cast(isnull(bv.Value,0) as decimal (20,2) )) as BillingValue,
	sum(cast(isnull(bv.Value,0) - ce.SecondaryDistribution  as decimal (20,2) )) as Diference,
	sum(cast((isnull(bv.Value,0) - ce.SecondaryDistribution) * 100 / case isnull(ce.SecondaryDistribution,0) when 0 then 1 else isnull(ce.SecondaryDistribution,0) end as decimal (20,2) )) as Margin,
	case isnull(bv.Value,0) when 0 then 0 else sum(cast((isnull(bv.Value,0) - ce.SecondaryDistribution) * 100 / case isnull(bv.Value,0) when 0 then 1 else isnull(bv.Value,0) end as decimal (20,2) )) end as Utility
	from InteropCost.ProductionCenter pc
	inner join (select ProductionCenterId, sum(SecondaryDistribution) as SecondaryDistribution from InteropCost.CostEstimation where [Month] >= @InitialMonth and [Month] <= @EndMonth and [Year] = @Year group by ProductionCenterId) ce on ce.ProductionCenterId = pc.Id
	left join (select ProductionCenterId, sum(Value) as Value from @TableBillingValue group by ProductionCenterId) bv on bv.ProductionCenterId = pc.Id
	where pc.CenterType = 1 and pc.[Status] = 1 and pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin
	group by ce.ProductionCenterId, pc.Id, pc.Code, pc.[Name], bv.[Value]
	order by case isnull(bv.Value,0) when 0 then 0 else sum(cast((isnull(bv.Value,0) - ce.SecondaryDistribution) * 100 / case isnull(bv.Value,0) when 0 then 1 else isnull(bv.Value,0) end as decimal (20,2) )) end desc
END