


-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 09/12/2015
-- Description:	Procedimiento que se encarga de validar el traslado
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ValidateTransfers_Output] 
	@PortfolioTransferXml as Xml,
	@Confirm as bit,
	@CodeMessage Int Output,
	@Message Varchar(Max) Output,
	@IdTransfer Int Output
AS
BEGIN
	--Se declaran las variables para obtener la cabecera
	declare @Id int, @Code varchar(20), @DocumentDate datetime, @CustomerId int, @ThirdPartyId int, @PortfolioAdvanceId int, @TransferType tinyint, @MainAccountId int, @CostCenterId int, @Observations varchar(300), @OperatingUnitId int, @Status tinyint
	
	--Tabla temporal para obtener los detalles de portfolioTransferDetail
	declare @PortfolioTransferDetail table(Id int, PortfolioTrasferId int, AccountReceivableId int, MainAccountId int, CostCenterId int, Value decimal(18,0))
	
	--Tabla temporal para obtener los detalles de portfolioTransferOtherConcept
	declare @PortfolioTransferOtherConcept table(Id int, PortfolioTrasferId int, PortfolioNoteConceptId int, MainAccountId int, CostCenterId int, Nature tinyint, Value decimal(18,0))

	--begin transaction
	Begin try
		--Se obtiene la cabecera del xml(PortfolioTransfer)
		select 
		@Id = t.x.value('Id[1]','int'),
		@Code = t.x.value('Code[1]','varchar(20)'),
		@DocumentDate = convert(datetime, t.x.value('DocumentDate[1]','varchar(20)'), 103),
		@CustomerId = t.x.value('CustomerId[1]','int'),
		@ThirdPartyId = t.x.value('ThirdPartyId[1]','int'),
		@PortfolioAdvanceId = t.x.value('PortfolioAdvanceId[1]','int'),
		@TransferType = t.x.value('TransferType[1]','tinyint'),
		@MainAccountId = t.x.value('MainAccountId[1]','int'),
		@CostCenterId = case when t.x.value('CostCenterId[1]','int') = 0 then null else t.x.value('CostCenterId[1]','int') end,
		@Observations = t.x.value('Observations[1]','varchar(300)'),
		@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
		@Status = t.x.value('Status[1]','tinyint')
		from @PortfolioTransferXml.nodes('/PortfolioTransfer') t(x)
		
		--Se obtiene los detalles del xml(PortfolioTransferDetail)
		insert into @PortfolioTransferDetail
		select 
		t.x.value('Id[1]','int') as Id,
		t.x.value('PortfolioTrasferId[1]','int') as PortfolioTrasferId,
		t.x.value('AccountReceivableId[1]','int') as AccountReceivableId,
		t.x.value('MainAccountId[1]','int') as MainAccountId,
		case when t.x.value('CostCenterId[1]','int') = 0 then null else t.x.value('CostCenterId[1]','int') end as CostCenterId,
		t.x.value('Value[1]','decimal(18, 0)') as Value
		from @PortfolioTransferXml.nodes('/PortfolioTransfer/PortfolioTransferDetail') t(x)

		--Se obtiene los detalles del xml(PortfolioTrabsferOtherConcept)
		insert into @PortfolioTransferOtherConcept
		select 
		t.x.value('Id[1]','int') as Id,
		t.x.value('PortfolioTransferId[1]','int') as PortfolioTransferId,
		t.x.value('PortfolioNoteConceptId[1]','int') as PortfolioNoteConceptId,
		t.x.value('MainAccountId[1]','int') as MainAccountId,
		case when t.x.value('CostCenterId[1]','int') = 0 then null else t.x.value('CostCenterId[1]','int') end as CostCenterId,
		t.x.value('Nature[1]','tinyint') as Nature,
		t.x.value('Value[1]','decimal(18, 0)') as Value
		from @PortfolioTransferXml.nodes('/PortfolioTransfer/PortfolioTransferOtherConcept') t(x)

		--Valido que el mes este abrierto
		if(select count(*) from [GeneralLedger].[ClosedMonth] where [Year] = Year(@DocumentDate) and [Month] = Month(@DocumentDate) and Status = 1) = 0 --- Si el mes no esta abierto
		Begin
			--rollback tran tVoucher
			--select 999 as CodeMessage, 'El mes ' + cast(MONTH(@DocumentDate) as varchar(2)) + ' no se encuentra abierto' as Message, 0 Id
			Set @CodeMessage = 999
			Set @Message = 'El mes ' + cast(MONTH(@DocumentDate) as varchar(2)) + ' no se encuentra abierto'
			Set @IdTransfer = 0
			return
		End

		--Total de debitos y creditos
		declare @debitCreditResult as decimal(18,0) = 0

		----Valido que hayan detalles del traslado(PortfolioTransferDetail)
		--if(select count(*) from @PortfolioTransferDetail) = 0
		--Begin
		--	--rollback tran tVoucher
		--	select 999 as CodeMessage, 'Se debe agregar mínimo una factura para realizar el traslado' as Message, 0 Id
		--	return
		--End

		--Si hay detalles de otros conceptos(PortfolioTransferOtherConcept)
		if(select count(*) from @PortfolioTransferOtherConcept) > 0
		Begin
			--Valor debito(1)
			declare @debit as decimal(18,0) = 0
			--Valor credito(2)
			declare @credit as decimal(18,0) = 0
			--Valor total de los detalles(PortfolioTransferDetail)
			declare @totalValueBills as decimal(18,0) = 0
			---Saldo del anticipo
			declare @PortfolioAdvanceBalance as decimal(18,0) = 0

			--Se obtiene el total del valor de los debitos y los creditos
			select @debit = ISNULL(SUM(Value), 0) from @PortfolioTransferOtherConcept where Nature = 1
			select @credit = ISNULL(SUM(Value), 0) from @PortfolioTransferOtherConcept where Nature = 2
			--Se obtiene el valor total de los detalles
			select @totalValueBills = SUM(Value) from @PortfolioTransferDetail
			--Se obtiene el saldo del anticipo
			select @PortfolioAdvanceBalance = Balance from Portfolio.PortfolioAdvance where Id = @PortfolioAdvanceId

			--Se valida si los valores debito y credito son menores a cero
			if ((@PortfolioAdvanceBalance + @debit) - (@credit + @totalValueBills)) < 0
			Begin
				--rollback tran tVoucher
				--select 999 as CodeMessage, 'No se puede guardar porque el valor del documento es menor a 0' as Message, 0 Id
				Set @CodeMessage = 999
				Set @Message = 'No se puede guardar porque el valor del documento es menor a 0'
				Set @IdTransfer = 0
				return
			End

			--Se valida si el valor de los otros conceptos es mayor al total de las facturas
			set @debitCreditResult = @debit - @credit
			--Se convierte el resultado a positivo
			if @debitCreditResult < 0
			Begin
				set @debitCreditResult *= -1
			End
			else begin
				--Se valida si el valor de los otros conceptos es mayor al total de las facturas
				if @debitCreditResult > @totalValueBills
				Begin
					--rollback tran tVoucher
					--select 999 as CodeMessage, 'No se puede guardar porque el valor de otros conceptos es mayor al total de las facturas' as Message, 0 Id
					Set @CodeMessage = 999
					Set @Message = 'No se puede guardar porque el valor de otros conceptos es mayor al total de las facturas'
					Set @IdTransfer = 0
					return
				End
			end
		End

		--Si la variable que se recibe de Confirm es true
		if @Confirm = 1
		Begin
			--Saldo del anticipo
			declare @balanceAdvance as decimal(18,0) = 0
			--Total de las facturas
			declare @billsValue as decimal(18,0) = 0
			--Se obtiene el saldo del anticipo
			select @balanceAdvance = Balance from Portfolio.PortfolioAdvance where Id = @PortfolioAdvanceId
			--Se obtiene el valor total de los detalles
			select @billsValue = SUM(Value) from @PortfolioTransferDetail

			--Se valida que la sumatoria del saldo del anticipo + el resultado debito credito no sea menor al total de las facturas
			if (@balanceAdvance + @debitCreditResult) < @billsValue
			Begin
			--rollback tran tVoucher
				--select 999 as CodeMessage, 'El valor del traslado es mayor al saldo del anticipo' as Message, 0 Id
				Set @CodeMessage = 999
				Set @Message = 'El valor del traslado es mayor al saldo del anticipo'
				Set @IdTransfer = 0
				return
			End

			declare @IdLegalBookOfficial int = (select Id from GeneralLedger.LegalBook where OfficialBook = 1)

			--Se valida que los valores de los detalles no sean mayores al saldo de accountReceivableAccounting
			if (select COUNT(*) from @PortfolioTransferDetail ptd
			inner join Portfolio.AccountReceivable ar on  ptd.AccountReceivableId = ar.Id
			inner join Portfolio.AccountReceivableAccounting ara on ar.Id = ara.AccountReceivableId and ptd.MainAccountId = ara.MainAccountId
			inner join GeneralLedger.MainAccounts ma on ptd.MainAccountId = ma.Id and ma.LegalBookId = @IdLegalBookOfficial
			where ara.Balance < ptd.Value) > 0
			Begin
				--Se declara la variable de errores
				declare @Errors varchar(max)
				--Se arma la variable con los errores
				select @Errors = STUFF((select N'; El saldo de la cuenta contable ' + ma.Number + ' - ' + ma.Name + ' de la factura ' + ar.InvoiceNumber + ' es menor que el valor ' + CONVERT(varchar(max), ptd.Value) + ' a trasladar por esta, Saldo de la cuenta: ' + CONVERT(varchar(max), ara.Balance)
				from @PortfolioTransferDetail ptd
				inner join Portfolio.AccountReceivable ar on  ptd.AccountReceivableId = ar.Id
				inner join Portfolio.AccountReceivableAccounting ara on ar.Id = ara.AccountReceivableId and ptd.MainAccountId = ara.MainAccountId
				inner join GeneralLedger.MainAccounts ma on ptd.MainAccountId = ma.Id and ma.LegalBookId = @IdLegalBookOfficial
				where ara.Balance < ptd.Value
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				--select 999 as CodeMessage, @Errors as Message, 0 Id
				Set @CodeMessage = 999
				Set @Message = @Errors
				Set @IdTransfer = 0
				return
			End
		End
	
		
		--commit transaction
		--select 0 as CodeMessage, 'Se validó correctamente' as Message, @Id as Id
		Set @CodeMessage = 0
		Set @Message = 'Se validó correctamente'
		Set @IdTransfer = @Id
		
	end try
	begin catch
		--rollback transaction
		--select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id
		Set @CodeMessage = 999
		Set @Message = (Select ERROR_MESSAGE())
		Set @IdTransfer = 0
	end catch

END