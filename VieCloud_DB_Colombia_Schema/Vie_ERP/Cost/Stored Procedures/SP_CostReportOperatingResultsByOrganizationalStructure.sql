-- Author:		Cristhian Mauricio Salazar
-- Create date: 04/11/2016
-- Description:	Store para el reporte de resultado de operaciones
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostReportOperatingResultsByOrganizationalStructure]
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
select pc.Code + ' - ' + pc.Name as CostCenter,
b.Value as BillingValue,
round((isnull(b.Value,0) * 100.00 / @TotalBillingValue), 3) as BillingPercentage,
cast(ce.SecondaryDistribution as decimal (20,2)) as CostValue,
round((ce.SecondaryDistribution * 100 / @TotalCost), 3) as CostPercentage,
isnull(b.Value,0) - isnull(ce.SecondaryDistribution,0) as Diference,
(isnull(b.Value,0) - isnull(ce.SecondaryDistribution,0)) * 100 / b.Value as Utility
from [Cost].[CostProductionCenter] pc
inner join (select ProductionCenterId, sum(SecondaryDistribution) as SecondaryDistribution from [Cost].[CostEstimationNative] where [Month] >= @InitialMonth and [Month] <= @EndMonth and [Year] = @Year group by ProductionCenterId) ce on ce.ProductionCenterId = pc.Id
left join (select ProductionCenterId, sum(Value) as Value from @TableBillingValue group by ProductionCenterId) b on b.ProductionCenterId = pc.Id
where pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin and pc.CenterType = 1
--and pc.OrganizationalStructureOfCostId In (select id from [InteropCost].[OrganizationalStructureOfCosts] as oec where oec.Id = @StructureOfCostId or oec.[ParentId] = @StructureOfCostId)
and pc.OrganizationalStructureOfCostId In (select * from [Cost].fnCostRecursiveStructure(case @StructureOfCostId when 0 then 1 else @StructureOfCostId end))

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de resultados de operación por estructura organizacional de costos. Consolida, para un rango de meses y año seleccionados, el valor facturado (ingresos del libro mayor por centros de producción de tipo asistencial) y el costo total distribuido (distribución secundaria de la estimación nativa de costos) por cada centro de producción, filtrando por rango de código de centro y por la estructura organizacional de costos mediante una función recursiva jerárquica. Produce para cada centro de producción: el valor facturado, su participación porcentual sobre el total facturado, el costo incurrido, su participación porcentual sobre el total de costos, la diferencia (utilidad bruta) y el margen de utilidad, permitiendo evaluar la rentabilidad operativa de cada unidad dentro de la estructura organizacional de la institución.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostReportOperatingResultsByOrganizationalStructure';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de resultado de operaciones comparando valores facturados (libro mayor) versus costos estimados por centro de producción dentro de una estructura organizacional de costos y rango de períodos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de meses (InitialMonth/EndMonth) y año deben corresponder a períodos cargados en GeneralLedgerBalance y CostEstimationNative.; Deben existir homologaciones en CostProductionCenterHomologation con HomologationType = 6 para que se obtenga valor facturado.; Los centros de producción a reportar deben ser de tipo CenterType = 1.; Debe existir la función Cost.fnCostRecursiveStructure y la estructura organizacional referenciada por StructureOfCostId (si es 0 se asume 1).; El parámetro Container debe corresponder a un nombre válido aunque en la versión vigente no se usa en el SQL ejecutado dinámicamente.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte solo considera centros de producción con CenterType = 1.; El alcance organizacional siempre se resuelve mediante la función recursiva Cost.fnCostRecursiveStructure sobre la estructura indicada (o la 1 por defecto).; El cálculo del valor facturado depende de la naturaleza contable de la cuenta (débito o crédito).; Solo se consideran homologaciones contables con HomologationType = 6.; Los totales usados como denominador nunca son cero (se sustituyen por 1).; El filtro por código de centro de producción aplica el rango [CodePCenterIni, CodePCenterFin], usando ''z'' como tope si el final viene vacío.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Estructura organizacional de costos; Cuenta contable y naturaleza (débito/crédito); Saldo del libro mayor; Homologación contable de costos; Distribución secundaria de costos; Valor facturado; Utilidad; Resultado de operaciones', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado del SP: Devuelve por cada centro de producción (CenterType=1) dentro del rango de códigos y de la estructura organizacional recursiva: código+nombre, valor facturado, % facturación sobre total, costo (SecondaryDistribution), % costo sobre total, diferencia y utilidad porcentual.; [INSERT] @TableBillingValue: Inserta el valor facturado calculado dinámicamente desde GeneralLedgerBalance: si la naturaleza de la cuenta es 1 entonces Débito-Crédito, en caso contrario Crédito-Débito, agrupado por centro de producción y centro de costo, filtrando por el rango de meses y CenterType=1.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodePCenterFin es cadena vacía → Se asigna ''z'' como límite superior del rango de códigos de centro de producción para no restringir el extremo final.; si cue.Nature = 1 → El valor facturado se calcula como SUM(DebitValue) - SUM(CreditValue). else Se calcula como SUM(CreditValue) - SUM(DebitValue).; si @StructureOfCostId = 0 → Se usa 1 como estructura organizacional raíz para la búsqueda recursiva. else Se usa el valor recibido.; si @TotalBillingValue = 0 → Se fuerza @TotalBillingValue = 1 para evitar división por cero al calcular porcentajes de facturación.; si @TotalCost = 0 → Se fuerza @TotalCost = 1 para evitar división por cero al calcular porcentajes de costo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; Cost.CostProductionCenterHomologation; Cost.CostProductionCenter; Cost.CostProductionCenterCostCenter; Cost.CostEstimationNative; Cost.fnCostRecursiveStructure; InteropCost.OrganizationalStructureOfCosts', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStructure';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostReportOperatingResultsByOrganizationalStructure';
-- GO
