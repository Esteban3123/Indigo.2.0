-- =============================================
-- Author:      Giovanny Plazas Lozano
-- Create Date: 19/06/2024
-- Description: Procedimiento que se usa para revalorizar un documento a la fecha actual (TRM)
-- =============================================

/* @ListPortfolioAdvance =
'<PortfolioAdvance>
<Id></Id>
<ValueAdjustment></ValueAdjustment>
<EntityName></EntityName>
<EntityId></EntityId>
<DocumentDate></DocumentDate>
</PortfolioAdvance>'*/
CREATE PROCEDURE [Portfolio].[SP_PortfolioAdvanceRevaluation_Output]
(
    @ListPortfolioAdvanceXml Xml,
	@UserCode VARCHAR(25),
	@XmlOutPut xml OUTPUT
)
AS
BEGIN

	Declare @ListPortfolioAdvance Table	
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
		Nature TINYINT DEFAULT(1), --Naturaleza  1 - Debito / 2 - Credito, la naturaleza del documento
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
	INSERT INTO @ListPortfolioAdvance (Id,ValueAdjustment,EntityName,EntityId,DocumentDate)
	SELECT
	t.x.value('Id[1]', 'int')  Id,
	t.x.value('ValueAdjustment[1]', 'NUMERIC(20,2)')  ValuePaid,
	t.x.value('EntityName[1]', 'VARCHAR(250)')  EntityName,
	t.x.value('EntityId[1]', 'INT')  EntityId,
	t.x.value('DocumentDate[1]', 'DATE')  DocumentDate
	from @ListPortfolioAdvanceXml.nodes('/PortfolioAdvance') t(x);

	-- actualizamos la tabla variable con la moneda correspondiente de cada cuenta
	UPDATE temp set temp.CurrencyId = pa.CurrencyId
	from @ListPortfolioAdvance temp
	join Portfolio.PortfolioAdvance pa with(NOLOCK) on temp.Id = pa.Id

	UPDATE temp set EntityCode = pt.Code, Nature=IIF(pt.Status = 4,2,1)
	from @ListPortfolioAdvance temp
	JOIN Portfolio.PortfolioTransfer pt with(NOLOCK) on temp.EntityId=pt.Id and temp.EntityName='PortfolioTransfer'

	UPDATE temp set EntityCode = pn.Code, Nature=pn.Nature
	from @ListPortfolioAdvance temp
	JOIN Portfolio.PortfolioNote pn with(NOLOCK) on temp.EntityId=pn.Id and temp.EntityName='PortfolioNote'

	--se validan que todas las facturas tengan moneda
	if EXISTS(SELECT 1 from @ListPortfolioAdvance where CurrencyId is null or CurrencyId =0) BEGIN
		INSERT INTO @tempTable
		SELECT 999 AS MessageCode, 
				concat('Los anticipos de cartera : ','( ',STRING_AGG(pa.Code,','),' ) ','No tienen moneda especificada') MessageVoucher,
				0 JournalVocuherId
		from @ListPortfolioAdvance temp
		join Portfolio.PortfolioAdvance pa with(NOLOCK) on temp.Id = pa.Id
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
				from @ListPortfolioAdvance a
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
							FROM @ListPortfolioAdvance temp
							WHERE temp.CurrencyId=@DocumentCurrencyId
							GROUP BY temp.DocumentDate, temp.EntityId,temp.CurrencyId ) temp 
					ON trm.MeasurementDate = temp.DocumentDate AND trm.CurrencyId = temp.CurrencyId AND trm.OfficialCurrencyId = @currencyId) begin
			
				insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse,EntityId)
				select @DocumentCurrencyId, @currencyId, [Value], ValueOfficialToCurrency,temp.EntityId
				from Common.TRM trm WITH(NOLOCK)
				JOIN (	SELECT temp.DocumentDate, temp.EntityId, temp.CurrencyId
						FROM @ListPortfolioAdvance temp
						WHERE temp.CurrencyId=@DocumentCurrencyId
						GROUP BY temp.DocumentDate, temp.EntityId,temp.CurrencyId ) temp 
				ON trm.MeasurementDate = temp.DocumentDate AND trm.CurrencyId = temp.CurrencyId AND trm.OfficialCurrencyId = @currencyId
			
		end
		else if EXISTS(	select 1 
						from Common.TRM trm WITH(NOLOCK)
						JOIN (	SELECT temp.DocumentDate, temp.EntityId, temp.CurrencyId
							FROM @ListPortfolioAdvance temp
							WHERE temp.CurrencyId=@DocumentCurrencyId
							GROUP BY temp.DocumentDate, temp.EntityId,temp.CurrencyId ) temp
						ON trm.MeasurementDate = temp.DocumentDate AND trm.OfficialCurrencyId=temp.CurrencyId and trm.CurrencyId = @currencyId) begin
			
				insert into @actualCurrencyRate (CurrencyFrom, CurrencyTo, [Value], ValueReverse,EntityId)
				select @DocumentCurrencyId, @currencyId, ValueOfficialToCurrency, [Value], temp.EntityId
				from Common.TRM trm WITH(NOLOCK)
				JOIN (	SELECT temp.DocumentDate, temp.EntityId, temp.CurrencyId
					FROM @ListPortfolioAdvance temp
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
			from Portfolio.PortfolioAdvance pa WITH(NOLOCK)
			join @ListPortfolioAdvance temp on pa.Id = temp.Id and temp.CurrencyId = @DocumentCurrencyId
			left join Portfolio.PortfolioAdvanceExchangeRate er with(NOLOCK) on er.PortfolioAdvanceId = pa.Id and er.CurrencyId = @currencyId
			where er.Id is null) > 0 begin 

			INSERT INTO @tempTable
			select 999 AS MessageCode, 
			'No se encontro un TRM definido para el/los Anticipo(s) (' + (select STRING_AGG( pa.Code,',')
																	from Portfolio.PortfolioAdvance pa WITH(NOLOCK)
																	join @ListPortfolioAdvance temp on pa.Id = temp.Id and temp.CurrencyId = @DocumentCurrencyId
																	where temp.CurrencyId =@DocumentCurrencyId)  +')'  MessageVoucher,
					0 JournalVoucherId
			
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
		FROM @ListPortfolioAdvance temp
		join @actualCurrencyRate acr on acr.CurrencyFrom = temp.CurrencyId and acr.CurrencyTo = @currencyId AND acr.EntityId = temp.EntityId
		join Portfolio.PortfolioAdvanceExchangeRate paer WITH(NOLOCK) on paer.PortfolioAdvanceId = temp.Id AND paer.CurrencyId = @currencyId
		where temp.CurrencyId = @DocumentCurrencyId

		UPDATE temp SET
				temp.ProfitLostValue = ( temp.ValueTRMDocument - temp.ValueTRMConverted ) * IIF(temp.Nature=1,1,-1)
		FROM @ListPortfolioAdvance temp
		where temp.CurrencyId = @DocumentCurrencyId

		IF NOT EXISTS(	SELECT 1 
						from @ListPortfolioAdvance temp
						where abs(temp.ProfitLostValue) >0 and temp.CurrencyId = @DocumentCurrencyId) BEGIN
			GOTO NEXT_ROW
		END

		DECLARE @MappingTable as TABLE(TempJvId  INT,
										EntityId INT,
										EntityName varchar(220))

		IF EXISTS(SELECT 1 FROM @MappingTable) BEGIN
			DELETE FROM @MappingTable
		END

		MERGE INTO @JournalVourcherTmp AS target
		USING (SELECT temp.EntityName, temp.EntityId,vend.[Description],temp.EntityCode,temp.DocumentDate
				FROM @ListPortfolioAdvance temp
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
						CONCAT('Ajuste por diferencia en anticipos de cartera, ', source.[Description],'( ',ISNULL(source.EntityCode,''),' ) ',cast(common.GETDATE() as VARCHAR)),  
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
				iif(ma.HandlesThirdParty = 1, pa.ThirdPartyId, null),
				iif(ma.HandlesCostCenter = 1, pa.CostCenterId, null),
				iif(temp.ProfitLostValue > 0, ABS(temp.ProfitLostValue), 0),
				iif(temp.ProfitLostValue > 0, 0, abs(temp.ProfitLostValue)),
				Concat('Anticipo No : ',pa.Code), null, null, null, null 
			from Portfolio.PortfolioAdvance pa WITH(NOLOCK)
			join @ListPortfolioAdvance temp on pa.Id = temp.Id
			INNER JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id = pa.MainAccountId
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
					,IIF(temp.ProfitLostValue > 0, abs(temp.ProfitLostValue), 0)
					, ''
					, null
					, null
					, null
					, null
			FROM
				(	SELECT mt.TempJvId, SUM(temp.ProfitLostValue) ProfitLostValue, temp.Nature
					FROM @ListPortfolioAdvance temp
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
					iif(ma.HandlesThirdParty = 1, pa.ThirdPartyId, null),
					iif(ma.HandlesCostCenter = 1, pa.CostCenterId, null),
					iif(temp.ProfitLostValue > 0, ABS(temp.ProfitLostValue), 0),
					iif(temp.ProfitLostValue > 0, 0, abs(temp.ProfitLostValue)),
					Concat('Anticipo No : ',pa.Code), null, null, null, null 
				from Portfolio.PortfolioAdvance pa WITH(NOLOCK)
				INNER JOIN @ListPortfolioAdvance temp on pa.Id = temp.Id
				INNER JOIN GeneralLedger.HomologationAccount ha WITH(NOLOCK) ON ha.OfficialMainAccountId = pa.MainAccountId
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
						FROM @ListPortfolioAdvance temp
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la revalorización de anticipos de cartera a la fecha actual aplicando la Tasa Representativa del Mercado (TRM). Toma una lista de anticipos de cartera (PortfolioAdvance) en moneda extranjera, calcula la diferencia de cambio entre la TRM del documento original y la TRM vigente, y genera los comprobantes contables de ajuste por diferencia en cambio (ganancia o pérdida por conversión de moneda). Consulta traslados de cartera (PortfolioTransfer) y notas de cartera (PortfolioNote) para determinar la naturaleza y código del documento origen, valida la configuración contable de la empresa (cuentas de ganancias/pérdidas por diferencial cambiario, libro oficial, centro de costos) y devuelve el resultado en formato XML con los comprobantes contables generados o los errores encontrados. Se utiliza para el cierre contable y ajuste de saldos en moneda extranjera dentro del módulo de cartera y tesorería.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_PortfolioAdvanceRevaluation_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_PortfolioAdvanceRevaluation_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Revaloriza anticipos de cartera a la TRM de la fecha actual generando comprobantes contables de ajuste por diferencia en cambio (utilidad o pérdida) para los libros legales cuya moneda oficial difiere de la del anticipo.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioAdvanceRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodos /PortfolioAdvance con Id, ValueAdjustment, EntityName, EntityId y DocumentDate.; Cada anticipo referenciado debe existir en Portfolio.PortfolioAdvance y tener CurrencyId definido (no nulo ni 0).; GeneralLedger.CompanySettings debe tener configurados ProfitLostByExchangeCurrencyAccountId, LostByExchangeCurrencyAccountId, ProfitLostJournalVoucherTypeId y CostCenterId.; Debe existir un libro oficial en GeneralLedger.LegalBook (OfficialBook = 1).; Debe existir TRM en Common.TRM para la DocumentDate del anticipo entre la moneda del documento y la moneda oficial del libro.; Debe existir registro en Portfolio.PortfolioAdvanceExchangeRate para cada anticipo y cada moneda oficial de los libros activos.; Si el libro destino no es el oficial, debe existir homologación en GeneralLedger.HomologationAccount para las cuentas involucradas (cuenta del anticipo y cuentas de utilidad/pérdida en cambio).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioAdvanceRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.PortfolioAdvance: Si algún anticipo de la lista tiene CurrencyId nulo o 0, se retorna XML con código 999 y mensaje ''Los anticipos de cartera : (...) No tienen moneda especificada'' y se aborta.; [RETURN_RESULT] Common.TRM: Si no existe TRM (en ningún sentido de conversión) entre la moneda del documento y la moneda oficial del libro para la DocumentDate, se retorna XML con código 999 y mensaje ''Ocurrio un error: no se encontro TRM (<moneda>) para la fecha actual'' y se aborta cerrando el cursor.; [RETURN_RESULT] Portfolio.PortfolioAdvanceExchangeRate: Si algún anticipo no tiene registro en PortfolioAdvanceExchangeRate para la moneda oficial del libro iterado, se retorna XML con código 999 y mensaje ''No se encontro un TRM definido para el/los Anticipo(s) (...)''.; [INSERT] GeneralLedger.JournalVoucher: Cuando |ProfitLostValue| > 0 para algún anticipo en la moneda iterada, se construye un comprobante contable (cabecera + detalles) y se invoca GeneralLedger.SP_CreateAndValidateJournalVoucherMovement para persistirlo, con detalle ''Ajuste por diferencia en anticipos de cartera, ...''.; [INSERT] GeneralLedger.JournalVoucherDetail: Por cada anticipo con diferencia se inserta un detalle contra la cuenta del anticipo (pa.MainAccountId u homologada) con débito si ProfitLostValue>0 o crédito si <0; y un detalle contrapartida contra ProfitByExchangeCurrencyAccountId (utilidad) o LostByExchangeCurrencyAccountId (pérdida) según el signo.; [INSERT] GeneralLedger.JournalVoucherDetail: Si el libro iterado no es el oficial (OfficialBookId<>@bookId), las cuentas se reemplazan por su equivalente en GeneralLedger.HomologationAccount filtrando por LegalBookId = @bookId.; [RETURN_RESULT] @tempTable: Al finalizar, retorna en @XmlOutPut la lista de resultados (Code, MessageOutput, JournalVoucherId) generados por las llamadas al SP de creación de comprobante.; [RAISERROR] @tempTable: En CATCH se cierra/desalloca el cursor si está abierto y se devuelve un select con MessageCode 999 y ERROR_MESSAGE() como mensaje.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioAdvanceRevaluation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioAdvanceRevaluation_Output';
-- GO
