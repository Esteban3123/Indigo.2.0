-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-12-29
-- Description:	Generar la cuenta por cobrar por tipo
-- =============================================
CREATE PROCEDURE [Billing].[SP_CreateAccountReceivable_Output]
	@CompanyType TINYINT,
	@OperativeUnitId INT,
	@RevenueControlDetailId INT,
	@InvoiceId INT,
	@FolioType INT,
	@LiquidationType INT,
	@AccountReceivableType TINYINT,
	@ThirdPartyId INT,
	@Value DECIMAL(18,2),
	@Balance DECIMAL(18,2),
	@ListPortfolioAdvanceCrossingXml XML,
	@UserCode VARCHAR(20),
	@CurrencyId INT =null,
	@TRMValue Decimal(20,5)=null,
	--Salidas
	@ResultStatus BIT OUTPUT,
	@ResultMessage VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @FormId VARCHAR(5),
			@IsManual BIT = 0,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			-------------------------------------------------------------------
			@PortfolioAccountReceivableId INT,
			@PortfolioAccountReceivableCode VARCHAR(20),
			-------------------------------------------------------------------
			@JournalXml XML,
			@JournalVoucherId INT,
			-------------------------------------------------------------------
			@Message VARCHAR(MAX),
			---------------------------
			@OfficialCurrencyId as INT

	DECLARE @TableJournalVoucherDetail TABLE
	(
		IdMainAccount INT NOT  NULL, 
		IdThirdParty INT, 
		IdCostCenter INT,
		DebitValue DECIMAL(21, 5) NOT NULL,
		CreditValue DECIMAL(21, 5) NOT NULL,
		Detail VARCHAR(MAX),
		IdRetention INT,
		RetentionRate DECIMAL(5, 2),
		BaseValue DECIMAL(18, 2),
		BillingValue DECIMAL(18, 2)
	)

	DECLARE @TableResultJournal TABLE
	(
		CodeMessage VARCHAR(20), 
		[Message] VARCHAR(MAX), 
		IdJournalVoucher INT
	)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		SET @ResultMessage = ''
		SET @OfficialCurrencyId =(	SELECT TOP 1 cs.OfficialCurrencyId
									FROM GeneralLedger.CompanySettings cs WITH(NOLOCK))

		SET @FormId = IIF(@AccountReceivableType = 4, '1510', '682')
		EXEC Common.SP_GetSequence 160, @FormId, @OperativeUnitId, NULL, NULL, @IsManual OUT, @PortfolioAccountReceivableCode OUT, @Code_Output OUT, @Message_Output OUT
		IF @Code_Output <> 0
		BEGIN
			SELECT	@ResultStatus = CONVERT(BIT, 0), 
					@ResultMessage = REPLACE(@Message_Output, '{0}', IIF(@AccountReceivableType = 4, 'Pagarés', 'Cuentas Por Cobrar'))
			RETURN
		END

		INSERT INTO [Portfolio].[AccountReceivable]
		(
			[Code],[AccountReceivableType],[ThirdPartyId],[CustomerId],[InvoiceId],[InvoiceNumber],[AccountReceivableDate],
			[Term],[ExpiredDate],[Observations],[PortfolioStatus],[OpeningBalance],[PaymentAgreement],[RegistrationAdjusted],
			[InvoiceCategoryId],[CostCenterId],[OperatingUnitId],[MainAccountWithoutFilingId],[NumberShares],[Value],[Balance],
			[Status],[AccountWithoutRadicateId],[AccountRadicateId],[AccountObjectionRemediedId],[AccountConciliationId],
			[AccountLegalCollectionId],[AccountDebtorOrder],[AccountCreditorOrder],[CreditProvisionAccountId],[DebitProvisionAccountId],
			[CreditAccountDeteriorationId],[DebitAccountDeteriorationId],[ReversalAccountDeteriorationId],
			[PreviousPeriodReversalAccountDeteriorationId],[AccountHardCollectionId],AffectBudget,BudgetId,CareGroupId,
			[CreationUser],[CreationDate],[CurrencyId],[TRMValue]
		)
		SELECT	@PortfolioAccountReceivableCode,@AccountReceivableType,@ThirdPartyId,c.Id,i.Id,i.InvoiceNumber,i.InvoiceDate,
				cg.InvoiceDeadlines,i.InvoiceExpirationDate,'',IIF(@FolioType = 3, 3, 1),0,0,0,
				i.InvoiceCategoryId,cg.CostCenterId,i.OperatingUnitId,
					CASE
					WHEN @FolioType = 3 AND cg.CareGroupType <> 3 AND rcd.IsMasterAccount = 4 then cas.AccountRecoveryFeeId
					WHEN @FolioType = 3 THEN cas.AccountParticularId
					WHEN @AccountReceivableType IN (4, 6) THEN cas.AccountRecoveryFeeId
					ELSE cas.AccountWithoutRadicateId
				END,1,@Value,@Balance,
				2,CASE 
					WHEN @FolioType = 3 AND cg.CareGroupType <> 3 AND rcd.IsMasterAccount = 4 then cas.AccountRecoveryFeeId
					WHEN @FolioType = 3 THEN cas.AccountParticularId
					WHEN @AccountReceivableType IN (4, 6) THEN cas.AccountRecoveryFeeId
					ELSE cas.AccountWithoutRadicateId
				END,cas.AccountRadicateId,cas.AccountObjectionRemediedId,cas.AccountConciliationId,
				cas.AccountLegalCollectionId,cas.AccountDebitOrderId,cas.AccountCreditOrderId,cas.CreditProvisionAccountId,cas.DebitProvisionAccountId,
				cas.CreditAccountDeteriorationId,cas.DebitAccountDeteriorationId,cas.ReversalAccountDeteriorationId,
				cas.PreviousPeriodReversalAccountDeteriorationId,cas.AccountHardCollectionId,cg.AffectBudget,CASE cg.AffectBudget
					WHEN 1 THEN
						CASE 
							WHEN @AccountReceivableType IN (4, 6) THEN cg.PromissoryNoteBudgetId
							ELSE cg.BillingBudgetId
						END
					ELSE NULL
				END,cg.Id,i.InvoicedUser,i.InvoicedDate,ISNULL(i.CurrencyId,@OfficialCurrencyId),@TRMValue
		FROM Billing.Invoice i WITH (NOLOCK)
		JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON i.RevenueControlDetailId = rcd.Id
		JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
		JOIN Contract.ContractAccountingStructure cas WITH (NOLOCK) ON cg.ContractAccountingStructureId = cas.Id
		LEFT JOIN Common.Customer c WITH (NOLOCK) ON @ThirdPartyId = c.ThirdPartyId
		WHERE i.Id = @InvoiceId

		SET @PortfolioAccountReceivableId = SCOPE_IDENTITY()

		INSERT INTO Portfolio.AccountReceivableShare
		(
			AccountReceivableId,Number,ExpiredDate,[Value],Balance,DebitValue,CreditValue,TransferValue,PaymentValue,
			CrossingValue,InterestValue,SurchargesValue,CapitalRepaymentAgreement,FinancialInterest,RepaymentAgreementInterest
		)
		SELECT	@PortfolioAccountReceivableId,1,ar.ExpiredDate,ar.Value,ar.Balance,0,0,0,0,
				0,0,0,0,0,0
		FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
		WHERE ar.Id = @PortfolioAccountReceivableId

		INSERT INTO Portfolio.AccountReceivableAccounting
		(
			AccountReceivableId,MainAccountId,ThirdPartyId,CostCenterId,[Value],Balance
		)
		SELECT	@PortfolioAccountReceivableId,ma.Id,
				IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL),
				IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL),
				ar.Value,ar.Balance
		FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ar.AccountWithoutRadicateId = ma.Id
		WHERE ar.Id = @PortfolioAccountReceivableId

		SET @Message = CONCAT('Código Cuenta por Cobrar ', CASE @AccountReceivableType
				WHEN 2 THEN IIF(@FolioType = 3, 'al Paciente: ', 'a la Entidad: ')
				WHEN 4 THEN 'tipo Pagaré: '
				WHEN 6 THEN 'al Paciente: '
			END,  @PortfolioAccountReceivableCode)
		SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)

		----------------------------------------- RECONOCIMIENTO PRESUPUESTAL -----------------------------------------
		
		EXEC [Portfolio].[SP_GenerateRecognitionByAccountReceivableId_Output] @OperativeUnitId, @PortfolioAccountReceivableId, @UserCode, @Code_Output OUT, @Message_Output OUT

		IF @Code_Output <> 0
		BEGIN
			SELECT	@ResultStatus = CONVERT(BIT, 0), 
					@ResultMessage = ISNULL(@Message_Output, 'No se pudo generar el reconocimiento presupuestal')
			RETURN
		END

		SET @Message = ISNULL(@Message_Output, '')
		SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)

		--------------------------------------------  COMPROBANTE CONTABLE --------------------------------------------

		IF @AccountReceivableType = 2
		BEGIN

			Declare @ApplyElectronicSalesTicket BIT,
					@IsElectronicBillerThirdParty BIT

			SELECT	@ApplyElectronicSalesTicket = sb.ApplyElectronicSalesTicket,
					@IsElectronicBillerThirdParty = tp.ElectronicBiller
			FROM Billing.Invoice i WITH (NOLOCK)
			JOIN Common.ThirdParty tp WITH (NOLOCK) ON i.ThirdPartyId = tp.Id
			JOIN Billing.SettingsBilling sb WITH (NOLOCK) ON i.OperatingUnitId = sb.IdOperatingUnit
			WHERE i.Id = @InvoiceId

			INSERT INTO @TableJournalVoucherDetail (IdMainAccount,IdThirdParty,IdCostCenter,CreditValue,DebitValue,Detail)
				EXEC Billing.SP_GenerateJournalVoucherDetails @RevenueControlDetailId, 
					@OperativeUnitId, 
					0, 
					@InvoiceId

			SET @JournalXml = 
			(
				SELECT *
				FROM 
				(
					SELECT TOP 1
							IIF(@ApplyElectronicSalesTicket = 1 AND @IsElectronicBillerThirdParty = 0, 
								sb.AccountingVoucherGenerationId ,
								sb.InvoiceJournalVoucherTypeId) AS IdJournalVoucher,
							i.InvoiceDate AS VoucherDate,
							0 AS Imported,
							2 AS Status,
							CONCAT('Factura No. ', i.InvoiceNumber, ' - Tercero: (', tp.Nit, ' - ', tp.Name, ')') AS Detail,
							i.InvoiceNumber AS EntityCode,
							@InvoiceId AS EntityId,
							'Invoice' AS EntityName,
							0 AS IsClosedYear,
							i.CurrencyId
					FROM Billing.Invoice i WITH (NOLOCK)
					JOIN Common.ThirdParty tp WITH (NOLOCK) ON i.ThirdPartyId = tp.Id
					JOIN Billing.SettingsBilling sb WITH (NOLOCK) ON i.OperatingUnitId = sb.IdOperatingUnit
					WHERE i.Id = @InvoiceId
				) As JournalVoucher
				CROSS APPLY @TableJournalVoucherDetail As JournalVoucherDetail
				For Xml Auto, Elements
			)
			--SELECT * FROM @TableJournalVoucherDetail	 
			INSERT INTO @TableResultJournal
				EXEC [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement] @JournalXml, @UserCode
					
			IF EXISTS (SELECT CodeMessage FROM @TableResultJournal Where CodeMessage <> 0) 
			BEGIN
				SELECT @Message = [Message] FROM @TableResultJournal Where CodeMessage <> 0
				
				SELECT	@ResultStatus = CONVERT(BIT, 0), 
						@ResultMessage = 'Ocurrieron errores al intentar Generar el comprobante contable: ' + ISNULL(@Message, 'No se pudo generar el comprobante contable')
				RETURN
			END

			SELECT TOP 1 @JournalVoucherId = IdJournalVoucher FROM @TableResultJournal

			UPDATE Billing.Invoice SET JournalVoucherId = @JournalVoucherId WHERE Id = @InvoiceId

			SELECT @Message = CONCAT('Se generó el Comprobante contable de tipo ', jvt.Code, ' - ', jvt.Name)
			FROM Billing.Invoice i
			JOIN Billing.SettingsBilling sb ON i.OperatingUnitId = sb.IdOperatingUnit
			JOIN GeneralLedger.JournalVoucherTypes jvt On jvt.Id = IIF(@ApplyElectronicSalesTicket = 1 AND @IsElectronicBillerThirdParty = 0, sb.AccountingVoucherGenerationId, sb.InvoiceJournalVoucherTypeId)
			Where i.Id = @InvoiceId

			SELECT @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END
		ELSE IF @LiquidationType <> 1 AND @AccountReceivableType = 4
		BEGIN
			SET @JournalXml = 
			(
				SELECT *
				FROM 
				(
					SELECT TOP 1
							sb.InvoiceJournalVoucherTypeId AS IdJournalVoucher,
							i.InvoiceDate AS VoucherDate,
							0 AS Imported,
							2 AS Status,
							CONCAT('Pagaré Asociado al Control de Servicio No. ', i.InvoiceNumber) AS Detail,
							i.InvoiceNumber AS EntityCode,
							@InvoiceId AS EntityId,
							'Invoice' AS EntityName,
							0 AS IsClosedYear
					FROM Billing.Invoice i WITH (NOLOCK)					
					JOIN Billing.SettingsBilling sb WITH (NOLOCK) ON i.OperatingUnitId = sb.IdOperatingUnit
					WHERE i.Id = @InvoiceId
				) As JournalVoucher
				CROSS APPLY 
				(
						SELECT	ma.Id IdMainAccount,
								IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL) IdThirdParty,
								IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL) IdCostCenter,
								ar.Value DebitValue,
								0 CreditValue
						FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
						JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ar.AccountWithoutRadicateId = ma.Id
						WHERE ar.InvoiceId = @InvoiceId AND ar.AccountReceivableType = 4
					UNION ALL
						SELECT	ma.Id IdMainAccount,
								IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL) IdThirdParty,
								IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL) IdCostCenter,
								0 DebitValue,
								ar.Value CreditValue
						FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
						JOIN Billing.SettingsBilling sb WITH (NOLOCK) ON ar.OperatingUnitId = sb.IdOperatingUnit
						JOIN Treasury.CashReceiptConcepts crc WITH (NOLOCK) ON sb.CapitedPatientAdvanceCashReceiptConceptId = crc.Id
						JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON crc.IdMainAccount = ma.Id
						WHERE ar.InvoiceId = @InvoiceId AND ar.AccountReceivableType = 4
				) As JournalVoucherDetail
				For Xml Auto, Elements
			)
	
				
			INSERT INTO @TableResultJournal
				EXEC [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement] @JournalXml, @UserCode
					
			IF EXISTS (SELECT CodeMessage FROM @TableResultJournal Where CodeMessage <> 0) 
			BEGIN
				SELECT @Message = [Message] FROM @TableResultJournal Where CodeMessage <> 0
				
				SELECT	@ResultStatus = CONVERT(BIT, 0), 
						@ResultMessage = 'Ocurrieron errores al intentar Generar el comprobante contable del Pagaré: ' + ISNULL(@Message, 'No se pudo generar el comprobante contable')
				RETURN
			END

			SELECT TOP 1 @JournalVoucherId = IdJournalVoucher FROM @TableResultJournal

			UPDATE Billing.Invoice SET JournalVoucherId = @JournalVoucherId WHERE Id = @InvoiceId

			SELECT @Message = CONCAT('Se generó el Comprobante contable de tipo ', jvt.Code, ' - ', jvt.Name)
			FROM Billing.Invoice i
			JOIN Billing.SettingsBilling sb ON i.OperatingUnitId = sb.IdOperatingUnit
			JOIN GeneralLedger.JournalVoucherTypes jvt On jvt.Id = sb.InvoiceJournalVoucherTypeId
			Where i.Id = @InvoiceId

			SELECT @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		--------------------------------------------------  ANTICIPO --------------------------------------------------
		IF @LiquidationType = 1 AND @ListPortfolioAdvanceCrossingXml IS NOT NULL
		BEGIN
			EXEC [Portfolio].[SP_GeneratePortfolioTransfer] 
				@ListPortfolioAdvanceCrossingXml,
				@OperativeUnitId,
				@UserCode,
				@PortfolioAccountReceivableId,
				@CompanyType,
				--Salidas
				@Code_Output OUTPUT,
				@Message_Output OUTPUT
					
			IF @Code_Output <> 0 
			BEGIN
				SELECT	@ResultStatus = CONVERT(BIT, 0), 
						@ResultMessage = ISNULL(@Message_Output, 'Error al cruzar el cruce de anticipo vs CxC')
				RETURN
			END

			SET @Message = @Message_Output
			SET @ResultMessage = ISNULL(@ResultMessage, '') + IIF(@Message = '', '', IIF(ISNULL(@ResultMessage, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END
		ELSE IF @LiquidationType <> 1 AND @AccountReceivableType = 6
		BEGIN
			UPDATE pa
				SET pa.Balance = pa.Balance - ipa.Value,
					pa.TransferValue = pa.TransferValue + ipa.Value
			FROM Billing.Invoice i
			JOIN Billing.InvoicePortfolioAdvance ipa ON i.Id = ipa.InvoiceId
			JOIN Portfolio.PortfolioAdvance pa ON ipa.PortfolioAdvanceId = pa.Id
			WHERE i.Id = @InvoiceId
		END

		---------------------------------------------------------------------------------------------------------------

		SELECT	@ResultStatus = CONVERT(BIT, 1)				
	END TRY
	BEGIN CATCH
		SELECT	@ResultStatus = CONVERT(BIT, 0),
				@ResultMessage = CONCAT('Error creando las cuentas por cobrar: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Crea y registra una cuenta por cobrar en el módulo de cartera a partir de una factura de facturación, determinando automáticamente el tipo de documento (cuenta por cobrar ordinaria o pagaré) según el tipo de cuenta recibido. Genera el código consecutivo del documento mediante el servicio de secuencias, inserta el registro en la tabla de cuentas por cobrar con todos sus datos financieros (valor, saldo, fecha de vencimiento, plazo, estado de cartera, cuentas contables de radicación, objeción, conciliación, deterioro y cobro jurídico) y crea la cuota inicial asociada. Complementa el proceso produciendo el comprobante contable (asiento de diario) que respalda el reconocimiento de la cartera en la contabilidad, afectando las cuentas del plan de cuentas configuradas en la estructura contable del contrato y del grupo de atención. Es invocado por el proceso de facturación al momento de liquidar y radicar facturas a terceros, aseguradoras o pacientes particulares, soportando los flujos de cartera, recaudo y presupuesto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreateAccountReceivable_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreateAccountReceivable_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por cobrar; Pagaré; Cuota de recuperación; Paciente particular; Radicación de cuentas; Comprobante contable; Reconocimiento presupuestal; Anticipo de cartera; Cruce de anticipo vs CxC; Boleta electrónica de venta; Facturador electrónico; Tercero pagador; Centro de costo; Plan de cuentas (PUC); Moneda y TRM; Grupo de atención (CareGroup); Estructura contable del contrato', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivable_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @AccountReceivableType = 4 → Usa FormId ''1510'' (Pagarés) para obtener consecutivo else Usa FormId ''682'' (Cuentas por Cobrar); si @FolioType = 3 AND cg.CareGroupType <> 3 AND rcd.IsMasterAccount = 4 → MainAccountWithoutFilingId/AccountWithoutRadicateId = cas.AccountRecoveryFeeId else Aplica lógica jerárquica adicional según FolioType y AccountReceivableType; si @FolioType = 3 (y no se cumple la regla anterior) → Cuenta = cas.AccountParticularId (cuenta de paciente particular); si @AccountReceivableType IN (4,6) → Cuenta = cas.AccountRecoveryFeeId (cuotas de recuperación / pagaré) else Cuenta = cas.AccountWithoutRadicateId; si @FolioType = 3 → PortfolioStatus = 3 else PortfolioStatus = 1; si cg.AffectBudget = 1 AND @AccountReceivableType IN (4,6) → BudgetId = cg.PromissoryNoteBudgetId else BudgetId = cg.BillingBudgetId si AffectBudget=1, NULL si AffectBudget<>1; si @AccountReceivableType = 2 → Genera comprobante contable de factura usando SP_GenerateJournalVoucherDetails y elige tipo de voucher según ApplyElectronicSalesTicket y ElectronicBiller del tercero else Evalúa rama de Pagaré o Anticipo; si @AccountReceivableType = 2 AND sb.ApplyElectronicSalesTicket = 1 AND tp.ElectronicBiller = 0 → IdJournalVoucher = sb.AccountingVoucherGenerationId (boleta electrónica de venta) else IdJournalVoucher = sb.InvoiceJournalVoucherTypeId; si @LiquidationType <> 1 AND @AccountReceivableType = 4 → Genera comprobante contable del Pagaré con débito a cuenta de la CxC y crédito a la cuenta del concepto de recaudo CapitedPatientAdvanceCashReceiptConceptId; si @LiquidationType = 1 AND @ListPortfolioAdvanceCrossingXml IS NOT NULL → Ejecuta Portfolio.SP_GeneratePortfolioTransfer para cruzar anticipo vs CxC else Si @LiquidationType<>1 AND @AccountReceivableType=6, descuenta el valor cruzado del saldo del anticipo (PortfolioAdvance); si @LiquidationType <> 1 AND @AccountReceivableType = 6 → Actualiza Portfolio.PortfolioAdvance restando ipa.Value de Balance y sumándolo a TransferValue; si EXISTS resultado de SP_CreateAndValidateJournalVoucherMovement con CodeMessage <> 0 → Retorna ResultStatus=0 con mensaje de error y aborta else Actualiza Billing.Invoice.JournalVoucherId con el comprobante generado', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivable_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Portfolio.SP_GenerateRecognitionByAccountReceivableId_Output; Billing.SP_GenerateJournalVoucherDetails; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Portfolio.SP_GeneratePortfolioTransfer', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivable_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Billing.Invoice; Billing.RevenueControlDetail; Contract.CareGroup; Contract.ContractAccountingStructure; Common.Customer; Portfolio.AccountReceivable; GeneralLedger.MainAccounts; Common.ThirdParty; Billing.SettingsBilling; Treasury.CashReceiptConcepts; Billing.InvoicePortfolioAdvance; Portfolio.PortfolioAdvance; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivable_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateAccountReceivable_Output';
-- GO
