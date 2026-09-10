-- =============================================
-- Author:      Cristhian Salazar
-- Create Date: 19-12-2022
-- Description: SP para revalorizar la cartera 
-- =============================================
CREATE PROCEDURE [Portfolio].[SPCurrencyRevaluation]
(
    @month int,
	@year int,
	@status tinyint,
	@userCode varchar(50)
)
WITH RECOMPILE  
AS
BEGIN

	declare @resultsTable table(MessageCode int, MessageVoucher varchar(max), JournalVoucherId int)
	begin try

		declare @currencyId int
		declare @OfficialCurrencyId int,
				@ProfitLostByExchangeCurrencyAccountId int,
				@ProfitLostJournalVoucherTypeId int,
				@LostByExchangeCurrencyAccountId int,
				@CostCenterId INT
		declare @actualCurrencyRate table (CurrencyFrom int, CurrencyTo int, [Value] numeric(20, 5), ValueReverse numeric(20, 5))
		declare @totalAdjusted numeric(18,2), @revaluationId int
		declare @LastDayMonth date
		declare @DateTRM date

		--Tabla RESULTADO
		IF OBJECT_ID('tempdb..#AccountReceivableAtCut') IS NOT NULL DROP TABLE #AccountReceivableAtCut
		IF OBJECT_ID('tempdb..#PortfolioAdvanceAtCut') IS NOT NULL DROP TABLE #PortfolioAdvanceAtCut

		CREATE TABLE #AccountReceivableAtCut  ( Id INT,
												CurrencyId INT,
												Balance NUMERIC(20,2),
												MainAccountId INT,
												AccountReceivableDate DATETIME,
												ThirdPartyId INT,
												CostCenterId INT,
												[Value] NUMERIC(20,2),
												Code VARCHAR(25),
												[Status] TINYINT
												)

		CREATE TABLE #PortfolioAdvanceAtCut  (	Id INT,
												CurrencyId INT,
												Balance NUMERIC(20,2),
												MainAccountId INT,
												DocumentDate DATETIME,
												ThirdPartyId INT,
												CostCenterId INT,
												[Value] NUMERIC(20,2),
												Code VARCHAR(20),
												[Status] TINYINT)

		declare @legalbookId int 
		set @legalbookId = (select Id from GeneralLedger.LegalBook where OfficialBook = 1)

		set @LastDayMonth =  DATEADD(day, -1, DATEADD(month, 1, cast(concat(@year,'-',@month,'-01') as date)))
		set @DateTRM = cast(Common.GETDATE() as date)

		if @status = 1 and @DateTRM < @LastDayMonth begin 
			THROW 50000, N'No se puede confirmar el ajuste si la fecha actual es menor al ultimo dia del mes del cierre', 1;
		end

		if @DateTRM > @LastDayMonth begin
			set @DateTRM = @LastDayMonth
		end

		--Variable para obtener el xml
		DECLARE @JournalVoucherXML as XML

		declare @currencyAdjustmentDetail table (
			DocumentType TINYINT, PortfolioAdvanceId INT NULL,
			AccountReceivableId INT NULL, Code varchar(100), ThirdPartyId int, CostCenterId int, [Value] numeric(18, 2), Balance numeric(18, 2), CurrencyId int, 
			CurrencyConverterId int, ValueCurrency numeric(20, 5), ValueCurrencyReverse numeric(20, 5), ActualValueCurrency numeric(20, 5), ActualValueCurrencyReverse numeric(20, 5),
			MainAccountId int, LegalBookId int, BalanceConverted numeric(18, 2), ActualBalanceConverter numeric(18, 2)
		)

		select top 1 
		@OfficialCurrencyId = OfficialCurrencyId, 
		@ProfitLostByExchangeCurrencyAccountId = ProfitLostByExchangeCurrencyAccountId,
		@ProfitLostJournalVoucherTypeId = ProfitLostJournalVoucherTypeId,
		@LostByExchangeCurrencyAccountId =LostByExchangeCurrencyAccountId,
		@CostCenterId = CostCenterId
		from GeneralLedger.CompanySettings

		if (select count(*) from [Portfolio].[Revaluation] where [Month] = @month and [Year] = @year and [Status] = 2) > 0 begin 
			THROW 50000, N'Ya existe una revalorizacion confirmada para este periodo', 1;
		end

		if (select count(*) from [Portfolio].[Revaluation] where [Month] = @month and [Year] = @year) = 0 begin
			--inserto la cabecera de la transaccion del la revalorizacion
			INSERT INTO [Portfolio].[Revaluation]
				   ([Month]
				   ,[Year]
				   ,[Status]
				   ,[CreationUser]
				   ,[CreationDate])
			values (@month, @year, 1, @userCode, Common.GETDATE())
		end

		set @revaluationId = (select Id from [Portfolio].[Revaluation] where [Month] = @month and [Year] = @year)
		delete from Portfolio.RevaluationDetail where RevaluationId = @revaluationId

		/******************** Se consume Cartera por edades para obtener el saldo del Corte a la fecha **********************/
			INSERT INTO #AccountReceivableAtCut(Id,
												CurrencyId,
												Balance,
												MainAccountId,
												AccountReceivableDate,
												ThirdPartyId,
												CostCenterId,
												[Value],
												Code,
												[Status])
			SELECT	ar.Id,
					ar.CurrencyId,
					ar.Balance,
					ar.MainAccountId,
					ar.AccountReceivableDate,
					ar.ThirdPartyId,
					arO.CostCenterId,
					arO.[Value],
					arO.Code,
					arO.[Status]
			FROM [Portfolio].[GetAccountReceivableByAge](NULL, @LastDayMonth) AS ar	
			JOIN Portfolio.AccountReceivable arO WITH(NOLOCK) on ar.Id =arO.Id 
   			WHERE ar.Balance > 0

			INSERT INTO #PortfolioAdvanceAtCut(	Id,
												CurrencyId,
												Balance,
												MainAccountId,
												DocumentDate,
												ThirdPartyId,
												CostCenterId,
												[Value],
												Code,
												[Status])
			SELECT	pa.Id,
					pa.CurrencyId,
					pa.Balance,
					pa.MainAccountId,
					pa.DocumentDate,
					pa.ThirdPartyId,
					paO.CostCenterId,
					paO.[value],
					pa.Code,
					paO.[Status]
			FROM [Portfolio].[GetPortfolioAdvanceByAge](@LastDayMonth) pa
			JOIN Portfolio.PortfolioAdvance paO WITH(NOLOCK) ON pa.Id = paO.Id
   			WHERE pa.Balance > 0

			--RETURN
		/*******************************************************************************************************************/

		DECLARE currency_cursor CURSOR FOR   
		select distinct OfficialCurrencyId from GeneralLedger.LegalBook where [Status] = 1 

		OPEN currency_cursor  

		FETCH NEXT FROM currency_cursor   
		INTO @currencyId

		WHILE @@FETCH_STATUS = 0  
		BEGIN    

			--Valido que hayan libros en contabilidad con monedas diferente a la que sesta analizando, si es asi entonces tengo que revalorizar mi cartera
			if ((select count(OfficialCurrencyId) from GeneralLedger.LegalBook where [Status] = 1 and OfficialCurrencyId <> @currencyId) = 0) begin
				goto cont
			end

			declare @CurrencyBookRevalueId int
			DECLARE currencyRevalue_cursor CURSOR FOR   
			select OfficialCurrencyId from GeneralLedger.LegalBook where [Status] = 1 and OfficialCurrencyId <> @currencyId 

			OPEN currencyRevalue_cursor  

			FETCH NEXT FROM currencyRevalue_cursor   
			INTO @CurrencyBookRevalueId

			WHILE @@FETCH_STATUS = 0  
			BEGIN 
				delete from @actualCurrencyRate
				--Busco la tasa de conversion de la moneda con la fecha actual
				if (select count(*) from Common.TRM where MeasurementDate = @DateTRM and CurrencyId = @currencyId and OfficialCurrencyId = @CurrencyBookRevalueId) > 0 begin
					insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
					select @currencyId, @CurrencyBookRevalueId, [Value], ValueOfficialToCurrency 
					from Common.TRM where MeasurementDate = @DateTRM and CurrencyId = @currencyId and OfficialCurrencyId = @CurrencyBookRevalueId
				end
				else if (select count(*) from Common.TRM where MeasurementDate = @DateTRM and OfficialCurrencyId = @currencyId and CurrencyId = @CurrencyBookRevalueId) > 0 begin
					insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
					select @currencyId, @CurrencyBookRevalueId, ValueOfficialToCurrency, [Value] 
					from Common.TRM where MeasurementDate = @DateTRM and OfficialCurrencyId = @currencyId and CurrencyId = @CurrencyBookRevalueId
				end

				if (select count(*) from @actualCurrencyRate) = 0 begin
					CLOSE currencyRevalue_cursor;  
						DEALLOCATE currencyRevalue_cursor;
						CLOSE currency_cursor;  
						DEALLOCATE currency_cursor;  
						SELECT 999 AS MessageCode, 'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @CurrencyBookRevalueId)  +') para la fecha del cierre ' MessageVoucher, 0 as JournalVoucherId
						return
				end

				declare @message varchar(max)
				if (select count(*) 
					from #PortfolioAdvanceAtCut pa 
					left join Portfolio.PortfolioAdvanceExchangeRate par on par.PortfolioAdvanceId = pa.Id and par.CurrencyId = @CurrencyBookRevalueId
					where pa.CurrencyId = @currencyId and par.Id is null and CAST(pa.DocumentDate as DATE) <= @LastDayMonth ) > 0 begin 

					--Valido que haya TRM para los anticipos
					set @message = (select STRING_AGG(dateTRM, ', ') 
					from (
					select distinct  cast(pa.DocumentDate as date) as dateTRM
					from #PortfolioAdvanceAtCut pa
					left join Portfolio.PortfolioAdvanceExchangeRate par on par.PortfolioAdvanceId = pa.Id and par.CurrencyId = @CurrencyBookRevalueId
					left join Common.TRM t on t.CurrencyId = iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) and t.MeasurementDate = cast(pa.DocumentDate as date)
					where pa.CurrencyId = @currencyId and Balance > 0 and par.id is null and t.Id is null and CAST(pa.DocumentDate as DATE) <= @LastDayMonth
					) as dat)

					if (@message is not null and len(@message) > 0) begin
						CLOSE currencyRevalue_cursor;  
						DEALLOCATE currencyRevalue_cursor;
						CLOSE currency_cursor;  
						DEALLOCATE currency_cursor;  
						select 999 AS MessageCode, 'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @CurrencyBookRevalueId)  +') para las fechas ' + @message MessageVoucher, 0 as JournalVoucherId
						return
					END

					-- Inserto el TRM al anticipo
					INSERT INTO [Portfolio].[PortfolioAdvanceExchangeRate]
					   ([PortfolioAdvanceId]
					   ,[CurrencyId]
					   ,[Value]
					   ,[ValueReverse])
					select pa.Id, iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @OfficialCurrencyId, t.CurrencyId), iif(@CurrencyBookRevalueId = @OfficialCurrencyId, t.[Value], t.ValueOfficialToCurrency), iif(@CurrencyBookRevalueId = @OfficialCurrencyId, t.ValueOfficialToCurrency, t.[Value])
					from #PortfolioAdvanceAtCut pa
					left join Portfolio.PortfolioAdvanceExchangeRate par on par.PortfolioAdvanceId = pa.Id and par.CurrencyId = @CurrencyBookRevalueId
					left join Common.TRM t on t.CurrencyId = iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) and t.MeasurementDate = cast(pa.DocumentDate as date)
					where pa.CurrencyId = @currencyId and Balance > 0 and par.id is null and CAST(pa.DocumentDate as DATE) <= @LastDayMonth

				END

				if (select count(*)
					from #AccountReceivableAtCut ar
					left join Portfolio.AccountReceivableExchangeRate er on er.AccountReceivableId = ar.Id and er.CurrencyId = @CurrencyBookRevalueId
					where ar.CurrencyId = @currencyId and er.Id is null AND CAST(ar.AccountReceivableDate AS DATE) <= @LastDayMonth) > 0 begin 

					--Valido que haya TRM para las cuentas por cobrar
					set @message = (select STRING_AGG(dateTRM, ', ') 
					from (
					select distinct  cast(ar.AccountReceivableDate as date) as dateTRM
					from #AccountReceivableAtCut ar
					left join Portfolio.AccountReceivableExchangeRate er on er.AccountReceivableId = ar.Id and er.CurrencyId = @CurrencyBookRevalueId
					left join Common.TRM t on t.CurrencyId = iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) and t.MeasurementDate = cast(ar.AccountReceivableDate as date)
					where ar.CurrencyId = @currencyId and Balance > 0 and er.id is null and t.Id is null AND CAST(ar.AccountReceivableDate AS DATE) <= @LastDayMonth
					) as dat)

					if (@message is not null and len(@message) > 0) begin
						CLOSE currencyRevalue_cursor;  
						DEALLOCATE currencyRevalue_cursor;
						CLOSE currency_cursor;  
						DEALLOCATE currency_cursor;  
						select 999 AS MessageCode, 'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @CurrencyBookRevalueId)  +') para las fechas ' + @message MessageVoucher, 0 as JournalVoucherId
						return
					END

					-- Inserto el TRM a la cuenta por cobrar
					INSERT INTO [Portfolio].[AccountReceivableExchangeRate]
					   ([AccountReceivableId]
					   ,[CurrencyId]
					   ,[Value]
					   ,[ValueReverse])
					select	ar.Id,
							iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @OfficialCurrencyId, t.CurrencyId),
							iif(@CurrencyBookRevalueId = @OfficialCurrencyId, t.[Value], t.ValueOfficialToCurrency),
							iif(@CurrencyBookRevalueId = @OfficialCurrencyId, t.ValueOfficialToCurrency, t.[Value])
					from #AccountReceivableAtCut ar
					left join Portfolio.AccountReceivableExchangeRate er on er.AccountReceivableId = ar.Id and er.CurrencyId = @CurrencyBookRevalueId
					left join Common.TRM t on t.CurrencyId = iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) and t.MeasurementDate = cast(ar.AccountReceivableDate as date)
					where ar.CurrencyId = @currencyId and Balance > 0 and er.id is NULL AND CAST(ar.AccountReceivableDate AS DATE) <= @LastDayMonth

				end

				-- Inserto las cuentas por cobrar 
				insert into  @currencyAdjustmentDetail (
								DocumentType,
								AccountReceivableId,
								Code,
								ThirdPartyId,
								CostCenterId,
								[Value],
								Balance, 
								CurrencyId, 
								CurrencyConverterId,
								ValueCurrency,
								ValueCurrencyReverse,
								ActualValueCurrency,
								ActualValueCurrencyReverse,
								MainAccountId,
								LegalBookId,
								BalanceConverted,
								ActualBalanceConverter)
				select	1,
						ar.Id,
						ar.Code,
						ar.ThirdPartyId,
						ar.CostCenterId,
						ar.[Value],
						ar.Balance,
						ar.CurrencyId,
						er.CurrencyId,
						er.[Value], 
						er.ValueReverse,
						acr.[Value],
						acr.ValueReverse,
						ar.MainAccountId,
						ma.LegalBookId,
						[Portfolio].fnConvertValueBasedOnExchangeRate(ar.Balance, er.[Value] , er.ValueReverse) as ValueConvertedPortfolio, 
						[Portfolio].fnConvertValueBasedOnExchangeRate(ar.Balance, acr.[Value] , acr.ValueReverse) as ActualValueConvertedPortfolio
				from #AccountReceivableAtCut ar
				--inner join Portfolio.AccountReceivableAccounting ara on ara.AccountReceivableId = ar.Id
				inner join GeneralLedger.MainAccounts ma on ma.Id = ar.MainAccountId
				inner join Portfolio.AccountReceivableExchangeRate er on er.AccountReceivableId = ar.Id and er.CurrencyId = @CurrencyBookRevalueId
				inner join @actualCurrencyRate acr on 1 = 1
				where ar.CurrencyId = @currencyId 
					and ar.Balance > 0 and cast(ar.AccountReceivableDate as date) <= @LastDayMonth
					and ar.Status =2

				-- Inserto los anticipos 
				insert into  @currencyAdjustmentDetail (
								DocumentType,
								PortfolioAdvanceId,
								Code,
								ThirdPartyId,
								CostCenterId,
								[Value],
								Balance,
								CurrencyId, 
								CurrencyConverterId,
								ValueCurrency,
								ValueCurrencyReverse,
								ActualValueCurrency,
								ActualValueCurrencyReverse,
								MainAccountId,
								LegalBookId,
								BalanceConverted,
								ActualBalanceConverter)
				select	2,
						pa.Id,
						pa.Code,
						pa.ThirdPartyId,
						pa.CostCenterId,
						pa.[Value],
						pa.Balance, 
						pa.CurrencyId,
						par.CurrencyId, 
						par.[Value],
						par.ValueReverse,
						acr.[Value], 
						acr.ValueReverse,
						pa.MainAccountId,
						ma.LegalBookId,
						[Portfolio].fnConvertValueBasedOnExchangeRate(pa.Balance, par.[Value] , par.ValueReverse) as ValueConvertedPortfolio, 
						[Portfolio].fnConvertValueBasedOnExchangeRate(pa.Balance, acr.[Value] , acr.ValueReverse) as ActualValueConvertedPortfolio
				from #PortfolioAdvanceAtCut pa
				inner join GeneralLedger.MainAccounts ma on ma.Id = pa.MainAccountId
				inner join Portfolio.PortfolioAdvanceExchangeRate par on par.PortfolioAdvanceId = pa.Id and par.CurrencyId = @CurrencyBookRevalueId
				inner join @actualCurrencyRate acr on 1 = 1
				where pa.CurrencyId = @currencyId and pa.Balance > 0
						and cast(pa.DocumentDate as date) <= @LastDayMonth
						AND pa.Status =2

				--inserto en la tabla de detalle de revalorizacion
				INSERT INTO [Portfolio].[RevaluationDetail]
			   ([RevaluationId]
			   ,[DocumentType]
			   ,[PortfolioAdvanceId]
			   ,[AccountReceivableId]
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
			   ,ProfitLostValue)
			   select 
			   @revaluationId
			   ,DocumentType
			   ,[PortfolioAdvanceId]
			   ,[AccountReceivableId]
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
			   ,IIF(DocumentType = 1, [ActualBalanceConverter] - [BalanceConverted], [BalanceConverted] - [ActualBalanceConverter])
			   from @currencyAdjustmentDetail

				--Busco el libro contable que tenga la moneda que estoy valorizando
				declare @legalbookMovementId int 

				set @legalbookMovementId = (select Id from GeneralLedger.LegalBook where OfficialCurrencyId = @CurrencyBookRevalueId)

				/***********************************/
				declare @documentType tinyint
				DECLARE documentType_Cursor CURSOR FOR   
				SELECT DocumentType 
				FROM [Portfolio].[RevaluationDetail] cad
				where cad.[RevaluationId] = @revaluationId and CurrencyConverterId = @CurrencyBookRevalueId
				GROUP BY DocumentType

				OPEN documentType_Cursor  
				FETCH NEXT FROM documentType_Cursor   
				INTO @documentType

				WHILE @@FETCH_STATUS = 0  
				BEGIN 				

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
					@LastDayMonth,
					2, 
					CONCAT('Revalorización de cartera ', IIF(@documentType = 1,'Facturas','Anticipos')), 
					null,	
					null, 
					'JournalVouchers',
					@CurrencyBookRevalueId
				)

				if (@legalbookMovementId = @legalbookId) begin --entonces es por que estoy afectando el libro oficial y no hago homologacion de cuentas
					insert into @JournalVourcherDetailTmp 
					(IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
					select 
					cad.MainAccountId, 
					iif(ma.HandlesThirdParty = 1, cad.ThirdPartyId, null),
					iif(ma.HandlesCostCenter = 1, cad.CostCenterId, null),
					iif(ProfitLostValue > 0, abs(ProfitLostValue), 0),
					iif(ProfitLostValue > 0, 0, abs(ProfitLostValue)),
					concat('Documento No: ',ISNULL(ar.InvoiceNumber,pa.Code)), null, null, null, null
					from [Portfolio].[RevaluationDetail] cad
					inner join GeneralLedger.MainAccounts ma on ma.Id = cad.MainAccountId
					LEFT JOIN Portfolio.AccountReceivable ar on cad.AccountReceivableId = ar.Id
					LEFT JOIN Portfolio.PortfolioAdvance pa on cad.PortfolioAdvanceId = pa.Id
					where cad.[RevaluationId] = @revaluationId and CurrencyConverterId = @CurrencyBookRevalueId 
					AND cad.DocumentType =@documentType

					--Calculo la contra partida para balancear el comprobante
					set @totalAdjusted = (
						select sum(ProfitLostValue)
						from [Portfolio].[RevaluationDetail] cad
						inner join GeneralLedger.MainAccounts ma on ma.Id = cad.MainAccountId
						where cad.[RevaluationId] = @revaluationId and CurrencyConverterId = @CurrencyBookRevalueId
						AND cad.DocumentType =@documentType
					)

					insert into @JournalVourcherDetailTmp 
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

					SELECT TOP 1	ma.Id,
									NULL,
									IIF(ma.HandlesCostCenter=1,@CostCenterId,NULL),
									IIF(@totalAdjusted > 0, 0, abs(@totalAdjusted)),
									IIF(@totalAdjusted > 0, abs(@totalAdjusted), 0),
									'',
									null,
									null,
									null,
									null
					FROM GeneralLedger.MainAccounts ma WITH(NOLOCK)
					WHERE ma.Id =IIF(@totalAdjusted > 0, @ProfitLostByExchangeCurrencyAccountId, @LostByExchangeCurrencyAccountId)
				end
				else begin -- si los libros son diferentes entonces hago homologacion

					insert into @JournalVourcherDetailTmp 
					(IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
					select 
					cad.MainAccountId, 
					iif(ma.HandlesThirdParty = 1, cad.ThirdPartyId, null),
					iif(ma.HandlesCostCenter = 1, cad.CostCenterId, null),
					iif(ProfitLostValue > 0, abs(ProfitLostValue), 0),
					iif(ProfitLostValue > 0, 0, abs(ProfitLostValue)),
					concat('Documento No: ',ISNULL(ar.InvoiceNumber,pa.Code)), null, null, null, null
					from [Portfolio].[RevaluationDetail] cad
					inner join GeneralLedger.HomologationAccount ha on ha.OfficialMainAccountId = cad.MainAccountId
					inner join GeneralLedger.MainAccounts ma on ma.Id = ha.MainAccountId and ma.LegalBookId = @legalbookMovementId
					LEFT JOIN Portfolio.AccountReceivable ar on cad.AccountReceivableId = ar.Id
					LEFT JOIN Portfolio.PortfolioAdvance pa on cad.PortfolioAdvanceId = pa.Id
					where cad.[RevaluationId] = @revaluationId and CurrencyConverterId = @CurrencyBookRevalueId
					AND cad.DocumentType =@documentType

					--Calculo la contra partida para balancear el comprobante
					set @totalAdjusted = (
						select sum(ProfitLostValue)
						from [Portfolio].[RevaluationDetail] cad
						inner join GeneralLedger.MainAccounts ma on ma.Id = cad.MainAccountId
						where cad.[RevaluationId] = @revaluationId and CurrencyConverterId = @CurrencyBookRevalueId
						AND cad.DocumentType =@documentType
					)

					insert into @JournalVourcherDetailTmp 
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
					SELECT TOP 1	ma.Id,
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
					inner join GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ha.MainAccountId and ma.LegalBookId = @legalbookMovementId
					where ha.OfficialMainAccountId = IIF(@totalAdjusted > 0, @ProfitLostByExchangeCurrencyAccountId, @LostByExchangeCurrencyAccountId)
				end

				--Eliminamos cuentas en 0
				delete from @JournalVourcherDetailTmp where DebitValue = 0 and CreditValue = 0

				--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
				SELECT @JournalVoucherXML = CONVERT(xml, 
					(
						SELECT * FROM @JournalVourcherTmp JournalVoucher 
						JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
						For xml AUTO,TYPE, ELEMENTS
					)
				)

				if (@status = 1) begin
					--Se consume el sp que guarda el comprobante contable
					Declare @CodeMessage Int,
						@MessageVoucher Varchar(Max),
						@IdJournalVoucherResult Int

					--Se consume el sp que guarda el movimiento contable
					insert @resultsTable exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,'999' 			
				end

				NEXT_ROW:
				delete from @JournalVourcherTmp
				delete from @JournalVourcherDetailTmp

				FETCH NEXT FROM documentType_Cursor   
				INTO @documentType

				END
				CLOSE documentType_Cursor;  
				DEALLOCATE documentType_Cursor;

				if (@status = 1) begin
					update er set er.[Value] = acr.[Value] , er.ValueReverse = acr.ValueReverse
					from #AccountReceivableAtCut ar
					--inner join Portfolio.AccountReceivableAccounting ara on ara.AccountReceivableId = ar.Id
					inner join GeneralLedger.MainAccounts ma on ma.Id = ar.MainAccountId
					inner join Portfolio.AccountReceivableExchangeRate er on er.AccountReceivableId = ar.Id and er.CurrencyId = @CurrencyBookRevalueId
					inner join @actualCurrencyRate acr on 1 = 1
					where ar.CurrencyId = @currencyId and ar.Balance > 0
							AND CAST(ar.AccountReceivableDate as DATE) <= @LastDayMonth
							and ar.[Status]=2

					update er set er.[Value] = acr.[Value] , er.ValueReverse = acr.ValueReverse
					from Portfolio.PortfolioAdvanceExchangeRate er
					inner join #PortfolioAdvanceAtCut pa on pa.Id = er.PortfolioAdvanceId
					inner join @actualCurrencyRate acr on 1 = 1
					where pa.CurrencyId = @currencyId and pa.Balance > 0 
						AND CAST(pa.DocumentDate as DATE) <= @LastDayMonth
						AND pa.Status=2

					update Portfolio.Revaluation set Status = 2 where Id = @revaluationId
				END

				delete from @currencyAdjustmentDetail

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

		if (@status = 1) begin			
			select  * from @resultsTable
		end
		else begin
			select 0 MessageCode, 'Se calculo correctamente la valorizacion' MessageVoucher, 0 as JournalVoucherId
		end
	end try
	begin catch

		IF CURSOR_STATUS('global','currency_cursor') >= -1  BEGIN
		  IF CURSOR_STATUS('global','currency_cursor') > -1 BEGIN
			CLOSE currency_cursor
		  END
		 DEALLOCATE currency_cursor
		END

		IF CURSOR_STATUS('global','documentType_Cursor') >= -1  BEGIN
		  IF CURSOR_STATUS('global','documentType_Cursor') > -1 BEGIN
			CLOSE documentType_Cursor
		  END
		 DEALLOCATE documentType_Cursor
		END

		--Tabla RESULTADO
		IF OBJECT_ID('tempdb..#AccountReceivableAtCut') IS NOT NULL DROP TABLE #AccountReceivableAtCut
		IF OBJECT_ID('tempdb..#PortfolioAdvanceAtCut') IS NOT NULL DROP TABLE #PortfolioAdvanceAtCut

		select 999 MessageCode, ERROR_MESSAGE() as MessageVoucher, 0 as JournalVoucherId
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta el proceso de revalorización (ajuste por diferencia en cambio) de la cartera en moneda extranjera para un período contable específico (mes y año). Calcula el saldo de las cuentas por cobrar y anticipos al corte del último día del mes, los reexpresa a la tasa de cambio vigente y genera el comprobante contable de ajuste con las ganancias o pérdidas por diferencial cambiario. Crea o actualiza el encabezado de la revaluación en Portfolio.Revaluation y su detalle en Portfolio.RevaluationDetail, consultando la configuración contable de la empresa (moneda oficial, cuentas de pérdida/ganancia y tipo de comprobante) desde GeneralLedger.CompanySettings, e impide confirmar el ajuste si el período ya fue cerrado o si la fecha actual es anterior al último día del mes.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SPCurrencyRevaluation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SPCurrencyRevaluation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Revalorización de cartera; Cuentas por cobrar (AccountReceivable); Anticipos de cartera (PortfolioAdvance); Tasa de cambio TRM; Diferencia en cambio (utilidad/pérdida); Libro contable oficial y auxiliares; Homologación de cuentas contables; Comprobante contable (JournalVoucher); Centro de costo; Cierre contable mensual; Tercero; Plan de cuentas (MainAccounts)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SPCurrencyRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @status = 1 AND fecha actual (Common.GETDATE) < último día del mes/año del cierre → Lanza THROW 50000 ''No se puede confirmar el ajuste si la fecha actual es menor al último día del mes del cierre''; si fecha actual > último día del mes del cierre → Se fija @DateTRM = último día del mes (se usa la TRM del cierre, no la del día actual); si Existe ya una Portfolio.Revaluation con Month/Year y Status = 2 (confirmada) → Lanza THROW 50000 ''Ya existe una revalorización confirmada para este periodo''; si No existe Portfolio.Revaluation para Month/Year → INSERT cabecera con Status = 1 (calculada/borrador) else Reutiliza la revaluación existente y borra su detalle; si Solo existe un libro contable activo con la misma OfficialCurrencyId que la analizada (no hay libros con moneda distinta) → Salta a etiqueta ''cont'' sin revalorizar esa moneda; si No se encuentra TRM para la fecha del cierre entre @currencyId y @CurrencyBookRevalueId → Cierra cursores y retorna fila con MessageCode 999 indicando moneda sin TRM en la fecha del cierre; si Existen anticipos/cuentas por cobrar sin TRM registrada (PortfolioAdvanceExchangeRate/AccountReceivableExchangeRate) y tampoco hay TRM en Common.TRM para sus fechas → Cierra cursores y retorna MessageCode 999 listando las fechas sin TRM; si Existen anticipos/CxC sin tasa de cambio registrada pero SÍ hay TRM en Common.TRM para su fecha → INSERT en PortfolioAdvanceExchangeRate / AccountReceivableExchangeRate con la TRM correspondiente; si DocumentType = 1 (Cuentas por cobrar) → ProfitLostValue = ActualBalanceConverter - BalanceConverted else DocumentType = 2 (Anticipos): ProfitLostValue = BalanceConverted - ActualBalanceConverter; si @legalbookMovementId = @legalbookId (libro de la moneda a revalorizar es el oficial) → Genera el comprobante usando MainAccountId directamente, sin homologación else Genera el comprobante usando GeneralLedger.HomologationAccount para mapear las cuentas al libro destino; si @totalAdjusted > 0 → La contrapartida usa @ProfitLostByExchangeCurrencyAccountId (utilidad por diferencia en cambio) en CRÉDITO else Usa @LostByExchangeCurrencyAccountId (pérdida por diferencia en cambio) en DÉBITO; si ProfitLostValue > 0 en cada línea de detalle → Va al DebitValue else Va al CreditValue; si @status = 1 (confirmar) → Llama GeneralLedger.SP_CreateAndValidateJournalVoucherMovement, actualiza tasas en AccountReceivableExchangeRate y PortfolioAdvanceExchangeRate con la TRM actual y marca Portfolio.Revaluation.Status = 2 else Solo retorna mensaje informativo ''Se calculo correctamente la valorización'' sin generar comprobante ni confirmar', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SPCurrencyRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Portfolio.GetAccountReceivableByAge; Portfolio.GetPortfolioAdvanceByAge; Portfolio.fnConvertValueBasedOnExchangeRate', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SPCurrencyRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; GeneralLedger.CompanySettings; Portfolio.Revaluation; Portfolio.AccountReceivable; Portfolio.PortfolioAdvance; Portfolio.AccountReceivableExchangeRate; Portfolio.PortfolioAdvanceExchangeRate; Common.TRM; Common.Currency; GeneralLedger.MainAccounts; GeneralLedger.HomologationAccount; Portfolio.RevaluationDetail', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SPCurrencyRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SPCurrencyRevaluation';
-- GO
