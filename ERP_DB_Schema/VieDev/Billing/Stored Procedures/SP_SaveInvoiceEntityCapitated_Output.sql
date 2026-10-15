-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-11
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una factura de monto fijo
-- =============================================
CREATE PROCEDURE [Billing].[SP_SaveInvoiceEntityCapitated_Output]
    @InvoiceEntityCapitatedXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	------------------------------------------------------
	@Id INT OUTPUT,
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables para obtener la cabecera
	DECLARE @OperatingUnitId INT,
			@BillingAuthorizationId INT,
			@CareGroupId INT,
			@InvoicePeriod TINYINT,
			@PreviousRIPSInvoice INT,
			@InvoiceCategoryId INT,
			@DocumentDate DATETIME,
			@InitialDate DATE,
			@EndDate DATE,
			@UserNumber INT,
			@UserValue DECIMAL(18,2),
			@SubTotalValue DECIMAL(18,2),
			@DiscountPercentage DECIMAL(5,2),
			@DiscountValue DECIMAL(18,2),
			@TotalValue DECIMAL(18,2),
			@Status TINYINT,
			@FilePath VARCHAR(MAX),
			@Observations VARCHAR(MAX),
			-------------------------------------------------------------------
			@IdForm INT = 758,
			@DocumentTypeControl INT = 1,
			-------------------------------------------------------------------
			@InvoiceId INT,
			@InvoiceNumber VARCHAR(15),
			@InvoicePrefix VARCHAR(5),
			@AuthorizationConsecutive BIGINT,
			@AuthorizationInitialInvoice BIGINT,
			@AuthorizationFinalInvoice BIGINT,
			@AuthorizationInitialDate DATE,
			@AuthorizationFinalDate DATE,
			@PortfolioAccountReceivableId INT,
			@PortfolioAccountReceivableCode VARCHAR(20),
			@JournalVoucherId INT,
			@CurrencyId INT,
			@ListPortfolioAdvanceXml XML,
			@CompanyType TINYINT,
			-------------------------------------------------------------------
			@Message VARCHAR(MAX),
			-------------------------------------------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Se obtienen los datos de la cabecera
	SELECT	@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@CareGroupId = t.x.value('CareGroupId[1]','int'),
			@InvoiceCategoryId = t.x.value('InvoiceCategoryId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@InitialDate = t.x.value('InitialDate[1]','date'),
			@EndDate = t.x.value('EndDate[1]','date'),
			@UserNumber = t.x.value('UserNumber[1]','int'),
			@UserValue = t.x.value('UserValue[1]','decimal(18,2)'),
			@SubTotalValue = t.x.value('SubTotalValue[1]','decimal(18,2)'),
			@DiscountPercentage = t.x.value('DiscountPercentage[1]','decimal(5,2)'),
			@DiscountValue = t.x.value('DiscountValue[1]','decimal(18,2)'),
			@TotalValue = t.x.value('TotalValue[1]','decimal(18,2)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@FilePath = t.x.value('FilePath[1]','varchar(max)'),
			@Observations = t.x.value('Observations[1]','varchar(max)'),
			@InvoicePeriod = t.x.value('InvoicePeriod[1]','tinyint'),
			@PreviousRIPSInvoice = t.x.value('PreviousRIPSInvoice[1]','int'),
			@CurrencyId = t.x.value('CurrencyId[1]','int'),
			@ListPortfolioAdvanceXml = t.x.query('ListPortfolioAdvanceCrossing'),
			@CompanyType = t.x.value('CompanyType[1]','tinyint')
	FROM @InvoiceEntityCapitatedXml.nodes('/InvoiceEntityCapitated') t(x)

	SELECT	@BillingAuthorizationId = sb.EntityCapitatedBillingAuthorizationId
	FROM Billing.SettingsBilling sb WITH (NOLOCK)
	WHERE sb.IdOperatingUnit = @OperatingUnitId

	--tabla temporal para almacenar el resultado del movimiento contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	/*************************************************  VALIDACIONES *************************************************/

	IF EXISTS (SELECT 1 FROM Billing.InvoiceEntityCapitated WHERE Id = @Id AND Status <> 1)
	BEGIN
		SELECT	@CodeResult = 999, 
				@MessageResult = 'La factura monto fijo se encuentra en estado: ' + CASE Status 
																		WHEN 2 THEN 'Confirmado'
																		WHEN 3 THEN 'Anulado'
																		WHEN 4 THEN 'Reversado'
																	END
		FROM Billing.InvoiceEntityCapitated
		WHERE Id = @Id
		RETURN
	END

	IF @Status = 3
	BEGIN
		UPDATE Billing.InvoiceEntityCapitated
			SET Status = @Status,
				ModificationUser = @CodeUser,
				ModificationDate = [Common].[GETDATE](),
				AnnulmentUser = @CodeUser,
				AnnulmentDate = [Common].[GETDATE]()
		WHERE Id = @Id
	END
	ELSE
	BEGIN
		/***********************************************  VALIDACIONES ***********************************************/

		IF @Status= 5 AND @InvoicePeriod <> 3 BEGIN
			
			SELECT	@CodeResult = 999, 
					@MessageResult = 'El estado "Confirmado RIPS Final" Solo aplica cuando el periodo de la capita es "Final"'
			RETURN
		END
		
		IF @FilePath IS NULL BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'No se ha enviado la ruta de almacenamiento de la factura electrónica'
			RETURN
		END

		IF NOT EXISTS (
			SELECT 1 
			FROM Billing.BillingAuthorization WITH (NOLOCK) 
			WHERE Id = @BillingAuthorizationId AND [Status] = 1
		) BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'La autorizacion de facturacion esta inactiva'
			RETURN
		END

		IF NOT EXISTS (
			SELECT 1
			FROM Billing.BillingAuthorization ba WITH (NOLOCK) 
			JOIN Billing.BillingAuthorizationUser bau WITH (NOLOCK) ON ba.Id = bau.BillingAuthorizationId
			WHERE ba.Id = @BillingAuthorizationId AND bau.UserCode = @CodeUser 
		) BEGIN 
			SELECT @CodeResult = 999,
				   @MessageResult = 'No tiene permitido usar la autorización n°: ' + ba.Code + ' - ' + ba.Name
			FROM Billing.BillingAuthorization ba WITH (NOLOCK) 
			WHERE ba.Id = @BillingAuthorizationId  
			RETURN 
		END
				
		IF EXISTS (
			SELECT 1 
			FROM Billing.BillingAuthorization WITH (NOLOCK) 
			WHERE Id = @BillingAuthorizationId AND [Status] = 1
				AND (@DocumentDate < InitialDate OR @DocumentDate >= FinalDate)
		) BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'La autorización asignada no se encuentra vigente para la fecha del documento'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg WITH (NOLOCK)
			WHERE cg.Id = @CareGroupId AND cg.Status = 0
		) BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'El grupo de atención está inactivo'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg WITH (NOLOCK)
			WHERE cg.Id = @CareGroupId AND cg.ContractAccountingStructureId IS NULL
		) BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'El grupo de atención no tiene asignado la estructura contable'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg WITH (NOLOCK)
			LEFT JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id AND c.Status = 1
			WHERE cg.Id = @CareGroupId AND c.Id IS NULL --Las facturas monto fijo debe tener un contrato
		) BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'El grupo de atención no tiene un contrato vigente o válido'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg WITH (NOLOCK)
			LEFT JOIN Contract.ContractDetail cd WITH (NOLOCK) ON cg.ContractId = cd.ContractId AND cd.ValidRecord = 1
			WHERE cg.Id = @CareGroupId AND cd.Id IS NULL
		) BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'El grupo de atención no tiene contrato o este no tiene un detalle válido'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg WITH (NOLOCK)
			JOIN Contract.ContractDetail cd WITH (NOLOCK) ON cg.ContractId = cd.ContractId AND cd.ValidRecord = 1 AND cd.TerminationControl IN (2, 4)
			WHERE cg.Id = @CareGroupId AND (@DocumentDate > cd.EndDate OR @DocumentDate > cd.BillingEndDate)
		) BEGIN			
			SELECT	@CodeResult = 999, 
					@MessageResult = 'El contrato asociado al grupo de atención ya venció'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg WITH (NOLOCK)
			WHERE cg.Id = @CareGroupId AND  cg.LiquidationType = 5
		) AND @SubTotalValue <> ISNULL((
			SELECT SUM(g.TotalContract) TotalContract
			FROM [Contract].GroupersCareGroup gcg
			JOIN [Contract].Groupers g ON g.Id = gcg.GroupersId
			WHERE gcg.CareGroupId = @CareGroupId
		), 0) BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'El subtotal de los agrupadores ha cambiado, por favor recargue el formulario'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg WITH (NOLOCK)
			JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id
			JOIN Contract.HealthAdministrator ha WITH (NOLOCK) ON c.HealthAdministratorId = ha.Id
			JOIN Common.ThirdParty tp WITH (NOLOCK) ON ha.ThirdPartyId = tp.Id
			LEFT JOIN Common.Address a WITH (NOLOCK) ON tp.PersonId = a.IdPerson
			WHERE cg.Id = @CareGroupId AND (a.Id IS NULL OR a.DepartmentId IS NULL OR a.CityId IS NULL)
		) BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'El tercero no tiene parametrizada una dirección válida'
			RETURN
		END

		IF NOT EXISTS (
			SELECT 1
			FROM Portfolio.PortfolioSequence s WITH (NOLOCK)
			JOIN Portfolio.PortfolioSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequensePortfolioC
			JOIN Common.Sequense cs on sd.IdSequense = cs.Id
			WHERE s.IdForm = '682' AND s.IsManual = 0
				AND 
				(
					(s.Scope = 'O')
					OR
					(s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
		) BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'No existe una secuencia automatica de Cuentas Por Cobrar'
			RETURN
		END

		/****************************************  INSERTAR / ACTUALIZAR CABECERA ****************************************/

		DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status in (2 ,5)THEN @CodeUser ELSE NULL END
		DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status in (2,5) THEN [Common].[GETDATE]() ELSE NULL END

		IF @Id = 0
		BEGIN --Si se esta insertando por primera vez se consulta la secuencia numerica
			DECLARE @IsManual BIT
				
			EXEC Common.SP_GetSequence 180, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = REPLACE(@Message_Output, '{0}', 'factura monto fijo')
				RETURN
			END

			--Se inserta la cabecera
			INSERT INTO Billing.InvoiceEntityCapitated
			(
				[OperatingUnitId],[BillingAuthorizationId],[CareGroupId],[InvoiceCategoryId],[Code],[DocumentDate],[InitialDate],[EndDate],
				[UserNumber],[UserValue],[DiscountPercentage],[DiscountValue],[TotalValue],
				[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate], [Observations], [InvoicePeriod], [PreviousRIPSInvoice], [CurrencyId]
			)
			SELECT	@OperatingUnitId,@BillingAuthorizationId,@CareGroupId,@InvoiceCategoryId,@Code,@DocumentDate,@InitialDate,@EndDate,
					@UserNumber,@UserValue,@DiscountPercentage,@DiscountValue,@TotalValue,
					@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate, @Observations, @InvoicePeriod, @PreviousRIPSInvoice, @CurrencyId

			--Obtengo el id de la cabcera
			SET @Id = SCOPE_IDENTITY()
		END
		ELSE --Si se esta actualizando
		BEGIN
			UPDATE Billing.InvoiceEntityCapitated
				SET [OperatingUnitId] = @OperatingUnitId,
					[BillingAuthorizationId] = @BillingAuthorizationId,
					[CareGroupId] = @CareGroupId,
					[InvoiceCategoryId] = @InvoiceCategoryId,
					[Code] = @Code,
					[DocumentDate] = @DocumentDate,
					[InitialDate] = @InitialDate,
					[EndDate] = @EndDate,
					[UserNumber] = @UserNumber,
					[UserValue] = @UserValue,
					[DiscountPercentage] = @DiscountPercentage,
					[DiscountValue] = @DiscountValue,
					[TotalValue] = @TotalValue,
					[Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[ConfirmationUser] = @ConfirmationUser,
					[ConfirmationDate] = @ConfirmationDate,
					[Observations] = @Observations,
					[InvoicePeriod] = @InvoicePeriod,
					[PreviousRIPSInvoice] = @PreviousRIPSInvoice,
					[CurrencyId] = @CurrencyId
			WHERE Id = @Id
		END

		/*************************************************  CONFIRMACION *************************************************/

		IF @Status = 2
		BEGIN
			------ AUDITORIA de agrupadores
			IF EXISTS (
				SELECT 1
				FROM Contract.CareGroup cg WITH (NOLOCK)
				WHERE cg.Id = @CareGroupId AND  cg.LiquidationType = 5
			) BEGIN
				INSERT INTO [Billing].[InvoiceEntityCapitatedGroupers]
					(InvoiceEntityCapitatedId, GroupersId, Code, Description, UserMin, UserMax, ProjectCME, TotalContract)
				SELECT
					@Id, g.Id, g.Code, g.Description, g.UserMin, g.UserMax, g.ProjectCME, g.TotalContract
				FROM [Contract].[GroupersCareGroup] gcg
				JOIN [Contract].[Groupers] g ON gcg.GroupersId = g.Id
				WHERE @CareGroupId = gcg.CareGroupId
			END

			------ FACTURA

			UPDATE ba 
				SET @InvoicePrefix = ba.InvoicePrefix, 
					@AuthorizationConsecutive = ba.Consecutive = ba.Consecutive + 1,
					@AuthorizationInitialInvoice = ba.InitialInvoice,
					@AuthorizationFinalInvoice = ba.FinalInvoice,
					@AuthorizationInitialDate = ba.InitialDate,
					@AuthorizationFinalDate = ba.FinalDate
			FROM Billing.BillingAuthorization ba
			WHERE ba.Id = @BillingAuthorizationId

			SET @AuthorizationConsecutive -= 1

			IF @AuthorizationConsecutive < @AuthorizationInitialInvoice OR @AuthorizationConsecutive >= @AuthorizationFinalInvoice
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'No hay consecutivos disponibles para asignar a la factura del número de autorización asignado'
				RETURN
			END

			IF @DocumentDate < @AuthorizationInitialDate OR @DocumentDate >= @AuthorizationFinalDate
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'La autorización asignada no se encuentra vigente'
				RETURN
			END

			/*Se valida para los tipo PGP si la fecha de la factura es menor a la fecha final*/
			IF EXISTS(	SELECT 1
						FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
						JOIN Contract.CareGroup cg WITH (NOLOCK) ON iec.CareGroupId = cg.Id
						WHERE cg.LiquidationType = 5 AND @DocumentDate < iec.EndDate AND iec.Id = @Id) BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'La fecha de la factura no puede ser menor a la fecha final'
				RETURN
			END

			SET @InvoiceNumber = CONCAT(@InvoicePrefix, @AuthorizationConsecutive)

			------------------------------------------  GENERACION DE LA FACTURA ------------------------------------------

			INSERT INTO Billing.Invoice 
			(
				[OperatingUnitId],[DocumentType],[InvoiceNumber],[HealthAdministratorId],[ThirdPartyId],[CareGroupId],
				[InvoiceDate],[InvoiceExpirationDate],[TotalInvoice],[CapitationInitialDate],[CapitationEndDate],
				[CapitationlPatientsAmount],[CapitationPatientValue],[ThirdPartySalesValue],[ThirdPartyDiscountValue],
				[ResponsibleRecoveryFee],[TotalPatientSalesPrice],[PatientDiscount],[PatientDiscountPercentage],
				[TotalPatientWithDiscount],[ValueVoucher],[PatientPaidValue],[ThirdPartyAccountReceivableValue],
				[PatientAccountReceivableValue],[CREETaxRetentionValue],[CREETaxRetentionBaseValue],[Status],Observation,
				[InvoicedUser],[InvoicedDate],[InvoiceCategoryId],[OutputDate],[IsCutAccount],[InitialDate],[CutType],
				[BillingAuthorizationId],[ContractId],[InvoiceValue],[ValueTax],[TotalValue],[CUFE], [CurrencyId]
			)
			SELECT TOP 1 
					iec.OperatingUnitId,4,@InvoiceNumber,c.HealthAdministratorId,ha.ThirdPartyId,cg.Id,
					@DocumentDate,DATEADD(DAY, cg.InvoiceDeadlines, @DocumentDate),iec.TotalValue,iec.InitialDate,iec.EndDate,
					iec.UserNumber,iec.UserValue,iec.TotalValue,iec.DiscountValue,
					0,0,0,0,
					0,0,0,iec.TotalValue,
					0,0,0,1,CONCAT(IIF(cd.PermanentObservationOfTheInvoice IS NULL, '', cd.PermanentObservationOfTheInvoice + ' - '), @Observations) AS Observations,
					@CodeUser,Common.[GETDATE](),iec.InvoiceCategoryId,Common.[GETDATE](),0,iec.InitialDate,0,
					@BillingAuthorizationId,cg.ContractId,iec.TotalValue,0,iec.TotalValue, IIF(ISNULL(ba.InvoiceType, 0) = 3, 'CUFE', NULL), iec.CurrencyId
			FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON iec.CareGroupId = cg.Id
			JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id
			JOIN Contract.HealthAdministrator ha WITH (NOLOCK) ON c.HealthAdministratorId = ha.Id
			JOIN Billing.BillingAuthorization ba WITH (NOLOCK) ON iec.BillingAuthorizationId = ba.Id
			LEFT JOIN Contract.ContractDetail cd WITH (NOLOCK) ON c.Id = cd.ContractId AND cd.ValidRecord = 1
			WHERE iec.Id = @Id

			SET @InvoiceId = SCOPE_IDENTITY()

			SET @Message_Output = CONCAT('Factura número: ', @InvoiceNumber)
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

			---------------------------------------------  ASOCIACION FACTURA ---------------------------------------------

			UPDATE Billing.InvoiceEntityCapitated
				SET [InvoiceId] = @InvoiceId
			WHERE Id = @Id

			---------------------------------------- GENERACION CUENTAS POR COBRAR ----------------------------------------

			EXEC Common.SP_GetSequence 160, 682, @OperatingUnitId, NULL, NULL, @IsManual OUT, @PortfolioAccountReceivableCode OUT, @Code_Output OUT, @Message_Output OUT
			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = REPLACE(@Message_Output, '{0}', 'Cuentas Por Cobrar')
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
				[PreviousPeriodReversalAccountDeteriorationId],[AccountHardCollectionId],AffectBudget,BudgetId,CareGroupId,[CurrencyId],[CreationUser],[CreationDate]
			)
			SELECT	@PortfolioAccountReceivableCode,2,i.ThirdPartyId,c.Id,i.Id,i.InvoiceNumber,i.InvoiceDate,
					cg.InvoiceDeadlines,i.InvoiceExpirationDate,'',1,0,0,0,
					i.InvoiceCategoryId,cg.CostCenterId,i.OperatingUnitId,cas.AccountWithoutRadicateId,1,i.ThirdPartySalesValue,i.ThirdPartySalesValue,
					2,cas.AccountWithoutRadicateId,cas.AccountRadicateId,cas.AccountObjectionRemediedId,cas.AccountConciliationId,
					cas.AccountLegalCollectionId,cas.AccountDebitOrderId,cas.AccountCreditOrderId,cas.CreditProvisionAccountId,cas.DebitProvisionAccountId,
					cas.CreditAccountDeteriorationId,cas.DebitAccountDeteriorationId,cas.ReversalAccountDeteriorationId,
					cas.PreviousPeriodReversalAccountDeteriorationId,cas.AccountHardCollectionId,cg.AffectBudget,cg.BillingBudgetId,cg.Id,i.CurrencyId,i.InvoicedUser,i.InvoicedDate
			FROM Billing.Invoice i WITH (NOLOCK)
			JOIN Contract.CareGroup cg WITH (NOLOCK) ON i.CareGroupId = cg.Id
			JOIN Contract.ContractAccountingStructure cas WITH (NOLOCK) ON cg.ContractAccountingStructureId = cas.Id
			LEFT JOIN Common.Customer c WITH (NOLOCK) ON i.ThirdPartyId = c.ThirdPartyId
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

			SET @Message_Output = CONCAT('Código Cuenta por Cobrar ',  @PortfolioAccountReceivableCode)
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

			--------------------------------------------------  ANTICIPO --------------------------------------------------
			IF @ListPortfolioAdvanceXml IS NOT NULL BEGIN

				DECLARE @ListPortfolioAdvanceCrossing TABLE
				(
					RowId INT IDENTITY(1,1) PRIMARY KEY,
					Id INT,
					Code VARCHAR(20),
					CrossingValue DECIMAL(18, 2),
					CashReceiptDetailIdTmp INT
				)

				INSERT INTO @ListPortfolioAdvanceCrossing
				SELECT t.x.value('Id[1]', 'INT'),
					t.x.value('Code[1]', 'VARCHAR(20)'),
					t.x.value('CrossingValue[1]', 'DECIMAL(18, 2)'),
					t.x.value('CashReceiptDetailIdTmp[1]', 'INT')
				FROM @InvoiceEntityCapitatedXml.nodes('InvoiceEntityCapitated/ListPortfolioAdvanceCrossing') t(x)

				INSERT INTO Billing.InvoicePortfolioAdvance(InvoiceId, PortfolioAdvanceId, Value)
				SELECT @InvoiceId,lpac.Id, lpac.CrossingValue
				FROM @ListPortfolioAdvanceCrossing lpac

				EXEC [Portfolio].[SP_GeneratePortfolioTransfer]  @ListPortfolioAdvanceXml
																,@OperatingUnitId
																,@CodeUser
																,@PortfolioAccountReceivableId
																,@CompanyType
																,@Code_Output OUTPUT
																,@Message_Output OUTPUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = 'Error al cruzar el cruce de anticipo vs CxC'
					RETURN
				END

				SET @Message_Output = ISNULL(@Message_Output, '')
				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
			END

			-------------------------------------- GENERACION DE FACTURA ELECTRONICA --------------------------------------

			INSERT INTO Billing.ElectronicDocument
			(
				DianVersion, OperatingUnitId, CustomerPartyId, EntityId, EntityName, DocumentDate, DocumentType, Status, CreationDate, Container, FilePath, Prefix, DocumentNumber, CUFE, Year
			)
			SELECT TOP 1
				gls.DianVersion, i.OperatingUnitId, i.ThirdPartyId, i.Id, 'Invoice', i.InvoiceDate, i.DocumentType, 1, i.InvoicedDate, DB_NAME(), [Billing].[GetElectronicDocumentFilePath](@FilePath, ou.UnitCode, i.InvoiceDate, i.DocumentType, i.InvoiceNumber), ba.InvoicePrefix, REPLACE(i.InvoiceNumber, ba.InvoicePrefix, ''), 'CUFE', YEAR(i.InvoiceDate)
			FROM Billing.Invoice i WITH (NOLOCK)
			JOIN GeneralLedger.GeneralLedgerSettings gls WITH (NOLOCK) ON i.OperatingUnitId = gls.IdOperatingUnit
			JOIN Billing.BillingAuthorization ba WITH (NOLOCK) ON i.BillingAuthorizationId = ba.Id
			JOIN Common.OperatingUnit ou WITH (NOLOCK) On i.OperatingUnitId = ou.Id
			WHERE i.Id = @InvoiceId AND gls.HandlesElectronicBilling = 1 AND ba.InvoiceType = 3

			----------------------------------------- RECONOCIMIENTO PRESUPUESTAL -----------------------------------------
		
			EXEC [Portfolio].[SP_GenerateRecognitionByAccountReceivableId_Output] @OperatingUnitId, @PortfolioAccountReceivableId, @CodeUser, @Code_Output OUT, @Message_Output OUT
			
			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el reconocimiento presupuestal')
				RETURN
			END

			SET @Message_Output = ISNULL(@Message_Output, '')
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

			--------------------------------------------  COMPROBANTE CONTABLE --------------------------------------------

			SET @SubXml = CONVERT
			(
				XML, 
				(
					SELECT *
					FROM 
					(
						SELECT	0 Id,
								0 Consecutive,
								sb.InvoiceJournalVoucherTypeId IdJournalVoucher,
								iec.DocumentDate VoucherDate, 
								'False' Imported,
								2 Status,
								'' Detail, 
								'InvoiceEntityCapitated' EntityName,
								@Code EntityCode,
								@Id EntityId,
								0 IsClosedYear
						FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
						JOIN Billing.SettingsBilling sb WITH (NOLOCK) ON iec.OperatingUnitId = sb.IdOperatingUnit
						WHERE iec.Id = @Id
					) JournalVoucher
					JOIN
					( 
							SELECT	0 Id,
									0 IdAccounting,
									ma.Id IdMainAccount,
									IIF(ma.HandlesThirdParty = 1, ha.ThirdPartyId, NULL) IdThirdParty, 
									IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) IdCostCenter, 									
									0 DebitValue, 
									iec.TotalValue CreditValue
							FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
							JOIN Contract.CareGroup cg WITH (NOLOCK) ON iec.CareGroupId = cg.Id
							JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id
							JOIN Contract.HealthAdministrator ha WITH (NOLOCK) ON c.HealthAdministratorId = ha.Id
							JOIN Billing.SettingsBilling sb WITH (NOLOCK) ON iec.OperatingUnitId = sb.IdOperatingUnit
							JOIN GeneralLedger.MainAccounts ma ON sb.CapitationRevenueMainAccountId = ma.Id
							WHERE iec.Id = @Id
						UNION ALL
							SELECT	0 Id,
									0 IdAccounting,
									ma.Id IdMainAccount,
									IIF(ma.HandlesThirdParty = 1, ha.ThirdPartyId, NULL) IdThirdParty, 
									IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) IdCostCenter, 									
									iec.TotalValue DebitValue, 
									0 CreditValue
							FROM Billing.InvoiceEntityCapitated iec WITH (NOLOCK)
							JOIN Contract.CareGroup cg WITH (NOLOCK) ON iec.CareGroupId = cg.Id
							JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id
							JOIN Contract.HealthAdministrator ha WITH (NOLOCK) ON c.HealthAdministratorId = ha.Id
							JOIN Contract.ContractAccountingStructure cas WITH (NOLOCK) ON cg.ContractAccountingStructureId = cas.Id
							JOIN GeneralLedger.MainAccounts ma ON cas.AccountWithoutRadicateId = ma.Id
							WHERE iec.Id = @Id
					) JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
					For xml AUTO,TYPE, ELEMENTS
				)
			)
					
			--Se consume el sp que guarda el movimiento contable
			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml,@CodeUser 
			select 
				@Code_Output = rjv.code, 
				@Message_Output = rjv.MessageResult, 
				@JournalVoucherId = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv
					
			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Ocurrieron errores al intentar Generar el comprobante contable: ' + ISNULL(@Message_Output, 'No se pudo generar el comprobante contable')
				RETURN
			END

			UPDATE Billing.Invoice SET JournalVoucherId = @JournalVoucherId WHERE Id = @InvoiceId

			SELECT @Message_Output = CONCAT('Se generó el Comprobante contable ', jv.Consecutive, ' de tipo ', jvt.Code, ' - ', jvt.Name)
			FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
			JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) On jv.IdJournalVoucher = jvt.Id
			Where jv.Id = @JournalVoucherId

			SELECT @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END
	END

	/********************************** *************************************** **********************************/

	IF @Status = 1
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Billing.BillingControl WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code)
		BEGIN
			INSERT INTO Billing.BillingControl (DocumentNumber, DocumentType, DocumentUser, DocumentDate)
			SELECT @Code, @DocumentTypeControl, @CodeUser, @DocumentDate
		END
	END
	ELSE
	BEGIN
		DELETE FROM Billing.BillingControl WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code
	END

	---------------------------------------------------- RESULTADO ----------------------------------------------------

	SELECT	@CodeResult = 0, 
			@MessageResult = @Message
END