

CREATE  PROCEDURE [Glosas].[SubirSaldosCartera]

AS
BEGIN

	BEGIN TRY

begin transaction SALDOGLOSA


	
	
	CREATE TABLE #tablaValidaciones(
		Id integer identity(1,1),
		Mensaje  varchar(200),
		Factura varchar(20)
	)


	declare @count as integer = (select count(*) from dbo.[SALDOSCARTERA])
	--select @count
	declare @contador as integer = 0

	WHILE @contador < @count BEGIN

		set @contador = @contador + 1
		print 'holaa'
		declare @Code  varchar(20)
        declare @OperatingUnitId int = 1 --pitalito
        declare @AccountReceivableType tinyint = 2 --ley 100
        declare   @ThirdPartyId int
        declare   @CustomerId int
        declare  @SellerId int = null 
        declare   @InvoiceId int = null
        declare   @InvoiceNumber varchar(20)
        declare   @AccountReceivableDate datetime
        declare   @Term int = 30 --30 dias
        declare  @ExpiredDate datetime
        declare   @Observations varchar(300)
        declare   @PortfolioStatus tinyint --= 3 -- radicada confirmada  OJOOOOOOOO
        declare   @OpeningBalance bit = 1 --por saldo inicial
        declare   @RecognitionId int = null -- no presupuesto
        declare   @PaymentAgreement bit = 0 --no acuerdo de pago
        declare    @RegistrationAdjusted bit = 0 -- registro NO ajustado
        declare   @MainAccountWithoutFilingId int
        declare  @NumberShares int = 1 --1 cuota
        declare  @Value numeric(18,0)
        declare  @Balance numeric(18,0)
        declare   @Status tinyint = 2 --confirmado
        declare   @CostCenterId int = null
        declare   @InvoiceCategoryId int = 1 --GENERAL
        declare   @AccountWithoutRadicateId int
        declare   @AccountRadicateId int
        declare   @AccountObjectionRemediedId int = null
        declare   @AccountConciliationId int = null
        declare   @AccountLegalCollectionId int
        declare   @AccountDebtorOrder int 
        declare   @AccountCreditorOrder int 
        declare   @AffectBudget bit = 0 --no
        declare   @BudgetId int = null
        


		--variables temporales
		declare @nit as varchar(20)
		declare @CuentaSinRadicar as varchar(20)
	--	declare @CentroCOsto as varchar(20)
		declare @CuentaRadicar as varchar(20)
		declare @CuentaOrdenGlosa as varchar(20)
		declare @CuentaOrdenAcreedores as varchar(20)
		declare @CuentaTrasladojuridico as varchar(20)
		declare @EstadoFactura as varchar(20)

		declare @CuentaContableSaldo as varchar(20)
		--print 'holaa2'
		select  @code = Factura, @nit = Cliente, @InvoiceNumber = Factura,@AccountReceivableDate= Fecha ,@ExpiredDate = DATEADD(month, 1, Fecha),
		@Observations = Observacion, @CuentaSinRadicar = CuentaSinRadicar, @Value = Valor,@Balance = saldo,
		@CuentaRadicar =CuentaRadicada, @CuentaOrdenGlosa=CuentaOrdenGlosa,@CuentaOrdenAcreedores=CuentaAcreedoresGlosas ,@CuentaTrasladojuridico=CuentaJuridico
		,@CuentaContableSaldo = CuentaContableSaldo, @EstadoFactura = EstadoFactura  from dbo.SALDOSCARTERA where Id = @contador
		

	--	print 'holaa3'

		if (select count(*) from Common.ThirdParty where nit = @nit  ) = 0 begin
		  print 'holaa4'
		    insert into #tablaValidaciones 
			select 'el tercero no existe @nit: ' + convert(varchar(20), @nit) AS  Mensaje, @InvoiceNumber
		/*	select '999' as CodigoMensaje, 'No se encontro el tercero' + convert(varchar(20), @nit) AS  Mensaje
			rollback transaction SALDOGLOSA
			return */
		end

		if (select count(*) from Common.Customer where nit = @nit  ) = 0 begin
			insert into #tablaValidaciones 
			select 'el cliente no existe @nit: ' + convert(varchar(20), @nit) AS  Mensaje, @InvoiceNumber
		    /*print 'holaa5'
			select '999' as CodigoMensaje, 'No se encontro cliente' + convert(varchar(20), @nit) AS  Mensaje
			rollback transaction SALDOGLOSA
			return */
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaSinRadicar and LegalBookId = 1  ) = 0 begin
		   insert into #tablaValidaciones 
			select 'la cuenta sin radicar no existe @CuentaSinRadicar: ' + convert(varchar(20), @CuentaSinRadicar) AS  Mensaje, @InvoiceNumber
			/*print 'holaa6'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaSinRadicar' + convert(varchar(20), @CuentaSinRadicar) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaRadicar and LegalBookId = 1 ) = 0 begin
		  insert into #tablaValidaciones 
			select 'la cuenta radicADA no existe @CuentaRadicar: ' + convert(varchar(20), @CuentaRadicar) AS  Mensaje, @InvoiceNumber
		/*	print 'holaa7'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaRadicar' + convert(varchar(20), @CuentaRadicar) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaOrdenGlosa and LegalBookId = 1 ) = 0 begin
		  insert into #tablaValidaciones 
			select 'la cuenta ORDEN DE GLOSA no existe @CuentaOrdenGlosa: ' + convert(varchar(20), @CuentaOrdenGlosa) AS  Mensaje, @InvoiceNumber
		/*	print 'holaa7'
			print 'holaa8'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaOrdenGlosa' + convert(varchar(20), @CuentaOrdenGlosa) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaOrdenAcreedores  and LegalBookId = 1) = 0 begin
		  insert into #tablaValidaciones 
			select 'la cuenta ORDEN ACREEDORES no existe @@CuentaOrdenAcreedores: ' + convert(varchar(20), @CuentaOrdenAcreedores) AS  Mensaje, @InvoiceNumber
			/*print 'holaa9'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaOrdenAcreedores' + convert(varchar(20), @CuentaOrdenAcreedores) AS  Mensaje
			rollback transaction SALDOGLOSA
			return */
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaTrasladojuridico  and LegalBookId = 1) = 0 begin
		  insert into #tablaValidaciones 
			select 'la cuenta juridico no existe @CuentaTrasladojuridico: ' + convert(varchar(20), @CuentaTrasladojuridico) AS  Mensaje, @InvoiceNumber
			/*print 'holaa10'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaTrasladojuridico' + convert(varchar(20), @CuentaTrasladojuridico) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaContableSaldo and LegalBookId = 1 ) = 0 begin
		 insert into #tablaValidaciones 
			select 'la cuenta de saldo no existe @CuentaContableSaldo: ' + convert(varchar(20), @CuentaContableSaldo) AS  Mensaje, @InvoiceNumber
			/*print 'holaa11'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaContableSaldo' + convert(varchar(20), @CuentaContableSaldo) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end

		/*if (select count(*) from [Payroll].[CostCenter] where  Code = @CentroCOsto  ) = 0 begin
		 insert into #tablaValidaciones 
			select 'el centro de costo no existe @CentroCOsto: ' + convert(varchar(20), @CentroCOsto) AS  Mensaje, @InvoiceNumber
		/*	print 'holaa12'
			select '999' as CodigoMensaje, 'No se encontro centro de costo' + convert(varchar(20), isnull(@CentroCOsto,'')) AS  Mensaje
			rollback transaction SALDOGLOSA
			return*/
		end*/

		select @ThirdPartyId = id from Common.ThirdParty where nit = @nit   
		select @CustomerId = Id from Common.Customer where nit = @nit
		select @MainAccountWithoutFilingId = Id ,@AccountWithoutRadicateId = Id from GeneralLedger.MainAccounts where  Number = @CuentaSinRadicar and LegalBookId = 1
		--select @CostCenterId = Id from [Payroll].[CostCenter] where Code = @CentroCOsto 
		
		select @AccountRadicateId = Id from GeneralLedger.MainAccounts where  Number = @CuentaRadicar and LegalBookId = 1
		select @AccountDebtorOrder = Id from GeneralLedger.MainAccounts where  Number = @CuentaOrdenGlosa and LegalBookId = 1
		select @AccountCreditorOrder = Id from GeneralLedger.MainAccounts where  Number = @CuentaOrdenAcreedores and LegalBookId = 1
		select @AccountLegalCollectionId = Id from GeneralLedger.MainAccounts where  Number = @CuentaTrasladojuridico and LegalBookId = 1
		

		declare @MainAccountId int
		select @MainAccountId = Id from GeneralLedger.MainAccounts where  Number = @CuentaContableSaldo and LegalBookId = 1
				
	
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
           (@code
           ,@OperatingUnitId
           ,@AccountReceivableType
           ,@ThirdPartyId
           ,@CustomerId
           ,@SellerId
           ,@InvoiceId
           ,@InvoiceNumber
           ,@AccountReceivableDate
           ,@Term
           ,@ExpiredDate
           ,@Observations
           ,@EstadoFactura
           ,@OpeningBalance
           ,@RecognitionId
           ,@PaymentAgreement
           ,@RegistrationAdjusted
           ,@MainAccountWithoutFilingId
           ,@NumberShares
           ,@Value
           ,@Balance
           ,@Status
           ,@CostCenterId
           ,@InvoiceCategoryId
           ,@AccountWithoutRadicateId
           ,@AccountRadicateId
           ,@AccountObjectionRemediedId
           ,@AccountConciliationId
           ,@AccountLegalCollectionId
           ,@AccountDebtorOrder
           ,@AccountCreditorOrder
           ,@AffectBudget
           ,@BudgetId
           ,'999'
           ,'27/01/2016'
           ,null
           ,null
           ,'999'
           ,'27/01/2016'
           ,null
           ,null)

		   declare @AccountReceivableId as int
		   Select @AccountReceivableId = SCOPE_IDENTITY()

		  


	  INSERT INTO [Portfolio].[AccountReceivableAccounting]
           ([AccountReceivableId]
           ,[MainAccountId]
           ,[ThirdPartyId]
           ,[CostCenterId]
           ,[Value]
           ,[Balance])
     VALUES
           (@AccountReceivableId
           ,@MainAccountId
           ,@ThirdPartyId
           ,@CostCenterId
           ,@Balance
           ,@Balance)


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
           (@AccountReceivableId
           ,1
           ,@ExpiredDate  
           ,@Value
           ,@Balance
           ,0
           ,0
           ,0
           ,0
           ,0
           ,null
           ,0
           ,0
           ,0
           ,0
           ,0)
		   

		print @contador
	END 
	
	
	if (select COUNT(*) from #tablaValidaciones) > 0 begin
		select * from #tablaValidaciones
		rollback transaction SALDOGLOSA
		return 
	end  


      --Commit Transaction SALDOGLOSA
		rollback transaction SALDOGLOSA
	END TRY
	BEGIN CATCH
		rollback transaction SALDOGLOSA
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END