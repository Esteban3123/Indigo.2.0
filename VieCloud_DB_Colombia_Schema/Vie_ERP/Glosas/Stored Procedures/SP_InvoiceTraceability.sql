-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 21/09/2014
-- Description:	Procedimientos para listar los datos de la trazabilidad de la factura
-- =============================================
CREATE PROCEDURE [Glosas].[SP_InvoiceTraceability]
	@ContainerDG varchar(30),
	@SecurityContainer varchar(50),
	@InvoiceNumber varchar(50)
AS
BEGIN
	declare @sql nvarchar(MAX)

	/* saber si es una glosa o reiteracion y cargar la fecha de creacion de oficio de glosa */
	declare @count as integer
	set @count =( select count(*) from Glosas.GlosaObjectionsReceptionD WITH (NOLOCK) where invoicenumber = @InvoiceNumber and state in (1,2))

	declare @DateGlosaORReiteration as date
	declare @DayFalatante as varchar(500)
	declare @Reiteration as bit
	if @count = 0 begin
		set @DayFalatante = 'No esta Proceso de Glosa'
	end else if @count = 1  begin --glosa
		set @Reiteration = 0
		select @DateGlosaORReiteration = C.RadicatedDate from Glosas.GlosaObjectionsReceptionC C WITH (NOLOCK) inner join Glosas.GlosaObjectionsReceptionD D WITH (NOLOCK) on c.id = d.GlosaObjectionsReceptionCId and D.DocumentType = '1' where invoicenumber = @InvoiceNumber
	end else if @count = 2  begin --reiteracion
		set @Reiteration = 1
		select @DateGlosaORReiteration = C.RadicatedDate from Glosas.GlosaObjectionsReceptionC C WITH (NOLOCK) inner join Glosas.GlosaObjectionsReceptionD D WITH (NOLOCK) on c.id = d.GlosaObjectionsReceptionCId and D.DocumentType = '2' where invoicenumber = @InvoiceNumber
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
	from [Glosas].[TimeParameters] WITH (NOLOCK)

	--cargo estado de cartera glosa para saber el proceso en el que esta la factura y asi determinar tiempos
	declare @State as integer = (select state from Glosas.GlosaPortfolioGlosada WITH (NOLOCK) where invoicenumber = @InvoiceNumber)
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
	END;

	WITH cte_company as (SELECT TOP 1 OfficialCurrencyId from GeneralLedger.CompanySettings)

	select top 1  car.InvoiceNumber as InvoiceNumber,
	cust.NIT + ' - ' + cust.Name as Customer,
	car.AccountReceivableDate  as InvoiceDate,
	isnull(userC.UserCode +' - '+ Person.Fullname,'Saldo inicial')  as BillerName,
	isnull(contra.code,'Saldo inicial') as ContractCode,
	isnull(sal.ThirdPartySalesValue,car.Value)  as InvoiceValueEntity,
	isnull(sal.PatientPaidValue,0) as InvoiceValuePacient,
	isnull(C.RadicatedConsecutive,0) as ObjectionCode,
	isnull(C.DocumentDate,'01/01/1900') as ObjectionDate,
	isnull(P.ValueGlosado,0) as ObjectionValue,
	isnull(P.ValueAcceptedFirstInstance,0) as ValueAcceptedFirstInstance,
	isnull(CR.RadicatedConsecutive,0) as ReiterationCode,
	isnull(CR.DocumentDate,'01/01/1900') as ReiterationDate,
	isnull(P.ValueReiterated,0) as ValueReiterated,
	isnull(P.ValueAcceptedSecondInstance,0) as ValueAcceptedSecondInstance,
	car.value as InvoiceTotal,
	car.Balance as PortfolioCurrentBalance,
	ISNULL(icr.Value, 0) as PortfolioCurrentRetention,
	CASE car.PortfolioStatus 
		WHEN 1 THEN 'Sin Radicar'
		WHEN 2 THEN 'Radicada sin Confirmar'
		WHEN 3 THEN 'Radicada Entidad'
		WHEN 7 THEN 'Certificada Parcial'
		WHEN 8 THEN 'Certificada Total'
		WHEN 14 THEN 'Devolución Factura'
		WHEN 15 THEN 'Cuenta de Dificil Recaudo'
		WHEN 16 THEN 'Cobro Jurídico'
		ELSE ''
	END as PortfolioStatusName,
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
		ELSE 'No esta Glosada'
	END as State,
	NULL as StateERPIntegration,
	'' + isnull(@DayFalatante,'No se pudo calcular el tiempo')  + '' as TimeResponse,
	cu.id as CurrencyId,
	cu.Abbreviation AS CurrencyAbbreviation
	from portfolio.AccountReceivable car WITH (NOLOCK)
	JOIN cte_company cte WITH (NOLOCK) on 1=1 
	JOIN Common.Currency cu WITH (NOLOCK) on cu.Id = ISNULL(car.CurrencyId, cte.OfficialCurrencyId)
	LEFT JOIN Billing.invoice sal WITH (NOLOCK) on sal.Id = car.Invoiceid
	LEFT JOIN [Contract].CareGroup careGr WITH (NOLOCK) on careGr.Id =  sal.CareGroupId 
	LEFT JOIN [Contract].[Contract] contra WITH (NOLOCK) on Contra.Id = careGr.ContractId 
	LEFT JOIN [Security].[User] userC on userC.usercode = sal.InvoicedUser 
	LEFT JOIN [Security].[Person] Person on Person.Id = userC.IdPerson 
	LEFT JOIN Glosas.GlosaObjectionsReceptionD D WITH (NOLOCK) on D.InvoiceNumber =car.InvoiceNumber 
	LEFT JOIN Glosas.GlosaObjectionsReceptionC C WITH (NOLOCK) on c.id = d.GlosaObjectionsReceptionCId and D.DocumentType = '1'  
	LEFT JOIN Common.customer cust WITH (NOLOCK) on cust.id = car.CustomerId 
	LEFT JOIN Glosas.GlosaObjectionsReceptionD DR WITH (NOLOCK) on DR.InvoiceNumber =car.InvoiceNumber and DR.DocumentType = '2' 
	LEFT JOIN Glosas.GlosaObjectionsReceptionC CR WITH (NOLOCK) on  CR.id = DR.GlosaObjectionsReceptionCId 
	LEFT JOIN Glosas.GlosaPortfolioGlosada P WITH (NOLOCK) on P.Id =  D.PortfolioGlosaId 
	LEFT JOIN 
	(
		SELECT InvoiceId, SUM(Value) Value
		FROM Portfolio.ViewInvoiceCustomerRetention WITH (NOLOCK)
		WHERE InvoiceNumber = @InvoiceNumber
		GROUP BY InvoiceId
	) icr on sal.Id =  icr.InvoiceId 
	where car.InvoiceNumber = @InvoiceNumber
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que calcula y muestra la trazabilidad completa del proceso de glosas para una factura específica. Consulta el estado actual de la factura en la cartera glosada, determina si se encuentra en etapa de glosa o reiteración, y calcula los días restantes o vencidos para cada plazo del ciclo (confirmación de glosa, evaluación, envío de oficio de respuesta, reiteración, conciliación), usando los parámetros de tiempos configurados en la tabla de plazos máximos. Es utilizado por el área de cartera y glosas para hacer seguimiento al cumplimiento de términos legales ante aseguradoras y EPS, alertando sobre vencimientos o plazos pendientes en cada etapa del trámite de una factura glosada.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceability';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_InvoiceTraceability';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye la trazabilidad de una factura en el proceso de glosas, calculando el tiempo restante o vencido según el estado de cartera glosada y entregando datos consolidados de la factura, contrato, glosas, reiteraciones y conciliaciones.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la factura en portfolio.AccountReceivable con el InvoiceNumber dado; Debe existir al menos un registro en Glosas.TimeParameters para obtener los plazos máximos; Debe existir configuración en GeneralLedger.CompanySettings con OfficialCurrencyId para resolver moneda por defecto', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El plazo se computa como datediff(day, fecha_actual, fecha_radicación_glosa_o_reiteración) + parámetro de tiempo correspondiente al estado; Si no se logra calcular el tiempo, el mensaje retornado es ''No se pudo calcular el tiempo''; La moneda se resuelve usando la de la factura o, en su defecto, la moneda oficial de la empresa; Cuando no hay usuario facturador o contrato asociado, los campos se devuelven como ''Saldo inicial''; El estado del proceso de glosa (1..15) se traduce a una descripción legible en el resultado; El estado de cartera (PortfolioStatus) se traduce a etiquetas: Sin Radicar, Radicada sin Confirmar, Radicada Entidad, Certificada Parcial/Total, Devolución Factura, Cuenta Difícil Recaudo, Cobro Jurídico; TotalAcceptIPS = ValueAcceptedIPSconciliation + ValueAcceptedFirstInstance + ValueAcceptedSecondInstance; TotalAcceptEAPB = ValueAcceptedEAPBconciliation + ValueReiterationBalance; Glosa corresponde a DocumentType=''1'' y reiteración a DocumentType=''2''; Sólo se consideran glosas activas las objeciones con state in (1,2) para determinar si la factura está en proceso de glosa o reiteración', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Glosa; Reiteración; Conciliación; Cartera glosada; Oficio de respuesta; Plazo extemporáneo; Cobro jurídico; Pago parcial; Retención de cartera; Contrato; EAPB/IPS; Cliente/Entidad pagadora; Moneda oficial de la empresa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un único registro consolidado de la factura con datos de cartera, glosas, reiteraciones, conciliaciones, totales aceptados por IPS/EAPB, estado descriptivo y mensaje de tiempo restante/vencido', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si count de Glosas.GlosaObjectionsReceptionD con state in (1,2) = 0 → Marca la factura como ''No esta Proceso de Glosa''; si count = 1 (una sola objeción) → Se trata como glosa (Reiteration=0) y carga RadicatedDate del documento DocumentType=''1''; si count = 2 (dos objeciones) → Se trata como reiteración (Reiteration=1) y carga RadicatedDate del documento DocumentType=''2''; si State=1 (Pendiente Confirmar Glosa) → Calcula días contra MaxTimeExtemporaneousGlosa y arma mensaje de plazo de confirmación de glosa; si State=2 (Pendiente Evaluación Glosa) → Calcula días contra MaxTimeResponse y arma mensaje de plazo de evaluación de glosa; si State=3 (Pendiente envío de oficio) → Calcula días contra MaxTimeSendingDocumentResponse y arma mensaje de plazo para enviar oficio de respuesta; si State=4 (Pendiente confirmar reiteración) → Calcula días contra MaxTimeExtemporaneousReiteration y arma mensaje de plazo de confirmación de reiteración; si State=5 (Pendiente evaluación reiteración) → Calcula días contra MaxTimeSendingReiterationDocumentResponse y arma mensaje de plazo de envío de oficio de respuesta; si State=6 o State=7 → Calcula días contra MaxTimeConciliation y arma mensaje de plazo de conciliación; si State in (8,9,11,12,13,14,15) → Asigna mensaje fijo según el estado (conciliada, conciliada parcialmente, glosa con respuesta, reiteración con respuesta, pendiente/confirmado pago parcial, cobro jurídico); si DifereniaDias = 0 / <0 / >0 → Genera mensaje ''Hoy se vence'', ''lleva N días vencido'' o ''Falta N días para vencer'' respectivamente', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; Glosas.TimeParameters; Glosas.GlosaPortfolioGlosada; GeneralLedger.CompanySettings; portfolio.AccountReceivable; Common.Currency; Billing.invoice; Contract.CareGroup; Contract.Contract; Security.User; Security.Person; Common.customer; Portfolio.ViewInvoiceCustomerRetention', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_InvoiceTraceability';
-- GO
