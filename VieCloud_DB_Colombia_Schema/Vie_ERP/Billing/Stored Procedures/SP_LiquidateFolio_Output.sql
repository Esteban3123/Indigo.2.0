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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de liquidación de folios de facturación al momento de la salida del paciente. Recibe el código del paciente, número de ingreso, autorización de facturación y una lista en XML con los detalles de cruce de control de ingresos (fechas de corte, descuentos del paciente, diagnóstico de salida, tipo de moneda y TRM, devolución de impuestos, entre otros). Primero ejecuta el procedimiento de validaciones [SP_LiquidateFolioValidations_Output] para verificar que el proceso pueda continuar; si las validaciones pasan, procesa cada detalle de la lista de manera iterativa generando facturas o documentos de cobro por cada ítem. Consulta la moneda oficial de la empresa desde [GeneralLedger.CompanySettings] para usarla como moneda por defecto cuando no se especifica. Devuelve el estado del resultado, mensajes de éxito o error y un XML con el resultado de la facturación generada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidateFolio_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidateFolio_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Orquesta la liquidación de folios: valida, genera facturas por cada detalle del XML, cierra la admisión y actualiza el número de factura en FURIPS cuando aplica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las validaciones previas ejecutadas por SP_LiquidateFolioValidations_Output deben retornar @ResultStatus=1; en caso contrario aborta sin procesar.; El XML @RevenueControlDetailCrossingListXml debe contener nodos ''RevenueControlDetailCrossingList'' con los campos esperados (RevenueControlDetailId, OutputDate en formato 103, etc.).; Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId para usarse como moneda por defecto.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La liquidación es transaccionalmente sensible: si una factura del lote falla, se aborta el proceso y no se ejecutan los pasos posteriores (cierre de admisión, actualización FURIPS).; Sólo los folios tipo 4 (Aseguradoras) participan en la actualización de NUMFAC sobre ADFURIPSU.; El cierre de admisión vía SP_CloseAdmission_Output sólo ocurre cuando ya no quedan detalles de folio en estados 1, 3 o 5 (excluyendo cuentas maestras tipo 3).; Se inyecta TaxDevolutionValue dentro del XML ListPortfolioAdvanceCrossing antes de invocar la creación de factura, exponiéndolo como variable contextual al subproceso.; Cualquier excepción no controlada (CATCH) retorna estado 0 con el mensaje de error y línea, y XML vacío.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de folios; Facturación; Admisión del paciente; Cierre de admisión; FURIPS (afectación a aseguradoras); Moneda oficial y TRM; Descuento al paciente; Diagnóstico de salida; Tipo de corte de cuenta; Folio tipo Aseguradoras; Cuenta maestra', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.ADINGRESO: Si tras facturar aún existen detalles del folio (RevenueControlDetail.Status IN (1,3,5) y IsMasterAccount<>3) en la admisión, se marca IESTADOIN=''P'' (pendiente) sólo cuando el estado actual es '' '' (en blanco).; [UPDATE] dbo.ADFURIPSU: Cuando NO existe ningún FURIPS con NUMFAC ya asignado para folios tipo 4 (Aseguradoras) en la admisión, se actualiza NUMFAC con el último @InvoiceNumber generado para todos los FURIPS de esa admisión cuyo folio sea tipo 4.; [RETURN_RESULT] OUTPUT: Devuelve @ResultXml con la lista de InvoiceId/InvoiceNumber generados; @ResultStatus=1 con mensajes de éxito si todo prosperó, o =0 con mensaje de error si falló validación, creación de factura o cierre de admisión.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SP_LiquidateFolioValidations_Output devuelve @ResultStatus=0 → Aborta retornando estado 0 y mensaje ''No se logro completar el proceso de validacion'' si no hay mensaje específico. else Continúa con el proceso de facturación.; si Para un detalle, SP_CreateInvoice_Output retorna @StatusResultInvoice=0 → Acumula el error en @errorList y rompe el bucle (BREAK), abortando la liquidación con estado 0. else Acumula mensajes de éxito y registra la factura en @InvoiceResult.; si CurrencyId del detalle es NULL o 0 → Se reemplaza por la moneda oficial obtenida de GeneralLedger.CompanySettings.OfficialCurrencyId.; si TRMValue del XML es vacío o NULL → Se asigna 1 como valor TRM por defecto. else Se usa el TRMValue provisto.; si Existen detalles de RevenueControlDetail con Status IN (1,3,5) y IsMasterAccount<>3 para la admisión tras facturar → Marca la admisión como pendiente (IESTADOIN=''P'') sin invocar cierre. else Invoca SP_CloseAdmission_Output para cerrar la admisión; si éste falla, aborta con estado 0.; si No existe ningún ADFURIPSU con NUMFAC ya diligenciado para folios tipo 4 (Aseguradoras) en la admisión → Actualiza NUMFAC en ADFURIPSU con el último @InvoiceNumber generado. else No modifica los FURIPS existentes.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_LiquidateFolioValidations_Output; Billing.SP_CreateInvoice_Output; Billing.SP_CloseAdmission_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Billing.RevenueControl; Billing.RevenueControlDetail; dbo.ADFURIPSU', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolio_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateFolio_Output';
-- GO
