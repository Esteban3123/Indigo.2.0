-- =============================================
-- Author:		Rafael Patiño
-- Create date: 28/05/2013
-- Description:	Sp que retorna los detalles de las facturas Quirurgicos
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceDetailListQX__FOX]
	@container varchar(50),
	@numeroConsecutivo varchar(15),
	@ordenServicio varchar(15),
	@ServiceCode varchar(15),
	@consecutiveOrder varchar(15),
	@ServiceNumber as varchar(15),
	@ConsecutivoInventory as varchar(15)
AS
BEGIN

	SET NOCOUNT ON;

	set @container = 'DGEMPRES12'

	/*set @container = 'DGEMPRES12'
	set @numeroConsecutivo = '0000002016'
	set @ordenServicio = '0000028820'*/
	
	Declare @tablaDetalleFactura table(
		ServiceOrderDetailSurgicalId int,
		ServiceCode varchar(20),
		ServiceName varchar(300),
		MedicalCode varchar(20),
		MedicalName varchar(200),
		ValueServiceManual money,
		UnitValue money,
		InvoicedValue money, 
		Ammount int,
		CostCenterCode varchar(14),
		CostCenterName varchar(500),
		ServiceAreaCode varchar(10),
		DescriptionServiceArea varchar(300),
		AccountantAccountIncome varchar(30)
		)

		
		declare @sql as nVarchar(max)
	
	set @sql= 'USE ' + @container  +' 
		SELECT 
			null as ServiceOrderDetailSurgicalId,
			RTRIM(SIPCODIG1) AS ServiceCode,
			RTRIM(ges.SIPNOMBRE) as ServiceName,
			RTRIM(slp.gmecodigo) as MedicalCode,
			RTRIM(med.GMENOMBRE) as MedicalName,
			slp.sphvalser as ValueServiceManual,
			(slp.sphtotent+slp.sphtotpac) as UnitValue,
			(slp.sphtotent+slp.sphtotpac) * slp.sphcanser as InvoicedValue,
			slp.sphcanser as Ammount,
			RTRIM(slp.ccccodcen) as CostCenterCode,
			RTRIM(CCCNOMCEN) as CostCenterName,
			RTRIM(slp.gascodig1) as ServiceAreaCode,
			RTRIM(are.GASNOMBRE) as DescriptionServiceArea,
			RTRIM(are.CPCCODCUE) as AccountantAccountIncome
		FROM 
			..slpaqhoj slp INNER JOIN
			..geserips ges ON ges.sipcodigo = slp.sipcodig1 INNER JOIN
			..geareser are on are.gascodigo=slp.gascodig1 INNER JOIN
			..ctcencos cen on cen.CCCCODCEN=slP.CCCCODCEN LEFT OUTER JOIN
			..gemedico med on med.gmecodigo=slp.gmecodigo 
		WHERE ainconsec=@numeroConsecutivo  and sosordser=@ordenServicio AND slp.sipcodigo = @ServiceCode AND slp.sphconsec = @consecutiveOrder AND  slp.sphnumero = @ServiceNumber  AND  slp.sphconinv = @ConsecutivoInventory'
			
		--	slp.SIPCODIG1 = ''S41201'' and   slp.sphconsec = ''0801'' AND  slp.sphnumero = ''01''  AND  slp.sphconinv = ''0001''
			--and sosordser =''0000020684'' and  ainconsec =''0000000104'''
		

	--	
		
	print @sql
	INSERT INTO @tablaDetalleFactura
	execute sp_executesql @sql ,N'@numeroConsecutivo varchar(15), @ordenServicio varchar(15), @ServiceCode varchar(15),@consecutiveOrder varchar(15),@ServiceNumber varchar(15),@ConsecutivoInventory varchar(15)',@numeroConsecutivo,@ordenServicio,@ServiceCode,@consecutiveOrder,@ServiceNumber,@ConsecutivoInventory

	SELECT * FROM @tablaDetalleFactura

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que retorna el detalle de los servicios quirúrgicos incluidos en una factura específica, consultando la información del módulo de glosas. Dado un número de ingreso (consecutivo de admisión), una orden de servicio quirúrgico y filtros adicionales como código de servicio, consecutivo de orden, número de servicio y consecutivo de inventario, devuelve por cada ítem el código y nombre del servicio (CUPS), el médico tratante, los valores facturados (valor manual, valor unitario e importe total), la cantidad, el centro de costo y el área de servicio con su cuenta contable de ingresos. Se utiliza en el proceso de revisión y auditoría de glosas para verificar la composición económica de facturas quirúrgicas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailListQX__FOX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailListQX__FOX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna el detalle de servicios quirúrgicos asociados a una factura específica, consultando la base de datos externa de FOX para enriquecer con datos de servicio, médico, centro de costo y área de servicio.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La base de datos externa ''DGEMPRES12'' (FOX) debe existir y ser accesible desde el servidor; Las tablas slpaqhoj, geserips, geareser, ctcencos y gemedico deben existir en el contenedor; Deben proporcionarse los identificadores de consecutivo factura, orden de servicio, código de servicio, consecutivo de hoja quirúrgica, número de servicio y consecutivo de inventario', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El contenedor de base de datos siempre se sobrescribe a ''DGEMPRES12'' ignorando el parámetro recibido; El valor facturado siempre se calcula como (porción entidad + porción paciente) multiplicado por la cantidad del servicio; ServiceOrderDetailSurgicalId siempre se devuelve como NULL; La unión con la tabla de médicos es opcional (LEFT OUTER JOIN), permitiendo registros sin médico asignado', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Detalle de factura quirúrgica; Orden de servicio; Servicio quirúrgico; Médico; Centro de costo; Área de servicio; Cuenta contable de ingreso; Valor entidad; Valor paciente; Hoja quirúrgica', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @tablaDetalleFactura: Inserta el resultado del SELECT dinámico ejecutado contra la BD externa filtrando por ainconsec, sosordser, sipcodigo, sphconsec, sphnumero y sphconinv; [RETURN_RESULT] RESULT: Devuelve el detalle de la factura quirúrgica con servicio, médico, valores unitarios, valor facturado (sphtotent+sphtotpac)*sphcanser, centro de costo y cuenta contable de ingreso', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DGEMPRES12..slpaqhoj; DGEMPRES12..geserips; DGEMPRES12..geareser; DGEMPRES12..ctcencos; DGEMPRES12..gemedico', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__FOX';
-- GO
