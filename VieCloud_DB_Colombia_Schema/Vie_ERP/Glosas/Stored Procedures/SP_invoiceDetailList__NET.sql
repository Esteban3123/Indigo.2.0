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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que retorna el detalle completo de los ítems facturados para una factura específica, identificada por número de factura y número consecutivo de ingreso. Consolida servicios médicos (procedimientos, consultas) e insumos/medicamentos en una sola lista, incluyendo fecha de prestación, código y nombre del servicio CUPS, médico tratante, facturador, grupo de facturación, centro de costo, valores unitarios, cantidades y valor total facturado. Se utiliza en el módulo de Glosas para visualizar y auditar el detalle de cada factura enviada a la aseguradora o pagador, permitiendo identificar los ítems susceptibles de glosa o devolución.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailList__NET';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailList__NET';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna el detalle consolidado (servicios y productos) de una factura específica del ERP NET, unificando información clínica, administrativa y contable para procesos de glosas.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El contenedor ''DGEMPRES99'' debe existir como base de datos accesible en la instancia; Las tablas y vistas referenciadas (slnserpro, SLNSERHOJ, SLNPROHOJ, slnordser, slnfactur, genserips, INNPRODUC, genareser, GEnMEDICO, genconfac, genusuario, ctncencos, GENDETCON, CTNCUENTA) deben existir en dicho contenedor; Debe suministrarse el número de factura (sfanumfac) para filtrar el resultado; Las órdenes de servicio asociadas deben tener SOSESTADO=''1'' para que sus detalles aparezcan', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El contenedor de datos siempre se fuerza a ''DGEMPRES99'', ignorando el contenedor recibido como parámetro; Solo se incluyen detalles cuya orden de servicio asociada esté en estado ''1'' (SOSESTADO=''1''); El nombre del servicio se trunca a 250 caracteres (SUBSTRING 1..250); El valor unitario se calcula siempre como suma de servalent + servalpac + servalcar; El valor facturado es el valor unitario multiplicado por la cantidad (sercantid); Los detalles de servicios y de productos se combinan vía UNION ALL en un único resultado homogéneo; El emparejamiento factura-servicio exige coincidencia simultánea de ingreso (adningres1=adningreso) y tipo de facturación (SERENTACC=SFATIFAAC); InvoiceDetailId, ServiceOrderDetailId, consecutiveOrder, ServiceNumber y ConsecutivoInventory siempre se devuelven NULL', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'factura; detalle de factura; servicios médicos; productos/insumos; orden de servicio; centro de costo; grupo de facturación; cuenta contable de ingreso; área de servicio; médico tratante; facturador; ingreso del paciente', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @tablaDetalleFactura: Cuando slf.sfanumfac = @numerofactura y la orden tiene SOSESTADO=''1'', se inserta una fila por cada servicio (SLNSERHOJ) marcada como TypeServiceProduct=''1''; [INSERT] @tablaDetalleFactura: Cuando slf.sfanumfac = @numerofactura y la orden tiene SOSESTADO=''1'', se inserta una fila por cada producto (SLNPROHOJ) marcada como TypeServiceProduct=''2'' y TypeProcedure=''4''; [RETURN_RESULT] RESULTSET: Devuelve todas las filas acumuladas en la tabla variable como conjunto de resultados final del procedimiento', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen del detalle: registro proveniente de SLNSERHOJ (servicios) → Se marca TypeServiceProduct=''1'' y TypeProcedure se toma de ges.siptipser, usando catálogo genserips para el código del servicio; si Origen del detalle: registro proveniente de SLNPROHOJ (productos/insumos) → Se marca TypeServiceProduct=''2'' y TypeProcedure se fija en ''4'', usando catálogo INNPRODUC para el código del producto; si Existe coincidencia entre are.ctncuenta1 y CTNCUENTA.OID (LEFT JOIN cc) → AccountantAccountIncome toma cc.CUECODIGO else AccountantAccountIncome toma are.ctncuenta1', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DGEMPRES99..slnserpro; DGEMPRES99..SLNSERHOJ; DGEMPRES99..slnordser; DGEMPRES99..slnfactur; DGEMPRES99..genserips; DGEMPRES99..genareser; DGEMPRES99..GEnMEDICO; DGEMPRES99..genconfac; DGEMPRES99..genusuario; DGEMPRES99..ctncencos; DGEMPRES99..GENDETCON; DGEMPRES99..CTNCUENTA; DGEMPRES99..SLNPROHOJ; DGEMPRES99..INNPRODUC', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__NET';
-- GO
