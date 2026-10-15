-- =============================================
-- Author:		Rafael Patiño
-- Create date: 19/04/2013
-- Description:	Sp que retorna los detalles de las facturas por contenedor
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceDetailList__FOX]
	@container varchar(50),
	@HISContainer varchar(50),
	@SecurityContainer varchar(50),
	@numerofactura varchar(15),
	@numeroConsecutivo varchar(15)
AS
BEGIN

	SET NOCOUNT ON;


	set @container = 'DGEMPRES01'

	/*set @container = 'DGEMPRES12'
	set @numeroConsecutivo = '0000011030'
	'set @numerofactura = '0000011030'*/
	
	Declare @tablaDetalleFactura table(
		InvoiceNumber varchar(50),
		ServiceDate datetime,
		ServiceCode varchar(20),    --dato para filtro
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
	
	set @sql= 'USE ' + @container  +' 
		SELECT  
			slf.sfanumfac as InvoiceNumber,
			sls.serfecser as ServiceDate,
			sls.sipcodigo as ServiceCode,
			sls.serdesser as ServiceName,
			sls.gascodigo as ServiceAreaCode,
			GASNOMBRE as DescriptionServiceArea,
			null as InvoiceDetailId,
			null as ServiceOrderDetailId,
			sls.gmecodigo as MedicalCode,
			med.GMENOMBRE as MedicalName,
			slf.usucodigo as BillerCode,
			usu.USUNOMBRE as BillerName, 
			ges.gcfcodigo as BillingGroupCode,
			geco.gcfnombre as BillingGroup,
			sls.SERVALPRO as ValueServiceManual,
			(sls.servalent + sls.servalpac + isnull(sls.servalcar,0)) as UnitValue,
			(sls.servalent + sls.servalpac + isnull(sls.servalcar,0)) * sls.sercantid as InvoicedValue,
			sls.sercantid as Ammount,
			sls.ccccodcen as CostCenterCode, 
			CCCNOMCEN as CostCenterName,
			''1'' as TypeServiceProduct,
			ges.siptipser as TypeProcedure,
			are.CPCCODCUE as AccountantAccountIncome,
			sls.ainconsec as Ingress,
			sls.sosordser as ServiceOrder,
			sls.serconsec as consecutiveOrder,
			sls.SERNUMERO as ServiceNumber,
			sls.SERCONINV as ConsecutivoInventory
	   FROM ..slserhoj sls INNER JOIN 
			..slordser slo ON slo.sosordser = sls.sosordser and SOSESTADO =''1''  INNER JOIN
			..slfactur slf ON sls.ainconsec = slf.ainconsec AND sls.geccodigo = slf.geccodigo AND sls.placodigo = slf.placodigo INNER JOIN 
			..geserips ges ON ges.sipcodigo = sls.sipcodigo INNER JOIN 
			..geareser are on are.gascodigo=sls.gascodigo INNER JOIN
			..GEMEDICO med ON med.gmecodigo=sls.gmecodigo INNER JOIN
			..geconfac geco ON geco.gcfcodigo = ges.gcfcodigo INNER JOIN
			..geusuari as usu on usu.usucodigo= slf.usucodigo INNER JOIN
			..ctcencos cen on cen.CCCCODCEN=sls.CCCCODCEN
		WHERE sls.serinvent = ''0'' AND slf.sfanumfac = @numerofactura
	UNION  ALL
		 SELECT 
			slf.sfanumfac as InvoiceNumber,
			sls.serfecser as ServiceDate,
			sls.sipcodigo as ServiceCode,
			sls.serdesser as ServiceName,
			sls.gascodigo as ServiceAreaCode,
			GASNOMBRE as DescriptionServiceArea,
			null as InvoiceDetailId,
			null as ServiceOrderDetailId,
			sls.gmecodigo as MedicalCode,
			med.GMENOMBRE as MedicalName,
			slf.usucodigo as BillerCode,
			usu.USUNOMBRE as BillerName, 
			inp.gcfcodigo as BillingGroupCode,
			geco.gcfnombre as BillingGroup,
			sls.SERVALPRO as ValueServiceManual,
			(sls.servalent + sls.servalpac + isnull(sls.servalcar,0)) as UnitValue,
			(sls.servalent + sls.servalpac + isnull(sls.servalcar,0)) * sls.sercantid as InvoicedValue,
			sls.sercantid as Ammount,
			sls.ccccodcen as CostCenterCode, 
			CCCNOMCEN as CostCenterName,
			''2'' as TypeServiceProduct,
			''4'' as TypeProcedure,
			are.CPCCODCUE as AccountantAccountIncome,
			sls.ainconsec as Ingress,
			sls.sosordser as ServiceOrder,
			sls.serconsec as consecutiveOrder,
			sls.SERNUMERO as ServiceNumber,
			sls.SERCONINV as ConsecutivoInventory
		 FROM ..slserhoj sls  INNER JOIN
			..slordser slo ON slo.sosordser = sls.sosordser and SOSESTADO =''1'' INNER JOIN
			..slfactur slf ON sls.ainconsec = slf.ainconsec AND sls.geccodigo = slf.geccodigo AND sls.placodigo = slf.placodigo INNER JOIN 
			..inproduc inp ON inp.iprcodigo = sls.sipcodigo INNER JOIN
			..geareser are on are.gascodigo=sls.gascodigo INNER JOIN
			..GEMEDICO med ON med.gmecodigo=sls.gmecodigo INNER JOIN
			..geconfac geco ON geco.gcfcodigo = inp.gcfcodigo INNER JOIN
			..geusuari as usu on usu.usucodigo= slf.usucodigo inner join
			..ctcencos cen on cen.CCCCODCEN=sls.CCCCODCEN 
		WHERE  sls.serinvent = ''1'' AND slf.sfanumfac = @numerofactura '
	
	print @sql
	INSERT INTO @tablaDetalleFactura
	execute sp_executesql @sql,N'@numeroConsecutivo varchar(15), @numerofactura varchar(15)',@numeroConsecutivo, @numerofactura

	SELECT * FROM @tablaDetalleFactura
		

END