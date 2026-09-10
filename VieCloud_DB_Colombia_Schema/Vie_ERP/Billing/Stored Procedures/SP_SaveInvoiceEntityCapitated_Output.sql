-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-11
-- Description:	Guarda, actualiza o confirma una factura de monto fijo (capitada).
--              Flujo: Validaciones -> Cabecera -> Confirmación (Factura, CxC, Anticipo, Electrónica, Contable)
--
-- INSERTS (orden de ejecución):
--   1. Billing.InvoiceEntityCapitated         - Cabecera factura monto fijo (nueva)
--   2. Billing.InvoiceEntityCapitatedGroupers - Agrupadores PGP (si LiquidationType=5)
--   3. Billing.Invoice                        - Factura principal
--   4. Portfolio.AccountReceivable            - Cuenta por cobrar
--   5. Portfolio.AccountReceivableShare       - Cuota CxC
--   6. Portfolio.AccountReceivableAccounting - Contabilidad CxC
--   7. @ListPortfolioAdvanceCrossing          - Temp: anticipos a cruzar
--   8. Billing.InvoicePortfolioAdvance        - Cruce factura vs anticipos
--   9. Billing.ElectronicDocument            - Documento electrónico
--  10. Billing.BillingControl                - Control facturación (Status=1)
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

	-- Variables cabecera (desde XML)
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
			@CopaymentAmount DECIMAL(18,2),
			@ModeratingFeeAmount DECIMAL(18,2),
			@SharedPaymentAmount DECIMAL(18,2),
			@Status TINYINT,
			@FilePath VARCHAR(MAX),
			@Observations VARCHAR(MAX),
			-------------------------------------------------------------------
			@IdForm INT = 758,
			@DocumentTypeControl INT = 1,
			-------------------------------------------------------------------
			@InvoiceId INT,
			@IdElectronicDocument INT,
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

	-- Extraer datos de cabecera desde XML
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
			@CompanyType = t.x.value('CompanyType[1]','tinyint'),
			@CopaymentAmount = ISNULL(t.x.value('CopaymentAmount[1]','decimal(18,2)'), 0),
			@ModeratingFeeAmount = ISNULL(t.x.value('ModeratingFeeAmount[1]','decimal(18,2)'), 0),
			@SharedPaymentAmount = ISNULL(t.x.value('SharedPaymentAmount[1]','decimal(18,2)'), 0)
	FROM @InvoiceEntityCapitatedXml.nodes('/InvoiceEntityCapitated') t(x)

	SELECT @BillingAuthorizationId = sb.EntityCapitatedBillingAuthorizationId
	FROM Billing.SettingsBilling sb 
	WHERE sb.IdOperatingUnit = @OperatingUnitId

	DECLARE @resultJournalVoucher TABLE (code VARCHAR(20), MessageResult VARCHAR(MAX), IdJournalVoucher INT)

	-- ==================== VALIDACIONES ====================

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
		-- Validaciones para confirmación
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
			FROM Billing.BillingAuthorization 
			WHERE Id = @BillingAuthorizationId AND [Status] = 1
		) BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'La autorizacion de facturacion esta inactiva'
			RETURN
		END

		IF NOT EXISTS (
			SELECT 1
			FROM Billing.BillingAuthorization ba 
			JOIN Billing.BillingAuthorizationUser bau  ON ba.Id = bau.BillingAuthorizationId
			WHERE ba.Id = @BillingAuthorizationId AND bau.UserCode = @CodeUser
		) BEGIN
			SELECT @CodeResult = 999,
				   @MessageResult = 'No tiene permitido usar la autorización n°: ' + ba.Code + ' - ' + ba.Name
			FROM Billing.BillingAuthorization ba 
			WHERE ba.Id = @BillingAuthorizationId
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Billing.BillingAuthorization 
			WHERE Id = @BillingAuthorizationId AND [Status] = 1
				AND (@DocumentDate < InitialDate OR @DocumentDate >= FinalDate)
		) BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'La autorización asignada no se encuentra vigente para la fecha del documento'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg 
			WHERE cg.Id = @CareGroupId AND cg.Status = 0
		) BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'El grupo de atención está inactivo'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg 
			WHERE cg.Id = @CareGroupId AND cg.ContractAccountingStructureId IS NULL
		) BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'El grupo de atención no tiene asignado la estructura contable'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg 
			LEFT JOIN Contract.Contract c  ON cg.ContractId = c.Id AND c.Status = 1
			WHERE cg.Id = @CareGroupId AND c.Id IS NULL --Las facturas monto fijo debe tener un contrato
		) BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'El grupo de atención no tiene un contrato vigente o válido'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg 
			LEFT JOIN Contract.ContractDetail cd  ON cg.ContractId = cd.ContractId AND cd.ValidRecord = 1
			WHERE cg.Id = @CareGroupId AND cd.Id IS NULL
		) BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'El grupo de atención no tiene contrato o este no tiene un detalle válido'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg 
			JOIN Contract.ContractDetail cd  ON cg.ContractId = cd.ContractId AND cd.ValidRecord = 1 AND cd.TerminationControl IN (2, 4)
			WHERE cg.Id = @CareGroupId AND (@DocumentDate > cd.EndDate OR @DocumentDate > cd.BillingEndDate)
		) BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'El contrato asociado al grupo de atención ya venció'
			RETURN
		END

		IF EXISTS (
			SELECT 1
			FROM Contract.CareGroup cg 
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
			FROM Contract.CareGroup cg 
			JOIN Contract.Contract c  ON cg.ContractId = c.Id
			JOIN Contract.HealthAdministrator ha  ON c.HealthAdministratorId = ha.Id
			JOIN Common.ThirdParty tp  ON ha.ThirdPartyId = tp.Id
			LEFT JOIN Common.Address a  ON tp.PersonId = a.IdPerson
			WHERE cg.Id = @CareGroupId AND (a.Id IS NULL OR a.DepartmentId IS NULL OR a.CityId IS NULL)
		) BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'El tercero no tiene parametrizada una dirección válida'
			RETURN
		END

		IF NOT EXISTS (
			SELECT 1
			FROM Portfolio.PortfolioSequence s 
			JOIN Portfolio.PortfolioSequenceDetail sd  ON s.Id = sd.IdSequensePortfolioC
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

		-- ==================== INSERTAR / ACTUALIZAR CABECERA ====================

		DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status in (2 ,5)THEN @CodeUser ELSE NULL END
		DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status in (2,5) THEN [Common].[GETDATE]() ELSE NULL END

		IF @Id = 0
		BEGIN
			-- Nueva factura: obtener secuencia numérica
			DECLARE @IsManual BIT

			EXEC Common.SP_GetSequence 180, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = REPLACE(@Message_Output, '{0}', 'factura monto fijo')
				RETURN
			END

			-- Insertar cabecera
			INSERT INTO Billing.InvoiceEntityCapitated
			(
				[OperatingUnitId],           -- 1
				[BillingAuthorizationId],    -- 2
				[CareGroupId],              -- 3
				[InvoiceCategoryId],        -- 4
				[Code],                     -- 5
				[DocumentDate],             -- 6
				[InitialDate],              -- 7
				[EndDate],                  -- 8
				[UserNumber],               -- 9
				[UserValue],                -- 10
				[DiscountPercentage],       -- 11
				[DiscountValue],            -- 12
				[TotalValue],               -- 13
				[CopaymentAmount],          -- 14
				[ModeratingFeeAmount],      -- 15
				[SharedPaymentAmount],      -- 16
				[Status],                   -- 17
				[CreationUser],             -- 18
				[CreationDate],             -- 19
				[ModificationUser],         -- 20
				[ModificationDate],         -- 21
				[ConfirmationUser],         -- 22
				[ConfirmationDate],         -- 23
				[Observations],             -- 24
				[InvoicePeriod],            -- 25
				[PreviousRIPSInvoice],      -- 26
				[CurrencyId]                -- 27
			)
			SELECT	@OperatingUnitId,           -- 1
					@BillingAuthorizationId,    -- 2
					@CareGroupId,               -- 3
					@InvoiceCategoryId,         -- 4
					@Code,                      -- 5
					@DocumentDate,              -- 6
					@InitialDate,               -- 7
					@EndDate,                   -- 8
					@UserNumber,                -- 9
					@UserValue,                 -- 10
					@DiscountPercentage,        -- 11
					@DiscountValue,             -- 12
					@TotalValue,                -- 13
					@CopaymentAmount,           -- 14
					@ModeratingFeeAmount,       -- 15
					@SharedPaymentAmount,      -- 16
					@Status,                    -- 17
					@CodeUser,                  -- 18
					[Common].[GETDATE](),       -- 19
					@ConfirmationUser,          -- 20
					@ConfirmationDate,          -- 21
					@ConfirmationUser,          -- 22
					@ConfirmationDate,          -- 23
					@Observations,               -- 24
					@InvoicePeriod,              -- 25
					@PreviousRIPSInvoice,       -- 26
					@CurrencyId                  -- 27

			SET @Id = SCOPE_IDENTITY()
		END
		ELSE
		BEGIN
			-- Actualizar cabecera existente
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
					[CopaymentAmount] = @CopaymentAmount,
					[ModeratingFeeAmount] = @ModeratingFeeAmount,
					[SharedPaymentAmount] = @SharedPaymentAmount,
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

		-- ==================== CONFIRMACIÓN ====================

		IF @Status = 2
		BEGIN
			-- Auditoría de agrupadores (PGP)
			IF EXISTS (
				SELECT 1
				FROM Contract.CareGroup cg 
				WHERE cg.Id = @CareGroupId AND  cg.LiquidationType = 5
			) BEGIN
				INSERT INTO [Billing].[InvoiceEntityCapitatedGroupers]
				(
					InvoiceEntityCapitatedId,  -- 1
					GroupersId,                -- 2
					Code,                      -- 3
					Description,               -- 4
					UserMin,                   -- 5
					UserMax,                   -- 6
					ProjectCME,                -- 7
					TotalContract              -- 8
				)
				SELECT	@Id,           -- 1
						g.Id,          -- 2
						g.Code,        -- 3
						g.Description, -- 4
						g.UserMin,     -- 5
						g.UserMax,     -- 6
						g.ProjectCME,  -- 7
						g.TotalContract -- 8
				FROM [Contract].[GroupersCareGroup] gcg
				JOIN [Contract].[Groupers] g ON gcg.GroupersId = g.Id
				WHERE @CareGroupId = gcg.CareGroupId
			END

			-- Generar número de factura
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
						FROM Billing.InvoiceEntityCapitated iec 
						JOIN Contract.CareGroup cg  ON iec.CareGroupId = cg.Id
						WHERE cg.LiquidationType = 5 AND @DocumentDate < iec.EndDate AND iec.Id = @Id) BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = 'La fecha de la factura no puede ser menor a la fecha final'
				RETURN
			END

			SET @InvoiceNumber = CONCAT(@InvoicePrefix, @AuthorizationConsecutive)

			-- Insertar factura capitada (copago + cuota moderadora + cuota compartida)
			DECLARE @PatientRecoveryTotal DECIMAL(18,2)
			SELECT @PatientRecoveryTotal = CopaymentAmount + ModeratingFeeAmount + SharedPaymentAmount
			FROM Billing.InvoiceEntityCapitated WHERE Id = @Id

			INSERT INTO Billing.Invoice
			(
				[OperatingUnitId],              -- 1
				[DocumentType],                 -- 2  Factura Capitada
				[InvoiceNumber],                -- 3
				[HealthAdministratorId],        -- 4
				[ThirdPartyId],                 -- 5
				[CareGroupId],                  -- 6
				[InvoiceDate],                  -- 7
				[InvoiceExpirationDate],        -- 8
				[TotalInvoice],                 -- 9
				[CapitationInitialDate],        -- 10
				[CapitationEndDate],            -- 11
				[CapitationlPatientsAmount],    -- 12
				[CapitationPatientValue],       -- 13
				[ThirdPartySalesValue],         -- 14
				[ThirdPartyDiscountValue],      -- 15
				[ResponsibleRecoveryFee],       -- 16  2=Paciente, 1=Ninguno
				[TotalPatientSalesPrice],       -- 17
				[PatientDiscount],              -- 18
				[PatientDiscountPercentage],    -- 19
				[TotalPatientWithDiscount],     -- 20
				[ValueVoucher],                 -- 21
				[PatientPaidValue],             -- 22
				[ThirdPartyAccountReceivableValue], -- 23
				[PatientAccountReceivableValue],   -- 24
				[CREETaxRetentionValue],        -- 25
				[CREETaxRetentionBaseValue],    -- 26
				[Status],                       -- 27
				[Observation],                  -- 28
				[InvoicedUser],                 -- 29
				[InvoicedDate],                 -- 30
				[InvoiceCategoryId],            -- 31
				[OutputDate],                   -- 32
				[IsCutAccount],                 -- 33
				[InitialDate],                  -- 34
				[CutType],                      -- 35
				[BillingAuthorizationId],      -- 36
				[ContractId],                   -- 37
				[InvoiceValue],                 -- 38
				[ValueTax],                     -- 39
				[TotalValue],                   -- 40
				[CUFE],                         -- 41
				[CurrencyId]                     -- 42
			)
			SELECT TOP 1
				iec.OperatingUnitId,            -- 1
				4,                              -- 2  DocumentType: Factura Capitada
				@InvoiceNumber,                 -- 3
				c.HealthAdministratorId,        -- 4
				ha.ThirdPartyId,                 -- 5
				cg.Id,                          -- 6
				@DocumentDate,                  -- 7
				DATEADD(DAY, cg.InvoiceDeadlines, @DocumentDate), -- 8
				iec.TotalValue,                 -- 9
				iec.InitialDate,                -- 10
				iec.EndDate,                    -- 11
				iec.UserNumber,                 -- 12
				iec.UserValue,                  -- 13
				iec.TotalValue,                 -- 14  ThirdPartySalesValue
				iec.DiscountValue,              -- 15
				IIF(@PatientRecoveryTotal > 0, 2, 1), -- 16  ResponsibleRecoveryFee: 2=Paciente, 1=Ninguno
				@PatientRecoveryTotal,          -- 17  TotalPatientSalesPrice (copago + cuota mod + cuota comp)
				0,                              -- 18  PatientDiscount
				0,                              -- 19  PatientDiscountPercentage
				@PatientRecoveryTotal,          -- 20  TotalPatientWithDiscount
				0,                              -- 21  ValueVoucher
				0,                              -- 22  PatientPaidValue
				iec.TotalValue,                 -- 23  ThirdPartyAccountReceivableValue
				0,                              -- 24  PatientAccountReceivableValue
				0,                              -- 25  CREETaxRetentionValue
				0,                              -- 26  CREETaxRetentionBaseValue
				1,                              -- 27  Status
				CONCAT(IIF(cd.PermanentObservationOfTheInvoice IS NULL, '', cd.PermanentObservationOfTheInvoice + ' - '), @Observations), -- 28
				@CodeUser,                      -- 29
				Common.[GETDATE](),             -- 30
				iec.InvoiceCategoryId,          -- 31
				Common.[GETDATE](),             -- 32
				0,                              -- 33
				iec.InitialDate,                -- 34
				0,                              -- 35
				@BillingAuthorizationId,       	-- 36
				cg.ContractId,                  -- 37
				@SubTotalValue,                 -- 38  InvoiceValue
				0,                              -- 39  ValueTax
				iec.TotalValue,                 -- 40  TotalValue = PayableAmount (subtotal - descuentos - recaudos)
				IIF(ISNULL(ba.InvoiceType, 0) = 3, 'CUFE', NULL), -- 41
				iec.CurrencyId                  -- 42
			FROM Billing.InvoiceEntityCapitated iec 
			JOIN Contract.CareGroup cg  ON iec.CareGroupId = cg.Id
			JOIN Contract.Contract c  ON cg.ContractId = c.Id
			JOIN Contract.HealthAdministrator ha  ON c.HealthAdministratorId = ha.Id
			JOIN Billing.BillingAuthorization ba  ON iec.BillingAuthorizationId = ba.Id
			LEFT JOIN Contract.ContractDetail cd  ON c.Id = cd.ContractId AND cd.ValidRecord = 1
			WHERE iec.Id = @Id

			SET @InvoiceId = SCOPE_IDENTITY()
			SET @Message_Output = CONCAT('Factura número: ', @InvoiceNumber)
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

			-- Asociar factura a cabecera

			UPDATE Billing.InvoiceEntityCapitated
				SET [InvoiceId] = @InvoiceId
			WHERE Id = @Id

			-- Cuentas por cobrar

			EXEC Common.SP_GetSequence 160, 682, @OperatingUnitId, NULL, NULL, @IsManual OUT, @PortfolioAccountReceivableCode OUT, @Code_Output OUT, @Message_Output OUT
			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = REPLACE(@Message_Output, '{0}', 'Cuentas Por Cobrar')
				RETURN
			END

			INSERT INTO [Portfolio].[AccountReceivable]
			(
				[Code],                                 -- 1
				[AccountReceivableType],                 -- 2
				[ThirdPartyId],                          -- 3
				[CustomerId],                            -- 4
				[InvoiceId],                             -- 5
				[InvoiceNumber],                         -- 6
				[AccountReceivableDate],                 -- 7
				[Term],                                  -- 8
				[ExpiredDate],                           -- 9
				[Observations],                          -- 10
				[PortfolioStatus],                       -- 11
				[OpeningBalance],                        -- 12
				[PaymentAgreement],                      -- 13
				[RegistrationAdjusted],                 -- 14
				[InvoiceCategoryId],                     -- 15
				[CostCenterId],                          -- 16
				[OperatingUnitId],                       -- 17
				[MainAccountWithoutFilingId],            -- 18
				[NumberShares],                          -- 19
				[Value],                                 -- 20
				[Balance],                               -- 21
				[Status],                                -- 22
				[AccountWithoutRadicateId],              -- 23
				[AccountRadicateId],                     -- 24
				[AccountObjectionRemediedId],            -- 25
				[AccountConciliationId],                -- 26
				[AccountLegalCollectionId],              -- 27
				[AccountDebtorOrder],                    -- 28
				[AccountCreditorOrder],                  -- 29
				[CreditProvisionAccountId],              -- 30
				[DebitProvisionAccountId],               -- 31
				[CreditAccountDeteriorationId],          -- 32
				[DebitAccountDeteriorationId],           -- 33
				[ReversalAccountDeteriorationId],       -- 34
				[PreviousPeriodReversalAccountDeteriorationId], -- 35
				[AccountHardCollectionId],               -- 36
				AffectBudget,                            -- 37
				BudgetId,                                -- 38
				CareGroupId,                             -- 39
				[CurrencyId],                            -- 40
				[CreationUser],                          -- 41
				[CreationDate]                           -- 42
			)
			SELECT	@PortfolioAccountReceivableCode,     -- 1
					2,                                   -- 2
					i.ThirdPartyId,                       -- 3
					c.Id,                                 -- 4
					i.Id,                                 -- 5
					i.InvoiceNumber,                      -- 6
					i.InvoiceDate,                        -- 7
					cg.InvoiceDeadlines,                  -- 8
					i.InvoiceExpirationDate,              -- 9
					'',                                  -- 10
					1,                                    -- 11
					0,                                    -- 12
					0,                                    -- 13
					0,                                    -- 14
					i.InvoiceCategoryId,                  -- 15
					cg.CostCenterId,                      -- 16
					i.OperatingUnitId,                     -- 17
					cas.AccountWithoutRadicateId,         -- 18
					1,                                    -- 19
					i.TotalValue,                         -- 20  Value = PayableAmount (lo que la entidad paga)
					i.TotalValue,                         -- 21  Balance = PayableAmount
					2,                                    -- 22
					cas.AccountWithoutRadicateId,         -- 23
					cas.AccountRadicateId,                -- 24
					cas.AccountObjectionRemediedId,       -- 25
					cas.AccountConciliationId,            -- 26
					cas.AccountLegalCollectionId,         -- 27
					cas.AccountDebitOrderId,              -- 28
					cas.AccountCreditOrderId,             -- 29
					cas.CreditProvisionAccountId,         -- 30
					cas.DebitProvisionAccountId,          -- 31
					cas.CreditAccountDeteriorationId,      -- 32
					cas.DebitAccountDeteriorationId,      -- 33
					cas.ReversalAccountDeteriorationId,   -- 34
					cas.PreviousPeriodReversalAccountDeteriorationId, -- 35
					cas.AccountHardCollectionId,          -- 36
					cg.AffectBudget,                      -- 37
					cg.BillingBudgetId,                   -- 38
					cg.Id,                                -- 39
					i.CurrencyId,                         -- 40
					i.InvoicedUser,                       -- 41
					i.InvoicedDate                        -- 42
			FROM Billing.Invoice i 
			JOIN Contract.CareGroup cg  ON i.CareGroupId = cg.Id
			JOIN Contract.ContractAccountingStructure cas  ON cg.ContractAccountingStructureId = cas.Id
			LEFT JOIN Common.Customer c  ON i.ThirdPartyId = c.ThirdPartyId
			WHERE i.Id = @InvoiceId

			SET @PortfolioAccountReceivableId = SCOPE_IDENTITY()

			INSERT INTO Portfolio.AccountReceivableShare
			(
				AccountReceivableId,           -- 1
				Number,                        -- 2
				ExpiredDate,                   -- 3
				[Value],                       -- 4
				Balance,                       -- 5
				DebitValue,                    -- 6
				CreditValue,                   -- 7
				TransferValue,                 -- 8
				PaymentValue,                  -- 9
				CrossingValue,                 -- 10
				InterestValue,                 -- 11
				SurchargesValue,               -- 12
				CapitalRepaymentAgreement,     -- 13
				FinancialInterest,             -- 14
				RepaymentAgreementInterest     -- 15
			)
			SELECT	@PortfolioAccountReceivableId, -- 1
					1,                          -- 2
					ar.ExpiredDate,              -- 3
					ar.Value,                    -- 4
					ar.Balance,                  -- 5
					0,                           -- 6
					0,                           -- 7
					0,                           -- 8
					0,                           -- 9
					0,                           -- 10
					0,                           -- 11
					0,                           -- 12
					0,                           -- 13
					0,                           -- 14
					0                            -- 15
			FROM Portfolio.AccountReceivable ar 
			WHERE ar.Id = @PortfolioAccountReceivableId

			INSERT INTO Portfolio.AccountReceivableAccounting
			(
				AccountReceivableId,  -- 1
				MainAccountId,       -- 2
				ThirdPartyId,        -- 3
				CostCenterId,        -- 4
				[Value],             -- 5
				Balance              -- 6
			)
			SELECT	@PortfolioAccountReceivableId,                    -- 1
					ma.Id,                                             -- 2
					IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL), -- 3
					IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL),  -- 4
					ar.Value,                                          -- 5
					ar.Balance                                         -- 6
			FROM Portfolio.AccountReceivable ar 
			JOIN GeneralLedger.MainAccounts ma  ON ar.AccountWithoutRadicateId = ma.Id
			WHERE ar.Id = @PortfolioAccountReceivableId

			SET @Message_Output = CONCAT('Código Cuenta por Cobrar ', @PortfolioAccountReceivableCode)
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

			-- Cruce de anticipos
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
				(
					Id,                    -- 1
					Code,                  -- 2
					CrossingValue,         -- 3
					CashReceiptDetailIdTmp -- 4
				)
				SELECT	t.x.value('Id[1]', 'INT'),                    -- 1
						t.x.value('Code[1]', 'VARCHAR(20)'),           -- 2
						t.x.value('CrossingValue[1]', 'DECIMAL(18, 2)'), -- 3
						t.x.value('CashReceiptDetailIdTmp[1]', 'INT')  -- 4
				FROM @InvoiceEntityCapitatedXml.nodes('InvoiceEntityCapitated/ListPortfolioAdvanceCrossing') t(x)

				INSERT INTO Billing.InvoicePortfolioAdvance
				(
					InvoiceId,         -- 1
					PortfolioAdvanceId, -- 2
					Value              -- 3
				)
				SELECT	@InvoiceId,      -- 1
						lpac.Id,          -- 2
						lpac.CrossingValue -- 3
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

			-- Factura electrónica

			SET @IdElectronicDocument = NULL;

			INSERT INTO Billing.ElectronicDocument
			(
				DianVersion,       -- 1
				OperatingUnitId,   -- 2
				CustomerPartyId,   -- 3
				EntityId,          -- 4
				EntityName,        -- 5
				DocumentDate,      -- 6
				DocumentType,      -- 7
				Status,            -- 8
				CreationDate,      -- 9
				Container,         -- 10
				FilePath,          -- 11
				Prefix,            -- 12
				DocumentNumber,    -- 13
				CUFE,              -- 14
				Year               -- 15
			)
			SELECT TOP 1
				gls.DianVersion,                                                                                    -- 1
				i.OperatingUnitId,                                                                                   -- 2
				i.ThirdPartyId,                                                                                      -- 3
				i.Id,                                                                                                -- 4
				'Invoice',                                                                                           -- 5
				i.InvoiceDate,                                                                                       -- 6
				i.DocumentType,                                                                                      -- 7
				1,                                                                                                   -- 8
				i.InvoicedDate,                                                                                      -- 9
				DB_NAME(),                                                                                           -- 10
				[Billing].[GetElectronicDocumentFilePath](@FilePath, ou.UnitCode, i.InvoiceDate, i.DocumentType, i.InvoiceNumber), -- 11
				ba.InvoicePrefix,                                                                                    -- 12
				REPLACE(i.InvoiceNumber, ba.InvoicePrefix, ''),                                                      -- 13
				'CUFE',                                                                                              -- 14
				YEAR(i.InvoiceDate)                                                                                  -- 15
			FROM Billing.Invoice i 
			JOIN GeneralLedger.GeneralLedgerSettings gls  ON i.OperatingUnitId = gls.IdOperatingUnit
			JOIN Billing.BillingAuthorization ba  ON i.BillingAuthorizationId = ba.Id
			JOIN Common.OperatingUnit ou  On i.OperatingUnitId = ou.Id
			WHERE i.Id = @InvoiceId AND gls.HandlesElectronicBilling = 1 AND ba.InvoiceType = 3

			SET @IdElectronicDocument = SCOPE_IDENTITY();

			INSERT INTO Billing.OutboxEvent
			(
				EventType,
				AggregateType,
				AggregateId,
				PayloadJson,
				OccurredAtUtc
			)
			VALUES
			(
				'Billing.InvoiceConfirmed.v1',
				'BillingRecord',
				CAST(@InvoiceId AS NVARCHAR(255)),
				CONCAT(
					N'{"ElectronicDocumentId":', CAST(@IdElectronicDocument AS NVARCHAR(20)),
					N',"InvoiceId":', CAST(@InvoiceId AS NVARCHAR(20)),
					N',"InvoiceNumber":"', @InvoiceNumber, N'"}'
				),
				[Common].[GETDATE]()
			);

			-- Reconocimiento presupuestal

			EXEC [Portfolio].[SP_GenerateRecognitionByAccountReceivableId_Output] @OperatingUnitId, @PortfolioAccountReceivableId, @CodeUser, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el reconocimiento presupuestal')
				RETURN
			END

			SET @Message_Output = ISNULL(@Message_Output, '')
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)

			-- Comprobante contable

			DECLARE @ValidationCareGroupCode VARCHAR(20), @ValidationAccountingStructureCode VARCHAR(20)

			IF EXISTS (
				SELECT 1
				FROM Billing.InvoiceEntityCapitated iec 
				JOIN Contract.CareGroup cg  ON iec.CareGroupId = cg.Id
				JOIN Billing.SettingsBilling sb  ON iec.OperatingUnitId = sb.IdOperatingUnit
				WHERE iec.Id = @Id AND sb.CapitationRevenueMainAccountId IS NULL
			) BEGIN
				SELECT	@ValidationCareGroupCode = cg.Code
				FROM Billing.InvoiceEntityCapitated iec 
				JOIN Contract.CareGroup cg  ON iec.CareGroupId = cg.Id
				WHERE iec.Id = @Id

				SELECT	@CodeResult = 999,
						@MessageResult = 'El grupo de atención con código ' + @ValidationCareGroupCode + ' no tiene parametrizada la cuenta de ingreso de factura de monto fijo para la unidad operativa'
				RETURN
			END

			IF EXISTS (
				SELECT 1
				FROM Billing.InvoiceEntityCapitated iec 
				JOIN Contract.CareGroup cg  ON iec.CareGroupId = cg.Id
				JOIN Contract.ContractAccountingStructure cas  ON cg.ContractAccountingStructureId = cas.Id
				WHERE iec.Id = @Id AND cas.AccountWithoutRadicateId IS NULL
			) BEGIN
				SELECT	@ValidationCareGroupCode = cg.Code,
						@ValidationAccountingStructureCode = cas.Code
				FROM Billing.InvoiceEntityCapitated iec 
				JOIN Contract.CareGroup cg  ON iec.CareGroupId = cg.Id
				JOIN Contract.ContractAccountingStructure cas  ON cg.ContractAccountingStructureId = cas.Id
				WHERE iec.Id = @Id

				SELECT	@CodeResult = 999,
						@MessageResult = 'El grupo de atención con código ' + @ValidationCareGroupCode + ' y estructura contable ' + @ValidationAccountingStructureCode + ' no tiene parametrizada la cuenta contable sin radicar'
				RETURN
			END

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
						FROM Billing.InvoiceEntityCapitated iec 
						JOIN Billing.SettingsBilling sb  ON iec.OperatingUnitId = sb.IdOperatingUnit
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
							FROM Billing.InvoiceEntityCapitated iec 
							JOIN Contract.CareGroup cg  ON iec.CareGroupId = cg.Id
							JOIN Contract.Contract c  ON cg.ContractId = c.Id
							JOIN Contract.HealthAdministrator ha  ON c.HealthAdministratorId = ha.Id
							JOIN Billing.SettingsBilling sb  ON iec.OperatingUnitId = sb.IdOperatingUnit
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
							FROM Billing.InvoiceEntityCapitated iec 
							JOIN Contract.CareGroup cg  ON iec.CareGroupId = cg.Id
							JOIN Contract.Contract c  ON cg.ContractId = c.Id
							JOIN Contract.HealthAdministrator ha  ON c.HealthAdministratorId = ha.Id
							JOIN Contract.ContractAccountingStructure cas  ON cg.ContractAccountingStructureId = cas.Id
							JOIN GeneralLedger.MainAccounts ma ON cas.AccountWithoutRadicateId = ma.Id
							WHERE iec.Id = @Id
					) JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
					For xml AUTO,TYPE, ELEMENTS
				)
			)

			INSERT @resultJournalVoucher EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml, @CodeUser
			SELECT @Code_Output = rjv.code, @Message_Output = rjv.MessageResult, @JournalVoucherId = rjv.IdJournalVoucher
			FROM @resultJournalVoucher rjv

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = 'Ocurrieron errores al intentar Generar el comprobante contable: ' + ISNULL(@Message_Output, 'No se pudo generar el comprobante contable')
				RETURN
			END

			UPDATE Billing.Invoice SET JournalVoucherId = @JournalVoucherId WHERE Id = @InvoiceId

			SELECT @Message_Output = CONCAT('Se generó el Comprobante contable de tipo ', jvt.Code, ' - ', jvt.Name)
			FROM Billing.InvoiceEntityCapitated iec
			JOIN Billing.SettingsBilling sb ON iec.OperatingUnitId = sb.IdOperatingUnit
			JOIN GeneralLedger.JournalVoucherTypes jvt On sb.InvoiceJournalVoucherTypeId = jvt.Id
			Where iec.Id = @Id

			SELECT @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		-- ==================== CONFIRMACIÓN RIPS FINAL (Status=5, InvoicePeriod=3) ====================
		-- Provisiona registros que requiere el handler `FinalCapitatedInvoiceRipsHandler` (Prometheus)
		-- y la consulta `SP_GetInfoInvoiceRIPS` (INNER JOIN EP+ER con EntityName='InvoiceEntityCapitated').

		IF @Status = 5
		BEGIN
			IF NOT EXISTS (
				SELECT 1
				FROM Billing.ElectronicsProperties 
				WHERE EntityId = @Id AND EntityName = 'InvoiceEntityCapitated'
			)
			BEGIN
				DECLARE @ElectronicsPropertiesId INT

				INSERT INTO Billing.ElectronicsProperties
				(
					EntityId,         -- 1
					EntityName,       -- 2
					EntityCode,       -- 3
					StatusRIPS,       -- 4  (1 = Registrado)
					CUV,              -- 5
					CreationUser,     -- 6
					CreationDate,     -- 7
					ModificationUser, -- 8
					ModificationDate, -- 9
					ConditionSalesId  -- 10
				)
				VALUES
				(
					@Id,                       -- 1
					'InvoiceEntityCapitated',  -- 2
					@Code,                     -- 3
					1,                         -- 4
					NULL,                      -- 5
					@CodeUser,                 -- 6
					[Common].[GETDATE](),      -- 7
					NULL,                      -- 8
					NULL,                      -- 9
					NULL                       -- 10
				)

				SET @ElectronicsPropertiesId = SCOPE_IDENTITY()

				INSERT INTO Billing.ElectronicsRIPS
				(
					ElectronicsPropertiesId, -- 1
					RadicateDate,            -- 2
					sendDate,                -- 3
					Retry,                   -- 4
					CosmoDBId,               -- 5
					FilePath,                -- 6
					CreationUser,            -- 7
					CreationDate,            -- 8
					ModificationUser,        -- 9
					ModificationDate         -- 10
				)
				VALUES
				(
					@ElectronicsPropertiesId, -- 1
					[Common].[GETDATE](),     -- 2
					[Common].[GETDATE](),     -- 3
					0,                        -- 4
					'',                       -- 5
					'',                       -- 6
					@CodeUser,                -- 7
					[Common].[GETDATE](),     -- 8
					NULL,                     -- 9
					NULL                      -- 10
				)

				SET @Message = ISNULL(@Message, '') + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + 'Se inicializó el registro de RIPS electrónico para la cápita final'
			END
		END
	END

	-- Control de facturación

	IF @Status = 1
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Billing.BillingControl WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code)
		BEGIN
			INSERT INTO Billing.BillingControl
			(
				DocumentNumber,  -- 1
				DocumentType,    -- 2
				DocumentUser,    -- 3
				DocumentDate     -- 4
			)
			SELECT	@Code,              -- 1
					@DocumentTypeControl, -- 2
					@CodeUser,          -- 3
					@DocumentDate       -- 4
		END
	END
	ELSE
	BEGIN
		DELETE FROM Billing.BillingControl WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code
	END

	-- Resultado

	SELECT	@CodeResult = 0,
			@MessageResult = @Message
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea, actualiza, confirma o anula facturas de capitación (monto fijo) emitidas a entidades aseguradoras (EPS/ARS). Recibe los datos de la factura en formato XML y el código del usuario, valida que la factura no esté ya confirmada o anulada, verifica que la autorización de facturación DIAN esté activa y que el usuario tenga permiso para usarla (consultando BillingAuthorization y BillingAuthorizationUser), y aplica la operación correspondiente sobre InvoiceEntityCapitated según el estado enviado (borrador, confirmado, confirmado RIPS final o anulado). Retorna el identificador y código de la factura resultante, así como códigos y mensajes de resultado para informar al frontend si la operación fue exitosa o si hubo algún impedimento de negocio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInvoiceEntityCapitated_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInvoiceEntityCapitated_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda, actualiza, anula o confirma una factura de capitación (monto fijo) generando además la factura comercial, la cuenta por cobrar, los cruces con anticipos, la factura electrónica, el reconocimiento presupuestal y el comprobante contable asociado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Billing.SettingsBilling para la unidad operativa con EntityCapitatedBillingAuthorizationId configurado.; La factura, si ya existe, debe estar en estado 1 (Borrador); no se permite modificar facturas en estado Confirmado, Anulado o Reversado.; Cuando @Status=5 (Confirmado RIPS Final) el InvoicePeriod debe ser 3 (Final).; Cuando @Status<>3 se exige FilePath no nulo (ruta de la factura electrónica).; La autorización de facturación referida debe estar activa (Status=1) y vigente respecto a @DocumentDate (InitialDate ≤ DocumentDate < FinalDate).; El usuario @CodeUser debe estar asociado a la autorización en Billing.BillingAuthorizationUser.; El CareGroup debe estar activo (Status<>0), tener ContractAccountingStructureId, contrato vigente (Status=1) y al menos un ContractDetail con ValidRecord=1.; Si el contrato tiene TerminationControl IN (2,4), @DocumentDate no puede ser mayor a EndDate ni a BillingEndDate.; Si el CareGroup tiene LiquidationType=5 (PGP), el SubTotalValue debe coincidir con la suma de TotalContract de sus agrupadores.; El tercero (HealthAdministrator) debe tener una dirección parametrizada con DepartmentId y CityId.; Debe existir una secuencia automática para Cuentas por Cobrar (PortfolioSequence IdForm=682, IsManual=0) con scope O o coincidente con la unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.InvoiceEntityCapitated: Cuando @Status=3 se anula la factura: se actualizan ModificationUser/Date y AnnulmentUser/Date con el usuario y fecha actual.; [INSERT] Billing.InvoiceEntityCapitated: Cuando @Id=0 se inserta la cabecera con el código obtenido vía Common.SP_GetSequence (Id 180, IdForm 758) y se asignan ConfirmationUser/Date solo si @Status IN (2,5).; [UPDATE] Billing.InvoiceEntityCapitated: Cuando @Id<>0 y @Status<>3 se actualizan todas las columnas de cabecera con los valores del XML.; [INSERT] Billing.InvoiceEntityCapitatedGroupers: Al confirmar (@Status=2) y si el CareGroup tiene LiquidationType=5, se insertan los agrupadores asociados (auditoría) tomados de Contract.GroupersCareGroup/Groupers.; [UPDATE] Billing.BillingAuthorization: Al confirmar se incrementa Consecutive en 1 sobre la autorización correspondiente y se capturan los rangos para validar disponibilidad.; [INSERT] Billing.Invoice: Al confirmar (@Status=2) se genera la factura comercial con DocumentType=4, número = InvoicePrefix+Consecutive, fecha de vencimiento = DocumentDate + InvoiceDeadlines, valores tomados de la capitada y CUFE=''CUFE'' solo si BillingAuthorization.InvoiceType=3.; [UPDATE] Billing.InvoiceEntityCapitated: Tras crear la factura comercial se asocia su Id en InvoiceEntityCapitated.InvoiceId.; [INSERT] Portfolio.AccountReceivable: Se crea CxC tipo 2 con código obtenido de Common.SP_GetSequence (Id 160, IdForm 682), saldo igual a ThirdPartySalesValue, Status=2 y cuentas tomadas de ContractAccountingStructure.; [INSERT] Portfolio.AccountReceivableShare: Se crea una cuota única (Number=1) por la CxC con Value y Balance iguales a los de la CxC.; [INSERT] Portfolio.AccountReceivableAccounting: Se inserta el detalle contable de la CxC usando AccountWithoutRadicateId; ThirdPartyId/CostCenterId solo si la cuenta los maneja (HandlesThirdParty/HandlesCostCenter=1).; [INSERT] Billing.InvoicePortfolioAdvance: Si llega ListPortfolioAdvanceCrossing en el XML, se registra el cruce de cada anticipo con la factura.; [INSERT] Billing.ElectronicDocument: Se registra documento electrónico (DocumentType=Invoice, Status=1) solo si GeneralLedgerSettings.HandlesElectronicBilling=1 y BillingAuthorization.InvoiceType=3.; [UPDATE] Billing.Invoice: Tras generar el comprobante contable se asigna JournalVoucherId en la factura comercial.; [INSERT] Billing.BillingControl: Cuando @Status=1 (Borrador) y no existe registro previo con DocumentType=1 y mismo Code, se inserta control de documento.; [DELETE] Billing.BillingControl: Cuando @Status<>1 se elimina el registro de control para DocumentType=1 y DocumentNumber=@Code.; [RETURN_RESULT] Billing.InvoiceEntityCapitated: Si cualquier validación falla retorna @CodeResult=999 con mensaje específico; en caso exitoso retorna @CodeResult=0 con detalle de número de factura, código CxC y comprobante contable.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated_Output';
-- GO
