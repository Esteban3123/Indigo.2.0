-- =============================================
-- Author:		Rafael Patiño
-- Create date: 28/05/2013
-- Description:	Sp que retorna los detalles de las facturas Quirurgicos
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceDetailListQX__NET]
	@container varchar(50),
	@numeroConsecutivo varchar(15),
	@ordenServicio varchar(15),  --para net no aplica
	@ServiceCode varchar(15),
	@consecutiveOrder varchar(15),   --para net no aplica
	@ServiceNumber as varchar(15),    --para net not aplica
	@ConsecutivoInventory as varchar(15) --para net no aplica
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    set @container = 'DGEMPRES99'
	/*set @container = 'DGEMPRES12'
	set @numeroConsecutivo = '0000002016'
	set @ordenServicio = '0000028820'*/
	
	Declare @tablaDetalleFactura table(
	    ServiceOrderDetailSurgicalId int,
		ServiceCode varchar(20),
		ServiceName varchar(500),
		MedicalCode varchar(20),
		MedicalName varchar(200),
		ValueServiceManual money,
		UnitValue money,
		InvoicedValue money, 
		Ammount int,
		CostCenterCode varchar(100),
		CostCenterName varchar(500),
		ServiceAreaCode varchar(10),
		DescriptionServiceArea varchar(300),
		AccountantAccountIncome varchar(30)
		)

		
		declare @sql as nVarchar(max)
	/*
	set @sql= 'USE ' + @container  +' 
		SELECT 
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
		WHERE ainconsec=@numeroConsecutivo and sosordser=@ordenServicio '*/

			set @sql= 'USE ' + @container  +' 
		SELECT 
		    null as ServiceOrderDetailSurgicalId,
			RTRIM(ges.SIPCODIGO) AS ServiceCode,
			RTRIM(ges.SIPNOMBRE) as ServiceName,
			RTRIM(MED.gmecodigo) as MedicalCode,
			RTRIM(med.GMENOMCOM) as MedicalName,
			slp.sphvalser as ValueServiceManual,
			(slp.sphtotent+slp.sphtotpac) as UnitValue,
			(slp.sphtotent+slp.sphtotpac) * slp.sphcanser as InvoicedValue,
			slp.sphcanser as Ammount,
			RTRIM(CEN.CCCODIGO) as CostCenterCode,
			RTRIM(CEN.CCNOMBRE) as CostCenterName,
			RTRIM(ARE.GASCODIGO) as ServiceAreaCode,
			RTRIM(are.GASNOMBRE) as DescriptionServiceArea,
			RTRIM(ISNULL(cc.CUECODIGO, are.ctncuenta1)) as AccountantAccountIncome
		FROM 
			..slnpaqhoj slp INNER JOIN
			..SLNSERHOJ sls on  slp.slnserhoj1=sls.oid inner join
			..slnserpro sl on sl.OID=sls.OID INNER JOIN 
			..slnordser slo ON slo.oid = sl.slnordser1 and SOSESTADO =''1''  INNER JOIN 
			..genserips ges ON ges.oid = sls.genserips1 INNER JOIN
			..genareser are on are.oid=slp.genareser1 INNER JOIN
			..ctncencos cen on cen.oid=slp.ctcencos1 LEFT OUTER JOIN
			..genmedico med on med.oid=slp.genmedico1 LEFT JOIN 
			..CTNCUENTA cc ON are.ctncuenta1 = cc.OID
		WHERE sl.adningres1=@numeroConsecutivo and slo.SOSORDSER=@ordenServicio AND ges.sipcodigo = @ServiceCode '

		--where SLs.ainconsec = 

		
	print @sql
	INSERT INTO @tablaDetalleFactura
	--execute sp_executesql @sql,N'@numeroConsecutivo varchar(15), @ordenServicio varchar(15)',@numeroConsecutivo, @ordenServicio
	execute sp_executesql @sql,N'@numeroConsecutivo varchar(15), @ordenServicio varchar(15), @ServiceCode varchar(15)', @numeroConsecutivo,@ordenServicio,@ServiceCode

	SELECT * FROM @tablaDetalleFactura

	--NOTA *
	--NOTA: FALTA REVISAR FILTROS....
	--NOTA * 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que retorna el detalle de los servicios quirúrgicos incluidos en una factura específica, utilizado en el módulo de Glosas para revisar y auditar los ítems facturados en cirugías. Recibe como parámetros el contenedor (empresa), el número consecutivo de ingreso y el código de servicio CUPS, y consulta dinámicamente las tablas de órdenes quirúrgicas, servicios, áreas de servicio, centros de costo y médicos del sistema clínico para devolver por cada línea: el código y nombre del servicio, el médico tratante, el valor manual, el valor unitario, el valor facturado, la cantidad, el centro de costo y la cuenta contable de ingreso. Existe para soportar la conciliación y revisión de glosas en facturas de procedimientos quirúrgicos, permitiendo identificar discrepancias entre lo ordenado y lo cobrado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailListQX__NET';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceDetailListQX__NET';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna el detalle de servicios quirúrgicos facturados (hoja quirúrgica) de una orden de servicio específica, incluyendo valores, médico tratante, centro de costo, área de servicio y cuenta contable de ingreso.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El contenedor (base de datos) destino debe existir y ser accesible vía USE dinámico (forzado a ''DGEMPRES99'').; La orden de servicio debe estar en estado activo (SOSESTADO = ''1'').; Debe existir relación válida entre slnpaqhoj, slnserhoj, slnserpro y slnordser por sus OIDs.; El ingreso (adningres1) y la orden (SOSORDSER) deben coincidir con los parámetros recibidos.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El parámetro @container es ignorado: siempre se sobreescribe a ''DGEMPRES99'' antes de armar el SQL dinámico.; Solo se consideran órdenes de servicio activas (SOSESTADO=''1'').; InvoicedValue siempre se calcula como (sphtotent + sphtotpac) * sphcanser.; UnitValue siempre es la suma de la porción a cargo de la entidad y del paciente (sphtotent + sphtotpac).; ServiceOrderDetailSurgicalId siempre se devuelve en NULL.; El médico es opcional (LEFT JOIN sobre genmedico).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura quirúrgica; Hoja quirúrgica / paquete quirúrgico; Orden de servicio; Servicio médico (SIP); Médico tratante; Centro de costo; Área de servicio; Cuenta contable de ingreso; Valor a cargo de entidad y paciente; Ingreso (admisión)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @tablaDetalleFactura: Inserta el resultado del SELECT dinámico filtrado por ingreso, orden de servicio y código de servicio quirúrgico.; [RETURN_RESULT] @tablaDetalleFactura: Devuelve todos los registros acumulados en la tabla variable como resultset final.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ges.sipcodigo coincide con el código de servicio y la orden está SOSESTADO=''1'' → Se incluye el detalle quirúrgico en el resultado else No se retorna fila para esa combinación; si cc.CUECODIGO IS NULL (no hay cuenta contable mapeada vía CTNCUENTA) → Se usa are.ctncuenta1 como AccountantAccountIncome else Se usa cc.CUECODIGO como AccountantAccountIncome', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sp_executesql', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'slnpaqhoj; SLNSERHOJ; slnserpro; slnordser; genserips; genareser; ctncencos; genmedico; CTNCUENTA', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceDetailListQX__NET';
-- GO
