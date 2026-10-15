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

			SELECT @Message = CONCAT('Se generó el Comprobante contable ', jv.Consecutive, ' de tipo ', jvt.Code, ' - ', jvt.Name)
			FROM GeneralLedger.JournalVouchers jv
			JOIN GeneralLedger.JournalVoucherTypes jvt On jv.IdJournalVoucher = jvt.Id
			Where jv.Id = @JournalVoucherId

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

			SELECT @Message = CONCAT('Se generó el Comprobante contable ', jv.Consecutive, ' de tipo ', jvt.Code, ' - ', jvt.Name)
			FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
			JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) On jv.IdJournalVoucher = jvt.Id
			Where jv.Id = @JournalVoucherId

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