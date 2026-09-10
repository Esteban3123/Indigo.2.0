
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de resultados de producción de costos y gastos por centro de producción. Consolida y distribuye los costos de un período (rango de meses y año) para centros de producción de tipo directo, calculando la participación porcentual de cada componente del costo: mano de obra directa e indirecta, dispensación, transferencias entre centros, activos fijos, logística (centros tipo 3), administración (centros tipo 2) y facturación (obtenida desde contabilidad del contenedor/empresa indicado). A partir de esos valores calcula el costo total del centro y la utilidad como diferencia entre la facturación y el total de costos. Se usa para el informe gerencial de resultados de la operación, permitiendo filtrar por rango de códigos de centro de producción, rango de meses y año fiscal.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportResultProductionCostsExpenses';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportResultProductionCostsExpenses';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte mensual de producción de costos y resultado de la operación por centro de producción primario, consolidando costos directos, mano de obra, activos fijos, distribuciones logísticas y administrativas, ingresos por facturación y utilidad.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La base de datos contable externa indicada en @Container debe existir y contener las tablas CTNCOM{Year}, CTNCOMD{Year}, CTNCUENTA y CTNCLASE.; Deben existir homologaciones en InteropCost.ProductionCenterHomologation con HomologationType = 6 (facturación) para los centros de producción a reportar.; Deben existir registros en InteropCost.CostEstimation para los meses y año solicitados.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan centros de producción primarios activos (CenterType = 1, Status = 1).; El rango de códigos opera como filtro inclusivo lexicográfico (''0''..''z'' por defecto).; LogisticValue agrega solo costos provenientes de centros con CenterType = 3 activos; AdministrativeValue solo de centros con CenterType = 2 activos.; BillingValue se calcula únicamente sobre comprobantes contables con com.COMESTADO = 1 (estado activo) y MONTH(com.COMFFECHA) = mes del CostEstimation.; La facturación se vincula al centro de producción mediante ProductionCenterHomologation con HomologationType = 6.; Total = manoObra (directa+indirecta) + DispensingDistribution + TransferDistribution + FixedAssetDistribution + DirectCostDistribution + AutoCostDistribution + LogisticValue + AdministrativeValue.; porcentageUtility nunca es inferior a -100.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Costo directo; Mano de obra directa e indirecta; Distribución de activos fijos; Distribución de dispensación; Distribución de traslados; Costos logísticos; Costos administrativos; Facturación contable; Utilidad operacional; Homologación contable; Naturaleza de cuenta (débito/crédito)', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @resultPorcentage: Inserta una fila por centro de producción primario (pc.CenterType = 1 y pc.Status = 1) cuyo Code esté en el rango [@InitialCodeProduction, @EndCodeProduction] y cuyo período (Month, Year) esté dentro de [@InitialMonth..@EndMonth] del @Year, ejecutando dinámicamente sobre la BD @Container.; [UPDATE] @resultPorcentage: Cuando Total = 0 todos los porcentajes y la utilidad se fijan en 0; en caso contrario porcentageX = valorX * 100 (escalado luego dividido por Total en el SELECT final).; [UPDATE] @resultPorcentage: UtilityValue = BillingValue - Total cuando Total ≠ 0, y 0 en caso contrario.; [UPDATE] @resultPorcentage: porcentageUtility = -100 cuando BillingValue = 0 (con Total ≠ 0) o cuando ((BillingValue - Total) * 100 / BillingValue) < 0; en otro caso = (BillingValue - Total) * 100 / BillingValue.; [RETURN_RESULT] @resultPorcentage: Devuelve el resultado final con porcentajes normalizados dividiendo cada porcentaje por Total (o 0 si Total = 0).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @InitialCodeProduction = '''' → Se reemplaza por ''0'' (límite inferior por defecto del rango de códigos).; si @EndCodeProduction = '''' → Se reemplaza por ''z'' (límite superior por defecto del rango de códigos).; si Subconsulta de CostEstimationProductionCenterSecondary con pcs.CenterType = 3 y pcs.Status = 1 → Se computa LogisticValue: suma de costos secundarios provenientes de centros logísticos activos hacia el centro destino.; si Subconsulta de CostEstimationProductionCenterSecondary con pcs.CenterType = 2 y pcs.Status = 1 → Se computa AdministrativeValue: suma de costos secundarios provenientes de centros administrativos activos hacia el centro destino.; si cla.CLANATURA = 1 (cuenta de naturaleza débito) → BillingValue suma (CMMVALDEB - CMMVALCRE). else BillingValue suma (CMMVALCRE - CMMVALDEB).; si Total = 0 → Todos los porcentajes y UtilityValue se devuelven como 0. else Se calculan porcentajes proporcionales sobre Total y la utilidad como BillingValue - Total.; si BillingValue = 0 o utilidad relativa negativa → porcentageUtility se acota a -100.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.ProductionCenter; InteropCost.CostEstimation; InteropCost.CostEstimationProductionCenterSecondary; InteropCost.ProductionCenterHomologation; InteropCost.ProductionCenterCostCenter; CTNCOM{Year}; CTNCOMD{Year}; CTNCUENTA; CTNCLASE', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpenses';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpenses';
-- GO
