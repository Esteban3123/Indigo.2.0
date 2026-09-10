
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta y retorna el listado de facturas asociadas a un contenedor (sede o empresa) y a un tercero (NIT de entidad pagadora), utilizado en el módulo de Glosas y Cartera para la gestión de cobros y radicación de facturas ante aseguradoras o EPS. Combina información de cuentas por cobrar, facturación, datos del paciente (nombre, código, ingreso hospitalario) y contrato de salud, permitiendo filtrar facturas ya radicadas o pendientes de radicar según el parámetro @FlagNotConfirmInvoice. Soporta paginación mediante @TopQuery y construcción dinámica del SQL para operar en múltiples empresas Indigo (@IndigoCompany) y contenedores HIS (@HISContainer), siendo la fuente principal de datos para la pantalla de selección y gestión de facturas en el proceso de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceList';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceList';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye y ejecuta dinámicamente una consulta que lista facturas de un tercero (NIT) según su estado en el ciclo de radicación/glosa: no radicadas, radicadas con glosa, o en devolución de radicado, devolviendo datos clínicos, contractuales y de cartera.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de bandera de modo debe valer 0, 1 o 2 para que se arme alguna consulta SQL; en otro caso no se asigna @sql.; La compañía Indigo y el contenedor HIS recibidos deben corresponder a bases de datos accesibles, ya que se usan en ''USE @IndigoCompany'' y en JOINs con @HISContainer..INPACIENT/ADINGRESO.; Debe existir el NIT del cliente en Common.Customer/ThirdParty para retornar resultados.; Si se envía filtro adicional (@StringSQl), debe ser una cláusula SQL válida para concatenar tras un AND.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se listan facturas con saldo positivo (car.balance > 0).; Sólo se consideran cuentas por cobrar con Status = 2.; La consulta siempre se ejecuta sobre la base de la compañía Indigo indicada (USE @IndigoCompany) y usa el contenedor HIS dinámico para datos de paciente e ingreso.; En modo ''no radicadas'' se excluyen facturas que ya están radicadas (RadicateInvoiceD state 1 o 2) y se exigen cuotas únicas (NumberShares=1).; En modo ''radicadas'' se excluyen facturas con objeciones de glosa activas (documenttype=1, state=1).; En modo ''devolución de radicado'' se garantiza que el valor radicado coincide con el saldo (ara.Value = ara.Balance) y que la factura no está en proceso de glosa ni en otro oficio de devolución pendiente.; El filtro por NIT del cliente siempre se aplica vía parámetro @nit pasado a sp_executesql (parametrizado, no concatenado).; El TOP se acota por @TopQuery (default 50) en todos los modos.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Radicación de factura; Glosa; Objeción de glosa; Devolución de radicado; Nota crédito; Nota débito; Cartera / edad de cartera; Paciente; Ingreso hospitalario; Contrato con administradora de salud; Plan de atención (CareGroup); Saldo de cuenta por cobrar; Concepto de devolución; Copago / valor a cargo del paciente; Moneda / divisa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #tablaFactura: Inserta el resultado del sp_executesql dinámico (lista de facturas del modo seleccionado) en la tabla temporal.; [RETURN_RESULT] #tablaFactura: Devuelve al cliente todas las filas insertadas en la tabla temporal mediante SELECT * final.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TopQuery = '''' → Se asigna 50 como límite TOP por defecto para la consulta.; si @FlagNotConfirmInvoice = 1 (facturas no radicadas) → Arma consulta sobre AccountReceivable con balance>0, PortfolioStatus=''1'', Status=2, AccountReceivableType=2, NumberShares=1 y excluye facturas que ya están en Portfolio.RadicateInvoiceD con state IN (1,2); incluye notas crédito/débito desde AccountReceivableShare y marca devolución si existe en GlosaDevolutionsReceptionC.; si @FlagNotConfirmInvoice = 0 (facturas radicadas con posible glosa) → Arma consulta sobre AccountReceivable con balance>0, Status=2 y PortfolioStatus IN (3,15,16); excluye facturas con objeción activa (GlosaObjectionsReceptionC.state=1, documenttype=1) y trae datos de radicación, edad de cartera y valor glosado.; si @FlagNotConfirmInvoice = 2 (facturas en devolución de radicado) → Arma consulta sobre AccountReceivable con AccountReceivableType IN (1,2), NumberShares=1, Status=2, balance>0, PortfolioStatus=3, ara.Value=ara.Balance, sin registro en GlosaPortfolioGlosada y sin GlosaDevolutionsReceptionD con State=1.; si @InvoiceNumber <> '''' → Se concatena al SQL un filtro adicional por número de factura exacto.; si @StringSQl <> '''' → Se concatena el filtro libre al WHERE y se agrega ORDER BY car.invoicenumber.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Portfolio.AccountReceivableShare; Portfolio.AccountReceivableAccounting; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; Billing.Invoice; Billing.InvoiceCategories; GeneralLedger.CompanySettings; GeneralLedger.MainAccounts; Common.Currency; Common.ThirdParty; Common.Customer; Common.ConceptGlosas; Contract.HealthAdministrator; Contract.CareGroup; Contract.Contract; Glosas.GlosaDevolutionsReceptionD; Glosas.GlosaDevolutionsReceptionC; Glosas.GlosaMovementDevolutions; Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; INPACIENT; ADINGRESO', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList';
-- GO
