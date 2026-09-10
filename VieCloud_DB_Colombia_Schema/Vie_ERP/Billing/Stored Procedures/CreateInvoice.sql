-- =============================================
-- Author:		Diego A. Roldan
-- Create date: 2015-09-24
-- Description:	Genera la factura y las cuentas por cobrar de un folio a liquidar
-- =============================================

CREATE PROCEDURE [Billing].[CreateInvoice]
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

	DECLARE @InvoiceJournalVoucherTypeId INT,
			@JournalXml Xml,
			@JournalVoucherId INT

	DECLARE @ListPortfolioAdvanceCrossing TABLE
	(
		RowId INT IDENTITY(1,1) PRIMARY KEY,
		Id INT, --portfolioadvanceid
		Code VARCHAR(20),
		CrossingValue DECIMAL(18, 2),
		CashReceiptDetailIdTmp INT NULL
	)

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

	DECLARE @TableResultJournal TABLE
	(
		CodeMessage VARCHAR(20), 
		[Message] VARCHAR(MAX), 
		IdJournalVoucher INT
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

		-- Se obtiene el tipo de comprobante contable
		SELECT @InvoiceJournalVoucherTypeId = InvoiceJournalVoucherTypeId
		FROM Billing.SettingsBilling With(Nolock)
		Where IdOperatingUnit = @OperativeUnitId
				
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

		DECLARE @ResultCreateInvoice TABLE
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

		DELETE FROM @ResultCreateInvoice
		INSERT INTO @ResultCreateInvoice
			EXEC Billing.SP_CreateInvoice @RevenueControlDetailId, 
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
		FROM @ResultCreateInvoice

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

			/*Detalles del Comprobante contable*/
			INSERT INTO @TableJournalVoucherDetail (IdMainAccount,IdThirdParty,IdCostCenter,CreditValue,DebitValue,Detail)
				EXEC Billing.SP_GenerateJournalVoucherDetails @RevenueControlDetailId, 
					@OperativeUnitId, 
					0, 
					@__InvoiceId

			SET @JournalXml = 
			(
				SELECT *
				FROM 
				(
					SELECT	@InvoiceJournalVoucherTypeId AS IdJournalVoucher,
							Common.Getdate() AS VoucherDate,
							0 AS Imported,
							2 AS Status,
							CASE WHEN @ThirdPartyNitName Is NULL 
								THEN 
									'Factura No. ' + @__InvoiceNumber 
								ELSE 
									'Factura No. ' + @__InvoiceNumber + ' - Tercero: (' + LTrim(RTrim(@ThirdPartyNitName)) + ')' 
							END AS Detail,
							@__InvoiceNumber AS EntityCode,
							@__InvoiceId AS EntityId,
							'Invoice' AS EntityName,
							0 AS IsClosedYear
				) As JournalVoucher
				CROSS APPLY @TableJournalVoucherDetail As JournalVoucherDetail
				For Xml Auto, Elements
			)
					
			INSERT INTO @TableResultJournal
				EXEC [GeneralLedger].[SP_SaveJournalVoucher] @JournalXml, @UserCode
					
			IF EXISTS (SELECT CodeMessage FROM @TableResultJournal Where CodeMessage <> @Cero) 
			BEGIN
				SELECT @errorList += [Message] + Char(13) + Char(10) FROM @TableResultJournal Where CodeMessage <> 0
				
				SET @Message = 'Ocurrieron errores al intentar Generar el comprobante contable: ' + @errorList
				RETURN
			END

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

					--Si no es pago por servicios, se crea la contabilización del pagaré
					IF @__LiquidationType <> 1
					BEGIN
						SET @JournalXml = 
						(
							SELECT *
							FROM 
							(
								SELECT	@InvoiceJournalVoucherTypeId AS IdJournalVoucher,
										Common.Getdate() AS VoucherDate,
										0 AS Imported,
										2 AS Status,
										'Pagaré Asociado al Control de Servicio No. ' + @__InvoiceNumber AS Detail,
										@__InvoiceNumber AS EntityCode,
										@__InvoiceId AS EntityId,
										'Invoice' AS EntityName,
										0 AS IsClosedYear
							) As JournalVoucher
							CROSS APPLY 
							(
									SELECT	ma.Id IdMainAccount,
											IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL) IdThirdParty,
											IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL) IdCostCenter,
											ar.Value DebitValue,
											0 CreditValue
									FROM Portfolio.AccountReceivable ar
									JOIN GeneralLedger.MainAccounts ma ON ar.AccountWithoutRadicateId = ma.Id
									WHERE ar.InvoiceId = @__InvoiceId AND ar.AccountReceivableType = 4
								UNION ALL
									SELECT	ma.Id IdMainAccount,
											IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL) IdThirdParty,
											IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL) IdCostCenter,
											0 DebitValue,
											ar.Value CreditValue
									FROM Portfolio.AccountReceivable ar
									JOIN Billing.SettingsBilling sb ON ar.OperatingUnitId = sb.IdOperatingUnit
									JOIN Treasury.CashReceiptConcepts crc ON sb.CapitedPatientAdvanceCashReceiptConceptId = crc.Id
									JOIN GeneralLedger.MainAccounts ma ON crc.IdMainAccount = ma.Id
									WHERE ar.InvoiceId = @__InvoiceId AND ar.AccountReceivableType = 4
							) As JournalVoucherDetail
							For Xml Auto, Elements
						)
					
						INSERT INTO @TableResultJournal
							EXEC [GeneralLedger].[SP_SaveJournalVoucher] @JournalXml, @UserCode
					
						IF EXISTS (SELECT CodeMessage FROM @TableResultJournal Where CodeMessage <> @Cero) 
						BEGIN
							SELECT @errorList += [Message] + Char(13) + Char(10) FROM @TableResultJournal Where CodeMessage <> 0
				
							SET @Message = 'Ocurrieron errores al intentar Generar el comprobante contable: ' + @errorList
							RETURN
						END

						SELECT Top 1 @JournalVoucherId = IdJournalVoucher FROM @TableResultJournal

						SELECT @codesGenerate = @codesGenerate + CHAR(13) + CHAR(10) + 'Se generó el Comprobante contable ' + CAST(jv.Consecutive AS VARCHAR(30)) + ' de tipo ' + jvt.Code + ' - ' + jvt.Name
						FROM GeneralLedger.JournalVouchers jv
						Join GeneralLedger.JournalVoucherTypes jvt On jv.IdJournalVoucher = jvt.Id
						Where jv.Id = @JournalVoucherId

						Update Billing.Invoice SET JournalVoucherId = @JournalVoucherId Where Id = @__InvoiceId
					END
				END
			END
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la factura y las cuentas por cobrar de un paciente a partir de un folio de liquidación. Orquesta el proceso completo de facturación: consulta la configuración contable por unidad operativa en SettingsBilling, obtiene la secuencia de cartera, ejecuta el procedimiento interno SP_CreateInvoice para crear la factura con su número y categoría, y procesa el cruce de anticipos del paciente recibidos como XML. Devuelve el identificador y número de factura generada, el estado del resultado y mensajes de error o éxito, permitiendo además indicar si se cierra la admisión, el diagnóstico de egreso, descuento al paciente, tipo de corte y fecha de corte o salida.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'CreateInvoice';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'CreateInvoice';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor total cruzado de anticipos nunca puede exceder el saldo de cada anticipo individual ni el total a pagar por el paciente (cuando CareGroupType <> 3).; El pagaré solo se genera por la diferencia entre lo que paga el paciente y lo cruzado con anticipos (FolioTotalPatientWithDiscount - TotalCrossingValue), y solo cuando el cruce es estrictamente menor al valor del paciente.; Si CareGroupType = 3 (particular) no se genera cuenta por cobrar a paciente separada; se reutiliza la cuenta por cobrar de la entidad para el traslado de anticipos.; Cada comprobante contable generado queda enlazado a la factura mediante Billing.Invoice.JournalVoucherId.; Toda la operación se ejecuta dentro de TRY/CATCH; ante cualquier excepción se devuelve StatusResult=0 y los identificadores de salida en cero/vacío.; El detalle del comprobante contable de la factura se construye vía SP_GenerateJournalVoucherDetails y solo se genera cuando la liquidación es Pago por Servicios (@__LiquidationType = 1).; El comprobante contable del pagaré se construye con dos partidas: débito sobre AccountWithoutRadicateId de la AR tipo 4 y crédito sobre la cuenta del concepto de anticipo paciente capitado configurado en SettingsBilling.; El tipo de comprobante contable usado es InvoiceJournalVoucherTypeId tomado de Billing.SettingsBilling para la unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'CreateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'factura; folio a liquidar; cuenta por cobrar a entidad; cuenta por cobrar a paciente; pagaré; anticipo de cartera; cruce de anticipos; comprobante contable; tipo de liquidación (pago por servicios); grupo de atención (CareGroup); particular; tercero; unidad operativa; secuencia de cartera; descuento al paciente; control de servicio', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'CreateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @__LiquidationType = 1 (Pago por Servicios) y @__InvoiceThirdPartySalesValue <> 0 → Genera cuenta por cobrar a la entidad (tipo 1) vía Billing.GenerateAccountReceivable else Si valor a tercero es 0 se permite continuar sin generar cuenta por cobrar a entidad; si @__LiquidationType = 1 (Pago por Servicios) → Genera detalles del comprobante contable de la factura, lo guarda vía GeneralLedger.SP_SaveJournalVoucher y actualiza Billing.Invoice.JournalVoucherId; si @__FolioTotalPatientWithDiscount <> 0 OR @__CareGroupType = 3 → Procesa lógica de cuenta por cobrar a paciente y/o pagaré; si @TotalCrossingValue > 0 y @__CareGroupType <> 3 → Genera cuenta por cobrar al paciente (tipo 2) y luego ejecuta Portfolio.SP_GeneratePortfolioTransfer si es Pago por Servicios; si @__CareGroupType = 3 → Usa la cuenta por cobrar de la entidad como AccountReceivableId para el cruce/traslado de anticipos else Usa la cuenta por cobrar del paciente; si @TotalCrossingValue > @__FolioTotalPatientWithDiscount (cuando CareGroupType <> 3) → Aborta con mensaje ''El total a cruzar no debe superar al valor a pagar por el paciente''; si @TotalCrossingValue < @__FolioTotalPatientWithDiscount (cuando CareGroupType <> 3) → Obtiene secuencia con tag ''1510'' y genera cuenta por cobrar tipo pagaré (tipo 3) por @PagareValue = TotalPatientWithDiscount - TotalCrossingValue; si Cuenta por cobrar tipo pagaré creada y @__LiquidationType <> 1 → Genera comprobante contable adicional para el pagaré usando Portfolio.AccountReceivable con AccountReceivableType=4 y la cuenta del concepto CapitedPatientAdvanceCashReceiptConceptId, y actualiza Billing.Invoice.JournalVoucherId; si EXISTS registro en @TableResultJournal con CodeMessage <> 0 tras SP_SaveJournalVoucher → Aborta con mensaje ''Ocurrieron errores al intentar Generar el comprobante contable: '' + lista de errores; si Algún anticipo cruzado supera el saldo disponible (ipa.Value > pa.Balance) → Aborta con mensaje ''El valor a cruzar supera el saldo del anticipo''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'CreateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetPortfolioSequenceByTag; Billing.SP_CreateInvoice; Billing.GenerateAccountReceivable; Billing.SP_GenerateJournalVoucherDetails; GeneralLedger.SP_SaveJournalVoucher; Portfolio.SP_GeneratePortfolioTransfer', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'CreateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.SettingsBilling; Portfolio.PortfolioAdvance; Common.ThirdParty; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes; Portfolio.AccountReceivable; GeneralLedger.MainAccounts; Treasury.CashReceiptConcepts', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'CreateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'CreateInvoice';
-- GO
