-- =============================================
-- Author:		Rafael patiño
-- Create date: 25/03/2014
-- Description:	Listar Facturas de ERP .NET
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceList__NET]
	@container varchar(150),
	@nit varchar(15),
	@InvoiceNumber varchar(50),
	@IndigoCompany varchar(20),
	@HISContainer varchar(10),
	@StringSQl varchar(1000),
	@TopQuery varchar(4),
	@FlagNotConfirmInvoice varchar(20)
AS
BEGIN
	
		SET NOCOUNT ON;
	
	Create table #tablaFactura(
		InvoiceNumber  varchar(50),
		InvoiceCategory varchar(200),
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
		ConceptDevolution varchar(100),
		OpeningBalance bit,
		DocumentType tinyint			 			  
	)

	
	declare @sql as nVarchar(max)
	
		IF @TopQuery = '' BEGIN
			set @TopQuery = '50'
		END
		
		--VERSION DE FOX

	/*	set @sql = 'USE ' + @container  + '           
				SELECT top ' + @TopQuery + ' 
			car.cemnumfac as InvoiceNumber,
			car.cemfecfac as InvoiceDate,
			crc.ccrnumrad as RadicatedNumber,
			crc.ccrfecrad as RadicatedDate,
			com.gpacodigo as PatientCode,
			RTRIM(com.gpanombre) + '' ''+ LTRIM(com.gpaapelli) as PatientName,
			sal.ainconsec as IngressNumber,
			usu.USUNOMBRE as UserNameInvoice,
			'''' as Comment,
			sal.GECCODIGO as ContractCode,
			con.GECNOMENT as ContractNombre,
			1 as State,
			car.cemsalfac as BalanceInvoice,
			''false'' as Selection,
			sal.SFATOTFAC as InvoiceValueEntity,
			sal. SFAVALPAC as InvoiceValuePacient,
			car.CPCCODCUE as AccountantAccountCustomers,
			cast(datediff(dd,crc.ccrfecrad,[Common].[GETDATE]()) as int) as PortfolioAge,
			glo.ValueGlosado as ValueGlosado,
			car.cemestado as StateCurrentInvoice,
							(select top 1 ObjC.RadicatedConsecutive from ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionC ObjC inner join 
	' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionD  objD on ObjC.Id = objD.GlosaObjectionsReceptionCId
	where ObjD.invoicenumber = glo.InvoiceNumber and objD.DocumentType = 1) as RadicatedConsecutive,
					(select top 1 ObjC.state from ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionC ObjC inner join 
	' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionD  objD on ObjC.Id = objD.GlosaObjectionsReceptionCId
	where ObjD.invoicenumber = glo.InvoiceNumber and objD.DocumentType = 1) as StateObjectionReceptionC,
						(				select top 1 ObjC.RadicatedConsecutive from ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionC ObjC inner join 
	' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionD  objD on ObjC.Id = objD.GlosaObjectionsReceptionCId
	where ObjD.invoicenumber = glo.InvoiceNumber and objD.DocumentType = 2) as Reiterated,
	glo.State as StatePortfolioGlosada
	 FROM ..crcarter car 
			INNER JOIN ..slfactur sal ON car.cemnumfac = sal.sfanumfac INNER JOIN ..gepacien com ON sal.gpacodigo = com.gpacodigo
			INNER JOIN ..gecontra con on con.geccodigo=sal.geccodigo INNER JOIN ..geusuari usu on usu.USUCODIGO=sal.USUCODIGO
			INNER JOIN ..adingres ing ON sal.ainconsec = ing.ainconsec INNER JOIN ..adcenate cen on cen.acacodigo = ing.acacodigo
			LEFT OUTER JOIN ..crmracts crm ON car.cemnumfac = crm.cmrnumfac LEFT OUTER JOIN ..crcracts crc ON crm.ccrnumrad = crc.ccrnumrad 
			LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaPortfolioGlosada glo on glo.InvoiceNumber=car.cemnumfac
	WHERE car.tercodter =  right(''000000000000000'' +  Ltrim(Rtrim(@nit)),15) and car.cemsalfac > 0 	'
	*/

	

		--VERSION DE NET

	--facturas no radicadas
		if @FlagNotConfirmInvoice = 1 begin

	/*	set @sql = 'USE ' + @container  +'           
				SELECT top ' + @TopQuery + '
			car.cxcdocume as InvoiceNumber,
			car.cxcdocfecha as InvoiceDate,
			null as RadicatedNumber,
			null RadicatedDate,
			pac.pacnumdoc as PatientCode,
			RTRIM(pac.pacprinom) + ''''+ LTRIM(pac.pacpriape) as PatientName,
			RTRIM(pac.pacprinom) as PatientNameInicial,
			LTRIM(pac.pacpriape) as PatientLastName,
			convert(varchar(20),com.ainconsec) as IngressNumber,
			usu.USUNOMBRE as UserNameInvoice,
			'''' as Comment,
			CON.GECCODIGO as ContractCode,
			con.GECNOMENT as ContractName,
			''1'' as State,
			car.crnsaldo as BalanceInvoice,
			convert(bit,0) as Selection,
			sal.SFATOTFAC as InvoiceValueEntity,
			sal. SFAVALPAC as InvoiceValuePacient,
			CTA.CUECODIGO as AccountantAccountCustomers,
			NULL as PortfolioAge,
			NULL as ValueGlosado,
			convert(varchar(20),car.CXCESTCAR) as StateCurrentInvoice,
			planB.GDECODIGO as CodePlan,
			CON.GECCODIGO as ContractEntity,
			null AS RadicatedConsecutive,
			null  as StateObjectionReceptionC,
			null as Reiterated,
			null  as StatePortfolioGlosada,
			0 as CreditNoteValue,
			0  as DebitNoteValue
	 FROM ..CRNCXC car 
			INNER JOIN ..CRNCXCC B ON  car.OID=B.CRNCXC
			INNER JOIN ..SLNFACTUR sal ON car.CXCDOCUME = sal.sfanumfac 
			INNER JOIN ..ADNINGRESO com ON sal.ADNINGRESO = com.OID 
			INNER JOIN ..GENPACIEN pac ON COM.GENPACIEN = pac.oid
			INNER JOIN ..GENDETCON planB on planB.OID=sal.GENDETCON 
			INNER JOIN ..GENCONTRA CON on planB.GENCONTRA1 = CON.OID
			INNER JOIN ..GENUSUARIO usu on usu.OID=sal.GENUSUARIO1
			INNER JOIN ..CTNCUENTA CTA on CTA.oid = CAR.CTNCUENTA
			INNER JOIN  ..GENTERCER T ON  car.GENTERCER = T.OID	
	WHERE car.CXCDOCUME NOT IN (select InvoiceNumber from ' + @IndigoCompany + '.Glosas.RadicateInvoiceD where state IN (1,2) )
	 AND  T.TERNUMDOC =  Ltrim(Rtrim(@nit)) and (B.CCVALOR+B.CCVALDEB-B.CCVALCRE-B.CCVALABO-B.CCVALTRA) > 0 AND car.CXCESTCAR = 1  	' 

	 */

	 select ''
	end else if @FlagNotConfirmInvoice = 0 begin

	--set @Sql = ''
	set @Sql = 'USE ' + @container  +'           
				SELECT top ' + @TopQuery + '
			car.cxcdocume as InvoiceNumber,
			'''' as InvoiceCategory,
			car.cxcdocfecha as InvoiceDate,
			convert(varchar(20),DOC.CDCONSEC) as RadicatedNumber,
			crm.CRFFECRAD as RadicatedDate,
			pac.pacnumdoc as PatientCode,
			RTRIM(pac.pacprinom) + '' ''+ LTRIM(pac.pacpriape) as PatientName,
			RTRIM(pac.pacprinom) as PatientNameInicial,
			LTRIM(pac.pacpriape) as PatientLastName,
			convert(varchar(20),com.ainconsec) as IngressNumber,
			com.AINFECING as IngressDate,
			usu.USUNOMBRE as UserNameInvoice,
			'''' as Comment,
			CON.GECCODIGO as ContractCode,
			con.GECNOMENT as ContractName,
			''1'' as State,
			car.crnsaldo as BalanceInvoice,
			convert(bit,0) as Selection,
			sal.SFATOTFAC as InvoiceValueEntity,
			sal. SFAVALPAC as InvoiceValuePacient,
			CTA.CUECODIGO as AccountantAccountCustomers,
			cast(datediff(dd,crc.CRFFECRAD,[Common].[GETDATE]()) as int) as PortfolioAge,
			glo.ValueGlosado as ValueGlosado,
			convert(varchar(20),car.CXCESTCAR) as StateCurrentInvoice,
			planB.GDECODIGO as CodePlan,
			CON.GECCODIGO as ContractEntity,
			C.RadicatedConsecutive AS RadicatedConsecutive,
			convert(int,C.state)  as StateObjectionReceptionC,
			(select count(Id) from ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionD  where InvoiceNumber = car.CXCDOCUME AND state <> 4  ) as Reiterated,
			convert(int,glo.State)  as StatePortfolioGlosada,
			NUll as CreditNoteValue,
			NULL as DebitNoteValue,
			0 as Devolution,
			NULL as ConceptDevolution,
			NULL as OpeningBalance,
			NULL as DocumentType
	 FROM ..CRNCXC car 	
			INNER JOIN ..CRNCXCC B ON  car.OID=B.CRNCXC
			INNER JOIN ..SLNFACTUR sal ON car.CXCDOCUME = sal.sfanumfac 
			INNER JOIN ..ADNINGRESO com ON sal.ADNINGRESO = com.OID 
			INNER JOIN ..GENPACIEN pac ON COM.GENPACIEN = pac.oid
			INNER JOIN ..GENDETCON planB on planB.OID=sal.GENDETCON 
			INNER JOIN ..GENCONTRA CON on planB.GENCONTRA1 = CON.OID
			INNER JOIN ..GENUSUARIO usu on usu.OID=sal.GENUSUARIO1
			INNER JOIN ..CTNCUENTA CTA on CTA.oid = CAR.CTNCUENTA
			LEFT OUTER JOIN ..CRNRADFACD crm ON car.OID = crm.CRNCXC
			LEFT OUTER JOIN ..CRNRADFACC crc ON crm.CRNRADFACC = crc.OID 
			INNER JOIN ..CRNDOCUME DOC on DOC.oid = CRC.OID 
			INNER JOIN  ..GENTERCER T ON  car.GENTERCER = T.OID	
			LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaPortfolioGlosada glo on glo.InvoiceNumber=car.cxcdocume
			LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionD D on D.InvoiceNumber=glo.InvoiceNumber AND D.documenttype = 1
			LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionC C on C.Id=D.GlosaObjectionsReceptionCId
			WHERE car.CXCDOCUME not in (select InvoiceNumber from ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionD D inner join ' + @IndigoCompany + '.Glosas.GlosaObjectionsReceptionC C on C.id = D.GlosaObjectionsReceptionCId where D.documenttype = 1  AND C.state = 1 ) AND  T.TERNUMDOC =  Ltrim(Rtrim(@nit)) and (B.CCVALOR+B.CCVALDEB-B.CCVALCRE-B.CCVALABO-B.CCVALTRA) > 0 AND car.CXCESTCAR = 2'
	
	end else if @FlagNotConfirmInvoice = 2 begin 

		set @Sql = 'USE ' + @container  +'           
			SELECT top ' + @TopQuery + '
			car.cxcdocume as InvoiceNumber,
			'''' as InvoiceCategory,
			car.cxcdocfecha as InvoiceDate,
			convert(varchar(20),DOC.CDCONSEC) as RadicatedNumber,
			crm.CRFFECRAD as RadicatedDate,
			pac.pacnumdoc as PatientCode,
			RTRIM(pac.pacprinom) + '' ''+ LTRIM(pac.pacpriape) as PatientName,
			RTRIM(pac.pacprinom) as PatientNameInicial,
			LTRIM(pac.pacpriape) as PatientLastName,
			convert(varchar(20),com.ainconsec) as IngressNumber,
			com.AINFECING as IngressDate,
			usu.USUNOMBRE as UserNameInvoice,
			'''' as Comment,
			CON.GECCODIGO as ContractCode,
			con.GECNOMENT as ContractName,
			''1'' as State,
			car.crnsaldo as BalanceInvoice,
			convert(bit,0) as Selection,
			sal.SFATOTFAC as InvoiceValueEntity,
			sal. SFAVALPAC as InvoiceValuePacient,
			CTA.CUECODIGO as AccountantAccountCustomers,
			cast(datediff(dd,crc.CRFFECRAD,[Common].[GETDATE]()) as int) as PortfolioAge,
			NULL as ValueGlosado,
			convert(varchar(20),car.CXCESTCAR) as StateCurrentInvoice,
			planB.GDECODIGO as CodePlan,
			CON.GECCODIGO as ContractEntity,
			C.RadicatedConsecutive AS RadicatedConsecutive,
			convert(int,C.state)  as StateObjectionReceptionC,
			null as Reiterated,
			null  as StatePortfolioGlosada,
			null as CreditNoteValue,
			null  as DebitNoteValue,
			0 as Devolution,
			NULL as ConceptDevolution,
			NULL as OpeningBalance,
			NULL as DocumentType
			FROM ..CRNCXC car  INNER JOIN ..CRNCXCC B ON  car.OID=B.CRNCXC	
			INNER JOIN ..SLNFACTUR sal ON car.CXCDOCUME = sal.sfanumfac 
			INNER JOIN ..ADNINGRESO com ON sal.ADNINGRESO = com.OID 
			INNER JOIN ..GENPACIEN pac ON COM.GENPACIEN = pac.oid
			INNER JOIN ..GENDETCON planB on planB.OID=sal.GENDETCON 
			INNER JOIN ..GENCONTRA CON on planB.GENCONTRA1 = CON.OID
			INNER JOIN ..GENUSUARIO usu on usu.OID=sal.GENUSUARIO1
			INNER JOIN ..CTNCUENTA CTA on CTA.oid = CAR.CTNCUENTA
			INNER JOIN  ..GENTERCER T ON  car.GENTERCER = T.OID	
			LEFT OUTER JOIN ..CRNRADFACD crm ON car.OID = crm.CRNCXC
			LEFT OUTER JOIN ..CRNRADFACC crc ON crm.CRNRADFACC = crc.OID 
			INNER JOIN ..CRNDOCUME DOC on DOC.oid = CRC.OID
			LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaDevolutionsReceptionD D on car.cxcdocume = D.InvoiceNumber 
			LEFT OUTER JOIN ' + @IndigoCompany + '.Glosas.GlosaDevolutionsReceptionC c on c.id = d.GlosaDevolutionsReceptionCId  
			WHERE  car.cxcdocume not in (select InvoiceNumber from ' + @IndigoCompany + '.Glosas.GlosaPortfolioGlosada   --no incluir las que estan en un proceso de glosa
			union all
			select InvoiceNumber from ' + @IndigoCompany + '.Glosas.GlosaDevolutionsReceptionD  where State = 1 --no incluir las que estan ya en un ofico de devolucion sin tramitar
			) AND  T.TERNUMDOC =  Rtrim(Rtrim(@nit)) and (B.CCVALOR+B.CCVALDEB-B.CCVALCRE-B.CCVALABO-B.CCVALTRA) > 0 AND car.CXCESTCAR = 2 			'

	end
	

	IF @InvoiceNumber <> '' BEGIN
	 set @sql = @sql + ' AND car.CXCDOCUME  = ''' + @InvoiceNumber + ''' '
	
	END
	
	IF @StringSQl <> '' BEGIN
		set @sql = @sql + ' AND ' +  @StringSQl + ' ORDER BY car.CXCDOCUME'
	END

	print @sql
	--INSERT INTO #tablaFactura
	execute sp_executesql @sql,N'@nit varchar(15)',@nit

	/*print @sql
	INSERT INTO #tablaFactura
	execute sp_executesql @sql,N'@nit varchar(15)',@nit

	set @sql = ''
	set @sql = 'SELECT  top ' + @TopQuery + '  InvoiceNumber,
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
						RadicatedConsecutive,
						CodePlan,
						ContractEntity,
						StateObjectionReceptionC,
						Reiterated,
						StatePortfolioGlosada,
						CreditNoteValue,
						DebitNoteValue 	
				FROM #tablaFactura WHERE 1=1  '
	

	IF @InvoiceNumber <> '' BEGIN
		set @sql = @sql + ' AND InvoiceNumber = Ltrim(Rtrim(''' + @InvoiceNumber + ''')) '
	END
	
	IF @StringSQl <> '' BEGIN
		set @sql = @sql + ' AND ' +  @StringSQl
	END

	print @sql

	execute sp_executesql @sql*/

	--NOTA *
	--NOTA: FALTA REVISAR EL CAMPO car.tercodter Y LA CANTIDAD DE CARACTERES DE CADA CAMPO, ASI COMO TAMBIEN FILTROS
	--NOTA * 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que lista facturas de cartera del ERP Indigo (versión .NET y legado FOX) para un pagador identificado por NIT. Recupera información de facturación como número de factura, fecha, datos del paciente (cédula, nombre), número de ingreso, contrato, saldo pendiente, valor glosado, estado de la factura y notas crédito/débito. Permite filtrar por número de factura, empresa Indigo, contenedor HIS y cantidad máxima de registros; también soporta un modo especial para facturas no confirmadas/radicadas. Construye SQL dinámico en tiempo de ejecución para consultar tablas de facturación, cartera, pacientes, contratos y la tabla de glosas (Glosas.GlosaPortfolioGlosada), además de cruzar con recepciones de objeciones (GlosaObjectionsReceptionC/D) para obtener el estado de glosa, radicación y reiteración de cada factura.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceList__NET';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceList__NET';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista facturas de cartera del ERP .NET para gestión de glosas, devoluciones y radicación, según el modo solicitado (no radicadas, en glosa o disponibles para devolución).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El @container debe corresponder a una base de datos válida del ERP financiero accesible vía USE dinámico.; @IndigoCompany debe apuntar a la base de datos donde residen los esquemas Glosas (GlosaPortfolioGlosada, GlosaObjectionsReceptionC/D, GlosaDevolutionsReceptionC/D, RadicateInvoiceD).; El NIT del tercero (@nit) se compara contra GENTERCER.TERNUMDOC tras Ltrim/Rtrim.; Si @TopQuery viene vacío se asume ''50'' como límite de filas.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas con saldo neto positivo: (CCVALOR+CCVALDEB-CCVALCRE-CCVALABO-CCVALTRA) > 0.; En los modos 0 y 2 solo se consideran facturas con cartera en estado CXCESTCAR = 2.; El NIT se busca con coincidencia exacta sobre GENTERCER.TERNUMDOC.; La antigüedad de cartera (PortfolioAge) se calcula en días entre la fecha de radicación (CRFFECRAD) y la fecha actual de [Common].[GETDATE]().; El conteo de Reiterated cuenta registros en GlosaObjectionsReceptionD donde state <> 4 para la factura.; Una factura nunca aparece simultáneamente en el modo 2 si está en GlosaPortfolioGlosada o tiene devolución en GlosaDevolutionsReceptionD con State=1.; El nombre del paciente se concatena como RTRIM(primer nombre) + '' '' + LTRIM(primer apellido).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Cartera; Saldo de factura; Paciente; Ingreso (admisión); Contrato; Plan; Glosa; Portafolio glosado; Objeciones de recepción; Reiteración de glosa; Radicación de factura; Devolución; Antigüedad de cartera; Cuenta contable de clientes; Tercero/NIT', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @FlagNotConfirmInvoice = 1 se devuelve un resultset vacío (SELECT '''') correspondiente al modo de facturas no radicadas (rama deshabilitada en producción).; [RETURN_RESULT] resultset: Cuando @FlagNotConfirmInvoice = 0 se retornan facturas con CXCESTCAR = 2 (cartera activa), saldo positivo (CCVALOR+CCVALDEB-CCVALCRE-CCVALABO-CCVALTRA > 0) y que NO estén en GlosaObjectionsReceptionD con documenttype=1 y GlosaObjectionsReceptionC.state=1, incluyendo datos de glosa y objeciones.; [RETURN_RESULT] resultset: Cuando @FlagNotConfirmInvoice = 2 se retornan facturas con CXCESTCAR = 2 y saldo positivo que NO estén en GlosaPortfolioGlosada ni en GlosaDevolutionsReceptionD con State=1 (excluyendo facturas ya en proceso de glosa o en oficio de devolución sin tramitar).; [RETURN_RESULT] resultset: Si @InvoiceNumber no está vacío, se filtra adicionalmente por car.CXCDOCUME = @InvoiceNumber.; [RETURN_RESULT] resultset: Si @StringSQl no está vacío, se concatena como filtro adicional al WHERE y se aplica ORDER BY car.CXCDOCUME (riesgo de inyección SQL).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TopQuery = '''' → Se asigna ''50'' como tope por defecto de filas.; si @FlagNotConfirmInvoice = 1 → Modo facturas no radicadas: actualmente solo ejecuta SELECT '''' (la consulta original está comentada).; si @FlagNotConfirmInvoice = 0 → Modo facturas en proceso de glosa: arma consulta cruzando GlosaPortfolioGlosada y GlosaObjectionsReceptionC/D para reportar estado de glosa, radicación y reiteración.; si @FlagNotConfirmInvoice = 2 → Modo facturas elegibles para devolución: excluye facturas ya glosadas o en oficios de devolución sin tramitar y cruza con GlosaDevolutionsReceptionC/D.; si @InvoiceNumber <> '''' → Agrega filtro por número de factura específico.; si @StringSQl <> '''' → Concatena filtro libre y agrega ORDER BY por número de documento.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'CRNCXC; CRNCXCC; SLNFACTUR; ADNINGRESO; GENPACIEN; GENDETCON; GENCONTRA; GENUSUARIO; CTNCUENTA; GENTERCER; CRNRADFACD; CRNRADFACC; CRNDOCUME; Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionC; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaDevolutionsReceptionC; Glosas.GlosaDevolutionsReceptionD; Glosas.RadicateInvoiceD', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__NET';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceList__NET';
-- GO
