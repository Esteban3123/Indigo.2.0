
-- =============================================
-- Author:      Giovanny Plazas
-- Create Date: 27/12/2022
-- Description: Store Procedure que se encarga de guardar en la tabla de tasa de cambio, de la respectiva tabla
-- =============================================
CREATE PROCEDURE [Common].[SP_InsertIntoExchangeRateWithDate]

    @EntityName as varchar(50),
	@EntityId as int,
	@EntityCurrencyId as int,
	@Date as Date,
	@StateResult INT = NULL OUTPUT,
	@MessageOutput VARCHAR(100) = NULL OUTPUT
------------------------------------------------------
AS
SET
  ANSI_NULLS,
  QUOTED_IDENTIFIER,
  CONCAT_NULL_YIELDS_NULL,
  ANSI_WARNINGS,
  ANSI_PADDING
ON;
BEGIN
BEGIN TRY
	
	DECLARE @TableResult table (StateResult varchar(3), MessageResult varchar(100))
	DECLARE @actualCurrencyRate table (CurrencyFrom int, CurrencyTo int, [Value] numeric(20, 5), ValueReverse numeric(20, 5))
	DECLARE @Table_EntityId AS TABLE(Id INT)
	DECLARE @currencyId AS INT,
			@CustomTRM AS BIT= 0

	if @EntityName is null or @EntityName='' or @EntityId is null or @EntityId =0 or @EntityCurrencyId is null or @EntityCurrencyId= 0 begin		
		SET @StateResult =999
		return
	end

	if @Date is null begin
		set @Date = CAST(Common.GETDATE() as DATE)
	end

	IF  EXISTS(	SELECT 1  
				FROM Portfolio.PortfolioAdvance pa WITH(NOLOCK)
				JOIN Treasury.CashReceipts cr WITH(NOLOCK) on pa.CashReceiptId=cr.Id
				where pa.Id = @EntityId and @EntityName='PortfolioAdvance' and cr.EntityName='Invoice' ) BEGIN
		SET @CustomTRM = 1
	END

	--Validación para cuando no existan libros con moneda diferente a la oficial
	if NOT EXISTS( SELECT DISTINCT 1 FROM Common.Currency c
			JOIN GeneralLedger.LegalBook lb ON c.Id = lb.OfficialCurrencyId
			WHERE c.Id <> @EntityCurrencyId AND lb.Status = 1) BEGIN
			SET @StateResult =0
			return
	END

	DECLARE Cursor1 CURSOR LOCAL
		FOR SELECT DISTINCT c.Id FROM Common.Currency c
			JOIN GeneralLedger.LegalBook lb ON c.Id = lb.OfficialCurrencyId
			WHERE c.Id <> @EntityCurrencyId AND lb.Status = 1
	
	
	--Abrir el cursor
	
	OPEN Cursor1
	--Navegar
	FETCH NEXT FROM Cursor1 INTO @currencyId
		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			delete from @actualCurrencyRate
			print @CustomTRM
			--Busco la tasa de conversion de la moneda con la fecha actual -- en custom TRM o en TRM dependiendo si viene de Invoice el registro
			if EXISTS(	select 1 
						from Common.TRM 
						where MeasurementDate = @Date and CurrencyId = @EntityCurrencyId and OfficialCurrencyId = @currencyId and @CustomTRM=0
						
						UNION ALL 

						select 1 
						from Billing.CustomTRM 
						where  (@Date BETWEEN InitialMeasurementDate and FinalMeasurementDate)  and CurrencyId = @EntityCurrencyId and OfficialCurrencyId = @currencyId and @CustomTRM=1)  begin	
		               --------------------------------------------------------------------------------------------------------------								
						insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
						select @EntityCurrencyId, @currencyId, [Value], ValueOfficialToCurrency 
						from Common.TRM
						where MeasurementDate = @Date and CurrencyId = @EntityCurrencyId and OfficialCurrencyId = @currencyId and @CustomTRM=0

						insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
						select top 1 @EntityCurrencyId, @currencyId, [Value], ValueOfficialToCurrency 
						from Billing.CustomTRM 
						where  (@Date BETWEEN InitialMeasurementDate and FinalMeasurementDate)  and CurrencyId = @EntityCurrencyId and OfficialCurrencyId = @currencyId and @CustomTRM=1
			
			end
			else if EXISTS(	select 1
							from Common.TRM 
							where MeasurementDate = @Date and OfficialCurrencyId = @EntityCurrencyId and CurrencyId = @currencyId and @CustomTRM=0
							
							UNION ALL

							select 1
							from Billing.CustomTRM
							where (@Date BETWEEN InitialMeasurementDate and FinalMeasurementDate) and OfficialCurrencyId = @EntityCurrencyId and CurrencyId = @currencyId and @CustomTRM=1	) begin
		               --------------------------------------------------------------------------------------------------------------				
							insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
							select @EntityCurrencyId, @currencyId, ValueOfficialToCurrency, [Value]
							from Common.TRM where MeasurementDate = @Date and OfficialCurrencyId = @EntityCurrencyId and CurrencyId = @currencyId and @CustomTRM=0

							insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
							select @EntityCurrencyId, @currencyId, ValueOfficialToCurrency, [Value]
							from Billing.CustomTRM
							where (@Date BETWEEN InitialMeasurementDate and FinalMeasurementDate) and OfficialCurrencyId = @EntityCurrencyId and CurrencyId = @currencyId and @CustomTRM=1
			END
			ELSE BEGIN
				INSERT INTO @TableResult VALUES('999', 'No se encuentra Tasa de cambio para la moneda de la transacción para la fecha ' + CONVERT(VARCHAR, @Date, 103))
				FETCH NEXT FROM Cursor1 INTO @currencyId
				CONTINUE 
			END

			IF @EntityName ='AdvancePayments'  BEGIN
				INSERT INTO [Payments].[AdvancePaymentsExchangeRate]
						([AdvancePaymentsId]
						,[CurrencyId]
						,[Value]
						,[ValueReverse])
				select	@EntityId,
						CurrencyTo,
						[Value],
						[ValueReverse] 
				from @actualCurrencyRate
			END
			ELSE IF @EntityName ='AccountPayable' BEGIN
				INSERT INTO [Payments].[AccountPayableExchangeRate]
						([AccountPayableId]
						,[CurrencyId]
						,[Value]
						,[ValueReverse])
				select	@EntityId,
						CurrencyTo,
						[Value],
						[ValueReverse] 
				from @actualCurrencyRate
			END
			ELSE IF @EntityName='PortfolioAdvance'BEGIN
				INSERT INTO [Portfolio].[PortfolioAdvanceExchangeRate]
						([PortfolioAdvanceId]
						,[CurrencyId]
						,[Value]
						,[ValueReverse])
				select	@EntityId,
						CurrencyTo,
						[Value],
						[ValueReverse] 
				from @actualCurrencyRate
			END
			ELSE IF @EntityName='DeferredCausation'BEGIN
				INSERT INTO [Payments].[DeferredCausationExchangeRate]
						([DeferredCausationId]
						,[CurrencyId]
						,[Value]
						,[ValueReverse])
				select	@EntityId,
						CurrencyTo,
						[Value],
						[ValueReverse] 
				from @actualCurrencyRate
			END
			ELSE BEGIN			
				SET @StateResult =999
				RETURN
			END

			INSERT INTO @TableResult VALUES('0', 'Proceso exitoso')
			FETCH NEXT FROM Cursor1 INTO @currencyId
		END
		--Cerrar el cursor
		CLOSE Cursor1
		--Liberar Memoria
		DEALLOCATE Cursor1

	SELECT @StateResult = StateResult, @MessageOutput = MessageResult
	FROM @TableResult
END TRY
BEGIN CATCH
	IF CURSOR_STATUS('global','Cursor1') >= -1  BEGIN
		  IF CURSOR_STATUS('global','Cursor1') > -1 BEGIN
			CLOSE Cursor1
		  END
		 DEALLOCATE Cursor1
		END

		SET	@StateResult =999
		SET @MessageOutput = ERROR_MESSAGE()
END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra las tasas de cambio (TRM) aplicables a una transacción financiera específica —anticipos de cartera, cuentas por pagar, facturas, causaciones diferidas, entre otras— para una fecha determinada. Busca el tipo de cambio correcto consultando primero la TRM estándar (Common.TRM) o la TRM personalizada por unidad operativa (Billing.CustomTRM), eligiendo una u otra según si la transacción proviene de una factura de anticipo de cartera. Itera sobre todas las monedas oficiales activas registradas en los libros contables (GeneralLedger.LegalBook) que sean distintas a la moneda de la entidad, e inserta el par de conversión (valor directo e inverso) en la tabla de tasas de cambio correspondiente a cada tipo de entidad (anticipos de pago, cuentas por pagar, anticipos de cartera, causaciones diferidas, etc.). Su propósito es garantizar que cada documento financiero quede valorizado con el tipo de cambio vigente a su fecha, soportando la contabilización multimoneda del sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_InsertIntoExchangeRateWithDate';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_InsertIntoExchangeRateWithDate';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra las tasas de cambio aplicables a una entidad financiera (anticipos, cuentas por pagar, anticipos de cartera o causaciones diferidas) para cada moneda oficial de los libros contables activos en una fecha dada.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRateWithDate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'EntityName no nulo ni vacío; EntityId no nulo y distinto de 0; EntityCurrencyId no nulo y distinto de 0; Si la fecha viene nula, se asume la fecha actual del sistema (Common.GETDATE); Debe existir al menos un libro contable activo (Status=1) cuya moneda oficial sea distinta a la moneda de la entidad', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRateWithDate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan monedas oficiales de libros legales con Status=1 distintas a la moneda de la entidad; Para PortfolioAdvance originado en facturación (Invoice) se usan tasas personalizadas (CustomTRM) en lugar de las oficiales (TRM); Cada inserción de tasa incluye siempre el valor directo y su reverso (ValueReverse); Si la fecha no se especifica, se utiliza la fecha actual del sistema; Errores no detienen el proceso global: el CATCH cierra/libera el cursor y reporta StateResult=999; Si EntityName no corresponde a una de las 4 entidades soportadas, no se persiste ninguna tasa', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRateWithDate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tasa de cambio (TRM); TRM personalizada por facturación; Moneda oficial de libro contable; Anticipos de pago; Cuentas por pagar; Anticipos de cartera; Causación diferida; Recibos de caja; Factura', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRateWithDate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Payments.AdvancePaymentsExchangeRate: Cuando EntityName=''AdvancePayments'' y se encuentra tasa para la fecha, se inserta la tasa (CurrencyTo, Value, ValueReverse) asociada al EntityId; [INSERT] Payments.AccountPayableExchangeRate: Cuando EntityName=''AccountPayable'' y se encuentra tasa para la fecha, se inserta la tasa asociada al EntityId; [INSERT] Portfolio.PortfolioAdvanceExchangeRate: Cuando EntityName=''PortfolioAdvance'' y se encuentra tasa para la fecha, se inserta la tasa asociada al EntityId; [INSERT] Payments.DeferredCausationExchangeRate: Cuando EntityName=''DeferredCausation'' y se encuentra tasa para la fecha, se inserta la tasa asociada al EntityId; [RETURN_RESULT] OUTPUT @StateResult/@MessageOutput: Si parámetros obligatorios son nulos/vacíos/cero o EntityName no coincide con ninguno de los 4 tipos soportados, retorna StateResult=999; [RETURN_RESULT] OUTPUT @StateResult: Si no existen libros legales activos con moneda distinta a la de la entidad, retorna StateResult=0 sin insertar; [RETURN_RESULT] OUTPUT @MessageOutput: Si no se encuentra tasa de cambio para la moneda y la fecha durante el cursor, se asigna mensaje ''No se encuentra Tasa de cambio para la moneda de la transacción para la fecha <dd/mm/yyyy>'' con StateResult=999; [RETURN_RESULT] OUTPUT @MessageOutput: En caso de excepción capturada, retorna StateResult=999 y el mensaje de ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRateWithDate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe PortfolioAdvance ligado a CashReceipts cuyo EntityName=''Invoice'' y EntityName=''PortfolioAdvance'' → Activa modo CustomTRM=1: la búsqueda de tasa se realiza contra Billing.CustomTRM por rango de fechas else CustomTRM=0: la búsqueda de tasa se realiza contra Common.TRM por fecha exacta; si Existe registro con CurrencyId=EntityCurrencyId y OfficialCurrencyId=currencyId (moneda entidad como origen) → Inserta en @actualCurrencyRate usando Value como tasa directa y ValueOfficialToCurrency como reversa; si Existe registro con OfficialCurrencyId=EntityCurrencyId y CurrencyId=currencyId (moneda entidad como oficial) → Inserta en @actualCurrencyRate invirtiendo: ValueOfficialToCurrency como directa y Value como reversa; si EntityName no es AdvancePayments, AccountPayable, PortfolioAdvance ni DeferredCausation → Retorna StateResult=999 sin insertar', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRateWithDate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioAdvance; Treasury.CashReceipts; Common.Currency; GeneralLedger.LegalBook; Common.TRM; Billing.CustomTRM', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRateWithDate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRateWithDate';
-- GO
