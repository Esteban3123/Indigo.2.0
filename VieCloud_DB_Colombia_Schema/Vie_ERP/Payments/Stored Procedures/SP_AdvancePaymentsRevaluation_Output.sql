-- =============================================
-- Author:      Giovanny Plazas Lozano
-- Create Date: 02/07/2024
-- Description: Procedimiento que se usa para revalorizar un documento a la fecha actual (TRM)
-- =============================================

/* @ListAdvancePayments =
'<AdvancePaymentsRevaluation>
<Id></Id>
<ValueAdjustment></ValueAdjustment>
<EntityName></EntityName>
<EntityId></EntityId>
<DocumentDate></DocumentDate>
</AdvancePaymentsRevaluation>'*/
CREATE PROCEDURE [Payments].[SP_AdvancePaymentsRevaluation_Output]
(
    @ListAdvancePaymentsXml Xml,
	@UserCode VARCHAR(25),
	@XmlOutPut xml OUTPUT
)
AS
BEGIN

	Declare @ListAdvancePayments Table	
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
	INSERT INTO @ListAdvancePayments (Id,ValueAdjustment,EntityName,EntityId,DocumentDate)
	SELECT
	t.x.value('Id[1]', 'int')  Id,
	t.x.value('ValueAdjustment[1]', 'NUMERIC(20,2)')  ValuePaid,
	t.x.value('EntityName[1]', 'VARCHAR(250)')  EntityName,
	t.x.value('EntityId[1]', 'INT')  EntityId,
	t.x.value('DocumentDate[1]', 'DATE')  DocumentDate
	from @ListAdvancePaymentsXml.nodes('/AdvancePaymentsRevaluation') t(x);

	-- actualizamos la tabla variable con la moneda correspondiente de cada cuenta
	UPDATE temp set temp.CurrencyId = ap.CurrencyId
	from @ListAdvancePayments temp
	join Payments.AdvancePayments ap with(NOLOCK) on temp.Id = ap.Id

	UPDATE temp set EntityCode = pt.Code, Nature=2
	from @ListAdvancePayments temp
	JOIN Payments.PaymentTransfer pt with(NOLOCK) on temp.EntityId=pt.Id and temp.EntityName='PaymentTransfer'

	UPDATE temp set EntityCode = pn.Code, Nature=pn.Nature
	from @ListAdvancePayments temp
	JOIN Payments.PaymentNotes pn with(NOLOCK) on temp.EntityId=pn.Id and temp.EntityName='PaymentNotes'

	--se validan que todas las facturas tengan moneda
	if EXISTS(SELECT 1 from @ListAdvancePayments where CurrencyId is null or CurrencyId =0) BEGIN
		INSERT INTO @tempTable
		SELECT 999 AS MessageCode, 
				concat('Los anticipos de cartera : ','( ',STRING_AGG(ap.Code,','),' ) ','No tienen moneda especificada') MessageVoucher,
				0 JournalVocuherId
		from @ListAdvancePayments temp
		join Payments.AdvancePayments ap with(NOLOCK) on temp.Id = ap.Id
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
	from GeneralLedger.CompanySettings	with(NOLOCK)

	--libro oficial
	SET @OfficialBookId = (SELECT TOP 1 lb.Id FROM GeneralLedger.LegalBook lb with(NOLOCK) WHERE lb.OfficialBook = 1)

	DECLARE currency_cursor1 CURSOR FOR   
		select Id, OfficialCurrencyId,temp.CurrencyId
		from GeneralLedger.LegalBook l WITH(NOLOCK)
		join (	SELECT a.CurrencyId
				from @ListAdvancePayments a
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
							FROM @ListAdvancePayments temp
							WHERE temp.CurrencyId=@DocumentCurrencyId
							GROUP BY temp.DocumentDate, temp.EntityId,temp.CurrencyId ) temp 
					ON trm.MeasurementDate = temp.DocumentDate AND trm.CurrencyId = temp.CurrencyId AND trm.OfficialCurrencyId = @currencyId) begin
			
				insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse,EntityId)
				select @DocumentCurrencyId, @currencyId, [Value], ValueOfficialToCurrency,temp.EntityId
				from Common.TRM trm WITH(NOLOCK)
				JOIN (	SELECT temp.DocumentDate, temp.EntityId, temp.CurrencyId
						FROM @ListAdvancePayments temp
						WHERE temp.CurrencyId=@DocumentCurrencyId
						GROUP BY temp.DocumentDate, temp.EntityId,temp.CurrencyId ) temp 
				ON trm.MeasurementDate =temp.DocumentDate AND trm.CurrencyId = temp.CurrencyId AND trm.OfficialCurrencyId = @currencyId
			
		end
		else if EXISTS(	select 1 
						from Common.TRM trm WITH(NOLOCK)
						JOIN (	SELECT temp.DocumentDate, temp.EntityId, temp.CurrencyId
								FROM @ListAdvancePayments temp
								WHERE temp.CurrencyId=@DocumentCurrencyId
								GROUP BY temp.DocumentDate, temp.EntityId,temp.CurrencyId ) temp
						ON trm.MeasurementDate = temp.DocumentDate AND trm.OfficialCurrencyId=temp.CurrencyId and trm.CurrencyId = @currencyId) begin
			
				insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse,EntityId)
				select @DocumentCurrencyId, @currencyId, ValueOfficialToCurrency, [Value], temp.EntityId
				from Common.TRM trm WITH(NOLOCK)
				JOIN (	SELECT temp.DocumentDate, temp.EntityId, temp.CurrencyId
						FROM @ListAdvancePayments temp
						WHERE temp.CurrencyId=@DocumentCurrencyId
						GROUP BY temp.DocumentDate, temp.EntityId,temp.CurrencyId ) temp
				ON trm.MeasurementDate =temp.DocumentDate AND trm.OfficialCurrencyId=temp.CurrencyId and trm.CurrencyId = @currencyId		
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
			from Payments.AdvancePayments ap WITH(NOLOCK)
			join @ListAdvancePayments temp on ap.Id = temp.Id and temp.CurrencyId = @DocumentCurrencyId
			left join Payments.AdvancePaymentsExchangeRate er with(NOLOCK) on er.AdvancePaymentsId = ap.Id and er.CurrencyId = @currencyId
			where er.Id is null) > 0 begin 

			INSERT INTO @tempTable
			select 999 AS MessageCode, 
			'No se encontro un TRM definido para el/los Anticipo(s) (' + (	select STRING_AGG( ap.Code,',')
																			from Payments.AdvancePayments ap WITH(NOLOCK)
																			join @ListAdvancePayments temp on ap.Id = temp.Id and temp.CurrencyId = @DocumentCurrencyId
																			where temp.CurrencyId =@DocumentCurrencyId)  +')'  MessageVoucher,	0 JournalVoucherId
			
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
				temp.ValueTRMDocument = [Portfolio].fnConvertValueBasedOnExchangeRate(temp.ValueAdjustment, paer.[Value] , paer.ValueReverse) 
		FROM @ListAdvancePayments temp
		join @actualCurrencyRate acr on acr.CurrencyFrom = temp.CurrencyId and acr.CurrencyTo = @currencyId AND acr.EntityId = temp.EntityId
		join Payments.AdvancePaymentsExchangeRate paer WITH(NOLOCK) on paer.AdvancePaymentsId = temp.Id AND paer.CurrencyId = @currencyId
		where temp.CurrencyId = @DocumentCurrencyId

		UPDATE temp SET
				temp.ProfitLostValue = (temp.ValueTRMConverted - temp.ValueTRMDocument ) * IIF(temp.Nature=2,1,-1)
		FROM @ListAdvancePayments temp
		where temp.CurrencyId = @DocumentCurrencyId

		IF NOT EXISTS(	SELECT 1 
						from @ListAdvancePayments temp
						where abs(temp.ProfitLostValue) >0 AND temp.CurrencyId = @DocumentCurrencyId) BEGIN
			GOTO NEXT_ROW
		END

		DECLARE @MappingTable as TABLE(TempJvId  INT,
										EntityId INT,
										EntityName varchar(220))

		IF EXISTS(SELECT 1 FROM @MappingTable) BEGIN
			DELETE FROM @MappingTable
		END
		DELETE FROM  @JournalVourcherTmp
		DELETE FROM @JournalVourcherDetailTmp

		MERGE INTO @JournalVourcherTmp AS target
		USING (SELECT temp.EntityName, temp.EntityId,vend.[Description],temp.EntityCode,temp.DocumentDate
				FROM @ListAdvancePayments temp
				JOIN GeneralLedger.ViewEntityNameDescriptions vend with(NOLOCK) on temp.EntityName =vend.EntityName
				WHERE temp.CurrencyId = @DocumentCurrencyId
				GROUP by temp.EntityName, temp.EntityId,vend.[Description],temp.EntityCode,temp.DocumentDate) AS source ON 1=0
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
						CONCAT('Ajuste por diferencia en cambio en anticipos de pagos, ', source.[Description],'( ',ISNULL(source.EntityCode,''),' ) ',cast(common.GETDATE() as VARCHAR)),  
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
				iif(ma.HandlesThirdParty = 1, ap.IdThirdParty, null),
				iif(ma.HandlesCostCenter = 1, ap.IdCostCenter, null),
				iif(temp.ProfitLostValue > 0, ABS(temp.ProfitLostValue), 0),
				iif(temp.ProfitLostValue > 0, 0, ABS(temp.ProfitLostValue)),
				Concat('Anticipo No : ',ap.Code), null, null, null, null 
			from Payments.AdvancePayments ap WITH(NOLOCK)
			join @ListAdvancePayments temp on ap.Id = temp.Id
			INNER JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = ap.IdAccount
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
					FROM @ListAdvancePayments temp
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
					iif(ma.HandlesThirdParty = 1, ap.IdThirdParty, null),
					iif(ma.HandlesCostCenter = 1, ap.IdCostCenter, null),
					iif(temp.ProfitLostValue > 0, ABS(temp.ProfitLostValue), 0),
					iif(temp.ProfitLostValue > 0, 0, abs(temp.ProfitLostValue)),
					Concat('Anticipo No : ',ap.Code), null, null, null, null 
				from Payments.AdvancePayments ap WITH(NOLOCK)
				INNER JOIN @ListAdvancePayments temp on ap.Id = temp.Id
				INNER JOIN GeneralLedger.HomologationAccount ha WITH(NOLOCK) ON ha.OfficialMainAccountId = ap.IdAccount
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
						FROM @ListAdvancePayments temp
						JOIN @MappingTable mt on temp.EntityName= mt.EntityName and temp.EntityId=mt.EntityId
						where temp.CurrencyId = @DocumentCurrencyId
						GROUP by mt.TempJvId,temp.Nature
						) temp
				JOIN GeneralLedger.MainAccounts maOf WITH(NOLOCK)
					ON maOf.Id =iif(temp.ProfitLostValue > 0 ,@ProfitByExchangeCurrencyAccountId,@LostByExchangeCurrencyAccountId)
				JOIN GeneralLedger.HomologationAccount ha WITH(NOLOCK) ON ha.OfficialMainAccountId= maOf.Id
				JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id=ha.MainAccountId and ma.LegalBookId = @bookId 
			
		end

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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la revalorización (ajuste por diferencia en cambio) de anticipos o pagos adelantados a la fecha actual, aplicando la TRM (tasa representativa del mercado) vigente. Recibe una lista de anticipos en formato XML, consulta la moneda de cada anticipo en la tabla de anticipos (AdvancePayments), y obtiene el código del documento fuente desde traslados de pago (PaymentTransfer) o notas de pago (PaymentNotes). Calcula la ganancia o pérdida por diferencial cambiario usando las cuentas contables configuradas en los parámetros de empresa (CompanySettings), y genera comprobantes contables de ajuste en el libro oficial. Retorna el resultado del proceso en un XML de salida, incluyendo mensajes de error si algún anticipo no tiene moneda especificada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_AdvancePaymentsRevaluation_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_AdvancePaymentsRevaluation_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Revaloriza anticipos de pago a la TRM de la fecha actual generando un comprobante contable por la diferencia en cambio (utilidad/pérdida) por cada libro legal y moneda involucrada.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AdvancePaymentsRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener Id, ValueAdjustment, EntityName, EntityId y DocumentDate por cada anticipo a revalorizar.; Cada anticipo referenciado debe existir en Payments.AdvancePayments con CurrencyId definido y distinto de cero.; La entidad asociada (PaymentTransfer o PaymentNotes) debe existir según EntityName para resolver EntityCode y Nature.; Debe existir configuración en GeneralLedger.CompanySettings con cuentas de utilidad/pérdida por diferencia en cambio, tipo de comprobante y centro de costo.; Debe existir un libro oficial (OfficialBook=1) y libros legales activos (Status=1).; Debe existir TRM en Common.TRM para la fecha del documento que relacione la moneda del anticipo con la moneda oficial del libro.; Cada anticipo debe tener registro en Payments.AdvancePaymentsExchangeRate para la moneda oficial del libro destino.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AdvancePaymentsRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @tempTable: Si algún anticipo tiene CurrencyId nulo o cero, retorna XML con código 999 y mensaje ''Los anticipos de cartera (...) No tienen moneda especificada'' y termina.; [RETURN_RESULT] @tempTable: Si no existe TRM para la fecha actual entre la moneda del documento y la moneda oficial del libro, retorna XML con código 999 y mensaje ''no se encontro TRM (moneda) para la fecha actual'' y termina.; [RETURN_RESULT] @tempTable: Si algún anticipo no tiene registro en Payments.AdvancePaymentsExchangeRate para la moneda oficial del libro, retorna XML con código 999 y mensaje ''No se encontro un TRM definido para el/los Anticipo(s)'' y termina.; [INSERT] GeneralLedger.JournalVoucher: Cuando existe diferencia de revalorización (|ProfitLostValue|>0) por libro y moneda, se construye XML de comprobante y se invoca GeneralLedger.SP_CreateAndValidateJournalVoucherMovement para crearlo, usando como tipo de comprobante el ProfitLostJournalVoucherTypeId de CompanySettings.; [INSERT] @JournalVourcherDetailTmp: Por cada anticipo se inserta un detalle contra la cuenta contable del anticipo (ap.IdAccount o su homologada al libro destino): Débito = ProfitLostValue si es positivo, Crédito si es negativo (en valor absoluto).; [INSERT] @JournalVourcherDetailTmp: Como contrapartida se inserta detalle contra ProfitByExchangeCurrencyAccountId si ProfitLostValue>0 o LostByExchangeCurrencyAccountId si ≤0, asignando @CostCenterCsId cuando la cuenta maneja centro de costo.; [RETURN_RESULT] @tempTable: En caso de excepción, devuelve fila con MessageCode=999, ERROR_MESSAGE() y JournalVoucherId=0.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AdvancePaymentsRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityName=''PaymentTransfer'' → Asigna EntityCode desde Payments.PaymentTransfer y fija Nature=2 (crédito).; si EntityName=''PaymentNotes'' → Asigna EntityCode y Nature desde Payments.PaymentNotes (1 débito / 2 crédito).; si Existe TRM con CurrencyId=moneda del documento y OfficialCurrencyId=moneda oficial del libro → Toma Value y ValueOfficialToCurrency directamente. else Si existe TRM con OfficialCurrencyId=moneda del documento y CurrencyId=moneda del libro, toma los valores invertidos (ValueOfficialToCurrency como Value y viceversa); si tampoco existe, retorna error de TRM no encontrada.; si @OfficialBookId = @bookId (libro oficial coincide con el libro procesado) → Usa directamente ap.IdAccount y las cuentas de utilidad/pérdida de CompanySettings sin homologar. else Homologa las cuentas vía GeneralLedger.HomologationAccount al MainAccount del libro destino (ma.LegalBookId=@bookId).; si Nature=2 (crédito) → ProfitLostValue = (ValueTRMConverted - ValueTRMDocument) * 1. else ProfitLostValue = (ValueTRMConverted - ValueTRMDocument) * -1 (naturaleza débito invierte el signo).; si NOT EXISTS abs(ProfitLostValue)>0 para la moneda procesada → Salta a NEXT_ROW sin generar comprobante para ese libro/moneda.; si ma.HandlesThirdParty=1 → Asigna IdThirdParty del anticipo al detalle; en caso contrario NULL.; si ma.HandlesCostCenter=1 → Asigna IdCostCenter del anticipo (o @CostCenterCsId en la contrapartida) al detalle; en caso contrario NULL.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AdvancePaymentsRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AdvancePaymentsRevaluation_Output';
-- GO
