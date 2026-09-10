-- =============================================
-- Author:      Giovanny Plazas
-- Create Date: 14-02-2023
-- Description: SP para revalorizar la tesoreria 
-- =============================================
CREATE PROCEDURE [Treasury].[SPCurrencyRevaluation]
(
    @month int,
	@year int,
	@status tinyint,
	@userCode varchar(50)
)
AS
BEGIN
   
	declare @resultsTable table(MessageCode int, MessageVoucher varchar(max), JournalVoucherId int)
	begin try
	
		declare @currencyId int,
				@OfficialCurrencyId int,
				@ProfitLostByExchangeCurrencyAccountId int,
				@LostByExchangeCurrencyAccountId int,
				@ProfitLostJournalVoucherTypeId int,
				@totalAdjusted numeric(18,2),
				@_revaluationControlId int,
				@CutOffDate date,
				@ThirdPartyCompanyId int ,
				@CostCenterId int

		declare @actualCurrencyRate table (	CurrencyFrom int,
											CurrencyTo int,
											[Value] numeric(20, 5),
											ValueReverse numeric(20, 5))

		declare @legalbookId int 
		set @legalbookId = (select Id from GeneralLedger.LegalBook where OfficialBook = 1)

		--Variable para obtener el xml
		DECLARE @JournalVoucherXML as XML

		declare @currencyAdjustmentDetail table (
													TreasuryRevaluationId	INT,
													DocumentType			TINYINT NOT NULL,
													Nature					TINYINT NOT NULL,
													DocumentNumber			varchar(20) NOT NULL,
													DocumentDate			DATETIME NOT NULL,
													ValueMovement			NUMERIC(18,2) NOT NULL,
													CurrencyId				int NOT NULL,
													CurrencyConverterId		int NOT NULL,
													ValueCurrency			numeric(20, 5) NOT NULL,
													ValueCurrencyReverse	numeric(20, 5) NOT NULL,
													ActualValueCurrency		numeric(20, 5) NOT NULL,
													ActualValueCurrencyReverse		numeric(20, 5) NOT NULL,
													ValueMovementConverted			NUMERIC(18,2) NOT NULL,
													ActualValueMovementConverted	NUMERIC(18,2) NOT NULL,
													TreasuryBalanceId				INT,
													CashRegisterId					INT,
													EntityBankAccountId				INT,
													MainAccountId					INT NOT NULL,
													ProfitLostValue numeric(20, 5) NULL)
													
		select top 1 
		@OfficialCurrencyId = OfficialCurrencyId, 
		@ProfitLostByExchangeCurrencyAccountId = ProfitLostByExchangeCurrencyAccountId,
		@ProfitLostJournalVoucherTypeId = ProfitLostJournalVoucherTypeId,
		@LostByExchangeCurrencyAccountId = LostByExchangeCurrencyAccountId,
		@CostCenterId = CostCenterId
		from GeneralLedger.CompanySettings	

		if (select count(*) from [Treasury].[TreasuryRevaluationControl] where [Month] = @month and [Year] = @year and [Status] = 2) > 0 begin 
			THROW 50000, N'Ya existe una revalorizacion confirmada para este periodo', 1;
		end

		Set @CutOffDate = CAST( DATEADD(DAY,-1, DATEADD(MONTH,1,  DATEFROMPARTS(@year,@month,1))) as date)

		DECLARE @_dateValidation as date = dateadd(month,-1,@CutOffDate)
		-- Requiere que el período anterior esté confirmado (Status=2) siempre que alguna
		-- caja/banco ya haya sido revalorizada antes (PeriodLastRevaluation > 0).
		-- Permite la primera revalorización histórica pero impide saltar o reordenar meses.
		if (	EXISTS(SELECT 1 FROM Treasury.CashRegisters      WHERE PeriodLastRevaluation > 0)
			OR	EXISTS(SELECT 1 FROM Treasury.EntityBankAccounts WHERE PeriodLastRevaluation > 0))
		   AND NOT EXISTS(
				SELECT 1 FROM [Treasury].[TreasuryRevaluationControl]
				WHERE [Status] = 2
				  AND [Month] = MONTH(@_dateValidation)
				  AND [Year]  = YEAR(@_dateValidation)) begin
			THROW 50000, N'No se puede Revalorizar, porque el periodo anterior no se ha confirmado', 1;
		end

		Set @ThirdPartyCompanyId =(select top 1 IdDian from GeneralLedger.GeneralLedgerSettings)

		if NOT EXISTS(select 1 from [Treasury].[TreasuryRevaluationControl] where [Month] = @month and [Year] = @year) begin
			--inserto la cabecera de la transaccion del la revalorizacion
			INSERT INTO [Treasury].[TreasuryRevaluationControl]
				   ([Month]
				   ,[Year]
				   ,[Status]
				   ,[CreationUser]
				   ,[CreationDate])
			values (@month, @year, 1, @userCode, Common.GETDATE())
		end

		set @_revaluationControlId = (select Id from [Treasury].[TreasuryRevaluationControl] where [Month] = @month and [Year] = @year)

		delete from [Treasury].[TreasuryRevaluationDetail] 
		where TreasuryRevaluationId in (select tr.Id 
										from [Treasury].[TreasuryRevaluation] tr with(NOLOCK)
										WHERE tr.TreasuryRevaluationControlId = @_revaluationControlId)
		DELETE from [Treasury].[TreasuryRevaluation] where TreasuryRevaluationControlId =@_revaluationControlId

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
				if (select count(*) from Common.TRM where MeasurementDate = @CutOffDate and CurrencyId = @currencyId and OfficialCurrencyId = @CurrencyBookRevalueId) > 0 begin
					insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
					select @currencyId, @CurrencyBookRevalueId, [Value], ValueOfficialToCurrency 
					from Common.TRM where MeasurementDate = @CutOffDate and CurrencyId = @currencyId and OfficialCurrencyId = @CurrencyBookRevalueId
				end
				else if (select count(*) from Common.TRM where MeasurementDate = @CutOffDate and OfficialCurrencyId = @currencyId and CurrencyId = @CurrencyBookRevalueId) > 0 begin
					insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
					select @currencyId, @CurrencyBookRevalueId, ValueOfficialToCurrency, [Value] 
					from Common.TRM where MeasurementDate = @CutOffDate and OfficialCurrencyId = @currencyId and CurrencyId = @CurrencyBookRevalueId
				end

				if (select count(*) from @actualCurrencyRate) = 0 begin
					CLOSE currencyRevalue_cursor;  
						DEALLOCATE currencyRevalue_cursor;
						CLOSE currency_cursor;  
						DEALLOCATE currency_cursor;  
						SELECT 999 AS MessageCode, 'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @CurrencyBookRevalueId)  +') para la fecha del Comprobante Contable ' MessageVoucher, 0 as JournalVoucherId
						return
				end
			
				declare @message varchar(max)
				--si existen cajas sin crearse en la tabla de exchange Rate
				if EXISTS(	SELECT 1
							from Treasury.CashRegisters cr WITH(NOLOCK)
							LEFT JOIN Treasury.CashRegisterExchangeRate crer WITH(NOLOCK) ON cr.Id= crer.CashRegisterId and crer.CurrencyId = @CurrencyBookRevalueId
							where cr.CurrencyId = @currencyId and crer.Id is null and cr.InitialDate <= @CutOffDate) begin 

					--Valido que haya TRM
					set @message = (select STRING_AGG(dateTRM, ', ') 
									from (
									select distinct  cast(cr.InitialDate as date) as dateTRM
									from Treasury.CashRegisters cr WITH(NOLOCK)
									left join Treasury.CashRegisterExchangeRate crer WITH(NOLOCK) on crer.CashRegisterId = cr.Id and crer.CurrencyId = @CurrencyBookRevalueId
									left join Common.TRM t on t.CurrencyId = iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) and t.MeasurementDate = cast(cr.InitialDate as date)
									where cr.CurrencyId = @currencyId  and crer.id is null and t.Id is null and cast(cr.InitialDate as DATE) <= @CutOffDate
									) as dat)

					if (@message is not null and len(@message) > 0) begin
						CLOSE currencyRevalue_cursor;  
						DEALLOCATE currencyRevalue_cursor;
						CLOSE currency_cursor;  
						DEALLOCATE currency_cursor;  
						select 999 AS MessageCode, 'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @CurrencyBookRevalueId)  +') para las fechas ' + @message MessageVoucher, 0 as JournalVoucherId
						return
					END

					-- Inserto el TRM , valor inicial de la caja
					INSERT INTO Treasury.CashRegisterExchangeRate
					   ([CashRegisterId]
					   ,[CurrencyId]
					   ,[Value]
					   ,[ValueReverse])
					select cr.Id, iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @OfficialCurrencyId, t.CurrencyId), iif(@CurrencyBookRevalueId = @OfficialCurrencyId, t.[Value], t.ValueOfficialToCurrency), iif(@CurrencyBookRevalueId = @OfficialCurrencyId, t.ValueOfficialToCurrency, t.[Value])
					from Treasury.CashRegisters cr WITH(NOLOCK)
					LEFT JOIN Treasury.CashRegisterExchangeRate crer WITH(NOLOCK) on crer.CashRegisterId = cr.Id and crer.CurrencyId = @CurrencyBookRevalueId
					left join Common.TRM t on t.CurrencyId = iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) and t.MeasurementDate = cast(cr.InitialDate as date)
					where cr.CurrencyId = @currencyId and crer.id is null and cast(cr.InitialDate as DATE) <= @CutOffDate

				END
				-------

				--si existen bancos sin crearse en la tabla de exchange Rate
				if EXISTS(	SELECT 1
							from Treasury.EntityBankAccounts eba WITH(NOLOCK)
							LEFT JOIN Treasury.EntityBankAccountExchangeRate ebaer WITH(NOLOCK) ON eba.Id= ebaer.EntityBankAccountId and ebaer.CurrencyId = @CurrencyBookRevalueId
							where eba.CurrencyId = @currencyId and ebaer.Id is null and eba.InitialDate <= @CutOffDate) begin 

					--Valido que haya TRM
					set @message = (select STRING_AGG(dateTRM, ', ') 
									from (
									select distinct  cast(eba.InitialDate as date) as dateTRM
									from Treasury.EntityBankAccounts eba WITH(NOLOCK)
									left join Treasury.EntityBankAccountExchangeRate ebaer WITH(NOLOCK) on ebaer.EntityBankAccountId = eba.Id and ebaer.CurrencyId = @CurrencyBookRevalueId
									left join Common.TRM t on t.CurrencyId = iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) and t.MeasurementDate = cast(eba.InitialDate as date)
									where eba.CurrencyId = @currencyId and ebaer.id is null and t.Id is null and eba.InitialDate <= @CutOffDate
									) as dat)

					if (@message is not null and len(@message) > 0) begin
						CLOSE currencyRevalue_cursor;  
						DEALLOCATE currencyRevalue_cursor;
						CLOSE currency_cursor;  
						DEALLOCATE currency_cursor;  
						select 999 AS MessageCode, 'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @CurrencyBookRevalueId)  +') para las fechas ' + @message MessageVoucher, 0 as JournalVoucherId
						return
					END

					-- Inserto el TRM 
					INSERT INTO Treasury.EntityBankAccountExchangeRate
					   ([EntityBankAccountId]
					   ,[CurrencyId]
					   ,[Value]
					   ,[ValueReverse])
					select	eba.Id,
							iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @OfficialCurrencyId, t.CurrencyId),
							iif(@CurrencyBookRevalueId = @OfficialCurrencyId, t.[Value], t.ValueOfficialToCurrency),
							iif(@CurrencyBookRevalueId = @OfficialCurrencyId, t.ValueOfficialToCurrency, t.[Value])
					from Treasury.EntityBankAccounts eba WITH(NOLOCK)
					LEFT JOIN Treasury.EntityBankAccountExchangeRate ebaer WITH(NOLOCK) on ebaer.EntityBankAccountId = eba.Id and ebaer.CurrencyId = @CurrencyBookRevalueId
					left join Common.TRM t on t.CurrencyId = iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) and t.MeasurementDate = cast(eba.InitialDate as date)
					where eba.CurrencyId = @currencyId and ebaer.id is null and eba.InitialDate <= @CutOffDate

				END
				-------

				-- valido que treasury balance este en la tabla exchange rate
				if EXISTS(	SELECT 1
							from Treasury.TreasuryBalance tb WITH(NOLOCK)
							LEFT JOIN Treasury.CashRegisters cr WITH(NOLOCK) on tb.CashRegisterId=cr.Id
							LEFT JOIN Treasury.EntityBankAccounts eba WITH(NOLOCK) on tb.EntityBankAccountId=eba.Id							
							LEFT JOIN Treasury.TreasuryBalanceExchangeRate tber WITH(NOLOCK) ON tb.Id= tber.TreasuryBalanceId and tber.CurrencyId = @CurrencyBookRevalueId
							where	iif(cr.id is null,isnull(eba.CurrencyId,@OfficialCurrencyId),ISNULL(cr.CurrencyId,@OfficialCurrencyId)) = @currencyId 
									and tber.Id is null AND (DATEPART(MONTH,tb.DocumentDate) = @month AND DATEPART(YEAR,tb.DocumentDate) = @year)) begin 

						--Valido que haya TRM
						set @message = (select STRING_AGG(dateTRM, ', ') 
										from (
										select distinct  cast(tb.DocumentDate as date) as dateTRM
										from Treasury.TreasuryBalance tb WITH(NOLOCK)
										LEFT JOIN Treasury.CashRegisters cr WITH(NOLOCK) on tb.CashRegisterId=cr.Id
										LEFT JOIN Treasury.EntityBankAccounts eba WITH(NOLOCK) on tb.EntityBankAccountId=eba.Id
										left join Treasury.TreasuryBalanceExchangeRate tber WITH(NOLOCK) on tber.TreasuryBalanceId = tb.Id and tber.CurrencyId = @CurrencyBookRevalueId
										left join Common.TRM t on t.CurrencyId = iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) and t.MeasurementDate = cast(tb.DocumentDate as date)
										where	iif(cr.id is null,isnull(eba.CurrencyId,@OfficialCurrencyId),ISNULL(cr.CurrencyId,@OfficialCurrencyId)) = @currencyId and tb.ValueMovement > 0 
												and tber.id is null and t.Id is null AND (DATEPART(MONTH,tb.DocumentDate) = @month AND DATEPART(YEAR,tb.DocumentDate) = @year)
										) as dat)

						if (@message is not null and len(@message) > 0) begin
							CLOSE currencyRevalue_cursor;  
							DEALLOCATE currencyRevalue_cursor;
							CLOSE currency_cursor;  
							DEALLOCATE currency_cursor;  
							select 999 AS MessageCode, 'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @CurrencyBookRevalueId)  +') para las fechas ' + @message MessageVoucher, 0 as JournalVoucherId
							return
						END

						-- Inserto el TRM  para registrar el TRM del valor del documento
						INSERT INTO Treasury.TreasuryBalanceExchangeRate
								   ([TreasuryBalanceId]
								   ,[CurrencyId]
								   ,[Value]
								   ,[ValueReverse])
							select	tb.Id,
									iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @OfficialCurrencyId,ISNULL(ct.CurrencyId,t.CurrencyId)),
									iif(@CurrencyBookRevalueId = @OfficialCurrencyId,ISNULL(ct.[Value],t.[Value]),ISNULL(ct.ValueOfficialToCurrency,t.ValueOfficialToCurrency)),
									iif(@CurrencyBookRevalueId = @OfficialCurrencyId, ISNULL(ct.ValueOfficialToCurrency,t.ValueOfficialToCurrency),ISNULL(ct.[value], t.[Value]))
						from Treasury.TreasuryBalance tb WITH(NOLOCK)
						LEFT JOIN Treasury.CashRegisters cr WITH(NOLOCK) on tb.CashRegisterId=cr.Id
						LEFT JOIN Treasury.EntityBankAccounts eba WITH(NOLOCK) on tb.EntityBankAccountId=eba.Id
						LEFT JOIN Treasury.TreasuryBalanceExchangeRate tber WITH(NOLOCK) on tber.TreasuryBalanceId = tb.Id and tber.CurrencyId = @CurrencyBookRevalueId
						LEFT JOIN Common.TRM t with(NOLOCK) on t.CurrencyId = iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId) and t.MeasurementDate = cast(tb.DocumentDate as date)
						LEFT JOIN Treasury.CashReceipts crs WITH(NOLOCK) ON crs.Code = tb.DocumentNumber and tb.DocumentType=1 and (crs.EntityName ='Invoice' or SUBSTRING(crs.EntityName,0,10) = 'Automatic')
						LEFT JOIN Billing.CustomTRM ct with(NOLOCK) on ct.CurrencyId = iif(@CurrencyBookRevalueId = @OfficialCurrencyId, @currencyId, @CurrencyBookRevalueId)
																		AND cast(tb.DocumentDate as date) BETWEEN ct.InitialMeasurementDate and ct.FinalMeasurementDate
																		AND tb.DocumentType =1  and crs.Id IS NOT NULL
						where	iif(cr.id is null,isnull(eba.CurrencyId,@OfficialCurrencyId),ISNULL(cr.CurrencyId,@OfficialCurrencyId)) = @currencyId 
								and tb.ValueMovement > 0 and tber.id is null AND (DATEPART(MONTH,tb.DocumentDate) = @month AND DATEPART(YEAR,tb.DocumentDate) = @year)
				END;
				--------
				WITH 
					tmp_Currency as (	SELECT u.Id cashOrBankId,c.Id CurrencyId,u.CashOrBank,u.Balance,u.CashOrBankCode,u.InitialDate,u.IdMainAccount
										from Common.Currency c
										join (	SELECT	cr.id,
														isnull(cr.CurrencyId,@OfficialCurrencyId) CurrencyId,
														IIF(cr.PeriodLastRevaluation =0,cr.InitialBalance,cr.BalanceLastRevaluation) Balance,
														 1 CashOrBank,
														 cr.Code CashOrBankCode,
														 iif(cr.PeriodLastRevaluation=0,cr.InitialDate,CAST( DATEADD(MONTH,1,  CONCAT(cr.PeriodLastRevaluation,'01')) as date)) InitialDate,
														 cr.IdMainAccount
												from Treasury.CashRegisters cr												

												UNION ALL

												SELECT	eba.id,
														isnull(eba.CurrencyId,@OfficialCurrencyId) CurrencyId,
														IIF(eba.PeriodLastRevaluation =0,eba.InitialBalance,eba.BalanceLastRevaluation) Balance,
														2 CashOrBank,
														eba.Code CashOrBankCode,
														IIF(eba.PeriodLastRevaluation =0,eba.InitialDate,CAST(DATEADD(MONTH,1,  CONCAT(eba.PeriodLastRevaluation,'01')) as date)) InitialDate,
														eba.IdMainAccount
												from Treasury.EntityBankAccounts eba
												) u on c.Id=u.CurrencyId)
			---hablar con cris
			/**************--Se Inserta en la tabla Variable para su posteriror uso--**************************************/
				INSERT INTO @currencyAdjustmentDetail (TreasuryRevaluationId,
														DocumentType,
														Nature,
														DocumentNumber,
														DocumentDate,
														ValueMovement,
														CurrencyId,
														CurrencyConverterId,
														ValueCurrency,
														ValueCurrencyReverse,
														ActualValueCurrency	,
														ActualValueCurrencyReverse,
														ValueMovementConverted,
														ActualValueMovementConverted,
														TreasuryBalanceId,
														CashRegisterId,
														EntityBankAccountId,
														MainAccountId)			
						SELECT	NULL,
								trb.DocumentType,
								trb.Nature,
								trb.DocumentNumber,
								trb.DocumentDate,
								trb.ValueMovement,
								ISNULL(tmp.CurrencyId,tmp2.CurrencyId) CurrencyId,
								tber.CurrencyId CurrencyConvertedId,
								tber.[Value],
								tber.ValueReverse,
								acr.[Value],
								acr.ValueReverse,
								[Portfolio].fnConvertValueBasedOnExchangeRate(trb.ValueMovement, tber.[Value] , tber.ValueReverse) ValueMovementConverter,
								[Portfolio].fnConvertValueBasedOnExchangeRate(trb.ValueMovement, acr.[Value] , acr.ValueReverse) ActualValueMovementConverter,
								trb.Id,
								trb.CashRegisterId,
								trb.EntityBankAccountId,
								ISNULL(tmp.IdMainAccount,tmp2.IdMainAccount) IdMainAccount
						FROM Treasury.TreasuryBalance trb WITH(NOLOCK)
						inner join Treasury.TreasuryBalanceExchangeRate tber WITH(NOLOCK) ON trb.Id=tber.TreasuryBalanceId
						left join tmp_Currency tmp on tmp.CashOrBank=1 and tmp.cashOrBankId = trb.CashRegisterId
						left join tmp_Currency tmp2 on tmp2.CashOrBank=2 and tmp2.cashOrBankId= trb.EntityBankAccountId
						inner join @actualCurrencyRate acr on 1 = 1
						WHERE ISNULL(tmp.CurrencyId,tmp2.CurrencyId) = @currencyId AND (DATEPART(MONTH,trb.DocumentDate) = @month AND DATEPART(YEAR,trb.DocumentDate) = @year)

						union ALL

						SELECT	NULL,
								0 DocumentType,
								1 Nature,
								tmpCash.CashOrBankCode DocumentNumber,
								tmpCash.InitialDate,
								sum(tmpCash.Balance) ValueMovement,
								tmpCash.CurrencyId,
								crer.CurrencyId,
								crer.[Value],
								crer.ValueReverse,
								acr.[Value],
								acr.[ValueReverse],
								sum([Portfolio].fnConvertValueBasedOnExchangeRate(tmpCash.Balance, crer.[Value] , crer.ValueReverse)) ValueMovementConverter,
								sum([Portfolio].fnConvertValueBasedOnExchangeRate(tmpCash.Balance, acr.[Value] , acr.ValueReverse)) ActualValueMovementConverter,
								NULL TreasuryBalanceId,
								tmpCash.cashOrBankId,
								NULL EntityBankAccountId,
								tmpCash.IdMainAccount
						from tmp_Currency tmpCash
						INNER JOIN Treasury.CashRegisterExchangeRate crer with(NOLOCK) on tmpCash.cashOrBankId= crer.CashRegisterId
						inner join @actualCurrencyRate acr on 1 = 1
						where tmpCash.CashOrBank=1 and tmpCash.CurrencyId=@currencyId and cast(tmpCash.InitialDate as date) <= @CutOffDate
						group by	tmpCash.cashOrBankId,tmpCash.CashOrBank,tmpCash.CurrencyId,
									tmpCash.CashOrBankCode,crer.CurrencyId,	crer.[Value],
									crer.ValueReverse,acr.[Value],acr.[ValueReverse],tmpCash.[InitialDate],
									tmpCash.IdMainAccount
						UNION ALL

						SELECT	NULL,
								0 DocumentType,
								1 Nature,
								tmpBank.CashOrBankCode DocumentNumber,
								tmpBank.InitialDate,
								sum(tmpBank.Balance) ValueMovement,
								tmpBank.CurrencyId,
								ebaer.CurrencyId,
								ebaer.[Value],
								ebaer.ValueReverse,
								acr.[Value],
								acr.[ValueReverse],
								sum([Portfolio].fnConvertValueBasedOnExchangeRate(tmpBank.Balance, ebaer.[Value] , ebaer.ValueReverse)) ValueMovementConverter,
								sum([Portfolio].fnConvertValueBasedOnExchangeRate(tmpBank.Balance, acr.[Value] , acr.ValueReverse)) ActualValueMovementConverter,
								NULL TreasuryBalanceId,
								NULL CashRegisterId,
								tmpBank.cashOrBankId,
								tmpBank.IdMainAccount
						from tmp_Currency tmpBank
						INNER JOIN Treasury.EntityBankAccountExchangeRate ebaer WITH(NOLOCK) on tmpBank.cashOrBankId=ebaer.EntityBankAccountId
						inner join @actualCurrencyRate acr on 1 = 1
						where tmpBank.CashOrBank=2 and tmpBank.CurrencyId=@currencyId and cast(tmpBank.InitialDate as DATE) <= @CutOffDate
						group by	tmpBank.cashOrBankId, tmpBank.CashOrBank,tmpBank.CurrencyId,
									tmpBank.CashOrBankCode,tmpBank.InitialDate,ebaer.CurrencyId,
									ebaer.[Value],ebaer.ValueReverse,acr.[Value],acr.[ValueReverse],
									tmpBank.IdMainAccount

				/*******************************************************************************************************************/
						--Inserto cuando no existan registro de las cajas o bancos en TreasuryRevaluation
						INSERT INTO [Treasury].[TreasuryRevaluation]
						SELECT	@_revaluationControlId,
								a.CashRegisterId,
								a.EntityBankAccountId,
								sum(a.Balance),
								sum(a.NewBalance)
						from(
								SELECT	cad.CashRegisterId,
										cad.EntityBankAccountId,
										iif(cad.DocumentType=0,cad.ValueMovement,0) Balance,
										iif(cad.Nature=1,1,-1)*cad.ValueMovement NewBalance
								from @currencyAdjustmentDetail cad
								where cad.CurrencyId =@currencyId
								) a
						GROUP by a.CashRegisterId,a.EntityBankAccountId																			
				--------------------------------------------------------------------------------------------------------------------------	
				update ad
				set ProfitLostValue = IIF(ad.Nature = 1,
										ad.ActualValueMovementConverted - ad.ValueMovementConverted,
										ad.ValueMovementConverted - ad.ActualValueMovementConverted)
				from @currencyAdjustmentDetail ad
						   JOIN [Treasury].[TreasuryRevaluation] tr WITH(NOLOCK)
						   on		(ad.CashRegisterId =tr.CashRegisterId or ad.CashRegisterId is null) 
								and (ad.EntityBankAccountId=tr.BankAccountId or ad.EntityBankAccountId is null) 
								and tr.TreasuryRevaluationControlId=@_revaluationControlId

				INSERT INTO [Treasury].[TreasuryRevaluationDetail]
								   (TreasuryRevaluationId,
									DocumentType,
									Nature,
									DocumentNumber,
									DocumentDate,
									ValueMovement,
									CurrencyId,
									CurrencyConvertedId,
									ValueCurrency,
									ValueCurrencyReverse,
									ActualValueCurrency,
									ActualValueCurrencyReverse,
									ValueMovementConverted,
									ActualValueMovementConverted,
									ProfitLostValue,
									TreasuryBalanceId
									)
						   select	tr.Id,
									ad.DocumentType,
									ad.Nature,
									ad.DocumentNumber,
									ad.DocumentDate,
									ad.ValueMovement,
									ad.CurrencyId,
									ad.CurrencyConverterId,
									ad.ValueCurrency,
									ad.ValueCurrencyReverse,
									ad.ActualValueCurrency,
									ad.ActualValueCurrencyReverse,
									ad.ValueMovementConverted,
									ad.ActualValueMovementConverted,
									ad.ProfitLostValue,
									ad.TreasuryBalanceId
						   from @currencyAdjustmentDetail ad
						   JOIN [Treasury].[TreasuryRevaluation] tr WITH(NOLOCK)
						   on		(ad.CashRegisterId =tr.CashRegisterId or ad.CashRegisterId is null) 
								and (ad.EntityBankAccountId=tr.BankAccountId or ad.EntityBankAccountId is null) 
								and tr.TreasuryRevaluationControlId=@_revaluationControlId

				--Busco el libro contable que tenga la moneda que estoy valorizando
				declare @legalbookMovementId int 

				set @legalbookMovementId = (select Id from GeneralLedger.LegalBook where OfficialCurrencyId = @CurrencyBookRevalueId)

				
				/***********************************/
				declare @documentType tinyint
				DECLARE documentType_Cursor CURSOR FOR   
				SELECT DocumentType 
				FROM @currencyAdjustmentDetail
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
					@CutOffDate, 
					2, 
					CONCAT('Revalorización de Tesoreria ',CASE @documentType
															when 1 then 'Recibo de Caja'
															when 2 then 'Comprobante de Egreso'
															when 3 then 'Consignaciones'
															when 4 then 'Notas'
															ELSE '' end	), 
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
					iif(ma.HandlesThirdParty = 1, @ThirdPartyCompanyId, null),
					NULL,--iif(ma.HandlesCostCenter = 1, cad.CostCenterId, null),
					iif(ProfitLostValue > 0, abs(ProfitLostValue), 0),
					iif(ProfitLostValue > 0, 0, abs(ProfitLostValue)),
					CONCAT('Documento no : ',cad.DocumentNumber), null, null, null, null
					from @currencyAdjustmentDetail cad
					inner join GeneralLedger.MainAccounts ma on ma.Id = cad.MainAccountId
					where CurrencyConverterId = @CurrencyBookRevalueId AND cad.DocumentType =@documentType

					--Calculo la contra partida para balancear el comprobante
					set @totalAdjusted = (
						select sum(ProfitLostValue)
						from @currencyAdjustmentDetail cad
						where CurrencyConverterId = @CurrencyBookRevalueId AND cad.DocumentType =@documentType
					)

					insert into @JournalVourcherDetailTmp 
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

					SELECT top 1	ma.Id,
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
					WHERE ma.Id=IIF(@totalAdjusted > 0, @ProfitLostByExchangeCurrencyAccountId, @LostByExchangeCurrencyAccountId)
				end
				else begin -- si los libros son diferentes entonces hago homologacion
				
					insert into @JournalVourcherDetailTmp 
					(IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
					select 
					cad.MainAccountId, 
					iif(ma.HandlesThirdParty = 1, @ThirdPartyCompanyId, null),
					NULL,--iif(ma.HandlesCostCenter = 1, cad.CostCenterId, null),
					iif(ProfitLostValue > 0, abs(ProfitLostValue), 0),
					iif(ProfitLostValue > 0, 0, abs(ProfitLostValue)),
					CONCAT('Documento no : ',cad.DocumentNumber), null, null, null, null
					from @currencyAdjustmentDetail cad
					inner join GeneralLedger.HomologationAccount ha on ha.OfficialMainAccountId = cad.MainAccountId
					inner join GeneralLedger.MainAccounts ma on ma.Id = ha.MainAccountId and ma.LegalBookId = @legalbookMovementId
					where CurrencyConverterId = @CurrencyBookRevalueId AND cad.DocumentType =@documentType

					--Calculo la contra partida para balancear el comprobante
					set @totalAdjusted = (
						select sum(ProfitLostValue)
						from @currencyAdjustmentDetail cad
						where CurrencyConverterId = @CurrencyBookRevalueId AND cad.DocumentType =@documentType
					)

						insert into @JournalVourcherDetailTmp 
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

					SELECT TOP 1	ma.Id,
									NULL,
									IIF(ma.HandlesCostCenter=1,@CostCenterId,NULL), 
									IIF(@totalAdjusted > 0, 0, abs(@totalAdjusted)),
									IIF(@totalAdjusted > 0, 
									abs(@totalAdjusted), 0), 
									'', 
									null,
									null, 
									null,
									null
					FROM GeneralLedger.HomologationAccount ha WITH(NOLOCK)
					inner join GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ha.MainAccountId and ma.LegalBookId = @legalbookMovementId
					where ha.OfficialMainAccountId = IIF(@totalAdjusted > 0, @ProfitLostByExchangeCurrencyAccountId, @LostByExchangeCurrencyAccountId)
				end
			
				--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
				SELECT @JournalVoucherXML = CONVERT(xml, 
					(
						SELECT * FROM @JournalVourcherTmp JournalVoucher 
						JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
						WHERE JournalVoucherDetail.DebitValue <> 0 OR JournalVoucherDetail.CreditValue <> 0
						For xml AUTO,TYPE, ELEMENTS
					)
				)

				if (@status = 1) begin
					--Se consume el sp que guarda el comprobante contable
					insert @resultsTable exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,'999' 
					
					
				end

				delete from @JournalVourcherTmp
				delete from @JournalVourcherDetailTmp

				NEXT_ROW:
				FETCH NEXT FROM documentType_Cursor   
				INTO @documentType

				END
				CLOSE documentType_Cursor;  
				DEALLOCATE documentType_Cursor;
				/**********************************/
				
				if (@status = 1) BEGIN
					update crer set crer.[Value] = acr.[Value] , crer.ValueReverse = acr.ValueReverse
					from Treasury.CashRegisters cr with(NOLOCK)					
					inner join Treasury.CashRegisterExchangeRate crer with(NOLOCK) on crer.CashRegisterId = cr.Id and crer.CurrencyId = @CurrencyBookRevalueId
					inner join @actualCurrencyRate acr on 1 = 1
					where isnull(cr.CurrencyId,@OfficialCurrencyId) = @currencyId

					update cr set cr.BalanceLastRevaluation=tr.NewBalance,cr.PeriodLastRevaluation=cast(concat(DATEPART(YEAR,@CutOffDate),FORMAT(@CutOffDate,'MM')) as int)
					from Treasury.CashRegisters cr with(NOLOCK)
					join(	SELECT tr.CashRegisterId,tr.NewBalance,trc.Month,trc.Year
							from [Treasury].[TreasuryRevaluation] tr with(NOLOCK)
							join Treasury.TreasuryRevaluationControl trc with(NOLOCK) on tr.TreasuryRevaluationControlId =trc.Id
							where tr.CashRegisterId is not null) tr on cr.Id=tr.CashRegisterId
					where isnull(cr.CurrencyId,@OfficialCurrencyId) = @currencyId   AND tr.Month = FORMAT(@CutOffDate,'MM') AND tr.Year = DATEPART(YEAR,@CutOffDate)

					update ebaer set ebaer.[Value] = acr.[Value] , ebaer.ValueReverse = acr.ValueReverse
					from Treasury.EntityBankAccounts eba with(NOLOCK)					
					inner join Treasury.EntityBankAccountExchangeRate ebaer with(NOLOCK) on ebaer.EntityBankAccountId = eba.Id and ebaer.CurrencyId = @CurrencyBookRevalueId
					inner join @actualCurrencyRate acr on 1 = 1
					where isnull(eba.CurrencyId,@OfficialCurrencyId) = @currencyId

					update eba set eba.BalanceLastRevaluation=tr.NewBalance,eba.PeriodLastRevaluation=cast(concat(DATEPART(YEAR,@CutOffDate),FORMAT(@CutOffDate,'MM')) as int)
					from Treasury.EntityBankAccounts eba with(NOLOCK)
					join(	SELECT tr.BankAccountId,tr.NewBalance,trc.Month,trc.Year
							from [Treasury].[TreasuryRevaluation] tr with(NOLOCK)
							join Treasury.TreasuryRevaluationControl trc with(NOLOCK) on tr.TreasuryRevaluationControlId =trc.Id
							where tr.BankAccountId is not null) tr on eba.Id=tr.BankAccountId
					where isnull(eba.CurrencyId,@OfficialCurrencyId) = @currencyId  AND tr.Month = FORMAT(@CutOffDate,'MM') AND tr.Year = DATEPART(YEAR,@CutOffDate)

					update treasury.TreasuryRevaluationControl set 
					Status = 2 , 
					ModificationUser =@userCode,
					ModificarionDate=Common.GETDATE(),
					ConfirmationUser = @userCode,
					ConfirmationDate = common.GETDATE()
					where Id = @_revaluationControlId
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
			select * from @resultsTable
		end
		else begin
			select 0 MessageCode, 'Se calculo correctamente la valorizacion' MessageVoucher, 0 as JournalVoucherId
		end
	end try
	begin catch 
		IF CURSOR_STATUS('global','currencyRevalue_cursor') >= -1  BEGIN
		  IF CURSOR_STATUS('global','currencyRevalue_cursor') > -1 BEGIN
			CLOSE currencyRevalue_cursor
		  END
		 DEALLOCATE currencyRevalue_cursor
		END
		
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
		select 999 MessageCode, ERROR_MESSAGE() as MessageVoucher, 0 as JournalVoucherId
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta el proceso de revalorización (ajuste por diferencia en cambio) de la tesorería para un mes y año determinados. Calcula las ganancias o pérdidas generadas por la fluctuación de tasas de cambio en documentos financieros con moneda extranjera (facturas, pagos, notas), comparando la tasa original del documento contra la tasa vigente al cierre del período (último día del mes). Crea o actualiza el encabezado de control del período en TreasuryRevaluationControl y regenera el detalle línea por línea en TreasuryRevaluationDetail y TreasuryRevaluation; también genera comprobantes contables de ajuste (ganancias o pérdidas por diferencial cambiario) usando las cuentas contables configuradas en CompanySettings. Valida que el período anterior ya esté confirmado y que no exista ya una revalorización confirmada para el mismo mes y año, garantizando la integridad del cierre contable de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SPCurrencyRevaluation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SPCurrencyRevaluation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se permiten dos revalorizaciones confirmadas (Status=2) para el mismo Month/Year; Un período sólo puede revalorizarse si el período inmediatamente anterior está confirmado (no quedó en Status=1); Antes de recalcular, se borra todo TreasuryRevaluation y TreasuryRevaluationDetail asociados al control del período para evitar duplicados; Sólo se procesan monedas que tienen al menos un libro legal activo en moneda distinta; Para cada caja, banco y saldo de tesorería involucrado debe existir TRM (o registro en CashRegisterExchangeRate/EntityBankAccountExchangeRate/TreasuryBalanceExchangeRate); si falta, el proceso aborta sin escribir comprobante; El comprobante contable se genera contra ProfitLostByExchangeCurrencyAccountId si hay ganancia y LostByExchangeCurrencyAccountId si hay pérdida, tomados de GeneralLedger.CompanySettings; Si el libro destino no es el oficial, todas las cuentas se traducen mediante GeneralLedger.HomologationAccount; Sólo en modo confirmación (@status=1) se actualizan tasas vigentes en CashRegisterExchangeRate/EntityBankAccountExchangeRate y se sellan BalanceLastRevaluation/PeriodLastRevaluation; El tercero del comprobante se asigna sólo cuando la cuenta tiene HandlesThirdParty=1, usando IdDian de GeneralLedgerSettings; La fecha de corte siempre es el último día del mes (@year,@month)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SPCurrencyRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Revalorización de tesorería; Diferencia en cambio (ganancia/pérdida); TRM (Tasa Representativa del Mercado); Libro contable oficial y homologación de cuentas; Comprobante contable (Journal Voucher); Cajas registradoras y cuentas bancarias en moneda extranjera; Saldo de tesorería (TreasuryBalance); Cierre/confirmación de período de revaluación; Tercero DIAN de la compañía; Centro de costo', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SPCurrencyRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en TreasuryRevaluationControl con Status=2 para el mes/año solicitados → THROW 50000 ''Ya existe una revalorizacion confirmada para este periodo''; si Existe TreasuryRevaluationControl con Status=1 para el mes/año previo a la fecha de corte → THROW 50000 ''No se puede Revalorizar, porque el periodo anterior no se ha confirmado''; si No existe TreasuryRevaluationControl para mes/año → Inserta cabecera con Status=1 (revalorización en cálculo) else Reutiliza el control existente; si Sólo existe un libro legal activo (sin libros con moneda distinta a @currencyId) → Salta la revalorización para esa moneda (GOTO cont); si No se encuentra TRM para la fecha de corte entre @currencyId y @CurrencyBookRevalueId → Cierra cursores y retorna MessageCode=999 con mensaje de TRM no encontrada; si Existen cajas/bancos/saldos de tesorería sin TRM en sus fechas iniciales/de documento → Retorna MessageCode=999 listando las fechas faltantes; si @legalbookMovementId = libro oficial (@legalbookId) → Genera detalle del comprobante usando MainAccountId directamente del documento else Realiza homologación vía GeneralLedger.HomologationAccount para obtener la cuenta del libro destino; si @totalAdjusted > 0 (ganancia neta) → Contrapartida con crédito en ProfitLostByExchangeCurrencyAccountId else Contrapartida con débito en LostByExchangeCurrencyAccountId; si @status = 1 (confirmar) → Ejecuta SP_CreateAndValidateJournalVoucherMovement, actualiza tasas en CashRegisterExchangeRate/EntityBankAccountExchangeRate, actualiza BalanceLastRevaluation y PeriodLastRevaluation en cajas/bancos, y marca TreasuryRevaluationControl.Status=2 con auditoría de confirmación else Sólo entrega cálculo previo con mensaje ''Se calculo correctamente la valorizacion''; si ProfitLostValue se calcula según naturaleza → Si Nature=1: ActualValueMovementConverted - ValueMovementConverted; en caso contrario: ValueMovementConverted - ActualValueMovementConverted', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SPCurrencyRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Portfolio.fnConvertValueBasedOnExchangeRate; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SPCurrencyRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; GeneralLedger.CompanySettings; GeneralLedger.GeneralLedgerSettings; GeneralLedger.MainAccounts; GeneralLedger.HomologationAccount; Treasury.TreasuryRevaluationControl; Treasury.TreasuryRevaluation; Treasury.TreasuryRevaluationDetail; Treasury.CashRegisters; Treasury.CashRegisterExchangeRate; Treasury.EntityBankAccounts; Treasury.EntityBankAccountExchangeRate; Treasury.TreasuryBalance; Treasury.TreasuryBalanceExchangeRate; Treasury.CashReceipts; Common.TRM; Common.Currency; Billing.CustomTRM', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SPCurrencyRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SPCurrencyRevaluation';
-- GO
