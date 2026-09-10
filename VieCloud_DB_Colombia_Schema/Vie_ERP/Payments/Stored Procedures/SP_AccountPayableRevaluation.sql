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
			@DocumentDate DATE, -- Fecha del Documento que genera el ajuste diferencial
			@CurrentEntityName VARCHAR(250), -- >>> FIX CIMA #52141: EntityName real del documento origen
			@CurrentEntityId INT              -- >>> FIX CIMA #52141: EntityId real del documento origen

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

		-- >>> FIX CIMA #52141: se recupera el EntityName/EntityId real del documento que originó el ajuste
		SELECT TOP 1 
			@CurrentEntityName = temp.EntityName,
			@CurrentEntityId = temp.EntityId
		FROM @ListAccountPayable temp
		WHERE temp.CurrencyId = @DocumentCurrencyId

		INSERT INTO @JournalVourcherTmp
		(LegalBookId, IdJournalVoucher, VoucherDate, Status, Detail, EntityCode, EntityId, EntityName, CurrencyId)
		VALUES
		(
			@bookId, 
			@ProfitLostJournalVoucherTypeId, 
			@DocumentDate, 
			2, 
			CONCAT('Ajuste por diferencia en cambio CxP, ', @EntityNameDescription ,'( ',ISNULL(@DocumentCode,''),' ) ',cast(common.GETDATE() as VARCHAR)),  
			ISNULL(@DocumentCode, ''),              -- >>> FIX CIMA #52141: antes ''
			ISNULL(@CurrentEntityId, 0),             -- >>> FIX CIMA #52141: antes 0
			ISNULL(@CurrentEntityName, 'JournalVouchers'), -- >>> FIX CIMA #52141: antes 'JournalVouchers'
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de revalorización de cuentas por pagar en moneda extranjera a la TRM del día. Recibe una lista de cuentas por pagar (en formato XML) que pueden estar asociadas a comprobantes de tesorería (VoucherTransaction), notas de pago (PaymentNotes), traslados de pago (PaymentTransfer) o cruzamientos de cuenta (CrossingAccount), calcula la diferencia entre el valor original convertido a la tasa de cambio del documento y el valor a la tasa de cambio actual, y genera automáticamente un comprobante contable de ajuste por diferencia en cambio, registrando la utilidad o pérdida cambiaria en las cuentas contables configuradas para ese fin.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_AccountPayableRevaluation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_AccountPayableRevaluation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Revaloriza cuentas por pagar a la fecha del documento generando comprobantes contables de ajuste por diferencia en cambio (utilidad/pérdida) según TRM vigente.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener al menos un AccountPayable con Id, ValuePaid, EntityName y EntityId.; Cada CxP referenciada debe tener una moneda (CurrencyId) asignada distinta de NULL y de 0.; La entidad origen del ajuste (VoucherTransaction, PaymentNotes, PaymentTransfer o CrossingAccount) debe existir y aportar la fecha del documento.; Debe existir TRM en Common.TRM para la fecha del documento entre la moneda del documento y la moneda oficial del libro.; Debe existir registro en Payments.AccountPayableExchangeRate para cada CxP en la moneda oficial del libro destino.; Debe existir configuración en GeneralLedger.CompanySettings con cuentas de utilidad/pérdida por diferencia en cambio, tipo de comprobante y centro de costo.; Debe existir un libro oficial (OfficialBook=1) en GeneralLedger.LegalBook.; Si el libro destino no es el oficial, debe existir homologación en GeneralLedger.HomologationAccount para las cuentas involucradas.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @tempTable (resultado XML de salida): Cuando el XML de entrada no contiene CxP, retorna código ''0'' con mensaje ''No hay cxp para revalorizar'' y JournalVoucherId=0.; [RETURN_RESULT] @tempTable (resultado XML de salida): Cuando @DocumentDate es NULL (no se halla fecha del documento origen), retorna código ''999'' con mensaje ''No se encontró la fecha del documento''.; [RETURN_RESULT] @tempTable (resultado XML de salida): Cuando alguna CxP tiene CurrencyId NULL o 0, retorna código 999 listando las facturas sin moneda especificada.; [RETURN_RESULT] @tempTable (resultado XML de salida): Cuando no existe TRM en Common.TRM para la fecha y monedas requeridas, cierra el cursor y retorna código 999 con mensaje ''no se encontro TRM (...) para la fecha actual''.; [RETURN_RESULT] @tempTable (resultado XML de salida): Cuando alguna CxP no tiene registro en Payments.AccountPayableExchangeRate para la moneda oficial del libro, retorna código 999 listando las facturas sin TRM definido.; [INSERT] GeneralLedger (vía SP_CreateAndValidateJournalVoucherMovement): Cuando existe diferencia (|ProfitLostValue|>0) para alguna CxP en la moneda procesada, se construye XML de comprobante contable y se invoca el SP que crea y valida el movimiento contable.; [INSERT] @JournalVourcherDetailTmp: Cuando ProfitLostValue<0 se registra como Débito (ABS) y CreditValue=0; cuando ProfitLostValue>=0 se registra como Crédito y DebitValue=0, en la cuenta principal de la CxP.; [INSERT] @JournalVourcherDetailTmp: Cuando @TotalProfitLostValue<0 se usa la cuenta ProfitByExchangeCurrencyAccountId (utilidad); en caso contrario LostByExchangeCurrencyAccountId (pérdida) como contrapartida.; [INSERT] @JournalVourcherDetailTmp: Cuando el libro destino no es el oficial, las cuentas se reemplazan por sus homólogas en GeneralLedger.HomologationAccount filtrando por LegalBookId=@bookId.; [UPDATE] @ListAccountPayable: Asigna a cada CxP su CurrencyId tomado de Payments.AccountPayable; calcula ValuePaidTRMConverted (con tasa actual) y ValuePaidTRMDocument (con tasa del documento) usando Portfolio.fnConvertValueBasedOnExchangeRate; calcula ProfitLostValue=(Convertido-Documento)*signo según naturaleza.; [RETURN_RESULT] @tempTable (resultado XML de salida): En caso de error en TRY/CATCH retorna MessageCode=999 con ERROR_MESSAGE() y JournalVoucherId=0.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation';
-- GO
