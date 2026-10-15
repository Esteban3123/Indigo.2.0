
CREATE PROCEDURE [Glosas].[SubirSaldosGlosas_FacturaYaCartera]

AS
BEGIN

	BEGIN TRY

begin transaction SALDOGLOSAYACARTERA

	CREATE TABLE #tablaValidaciones(
		Id integer identity(1,1),
		Mensaje  varchar(200),
		Factura varchar(20),
		valorTotal decimal(18,0),
		cartera decimal(18,0),
		Glosa decimal(18,0),
		valorSaldo decimal(18,0)
	)
		
	declare @count as integer = (select count(*) from [dbo].[SALDOSGLOSAS_ESTANCARTERA])
	--select @count
	declare @contador as integer = 0
	print @count
		print 'Entro'
	WHILE @contador < @count BEGIN

		set @contador = @contador + 1
		declare   @InvoiceNumber varchar(20)
		declare   @ThirdPartyId int
		declare  @Value numeric(18,0)
		declare  @Balance numeric(18,0)

		--variables temporales
		declare @nit as varchar(20)
		declare @CuentaContableSaldo as varchar(20)
		
		select  @nit = Cliente, @InvoiceNumber = Factura,@Value = Valor,@Balance = saldo,@CuentaContableSaldo = CuentaContableSaldo from dbo.[SALDOSGLOSAS_ESTANCARTERA] where Id = @contador


			
		if (select count(*) from GeneralLedger.MainAccounts where  Number = @CuentaContableSaldo and LegalBookId = 1  ) = 0 begin
			print 'holaa11'
			select '999' as CodigoMensaje, 'No se encontro cuenta @CuentaContableSaldo' + convert(varchar(20), @CuentaContableSaldo) AS  Mensaje
			rollback transaction SALDOGLOSAYACARTERA
			return
		end

		if (select count(*) from [Portfolio].[AccountReceivable] where  InvoiceNumber = @InvoiceNumber  ) = 0 begin
			print 'holaa12'
			select '999' as CodigoMensaje, 'No se encontro cuenta por cobrar para la factura' + convert(varchar(20), @InvoiceNumber) AS  Mensaje
			rollback transaction SALDOGLOSAYACARTERA
			return
		end

		
		declare @MainAccountId int
		select @MainAccountId = Id from GeneralLedger.MainAccounts where  Number = @CuentaContableSaldo and LegalBookId = 1
				

		declare @AccountReceivableId as int
		declare @ValorTotalActual as decimal(18,0)
		 declare @saldoCartera as decimal(18,0)
		select @AccountReceivableId = Id , @ThirdPartyId = ThirdPartyId ,@ValorTotalActual = Value,@saldoCartera = Balance  from  [Portfolio].[AccountReceivable] where InvoiceNumber = @InvoiceNumber 


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
           ,null
           ,@Balance
           ,@Balance)



		   declare @saldoActualizar as decimal(18,0)
		   select @saldoActualizar = sum(Balance) from [Portfolio].[AccountReceivableAccounting] where [AccountReceivableId] = @AccountReceivableId


		   if @saldoActualizar  > @ValorTotalActual begin
				insert into #tablaValidaciones 
				select 'El saldo total de movimiento no puede ser mayor al valor de la cuenta por cobrar: ' + convert(varchar(20), @InvoiceNumber) AS  Mensaje, @InvoiceNumber,  @ValorTotalActual,@saldoCartera,@Balance, @saldoActualizar
		   end 


		  /* declare @ValorCuota as decimal(18,0)
		   select @ValorCuota =Value  from Portfolio.AccountReceivableShare where AccountReceivableId = @AccountReceivableId 

		   if @saldoActualizar > @ValorCuota begin 
				 insert into #tablaValidaciones 
				select 'El saldo de la cuota no puede ser mayor al valor de la cuota AccountReceivableShare : ' + convert(varchar(20), @InvoiceNumber) AS  Mensaje, @InvoiceNumber,  @ValorCuota,@saldoActualizar,0,0
			end */

		   update Portfolio.AccountReceivable set Balance = @saldoActualizar where id = @AccountReceivableId

		  update Portfolio.AccountReceivableShare set Value = @saldoActualizar, Balance = @saldoActualizar where AccountReceivableId = @AccountReceivableId
		
	  
		print @contador
	END 
	
	
	if (select COUNT(*) from #tablaValidaciones) > 0 begin
		select * from #tablaValidaciones
		rollback transaction SALDOGLOSAYACARTERA
		return 
	end  


	 Commit Transaction SALDOGLOSAYACARTERA
	--	rollback transaction SALDOGLOSAYACARTERA
	END TRY
	BEGIN CATCH
		rollback transaction SALDOGLOSAYACARTERA
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END