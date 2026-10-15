-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-11-19
-- Description:	Procedimiento que se encarga de generar el comprobante contable para la facturacion basica
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[SP_GenerateInvoiceByBasicBilling]
	@Id As INT,
	@CodeUser as VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	------------------------------------------------------
	@InvoiceId INT OUTPUT, 
	@InvoiceNumber VARCHAR(15) OUTPUT,
	@AccountReceivableId INT OUTPUT
AS
BEGIN
	--Se declaran las variables
	DECLARE @Code VARCHAR(20),
			@OperatingUnitId INT,
			@BillingAuthorizationId INT,
			-------------------------
			@InvoicePrefix as varchar(5),
			@AuthorizationConsecutive bigint,
			@AuthorizationFinalInvoice bigint,
			@AuthorizationInitialDate date,
			@AuthorizationFinalDate date,
			@InvoiceDate datetime,
			-------------------------
			@AccountReceivableCode VARCHAR(20),
			-------------------------
			@BudgetInterface BIT,
			@ApplyElectronicSalesTicket BIT,
			@IsElectronicTicket BIT = 0,
			@BasicBillingBudgetId INT,
			-------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	BEGIN TRY
		SELECT @Code = bb.Code,
			@InvoiceDate = bb.DocumentDate,
			@OperatingUnitId = bb.OperatingUnitId,
			@BillingAuthorizationId = bb.BillingAuthorizationId,
			@BasicBillingBudgetId = bb.BudgetId
		FROM Billing.BasicBilling bb
		WHERE bb.Id = @Id

		SELECT	@BudgetInterface = IIF(sb.BasicBillingBudgetId  IS NULL, 0, sb.BudgetInterface),
				@ApplyElectronicSalesTicket = sb.ApplyElectronicSalesTicket
		FROM Billing.SettingsBilling sb
		WHERE sb.IdOperatingUnit = @OperatingUnitId

		/******************************************************************* FACTURA ********************************************************/

		IF @ApplyElectronicSalesTicket = 1 AND EXISTS(SELECT 1 
															FROM Billing.BasicBilling bb
															JOIN Common.Customer c ON c.Id = bb.CustomerId
															JOIN Common.ThirdParty th ON th.Id = c.ThirdPartyId
															WHERE th.ElectronicBiller = 0 AND bb.Id = @Id) BEGIN

			SELECT	@BillingAuthorizationId = sb.BillingAuthorizationId,
					@IsElectronicTicket = 1
			FROM Billing.SettingsBilling sb
			WHERE sb.IdOperatingUnit = @OperatingUnitId
		END

		--==Actualizo la tabla de billing authorization con el nuevo consecutivo			
		UPDATE ba 
			SET @InvoicePrefix = InvoicePrefix, 
				@AuthorizationConsecutive = Consecutive = Consecutive + 1,
				@AuthorizationFinalInvoice = FinalInvoice,
				@AuthorizationInitialDate = InitialDate,
				@AuthorizationFinalDate = FinalDate
		FROM Billing.BillingAuthorization ba
		WHERE Id = @BillingAuthorizationId
	
		SET @AuthorizationConsecutive -= 1

		--==Validación de consecutivos de autorización
		IF @AuthorizationConsecutive > @AuthorizationFinalInvoice
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'No hay consecutivos disponibles para asignar a la factura del número de autorización asignado.'
			RETURN
		END

		--Si ya se ha parametrizado las fechas de la vigencia
		IF @AuthorizationInitialDate IS NOT NULL AND @AuthorizationFinalDate IS NOT NULL
		BEGIN
			--==Validación de autorización vigente
			IF @invoiceDate < @AuthorizationInitialDate OR @invoiceDate >= @AuthorizationFinalDate
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = 'La autorización asignada no se encuentra vigente.'
				RETURN
			END
		END
		
		SET @InvoiceNumber = Concat(@InvoicePrefix, @AuthorizationConsecutive)
	
		--====================GENERACIÓN DE FACTURA======================

		INSERT INTO Billing.Invoice 
		(
			[OperatingUnitId],[DocumentType],[InvoiceNumber],[ThirdPartyId],[InvoiceDate],[InvoiceExpirationDate],[TotalInvoice],[ThirdPartyDiscountValue],
			[ResponsibleRecoveryFee],[Status],[InvoicedUser],[InvoicedDate],[OutputDate],[InitialDate],[BillingAuthorizationId],[Observation],
			[CapitationPatientValue],[ThirdPartySalesValue],[PatientPaidValue],[ThirdPartyAccountReceivableValue],[PatientAccountReceivableValue],
			[CREETaxRetentionValue],[CREETaxRetentionBaseValue],
			[InvoiceValue],[ValueTax],[TotalValue],[CurrencyId],[IsElectronicTicket]
		)
		SELECT 
			bb.OperatingUnitId,6,@InvoiceNumber,c.ThirdPartyId,bb.DocumentDate,DATEADD(DAY, c.Term, bb.DocumentDate),bb.TotalValue,bb.ValueDiscount,
			1,1,@CodeUser,[Common].[GETDATE](),[Common].[GETDATE](),[Common].[GETDATE](),@BillingAuthorizationId,bb.Description,
			0,bb.TotalValue,0,bb.TotalValue,0,
			0,0,
			bb.Value-bb.ValueDiscount,bb.ValueIVA,bb.Value-bb.ValueDiscount+bb.ValueIVA,
			bb.CurrencyId, @IsElectronicTicket
		FROM Billing.BasicBilling bb
		JOIN Common.Customer c ON bb.CustomerId = c.Id
		WHERE bb.Id = @Id
		
		SELECT @InvoiceId = SCOPE_IDENTITY()

		--====================DETALLES DE LA FACTURA======================

		INSERT INTO Billing.InvoiceDetailBasicInvoice
		(
			InvoiceId, BasicBillingDetailId, Quantity, UnitSalesPrice, TotalSalesPrice, Balance
		)
			SELECT	@InvoiceId,
					bbd.Id,
					----------------------------------
					bbd.Quantity,
					bbd.Price,
					ROUND
					(
						bbd.Quantity * bbd.Price,
						bb.RoundLevel
					) Value,
					ROUND
					(
						bbd.Quantity * bbd.Price,
						bb.RoundLevel
					) Balance
			FROM Billing.BasicBilling bb
			JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
			WHERE bb.Id = @Id

		--====================GENERACIÓN DE CUENTA POR COBRAR======================

		--Si se esta insertando por primera vez se consulta la secuencia numerica
		DECLARE @IsManual BIT
				
		EXEC Common.SP_GetSequence 160, 682, @OperatingUnitId, NULL, NULL, @IsManual OUT, @AccountReceivableCode OUT, @Code_Output OUT, @Message_Output OUT

		IF @Code_Output <> 0
		BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = REPLACE(@Message_Output, '{0}', 'Cuentas Por Cobrar')
			RETURN
		END

		/*****se saca el centro de costo de el primer detalle que venga*****/
		--se toma asi ya que cuando la cuenta maneja centro de costos genera un error por no tener uno asociado a la cuenta por cobrar
		--esto se deberia pedir por el cliente en la cabecera
		DECLARE @IdCostCenter INT

		SELECT TOP 1 @IdCostCenter = fu.CostCenterId
		FROM Billing.BasicBillingDetail bd WITH(NOLOCK)
		join Payroll.FunctionalUnit fu WITH(NOLOCK) on bd.FunctionalUnitId = fu.Id
		WHERE bd.BasicBillingId =@Id

		/*******************************************************************/
		
		--Insertamos la cuenta por cobrar
		INSERT INTO [Portfolio].[AccountReceivable]
        (
			[Code],[OperatingUnitId],[AccountReceivableType],[ThirdPartyId],[CustomerId],[InvoiceId],[InvoiceNumber],[AccountReceivableDate],[Term],[ExpiredDate],[Observations],[PortfolioStatus],
			[MainAccountWithoutFilingId],[AccountWithoutRadicateId],[NumberShares],[Value],[Balance],[Status],[CreationUser],[CreationDate],[ConfirmationUser],[ConfirmationDate],
			[OpeningBalance],[PaymentAgreement],[RegistrationAdjusted],
			[AccountRadicateId],[AccountObjectionRemediedId],
			[AffectBudget],[BudgetId],[CurrencyId],[CostCenterId]
		)
		SELECT 
			@AccountReceivableCode,@OperatingUnitId,1,c.ThirdPartyId,c.Id,@InvoiceId,@InvoiceNumber,bb.DocumentDate,c.Term,DATEADD(DAY, c.Term, bb.DocumentDate),'Cuenta por cobrar generada desde facturación básica ' + @Code,3,
			c.MainAccountReceivableId,c.MainAccountReceivableId,1,bb.TotalValue,bb.TotalValue,2,@CodeUser,[Common].[GETDATE](),@CodeUser,[Common].[GETDATE](),
			0,0,0,
			NULL, NULL,
			@BudgetInterface, @BasicBillingBudgetId,
			bb.CurrencyId,
			@IdCostCenter
		FROM Billing.BasicBilling bb
		JOIN Common.Customer c ON bb.CustomerId = c.Id
		WHERE bb.Id = @Id

		declare @StructureAccountRecoveryFeeId Int
		select top 1 @StructureAccountRecoveryFeeId = st.AccountRecoveryFeeId
		from Billing.BasicBilling bb
		join Billing.Invoice i on bb.InvoiceId = i.Id
		join Contract.CareGroup cg on  i.CareGroupId = cg.Id
		join Contract.ContractAccountingStructure st ON cg.ContractAccountingStructureId = st.Id
		where bb.[Status] = 2 and i.[Status] = 1 and bb.Id = @Id

		SELECT @AccountReceivableId = SCOPE_IDENTITY()

		--Insertamos AccountReceivableAccounting
		INSERT INTO [Portfolio].[AccountReceivableAccounting]
        (
			AccountReceivableId,MainAccountId,ThirdPartyId,Value,Balance
		)
		SELECT 
			@AccountReceivableId,isnull(@StructureAccountRecoveryFeeId, c.MainAccountReceivableId),c.ThirdPartyId,bb.TotalValue,bb.TotalValue
		FROM Billing.BasicBilling bb
		JOIN Common.Customer c ON bb.CustomerId = c.Id
		WHERE bb.Id = @Id

		--Insertamos AccountReceivableShare
		INSERT INTO [Portfolio].[AccountReceivableShare]
        (
			AccountReceivableId,Number,ExpiredDate,Value,Balance
		)
		SELECT 
			@AccountReceivableId,1,DATEADD(DAY, c.Term, bb.DocumentDate),bb.TotalValue,bb.TotalValue
		FROM Billing.BasicBilling bb
		JOIN Common.Customer c ON bb.CustomerId = c.Id
		WHERE bb.Id = @Id

		SET @Message = 'Se generó la factura: ' + @InvoiceNumber

		/**************************************** RECONOCIMIENTO PRESUPUESTAL ****************************************/

		IF @BudgetInterface = 1
		BEGIN
			EXEC [Portfolio].[SP_GenerateRecognitionByAccountReceivableId_Output] @OperatingUnitId, @AccountReceivableId, @CodeUser, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el reconocimiento presupuestal')
				RETURN
			END

			SELECT @Message = @Message + IIF(@Message_Output = '', '', CHAR(13) + CHAR(10) + @Message_Output)
		END

		/**************************************** CONTABILIZACIÓN ****************************************/

		DECLARE @LegalBookId INT = 0,
				@HasFixedAssets BIT = 0,
				@LegalBookRows INT = 1

		-- Verificar si la factura básica contiene activos fijos
		SELECT @HasFixedAssets = 1
		FROM Billing.BasicBillingDetail bbd
		WHERE bbd.BasicBillingId = @Id AND bbd.DetailType = 3

		IF @HasFixedAssets = 1
		BEGIN
			-- Flujo para activos fijos: generar comprobantes por cada libro
			WHILE @LegalBookRows > 0
			BEGIN
				SELECT TOP 1 @LegalBookId = fapadb.LegalBookId
				FROM Billing.BasicBillingDetail bbd
				JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON bbd.PhysicalAssetId = fapadb.PhysicalAssetId
				JOIN GeneralLedger.LegalBook lb ON fapadb.LegalBookId = lb.Id
				WHERE bbd.BasicBillingId = @Id 
					AND bbd.DetailType = 3 
					AND lb.Status = 1
					AND fapadb.LegalBookId > @LegalBookId
				ORDER BY fapadb.LegalBookId
				
				SET @LegalBookRows = @@RowCount
				
				IF @LegalBookRows = 0
					BREAK
					
				-- Generar comprobante para este libro (con flag de activo fijo)
				EXEC Billing.SP_GenerateJournalVoucherByBasicBilling 
					@LegalBookId, @Id, @InvoiceNumber, @CodeUser, 
					1, -- @IsFixedAsset = 1
					@Code_Output OUT, @Message_Output OUT
					
				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999,
							@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el comprobante contable')
					RETURN
				END
				
				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', CHAR(13) + CHAR(10) + @Message_Output)
			END
		END
		ELSE
		BEGIN
			-- Flujo normal para productos y servicios (un solo comprobante)
			SET @LegalBookId = (SELECT Id FROM GeneralLedger.LegalBook WHERE OfficialBook = 1)
			
			EXEC Billing.SP_GenerateJournalVoucherByBasicBilling 
				@LegalBookId, @Id, @InvoiceNumber, @CodeUser, 
				0, -- @IsFixedAsset = 0
				@Code_Output OUT, @Message_Output OUT
				
			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el comprobante contable')
				RETURN
			END
			
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		/************************************************************************************************************************************/

		SELECT	@CodeResult = 0, 
				@MessageResult = ISNULL(@Message, '')
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 999, 
				@MessageResult = ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END