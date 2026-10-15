
CREATE PROCEDURE [Glosas].[CorregirSaldoCartera]

AS
BEGIN

	BEGIN TRY

begin transaction CorregirSaldoCartera

	
	declare @count as integer = (select count(*) from [dbo].SALDOSGLOSAS_CORREGIRSALDOCARTERA)
	--select @count
	declare @contador as integer = 0

	WHILE @contador < @count BEGIN

		set @contador = @contador + 1
		declare   @InvoiceNumber varchar(20)
		declare  @Value numeric(18,0)
		declare  @Balance numeric(18,0)
		declare @MainAccountSinRadicar as varchar(20)
	
		select   @InvoiceNumber = Factura,@Value = Valor,@Balance = saldo,@MainAccountSinRadicar = cuentasaldo from dbo.SALDOSGLOSAS_CORREGIRSALDOCARTERA where Id = @contador

		if (select count(*) from GeneralLedger.MainAccounts where  Number = @MainAccountSinRadicar and LegalBookId = 1 ) = 0 begin
			print 'holaa11'
			select '999' as CodigoMensaje, 'No se encontro cuenta @MainAccountSinRadicar' + convert(varchar(20), @MainAccountSinRadicar) AS  Mensaje
			rollback transaction SALDOGLOSAYACARTERA
			return
		end
				
		declare @MainAccountId int
		select @MainAccountId = Id from GeneralLedger.MainAccounts where  Number = @MainAccountSinRadicar and LegalBookId = 1

		if (select count(*) from [Portfolio].[AccountReceivable] where  InvoiceNumber = @InvoiceNumber  ) = 0 begin
			print 'holaa12'
			select '999' as CodigoMensaje, 'No se encontro cuenta por cobrar para la factura' + convert(varchar(20), @InvoiceNumber) AS  Mensaje
			rollback transaction CorregirSaldoCartera
			return
		end

		
						

		declare @AccountReceivableId as int
		select @AccountReceivableId = Id  from  [Portfolio].[AccountReceivable] where InvoiceNumber = @InvoiceNumber 


		update [Portfolio].[AccountReceivableAccounting] set Balance = @Balance where AccountReceivableId = @AccountReceivableId AND MainAccountId = @MainAccountId

	


		declare @saldoActualizar as decimal(18,0)
		select @saldoActualizar = sum(Balance) from [Portfolio].[AccountReceivableAccounting] where [AccountReceivableId] = @AccountReceivableId


		 update Portfolio.AccountReceivable set Balance = @saldoActualizar where id = @AccountReceivableId

		 update Portfolio.AccountReceivableShare set Balance = @saldoActualizar where AccountReceivableId = @AccountReceivableId
		
	  
		print @contador
	END 
	
	
	

	 Commit Transaction CorregirSaldoCartera
		--rollback transaction CorregirSaldoCartera
	END TRY
	BEGIN CATCH
		rollback transaction CorregirSaldoCartera
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END