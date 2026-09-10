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
	DECLARE @DocumentCurrencyId INT = (SELECT ar.CurrencyId FROM Portfolio.AccountReceivable ar WHERE ar.Id = @AccountReceivableId)
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
	from GeneralLedger.CompanySettings

	SET @OfficialBookId = (SELECT TOP 1 lb.Id FROM GeneralLedger.LegalBook lb WHERE lb.OfficialBook = 1)

	-----Nombre de la entidad que genera el ajuste diferencial------
	SELECT top 1 @EntityNameDescription = Description
	from GeneralLedger.ViewEntityNameDescriptions
	where EntityName = @EntityName
	-------------------------------------------------------------------
	/*------Identificacion Movimento de reversion-----*/
	IF ISNULL(@EntityName,'') ='PortfolioNote' BEGIN
		SELECT @Nature = pn.Nature--Naturaleza de la nota 1 - Debito 2 - Credito
		from Portfolio.PortfolioNote pn
		WHERE pn.Id = @EntityId
	END
	ELSE IF (ISNULL(@EntityName,'') ='PortfolioTransfer') BEGIN
		SELECT  @Nature = IIF(pt.Status=2,2,1)
		from Portfolio.PortfolioTransfer pt
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
	select distinct Id, OfficialCurrencyId from GeneralLedger.LegalBook where [Status] = 1 AND OfficialCurrencyId <> @DocumentCurrencyId
  
	OPEN currency_cursor  
  
	FETCH NEXT FROM currency_cursor   
	INTO @bookId, @currencyId
  
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
				SELECT 999 AS CodeMessage, 
				'Ocurrio un error: no se encontro TRM (' + (select [Name] from Common.Currency where Id = @currencyId)  +') para la fecha actual ' [Message],
				0 as IdJournalVoucher
				return
		end

		if (select count(*) from Portfolio.AccountReceivable ar
			left join Portfolio.AccountReceivableExchangeRate er on er.AccountReceivableId = ar.Id and er.CurrencyId = @currencyId
			where ar.Id = @AccountReceivableId and er.Id is null) > 0 begin 
 
			select 999 AS CodeMessage, 
			'No se encontro un TRM definido para la factura (' + (select ar.InvoiceNumber from Portfolio.AccountReceivable ar where ar.Id = @AccountReceivableId)  +')'  [Message],
			0 as IdJournalVoucher
			return
		END

		DECLARE @ValuePaidTRMConverted DECIMAL(18, 2), @BalanceTRMConverted DECIMAL(18, 2)
		SET @ValuePaidTRMConverted = (SELECT [Portfolio].fnConvertValueBasedOnExchangeRate(@ValuePaid, er.[Value] , er.ValueReverse) FROM @actualCurrencyRate er)
		SET @BalanceTRMConverted = (SELECT [Portfolio].fnConvertValueBasedOnExchangeRate(@ValuePaid, arer.[Value] , arer.ValueReverse) 
										FROM Portfolio.AccountReceivableExchangeRate arer
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
			from Portfolio.AccountReceivable ar
			INNER JOIN Portfolio.AccountReceivableAccounting ara ON ara.AccountReceivableId = ar.Id
			inner join GeneralLedger.MainAccounts ma on ma.Id = ara.MainAccountId
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
			FROM GeneralLedger.MainAccounts ma where ma.Id = IIF( @ValueAffected > 0 ,@ProfitLostByExchangeCurrencyAccountId,@LostByExchangeCurrencyAccountId)
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
			from Portfolio.AccountReceivable ar
			INNER JOIN Portfolio.AccountReceivableAccounting ara ON ara.AccountReceivableId = ar.Id
			inner join GeneralLedger.HomologationAccount ha ON ha.OfficialMainAccountId = ara.MainAccountId
			inner join GeneralLedger.MainAccounts ma on ma.Id = ha.MainAccountId and ma.LegalBookId = @bookId
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
			from GeneralLedger.HomologationAccount ha
			inner join GeneralLedger.MainAccounts ma on ma.Id = ha.MainAccountId and ma.LegalBookId = @bookId
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de revalorización (ajuste por diferencia en cambio) de cuentas por cobrar en moneda extranjera. Toma una cuenta por cobrar específica y, usando la TRM vigente para una fecha dada, calcula la ganancia o pérdida por diferencia de cambio entre la tasa original del documento y la tasa actual, generando automáticamente el comprobante contable de ajuste en el libro oficial de contabilidad. Consulta la configuración contable de la empresa para obtener las cuentas de ganancia/pérdida por diferencia en cambio y el tipo de comprobante correspondiente, y soporta documentos originados desde notas de cartera o traslados de cartera. Si no existe TRM registrada para la moneda y fecha indicadas, el proceso se interrumpe con un mensaje de error informativo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_AccountReceivableRevaluation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_AccountReceivableRevaluation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera comprobantes contables de ajuste por diferencia en cambio (revaluación) sobre una cuenta por cobrar, comparando el valor pagado convertido con la TRM actual contra la TRM original registrada, para cada libro contable con moneda distinta a la del documento.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_AccountReceivableRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La fecha del documento (@DocumentDate) no puede venir vacía; si lo está, retorna mensaje de error con CodeMessage 999.; La cuenta por cobrar (@AccountReceivableId) debe existir en Portfolio.AccountReceivable y tener una moneda asociada (CurrencyId).; Debe existir TRM en Common.TRM para la fecha del documento entre la moneda del documento y la moneda oficial del libro (en cualquiera de las dos direcciones).; Debe existir un registro en Portfolio.AccountReceivableExchangeRate para la cuenta por cobrar y la moneda del libro; si no existe, se aborta con error referenciando la factura.; GeneralLedger.CompanySettings debe tener configurados ProfitLostByExchangeCurrencyAccountId, LostByExchangeCurrencyAccountId, ProfitLostJournalVoucherTypeId y CostCenterId.; Debe existir un libro oficial (OfficialBook=1) en GeneralLedger.LegalBook.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_AccountReceivableRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan libros legales con Status=1 y OfficialCurrencyId distinto al CurrencyId del documento.; El ajuste contable es de doble partida: una línea contra la cuenta de la cartera (con tercero/centro de costo según configuración de la cuenta) y otra contra la cuenta de utilidad o pérdida por diferencia en cambio (con CostCenterCsId).; El detalle de la cabecera siempre tiene EntityName=''JournalVouchers'', Status=2 y descripción ''Ajuste por diferencia en cambio : <descripción entidad> <código entidad>''.; Cuando HandlesThirdParty=1 se asigna ar.ThirdPartyId; cuando HandlesCostCenter=1 se asigna ar.CostCenterId (o @CostCenterCsId en la contrapartida).; El signo del ajuste se invierte para movimientos de naturaleza débito (@Nature=1), multiplicando por -1.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_AccountReceivableRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger (vía SP_CreateAndValidateJournalVoucherMovement): Por cada libro legal activo con moneda distinta a la del documento, si @ValueAffected ≠ 0, se construye XML de comprobante y se invoca GeneralLedger.SP_CreateAndValidateJournalVoucherMovement para crear el movimiento contable de ajuste por diferencia en cambio.; [RETURN_RESULT] Portfolio.AccountReceivable / GeneralLedger movimientos: Si @DocumentDate es vacío/NULL retorna SELECT con CodeMessage=999 y mensaje ''La fecha del documento no pude venir vacia'' e IdJournalVoucher=0.; [RETURN_RESULT] Common.TRM: Si no se encuentra TRM en Common.TRM para la fecha y combinación de monedas, cierra cursor y retorna CodeMessage=999 con mensaje indicando la moneda sin TRM.; [RETURN_RESULT] Portfolio.AccountReceivableExchangeRate: Si no existe AccountReceivableExchangeRate para la cuenta por cobrar y la moneda del libro, retorna CodeMessage=999 con mensaje ''No se encontro un TRM definido para la factura (InvoiceNumber)''.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_AccountReceivableRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EntityName = ''PortfolioNote'' → @Nature toma el valor de PortfolioNote.Nature (1=Débito, 2=Crédito) para determinar signo del ajuste. else Si @EntityName = ''PortfolioTransfer'', @Nature = 2 cuando pt.Status=2, sino 1.; si Existe TRM con CurrencyId=@DocumentCurrencyId y OfficialCurrencyId=@currencyId en la fecha → Toma Value y ValueOfficialToCurrency directos. else Si existe TRM en sentido inverso (OfficialCurrencyId=@DocumentCurrencyId, CurrencyId=@currencyId), invierte Value y ValueOfficialToCurrency.; si @ValueAffected = (@ValuePaidTRMConverted - @BalanceTRMConverted) * IIF(@Nature=2,1,-1) = 0 → Salta a NEXT_ROW sin generar comprobante para ese libro. else Inserta cabecera y detalles del comprobante.; si @OfficialBookId = @bookId (mismo libro oficial) → Inserta detalles usando directamente las cuentas de Portfolio.AccountReceivableAccounting y GeneralLedger.MainAccounts sin homologación. else Aplica homologación vía GeneralLedger.HomologationAccount para obtener las cuentas equivalentes en el libro destino.; si @ValueAffected > 0 → Carga la cuenta de la cartera al débito y la cuenta ProfitLostByExchangeCurrencyAccountId al crédito (utilidad por diferencia en cambio). else Carga la cuenta de la cartera al crédito y la cuenta LostByExchangeCurrencyAccountId al débito (pérdida por diferencia en cambio).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_AccountReceivableRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Portfolio.fnConvertValueBasedOnExchangeRate', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_AccountReceivableRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Portfolio.AccountReceivableExchangeRate; Portfolio.AccountReceivableAccounting; Portfolio.PortfolioNote; Portfolio.PortfolioTransfer; GeneralLedger.CompanySettings; GeneralLedger.LegalBook; GeneralLedger.MainAccounts; GeneralLedger.HomologationAccount; GeneralLedger.ViewEntityNameDescriptions; Common.TRM; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_AccountReceivableRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_AccountReceivableRevaluation';
-- GO
