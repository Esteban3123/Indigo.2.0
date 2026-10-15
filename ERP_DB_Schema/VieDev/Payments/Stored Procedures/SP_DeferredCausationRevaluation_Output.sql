-- =============================================
-- Author:      Giovanny Plazas Lozano
-- Create Date: 22/07/2024
-- Description: Procedimiento que se usa para revalorizar un documento a la fecha actual (TRM)
-- =============================================

/* @ListDeferredCausationXml =
'<Data>
<DeferredCausationRevaluation>
<Id></Id>
<ValueAdjustment></ValueAdjustment>
<EntityName></EntityName>
<EntityId></EntityId>
<DocumentDate></DocumentDate>
</DeferredCausationRevaluation>
<DeferredCausationRevaluation>
<Id></Id>
<ValueAdjustment></ValueAdjustment>
<EntityName></EntityName>
<EntityId></EntityId>
<DocumentDate></DocumentDate>
</DeferredCausationRevaluation>
</Data>'*/
CREATE PROCEDURE [Payments].[SP_DeferredCausationRevaluation_Output]
(
    @ListDeferredCausationXml Xml,
	@UserCode VARCHAR(25),
	@XmlOutPut xml OUTPUT
)
AS
BEGIN

	Declare @ListDeferredCausation Table	
	(
		Id INT  NOT NULL,
		ValueAdjustment NUMERIC(20,2) NOT NULL,
		EntityName VARCHAR(250),
		EntityId INT,
		CurrencyId INT,
		ValueTRMConverted NUMERIC(20,2),
		ValueTRMDocument NUMERIC(20,2),
		ProfitLostValue NUMERIC(20,2),
		EntityCode VARCHAR(20),
		Nature TINYINT DEFAULT(2), --Naturaleza  1 - Debito / 2 - Credito, la naturaleza del documento
		DocumentDate DATE)

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE 
	(	IdTemp INT IDENTITY(1,1),
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
	(	IdTempHeader INT DEFAULT(0),
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
										ValueReverse numeric(20, 5),
										EntityId INT)

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
			@CostCenterCsId INT

BEGIN TRY

	-- se almacenan los datos del input en la tabla variable
	INSERT INTO @ListDeferredCausation (Id,ValueAdjustment,EntityName,EntityId,DocumentDate)
	SELECT
	t.x.value('Id[1]', 'int')  Id,
	t.x.value('ValueAdjustment[1]', 'NUMERIC(20,2)')  ValuePaid,
	t.x.value('EntityName[1]', 'VARCHAR(250)')  EntityName,
	t.x.value('EntityId[1]', 'INT')  EntityId,
	t.x.value('DocumentDate[1]', 'DATE')  DocumentDate
	from @ListDeferredCausationXml.nodes('Data/DeferredCausationRevaluation') t(x);

	-- actualizamos la tabla variable con la moneda correspondiente de cada cuenta
	UPDATE temp set temp.CurrencyId = ap.CurrencyId
	from @ListDeferredCausation temp
	join Payments.DeferredCausation dc with(NOLOCK) on temp.Id = dc.Id
	join Payments.AccountPayable ap WITH(NOLOCK) on dc.IdAccountPayable =ap.Id

	--se validan que todas las facturas tengan moneda
	if EXISTS(SELECT 1 from @ListDeferredCausation where CurrencyId is null or CurrencyId =0) BEGIN
		INSERT INTO @tempTable
		SELECT 999 AS MessageCode, 
				concat('Los diferidos de las CxP : ','( ',STRING_AGG(ap.Code,','),' ) ','no tienen moneda especificada') MessageVoucher,
				0 JournalVocuherId
		from @ListDeferredCausation temp
		join  Payments.DeferredCausation dc with(NOLOCK) on temp.Id = dc.Id
		join Payments.AccountPayable ap WITH(NOLOCK) on ap.Id =dc.IdAccountPayable
		where temp.CurrencyId is null or temp.CurrencyId =0;

		SET @XmlOutPut = CONVERT(xml, 
								(
									SELECT * FROM @tempTable TableResult 
									For xml AUTO,TYPE, ELEMENTS
								))
		RETURN
	END

	if EXISTS(SELECT 1 from @ListDeferredCausation where DocumentDate IS NULL) BEGIN
		INSERT INTO @tempTable
		SELECT 999 AS MessageCode, 
				concat('Los diferidos de las CxP : ','( ',STRING_AGG(ap.Code,','),' ) ','no tiene fecha para ejecutar el ajuste diferencial') MessageVoucher,
				0 JournalVocuherId
		from @ListDeferredCausation temp
		join  Payments.DeferredCausation dc with(NOLOCK) on temp.Id = dc.Id
		join Payments.AccountPayable ap WITH(NOLOCK) on ap.Id =dc.IdAccountPayable
		where temp.DocumentDate IS NULL;

		SET @XmlOutPut = CONVERT(xml, 
								(
									SELECT * FROM @tempTable TableResult 
									For xml AUTO,TYPE, ELEMENTS
								))
		RETURN
	END

	-- Se obtienen los parametros de empresa
	select top 1 
	@ProfitByExchangeCurrencyAccountId = ProfitLostByExchangeCurrencyAccountId,
	@LostByExchangeCurrencyAccountId = LostByExchangeCurrencyAccountId,
	@ProfitLostJournalVoucherTypeId = ProfitLostJournalVoucherTypeId,
	@CostCenterCsId = CostCenterId
	from GeneralLedger.CompanySettings	with(NOLOCK)

	--libro oficial
	SET @OfficialBookId = (SELECT TOP 1 lb.Id FROM GeneralLedger.LegalBook lb with(NOLOCK) WHERE lb.OfficialBook = 1)

	DECLARE currency_cursor1 CURSOR FOR   
		select Id, OfficialCurrencyId,temp.CurrencyId
		from GeneralLedger.LegalBook l WITH(NOLOCK)
		join (	SELECT a.CurrencyId
				from @ListDeferredCausation a
				GROUP by a.CurrencyId) temp on l.OfficialCurrencyId <> temp.CurrencyId
		where [Status] = 1 
		GROUP by l.Id, l.OfficialCurrencyId, temp.CurrencyId
  
	OPEN currency_cursor1  
  
	FETCH NEXT FROM currency_cursor1   
	INTO @bookId, @currencyId, @DocumentCurrencyId
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN
		delete from @actualCurrencyRate
		--Busco la tasa de conversion de la moneda con la fecha actual
		if EXISTS (	select 1
					from Common.TRM trm WITH(NOLOCK)
					JOIN (	SELECT temp.DocumentDate, temp.EntityId, temp.CurrencyId
							FROM @ListDeferredCausation temp
							WHERE temp.CurrencyId=@DocumentCurrencyId
							GROUP BY temp.DocumentDate, temp.EntityId,temp.CurrencyId ) temp 
					ON trm.MeasurementDate = temp.DocumentDate AND trm.CurrencyId = temp.CurrencyId AND trm.OfficialCurrencyId = @currencyId) begin
			
				insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse,EntityId)
				select @DocumentCurrencyId, @currencyId, [Value], ValueOfficialToCurrency,temp.EntityId
				from Common.TRM trm WITH(NOLOCK)
				JOIN (	SELECT temp.DocumentDate, temp.EntityId, temp.CurrencyId
						FROM @ListDeferredCausation temp
						WHERE temp.CurrencyId=@DocumentCurrencyId
						GROUP BY temp.DocumentDate, temp.EntityId,temp.CurrencyId ) temp 
				ON trm.MeasurementDate = temp.DocumentDate AND trm.CurrencyId = temp.CurrencyId AND trm.OfficialCurrencyId = @currencyId
			
		end
		else if EXISTS(	select 1 
						from Common.TRM trm WITH(NOLOCK)
						JOIN (	SELECT temp.DocumentDate, temp.EntityId, temp.CurrencyId
								FROM @ListDeferredCausation temp
								WHERE temp.CurrencyId=@DocumentCurrencyId
								GROUP BY temp.DocumentDate, temp.EntityId,temp.CurrencyId ) temp
						ON trm.MeasurementDate = temp.DocumentDate AND trm.OfficialCurrencyId=temp.CurrencyId and trm.CurrencyId = @currencyId) begin
			
				insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse,EntityId)
				select @DocumentCurrencyId, @currencyId, ValueOfficialToCurrency, [Value], temp.EntityId
				from Common.TRM trm WITH(NOLOCK)
				JOIN (	SELECT temp.DocumentDate, temp.EntityId, temp.CurrencyId
						FROM @ListDeferredCausation temp
						WHERE temp.CurrencyId=@DocumentCurrencyId
						GROUP BY temp.DocumentDate, temp.EntityId,temp.CurrencyId ) temp
				ON trm.MeasurementDate = temp.DocumentDate AND trm.OfficialCurrencyId=temp.CurrencyId and trm.CurrencyId = @currencyId		
		end
		else begin
			CLOSE currency_cursor1;  
				DEALLOCATE currency_cursor1;  
				
				INSERT INTO @tempTable
				SELECT 999 AS MessageCode, 
				'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @currencyId)  +') para la fecha actual ' MessageVoucher,
				0 JournalVoucherId
				
				SET @XmlOutPut = CONVERT(xml, 
								(
									SELECT * FROM @tempTable TableResult 
									For xml AUTO,TYPE, ELEMENTS
								))
				RETURN
		end

		if (select count(*) 
			from Payments.DeferredCausation dc WITH(NOLOCK)
			join @ListDeferredCausation temp on dc.Id = temp.Id and temp.CurrencyId = @DocumentCurrencyId
			left join Payments.DeferredCausationExchangeRate dcer with(NOLOCK) on dcer.DeferredCausationId = dc.Id and dcer.CurrencyId = @currencyId
			where dcer.Id is null) > 0 begin 

			INSERT INTO @tempTable
			select 999 AS MessageCode, 
			'No se encontro un TRM definido para el/los diferido(s) (' + (	select STRING_AGG( ap.Code,',')
																			from Payments.DeferredCausation dc WITH(NOLOCK)
																			join @ListDeferredCausation temp on dc.Id = temp.Id and temp.CurrencyId = @DocumentCurrencyId
																			join Payments.AccountPayable ap WITH(NOLOCK) on dc.IdAccountPayable =ap.Id
																			left join Payments.DeferredCausationExchangeRate dcer with(NOLOCK) on dcer.DeferredCausationId = dc.Id and dcer.CurrencyId = @currencyId
																			where dcer.Id is null)  +')'  MessageVoucher,	0 JournalVoucherId
			
			SET @XmlOutPut = CONVERT(xml, 
								(
									SELECT * FROM @tempTable TableResult 
									For xml AUTO,TYPE, ELEMENTS
								))
			CLOSE currency_cursor1;  
			DEALLOCATE currency_cursor1;  
			RETURN
		END

		UPDATE temp SET
				temp.ValueTRMConverted = [Portfolio].fnConvertValueBasedOnExchangeRate(temp.ValueAdjustment, acr.[Value] , acr.ValueReverse),
				temp.ValueTRMDocument = [Portfolio].fnConvertValueBasedOnExchangeRate(temp.ValueAdjustment, dcer.[Value] , dcer.ValueReverse) 
		FROM @ListDeferredCausation temp
		join @actualCurrencyRate acr on acr.CurrencyFrom = temp.CurrencyId and acr.CurrencyTo = @currencyId AND acr.EntityId = temp.EntityId
		join Payments.DeferredCausationExchangeRate dcer WITH(NOLOCK) on dcer.DeferredCausationId = temp.Id AND dcer.CurrencyId = @currencyId
		where temp.CurrencyId = @DocumentCurrencyId

		UPDATE temp SET
			   temp.ProfitLostValue = (temp.ValueTRMConverted - temp.ValueTRMDocument ) * IIF(temp.Nature=2,1,-1)
		FROM @ListDeferredCausation temp
		where temp.CurrencyId = @DocumentCurrencyId

		IF NOT EXISTS(	SELECT 1 
						from @ListDeferredCausation temp
						where abs(temp.ProfitLostValue) >0 AND temp.CurrencyId = @DocumentCurrencyId) BEGIN
			GOTO NEXT_ROW
		END

		DECLARE @MappingTable as TABLE( TempJvId  INT,
										EntityId INT,
										EntityName varchar(220))

		IF EXISTS(SELECT 1 FROM @MappingTable) BEGIN
			DELETE FROM @MappingTable
		END

		DELETE FROM  @JournalVourcherTmp
		DELETE FROM @JournalVourcherDetailTmp

		MERGE INTO @JournalVourcherTmp AS target
		USING (SELECT temp.EntityName, temp.EntityId,'Diferidos' as [Description],temp.DocumentDate
				FROM @ListDeferredCausation temp
				WHERE temp.CurrencyId = @DocumentCurrencyId
				GROUP by temp.EntityName, temp.EntityId,temp.DocumentDate) AS source ON 1=0
		WHEN NOT MATCHED THEN
				INSERT(	LegalBookId,
						IdJournalVoucher,
						VoucherDate,
						Status,
						Detail,
						EntityCode,
						EntityId,
						EntityName,
						CurrencyId)

				VALUES(	@bookId, 
						@ProfitLostJournalVoucherTypeId, 
						source.DocumentDate, 
						2, 
						CONCAT('Ajuste por diferencia en cambio amortización de diferidos, ', source.[Description],' ',cast(common.GETDATE() as VARCHAR)),  
						'', 
						0,
						'JournalVouchers',
						@currencyId)
			OUTPUT
				INSERTED.IdTemp,
				source.EntityId,
				source.EntityName
				into @MappingTable (TempJvId,EntityId,EntityName);

		IF @OfficialBookId = @bookId BEGIN --Si es el mismo libro entonces no hacemos homologacion 

			insert into @JournalVourcherDetailTmp 
			(	IdTempHeader,
				IdMainAccount, 
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
				mt.TempJvId,
				ma.Id, 
				iif(ma.HandlesThirdParty = 1, dc.IdThirdParty, null),
				iif(ma.HandlesCostCenter = 1, dc.IdCostCenter, null),
				iif(temp.ProfitLostValue > 0, ABS(temp.ProfitLostValue), 0),
				iif(temp.ProfitLostValue > 0, 0, ABS(temp.ProfitLostValue)),
				Concat('CxP No : ',ap.Code), null, null, null, null 
			from Payments.DeferredCausation dc WITH(NOLOCK)
			join @ListDeferredCausation temp on dc.Id = temp.Id
			INNER JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = dc.IdMainAccount
			INNER JOIN Payments.AccountPayable ap WITH(NOLOCK) ON ap.Id = dc.IdAccountPayable
			JOIN @MappingTable mt on temp.EntityName= mt.EntityName and temp.EntityId=mt.EntityId
			where temp.CurrencyId = @DocumentCurrencyId

			insert into @JournalVourcherDetailTmp 
			( IdTempHeader, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)			
			select	
					temp.TempJvId
					,ma.Id
					,NULL
					,IIF(ma.HandlesCostCenter = 1,@CostCenterCsId,NULL)
					,IIF(temp.ProfitLostValue > 0, 0, ABS(temp.ProfitLostValue))
					,IIF(temp.ProfitLostValue > 0, ABS(temp.ProfitLostValue), 0)
					, ''
					, null
					, null
					, null
					, null
			FROM
				(	SELECT mt.TempJvId, SUM(temp.ProfitLostValue) ProfitLostValue, temp.Nature
					FROM @ListDeferredCausation temp
					JOIN @MappingTable mt on temp.EntityName= mt.EntityName and temp.EntityId=mt.EntityId
					where temp.CurrencyId = @DocumentCurrencyId
					GROUP by mt.TempJvId,temp.Nature
					) temp
			JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK)  
			ON ma.Id =iif(temp.ProfitLostValue > 0 ,@ProfitByExchangeCurrencyAccountId,@LostByExchangeCurrencyAccountId)	
		END
		ELSE BEGIN -- homologamos

			insert into @JournalVourcherDetailTmp 
				(	IdTempHeader,
					IdMainAccount, 
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
					mt.TempJvId,
					ma.Id, 
					iif(ma.HandlesThirdParty = 1, dc.IdThirdParty, null),
					iif(ma.HandlesCostCenter = 1, dc.IdCostCenter, null),
					iif(temp.ProfitLostValue > 0, ABS(temp.ProfitLostValue), 0),
					iif(temp.ProfitLostValue > 0, 0, abs(temp.ProfitLostValue)),
					Concat('Anticipo No : ',ap.Code), null, null, null, null 
				from Payments.DeferredCausation dc WITH(NOLOCK)
				INNER JOIN @ListDeferredCausation temp on dc.Id = temp.Id
				INNER JOIN Payments.AccountPayable ap WITH(NOLOCK) on dc.IdAccountPayable = ap.Id
				INNER JOIN GeneralLedger.HomologationAccount ha WITH(NOLOCK) ON ha.OfficialMainAccountId = dc.IdMainAccount
				INNER JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ha.MainAccountId and ma.LegalBookId = @bookId
				INNER JOIN @MappingTable mt on temp.EntityName= mt.EntityName and temp.EntityId=mt.EntityId
				where temp.CurrencyId = @DocumentCurrencyId
				
				insert into @JournalVourcherDetailTmp 
				( IdTempHeader, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue)			
				select	
						temp.TempJvId
						,ma.Id
						,NULL
						,IIF(ma.HandlesCostCenter = 1,@CostCenterCsId,NULL)
						,IIF(temp.ProfitLostValue > 0, 0, ABS(temp.ProfitLostValue))
						,IIF(temp.ProfitLostValue > 0, abs(temp.ProfitLostValue), 0)
						, ''
						, null
						, null
						, null
						, null
				FROM
					(	SELECT mt.TempJvId, SUM(temp.ProfitLostValue) ProfitLostValue, temp.Nature
						FROM @ListDeferredCausation temp
						JOIN @MappingTable mt on temp.EntityName= mt.EntityName and temp.EntityId=mt.EntityId
						where temp.CurrencyId = @DocumentCurrencyId
						GROUP by mt.TempJvId,temp.Nature
						) temp
				JOIN GeneralLedger.MainAccounts maOf WITH(NOLOCK)
					ON maOf.Id =iif(temp.ProfitLostValue > 0 ,@ProfitByExchangeCurrencyAccountId,@LostByExchangeCurrencyAccountId)
				JOIN GeneralLedger.HomologationAccount ha WITH(NOLOCK) ON ha.OfficialMainAccountId= maOf.Id
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id=ha.MainAccountId and ma.LegalBookId = @bookId 
			
		end

		--Eliminamos cuentas en 0
		delete from @JournalVourcherDetailTmp where DebitValue = 0 and CreditValue = 0

		--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
		SELECT @JournalVoucherXML = CONVERT(xml, 
			(
				SELECT * 
				FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail
					ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
					AND JournalVoucher.IdTemp= JournalVoucherDetail.IdTempHeader
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		insert into @tempTable(Code,MessageOutput,JournalVoucherId)
		exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @UserCode

		NEXT_ROW:
		FETCH NEXT FROM currency_cursor1 
		INTO @bookId, @currencyId, @DocumentCurrencyId

	END   
	CLOSE currency_cursor1;  
	DEALLOCATE currency_cursor1;

	SET @XmlOutPut = CONVERT(xml, 
								(
									SELECT * FROM @tempTable TableResult 
									For xml AUTO,TYPE, ELEMENTS
								))

	RETURN
END TRY
BEGIN CATCH
	IF CURSOR_STATUS('global','currency_cursor1') >= -1  BEGIN
		  IF CURSOR_STATUS('global','currency_cursor1') > -1 BEGIN
			CLOSE currency_cursor1
		  END
		 DEALLOCATE currency_cursor1
		END
		select 999 MessageCode, ERROR_MESSAGE() as MessageVoucher, 0 as JournalVoucherId
END CATCH
END