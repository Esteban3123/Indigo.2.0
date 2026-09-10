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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la revalorización (ajuste por diferencia en cambio) de causaciones diferidas de cuentas por pagar a la TRM vigente a la fecha indicada. Recibe una lista de causaciones diferidas en formato XML, obtiene la moneda de cada cuenta por pagar asociada, calcula la diferencia entre el valor convertido a la tasa actual y el valor original del documento, y genera los comprobantes contables de ajuste por ganancia o pérdida en cambio de moneda extranjera. Valida que todas las causaciones tengan moneda y fecha de ajuste antes de proceder; en caso de error, retorna un XML con el detalle de las facturas (cuentas por pagar) que presentan inconsistencias. Utiliza los parámetros contables de la empresa (cuentas de pérdida/ganancia en cambio, tipo de comprobante y centro de costos) configurados en GeneralLedger.CompanySettings, y trabaja sobre el libro oficial registrado en GeneralLedger.LegalBook.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_DeferredCausationRevaluation_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_DeferredCausationRevaluation_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Revaloriza causaciones diferidas de cuentas por pagar en moneda extranjera a la TRM de una fecha dada y genera los comprobantes contables de ajuste por diferencia en cambio (ganancia/pérdida) por cada libro legal afectado.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodos Data/DeferredCausationRevaluation con Id, ValueAdjustment, EntityName, EntityId y DocumentDate; Cada Id debe existir en Payments.DeferredCausation y su AccountPayable asociado debe tener CurrencyId definido y >0; Cada DeferredCausation debe tener una tasa registrada en Payments.DeferredCausationExchangeRate para la moneda oficial del libro a procesar; Debe existir TRM en Common.TRM para la fecha del documento entre la moneda del documento y la moneda oficial del libro; GeneralLedger.CompanySettings debe tener configuradas las cuentas ProfitLostByExchangeCurrencyAccountId, LostByExchangeCurrencyAccountId, el tipo de comprobante ProfitLostJournalVoucherTypeId y el CostCenterId; Debe existir al menos un LegalBook con OfficialBook=1 y libros activos (Status=1); Para libros distintos al oficial debe existir homologación en GeneralLedger.HomologationAccount para las cuentas involucradas', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan registros cuya CurrencyId difiere de la moneda oficial del libro (el cursor recorre libros con OfficialCurrencyId<>CurrencyId del documento); Solo se consideran libros con Status=1 en LegalBook; El comprobante contable se crea con Status=2 (preliminar/borrador) y EntityName fijo ''JournalVouchers''; El detalle del comprobante incluye en su descripción el código de la CxP (prefijado ''CxP No :'' en libro oficial o ''Anticipo No :'' al homologar); Las líneas con DebitValue=0 y CreditValue=0 se eliminan antes de armar el XML; El signo del ajuste depende de la naturaleza: ProfitLostValue = (ValueTRMConverted - ValueTRMDocument) * (1 si Nature=2, -1 en otro caso); Si no hay diferencia de cambio significativa (|ProfitLostValue|=0) no se genera comprobante para esa moneda; Cuando se homologa, la cuenta destino debe pertenecer al libro en proceso (ma.LegalBookId = @bookId); Ante cualquier excepción se cierra/desaloja el cursor global y se retorna un mensaje con código 999 y JournalVoucherId=0', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Causación diferida; Cuenta por pagar (CxP); Reexpresión / ajuste por diferencia en cambio; TRM (Tasa Representativa del Mercado); Comprobante contable (Journal Voucher); Libro oficial / libro legal; Homologación de cuentas contables; Cuenta de ganancia y pérdida por diferencia en cambio; Centro de costos; Tercero; Naturaleza débito/crédito; Anticipo', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe alguna causación diferida cuyo CurrencyId es NULL o 0 tras enlazar con AccountPayable → Devuelve XML con código 999 indicando que las CxP no tienen moneda especificada y termina sin generar comprobante else Continúa la validación de fecha de documento; si Existe alguna causación diferida sin DocumentDate → Devuelve XML con código 999 indicando que no hay fecha para ejecutar el ajuste diferencial y termina else Continúa con la obtención de parámetros de empresa; si No se encuentra TRM para la moneda y fecha del documento (ni directa ni inversa) en Common.TRM → Cierra/desaloja el cursor, retorna XML con código 999 ''no se encontró TRM'' y termina else Inserta la tasa en @actualCurrencyRate (directa o invirtiendo Value/ValueOfficialToCurrency); si Alguna causación diferida no tiene registro en Payments.DeferredCausationExchangeRate para la moneda del libro → Devuelve XML con código 999 ''No se encontró TRM definido para el/los diferido(s)'' con los códigos de CxP afectadas y termina else Procede al cálculo de valores reexpresados; si Para la moneda actual, ningún registro tiene |ProfitLostValue|>0 → Salta (GOTO NEXT_ROW) sin generar comprobante para esa moneda/libro else Genera la cabecera y los detalles del comprobante contable; si El libro a procesar es el libro oficial (@OfficialBookId = @bookId) → Inserta detalle usando directamente la cuenta dc.IdMainAccount y las cuentas de ganancia/pérdida en cambio sin homologación else Homologa cuentas usando GeneralLedger.HomologationAccount al libro destino antes de insertar el detalle; si ProfitLostValue > 0 → Registra el valor como Débito en la cuenta del diferido y como Crédito en la cuenta de ganancia por diferencia en cambio (ProfitByExchangeCurrencyAccountId) else Registra el valor como Crédito en la cuenta del diferido y como Débito en la cuenta de pérdida por diferencia en cambio (LostByExchangeCurrencyAccountId); si La cuenta principal tiene HandlesThirdParty=1 / HandlesCostCenter=1 → Asigna respectivamente IdThirdParty / IdCostCenter del diferido al detalle; en la contrapartida de ganancia/pérdida se usa @CostCenterCsId si HandlesCostCenter=1 else Deja NULL en esos campos', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.DeferredCausation; Payments.AccountPayable; GeneralLedger.CompanySettings; GeneralLedger.LegalBook; Common.TRM; Common.Currency; Payments.DeferredCausationExchangeRate; GeneralLedger.MainAccounts; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation_Output';
-- GO
