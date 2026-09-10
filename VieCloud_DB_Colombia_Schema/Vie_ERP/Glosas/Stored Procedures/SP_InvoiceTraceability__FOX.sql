-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 21/09/2014
-- Description:	Procedimientos para listar los datos de la trazabilidad de la factura
-- =============================================
CREATE PROCEDURE [Glosas].[SP_InvoiceTraceability__FOX]
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
	select car.CEMNUMFAC as InvoiceNumber,
	cust.NIT + '' - '' + cust.Name as Customer,
	car.CEMFECFAC as InvoiceDate,
	slf.usucodigo +'' - ''+ usu.USUNOMBRE as BillerName,
	slf.GECCODIGO as ContractCode,
	slf.SFATOTFAC as InvoiceValueEntity,
	slf. SFAVALPAC as InvoiceValuePacient,
	isnull(C.RadicatedConsecutive,0) as ObjectionCode,
	isnull(C.DocumentDate,''01/01/1900'') as ObjectionDate,
	isnull(P.ValueGlosado,0) as ObjectionValue,
	isnull(P.ValueAcceptedFirstInstance,0) as ValueAcceptedFirstInstance,
	isnull(CR.RadicatedConsecutive,0) as ReiterationCode,
	isnull(CR.DocumentDate,''01/01/1900'') as ReiterationDate,
	isnull(P.ValueReiterated,0) as ValueReiterated,
	isnull(P.ValueAcceptedSecondInstance,0) as ValueAcceptedSecondInstance,
	car.CEMVRFACT as InvoiceTotal,
	car.CEMSALFAC as PortfolioCurrentBalance,
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
	CASE car.cemestado
		when 1 then ''Facturado''
		when ''T'' then ''Radicación Sin Confirmar''
		when 2 then ''Radicado Confirmado''
		when 3 then ''Glosado''
		when 4 then ''Glosa Aceptada''
		when 5 then ''Certificado''
		when 6 then ''Anula Paciente''
		when ''A'' then ''Anulada''
		else
		    ''Estado no determinado''
	END as StateERPIntegration,
	''' + isnull(@DayFalatante,'No se pudo calcular el tiempo')  + ''' as TimeResponse 
	from ' + @ContainerDG + '..CRCARTER car  INNER JOIN
	'+ @ContainerDG +'..slfactur slf ON car.cemnumfac = slf.sfanumfac INNER JOIN
	Portfolio.RadicateInvoiceD RD on RD.invoicenumber = car.cemnumfac INNER JOIN
	Portfolio.RadicateInvoiceC RC on RC.id= RD.RAdicateInvoiceCid INNER JOIN
	Common.customer cust on cust.id = RC.CustomerId  INNER JOIN
	' + @ContainerDG + '..geusuari as usu on usu.usucodigo= slf.usucodigo LEFT JOIN
	Glosas.GlosaObjectionsReceptionD D on D.InvoiceNumber =car.cemnumfac  LEFT join
	Glosas.GlosaObjectionsReceptionC  C  on c.id = d.GlosaObjectionsReceptionCId and D.DocumentType = ''1''  Left join
	Glosas.GlosaObjectionsReceptionD DR on DR.InvoiceNumber =car.cemnumfac and DR.DocumentType = ''2'' Left join
	Glosas.GlosaObjectionsReceptionC  CR on  CR.id = DR.GlosaObjectionsReceptionCId Left join
	Glosas.GlosaPortfolioGlosada P on P.Id =  D.PortfolioGlosaId 
	where car.CEMNUMFAC = '''+ @InvoiceNumber +''' '
	execute sp_executesql @sql

	print @sql
/*select car.CEMNUMFAC as InvoiceNumber,
	cust.NIT + ' - ' + cust.Name as Customer,
	car.CEMFECFAC as InvoiceDate,
	slf.usucodigo +' - '+ usu.USUNOMBRE as BillerName,
	slf.GECCODIGO as ContractCode,
	slf.SFATOTFAC as InvoiceValueEntity,
	slf. SFAVALPAC as InvoiceValuePacient,
	isnull(C.RadicatedConsecutive,0) as ObjectionCode,
	isnull(C.DocumentDate,'01/01/1900') as ObjectionDate,
	isnull(P.ValueGlosado,0) as ObjectionValue,
	isnull(P.ValueAcceptedFirstInstance,0) as ValueAcceptedFirstInstance,
	isnull(CR.RadicatedConsecutive,0) as ReiterationCode,
	isnull(CR.DocumentDate,'01/01/1900') as ReiterationDate,
	isnull(P.ValueReiterated,0) as ValueReiterated,
	isnull(P.ValueAcceptedSecondInstance,0) as ValueAcceptedSecondInstance,
	car.CEMVRFACT as InvoiceTotal,
	car.CEMSALFAC as PortfolioCurrentBalance,
	isnull(P.ValueGlosado,0) as GlossedTotal,
	isnull(P.BalanceGlosa,0) as BalanceReconcile,
	isnull(p.ValuePayments,0) as ValuePayments,
	isnull(P.ValueAcceptedIPSconciliation,0) + isnull(P.ValueAcceptedFirstInstance,0) + isnull(p.ValueAcceptedSecondInstance,0)  as TotalAcceptIPS,
	isnull(P.ValueAcceptedEAPBconciliation,0) + isnull(P.ValueReiterationBalance,0)  as TotalAcceptEAPB,
	CASE p.state 
		WHEN 1 THEN 'Pendiente Confirmar Glosa'
		WHEN 2 THEN 'Pendiente Evaluacion Glosa'
		WHEN 3 THEN 'Pendiente envio de oficio'
		WHEN 4 THEN 'Pendiente confirmar reiteracion'
		WHEN 5 THEN 'Pendiente evaluacion reitreacion'
		WHEN 6 THEN 'Pendiente conciliacion'
		WHEN 7 THEN 'Pendiente de confirmar Conciliacion'
		WHEN 8 THEN 'Conciliada'
		WHEN 9 THEN 'Conciliada Parcialmente'
		WHEN 11 THEN 'Glosa con Respuesta'
		WHEN 12 THEN 'reiteracion con respuesta'
		WHEN 13 THEN 'pendiente confirmar pago parcial'
		WHEN 14 THEN 'confirmado pago parcial'
		WHEN 15 THEN 'cobro juridico'
		ELSE 
			'No esta Glosada' 
	END as State,
	CASE car.cemestado
		when 1 then 'Facturado'
		when 'T' then 'Radicación Sin Confirmar'
		when 2 then 'Radicado Confirmado'
		when 3 then 'Glosado'
		when 4 then 'Glosa Aceptada'
		when 5 then 'Certificado'
		when 6 then 'Anula Paciente'
		when 'A' then 'Anulada'
		else
		    'Estado no determinado'
	END as StateERPIntegration,
	'No esta Proceso de Glosa' as TimeResponse 
	from .CRCARTER car  INNER JOIN
	.slfactur slf ON car.cemnumfac = slf.sfanumfac INNER JOIN
	Glosas.RadicateInvoiceD RD on RD.invoicenumber = car.cemnumfac INNER JOIN
	Glosas.RadicateInvoiceC RC on RC.id= RD.RAdicateInvoiceCid INNER JOIN
	Common.customer cust on cust.id = RC.CustomerId  INNER JOIN
	.geusuari as usu on usu.usucodigo= slf.usucodigo LEFT JOIN
	Glosas.GlosaObjectionsReceptionD D on D.InvoiceNumber =car.cemnumfac  LEFT join
	Glosas.GlosaObjectionsReceptionC  C  on c.id = d.GlosaObjectionsReceptionCId and D.DocumentType = '1'  Left join
	Glosas.GlosaObjectionsReceptionD DR on DR.InvoiceNumber =car.cemnumfac and DR.DocumentType = '2' Left join
	Glosas.GlosaObjectionsReceptionC  CR on  CR.id = DR.GlosaObjectionsReceptionCId Left join
	Glosas.GlosaPortfolioGlosada P on P.Id =  D.PortfolioGlosaId 
	where car.CEMNUMFAC = '00000000665229' 
 */

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera la trazabilidad completa de una factura dentro del ciclo de glosas: determina si la factura está en proceso de glosa o reiteración, calcula los días faltantes o vencidos para cada etapa del trámite (confirmación de glosa, evaluación, envío de oficio de respuesta, reiteración, conciliación) usando los parámetros de tiempo configurados en TimeParameters y la fecha de radicación registrada en GlosaObjectionsReceptionC/D. Consulta el estado actual de la factura en la cartera glosada (GlosaPortfolioGlosada) y construye dinámicamente mediante SQL generado en runtime un resumen del historial de la factura cruzando múltiples tablas del módulo de glosas. Es utilizado por el área de cartera y glosas para hacer seguimiento al vencimiento de plazos legales de cada factura objetada por una EPS o aseguradora, alertando sobre fechas críticas del proceso de glosa, reiteración y conciliación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceability__FOX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceability__FOX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye y ejecuta dinámicamente una consulta de trazabilidad de una factura, retornando datos de cartera, radicación, glosa, reiteración, conciliación y un mensaje de tiempo restante o vencido según el estado actual del proceso de glosa.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir en CRCARTER y SLFACTUR del contenedor ERP indicado; Debe existir radicación previa en Portfolio.RadicateInvoiceD/C asociada a la factura para que el SELECT principal retorne filas; La tabla Glosas.TimeParameters debe contener al menos un registro con parámetros de tiempo, de lo contrario los plazos quedan en NULL; El contenedor ERP suministrado debe ser accesible vía nombre de base de datos en el SQL dinámico', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El cálculo de días siempre usa Common.GETDATE() como fecha actual del sistema; Si los parámetros de tiempo son NULL se sustituyen por 0 vía ISNULL antes de sumarlos a la fecha base; Los valores monetarios devueltos son protegidos con ISNULL a 0 y las fechas faltantes a ''01/01/1900''; La rama state=7 dentro del bloque conciliación se evalúa antes que la rama posterior ''state=7 pendiente confirmar conciliacion'', por lo que la segunda nunca se ejecuta (código inalcanzable); Solo se consideran documentos de objeción tipo ''1'' (glosa) y ''2'' (reiteración)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Reiteración; Conciliación; Cartera glosada; Radicación de factura; Oficio de respuesta; Plazos de respuesta (extemporáneo); Pago parcial; Cobro jurídico; IPS; EAPB; Cliente/Pagador (NIT); Facturador (usuario); Contrato', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando hay 0 documentos de glosa/reiteración para la factura (state IN (1,2)), el campo TimeResponse retorna ''No esta Proceso de Glosa''; [RETURN_RESULT] N/A: Cuando existe exactamente 1 documento (glosa) se toma RadicatedDate del documento DocumentType=''1''; cuando existen 2 (reiteración) se toma del DocumentType=''2'' para calcular el plazo; [RETURN_RESULT] N/A: Cuando state=1 el plazo se calcula con MaxTimeExtemporaneousGlosa y se retorna mensaje de confirmación de glosa (hoy vence / lleva N días vencido / faltan N días); [RETURN_RESULT] N/A: Cuando state=2 el plazo se calcula con MaxTimeResponse para evaluación de glosa; [RETURN_RESULT] N/A: Cuando state=3 el plazo se calcula con MaxTimeSendingDocumentResponse para envío de oficio de respuesta; [RETURN_RESULT] N/A: Cuando state=4 el plazo se calcula con MaxTimeExtemporaneousReiteration para confirmación de reiteración; [RETURN_RESULT] N/A: Cuando state=5 el plazo se calcula con MaxTimeSendingReiterationDocumentResponse para evaluación de reiteración; [RETURN_RESULT] N/A: Cuando state=6 o 7 el plazo se calcula con MaxTimeConciliation para conciliación; [RETURN_RESULT] N/A: Cuando state=8 retorna ''la factura fue conciliada''; state=9 ''factura conciliada Parcialmente''; state=11 ''ya se gloso, tramito y emitio oficio de respuesta''; state=12 ''reiteracion con respuesta''; state=13 ''factura pendiente confirmar pago parcial''; state=14 ''Factura em Pago parcial confirmado''; state=15 ''cobro juridico''; [RETURN_RESULT] N/A: TotalAcceptIPS = ValueAcceptedIPSconciliation + ValueAcceptedFirstInstance + ValueAcceptedSecondInstance; TotalAcceptEAPB = ValueAcceptedEAPBconciliation + ValueReiterationBalance; [RETURN_RESULT] N/A: El estado de glosa se traduce a etiqueta legible (1..15) y el estado ERP de cartera (cemestado) se traduce a Facturado/Radicado/Glosado/Anulada/etc.; valores no contemplados muestran ''No esta Glosada'' o ''Estado no determinado''', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si count de Glosas.GlosaObjectionsReceptionD con state IN (1,2) = 0 → marca TimeResponse como ''No esta Proceso de Glosa'' else según el conteo (1=glosa, 2=reiteración) se toma la fecha de radicación del documento correspondiente; si @State (Glosas.GlosaPortfolioGlosada) entre 1 y 7 → calcula días restantes/vencidos sumando el parámetro de tiempo correspondiente a la etapa else para estados 8..15 retorna mensaje fijo descriptivo del estado final; si @DifereniaDias = 0 / < 0 / > 0 → genera mensaje ''Hoy se vence'', ''lleva N dias vencido'' o ''Falta N dias para vencer'' respectivamente', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; Glosas.TimeParameters; Glosas.GlosaPortfolioGlosada; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; Common.customer; CRCARTER; SLFACTUR; GEUSUARI', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability__FOX';
-- GO
