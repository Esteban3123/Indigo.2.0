-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 07/10/2014
-- Description:	Procedimientos para listar los datos de la trazabilidad de la factura Version NET
-- =============================================
CREATE PROCEDURE [Glosas].[SP_InvoiceTraceability__NET]
	@ContainerDG varchar(30),
	@SecurityContainer varchar(50),
	@InvoiceNumber varchar(50)
AS
BEGIN
	declare @sql nvarchar(MAX)

		/* saber si es una glosa o reiteracion y cargar la fecha de creacion de oficio de glosa */
	declare @count as integer
	set @count =( select count(*) from Glosas.GlosaObjectionsReceptionD  where invoicenumber = @InvoiceNumber and state in (1,2))

	declare @DateGlosaORReiteration as date
	declare @DayFalatante as varchar(500)
	declare @Reiteration as bit
	if @count = 0 begin
		set @DayFalatante = 'No esta Proceso de Glosa'
	end else if @count = 1  begin --glosa
		set @Reiteration = 0
		select @DateGlosaORReiteration = C.RadicatedDate from Glosas.GlosaObjectionsReceptionC  C inner join Glosas.GlosaObjectionsReceptionD D  on c.id = d.GlosaObjectionsReceptionCId and D.DocumentType = '1' where invoicenumber = @InvoiceNumber
	end else if @count = 2  begin --reiteracion
		set @Reiteration = 1
		select @DateGlosaORReiteration = C.RadicatedDate from 	Glosas.GlosaObjectionsReceptionC  C inner join Glosas.GlosaObjectionsReceptionD D  on c.id = d.GlosaObjectionsReceptionCId and D.DocumentType = '2' where invoicenumber = @InvoiceNumber
	end

	
		
	--cargo parametros de tiempos	
	declare @MaxTimeExtemporaneousGlosa as integer 
	declare @MaxTimeSendingDocumentResponse as integer 
	declare @MaxTimeResponse as integer
	declare @MaxTimeExtemporaneousReiteration as integer
	declare @MaxTimeSendingReiterationDocumentResponse as integer
	declare @MaxTimeConciliation as integer

	select top 1  
		@MaxTimeExtemporaneousGlosa = MaxTimeExtemporaneousGlosa,
		@MaxTimeResponse = MaxTimeResponse,
		@MaxTimeSendingDocumentResponse = MaxTimeSendingDocumentResponse,
		@MaxTimeExtemporaneousReiteration =MaxTimeExtemporaneousReiteration,
		@MaxTimeSendingReiterationDocumentResponse = MaxTimeSendingReiterationDocumentResponse,
		@MaxTimeConciliation = MaxTimeConciliation
	from [Glosas].[TimeParameters]


	--cargo estado de cartera glosa para saber el proceso en el que esta la factura y asi determinar tiempos
	declare @State as integer = (select state from Glosas.GlosaPortfolioGlosada where invoicenumber = @InvoiceNumber)
	declare @DifereniaDias as integer
	if @State = 1 begin
		
		set @DifereniaDias = (select datediff(day,[Common].[GETDATE](),@DateGlosaORReiteration) + isnull(@MaxTimeExtemporaneousGlosa,0)) --1- Pediente Confirmado
		if @DifereniaDias = 0 begin 
			set @DayFalatante ='Hoy se vence el plazo para confirmar la glosa ' + convert(varchar(20),@DifereniaDias) 
		end else if @DifereniaDias < 0  begin
			set @DayFalatante ='lleva ' + convert(varchar(20),ABS(@DifereniaDias)) + ' dias vencido del plazo de glosa' 
		end else if @DifereniaDias > 0  begin
			set @DayFalatante ='Falta ' + convert(varchar(20),@DifereniaDias) + ' dias para vencer el plazo de glosa' 
		end 

	end else if @State = 2  begin
		
		set @DifereniaDias = (select datediff(day,[Common].[GETDATE](),@DateGlosaORReiteration) + isnull(@MaxTimeResponse,0)) --2- Pendiente Evaluacion Glosa
		if @DifereniaDias = 0 begin 
			set @DayFalatante ='Hoy se vence el plazo para evaluar la glosa ' + convert(varchar(20),@DifereniaDias) 
		end else if @DifereniaDias < 0  begin
			set @DayFalatante ='lleva ' + convert(varchar(20),ABS(@DifereniaDias)) + ' dias vencido el tramite de glosa' 
		end else if @DifereniaDias > 0  begin
			set @DayFalatante ='Falta ' + convert(varchar(20),@DifereniaDias) + ' dias para vencer el tramite de glosa' 
		end 

	end else if @State = 3  begin
		
		set @DifereniaDias = (select datediff(day,[Common].[GETDATE](),@DateGlosaORReiteration) + isnull(@MaxTimeSendingDocumentResponse,0)) --3-Pendiente envio de oficio
		if @DifereniaDias = 0 begin 
			set @DayFalatante ='Hoy se vence el plazo para enviar oficio de respuesta ' + convert(varchar(20),@DifereniaDias) 
		end else if @DifereniaDias < 0  begin
			set @DayFalatante ='lleva ' + convert(varchar(20),ABS(@DifereniaDias)) + ' dias vencido el plazo para enviar oficio de respuesta' 
		end else if @DifereniaDias > 0  begin
			set @DayFalatante ='Falta ' + convert(varchar(20),@DifereniaDias) + ' dias para vencer plazo de envio oficio de respuesta' 
		end 

	end else if @State = 4  begin
		
		set @DifereniaDias = (select datediff(day,[Common].[GETDATE](),@DateGlosaORReiteration) + isnull(@MaxTimeExtemporaneousReiteration,0)) --4 -pendiente confirmar reiteracion
		if @DifereniaDias = 0 begin 
			set @DayFalatante ='Hoy se vence el plazo para confirmar la reiteracion ' + convert(varchar(20),@DifereniaDias) 
		end else if @DifereniaDias < 0  begin
			set @DayFalatante ='lleva ' + convert(varchar(20),ABS(@DifereniaDias)) + ' dias vencido del plazo de reiteracion' 
		end else if @DifereniaDias > 0  begin
			set @DayFalatante ='Falta ' + convert(varchar(20),@DifereniaDias) + ' dias para vencer plazo de confirmación de reiteracion' 
		end 

	end else if @State = 5  begin
		
		set @DifereniaDias = (select datediff(day,[Common].[GETDATE](),@DateGlosaORReiteration) + isnull(@MaxTimeSendingReiterationDocumentResponse,0)) --5-pendiente evaluacion reitreacion
		if @DifereniaDias = 0 begin 
			set @DayFalatante ='Hoy se vence el plazo para enviar oficio de respuesta ' + convert(varchar(20),@DifereniaDias) 
		end else if @DifereniaDias < 0  begin
			set @DayFalatante ='lleva ' + convert(varchar(20),ABS(@DifereniaDias)) + ' dias vencido el plazo para enviar oficio de respuesta' 
		end else if @DifereniaDias > 0  begin
			set @DayFalatante ='Falta ' + convert(varchar(20),@DifereniaDias) + ' dias para vencer plazo de envio oficio de respuesta' 
		end 


	end else if @State = 6 or @State = 7  begin
		
		set @DifereniaDias = (select datediff(day,[Common].[GETDATE](),@DateGlosaORReiteration) + isnull(@MaxTimeConciliation,0)) --5-pendiente evaluacion reitreacion
		if @DifereniaDias = 0 begin 
			set @DayFalatante ='Hoy se vence el plazo para conciliar ' + convert(varchar(20),@DifereniaDias) 
		end else if @DifereniaDias < 0  begin
			set @DayFalatante ='lleva ' + convert(varchar(20),ABS(@DifereniaDias)) + ' dias vencido el plazo para conciliar' 
		end else if @DifereniaDias > 0  begin
			set @DayFalatante ='Falta ' + convert(varchar(20),@DifereniaDias) + ' dias para vencer el plazo para conciliar' 
		end 

	end else if @State = 7  begin
		set @DayFalatante ='pendiente de confirmar factura conciliacion' 
	end else if @State = 8  begin
		set @DayFalatante ='la factura fue conciliada' 
	end else if @State = 9  begin
		set @DayFalatante ='factura conciliada Parcialmente' 
	end else if @State = 11  begin
		set @DayFalatante ='ya se gloso, tramito y emitio oficio de respuesta' 
	end else if @State = 12  begin
		set @DayFalatante ='reiteracion con respuesta' 
	end else if @State = 13  begin
		set @DayFalatante ='factura pendiente confirmar pago parcial' 
	end else if @State = 14  begin
		set @DayFalatante ='Factura em Pago parcial confirmado' 
	end else if @State = 15  begin
		set @DayFalatante ='cobro juridico' 
	end

	

	set @sql = N'
		select car.CXCDOCUME as InvoiceNumber,
	GT.CLICODIGO  + '' - '' + GT.CLINOMBRE  as Customer,
	car.cxcdocfecha as InvoiceDate,
	usu.usudescri as BillerName,
	CON.GDECODIGO  as ContractCode,
	sal.SFATOTFAC as InvoiceValueEntity,
	sal. SFAVALPAC as InvoiceValuePacient,
	isnull(C.RadicatedConsecutive,0) as ObjectionCode,
	isnull(C.DocumentDate,''01/01/1900'') as ObjectionDate,
	isnull(P.ValueGlosado,0) as ObjectionValue,
	isnull(P.ValueAcceptedFirstInstance,0) as ValueAcceptedFirstInstance,
	isnull(CR.RadicatedConsecutive,0) as ReiterationCode,
	isnull(CR.DocumentDate,''01/01/1900'') as ReiterationDate,
	isnull(P.ValueReiterated,0) as ValueReiterated,
	isnull(P.ValueAcceptedSecondInstance,0) as ValueAcceptedSecondInstance,
	car.crnValor as InvoiceTotal,
	car.crnsaldo as PortfolioCurrentBalance,
	isnull(P.ValueGlosado,0) as GlossedTotal,
	isnull(P.BalanceGlosa,0) as BalanceReconcile,
	isnull(p.ValuePayments,0) as ValuePayments,
	isnull(P.ValueAcceptedIPSconciliation,0) + isnull(P.ValueAcceptedFirstInstance,0) + isnull(p.ValueAcceptedSecondInstance,0)  as TotalAcceptIPS,
	isnull(P.ValueAcceptedEAPBconciliation,0) + isnull(P.ValueReiterationBalance,0)  as TotalAcceptEAPB,
	CASE p.state 
		WHEN 1 THEN ''Pendiente Confirmar Glosa''
		WHEN 2 THEN ''Pendiente Evaluacion Glosa''
		WHEN 3 THEN ''Pendiente envio de oficio''
		WHEN 4 THEN ''Pendiente confirmar reiteracion''
		WHEN 5 THEN ''Pendiente evaluacion reitreacion''
		WHEN 6 THEN ''Pendiente conciliacion''
		WHEN 7 THEN ''Pendiente de confirmar Conciliacion''
		WHEN 8 THEN ''Conciliada''
		WHEN 9 THEN ''Conciliada Parcialmente''
		WHEN 11 THEN ''Glosa con Respuesta''
		WHEN 12 THEN ''reiteracion con respuesta''
		WHEN 13 THEN ''pendiente confirmar pago parcial''
		WHEN 14 THEN ''confirmado pago parcial''
		WHEN 15 THEN ''cobro juridico''
		ELSE 
			''No esta Glosada'' 
	END as State,
	CASE car.CXCESTCAR
		when 0 then ''Sin Radicar''
		when 1 then ''Radicada Sin Confirmar''
		when 2 then ''Radicado Confirmado''
		when 3 then ''Objetada''
		when 4 then ''Contestada Radicada''
		when 5 then ''Aceptada''
		when 6 then ''Certificada Parcialmente''
		when 7 then ''Certificada Total''
		when 8 then ''No Subsanable''
		when 9 then ''Difícil Recaudo''
		when 10 then ''factura Devuelta''
		when 11 then ''Glosa Ratificada''
		when 12 then ''Radicacion Tramite Objecion''
		else
		    ''Estado no determinado''
	END as StateERPIntegration,
	''' + isnull(@DayFalatante,'No se pudo calcular el tiempo')  + ''' as TimeResponse  
	from ' + @ContainerDG + '..CRNCXC car  INNER JOIN
	' + @ContainerDG + '..SLNFACTUR sal ON car.CXCDOCUME = sal.sfanumfac INNER JOIN
	' + @ContainerDG + '..GENDETCON con on con.OID=sal.GENDETCON INNER JOIN
	' + @ContainerDG + '..GENTERCERC GT on GT.OID = car.GENTERCERC INNER JOIN  
	' + @ContainerDG + '..GENUSUARIO as usu on usu.OID= sal.GENUSUARIO1 LEFT JOIN
	Glosas.GlosaObjectionsReceptionD D on D.InvoiceNumber =car.CXCDOCUME  LEFT join
	Glosas.GlosaObjectionsReceptionC  C  on c.id = d.GlosaObjectionsReceptionCId and D.DocumentType = ''1''  Left join
	Glosas.GlosaObjectionsReceptionD DR on DR.InvoiceNumber =car.CXCDOCUME and DR.DocumentType = ''2'' Left join
	Glosas.GlosaObjectionsReceptionC  CR on  CR.id = DR.GlosaObjectionsReceptionCId Left join
	Glosas.GlosaPortfolioGlosada P on P.Id =  D.PortfolioGlosaId 
	where car.CXCDOCUME = '''+ @InvoiceNumber +''' '
	 
	execute sp_executesql @sql

	print @sql
	

END