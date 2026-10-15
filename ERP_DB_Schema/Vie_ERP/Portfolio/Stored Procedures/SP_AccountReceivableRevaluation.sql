-- =============================================
-- Author:      Cristhian Salazar
-- Create Date: 03/02/2023
-- Description: Procedimiento que se usa para revalorizar un documento a la fecha actual (TRM)
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_AccountReceivableRevaluation]
(
    @AccountReceivableId INT,
	@ValuePaid DECIMAL(18,2),
	@EntityCode VARCHAR(20),
	@EntityId INT,
	@EntityName varchar(250),
	@UserCode VARCHAR(25),
	@DocumentDate DATE
)
AS
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

	declare @actualCurrencyRate table (CurrencyFrom int, CurrencyTo int, [Value] numeric(20, 5), ValueReverse numeric(20, 5))
	DECLARE @DocumentCurrencyId INT = (SELECT ar.CurrencyId FROM Portfolio.AccountReceivable ar WITH(NOLOCK) WHERE ar.Id = @AccountReceivableId)
	DECLARE @currencyId INT, @bookId INT, @ProfitLostJournalVoucherTypeId INT, @ProfitLostByExchangeCurrencyAccountId INT, @OfficialBookId INT,@CostCenterCsId INT
	--Variable para obtener el xml
	DECLARE @JournalVoucherXML as XML
	DECLARE @LostByExchangeCurrencyAccountId INT
	DECLARE @EntityNameDescription as varchar(250)
	DECLARE	@Nature TINYINT=2

	select top 1 
	@ProfitLostByExchangeCurrencyAccountId = ProfitLostByExchangeCurrencyAccountId,
	@ProfitLostJournalVoucherTypeId = ProfitLostJournalVoucherTypeId,
	@CostCenterCsId = CostCenterId,
	@LostByExchangeCurrencyAccountId = LostByExchangeCurrencyAccountId
	from GeneralLedger.CompanySettings WITH(NOLOCK)	

	SET @OfficialBookId = (SELECT TOP 1 lb.Id FROM GeneralLedger.LegalBook lb WITH(NOLOCK) WHERE lb.OfficialBook = 1)

	-----Nombre de la entidad que genera el ajuste diferencial------
	SELECT top 1 @EntityNameDescription = Description
	from GeneralLedger.ViewEntityNameDescriptions WITH(NOLOCK)	
	where EntityName = @EntityName
	-------------------------------------------------------------------
	/*------Identificacion Movimento de reversion-----*/
	IF ISNULL(@EntityName,'') ='PortfolioNote' BEGIN
		SELECT @Nature = pn.Nature--Naturaleza de la nota 1 - Debito 2 - Credito
		from Portfolio.PortfolioNote pn WITH(NOLOCK)
		WHERE pn.Id = @EntityId
	END
	ELSE IF (ISNULL(@EntityName,'') ='PortfolioTransfer') BEGIN
		SELECT  @Nature = IIF(pt.Status=2,2,1)
		from Portfolio.PortfolioTransfer pt WITH(NOLOCK)
		WHERE pt.Id = @EntityId
	END
	/*--------------------------------------------*/

	/*--------------Validacion Fecha--------------------*/
	if  COALESCE(@DocumentDate,'') ='' begin
				
				SELECT 999 AS CodeMessage, 
				'La fecha del documento no pude venir vacia' [Message],
				0 as IdJournalVoucher
				return
	end
	/*-------------------------------------------------*/

	DECLARE currency_cursor CURSOR FOR   
	select distinct Id, OfficialCurrencyId from GeneralLedger.LegalBook WITH(NOLOCK) where [Status] = 1 AND OfficialCurrencyId <> @DocumentCurrencyId
  
	OPEN currency_cursor  
  
	FETCH NEXT FROM currency_cursor   
	INTO @bookId, @currencyId
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN
		delete from @actualCurrencyRate
		--Busco la tasa de conversion de la moneda con la fecha actual
		if (select count(*) from Common.TRM WITH(NOLOCK) where MeasurementDate = @DocumentDate and CurrencyId = @DocumentCurrencyId and OfficialCurrencyId = @currencyId) > 0 begin
			insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
			select @DocumentCurrencyId, @currencyId, [Value], ValueOfficialToCurrency 
			from Common.TRM WITH(NOLOCK) where MeasurementDate = @DocumentDate and CurrencyId = @DocumentCurrencyId and OfficialCurrencyId = @currencyId
		end
		else if (select count(*) from Common.TRM WITH(NOLOCK) where MeasurementDate = @DocumentDate and OfficialCurrencyId = @DocumentCurrencyId and CurrencyId = @currencyId) > 0 begin
			insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
			select @DocumentCurrencyId, @currencyId, ValueOfficialToCurrency, [Value] 
			from Common.TRM WITH(NOLOCK) where MeasurementDate = @DocumentDate and OfficialCurrencyId = @DocumentCurrencyId and CurrencyId = @currencyId
		end

		if (select count(*) from @actualCurrencyRate) = 0 begin
				CLOSE currency_cursor;  
				DEALLOCATE currency_cursor;  
				SELECT 999 AS CodeMessage, 
				'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency WITH(NOLOCK)	 where Id = @currencyId)  +') para la fecha actual ' [Message],
				0 as IdJournalVoucher
				return
		end

		if (select count(*) from Portfolio.AccountReceivable ar WITH(NOLOCK)	
			left join Portfolio.AccountReceivableExchangeRate er WITH(NOLOCK) on er.AccountReceivableId = ar.Id and er.CurrencyId = @currencyId
			where ar.Id = @AccountReceivableId and er.Id is null) > 0 begin 
 
			select 999 AS CodeMessage, 
			'No se encontro un TRM definido para la factura (' + (select ar.InvoiceNumber from Portfolio.AccountReceivable ar WITH(NOLOCK) where ar.Id = @AccountReceivableId)  +')'  [Message],
			0 as IdJournalVoucher
			return
		END

		DECLARE @ValuePaidTRMConverted DECIMAL(18, 2), @BalanceTRMConverted DECIMAL(18, 2)
		SET @ValuePaidTRMConverted = (SELECT [Portfolio].fnConvertValueBasedOnExchangeRate(@ValuePaid, er.[Value] , er.ValueReverse) FROM @actualCurrencyRate er)
		SET @BalanceTRMConverted = (SELECT [Portfolio].fnConvertValueBasedOnExchangeRate(@ValuePaid, arer.[Value] , arer.ValueReverse) 
										FROM Portfolio.AccountReceivableExchangeRate arer WITH(NOLOCK)
										WHERE arer.AccountReceivableId = @AccountReceivableId AND arer.CurrencyId = @currencyId
									)
		DECLARE @ValueAffected DECIMAL(18, 2) =  (@ValuePaidTRMConverted - @BalanceTRMConverted) * IIF(@Nature =2,1,-1)

		IF @ValueAffected = 0 BEGIN
			GOTO NEXT_ROW
		END

		INSERT INTO @JournalVourcherTmp
		(LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, CurrencyId)
		VALUES
		(
			@bookId, 
			@ProfitLostJournalVoucherTypeId, 
			@DocumentDate, 
			2, 
			CONCAT('Ajuste por diferencia en cambio ',': ',ISNULL(@EntityNameDescription,''),' ',@EntityCode),
			'', 
			0,
			'JournalVouchers',
			@currencyId
		)

		IF @OfficialBookId = @bookId BEGIN --Si es el mismo libro entonces no hacemos homologacion 
			insert into @JournalVourcherDetailTmp 
			(IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
			select 
			ma.Id, 
			iif(ma.HandlesThirdParty = 1, ar.ThirdPartyId, null),
			iif(ma.HandlesCostCenter = 1, ar.CostCenterId, null),
			iif(@ValueAffected > 0 , ABS(@ValueAffected), 0),
			iif(@ValueAffected > 0 , 0, abs(@ValueAffected)),
			CONCAT('Factura No : ',ar.InvoiceNumber), null, null, null, null
			from Portfolio.AccountReceivable ar WITH(NOLOCK)
			INNER JOIN Portfolio.AccountReceivableAccounting ara WITH(NOLOCK) ON ara.AccountReceivableId = ar.Id
			inner join GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ara.MainAccountId
			where ar.Id = @AccountReceivableId

			insert into @JournalVourcherDetailTmp 
			(IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)			
			select	ma.Id
					,NULL
					,IIF(ma.HandlesCostCenter = 1,@CostCenterCsId,NULL)
					,IIF(@ValueAffected > 0, 0, ABS(@ValueAffected))
					,IIF(@ValueAffected > 0, abs(@ValueAffected), 0)
					, ''
					, null
					, null
					, null
					, null
			FROM GeneralLedger.MainAccounts ma WITH(NOLOCK)	 where ma.Id = IIF( @ValueAffected > 0 ,@ProfitLostByExchangeCurrencyAccountId,@LostByExchangeCurrencyAccountId) 
		END
		ELSE BEGIN -- homologamos
			insert into @JournalVourcherDetailTmp 
			(IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
			select 
			ma.Id, 
			iif(ma.HandlesThirdParty = 1, ar.ThirdPartyId, null),
			iif(ma.HandlesCostCenter = 1, ar.CostCenterId, null),
			iif(@ValueAffected > 0, ABS(@ValueAffected), 0),
			iif(@ValueAffected > 0, 0, abs(@ValueAffected)),
			CONCAT('Factura No : ',ar.InvoiceNumber), null, null, null, null
			from Portfolio.AccountReceivable ar WITH(NOLOCK)	
			INNER JOIN Portfolio.AccountReceivableAccounting ara WITH(NOLOCK) ON ara.AccountReceivableId = ar.Id
			inner join GeneralLedger.HomologationAccount ha WITH(NOLOCK) ON ha.OfficialMainAccountId = ara.MainAccountId
			inner join GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ha.MainAccountId and ma.LegalBookId = @bookId
			where ar.Id = @AccountReceivableId

			insert into @JournalVourcherDetailTmp 
			(IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
			select	ma.Id
					,null
					,IIF(ma.HandlesCostCenter=1,@CostCenterCsId,NULL)
					,IIF(@ValueAffected > 0 , 0, ABS(@ValueAffected))
					,IIF(@ValueAffected > 0 , abs(@ValueAffected), 0)
					,''
					, null
					, null
					, null
					, null
			from GeneralLedger.HomologationAccount ha WITH(NOLOCK)	
			inner join GeneralLedger.MainAccounts ma WITH(NOLOCK)	 on ma.Id = ha.MainAccountId and ma.LegalBookId = @bookId
			where ha.OfficialMainAccountId =  IIF( @ValueAffected > 0 ,@ProfitLostByExchangeCurrencyAccountId,@LostByExchangeCurrencyAccountId) 
		end

		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		SELECT @JournalVoucherXML = CONVERT(xml, 
			(
				SELECT * FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @UserCode

		NEXT_ROW:
		FETCH NEXT FROM currency_cursor   
		INTO @bookId, @currencyId

	END   
	CLOSE currency_cursor;  
	DEALLOCATE currency_cursor;

END