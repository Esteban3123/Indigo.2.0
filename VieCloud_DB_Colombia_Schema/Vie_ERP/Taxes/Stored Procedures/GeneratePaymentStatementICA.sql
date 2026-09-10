-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	Genera Registro de pago de la declaracion unica de impuesto de industria y comercio
-- =============================================
CREATE PROCEDURE [Taxes].[GeneratePaymentStatementICA]
		@Nit as varchar(20),
		@Valor as numeric(18,0),

		@Valor13 as numeric(18,0),
		@Valor14 as numeric(18,0),
		@Valor17 as numeric(18,0),
		@Valor18 as numeric(18,0),
		@Valor20 as numeric(18,0),
		@Valor21 as numeric(18,0)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Begin try

	declare @Code varchar(20)

	-- Consultamos la secuencia numerica del form
	declare @idSequenceDetail int
	declare @pattern varchar(300)
	declare @NextS int
	select @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  from Portfolio.PortfolioSequenceDetail bsd inner join Portfolio.PortfolioSequence bs on bs.Id = bsd.IdSequensePortfolioC inner join Common.Sequense cs on cs.Id = bsd.IdSequense
	where bs.IdForm = '687'
	if (@idSequenceDetail is null)
	Begin
		select 999 as CodeMessage, 'Secuencia no encontrada'  as Message, 0 Id, '' as CodeTransfer
		return
	End
	select @Code = dbo.GetSequence('',@pattern,@NextS)

	if @Code = '__ERROR_MAXVALUE__' begin
		select 999 as CodeMessage, 'La secuencia alcanzo su valor maximo'  as Message, 0 Id, '' as CodeTransfer
		return
	end

	update Portfolio.PortfolioSequenceDetail set [Next] += 1 where Id = @idSequenceDetail

		if @nit = '' begin
			select 999 as CodeMessage, 'Nit no puede ser vacio'  as Message, 0 Id, '' as CodeTransfer
			return
		end 
				
		declare @CustomerId as integer =(select ID from Common.Customer  where nit  = @nit)
		declare @terceroId as integer = (select  ID from Common.ThirdParty where Nit = @Nit)
		declare @OperatingUnitId as integer = (select top 1 ID from Common.OperatingUnit  )
		declare @FechaProceso as datetime = [Common].[GETDATE]()
		--declare @IdUsuario as integer = (select  ID from [Security].[User] where IdPerson = @IdPerson   )
		declare @IdUsuario as integer = (select  top 1 ID from [Security].[User]    )

		declare @IdCuenta as integer = (select Id from GeneralLedger.MainAccounts  where Number = '13050801' and LegalBookId =1)
		
		declare @Observacion as varchar(500) = 'Declaracion única de impuesto de industria y comercio'

				--consultamos Id del presupuesto
		declare @Idbudget as integer = (
		select B.id from Budget.BudgetaryEntity e   inner join 
		Budget.BudgetaryValidity v on e.id = v.BudgetaryEntityId inner join 
		Budget.BudgetHeader H on h.BudgetaryValidityId =  v.Id inner join
		Budget.Category C on C.BudgetaryValidityId = v.id inner join  
		Budget.Budget B on B.BudgetHeaderId = H.id  and B.CategoryId = C.id 
		 where e.Code = '0101NVA' and v.[Year] = 2016 and C.Code = '01010120001')

		INSERT INTO [Portfolio].[AccountReceivable]
           ([Code]
           ,[OperatingUnitId]
           ,[AccountReceivableType]
           ,[ThirdPartyId]
           ,[CustomerId]
           ,[SellerId]
           ,[InvoiceId]
           ,[InvoiceNumber]
           ,[AccountReceivableDate]
           ,[Term]
           ,[ExpiredDate]
           ,[Observations]
           ,[PortfolioStatus]
           ,[OpeningBalance]
           ,[RecognitionId]
           ,[PaymentAgreement]
           ,[RegistrationAdjusted]
           ,[MainAccountWithoutFilingId]
           ,[NumberShares]
           ,[Value]
           ,[Balance]
           ,[Status]
           ,[CostCenterId]
           ,[InvoiceCategoryId]
           ,[AccountWithoutRadicateId]
           ,[AccountRadicateId]
           ,[AccountObjectionRemediedId]
           ,[AccountConciliationId]
           ,[AccountLegalCollectionId]
           ,[AccountHardCollectionId]
           ,[AccountDebtorOrder]
           ,[AccountCreditorOrder]
           ,[AffectBudget]
           ,[BudgetId]
           ,[CreationUser]
           ,[CreationDate]
           ,[ModificationUser]
           ,[ModificationDate]
           ,[ConfirmationUser]
           ,[ConfirmationDate]
           ,[AnnulmentUser]
           ,[AnnulmentDate])
     VALUES
           (@Code
           ,@OperatingUnitId
           ,3 --impuestos
           ,@terceroId
           ,@CustomerId 
           ,null --<SellerId, int,>
           ,null --<InvoiceId, int,>
           ,@Code
           ,@FechaProceso
           ,30 --<Term, int,>
           ,dateadd(day,30,@FechaProceso)
           ,@Observacion
           ,1 -- sin radicar
           ,@Valor
           ,null--<RecognitionId, int,>
           ,0 --<PaymentAgreement, bit,>
           ,0 --<RegistrationAdjusted, bit,>
           ,@IdCuenta --<MainAccountWithoutFilingId, int,>
           ,1 --<NumberShares, int,>
           ,@Valor --<Value, numeric(18,0),>
           ,@Valor
           ,2 --<Status, tinyint,>
           ,null --<CostCenterId, int,>
           ,null --<InvoiceCategoryId, int,>
           ,null --<AccountWithoutRadicateId, int,>
           ,null --<AccountRadicateId, int,>
           ,null --<AccountObjectionRemediedId, int,>
           ,null --<AccountConciliationId, int,>
           ,null --<AccountLegalCollectionId, int,>
           ,null --<AccountHardCollectionId, int,>
           ,null --AccountDebtorOrder, int,>
           ,null --<AccountCreditorOrder, int,>
           ,1 ---<AffectBudget, bit,>
           ,@Idbudget  --<BudgetId, int,>
           ,@IdUsuario
           ,@FechaProceso 
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null)

	 declare @IdAccountRecivable as integer =SCOPE_IDENTITY()

	INSERT INTO [Portfolio].[AccountReceivableAccounting]
           ([AccountReceivableId]
           ,[MainAccountId]
           ,[ThirdPartyId]
           ,[CostCenterId]
           ,[Value]
           ,[Balance])
     VALUES
           (@IdAccountRecivable
           ,@IdCuenta
           ,@terceroId 
           ,null
           ,@Valor 
           ,@Valor)

	INSERT INTO [Portfolio].[AccountReceivableShare]
           ([AccountReceivableId]
           ,[Number]
           ,[ExpiredDate]
           ,[Value]
           ,[Balance]
           ,[DebitValue]
           ,[CreditValue]
           ,[TransferValue]
           ,[PaymentValue]
           ,[CrossingValue]
           ,[InterestPaymentDate]
           ,[InterestValue]
           ,[SurchargesValue]
           ,[CapitalRepaymentAgreement]
           ,[FinancialInterest]
           ,[RepaymentAgreementInterest])
     VALUES
           (@IdAccountRecivable
           ,1 --<Number, int,>
           ,dateadd(day,30,@FechaProceso) --<ExpiredDate, datetime,>
           ,@Valor --<Value, numeric(18,0),>
           ,@Valor --<Balance, numeric(18,0),>
           ,0 --<DebitValue, numeric(18,0),>
           ,0
           ,0
           ,0--<PaymentValue, numeric(18,0),>
           ,0--<CrossingValue, decimal(18,0),>
           ,null
           ,null --<InterestValue, numeric(18,0),>
           ,null--<SurchargesValue, numeric(18,0),>
           ,null--<CapitalRepaymentAgreement, numeric(18,0),>
           ,null--<FinancialInterest, numeric(18,0),>
           ,null)

			declare @TableJournalVoucher table(IdJournalVoucher int NOT NULL, VoucherDate datetime NOT NULL, Imported bit NOT NULL, [Status] tinyint NOT NULL, Detail varchar(500) NULL, EntityCode varchar(20) NULL, EntityId int NULL, EntityName varchar(250) NULL, IsClosedYear bit NOT NULL)
			declare @TableJournalVoucherDetail table(IdMainAccount int NOT NULL, IdThirdParty int NULL, IdCostCenter int NULL,DebitValue decimal(18, 2) NOT NULL,CreditValue decimal(18, 2) NOT NULL,Detail varchar(max) NULL,IdRetention int NULL,RetentionRate decimal(5, 2) NULL,BaseValue decimal(18, 0) NULL,BillingValue decimal(18, 0) NULL)

			declare @SalesJournalVoucherTypeId int
			select top 1 @SalesJournalVoucherTypeId = SalesJournalVoucherTypeId from Inventory.SettingInventory
				
				--PharmaceuticalDispensing

			---Inserto la cabecera del comprobante
			insert into @TableJournalVoucher
			values (@SalesJournalVoucherTypeId, @FechaProceso , 0, 2, 'Comprobante generado por: ' + @Observacion,@Code, @IdAccountRecivable, 'JournalVouchers', 0)

			declare @idcostcenter as integer = (select top 1 id from Payroll.CostCenter   )

			declare @ICAMainAccountId int = (select Id from GeneralLedger.MainAccounts where Number = '13050801')
			declare @ExpenseICAMainAccountId int = (select Id from GeneralLedger.MainAccounts where Number = '41050801')
			declare @BOMMainAccountId int = (select Id from GeneralLedger.MainAccounts where Number = '13056202')
			declare @ExpenseBOMMainAccountId int = (select Id from GeneralLedger.MainAccounts where Number = '41056202')
			declare @AVTMainAccountId int = (select Id from GeneralLedger.MainAccounts where Number = '13052101')
			declare @ExpenseAVTMainAccountId int = (select Id from GeneralLedger.MainAccounts where Number = '41052101')

			---- Inserto los movimientos del impuesto de industria y comercio
			insert into @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail,IdRetention,RetentionRate,BaseValue,BillingValue)
			values(@ICAMainAccountId, @terceroId, @idcostcenter, @Valor13, 0, @Observacion + @Code, null,null,null,null)

			insert into @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail,IdRetention,RetentionRate,BaseValue,BillingValue)
			values(@ExpenseICAMainAccountId, @terceroId, @idcostcenter, 0, @Valor13, @Observacion + @Code, null,null,null,null)

			---- Inserto los impuestos de avisos y tableros
			insert into @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail,IdRetention,RetentionRate,BaseValue,BillingValue)
			values(@AVTMainAccountId, @terceroId, @idcostcenter, @Valor14, 0, @Observacion + @Code, null,null,null,null)

			insert into @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail,IdRetention,RetentionRate,BaseValue,BillingValue)
			values(@ExpenseAVTMainAccountId, @terceroId, @idcostcenter, 0, @Valor14, @Observacion + @Code, null,null,null,null)

			---- Inserto la sobretasa bomberil
			insert into @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail,IdRetention,RetentionRate,BaseValue,BillingValue)
			values(@BOMMainAccountId, @terceroId, @idcostcenter, @Valor17, 0, @Observacion + @Code, null,null,null,null)

			insert into @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail,IdRetention,RetentionRate,BaseValue,BillingValue)
			values(@ExpenseBOMMainAccountId, @terceroId, @idcostcenter, 0, @Valor17, @Observacion + @Code, null,null,null,null)
			

			declare @JournalXml xml =(select *
			from @TableJournalVoucher as JournalVoucher
			CROSS APPLY @TableJournalVoucherDetail as JournalVoucherDetail
			for xml auto, elements)
			--select @JournalXml
			declare @TableResultJournal table(CodeMessage varchar(20), Message varchar(max), IdJournalVoucher int)
			insert into @TableResultJournal
			exec [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement] @JournalXml, @IdUsuario
					
			if(select count(*) from @TableResultJournal where CodeMessage <> '0') > 0 begin
				select CodeMessage, Message, 0 as Id, '' as CodeTransfer from @TableResultJournal
				return
			end

		   select 111 as CodeMessage, 'Transaccion Exitosa' as Message, @IdAccountRecivable as Id, @Code as CodeTransfer
	end try
	begin catch
		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id, '' as CodeTransfer
	end catch

	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el registro de pago de la declaración única del Impuesto de Industria y Comercio (ICA). Crea una cuenta por cobrar en cartera (tipo impuestos) asociando el NIT del contribuyente —buscado tanto en clientes como en terceros— con la unidad operativa principal, la cuenta contable 13050801 del libro legal y el presupuesto vigente de la entidad presupuestaria. Registra los movimientos contables desagregados por tarifa (ítems 13, 14, 17, 18, 20 y 21) y utiliza una secuencia numérica del formulario 687 para generar el código del documento. Existe para formalizar dentro del sistema financiero el pago de la obligación tributaria de ICA, afectando presupuesto y dejando trazabilidad contable y de cartera.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'GeneratePaymentStatementICA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'GeneratePaymentStatementICA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la cuenta por cobrar y el comprobante contable correspondientes al pago de la declaración única del impuesto de industria y comercio (ICA), avisos y tableros y sobretasa bomberil para un tercero.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'GeneratePaymentStatementICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una secuencia configurada en Portfolio.PortfolioSequence para IdForm=''687''.; La secuencia generada no debe haber alcanzado su valor máximo.; El NIT recibido no puede estar vacío.; Deben existir las cuentas contables PUC 13050801, 41050801, 13056202, 41056202, 13052101 y 41052101 en GeneralLedger.MainAccounts.; Debe existir al menos una OperatingUnit, un usuario en Security.User y un CostCenter en Payroll.CostCenter.; Debe existir un presupuesto configurado para entidad ''0101NVA'', año 2016 y categoría ''01010120001''.; Debe existir configuración de SalesJournalVoucherTypeId en Inventory.SettingInventory.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'GeneratePaymentStatementICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código generado para la cuenta por cobrar también se usa como InvoiceNumber.; Las cuentas por cobrar de impuestos se crean siempre con AccountReceivableType=3, PortfolioStatus=1 (sin radicar), Status=2, NumberShares=1 y AffectBudget=1.; La fecha de vencimiento siempre es 30 días después de la fecha de proceso.; Por cada concepto tributario (ICA, AVT, Bomberil) se generan asientos contables balanceados (un débito y un crédito por el mismo valor).; El presupuesto afectado se resuelve siempre para la vigencia 2016, entidad ''0101NVA'' y categoría ''01010120001''.; Los valores @Valor18, @Valor20 y @Valor21 son recibidos pero no se utilizan en ningún asiento ni inserción.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'GeneratePaymentStatementICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Impuesto de industria y comercio (ICA); Avisos y tableros; Sobretasa bomberil; Declaración única de impuestos; Cuenta por cobrar; Cuotas de cartera; Comprobante contable; Presupuesto y vigencia presupuestal; Tercero / NIT; Secuencia/consecutivo de formulario', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'GeneratePaymentStatementICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Portfolio.PortfolioSequenceDetail: Cuando se obtiene una secuencia válida, se incrementa en 1 el campo [Next] del detalle de secuencia usado.; [INSERT] Portfolio.AccountReceivable: Tras generar el código de secuencia y validar el NIT, se inserta una cuenta por cobrar tipo 3 (impuestos), estado 2, PortfolioStatus=1 (sin radicar), término 30 días, AffectBudget=1 y MainAccountWithoutFilingId apuntando a la cuenta 13050801.; [INSERT] Portfolio.AccountReceivableAccounting: Por cada AccountReceivable creado se inserta un registro contable con la cuenta 13050801, el tercero y el valor total de la declaración.; [INSERT] Portfolio.AccountReceivableShare: Se inserta una única cuota (Number=1) con vencimiento a 30 días desde la fecha de proceso y valor/saldo iguales al total declarado.; [EXECUTE] GeneralLedger.JournalVoucher: Se arma un comprobante contable con seis movimientos (débito/crédito por ICA con @Valor13, AVT con @Valor14 y bomberil con @Valor17) y se invoca el SP de creación y validación; si retorna CodeMessage distinto de ''0'' se aborta devolviendo el error.; [RETURN_RESULT] RESULT: Devuelve CodeMessage=111 y ''Transaccion Exitosa'' con Id=AccountReceivable y CodeTransfer=código generado cuando todo el flujo concluye sin errores; en cualquier validación fallida o excepción retorna CodeMessage=999.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'GeneratePaymentStatementICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No se encuentra detalle de secuencia para IdForm=''687'' → Retorna CodeMessage=999 ''Secuencia no encontrada'' y termina.; si GetSequence retorna ''__ERROR_MAXVALUE__'' → Retorna CodeMessage=999 ''La secuencia alcanzo su valor maximo'' y termina.; si @Nit es cadena vacía → Retorna CodeMessage=999 ''Nit no puede ser vacio'' y termina.; si El SP de creación de comprobante devuelve algún CodeMessage distinto de ''0'' → Retorna los mensajes de error del comprobante y termina sin reportar éxito.; si Ocurre cualquier excepción capturada por el TRY/CATCH → Retorna CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'GeneratePaymentStatementICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'GeneratePaymentStatementICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioSequenceDetail; Portfolio.PortfolioSequence; Common.Sequense; Common.Customer; Common.ThirdParty; Common.OperatingUnit; Security.User; GeneralLedger.MainAccounts; Budget.BudgetaryEntity; Budget.BudgetaryValidity; Budget.BudgetHeader; Budget.Category; Budget.Budget; Inventory.SettingInventory; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'GeneratePaymentStatementICA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'GeneratePaymentStatementICA';
-- GO
