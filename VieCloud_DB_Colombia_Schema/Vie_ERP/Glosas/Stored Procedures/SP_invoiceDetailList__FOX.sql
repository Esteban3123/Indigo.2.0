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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que retorna el detalle de los ítems facturados para una factura específica, identificada por su número de factura. Consolida en un único resultado dos tipos de productos facturados: servicios de salud (procedimientos, consultas) y productos de inventario (medicamentos, insumos), tomando de cada ítem la fecha de prestación, el código y nombre del servicio o producto, el área de servicio, el médico tratante, el facturador, el grupo de facturación, los valores unitarios y totales facturados, la cantidad, el centro de costos y la cuenta contable de ingreso. Opera sobre la base de datos del contenedor HIS (empresa) especificado, filtrando por el número de factura recibido como parámetro. Se usa en el módulo de Glosas para consultar y revisar el detalle de una factura antes o durante el proceso de glosa o auditoría de cuentas médicas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailList__FOX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailList__FOX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna el detalle de servicios y productos facturados (procedimientos e insumos/inventario) asociados a una factura específica, consultando dinámicamente la base de datos del contenedor empresarial.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La base de datos pasada como contenedor debe existir y contener las tablas del esquema HIS (slserhoj, slordser, slfactur, etc.); Debe existir una factura con el número indicado; Las órdenes de servicio asociadas deben estar en estado ''1'' (activas)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El parámetro de contenedor recibido se sobrescribe forzosamente a ''DGEMPRES01'', ignorando el valor original; El valor unitario se calcula como servalent + servalpac + ISNULL(servalcar,0); El valor facturado es el valor unitario multiplicado por la cantidad (sercantid); Solo se consideran órdenes de servicio en estado ''1''; Los productos de inventario siempre tienen TypeProcedure=''4''; Los servicios no inventariables siempre tienen TypeServiceProduct=''1'' y los productos siempre TypeServiceProduct=''2''', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Detalle de factura; Servicio médico; Procedimiento; Producto de inventario / insumo; Orden de servicio; Centro de costos; Grupo de facturación; Médico; Área de servicio; Ingreso del paciente; Cuenta contable de ingreso; Facturador (usuario)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @tablaDetalleFactura: Inserta los servicios no inventariables (serinvent=''0'') de la factura, marcándolos como TypeServiceProduct=''1'' y tomando el tipo de procedimiento desde geserips.siptipser; [INSERT] @tablaDetalleFactura: Inserta los productos de inventario (serinvent=''1'') de la factura mediante UNION ALL, marcándolos como TypeServiceProduct=''2'' y TypeProcedure=''4'' fijo, resolviendo el grupo de facturación desde inproduc; [RETURN_RESULT] @tablaDetalleFactura: Devuelve el contenido completo de la tabla con los detalles de servicios y productos de la factura', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si sls.serinvent = ''0'' (registro corresponde a un servicio, no a inventario) → Se obtiene el detalle desde geserips, asignando TypeServiceProduct=''1'' y TypeProcedure desde ges.siptipser else Si serinvent=''1'', se obtiene el detalle desde inproduc, asignando TypeServiceProduct=''2'' y TypeProcedure=''4'' fijo; si SOSESTADO = ''1'' en slordser → Solo se incluyen registros cuya orden de servicio esté activa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DGEMPRES01..slserhoj; DGEMPRES01..slordser; DGEMPRES01..slfactur; DGEMPRES01..geserips; DGEMPRES01..geareser; DGEMPRES01..GEMEDICO; DGEMPRES01..geconfac; DGEMPRES01..geusuari; DGEMPRES01..ctcencos; DGEMPRES01..inproduc', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailList__FOX';
-- GO
