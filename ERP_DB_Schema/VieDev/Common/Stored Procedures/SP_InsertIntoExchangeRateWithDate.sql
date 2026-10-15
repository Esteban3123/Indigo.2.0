
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