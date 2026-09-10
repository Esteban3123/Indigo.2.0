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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el listado de facturas asociadas a un contenedor (empresa o sede) y un NIT de entidad pagadora, permitiendo consultar tanto facturas no radicadas ante la aseguradora como facturas ya radicadas con su información de glosas, devoluciones y estado de cartera. Construye SQL dinámico para cruzar datos del sistema HIS (facturación, pacientes, contratos, ingresos) con las tablas del módulo de Glosas (radicaciones, objeciones, devoluciones y conceptos de glosa). Se usa en el módulo de Glosas y Cartera para listar facturas por asegurador, mostrando número de factura, fecha, datos del paciente, número de ingreso, valor facturado a entidad y paciente, saldo, estado actual, valor glosado, notas crédito/débito y si la factura tiene devolución, facilitando la gestión y seguimiento de cuentas médicas y procesos de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceList__FOX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceList__FOX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista facturas de un contenedor HIS filtradas por NIT del tercero, clasificadas según su estado de radicación, glosa u oficio de devolución, con datos de paciente, contrato, cartera y antigüedad.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El contenedor (BD HIS) recibido debe existir y contener las tablas crcarter/slfactur/gepacien/gecontra/geusuari/adingres/adcenate/crmracts/crcracts.; La base IndigoCompany debe contener los esquemas Glosas, Portfolio y Common con sus tablas referenciadas.; El NIT se normaliza a 15 caracteres rellenando con ceros a la izquierda para coincidir con tercodter.; Si TopQuery viene vacío se asume 50 registros.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las consultas filtran por tercodter igual al NIT normalizado a 15 dígitos con ceros a la izquierda.; La selección siempre se limita por TOP @TopQuery (default 50).; Las facturas en proceso de objeción abierta (documenttype=1, state=1) nunca aparecen en el flujo de radicadas.; Las facturas en proceso de glosa o con oficio de devolución sin tramitar nunca aparecen en el flujo de devoluciones (Flag=2).; Las facturas ya radicadas (RadicateInvoiceD state 1 o 2) nunca aparecen en el flujo de no radicadas (Flag=1).; La columna Selection se devuelve siempre en 0 (false).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Paciente; Ingreso; Contrato; Cartera; Radicación; Glosa; Objeción; Devolución; Nota crédito; Nota débito; Edad de cartera; Plan; NIT/Tercero; Concepto de glosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @FlagNotConfirmInvoice = 1 retorna facturas con cemestado=''1'' cuyo cemnumfac NO está en RadicateInvoiceD con state IN (1,2), es decir facturas no radicadas; incluye marca Devolution=1 si existe registro en GlosaDevolutionsReceptionC.; [RETURN_RESULT] resultset: Cuando @FlagNotConfirmInvoice = 0 retorna facturas radicadas (join con crmracts/crcracts) con cemsalfac > 0 y excluye las que tienen objeción documenttype=1 con C.state=1; incluye PortfolioAge, ValueGlosado y conteo de reiteradas (objeciones con state<>4).; [RETURN_RESULT] resultset: Cuando @FlagNotConfirmInvoice = 2 retorna facturas con cemestado=''2'' que NO están en GlosaPortfolioGlosada ni en GlosaDevolutionsReceptionD con State=1 (devoluciones sin tramitar).; [RETURN_RESULT] resultset: Si @InvoiceNumber no es vacío, se agrega filtro adicional por número de factura exacto.; [RETURN_RESULT] resultset: Si @StringSQl no es vacío, se concatena como filtro adicional dinámico y se ordena por cemnumfac.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TopQuery = '''' → Se asigna 50 como tope por defecto del SELECT TOP.; si @FlagNotConfirmInvoice = 1 → Construye consulta de facturas NO radicadas (cemestado=''1'' y no presentes en RadicateInvoiceD activos), exponiendo notas crédito/débito y concepto de devolución.; si @FlagNotConfirmInvoice = 0 → Construye consulta de facturas radicadas con saldo > 0, excluyendo las que están en objeción abierta (state=1, documenttype=1), e incluye edad de cartera, valor glosado y reiteraciones.; si @FlagNotConfirmInvoice = 2 → Construye consulta de facturas en estado ''2'' (anuladas/devueltas) excluyendo las que están en proceso de glosa o en oficio de devolución sin tramitar (State=1).; si @InvoiceNumber <> '''' → Aplica filtro AND car.cemnumfac = @InvoiceNumber.; si @StringSQl <> '''' → Concatena cláusula AND dinámica y agrega ORDER BY cemnumfac.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'crcarter; slfactur; gepacien; gecontra; geusuari; adingres; adcenate; crmracts; crcracts; Glosas.GlosaDevolutionsReceptionD; Glosas.GlosaDevolutionsReceptionC; Glosas.GlosaMovementDevolutions; Common.ConceptGlosas; Portfolio.RadicateInvoiceD; Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__FOX';
-- GO
