
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de resultado operativo por centro de producción para un rango de meses y año seleccionados, comparando el costo total distribuido (distribución secundaria) contra el valor facturado contable de cada centro productivo activo. Consolida los costos desde la tabla de estimación de costos (CostEstimation) y obtiene el valor de facturación desde las tablas contables del contenedor (empresa) indicado, aplicando homologación de cuentas y centros de costo. Calcula métricas financieras clave por centro de producción: costo total, valor facturado, diferencia, margen y utilidad porcentual, permitiendo evaluar la rentabilidad operativa de cada unidad productiva. Filtra centros de tipo productivo (CenterType=1), activos (Status=1) y dentro del rango de códigos de centro indicado, ordenando el resultado de mayor a menor utilidad.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportOperatingResultProductionCenter';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportOperatingResultProductionCenter';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de resultado operativo por centro de producción comparando el costo distribuido (estimación) contra el valor facturado obtenido de la contabilidad externa, calculando diferencia, margen y utilidad por periodo.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir en la base @Container las tablas CTNSAL{Year}, CTNCOMD{Year}, CTNCOM{Year} y CTNCUENTA correspondientes al año solicitado.; Las cuentas contables consultadas deben estar homologadas en InteropCost.ProductionCenterHomologation con HomologationType = 6.; Los centros de producción deben estar asociados a sus centros de costo en InteropCost.ProductionCenterCostCenter.; @InitialMonth y @EndMonth deben representar un rango válido de meses (1..12); el flujo no contempla @InitialMonth > 11 con @EndMonth <> 12.; Debe existir información de estimación en InteropCost.CostEstimation para el rango (@InitialMonth, @EndMonth, @Year).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran cuentas homologadas con HomologationType = 6 para obtener el valor facturado contable.; En los movimientos de comprobantes se excluye siempre CTNTIPCOM = 67 y se restringe a cuentas con CTNCLASE = 5 (cuentas de ingresos/clase 5).; El reporte solo incluye centros de producción con CenterType = 1 y Status = 1.; Cuando SecondaryDistribution es 0 o NULL, el divisor se reemplaza por 1 para evitar división por cero al calcular el Margen.; Cuando bv.Value es 0, la Utilidad se devuelve como 0 (cortocircuito de la división).; El nombre de las tablas contables (CTNSAL, CTNCOMD, CTNCOM) se construye dinámicamente concatenando el @Year, asumiendo particionamiento anual por tabla.; Los costos del periodo se obtienen agregando SecondaryDistribution de CostEstimation entre @InitialMonth y @EndMonth para el @Year indicado.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'centro de producción; centro de costo; homologación contable; saldo contable; comprobante contable; distribución secundaria de costos; valor facturado; margen; utilidad; resultado de operación', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableBillingValue: Cuando @InitialMonth = @EndMonth = 12, inserta los valores facturados calculados como SUM(CMMVALCRE - CMMVALDEB) desde CTNCOMD/CTNCOM del mes 12, excluyendo CTNTIPCOM=67 y filtrando CTNCLASE=5.; [INSERT] @TableBillingValue: Cuando @EndMonth = 12 y @InitialMonth <= 11, inserta la unión de los saldos contables (ABS(SUM(CSCDEBITO - CSCCREDITO))) entre @InitialMonth y mes 11 más los movimientos de comprobantes del mes 12.; [INSERT] @TableBillingValue: Cuando @EndMonth <> 12 y @InitialMonth <= 11, inserta solo los valores facturados como ABS(SUM(CSCCREDITO - CSCDEBITO)) desde CTNSAL para el rango de meses indicado.; [RETURN_RESULT] ResultSet: Devuelve por cada ProductionCenter activo (CenterType=1, Status=1) dentro del rango de códigos: TotalCost, BillingValue, Diference, Margin y Utility, ordenado por Utility descendente.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodePCenterFin = '''' → Se asigna ''z'' como límite superior del rango de códigos para no acotar el filtro por código de centro de producción.; si @EndMonth = 12 AND @InitialMonth = 12 → Se obtiene el valor facturado SOLO desde los comprobantes contables (CTNCOMD/CTNCOM) del mes 12, excluyendo el tipo de comprobante 67 y filtrando cuentas con CTNCLASE=5.; si @EndMonth = 12 AND @InitialMonth <= 11 → Se combina (UNION ALL) el saldo contable (CTNSAL) de los meses entre @InitialMonth y 11 con los comprobantes (CTNCOMD/CTNCOM) del mes 12 (CTNTIPCOM<>67, CTNCLASE=5).; si @EndMonth <> 12 AND @InitialMonth <= 11 → Se obtiene el valor facturado únicamente desde el saldo contable CTNSAL para el rango de meses indicado, usando ABS(SUM(CSCCREDITO - CSCDEBITO)).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sp_executesql', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.ProductionCenterHomologation; InteropCost.ProductionCenterCostCenter; InteropCost.ProductionCenter; InteropCost.CostEstimation; CTNSAL{Year}; CTNCUENTA; CTNCOMD{Year}; CTNCOM{Year}', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportOperatingResultProductionCenter';
-- GO
