
-- =============================================
-- Author:		Rafael Patiño
-- Create date: 10/04/2013
-- Description:	Sp que retorna las facturas por contenedor
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceList]
	@container varchar(150),
	@nit varchar(15),
	@InvoiceNumber varchar(50),
	@IndigoCompany varchar(10),
	@HISContainer varchar(10),
	@StringSQl varchar(1000),
	@TopQuery varchar(5),
	@FlagNotConfirmInvoice varchar(20)
AS
BEGIN

	SET NOCOUNT ON;
	
	create table #tablaFactura
	(
		InvoiceNumber  varchar(50),
		InvoiceCategory varchar(200),
		InvoiceDate  datetime,
		RadicatedNumber varchar(50),
		RadicatedDate date,
		PatientCode varchar(20),
		PatientName varchar(300),
		PatientNameInicial varchar(200),
		PatientLastName varchar(200),
		IngressNumber varchar(15),
		IngressDate datetime,
		UserNameInvoice varchar(200),
		Comment varchar(250),
		ContractCode varchar(100),
		ContractName varchar(200),
		State char(1),
		BalanceInvoice  decimal(18,2),
		Selection bit,
		InvoiceValueEntity money,
		InvoiceValuePacient money,
		AccountantAccountCustomers varchar(30),
		PortfolioAge int,
		ValueGlosado money,
		StateCurrentInvoice varchar(2),
		CodePlan varchar(200),
		ContractEntity varchar(200),
		RadicatedConsecutive int,
		StateObjectionReceptionC int,
		Reiterated int,
		TraslateJuridical int,
		StatePortfolioGlosada int,
		CreditNoteValue money,
		DebitNoteValue money,
		Devolution  int,
		ConceptDevolution varchar(520),
		OpeningBalance bit,
		DocumentType tinyint,
		CurrencyAbbreviation varchar(5)
	)

	declare @sql as nVarchar(max)
	
	IF @TopQuery = '' BEGIN
		set @TopQuery = '50'
	END

	--facturas no radicadas
	if @FlagNotConfirmInvoice = 1 begin
		set @sql = 'USE ' + @IndigoCompany  + '           
		SELECT   top ' + @TopQuery + '
			car.InvoiceNumber as InvoiceNumber,
			case when invc.Id is null then '''' else invc.Code + '' - '' + invc.Name end as InvoiceCategory,
			isnull(sal.InvoiceDate,car.accountreceivabledate) as InvoiceDate,
			null as RadicatedNumber,
			null as RadicatedDate,
			isnull(sal.PatientCode,'''') as PatientCode,
			RTRIM(LTRIM(pac.IPPRINOMB)) + '' '' + RTRIM(LTRIM(pac.IPPRIAPEL)) as PatientName,
			RTRIM(LTRIM(pac.IPPRINOMB)) as PatientNameInicial,
			RTRIM(LTRIM(pac.IPPRIAPEL)) as PatientLastName,
			isnull(sal.AdmissionNumber,'''') as IngressNumber,
			Ing.IFECHAING as IngressDate,
			isnull(sal.InvoicedUser,'''') as UserNameInvoice,
			'''' as Comment,
			isnull(contra.code,'''') as ContractCode,
			isnull(contra.ContractName,'''') as ContractName,
			''1'' as State,
			car.balance as BalanceInvoice,
			convert(bit,0) as Selection,
			isnull(sal.ThirdPartySalesValue,car.balance) as InvoiceValueEntity,
			isnull(sal.TotalPatientSalesPrice,0) as InvoiceValuePacient,
			ac.number as AccountantAccountCustomers,
			null as PortfolioAge,
			null as ValueGlosado,
			convert(varchar(2),car.PortfolioStatus) as StateCurrentInvoice,
			isnull(careGr.code,'''') as CodePlan,
			isnull(careGr.code,'''') as ContractEntity,
			null AS RadicatedConsecutive,
			null  as StateObjectionReceptionC,
			null as Reiterated,
			NULL as TraslateJuridical,
			null  as StatePortfolioGlosada,
			ars.CreditValue as CreditNoteValue,
			ars.DebitValue as DebitNoteValue,
			CASE WHEN C.RadicatedConsecutive is NULL THEN 0 ELSE 1 END  as Devolution,
			Concept.code + '' - '' +  Concept.NameSpecific as ConceptDevolution,
			car.OpeningBalance,
			sal.DocumentType as DocumentType,
			cu.Abbreviation as CurrencyAbbreviation
		FROM portfolio.AccountReceivable car 
		INNER JOIN Portfolio.AccountReceivableShare ars on ars.AccountReceivableId = car.Id 
		INNER JOIN Billing.InvoiceCategories invc on car.InvoiceCategoryId = invc.Id 
		INNER JOIN GeneralLedger.CompanySettings cs on 1=1
		INNER JOIN Common.Currency cu on  cu.Id = ISNULL(car.CurrencyId,cs.OfficialCurrencyId)
		LEFT JOIN Billing.invoice sal on  sal.Id = car.Invoiceid 
		LEFT JOIN ' + @HISContainer + '..INPACIENT pac on pac.IPCODPACI =  sal.PatientCode 
		LEFT JOIN ' + @HISContainer + '..ADINGRESO Ing on Ing.NUMINGRES =  sal.AdmissionNumber 
		LEFT JOIN Contract.HealthAdministrator ha ON sal.HealthAdministratorId = ha.Id
		LEFT JOIN [Contract].CareGroup careGr on careGr.Id =  sal.CareGroupId 
		LEFT JOIN [Contract].[Contract] contra on Contra.Id = careGr.ContractId 
		LEFT JOIN [GeneralLedger].[MainAccounts] Ac on Ac.id = car.AccountWithoutRadicateId 
		LEFT JOIN Common.ThirdParty t on t.Id = car.ThirdPartyId 
		LEFT join Common.Customer cust on cust.ThirdPartyId = t.id 
		LEFT JOIN Glosas.GlosaDevolutionsReceptionD D on car.InvoiceNumber = D.InvoiceNumber
		LEFT JOIN Glosas.GlosaDevolutionsReceptionC c on c.id = d.GlosaDevolutionsReceptionCId
		LEFT JOIN Glosas.[GlosaMovementDevolutions] m on m.IdDevolutionsreceptionD = D.Id 
		LEFT JOIN Common.ConceptGlosas Concept on Concept.id = m.IdConceptGlosa 
		WHERE cust.nit =@nit and car.balance > 0  AND car.PortfolioStatus = ''1'' AND car.Status=2 AND car.AccountReceivableType =2 AND car.InvoiceNumber NOT IN (select InvoiceNumber from Portfolio.RadicateInvoiceD where state IN (1,2) ) and car.NumberShares = 1'
	end 
	else if @FlagNotConfirmInvoice = 0 
	begin
		set @sql = 'USE ' + @IndigoCompany  + '           
		SELECT  top ' + @TopQuery + '
			car.InvoiceNumber as InvoiceNumber,
			case when invc.Id is null then '''' else invc.Code +'' - ''+ invc.Name end as InvoiceCategory,
			car.AccountReceivableDate  as InvoiceDate,
			convert(varchar(50),RC.RadicatedConsecutive) as RadicatedNumber,
			RC.RadicatedDate as RadicatedDate,
			sal.PatientCode as PatientCode,
			RTRIM(LTRIM(sal.PatientCode)) + '' ''+ RTRIM(LTRIM(pac.IPNOMCOMP)) as PatientName,
			RTRIM(LTRIM(pac.IPPRINOMB)) as PatientNameInicial,
			RTRIM(LTRIM(pac.IPPRIAPEL)) as PatientLastName,
			sal.AdmissionNumber as IngressNumber,
			Ing.IFECHAING as IngressDate,
			sal.InvoicedUser as UserNameInvoice,
			'''' as Comment,
			contra.code as ContractCode,
			contra.ContractName as ContractName,
			''1'' as State,
			car.balance as BalanceInvoice,
			convert(bit,0) as Selection,
			isnull(sal.ThirdPartySalesValue,car.Value)  as InvoiceValueEntity,
			isnull(sal.TotalPatientSalesPrice,0) as InvoiceValuePacient,
			--car.CPCCODCUE as AccountantAccountCustomers,
			'''' as AccountantAccountCustomers,
			cast(datediff(dd,RC.RadicatedDate,[Common].[GETDATE]()) as int) as PortfolioAge,
			glo.ValueGlosado as ValueGlosado,
			convert(varchar(2),car.PortfolioStatus) as StateCurrentInvoice,
			careGr.code as CodePlan,
			contra.code as ContractEntity,
			C.RadicatedConsecutive AS RadicatedConsecutive,
			convert(int,C.state)  as StateObjectionReceptionC,
			(select count(Id) from Glosas.GlosaObjectionsReceptionD  where InvoiceNumber = car.invoicenumber AND state <> 4 ) as Reiterated,
			NULL as TraslateJuridical,
			convert(int,glo.State)  as StatePortfolioGlosada,
			NUll as CreditNoteValue,
			NULL as DebitNoteValue,
			0 as Devolution,
			NULL as ConceptDevolution,
			car.OpeningBalance,
			sal.DocumentType as DocumentType,
			cu.Abbreviation as CurrencyAbbreviation
		FROM	portfolio.AccountReceivable car 
		LEFT JOIN Billing.invoice sal on  sal.Id = car.Invoiceid 
		INNER JOIN Billing.InvoiceCategories invc on car.InvoiceCategoryId = invc.Id 
		INNER JOIN GeneralLedger.CompanySettings cs on 1=1
		INNER JOIN Common.Currency cu on  cu.Id = ISNULL(car.CurrencyId,cs.OfficialCurrencyId)
		LEFT JOIN ' + @HISContainer + '..INPACIENT pac on pac.IPCODPACI =  sal.PatientCode  
		LEFT JOIN ' + @HISContainer + '..ADINGRESO Ing on Ing.NUMINGRES =  sal.AdmissionNumber   
		LEFT JOIN [Contract].CareGroup careGr on careGr.Id =  sal.CareGroupId 
		LEFT JOIN [Contract].[Contract] contra on Contra.Id = careGr.ContractId 
		LEFT JOIN Common.ThirdParty t on t.Id = car.ThirdPartyId 
		inner join Common.Customer cust on cust.ThirdPartyId = t.id 
		left outer join [Portfolio].[RadicateInvoiceD] RD on RD.invoicenumber = car.InvoiceNumber AND RD.state = 2 
		LEFT OUTER JOIN [Portfolio].[RadicateInvoiceC] RC on RC.id =  RD.RadicateInvoiceCId 
		LEFT OUTER JOIN Glosas.GlosaPortfolioGlosada glo on glo.InvoiceNumber=car.Invoicenumber
		LEFT OUTER JOIN Glosas.GlosaObjectionsReceptionD D on D.InvoiceNumber=glo.InvoiceNumber AND D.documenttype = 1 AND D.State <> ''4''
		LEFT OUTER JOIN Glosas.GlosaObjectionsReceptionC C on C.Id=D.GlosaObjectionsReceptionCId AND C.State <> ''4''
		WHERE   car.invoicenumber not in (select InvoiceNumber from Glosas.GlosaObjectionsReceptionD D inner join Glosas.GlosaObjectionsReceptionC C on C.id = D.GlosaObjectionsReceptionCId where D.documenttype = 1  AND C.state = 1   ) AND  cust.nit =@nit and car.balance > 0 AND car.PortfolioStatus IN (3, 15, 16) AND car.Status = 2' 	
	end 
	--Lista las facturas en devolución de radicados
	else if @FlagNotConfirmInvoice = 2 
	begin 
		set @sql = 'USE ' + @IndigoCompany  + '          
		SELECT  top ' + @TopQuery + '
			car.InvoiceNumber as InvoiceNumber,
			case when invc.Id is null then '''' else invc.Code + '' - '' +  invc.Name end as InvoiceCategory,
			car.AccountReceivableDate  as InvoiceDate,
			convert(varchar(50),RC.RadicatedConsecutive) as RadicatedNumber,
			RC.RadicatedDate as RadicatedDate,
			RD.PatientCode as PatientCode,
			RD.PatientName as PatientName,
			RTRIM(LTRIM(pac.IPPRINOMB)) as PatientNameInicial,
			RTRIM(LTRIM(pac.IPPRIAPEL)) as PatientLastName,
			RD.IngressNumber as IngressNumber,
			Ing.IFECHAING as IngressDate,
			isnull(RD.UserNameInvoice,'''') as UserNameInvoice,
			'''' as Comment,
			RD.ContractCode as ContractCode,
			RD.ContractCode as ContractName,
			''1'' as State,
			car.balance as BalanceInvoice,
			convert(bit,0) as Selection,
			isnull(sal.ThirdPartySalesValue,car.Value)  as InvoiceValueEntity,
			isnull(sal.TotalPatientSalesPrice,0) as InvoiceValuePacient,
			--car.CPCCODCUE as AccountantAccountCustomers,
			'''' as AccountantAccountCustomers,
			cast(datediff(dd,RC.RadicatedDate,[Common].[GETDATE]()) as int) as PortfolioAge,
			glo.ValueGlosado as ValueGlosado,
			convert(varchar(2),car.PortfolioStatus) as StateCurrentInvoice,
			RD.ContractCode as CodePlan,
			RD.ContractCode as ContractEntity,
			C.RadicatedConsecutive AS RadicatedConsecutive,
			convert(int,C.state)  as StateObjectionReceptionC,
			null as Reiterated,
			NULL as TraslateJuridical,
			null  as StatePortfolioGlosada,
			NUll as CreditNoteValue,
			NULL as DebitNoteValue,
			0 as Devolution,
			NULL as ConceptDevolution,
			car.OpeningBalance,
			sal.DocumentType as DocumentType,
			cu.Abbreviation as CurrencyAbbreviation
		FROM portfolio.AccountReceivable car
		JOIN Portfolio.AccountReceivableAccounting ara ON car.Id = ara.AccountReceivableId AND car.AccountRadicateId = ara.MainAccountId
		INNER JOIN GeneralLedger.CompanySettings cs on 1=1
		INNER JOIN Common.Currency cu on  cu.Id = ISNULL(car.CurrencyId,cs.OfficialCurrencyId)
		LEFT JOIN Billing.invoice sal on  sal.Id = car.Invoiceid 
		LEFT JOIN Billing.InvoiceCategories invc on car.InvoiceCategoryId = invc.Id  
		LEFT JOIN ' + @HISContainer + '..INPACIENT pac on pac.IPCODPACI =  sal.PatientCode    
		LEFT JOIN ' + @HISContainer + '..ADINGRESO Ing on Ing.NUMINGRES =  sal.AdmissionNumber    
		LEFT JOIN Common.ThirdParty t on t.Id = car.ThirdPartyId 
		LEFT JOIN Common.Customer cust on cust.ThirdPartyId = t.id 
		LEFT JOIN [Portfolio].[RadicateInvoiceD] RD on ((sal.id is not null and (RD.InvoiceNumber = sal.InvoiceNumber)) or  RD.invoicenumber =  car.InvoiceNumber) AND RD.state = 2 
		LEFT JOIN [Portfolio].[RadicateInvoiceC] RC on RC.id =  RD.RadicateInvoiceCId 		
		LEFT JOIN Glosas.GlosaDevolutionsReceptionD D on car.invoicenumber= D.InvoiceNumber 
		LEFT JOIN Glosas.GlosaDevolutionsReceptionC c on c.id = d.GlosaDevolutionsReceptionCId 
		LEFT JOIN Glosas.GlosaPortfolioGlosada glo on glo.InvoiceNumber=car.Invoicenumber
		LEFT JOIN Glosas.GlosaDevolutionsReceptionD gdrd ON car.InvoiceNumber = gdrd.InvoiceNumber AND gdrd.State = 1
		WHERE car.AccountReceivableType in (1,2) AND car.NumberShares = 1 AND car.Status = 2 AND car.balance > 0 AND car.PortfolioStatus IN (3) 
			AND ara.Value = ara.Balance --Debe estar el mismo valor con el que se radico para poder realizar la devolucion
			AND glo.Id IS NULL --no incluir las que estan en un proceso de glosa
			AND gdrd.Id IS NULL --no incluir las que estan ya en un ofico de devolucion sin tramitar
			AND cust.nit = @nit'
	end

	IF @InvoiceNumber <> '' BEGIN
	 set @sql = @sql + ' AND car.invoicenumber  = ''' + @InvoiceNumber + ''' '
	
	END
	
	IF @StringSQl <> '' BEGIN
		set @sql = @sql + ' AND ' +  @StringSQl + ' ORDER BY car.invoicenumber'
	END

	INSERT INTO #tablaFactura
		execute sp_executesql @sql,N'@nit varchar(15)',@nit    

	select * from #tablaFactura  
END