-- =============================================
-- Author:		Diego A. Roldan
-- ALTER date: 2015-09-24
-- Description:	Genera la factura y las cuentas por cobrar de un folio a liquidar
-- =============================================

CREATE PROCEDURE [Billing].[ALTERInvoice]
    @RevenueControlDetailId INT,
    @BillingAuthorizationId INT,
    @PatientCode VARCHAR(20),
    @AdmissionNumber VARCHAR(20),
    @ContainerCrystal VARCHAR(10),
    @TotalPatientDiscount DECIMAL(18, 2),
    @CloseAdmission BIT,
    @OutputDate DATETIME,
    @IsCutAccount BIT,
    @OutputDiagnosis VARCHAR(10),
    @InitialDate DATETIME,
    @CutType INT,
    @CompanyType TINYINT,
    @ThirdPartyPatientId INT,
    @OperativeUnitId INT,
    @UserCode VARCHAR(20),
    @ListPortfolioAdvanceCrossingXml XML,
	--Salidas
    @StatusResult BIT OUTPUT,
    @MessageResult VARCHAR(MAX) OUTPUT,
    @Message VARCHAR(MAX) OUTPUT,
    @InvoiceId INT OUTPUT,
    @InvoiceNumber VARCHAR(20) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @Cero TINYINT = 0
	
	DECLARE @errorList VARCHAR(MAX) = '',
			@codesGenerate VARCHAR(MAX) = '',
			@resSaveAccReceivablePatient VARCHAR(10),
			@resSaveAccountReceivableEntity VARCHAR(10),
			@resAccReceivablePagare VARCHAR(10),
			@resGenerateTransfer VARCHAR(10)

	DECLARE @ListPortfolioAdvanceCrossing TABLE
	(
		RowId INT IDENTITY(1,1) PRIMARY KEY,
		Id INT, --portfolioadvanceid
		Code VARCHAR(20),
		CrossingValue DECIMAL(18, 2),
		CashReceiptDetailIdTmp INT NULL
	)

	DELETE FROM @ListPortfolioAdvanceCrossing
	INSERT INTO @ListPortfolioAdvanceCrossing
		SELECT t.x.value('Id[1]', 'INT'),
			t.x.value('Code[1]', 'VARCHAR(20)'),
			t.x.value('CrossingValue[1]', 'DECIMAL(18, 2)'),
			t.x.value('CashReceiptDetailIdTmp[1]', 'INT')
		FROM @ListPortfolioAdvanceCrossingXml.nodes('ListPortfolioAdvanceCrossing') t(x)

	BEGIN TRY
		SET @StatusResult = 0
		SET @MessageResult = ''
		SET @Message = ''
		SET @InvoiceId = 0
		SET @InvoiceNumber = ''
				
		DECLARE @StatusSequence BIT,
			@MessageSequence VARCHAR(255),
			@IdSequence INT

		EXEC [Portfolio].[GetPortfolioSequenceByTag] '682', 
													 @OperativeUnitId, 
													 --Salidas
													 @StatusSequence OUTPUT, 
													 @MessageSequence OUTPUT, 
													 @IdSequence OUTPUT

		IF @StatusSequence = 0
		BEGIN
			SET @Message = @MessageSequence
			RETURN
		END

		/*Variables generadas por Creacion de cuenta por cobrar a entidad (accountReceivableEntity)*/
		DECLARE @ArEntityStatusResult BIT = 0,
				@ArEntityMessage VARCHAR(255),
				@ArEntityPortfolioAccountReceivableId INT,
				@ArEntityPortfolioAccountReceivableCode VARCHAR(20)

		/*Variables cuenta por cobrar a paciente (accountReceivablePatient)*/
		DECLARE @ArPatientStatusResult BIT = 0,
				@ArPatientMessage VARCHAR(255),
				@ArPatientPortfolioAccountReceivableId INT,
				@ArPatientPortfolioAccountReceivableCode VARCHAR(20)

		/*Variables cuenta por cobrar anticipo paciente*/
		DECLARE @PtCodeResult INT,
				@PtMessageResult VARCHAR(MAX)

		/*Variables Pagare*/
		DECLARE @ArPagareResult BIT,
				@ArPagareMessage VARCHAR(MAX),
				@ArPagareAccountReceivableId INT,
				@ArPagareAccountReceivableCode VARCHAR(20)
		
		DECLARE @TotalCrossingValue DECIMAL(18, 2)
		SET @TotalCrossingValue = COALESCE((SELECT SUM(CrossingValue) FROM @ListPortfolioAdvanceCrossing), 0)

		DECLARE @__StatusResult BIT,
				@__MessageResult VARCHAR(MAX),
				@__CareGroupType TINYINT,
				@__LiquidationType TINYINT,
				@__InvoiceId INT,
				@__InvoiceNumber VARCHAR(20),
				@__InvoiceThirdPartySalesValue DECIMAL(18,2),
				@__FolioCareGroupId INT,
				@__FolioThirdPartyId INT,
				@__FolioTotalPatientWithDiscount DECIMAL(18, 2),
				@__MessageValidationContract VARCHAR(MAX),
				@__InvoiceCategory INT

		DECLARE @ResultALTERInvoice TABLE
		(
			RowId INT IDENTITY(1,1) PRIMARY KEY,
			StatusResult BIT,
			MessageResult VARCHAR(MAX),
			CareGroupType TINYINT NULL,
			LiquidationType TINYINT NULL,
			InvoiceId INT NULL,
			InvoiceNumber VARCHAR(20),
			InvoiceThirdPartySalesValue DECIMAL(18,2) NULL,
			FolioCareGroupId INT NULL,
			FolioThirdPartyId INT NULL,
			FolioTotalPatientWithDiscount DECIMAL(18, 2) NULL,
			MessageValidationContract VARCHAR(MAX),
			InvoiceCategory INT NULL
		)

		DELETE FROM @ResultALTERInvoice
		INSERT INTO @ResultALTERInvoice
			EXEC Billing.SP_ALTERInvoice @RevenueControlDetailId, 
										  @BillingAuthorizationId, 
										  @OperativeUnitId, 
										  @PatientCode, 
										  @AdmissionNumber, 
										  @ContainerCrystal, 
										  @UserCode, 
										  @TotalPatientDiscount, 
										  @TotalCrossingValue, 
										  @closeAdmission, 
										  @OutputDate, 
										  @IsCutAccount, 
										  @OutputDiagnosis, 
										  @InitialDate, 
										  @CutType

		SELECT TOP 1 @__StatusResult = StatusResult,
					 @__MessageResult = MessageResult,
					 @__CareGroupType = CareGroupType,
					 @__LiquidationType = LiquidationType,
					 @__InvoiceId = InvoiceId, 
					 @__InvoiceNumber = InvoiceNumber, 
					 @__InvoiceThirdPartySalesValue = InvoiceThirdPartySalesValue,
					 @__FolioCareGroupId = FolioCareGroupId,
					 @__FolioThirdPartyId = FolioThirdPartyId,
					 @__FolioTotalPatientWithDiscount = FolioTotalPatientWithDiscount,
					 @__MessageValidationContract = MessageValidationContract,
					 @__InvoiceCategory = InvoiceCategory
		FROM @ResultALTERInvoice

		IF @__StatusResult = 0 
		BEGIN
			SET @Message = @__MessageResult
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Portfolio.PortfolioAdvance pa
			JOIN 
			(
				SELECT	ipa.Id PortfolioAdvanceId, 
						SUM(ipa.CrossingValue) Value
				FROM @ListPortfolioAdvanceCrossing ipa
				GROUP BY ipa.Id
			) ipa ON pa.Id = ipa.PortfolioAdvanceId
			WHERE ipa.Value > pa.Balance
		)
		BEGIN
			SET @Message = 'El valor a cruzar supera el saldo del anticipo'
			RETURN
		END

		IF EXISTS (SELECT 1 FROM @ListPortfolioAdvanceCrossing) 
		BEGIN
			INSERT INTO Billing.InvoicePortfolioAdvance
				SELECT @__InvoiceId, Id, CrossingValue
				FROM @ListPortfolioAdvanceCrossing
		END

		SET @codesGenerate = 'Factura número: ' + @__InvoiceNumber

		IF @__LiquidationType = 1 
		BEGIN --Pago por Servicios
			--GENERAR CUENTA POR COBRAR - FACTURA (A LA ENTIDAD o PACIENTE (Si el caregroup es a particular))			
			DECLARE @continueWithOutAccountReceivable BIT = 0 --permite continuar sin generar cuenta por cobrar
			IF @__InvoiceThirdPartySalesValue <> 0 
			BEGIN						
				EXEC [Billing].[GenerateAccountReceivable] @__InvoiceNumber,
					@__InvoiceThirdPartySalesValue,
					@__FolioCareGroupId,
					@__FolioThirdPartyId,
					@__FolioTotalPatientWithDiscount,
					@OperativeUnitId,
					1,
					@__InvoiceCategory,
					0,
					-1,
					@__InvoiceId,
					@IdSequence,
					@UserCode,
					@ArEntityStatusResult OUTPUT,
					@ArEntityMessage OUTPUT,
					@ArEntityPortfolioAccountReceivableId OUTPUT,
					@ArEntityPortfolioAccountReceivableCode OUTPUT
					
				SET @codesGenerate = @codesGenerate + CHAR(13) + CHAR(10) + @ArEntityMessage
			END
			ELSE 
			BEGIN
				SET @continueWithOutAccountReceivable = 1
			END

			IF @continueWithOutAccountReceivable = 0 And @ArEntityStatusResult = 0 
			BEGIN
				SET @Message = @ArEntityMessage
				RETURN
			END

			--GENERACION DEL DOCUMENTO CONTABLE
			DECLARE @ThirdPartyNitName VARCHAR(300)
			IF @__FolioThirdPartyId Is Not NULL
				SELECT @ThirdPartyNitName = Concat(tp.Nit, ' - ', tp.[Name])
				FROM Common.ThirdParty tp With(Nolock)
				Where Id = @__FolioThirdPartyId

			DECLARE @IdJournalVoucher INT,
				@InvoiceJournalVoucherTypeId INT,
				@SettingBillingId INT

			SELECT @SettingBillingId = Id, 
				@InvoiceJournalVoucherTypeId = InvoiceJournalVoucherTypeId
			FROM Billing.SettingsBilling With(Nolock)
			Where IdOperatingUnit = @OperativeUnitId

			IF @SettingBillingId Is NULL 
			BEGIN
				SET @Message = 'No se encontraron parámetros de Facturación para la unidad operativa seleccionada'
				RETURN
			END

			SET @IdJournalVoucher = @InvoiceJournalVoucherTypeId

			DECLARE @TableJournalVoucher TABLE
			(
				IdJournalVoucher INT NOT NULL, 
				VoucherDate DateTime NOT NULL, 
				Imported BIT NOT NULL, 
				[Status] TINYINT NOT NULL, 
				Detail VARCHAR(500) NULL, 
				EntityCode VARCHAR(20) NULL, 
				EntityId INT NULL, 
				EntityName VARCHAR(250) NULL, 
				IsClosedYear BIT NOT NULL
			)
			DELETE FROM @TableJournalVoucher
			DECLARE @TableJournalVoucherDetail TABLE
			(
				IdMainAccount INT NOT NULL, 
				IdThirdParty INT NULL, 
				IdCostCenter INT NULL,
				DebitValue DECIMAL(18, 2) NOT NULL,
				CreditValue DECIMAL(18, 2) NOT NULL,
				Detail VARCHAR(MAX) NULL,
				IdRetention INT NULL,
				RetentionRate DECIMAL(5, 2) NULL,
				BaseValue DECIMAL(18, 0) NULL,
				BillingValue DECIMAL(18, 0) NULL
			)					
			DELETE FROM @TableJournalVoucherDetail

			/*Cabecera del comprobante contable*/
			INSERT INTO @TableJournalVoucher
			VALUES (
				@IdJournalVoucher,
				GetDate(),
				0,
				2,
				CASE WHEN @ThirdPartyNitName Is NULL 
					THEN 
						'Factura No. ' + @__InvoiceNumber 
					ELSE 
						'Factura No. ' + @__InvoiceNumber + ' - Tercero: (' + LTrim(RTrim(@ThirdPartyNitName)) + ')' 
				END,
				@__InvoiceNumber,
				@__InvoiceId,
				'Invoice',
				0
			)

			/*Detalles del Comprobante contable*/
			INSERT INTO @TableJournalVoucherDetail (IdMainAccount,IdThirdParty,IdCostCenter,CreditValue,DebitValue,Detail)
				EXEC Billing.SP_GenerateJournalVoucherDetails @RevenueControlDetailId, 
					@OperativeUnitId, 
					0, 
					@__InvoiceId

			DECLARE @JournalXml Xml = (
				SELECT *
				FROM @TableJournalVoucher As JournalVoucher
				Cross Apply @TableJournalVoucherDetail As JournalVoucherDetail
				For Xml Auto, Elements
			)					
			DECLARE @TableResultJournal TABLE(CodeMessage VARCHAR(20), [Message] VARCHAR(MAX), IdJournalVoucher INT)
					
			INSERT INTO @TableResultJournal
			EXEC [GeneralLedger].[SP_SaveJournalVoucher] @JournalXml, @UserCode
					
			IF EXISTS (SELECT CodeMessage FROM @TableResultJournal Where CodeMessage <> @Cero) 
			BEGIN
				DECLARE @MsgErrorJournalVoucher VARCHAR(MAX) = ''
				SELECT @MsgErrorJournalVoucher += [Message] + Char(13) + Char(10) FROM @TableResultJournal Where CodeMessage <> 0
				
				SET @Message = 'Ocurrieron errores al intentar Generar el comprobante contable: ' + @MsgErrorJournalVoucher
				RETURN
			END

			DECLARE @JournalVoucherId INT
			SELECT Top 1 @JournalVoucherId = IdJournalVoucher FROM @TableResultJournal

			SELECT @codesGenerate = @codesGenerate + CHAR(13) + CHAR(10) + 'Se generó el Comprobante contable ' + CAST(jv.Consecutive AS VARCHAR(30)) + ' de tipo ' + jvt.Code + ' - ' + jvt.Name
			FROM GeneralLedger.JournalVouchers jv
			Join GeneralLedger.JournalVoucherTypes jvt On jv.IdJournalVoucher = jvt.Id
			Where jv.Id = @JournalVoucherId

			Update Billing.Invoice SET JournalVoucherId = @JournalVoucherId Where Id = @__InvoiceId
		END
		
		--SOLO SI HAY VALOR A PACIENTE SE GENERA LA CUENTA POR COBRAR A ESTE
		IF @__FolioTotalPatientWithDiscount <> 0 Or @__CareGroupType = 3 
		BEGIN
			DECLARE @PagareValue DECIMAL(18, 2) = 0
			IF @__CareGroupType <> 3
				SET @PagareValue = @__FolioTotalPatientWithDiscount - @TotalCrossingValue

			--CUENTA POR COBRAR PACIENTE SOLO SI SE CRUZA ALGÚN VALOR
			IF @TotalCrossingValue > 0 
			BEGIN				
				IF @__CareGroupType <> 3 
				BEGIN								
					EXEC [Billing].[GenerateAccountReceivable] @__InvoiceNumber,
						@__InvoiceThirdPartySalesValue,
						@__FolioCareGroupId,
						@__FolioThirdPartyId,
						@__FolioTotalPatientWithDiscount,
						@OperativeUnitId,
						2,
						@__InvoiceCategory,
						@PagareValue,
						@ThirdPartyPatientId,
						@__InvoiceId,
						@IdSequence,
						@UserCode,

						@ArPatientStatusResult OUTPUT,
						@ArPatientMessage OUTPUT,
						@ArPatientPortfolioAccountReceivableId OUTPUT,
						@ArPatientPortfolioAccountReceivableCode OUTPUT

					IF @ArPatientStatusResult = 0 
					BEGIN
						SET @Message = @ArPatientMessage
						RETURN
					END
						
					SET @codesGenerate = @codesGenerate + CHAR(13) + CHAR(10) + @ArPatientMessage
				END

				DECLARE @AccountReceivableId INT

				IF @__CareGroupType = 3 
				BEGIN
					SET @AccountReceivableId = @ArEntityPortfolioAccountReceivableId
				END
				ELSE 
				BEGIN
					SET @AccountReceivableId = @ArPatientPortfolioAccountReceivableId
				END

				IF @__LiquidationType = 1 
				BEGIN --Pago por Servicios
					EXEC [Portfolio].[SP_GeneratePortfolioTransfer] 
						@ListPortfolioAdvanceCrossingXml,
						@OperativeUnitId,
						@UserCode,
						@AccountReceivableId,
						@CompanyType,
						--Salidas
						@PtCodeResult OUTPUT,
						@PtMessageResult OUTPUT
					
					IF @PtCodeResult <> 0 
					BEGIN
						SET @Message = @PtMessageResult
						RETURN
					END

					SET @codesGenerate = @codesGenerate + CHAR(13) + CHAR(10) + @PtMessageResult
				END
			END
								
			DECLARE @totalPatienWithDiscount DECIMAL(18, 2) = @__FolioTotalPatientWithDiscount
			IF @__CareGroupType <> 3 
			BEGIN
				IF @TotalCrossingValue > @totalPatienWithDiscount 
				BEGIN
					SET @Message = 'El total a cruzar no debe superar al valor a pagar por el paciente'						
					RETURN
				END
				ELSE IF @TotalCrossingValue < @totalPatienWithDiscount 
				BEGIN
					--Cuenta por cobrar de tipo pagaré
					EXEC [Portfolio].[GetPortfolioSequenceByTag] '1510', @OperativeUnitId, @StatusSequence OUTPUT, @MessageSequence OUTPUT, @IdSequence OUTPUT

					IF @StatusSequence = 0 
					BEGIN
						SET @Message = @MessageSequence
						RETURN
					END

					EXEC [Billing].[GenerateAccountReceivable] @__InvoiceNumber,
						@__InvoiceThirdPartySalesValue,
						@__FolioCareGroupId,
						@__FolioThirdPartyId,
						@totalPatienWithDiscount,
						@OperativeUnitId,
						3,
						@__InvoiceCategory,
						@PagareValue,
						@ThirdPartyPatientId,
						@__InvoiceId,
						@IdSequence,
						@UserCode,

						@ArPagareResult OUTPUT,
						@ArPagareMessage OUTPUT,
						@ArPagareAccountReceivableId OUTPUT,
						@ArPagareAccountReceivableCode OUTPUT

					IF @ArPagareResult = 0 
					BEGIN
						SET @Message = @ArPagareMessage
						RETURN
					END
						
					SET @codesGenerate = @codesGenerate + CHAR(13) + CHAR(10) + @ArPagareMessage
				END
			END
		END

		IF @errorList <> '' 
		BEGIN
			SET @Message = @errorList
			RETURN
		END

		SET @StatusResult = 1
		SET @MessageResult = @codesGenerate
		SET @Message = @__MessageValidationContract
		SET @InvoiceId = @__InvoiceId
		SET @InvoiceNumber = @__InvoiceNumber

	END TRY
	BEGIN CATCH
		SET @StatusResult = 0
		SET @MessageResult = ''
		SET @Message = (SELECT Error_Message())
		SET @InvoiceId = 0
		SET @InvoiceNumber = ''
		RETURN
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera la factura y las cuentas por cobrar (a entidad y a paciente) a partir de un folio de liquidación. Orquesta el proceso completo de facturación: obtiene el consecutivo de factura mediante [Portfolio].[GetPortfolioSequenceByTag], ejecuta la lógica central de facturación con [Billing].[SP_ALTERInvoice], y aplica cruces de anticipos del portafolio del paciente contra el valor de la factura. Cubre escenarios de corte de cuenta, cierre de admisión, descuentos al paciente y ventas a terceros, retornando el identificador y número de factura generados, junto con el estado y mensaje del resultado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'ALTERInvoice';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'ALTERInvoice';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Orquesta la liquidación de un folio: genera la factura, aplica cruces de anticipos, crea cuentas por cobrar (a entidad/paciente/pagaré), registra el comprobante contable y dispara el traslado de anticipos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ALTERInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una secuencia de portafolio configurada con tag ''682'' para la unidad operativa (GetPortfolioSequenceByTag); de lo contrario aborta.; Si el tipo de liquidación es ''Pago por Servicios'' (=1), debe existir configuración en Billing.SettingsBilling para la unidad operativa.; La suma de CrossingValue por anticipo no puede exceder el Balance del PortfolioAdvance correspondiente.; El total cruzado (@TotalCrossingValue) no puede superar el valor del paciente con descuento cuando CareGroupType <> 3.; Si CareGroupType <> 3 y queda saldo pendiente del paciente (cruzado < total), debe existir secuencia de portafolio con tag ''1510'' para emitir pagaré.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ALTERInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La factura sólo se vincula a un comprobante contable cuando la liquidación es ''Pago por Servicios'' (LiquidationType=1).; Para CareGroupType=3 (paciente particular), nunca se calcula PagareValue ni se emite CxC tipo paciente/pagaré; la CxC de entidad se reutiliza como referencia para el traslado.; Los anticipos cruzados nunca exceden el saldo (Balance) disponible del PortfolioAdvance.; El cruce total con anticipos nunca puede superar el valor a pagar por el paciente cuando CareGroupType<>3.; Toda excepción no manejada se captura en CATCH devolviendo StatusResult=0, InvoiceId=0 e InvoiceNumber vacío.; El detalle del comprobante contable se etiqueta con ''Invoice'' y referencia el número e Id de la factura; si hay tercero, incluye su Nit y nombre concatenados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ALTERInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Folio a liquidar; Cuenta por cobrar a entidad; Cuenta por cobrar a paciente; Pagaré; Anticipo de cartera; Cruce de anticipos; Comprobante contable; Tercero (Nit); Unidad operativa; Tipo de liquidación (Pago por Servicios); CareGroup particular; Descuento al paciente; Diagnóstico de salida; Corte de cuenta', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ALTERInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Billing.InvoicePortfolioAdvance: Cuando hay registros en @ListPortfolioAdvanceCrossing, se inserta un renglón por anticipo con (InvoiceId, PortfolioAdvanceId, CrossingValue) vinculando los anticipos cruzados a la factura generada.; [UPDATE] Billing.Invoice: Tras generar exitosamente el comprobante contable (LiquidationType=1), se actualiza JournalVoucherId de la factura recién creada con el Id retornado por SP_SaveJournalVoucher.; [RETURN_RESULT] OUTPUT: Devuelve @StatusResult=1 y en @MessageResult el listado de códigos generados (factura, comprobante contable, cuentas por cobrar) sólo si todas las etapas tuvieron éxito; ante cualquier fallo retorna con StatusResult=0 y mensaje del error.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ALTERInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @__LiquidationType = 1 (Pago por Servicios) y @__InvoiceThirdPartySalesValue <> 0 → Genera cuenta por cobrar a la entidad (tipo=1) vía Billing.GenerateAccountReceivable. else Si el valor a tercero es 0, omite la generación de la CxC a entidad (continueWithOutAccountReceivable=1).; si @__LiquidationType = 1 → Construye cabecera y detalle del comprobante contable, lo guarda con GeneralLedger.SP_SaveJournalVoucher y vincula el JournalVoucherId a la factura.; si @__FolioTotalPatientWithDiscount <> 0 OR @__CareGroupType = 3 → Procesa la liquidación al paciente: posibles CxC paciente, traslado de anticipos y/o pagaré. else No genera CxC ni pagaré para el paciente.; si @__CareGroupType <> 3 y @TotalCrossingValue > 0 → Genera CxC al paciente (tipo=2) por el valor cruzado. else Si CareGroupType=3 reutiliza la CxC de entidad como AccountReceivableId para el traslado.; si @__CareGroupType <> 3 y @TotalCrossingValue < @totalPatienWithDiscount → Obtiene secuencia tag ''1510'' y genera CxC tipo Pagaré (tipo=3) por @PagareValue. else Si TotalCrossingValue > totalPatienWithDiscount aborta con error ''El total a cruzar no debe superar al valor a pagar por el paciente''.; si EXISTS anticipos cuya suma de CrossingValue supera el Balance del PortfolioAdvance → Aborta con mensaje ''El valor a cruzar supera el saldo del anticipo''.; si Resultado de SP_SaveJournalVoucher contiene CodeMessage <> 0 → Aborta con mensaje ''Ocurrieron errores al intentar Generar el comprobante contable: ...''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ALTERInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetPortfolioSequenceByTag; Billing.SP_ALTERInvoice; Billing.GenerateAccountReceivable; Billing.SP_GenerateJournalVoucherDetails; GeneralLedger.SP_SaveJournalVoucher; Portfolio.SP_GeneratePortfolioTransfer', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ALTERInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioAdvance; Common.ThirdParty; Billing.SettingsBilling; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ALTERInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ALTERInvoice';
-- GO
