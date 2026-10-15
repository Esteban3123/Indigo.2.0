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