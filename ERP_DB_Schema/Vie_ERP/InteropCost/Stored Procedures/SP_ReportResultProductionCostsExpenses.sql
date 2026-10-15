
-- =============================================
-- Author:		Jhefersson Muñoz
-- Create date: 04/11/2016
-- Description:	Reporte de Produccion de costos o resultado de la operacion
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_ReportResultProductionCostsExpenses]
	@InitialMonth integer,
	@EndMonth integer,
	@Year integer,
	@InitialCodeProduction varchar(20),
	@EndCodeProduction varchar(20), 
	@Container varchar(20)
AS
BEGIN
	if @InitialCodeProduction = '' begin
		set @InitialCodeProduction = '0'
	end
	if @EndCodeProduction = '' begin
		set @EndCodeProduction = 'z'
	end	

declare @sql nvarchar(MAX)

--Tabla temporal que almacena los datos del String executado. 
declare @resultPorcentage table ([Month] int, [Year] int, ManPowerDistribution numeric(20,4), porcentageManPowerDistribution decimal, DispensingDistribution numeric(20,4),
porcentageDispensingDistribution decimal, TransferDistribution numeric(20,4), porcentageTransferDistribution decimal, FixedAssetDistribution numeric(20,4), porcentangeFixedAssetDistribution decimal,
LogisticValue numeric(20,4),porcentageLogisticValue decimal, AdministrativeValue numeric(20,4), porcentageAdministrativeValue decimal, BillingValue numeric(20,4), Total numeric(20,4),
UtilityValue numeric(20,4), porcentageUtility decimal, Code varchar(20), [Name] varchar(100))

set @sql = N' 
select ce.[Month], ce.[Year]
, ce.ManPowerDistributionDirect + ce.ManPowerDistributionInDirect as ManPowerDistribution
, 0
, round((ce.TransferDistribution + ce.DispensingDistribution),0) as DispensingDistribution
,0
, (ce.[DirectCostDistribution] + ce.[AutoCostDistribution]) as TransferDistribution
,0
, ce.FixedAssetDistribution
,0
, isnull((
select sum(DirectCost + AutoCostDistribution + ManPowerDistributionDirect + ManPowerDistributionInDirect + FixedAssetDistribution + DispensingDistribution + TransferDistribution) from InteropCost.CostEstimationProductionCenterSecondary as es
inner join InteropCost.ProductionCenter as pcs on pcs.Id = es.SourceProductionCenterId
inner join InteropCost.ProductionCenter as pct on pct.Id = es.TargetProductionCenterId and pcs.CenterType = 3 and pcs.Status = 1 and pct.Id = pc.Id and [Month] = ce.[Month] and [Year] = ce.[year]
),0) as LogisticValue
, 0
, isnull((
select sum(DirectCost + AutoCostDistribution + ManPowerDistributionDirect + ManPowerDistributionInDirect + FixedAssetDistribution + DispensingDistribution + TransferDistribution) from InteropCost.CostEstimationProductionCenterSecondary as es
inner join InteropCost.ProductionCenter as pcs on pcs.Id = es.SourceProductionCenterId
inner join InteropCost.ProductionCenter as pct on pct.Id = es.TargetProductionCenterId and pcs.CenterType = 2 and pcs.Status = 1 and pct.Id = pc.Id and [Month] = ce.[Month] and [Year] = ce.[year]
),0) as AdministrativeValue
, 0
, isnull((
select isnull(sum(case cla.CLANATURA when 1 then isnull(comd.CMMVALDEB,0) - isnull(comd.CMMVALCRE,0) else isnull(comd.CMMVALCRE,0) - isnull(comd.CMMVALDEB,0) end),0)
from ' + @Container + '.dbo.CTNCOM'+ cast(@Year as varchar(20)) + ' com
inner join ' + @Container + '.dbo.CTNCOMD'+ cast(@Year as varchar(20)) + ' comd on com.OID = comd.CTNCOMCONC
inner join ' + @Container + '.dbo.CTNCUENTA cue on cue.OID = comd.CTNCUENTA
inner join ' + @Container + '.dbo.CTNCLASE cla on cla.OID = cue.CTNCLASE
inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = cue.CUECODIGO and hm.HomologationType = 6 and hm.ProductionCenterId = pc.Id
inner join InteropCost.ProductionCenterCostCenter pccc on pccc.ProductionCenterId = hm.ProductionCenterId and comd.CTNCENCOS = pccc.CostCenterId
inner join InteropCost.ProductionCenter pctmp on pctmp.Id = pccc.ProductionCenterId
where com.COMESTADO = 1 AND MONTH(com.COMFFECHA) = ce.[Month] and pctmp.Id = pc.Id
),0) as BillingValue
, ce.ManPowerDistributionDirect + ce.ManPowerDistributionInDirect + ce.DispensingDistribution + ce.TransferDistribution + ce.FixedAssetDistribution + [DirectCostDistribution] +  ce.[AutoCostDistribution]
+ 
isnull((
select sum(DirectCost + AutoCostDistribution + ManPowerDistributionDirect + ManPowerDistributionInDirect + FixedAssetDistribution + DispensingDistribution + TransferDistribution) from InteropCost.CostEstimationProductionCenterSecondary as es
inner join InteropCost.ProductionCenter as pcs on pcs.Id = es.SourceProductionCenterId
inner join InteropCost.ProductionCenter as pct on pct.Id = es.TargetProductionCenterId and pcs.CenterType = 3 and pcs.Status = 1 and pct.Id = pc.Id and [Month] = ce.[Month] and [Year] = ce.[year]
),0)
+ 
isnull((
select sum(DirectCost + AutoCostDistribution + ManPowerDistributionDirect + ManPowerDistributionInDirect + FixedAssetDistribution + DispensingDistribution + TransferDistribution) from InteropCost.CostEstimationProductionCenterSecondary as es
inner join InteropCost.ProductionCenter as pcs on pcs.Id = es.SourceProductionCenterId
inner join InteropCost.ProductionCenter as pct on pct.Id = es.TargetProductionCenterId and pcs.CenterType = 2 and pcs.Status = 1 and pct.Id = pc.Id and [Month] = ce.[Month] and [Year] = ce.[year]
),0) as Total
, 0
, 0
, pc.Code
, pc.Name
from  InteropCost.ProductionCenter pc
inner join InteropCost.CostEstimation ce on ce.ProductionCenterId = pc.Id
where pc.CenterType = 1 and pc.Status = 1 and Month >=' + cast(@InitialMonth as varchar(20)) + ' And Month <=' + cast(@EndMonth as varchar(20)) + ' And year =' + cast(@Year as varchar(20)) + ' AND pc.Code  >= ''' + @InitialCodeProduction + ''' AND pc.Code <= ''' + @EndCodeProduction + ''''

insert into @resultPorcentage
execute sp_executesql @sql
update r set porcentageManPowerDistribution = case r.Total when 0 then 0 else (r.ManPowerDistribution * 100) end, porcentageDispensingDistribution = case r.Total when 0 then 0 else (r.DispensingDistribution * 100) end
,porcentageTransferDistribution = case r.Total when 0 then 0 else (r.TransferDistribution * 100) end, porcentangeFixedAssetDistribution = case r.Total when 0 then 0 else (FixedAssetDistribution * 100) end
,porcentageLogisticValue = case r.Total when 0 then 0 else (r.LogisticValue * 100) end, porcentageAdministrativeValue = case r.Total when 0 then 0 else (AdministrativeValue * 100) end
,UtilityValue = case r.Total when 0 then 0 else (r.BillingValue - r.Total) end, porcentageUtility = case r.Total when 0 then 0 else case r.BillingValue when 0 then -100 else case when ((r.BillingValue - r.Total) * 100 / r.BillingValue) < 0 then -100 else (r.BillingValue - r.Total) * 100 / r.BillingValue end end end
from @resultPorcentage as r
--case when ((r.BillingValue - r.Total) * 100 / r.BillingValue) > 100 then -100 else (r.BillingValue - r.Total) * 100 / r.BillingValue end
select [Month],[Year],ManPowerDistribution, 
	Case Total 
		When 0 Then
			0
		Else
		porcentageManPowerDistribution/Total 
	End
as porcentageManPowerDistribution,DispensingDistribution,
	Case Total 
		When 0 Then
			0
		Else
		porcentageDispensingDistribution/Total 
	End
as porcentageDispensingDistribution,TransferDistribution,
	Case Total 
		When 0 Then
			0
		Else
		porcentageTransferDistribution/Total 
	End
as porcentageTransferDistribution,FixedAssetDistribution
,
	Case Total 
		When 0 Then
			0
		Else
		porcentangeFixedAssetDistribution/Total 
	End
as porcentangeFixedAssetDistribution, LogisticValue, 
	Case Total 
		When 0 Then
			0
		Else
		porcentageLogisticValue/Total 
	End
as porcentageLogisticValue,  AdministrativeValue, 
	Case Total 
		When 0 Then
			0
		Else
		porcentageAdministrativeValue/Total 
	End
as porcentageAdministrativeValue, BillingValue, Total, UtilityValue, porcentageUtility, Code, [Name] 
from @resultPorcentage 
END