




-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 01/11/2016
-- Description:	Procedimiento que se encarga de actualizar los saldos de la cxc y además generar los detalles de comprobante contable
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SaveMasiveCxCCrossingAccount] 
	@XmlObject as Xml,
	@CrossingType as int,
	@ThirdPartyId as int
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY, AccountReceivableId int, CrossingValue decimal(18,2), MainAccountId int,
	Detail varchar(300), AccountReceivableAccountingId INT, ValueInCurrencyInvoice decimal(20,2))

	--Tabla para devolver los resultados
	--El status tiene estos valores: 0. Validación Controlada, 1. Correcto, 2. Excepción
	declare @TableResult table(Id int IDENTITY PRIMARY KEY, [Status] int, [Message] varchar(max),
	IdMainAccount int, IdThirdParty int, IdCostCenter int, DebitValue decimal(20, 4), CreditValue decimal(20, 4), Detail varchar(300))

	--Id centro costo
	declare @CostCenterId int

	Begin try
		
		insert into @TableXmlObject
		select 
		t.x.value('AccountReceivableId[1]','int') as AccountReceivableId,
		t.x.value('CrossingValue[1]','decimal(18,2)') as CrossingValue,
		t.x.value('MainAccountId[1]','int') as MainAccountId,
		t.x.value('Detail[1]','varchar(300)') as Detail,
		t.x.value('AccountReceivableAccountingId[1]','int') as AccountReceivableAccountingId,
	    t.x.value('ValueInCurrencyInvoice[1]','decimal(20,2)') as ValueInCurrencyInvoice
		from @XmlObject.nodes('/Data') t(x)
		
		--Se declara un cursor y las variables que lleva el cursor
		declare @Id as int
		declare @AccountReceivableId as int
		declare @CrossingValue as decimal(18,2)
		declare @MainAccountId as int
		declare @Detail as varchar(300)
		declare @AccountReceivableAccountingId as INT
		declare @ValueInCurrencyInvoice AS DECIMAL(20,2)
		Declare InfoItem Cursor For Select [Id], [AccountReceivableId], [CrossingValue], [MainAccountId], [Detail],
		AccountReceivableAccountingId, [ValueInCurrencyInvoice] From @TableXmlObject
		
		Open InfoItem
		Fetch Next From InfoItem Into @Id, @AccountReceivableId, @CrossingValue, @MainAccountId, @Detail, @AccountReceivableAccountingId, @ValueInCurrencyInvoice

		While @@fetch_status = 0
		Begin
			




			--Variable temporal para poder disminuir las cuotas
			declare @CrossingAccountTemp decimal(18,2) = @ValueInCurrencyInvoice
			
			--Saldo de la cuota
			declare @BalanceShare decimal(18,2)

			--Se recorren las cuotas para poder disminuir el valor
			declare @ShareId as int
			declare InfoShare Cursor For Select Id from Portfolio.AccountReceivableShare where AccountReceivableId = @AccountReceivableId

			Open InfoShare
			Fetch Next From InfoShare Into @ShareId
			
			While @@Fetch_status = 0
			Begin
				
				--Se obtiene el saldo de la cuota
				select @BalanceShare = Balance from Portfolio.AccountReceivableShare where Id = @ShareId

				if @CrossingAccountTemp >= @BalanceShare
				Begin
					set @CrossingAccountTemp -= @BalanceShare
					update Portfolio.AccountReceivableShare set Balance = 0, CrossingValue = Balance where Id = @ShareId
				End
				Else
				Begin
					update Portfolio.AccountReceivableShare set Balance -= @CrossingAccountTemp, CrossingValue += @CrossingAccountTemp where Id = @ShareId
					set @CrossingAccountTemp = 0
					break
				End

				Fetch Next From InfoShare Into @ShareId
				continue

			End

			Close InfoShare
			Deallocate InfoShare




			--Se actualiza en Accounting el saldo
			update Portfolio.AccountReceivableAccounting set Balance -= @ValueInCurrencyInvoice where Id = @AccountReceivableAccountingId

			--Se actualiza la cabecera
			update Portfolio.AccountReceivable set Balance -= @ValueInCurrencyInvoice where Id = @AccountReceivableId


			--Si el tipo de cruce es de diferente tercero
			if @CrossingType <> 1
			Begin
				select @ThirdPartyId = ThirdPartyId from Portfolio.AccountReceivable where Id = @AccountReceivableId
			End

			--Se asigna el centro costo
			select @CostCenterId = CostCenterId from Portfolio.AccountReceivableAccounting where Id = @AccountReceivableAccountingId

			--Se genera el registro en la tabla que se va a devolver que son los detalles de comprobante contable
			insert into @TableResult([Status], [Message], IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail)
			values(1, 'Registro Correcto', @MainAccountId, @ThirdPartyId, @CostCenterId, 0, @CrossingValue, @Detail)



			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @Id, @AccountReceivableId, @CrossingValue, @MainAccountId, @Detail, @AccountReceivableAccountingId, @ValueInCurrencyInvoice
			continue

		End

		Close InfoItem
		Deallocate InfoItem
		
		--Se retorna la tabla
		select * from @TableResult
		
	end try
	begin catch

		insert into @TableResult([Status], [Message])
		values
		(2, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5)))
		select * from @TableResult

	end catch

END