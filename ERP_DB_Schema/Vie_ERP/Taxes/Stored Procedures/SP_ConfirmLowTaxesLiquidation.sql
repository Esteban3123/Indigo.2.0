
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