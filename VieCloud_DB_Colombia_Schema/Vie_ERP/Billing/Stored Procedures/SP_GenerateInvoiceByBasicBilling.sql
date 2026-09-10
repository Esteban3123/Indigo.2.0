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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la factura definitiva (comprobante contable) a partir de una prefactura o documento de facturación básica ya registrado en el sistema. Valida y consume el consecutivo de numeración habilitado por la resolución DIAN (autorización de facturación), crea el encabezado y el detalle de la factura en las tablas de Billing, y determina si aplica como tiquete electrónico de venta según la configuración de la unidad operativa y el tipo de cliente (EPS, aseguradora u otro tercero). Adicionalmente genera la cuenta por cobrar asociada a la factura emitida y retorna el identificador de la factura, el número de factura asignado y el identificador de la cuenta por cobrar como parámetros de salida.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateInvoiceByBasicBilling';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateInvoiceByBasicBilling';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera, a partir de una facturación básica, la factura de venta con su consecutivo de autorización, los detalles facturados, la cuenta por cobrar con su contabilización y cuotas, y dispara el reconocimiento presupuestal y el comprobante contable asociados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateInvoiceByBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Billing.BasicBilling con el Id recibido.; Debe existir configuración de facturación (Billing.SettingsBilling) para la unidad operativa de la BasicBilling.; La BasicBilling debe tener una BillingAuthorization asociada con consecutivo disponible (Consecutive < FinalInvoice).; Si la autorización tiene fechas de vigencia, la fecha del documento debe estar dentro de [InitialDate, FinalDate).; El cliente (Common.Customer) y su tercero (Common.ThirdParty) deben existir.; Para tiquete electrónico, SettingsBilling debe tener configurada una BillingAuthorizationId válida.; Debe existir una secuencia parametrizada (TableId=160, ColumnId=682) para la unidad operativa que emita el código de la cuenta por cobrar.; Debe existir un LegalBook marcado como OfficialBook=1 para la contabilización.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateInvoiceByBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El consecutivo de la autorización (BillingAuthorization.Consecutive) se incrementa en 1 antes de cualquier validación, incluso si luego se aborta por exceder FinalInvoice o por vigencia.; El número de factura siempre se compone como Prefijo + Consecutivo de la autorización utilizada.; La factura siempre se crea con DocumentType=6, Status=1 y ResponsibleRecoveryFee=1.; El TotalInvoice de la factura usa BasicBilling.TotalValue; el InvoiceValue se calcula como Value-ValueDiscount y TotalValue como Value-ValueDiscount+ValueIVA.; La cuenta por cobrar siempre se inserta con AccountReceivableType=1, PortfolioStatus=3, Status=2, NumberShares=1 y una sola cuota igual al TotalValue con vencimiento DocumentDate + Customer.Term.; La fecha de expiración de la factura y de la cuota se calcula como DocumentDate + Customer.Term días.; El centro de costo de la cuenta por cobrar se toma del primer detalle de BasicBillingDetail vía FunctionalUnit.CostCenterId.; Si el tercero asociado al cliente no es facturador electrónico y la unidad operativa aplica tiquete electrónico, la factura se emite como tiquete electrónico usando la autorización configurada en SettingsBilling.; Cualquier excepción no controlada se captura y devuelve CodeResult=999 con el mensaje de error y línea.; El reconocimiento presupuestal solo se genera cuando la BasicBilling tiene BudgetId asociado y SettingsBilling.BudgetInterface=1.; La contabilización siempre se ejecuta sobre el LegalBook marcado como OfficialBook=1.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateInvoiceByBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación básica; Factura de venta; Tiquete electrónico (POS); Facturador electrónico; Autorización DIAN / resolución de facturación; Consecutivo de factura; Vigencia de autorización; Cuenta por cobrar; Cuotas de cuenta por cobrar; Centro de costo; Reconocimiento presupuestal; Comprobante contable (journal voucher); Libro oficial contable; Cuota de recuperación (RecoveryFee)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateInvoiceByBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SettingsBilling.ApplyElectronicSalesTicket = 1 y el ThirdParty del cliente tiene ElectronicBiller = 0 → Se sobrescribe BillingAuthorizationId con el de SettingsBilling y se marca la factura como tiquete electrónico (IsElectronicTicket=1) else Se conserva la BillingAuthorizationId original de la BasicBilling y la factura no es tiquete electrónico; si Consecutivo recién incrementado de BillingAuthorization > FinalInvoice → Aborta con código 999 y mensaje ''No hay consecutivos disponibles para asignar a la factura del número de autorización asignado.''; si InitialDate y FinalDate de la autorización no son nulas y la fecha de factura está fuera del rango [InitialDate, FinalDate) → Aborta con código 999 y mensaje ''La autorización asignada no se encuentra vigente.''; si SP_GetSequence retorna código distinto de 0 → Aborta con código 999 y mensaje formateado para ''Cuentas Por Cobrar''; si BudgetInterface = 1 (configuración con interfaz presupuestal y BasicBillingBudgetId no nulo) → Ejecuta SP_GenerateRecognitionByAccountReceivableId_Output para generar reconocimiento presupuestal; si falla aborta con 999; si SP_GenerateJournalVoucherByBasicBilling retorna código distinto de 0 → Aborta con código 999 y mensaje ''No se pudo generar el comprobante contable''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateInvoiceByBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Portfolio.SP_GenerateRecognitionByAccountReceivableId_Output; Billing.SP_GenerateJournalVoucherByBasicBilling; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateInvoiceByBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BasicBilling; Billing.SettingsBilling; Common.Customer; Common.ThirdParty; Billing.BillingAuthorization; Billing.BasicBillingDetail; Payroll.FunctionalUnit; Billing.Invoice; Contract.CareGroup; Contract.ContractAccountingStructure; GeneralLedger.LegalBook', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateInvoiceByBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateInvoiceByBasicBilling';
-- GO
