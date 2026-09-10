
-- =============================================
-- Author:		Rafael Eduardo Patiño Cabrera
-- Create date: 28-08-2016
-- Description:	Confirma la liquidación de impuestos Menores
-- =============================================
CREATE PROCEDURE [Taxes].[SP_ConfirmLowTaxesLiquidation]
	@IdLowtaxesLiquidation as integer,
	@CodeUser as varchar(20)
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	--Código que se ejecuta para realizar la consulta
	declare @Sql nvarchar(max) = ''

	--Códigos de la cuenta por cobrar
	declare @MessagesAccountReceivableCode varchar(max)

	--Consecutivos contables
	declare @MessagesJournalVoucher varchar(max)

	--Variable para saber el id de la cuenta por cobrar que se va generando
	declare @AccountReceivableId int

	--Tipo de comprobante
	declare @JournalVoucherType varchar(max) 

	--Prefijo para la factura que se va a generar
	declare @Prefix varchar(5)

	--Consecutivo para la factura
	declare @Consecutive int

	--Id de los consecutivos
	declare @TaxesConsecutiveBillingId int

	--Id del libro oficial
	declare @LegalBookId integer

	--Id del comprobante contable
	declare @JournalVoucherId int

	--Código de la cuenta por cobrar
	declare @AccountReceivableCode varchar(20)

	--Variables para la secuencia numerica del formulario cxc
	declare @idSequenceDetail INT
    declare @pattern VARCHAR(300)
    declare @NextS BIGINT
    declare @Scope VARCHAR(5)
    declare @IdSequence INT

	declare @ThirdPartyId int
	declare @TaxValue decimal(18,0)

	BEGIN TRY
		
		print '1'
		--Se valida que la cxc por cobrar tenga la secuencia numérica
		if (select count(*) from Portfolio.PortfolioSequence  where IdForm = '682') = 0 
		Begin
			select 0 as State , 'No se ha creado secuencia numerica para el proceso de cuentas por cobrar' Message, '' as MessagesAccountReceivableCode, '' as MessagesJournalVoucher
			return
		End
		
		--Se valida que existan registros de parámetros de liquidación de impuestos
		if (select count(*) from Taxes.SettingsTaxes) = 0
		Begin
			select 0 as State , 'No existe parámetros de liquidación de impuestos' Message,  '' as MessagesAccountReceivableCode, '' as MessagesJournalVoucher
			return
		End

		select @IdSequence = Id, @Scope = Scope from Portfolio.PortfolioSequence  where IdForm = '682'
		if @Scope = 'OU' 
		Begin
			select @pattern = cs.Pattern, @idSequenceDetail = psd.Id  from Portfolio.PortfolioSequenceDetail psd inner join Common.Sequense cs on cs.Id = psd.IdSequense where psd.IdSequensePortfolioC  = @IdSequence and IdOperatingUnit = 14
		End
		Else 
		Begin
			select @pattern = cs.Pattern, @idSequenceDetail = psd.Id  from Portfolio.PortfolioSequenceDetail psd inner join Common.Sequense cs on cs.Id = psd.IdSequense where psd.IdSequensePortfolioC = @IdSequence
		End
		if (@idSequenceDetail is null) 
		Begin
			select 0 as State , 'La secuencia para el proceso de cuentas por cobrar no esta parametrizada' Message, '' as MessagesAccountReceivableCode, '' as MessagesJournalVoucher
			return
		End
		
		
		print '2'

		--Se consulta el prefijo
		select top 1 @TaxesConsecutiveBillingId = Id, @Prefix = InvoicePrefix, @Consecutive = Consecutive from Taxes.TaxesConsecutiveBilling where Name like '%Impuesto Menores%'

		set @Consecutive  = @Consecutive + 1
		--Se actualiza el consecutivo en la table TaxesConsecutiveBilling
		update Taxes.TaxesConsecutiveBilling set Consecutive =@Consecutive where Id = @TaxesConsecutiveBillingId
		
		
		--select top 1  Id, InvoicePrefix,  Consecutive from Taxes.TaxesConsecutiveBilling where Name like '%Impuesto Menores%'

		--Se consulta el tipo de comprobante contable para enviarlo al form
		select top 1 @JournalVoucherType = CONCAT(jvt.Code, ' - ', jvt.Name) 
		from Taxes.SettingsTaxes st
		inner join GeneralLedger.JournalVoucherTypes jvt on jvt.Id = st.InvoiceLowTaxesJournalVoucherTypeId

		--Obtengo el libro oficial
		select @LegalBookId = id  from GeneralLedger.LegalBook where OfficialBook = 1

		
		print '3'
		
		select @ThirdPartyId = ThirdPartyId from Taxes.LowTaxLiquidation where id = @IdLowtaxesLiquidation 
		set @taxValue = (select sum(d.TotalValueTax ) from [Taxes].[LowTaxLiquidation] c inner join [Taxes].[LowTaxLiquidationDetail] d on c.id = d.LowTaxLiquidationId where c.id = @IdLowtaxesLiquidation )
		set @taxValue = ROUND(@taxValue,-2)
			
		declare @IdCuenta as integer = (select Id from GeneralLedger.MainAccounts  where Number = '13105601' and LegalBookId =1) --DB
		declare @IdcuentaAcreditar as integer = (select id from GeneralLedger.MainAccounts  where Number = '41055801' and LegalBookId =1) --CR
		declare @InvoiceNumber as varchar(20) =  CONCAT(@Prefix, '900', CAST(@Consecutive as varchar))
		update Taxes.LowTaxLiquidation set invoicenumber = @InvoiceNumber, [TotalToPay] = @TaxValue  where id = @IdLowtaxesLiquidation  

		--consultamos Id del presupuesto
		declare @Idbudget as integer = (
		select B.id from Budget.BudgetaryEntity e   inner join 
		Budget.BudgetaryValidity v on e.id = v.BudgetaryEntityId inner join 
		Budget.BudgetHeader H on h.BudgetaryValidityId =  v.Id inner join
		Budget.Category C on C.BudgetaryValidityId = v.id inner join  
		Budget.Budget B on B.BudgetHeaderId = H.id  and B.CategoryId = C.id 
		 where e.Code = '0101NVA' and v.[Year] = 2016 and C.Code = '01010130001')

			--------------------------------- Inserts CxC ----------------------------------------------------------

			--Se genera el codigo de la cuenta por cobrar
			update Portfolio.PortfolioSequenceDetail set @NextS = [Next] += 1 where Id = @idSequenceDetail
			select @AccountReceivableCode = dbo.GetSequence('',@pattern,(@NextS - 1))
			
			--Se genera la cabecera de la cxc AccountReceivable
			insert into Portfolio.AccountReceivable(Code, OperatingUnitId, AccountReceivableType, ThirdPartyId, InvoiceNumber, AccountReceivableDate,
			Term, ExpiredDate, Observations, PortfolioStatus,  OpeningBalance, PaymentAgreement, RegistrationAdjusted, NumberShares,
			Value, Balance, Status, AccountWithoutRadicateId, AccountRadicateId, AccountObjectionRemediedId,AffectBudget, BudgetId, CreationUser, CreationDate)
			values(@AccountReceivableCode, 14, 3, @ThirdPartyId, CONCAT(@Prefix, '900', CAST(@Consecutive as varchar)), [Common].[GETDATE](), 30, [Common].[GETDATE](),
			'Cuenta por cobrar generada desde liquidación de impuestos menores', 1, 0, 0, 0, 1, @TaxValue, @TaxValue, 2, 
			null, null, null, 1,@Idbudget,
			@CodeUser, [Common].[GETDATE]())

			--Se obtiene el id de la cxc que se esta generando
			set @AccountReceivableId = SCOPE_IDENTITY()

			--Se generan los detalles de la cxc AccountReceivableAccounting con la tabla de TaxesInvoiceDetail
			insert into Portfolio.AccountReceivableAccounting(AccountReceivableId, MainAccountId, ThirdPartyId, Value, Balance)
			values(@AccountReceivableId,@IdCuenta , @ThirdPartyId, @TaxValue, @TaxValue)

			--Se generan el detalle de la cxc AccountReceivableShare
			insert into Portfolio.AccountReceivableShare(AccountReceivableId, Number, ExpiredDate, Value, Balance, DebitValue, CreditValue, 
			TransferValue, PaymentValue, CrossingValue)
			values(@AccountReceivableId, 1, [Common].[GETDATE](), @TaxValue, @TaxValue, 0, 0, 0, 0, 0)

			
		
			--------------------------------- Fin Inserts CxC ----------------------------------------------------------

				 
			---------------------------------- Inserts comprobante contable ------------------------------------------------

			--Cabecera del comprobante contable
			declare @JournalVouchers as table([Id] [int] NOT NULL, [AccountingMovementId] [int] NOT NULL, [Consecutive] [bigint] NOT NULL,
				[LegalBookId] [int] NOT NULL, [IdJournalVoucher] [int] NOT NULL, [VoucherDate] [datetime] NOT NULL, [Imported] [bit] NOT NULL,
				[Status] [tinyint] NOT NULL, [Detail] [varchar](500) NULL, [EntityCode] [varchar](20) NULL, [EntityId] [int] NULL,
				[EntityName] [varchar](250) NULL, [IsClosedYear] [bit] NOT NULL, [CreationUser] [varchar](20) NOT NULL, [CreationDate] [datetime] NOT NULL,
				[ModificationUser] [varchar](20) NULL, [ModificationDate] [datetime] NULL, [ConfirmationUser] [varchar](20) NULL,
				[ConfirmationDate] [datetime] NULL)

			--Detalle del comprobante contable
			declare @JournalVoucherDetails as table([Id] [int] NOT NULL, [IdAccounting] [int] NOT NULL, [IdMainAccount] [int] NOT NULL,
				[IdThirdParty] [int] NULL, [IdCostCenter] [int] NULL, [DebitValue] [decimal](18, 2) NOT NULL, [CreditValue] [decimal](18, 2) NOT NULL,
				[Detail] [varchar](max) NULL, [IdRetention] [int] NULL, [RetentionRate] [decimal](5, 3) NULL, [BaseValue] [decimal](18, 0) NULL ,
				[BillingValue] [decimal](18, 0) NULL)

				print 'hola rfa'

			--inserto la cabecera del comprobante
			insert into @JournalVouchers 
			select 0, 0, 0, @LegalBookId , (select top 1 InvoiceLowTaxesJournalVoucherTypeId from Taxes.SettingsTaxes),
			[Common].[GETDATE](), 0 , 2, 'Creado desde liquidación de impuestos Menores', @InvoiceNumber , Id, 'LowTaxLiquidation', 0, 
			@CodeUser, [Common].[GETDATE](), @CodeUser, [Common].[GETDATE](), @CodeUser, [Common].[GETDATE]()
			from Taxes.LowTaxLiquidation  where Id = @IdLowtaxesLiquidation

			--se insertan los detalles por cada [LowTaxLiquidationDetail] para Debito
			insert into @JournalVoucherDetails 
			select 0, 
			0, 
			@IdCuenta, --DB
			@ThirdPartyId, 
			(select top 1 Id from Payroll.CostCenter),
	        round(TotalValueTax,0), 
			0, 
			'Detalle generado desde liquidación de impuestos Menores',
			null, 0, 0, 0
			from Taxes.LowTaxLiquidationDetail 	where LowTaxLiquidationId  = @IdLowtaxesLiquidation

			--se insertan los detalles por cada [LowTaxLiquidationDetail] para Credito
			insert into @JournalVoucherDetails 
			select 0, 
			0, 
			@IdcuentaAcreditar,
			@ThirdPartyId, 
			(select top 1 Id from Payroll.CostCenter),
			0, 
			round(TotalValueTax,0), 
			'Detalle generado desde liquidación de impuestos Menores',
			null, 0, 0, 0
			from Taxes.LowTaxLiquidationDetail 	where LowTaxLiquidationId  = @IdLowtaxesLiquidation

			--Tabla de resultado del comprobante
			declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)
			
			
			--Xml que se crea para guardar el comprobante
			declare @JournalVoucherXML as XML

			--Genero el XML para guardar el comprobante 
			select @JournalVoucherXML =  convert(xml, (select * from @JournalVouchers JournalVoucher 
			inner join @JournalVoucherDetails JournalVoucherDetail on JournalVoucher.id = JournalVoucherDetail.IdAccounting   
			For xml AUTO,TYPE, ELEMENTS))
			
			--Se envia a guardar al sp
			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 
			
				print 'hola se genero comprobante'

			--Se validan los resultados
			if (select code  from @resultJournalVoucher) = '999' 
			Begin	
				declare @errorJV varchar(max)
				select @errorJV = MessageResult  from @resultJournalVoucher 
				select 0 as State , @errorJV Message, '' as MessagesIds, 
				 '' as MessagesAccountReceivableCode, '' as MessagesJournalVoucher
				return
			End  

			select @JournalVoucherId = IdJournalVoucher  from @resultJournalVoucher
			
			declare @JournalVoucherCode as varchar(max) = isnull( (select cast( Consecutive as varchar(30))  
			from GeneralLedger.JournalVouchers where id = @JournalVoucherId ),0)
			
			---------------------------------- Fin Inserts comprobante contable ------------------------------------------------

			
			
			--Se asignan los numeros de factura y los codigos de cxc que se van a retornar
			--set @MessagesInvoiceNumber = CONCAT(@Prefix, '-', CAST(@Consecutive as varchar))
			set @MessagesAccountReceivableCode = @AccountReceivableCode
			set @MessagesJournalVoucher = 'tipo ' + @JournalVoucherType + ' con consecutivos: ' + @JournalVoucherCode
			
			
			
			select 1 as State , 'Se confirmó correctamente' Message, 
			 @MessagesAccountReceivableCode as MessagesAccountReceivableCode,
			@MessagesJournalVoucher as MessagesJournalVoucher

	END TRY
    BEGIN CATCH
        select 0 as State , ERROR_MESSAGE() + ', Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) Message, '' as MessagesAccountReceivableCode, '' as MessagesJournalVoucher
    END CATCH;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma la liquidación de impuestos de bajo monto (impuestos menores) para un tercero contribuyente. Valida que exista la secuencia numérica configurada para cuentas por cobrar (formulario 682) y los parámetros de liquidación de impuestos antes de proceder. Genera el número de factura usando el prefijo y consecutivo del módulo de impuestos menores, calcula y redondea el valor total del impuesto, y crea los registros contables (comprobante de diario) y la cuenta por cobrar en el portafolio, vinculando las cuentas del libro mayor oficial (débito 13105601, crédito 41055801) y asociando el presupuesto de la entidad presupuestaria correspondiente al año y categoría configurados.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmLowTaxesLiquidation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmLowTaxesLiquidation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma una liquidación de impuestos menores generando la factura con consecutivo, la cuenta por cobrar al tercero (cabecera, contabilización y cuota) y el comprobante contable con débito y crédito por cada detalle de la liquidación.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmLowTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una secuencia numérica parametrizada para el formulario IdForm=682 (cuentas por cobrar); Debe existir al menos un registro en Taxes.SettingsTaxes con el tipo de comprobante para impuestos menores; Debe existir el detalle de secuencia (PortfolioSequenceDetail), filtrado por OU=14 si el Scope es ''OU''; Debe existir un registro en Taxes.TaxesConsecutiveBilling cuyo Name contenga ''Impuesto Menores''; Debe existir un libro oficial (OfficialBook=1) en GeneralLedger.LegalBook; Deben existir las cuentas contables 13105601 y 41055801 en el libro legal 1; Debe existir la liquidación de impuestos identificada y al menos un detalle asociado; Debe existir el presupuesto con entidad ''0101NVA'', vigencia 2016 y categoría ''01010130001''; Debe existir al menos un centro de costo en Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmLowTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El consecutivo de facturación de ''Impuesto Menores'' siempre se incrementa en 1 antes de generar la factura; El número de factura se compone como Prefijo + ''900'' + consecutivo; El valor total del impuesto se redondea a la centena (ROUND(valor,-2)); Toda CxC generada se crea con OperatingUnitId=14, AccountReceivableType=3, PortfolioStatus=1, Status=2, Term=30 días, NumberShares=1, AffectBudget=1; El asiento contable usa cuenta débito 13105601 y cuenta crédito 41055801 del libro legal 1; Por cada detalle de la liquidación se generan dos movimientos contables (uno débito y uno crédito) con valor redondeado; El presupuesto afectado corresponde a la entidad ''0101NVA'', vigencia 2016 y categoría ''01010130001''; Solo se considera el libro contable marcado como OfficialBook=1 para el comprobante; El comprobante se inserta con Status=2 (confirmado) e Imported=0; Si falla la creación del comprobante contable, no se retorna éxito (pero los inserts previos de CxC ya se ejecutaron sin rollback explícito)', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmLowTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de impuestos menores; Cuenta por cobrar; Comprobante contable; Secuencia/consecutivo de facturación; Prefijo de factura; Libro oficial contable; Tercero; Presupuesto; Centro de costo; Plan de cuentas (PUC); Débito/Crédito', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmLowTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe secuencia numérica configurada para el formulario de cuentas por cobrar (IdForm=682) → Retorna State=0 con mensaje de error y termina; si No existen registros en parámetros de liquidación de impuestos (Taxes.SettingsTaxes vacía) → Retorna State=0 con mensaje de error y termina; si Scope de la secuencia es ''OU'' → Obtiene el detalle de secuencia filtrando por unidad operativa 14 else Obtiene el detalle de secuencia sin filtrar por unidad operativa; si El detalle de secuencia (idSequenceDetail) no se encontró → Retorna State=0 con mensaje ''La secuencia para el proceso de cuentas por cobrar no esta parametrizada'' y termina; si El SP de creación del comprobante contable retorna code=''999'' → Retorna State=0 con el mensaje de error del comprobante y termina; si Cualquier error en el TRY → CATCH retorna State=0 con ERROR_MESSAGE() y línea del error', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmLowTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmLowTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioSequence; Taxes.SettingsTaxes; Portfolio.PortfolioSequenceDetail; Common.Sequense; Taxes.TaxesConsecutiveBilling; GeneralLedger.JournalVoucherTypes; GeneralLedger.LegalBook; Taxes.LowTaxLiquidation; Taxes.LowTaxLiquidationDetail; GeneralLedger.MainAccounts; Budget.BudgetaryEntity; Budget.BudgetaryValidity; Budget.BudgetHeader; Budget.Category; Budget.Budget; Payroll.CostCenter; GeneralLedger.JournalVouchers', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmLowTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmLowTaxesLiquidation';
-- GO
