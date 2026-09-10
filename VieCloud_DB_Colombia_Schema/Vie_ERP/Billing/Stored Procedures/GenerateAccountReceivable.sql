-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-07-19
-- Description:	Genera Cuenta por cobrar
-- =============================================

CREATE Procedure [Billing].[GenerateAccountReceivable]
	@InvoiceNumber Varchar(20),
	@InvoiceThirdPartySalesValue Decimal(18, 2),
	@CareGroupId Int,
	@FolioThirdPartyId Int,
	@FolioTotalPatientWithDiscount Decimal(18,2),
	@OperativeUnitId Int,
	@AccountReceivableGenerate Int,
	@InvoiceCategoryId Int,
	@PagareValue Decimal(18, 2),
	@ThirdPartyPatientId Int,
	@InvoiceId Int,
	@IdSequence Int,
	@UserCode Varchar(10),
	
	@StatusResult Bit Output,
	@Message Varchar(255) Output,
	@PortfolioAccountReceivableId Int Output,
	@PortfolioAccountReceivableCode Varchar(20) Output
AS
Begin
	Set Nocount On;
	Begin Try

		Declare @Code Varchar(20) = '',
			@AccountReceivableType Tinyint,
			@ThirdPartyId Int,
			@CustomerId Int,
			@ExpiratedDate DateTime,
			@PortfolioStatus Tinyint,
			@MainAccountWithoutFilingId Int,
			@Value Decimal(18, 2),
			@Balance Decimal(18,2),
			@AccountWithoutRadicateId Int,
			@AccountRadicateId Int,
			@AccountObjectionRemediedId Int,
			@AccountConciliationId Int,
			@AccountLegalCollectionId Int,
			@AccountDebtorOrder Int,
			@AccountCreditorOrder Int,
			@CreditProvisionAccountId Int,
			@DebitProvisionAccountId Int,
			@CreditAccountDeteriorationId Int,
			@DebitAccountDeteriorationId Int,
			@ReversalAccountDeteriorationId Int,
			@PreviousPeriodReversalAccountDeteriorationId Int			

		Declare @CgInvoiceDeadLines Int,
			@CgCostcenterId Int,
			@CgLiquidationType Tinyint,
			@CgCareGroupType Tinyint,
			@CgContractAccountingStructureId Int,
			@CgCodeName Varchar(300),
			@CgAccountRecoveryFeeId Int,
			@CgAccountWithoutRadicateId Int,
			@CgAccountParticularId Int,
			@CgAccountRadicateId Int,
			@CgAccountObjectionRemediedId Int,
			@CgAccountConciliationId Int,
			@CgAccountLegalCollectionId Int,
			@CgAccountDebitOrderId Int,
			@CgAccountCreditOrderId int,
			@CgCreditProvisionAccountId Int,
			@CgDebitProvisionAccountId Int,
			@CgCreditAccountDeteriorationId Int,
			@CgDebitAccountDeteriorationId Int,
			@CgReversalAccountDeteriorationId Int,
			@CgPreviousPeriodReversalAccountDeteriorationId Int,
			@CgAccountHardCollectionId INT,
			@CgAffectBudget BIT,
			@CgBillingBudgetId INT,
			@CgPromissoryNoteBudgetId INT,
			@CgBudgetId INT

		Select @CgInvoiceDeadLines = cg.InvoiceDeadlines,
			@CgCostcenterId = cg.CostCenterId,
			@CgLiquidationType = cg.LiquidationType,
			@CgCareGroupType = cg.CareGroupType,
			@CgCodeName = Concat(cg.Code, ' - ', cg.[Name]),
			@CgContractAccountingStructureId = cg.ContractAccountingStructureId,
			@CgAccountRecoveryFeeId = cas.AccountRecoveryFeeId,
			@CgAccountWithoutRadicateId = cas.AccountWithoutRadicateId,
			@CgAccountParticularId = cas.AccountParticularId,
			@CgAccountRadicateId = cas.AccountRadicateId,
			@CgAccountObjectionRemediedId = cas.AccountObjectionRemediedId,
			@CgAccountConciliationId = cas.AccountConciliationId,
			@CgAccountLegalCollectionId = cas.AccountLegalCollectionId,
			@CgAccountDebitOrderId = cas.AccountDebitOrderId,
			@CgAccountCreditOrderId = cas.AccountCreditOrderId,
			@CgCreditProvisionAccountId = cas.CreditProvisionAccountId,
			@CgDebitProvisionAccountId = cas.DebitProvisionAccountId,
			@CgCreditAccountDeteriorationId = cas.CreditAccountDeteriorationId,
			@CgDebitAccountDeteriorationId = cas.DebitAccountDeteriorationId,
			@CgReversalAccountDeteriorationId = cas.ReversalAccountDeteriorationId,
			@CgPreviousPeriodReversalAccountDeteriorationId = cas.PreviousPeriodReversalAccountDeteriorationId,
			@CgAccountHardCollectionId = cas.AccountHardCollectionId,
			@CgAffectBudget = cg.AffectBudget,
			@CgBillingBudgetId = cg.BillingBudgetId,
			@CgPromissoryNoteBudgetId = cg.PromissoryNoteBudgetId
		From [Contract].CareGroup cg With(Nolock) 
		Left Join [Contract].ContractAccountingStructure cas With(Nolock) On cg.ContractAccountingStructureId = cas.Id
		Where cg.Id = @CareGroupId

		If @CgContractAccountingStructureId Is Null Begin
			Set @StatusResult = 0
			Set @Message = 'El grupo de atención ' + @CgCodeName + ' no tiene asignado la estructura contable'
			Set @PortfolioAccountReceivableId = -1
			Set @PortfolioAccountReceivableCode = ''
			Return
		End

		If @AccountReceivableGenerate = 1 Begin
			Set @AccountReceivableType = 2
			Set @ThirdPartyId = @FolioThirdPartyId
			Set @Value = @InvoiceThirdPartySalesValue
			SET @Balance = @Value
		End
		Else If @AccountReceivableGenerate = 2 Begin
			Set @AccountReceivableType = 6
			Set @ThirdPartyId = @ThirdPartyPatientId
			Set @Value = @FolioTotalPatientWithDiscount - @PagareValue
			SET @Balance = IIF(@CgLiquidationType = 1, @Value, 0)
		End
		Else If @AccountReceivableGenerate = 3 Begin
			Set @AccountReceivableType = 4
			Set @ThirdPartyId = @ThirdPartyPatientId
			Set @Value = @PagareValue
			SET @Balance = @Value
		End
		Else Begin
			Set @AccountReceivableType = 4
			Set @ThirdPartyId = @FolioThirdPartyId
			Set @Value = @PagareValue
			SET @Balance = @Value
		End

		Select @CustomerId = Id From Common.Customer With(Nolock) Where ThirdPartyId = @FolioThirdPartyId
		IF @AccountReceivableType = 2 AND @CgCareGroupType IN (1,2,4) AND @CustomerId Is Null BEGIN
			Set @StatusResult = 0
			SELECT @Message = 'No existe un cliente asociado al tercero ' + CONCAT(tp.Nit, ' - ', tp.Name) FROM Common.ThirdParty tp WHERE tp.Id = @FolioThirdPartyId
			Set @PortfolioAccountReceivableId = -1
			Set @PortfolioAccountReceivableCode = ''
			RETURN
		END
		
		Declare @InvoiceDocumentType Tinyint
		Select @InvoiceDocumentType = DocumentType From Billing.Invoice With(Nolock) Where InvoiceNumber = @InvoiceNumber

		If @InvoiceDocumentType = 3
			Set @PortfolioStatus = 3
		Else
			Set @PortfolioStatus = 1

		If @CgCareGroupType = 1 Or @CgCareGroupType = 2 Or @CgCareGroupType = 4 Begin
			If @AccountReceivableType = 4 Or @AccountReceivableType = 6
				Set @MainAccountWithoutFilingId = @CgAccountRecoveryFeeId
			Else
				Set @MainAccountWithoutFilingId = @CgAccountWithoutRadicateId
		End
		Else If @CgCareGroupType = 3 Begin
			If @CgAccountParticularId Is Null Begin
				Set @StatusResult = 0
				Set @Message = 'El grupo de atención ' + @CgCodeName + ' esta asignado como Tipo Particular, pero no tiene la cuenta contable a particulares parametrizada'
				Set @PortfolioAccountReceivableId = -1
				Set @PortfolioAccountReceivableCode = ''
				Return
			End
			Set @MainAccountWithoutFilingId = @CgAccountParticularId
		End
		Else 
			Set @MainAccountWithoutFilingId = Null

		--Resolvemos la secuencia		
		--genero la secuencia para la orden de servicio
		Declare @Sequential Bit, @Pattern Varchar(20), @Next Bigint
		Select @Sequential = ps.Sequential, @Pattern = cs.Pattern
		From Portfolio.PortfolioSequenceDetail psd With(Nolock) 
		Inner Join Portfolio.PortfolioSequence ps With(Nolock) On psd.IdSequensePortfolioC = ps.Id
		Inner Join Common.Sequense cs With(Nolock) On cs.Id = psd.IdSequense
		Where psd.Id = @IdSequence

		If @Sequential = 1 Begin
			Update Portfolio.PortfolioSequenceDetail Set @Next = [Next] += 1 Where Id = @IdSequence
			Select @Code = dbo.GetSequence('', @Pattern, (@Next - 1))
			If @Code = '__ERROR_MAXVALUE__' Begin
				Set @StatusResult = 0
				Set @Message = 'Secuencia no encontrada (AccountReceivable)'
				Set @PortfolioAccountReceivableId = -1
				Set @PortfolioAccountReceivableCode = ''
				Return
			End	
		End
		Else Begin
			Set @StatusResult = 0
			Set @Message = 'Secuencia no encontrada (AccountReceivable)'
			Set @PortfolioAccountReceivableId = -1
			Set @PortfolioAccountReceivableCode = ''
			Return
		End

		Select	@ExpiratedDate  = DateAdd(Day, @CgInvoiceDeadLines, Common.Getdate()),
				@CgBudgetId = IIF(@CgAffectBudget = 1, IIF(@AccountReceivableType = 4 OR @AccountReceivableType = 6, @CgPromissoryNoteBudgetId, @CgBillingBudgetId), NULL)

		--Si la factura maneja retenciones y estas son de tipo reconocer, se debe reducir el saldo de acuerdo a las retenciones asociadas
		IF @AccountReceivableType = 2
		BEGIN
			SELECT @Balance = @Balance - Value
			FROM Billing.InvoiceCustomerRetention
			WHERE InvoiceId = @InvoiceId AND CalculateTaxAdvance = 2
		END

		--Insertamos la cabecera
		Insert Into [Portfolio].[AccountReceivable]
           ([Code],[AccountReceivableType],[ThirdPartyId],[CustomerId],[InvoiceId],[InvoiceNumber],[AccountReceivableDate]
		   ,[Term],[ExpiredDate],[Observations],[PortfolioStatus],[OpeningBalance],[PaymentAgreement],[RegistrationAdjusted]
		   ,[InvoiceCategoryId],[CostCenterId],[OperatingUnitId],[MainAccountWithoutFilingId],[NumberShares],[Value],[Balance]
		   ,[Status],[AccountWithoutRadicateId],[AccountRadicateId],[AccountObjectionRemediedId]
		   ,[AccountConciliationId],[AccountLegalCollectionId],[AccountDebtorOrder],[AccountCreditorOrder]
		   ,[CreditProvisionAccountId],[DebitProvisionAccountId],[CreditAccountDeteriorationId]
		   ,[DebitAccountDeteriorationId],[ReversalAccountDeteriorationId],[PreviousPeriodReversalAccountDeteriorationId],[AccountHardCollectionId]
		   ,AffectBudget,BudgetId,CareGroupId
		   ,[CreationUser],[CreationDate])
		Values
           (@Code,@AccountReceivableType,@ThirdPartyId,@CustomerId,@InvoiceId,@InvoiceNumber,Common.Getdate(),
			@CgInvoiceDeadLines,@ExpiratedDate,'',@PortfolioStatus,0,0,0,
			@InvoiceCategoryId,@CgCostcenterId,@OperativeUnitId,@MainAccountWithoutFilingId,1,@Value,@Balance,
			2,@MainAccountWithoutFilingId,@CgAccountRadicateId,@CgAccountObjectionRemediedId,
			@CgAccountConciliationId,@CgAccountLegalCollectionId,@CgAccountDebitOrderId,@CgAccountCreditOrderId,
			@CgCreditProvisionAccountId,@CgDebitProvisionAccountId,@CgCreditAccountDeteriorationId,
			@CgDebitAccountDeteriorationId,@CgReversalAccountDeteriorationId,@CgPreviousPeriodReversalAccountDeteriorationId,@CgAccountHardCollectionId,
			@CgAffectBudget,@CgBudgetId,@CareGroupId,
			@UserCode,Common.Getdate())

		Set @PortfolioAccountReceivableId = Scope_Identity()
		Set @PortfolioAccountReceivableCode = @Code

		--Insertamos detalle de cuota
		Insert Into Portfolio.AccountReceivableShare
		(AccountReceivableId,Number,ExpiredDate,[Value]
		,Balance,DebitValue,CreditValue,TransferValue
		,PaymentValue,CrossingValue,InterestValue
		,SurchargesValue,CapitalRepaymentAgreement
		,FinancialInterest,RepaymentAgreementInterest)
		Values
		(@PortfolioAccountReceivableId,1,@ExpiratedDate,
		@Value,@Balance,0,0,0,0,0,0,0,0,0,0)

		--Insertamos detalle de cuenta
		Declare @HandlesThirdParty Bit, @HandlesCostCenter Bit

		Select @HandlesThirdParty = HandlesThirdParty, @HandlesCostCenter = HandlesCostCenter 
		From GeneralLedger.MainAccounts ma With(Nolock) Where Id = @MainAccountWithoutFilingId
		
		Insert Into Portfolio.AccountReceivableAccounting
		(AccountReceivableId,MainAccountId,ThirdPartyId,
		CostCenterId,[Value],Balance)
		Values
		(@PortfolioAccountReceivableId,@MainAccountWithoutFilingId,
		Case When @HandlesThirdParty = 1 Then @FolioThirdPartyId Else Null End,
		Case When @HandlesCostCenter = 1 Then @CgCostcenterId Else Null End,
		@Value,@Balance)

		/*************************************************************************************************************/

		IF @CgLiquidationType <> 1 AND @AccountReceivableGenerate = 2 
		BEGIN
			UPDATE pa
				SET pa.Balance = pa.Balance - ipa.Value,
					pa.TransferValue = pa.TransferValue + ipa.Value
			FROM Billing.Invoice i
			JOIN Billing.InvoicePortfolioAdvance ipa ON i.Id = ipa.InvoiceId
			JOIN Portfolio.PortfolioAdvance pa ON ipa.PortfolioAdvanceId = pa.Id
			WHERE i.Id = @InvoiceId
		END

		/*************************************************************************************************************/
		
		Set @StatusResult = 1
		Set @Message = CASE @AccountReceivableGenerate
				WHEN 1 THEN CONCAT(
					IIF
					(
						@CgCareGroupType = 3, 
						'Código Cuenta por cobrar al Paciente: ', 
						'Código Cuenta por cobrar a la Entidad: '
					), 
					@Code
				)
				WHEN 2 THEN CONCAT('Código Cuenta por cobrar al Paciente: ', @Code)
				WHEN 3 THEN CONCAT('Código Cuenta por cobrar tipo Pagaré: ', @Code)
				ELSE CONCAT('Se guardó la Cuenta por cobrar: ', @Code)
			END

		/**************************************** RECONOCIMIENTO PRESUPUESTAL ****************************************/

		IF @InvoiceDocumentType = 3 OR (@AccountReceivableType = 4 OR @AccountReceivableType = 6)
		BEGIN
			DECLARE @Code_Output INT,
					@Message_Output VARCHAR(MAX)

			EXEC [Portfolio].[SP_GenerateRecognitionByAccountReceivableId_Output] @OperativeUnitId, @PortfolioAccountReceivableId, @UserCode, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT	@StatusResult = 0,
						@Message = ISNULL(@Message_Output, 'No se pudo generar el reconocimiento presupuestal'),
						@PortfolioAccountReceivableId = -1,
						@PortfolioAccountReceivableCode = ''
				RETURN
			END

			SELECT @Message = @Message + IIF(@Message_Output = '', '', CHAR(13) + CHAR(10) + @Message_Output)
		END
	End Try
	Begin Catch
		SELECT	@StatusResult = 0,
				@Message = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50)),
				@PortfolioAccountReceivableId = -1,
				@PortfolioAccountReceivableCode = ''
	End Catch	
End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la cuenta por cobrar en cartera a partir de una factura emitida, determinando el tipo de deudor (entidad pagadora, paciente con copago o pagaré) y el valor a cobrar según el grupo de atención del contrato. Consulta el grupo de atención (CareGroup) para obtener los plazos de vencimiento, tipo de liquidación, centro de costos y si afecta presupuesto, y cruza con la estructura contable del contrato (ContractAccountingStructure) para asignar las cuentas contables correctas según la etapa de cartera: radicación, sin radicar, glosas subsanadas, conciliación, cobro jurídico, cobro difícil, deterioro de cartera y provisiones. Valida que el grupo de atención tenga estructura contable asignada y que exista un cliente vinculado al tercero pagador cuando corresponde, retornando el identificador y código de la cuenta por cobrar creada o un mensaje de error en caso de inconsistencia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'GenerateAccountReceivable';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'GenerateAccountReceivable';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda cuenta por cobrar se inserta con Status=2 y se crea siempre con una única cuota inicial (Number=1, NumberShares=1) cuyo vencimiento es la fecha actual + InvoiceDeadlines del CareGroup.; El código de la cuenta por cobrar se obtiene exclusivamente de una secuencia configurada como Sequential=1 en PortfolioSequenceDetail; cualquier otro caso aborta sin insertar.; Si el CareGroup no tiene ContractAccountingStructure asignada, no se genera ninguna cuenta por cobrar.; Si el CareGroup es de tipo Particular (3) y no tiene AccountParticularId, no se genera la cuenta por cobrar.; Para CareGroupType IN (1,2,4) con AccountReceivableType=2, debe existir un Customer asociado al FolioThirdPartyId; si no, se aborta.; OpeningBalance, PaymentAgreement y RegistrationAdjusted siempre se insertan en 0.; El AccountWithoutRadicateId del registro insertado se asigna con el MainAccountWithoutFilingId resuelto, no con CgAccountWithoutRadicateId directamente.; El presupuesto solo se afecta si CgAffectBudget=1; el tipo de presupuesto depende de si la cuenta es pagaré (4/6) o factura (otros).; Cualquier excepción captura ERROR_MESSAGE y devuelve StatusResult=0 con PortfolioAccountReceivableId=-1.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GenerateAccountReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por cobrar; Cartera; Grupo de atención (CareGroup); Estructura contable de contrato; Cliente / Tercero pagador; Factura; Pagaré; Paciente particular; Retenciones tributarias (reconocer); Anticipo de cartera; Secuencia/consecutivo de portafolio; Reconocimiento presupuestal; Cuota de cartera; Cuenta sin radicar / radicada / objeción / conciliación / cobro jurídico / cobro pre-jurídico (hard collection); Provisión y deterioro de cartera; Liquidación (LiquidationType)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GenerateAccountReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @AccountReceivableGenerate = 1 (cuenta a la entidad/tercero pagador) → AccountReceivableType=2, ThirdPartyId=FolioThirdPartyId, Value=InvoiceThirdPartySalesValue, Balance=Value; si @AccountReceivableGenerate = 2 (cuenta al paciente) → AccountReceivableType=6, ThirdPartyId=ThirdPartyPatientId, Value=FolioTotalPatientWithDiscount-PagareValue; Balance=Value sólo si LiquidationType=1, en caso contrario Balance=0; si @AccountReceivableGenerate = 3 (pagaré del paciente) → AccountReceivableType=4, ThirdPartyId=ThirdPartyPatientId, Value=PagareValue, Balance=Value; si @AccountReceivableGenerate distinto de 1,2,3 (default pagaré tercero) → AccountReceivableType=4, ThirdPartyId=FolioThirdPartyId, Value=PagareValue, Balance=Value; si CareGroupType IN (1,2,4) → MainAccountWithoutFilingId = AccountRecoveryFeeId si tipo es 4 o 6, en caso contrario AccountWithoutRadicateId else Si CareGroupType=3 usa AccountParticularId; otro tipo => MainAccountWithoutFilingId NULL; si InvoiceDocumentType = 3 (nota crédito/documento tipo 3) → PortfolioStatus=3 else PortfolioStatus=1; si AccountReceivableType=2 (cuenta a entidad) → Resta del Balance las retenciones de InvoiceCustomerRetention con CalculateTaxAdvance=2 (retenciones de tipo ''reconocer''); si CgLiquidationType <> 1 AND AccountReceivableGenerate = 2 → Actualiza Portfolio.PortfolioAdvance restando del Balance y sumando a TransferValue el valor de los InvoicePortfolioAdvance de la factura; si CgAffectBudget = 1 → BudgetId = PromissoryNoteBudgetId si tipo 4 o 6, en caso contrario BillingBudgetId else BudgetId = NULL; si InvoiceDocumentType = 3 OR AccountReceivableType IN (4,6) → Invoca Portfolio.SP_GenerateRecognitionByAccountReceivableId_Output para generar el reconocimiento presupuestal; si retorna código <> 0 aborta con error; si MainAccount.HandlesThirdParty=1 / HandlesCostCenter=1 → En el detalle contable se asigna ThirdPartyId / CostCenterId; en caso contrario quedan en NULL', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GenerateAccountReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_GenerateRecognitionByAccountReceivableId_Output; dbo.GetSequence; Common.Getdate', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GenerateAccountReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Contract.ContractAccountingStructure; Common.Customer; Common.ThirdParty; Billing.Invoice; Portfolio.PortfolioSequenceDetail; Portfolio.PortfolioSequence; Common.Sequense; Billing.InvoiceCustomerRetention; GeneralLedger.MainAccounts; Billing.InvoicePortfolioAdvance; Portfolio.PortfolioAdvance', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GenerateAccountReceivable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GenerateAccountReceivable';
-- GO
