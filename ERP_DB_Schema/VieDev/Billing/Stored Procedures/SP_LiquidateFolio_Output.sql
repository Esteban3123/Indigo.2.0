-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-12-28
-- Description:	Liquidación
-- =============================================
CREATE PROCEDURE [Billing].[SP_LiquidateFolio_Output]
		@PatientCode VARCHAR(20),
	@AdmissionNumber VARCHAR(20),
	@ContainerCrystal VARCHAR(10),
	@BillingAuthorizationId INT,
	@OperativeUnitId INT,
	@ThirdPartyPatientId INT,
	@UserCode VARCHAR(20),
	@CompanyType TINYINT,
	@RevenueControlDetailCrossingListXml XML,
	@SkipAccountControlValidations BIT,
	-------------------------------------------------------
	@ResultStatus BIT OUTPUT,
	@ResultMessageInvoice VARCHAR(MAX) OUTPUT,
	@ResultMessage VARCHAR(MAX) OUTPUT,
	@ResultXml XML OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE	@RevenueControlDetailId INT,
			@TotalPatientDiscount DECIMAL(18, 2),
			@CutType INT,
			@IsCutAccount BIT,
			@InitialDate DATETIME,
			@OutputDate DATETIME,
			@OutputDiagnosis VARCHAR(10),
			@FilePath VARCHAR(MAX),
			@ListPortfolioAdvanceXml XML,
			---------------------------
			@errorList As VARCHAR(MAX) = '',
			@consecutiveList VARCHAR(MAX) = '',
			@messageNotification VARCHAR(MAX) = '',
			---------------------------
			@Rows INT = 1, 
			@RowId INT = 0, 
			---------------------------
			@StatusResultInvoice BIT,
			@MessageResultInvoice VARCHAR(MAX),
			@MessageInvoice VARCHAR(MAX),
			@InvoiceId INT,
			@InvoiceNumber VARCHAR(20),
			@CurrencyId INT,
			@TRMValue DECIMAL(20,5),
			@TaxDevolutionValue NUMERIC(20,2),
			@ConditionSalesId INT,
			@EconomicActivityId INT,
			@AdditionalParametersXml XML

	DECLARE @RevenueControlDetailCrossingList AS TABLE
	(
		RowId INT IDENTITY(1,1) PRIMARY KEY, 
		RevenueControlDetailId INT,
		ListPortfolioAdvanceCrossing XML,
		TotalPatientDiscount DECIMAL(18, 2),
		OutputDate DATETIME,
		IsCutAccount BIT,
		OutputDiagnosis VARCHAR(10),
		InitialDate DATETIME,
		CutType INT,
		FilePath VARCHAR(MAX),
		CurrencyId INT,
		TRMValue DECIMAL(20,5),
		TaxDevolutionValue NUMERIC(20,2),
		ConditionSalesId INT,
		EconomicActivityId INT
	)

	DECLARE @InvoiceResult AS TABLE
	(
		InvoiceId INT,
		InvoiceNumber VARCHAR(20)
	)
	
	DECLARE @OficialCurrency INT
	Set @OficialCurrency =(select top 1 OfficialCurrencyId from GeneralLedger.CompanySettings WITH(NOLOCK))
	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		
		EXEC [Billing].[SP_LiquidateFolioValidations_Output]	@PatientCode, 
																@AdmissionNumber, 
																@ContainerCrystal, 
																@BillingAuthorizationId, 
																@OperativeUnitId, 
																@ThirdPartyPatientId, 
																@UserCode, 
																@CompanyType, 
																@RevenueControlDetailCrossingListXml, 
																@SkipAccountControlValidations,
																--Salidas
																@ResultStatus OUTPUT, 
																@ResultMessage OUTPUT 

		
		IF @ResultStatus = 0
		BEGIN
			SELECT	@ResultStatus  =  CONVERT(BIT, 0),
					@ResultMessageInvoice = '',
					@ResultMessage = ISNULL(@ResultMessage, 'No se logro completar el proceso de validacion'),
					@ResultXml = ''
			RETURN
		END
		-------------------------------------------------------------------------------------------------------------------

		INSERT INTO @RevenueControlDetailCrossingList
			SELECT	t.x.value('RevenueControlDetailId[1]', 'INT'),
					t.x.query('ListPortfolioAdvanceCrossing'),
					t.x.value('TotalPatientDiscount[1]', ' DECIMAL(18, 2)'),
					CONVERT(DATETIME, t.x.value('OutputDate[1]', 'nvarchar(19)'), 103),
					t.x.value('IsCutAccount[1]', 'BIT'),
					t.x.value('OutputDiagnosis[1]', 'VARCHAR(10)'),
					CONVERT(DATETIME, t.x.value('InitialDate[1]', 'nvarchar(19)'), 103),
					t.x.value('CutType[1]', 'INT'),
					t.x.value('FilePath[1]', 'VARCHAR(MAX)'),
					t.x.value('CurrencyId[1]', 'INT'),
					IIF(COALESCE( t.x.value('TRMValue[1]', 'varchar(20)'),'') ='',1, t.x.value('TRMValue[1]', 'DECIMAL(20,5)')),
					t.x.value('TaxDevolutionValue[1]', 'NUMERIC(20,2)'),
					t.x.value('ConditionSalesId[1]', 'INT'),
					t.x.value('EconomicActivityId[1]', 'INT')
			FROM @RevenueControlDetailCrossingListXml.nodes('RevenueControlDetailCrossingList') t(x)
		
		---------------------------------------------------------------------------------------------------------------

		WHILE @Rows > 0
		BEGIN
			SELECT TOP 1
				@RowId = RowId,
				@RevenueControlDetailId = RevenueControlDetailId,
				@ListPortfolioAdvanceXml = ListPortfolioAdvanceCrossing,
				@TotalPatientDiscount = TotalPatientDiscount,
				@OutputDate = OutputDate,
				@IsCutAccount = IsCutAccount,
				@OutputDiagnosis = OutputDiagnosis,
				@InitialDate = InitialDate,				
				@CutType = CutType,
				@FilePath = FilePath,
				@CurrencyId = IIF(COALESCE(CurrencyId,0)=0,@OficialCurrency,CurrencyId),
				@TRMValue= TRMValue,
				@TaxDevolutionValue = TaxDevolutionValue,
				@ConditionSalesId = ConditionSalesId,
				@EconomicActivityId = EconomicActivityId
			FROM @RevenueControlDetailCrossingList
			WHERE RowId > @RowId 
			ORDER BY RowId

			SET @Rows = @@RowCount
			IF @Rows = 0 
				BREAK

			set @ListPortfolioAdvanceXml.modify('insert  <Variables>
															<TaxDevolutionValue>{sql:variable("@TaxDevolutionValue") }</TaxDevolutionValue>
														 </Variables>
													before (/ListPortfolioAdvanceCrossing)[1]')

			SELECT @AdditionalParametersXml = CONVERT
				(
					XML, 
					(
						SELECT AdditionalParameters.* 
						FROM
						(SELECT
							@FilePath FilePath,
							@CurrencyId CurrencyId,
							@TRMValue TRMValue,
							@ConditionSalesId ConditionSalesId,
							@EconomicActivityId EconomicActivityId
						FROM @RevenueControlDetailCrossingList
						) AdditionalParameters
						For XML AUTO,TYPE, ELEMENTS
					)
				)

			-----------------------------------------------------------------------------------------------------------
			EXEC [Billing].[SP_CreateInvoice_Output]	@RevenueControlDetailId, 
														@BillingAuthorizationId, 
														@PatientCode,
														@AdmissionNumber, 
														@ContainerCrystal, 
														@TotalPatientDiscount,
														@OutputDate,
														@IsCutAccount, 
														@OutputDiagnosis, 
														@InitialDate, 
														@CutType, 
														@CompanyType, 
														@ThirdPartyPatientId,
														@OperativeUnitId, 
														@UserCode, 
														@ListPortfolioAdvanceXml,
														@AdditionalParametersXml,
														@StatusResultInvoice OUTPUT, 
														@MessageResultInvoice OUTPUT,
														@MessageInvoice OUTPUT, 
														@InvoiceId OUTPUT, 
														@InvoiceNumber OUTPUT
			
			IF @StatusResultInvoice = 0 
			BEGIN
				SET @errorList += @MessageInvoice + CHAR(13) + CHAR(10)
				BREAK
			END
			-----------------------------------------------------------------------------------------------------------

			SET @consecutiveList += 'El Folio se liquidó correctamente generando: ' + CHAR(13) + CHAR(10) + @MessageResultInvoice + '@' + CHAR(13) + CHAR(10)				
			SET @messageNotification += @MessageInvoice + CHAR(13) + CHAR(10)

			INSERT INTO @InvoiceResult (InvoiceId, InvoiceNumber) VALUES (@InvoiceId, @InvoiceNumber)
		END

		---------------------------------------------------------------------------------------------------------------
		
		IF @errorList <> '' 
		BEGIN
			SELECT	@ResultStatus  =  CONVERT(BIT, 0),
					@ResultMessageInvoice = '',
					@ResultMessage = ISNULL(@errorList, 'Error creando la factura'),
					@ResultXml = ''
			RETURN
		END

		/********************************************** CIERRE ADMISION **********************************************/

		IF EXISTS (
			SELECT 1
			FROM Billing.RevenueControl rc WITH (NOLOCK)
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON rc.Id = rcd.RevenueControlId
			WHERE rc.AdmissionNumber = @AdmissionNumber AND rcd.Status IN (1, 3, 5) AND rcd.IsMasterAccount <> 3
		) BEGIN
			UPDATE ing 
				SET IESTADOIN = 'P'
			FROM [dbo].[ADINGRESO] ing
			WHERE ing.NUMINGRES = @AdmissionNumber AND ing.IESTADOIN = ' '
		END
		ELSE 
		BEGIN
			EXEC [Billing].[SP_CloseAdmission_Output]	@AdmissionNumber, 
														@ContainerCrystal, 
														@UserCode,
														--Salidas
														@ResultStatus OUTPUT, 
														@ResultMessage OUTPUT

			IF @ResultStatus = 0
			BEGIN
				SELECT	@ResultStatus  =  CONVERT(BIT, 0),
						@ResultMessageInvoice = '',
						@ResultMessage = ISNULL(@ResultMessage, 'No se logro cerrar el ingreso'),
						@ResultXml = ''
				RETURN
			END
		END
		
		--Afectación Furips--
		IF NOT EXISTS
		(
			SELECT 1 
			FROM  Billing.RevenueControl rc WITH (NOLOCK)
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON rc.Id = rcd.RevenueControlId
			JOIN ..ADFURIPSU fur WITH (NOLOCK) ON fur.NUMINGRES = rc.AdmissionNumber
			WHERE rc.AdmissionNumber = @AdmissionNumber
				AND rcd.FolioType = 4 --Aseguradoras
				AND LEN(fur.NUMFAC) > 0
		)
		BEGIN
			UPDATE fur SET fur.NUMFAC = @InvoiceNumber
			FROM  Billing.RevenueControl rc WITH (NOLOCK)
			JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON rc.Id = rcd.RevenueControlId
			JOIN ..ADFURIPSU fur WITH (NOLOCK) ON fur.NUMINGRES = rc.AdmissionNumber
			WHERE rc.AdmissionNumber = @AdmissionNumber
				AND rcd.FolioType = 4 --Aseguradoras
		END
		
		---------------------------------------------------------------------------------------------------------------

		SELECT @ResultXml = CONVERT
		(
			XML, 
			(
				SELECT 
					InvoiceId,
					InvoiceNumber
				FROM @InvoiceResult AS Data
				For XML AUTO,TYPE, ELEMENTS
			)
		)
				
		SELECT	@ResultStatus  = CONVERT(BIT, 1),
				@ResultMessageInvoice = @consecutiveList,
				@ResultMessage = @messageNotification
		FROM @InvoiceResult
	END Try
	BEGIN CATCH
		SELECT	@ResultStatus  =  CONVERT(BIT, 0),
				@ResultMessageInvoice = '',
				@ResultMessage = CONCAT('Error Liquidando los Folios: ', ERROR_MESSAGE(),' - Linea: ', ERROR_LINE()),
				@ResultXml = ''
	END CATCH
END