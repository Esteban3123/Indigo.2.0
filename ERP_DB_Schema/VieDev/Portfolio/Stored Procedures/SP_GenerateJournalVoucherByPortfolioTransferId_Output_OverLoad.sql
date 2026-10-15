-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-09
-- Description:	Procedimiento que se encarga de generar el reconocimiento a partir de una cuenta por cobrar
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateJournalVoucherByPortfolioTransferId_Output_OverLoad]
	@PortfolioTransferId INT,
	@CodeUser VARCHAR(20),
	@XmlParameters XML,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @Status TINYINT,
			------------------------------
			@JournalVoucherTypeId INT,
			@VoucherDate DATETIME,
			@Detail VARCHAR(MAX),
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			@CurrencyId INT,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id_Output INT

	--Tabla de resultados para la generacion de detalles de comprobante contable para provision y deterioro
	DECLARE @ResultProvisionAndDeterioration TABLE
	(
		Id INT, 
		[Status] INT, 
		[Message] VARCHAR(MAX),
		IdMainAccount INT, 
		IdThirdParty INT, 
		IdCostCenter INT, 
		DebitValue DECIMAL(21, 5), 
		CreditValue DECIMAL(21, 5)
	)

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	DECLARE @JournalVourcherTmp TABLE 
	(
		Id INT DEFAULT(0),
		Consecutive BIGINT DEFAULT(0),
		LegalBookId INT,
		IdJournalVoucher INT,
		VoucherDate DATETIME,
		Imported VARCHAR(5) DEFAULT('False'),
		Status TINYINT,
		Detail VARCHAR(MAX),
		EntityCode VARCHAR(20),
		EntityId INT,
		EntityName VARCHAR(250),
		IsClosedYear TINYINT DEFAULT(0),
		CurrencyId INT
	)

	--Se declara una tabla temporal para los detalles del comprobante
	DECLARE @JournalVourcherDetailTmp TABLE 
	(
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(21,5),
		CreditValue DECIMAL(21,5),
		Detail VARCHAR(MAX),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--tabla temporal para almacenar el resultado deL movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		
		SELECT	
				@EntityName =  t.x.value('EntityName[1]','VARCHAR(100)')
		FROM @XmlParameters.nodes('/Parameters') t(x)
		
		SELECT	@Status = pt.Status
		FROM Portfolio.PortfolioTransfer pt
		WHERE pt.Id = @PortfolioTransferId

		--si viene desde fact basica tiene que tomar el TRM Oficial en contabilidad, solo se usa EL TRM custom cuando se genera desde Fact salud (Invoice)
		IF @EntityName is not NULL AND @EntityName <> 'Invoice' BEGIN
			SET @EntityName=''
		END

		IF @Status = 2
		BEGIN
			SELECT @Detail = ' Facturas: ' + STUFF((
				SELECT DISTINCT ', ' + ar.InvoiceNumber
				FROM Portfolio.PortfolioTransferDetail ptd
				JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
				WHERE ptd.PortfolioTrasferId = @PortfolioTransferId
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@JournalVoucherTypeId = sp.JournalVoucherTypeTranslationId,
					@VoucherDate = pt.DocumentDate,
					@Detail = 'Generado desde Cruce Anticipo vs CxC ' + pt.Code + ' - Anticipo: ' + pa.Code + ISNULL(@Detail, ''),
					@EntityId = pt.Id,
					@EntityCode = pt.Code,
					@EntityName = IIF(@EntityName is NULL OR @EntityName  ='','PortfolioTransfer','AutomaticPortfolioTransfer') ,
					@CurrencyId = pa.CurrencyId
			FROM Portfolio.PortfolioTransfer pt
			JOIN Portfolio.PortfolioAdvance pa ON pt.PortfolioAdvanceId = pa.Id
			JOIN Portfolio.SettingPortfolio sp ON pt.OperatingUnitId = sp.OperatingUnitId		
			WHERE pt.Id = @PortfolioTransferId
			
			/******************************** PROVISION Y DETERIORO ******************************/
			if @CurrencyId is null or @CurrencyId =0 BEGIN
				set @CurrencyId = (SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings)
			END

			SELECT @SubXml = CONVERT
			(
				XML,
				(
					SELECT Data.*
					FROM 
					(
						SELECT 
							ptd.AccountReceivableId, 
							@VoucherDate DocumentDate,
							ar.InvoiceNumber, 
							ptd.Value,
							@CurrencyId as CurrencyId
						FROM Portfolio.PortfolioTransferDetail ptd
						JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
						WHERE PortfolioTrasferId = @PortfolioTransferId
					) AS Data
					For XML AUTO,TYPE, ELEMENTS
				)
			)

			INSERT @ResultProvisionAndDeterioration
				EXEC [Portfolio].[SP_CreateDetailsJournalVoucher] @SubXml

			IF EXISTS (SELECT 1 FROM @ResultProvisionAndDeterioration WHERE [Status] = 0 OR [Status] = 2)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + Message
						FROM @ResultProvisionAndDeterioration 
						WHERE [Status] = 0 OR [Status] = 2
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
						@MessageResult = ISNULL(@Message, '')
				RETURN 
			END
		END
		ELSE IF @Status = 4
		BEGIN
			SELECT	@JournalVoucherTypeId = sb.ReverseTransferJournalVoucherTypeId,
					@VoucherDate = pn.NoteDate,
					@Detail = 'Generado desde la Nota de Cartera ' + pn.Code + ' reversando el Cruce Anticipo vs CxC ' + pt.Code,
					@EntityId = pn.Id,
					@EntityCode = pn.Code,
					@EntityName = IIF(@EntityName is NULL OR @EntityName  ='','PortfolioNote','AutomaticPortfolioNote') ,
					@CurrencyId= pn.CurrencyId
			FROM Portfolio.PortfolioNote pn
			JOIN Portfolio.PortfolioTransfer pt ON pn.PortfolioTransferId = pt.Id
			JOIN Billing.SettingsBilling sb ON pt.OperatingUnitId = sb.IdOperatingUnit
			WHERE pn.Status = 2 AND pt.Id = @PortfolioTransferId
		END

		--Inserto la cabecera del comprobante contable
		INSERT INTO @JournalVourcherTmp
		(
			IdJournalVoucher,VoucherDate,Status,Detail,EntityId,EntityCode,EntityName,CurrencyId
		)
		VALUES
		(
			@JournalVoucherTypeId,@VoucherDate,2,@Detail,@EntityId,@EntityCode,@EntityName,@CurrencyId
		)

			;WITH  CTE_PortfolioTransferId AS( 	
			SELECT pt.PortfolioAdvanceId, SUM(d.DebitValue - d.CreditValue) Value
			FROM Portfolio.PortfolioTransfer pt
			JOIN
			(
				SELECT d.PortfolioTrasferId PortfolioTransferId, 0 DebitValue, d.Value CreditValue
				FROM Portfolio.PortfolioTransferDetail d
				WHERE d.PortfolioTrasferId = @PortfolioTransferId
				UNION ALL
				SELECT d.PortfolioTransferId PortfolioTransferId, IIF(d.Nature = 1, d.Value, 0), IIF(d.Nature = 1, 0, d.Value) CreditValue
				FROM Portfolio.PortfolioTransferOtherConcept d
				WHERE d.PortfolioTransferId = @PortfolioTransferId
			) d ON pt.Id = d.PortfolioTransferId
			WHERE pt.Id = @PortfolioTransferId
			GROUP BY pt.PortfolioAdvanceId)

		--Se insertan los detalles del comprobante contable crédito
		INSERT INTO @JournalVourcherDetailTmp
		(
			IdMainAccount, 
			IdThirdParty, 
			IdCostCenter, 
			Detail, 
			DebitValue, 
			CreditValue
		)
		--Anticipo
			SELECT
				ma.Id, 
				CASE ma.HandlesThirdParty WHEN 1 THEN pa.ThirdPartyId ELSE NULL END as ThirdPartyId,
				CASE ma.HandlesCostCenter WHEN 1 THEN pa.CostCenterId ELSE NULL END as CostCenterId,
				'',
				CAST(ABS(IIF(@Status = 2, IIF(pt.Value > 0, 0, pt.Value), IIF(pt.Value > 0, pt.Value, 0))) AS DECIMAL(21, 5)) DebitValue, 
				CAST(ABS(IIF(@Status = 2, IIF(pt.Value > 0, pt.Value, 0), IIF(pt.Value > 0, 0, pt.Value))) AS DECIMAL(21, 5)) CreditValue
			FROM Portfolio.PortfolioAdvance pa
			JOIN GeneralLedger.MainAccounts ma ON pa.MainAccountId = ma.Id
			JOIN CTE_PortfolioTransferId pt ON pa.Id = pt.PortfolioAdvanceId
		UNION ALL
			--Facturas
			SELECT
				ma.Id, 
				CASE ma.HandlesThirdParty WHEN 1 THEN ar.ThirdPartyId ELSE NULL END as ThirdPartyId,
				CASE ma.HandlesCostCenter WHEN 1 THEN ptd.CostCenterId ELSE NULL END as CostCenterId,
				'',
				IIF(@Status = 2, 0, ptd.Value) DebitValue, 
				IIF(@Status = 2, ptd.Value, 0) CreditValue
			FROM Portfolio.PortfolioTransferDetail ptd			
			JOIN GeneralLedger.MainAccounts ma ON ptd.MainAccountId = ma.Id
			JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
			WHERE ptd.PortfolioTrasferId = @PortfolioTransferId
		UNION ALL
			--Otros Conceptos
			SELECT
				ma.Id, 
				CASE ma.HandlesThirdParty WHEN 1 THEN ptoc.ThirdPartyId ELSE NULL END as ThirdPartyId,
				CASE ma.HandlesCostCenter WHEN 1 THEN ptoc.CostCenterId ELSE NULL END as CostCenterId,
				'',
				IIF(@Status = 2, IIF(ptoc.Nature = 1, ptoc.Value, 0), IIF(ptoc.Nature = 1, 0, ptoc.Value)) DebitValue, 
				IIF(@Status = 2, IIF(ptoc.Nature = 1, 0, ptoc.Value), IIF(ptoc.Nature = 1, ptoc.Value, 0)) CreditValue
			FROM Portfolio.PortfolioTransferOtherConcept ptoc
			JOIN GeneralLedger.MainAccounts ma ON ptoc.MainAccountId = ma.Id
			WHERE ptoc.PortfolioTransferId = @PortfolioTransferId
			--Provision / Deterioro
		UNION ALL
			SELECT 
				ma.Id, 
				CASE ma.HandlesThirdParty WHEN 1 THEN jvd.IdThirdParty ELSE NULL END as ThirdPartyId,
				CASE ma.HandlesCostCenter WHEN 1 THEN jvd.IdCostCenter ELSE NULL END as CostCenterId,
				'Detalle generado con Provision/Deterioro',
				IIF(@Status = 2, jvd.DebitValue, jvd.CreditValue), 
				IIF(@Status = 2, jvd.CreditValue, jvd.DebitValue)
			FROM @ResultProvisionAndDeterioration jvd
			JOIN GeneralLedger.MainAccounts ma ON jvd.IdMainAccount = ma.Id
			WHERE jvd.Status = 1
		
		/******************************************** GENERAR COMPROBANTE ********************************************/

		SELECT @SubXml = CONVERT
		(
			XML,
			(
				SELECT *
				FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.id = JournalVoucherDetail.IdAccounting
				For XML AUTO,TYPE, ELEMENTS
			)
		)

		--Se consume el sp que guarda el movimiento contable
		insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml,@CodeUser 
			select 
				@Code_Output = rjv.code, 
				@Message_Output = rjv.MessageResult, 
				@Id_Output = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv

		IF @Code_Output <> 0
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = ISNULL(@Message_Output, '')
			RETURN
		END  

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,			
				@MessageResult = 'Se generó el Comprobante contable ' + CAST(jv.Consecutive AS VARCHAR(30)) + ' de tipo ' + jvt.Code + ' - ' + jvt.Name
		FROM GeneralLedger.JournalVouchers jv
		JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.Id
		WHERE jv.Id = @Id_Output
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END