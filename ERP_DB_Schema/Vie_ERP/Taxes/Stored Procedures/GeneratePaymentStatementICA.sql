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