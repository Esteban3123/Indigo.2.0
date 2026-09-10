

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma y formaliza la liquidación de impuestos prediales para un año fiscal determinado, convirtiendo los detalles de liquidación seleccionados (identificados por sus IDs) en facturas tributarias oficiales con su consecutivo de facturación, numeración y prefijo correspondientes. Durante el proceso genera las facturas de impuesto predial en la tabla TaxesInvoice, registra el detalle de conceptos liquidados (TaxesInvoiceDetail), crea el comprobante contable en el libro oficial del módulo de contabilidad (GeneralLedger) con sus movimientos de débito y crédito según la cuenta contable del concepto de liquidación y el tipo de comprobante configurado en los parámetros del módulo de impuestos (SettingsTaxes), y asocia el centro de costo de nómina al asiento generado. Retorna los números de factura emitidos, los códigos de cuentas por cobrar creadas y los consecutivos contables producidos en la confirmación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmTaxesLiquidation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmTaxesLiquidation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma una liquidación de impuestos generando facturas tributarias, sus detalles, la cuenta por cobrar asociada con sus accesorios y el comprobante contable de débito y crédito en el libro mayor.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en Taxes.SettingsTaxes; si no existe se aborta el proceso retornando State=0.; El parámetro de Ids debe ser una lista separada por comas convertible a numeric(18,0) procesable por dbo.Split.; Debe existir al menos un registro en Taxes.TaxesConsecutiveBilling para obtener prefijo y consecutivo de facturación.; Debe existir un libro contable marcado como OfficialBook=1 en GeneralLedger.LegalBook.; Debe existir el concepto de liquidación con Code=''01'' en Taxes.TaxesLiquidationConcept para obtener la cuenta débito de la CxC.; Debe existir al menos un centro de costo en Payroll.CostCenter y al menos un movimiento contable en GeneralLedger.AccountingMovement.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Taxes.TaxesInvoice: Por cada detalle de liquidación seleccionado se inserta una factura con Validity=@Year, State=1 e InvoiceNumber inicial igual al Id del detalle.; [UPDATE] Taxes.TaxesLiquidationDetail: Se asigna TaxesInvoiceId al detalle de liquidación cruzando por TaxesPropertyId, ThirdPartyId y Validity=@Year contra la factura recién creada.; [INSERT] Taxes.TaxesInvoiceDetail: Se replican los conceptos de Taxes.TaxesLiquidationDetailConcept como detalle de la factura para los detalles seleccionados.; [UPDATE] GeneralLedger.JournalVoucherTypes: Se incrementa en 1 el Consecutive del tipo de comprobante configurado en SettingsTaxes (InvoicePropertyJournalVoucherTypeId).; [INSERT] GeneralLedger.JournalVouchers: Se crea la cabecera del comprobante contable con Imported=1, Status=2, EntityName=''TaxesLiquidation'', detalle ''Creado desde liquidación de impuestos'' y fechas de creación/modificación/confirmación = [Common].[GETDATE]().; [UPDATE] Taxes.TaxesInvoice: Por cada fila del cursor se incrementa el consecutivo y se actualiza InvoiceNumber con formato ''<Prefix>-<Consecutivo>''.; [UPDATE] Taxes.TaxesConsecutiveBilling: Se persiste el último consecutivo utilizado tras emitir las facturas.; [INSERT] Portfolio.AccountReceivable: Por cada factura emitida se crea una CxC con OperatingUnitId=14, AccountReceivableType=8, Term=30, ExpiredDate=GETDATE+30 días, PortfolioStatus=1, Status=2, Value=Balance=TaxValue.; [INSERT] Portfolio.AccountReceivableAccounting: Por cada CxC creada se inserta una contabilización con MainAccountId = DebitAccountId del concepto Code=''01''.; [INSERT] Portfolio.AccountReceivableShare: Por cada CxC se crea una única cuota Number=1 con ExpiredDate=GETDATE() y Value=Balance=ar.Value, valores de débito/crédito/transfer/payment/crossing en 0.; [INSERT] Portfolio.AccountReceivablePromptPayment: Se generan registros de pronto pago únicamente para los descuentos de Taxes.DiscountPropertyTaxes con DeadLine >= fecha actual.; [INSERT] GeneralLedger.JournalVoucherDetails: Se inserta un detalle de débito por cada (DebitAccountId, ThirdPartyId) con DebitValue=SUM(Value) de los detalles de factura y CreditValue=0.; [INSERT] GeneralLedger.JournalVoucherDetails: Se inserta un detalle de crédito por cada (CreditAccountId, ThirdPartyId) con CreditValue=SUM(Value) de los detalles de factura y DebitValue=0.; [RETURN_RESULT] RESULT: Devuelve State=1 y mensaje ''Se confirmó correctamente'' al final exitoso, o State=0 con ERROR_MESSAGE y línea en caso de excepción capturada por CATCH.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si (select count(*) from Taxes.SettingsTaxes) = 0 → Retorna State=0 con mensaje ''No existe parámetros de liquidación de impuestos'' y termina sin generar facturas ni comprobantes. else Continúa el proceso de generación de facturas, CxC y comprobante contable.; si dpt.DeadLine >= fecha actual al insertar AccountReceivablePromptPayment → Se incluye el descuento de pronto pago en la CxC. else El descuento se omite.; si Bloque BEGIN CATCH activado por excepción → Devuelve State=0 con ERROR_MESSAGE() y número de línea en lugar de los identificadores generados.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTaxesLiquidation';
-- GO
