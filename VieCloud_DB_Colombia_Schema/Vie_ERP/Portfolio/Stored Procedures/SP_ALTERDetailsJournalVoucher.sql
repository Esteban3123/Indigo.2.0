-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- ALTER date: 01/11/2016
-- Description:	Procedimiento que se encarga de crear los detalles del comprobante contable para provisión/deterioro
--				además modifica los saldos(BalanceProvision/BalanceDeterioration y PaymentProvision/PaymentDeterioration)				
--				de la tabla AccountReceivable
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ALTERDetailsJournalVoucher] 
	@XmlObject as Xml
AS
BEGIN
	
	DECLARE 
		--Saldo de la factura que se va recorriendo
		@BalanceInvoice DECIMAL(18,2),
		--Saldo de la provision
		@ProvisionBalance DECIMAL(18,2),
		--Saldo del deterioro
		@DeteriorationBalance DECIMAL(18,2),
		--Saldo del deterioro del año actual
		@DeteriorationBalanceCurrentYear DECIMAL(18,2),
		--Saldo del deterioro de años anteriores
		@DeteriorationBalancePreviousYear DECIMAL(18,2),
		--Año del ultimo deterioro realizado
		@CurrentDeteriorationYear INT,
		--Valor a reducir en los saldos
		@ReduceValue DECIMAL(18,2),
		--Cuenta contable para la provision debito
		@ProvisionDebitMainAccountId INT,
		--Cuenta contable para la provision credito
		@ProvisionCreditMainAccountId INT,
		--Cuenta contable para el deterioro debito
		@DeteriorationDebitMainAccountId INT,
		--Cuenta contable para el deterioro credito
		@DeteriorationCreditMainAccountId INT,
		--Cuenta contable para la reversion del deterioro causada en el año en curso
		@ReversalAccountDeteriorationId INT,
		--Cuenta contable para la reversion del deterioro causada en años anteriores
		@PreviousPeriodReversalAccountDeteriorationId INT,
		--Id del tercero de la factura que se va recorriendo
		@ThirdPartyId INT,
		--Id del centro costo de la factura que se va recorriendo
		@CostCenterId INT,
		@ProvisionReduceValue DECIMAL(18,2),
		@newDeteriorationBalanceCurrentYear DECIMAL(18,2),
		@newDeteriorationBalancePreviousYear DECIMAL(18,2)

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @TableXmlObject TABLE
	(
		Id int IDENTITY PRIMARY KEY, 		
		AccountReceivableId INT, 
		DocumentDate DATETIME,
		InvoiceNumber VARCHAR(20), 
		Value numeric(18,2),
		PortfolioTransferId INT
	)

	--Tabla para devolver los resultados
	--El status tiene estos valores: 0. Validación Controlada, 1. Correcto, 2. Excepción
	DECLARE @TableResult TABLE
	(
		Id int IDENTITY PRIMARY KEY, 
		[Status] INT, 
		[Message] VARCHAR(max),
		IdMainAccount INT, 
		IdThirdParty INT, 
		IdCostCenter INT, 
		DebitValue DECIMAL(20, 4), 
		CreditValue DECIMAL(20, 4)
	)

	BEGIN TRY
		
		INSERT INTO @TableXmlObject
			(
				AccountReceivableId, 
				DocumentDate, 
				InvoiceNumber, 
				Value,
				PortfolioTransferId
			)
			SELECT
				t.x.value('AccountReceivableId[1]','int') as AccountReceivableId,
				t.x.value('DocumentDate[1]','datetime') as DocumentDate,
				t.x.value('InvoiceNumber[1]','VARCHAR(20)') as InvoiceNumber,
				t.x.value('Value[1]','numeric(18, 2)') as Value
				,t.x.value('PortfolioTransferId[1]','int') as PortfolioTransferId
			FROM @XmlObject.nodes('/Data') t(x)
		
		--Se declara un cursor y las variables que lleva el cursor
		DECLARE @Id as INT,
				@AccountReceivableId as INT,
				@DocumentDate as DATETIME,
				@InvoiceNumber as VARCHAR(20),
				@Value as DECIMAL(18,2)
				,@PortfolioTransferId as INT

		DECLARE InfoItem CURSOR
			FOR SELECT Id, AccountReceivableId, DocumentDate, InvoiceNumber, [Value], PortfolioTransferId FROM @TableXmlObject

		OPEN InfoItem
			FETCH NEXT FROM InfoItem INTO @Id, @AccountReceivableId, @DocumentDate, @InvoiceNumber, @Value, @PortfolioTransferId

			WHILE @@fetch_status = 0
			BEGIN	
					
				--Obtengo el saldo de la factura y los saldos de provision y deterioro y además las cuentas contables que son necesarias
				--para agregar los detalles del comprobante
				SELECT 
					@BalanceInvoice = Balance, 
					@ProvisionBalance = ProvisionBalance, 
					@DeteriorationBalance = DeteriorationBalance,
					@DeteriorationBalanceCurrentYear = DeteriorationBalanceCurrentYear,
					@DeteriorationBalancePreviousYear = DeteriorationBalancePreviousYear,
					@CurrentDeteriorationYear = CurrentDeteriorationYear,
					@ProvisionDebitMainAccountId = DebitProvisionAccountId, 
					@ProvisionCreditMainAccountId = CreditProvisionAccountId,
					@DeteriorationDebitMainAccountId = DebitAccountDeteriorationId, 
					@DeteriorationCreditMainAccountId = CreditAccountDeteriorationId,
					@ReversalAccountDeteriorationId = ReversalAccountDeteriorationId,
					@PreviousPeriodReversalAccountDeteriorationId = PreviousPeriodReversalAccountDeteriorationId,
					@ThirdPartyId = ThirdPartyId, 
					@CostCenterId = CostCenterId 
				FROM Portfolio.AccountReceivable 
				WHERE Id = @AccountReceivableId
			
				--Valido que el valor que se va a pagar no sea mayor al saldo
				IF @Value > @BalanceInvoice
				BEGIN
					--Se inserta un mensaje de error
					INSERT INTO @TableResult([Status], [Message])
					VALUES(0, 'El valor a pagar (' + CAST(@Value as VARCHAR(20)) + ') es mayor al saldo (' + CAST(@BalanceInvoice as VARCHAR(20)) + ') de la factura ' + @InvoiceNumber)		
					
					--Se pasa a la siguiente posicion del cursor
					GOTO InfoItem
				END
			
				SET @ReduceValue = 0
				SET @ProvisionReduceValue= 0
				SET @newDeteriorationBalanceCurrentYear = 0
				SET @newDeteriorationBalancePreviousYear = 0
			
				--Verifico si el saldo de provision es mayor a cero para modificar los respectivos campos y generar los detalles del comprobante
				IF @ProvisionBalance > 0
				BEGIN 
					--Calculo el valor que se va a restar
					SET @ReduceValue = @ProvisionBalance * (@Value / @BalanceInvoice)
					set @ProvisionReduceValue = @ReduceValue
				
					--Actualizo los campos en la tabla AccountReceivable
					UPDATE ar 
						SET ProvisionBalance -= @ReduceValue, 
							ProvisionPayment += @ReduceValue
					FROM Portfolio.AccountReceivable ar
					WHERE Id = @AccountReceivableId
				
					--Se inserta el detalle del comprobante con los valores de provision credito
					INSERT INTO @TableResult([Status], [Message], IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue)
					VALUES (1, 'Registro Correcto', @ProvisionCreditMainAccountId, @ThirdPartyId, @CostCenterId, @ReduceValue, 0)

					--Se inserta el detalle del comprobante con los valores de provision debito
					INSERT INTO @TableResult([Status], [Message], IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue)
					VALUES (1, 'Registro Correcto', @ProvisionDebitMainAccountId, @ThirdPartyId, @CostCenterId, 0, @ReduceValue)
				END

				SET @ReduceValue = 0
			
				--Verifico si el saldo de deterioro es mayor a cero para modificar los respectivos campos y generar los detalles del comprobante
				IF @DeteriorationBalance > 0
				BEGIN
					--Calculo el valor que se va a restar
					SET @ReduceValue = @DeteriorationBalance * (@Value / @BalanceInvoice)
					
					SELECT 
						@newDeteriorationBalanceCurrentYear = @DeteriorationBalanceCurrentYear - IIF
						(
							@ReduceValue > @DeteriorationBalanceCurrentYear, 
							@DeteriorationBalanceCurrentYear, 
							@ReduceValue
						),
						@newDeteriorationBalancePreviousYear = @DeteriorationBalancePreviousYear - IIF
						(
							@ReduceValue > @DeteriorationBalanceCurrentYear, IIF
							((@ReduceValue - @DeteriorationBalanceCurrentYear) > @DeteriorationBalancePreviousYear, @DeteriorationBalancePreviousYear, @ReduceValue - @DeteriorationBalanceCurrentYear), 
							0
						)

					--Se actualizan los campos de la cuenta por cobrar en la tabla AccountReceivable segun corresponda
					UPDATE ar 
						SET DeteriorationBalance = ar.DeteriorationBalance - @ReduceValue,
							DeteriorationBalanceCurrentYear = @newDeteriorationBalanceCurrentYear,
							DeteriorationBalancePreviousYear = @newDeteriorationBalancePreviousYear,
							DeteriorationPayment = DeteriorationPayment + @ReduceValue
					FROM Portfolio.AccountReceivable ar 
					WHERE ar.Id = @AccountReceivableId

					--Se inserta el detalle del comprobante con la reversión del deterioro realizado en el año en curso
					IF @CurrentDeteriorationYear = YEAR(@DocumentDate) AND @newDeteriorationBalanceCurrentYear <> @DeteriorationBalanceCurrentYear
					BEGIN
						INSERT INTO @TableResult ([Status], [Message], IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue)
						VALUES (1, 'Registro Correcto', @ReversalAccountDeteriorationId, @ThirdPartyId, @CostCenterId, 0, (@DeteriorationBalanceCurrentYear - @newDeteriorationBalanceCurrentYear))
					END

					--Se inserta el detalle del comprobante con la reversión del deterioro realizado en años anteriores
					IF @CurrentDeteriorationYear < YEAR(@DocumentDate) OR @newDeteriorationBalancePreviousYear <> @DeteriorationBalancePreviousYear
					BEGIN						
						INSERT INTO @TableResult ([Status], [Message], IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue)
						VALUES 
						(
							1, 
							'Registro Correcto', 
							@PreviousPeriodReversalAccountDeteriorationId, 
							@ThirdPartyId, 
							@CostCenterId, 
							0, 
							IIF(@CurrentDeteriorationYear < YEAR(@DocumentDate), (@DeteriorationBalanceCurrentYear - @newDeteriorationBalanceCurrentYear), 0)
							+
							(@DeteriorationBalancePreviousYear - @newDeteriorationBalancePreviousYear)
						)
					END

					--Se inserta el detalle del comprobante con los valores de deterioro debito
					INSERT INTO @TableResult ([Status], [Message], IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue)
					VALUES (1, 'Registro Correcto', @DeteriorationCreditMainAccountId, @ThirdPartyId, @CostCenterId, @ReduceValue, 0)
				END

				IF (@ProvisionBalance > 0 OR @DeteriorationBalance > 0) and @PortfolioTransferId is not null
				BEGIN
					INSERT INTO Portfolio.PortfolioTransferReceivableReduce (PortfolioTransferId
					,AccountReceivableId
					,ProvisionValue
					,DeteriorationValue
					,DeteriorationCurrentYearValue
					,DeteriorationPreviousYearValue)
					VALUES (@PortfolioTransferId, @AccountReceivableId, @ProvisionReduceValue, @ReduceValue, @DeteriorationBalanceCurrentYear - @newDeteriorationBalanceCurrentYear, @DeteriorationBalancePreviousYear - @newDeteriorationBalancePreviousYear)
				END
			
				--Se pasa a la siguiente posicion del cursor
				InfoItem:
				FETCH NEXT FROM InfoItem INTO @Id, @AccountReceivableId, @DocumentDate, @InvoiceNumber, @Value, @PortfolioTransferId
			END

		CLOSE InfoItem
		DEALLOCATE InfoItem
	END TRY
	BEGIN CATCH

		INSERT INTO @TableResult ([Status], [Message])
		VALUES (2, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as VARCHAR(5)))

	END CATCH

	--Se retorna la tabla
	SELECT * FROM @TableResult

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento contable que genera los detalles del comprobante de provisión y/o deterioro de cartera para un conjunto de facturas (cuentas por cobrar) recibidas en formato XML. Por cada factura procesada, valida que el valor a pagar no supere el saldo pendiente y luego recalcula y actualiza los saldos de provisión (ProvisionBalance, ProvisionPayment) y deterioro (DeteriorationBalance, tanto del año actual como de años anteriores) en la tabla AccountReceivable. Como resultado, retorna los movimientos contables necesarios (cuenta contable, tercero, centro de costo, valor débito y crédito) para registrar en el comprobante las reversiones y aplicaciones de provisión y deterioro, así como los mensajes de validación controlada en caso de inconsistencias. Se usa en el módulo de Cartera (Portfolio) para el cierre contable de pagos aplicados a facturas con provisión o deterioro acumulado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ALTERDetailsJournalVoucher';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ALTERDetailsJournalVoucher';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera los detalles contables del comprobante por pago/recaudo aplicado a provisión y deterioro de cuentas por cobrar, ajustando saldos y registrando reversiones según el año del deterioro.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ALTERDetailsJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe entregar nodos /Data con AccountReceivableId, DocumentDate, InvoiceNumber, Value y opcionalmente PortfolioTransferId.; Cada AccountReceivableId debe existir en Portfolio.AccountReceivable y tener configuradas las cuentas contables de provisión, deterioro y reversiones (DebitProvisionAccountId, CreditProvisionAccountId, DebitAccountDeteriorationId, CreditAccountDeteriorationId, ReversalAccountDeteriorationId, PreviousPeriodReversalAccountDeteriorationId).; El valor a aplicar (Value) no debe superar el saldo actual de la factura (Balance).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ALTERDetailsJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Portfolio.AccountReceivable: Cuando ProvisionBalance > 0: se reduce ProvisionBalance y se incrementa ProvisionPayment en ProvisionBalance * (Value/Balance).; [UPDATE] Portfolio.AccountReceivable: Cuando DeteriorationBalance > 0: se reduce DeteriorationBalance y se incrementa DeteriorationPayment en DeteriorationBalance * (Value/Balance); además se recalculan DeteriorationBalanceCurrentYear y DeteriorationBalancePreviousYear consumiendo primero el saldo del año en curso y luego el de años anteriores.; [INSERT] Portfolio.PortfolioTransferReceivableReduce: Si (ProvisionBalance > 0 OR DeteriorationBalance > 0) AND PortfolioTransferId IS NOT NULL: inserta una fila con los valores reducidos de provisión, deterioro total, deterioro del año en curso y deterioro de años anteriores.; [RETURN_RESULT] @TableResult: Cuando Value > BalanceInvoice: registra Status=0 con mensaje ''El valor a pagar (..) es mayor al saldo (..) de la factura ..'' y omite el procesamiento de esa factura (GOTO al siguiente cursor).; [RETURN_RESULT] @TableResult: Si ProvisionBalance > 0: emite dos detalles del comprobante — uno con CreditProvisionAccountId en Débito y otro con DebitProvisionAccountId en Crédito por el valor reducido de provisión.; [RETURN_RESULT] @TableResult: Si DeteriorationBalance > 0: emite un detalle con CreditAccountDeteriorationId en Débito por el valor reducido de deterioro.; [RETURN_RESULT] @TableResult: Si CurrentDeteriorationYear = YEAR(DocumentDate) y se afectó el saldo del año en curso: emite detalle con ReversalAccountDeteriorationId en Crédito por (DeteriorationBalanceCurrentYear - newDeteriorationBalanceCurrentYear).; [RETURN_RESULT] @TableResult: Si CurrentDeteriorationYear < YEAR(DocumentDate) OR se afectó el saldo de años anteriores: emite detalle con PreviousPeriodReversalAccountDeteriorationId en Crédito sumando (cuando aplica) el delta del año en curso más el delta de años anteriores.; [RETURN_RESULT] @TableResult: En BEGIN CATCH inserta una fila Status=2 con ERROR_MESSAGE() y ERROR_LINE() y retorna la tabla.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ALTERDetailsJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Value > @BalanceInvoice → Inserta validación controlada (Status=0) y salta a la siguiente fila del cursor (GOTO InfoItem). else Continúa con el procesamiento de provisión y deterioro.; si @ProvisionBalance > 0 → Calcula ReduceValue proporcional (ProvisionBalance * Value/Balance), actualiza ProvisionBalance/ProvisionPayment y emite los dos asientos (débito y crédito) de provisión.; si @DeteriorationBalance > 0 → Calcula ReduceValue proporcional, distribuye entre saldo del año en curso y de años anteriores, actualiza la cuenta y emite el asiento débito de deterioro.; si @CurrentDeteriorationYear = YEAR(@DocumentDate) AND @newDeteriorationBalanceCurrentYear <> @DeteriorationBalanceCurrentYear → Emite asiento de reversión del deterioro del año en curso usando ReversalAccountDeteriorationId.; si @CurrentDeteriorationYear < YEAR(@DocumentDate) OR @newDeteriorationBalancePreviousYear <> @DeteriorationBalancePreviousYear → Emite asiento de reversión del deterioro de años anteriores usando PreviousPeriodReversalAccountDeteriorationId; si el año del deterioro es anterior al del documento, suma también el delta del año en curso.; si (@ProvisionBalance > 0 OR @DeteriorationBalance > 0) AND @PortfolioTransferId IS NOT NULL → Inserta registro en Portfolio.PortfolioTransferReceivableReduce vinculando la transferencia con la cuenta por cobrar y los valores reducidos.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ALTERDetailsJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ALTERDetailsJournalVoucher';
-- GO
