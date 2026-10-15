-- =============================================
-- Author:      Jose Paez
-- Create Date: 03/02/2023
-- Description: Procedimiento que se usa para revalorizar un documento a la fecha actual (TRM)
-- =============================================

/* @ListAccountPayable =
'<AccountPayable>
<Id></Id>
<ValuePaid></ValuePaid>
<EntityName></EntityName>
<EntityId></EntityId>
</AccountPayable>'*/
CREATE PROCEDURE [Payments].[SP_AccountPayableRevaluation]
(
    @ListAccountPayableXml Xml,
	@UserCode VARCHAR(25),
	@XmlOutPut xml OUTPUT
)
AS
BEGIN

	Declare @ListAccountPayable Table	
	(
		Id INT  NOT NULL,
		ValuePaid NUMERIC(20,2) NOT NULL,
		EntityName VARCHAR(250),
		EntityId INT,
		CurrencyId INT,
		ValuePaidTRMConverted NUMERIC(20,2),
		ValuePaidTRMDocument NUMERIC(20,2),
		ProfitLostValue NUMERIC(20,2))

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

	declare @actualCurrencyRate table (	CurrencyFrom int,
										CurrencyTo int, 
										[Value] numeric(20, 5),
										ValueReverse numeric(20, 5))
-- tabla de resultado que guarda el id del combrobante contable
	declare @tempTable as table (Code varchar(20),MessageOutput varchar(max),JournalVoucherId INT)
	--Variable para obtener el xml
	DECLARE @JournalVoucherXML as XML
	DECLARE @currencyId INT,
			@bookId INT,
			@DocumentCurrencyId INT,
			@ProfitLostJournalVoucherTypeId INT,
			@ProfitByExchangeCurrencyAccountId INT,
			@LostByExchangeCurrencyAccountId INT,
			@OfficialBookId INT,
			@CostCenterCsId INT,
			@TotalProfitLostValue NUMERIC(20,2),
			@EntityNameDescription VARCHAR(250),
			@Nature TINYINT = 1, -- Naturaleza de la nota(Debito=1, Credito=2) debito ( resta valor a la cxp) - Credito (aumenta el valor de la cxp); 
			@DocumentCode VARCHAR(20),
			@DocumentDate DATE -- Fecha del Documento que genera el ajuste diferencial

BEGIN TRY

	-- se almacenan los datos del input en la tabla variable
	INSERT INTO @ListAccountPayable (Id,ValuePaid,EntityName,EntityId)
	SELECT
	t.x.value('Id[1]', 'int')  Id,
	t.x.value('ValuePaid[1]', 'NUMERIC(20,2)')  ValuePaid,
	t.x.value('EntityName[1]', 'VARCHAR(250)')  EntityName,
	t.x.value('EntityId[1]', 'INT')  EntityId
	from @ListAccountPayableXml.nodes('/AccountPayable') t(x);

	if NOT EXISTS(select 1 from @ListAccountPayable) BEGIN
		INSERT INTO @tempTable (Code,MessageOutput,JournalVoucherId) VALUES('0','No hay cxp para revalorizar',0)			
			SET @XmlOutPut = CONVERT(xml, 
									(
										SELECT * FROM @tempTable TableResult 
										For xml AUTO,TYPE, ELEMENTS
									))
			RETURN
	END

	/**** Se establece Datos de la entidad que genera el ajuste diferencial ****/
	SELECT	@DocumentCode = Header.Code,
			@EntityNameDescription= Header.Description,
			@Nature= Header.Nature,
			@DocumentDate = Header.DocumentDate			
	FROM (	
			SELECT	vend.Description,
					vt.Code,
					1 Nature,
					CAST(vt.DocumentDate AS DATE) AS DocumentDate
			FROM @ListAccountPayable temp
			JOIN Treasury.VoucherTransaction vt WITH(NOLOCK) on temp.EntityId=vt.Id and temp.EntityName='VoucherTransaction'
			JOIN [GeneralLedger].[ViewEntityNameDescriptions] vend WITH(NOLOCK) on vend.EntityName='VoucherTransaction'
			GROUP by vend.Description, vt.Code,vt.DocumentDate
			
			UNION ALL
			
			SELECT	vend.Description,
					pn.Code ,
					pn.Nature,
					CAST(pn.NoteDate AS DATE) AS DocumentDate
			FROM @ListAccountPayable temp
			JOIN Payments.PaymentNotes pn WITH(NOLOCK) on temp.EntityId=pn.Id and temp.EntityName='PaymentNotes'
			JOIN [GeneralLedger].[ViewEntityNameDescriptions] vend WITH(NOLOCK) on vend.EntityName='PaymentNotes'
			GROUP by vend.Description, pn.Code,pn.Nature,pn.NoteDate
			
			UNION ALL
			
			SELECT	vend.[Description],
					pt.Code,
					1 Nature,
					CAST(pt.DocumentDate AS DATE) AS DocumentDate
			FROM @ListAccountPayable temp
			JOIN Payments.PaymentTransfer pt WITH(NOLOCK) ON temp.EntityId=pt.Id and temp.EntityName='PaymentTransfer'
			JOIN [GeneralLedger].[ViewEntityNameDescriptions] vend WITH(NOLOCK) on vend.EntityName='PaymentTransfer'
			GROUP BY vend.[Description],pt.Code,pt.DocumentDate

			UNION ALL	

			SELECT	vend.[Description],
					ca.Code,
					1 Nature,
					CAST(ca.DocumentDate AS DATE) AS DocumentDate
			FROM @ListAccountPayable temp
			JOIN Treasury.CrossingAccount ca WITH(NOLOCK) ON temp.EntityId=ca.Id and temp.EntityName='CrossingAccount'
			JOIN [GeneralLedger].[ViewEntityNameDescriptions] vend WITH(NOLOCK) on vend.EntityName='CrossingAccount'
			GROUP BY vend.[Description],ca.Code,ca.DocumentDate

			) as Header
	
	--Se valida que venga la Fecha del Documento
	IF @DocumentDate IS NULL BEGIN
		INSERT INTO @tempTable (Code,MessageOutput,JournalVoucherId) VALUES('999','No se encontró la fecha del documento',0)			
			SET @XmlOutPut = CONVERT(xml, 
									(
										SELECT * FROM @tempTable TableResult 
										For xml AUTO,TYPE, ELEMENTS
									))
			RETURN
	END

	/*******************************************************/

	-- actualizamos la tabla variable con la moneda correspondiente de cada cuenta
	UPDATE temp set temp.CurrencyId = ap.CurrencyId
	from @ListAccountPayable temp
	join Payments.AccountPayable ap with(NOLOCK) on temp.Id = ap.Id

	--se validan que todas las facturas tengan moneda
	if EXISTS(SELECT 1 from @ListAccountPayable where CurrencyId is null or CurrencyId =0) BEGIN
		INSERT INTO @tempTable
		SELECT 999 AS MessageCode, 
				concat('Las facturas : ','( ',STRING_AGG(ap.BillNumber,','),' ) ','No tienen moneda especificada') MessageVoucher,
				0 JournalVocuherId
		from @ListAccountPayable temp
		join Payments.AccountPayable ap with(NOLOCK) on temp.Id = ap.Id
		where temp.CurrencyId is null or temp.CurrencyId =0;

		SET @XmlOutPut = CONVERT(xml, 
								(
									SELECT * FROM @tempTable TableResult 
									For xml AUTO,TYPE, ELEMENTS
								))
		return
	END

	-- Se obtienen los parametros de empresa
	select top 1 
	@ProfitByExchangeCurrencyAccountId = ProfitLostByExchangeCurrencyAccountId,
	@LostByExchangeCurrencyAccountId = LostByExchangeCurrencyAccountId,
	@ProfitLostJournalVoucherTypeId = ProfitLostJournalVoucherTypeId,
	@CostCenterCsId = CostCenterId
	from GeneralLedger.CompanySettings	

	--libro oficial
	SET @OfficialBookId = (SELECT TOP 1 lb.Id FROM GeneralLedger.LegalBook lb WHERE lb.OfficialBook = 1)

	DECLARE currency_cursor CURSOR FOR   
		select Id, OfficialCurrencyId,temp.CurrencyId
		from GeneralLedger.LegalBook l WITH(NOLOCK)
		join (	SELECT a.CurrencyId
				from @ListAccountPayable a
				GROUP by a.CurrencyId) temp on l.OfficialCurrencyId <> temp.CurrencyId
		where [Status] = 1 
		GROUP by l.Id, l.OfficialCurrencyId, temp.CurrencyId
  
	OPEN currency_cursor  
  
	FETCH NEXT FROM currency_cursor   
	INTO @bookId, @currencyId, @DocumentCurrencyId
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN
		delete from @actualCurrencyRate
		--Busco la tasa de conversion de la moneda con la fecha actual
		if (select count(*) from Common.TRM where MeasurementDate = @DocumentDate and CurrencyId = @DocumentCurrencyId and OfficialCurrencyId = @currencyId) > 0 begin
			
			insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
			select @DocumentCurrencyId, @currencyId, [Value], ValueOfficialToCurrency 
			from Common.TRM where MeasurementDate = @DocumentDate and CurrencyId = @DocumentCurrencyId and OfficialCurrencyId = @currencyId
		
		end
		else if (select count(*) from Common.TRM where MeasurementDate = @DocumentDate and OfficialCurrencyId = @DocumentCurrencyId and CurrencyId = @currencyId) > 0 begin
			insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse)
			select @DocumentCurrencyId, @currencyId, ValueOfficialToCurrency, [Value] 
			from Common.TRM where MeasurementDate = @DocumentDate and OfficialCurrencyId = @DocumentCurrencyId and CurrencyId = @currencyId
		end

		if (select count(*) from @actualCurrencyRate) = 0 begin
				CLOSE currency_cursor;  
				DEALLOCATE currency_cursor;  
				
				INSERT INTO @tempTable
				SELECT 999 AS MessageCode, 
				'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @currencyId)  +') para la fecha actual ' MessageVoucher,
				0 JournalVoucherId
				
				SET @XmlOutPut = CONVERT(xml, 
								(
									SELECT * FROM @tempTable TableResult 
									For xml AUTO,TYPE, ELEMENTS
								))
				return
		end

		if (select count(*) 
			from Payments.AccountPayable ap WITH(NOLOCK)
			join @ListAccountPayable temp on ap.Id = temp.Id and temp.CurrencyId = @DocumentCurrencyId
			left join Payments.AccountPayableExchangeRate er on er.AccountPayableId = ap.Id and er.CurrencyId = @currencyId
			where er.Id is null) > 0 begin 
			
			INSERT INTO @tempTable
			select 999 AS MessageCode, 
			'No se encontro un TRM definido para la(s) factura(s) (' + (select STRING_AGG( ap.BillNumber,',')
																	from Payments.AccountPayable ap WITH(NOLOCK)
																	join @ListAccountPayable temp on ap.Id = temp.Id and temp.CurrencyId = @DocumentCurrencyId
																	where temp.CurrencyId =@DocumentCurrencyId)  +')'  MessageVoucher, 0 JournalVoucherId
			
			SET @XmlOutPut = CONVERT(xml, 
								(
									SELECT * FROM @tempTable TableResult 
									For xml AUTO,TYPE, ELEMENTS
								))
			RETURN
		END

		--Se reutiliza la funcion de conversion de valores basado en la tasa de cambio de cartera (Portfolio)

		UPDATE temp SET
				temp.ValuePaidTRMConverted = [Portfolio].fnConvertValueBasedOnExchangeRate(temp.ValuePaid, acr.[Value] , acr.ValueReverse),
				temp.ValuePaidTRMDocument = [Portfolio].fnConvertValueBasedOnExchangeRate(temp.ValuePaid, aper.[Value] , aper.ValueReverse) 
		FROM @ListAccountPayable temp
		join @actualCurrencyRate acr on acr.CurrencyFrom = temp.CurrencyId and acr.CurrencyTo = @currencyId
		join Payments.AccountPayableExchangeRate aper WITH(NOLOCK) on aper.AccountPayableId = temp.Id AND aper.CurrencyId = @currencyId
		where temp.CurrencyId = @DocumentCurrencyId

		UPDATE temp SET
				temp.ProfitLostValue = ( temp.ValuePaidTRMConverted - temp.ValuePaidTRMDocument) * IIF(@Nature=1,1,-1)
		FROM @ListAccountPayable temp
		where temp.CurrencyId = @DocumentCurrencyId

		IF NOT EXISTS(SELECT 1 from @ListAccountPayable where abs(ProfitLostValue) >0) BEGIN
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
			CONCAT('Ajuste por diferencia en cambio CxP, ', @EntityNameDescription ,'( ',ISNULL(@DocumentCode,''),' ) ',cast(common.GETDATE() as VARCHAR)),  
			'', 
			0,
			'JournalVouchers',
			@currencyId
		)

		SET @TotalProfitLostValue = (	SELECT SUM(temp.ProfitLostValue)
											FROM @ListAccountPayable temp
											where temp.CurrencyId = @DocumentCurrencyId)


		IF @OfficialBookId = @bookId BEGIN --Si es el mismo libro entonces no hacemos homologacion 

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
			select 
				ma.Id, 
				iif(ma.HandlesThirdParty = 1, ap.IdThirdParty, null),
				iif(ma.HandlesCostCenter = 1, ap.IdCostCenter, null),
				iif(temp.ProfitLostValue < 0, ABS(temp.ProfitLostValue), 0),
				iif(temp.ProfitLostValue < 0, 0, abs(temp.ProfitLostValue)),
				Concat('Factura No : ',ap.BillNumber), null, null, null, null 
			from Payments.AccountPayable ap WITH(NOLOCK)
			join @ListAccountPayable temp on ap.Id = temp.Id
			INNER JOIN GeneralLedger.MainAccounts ma on ma.Id = ap.IdAccount
			where temp.CurrencyId = @DocumentCurrencyId

			insert into @JournalVourcherDetailTmp 
			(IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)			
			select	ma.Id
					,NULL
					,IIF(ma.HandlesCostCenter = 1,@CostCenterCsId,NULL)
					,IIF(@TotalProfitLostValue < 0, 0, ABS(@TotalProfitLostValue))
					,IIF(@TotalProfitLostValue < 0, abs(@TotalProfitLostValue), 0)
					, ''
					, null
					, null
					, null
					, null
			FROM GeneralLedger.MainAccounts ma WITH(NOLOCK)
			where ma.Id =iif(@TotalProfitLostValue < 0 ,@ProfitByExchangeCurrencyAccountId,@LostByExchangeCurrencyAccountId)
		END
		ELSE BEGIN -- homologamos
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
				select 
						ma.Id, 
						iif(ma.HandlesThirdParty = 1, ap.IdThirdParty, null),
						iif(ma.HandlesCostCenter = 1, ap.IdCostCenter, null),
						iif(temp.ProfitLostValue < 0, ABS(temp.ProfitLostValue), 0),
						iif(temp.ProfitLostValue < 0, 0, abs(temp.ProfitLostValue)),
						Concat('Factura No : ',ap.BillNumber), null, null, null, null
			from Payments.AccountPayable ap WITH(NOLOCK)
			join @ListAccountPayable temp on ap.Id = temp.Id
			INNER JOIN GeneralLedger.HomologationAccount ha ON ha.OfficialMainAccountId = ap.IdAccount
			INNER JOIN GeneralLedger.MainAccounts ma on ma.Id = ha.MainAccountId and ma.LegalBookId = @bookId
			where temp.CurrencyId = @DocumentCurrencyId

			insert into @JournalVourcherDetailTmp 
			(IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)
			select	ma.Id
					,null
					,IIF(ma.HandlesCostCenter=1,@CostCenterCsId,NULL)
					,IIF(@TotalProfitLostValue < 0, 0, ABS(@TotalProfitLostValue))
					,IIF(@TotalProfitLostValue < 0, abs(@TotalProfitLostValue), 0)
					,''
					, null
					, null
					, null
					, null
			from GeneralLedger.HomologationAccount ha
			inner join GeneralLedger.MainAccounts ma on ma.Id = ha.MainAccountId and ma.LegalBookId = @bookId
			where ha.OfficialMainAccountId = iif( @TotalProfitLostValue  < 0,@ProfitByExchangeCurrencyAccountId,@LostByExchangeCurrencyAccountId) 
		end

		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		SELECT @JournalVoucherXML = CONVERT(xml, 
			(
				SELECT * FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		insert into @tempTable(Code,MessageOutput,JournalVoucherId)
		exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @UserCode

		NEXT_ROW:
		FETCH NEXT FROM currency_cursor   
		INTO @bookId, @currencyId, @DocumentCurrencyId

	END   
	CLOSE currency_cursor;  
	DEALLOCATE currency_cursor;

	SET @XmlOutPut = CONVERT(xml, 
								(
									SELECT * FROM @tempTable TableResult 
									For xml AUTO,TYPE, ELEMENTS
								))

	RETURN
END TRY
BEGIN CATCH
	IF CURSOR_STATUS('global','currency_cursor') >= -1  BEGIN
		  IF CURSOR_STATUS('global','currency_cursor') > -1 BEGIN
			CLOSE currency_cursor
		  END
		 DEALLOCATE currency_cursor
		END
		select 999 MessageCode, ERROR_MESSAGE() as MessageVoucher, 0 as JournalVoucherId
END CATCH
END