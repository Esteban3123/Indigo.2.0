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