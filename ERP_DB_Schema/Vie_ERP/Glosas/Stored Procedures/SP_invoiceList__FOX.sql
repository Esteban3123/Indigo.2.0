-- =============================================
-- Author:		Rafael Patiño
-- Create date: 10/04/2013
-- Description:	Sp que retorna las facturas por contenedor
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceList__FOX]
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
	
 create table #tablaFactura(
	--Declare @tablaFactura table(
		InvoiceNumber  varchar(50),
		InvoiceDate  date,
		RadicatedNumber varchar(50),
		RadicatedDate date,
		PatientCode varchar(20),
		PatientName varchar(200),
		PatientNameInicial varchar(200),
		PatientLastName varchar(200),
		IngressNumber varchar(15),
		IngressDate datetime,
		UserNameInvoice varchar(200),
		Comment varchar(250),
		ContractCode varchar(10),
		ContractName varchar(200),
		State char(1),
		BalanceInvoice  decimal(18,2),
		Selection bit,
		InvoiceValueEntity money,
		InvoiceValuePacient money,
		AccountantAccountCustomers varchar(30),
		PortfolioAge int,
		ValueGlosado money,
		StateCurrentInvoice varchar(1),
		CodePlan varchar(4),
		ContractEntity varchar(20),
		RadicatedConsecutive int,
		StateObjectionReceptionC int,
		Reiterated int,
		StatePortfolioGlosada int,
		CreditNoteValue money,
		DebitNoteValue money,
		Devolution  int,
		ConceptDevolution varchar(100)		 
	)

	
	declare @sql as nVarchar(max)
	
		IF @TopQuery = '' BEGIN
			set @TopQuery = '50'
		END

		--facturas no radicadas
		if @FlagNotConfirmInvoice = 1 begin



		set @sql = 'USE ' + @container  + '           
				SELECT   top ' + @TopQuery + '
				car.cemnumfac as InvoiceNumber,
			car.cemfecfac as InvoiceDate,
			NULL as RadicatedNumber,
			NULL as RadicatedDate,
			com.gpacodigo as PatientCode,
			RTRIM(com.gpanombre) + '' ''+ LTRIM(com.gpaapelli) as PatientName,
			RTRIM(com.gpanombre) as PatientNameInicial,
			LTRIM(com.gpaapelli) as PatientLastName,
			sal.ainconsec as IngressNumber,
			ing.AINFECING as IngressDate,
			usu.USUNOMBRE as UserNameInvoice,
			'''' as Comment,
			sal.GECCODIGO as ContractCode,
			con.GECNOMENT as ContractName,
			''1'' as State,
			car.cemsalfac as BalanceInvoice,
			convert(bit,0) as Selection,
			sal.SFATOTFAC as InvoiceValueEntity,
			sal. SFAVALPAC as InvoiceValuePacient,
			car.CPCCODCUE as AccountantAccountCustomers,
			NULL as PortfolioAge,
			NULL as ValueGlosado,
			car.cemestado as StateCurrentInvoice,
			sal.placodigo as CodePlan,
			con.GECCODIGO as ContractEntity,
			NULL AS RadicatedConsecutive,
			NULL as StateObjectionReceptionC,
			NULL as Reiterated,
			NULL as StatePortfolioGlosada,
			car.CEMCREDIT as CreditNoteValue,
			car.CEMDEBITO as DebitNoteValue,
			CASE WHEN C.RadicatedConsecutive is NULL THEN 0 ELSE 1 END  as Devolution,
			Concept.code + '' - '' +  Concept.NameSpecific as ConceptDevolution
	 FROM ..crcarter car 
			INNER JOIN ..slfactur sal ON car.cemnumfac = sal.sfanumfac LEFT OUTER JOIN ..gepacien com ON sal.gpacodigo = com.gpacodigo
			INNER JOIN ..gecontra con on con.geccodigo=sal.geccodigo INNER JOIN ..geusuari usu on usu.USUCODIGO=sal.USUCODIGO
			LEFT OUTER JOIN ..adingres ing ON sal.ainconsec = ing.ainconsec LEFT OUTER JOIN ..adcenate cen on cen.acacodigo = ing.acacodigo
			LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaDevolutionsReceptionD D on car.CEMNUMFAC = D.InvoiceNumber 
			LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaDevolutionsReceptionC c on c.id = d.GlosaDevolutionsReceptionCId 
			LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.[GlosaMovementDevolutions] m on m.IdDevolutionsreceptionD = D.Id 
			LEFT OUTER JOIN ' + @IndigoCompany + '.Common.ConceptGlosas Concept on Concept.id = m.IdConceptGlosa 
	WHERE car.tercodter =  right(''000000000000000'' +  Ltrim(Rtrim(@nit)),15)  AND car.cemestado = ''1''	
	AND car.cemnumfac NOT IN (select InvoiceNumber from ' + @IndigoCompany + '.Portfolio.RadicateInvoiceD where state IN (1,2) ) '

	end else if @FlagNotConfirmInvoice = 0 begin

				set @sql = 'USE ' + @container  + '           
					SELECT  top ' + @TopQuery + '
					car.cemnumfac as InvoiceNumber,
					car.cemfecfac as InvoiceDate,
					crc.ccrnumrad as RadicatedNumber,
					crc.ccrfecrad as RadicatedDate,
					com.gpacodigo as PatientCode,
					RTRIM(com.gpanombre) + '' ''+ LTRIM(com.gpaapelli) as PatientName,
					RTRIM(com.gpanombre) as PatientNameInicial,
					LTRIM(com.gpaapelli) as PatientLastName,
					sal.ainconsec as IngressNumber,
					ing.AINFECING as IngressDate,
					usu.USUNOMBRE as UserNameInvoice,
					'''' as Comment,
					sal.GECCODIGO as ContractCode,
					con.GECNOMENT as ContractName,
					''1'' as State,
					car.cemsalfac as BalanceInvoice,
					convert(bit,0) as Selection,
					sal.SFATOTFAC as InvoiceValueEntity,
					sal. SFAVALPAC as InvoiceValuePacient,
					car.CPCCODCUE as AccountantAccountCustomers,
					cast(datediff(dd,crc.ccrfecrad,[Common].[GETDATE]()) as int) as PortfolioAge,
					glo.ValueGlosado as ValueGlosado,
					car.cemestado as StateCurrentInvoice,
					sal.placodigo as CodePlan,
					con.GECCODIGO as ContractEntity,
					C.RadicatedConsecutive AS RadicatedConsecutive,
					convert(int,C.state)  as StateObjectionReceptionC,
					(select count(Id) from ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionD  where InvoiceNumber = car.cemnumfac AND state <> 4 ) as Reiterated,
					convert(int,glo.State)  as StatePortfolioGlosada,
					NUll as CreditNoteValue,
					NULL as DebitNoteValue,
					0 as Devolution,
					NULL as ConceptDevolution
			 FROM ..crcarter car 
					INNER JOIN ..slfactur sal ON car.cemnumfac = sal.sfanumfac INNER JOIN ..gepacien com ON sal.gpacodigo = com.gpacodigo
					INNER JOIN ..gecontra con on con.geccodigo=sal.geccodigo INNER JOIN ..geusuari usu on usu.USUCODIGO=sal.USUCODIGO
					INNER JOIN ..adingres ing ON sal.ainconsec = ing.ainconsec INNER JOIN ..adcenate cen on cen.acacodigo = ing.acacodigo
					INNER JOIN ..crmracts crm ON car.cemnumfac = crm.cmrnumfac INNER JOIN ..crcracts crc ON crm.ccrnumrad = crc.ccrnumrad 
					LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaPortfolioGlosada glo on glo.InvoiceNumber=car.cemnumfac  
					LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionD D on D.InvoiceNumber=glo.InvoiceNumber AND D.documenttype = 1 --AND D.state <> 4
					LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionC C on C.Id=D.GlosaObjectionsReceptionCId
			WHERE   car.cemnumfac not in (select InvoiceNumber from ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionD D inner join ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionC C on C.id = D.GlosaObjectionsReceptionCId where D.documenttype = 1  AND C.state = 1   ) AND  car.tercodter =  right(''000000000000000'' +  Ltrim(Rtrim(@nit)),15) and car.cemsalfac > 0 ' 
	
	end else if @FlagNotConfirmInvoice = 2 begin 

		set @sql = 'USE ' + @container  + '          
				SELECT  top ' + @TopQuery + '
					car.cemnumfac as InvoiceNumber,
					car.cemfecfac as InvoiceDate,
					crc.ccrnumrad as RadicatedNumber,
					crc.ccrfecrad as RadicatedDate,
					com.gpacodigo as PatientCode,
					RTRIM(com.gpanombre) + '' ''+ LTRIM(com.gpaapelli) as PatientName,
					RTRIM(com.gpanombre) as PatientNameInicial,
					LTRIM(com.gpaapelli) as PatientLastName,
					sal.ainconsec as IngressNumber,
					ing.AINFECING as IngressDate,
					usu.USUNOMBRE as UserNameInvoice,
					'''' as Comment,
					sal.GECCODIGO as ContractCode,
					con.GECNOMENT as ContractName,
					''1'' as State,
					car.cemsalfac as BalanceInvoice,
					convert(bit,0) as Selection,
					sal.SFATOTFAC as InvoiceValueEntity,
					sal. SFAVALPAC as InvoiceValuePacient,
					car.CPCCODCUE as AccountantAccountCustomers,
					cast(datediff(dd,crc.ccrfecrad,[Common].[GETDATE]()) as int) as PortfolioAge,
					null as ValueGlosado,
					car.cemestado as StateCurrentInvoice,
					sal.placodigo as CodePlan,
					con.GECCODIGO as ContractEntity,
					C.RadicatedConsecutive AS RadicatedConsecutive,
					convert(int,C.state)  as StateObjectionReceptionC,
					null as Reiterated,
					null  as StatePortfolioGlosada,
					NUll as CreditNoteValue,
					NULL as DebitNoteValue,
					0 as Devolution,
					NULL as ConceptDevolution
			 FROM ..crcarter car 
					INNER JOIN ..slfactur sal ON car.cemnumfac = sal.sfanumfac INNER JOIN ..gepacien com ON sal.gpacodigo = com.gpacodigo
					INNER JOIN ..gecontra con on con.geccodigo=sal.geccodigo INNER JOIN ..geusuari usu on usu.USUCODIGO=sal.USUCODIGO
					INNER JOIN ..adingres ing ON sal.ainconsec = ing.ainconsec INNER JOIN ..adcenate cen on cen.acacodigo = ing.acacodigo
					INNER JOIN ..crmracts crm ON car.cemnumfac = crm.cmrnumfac INNER JOIN ..crcracts crc ON crm.ccrnumrad = crc.ccrnumrad 
					LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaDevolutionsReceptionD D on car.CEMNUMFAC = D.InvoiceNumber 
					LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaDevolutionsReceptionC c on c.id = d.GlosaDevolutionsReceptionCId 
			WHERE   car.cemnumfac not in (select InvoiceNumber from ' + @IndigoCompany + '.Glosas.GlosaPortfolioGlosada   --no incluir las que estan en un proceso de glosa
			union all
			select InvoiceNumber from ' + @IndigoCompany + '.Glosas.GlosaDevolutionsReceptionD  where State = 1 --no incluir las que estan ya en un ofico de devolucion sin tramitar
		 ) AND  car.tercodter =  right(''000000000000000'' +  Ltrim(Rtrim(@nit)),15)  AND car.cemestado = ''2'' '

	end

	IF @InvoiceNumber <> '' BEGIN
	 set @sql = @sql + ' AND car.cemnumfac  = ''' + @InvoiceNumber + ''' '
	
	END
	
	IF @StringSQl <> '' BEGIN
		set @sql = @sql + ' AND ' +  @StringSQl + ' ORDER BY car.cemnumfac'
	END

	print @sql
	--INSERT INTO #tablaFactura
	execute sp_executesql @sql,N'@nit varchar(15)',@nit

	/*set @sql = ''

	if @TopQuery = 'Todos' begin
		set @sql = 'SELECT  '
	end else begin
		set @sql = 'SELECT  top ' + @TopQuery + '  '
	end

	set @sql = ' ' + @sql + ' 
						InvoiceNumber, 
						InvoiceDate,
						RadicatedNumber,
						RadicatedDate,
						PatientCode,
						PatientName,
						PatientNameInicial,
						PatientLastName,
						IngressNumber,
						UserNameInvoice,
						Comment,
						ContractCode,
						ContractName,
						State,
						BalanceInvoice,
						Selection,
						InvoiceValueEntity,
						InvoiceValuePacient,
						AccountantAccountCustomers,
						PortfolioAge,
						ValueGlosado,
						StateCurrentInvoice,
						CodePlan,
						ContractEntity,
						RadicatedConsecutive,
						StateObjectionReceptionC,
						Reiterated,
						StatePortfolioGlosada,
						CreditNoteValue,
						DebitNoteValue 	
				FROM #tablaFactura WHERE 1=1  '
	
	
	IF @InvoiceNumber <> '' BEGIN
	 set @sql = @sql + ' AND InvoiceNumber  = ''' + @InvoiceNumber + ''' '
	
	END
	
	IF @StringSQl <> '' BEGIN
		set @sql = @sql + ' AND ' +  @StringSQl + ' ORDER BY InvoiceNumber'
	END

	print @sql
	execute sp_executesql @sql
	*/

	--select * from @tablaFactura 

	
END