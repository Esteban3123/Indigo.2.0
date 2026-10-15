

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 26-08-2016
-- Description:	Confirma la liquidación de impuestos
-- =============================================
CREATE PROCEDURE [Taxes].[SP_ConfirmTaxesLiquidation]
	@Year as int,
	@Ids as varchar(max),
	@CodeUser as varchar(20)
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON
	
	--Tabla temporal para obtener los registros que vienen en la variable @Ids
	declare @TaxesLiquidationDetail table(Id int, TaxesLiquidationId int, TaxesPropertyId int, Appraisal decimal(18,0), ThirdPartyId int,
	PercentageOwner decimal(5,2), TotalValueTax decimal(18,0), ValueOwner decimal(18,0), TaxesInvoiceId int)

	--Código que se ejecuta para realizar la consulta
	declare @Sql nvarchar(max) = ''

	--Numeros de facturas que se generan para retornar
	declare @MessagesInvoiceNumber varchar(max)

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

	--Variable para saber la posición del cursor y concatenar los números de facturas que se van generando
	declare @Position int = 0

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

	--Variables para el cursor
	declare @Id int
	declare @TaxesLiquidationId int
	declare @TaxesPropertyId int
	declare @ThirdPartyId int
	declare @Appraisal decimal(18,0)
	declare @TaxValue decimal(18,0)
	declare @TaxesInvoiceId int

	BEGIN TRY
		
		--Se valida que existan registros de parámetros de liquidación de impuestos
		if (select count(*) from Taxes.SettingsTaxes) = 0
		Begin
			select 0 as State , 'No existe parámetros de liquidación de impuestos' Message, '' as MessagesIds, 
			'' as MessagesInvoiceNumber, '' as MessagesAccountReceivableCode, '' as MessagesJournalVoucher
			return
		End

		declare @ConsecutiveId int

		--Se ejecuta el sql y se inserta en la tabla temporal de detalles de liquidación
		insert into @TaxesLiquidationDetail
		select Id, TaxesLiquidationId, TaxesPropertyId, Appraisal, ThirdPartyId, PercentageOwner,
		TotalValueTax, ValueOwner, TaxesInvoiceId from Taxes.TaxesLiquidationDetail where Id between (select MIN(CAST(Data as numeric(18,0))) from dbo.Split(@Ids,',')) and (select MAX(CAST(Data as numeric(18,0))) from dbo.Split(@Ids,','))
		
		--Se consulta el prefijo de la factura
		select top 1 @ConsecutiveId = Id, @TaxesConsecutiveBillingId = Id, @Prefix = InvoicePrefix, @Consecutive = Consecutive from Taxes.TaxesConsecutiveBilling

		--Se consulta el tipo de comprobante contable para enviarlo al form
		select top 1 @JournalVoucherType = CONCAT(jvt.Code, ' - ', jvt.Name) 
		from Taxes.SettingsTaxes st
		inner join GeneralLedger.JournalVoucherTypes jvt on jvt.Id = st.InvoicePropertyJournalVoucherTypeId

		--Obtengo el libro oficial
		select @LegalBookId = id  from GeneralLedger.LegalBook where OfficialBook = 1

		--Id de la cuenta contable que se asigna a la cxc
		declare @DebitAccountId int = (select DebitAccountId from Taxes.TaxesLiquidationConcept where Code = '01')

		--Id del tipo de comprobante contable
		declare @InvoicePropertyJournalVoucherTypeId int = (select top 1 InvoicePropertyJournalVoucherTypeId from Taxes.SettingsTaxes)

		--Id del centro de costo que va al comprobante
		declare @CostCenterId int =  (select top 1 Id from Payroll.CostCenter)

		declare @IDSInvoice table(Id int primary key)
		insert into Taxes.TaxesInvoice(Validity, InvoiceNumber, TaxesConsecutiveBillingId, TaxesPropertyId, ThirdPartyId, Appraisal,TaxValue, State, CreationUser, CreationDate) output inserted.Id into @IDSInvoice(Id)
		select @Year, cast(Id as varchar(20)), @TaxesConsecutiveBillingId, TaxesPropertyId, ThirdPartyId, Appraisal, ValueOwner, 1, @CodeUser, [Common].[GETDATE]()
		from @TaxesLiquidationDetail

		update td set td.TaxesInvoiceId = i.Id
		from Taxes.TaxesLiquidationDetail td
		inner join Taxes.TaxesInvoice i on i.TaxesPropertyId = td.TaxesPropertyId and i.ThirdPartyId = td.ThirdPartyId and i.Validity = @Year

		update td set td.TaxesInvoiceId = i.Id
		from @TaxesLiquidationDetail td
		inner join Taxes.TaxesInvoice i on i.TaxesPropertyId = td.TaxesPropertyId and i.ThirdPartyId = td.ThirdPartyId and i.Validity = @Year

		--Se insertan los detalles de la factura con los registros que estan en la tabla TaxesLiquidationDetailConcept
		insert into Taxes.TaxesInvoiceDetail(TaxesInvoiceId, LiquidationConceptId, PercentageConcept, BaseValue, Value)
		select d.TaxesInvoiceId, lc.TaxesLiquidationConceptId, lc.PercentageConcept, lc.BaseValue, lc.Value 
		from Taxes.TaxesLiquidationDetailConcept lc
		inner join @TaxesLiquidationDetail d on d.Id = lc.TaxesLiquidationDetailId
		
		declare @AccountingMovementId int = (select top 1 Id from GeneralLedger.AccountingMovement)
		declare @CodeAccounting bigint
		select @CodeAccounting = Consecutive from GeneralLedger.JournalVoucherTypes where Id = @InvoicePropertyJournalVoucherTypeId
		update GeneralLedger.JournalVoucherTypes set Consecutive = @CodeAccounting + 1 where Id = @InvoicePropertyJournalVoucherTypeId
		
		--inserto la cabecera del comprobante
		insert into GeneralLedger.JournalVouchers([AccountingMovementId], [Consecutive], [LegalBookId], [IdJournalVoucher], [VoucherDate], [Imported], [Status], 
		[Detail], [EntityCode], [EntityId], [EntityName], [IsClosedYear], [CreationUser], [CreationDate], [ModificationUser], [ModificationDate], [ConfirmationUser], [ConfirmationDate]) 
		values(@AccountingMovementId, @CodeAccounting, @LegalBookId, @InvoicePropertyJournalVoucherTypeId, [Common].[GETDATE](), 1 , 2, 'Creado desde liquidación de impuestos', @CodeAccounting, 0, 'TaxesLiquidation', 0, 
		@CodeUser, [Common].[GETDATE](), @CodeUser, [Common].[GETDATE](), @CodeUser, [Common].[GETDATE]())

		set @JournalVoucherId = SCOPE_IDENTITY()
		
		--Se recorren los detalles de la liquidación
		Declare InfoItem Cursor For 
		select Id, TaxesLiquidationId, TaxesPropertyId, ThirdPartyId, Appraisal, ValueOwner, TaxesInvoiceId from @TaxesLiquidationDetail
		
		Open InfoItem
		Fetch Next From InfoItem Into @Id, @TaxesLiquidationId, @TaxesPropertyId, @ThirdPartyId, @Appraisal, @TaxValue, @TaxesInvoiceId
		While @@fetch_status = 0
		Begin
			
			--Se aumenta el consecutivo
			set @Consecutive = @Consecutive + 1

			update Taxes.TaxesInvoice set InvoiceNumber = CONCAT(@Prefix, '-', CAST(@Consecutive as varchar)) where Id = @TaxesInvoiceId
			
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @Id, @TaxesLiquidationId, @TaxesPropertyId, @ThirdPartyId, @Appraisal, @TaxValue, @TaxesInvoiceId
		End
		Close InfoItem
		Deallocate InfoItem

		
			update Taxes.TaxesConsecutiveBilling set Consecutive = @Consecutive  where Id = @ConsecutiveId


			declare @IDSCxC table(Id int primary key)

			--Se genera la cabecera de la cxc AccountReceivable
			insert into Portfolio.AccountReceivable(Code, OperatingUnitId, AccountReceivableType, ThirdPartyId, InvoiceNumber, AccountReceivableDate,Term, ExpiredDate, Observations, PortfolioStatus, OpeningBalance, PaymentAgreement, RegistrationAdjusted, NumberShares,Value, Balance, Status, AccountWithoutRadicateId, AccountRadicateId, AccountObjectionRemediedId, CreationUser, CreationDate) output inserted.Id into @IDSCxC(Id)
			select i.InvoiceNumber, 14, 8, i.ThirdPartyId, i.InvoiceNumber, [Common].[GETDATE](), 30, DATEADD(DAY,30,[Common].[GETDATE]()), 'Cuenta por cobrar generada desde liquidación de impuestos', 1, 0, 0, 0, 1, i.TaxValue, i.TaxValue, 2, null, null, null,@CodeUser, [Common].[GETDATE]()
			from Taxes.TaxesInvoice i where i.Id in (select Id from @IDSInvoice)
			
			--Se generan los detalles de la cxc AccountReceivableAccounting con la tabla de TaxesInvoiceDetail
			insert into Portfolio.AccountReceivableAccounting(AccountReceivableId, MainAccountId, ThirdPartyId, Value, Balance)
			select ar.Id, @DebitAccountId, ar.ThirdPartyId, ar.Value, ar.Value
			from Portfolio.AccountReceivable ar
			where Id in (select Id from @IDSCxC)

			--Se generan el detalle de la cxc AccountReceivableShare
			insert into Portfolio.AccountReceivableShare(AccountReceivableId, Number, ExpiredDate, Value, Balance, DebitValue, CreditValue, 
			TransferValue, PaymentValue, CrossingValue)
			select ar.Id, 1, [Common].[GETDATE](), ar.Value, ar.Value, 0, 0, 0, 0, 0
			from Portfolio.AccountReceivable ar
			where Id in (select Id from @IDSCxC)

			--Se generan el detalle de la cxc AccountReceivablePromptPayment con la tabla DiscountPropertyTaxes
			declare @date as date = cast(getdate() as date)
			insert into Portfolio.AccountReceivablePromptPayment(AccountReceivableId, DeadLine, PercentageDiscount, MainAccountId)
			select Id, DeadLine, Percentage, MainAccountId
			from (select ids.Id, dpt.DeadLine, dpt.Percentage, dpt.MainAccountId 
			from Taxes.DiscountPropertyTaxes dpt
			full join @IDSCxC ids on 1 = 1
			where dpt.DeadLine >= @date) as a
			where a.Id is not null

			--se insertan los detalles por cada TaxesInvoiceDetail para Debito
			insert into GeneralLedger.JournalVoucherDetails([IdAccounting], [IdMainAccount], [IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue], [Detail], 
			[IdRetention], [RetentionRate], [BaseValue], [BillingValue])
			select @JournalVoucherId, 
			tlc.DebitAccountId,
			i.ThirdPartyId, 
			@CostCenterId,
			sum(tid.Value), 
			0, 
			'Detalle generado desde liquidación de impuestos',
			null, 0, 0, 0
			from Taxes.TaxesInvoiceDetail tid
			inner join Taxes.TaxesInvoice i on i.Id = tid.TaxesInvoiceId
			inner join Taxes.TaxesLiquidationConcept tlc on tlc.Id = tid.LiquidationConceptId
			where tid.TaxesInvoiceId in (select Id from @IDSInvoice)
			group by tlc.DebitAccountId, i.ThirdPartyId

			--se insertan los detalles por cada TaxesInvoiceDetail para Credito
			insert into GeneralLedger.JournalVoucherDetails([IdAccounting], [IdMainAccount], [IdThirdParty], [IdCostCenter], [DebitValue], [CreditValue], [Detail], 
			[IdRetention], [RetentionRate], [BaseValue], [BillingValue]) 
			select @JournalVoucherId, 
			tlc.CreditAccountId,
			i.ThirdPartyId, 
			@CostCenterId,
			0, 
			sum(tid.Value), 
			'Detalle generado desde liquidación de impuestos',
			null, 0, 0, 0
			from Taxes.TaxesInvoiceDetail tid
			inner join Taxes.TaxesInvoice i on i.Id = tid.TaxesInvoiceId
			inner join Taxes.TaxesLiquidationConcept tlc on tlc.Id = tid.LiquidationConceptId
			where tid.TaxesInvoiceId in (select Id from @IDSInvoice) group by tlc.CreditAccountId, i.ThirdPartyId
		
			set @MessagesJournalVoucher = 'tipo ' + @JournalVoucherType + ' con consecutivos: ' + CAST(@CodeAccounting as varchar)

			select 1 as State , 'Se confirmó correctamente' Message, @Ids as MessagesIds, 
			@MessagesInvoiceNumber as MessagesInvoiceNumber, @MessagesAccountReceivableCode as MessagesAccountReceivableCode,
			@MessagesJournalVoucher as MessagesJournalVoucher

	END TRY
    BEGIN CATCH
        select 0 as State , ERROR_MESSAGE() + ', Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) Message, '' as MessagesIds, 
		'' as MessagesInvoiceNumber, '' as MessagesAccountReceivableCode, '' as MessagesJournalVoucher
    END CATCH;
END