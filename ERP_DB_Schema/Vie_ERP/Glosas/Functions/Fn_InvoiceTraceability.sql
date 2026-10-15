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