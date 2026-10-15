-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 01/11/2016
-- Description:	Procedimiento que se encarga de crear los detalles del comprobante contable para provisión/deterioro
--				además modifica los saldos(BalanceProvision/BalanceDeterioration y PaymentProvision/PaymentDeterioration)				
--				de la tabla AccountReceivable
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_CreateDetailsJournalVoucher] 
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON
	
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
		DECLARE @AccountReceivableId as INT,
				@DocumentDate as DATETIME,
				@InvoiceNumber as VARCHAR(20),
				@Value as DECIMAL(18,2)
				,@PortfolioTransferId as INT

		DECLARE InfoItem CURSOR
			FOR SELECT AccountReceivableId, DocumentDate, InvoiceNumber, SUM([Value]) Value, PortfolioTransferId FROM @TableXmlObject GROUP BY AccountReceivableId, DocumentDate, InvoiceNumber, PortfolioTransferId

		OPEN InfoItem
			FETCH NEXT FROM InfoItem INTO @AccountReceivableId, @DocumentDate, @InvoiceNumber, @Value, @PortfolioTransferId

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
				FETCH NEXT FROM InfoItem INTO @AccountReceivableId, @DocumentDate, @InvoiceNumber, @Value, @PortfolioTransferId
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