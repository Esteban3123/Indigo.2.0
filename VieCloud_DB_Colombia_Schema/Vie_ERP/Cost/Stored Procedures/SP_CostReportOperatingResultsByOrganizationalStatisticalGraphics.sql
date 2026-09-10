-- Author:		Cristhian Mauricio Salazar
-- Create date: 04/11/2016
-- Description:	Store para el reporte de resultado de operaciones
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics]
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

set @StringSelect = 'select hm.ProductionCenterId, pccc.CostCenterId,isnull(case cue.Nature when 1 then sum(isnull(sal.DebitValue,0)) - sum(isnull(sal.CreditValue,0)) else sum(isnull(sal.CreditValue,0)) - sum(isnull(sal.DebitValue,0)) end,0)
from 
[GeneralLedger].[GeneralLedgerBalance] sal
inner join [GeneralLedger].[MainAccounts] cue on cue.Id = sal.IdMainAccount
--inner join ' + @Container + '.dbo.CTNCLASE cla on cla.OID = cue.CTNCLASE
inner join [Cost].[CostProductionCenterHomologation] hm on hm.AccountOriginId = cue.Id and hm.HomologationType = 6 
inner join [Cost].[CostProductionCenter] pc on pc.Id = hm.ProductionCenterId 
inner join [Cost].[CostProductionCenterCostCenter] pccc on pccc.ProductionCenterId = hm.ProductionCenterId and pccc.CostCenterId = sal.IdCostCenter
where sal.[Month] >= '+ cast(@InitialMonth as varchar(20)) +' and sal.[Month] <= '+ CAST(@EndMonth as varchar(20)) +' and pc.CenterType = 1 
group by hm.ProductionCenterId, pccc.CostCenterId, cue.Nature'
	
insert into @TableBillingValue
exec sp_executesql @StringSelect

--- Total de valor facturado
declare @TotalBillingValue numeric(20,0) = (
select sum(ISNULL(b.Value,0))
from [Cost].[CostProductionCenter] pc
left join (select ProductionCenterId, sum(Value) as Value from @TableBillingValue group by ProductionCenterId) b on b.ProductionCenterId = pc.Id
where pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin and pc.CenterType = 1
and pc.OrganizationalStructureOfCostId In (select * from [Cost].fnCostRecursiveStructure(case @StructureOfCostId when 0 then 1 else @StructureOfCostId end))
)

if @TotalBillingValue = 0 begin
	set @TotalBillingValue = 1
end

--- Total de costos 
declare @TotalCost numeric(20,0) = (
select sum(ISNULL(ce.SecondaryDistribution,0))
from [Cost].[CostProductionCenter] pc
inner join [Cost].[CostEstimationNative] ce on ce.ProductionCenterId = pc.Id
where ce.[Month] >= @InitialMonth and ce.[Month] <= @EndMonth and ce.[Year] = @Year and pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin
and pc.OrganizationalStructureOfCostId In (select * from [Cost].fnCostRecursiveStructure(case @StructureOfCostId when 0 then 1 else @StructureOfCostId end))
)

if @TotalCost = 0 begin
	set @TotalCost = 1
end
print ':P'
print @TotalBillingValue
select
null as CostCenter,
pc.Code + ' - ' + pc.Name as Name,
b.Value as BillingValue,
round((isnull(b.Value,0) * 100.00 / @TotalBillingValue), 3) as BillingPercentage,
cast(ce.SecondaryDistribution as decimal (20,2)) as CostValue,
round((ce.SecondaryDistribution * 100 / @TotalCost), 3) as CostPercentage,
isnull(b.Value,0) - isnull(ce.SecondaryDistribution,0) as Diference,
(isnull(b.Value,0) - isnull(ce.SecondaryDistribution,0)) * 100 / b.Value as Utility
from [Cost].[CostProductionCenter] pc
inner join (select ProductionCenterId, sum(SecondaryDistribution) as SecondaryDistribution from InteropCost.CostEstimation where [Month] >= @InitialMonth and [Month] <= @EndMonth and [Year] = @Year group by ProductionCenterId) ce on ce.ProductionCenterId = pc.Id
left join (select ProductionCenterId, sum(Value) as Value from @TableBillingValue group by ProductionCenterId) b on b.ProductionCenterId = pc.Id
where pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin and pc.CenterType = 1
--and pc.OrganizationalStructureOfCostId In (select id from [InteropCost].[OrganizationalStructureOfCosts] as oec where oec.Id = @StructureOfCostId or oec.[ParentId] = @StructureOfCostId)
and pc.OrganizationalStructureOfCostId In (select * from [Cost].fnCostRecursiveStructure(case @StructureOfCostId when 0 then 1 else @StructureOfCostId end))

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte gráfico de resultados de operación por estructura organizacional de costos, comparando el valor facturado contra el costo total de cada centro de producción en un rango de meses y año determinados. Para cada centro de producción de tipo facturador, calcula el valor facturado (obtenido del libro mayor contable cruzado con homologaciones de cuentas), el costo total acumulado tras la distribución secundaria (tomado de las estimaciones nativas e interoperables de costos), la diferencia entre ambos (utilidad o pérdida operativa) y los porcentajes de participación sobre los totales. Filtra los centros de producción por rango de códigos y por estructura organizacional de costos usando la función recursiva fnCostRecursiveStructure, lo que permite navegar jerarquías de estructura de costos. Es utilizado en el módulo de costos para reportería gerencial de resultados operativos por unidad organizacional, apoyando análisis de rentabilidad y eficiencia por centro de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera datos estadísticos comparativos de facturación vs costos por centro de producción dentro de una estructura organizacional, para alimentar gráficos del reporte de resultados de operación.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de meses (InitialMonth/EndMonth) y año deben corresponder a períodos contables existentes en GeneralLedgerBalance y CostEstimation.; Deben existir homologaciones en CostProductionCenterHomologation con HomologationType=6 entre cuentas contables y centros de producción.; Los centros de producción evaluados deben tener CenterType=1 (centros productivos).; Si StructureOfCostId=0 se asume la estructura raíz con Id=1.; Si CodePCenterFin viene vacío se reemplaza por ''z'' como cota superior.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El cálculo de facturación depende de la naturaleza contable: débito o crédito determina el signo del saldo neto.; Solo se incluyen centros de producción de tipo productivo (CenterType=1).; El universo de centros se restringe siempre a la estructura organizacional de costos resuelta recursivamente vía fnCostRecursiveStructure.; Los porcentajes (BillingPercentage, CostPercentage, Utility) nunca dividen entre cero porque los totales en cero se reemplazan por 1.; El total de facturación se calcula leyendo la tabla temporal previa, mientras que el detalle final lee CostEstimation desde InteropCost (no desde CostEstimationNative usado para el total de costo).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Cuenta contable; Naturaleza contable (débito/crédito); Saldo del libro mayor; Homologación contable de costos; Estimación y distribución secundaria de costos; Estructura organizacional de costos; Valor facturado vs costo; Utilidad porcentual', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado tabular: Devuelve por cada centro de producción (CenterType=1) dentro del rango de códigos y de la estructura recursiva: valor facturado, % de facturación, valor de costo (SecondaryDistribution), % de costo, diferencia y utilidad porcentual.; [INSERT] @TableBillingValue: Inserta el valor facturado por centro de producción y centro de costo calculado como: si la naturaleza de la cuenta es 1 (débito) entonces Débito-Crédito, en caso contrario Crédito-Débito, agrupado entre InitialMonth y EndMonth para centros con CenterType=1.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodePCenterFin = '''' → Se asigna ''z'' como límite superior del rango de códigos de centros.; si @TotalBillingValue = 0 → Se fuerza @TotalBillingValue=1 para evitar división por cero al calcular el porcentaje de facturación.; si @TotalCost = 0 → Se fuerza @TotalCost=1 para evitar división por cero al calcular el porcentaje de costo.; si @StructureOfCostId = 0 → Se utiliza la estructura raíz con Id=1 al invocar fnCostRecursiveStructure. else Se utiliza la estructura recibida como parámetro.; si cue.Nature = 1 → El valor facturado se calcula como suma(DebitValue) - suma(CreditValue). else El valor facturado se calcula como suma(CreditValue) - suma(DebitValue).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; Cost.CostProductionCenterHomologation; Cost.CostProductionCenter; Cost.CostProductionCenterCostCenter; Cost.CostEstimationNative; Cost.fnCostRecursiveStructure; InteropCost.CostEstimation', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics';
-- GO
