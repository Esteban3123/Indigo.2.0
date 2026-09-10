-- =============================================
-- Author:		Carlos Cordoba
-- Create date: 29-09-2016
-- Description:	Procedimiento para generar comprobantes de egreso
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SaveVoucherTransaction] 
	@VoucherTransactionXml AS XML,
	@User VARCHAR(20)
AS
BEGIN

	SET NOCOUNT ON;

	--variables para la cabecera del comprobante
	declare @IdVoucherTransaction int,@CodeVoucherTransaction varchar(20),@IdThirdParty int,@IdMainAccount int,@IdCostCenter int,@VoucherClass int,@ExpenseType tinyint,@Detail varchar(500)
	declare @DocumentDate datetime,@IdCashRegister int,@IdEntityBankAccount int,@Value decimal(18, 5),@PaymentMethod tinyint,@NoteNumber varchar(50)
	declare @IdChecks int,@CheckNumber bigint,@TransactionDate datetime,@TaxByMil bit,@TaxByMilValue decimal(18, 2),@CashRegisterExpense bit
	declare @RefundCashRegisterExpense bit,@SchedulePaymentId int,@BeneficiaryIdentification varchar(20),@Beneficiary varchar(100),@TransactionRelationship bit
	declare @CheckReconciled bit,@IdPaymentOrder int,@Printed bit,@RTEValue decimal(18, 2),@IVAValue decimal(18, 2),@ICAValue decimal(18, 2),@OtherValue decimal(18, 2)
	declare @BankAccountNumber varchar(50),@BankName varchar(100),@DetailsInterfaceBudget varchar(100),@IdUnitOperative int,@IdRefund int,@Status tinyint
	declare @Prefix VARCHAR(50)= '',@UpdateFields bit, @HandlesDocumentSupport bit,@SupplierBankAccountId int, @CurrencyIdHeader  int
	
	--tablas temporales
	declare @VoucherTransactionDetails TABLE(
	ChangeTracker VARCHAR(30),
	[IdTmp] [INT] NOT NULL,
	[Id] [int] NOT NULL,
	[IdVoucherTransaction] [int] NOT NULL,
	[IdEntityBankAccount] [int] NULL,
	[CashRegisterId] [int] NULL,
	[IdThirdParty] [int] NULL,
	[IdExpenseConcept] [int] NULL,
	[IdMainAccount] [int] NOT NULL,
	[Nature] [tinyint] NOT NULL,
	[IdCostCenter] [int] NULL,
	[Value] [decimal](18, 5) NOT NULL,
	[IdRetentionConcept] [int] NULL,
	[BaseValue] [decimal](18, 2) NULL,
	[BillingValue] [decimal](18, 5) NULL,
	[PercentRetention] [decimal](5, 3) NULL,
	[Detail] [varchar](max) NULL,
	[Observation] [varchar](max) NULL
	,[IdCashFlowConcept] [int] NULL,
	[discountableIVA]  bit NULL,
	[TaxRegistration]  tinyint NULL,
	[EconomicActivityId] int NULL,
	[IdGeneralLedgerIVA] int NULL,
	[ValueIVA] [decimal](18, 2) NULL,
	[TotalConcept] [decimal](18, 5),
	[SupplierBankAccountId] int NULL) 

	declare @VoucherTransactionAdvance  TABLE (
	ChangeTracker VARCHAR(30),
	[Id] [int] NOT NULL,
	[IdVoucherTransactionDTmp] [int] NOT NULL,
	[IdVoucherTransactionD] [int] NOT NULL,
	[PortfolioAdvanceId] [int] NOT NULL,
	[Value] [numeric](18, 2) NOT NULL,
	[Percentage] [numeric](5, 2) NOT NULL)
	
	declare @DischargeBill TABLE 
	(
		ChangeTracker VARCHAR(30),
		[IdTmp] [INT] NOT NULL,
		[Id] [int] NOT NULL,
		[IdVoucherTransactionDTmp] [int] NOT NULL,
		[IdVoucherTransactionD] [int] NOT NULL,
		[IdAccountPayable] [int] NOT NULL,
		[IdAccountPayableShare] [int] NOT NULL,
		[AdvancedValue] [decimal](18, 2) NOT NULL,
		[AdvancePercent] [decimal](5, 2) NOT NULL,
		[IdPaymentConcept] [int] NOT NULL,
		[BaseValueDiscount] [decimal](18, 2) NOT NULL,
		[DiscountPercent] [decimal](5, 2) NOT NULL,
		[PaymentOrderValue] [decimal] NOT NULL,
		[ValueInCurrencyHeader] [Decimal](18,5) NOT NULL,
		[TRMValue][numeric](20,5),
		[ValueDiscountInCurrencyHeader] [Decimal](18,5) NOT NULL
	)

	declare @DischargeBillBudget TABLE 
	(
		ChangeTracker VARCHAR(30),
		[Id] [int] NOT NULL,
		[IdDischargeBillTmp] [int] NOT NULL,
		[DischargeBillId] [int] NOT NULL,
		[ObligationDetailId] [int] NOT NULL,
		[Value] [decimal] NOT NULL
	)

	DECLARE @CodeMessageResultPaymentOrder INT,
			@MessageResultPaymentOrder VARCHAR(MAX)

	declare @TreasuryAdvances table(
									ChangeTracker VARCHAR(30),
									[Id] [int] NOT NULL,
									[IdVoucherTransactionDTmp] [int] NOT NULL,
									[IdVoucherTransactionDetail] [int] NOT NULL,
									[Detail] [varchar](500) NOT NULL,
									[Value] [decimal](18, 2) NOT NULL,
									[CurrencyId] [int],
									[ValueInCurrencyHeader][Decimal](18,5) NOT NULL,
									[TRMValue][numeric](20,5) NOT NULL)

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	--Parametro de companySetting para el registro del IVA
	Declare @TaxRegistration tinyint 

	BEGIN TRY
		--se asignan valores del xml
		select	@IdVoucherTransaction = t.x.value('Id[1]', 'int'),
				@CodeVoucherTransaction = t.x.value('Code[1]','varchar(20)'),
				@IdThirdParty = t.x.value('IdThirdParty[1]','int'),
				@IdMainAccount = t.x.value('IdMainAccount[1]','int'),
				@IdCostCenter = t.x.value('IdCostCenter[1]','int'),
				@VoucherClass = t.x.value('VoucherClass[1]','int'),
				@ExpenseType = t.x.value('ExpenseType[1]','tinyint'),
				@Detail = dbo.DecodeXmlToText(t.x.value('Detail[1]','varchar(max)')),
				@DocumentDate = convert(datetime, t.x.value('DocumentDate[1]', 'nvarchar(19)'), 103),
				--@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
				@IdCashRegister = t.x.value('IdCashRegister[1]','int'),
				@IdEntityBankAccount = t.x.value('IdEntityBankAccount[1]','int'),
				@Value = t.x.value('Value[1]','decimal(18, 5)'),
				@PaymentMethod  = t.x.value('PaymentMethod [1]','tinyint'),
				@NoteNumber  = t.x.value('NoteNumber [1]','varchar(50)'),
				@IdChecks = t.x.value('IdChecks[1]','int'),
				@CheckNumber = t.x.value('CheckNumber[1]','bigint'),
				@TransactionDate = convert(datetime, t.x.value('TransactionDate[1]', 'nvarchar(19)'), 103),
				--@TransactionDate = t.x.value('TransactionDate[1]','datetime'),
				@TaxByMil = t.x.value('TaxByMil[1]','bit'),
				@TaxByMilValue = t.x.value('TaxByMilValue[1]','decimal(18, 2)'),
				@CashRegisterExpense = t.x.value('CashRegisterExpense[1]','bit'),
				@RefundCashRegisterExpense = t.x.value('RefundCashRegisterExpense[1]','bit'),
				@SchedulePaymentId = t.x.value('SchedulePaymentId[1]','int'),
				@BeneficiaryIdentification = t.x.value('BeneficiaryIdentification[1]','varchar(20)'),
				@Beneficiary = dbo.DecodeXmlToText(t.x.value('Beneficiary[1]','varchar(100)')),
				@TransactionRelationship = t.x.value('TransactionRelationship[1]','bit'),
				@CheckReconciled = t.x.value('CheckReconciled[1]','bit'),
				@IdPaymentOrder = t.x.value('IdPaymentOrder[1]','int'),
				@Printed = t.x.value('Printed[1]','bit'),
				@RTEValue = t.x.value('RTEValue[1]','decimal(18, 2)'),
				@IVAValue = t.x.value('IVAValue[1]','decimal(18, 2)'),
				@ICAValue = t.x.value('ICAValue[1]','decimal(18, 2)'),
				@OtherValue = t.x.value('OtherValue[1]','decimal(18, 2)'),
				@BankAccountNumber = t.x.value('BankAccountNumber[1]','varchar(50)'),
				@BankName = t.x.value('BankName[1]','varchar(100)'),
				@DetailsInterfaceBudget = t.x.value('DetailsInterfaceBudget[1]','varchar(100)'),
				@IdUnitOperative = t.x.value('IdUnitOperative[1]','int'),
				@IdRefund = t.x.value('IdRefund[1]','int'),
				@Status = t.x.value('Status[1]','tinyint'),
				@UpdateFields = IIF(t.x.value('UpdateFields[1]','varchar(250)') = 'True', 1, 0),
				@HandlesDocumentSupport = ISNULL(t.x.value('HandlesDocumentSupport[1]','BIT'),0),
				@SupplierBankAccountId = t.x.value('SupplierBankAccountId[1]', 'int')	,			
				@CurrencyIdHeader = t.x.value('CurrencyId[1]','int')
		from @VoucherTransactionXml.nodes('/VoucherTransaction') t(x);

		if @ExpenseType = 1 begin --- Si es banco
			set @Prefix = (select Prefix from Treasury.EntityBankAccounts where Id = @IdEntityBankAccount)
		end
		else begin
			set @Prefix = (select Prefix from Treasury.CashRegisters where Id = @IdCashRegister)
		end

		insert into @VoucherTransactionDetails 
			SELECT t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
			t.x.value('IdTmp[1]', 'int') AS IdTmp,
			t.x.value('Id[1]', 'int') AS Id,
			t.x.value('IdVoucherTransaction[1]', 'int') AS IdVoucherTransaction,
			t.x.value('IdEntityBankAccount[1]', 'int') AS IdEntityBankAccount,
			t.x.value('CashRegisterId[1]', 'int') AS CashRegisterId,
			t.x.value('IdThirdParty[1]', 'int') AS IdThirdParty,
			t.x.value('IdExpenseConcept[1]', 'int') AS IdExpenseConcept,
			t.x.value('IdMainAccount[1]', 'int') AS IdMainAccount,
			t.x.value('Nature[1]', 'tinyint') AS Nature,
			t.x.value('IdCostCenter[1]', 'int') AS IdCostCenter,
			t.x.value('Value[1]', 'decimal(18,5)') AS Value,
			t.x.value('IdRetentionConcept[1]', 'int') AS IdRetentionConcept,
			t.x.value('BaseValue[1]', 'decimal(18,2)') AS BaseValue,
			t.x.value('BillingValue[1]', 'decimal(18,2)') AS BillingValue,
			t.x.value('PercentRetention[1]', 'decimal(5, 3)') AS PercentRetention,
			t.x.value('Detail[1]', 'varchar(max)') AS Detail,
			dbo.DecodeXmlToText(t.x.value('Observation[1]','varchar(max)')) AS Observation
			,t.x.value('IdCashFlowConcept[1]', 'int') AS IdCashFlowConcept,
			t.x.value('discountableIVA[1]', 'varchar(max)') AS discountableIVA,
			t.x.value('TaxRegistration[1]', 'varchar(max)') AS TaxRegistration,
			t.x.value('EconomicActivityId[1]', 'varchar(max)') AS EconomicActivityId,
			iif(t.x.value('IdGeneralLedgerIVA[1]', 'int')=0,null,t.x.value('IdGeneralLedgerIVA[1]', 'int')) AS IdGeneralLedgerIVA,
			t.x.value('ValueIVA[1]', 'varchar(max)') AS ValueIVA,
			t.x.value('TotalConcept[1]', 'decimal(18,5)') AS TotalConcept,
			t.x.value('SupplierBankAccountId[1]', 'int') AS SupplierBankAccountId
			from @VoucherTransactionXml.nodes('/VoucherTransaction/VoucherTransactionDetails') t(x);
			
		--Actualizo el concepto de flujo de caja
		UPDATE vtd
			SET vtd.IdCashFlowConcept = ec.IdCashFlowConcept
		FROM @VoucherTransactionDetails vtd
		JOIN Treasury.ExpenseConcepts ec ON vtd.IdExpenseConcept = ec.Id
		WHERE ISNULL(vtd.IdCashFlowConcept, 0) = 0

		insert into @VoucherTransactionAdvance 
		select t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
		t.x.value('Id[1]', 'int') AS Id,
		t.x.value('IdVoucherTransactionDTmp[1]', 'int') AS IdVoucherTransactionDTmp,
		t.x.value('IdVoucherTransactionD[1]', 'int') AS IdVoucherTransactionD,
		t.x.value('PortfolioAdvanceId[1]', 'int') AS PortfolioAdvanceId,
		t.x.value('Value[1]', 'numeric(18,2)') AS Value,
		t.x.value('Percentage[1]', 'numeric(18,2)') AS Percentage
		from @VoucherTransactionXml.nodes('/VoucherTransaction/VoucherTransactionDetails/VoucherTransactionAdvance') t(x);

		insert into @DischargeBill 
			select	t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
					t.x.value('IdTmp[1]', 'int') AS IdTmp,
					t.x.value('Id[1]', 'int') AS Id,
					t.x.value('IdVoucherTransactionDTmp[1]', 'int') AS IdVoucherTransactionDTmp,
					t.x.value('IdVoucherTransactionD[1]', 'int') AS IdVoucherTransactionD,
					t.x.value('IdAccountPayable[1]', 'int') AS IdAccountPayable,
					t.x.value('IdAccountPayableShare[1]', 'int') AS IdAccountPayableShare,
					t.x.value('AdvancedValue[1]', 'decimal(18,2)') AS AdvancedValue,
					t.x.value('AdvancePercent[1]', 'decimal(5,2)') AS AdvancePercent,
					t.x.value('IdPaymentConcept[1]', 'int') AS IdPaymentConcept,
					t.x.value('BaseValueDiscount[1]', 'decimal(18,2)') AS BaseValueDiscount,
					t.x.value('DiscountPercent[1]', 'decimal(5,2)') AS DiscountPercent,
					t.x.value('PaymentOrderValue[1]', 'decimal') AS PaymentOrderValue,
					t.x.value('ValueInCurrencyHeader[1]', 'decimal(18,5)') AS ValueInCurrencyHeader,
					t.x.value('TRMValue[1]', 'numeric(20,5)') AS ValueInCurrencyHeader,
					t.x.value('ValueDiscountInCurrencyHeader[1]', 'decimal(18,5)') AS ValueDiscountInCurrencyHeader
			from @VoucherTransactionXml.nodes('/VoucherTransaction/VoucherTransactionDetails/DischargeBill') t(x);
			
		insert into @DischargeBillBudget 
			select	t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
					t.x.value('Id[1]', 'int') AS Id,
					t.x.value('IdDischargeBillTmp[1]', 'int') AS IdDischargeBillTmp,
					t.x.value('DischargeBillId[1]', 'int') AS DischargeBillId,
					t.x.value('ObligationDetailId[1]', 'int') AS ObligationDetailId,
					t.x.value('Value[1]', 'decimal') AS Value
			from @VoucherTransactionXml.nodes('/VoucherTransaction/VoucherTransactionDetails/DischargeBill/DischargeBillBudget') t(x);
			
		UPDATE dbb
			SET dbb.ChangeTracker = 'Modified'
		FROM @DischargeBillBudget dbb
		WHERE dbb.ChangeTracker = 'Added' AND dbb.Id > 0
		
		insert into @TreasuryAdvances 
		select t.x.value('ChangeTracker[1]', 'varchar(30)') AS ChangeTracker,
		t.x.value('Id[1]', 'int') AS Id,
		t.x.value('IdVoucherTransactionDTmp[1]', 'int') AS IdVoucherTransactionDTmp,
		t.x.value('IdVoucherTransactionDetail[1]', 'int') AS IdVoucherTransactionDetail,
		t.x.value('Detail[1]', 'varchar(500)') AS Detail,
		t.x.value('Value[1]', 'decimal(18,2)') AS Value,
		t.x.value('CurrencyId[1]', 'int') as CurrencyId,
		t.x.value('ValueInCurrencyHeader[1]', 'decimal(18,5)') as ValueInCurrencyHeader,
		t.x.value('TRMValue[1]', 'numeric(20,5)') as TRMValue
		from @VoucherTransactionXml.nodes('/VoucherTransaction/VoucherTransactionDetails/TreasuryAdvances') t(x);

		DECLARE @ConfirmationUser AS VARCHAR(20)= CASE WHEN @Status <> 2 THEN NULL ELSE @User END;
        DECLARE @ConfirmationDate AS DATETIME= CASE WHEN @Status <> 2 THEN NULL ELSE [Common].[GETDATE]() END;
        DECLARE @AnnulmentUser AS VARCHAR(20)= CASE WHEN @Status <> 3 THEN NULL ELSE @User END;
        DECLARE @AnnulmentDate AS DATETIME= CASE WHEN @Status <> 3 THEN NULL ELSE [Common].[GETDATE]() END;

		/* se consulta el parametro de registro de IVA*/
		SELECT @TaxRegistration = cs.TaxRegistration
		FROM GeneralLedger.CompanySettings cs WITH(NOLOCK)

		--se valida el comprobante de egreso
		if (@status <3) 
		begin
			if (@UpdateFields = 1)
			begin		  
				UPDATE vt
					SET vt.NoteNumber = @NoteNumber,
						vt.Detail = @Detail,
						vt.ModificationUser = @User,
						vt.ModificationDate = [Common].[GETDATE]()
				FROM Treasury.VoucherTransaction vt
				WHERE vt.Id = @IdVoucherTransaction
				
				SELECT '0' AS CodeMessage, 'Se actualizaron los campos del comprobante de egreso' AS Message, @IdVoucherTransaction as IdVoucherTransaction,cast(1 as tinyint) as [Status];
				RETURN		
			end
			
			--Tabla para ir almacenando los errores 
			DECLARE @TableErrors as table([message] varchar(300))

			--Se valida que el comprobante si ya fue creado no este confirmado
			IF @IdVoucherTransaction > 0 BEGIN
				IF ISNULL(( SELECT COUNT(Id) FROM Treasury.VoucherTransaction WHERE Id = @IdVoucherTransaction AND [Status] = 2), 0) > 0 BEGIN
					INSERT INTO @TableErrors SELECT 'El comprobante ya se encuentra confirmado' + CHAR(13) + CHAR(10)
				END
			END

			--se valida que exista un libro oficial
			if (select COUNT(*)  from GeneralLedger.LegalBook where OfficialBook = 1) = 0 begin
				insert into @TableErrors select 'No se encontro libro oficial en contabilidad'+ CHAR(13) + CHAR(10)		
			end

			--variable para retornar todos los mensajes de error que tenga la tabla de errores
			DECLARE @ErrorsValidation varchar(max)
			--valido el que el mes este abierto en contabilidad
			if (select COUNT(*) from GeneralLedger.ClosedMonth where [Year] =YEAR(@DocumentDate ) and [Month]  = MONTH(@DocumentDate ) and [Status] =1)=0 begin
				insert into @TableErrors select 'El mes seleccionado en la fecha del comprobante de egreso esta cerrado'+ CHAR(13) + CHAR(10)				
			end
			--se valida que el valor sea mayor a 0
			if @Value <= 0 begin
				insert into @TableErrors select 'El valor de comprobante de egreso debe ser mayor a $0'+ CHAR(13) + CHAR(10)
			end
			--se valida si la cuenta maneja centro de costo
			if @IdMainAccount = 0 begin
				insert into @TableErrors select 'La cuenta contable esta vacia'+ CHAR(13) + CHAR(10)
			end
			else if (select HandlesCostCenter  from GeneralLedger.MainAccounts where Id = @IdMainAccount ) = 1 and @IdCostCenter = 0 begin
				insert into @TableErrors 
				select 'La cuenta contable '+ma.Number + ' - '+ma.Name + ' maneja centro de costo' + CHAR(13) + CHAR(10)
				from GeneralLedger.MainAccounts ma where ma.Id = @IdMainAccount					
			end

			--Se valida que los conceptos seleccionados se encuentren activos
			IF ISNULL((
					SELECT COUNT(*) 
					FROM @VoucherTransactionDetails vtd 
					JOIN Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id
					WHERE vtd.ChangeTracker<>'Deleted' AND ec.Status <> 1
				), 0) > 0 BEGIN
				DECLARE @errorExpensesInactives varchar(MAX)
				SELECT @errorExpensesInactives = stuff((SELECT N';  '+ ec.Code
					FROM @VoucherTransactionDetails vtd 
					JOIN Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id
					WHERE vtd.ChangeTracker<>'Deleted' AND ec.Status <> 1
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				INSERT INTO @TableErrors select 'Los siguientes conceptos se encuentran inactivos: ' + @errorExpensesInactives + CHAR(13) + CHAR(10)
			END

			-- se valida el tipo de egreso
			if @ExpenseType = 0 begin
				insert into @TableErrors select 'Tipo de Ingreso vacio'+ CHAR(13) + CHAR(10)
			end
			else begin
				if @ExpenseType = 1 begin--Cuenta bancaria
					if @IdEntityBankAccount= 0 begin
						insert into @TableErrors select 'Cuenta bancaria vacia'+ CHAR(13) + CHAR(10)
					end
					else begin 
						if @PaymentMethod = 0 begin
							insert into @TableErrors select 'Metodo de pago vacio'+ CHAR(13) + CHAR(10)
						end
						else if @PaymentMethod = 1 begin--cheque
							if @IdChecks = 0 begin 
								insert into @TableErrors select 'Cheque vacio'+ CHAR(13) + CHAR(10)
							end
						end 
						else if @PaymentMethod = 2 begin--nota debito
							if @NoteNumber = '' begin
								insert into @TableErrors select 'Numero de nota vacio'+ CHAR(13) + CHAR(10)
							end
						end
					end
				end
				else if @ExpenseType = 2 or @ExpenseType = 3 begin--Caja menor
					if @IdCashRegister = 0 begin
						insert into @TableErrors select 'Caja vacia'+ CHAR(13) + CHAR(10)
					end
				end 				
			end

			--se valida la unidad operativa
			if @IdUnitOperative = 0 begin
				insert into @TableErrors select 'Unidad operativa vacia'+ CHAR(13) + CHAR(10)
			end
			else if (select COUNT(*) from Treasury.SettingsTreasury where IdOperatingUnit = @IdUnitOperative ) = 0 begin
				insert into @TableErrors select 'No se encontraron parametros para la unidad operativa seleccionada '+ CHAR(13) + CHAR(10)
			end
			--se valida el valor de la tasa por mil
			if @TaxByMil = 1 and @TaxByMilValue = 0 begin
				insert into @TableErrors select 'El valor por mil debe ser mayor a $0'+ CHAR(13) + CHAR(10)
			end
			--se valida el tercero si la clase es de pago
			if @VoucherClass = 1 and @IdThirdParty = 0 begin
				insert into @TableErrors select 'Tercero vacio'+ CHAR(13) + CHAR(10)
			end
			--se valida que existan detalle en el comprobante de egreso
			if (select COUNT(*) from @VoucherTransactionDetails where ChangeTracker<>'Deleted' ) =0 begin
				insert into @TableErrors select 'Se debe agregar minimo un detalle al comprobante'+ CHAR(13) + CHAR(10)
			end

			if  EXISTS(SELECT 1 
						FROM  @VoucherTransactionDetails vtd 
						inner join @DischargeBill db on vtd.IdTmp = db.IdVoucherTransactionDTmp
						INNER join Payments.AccountPayable ap WITH(NOLOCK) on db.IdAccountPayable=ap.Id
						INNER JOIN Common.SuppliersDistributionLines sdl WITH(NOLOCK) on  ap.IdSuppliersDistributionLines =sdl.Id
						inner join Common.DistributionLines dl WITH(NOLOCK) on sdl.IdDistributionLine=dl.Id
						LEFT JOIN Payments.AccountPayableConceptNotes apcn with(NOLOCK) on dl.AccountPayableConceptNotesId=apcn.Id
						WHERE db.BaseValueDiscount >0 and vtd.ChangeTracker <> 'Deleted' and apcn.IdAccount is null) BEGIN
				
					insert into @TableErrors select 'Existen Facturas con Descuento que no tiene parametrizado la cuenta contable de pronto pago'+ CHAR(13) + CHAR(10)

			END

			--si existen errores  se termina el proceso 
			 if(select COUNT(*) from @TableErrors )>0 begin
				SELECT @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors				
				SELECT '999' AS CodeMessage,@ErrorsValidation AS Message,0 IdVoucherTransaction,CAST(3 AS TINYINT) AS [Status];
				RETURN;
			 end

			 --validaciones si se va a confirmar
			 if @Status = 2 begin
				if(@Value + @TaxByMilValue ) <> ((select SUM(ISNULL(TotalConcept, Value)) from @VoucherTransactionDetails where Nature= 1 and ChangeTracker<>'Deleted') - (select SUM(ISNULL(TotalConcept, Value)) from @VoucherTransactionDetails where Nature= 2 and ChangeTracker<>'Deleted')) begin
					insert into @TableErrors select 'El valor del comprobante no coincide con la suma de los valores de los conceptos'+ CHAR(13) + CHAR(10)
				end

				if @IdEntityBankAccount>0 begin 
					if(select CurrentBalance + ISNULL(Quota, 0) from Treasury.EntityBankAccounts where Id = @IdEntityBankAccount ) < @Value begin
						insert into @TableErrors select 'La cuenta bancaria no tiene saldo suficiente ni cupo de sobregiro para esta operación'+ CHAR(13) + CHAR(10)
					end
				end
				else begin
					if(select CurrentBalance  from Treasury.CashRegisters  where Id = @IdCashRegister ) < @Value begin
						insert into @TableErrors select 'La caja no tiene saldo suficiente para esta operación'+ CHAR(13) + CHAR(10)
					end
					if (select Type  from Treasury.CashRegisters  where Id = @IdCashRegister ) = 1 and (select AmountMax  from Treasury.CashRegisters  where Id = @IdCashRegister ) < @Value begin
						insert into @TableErrors select 'El valor a pagar no debe ser mayor a la cuantía máxima de la caja '+ CHAR(13) + CHAR(10)
					end
				end
				--validamos el movimiento del comprobante
				if @VoucherClass = 1 begin--pagos
					--se hacen validaciones dependiendo del comportamiento del concepto
					--se valida que los terceros esten creados como proveedores para los conceptos con comportamiento 3 pagos/anticipos de facturas
					insert into @TableErrors
					select 'El tercero '+tp.Nit + ' - ' + tp.Name + ' no se encuentra creado como proveedor'+ CHAR(13) + CHAR(10) from @VoucherTransactionDetails vtd 
					inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id
					inner join Common.ThirdParty tp on vtd.IdThirdParty = tp.id
					where vtd.ChangeTracker<> 'Deleted' and ec.Behavior=3 and vtd.IdThirdParty not in(select IdThirdParty from Common.Supplier)

					--SOLUCION TEMPORAL IDENTIFICACION ANTICIPOS (BUG 8529 - Antiguo TFS)
					IF ISNULL((SELECT COUNT(*) FROM @DischargeBill), 0) > 0 
					BEGIN
						--Se verifica que, si se esta realizando el pago de una factura, el concepto de egreso sea de comportamiento 2 (Caja Menor) o 3 (Pago/Anticipo de Facturas CxP)
						IF EXISTS
						(
							SELECT 1
							FROM Treasury.ExpenseConcepts ec
							JOIN @VoucherTransactionDetails vtd ON ec.Id = vtd.IdExpenseConcept
							JOIN @DischargeBill db ON vtd.IdTmp = db.IdVoucherTransactionDTmp
							WHERE vtd.ChangeTracker <> 'Deleted' AND db.ChangeTracker <>'Deleted' AND ec.Behavior NOT IN (2, 3)
						)
						BEGIN
							insert into @TableErrors
								SELECT DISTINCT 'El concepto de egreso ' + ec.Code + ' - ' + ec.Description + ' usado para el pago de facturas no tiene un comportamiento válido' + CHAR(13) + CHAR(10) 
								FROM Treasury.ExpenseConcepts ec
								JOIN @VoucherTransactionDetails vtd ON ec.Id = vtd.IdExpenseConcept
								JOIN @DischargeBill db ON vtd.IdTmp = db.IdVoucherTransactionDTmp
								WHERE vtd.ChangeTracker <> 'Deleted' AND db.ChangeTracker <>'Deleted' AND ec.Behavior NOT IN (2, 3)
						END

						--se valida el tercero y la cuenta contable de las facturas
						--Caja Menor - Pago/Anticipo de Facturas CxP
						if(select COUNT(*) from @VoucherTransactionDetails vtd 
						inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id
						inner join Payments.AccountPayable ap on ap.IdAccount = vtd.IdMainAccount
						inner join Payments.AccountPayableShares aps on ap.id = aps.IdAccountPayable
						inner join Common.Supplier s on ap.IdSupplier = s.Id and s.IdThirdParty = vtd.IdThirdParty					
						where ap.[Status] = 2 and aps.Balance>0 and ec.Behavior in(2,3) and vtd.ChangeTracker<>'Deleted') = 0 begin

							insert into @TableErrors
								select 'No hay facturas asociadas al tercero ' + t.Nit + ' - ' + t.name + ' y cuenta contable ' + ma.Number + ' - ' + ma.Name + CHAR(13) + CHAR(10) 
								from @VoucherTransactionDetails vtd 
									inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id
									inner join common.ThirdParty t on vtd.IdThirdParty = t.Id
									inner join GeneralLedger.MainAccounts ma on vtd.IdMainAccount = ma.Id
								where 
									vtd.ChangeTracker <> 'Deleted' AND ec.Behavior in(2,3)
									AND NOT EXISTS (
										SELECT ap.Id
										FROM Payments.AccountPayable ap
										inner join Payments.AccountPayableShares aps on ap.id = aps.IdAccountPayable
										inner join Common.Supplier s on ap.IdSupplier = s.Id
										WHERE 
											ap.IdAccount = vtd.IdMainAccount
											AND s.IdThirdParty = vtd.IdThirdParty
											AND ap.[Status] = 2 
											AND aps.Balance > 0
									)
						end
					END
										
					--Pago/Anticipo de Facturas CxP
					IF EXISTS 
					(
						SELECT 1
						FROM
						(
							select db.IdAccountPayableShare, SUM(db.AdvancedValue) AdvancedValue
							from @VoucherTransactionDetails vtd 
							inner join @DischargeBill  db on vtd.IdTmp = db.IdVoucherTransactionDTmp 
							where vtd.ChangeTracker<>'Deleted' and db.ChangeTracker <>'Deleted'
							GROUP BY db.IdAccountPayableShare
						) db
						JOIN Payments.AccountPayableShares aps on db.IdAccountPayableShare = aps.Id 
						WHERE db.AdvancedValue > aps.Balance
					) begin						
						insert into @TableErrors
						select 'La cuota '+ CAST(aps.Share as varchar(50))  +' de la factura ' + ap.code + ' tiene saldo inferior al valor a pagar'+ CHAR(13) + CHAR(10) 
						FROM
						(
							select db.IdAccountPayableShare, SUM(db.AdvancedValue) AdvancedValue
							from @VoucherTransactionDetails vtd 
							inner join @DischargeBill  db on vtd.IdTmp = db.IdVoucherTransactionDTmp 
							where vtd.ChangeTracker<>'Deleted' and db.ChangeTracker <>'Deleted'
							GROUP BY db.IdAccountPayableShare
						) db
						JOIN Payments.AccountPayableShares aps on db.IdAccountPayableShare = aps.Id 
						join Payments.AccountPayable ap on aps.IdAccountPayable = ap.Id
						WHERE db.AdvancedValue > aps.Balance
					END						

					--Valido que los conceptos de pago/anticipos de facturas cxp tenga sus debidos detalles
					if exists 
					(
						select 1 
						from @VoucherTransactionDetails vtd 
						inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id
						LEFT JOIN
						(
							SELECT IdVoucherTransactionDTmp, SUM(ValueInCurrencyHeader) AdvanceValue
							FROM @TreasuryAdvances
							GROUP BY IdVoucherTransactionDTmp
						) ta ON vtd.IdTmp = ta.IdVoucherTransactionDTmp
						LEFT JOIN
						(
							SELECT IdVoucherTransactionDTmp, SUM(db.ValueInCurrencyHeader + (db.ValueDiscountInCurrencyHeader)) DischargeBillValue
							FROM @DischargeBill db
							WHERE db.ChangeTracker <>'Deleted'
							GROUP BY IdVoucherTransactionDTmp
						) db ON vtd.IdTmp = db.IdVoucherTransactionDTmp
						where vtd.ChangeTracker<>'Deleted' AND ec.Behavior = 3 AND vtd.Value <> ISNULL(ta.AdvanceValue, 0) + ISNULL(db.DischargeBillValue, 0)
					)
					begin						
						insert into @TableErrors
							select CONCAT('El valor del concepto ', ec.Code, ' no coincide con el valor de sus detalles (Valor Concepto: ', FORMAT(vtd.Value, 'C0', 'es-CO'), ' - Anticipo: ', FORMAT(ISNULL(ta.AdvanceValue, 0), 'C0', 'es-CO'), ' - Cruce Factura: ', FORMAT(ISNULL(db.DischargeBillValue, 0), 'C0', 'es-CO'), ')') + CHAR(13) + CHAR(10) 
							from @VoucherTransactionDetails vtd 
							inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id
							LEFT JOIN
							(
								SELECT IdVoucherTransactionDTmp, SUM(ValueInCurrencyHeader) AdvanceValue
								FROM @TreasuryAdvances
								GROUP BY IdVoucherTransactionDTmp
							) ta ON vtd.IdTmp = ta.IdVoucherTransactionDTmp
							LEFT JOIN
							(
								SELECT IdVoucherTransactionDTmp, SUM(db.ValueInCurrencyHeader + db.ValueDiscountInCurrencyHeader) DischargeBillValue
								FROM @DischargeBill db
								where db.ChangeTracker <>'Deleted'
								GROUP BY IdVoucherTransactionDTmp
							) db ON vtd.IdTmp = db.IdVoucherTransactionDTmp
							where vtd.ChangeTracker<>'Deleted' AND ec.Behavior = 3 AND vtd.Value <> ISNULL(ta.AdvanceValue, 0) + ISNULL(db.DischargeBillValue, 0)
					end

					--Devolutivos de Anticipos RC
					if (select COUNT(*) from @VoucherTransactionDetails vtd inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id where ec.Behavior = 4 and vtd.ChangeTracker<>'Deleted') >0 begin
						if(select COUNT(*) from @VoucherTransactionDetails vtd
						inner join Portfolio.PortfolioAdvance pa on vtd.IdThirdParty = pa.ThirdPartyId 
						inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id
						where ec.Behavior = 4 and pa.Value>0 and pa.Status = 2 and vtd.ChangeTracker<>'Deleted'
						 ) =0 begin
							insert into @TableErrors
							select 'No se encontraron Anticipos para el tercero seleccionado' + CHAR(13) + CHAR(10)
						END
						IF (	
						SELECT SUM(vtd.Value)
							FROM @VoucherTransactionDetails vtd
							INNER JOIN Treasury.ExpenseConcepts ec ON vtd.IdExpenseConcept = ec.Id
							WHERE vtd.ChangeTracker <> 'Deleted' AND ec.Behavior = 4
						) > (
							SELECT SUM(vta.Value)
							FROM @VoucherTransactionAdvance vta
							INNER JOIN Portfolio.PortfolioAdvance pa ON vta.PortfolioAdvanceId = pa.Id
							WHERE pa.Status = 2
						)
						BEGIN 
							INSERT INTO @TableErrors
							SELECT 'El valor actual del anticipo no coincide con el valor del detalle' + CHAR (13) + CHAR (10)
						END
					end
				end

				if @VoucherClass = 2 begin--reembolsos
					
					if(select COUNT(*) from @VoucherTransactionDetails vtd
					inner join Treasury.Refunds r  on vtd.CashRegisterId = r.IdCashRegister 
					where vtd.ChangeTracker<> 'Deleted' and r.Refunded = 0 and r.[Status]= 2 ) = 0 begin						
						insert into @TableErrors
						select 'No hay Reembolsos para Afectar' + CHAR(13) + CHAR(10)
					end
				end

				--si existen errores  se termina el proceso 
				 if(select COUNT(*) from @TableErrors )>0 begin
					SELECT @ErrorsValidation = COALESCE(@ErrorsValidation + '', '') + [message]  from @TableErrors				
					SELECT '999' AS CodeMessage,@ErrorsValidation AS Message,0 IdVoucherTransaction,CAST(3 AS TINYINT) AS [Status];
					RETURN;
				 end
			 end

		end

		--se guarda el comprobante
		if @IdVoucherTransaction = 0 begin
			if @CodeVoucherTransaction = '' begin
				--- Obtenemos la secuencia numerica
				IF(SELECT COUNT(*) FROM Treasury.TreasurySequence WHERE IdForm = 636) = 0 BEGIN
					SELECT '999' AS CodeMessage,'No existe secuencia numerica para el formulario de comprobantes de egreso' AS Message,0 IdVoucherTransaction,CAST(3 AS TINYINT) AS [Status];
					RETURN
				END

				DECLARE @idSequenceDetail INT;
				DECLARE @pattern VARCHAR(300);
				DECLARE @NextS BIGINT;
				DECLARE @Scope VARCHAR(5);
				DECLARE @IdSequence INT;
				DECLARE @IdSequenceCommon INT;

				SELECT @IdSequence = Id,@Scope = Scope,@IdSequenceCommon = IdSequence FROM Treasury.TreasurySequence WHERE IdForm = 636;
			
				IF @Scope = 'O'	BEGIN --- Secuencia por Prefijo		
					IF(select count(*) FROM Treasury.TreasurySequenceDetail psd INNER JOIN Common.Sequense cs ON cs.Id = psd.IdSequense WHERE psd.Prefix = @Prefix and psd.IdSequenseTreasuryC = @IdSequence) = 0 BEGIN
						--se agrega un nuevo registro al detalle con el prefijo que no existe
						INSERT INTO Treasury.TreasurySequenceDetail(IdSequenseTreasuryC, IdSequense, IdOperatingUnit, Next, Prefix) 
						VALUES(@IdSequence,@IdSequenceCommon,@IdUnitOperative,1,@Prefix);
						SET @idSequenceDetail = SCOPE_IDENTITY();
						SET @NextS = 1;
					END
					SELECT @pattern = cs.Pattern,@idSequenceDetail = psd.Id,@NextS = psd.[Next]
					FROM Treasury.TreasurySequenceDetail psd
					INNER JOIN Common.Sequense cs ON cs.Id = psd.IdSequense
					WHERE psd.Prefix = @Prefix and psd.IdSequenseTreasuryC = @IdSequence;
				END
				ELSE IF @Scope = 'OU' BEGIN -- Secuencia por Unidad operativa		
					SELECT @pattern = cs.Pattern,@idSequenceDetail = psd.Id,@NextS = psd.[Next]
					FROM Treasury.TreasurySequenceDetail psd
					INNER JOIN Common.Sequense cs ON cs.Id = psd.IdSequense
					WHERE psd.IdSequenseTreasuryC  = @IdSequence AND IdOperatingUnit = @IdUnitOperative;
				END
				ELSE IF @Scope = 'CC' BEGIN -- Secuencia por tipo de comprobante		
					SELECT @pattern = cs.Pattern,@idSequenceDetail = psd.Id,@NextS = psd.[Next]
					FROM Treasury.TreasurySequenceDetail psd
					INNER JOIN Common.Sequense cs ON cs.Id = psd.IdSequense
					WHERE psd.IdSequenseTreasuryC  = @IdSequence AND Type = @VoucherClass;
				END
			
				IF @IdSequence IS NULL OR @idSequenceDetail IS NULL BEGIN
					SELECT '999' AS CodeMessage,'No existe secuencia numerica para el formulario de comprobantes de egreso' AS Message,0 IdVoucherTransaction,CAST(3 AS TINYINT) AS [Status];
					RETURN
				END
				
				SELECT @CodeVoucherTransaction = dbo.GetSequence(@Prefix, @pattern, @NextS);
				--valido que la secuancia tenga valor disponible
				IF @CodeVoucherTransaction = '__ERROR_MAXVALUE__' BEGIN
					SELECT '999' AS CodeMessage,'La secuencia alcanzo su valor maximo' AS Message,0 IdVoucherTransaction,CAST(3 AS TINYINT) AS [Status];
					RETURN;
				END;				
				--actualizo la secuencia
				UPDATE Treasury.TreasurySequenceDetail
				SET [Next]+=1 WHERE Id = @idSequenceDetail;
			end
			
            --inserto en la tabla de control de tesereria
            INSERT INTO [Treasury].[TreasuryControl]([DocumentNumber],[DocumentType],[DocumentUser],[DocumentDate])
            VALUES (@CodeVoucherTransaction,2,@User,@DocumentDate)

			--valido que existan paremtros para el modulo
			if(select COUNT(*) from Treasury.SettingsTreasury where IdOperatingUnit = @IdUnitOperative ) = 0 begin 
				SELECT '999' AS CodeMessage,'No se encontró parámetros para la unidad operativa seleccionada' AS Message,0 IdVoucherTransaction,CAST(3 AS TINYINT) AS [Status];
                RETURN;
			end

			
			if @IdChecks > 0 AND @PaymentMethod = 1 begin
			--se hace el control de cheques si en el parametro esta verdadero
				if (select CheckBookControl from Treasury.SettingsTreasury where IdOperatingUnit = @IdUnitOperative ) = 1 begin 

					--Se valida que el número de cheque no haya sido utilizado anteriormente en algún comprobante de egreso
					--Solo aplica si @CheckNumber > 0; cuando llega en 0, SP_SaveCheckNumber lo asignará más abajo.
					--Solo cuentan comprobantes vivos (Registrado=1 / Confirmado=2); los anulados/reversados no bloquean.
					if @CheckNumber > 0
					   and (select COUNT(*) from Treasury.VoucherTransaction
					        where CheckNumber = @CheckNumber
					          and IdChecks    = @IdChecks
					          and [Status] in (1, 2)) > 0
					Begin
						declare @codeVT varchar(20) = (select TOP 1 Code from Treasury.VoucherTransaction
						                               where CheckNumber = @CheckNumber
						                                 and IdChecks    = @IdChecks
						                                 and [Status] in (1, 2)
						                               order by Id desc)
						select '999' AS CodeMessage, 'El número de cheque ' + cast(@CheckNumber as varchar) + ' ya está siendo utilizado por el comprobante de egreso ' + @codeVT AS Message,0 IdVoucherTransaction,CAST(3 AS TINYINT) AS [Status]
						return
					End

			 		declare @resultSaveCheck table(StatusResult varchar(20),MessageResult varchar(300), CheckNumberToAssing bigint null)
					insert @resultSaveCheck exec Treasury.SP_SaveCheckNumber @IdUnitOperative,@IdEntityBankAccount,@CheckNumber,@User
					
					if (select StatusResult  from @resultSaveCheck) = '999' begin
					
					SELECT '999' AS CodeMessage,(select MessageResult  from @resultSaveCheck) AS Message,0 IdVoucherTransaction,CAST(3 AS TINYINT) AS [Status]
					return
					end

					if @CheckNumber =0 begin 
						select @CheckNumber = CheckNumberToAssing  from @resultSaveCheck		
					end								
				end
			end			

			
			--inserto la cabecera del comprobante de egreso
			INSERT INTO [Treasury].[VoucherTransaction]
			   ([Code]
			   ,[IdThirdParty]
			   ,[IdMainAccount]
			   ,[IdCostCenter]
			   ,[VoucherClass]
			   ,[ExpenseType]
			   ,[Detail]
			   ,[DocumentDate]
			   ,[IdCashRegister]
			   ,[IdEntityBankAccount]
			   ,[Value]
			   ,[PaymentMethod]
			   ,[NoteNumber]
			   ,[IdChecks]
			   ,[CheckNumber]
			   ,[TransactionDate]
			   ,[TaxByMil]
			   ,[TaxByMilValue]
			   ,[CashRegisterExpense]
			   ,[RefundCashRegisterExpense]
			   ,[SchedulePaymentId]
			   ,[BeneficiaryIdentification]
			   ,[Beneficiary]
			   ,[TransactionRelationship]
			   ,[CheckReconciled]
			   ,[IdPaymentOrder]
			   ,[Printed]
			   ,[RTEValue]
			   ,[IVAValue]
			   ,[ICAValue]
			   ,[OtherValue]
			   ,[BankAccountNumber]
			   ,[BankName]
			   ,[DetailsInterfaceBudget]
			   ,[IdUnitOperative]
			   ,[IdRefund]
			   ,[Status]
			   ,[CreationUser]
			   ,[CreationDate]
			   ,[ModificationUser]
			   ,[ModificationDate]
			   ,[ConfirmationUser]
			   ,[ConfirmationDate]
			   ,[AnnulmentUser]
			   ,[AnnulmentDate]
			   ,[ReversedUser]
			   ,[ReversedDate]
			   ,[EmailSent]
			   ,IdCheckCashingStatus
			   ,HandlesDocumentSupport
			   ,CurrencyId
			   ,SupplierBankAccountId)
		 VALUES
			   (@CodeVoucherTransaction
			   ,@IdThirdParty
			   ,@IdMainAccount
			   ,@IdCostCenter
			   ,@VoucherClass
			   ,@ExpenseType
			   ,@Detail
			   ,@DocumentDate
			   ,@IdCashRegister
			   ,@IdEntityBankAccount
			   ,@Value
			   ,@PaymentMethod
			   ,@NoteNumber
			   ,@IdChecks
			   ,CASE WHEN @PaymentMethod = 1 AND @IdChecks > 0 THEN @CheckNumber ELSE NULL END
			   ,@TransactionDate
			   ,@TaxByMil
			   ,@TaxByMilValue
			   ,@CashRegisterExpense
			   ,@RefundCashRegisterExpense
			   ,@SchedulePaymentId
			   ,@BeneficiaryIdentification
			   ,@Beneficiary
			   ,@TransactionRelationship
			   ,@CheckReconciled
			   ,@IdPaymentOrder
			   ,@Printed
			   ,@RTEValue
			   ,@IVAValue
			   ,@ICAValue
			   ,@OtherValue
			   ,@BankAccountNumber
			   ,@BankName
			   ,@DetailsInterfaceBudget
			   ,@IdUnitOperative
			   ,@IdRefund
			   ,@Status
			   ,@User
			   ,[Common].[GETDATE]()
			   ,null
			   ,null
			   ,@ConfirmationUser 
			   ,@ConfirmationDate 
			   ,null
			   ,null
			   ,null
			   ,null
			   ,0
			   ,1
			   ,@HandlesDocumentSupport,
			   @CurrencyIdHeader,
			   @SupplierBankAccountId)

			
			SET @IdVoucherTransaction = SCOPE_IDENTITY();		
			update @VoucherTransactionDetails set IdVoucherTransaction = @IdVoucherTransaction	

		end
		else begin 
			--si se actualiza
			UPDATE [Treasury].[VoucherTransaction]
			SET	 [Code] = @CodeVoucherTransaction
				,[IdThirdParty] = @IdThirdParty
				,[IdMainAccount] = @IdMainAccount
				,[IdCostCenter] = @IdCostCenter
				,[VoucherClass] = @VoucherClass
				,[ExpenseType] = @ExpenseType
				,[Detail] = @Detail
				,[DocumentDate] = @DocumentDate
				,[IdCashRegister] = @IdCashRegister
				,[IdEntityBankAccount] = @IdEntityBankAccount
				,[Value] = @Value
				,[PaymentMethod] = @PaymentMethod
				,[NoteNumber] = @NoteNumber
				,[IdChecks] = @IdChecks
				,[CheckNumber] = CASE WHEN @PaymentMethod = 1 AND @IdChecks > 0 THEN @CheckNumber ELSE NULL END
				,[TransactionDate] = @TransactionDate
				,[TaxByMil] = @TaxByMil
				,[TaxByMilValue] = @TaxByMilValue
				,[CashRegisterExpense] = @CashRegisterExpense
				,[RefundCashRegisterExpense] = @RefundCashRegisterExpense
				,[SchedulePaymentId] = @SchedulePaymentId
				,[BeneficiaryIdentification] = @BeneficiaryIdentification
				,[Beneficiary] = @Beneficiary
				,[TransactionRelationship] = @TransactionRelationship
				,[CheckReconciled] = @CheckReconciled
				,[IdPaymentOrder] = @IdPaymentOrder
				,[Printed] = @Printed
				,[RTEValue] = @RTEValue
				,[IVAValue] = @IVAValue
				,[ICAValue] = @ICAValue
				,[OtherValue] = @OtherValue
				,[BankAccountNumber] = @BankAccountNumber
				,[BankName] = @BankName
				,[DetailsInterfaceBudget] = @DetailsInterfaceBudget
				,[IdUnitOperative] = @IdUnitOperative
				,[IdRefund] = @IdRefund
				,[Status] = @Status			
				,[ModificationUser] = @User
				,[ModificationDate] = [Common].[GETDATE]()
				,[ConfirmationUser] = @ConfirmationUser
				,[ConfirmationDate] = @ConfirmationDate
				,[AnnulmentUser] = @AnnulmentUser
				,[AnnulmentDate] = @AnnulmentDate
				,[HandlesDocumentSupport] =@HandlesDocumentSupport
				,[CurrencyId] = @CurrencyIdHeader
				,[SupplierBankAccountId] = @SupplierBankAccountId
			WHERE  Id = @IdVoucherTransaction

			--si se esta anulando
			if @Status = 3 begin
				--elimino el registro de la tabla de control
                DELETE Treasury.TreasuryControl WHERE DocumentNumber = @CodeVoucherTransaction AND DocumentType = 2;
				--elimino el cheque bloqueado
				delete Treasury.CheckBlock where IdCheckbook = @IdChecks and CheckNumber = @CheckNumber 
				--inserto en la tabla de anulacion de cheques siempre y cuando sea tipo cheque
				if @PaymentMethod = 1 --Si es por cheque
				Begin
					--INSERT INTO [Treasury].[CancellationChecks]
					--([IdEntityAccount],[IdCheckBook],[CancellationDate],[CheckNumber],[Description],[CreationUser],[CreationDate])
					--VALUES
					--(@IdEntityBankAccount,@IdChecks,[Common].[GETDATE](),@CheckNumber,'Anulación por comprobante de egreso',@User,[Common].[GETDATE]())
					DECLARE @CheckStatus tinyint = 0
					SELECT @CheckStatus = IdCheckCashingStatus FROM [Treasury].[VoucherTransaction] WHERE ID = @IdVoucherTransaction
					INSERT INTO Treasury.CheckCashingControl 
					(IdEntityAccount,DueDate,Observation,BalanceAccount,CreationUser,CreationDate)
					VALUES
					(@IdEntityBankAccount,[Common].[GETDATE](),'Anulación por comprobante de egreso',0,@User,[Common].[GETDATE]())
					INSERT INTO Treasury.CheckCashingControlDetail
					(IdCheckCashingControl,IdVoucherTransaction,IdCheckBook,CheckNumber,PreviousCheckStatus,CurrentCheckStatus)
					VALUES
					(SCOPE_IDENTITY(),@IdVoucherTransaction,@IdChecks,@CheckNumber,@CheckStatus,CAST(5 AS TINYINT))
					UPDATE [Treasury].[VoucherTransaction] SET IdCheckCashingStatus = 5 WHERE ID = @IdVoucherTransaction
				End
			end
		end

		UPDATE vtd
			SET vtd.Observation = REPLACE(vtd.Observation, '{0}', @CodeVoucherTransaction)
		FROM @VoucherTransactionDetails vtd

		--si se esta guardando o confirmando
		IF(@Status < 3)
		BEGIN
			--se eliminan todos los detalles
			DELETE dbb 
			FROM @DischargeBillBudget dbbtmp 
			JOIN Treasury.DischargeBillBudget dbb ON dbbtmp.Id = dbb.Id 
			WHERE dbbtmp.ChangeTracker='Deleted' OR NOT (dbbtmp.Value > 0)

			DELETE dbb 
			FROM Treasury.DischargeBillBudget dbb 
			JOIN Treasury.DischargeBill db ON dbb.DischargeBillId = db.Id 
			JOIN Treasury.VoucherTransactionDetails vtd ON db.IdVoucherTransactionD = vtd.Id
			JOIN @VoucherTransactionDetails vtdtmp ON vtd.Id = vtdtmp.Id
			WHERE vtdtmp.ChangeTracker = 'Deleted'
		
			--Solución provisional para que elimine los datos de Treasury.DischargeBillBudget cuando se eliminan registros de Treasury.DischargeBill
			delete dbb
			FROM Treasury.DischargeBillBudget dbb 
			JOIN @DischargeBill db ON dbb.DischargeBillId = db.Id 
			JOIN Treasury.VoucherTransactionDetails vtd ON db.IdVoucherTransactionD = vtd.Id 
		    JOIN @VoucherTransactionDetails vtdtmp ON vtd.Id = vtdtmp.Id 
			WHERE db.ChangeTracker = 'Deleted'

			delete Treasury.DischargeBill from @DischargeBill dbtmp inner join Treasury.DischargeBill db on dbtmp.Id = db.Id where dbtmp.ChangeTracker='Deleted'
			delete Treasury.DischargeBill where Id in (select db.Id from @VoucherTransactionDetails vtdtmp inner join Treasury.VoucherTransactionDetails vtd on vtdtmp.Id = vtd.Id inner join Treasury.DischargeBill db on db.IdVoucherTransactionD = vtd.Id where vtdtmp.ChangeTracker = 'Deleted')

			delete Treasury.VoucherTransactionAdvance from @VoucherTransactionAdvance vtatmp inner join Treasury.VoucherTransactionAdvance vta on vtatmp.Id = vta.Id where vtatmp.ChangeTracker='Deleted'
			delete Treasury.VoucherTransactionAdvance where Id in (select vta.Id from @VoucherTransactionDetails vtdtmp inner join Treasury.VoucherTransactionDetails vtd on vtdtmp.Id = vtd.Id inner join Treasury.VoucherTransactionAdvance vta on vta.IdVoucherTransactionD = vtd.Id where vtdtmp.ChangeTracker = 'Deleted')

			delete Treasury.TreasuryAdvances from @TreasuryAdvances tatmp inner join Treasury.TreasuryAdvances ta on tatmp.Id = ta.Id where tatmp.ChangeTracker = 'Deleted' 
			delete Treasury.TreasuryAdvances where Id in (select vta.Id from @VoucherTransactionDetails vtdtmp inner join Treasury.VoucherTransactionDetails vtd on vtdtmp.Id = vtd.Id inner join Treasury.TreasuryAdvances vta on vta.IdVoucherTransactionDetail = vtd.Id where vtdtmp.ChangeTracker = 'Deleted')

			delete Treasury.VoucherTransactionDetails from @VoucherTransactionDetails vtdtmp inner join Treasury.VoucherTransactionDetails vtd on vtdtmp.Id = vtd.Id where vtdtmp.ChangeTracker = 'Deleted'
			
			--se actualizan los detalles
			UPDATE dbb
			   SET	[ObligationDetailId] = dbbtmp.ObligationDetailId,
					[Value] = dbbtmp.Value
			from @DischargeBillBudget dbbtmp 
			JOIN Treasury.DischargeBillBudget dbb ON dbbtmp.Id = dbb.Id 
			WHERE dbbtmp.ChangeTracker='Modified'

			UPDATE [Treasury].[DischargeBill]
			   SET 
				   [IdAccountPayable] = dbtmp.IdAccountPayable
				  ,[IdAccountPayableShare] = dbtmp.IdAccountPayableShare
				  ,[AdvancedValue] = dbtmp.AdvancedValue
				  ,[AdvancePercent] = dbtmp.AdvancePercent
				  ,[IdPaymentConcept] = dbtmp.IdPaymentConcept
				  ,[BaseValueDiscount] = dbtmp.BaseValueDiscount
				  ,[DiscountPercent] = dbtmp.DiscountPercent
				  ,[PaymentOrderValue] = dbtmp.PaymentOrderValue
				  ,[ValueInCurrencyHeader] = dbtmp.ValueInCurrencyHeader
				  ,[TRMValue] =dbtmp.TRMValue
				  ,[ValueDiscountInCurrencyHeader]=dbtmp.ValueDiscountInCurrencyHeader
			from @DischargeBill dbtmp inner join Treasury.DischargeBill db on dbtmp.Id = db.Id where dbtmp.ChangeTracker='Modified'

			UPDATE [Treasury].[VoucherTransactionAdvance]
			   SET 
				   [PortfolioAdvanceId] = vtatmp.PortfolioAdvanceId
				  ,[Value] = vtatmp.Value
				  ,[Percentage] = vtatmp.Percentage
			from @VoucherTransactionAdvance vtatmp inner join Treasury.VoucherTransactionAdvance vta on vtatmp.Id = vta.Id where vtatmp.ChangeTracker='Modified'

			UPDATE [Treasury].[VoucherTransactionDetails]
			   SET 
				   [IdEntityBankAccount] = vtdtmp.IdEntityBankAccount
				  ,[CashRegisterId] = vtdtmp.CashRegisterId
				  ,[IdThirdParty] = vtdtmp.IdThirdParty
				  ,[IdExpenseConcept] = vtdtmp.IdExpenseConcept
				  ,[IdMainAccount] = vtdtmp.IdMainAccount
				  ,[Nature] = vtdtmp.Nature
				  ,[IdCostCenter] = vtdtmp.IdCostCenter
				  ,[Value] = vtdtmp.Value
				  ,[IdRetentionConcept] = vtdtmp.IdRetentionConcept
				  ,[BaseValue] = vtdtmp.BaseValue
				  ,[BillingValue] = vtdtmp.BillingValue
				  ,[PercentRetention] = vtdtmp.PercentRetention
				  ,[Detail] = vtdtmp.Detail
				  ,[Observation] = vtdtmp.Observation
				  ,[IdCashFlowConcept] = vtdtmp.IdCashFlowConcept
				  ,[discountableIVA] = vtdtmp.discountableIVA
				  ,[TaxRegistration] = vtdtmp.TaxRegistration
				  ,[IdGeneralLedgerIVA] = vtdtmp.IdGeneralLedgerIVA
				  ,[ValueIVA] = vtdtmp.ValueIVA
				  ,[TotalConcept] = vtdtmp.TotalConcept
				  ,[SupplierBankAccountId] = vtdtmp.SupplierBankAccountId 
			from @VoucherTransactionDetails vtdtmp inner join Treasury.VoucherTransactionDetails vtd on vtdtmp.Id = vtd.Id where vtdtmp.ChangeTracker = 'Modified'

			update Treasury.TreasuryAdvances 
			set 
				Detail = tatmp.Detail ,Value = tatmp.Value,CurrencyId = tatmp.CurrencyId,ValueInCurrencyHeader=tatmp.ValueInCurrencyHeader,TRMValue=tatmp.TRMValue
			 from @TreasuryAdvances tatmp inner join Treasury.TreasuryAdvances ta on tatmp.Id = ta.Id where tatmp.ChangeTracker = 'Modified' 

			--recorro los detalles del recibo de caja
            DECLARE @VoucherTransactionDetailId INT, 
					@VoucherTransactionDetailIdTmp INT, 
					@DetailTmp VARCHAR(MAX),
					----------------------------------
					@DischargeBillRows INT,
					@DischargeBillIdTmp INT,
					@DischargeBillId INT

            DECLARE detail_cursor CURSOR
            FOR SELECT Id,IdTmp,Detail FROM @VoucherTransactionDetails WHERE ChangeTracker <> 'Deleted';
            OPEN detail_cursor;
            FETCH NEXT FROM detail_cursor INTO @VoucherTransactionDetailId, @VoucherTransactionDetailIdTmp, @DetailTmp;
				WHILE @@FETCH_STATUS = 0 BEGIN

				SELECT	@DetailTmp = REPLACE(@DetailTmp, '{0}', @CodeVoucherTransaction),
						-----------------------------------------------------------------
						@DischargeBillRows = 1,
						@DischargeBillIdTmp = 0

				--si el detalle es nuevo se inserta
				if @VoucherTransactionDetailId = 0 
				begin
					--se inserta el nuevo detalle
					INSERT INTO [Treasury].[VoucherTransactionDetails]
					([IdVoucherTransaction],[IdEntityBankAccount],[CashRegisterId],[IdThirdParty],[IdExpenseConcept],[IdMainAccount],[Nature],[IdCostCenter],[Value],[IdRetentionConcept],[BaseValue],[BillingValue],[PercentRetention],
					[Detail],[Observation],[IdCashFlowConcept],[discountableIVA],[IdGeneralLedgerIVA],[ValueIVA],[TotalConcept], [TaxRegistration],[EconomicActivityId],[SupplierBankAccountId])
					select @IdVoucherTransaction,IdEntityBankAccount,CashRegisterId,IdThirdParty,IdExpenseConcept,IdMainAccount,Nature,IdCostCenter,Value,IdRetentionConcept,BaseValue,BillingValue,PercentRetention,
					Detail,Observation,IdCashFlowConcept,discountableIVA, IdGeneralLedgerIVA,ValueIVA, TotalConcept,TaxRegistration,EconomicActivityId,SupplierBankAccountId 
					from @VoucherTransactionDetails
					WHERE IdTmp = @VoucherTransactionDetailIdTmp;
										
					SET @VoucherTransactionDetailId = SCOPE_IDENTITY();
				END

				update @VoucherTransactionAdvance set IdVoucherTransactionD = @VoucherTransactionDetailId where IdVoucherTransactionDTmp = @VoucherTransactionDetailIdTmp and ChangeTracker='Added'
				update @DischargeBill set IdVoucherTransactionD = @VoucherTransactionDetailId where IdVoucherTransactionDTmp = @VoucherTransactionDetailIdTmp and ChangeTracker='Added'
				update @VoucherTransactionDetails set Id = @VoucherTransactionDetailId where IdTmp = @VoucherTransactionDetailIdTmp and ChangeTracker='Added'

				--inserto los detalles nuevos
				INSERT INTO [Treasury].[VoucherTransactionAdvance]
				select @VoucherTransactionDetailId,[PortfolioAdvanceId],[Value],[Percentage] 
				FROM @VoucherTransactionAdvance WHERE IdVoucherTransactionDTmp = @VoucherTransactionDetailIdTmp  AND ChangeTracker = 'Added';

				WHILE @DischargeBillRows > 0
				BEGIN
					SELECT TOP 1
						@DischargeBillIdTmp = IdTmp
					FROM @DischargeBill 
					WHERE IdVoucherTransactionDTmp = @VoucherTransactionDetailIdTmp AND ChangeTracker = 'Added'
						AND IdTmp > @DischargeBillIdTmp
					ORDER BY IdTmp

					SET @DischargeBillRows = @@ROWCOUNT
					IF @DischargeBillRows = 0
						BREAK
						
					INSERT INTO [Treasury].[DischargeBill]
						SELECT @VoucherTransactionDetailId,[IdAccountPayable],[IdAccountPayableShare],[AdvancedValue],[AdvancePercent],[IdPaymentConcept],[BaseValueDiscount],[DiscountPercent],[PaymentOrderValue],[ValueInCurrencyHeader],[TRMValue],[ValueDiscountInCurrencyHeader]
						FROM @DischargeBill 
						WHERE IdVoucherTransactionDTmp = @VoucherTransactionDetailIdTmp  AND ChangeTracker = 'Added' AND IdTmp = @DischargeBillIdTmp

					SET @DischargeBillId = SCOPE_IDENTITY()

					INSERT INTO Treasury.DischargeBillBudget
						SELECT @DischargeBillId, ObligationDetailId, Value
						FROM @DischargeBillBudget
						WHERE IdDischargeBillTmp = @DischargeBillIdTmp AND ChangeTracker = 'Added' AND Value > 0
				END

				INSERT INTO Treasury.DischargeBillBudget
					SELECT dbbTmp.DischargeBillId, dbbTmp.ObligationDetailId, dbbTmp.Value
					FROM @DischargeBillBudget dbbTmp
					JOIN @DischargeBill dbTmp ON dbbTmp.IdDischargeBillTmp = dbTmp.IdTmp
					WHERE dbbTmp.ChangeTracker = 'Added' AND dbTmp.ChangeTracker = 'Modified' AND dbbTmp.Value > 0

				insert into Treasury.TreasuryAdvances(IdVoucherTransactionDetail, Detail, Value, TRMValue, ValueInCurrencyHeader,CurrencyId) 
				select	@VoucherTransactionDetailId,
						Detail,Value, 
						TRMValue, ValueInCurrencyHeader, CurrencyId
						from @TreasuryAdvances 
						where IdVoucherTransactionDTmp =@VoucherTransactionDetailIdTmp and  ChangeTracker = 'Added'     

				FETCH NEXT FROM detail_cursor INTO @VoucherTransactionDetailId, @VoucherTransactionDetailIdTmp, @DetailTmp;
				END;
			CLOSE detail_cursor;
			DEALLOCATE detail_cursor;
		end

		--********************************CONFIRMACION DEL COMPROBANTE***************************************************************

		if @Status = 2 
		begin
			--se elimina el registro de la tabla de control
			delete Treasury.TreasuryControl where DocumentNumber = @CodeVoucherTransaction and DocumentType = 2
			
			if @ExpenseType = 1 
			begin--cuenta bancaria
				if @PaymentMethod = 1 
				begin
					--se elimina el bloqueo del cheque
					delete Treasury.CheckBlock where Id = @IdChecks
					--se actualiza el estado del cheque
					update Treasury.Checkbooks set [Status] = 3
					from Treasury.Checkbooks cb 
					inner join Treasury.CheckBlock cbb on cb.Id = cbb.IdCheckbook
					inner join Treasury.OutstandingChecks oc on cb.Id = oc.IdCheckBook 
					where cb.IdEntityBanckAccount= @IdEntityBankAccount and cb.CurrentNumber = cb.EndNumber

				end

				--se inserta un registro en la tabla de saldos de teroreria
				INSERT INTO [Treasury].[TreasuryBalance]
				   ([DocumentNumber]
				   ,[DocumentDate]
				   ,[DocumentType]
				   ,[Nature]
				   ,[CashRegisterId]
				   ,[EntityBankAccountId]
				   ,[PreviousBalance]
				   ,[ValueMovement]
				   ,[CreationDate])
				select @CodeVoucherTransaction,@DocumentDate,2,2,null,@IdEntityBankAccount, CurrentBalance,@Value+@TaxByMil,[Common].[GETDATE]() from Treasury.EntityBankAccounts where Id = @IdEntityBankAccount
				--se actualiza el saldo de la cuenta
				update Treasury.EntityBankAccounts set CurrentBalance-= @Value+@TaxByMil where Id = @IdEntityBankAccount
			end
			else begin --caja
				--se inserta un registro en la tabla de saldos de teroreria
				INSERT INTO [Treasury].[TreasuryBalance]
				   ([DocumentNumber]
				   ,[DocumentDate]
				   ,[DocumentType]
				   ,[Nature]
				   ,[CashRegisterId]
				   ,[EntityBankAccountId]
				   ,[PreviousBalance]
				   ,[ValueMovement]
				   ,[CreationDate])
				select @CodeVoucherTransaction,@DocumentDate,2,2,@IdCashRegister,null,CurrentBalance,@Value+@TaxByMil,[Common].[GETDATE]() from Treasury.CashRegisters where Id = @IdCashRegister
				--se actualiza el saldo de la caja
				update Treasury.CashRegisters set CurrentBalance-= @Value+@TaxByMil, IsMovement=1 where Id = @IdCashRegister
			end
			
			if @VoucherClass = 1 begin--pagos
				--2 caja menor
				--se actuliza el saldo de las cuentas
				update Payments.AccountPayableShares set Balance-= (db.AdvancedValue + db.BaseValueDiscount) , PaymentValue += (db.AdvancedValue +db.BaseValueDiscount)
				from @VoucherTransactionDetails vtd 
				inner join @DischargeBill db on vtd.Id = db.IdVoucherTransactionD
				inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id
				inner join Payments.AccountPayableShares aps on db.IdAccountPayableShare = aps.Id
				where vtd.ChangeTracker<> 'Deleted' and db.ChangeTracker<>'Deleted' and ec.Behavior = 2

				--insertams en la tabla de movimientos de cuentas por pagar cuando el comportamiento sea 2
				INSERT INTO [Payments].[MovementAccountPayables]
				([IdAccountPayable]
				,[IdAccountPayableShare]
				,[EntityId]
				,[EntityCode]
				,[EntityName]
				,[MovementDate]
				,[Value])
				select db.IdAccountPayable ,db.IdAccountPayableShare ,@IdVoucherTransaction,@CodeVoucherTransaction,'VoucherTransaction',[Common].[GETDATE](),(db.AdvancedValue +db.BaseValueDiscount)
				from @VoucherTransactionDetails vtd 
				inner join @DischargeBill db on vtd.Id = db.IdVoucherTransactionD
				inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id				
				where vtd.ChangeTracker<> 'Deleted' and db.ChangeTracker<>'Deleted' and ec.Behavior = 2
				
				--3 Pagos/Anticipos de facturas
				update Payments.AccountPayableShares set Balance-= (db.AdvancedValue + db.BaseValueDiscount) , PaymentValue += (db.AdvancedValue +db.BaseValueDiscount)
				from @VoucherTransactionDetails vtd 
				inner join @DischargeBill db on vtd.Id = db.IdVoucherTransactionD
				inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id
				inner join Payments.AccountPayableShares aps on db.IdAccountPayableShare = aps.Id
				where vtd.ChangeTracker<> 'Deleted' and db.ChangeTracker<>'Deleted' and ec.Behavior = 3
				
				--insertams en la tabla de movimientos de cuentas por pagar cuando el comportamiento sea 3
				INSERT INTO [Payments].[MovementAccountPayables]
				([IdAccountPayable]
				,[IdAccountPayableShare]
				,[EntityId]
				,[EntityCode]
				,[EntityName]
				,[MovementDate]
				,[Value])
				select db.IdAccountPayable ,db.IdAccountPayableShare ,@IdVoucherTransaction,@CodeVoucherTransaction,'VoucherTransaction',[Common].[GETDATE](),(db.AdvancedValue +db.BaseValueDiscount)
				from @VoucherTransactionDetails vtd 
				inner join @DischargeBill db on vtd.Id = db.IdVoucherTransactionD
				inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id				
				where vtd.ChangeTracker<> 'Deleted' and db.ChangeTracker<>'Deleted' and ec.Behavior = 3

				if(	select count(*) from @VoucherTransactionDetails vtd 
					inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id 
					inner join Treasury.TreasuryAdvances ta on vtd.Id = ta.IdVoucherTransactionDetail 
					inner join Common.Supplier s on s.IdThirdParty = vtd.IdThirdParty 
					where vtd.ChangeTracker<> 'Deleted' and ec.Behavior = 3) > 0 begin					

					DECLARE @InsertedAdvancePaymentsIds TABLE (Id INT,CurrencyId INT)
					--se crean anticipos en advancePayments si existen en la tabla de TreasuryAdvances
					INSERT INTO [Payments].[AdvancePayments]
					([Code]
					,[IdSupplier]
					,[IdAccount]
					,[IdCostCenter]
					,[IdThirdParty]
					,[StateAdvancePayments]
					,[DocumentDate]
					,[Comments]
					,[Value]
					,[DebitValue]
					,[CreditValue]
					,[Balance]
					,[EntityId]
					,[EntityCode]
					,[EntityName]
					,[Status]
					,[CreationUser]
					,[CreationDate]
					,[ModificationUser]
					,[ModificationDate]
					,[ConfirmationUser]
					,[ConfirmationDate]
					,[AnnulmentUser]
					,[AnnulmentDate]
					,[CurrencyId]
					,[TRMValue]
					,[ValueCurrencyHeader])
					OUTPUT INSERTED.Id,INSERTED.CurrencyId
					INTO @InsertedAdvancePaymentsIds
					select	@CodeVoucherTransaction,
							s.Id,vtd.IdMainAccount,
							vtd.IdCostCenter,
							vtd.IdThirdParty,
							1,
							@DocumentDate,
							ta.Detail,
							ta.Value,
							0,
							0,
							ta.Value,
							@IdVoucherTransaction,
							@CodeVoucherTransaction,
							'VoucherTransaction',
							2,
							@User,
							[Common].[GETDATE](),
							null,
							null,
							@User,
							[Common].[GETDATE](),
							null,
							null,
							ta.CurrencyId,
							ta.TRMValue,
							ta.ValueInCurrencyHeader
					from @VoucherTransactionDetails vtd				
					inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id		
					inner join Treasury.TreasuryAdvances ta on vtd.Id = ta.IdVoucherTransactionDetail
					inner join Common.Supplier s on s.IdThirdParty = vtd.IdThirdParty 		
					where vtd.ChangeTracker<> 'Deleted' and ec.Behavior = 3	
					
					/*-------------------INSERCION TABLA EXGANGED DE ANTICIPOS CXP--------*/
					DECLARE @AdvancePaymentId INT,@CurrencyIdAdvancePayment INT
					DECLARE Cursor_A CURSOR LOCAL
						FOR SELECT Id,CurrencyId FROM @InsertedAdvancePaymentsIds
					OPEN Cursor_A
					FETCH NEXT FROM Cursor_A INTO @AdvancePaymentId,@CurrencyIdAdvancePayment
						WHILE (@@FETCH_STATUS = 0)
						BEGIN
						DECLARE @StateResult VARCHAR(3), @MessageOutput VARCHAR(100)

						EXEC [Common].[SP_InsertIntoExchangeRate] 'AdvancePayments',@AdvancePaymentId,@CurrencyIdAdvancePayment,@StateResult=@StateResult OUTPUT, @MessageOutput = @MessageOutput OUTPUT
						
						IF ISNULL(@StateResult, '999') = '999' BEGIN
							FETCH NEXT FROM Cursor_A INTO @AdvancePaymentId,@CurrencyIdAdvancePayment
							CLOSE Cursor_A
							DEALLOCATE Cursor_A
							SELECT '999' AS CodeMessage, 
									CONCAT('Error al insertar en la tasa de cambio: ', @MessageOutput) AS Message,
									0 IdVoucherTransaction,
									CAST(3 AS TINYINT) AS [Status]
							RETURN
						END

						FETCH NEXT FROM Cursor_A INTO @AdvancePaymentId,@CurrencyIdAdvancePayment
						END
					CLOSE Cursor_A
					DEALLOCATE Cursor_A
					
					/*--------------------------------------------------------------------*/

				end
				
				--actualizamos el saldo de la cabecera de la cuenta por pagar
				UPDATE Payments.AccountPayable 
				SET Balance = shares.sumBalance 
				FROM @VoucherTransactionDetails vtd 
				INNER JOIN @DischargeBill db ON vtd.Id = db.IdVoucherTransactionD 
				INNER JOIN Treasury.ExpenseConcepts ec ON vtd.IdExpenseConcept = ec.Id 
				INNER JOIN (
					SELECT 
						ap.Id as IdAccountPayable, 
						SUM(aps.Balance) as sumBalance 
					FROM Payments.AccountPayable ap 
					INNER JOIN Payments.AccountPayableShares aps ON ap.Id = aps.IdAccountPayable 
					WHERE ap.Id IN (
						SELECT DISTINCT db2.IdAccountPayable 
						FROM @DischargeBill db2 
						WHERE db2.ChangeTracker <> 'Deleted'
					)
					GROUP BY ap.Id
				) shares ON db.IdAccountPayable = shares.IdAccountPayable 
				INNER JOIN Payments.AccountPayable ac ON db.IdAccountPayable = ac.Id 
				WHERE ec.Behavior IN (2,3)

				--actulizamos el saldo de los anticipos
				update Portfolio.PortfolioAdvance set Balance -= vta.Value
				from @VoucherTransactionDetails vtd 
				inner join @VoucherTransactionAdvance vta on vtd.Id = vta.IdVoucherTransactionD
				inner join Portfolio.PortfolioAdvance pa on vta.PortfolioAdvanceId = pa.Id
				inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id		
				where vtd.ChangeTracker<> 'Deleted' and ec.Behavior = 4

				-- Verificar si @DischargeBill tiene filas
				IF EXISTS (SELECT 1 FROM @DischargeBill)
				BEGIN
				/*----------ajuste diferencial de cxp por pagos -------------*/
				declare @responseRevaluaiton table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)
				declare @ListAccountPayable TABLE (Id INT  NOT NULL,
													ValuePaid NUMERIC(20,2) NOT NULL,
													EntityName VARCHAR(250),
													EntityId INT)
				declare @TableResultTRMAdjustment TABLE (MessageOutput varchar(max))
				declare @ListAccountPayableXml as XML,
						@XmlOutput as XML
								
					INSERT INTO @ListAccountPayable
					SELECT db.IdAccountPayable, sum(db.AdvancedValue +db.BaseValueDiscount),'VoucherTransaction',vtd.IdVoucherTransaction
					from @VoucherTransactionDetails vtd 
					inner join @DischargeBill db on vtd.Id = db.IdVoucherTransactionD
					inner join Treasury.ExpenseConcepts ec on vtd.IdExpenseConcept = ec.Id
					inner join Payments.AccountPayableShares aps on db.IdAccountPayableShare = aps.Id
					where vtd.ChangeTracker<> 'Deleted' and db.ChangeTracker<>'Deleted' and ec.Behavior in(2,3)
					group by db.IdAccountPayable,vtd.IdVoucherTransaction
					
					SELECT @ListAccountPayableXml = CONVERT(xml, 
													(
														SELECT * FROM @ListAccountPayable AccountPayable 
														For xml AUTO,TYPE, ELEMENTS
													))

					EXEC [Payments].[SP_AccountPayableRevaluation]
						@ListAccountPayableXml,
						@User,@XmlOutput OUTPUT
						
					INSERT @responseRevaluaiton
					SELECT
					t.x.value('Code[1]', 'Varchar(20)')  Code,
					t.x.value('MessageOutput[1]', 'varchar(max)')  MessageOutput,
					t.x.value('JournalVoucherId[1]', 'INT')  JournalVoucherId
					from @XmlOutput.nodes('/TableResult') t(x);

					if (select COUNT(*) from @responseRevaluaiton WHERE Code = '999') > 0
					Begin	
						declare @Message as VARCHAR(MAX)
						set @Message = (select STRING_AGG(MessageResult, ', ')  from @responseRevaluaiton)
						
						SELECT '999' AS CodeMessage,
								@Message AS Message,
								0 IdVoucherTransaction,
								CAST(3 AS TINYINT) AS [Status];
						RETURN
					End 

					insert into @TableResultTRMAdjustment
					SELECT top 1 'Se generó el Comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name + CHAR(13) + CHAR(10)
					from GeneralLedger.JournalVoucherTypes jvt
					inner join GeneralLedger.CompanySettings cs on cs.ProfitLostJournalVoucherTypeId = jvt.id	
					END
				/*-----------------------------------------------------------*/
				
				-- Interfaz presupuesto
				IF EXISTS 
				(
					SELECT 1 
					FROM Treasury.VoucherTransactionDetails vtd 
					JOIN Treasury.DischargeBill db ON vtd.Id = db.IdVoucherTransactionD 
					JOIN Treasury.DischargeBillBudget dbb ON db.Id = dbb.DischargeBillId
					WHERE vtd.IdVoucherTransaction = @IdVoucherTransaction
				)
				BEGIN
					EXEC Budget.SP_GeneratePaymentOrder @IdVoucherTransaction, @CodeVoucherTransaction, 'VoucherTransaction', @User, @CodeMessageResultPaymentOrder OUTPUT, @MessageResultPaymentOrder OUTPUT

					IF @CodeMessageResultPaymentOrder <> 0
					BEGIN
						SELECT '999' AS CodeMessage, 
							   @MessageResultPaymentOrder AS Message,
							   0 IdVoucherTransaction,
							   CAST(3 AS TINYINT) AS [Status]
						RETURN
					END
				END
			end
			else if @VoucherClass = 2 begin--Reembolsos

				--se actualiza la entidad de reembolsos
				update Treasury.Refunds set Refunded = 1
				from @VoucherTransactionDetails vtd inner join 
				Treasury.Refunds r on vtd.CashRegisterId = r.IdCashRegister
				where vtd.ChangeTracker <>'Deleted' and r.[Status] = 2

				--se inserta el regstro en la tabla de balances de tesoreria
				INSERT INTO [Treasury].[TreasuryBalance]
				   ([DocumentNumber]
				   ,[DocumentDate]
				   ,[DocumentType]
				   ,[Nature]
				   ,[CashRegisterId]
				   ,[EntityBankAccountId]
				   ,[PreviousBalance]
				   ,[ValueMovement]
				   ,[CreationDate])
				   select @CodeVoucherTransaction,@DocumentDate,2,1,cr.Id,null,cr.CurrentBalance,r.Value,[Common].[GETDATE]() from @VoucherTransactionDetails vtd
				   inner join Treasury.Refunds r on vtd.CashRegisterId = r.IdCashRegister AND r.Refunded = 0
				   inner join Treasury.CashRegisters cr on vtd.CashRegisterId = cr.Id
				   where vtd.ChangeTracker <>'Deleted' and r.[Status] = 2

				--se actualiza el saldo actual de la caja
				update Treasury.CashRegisters set CurrentBalance += r.Value
				from @VoucherTransactionDetails vtd
				inner join Treasury.Refunds r on vtd.CashRegisterId = r.IdCashRegister and r.Id = (select max(Id) from Treasury.Refunds where IdCashRegister = vtd.CashRegisterId)
				inner join Treasury.CashRegisters cr on vtd.CashRegisterId = cr.Id
				where vtd.ChangeTracker <>'Deleted' and r.[Status] = 2

				--se actualiza el estado de la cabera a reembolsado
				update Treasury.VoucherTransaction set RefundCashRegisterExpense = 1
				from @VoucherTransactionDetails vtd
				inner join Treasury.VoucherTransaction vt on vtd.IdVoucherTransaction = vt.Id
				inner join  Treasury.Refunds r on vt.IdRefund = r.id
				where r.IdCashRegister = @IdCashRegister and vt.RefundCashRegisterExpense= 0

			end
			else if @VoucherClass = 3 begin--Traslados

				if @ExpenseType = 1 begin --Entre cuentas bancarias

					INSERT INTO [Treasury].[TreasuryBalance]
					([DocumentNumber]
					,[DocumentDate]
					,[DocumentType]
					,[Nature]
					,[CashRegisterId]
					,[EntityBankAccountId]
					,[PreviousBalance]
					,[ValueMovement]
					,[CreationDate])
					select @CodeVoucherTransaction,@DocumentDate,2,1,null,eba.id, eba.CurrentBalance, sum(vtd.Value),[Common].[GETDATE]() 
					from @VoucherTransactionDetails vtd
					inner join Treasury.EntityBankAccounts eba on vtd.IdEntityBankAccount = eba.Id					   
					where vtd.ChangeTracker <>'Deleted' 
					group by eba.id, eba.CurrentBalance

					update eba set eba.CurrentBalance += vtd_sum.Value
					from Treasury.EntityBankAccounts eba
					inner join (
						select eba2.Id, sum(vtd.Value) as Value
						from @VoucherTransactionDetails vtd
						inner join Treasury.EntityBankAccounts eba2 on vtd.IdEntityBankAccount = eba2.Id
						where vtd.ChangeTracker <> 'Deleted'
						group by eba2.Id
					) as vtd_sum on eba.Id = vtd_sum.Id
				end
				else if @ExpenseType = 3 begin
					--se inserta el regstro en la tabla de balances de tesoreria
					INSERT INTO [Treasury].[TreasuryBalance]
				   ([DocumentNumber]
				   ,[DocumentDate]
				   ,[DocumentType]
				   ,[Nature]
				   ,[CashRegisterId]
				   ,[EntityBankAccountId]
				   ,[PreviousBalance]
				   ,[ValueMovement]
				   ,[CreationDate])
				   select @CodeVoucherTransaction,@DocumentDate,2,1,cr.Id,null,cr.CurrentBalance,vtd.Value,[Common].[GETDATE]() 
				   from @VoucherTransactionDetails vtd				   
				   inner join Treasury.CashRegisters cr on vtd.CashRegisterId = cr.Id
				   where vtd.ChangeTracker <>'Deleted' 

				   --se actualiza el saldo actual de la caja
					update Treasury.CashRegisters set CurrentBalance += vtd.Value
					from @VoucherTransactionDetails vtd					
					inner join Treasury.CashRegisters cr on vtd.CashRegisterId = cr.Id
					where vtd.ChangeTracker <>'Deleted'
				end
			end

			--genereracion comprobante contable
			declare @JournalVouchers as table(
			[Id] [int] NOT NULL,
			[AccountingMovementId] [int] NOT NULL,
			[Consecutive] [bigint] NOT NULL,
			[LegalBookId] [int] NOT NULL ,
			[IdJournalVoucher] [int] NOT NULL,
			[VoucherDate] [datetime] NOT NULL,
			[Imported] [bit] NOT NULL,
			[Status] [tinyint] NOT NULL,
			[Detail] [varchar](500) NULL ,
			[EntityCode] [varchar](20) NULL,
			[EntityId] [int] NULL,
			[EntityName] [varchar](250) NULL,
			[IsClosedYear] [bit] NOT NULL,
			[CreationUser] [varchar](20) NOT NULL,
			[CreationDate] [datetime] NOT NULL,
			[ModificationUser] [varchar](20) NULL,
			[ModificationDate] [datetime] NULL,
			[ConfirmationUser] [varchar](20) NULL,
			[ConfirmationDate] [datetime] NULL,
			[CurrencyId]INT)

			declare @JournalVoucherDetails as table(
			[Id] [int] NOT NULL,
			[IdAccounting] [int] NOT NULL,
			[IdMainAccount] [int] NOT NULL,
			[IdThirdParty] [int] NULL,
			[IdCostCenter] [int] NULL,
			[DebitValue] [decimal](18, 5) NOT NULL ,
			[CreditValue] [decimal](18, 5) NOT NULL ,
			[Detail] [varchar](max) NULL ,
			[IdRetention] [int] NULL,
			[RetentionRate] [decimal](5, 3) NULL,
			[BaseValue] [decimal](18, 2) NULL ,
			[BillingValue] [decimal](18, 2) NULL)
			--fin generacion comprobante contable

			--Obtengo el libro oficial
			declare @LegalBookId integer
			select @LegalBookId = id  from GeneralLedger.LegalBook where OfficialBook = 1

			--se inserta la cabecera del comprobante contable
			insert into @JournalVouchers 
			select 0,0,0,@LegalBookId,JournalVoucherTypeVoucherTransaction,@DocumentDate ,0,2,@Detail,@CodeVoucherTransaction,@IdVoucherTransaction,'VoucherTransaction',0,@User,[Common].[GETDATE](),@User,[Common].[GETDATE](),@User,[Common].[GETDATE](),@CurrencyIdHeader
			from Treasury.SettingsTreasury where IdOperatingUnit =@IdUnitOperative 

			--se insertan los detalle 
			insert into @JournalVoucherDetails 
			select 0,0,vtd.IdMainAccount,
			case when ma.HandlesThirdParty = 1 
			then
				case 
					when @VoucherClass = 2 then cr.ThirdPartyId --refund
					when @VoucherClass = 1 then vtd.IdThirdParty --payment
					when @ExpenseType=1 then eba.ThirdPartyId --bank account
					else cr.ThirdPartyId 
				end
			else
				null
			end,
			case 
				when ma.HandlesCostCenter = 1 then vtd.IdCostCenter 
			else 
				null 
			end,
			case 
				when vtd.Nature  =  1 then IIF(vtd.TaxRegistration = 1,vtd.TotalConcept,vtd.Value )
			else 
				0 
			end as DebitValue,
			case 
				when vtd.Nature  =  1 then 0 
			else 
				IIF(vtd.TaxRegistration =1,vtd.TotalConcept,vtd.Value )
			end as CreditValue,
			vtd.Observation,
			vtd.IdRetentionConcept,
			vtd.PercentRetention,
			vtd.BaseValue,
			vtd.BillingValue			
			from @VoucherTransactionDetails vtd
			left join Treasury .EntityBankAccounts eba on vtd.IdEntityBankAccount = eba.id 
			left join Treasury.CashRegisters cr on vtd.CashRegisterId = cr.id 
			inner join GeneralLedger.MainAccounts ma on vtd.IdMainAccount = ma.Id
			where vtd.ChangeTracker <>'Deleted'

			
			if EXISTS(SELECT 1 
						FROM  @VoucherTransactionDetails vtd 
						inner join @DischargeBill db on vtd.Id = db.IdVoucherTransactionD
						WHERE db.BaseValueDiscount >0 and vtd.ChangeTracker <> 'Deleted')
			BEGIN
							
				--se insertan los detalle 
				insert into @JournalVoucherDetails 
				select 0,0,ma.Id,
				case when ma.HandlesThirdParty = 1 
				then
					case 
						when @VoucherClass = 2 then cr.ThirdPartyId --refund
						when @VoucherClass = 1 then vtd.IdThirdParty --payment
						when @ExpenseType=1 then eba.ThirdPartyId --bank account
						else cr.ThirdPartyId 
					end
				else
					null
				end,
				case 
					when ma.HandlesCostCenter = 1 then vtd.IdCostCenter 
				else 
					null 
				end,
				case 
					when ma.Nature  =  1 then (db.ValueDiscountInCurrencyHeader)
				else 
					0 
				end as DebitValue,
				case 
					when ma.Nature  =  1 then 0 
				else 
					(db.ValueDiscountInCurrencyHeader) 
				end as CreditValue,
				vtd.Observation,
				vtd.IdRetentionConcept,
				vtd.PercentRetention,
				vtd.BaseValue,
				vtd.BillingValue			
				from @VoucherTransactionDetails vtd
				left join Treasury .EntityBankAccounts eba WITH(NOLOCK) on  vtd.IdEntityBankAccount = eba.id 
				left join Treasury.CashRegisters cr WITH(NOLOCK) on vtd.CashRegisterId = cr.id 
				inner join @DischargeBill db on vtd.Id = db.IdVoucherTransactionD
				inner join Payments.AccountPayable ap WITH(NOLOCK) on ap.Id=db.IdAccountPayable
				inner join Common.SuppliersDistributionLines sdl WITH(NOLOCK) on ap.IdSuppliersDistributionLines =sdl.Id
				inner join Common.DistributionLines dl WITH(NOLOCK) on sdl.IdDistributionLine=dl.Id
				inner JOIN Payments.AccountPayableConceptNotes apcn with(NOLOCK) on dl.AccountPayableConceptNotesId=apcn.Id
				inner join GeneralLedger.MainAccounts ma  WITH(NOLOCK) on apcn.IdAccount = ma.Id
				where vtd.ChangeTracker <>'Deleted'

			END
			 
			--se inserta un detalle con la cuenta de la cabecera
			if @Value <> 0 begin
			
				insert into @JournalVoucherDetails
				select 0,0,@IdMainAccount,
				case when ma.HandlesThirdParty = 1 
					then
						case when @VoucherClass = 1 then --Pagos
							case when @ExpenseType=1 then --bank account
								case when st.GetThirdPartyBank = 1 then
									eba.ThirdPartyId 
								else
									@IdThirdParty
								end
							else
								case when st.GetThirdPartyCashRegister = 1 then
									cr.ThirdPartyId 
								else
									@IdThirdParty
								end
							end
						else
							case when @ExpenseType = 1 then
								eba.ThirdPartyId
							else
								cr.ThirdPartyId
							end
						end				
					else
						null
				end,
				case 
					when ma.HandlesCostCenter = 1 then vt.IdCostCenter 
				else 
					null 
				end,
				0,
				@Value + @TaxByMilValue,
				case 
					when @PaymentMethod = 1 then 'Comprobante de Egreso: '+@CodeVoucherTransaction+', Cheque # '+cast(@CheckNumber as varchar(50))
					when @PaymentMethod = 2 then 'Comprobante de Egreso: '+@CodeVoucherTransaction+', Nota Debito # '+@NoteNumber
					else
						'Comprobante de Egreso: '+@CodeVoucherTransaction
				end,
				null,
				0,0,0
				from Treasury .VoucherTransaction vt
				INNER JOIN GeneralLedger.MainAccounts ma on vt.IdMainAccount =ma.Id
				INNER JOIN Treasury.SettingsTreasury st on vt.IdUnitOperative = st.IdOperatingUnit
				INNER JOIN (	SELECT vtd.IdVoucherTransaction,sum(vtd.TotalConcept) TotalConcept
								from @VoucherTransactionDetails vtd
								where  vtd.ChangeTracker <>'Deleted' 
								GROUP by vtd.IdVoucherTransaction ) vtd on vtd.IdVoucherTransaction= vt.Id
				LEFT JOIN Treasury.CashRegisters cr on vt.IdCashRegister = cr.id
				LEFT JOIN Treasury.EntityBankAccounts eba on vt.IdEntityBankAccount = eba.Id
				where vt.id =@IdVoucherTransaction

				
			end

			--si tiene tasa por mil
			if @TaxByMil <> 0 begin 
				insert into @JournalVoucherDetails
				select 0,0,eba.FMGCounterpartMainAccountId,
				case when ma.HandlesThirdParty =1 then eba.FMGCounterpartThirdPartyId 
				else null 
				end,
				case when ma.HandlesCostCenter = 1 then eba.FMGCounterpartCostCenterId
				else null
				end,
				0,
				@TaxByMilValue,
				@Detail,
				null,
				0,0,0
				from Treasury .VoucherTransaction vt				
				inner join Treasury.EntityBankAccounts eba on vt.IdEntityBankAccount = eba.Id
				inner join GeneralLedger.MainAccounts ma on eba.FMGCounterpartMainAccountId = ma.Id
				where vt.id =@IdVoucherTransaction

				insert into @JournalVoucherDetails
				select 0,0,eba.FMGExpenseMainAccountId,
				case when ma.HandlesThirdParty =1 then eba.FMGExpenseMainAccountId 
				else null 
				end,
				case when ma.HandlesCostCenter = 1 then eba.FMGExpenseMainAccountId
				else null
				end,
				@TaxByMilValue,
				0,
				@Detail,
				null,
				0,0,0
				from Treasury .VoucherTransaction vt				
				inner join Treasury.EntityBankAccounts eba on vt.IdEntityBankAccount = eba.Id
				inner join GeneralLedger.MainAccounts ma on eba.FMGExpenseMainAccountId = ma.Id
				where vt.id =@IdVoucherTransaction
			end

					insert into @JournalVoucherDetails
						select  
							0,
							0,
							giva.IdAccountDebitControlFiscal,
							vtd.IdThirdParty,
							null,
							vtd.ValueIVA,
							0,
							null,
							null,
							null,
							0,
							0
						from Treasury.VoucherTransactionDetails vtd
						join GeneralLedger.GeneralLedgerIVA giva on vtd.IdGeneralLedgerIVA = giva.Id
						join GeneralLedger.MainAccounts ma on giva.IdAccountDebitControlFiscal = ma.Id
						where IdVoucherTransaction = @IdVoucherTransaction and vtd.TaxRegistration =1
						and vtd.ValueIVA > 0

						insert into @JournalVoucherDetails
						select  
							0,
							0,
							giva.IdAccountCreditControlFiscal,
							vtd.IdThirdParty,
							null,
							0,
							vtd.ValueIVA,
							null,
							null,
							null,
							0,
							0
						from Treasury.VoucherTransactionDetails vtd
						join GeneralLedger.GeneralLedgerIVA giva on vtd.IdGeneralLedgerIVA = giva.Id
						join GeneralLedger.MainAccounts ma on giva.IdAccountDebitControlFiscal = ma.Id
						where IdVoucherTransaction = @IdVoucherTransaction and vtd.TaxRegistration =1
						and vtd.ValueIVA > 0
			--------------------------------------------------------------------------------
			

						insert into @JournalVoucherDetails
						select  
							0,
							0,
							ma.Id,
							case 
							when ma.HandlesThirdParty = 1 then
								case 
									when @VoucherClass = 2 then cr.ThirdPartyId --refund
									when @VoucherClass = 1 then vtd.IdThirdParty --payment
									when @ExpenseType=1 then eba.ThirdPartyId --bank account
									else cr.ThirdPartyId 
								end
							else
								null
							end,--- tercero
							case 
								when ma.HandlesCostCenter = 1 then vtd.IdCostCenter 
							else 
								null 
							end,--- centro de costo
							iif(vtd.Nature = 1,vtd.ValueIVA,0), --deb
							iif(vtd.Nature = 1,0,vtd.ValueIVA), --cred
							CONCAT('IVA ',giva.Percentage,' % ',' - ',giva.Name), --detail
							null,
							null,
							vtd.Value, --base
							0
						from Treasury.VoucherTransactionDetails vtd
						join GeneralLedger.GeneralLedgerIVA giva on vtd.IdGeneralLedgerIVA = giva.Id
						join GeneralLedger.MainAccounts ma on vtd.IdMainAccount = ma.Id
						left join Treasury .EntityBankAccounts eba on vtd.IdEntityBankAccount = eba.id 
						left join Treasury.CashRegisters cr on vtd.CashRegisterId = cr.id 
						where IdVoucherTransaction = @IdVoucherTransaction and vtd.TaxRegistration not in (1,2)
						and vtd.ValueIVA > 0
			--------------------------------------------------------------------------------
					insert into @JournalVoucherDetails
						select  
							0,
							0,
							giva.IdAccountPurchaseService,
							vtd.IdThirdParty,
							null,
							vtd.ValueIVA,
							0,
							null,
							null,
							null,
							0,
							0
						from Treasury.VoucherTransactionDetails vtd
						join GeneralLedger.GeneralLedgerIVA giva on vtd.IdGeneralLedgerIVA = giva.Id
						join GeneralLedger.MainAccounts ma on giva.IdAccountDebitControlFiscal = ma.Id
						where IdVoucherTransaction = @IdVoucherTransaction and vtd.TaxRegistration =2
						and vtd.ValueIVA > 0
			------------------------------------------------------------------------------------------------------------------------
			
			declare @JournalVoucherXML as XML
			select @JournalVoucherXML =  convert(xml, (select * from @JournalVouchers JournalVoucher inner join @JournalVoucherDetails JournalVoucherDetail on JournalVoucher.id = JournalVoucherDetail.IdAccounting   For xml AUTO,TYPE, ELEMENTS))
			

			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@User 
		
			if (select code  from @resultJournalVoucher) = '999' 
			Begin			
				declare @errorJV varchar(max)
				select @errorJV = MessageResult  from @resultJournalVoucher 
				select '999' as CodeMessage,@errorJV as Message, 0 IdVoucherTransaction,CAST(3 AS TINYINT) AS [Status]
				return 
			End  

			declare @JournalVoucherId as int
			select @JournalVoucherId = IdJournalVoucher  from @resultJournalVoucher
		end

		--**********************************FIN CONFIRMACION*************************************************************************
		--retorno del mensaje
		if(@Status =2) begin
		  
			--retorno cuando el recibo de caja se confirma
			DECLARE @TableResult as table([message] varchar(300))

			insert into @TableResult 
				select 'Se guardó y se confirmó el comprobante de egreso con código ' + @CodeVoucherTransaction + CHAR(13) + CHAR(10)
						
			insert into @TableResult
				SELECT	'Se generó el Comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name + CHAR(13) + CHAR(10)
				FROM Treasury.SettingsTreasury st
				JOIN GeneralLedger.JournalVoucherTypes jvt on st.JournalVoucherTypeVoucherTransaction = jvt.Id
				WHERE st.IdOperatingUnit = @IdUnitOperative
			
			insert into @TableResult
				SELECT *
				from @TableResultTRMAdjustment
			
			IF ISNULL(@MessageResultPaymentOrder, '') <> ''
			BEGIN
				INSERT INTO @TableResult 
					SELECT @MessageResultPaymentOrder + CHAR(13) + CHAR(10)
			END			

			declare @mesaggeResult varchar(max)
			--retorno el mensaje 
			SELECT @mesaggeResult = COALESCE(@mesaggeResult + '', '') + [message]  from @TableResult				
			SELECT '0' AS CodeMessage,@mesaggeResult AS Message,@IdVoucherTransaction as IdVoucherTransaction,CAST(1 AS TINYINT) AS [Status];
			RETURN;
		  end
		  else begin
			SELECT '0' AS CodeMessage,'' AS Message,@IdVoucherTransaction as IdVoucherTransaction,cast(1 as tinyint) as [Status];
			 RETURN;		
		  end

		
	END TRY
    BEGIN CATCH
        SELECT '999' AS CodeMessage,ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message,0 AS IdVoucherTransaction,CAST(3 AS TINYINT) AS [Status];
    END CATCH	

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra y actualiza comprobantes de egreso en el módulo de Tesorería, procesando la cabecera del comprobante y sus líneas de detalle a partir de un XML de entrada. Gestiona pagos a terceros vinculando cuentas bancarias de la entidad (EntityBankAccounts), cajas registradoras (CashRegisters) y conceptos de gasto (ExpenseConcepts), incluyendo retenciones, IVA, ICA y otros impuestos asociados a cada movimiento financiero. Además, maneja el cruce de facturas pendientes de pago (descarga de cuentas por pagar), anticipos de tesorería y órdenes de pago, generando el comprobante contable correspondiente en el libro mayor. Existe para centralizar la creación y modificación de todos los movimientos de egreso, reembolsos y pagos de caja, garantizando la trazabilidad contable y presupuestal de cada transacción.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveVoucherTransaction';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveVoucherTransaction';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste, confirma o anula comprobantes de egreso de tesorería (pagos, reembolsos y traslados), validando reglas de negocio, afectando saldos de cuentas/cajas y cuentas por pagar, generando anticipos, ajustes por TRM, orden de pago presupuestal y el comprobante contable.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveVoucherTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /VoucherTransaction con la cabecera y opcionalmente VoucherTransactionDetails, DischargeBill, DischargeBillBudget, VoucherTransactionAdvance y TreasuryAdvances; Debe existir un libro oficial en GeneralLedger.LegalBook (OfficialBook=1); El mes/año de DocumentDate debe estar abierto en GeneralLedger.ClosedMonth (Status=1); Debe existir parametrización Treasury.SettingsTreasury para la unidad operativa indicada; Si se está confirmando (Status=2), el comprobante no debe estar previamente confirmado; Para crear un nuevo comprobante (Id=0 sin Code) debe existir Treasury.TreasurySequence con IdForm=636 y un detalle aplicable según Scope (O=prefijo, OU=unidad operativa, CC=clase de comprobante); Si ExpenseType=1 (banco), debe haberse seleccionado IdEntityBankAccount y método de pago; si PaymentMethod=1 requiere IdChecks; si PaymentMethod=2 requiere NoteNumber; Si ExpenseType=2 ó 3 (caja menor/traslado caja), debe haberse seleccionado IdCashRegister; VoucherClass=1 (pago) requiere IdThirdParty distinto de 0; Debe existir al menos un detalle no eliminado en VoucherTransactionDetails; Los conceptos de gasto referenciados deben estar activos (ExpenseConcepts.Status=1); Si BaseValueDiscount>0 en alguna factura, la línea de distribución debe tener cuenta contable de pronto pago parametrizada (AccountPayableConceptNotes.IdAccount no nulo)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveVoucherTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'comprobante de egreso; pagos a proveedores; anticipos a proveedores; anticipos de cartera; reembolsos de caja menor; traslados entre cuentas/cajas; cheques (control y anulación); cuentas por pagar y cuotas; cruce/descargue de facturas; descuentos por pronto pago; retenciones (RTE/IVA/ICA); tasa por mil (4xMil/GMF); comprobante contable / movimiento contable; secuencia numérica por prefijo/unidad operativa/clase; saldo de cuenta bancaria y caja; interfaz presupuestal (orden de pago); ajuste por diferencia en cambio (TRM); IVA descontable / control fiscal', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveVoucherTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Treasury.SP_SaveCheckNumber; Common.SP_InsertIntoExchangeRate; Payments.SP_AccountPayableRevaluation; Budget.SP_GeneratePaymentOrder; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; dbo.DecodeXmlToText; dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveVoucherTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.EntityBankAccounts; Treasury.CashRegisters; Treasury.ExpenseConcepts; GeneralLedger.CompanySettings; Treasury.VoucherTransaction; GeneralLedger.LegalBook; GeneralLedger.ClosedMonth; GeneralLedger.MainAccounts; Treasury.SettingsTreasury; Payments.AccountPayable; Common.SuppliersDistributionLines; Common.DistributionLines; Payments.AccountPayableConceptNotes; Common.ThirdParty; Common.Supplier; Payments.AccountPayableShares; Portfolio.PortfolioAdvance; Treasury.Refunds; Treasury.TreasurySequence; Treasury.TreasurySequenceDetail; Common.Sequense; Treasury.Checkbooks; Treasury.CheckBlock; Treasury.OutstandingChecks; Treasury.DischargeBill; Treasury.DischargeBillBudget; Treasury.VoucherTransactionDetails; Treasury.VoucherTransactionAdvance; Treasury.TreasuryAdvances; GeneralLedger.GeneralLedgerIVA (+2 adicionales)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveVoucherTransaction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveVoucherTransaction';
-- GO
