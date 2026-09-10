

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de tesorería que procesa de forma masiva el cruce (aplicación de pagos) de cuentas por cobrar recibiendo un listado en formato XML. Para cada cuenta por cobrar indicada, descuenta el valor cruzado de las cuotas o plazos de pago (AccountReceivableShare), actualiza el saldo contable (AccountReceivableAccounting) y el saldo de la cabecera de la cuenta por cobrar (AccountReceivable). Finalmente, genera y retorna los detalles de líneas de comprobante contable (cuenta contable principal, tercero, centro de costo, valores débito/crédito) necesarios para registrar el asiento contable del cruce en el módulo de cartera y tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveMasiveCxCCrossingAccount';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveMasiveCxCCrossingAccount';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Aplica masivamente cruces de cartera sobre cuentas por cobrar: distribuye el valor cruzado entre las cuotas pendientes, actualiza saldos en cabecera y registro contable, y devuelve los detalles para armar el comprobante contable.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMasiveCxCCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data con los nodos AccountReceivableId, CrossingValue, MainAccountId, Detail, AccountReceivableAccountingId y ValueInCurrencyInvoice.; Cada AccountReceivableId debe existir en Portfolio.AccountReceivable y tener cuotas en Portfolio.AccountReceivableShare.; El AccountReceivableAccountingId debe existir en Portfolio.AccountReceivableAccounting y tener un CostCenterId asignado.; La suma de saldos de cuotas debe ser suficiente para absorber @ValueInCurrencyInvoice (no se valida explícitamente; el cursor termina al recorrer todas las cuotas).; Si @CrossingType=1 el @ThirdPartyId recibido se usa directamente; si es distinto, debe poderse obtener desde la cuenta por cobrar.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMasiveCxCCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El saldo de una cuota nunca queda negativo: si el valor a cruzar supera el saldo, la cuota queda en Balance=0; en caso contrario se descuenta exactamente el remanente.; El monto total descontado de Portfolio.AccountReceivable y Portfolio.AccountReceivableAccounting coincide con @ValueInCurrencyInvoice del ítem procesado.; Cada ítem procesado correctamente genera exactamente un detalle contable en el resultado con DebitValue=0 y CreditValue=@CrossingValue (movimiento de naturaleza crédito).; Para cruces del mismo tercero (CrossingType=1) se respeta el tercero recibido; para cruces distintos se toma el tercero de la cuenta por cobrar de origen.; El centro de costo del detalle contable proviene siempre de Portfolio.AccountReceivableAccounting.; El recorrido de cuotas se detiene en cuanto se agota el valor a cruzar (break).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMasiveCxCCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por cobrar; Cuota de cartera; Cruce de cartera; Comprobante contable; Tercero; Centro de costo; Cuenta contable principal; Saldo pendiente', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMasiveCxCCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Portfolio.AccountReceivableShare: Cuando @CrossingAccountTemp >= Balance de la cuota: se fija Balance=0 y CrossingValue=Balance previo, saldando completamente la cuota.; [UPDATE] Portfolio.AccountReceivableShare: Cuando @CrossingAccountTemp < Balance: se descuenta parcialmente (Balance -= @CrossingAccountTemp, CrossingValue += @CrossingAccountTemp) y se interrumpe el recorrido de cuotas.; [UPDATE] Portfolio.AccountReceivableAccounting: Por cada ítem se descuenta @ValueInCurrencyInvoice del Balance del registro contable identificado por @AccountReceivableAccountingId.; [UPDATE] Portfolio.AccountReceivable: Por cada ítem se descuenta @ValueInCurrencyInvoice del Balance de la cabecera de la cuenta por cobrar identificada por @AccountReceivableId.; [RETURN_RESULT] @TableResult (resultado retornado): Por cada ítem procesado con éxito se devuelve una fila con Status=1, MainAccount, ThirdParty (ajustado según CrossingType), CostCenter de la cuenta contable, DebitValue=0 y CreditValue=@CrossingValue.; [RETURN_RESULT] @TableResult (resultado retornado): Si ocurre una excepción se devuelve una sola fila con Status=2 y el mensaje de error concatenado con el número de línea.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMasiveCxCCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CrossingAccountTemp >= @BalanceShare (el valor por cruzar cubre el saldo total de la cuota) → Se descuenta el saldo de la cuota del valor a cruzar y se deja la cuota en Balance=0, CrossingValue=Balance previo else Se aplica el valor restante a la cuota (Balance -= temp, CrossingValue += temp), se pone temp en 0 y se rompe el ciclo de cuotas; si @CrossingType <> 1 (cruce entre terceros distintos) → Se reemplaza @ThirdPartyId por el ThirdPartyId de la cuenta por cobrar (Portfolio.AccountReceivable) else Se conserva el @ThirdPartyId recibido como parámetro; si Se produce una excepción en cualquier paso del proceso (CATCH) → Se inserta en el resultado un registro con Status=2 y el mensaje de error con el número de línea, y se retorna la tabla de resultados', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMasiveCxCCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivableShare; Portfolio.AccountReceivableAccounting; Portfolio.AccountReceivable', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMasiveCxCCrossingAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMasiveCxCCrossingAccount';
-- GO
