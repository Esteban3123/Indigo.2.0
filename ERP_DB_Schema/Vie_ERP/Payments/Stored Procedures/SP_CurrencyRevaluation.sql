

-- =============================================
-- Author:      Juan David Capera N.
-- Create Date: 19-12-2022
-- Description: SP para revalorizar CxP
-- =============================================
CREATE PROCEDURE [Payments].[SP_CurrencyRevaluation]
(
    @month INT,
	@year INT,
	@status TINYINT,
	@userCode VARCHAR(50)
)
AS 
BEGIN 
	DECLARE @month_out INT,
			@year_out INT,
			@status_out TINYINT,
			@userCode_out VARCHAR(50)

	SET @month_out = @month
	SET @year_out = @year
	SET @status_out = @status
	SET @userCode_out  = @userCode

	BEGIN TRY

		/*------------------------------ DECLARACION DE VARIABLES -----------------------------*/
		DECLARE @currencyId INT,
				@OfficialCurrencyId INT, 
				@ProfitLostByExchangeCurrencyAccountId INT,
				@LostByExchangeCurrencyAccountId int,
				@ProfitLostJournalVoucherTypeId INT,
				@totalAdjusted NUMERIC(18,2), 
				@revaluationId INT,
				@CostCenterId INT,
				------------------------------
				@legalbookId INT,
				------------------------------
				@DateProcess AS date
		declare @LastDayMonth date

		DECLARE @actualCurrencyRate TABLE 
		(
			CurrencyFrom int,
			CurrencyTo int, 
			[Value] numeric(20, 5), 
			ValueReverse numeric(20, 5)
		)

		DECLARE @currencyAdjustmentDetail TABLE 
		(
			DocumentType tinyint, AccountPayableId INT null, AdvancePaymentId int null, Code VARCHAR(100), ThirdPartyId INT, CostCenterId INT, [Value] NUMERIC(18, 2), Balance NUMERIC(18, 2), CurrencyId INT, CurrencyConverterId INT, 
			ValueCurrency NUMERIC(20, 5), ValueCurrencyReverse NUMERIC(20, 5), ActualValueCurrency NUMERIC(20, 5), ActualValueCurrencyReverse NUMERIC(20, 5),
			MainAccountId int, LegalBookId int, BalanceConverted numeric(18, 2), ActualBalanceConverter numeric(18, 2),DeferredCausationId INT
		)

		--Se declara una tabla con los datos para la cabecera del comprobante contable 
		DECLARE @JournalVourcherTmp TABLE 
		(
			Id INT DEFAULT(0),
			Consecutive BIGINT DEFAULT(0),
			LegalBookId INT,
			IdJournalVoucher INT,
			VoucherDate VARCHAR(30),
			Imported VARCHAR(5) DEFAULT('False'),
			Status TINYINT,
			Detail VARCHAR(500),
			EntityCode VARCHAR(20),
			EntityId INT,
			EntityName VARCHAR(250),
			IsClosedYear TINYINT DEFAULT(0),
			CurrencyId int
		)

		--Se declara una tabla temporal para los detalles del comprobante
		DECLARE @JournalVourcherDetailTmp TABLE 
		(
			Id INT DEFAULT(0),
			IdAccounting INT DEFAULT(0),
			IdMainAccount INT,
			IdThirdParty INT,
			IdCostCenter INT,
			DebitValue DECIMAL(18,2),
			CreditValue DECIMAL(18,2),
			Detail VARCHAR(500),
			IdRetention INT,
			RetentionRate DECIMAL(6,3),
			BaseValue DECIMAL(18,2),
			BillingValue DECIMAL(18,2)
		)

		DECLARE @resultsTableJournalVoucher TABLE
		(
			MessageCode INT, 
			MessageVoucher VARCHAR(max), 
			JournalVoucherId INT
		)
			
		--Variable para obtener el xml
		DECLARE @JournalVoucherXML as XML

		/*----------------------------- OBTENCION  DE DATOS -----------------------------*/

		--Obtengo el libro oficial
		SET @legalbookId = (SELECT Id FROM GeneralLedger.LegalBook WHERE OfficialBook = 1)
		
		--Obtengo parametros
		SELECT TOP 1 
		@OfficialCurrencyId = OfficialCurrencyId, 
		@ProfitLostByExchangeCurrencyAccountId = ProfitLostByExchangeCurrencyAccountId,
		@ProfitLostJournalVoucherTypeId = ProfitLostJournalVoucherTypeId,
		@LostByExchangeCurrencyAccountId =LostByExchangeCurrencyAccountId,
		@CostCenterId = CostCenterId
		FROM GeneralLedger.CompanySettings

		-- Valido fechas
		set @LastDayMonth = DATEADD(day, -1, DATEADD(month, 1, cast(concat(@year,'-',@month,'-01') as date)))
		set @DateProcess = cast(Common.GETDATE() as date)
		

		if @status = 1 and @DateProcess < @LastDayMonth begin 
			THROW 50000, N'No se puede confirmar el ajuste si la fecha actual es menor al ultimo dia del mes del cierre', 1;
		end

		if @DateProcess > @LastDayMonth begin
			set @DateProcess = @LastDayMonth
		end


		/* ---------------------------- PROCESO ---------------------------- */
		IF EXISTS
		(
			SELECT 1
			FROM Payments.PaymentsRevaluation pr
			WHERE [pr].[Month] = @month_out AND [pr].[Year] = @year_out
				AND [pr].[Status] = 2
		)
		BEGIN
			SELECT 999 AS MessageCode,
				   'Ya existe una revalorizacion confirmada para este periodo' AS MessageVoucher,
				   0 AS JournalVoucherId
			RETURN 
		END
		ELSE IF NOT EXISTS
		(
			SELECT 1
			FROM Payments.PaymentsRevaluation pr
			WHERE [pr].[Month] = @month_out AND [pr].[Year] = @year_out
		)
		BEGIN
			INSERT INTO Payments.PaymentsRevaluation([Month], [Year], [Status], CreationUser, CreationDate)
			VALUES (@month_out, @year_out, 1, @userCode_out, Common.GETDATE())
		END

		SET @revaluationId = (SELECT Id FROM Payments.PaymentsRevaluation pr WHERE [pr].[Month] = @month_out AND [pr].[Year] = @year_out)

		DELETE FROM Payments.PaymentsRevaluationDetail WHERE PaymentsRevaluationId = @revaluationId

		DECLARE currency_cursor CURSOR FOR   
		SELECT DISTINCT OfficialCurrencyId 
		FROM GeneralLedger.LegalBook 
		WHERE [Status] = 1 
  
		OPEN currency_cursor  
		FETCH NEXT FROM currency_cursor   
		INTO @currencyId
  
		WHILE @@FETCH_STATUS = 0  
		BEGIN 
			
			IF NOT EXISTS
			(
				SELECT 1
				FROM GeneralLedger.LegalBook lb
				WHERE [lb].[Status] = 1
					AND lb.OfficialCurrencyId <> @currencyId
			)
			BEGIN 
				goto cont
			END

			DECLARE @CurrencyBookRevalueId INT

			DECLARE currencyRevalue_cursor CURSOR FOR   
			SELECT OfficialCurrencyId 
			FROM GeneralLedger.LegalBook 
			WHERE [Status] = 1 AND OfficialCurrencyId <> @currencyId 

			OPEN currencyRevalue_cursor  
			FETCH NEXT FROM currencyRevalue_cursor   
			INTO @CurrencyBookRevalueId

			WHILE @@FETCH_STATUS = 0  
			BEGIN
				DELETE FROM @actualCurrencyRate

				--Busco la tasa de conversion de la moneda con la fecha actual
				IF EXISTS
				(
					SELECT 1
					FROM Common.TRM 
					WHERE MeasurementDate = @DateProcess
						AND CurrencyId = @currencyId 
						AND OfficialCurrencyId = @CurrencyBookRevalueId
				)
				BEGIN
					INSERT INTO @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
					SELECT @currencyId, @CurrencyBookRevalueId, [Value], ValueOfficialToCurrency 
					FROM Common.TRM 
					WHERE MeasurementDate = @DateProcess
						AND CurrencyId = @currencyId 
						AND OfficialCurrencyId = @CurrencyBookRevalueId
				END
				ELSE IF EXISTS
				(
					SELECT 1
					FROM Common.TRM 
					WHERE MeasurementDate = @DateProcess
						AND OfficialCurrencyId = @currencyId 
						AND CurrencyId = @CurrencyBookRevalueId
				) 
				BEGIN
					INSERT INTO @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
					SELECT @currencyId, @CurrencyBookRevalueId, ValueOfficialToCurrency, [Value] 
					FROM Common.TRM 
					WHERE MeasurementDate = @DateProcess
					AND OfficialCurrencyId = @currencyId 
					AND CurrencyId = @CurrencyBookRevalueId
				END

				IF NOT EXISTS
				(
					SELECT 1
					FROM @actualCurrencyRate
				)
				BEGIN
					CLOSE currencyRevalue_cursor;  
					DEALLOCATE currencyRevalue_cursor;
					CLOSE currency_cursor;  
					DEALLOCATE currency_cursor;  
					SELECT 999 AS MessageCode, 
						  'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @CurrencyBookRevalueId)  +') para la fecha del cierre ' MessageVoucher,
					       0 AS JournalVoucherId
					RETURN
				END

				--------- Se valida que las cuentas por pagar tengan un TRM guardado -------
				IF EXISTS
				(
					SELECT 1 
					FROM Payments.AccountPayable ap 
					LEFT JOIN Payments.AccountPayableExchangeRate aper ON aper.AccountPayableId = ap.Id AND aper.CurrencyId = @CurrencyBookRevalueId
					WHERE ap.CurrencyId = @currencyId 
						AND aper.Id is NULL AND ap.Status =2
				)
				BEGIN 
					DECLARE @message VARCHAR(max) = (SELECT STRING_AGG(dateTRM, ', ') 
					FROM (
					SELECT DISTINCT  CAST(ap.DocumentDate as DATE) as dateTRM
					FROM Payments.AccountPayable ap 
					LEFT JOIN Payments.AccountPayableExchangeRate aper ON aper.AccountPayableId = ap.Id and aper.CurrencyId = @CurrencyBookRevalueId
					LEFT JOIN Common.TRM t on t.CurrencyId = IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) AND t.MeasurementDate = CAST(ap.DocumentDate AS DATE)
					WHERE ap.CurrencyId = @currencyId  AND ap.Status =2
						AND ap.Balance > 0 
						AND aper.id IS NULL 
						AND t.Id IS NULL
					) as dat)

					IF (@message IS NOT NULL AND LEN(@message) > 0) 
					BEGIN

						CLOSE currencyRevalue_cursor;  
						DEALLOCATE currencyRevalue_cursor;
						CLOSE currency_cursor;  
						DEALLOCATE currency_cursor;
						
						SELECT 999 AS MessageCode, 
							   'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @CurrencyBookRevalueId)  +') para las fechas ' + @message MessageVoucher, 
							   0 as JournalVoucherId
						RETURN
					END

					-- Inserto el TRM a la cxp
					INSERT INTO Payments.AccountPayableExchangeRate
					   ([AccountPayableId]
					   ,[CurrencyId]
					   ,[Value]
					   ,[ValueReverse])
					SELECT 
						ap.Id, 
						IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, @OfficialCurrencyId, t.CurrencyId), 
						IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, t.[Value], t.ValueOfficialToCurrency), 
						IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, t.ValueOfficialToCurrency, t.[Value])
					FROM Payments.AccountPayable ap 
					LEFT JOIN Payments.AccountPayableExchangeRate aper ON aper.AccountPayableId = ap.Id and aper.CurrencyId = @CurrencyBookRevalueId
					LEFT JOIN Common.TRM t ON t.CurrencyId = IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) AND t.MeasurementDate = CAST(ap.DocumentDate AS DATE)
					WHERE ap.CurrencyId = @currencyId AND ap.Status =2
						AND ap.Balance > 0 
						AND aper.id IS NULL

				END
				---------

				--------- Se valida que los anticipos tengan un TRM guardado -------
				IF EXISTS
				(
					SELECT 1 
					FROM Payments.AdvancePayments ap 
					LEFT JOIN Payments.AdvancePaymentsExchangeRate aper ON aper.AdvancePaymentsId = ap.Id AND aper.CurrencyId = @CurrencyBookRevalueId
					WHERE ap.CurrencyId = @currencyId 
						AND aper.Id is NULL
				)
				BEGIN 
					DECLARE @dateAdvances VARCHAR(max) = (SELECT STRING_AGG(dateTRM, ', ') 
					FROM (
					SELECT DISTINCT  CAST(ap.CreationDate as DATE) as dateTRM
					FROM Payments.AdvancePayments ap 
					LEFT JOIN Payments.AdvancePaymentsExchangeRate aper ON aper.AdvancePaymentsId = ap.Id and aper.CurrencyId = @CurrencyBookRevalueId
					LEFT JOIN Common.TRM t on t.CurrencyId = IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) AND t.MeasurementDate = CAST(ap.CreationDate AS DATE)
					WHERE ap.CurrencyId = @currencyId 
						AND ap.Balance > 0 
						AND aper.id IS NULL 
						AND t.Id IS NULL
					) as dat)

					IF (@dateAdvances IS NOT NULL AND LEN(@dateAdvances) > 0) 
					BEGIN

						CLOSE currencyRevalue_cursor;  
						DEALLOCATE currencyRevalue_cursor;
						CLOSE currency_cursor; 
						DEALLOCATE currency_cursor;
						
						SELECT 999 AS MessageCode, 
							   'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @CurrencyBookRevalueId)  +') para las fechas ' + @dateAdvances MessageVoucher, 
							   0 as JournalVoucherId
						RETURN
					END

					-- Inserto el TRM para los anticipos
					INSERT INTO Payments.AdvancePaymentsExchangeRate
					   (AdvancePaymentsId
					   ,[CurrencyId]
					   ,[Value]
					   ,[ValueReverse])
					SELECT 
						ap.Id, 
						IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, @OfficialCurrencyId, t.CurrencyId), 
						IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, t.[Value], t.ValueOfficialToCurrency), 
						IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, t.ValueOfficialToCurrency, t.[Value])
					FROM Payments.AdvancePayments ap 
					LEFT JOIN Payments.AdvancePaymentsExchangeRate aper ON aper.AdvancePaymentsId = ap.Id and aper.CurrencyId = @CurrencyBookRevalueId
					LEFT JOIN Common.TRM t ON t.CurrencyId = IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) AND t.MeasurementDate = CAST(ap.CreationDate AS DATE)
					WHERE ap.CurrencyId = @currencyId 
						AND ap.Balance > 0 
						AND aper.id IS NULL

				END

				/*--------- Se valida que los Diferidos tengan un TRM guardado -------*/
				IF EXISTS
				(
					SELECT 1 
					FROM Payments.DeferredCausation dc 
					JOIN Payments.AccountPayable ap WITH(NOLOCK) on ap.Id = dc.IdAccountPayable
					LEFT JOIN Payments.DeferredCausationExchangeRate dcer WITH(NOLOCK) ON dcer.DeferredCausationId = dc.Id AND dcer.CurrencyId = @CurrencyBookRevalueId
					WHERE ap.CurrencyId = @currencyId AND dc.Status =2
						  AND EXISTS( select 1 from payments.DeferredCausationShare where DeferredCausationId =dc.Id and Amortized =0) 
						  AND cast(ap.DocumentDate as date) <= @LastDayMonth
						  AND dcer.Id is NULL 
				)
				BEGIN 
					DECLARE @_messageDeferredCausation VARCHAR(max) = (SELECT STRING_AGG(dateTRM, ', ') 
																		FROM (
																				SELECT DISTINCT  CAST(ap.DocumentDate as DATE) as dateTRM
																				FROM Payments.DeferredCausation dc 
																				JOIN Payments.AccountPayable ap WITH(NOLOCK) on ap.Id = dc.IdAccountPayable 
																				LEFT JOIN Payments.DeferredCausationExchangeRate dcer WITH(NOLOCK) ON dcer.DeferredCausationId = dc.Id AND dcer.CurrencyId = @CurrencyBookRevalueId
																				LEFT JOIN Common.TRM t WITH(NOLOCK) on t.CurrencyId = IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) AND t.MeasurementDate = CAST(ap.DocumentDate AS DATE)
																				WHERE ap.CurrencyId = @currencyId AND dc.Status =2
																					  AND EXISTS( select 1 from payments.DeferredCausationShare where DeferredCausationId =dc.Id and Amortized =0) 
																					  AND dcer.id IS NULL 
																					  AND t.Id IS NULL
																					  AND cast(ap.DocumentDate as date) <= @LastDayMonth
																				) as dat)

					IF (COALESCE(@_messageDeferredCausation,'') <> '') 
					BEGIN

						CLOSE currencyRevalue_cursor;  
						DEALLOCATE currencyRevalue_cursor;
						CLOSE currency_cursor; 
						DEALLOCATE currency_cursor;
						
						SELECT 999 AS MessageCode, 
							   'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @CurrencyBookRevalueId)  +') para las fechas ' + @dateAdvances MessageVoucher, 
							   0 as JournalVoucherId
						RETURN
					END

					-- Inserto el TRM para los Diferidos
					INSERT INTO Payments.DeferredCausationExchangeRate
					   (DeferredCausationId
					   ,[CurrencyId]
					   ,[Value]
					   ,[ValueReverse])
					SELECT 
						dc.Id, 
						IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, @OfficialCurrencyId, t.CurrencyId), 
						IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, t.[Value], t.ValueOfficialToCurrency), 
						IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, t.ValueOfficialToCurrency, t.[Value])
					FROM Payments.DeferredCausation dc
					JOIN Payments.AccountPayable ap WITH(NOLOCK) ON dc.IdAccountPayable = ap.Id
					JOIN Common.TRM t ON t.CurrencyId = IIF(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) AND t.MeasurementDate = CAST(ap.DocumentDate AS DATE)
					LEFT JOIN Payments.DeferredCausationExchangeRate dcer WITH(NOLOCK) ON dcer.DeferredCausationId = dc.Id AND dcer.CurrencyId = @CurrencyBookRevalueId					
					WHERE ap.CurrencyId = @currencyId AND dc.Status =2
						AND EXISTS( select 1 from payments.DeferredCausationShare where DeferredCausationId =dc.Id and Amortized =0)  
						AND cast(ap.DocumentDate as date) <= @LastDayMonth
						AND dcer.id IS NULL

				END
				------ FIN Se valida que los diferidos con cuotas pendientes tengan un TRM guardado -------

				INSERT INTO  @currencyAdjustmentDetail (
					DocumentType, AccountPayableId, Code, ThirdPartyId, CostCenterId, [Value], Balance, CurrencyId, 
					CurrencyConverterId, ValueCurrency, ValueCurrencyReverse, ActualValueCurrency, ActualValueCurrencyReverse,
					MainAccountId, LegalBookId, BalanceConverted, ActualBalanceConverter)
				SELECT 1, ap.Id, ap.Code, ap.IdThirdParty, ap.IdCostCenter, ap.[Value], ap.Balance, ap.CurrencyId, 
					aper.CurrencyId, aper.[Value], aper.ValueReverse, acr.[Value], acr.ValueReverse, 
					ap.IdAccount, ma.LegalBookId, [Portfolio].fnConvertValueBasedOnExchangeRate(ap.Balance, aper.[Value], aper.ValueReverse) as BalanceConverted, 
				[Portfolio].fnConvertValueBasedOnExchangeRate(ap.Balance, acr.[Value] , acr.ValueReverse) as ActualValueConvertedPaymnts
				FROM Payments.AccountPayable ap
				JOIN GeneralLedger.MainAccounts ma on ma.Id = ap.IdAccount
				JOIN Payments.AccountPayableExchangeRate aper ON aper.AccountPayableId = ap.Id and aper.CurrencyId = @CurrencyBookRevalueId
				JOIN @actualCurrencyRate acr ON 1 = 1
				WHERE ap.CurrencyId = @currencyId AND ap.Status =2 AND cast(ap.DocumentDate AS DATE) <= @LastDayMonth
					AND ap.Balance > 0

				INSERT INTO  @currencyAdjustmentDetail (
					DocumentType, AdvancePaymentId, Code, ThirdPartyId, CostCenterId, [Value], Balance, CurrencyId, 
					CurrencyConverterId, ValueCurrency, ValueCurrencyReverse, ActualValueCurrency, ActualValueCurrencyReverse,
					MainAccountId, LegalBookId, BalanceConverted, ActualBalanceConverter)
				SELECT 2, ap.Id, ap.Code, ap.IdThirdParty, ap.IdCostCenter, ap.[Value], ap.Balance, ap.CurrencyId, 
					aper.CurrencyId, aper.[Value], aper.ValueReverse, acr.[Value], acr.ValueReverse, 
					ap.IdAccount, ma.LegalBookId, [Portfolio].fnConvertValueBasedOnExchangeRate(ap.Balance, aper.[Value], aper.ValueReverse) as BalanceConverted, 
				[Portfolio].fnConvertValueBasedOnExchangeRate(ap.Balance, acr.[Value] , acr.ValueReverse) as ActualValueConvertedPaymnts
				FROM Payments.AdvancePayments ap
				JOIN GeneralLedger.MainAccounts ma on ma.Id = ap.IdAccount
				JOIN Payments.AdvancePaymentsExchangeRate aper ON aper.AdvancePaymentsId = ap.Id and aper.CurrencyId = @CurrencyBookRevalueId
				JOIN @actualCurrencyRate acr ON 1 = 1
				WHERE ap.CurrencyId = @currencyId AND CAST(ap.DocumentDate AS DATE) <= @LastDayMonth
					AND ap.Balance > 0
				
				/* Segemento Diferidos */
				INSERT INTO  @currencyAdjustmentDetail (
						DocumentType,--1
						AccountPayableId,--2
						DeferredCausationId,--3
						Code,--4
						ThirdPartyId,--5
						CostCenterId,--6
						[Value],--7
						Balance,--8
						CurrencyId,--9 
						CurrencyConverterId,--10
						ValueCurrency,--11
						ValueCurrencyReverse,--12
						ActualValueCurrency, --13
						ActualValueCurrencyReverse,--14
						MainAccountId,--15
						LegalBookId,--16
						BalanceConverted,--17 
						ActualBalanceConverter) --18
				SELECT	3,--Diferidos --1
						ap.Id, --2
						dc.Id,--3
						ap.Code, --4
						dc.IdThirdParty, --5
						dc.IdCostCenter, --6
						dc.ValueCreditPeriod,--7
						dcs.Balance,--8
						ap.CurrencyId, --9
						dcer.CurrencyId,--10
						dcer.[Value], --11
						dcer.ValueReverse, --12
						acr.[Value], --13
						acr.ValueReverse, --14
						dc.IdMainAccount,--15
						ma.LegalBookId,--16
						[Portfolio].fnConvertValueBasedOnExchangeRate(dcs.Balance, dcer.[Value], dcer.ValueReverse) as BalanceConverted, --17
						[Portfolio].fnConvertValueBasedOnExchangeRate(dcs.Balance, acr.[Value] , acr.ValueReverse) as ActualValueConvertedPaymnts --18
				FROM Payments.DeferredCausation dc
				JOIN Payments.AccountPayable ap WITH(NOLOCK) ON dc.IdAccountPayable = ap.Id
				JOIN (	SELECT dcs.DeferredCausationId,sum(dcs.Value) Balance
						FROM Payments.DeferredCausationShare dcs WITH (NOLOCK)
						where dcs.Amortized =0
						GROUP by dcs.DeferredCausationId) dcs ON dc.Id = dcs.DeferredCausationId 
				JOIN GeneralLedger.MainAccounts ma on ma.Id = dc.IdMainAccount
				JOIN Payments.DeferredCausationExchangeRate dcer ON dcer.DeferredCausationId = dc.Id and dcer.CurrencyId = @CurrencyBookRevalueId
				JOIN @actualCurrencyRate acr ON 1 = 1
				WHERE ap.CurrencyId = @currencyId and dc.Status =2 AND cast(ap.DocumentDate as date) <= @LastDayMonth AND dcs.Balance > 0
				/*FIN Segemento Diferidos */

				--inserto en la tabla de detalle de revalorizacion
				INSERT INTO [Payments].[PaymentsRevaluationDetail]
			   ([PaymentsRevaluationId]
			   ,[DocumentType]
			   ,[AccountPayableId]
			   ,[AdvancePaymentId]
			   ,[ThirdPartyId]
			   ,[CostCenterId]
			   ,[Value]
			   ,[Balance]
			   ,[CurrencyId]
			   ,[CurrencyConverterId]
			   ,[ValueCurrency]
			   ,[ValueCurrencyReverse]
			   ,[ActualValueCurrency]
			   ,[ActualValueCurrencyReverse]
			   ,[MainAccountId]
			   ,[LegalBookId]
			   ,[BalanceConverted]
			   ,[ActualBalanceConverter]
			   ,[ProfitLostValue]
			   ,[DeferredCausationId])
			   SELECT 
			   @revaluationId
			   ,[DocumentType]
			   ,[AccountPayableId]
			   ,[AdvancePaymentId]
			   ,[ThirdPartyId]
			   ,[CostCenterId]
			   ,[Value]
			   ,[Balance]
			   ,[CurrencyId]
			   ,[CurrencyConverterId]
			   ,[ValueCurrency]
			   ,[ValueCurrencyReverse]
			   ,[ActualValueCurrency]
			   ,[ActualValueCurrencyReverse]
			   ,[MainAccountId]
			   ,[LegalBookId]
			   ,[BalanceConverted]
			   ,[ActualBalanceConverter]
			   ,iif([DocumentType] = 1, [BalanceConverted] - [ActualBalanceConverter], [ActualBalanceConverter] - [BalanceConverted])  -- Ojo Aca es al reves que en CxC pues si el saldo actual de CxP es mayor seria una perdida pues deberia mas, en anticipos si funciona igual que una CxC
			   ,[DeferredCausationId]
			   FROM @currencyAdjustmentDetail

			   --Busco el libro contable que tenga la moneda que estoy valorizando
				DECLARE @legalbookMovementId INT 

				SET @legalbookMovementId = (SELECT Id FROM GeneralLedger.LegalBook WHERE OfficialCurrencyId = @CurrencyBookRevalueId)

				
				/***********************************/
				declare @documentType tinyint
				DECLARE documentType_Cursor CURSOR FOR   
				SELECT DocumentType 
				FROM [Payments].[PaymentsRevaluationDetail] as cad
				WHERE cad.PaymentsRevaluationId = @revaluationId and CurrencyConverterId = @CurrencyBookRevalueId
				GROUP BY DocumentType

				OPEN documentType_Cursor  
				FETCH NEXT FROM documentType_Cursor   
				INTO @documentType
  
				WHILE @@FETCH_STATUS = 0  
				BEGIN 
					
					/*----------------------------- CONTABILIZACIÓN -----------------------------*/
				INSERT INTO @JournalVourcherTmp
				(
					LegalBookId, 
					IdJournalVoucher, 
					VoucherDate, 
					Status, 
					Detail, 
					EntityCode, 
					EntityId, 
					EntityName,
					CurrencyId
				)
				VALUES
				(
					@legalbookMovementId, 
					@ProfitLostJournalVoucherTypeId, 
					@DateProcess, 
					2, 
					CONCAT('Revalorización de pagos ', CASE @documentType
														WHEN 1 THEN 'Facturas'
														WHEN 2 THEN 'Anticipos'
														WHEN 3 THEN 'Diferidos'
														ELSE '' END), 
					null, 
					null, 
					'JournalVouchers',
					@CurrencyBookRevalueId
				)

				IF (@legalbookMovementId = @legalbookId)
				BEGIN
					INSERT INTO @JournalVourcherDetailTmp 
					(IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
					SELECT 
					cad.MainAccountId, 
					IIF(ma.HandlesThirdParty = 1, cad.ThirdPartyId, null),
					IIF(ma.HandlesCostCenter = 1, cad.CostCenterId, null),
					IIF(cad.ProfitLostValue > 0, ABS(cad.ProfitLostValue), 0),
					IIF(cad.ProfitLostValue > 0, 0, ABS(cad.ProfitLostValue)),
					concat('Documento no : ',isnull(ap.BillNumber,ad.Code)), null, null, null, null
					FROM [Payments].[PaymentsRevaluationDetail] cad
					JOIN GeneralLedger.MainAccounts ma on ma.Id = cad.MainAccountId
					LEFT JOIN Payments.AccountPayable ap on cad.AccountPayableId = ap.Id
					LEFT join Payments.AdvancePayments ad on cad.AdvancePaymentId = ad.Id
					WHERE cad.PaymentsRevaluationId = @revaluationId and CurrencyConverterId = @CurrencyBookRevalueId
							and cad.DocumentType =@documentType

					--Calculo la contra partida para balancear el comprobante
					SET @totalAdjusted = (
						SELECT SUM(cad.ProfitLostValue)
						FROM [Payments].[PaymentsRevaluationDetail] cad
						JOIN GeneralLedger.MainAccounts ma on ma.Id = cad.MainAccountId
						WHERE cad.PaymentsRevaluationId = @revaluationId and CurrencyConverterId = @CurrencyBookRevalueId
							AND cad.DocumentType =@documentType
					)

					INSERT INTO @JournalVourcherDetailTmp 
							(	IdMainAccount, 
								IdThirdParty,
								IdCostCenter,
								DebitValue,
								CreditValue,
								Detail,
								IdRetention,
								RetentionRate,
								BaseValue,
								BillingValue)

					SELECT top 1	ma.Id,
									NULL,
									IIF(ma.HandlesCostCenter =1,@CostCenterId,NULL),
									IIF(@totalAdjusted > 0, 0, ABS(@totalAdjusted)),
									IIF(@totalAdjusted > 0, ABS(@totalAdjusted), 0),
									'',
									null,
									null,
									null,
									null
					FROM GeneralLedger.MainAccounts ma WITH(NOLOCK)
					where ma.Id = IIF(@totalAdjusted > 0, @ProfitLostByExchangeCurrencyAccountId, @LostByExchangeCurrencyAccountId)
				END
				ELSE
				BEGIN
					INSERT INTO @JournalVourcherDetailTmp 
					(IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
					SELECT 
					cad.MainAccountId, 
					IIF(ma.HandlesThirdParty = 1, cad.ThirdPartyId, null),
					IIF(ma.HandlesCostCenter = 1, cad.CostCenterId, null),
					IIF(cad.ProfitLostValue > 0, ABS(cad.ProfitLostValue), 0),
					IIF(cad.ProfitLostValue > 0, 0, ABS(cad.ProfitLostValue)),
					concat('Documento no : ',isnull(ap.BillNumber,ad.Code)), null, null, null, null
					FROM [Payments].[PaymentsRevaluationDetail] cad
					JOIN GeneralLedger.HomologationAccount ha ON ha.OfficialMainAccountId = cad.MainAccountId
					JOIN GeneralLedger.MainAccounts ma ON ma.Id = ha.MainAccountId and ma.LegalBookId = @legalbookMovementId
					LEFT JOIN Payments.AccountPayable ap on cad.AccountPayableId = ap.Id
					LEFT JOIN Payments.AdvancePayments ad on cad.AdvancePaymentId = ad.Id
					WHERE cad.PaymentsRevaluationId = @revaluationId and CurrencyConverterId = @CurrencyBookRevalueId
						AND cad.DocumentType =@documentType

					--Calculo la contra partida para balancear el comprobante
					SET @totalAdjusted = (
						SELECT SUM(cad.ProfitLostValue)
						FROM [Payments].[PaymentsRevaluationDetail] cad
						JOIN GeneralLedger.MainAccounts ma on ma.Id = cad.MainAccountId
						WHERE cad.PaymentsRevaluationId = @revaluationId and CurrencyConverterId = @CurrencyBookRevalueId
						AND cad.DocumentType =@documentType
					)

					INSERT INTO @JournalVourcherDetailTmp 
								(IdMainAccount,
								IdThirdParty,
								IdCostCenter,
								DebitValue, 
								CreditValue,
								Detail, 
								IdRetention,
								RetentionRate,
								BaseValue, 
								BillingValue)

					SELECT TOP 1 ma.Id,
								NULL,
								IIF(ma.HandlesCostCenter=1,@CostCenterId,NULL),
								IIF(@totalAdjusted > 0, 0, abs(@totalAdjusted)),
								IIF(@totalAdjusted > 0, abs(@totalAdjusted), 0),
								'', 
								null,
								null, 
								null,
								null
					FROM GeneralLedger.HomologationAccount ha WITH(NOLOCK)
					JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ha.MainAccountId and ma.LegalBookId = @legalbookMovementId
					WHERE ha.OfficialMainAccountId =IIF(@totalAdjusted > 0, @ProfitLostByExchangeCurrencyAccountId, @LostByExchangeCurrencyAccountId)					
				END

				--Eliminamos cuentas en 0
				delete from @JournalVourcherDetailTmp where DebitValue = 0 and CreditValue = 0

				---------------
				--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
				SELECT @JournalVoucherXML = CONVERT(xml, 
					(
						SELECT * FROM @JournalVourcherTmp JournalVoucher 
						JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
						For xml AUTO,TYPE, ELEMENTS
					)
				)

				IF (@status = 1) BEGIN

					--Se consume el sp que guarda el movimiento contable
					insert @resultsTableJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@userCode_out 	
				END

				DELETE FROM @JournalVourcherTmp
				DELETE FROM @JournalVourcherDetailTmp
				
				NEXT_ROW:
				FETCH NEXT FROM documentType_Cursor   
				INTO @documentType

				END
				CLOSE documentType_Cursor;  
				DEALLOCATE documentType_Cursor;
				/***********************************/
				IF (@status = 1) BEGIN
					UPDATE aper SET aper.[Value] = acr.[Value] , aper.ValueReverse = acr.ValueReverse
					FROM Payments.AccountPayable ap
					JOIN Payments.AccountPayableExchangeRate aper on aper.AccountPayableId = ap.Id and aper.CurrencyId = @CurrencyBookRevalueId
					JOIN @actualCurrencyRate acr on 1 = 1
					WHERE ap.CurrencyId = @currencyId and ap.Status=2
						AND ap.Balance > 0 AND CAST(ap.DocumentDate as DATE) <= @LastDayMonth

					UPDATE aper SET aper.[Value] = acr.[Value] , aper.ValueReverse = acr.ValueReverse
					FROM Payments.AdvancePayments ap WITH(NOLOCK)
					JOIN Payments.AdvancePaymentsExchangeRate aper WITH(NOLOCK) on aper.AdvancePaymentsId = ap.Id and aper.CurrencyId = @CurrencyBookRevalueId
					JOIN @actualCurrencyRate acr on 1 = 1
					WHERE ap.CurrencyId = @currencyId 
						AND ap.Balance > 0 AND CAST(ap.CreationDate as DATE) <= @LastDayMonth

					UPDATE dcer SET dcer.[Value] = acr.[Value] , dcer.ValueReverse = acr.ValueReverse
					FROM Payments.DeferredCausation dc WITH(NOLOCK)
					JOIN Payments.AccountPayable ap WITH(NOLOCK) on dc.IdAccountPayable =ap.Id
					JOIN Payments.DeferredCausationExchangeRate dcer on dcer.DeferredCausationId = dc.Id and dcer.CurrencyId = @CurrencyBookRevalueId
					JOIN @actualCurrencyRate acr on 1 = 1
					WHERE ap.CurrencyId = @currencyId and dc.Status =2
						AND EXISTS( select 1 from payments.DeferredCausationShare where DeferredCausationId =dc.Id and Amortized =0)
						AND CAST(ap.DocumentDate as DATE) <= @LastDayMonth

					UPDATE Payments.PaymentsRevaluation  SET [Status] = 2 WHERE Id = @revaluationId
				END
						
				DELETE FROM @currencyAdjustmentDetail
				
				FETCH NEXT FROM currencyRevalue_cursor   
				INTO @CurrencyBookRevalueId
			END
			CLOSE currencyRevalue_cursor;  
			DEALLOCATE currencyRevalue_cursor;

			cont:
			FETCH NEXT FROM currency_cursor   
			INTO @currencyId
		END
		CLOSE currency_cursor;  
		DEALLOCATE currency_cursor;  
		
		IF (@status = 1) begin
			SELECT *
			FROM @resultsTableJournalVoucher
		END
		ELSE BEGIN
			SELECT 0 MessageCode, 'Se calculo correctamente la valorizacion' MessageVoucher, 0 as JournalVoucherId
		END
	END TRY
	BEGIN CATCH 
		SELECT 999 MessageCode, ERROR_MESSAGE() AS MessageVoucher, 0 AS JournalVoucherId
	END CATCH
END