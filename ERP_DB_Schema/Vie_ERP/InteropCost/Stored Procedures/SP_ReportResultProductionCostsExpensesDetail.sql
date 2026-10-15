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