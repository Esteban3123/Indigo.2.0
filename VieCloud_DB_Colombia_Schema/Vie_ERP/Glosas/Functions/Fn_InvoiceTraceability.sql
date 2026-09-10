-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [Glosas].[Fn_InvoiceTraceability]
(	
	@ContainerDG varchar(30),
	@SecurityContainer varchar(50),
	@InvoiceNumber varchar(50)
)
RETURNS @rtnTable TABLE 
(
	InvoiceNumber varchar(50),
	 Customer varchar(100),
	 InvoiceDate datetime,
	 BillerName varchar(100),
	 ContractCode varchar(20),
	InvoiceValueEntity decimal,
	InvoiceValuePacient decimal,
	ObjectionCode varchar(10),
	ObjectionDate datetime,
	ObjectionValue decimal,
	ValueAcceptedFirstInstance decimal,
	ReiterationCode varchar(10),
	ReiterationDate datetime,
	ValueReiterated decimal,
	ValueAcceptedSecondInstance decimal,
	InvoiceTotal decimal,
	PortfolioCurrentBalance decimal,
	GlossedTotal decimal,
	BalanceReconcile decimal,
	ValuePayments decimal,
	TotalAcceptIPS decimal,
	TotalAcceptEAPB decimal,
	state varchar(20),
	StateERPIntegration varchar(20),
	TimeResponse  varchar(50)
	)
AS

begin 

	
	declare @sql as nVarchar(max)
	--set @sql= N'exec [Glosas].[SP_InvoiceTraceability] @ContainerDG,@SecurityContainer,@InvoiceNumber'
	--insert into @rtnTable
	--execute sp_executesql @sql,N'@ContainerDG varchar(20),@SecurityContainer varchar(20),@InvoiceNumber varchar(20)',@ContainerDG,@SecurityContainer,@InvoiceNumber
	--Return
--	exec [Glosas].[SP_InvoiceTraceability] @ContainerDG,@SecurityContainer,@InvoiceNumber
	set @sql = N'
	select  car.InvoiceNumber as InvoiceNumber,
	cust.NIT + '' - '' + cust.Name as Customer,
	sal.InvoiceDate as InvoiceDate,
	userC.UserCode +'' - ''+ Person.Fullname  as BillerName,
	contra.code as ContractCode,
	sal.ThirdPartySalesValue as InvoiceValueEntity,
	sal.PatientPaidValue as InvoiceValuePacient,
	isnull(C.RadicatedConsecutive,0) as ObjectionCode,
	isnull(C.DocumentDate,''01/01/1900'') as ObjectionDate,
	isnull(P.ValueGlosado,0) as ObjectionValue,
	isnull(P.ValueAcceptedFirstInstance,0) as ValueAcceptedFirstInstance,
	isnull(CR.RadicatedConsecutive,0) as ReiterationCode,
	isnull(CR.DocumentDate,''01/01/1900'') as ReiterationDate,
	isnull(P.ValueReiterated,0) as ValueReiterated,
	isnull(P.ValueAcceptedSecondInstance,0) as ValueAcceptedSecondInstance,
	car.value as InvoiceTotal,
	car.Balance as PortfolioCurrentBalance,
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
	NULL as StateERPIntegration,
	'' as TimeResponse 
	from portfolio.AccountReceivable car INNER JOIN
	Billing.invoice sal on  sal.Id = car.Invoiceid INNER JOIN
	[Contract].CareGroup careGr on careGr.Id =  sal.CareGroupId INNER JOIN
	[Contract].[Contract] contra on Contra.Id = careGr.ContractId INNER JOIN
	'+@SecurityContainer+'.[Security].[User] userC on userC.usercode = sal.InvoicedUser LEFT JOIN
	'+@SecurityContainer+'.[Security].[Person] Person on Person.Id = userC.IdPerson  LEFT JOIN
	Glosas.GlosaObjectionsReceptionD D on D.InvoiceNumber =car.InvoiceNumber  LEFT join
	Glosas.GlosaObjectionsReceptionC  C  on c.id = d.GlosaObjectionsReceptionCId and D.DocumentType = ''1''  Left join
	Common.customer cust on cust.id = C.CustomerId Left join
	Glosas.GlosaObjectionsReceptionD DR on DR.InvoiceNumber =car.InvoiceNumber and DR.DocumentType = ''2'' Left join
	Glosas.GlosaObjectionsReceptionC  CR on  CR.id = DR.GlosaObjectionsReceptionCId Left join
	Glosas.GlosaPortfolioGlosada P on P.Id =  D.PortfolioGlosaId 
	where car.InvoiceNumber  = '''+ @InvoiceNumber +''' '
	exec sp_executesql @sql
	--print @sql
	Return
end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de trazabilidad de facturas en el módulo de Glosas: dado un número de factura, retorna el ciclo completo de vida de esa factura desde su emisión hasta la conciliación final, incluyendo datos del cliente (aseguradora/EPS), facturador, contrato, valores facturados a la entidad y al paciente, glosa inicial (código, fecha y valor glosado), primera instancia de respuesta, reiteración de glosa, segunda instancia, conciliaciones IPS y EAPB, pagos recibidos, saldo en cartera y estado actual del proceso de glosa (pendiente, conciliada, cobro jurídico, entre otros). Opera sobre las entidades de cartera (portfolio.AccountReceivable), facturación (Billing.invoice), contratos (Contract.Contract), recepción de glosas (Glosas.GlosaObjectionsReceptionC/D) y la glosa de cartera (Glosas.GlosaPortfolioGlosada), consultando además usuarios y personas en el contenedor de seguridad indicado mediante SQL dinámico con sp_executesql. Se usa para rastrear el estado financiero y de glosa de una factura específica en el flujo de cartera y conciliación con aseguradoras.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'FUNCTION', @level1name = N'Fn_InvoiceTraceability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'FUNCTION', @level1name = N'Fn_InvoiceTraceability';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la trazabilidad financiera y de estado de una factura, integrando cartera, facturación, contrato, usuario facturador y el ciclo de glosas/objeciones/reiteraciones/conciliaciones.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'Fn_InvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la factura en portfolio.AccountReceivable referenciada por número de factura.; El contenedor de seguridad indicado debe contener los esquemas Security.User y Security.Person para resolver el facturador.; La factura debe estar enlazada a una invoice de Billing, un CareGroup y un Contract.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'Fn_InvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'TotalAcceptIPS = ValueAcceptedIPSconciliation + ValueAcceptedFirstInstance + ValueAcceptedSecondInstance.; TotalAcceptEAPB = ValueAcceptedEAPBconciliation + ValueReiterationBalance.; Si no existen registros de glosa, los valores monetarios glosados retornan 0 y las fechas por defecto 01/01/1900.; El estado ERP de integración siempre se devuelve nulo y el tiempo de respuesta vacío.; El campo Customer corresponde al cliente asociado al documento de objeción inicial (DocumentType=''1''), no necesariamente al pagador del contrato.; BillerName se compone de UserCode + '' - '' + Fullname del usuario que facturó.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'Fn_InvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Cartera; Glosa; Objeción; Reiteración; Conciliación (IPS/EAPB); Pago parcial; Cobro jurídico; Contrato; Grupo de atención (CareGroup); Cliente/Pagador; Usuario facturador; Saldo de cartera; Valor pagado por paciente vs tercero', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'Fn_InvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Ejecuta dinámicamente el SELECT vía sp_executesql y retorna el conjunto de resultados directamente al cliente (no inserta en la tabla de retorno declarada).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'Fn_InvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si GlosaObjectionsReceptionD.DocumentType = ''1'' → Se vincula como documento de Objeción inicial (ObjectionCode/ObjectionDate y CustomerId).; si GlosaObjectionsReceptionD.DocumentType = ''2'' → Se vincula como documento de Reiteración (ReiterationCode/ReiterationDate).; si GlosaPortfolioGlosada.state = 1..15 → Traduce el estado numérico a etiqueta de negocio (Pendiente Confirmar Glosa, Pendiente Evaluacion Glosa, Pendiente envio de oficio, Pendiente confirmar reiteracion, Pendiente evaluacion reiteracion, Pendiente conciliacion, Pendiente de confirmar Conciliacion, Conciliada, Conciliada Parcialmente, Glosa con Respuesta, reiteracion con respuesta, pendiente confirmar pago parcial, confirmado pago parcial, cobro juridico). else Se reporta como ''No esta Glosada''.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'Fn_InvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'portfolio.AccountReceivable; Billing.invoice; Contract.CareGroup; Contract.Contract; Security.User; Security.Person; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; Common.customer; Glosas.GlosaPortfolioGlosada', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'Fn_InvoiceTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'Fn_InvoiceTraceability';
GO
