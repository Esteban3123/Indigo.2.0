-- =============================================
-- Author:		Rafael Patiño
-- Create date: 25/03/2014
-- Description:	Sp que retorna los detalles de las facturas por contenedor VERSION ERP NET
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceDetailList__NET] 
	@container varchar(50),
	@HISContainer varchar(50),
	@SecurityContainer varchar(50),
	@numerofactura varchar(15),
	@numeroConsecutivo varchar(15)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   	set @container = 'DGEMPRES99'
	--set @numeroConsecutivo = '0000011030'
	--set @numerofactura = '0000011030'
	
	Declare @tablaDetalleFactura table(
		InvoiceNumber varchar(50),
		ServiceDate datetime,
		ServiceCode varchar(20),
		ServiceName varchar(250),
		ServiceAreaCode varchar(10),
		DescriptionServiceArea varchar(300),
		InvoiceDetailId int,
		ServiceOrderDetailId int,
		MedicalCode varchar(20),
		MedicalName varchar(200),
		BillerCode varchar(20),
		BillerName varchar(200),
		BillingGroupCode varchar(2),
		BillingGroup varchar(120),
		ValueServiceManual money,
		UnitValue money,
		InvoicedValue money, 
		Ammount int,
		CostCenterCode varchar(14),
		CostCenterName varchar(500),
		TypeServiceProduct char(1),
		TypeProcedure char(1),
		AccountantAccountIncome varchar(30),
		Ingress varchar(50),					 --dato para filtro no para persistir
		ServiceOrder varchar(50),				--dato para filtro no para persistir
		consecutiveOrder varchar(50),			--dato para filtro no para persistir
		ServiceNumber  varchar(50),				--dato para filtro no para persistir
		ConsecutivoInventory  varchar(50)		--dato para filtro no para persistir	
		)

	declare @sql as nVarchar(max)
	
	/*
	set @sql= 'USE ' + @container  +' 
		SELECT  
			slf.sfanumfac as InvoiceNumber,
			sls.serfecser as ServiceDate,
			sls.sipcodigo as ServiceCode,
			sls.serdesser as ServiceName,
			sls.gascodigo as ServiceAreaCode,
			GASNOMBRE as DescriptionServiceArea,
			sls.gmecodigo as MedicalCode,
			med.GMENOMBRE as MedicalName,
			slf.usucodigo as BillerCode,
			usu.USUNOMBRE as BillerName, 
			ges.gcfcodigo as BillingGroupCode,
			geco.gcfnombre as BillingGroup,
			sls.SERVALPRO as ValueServiceManual,
			(sls.servalent + sls.servalpac + sls.servalcar) as UnitValue,
			(sls.servalent + sls.servalpac + sls.servalcar) * sls.sercantid as InvoicedValue,
			sls.sercantid as Ammount,
			sls.ccccodcen as CostCenterCode, 
			CCCNOMCEN as CostCenterName,
			''1'' as TypeServiceProduct,
			ges.siptipser as TypeProcedure,
			are.CPCCODCUE as AccountantAccountIncome,
			sls.ainconsec as Ingress,
			sls.sosordser as ServiceOrder,
			sls.serconsec as consecutiveOrder
	   FROM ..slserhoj sls INNER JOIN 
			..slordser slo ON slo.sosordser = sls.sosordser and SOSESTADO =''1''  INNER JOIN
			..slfactur slf ON sls.ainconsec = slf.ainconsec AND sls.geccodigo = slf.geccodigo AND sls.placodigo = slf.placodigo INNER JOIN 
			..geserips ges ON ges.sipcodigo = sls.sipcodigo INNER JOIN 
			..geareser are on are.gascodigo=sls.gascodigo INNER JOIN
			..GEMEDICO med ON med.gmecodigo=sls.gmecodigo INNER JOIN
			..geconfac geco ON geco.gcfcodigo = ges.gcfcodigo INNER JOIN
			..geusuari as usu on usu.usucodigo= slf.usucodigo INNER JOIN
			..ctcencos cen on cen.CCCCODCEN=sls.CCCCODCEN
		WHERE sls.ainconsec = @numeroConsecutivo AND sls.serinvent = ''0'' AND slf.sfanumfac = @numerofactura
	UNION 
		 SELECT 
			slf.sfanumfac as InvoiceNumber,
			sls.serfecser as ServiceDate,
			sls.sipcodigo as ServiceCode,
			sls.serdesser as ServiceName,
			sls.gascodigo as ServiceAreaCode,
			GASNOMBRE as DescriptionServiceArea,
			sls.gmecodigo as MedicalCode,
			med.GMENOMBRE as MedicalName,
			slf.usucodigo as BillerCode,
			usu.USUNOMBRE as BillerName, 
			inp.gcfcodigo as BillingGroupCode,
			geco.gcfnombre as BillingGroup,
			sls.SERVALPRO as ValueServiceManual,
			(sls.servalent + sls.servalpac + sls.servalcar) as UnitValue,
			(sls.servalent + sls.servalpac + sls.servalcar) * sls.sercantid as InvoicedValue,
			sls.sercantid as Ammount,
			sls.ccccodcen as CostCenterCode, 
			CCCNOMCEN as CostCenterName,
			''2'' as TypeServiceProduct,
			''4'' as TypeProcedure,
			are.CPCCODCUE as AccountantAccountIncome,
			sls.ainconsec as Ingress,
			sls.sosordser as ServiceOrder,
			sls.serconsec as consecutiveOrder
		 FROM ..slserhoj sls  INNER JOIN
			..slordser slo ON slo.sosordser = sls.sosordser and SOSESTADO =''1'' INNER JOIN
			..slfactur slf ON sls.ainconsec = slf.ainconsec AND sls.geccodigo = slf.geccodigo AND sls.placodigo = slf.placodigo INNER JOIN 
			..inproduc inp ON inp.iprcodigo = sls.sipcodigo INNER JOIN
			..geareser are on are.gascodigo=sls.gascodigo INNER JOIN
			..GEMEDICO med ON med.gmecodigo=sls.gmecodigo INNER JOIN
			..geconfac geco ON geco.gcfcodigo = inp.gcfcodigo INNER JOIN
			..geusuari as usu on usu.usucodigo= slf.usucodigo inner join
			..ctcencos cen on cen.CCCCODCEN=sls.CCCCODCEN 
		WHERE sls.ainconsec = @numeroConsecutivo AND sls.serinvent = ''1'' AND slf.sfanumfac = @numerofactura ' 

		*/

		Set @sql = 'USE ' + @container  +' 
		SELECT  
			slf.sfanumfac as InvoiceNumber,
			sls.serfecser as ServiceDate,
			ges.sipcodigo as ServiceCode,
			SUBSTRING(sls.serdesser, 1, 250) as ServiceName,
			are.GASCODIGO as ServiceAreaCode,
			are.GASNOMBRE as DescriptionServiceArea,
			null as InvoiceDetailId,
			null as ServiceOrderDetailId,
			med.gmecodigo as MedicalCode,
			med.GMENOMCOM as MedicalName,
			usu.USUNOMBRE as BillerCode,
			usu.usudescri  as BillerName, 
			geco.gcfcodigo as BillingGroupCode,
			geco.gcfnombre as BillingGroup,
			sls.SERVALPRO as ValueServiceManual,
			(sls.servalent + sls.servalpac + sls.servalcar) as UnitValue,
			(sls.servalent + sls.servalpac + sls.servalcar) * sls.sercantid as InvoicedValue,
			sls.sercantid as Ammount,
			cen.CCCODIGO as CostCenterCode, 
			cen.CCNOMBRE as CostCenterName,
			''1'' as TypeServiceProduct,
			ges.siptipser as TypeProcedure,
			ISNULL(cc.CUECODIGO, are.ctncuenta1) as AccountantAccountIncome,
			slf.adningreso as Ingress,
			slo.SOSORDSER as ServiceOrder,
			NULL as consecutiveOrder,
			NULL as ServiceNumber,
			NULL as ConsecutivoInventory
	   FROM ..slnserpro sls 
	   INNER JOIN ..SLNSERHOJ SLH ON SLS.OID=SLH.OID 
	   INNER JOIN ..slnordser slo ON slo.oid = sls.slnordser1 and SOSESTADO =''1''  
	   INNER JOIN ..slnfactur slf ON sls.adningres1 = slf.adningreso AND sls.SERENTACC = slf.SFATIFAAC
	   INNER JOIN ..genserips ges ON ges.OID = slH.GENSERIPS1 
	   INNER JOIN ..genareser are on are.OID=sls.GENARESER1 
	   INNER JOIN ..GEnMEDICO med ON med.OID=sls.GENMEDICO1 
	   INNER JOIN ..genconfac geco ON geco.OID = ges.GENCONFAC1 
	   INNER JOIN ..genusuario as usu on usu.OID= slf.GENUSUARIO1 
	   INNER JOIN ..ctncencos cen on cen.OID=sls.CTCENCOS1 
	   INNER JOIN ..GENDETCON CTO ON CTO.OID=SLF.GENDETCON 
	   LEFT JOIN ..CTNCUENTA cc ON are.ctncuenta1 = cc.OID
	   WHERE /*sls.adningres1 = @numeroConsecutivo AND*/ slf.sfanumfac = @numerofactura
	UNION ALL
		SELECT  
			slf.sfanumfac as InvoiceNumber,
			sls.serfecser as ServiceDate,
			ges.IPRCODIGO as ServiceCode,
			SUBSTRING(sls.serdesser, 1, 250) as ServiceName,
			are.GASCODIGO as ServiceAreaCode,
			are.GASNOMBRE as DescriptionServiceArea,
			null as InvoiceDetailId,
			null as ServiceOrderDetailId,
			med.gmecodigo as MedicalCode,
			med.GMENOMCOM as MedicalName,
			usu.USUNOMBRE as BillerCode,
			usu.usudescri  as BillerName, 
			geco.gcfcodigo as BillingGroupCode,
			geco.gcfnombre as BillingGroup,
			sls.SERVALPRO as ValueServiceManual,
			(sls.servalent + sls.servalpac + sls.servalcar) as UnitValue,
			(sls.servalent + sls.servalpac + sls.servalcar) * sls.sercantid as InvoicedValue,
			sls.sercantid as Ammount,
			cen.CCCODIGO as CostCenterCode, 
			cen.CCNOMBRE as CostCenterName,
			''2'' as TypeServiceProduct,
			''4'' as TypeProcedure,
			/*ges.siptipser as TypeProcedure,*/
			ISNULL(cc.CUECODIGO, are.ctncuenta1) as AccountantAccountIncome,
			slf.adningreso as Ingress,
			slo.SOSORDSER as ServiceOrder,
			NULL as consecutiveOrder,
			NULL as ServiceNumber,
			NULL as ConsecutivoInventory
	   FROM ..slnserpro sls 
	   INNER JOIN ..SLNPROHOJ SLH ON SLS.OID=SLH.OID 
	   INNER JOIN ..slnordser slo ON slo.oid = sls.slnordser1 and SOSESTADO =''1''  
	   INNER JOIN ..slnfactur slf ON sls.adningres1 = slf.adningreso AND sls.SERENTACC = slf.SFATIFAAC 
	   INNER JOIN ..INNPRODUC  ges ON ges.OID = slH.INNPRODUC1  
	   INNER JOIN ..genareser are on are.OID=sls.GENARESER1 
	   INNER JOIN ..GEnMEDICO med ON med.OID=sls.GENMEDICO1 
	   INNER JOIN ..genconfac geco ON geco.OID = ges.GENCONFAC  
	   INNER JOIN ..genusuario as usu on usu.OID= slf.GENUSUARIO1 
	   INNER JOIN ..ctncencos cen on cen.OID=sls.CTCENCOS1 
	   INNER JOIN ..GENDETCON CTO ON CTO.OID=SLF.GENDETCON
	   LEFT JOIN ..CTNCUENTA cc ON are.ctncuenta1 = cc.OID
	   WHERE /*sls.adningres1 = @numeroConsecutivo AND */ slf.sfanumfac = @numerofactura		'
	
	print @sql
	INSERT INTO @tablaDetalleFactura
	execute sp_executesql @sql,N'@numeroConsecutivo varchar(15), @numerofactura varchar(15)',@numeroConsecutivo, @numerofactura

	SELECT * FROM @tablaDetalleFactura
		

		
	--NOTA *
	--NOTA: FALTA REVISAR FILTROS CANTIDA DE FILTROS  SLS.SERCOPCTA
	--NOTA * 
END