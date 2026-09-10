-- =============================================
-- Author:		Jhefersson Muñoz
-- Create date: 04/11/2016
-- Description:	Reporte de Produccion de costos o resultado de la operacion
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_ReportResultProductionCostsExpensesDetail]
	@DateStart as date,
	@DateEnd as date,
	@InitialCodeProduction varchar(20),
	@EndCodeProduction varchar(20), 
	@Container varchar(20),
	@DetailType as integer
AS
BEGIN
	if @InitialCodeProduction = '' begin
		set @InitialCodeProduction = '0'
	end
	if @EndCodeProduction = '' begin
		set @EndCodeProduction = 'z'
	end	

declare @sql nvarchar(MAX)

--if @DetailType = 1 begin
----set @sql = N' 
--	select case when  iprcodigo is not null then iprcodigo else sipcodigo end Code,  
--	case when iprdescor  is not null then iprdescor else sipnombre  end Servicio,
--	ctn.CueCodigo as CodeAccount,
--	ctn.CueNombre as NameAccount,
--	pc.code as CodePc,
--	pc.Name,
--	sum(sercantid) Cantidad,  
--	sum(SERVALPRO*SERCANTID) TotalValue, 
--	case when SFATIPDOC>=1 then sum((SERVALENT+servalpac)*SERCANTID) when sfatipdoc=0 then sum(servalpac*SERCANTID) end ValorTotal2
--	from [DGEMPRES99].[dbo].[ADNINGRESO]  ing 
--	inner join [DGEMPRES99].dbo.GENPACIEN  pac on ing.GENPACIEN=pac.oid
--	inner join [DGEMPRES99].dbo.SLNFACTUR fac on fac.adningreso=ing.OID  
--	inner join [DGEMPRES99].dbo.SLNSERPRO  pri on pri.ADNINGRES1=ing.OID  and pri.ADNINGRES1=fac.ADNINGRESO  and pri.GENDETCON1=fac.GENDETCON
--	inner join [DGEMPRES99].dbo.GENARESER gen on gen.OID = pri.GENARESER1
--	inner join [DGEMPRES99].dbo.CTNCUENTA ctn on ctn.OID = gen.CTNCUENTA1
--	inner join InteropCost.ProductionCenterHomologation as ip on ip.AccountOriginId = ctn.OID and ip.HomologationType = 6
--	inner join InteropCost.ProductionCenter as pc on pc.Id = ip.ProductionCenterId
--	left join [DGEMPRES99].dbo.SLNSERHOJ se1 on se1.oid=pri.OID
--	left join [DGEMPRES99].dbo.SLNPROHOJ pr1 on pr1.oid=Pri.oid
--	left join [DGEMPRES99].dbo.GENSERIPS ser on ser.oid=se1.GENSERIPS1
--	left join [DGEMPRES99].dbo.INNPRODUC pro on pro.oid=pr1.INNPRODUC1
--	where sfafecfac >= @DateStart and  sfafecfac < @DateEnd and sfadocanu=0  and ainestado=1
--	group by iprcodigo, sipcodigo, iprdescor,sipnombre,sfatipdoc,ctn.CueCodigo,ctn.CueNombre,pc.code,pc.Name
--	order by Code
--end
--else
--VENTAS HomologationType = 6
if @DetailType = 2 begin
set @sql = N' 
	select case when  iprcodigo is not null then iprcodigo else sipcodigo end Code,  
	case when iprdescor  is not null then iprdescor else sipnombre  end Servicio,
	ctn.CueCodigo as CodeAccount,
	ctn.CueNombre as NameAccount,
	pc.code as CodePc,
	pc.Name,
	sum(sercantid) Cantidad,  
	sum(SERVALPRO*SERCANTID) TotalValue, 
	case when SFATIPDOC>=1 then sum((SERVALENT+servalpac)*SERCANTID) when sfatipdoc=0 then sum(servalpac*SERCANTID) end ValorTotal2
	from ' + @Container + '.[dbo].[ADNINGRESO]  ing 
	inner join ' + @Container + '.dbo.GENPACIEN  pac on ing.GENPACIEN=pac.oid
	inner join ' + @Container + '.dbo.SLNFACTUR fac on fac.adningreso=ing.OID  
	inner join ' + @Container + '.dbo.SLNSERPRO  pri on pri.ADNINGRES1=ing.OID  and pri.ADNINGRES1=fac.ADNINGRESO  and pri.GENDETCON1=fac.GENDETCON
	inner join ' + @Container + '.dbo.GENARESER gen on gen.OID = pri.GENARESER1
	inner join ' + @Container + '.dbo.CTNCUENTA ctn on ctn.OID = gen.CTNCUENTA1
	inner join InteropCost.ProductionCenterHomologation as ip on ip.AccountOriginId = ctn.OID and ip.HomologationType = 6
	inner join InteropCost.ProductionCenter as pc on pc.Id = ip.ProductionCenterId
	left join ' + @Container + '.dbo.SLNSERHOJ se1 on se1.oid=pri.OID
	left join ' + @Container + '.dbo.SLNPROHOJ pr1 on pr1.oid=Pri.oid
	left join ' + @Container + '.dbo.GENSERIPS ser on ser.oid=se1.GENSERIPS1
	left join ' + @Container + '.dbo.INNPRODUC pro on pro.oid=pr1.INNPRODUC1
	where sfafecfac >= ''' + convert(varchar(20),@DateStart,103) + ''' and  sfafecfac < ''' + convert(varchar(20),@DateEnd,103) + ''' and sfadocanu=0  and ainestado=1  AND pc.Code  >= ''' + @InitialCodeProduction + ''' AND pc.Code <= ''' + @EndCodeProduction + ''''
	+ ' group by iprcodigo, sipcodigo, iprdescor,sipnombre,sfatipdoc,ctn.CueCodigo,ctn.CueNombre,pc.code,pc.Name
	order by Code'
end
--else
--if @DetailType = 3 begin
----set @sql = N' 
--	select case when  iprcodigo is not null then iprcodigo else sipcodigo end Code,  
--	case when iprdescor  is not null then iprdescor else sipnombre  end Servicio,
--	ctn.CueCodigo as CodeAccount,
--	ctn.CueNombre as NameAccount,
--	sum(sercantid) Cantidad,  
--	sum(SERVALPRO*SERCANTID) TotalValue, 
--	case when SFATIPDOC>=1 then sum((SERVALENT+servalpac)*SERCANTID) when sfatipdoc=0 then sum(servalpac*SERCANTID) end ValorTotal2
--	from [DGEMPRES99].[dbo].[ADNINGRESO]  ing 
--	inner join [DGEMPRES99].dbo.GENPACIEN  pac on ing.GENPACIEN=pac.oid
--	inner join [DGEMPRES99].dbo.SLNFACTUR fac on fac.adningreso=ing.OID  
--	inner join [DGEMPRES99].dbo.SLNSERPRO  pri on pri.ADNINGRES1=ing.OID  and pri.ADNINGRES1=fac.ADNINGRESO  and pri.GENDETCON1=fac.GENDETCON
--	inner join [DGEMPRES99].dbo.GENARESER gen on gen.OID = pri.GENARESER1
--	inner join [DGEMPRES99].dbo.CTNCUENTA ctn on ctn.OID = gen.CTNCUENTA1
--	left join [DGEMPRES99].dbo.SLNSERHOJ se1 on se1.oid=pri.OID
--	left join [DGEMPRES99].dbo.SLNPROHOJ pr1 on pr1.oid=Pri.oid
--	left join [DGEMPRES99].dbo.GENSERIPS ser on ser.oid=se1.GENSERIPS1
--	left join [DGEMPRES99].dbo.INNPRODUC pro on pro.oid=pr1.INNPRODUC1
--	where sfafecfac >= @DateStart and  sfafecfac < @DateEnd and sfadocanu=0  and ainestado=1
--	group by iprcodigo, sipcodigo, iprdescor,sipnombre,sfatipdoc,ctn.CueCodigo,ctn.CueNombre
--	order by Code
--end
execute sp_executesql @sql
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte detallado de producción de costos y ventas por centro de producción, que consulta facturas, ingresos de pacientes y servicios prestados (CUPS/procedimientos) en un período de fechas determinado. Cruza la información contable de cuentas (CTNCUENTA) con la homologación de centros de producción (InteropCost.ProductionCenterHomologation, tipo 6 = ventas) para mostrar cantidad, valor profesional y valor total facturado por cada servicio o producto. Recibe como parámetros: rango de fechas de facturación, rango de códigos de centro de producción, base de datos contenedora (empresa/institución) y tipo de detalle. Construye SQL dinámico en tiempo de ejecución según la base de datos origen, por lo que se usa en entornos multi-empresa para analizar resultados operativos, costos y ventas por centro de producción en un rango de tiempo dado.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportResultProductionCostsExpensesDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportResultProductionCostsExpensesDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte detallado de producción de costos/ventas agrupado por servicio, cuenta contable y centro de producción para un rango de fechas y rango de códigos de centro de producción.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro @Container debe contener el nombre de una base de datos válida con esquema dbo y las tablas ADNINGRESO, GENPACIEN, SLNFACTUR, SLNSERPRO, GENARESER, CTNCUENTA, SLNSERHOJ, SLNPROHOJ, GENSERIPS, INNPRODUC.; Deben existir homologaciones en InteropCost.ProductionCenterHomologation con HomologationType = 6 que enlacen las cuentas (CTNCUENTA.OID) con centros de producción.; Solo el flujo @DetailType = 2 está activo; los otros bloques (1 y 3) están comentados.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas no anuladas (sfadocanu = 0) e ingresos activos (ainestado = 1).; El rango de fechas es semiabierto: sfafecfac >= @DateStart y sfafecfac < @DateEnd.; Solo se incluyen cuentas contables homologadas con HomologationType = 6 (homologación de ventas).; Las fechas se inyectan en el SQL dinámico con formato 103 (dd/mm/yyyy).; La agrupación siempre es por iprcodigo, sipcodigo, iprdescor, sipnombre, sfatipdoc, CueCodigo, CueNombre, pc.code y pc.Name.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producción de costos; Centro de producción / centro de costo; Homologación contable; Cuenta contable; Factura; Ingreso de paciente; Servicio IPS; Producto; Valor entidad / valor paciente; Anulación de documento', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dynamic_resultset: Cuando @DetailType = 2, devuelve filas agregadas (Code, Servicio, CodeAccount, NameAccount, CodePc, Name, Cantidad, TotalValue, ValorTotal2) filtradas por sfafecfac entre @DateStart y @DateEnd, sfadocanu=0, ainestado=1 y pc.Code dentro del rango [@InitialCodeProduction, @EndCodeProduction].', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @InitialCodeProduction = '''' → Se reasigna a ''0'' como límite inferior del rango de códigos de centro de producción.; si @EndCodeProduction = '''' → Se reasigna a ''z'' como límite superior del rango de códigos de centro de producción.; si @DetailType = 2 → Construye y ejecuta el SQL dinámico de ventas/costos sobre la base @Container con homologación de centro de producción (HomologationType = 6). else No se ejecuta ninguna consulta (los bloques @DetailType=1 y @DetailType=3 están comentados); @sql queda NULL y sp_executesql no produce resultados.; si SFATIPDOC >= 1 → ValorTotal2 = SUM((SERVALENT + SERVALPAC) * SERCANTID) — incluye valor entidad más valor paciente. else Si SFATIPDOC = 0, ValorTotal2 = SUM(SERVALPAC * SERCANTID) — solo valor paciente.; si iprcodigo IS NOT NULL → Code y Servicio toman iprcodigo/iprdescor (producto INNPRODUC). else Toman sipcodigo/sipnombre (servicio GENSERIPS).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'{@Container}.dbo.ADNINGRESO; {@Container}.dbo.GENPACIEN; {@Container}.dbo.SLNFACTUR; {@Container}.dbo.SLNSERPRO; {@Container}.dbo.GENARESER; {@Container}.dbo.CTNCUENTA; {@Container}.dbo.SLNSERHOJ; {@Container}.dbo.SLNPROHOJ; {@Container}.dbo.GENSERIPS; {@Container}.dbo.INNPRODUC; InteropCost.ProductionCenterHomologation; InteropCost.ProductionCenter', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpensesDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportResultProductionCostsExpensesDetail';
-- GO
