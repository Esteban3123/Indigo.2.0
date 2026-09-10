

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida un traslado de cartera antes de confirmarlo o guardarlo, recibiendo como entrada un XML con la cabecera del traslado, sus líneas de detalle (facturas o cuentas por cobrar) y otros conceptos (débitos/créditos adicionales). Verifica que el período contable esté abierto consultando [GeneralLedger].[ClosedMonth], que el saldo del anticipo en [Portfolio].[PortfolioAdvance] sea suficiente para cubrir el traslado, que las cuentas contables en [GeneralLedger].[MainAccounts] y [Portfolio].[AccountReceivableAccounting] correspondan al libro legal oficial, y que los valores débito/crédito estén balanceados. Retorna códigos de error, mensajes descriptivos e identificador del traslado generado, siendo el punto de control central antes de registrar definitivamente un traslado de cartera entre cuentas por cobrar, anticipos u otros conceptos financieros.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateTransfers_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateTransfers_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida la consistencia financiera y contable de un traslado de cartera (anticipos vs. facturas y otros conceptos) antes de su confirmación, verificando apertura del mes, saldos disponibles y cuadre de débitos/créditos.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTransfers_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener un nodo /PortfolioTransfer con cabecera y, opcionalmente, nodos PortfolioTransferDetail y PortfolioTransferOtherConcept.; El mes y año de DocumentDate deben existir en GeneralLedger.ClosedMonth con Status=1 (mes abierto).; Si se confirma el traslado, debe existir un registro en GeneralLedger.LegalBook marcado como OfficialBook=1.; Cuando se referencia un anticipo, debe existir el PortfolioAdvanceId en Portfolio.PortfolioAdvance.; Las cuentas por cobrar referenciadas deben tener registro contable en Portfolio.AccountReceivableAccounting con la misma MainAccountId.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTransfers_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un traslado solo es válido si su período (año/mes de DocumentDate) está abierto contablemente.; El valor neto del documento (anticipo + débitos - créditos - facturas) nunca puede ser negativo.; El exceso neto de débitos sobre créditos en otros conceptos no puede superar el total de facturas trasladadas.; Al confirmar, el saldo del anticipo más el ajuste neto de otros conceptos debe alcanzar para cubrir el total de facturas del traslado.; Al confirmar, ningún detalle puede trasladar un valor mayor al saldo contable disponible (Balance) en AccountReceivableAccounting bajo el libro oficial.; Las validaciones contables se evalúan únicamente contra cuentas vinculadas al LegalBook con OfficialBook=1.; El procedimiento solo valida; no modifica datos persistentes (no INSERT/UPDATE/DELETE sobre tablas reales).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTransfers_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'traslado de cartera; anticipo de cartera; saldo de anticipo; cuenta por cobrar; factura; otros conceptos (débito/crédito); cuenta contable principal (PUC); centro de costo; libro contable oficial; mes contable cerrado/abierto; cuadre débito-crédito', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTransfers_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] OUTPUT: Si no existe ClosedMonth abierto para el año/mes de DocumentDate → retorna CodeMessage=999, Message=''El mes N no se encuentra abierto'', IdTransfer=0.; [RETURN_RESULT] OUTPUT: Si hay otros conceptos y (PortfolioAdvance.Balance + débitos) - (créditos + total de detalles) < 0 → retorna CodeMessage=999, Message=''No se puede guardar porque el valor del documento es menor a 0''.; [RETURN_RESULT] OUTPUT: Si (débitos - créditos) > 0 y dicho resultado supera el total de PortfolioTransferDetail.Value → retorna CodeMessage=999, Message=''No se puede guardar porque el valor de otros conceptos es mayor al total de las facturas''.; [RETURN_RESULT] OUTPUT: Si @Confirm=1 y (PortfolioAdvance.Balance + |débitos-créditos|) < total de detalles → retorna CodeMessage=999, Message=''El valor del traslado es mayor al saldo del anticipo''.; [RETURN_RESULT] OUTPUT: Si @Confirm=1 y existe algún detalle cuyo Value supera el AccountReceivableAccounting.Balance (sobre el LegalBook oficial) → retorna CodeMessage=999 con un Message detallando cada cuenta/factura/saldo afectado.; [RETURN_RESULT] OUTPUT: Si todas las validaciones pasan → retorna CodeMessage=0, Message=''Se validó correctamente'', IdTransfer=@Id de la cabecera del XML.; [RETURN_RESULT] OUTPUT: En caso de excepción capturada por el CATCH → retorna CodeMessage=999, Message=ERROR_MESSAGE(), IdTransfer=0.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTransfers_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Mes/Año de DocumentDate no está en ClosedMonth con Status=1 → Retorna error 999 ''mes no abierto'' y termina else Continúa con validaciones de valores; si Existen filas en PortfolioTransferOtherConcept → Calcula débitos (Nature=1), créditos (Nature=2), total facturas y saldo del anticipo, y valida no-negatividad y cuadre con facturas else Omite validaciones de otros conceptos; @debitCreditResult permanece en 0; si @debitCreditResult (débitos-créditos) < 0 → Convierte a positivo (multiplica por -1) y omite la validación de exceso sobre total de facturas else Si @debitCreditResult > total de facturas → error 999; si @Confirm = 1 → Ejecuta validaciones adicionales: saldo del anticipo cubre el traslado y los Value de los detalles no exceden el Balance de AccountReceivableAccounting else Salta validaciones de confirmación y devuelve éxito; si CostCenterId = 0 en cualquier nodo XML → Se almacena como NULL else Se conserva el valor entero', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTransfers_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ClosedMonth; Portfolio.PortfolioAdvance; GeneralLedger.LegalBook; Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTransfers_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTransfers_Output';
-- GO
