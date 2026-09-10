-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-12-29
-- Description:	Genera la factura y las cuentas por cobrar de un folio a liquidar
-- =============================================
CREATE PROCEDURE [Billing].[SP_CreateInvoice_Output]
    @RevenueControlDetailId INT,
    @BillingAuthorizationId INT,
    @PatientCode VARCHAR(20),
    @AdmissionNumber VARCHAR(20),
    @ContainerCrystal VARCHAR(10),
    @TotalPatientDiscount DECIMAL(20, 2),
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
	@AdditionalParametersXml XML,
	--Salidas
    @StatusResult BIT OUTPUT,
    @MessageResult VARCHAR(MAX) OUTPUT,
    @MessageOutput VARCHAR(MAX) OUTPUT,
    @InvoiceId INT OUTPUT,
    @InvoiceNumber VARCHAR(20) OUTPUT
WITH RECOMPILE 
AS
BEGIN
    SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/
	
	DECLARE @FolioOrder TINYINT,
			@ThirdPartyDiscountValue DECIMAL(20,2) = 0,			
			@InvoiceThirdPartySalesValue DECIMAL(20,2),
			@TotalCrossingValue DECIMAL(20, 2),
			@InvoiceDate DATETIME,
			@Observation VARCHAR(MAX),
			-------------------------------------------------------------------
			@LiquidationType TINYINT,
			@FolioType TINYINT,
			@InvoiceDeadlines INT,
			@ContractId INT,
			@ContractCodeName VARCHAR(200),
			@ContractValue DECIMAL(18,0),
			@ContractExecuteValue DECIMAL(18,0),
			@TerminationControl TINYINT,
			@ContractNotificationValueType TINYINT,
			@ContractPercentageNotification DECIMAL(5,2),
			@ContractNotificationValue DECIMAL(18,0),
			@ContractEndDate DATE,
			@ContractNotificationTimeType TINYINT,
			@ContractNotificationDays INT,
			-------------------------------------------------------------------
			@InvoicePrefix VARCHAR(5),
			@AuthorizationConsecutive BIGINT,
			@AuthorizationInitialInvoice BIGINT,
			@AuthorizationFinalInvoice BIGINT,
			@AuthorizationInitialDate DATE,
			@AuthorizationFinalDate DATE,
			-------------------------------------------------------------------
			@CalculateTaxAdvance TINYINT,
			@PaymentMethodType TINYINT =0,
			@TaxDevolutionValue NUMERIC(20,2) =0,
			-------------------------------------------------------------------
			@ValidatePackaging bit,
			@AccountingPackage bit,
			@Message VARCHAR(MAX),
			@MaxInvoiceItems INT,
			@FilePath VARCHAR(MAX),
			@CurrencyId INT,
			@TRMValue Decimal(20,5),
			@ConditionSalesId INT,
			@EconomicActivityId INT,
			@ApplyElectronicSalesTicket BIT,
			@IsElectronicTicket BIT = 0

	DECLARE @ListPortfolioAdvanceCrossing TABLE
	(
		RowId INT IDENTITY(1,1) PRIMARY KEY,
		Id INT,
		Code VARCHAR(20),
		CrossingValue DECIMAL(18, 2),
		PortfolioAdvanceType Tinyint,
		CashReceiptDetailIdTmp INT
	)

	DECLARE @TaxDevolutionByTaxId TABLE
	(
		TaxId INT NOT NULL,
		TaxValue NUMERIC(20,2) NOT NULL
	)

	--Tabla para obtener los ids del detalle de la factura
	DECLARE @InvoiceDetailIds AS TABLE (Id INT PRIMARY KEY)	
	DECLARE @OfficialCurrencyId INT,
			@LiquidateMasterAccount BIT
	------------------------------------------------------------------------------------------------------------------
	BEGIN TRY
		SET @MessageResult = ''

		Select @OfficialCurrencyId =OfficialCurrencyId
		from GeneralLedger.CompanySettings

		SELECT	@CalculateTaxAdvance = CalculateTaxAdvance,
				@ValidatePackaging = ValidatePackaging,
				@AccountingPackage = AccountingPackage,
				@LiquidateMasterAccount = LiquidateMasterAccount,
				@MaxInvoiceItems = MaxInvoiceItems,
				@ApplyElectronicSalesTicket = ApplyElectronicSalesTicket
		FROM Billing.SettingsBilling WITH (NOLOCK)
		WHERE IdOperatingUnit = @OperativeUnitId

		SELECT	@FolioOrder = rcd.FolioOrder,
				@InvoiceThirdPartySalesValue = rcd.TotalFolio - rcd.TotalPatientSalesPrice,
				@InvoiceDate = Common.[GETDATE](),
				@Observation = cd.PermanentObservationOfTheInvoice,
				@LiquidationType = cg.LiquidationType,
				@FolioType = CASE 
								WHEN cg.LiquidationType <> 1 THEN 5 --Capitacion
								WHEN rcd.FolioType = 4 THEN 2 -- Sin contrato
								ELSE rcd.FolioType
							END,
				@InvoiceDeadlines = cg.InvoiceDeadlines,				
				@ContractId = c.Id,
				@ContractCodeName = Concat(c.Code, ' - ', cd.ContractName),
				@TerminationControl = cd.TerminationControl,
				@ContractNotificationValueType = cd.NotificationValueType,
				@ContractPercentageNotification = cd.PercentageNotification,
				@ContractNotificationValue = cd.NotificationValue,
				@ContractEndDate = cd.BillingEndDate,
				@ContractNotificationTimeType = cd.NotificationTimeType,
				@ContractNotificationDays = cd.NotificationDays
		FROM Billing.RevenueControlDetail rcd WITH (NOLOCK)
		JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
		LEFT JOIN Contract.Contract c WITH (NOLOCK) ON cg.ContractId = c.Id
		LEFT JOIN Contract.ContractDetail cd WITH (NOLOCK) ON cd.ContractId = c.Id and cd.ValidRecord = 1
		WHERE rcd.id = @RevenueControlDetailId

		SELECT @ThirdPartyDiscountValue = ISNULL(SUM(sodd.GrandTotalDiscount), 0) 
		FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
		WHERE sodd.RevenueControlDetailId = @RevenueControlDetailId AND sod.IsDelete = 0

		INSERT INTO @ListPortfolioAdvanceCrossing
			SELECT t.x.value('Id[1]', 'INT'),
				t.x.value('Code[1]', 'VARCHAR(20)'),
				t.x.value('CrossingValue[1]', 'DECIMAL(18, 2)'),
				t.x.value('PortfolioAdvanceType[1]', 'TINYINT'),
				t.x.value('CashReceiptDetailIdTmp[1]', 'INT')
			FROM @ListPortfolioAdvanceCrossingXml.nodes('ListPortfolioAdvanceCrossing') t(x)

		SELECT 
			@FilePath = t.x.value('FilePath[1]', 'VARCHAR(MAX)'),
			@CurrencyId = t.x.value('CurrencyId[1]', 'INT'),
			@TRMValue = t.x.value('TRMValue[1]', 'DECIMAL(20, 5)'),
			@ConditionSalesId = t.x.value('ConditionSalesId[1]', 'INT'),
			@EconomicActivityId = t.x.value('EconomicActivityId[1]', 'INT')
		FROM @AdditionalParametersXml.nodes('AdditionalParameters') t(x)

		SET @TotalCrossingValue = COALESCE((SELECT SUM(CrossingValue) FROM @ListPortfolioAdvanceCrossing WHERE PortfolioAdvanceType = 2), 0)
		SET @TaxDevolutionValue = (SELECT TOP 1 t.x.value('TaxDevolutionValue[1]', 'NUMERIC(20,2)') FROM @ListPortfolioAdvanceCrossingXml.nodes('/Variables') t(x))
		IF @TaxDevolutionValue > 0 BEGIN
			SET @PaymentMethodType = (	SELECT PaymentMethodTypes
										FROM @ListPortfolioAdvanceCrossing lpac
										JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON lpac.Id = pa.Id
										JOIN Treasury.CashReceipts cr WITH(NOLOCK) ON pa.CashReceiptId = cr.Id
										JOIN Treasury.PaymentMethods pm WITH(NOLOCK) ON cr.Id = pm.IdCashReceipt
										group by PaymentMethodTypes
									)
		END
		/*****************************_VALIDAR LIMITE MAX DE ITEMS POR FACTURA_***************************************/
		IF exists (	SELECT 1
					from (	SELECT COUNT(*) NumberOfItems
							from Billing.ServiceOrderDetailDistribution sodd with(NOLOCK)
							JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
							where sodd.RevenueControlDetailId = @RevenueControlDetailId and sod.IsDelete = 0 ) sodd
					WHERE @MaxInvoiceItems > 0 AND sodd.NumberOfItems > @MaxInvoiceItems) BEGIN

					SELECT	@StatusResult = CONVERT(BIT, 0), 
							@MessageResult = '', 
							@MessageOutput = CONCAT('El folio ', @FolioOrder, ' no se pueden liquidar debido a que se superó el número de lineas maxima por factura ( ', @MaxInvoiceItems,' )'), 
							@InvoiceId = 0, 
							@InvoiceNumber = '' 
					RETURN
		END
		----------------------------------------------  VALIDAR CONTRATO ----------------------------------------------

		IF @FolioType = 1
		BEGIN
			UPDATE c
				SET ExecuteValue = c.ExecuteValue + @InvoiceThirdPartySalesValue
			FROM Contract.Contract c
			WHERE c.Id = @ContractId

			IF @TerminationControl IN (2, 4)
			BEGIN
				IF @ContractNotificationTimeType = 2 AND DATEADD(DAY, @ContractNotificationDays, Common.[GETDATE]()) >= @ContractEndDate
				BEGIN
					SET @Message = CONCAT('Atención: La fecha del contrato vencerá en ', DATEDIFF(DD, @ContractEndDate, DATEADD(DAY, @ContractNotificationDays, Common.[GETDATE]())), ' días')
					SET @MessageOutput = ISNULL(@MessageOutput, '') + IIF(@Message = '', '', IIF(ISNULL(@MessageOutput, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
				END
			END
					
			IF @TerminationControl IN (3, 4)
			BEGIN
				SELECT	@ContractValue = c.ContractValue,
						@ContractExecuteValue = c.ExecuteValue
				FROM Contract.Contract c
				WHERE c.Id = @ContractId

				IF @ContractExecuteValue > @ContractValue 
				BEGIN
					SELECT	@StatusResult = CONVERT(BIT, 0), 
							@MessageResult = '', 
							@MessageOutput = CONCAT('El folio ', @FolioOrder, ' no se pueden liquidar debido a que se supera el valor del contrato ', @ContractCodeName), 
							@InvoiceId = 0, 
							@InvoiceNumber = '' 
					RETURN
				END

				IF @ContractNotificationValueType = 2 and @ContractExecuteValue > @ContractValue * @ContractPercentageNotification / 100
				BEGIN
					SET @Message = CONCAT('Atención: queda un saldo restante de ', (@ContractValue - @ContractExecuteValue), ' para la terminación del contrato ', @ContractCodeName)
					SET @MessageOutput = ISNULL(@MessageOutput, '') + IIF(@Message = '', '', IIF(ISNULL(@MessageOutput, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
				END
				ELSE IF @ContractNotificationValueType = 3 and @ContractExecuteValue > @ContractNotificationValue
				BEGIN
					SET @Message = CONCAT('Atención: queda un saldo restante de ', (@ContractValue - @ContractExecuteValue), ' para la terminación del contrato ', @ContractCodeName)
					SET @MessageOutput = ISNULL(@MessageOutput, '') + IIF(@Message = '', '', IIF(ISNULL(@MessageOutput, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
				END
			END
		END

		----------------------------------------- AUTORIZACION DE FACTURACION -----------------------------------------
		IF @LiquidationType = 1
		BEGIN

			IF @ApplyElectronicSalesTicket = 1 AND EXISTS(SELECT 1 
													FROM Billing.RevenueControlDetail rcd
													JOIN Common.ThirdParty th ON th.Id = rcd.ThirdPartyId
													WHERE th.ElectronicBiller = 0 AND rcd.Id = @RevenueControlDetailId) BEGIN

				SELECT	@BillingAuthorizationId = sb.BillingAuthorizationId,
						@IsElectronicTicket = 1 --Tiquete electronico de venta
				FROM Billing.SettingsBilling sb
				WHERE sb.IdOperatingUnit = @OperativeUnitId
			END

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
			IF NOT @AuthorizationConsecutive BETWEEN @AuthorizationInitialInvoice AND @AuthorizationFinalInvoice
			BEGIN
				SELECT	@StatusResult = CONVERT(BIT, 0), 
						@MessageResult = '', 
						@MessageOutput = 'No hay consecutivos disponibles para asignar a la factura del número de autorización asignado', 
						@InvoiceId = 0, 
						@InvoiceNumber = '' 
				RETURN
			END

			IF @InvoiceDate < @AuthorizationInitialDate OR @InvoiceDate >= @AuthorizationFinalDate
			BEGIN
				SELECT	@StatusResult = CONVERT(BIT, 0), 
						@MessageResult = '', 
						@MessageOutput = 'La autorización asignada no se encuentra vigente', 
						@InvoiceId = 0, 
						@InvoiceNumber = '' 
				RETURN
			END
		
			SET @InvoiceNumber = CONCAT(@InvoicePrefix, @AuthorizationConsecutive)
			
		END
		ELSE
		BEGIN
			UPDATE ba 
				SET @InvoicePrefix = ba.PrefixConsecutiveCapitation, 
					@AuthorizationConsecutive = ba.ConsecutiveControlCapitation = ba.ConsecutiveControlCapitation + 1,
					@BillingAuthorizationId = NULL
			FROM Billing.SettingsBilling ba
			WHERE ba.IdOperatingUnit = @OperativeUnitId
						
			SET @AuthorizationConsecutive -= 1

			SET @InvoiceNumber = CONCAT(@InvoicePrefix, @AuthorizationConsecutive)
		END

		/**********VALIDACION CANTIDADES****************/
		-- Se valida si existen cantidades negativas  o (en 0 en el Sodd)
		IF EXISTS (
			SELECT 1
			FROM Billing.ServiceOrderDetailDistribution sodd
			JOIN Billing.ServiceOrderDetail sod ON sod.Id = sodd.ServiceOrderDetailId
			JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
			WHERE rcd.Id = @RevenueControlDetailId AND (sodd.Quantity <= 0 OR sod.InvoicedQuantity < 0)
		) 
		BEGIN
			DECLARE @ProductNegativeMessageString VARCHAR(MAX)  
			SELECT @ProductNegativeMessageString = STRING_AGG(
					CONCAT(
						TRIM(ISNULL(ip.Code, ce.Code)),
						'-',
						TRIM(ISNULL(ip.Name, ce.Description))						
					), ', ')
			FROM Billing.ServiceOrderDetailDistribution sodd
			JOIN Billing.ServiceOrderDetail sod ON sod.Id = sodd.ServiceOrderDetailId
			JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
			LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = sod.ProductId
			LEFT JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId
			WHERE rcd.Id = @RevenueControlDetailId AND (sodd.Quantity <= 0 OR sod.InvoicedQuantity < 0)

			SELECT	@StatusResult = CONVERT(BIT, 0), 
						@MessageResult = '', 
						@MessageOutput = CONCAT('Existen cantidades negativas o en 0 en el folio relacionadas con los siguientes items: ',@ProductNegativeMessageString), 
						@InvoiceId = 0, 
						@InvoiceNumber = '' 
				RETURN
		END

		/*************************/

		---------------------------------Conversión y recálculo valores Folio --------------------------------------
		 Declare @RecalculateFolioDetails Table(
										StatusResult			Bit,
										MessageResult			Varchar(Max),
	
										RevenueControlDetailId	INTEGER,
										SodDistributionId		INTEGER,
										SubTotalSalesPrice		NUMERIC(20,2),
										GrandTotalDiscount		NUMERIC(20,2),
										UnitGrossValue			NUMERIC(20,2),
										TaxPercentage			NUMERIC(20,2),
										NetWorth				NUMERIC(20,2),
										NetUnitValue			NUMERIC(20,2),
										GrandTotalTaxes			NUMERIC(20,2),
										GrandTotalSalesPrice	NUMERIC(20,2),
										TotalSalesPrice			NUMERIC(20,2),
										InvoicedQuantity		INTEGER,
										IvaId					INTEGER,
										InvoiceThirdPartySalesValue NUMERIC(20,2),
										SubTotalPatientSalesPrice	NUMERIC(20,2)) 
			INSERT INTO @RecalculateFolioDetails
								 SELECT	StatusResult			,
										MessageResult	,
	
										RevenueControlDetailId	,
										SodDistributionId		,
										SubTotalSalesPrice		,
										GrandTotalDiscount		,
										UnitGrossValue			,
										TaxPercentage			,
										NetWorth				,
										NetUnitValue			,
										GrandTotalTaxes			,
										GrandTotalSalesPrice	,
										TotalSalesPrice			,
										InvoicedQuantity		,
										IvaId,
										InvoiceThirdPartySalesValue ,
										SubTotalPatientSalesPrice
			FROM [Billing].[RecalculateFolioDetailsByCurrency](@RevenueControlDetailId,@CurrencyId,NULL,@OperativeUnitId)

		IF NOT EXISTS (SELECT 1 FROM @RecalculateFolioDetails) OR EXISTS (SELECT 1 FROM @RecalculateFolioDetails WHERE StatusResult=0) BEGIN
			SELECT	@StatusResult = CONVERT(BIT, 0), 
					@MessageResult = '', 
					@MessageOutput = 'No se logró recalcular los detalle del folio en la moneda seleccionada', 
					@InvoiceId = 0, 
					@InvoiceNumber = '' 
			RETURN
		END

		--------------------------------- VALIDA DEVOLUCION DE IVA (SI APLICA O NO) ----------------------------------
					
		IF EXISTS (
					SELECT	1
					FROM GeneralLedger.GeneralLedgerIVA gli WITH(NOLOCK)
					JOIN (
							SELECT	rcd.Id RevenueControlDetailId,
									sod.IvaId,
									sum(sodd.GrandTotalTaxes) TaxValue
							from Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK)
							JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id=sodd.ServiceOrderDetailId
							JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id=sodd.RevenueControlDetailId
							where sod.IvaId is not null 
							GROUP BY rcd.Id,sod.IvaId) sodd ON sodd.IvaId= gli.Id
					WHERE gli.ApplyTaxDevolution=1 and sodd.RevenueControlDetailId=@RevenueControlDetailId and gli.PaymentMethodTypes like CONCAT('%',CAST(@PaymentMethodType AS varchar(3)),'%')
					GROUP BY sodd.RevenueControlDetailId, gli.PaymentMethodTypes,gli.Id
					) AND @TaxDevolutionValue > 0 BEGIN

						WITH cte_temp AS(	SELECT gli.Id,CAST(dbo.Data AS tinyint) PaymentMethodTypes
											from GeneralLedger.GeneralLedgerIVA gli
											CROSS APPLY dbo.Split(gli.PaymentMethodTypes,',') dbo
											where gli.PaymentMethodTypes is not null
											group by gli.Id,dbo.Data,dbo.Id)

						INSERT INTO @TaxDevolutionByTaxId (TaxId,TaxValue)
						SELECT	gli.Id,								
								sum(sodd.taxes) TaxValue
						FROM GeneralLedger.GeneralLedgerIVA gli WITH(NOLOCK)
						JOIN (
						SELECT	rcd.Id RevenueControlDetailId,
								sodd.IvaId,
								sum(sodd.GrandTotalTaxes) taxes
						from @RecalculateFolioDetails sodd 
						JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id=sodd.RevenueControlDetailId
						where sodd.IvaId is not null 
						GROUP BY rcd.Id,sodd.IvaId) sodd ON sodd.IvaId= gli.Id
						JOIN cte_temp gli2 ON gli.Id=gli2.Id
						WHERE gli.ApplyTaxDevolution=1 and sodd.RevenueControlDetailId = @RevenueControlDetailId and gli2.PaymentMethodTypes = @PaymentMethodType
						GROUP BY  gli.Id
		END
		--------------------------------------------------------------------------------------------------------------
		------------------------------------------  GENERACION DE LA FACTURA ------------------------------------------
		IF EXISTS (SELECT 1
			FROM Billing.RevenueControlDetail rcd WITH (NOLOCK)
			WHERE rcd.Id = @RevenueControlDetailId AND rcd.InvoiceCategoryId IS NULL) BEGIN
			SELECT	@StatusResult = CONVERT(BIT, 0), 
					@MessageResult = '', 
					@MessageOutput = 'Debe seleccionar una categoría para la factura a generar', 
					@InvoiceId = 0, 
					@InvoiceNumber = '' 
			RETURN
		END

		-- Se valida que las cantidad sean iguales entre el folio y la orden de servicio
		IF EXISTS (
			SELECT 1
			FROM Billing.ServiceOrderDetailDistribution sodd
			JOIN Billing.ServiceOrderDetail sod ON sod.Id = sodd.ServiceOrderDetailId
			JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
			LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = sod.ProductId
			LEFT JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId
			WHERE rcd.Id = @RevenueControlDetailId 
			AND sodd.Quantity <> sod.InvoicedQuantity
			AND (sodd.DistributionType = 1 AND sod.IncludeServiceOrderDetailId IS NULL AND sod.IsPackage = 0 AND sod.SettlementType = 1)
		) 
		BEGIN
			DECLARE @ProductDetailMessageString VARCHAR(MAX)  
			SELECT @ProductDetailMessageString = STRING_AGG(
					CONCAT(
						TRIM(ISNULL(ip.Code, ce.Code)),
						'-',
						TRIM(ISNULL(ip.Name, ce.Description)),
						'(',sod.InvoicedQuantity, ' vs ', sodd.Quantity,')'
					), ', ')
			FROM Billing.ServiceOrderDetailDistribution sodd
			JOIN Billing.ServiceOrderDetail sod ON sod.Id = sodd.ServiceOrderDetailId
			JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
			LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = sod.ProductId
			LEFT JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId
			WHERE rcd.Id = @RevenueControlDetailId 
			AND sodd.Quantity <> sod.InvoicedQuantity
			AND (sodd.DistributionType = 1 AND sod.IncludeServiceOrderDetailId IS NULL AND sod.IsPackage = 0 AND sod.SettlementType = 1)

			SELECT @StatusResult = CONVERT(BIT, 0), 
					@MessageResult = '', 
					@MessageOutput = CONCAT('Existen una diferencia entre las cantidades del folio y la orden de servicio para los siguientes productos: ', @ProductDetailMessageString), 
					@InvoiceId = 0, 
					@InvoiceNumber = '' 
			RETURN
		END

		INSERT INTO Billing.Invoice 
		(
			[OperatingUnitId], --1
			[DocumentType],--2
			[InvoiceNumber],--3
			[RevenueControlDetailId], --4
			[AdmissionNumber], --5
			[HealthAdministratorId], --6
			[ThirdPartyId], --7
			[PatientCode],--8
			[CareGroupId],--9
			[InvoiceDate],--10
			[InvoiceExpirationDate], --11
			[TotalInvoice], --12
			[CapitationPatientValue], --13
			[ThirdPartySalesValue], --14
			[ThirdPartyDiscountValue],--15
			[ResponsibleRecoveryFee], --16
			[TotalPatientSalesPrice], --17
			[PatientDiscount],--18
			[PatientDiscountPercentage], --19
			[TotalPatientWithDiscount], --20
			[ValueVoucher], --21
			[PatientPaidValue], --22
			[ThirdPartyAccountReceivableValue],--23
			[PatientAccountReceivableValue],--24
			[PatientType], --25
			[Observation], --26
			[PatientAffiliatedType], --27
			[PatientPaidAbility], --28
			[PatientSocialClass], --29
			[CREETaxRetentionValue], --30
			[CREETaxRetentionBaseValue], --31
			[Status], --32
			[InvoicedUser],--33
			[InvoicedDate], --34
			[InvoiceCategoryId], --35
			[OutputDate], --36
			[IsCutAccount], --37
			[OutputDiagnosis],--38
			[InitialDate] , --39 
			[CutType], --40
			[BillingAuthorizationId],  --41
			[ContractId],--42
			[InvoiceValue], --43
			[ValueTax], --44
			[TotalValue] , --45
			[CUFE], --46
			[CurrencyId], --47
			[TRMValue], --48
			[TaxDevolutionValue], --49
			[IsElectronicTicket], --50
			[EconomicActivityId] --51
		)
		SELECT	@OperativeUnitId, --1
				@FolioType, --2
				@InvoiceNumber, --3
				@RevenueControlDetailId, --4
				@AdmissionNumber, --5
				rcd.HealthAdministratorId, --6
				rcd.ThirdPartyId,--7
				@PatientCode,--8
				rcd.CareGroupId,--9
				@InvoiceDate,--10
				DATEADD(DAY, @InvoiceDeadlines, Common.[GETDATE]()),--11
				--Total de la factura sin iva devuelto
				sodd.GrandTotalSalesPrice, --12
				0,--13
				sodd.InvoiceThirdPartySalesValue,--14
				--Descuento total
				sodd.GrandTotalDiscount, --15			
				rcd.ResponsibleRecoveryFee,--16
				sodd.SubTotalPatientSalesPrice,--17
				[Common].[CurrencyConverterByModule](rcd.PatientDiscount,@OfficialCurrencyId, @CurrencyId,@OperativeUnitId,'Invoice',NULL), --18
				rcd.PatientDiscountPercentage,--19
				[Common].[CurrencyConverterByModule](rcd.TotalPatientWithDiscount,@OfficialCurrencyId, @CurrencyId,@OperativeUnitId,'Invoice',NULL),--20
				[Common].[CurrencyConverterByModule](rcd.ValueVoucher,@OfficialCurrencyId, @CurrencyId,@OperativeUnitId,'Invoice',NULL),--21
				@TotalCrossingValue,--22
				sodd.InvoiceThirdPartySalesValue,--23
				sodd.SubTotalPatientSalesPrice,--24
				p.IPTIPOPAC, --25
				CONCAT
				(
					@Observation, 
					IIF(ISNULL(@Observation, '') = '' OR ISNULL(rcd.Observation, '') = '', '', ': '),
					rcd.Observation
				),--26
				p.IPTIPOAFI,--27
				p.CAPACIPAG,--28
				p.NIVECODIGO,--29
				0,--30
				0,--31
				1,--32
				@userCode,--33
				Common.[GETDATE](),--34
				rcd.InvoiceCategoryId,--35
				@OutputDate,--36
				@IsCutAccount,--37
				@OutputDiagnosis,--38
				@InitialDate,--39
				@CutType,--40
				@BillingAuthorizationId,--41
				@ContractId,--42
				IIF(@LiquidateMasterAccount = 0,
					(sodd.InvoiceThirdPartySalesValue + IIF(@FolioType = 3, 0, sodd.SubTotalPatientSalesPrice)),
					sodd.GrandTotalSalesPrice), --43
				sodd.GrandTotalTaxes, --44
				sodd.InvoiceThirdPartySalesValue,--45
				IIF(ISNULL(ba.InvoiceType, 0) = 3, 'CUFE', NULL),--46
				@CurrencyId,--47
				@TRMValue,--48
				ISNULL((SELECT SUM(tdbt.TaxValue)FROM @TaxDevolutionByTaxId tdbt),0) as TaxDevolutionValue, --49
				@IsElectronicTicket, --50
				@EconomicActivityId  --51
		FROM Billing.RevenueControlDetail rcd WITH (NOLOCK)
		JOIN [dbo].[INPACIENT] p WITH (NOLOCK) ON @PatientCode = p.IPCODPACI
		JOIN (	SELECT	tmp.RevenueControlDetailId,
						SUM(tmp.SubTotalSalesPrice) SubTotalSalesPrice,
						SUM(tmp.GrandTotalDiscount) GrandTotalDiscount,
						SUM(tmp.NetWorth) NetWorth,
						SUM(tmp.GrandTotalTaxes) GrandTotalTaxes,
						SUM(tmp.GrandTotalSalesPrice) GrandTotalSalesPrice,
						SUM(tmp.InvoiceThirdPartySalesValue) InvoiceThirdPartySalesValue,
						SUM(tmp.SubTotalPatientSalesPrice) SubTotalPatientSalesPrice
				FROM @RecalculateFolioDetails tmp
				group by tmp.RevenueControlDetailId) sodd on sodd.RevenueControlDetailId = rcd.Id
		LEFT JOIN Billing.BillingAuthorization ba WITH (NOLOCK) ON @BillingAuthorizationId = ba.Id
		WHERE rcd.Id = @RevenueControlDetailId	

		SET @InvoiceId = SCOPE_IDENTITY()
		
		
		INSERT INTO Billing.InvoiceDetail 
		(
			InvoiceId,--1
			ServiceOrderDetailId,--2
			GrandTotalSalesPrice,--3
			GrandTotalDiscount,--4
			DistributionType,--5
			ThirdPartySalesPrice,--6
			ThirdPartyPercentage,--7
			ApplyRecoveryFee,--8
			RecoveryFeeType, --9
			SubTotalPatientSalesPrice,--10
			PatientPercentage,--11 
			InvoicedQuantity,--12
			TotalSalesPrice,--13
			ServiceDate,--14
			ThirdPartyDiscount,
			Presentation,--15
			RecordType, --16
			Balance,--17
			NetWorth,--18
			GrandTotalTaxes,--19
			TaxId --20
		) OUTPUT INSERTED.Id INTO @InvoiceDetailIds(Id)
		SELECT	@InvoiceId,--1
				sodd.ServiceOrderDetailId,--2
				temp.GrandTotalSalesPrice, --3
				temp.GrandTotalDiscount,--4
				sodd.DistributionType, --5
				IIF(@LiquidateMasterAccount =0,temp.InvoiceThirdPartySalesValue,temp.GrandTotalSalesPrice),--6
				sodd.ThirdPartyPercentage,--7
				sodd.ApplyRecoveryFee,--8
				sodd.RecoveryFeeType,--9
				IIF(@LiquidateMasterAccount=0,temp.SubTotalPatientSalesPrice,0),--10 se quita para cuando sea cuenta madre, porque en la separacion de la cuenta no se suma en la cabecera,si molesta revisar el trigger [Billing].[TriggerValidateDataInvoiceDetail]
				sodd.PatientPercentage,--11
				IIF(@LiquidateMasterAccount=0,sod.InvoicedQuantity,sodd.Quantity), --12
				temp.TotalSalesPrice, --13
				sod.ServiceDate, --14
				[Common].[CurrencyConverterByModule](sod.ThirdPartyDiscount, @OfficialCurrencyId, @CurrencyId,@OperativeUnitId,'Invoice',NULL),--15
				sod.Presentation,--15
				sod.RecordType,--16 
				temp.InvoiceThirdPartySalesValue , --17
				temp.NetWorth, --18
				temp.GrandTotalTaxes,--19
				sod.IvaId --20
		FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
		JOIN @RecalculateFolioDetails temp on sodd.Id=temp.SodDistributionId
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = sodd.ServiceOrderDetailId
		JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on sodd.RevenueControlDetailId=rcd.Id
		WHERE sodd.RevenueControlDetailId = @RevenueControlDetailId AND sod.IsDelete = 0 

		IF @ConditionSalesId IS NOT NULL AND @ConditionSalesId > 0 
		BEGIN

		INSERT INTO Billing.ElectronicsProperties
		VALUES (@InvoiceId, 'Invoice',NULL, 1,NULL,@UserCode, Common.GETDATE(), NULL, NULL, @ConditionSalesId)

		END	
		
		INSERT INTO Billing.InvoiceDetailSurgical
		(
			InvoiceDetailId, IPSServiceId, InvoicedQuantity, LiquidationPercentage, 
			RateManualSalePrice, TotalSalesPrice, PerformsHealthProfessionalCode,
			PerformsHealthProfessionalThirdPartyId, CostValue, BillingConceptId, 
			CostCenterId, RateManualDetailSurgicalId, SurchargeApply, 
			OnlyMedicalFees, IncomeMainAccountId, Balance
		)
		SELECT	ind.Id, sods.IPSServiceId, sods.InvoicedQuantity, sods.LiquidationPercentage, 
				sods.RateManualSalePrice, sods.TotalSalesPrice, sods.PerformsHealthProfessionalCode, 
				sods.PerformsHealthProfessionalThirdPartyId, sods.CostValue, sods.BillingConceptId, 
				sods.CostCenterId, sods.RateManualDetailSurgicalId, sods.SurchargeApply, 
				sods.OnlyMedicalFees, sods.IncomeMainAccountId, sods.TotalSalesPrice
		FROM @InvoiceDetailIds idt
		JOIN Billing.InvoiceDetail ind WITH (NOLOCK) ON idt.Id = ind.Id
		JOIN Billing.ServiceOrderDetailSurgical sods WITH (NOLOCK) ON ind.ServiceOrderDetailId = sods.ServiceOrderDetailId
		
		/*---------- INSERT INTO TABLE InvoiceTaxDevolution -------*/
		INSERT INTO [Billing].[InvoiceTaxDevolution]
		SELECT	@InvoiceId,
				tdbt.TaxId,
				tdbt.TaxValue,
				[Common].[CurrencyConverterByModule](tdbt.TaxValue,@CurrencyId, @OfficialCurrencyId,@OperativeUnitId,'Invoice',NULL)
		FROM @TaxDevolutionByTaxId tdbt
		/*--------------------------------------------------*/
		
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

		IF @FolioType <> 5 AND @CalculateTaxAdvance IN (1, 2)
		BEGIN
			INSERT INTO Billing.InvoiceCustomerRetention
			(
				InvoiceId, CustomerRetentionId, CalculateTaxAdvance, RetentionType, RetentionRate, BaseValue, Value
			)
			SELECT @InvoiceId, cr.Id, @CalculateTaxAdvance, ma.RetencionType, rc.Rate, IIF(ma.RetencionType = 2, 0, @InvoiceThirdPartySalesValue), ROUND(IIF(ma.RetencionType = 2, 0, @InvoiceThirdPartySalesValue) * rc.Rate / 100, 2)
			FROM Billing.RevenueControlDetail rcd WITH (NOLOCK)
			JOIN Common.Customer c WITH (NOLOCK) ON rcd.ThirdPartyId = c.ThirdPartyId
			JOIN Common.CustomerRetention cr WITH (NOLOCK) ON c.Id = cr.CustomerId
			JOIN Portfolio.PortfolioNoteConcept pnc WITH (NOLOCK) ON cr.PortfolioNoteConceptId = pnc.Id
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK)  ON pnc.IdAccount = ma.Id
			JOIN GeneralLedger.RetentionConcepts rc WITH (NOLOCK) ON cr.RetentionConceptId = rc.Id
			WHERE rcd.Id = @RevenueControlDetailId AND cr.Status = 1
				AND ROUND(IIF(ma.RetencionType = 2, 0, @InvoiceThirdPartySalesValue) * rc.Rate / 100, 2) <> 0
		END

		IF EXISTS (SELECT 1 FROM @ListPortfolioAdvanceCrossing) BEGIN
			INSERT INTO Billing.InvoicePortfolioAdvance
				SELECT @InvoiceId, Id, CrossingValue
				FROM @ListPortfolioAdvanceCrossing
		END

		SET @Message = CONCAT('Factura número: ', @InvoiceNumber)
		SET @MessageResult = ISNULL(@MessageResult, '') + IIF(@Message = '', '', IIF(ISNULL(@MessageResult, '') = '', '', CHAR(13) + CHAR(10)) + @Message)

		------------------------------------  GENERACION DE LAS CUENTAS POR COBRAR ------------------------------------

		EXEC [Billing].[SP_CreateAccountReceivables_Output]	@CompanyType,
															@OperativeUnitId,
															@RevenueControlDetailId,
															@InvoiceId, 
															@FolioType,
															@LiquidationType,
															@ThirdPartyPatientId, 
															@ListPortfolioAdvanceCrossingXml, 
															@UserCode, 
															@CurrencyId,
															@TRMValue,
															--Salidas
															@StatusResult OUTPUT, 
															@Message OUTPUT 

		IF @StatusResult = 0
		BEGIN
			SELECT	@StatusResult = CONVERT(BIT, 0), 
					@MessageResult = '', 
					@MessageOutput = ISNULL(@Message, 'Error generando las cuentas por cobrar'), 
					@InvoiceId = 0, 
					@InvoiceNumber = '' 
			RETURN
		END

		SET @MessageResult = ISNULL(@MessageResult, '') + IIF(@Message = '', '', IIF(ISNULL(@MessageResult, '') = '', '', CHAR(13) + CHAR(10)) + @Message)

		IF EXISTS (SELECT 1 FROM @ListPortfolioAdvanceCrossing WHERE PortfolioAdvanceType = 1) BEGIN --Anticipos que sean de la entidad
		DECLARE @PortfolioAdvanceThirdPartyBeneficiaryXml XML,
				@AccountReceivableIdTmp INT,
				@MessageTmp VARCHAR(MAX)

			SET @PortfolioAdvanceThirdPartyBeneficiaryXml =
				(
					SELECT *
					FROM 
					(
						SELECT RowId,
						Id,
						Code,
						CrossingValue,
						CashReceiptDetailIdTmp
						FROM @ListPortfolioAdvanceCrossing
						WHERE PortfolioAdvanceType = 1
					) AS ListPortfolioAdvanceCrossing
					For Xml Auto, Elements
				)

				SELECT TOP 1 
				@AccountReceivableIdTmp = Id 
				FROM Portfolio.AccountReceivable 
				WHERE InvoiceId = @InvoiceId

				EXEC [Portfolio].[SP_GeneratePortfolioTransfer]  @PortfolioAdvanceThirdPartyBeneficiaryXml
																,@OperativeUnitId
																,@UserCode
																,@AccountReceivableIdTmp
																,@CompanyType
																,@StatusResult OUTPUT
																,@MessageTmp OUTPUT

			IF @StatusResult <> 0
			BEGIN
				SELECT	@StatusResult = CONVERT(BIT, 0), 
						@MessageResult = '', 
						@MessageOutput = 'Error al cruzar el cruce de anticipo vs CxC', 
						@InvoiceId = 0, 
						@InvoiceNumber = '' 
				RETURN
			END

			SET @Message = @MessageTmp
			SET @MessageResult = ISNULL(@MessageResult, '') + IIF(@Message = '', '', IIF(ISNULL(@MessageResult, '') = '', '', CHAR(13) + CHAR(10)) + @Message)
		END

		--------------------------------------------- ACTUALIZACIONES EHR ---------------------------------------------

		--RIAS
		UPDATE rcp 
			SET rcp.IDFACTURA = @InvoiceId
		FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
		JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON sod.CUPSEntityId = ce.Id
		JOIN [dbo].[RIASCUPSPACIENTE] rcp WITH (NOLOCK) ON rcp.IPCODPACI = @PatientCode AND rcp.IDRIASCUPS = sod.RIASCupsId AND rcp.CODSERIPS = ce.Code and rcp.IDDETALLEORDENSERVICIO = sod.Id
		WHERE sodd.RevenueControlDetailId = @RevenueControlDetailId AND sod.ApplyRIAS = 1

		--Laboratorios
		UPDATE rcp 
			SET rcp.GENINVOICEID = @InvoiceId,
				rcp.GENINVOICE = @InvoiceNumber
		FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
		JOIN [dbo].[AMBORDLAB] rcp WITH (NOLOCK) ON sod.ControlExternalConsultationCode = rcp.AUTO
		WHERE sodd.RevenueControlDetailId = @RevenueControlDetailId AND sod.ControlExternalConsultation = 1

		--Patologias
		UPDATE rcp 
			SET rcp.GENINVOICEID = @InvoiceId,
				rcp.GENINVOICE = @InvoiceNumber
		FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
		JOIN [dbo].[AMBORDPAT] rcp WITH (NOLOCK) ON sod.ControlExternalConsultationCode = rcp.AUTO
		WHERE sodd.RevenueControlDetailId = @RevenueControlDetailId AND sod.ControlExternalConsultation = 2

		--Imagenes
		UPDATE rcp 
			SET rcp.GENINVOICEID = @InvoiceId,
				rcp.GENINVOICE = @InvoiceNumber
		FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
		JOIN [dbo].[AMBORDIMA] rcp WITH (NOLOCK) ON sod.ControlExternalConsultationCode = rcp.AUTO
		WHERE sodd.RevenueControlDetailId = @RevenueControlDetailId AND sod.ControlExternalConsultation = 3

		--Consulta Externa
		UPDATE rcp 
			SET rcp.GENINVOICEID = @InvoiceId,
				rcp.GENINVOICE = @InvoiceNumber
		FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
		JOIN [dbo].[ADCONCOEX] rcp WITH (NOLOCK) ON sod.ControlExternalConsultationCode = rcp.CODCONCEC
		WHERE sodd.RevenueControlDetailId = @RevenueControlDetailId AND sod.ControlExternalConsultation = 8

		--Copagos, cuotas moderadoras, recaudos
		IF EXISTS (
			SELECT 1 
			FROM [dbo].[INPACIENTTOPANU] rcp WITH (NOLOCK)
			WHERE rcp.IPCODPACI = @PatientCode AND ANIO = YEAR(Common.[GETDATE]())
		)
		BEGIN
			UPDATE rcp
				SET rcp.PAGADOCMO = rcp.PAGADOCMO + sodd.PAGADOCMO,
					rcp.PAGADOCOP = rcp.PAGADOCOP + sodd.PAGADOCOP,
					rcp.PAGADOCRE = rcp.PAGADOCRE + sodd.PAGADOCRE
			FROM [dbo].[INPACIENTTOPANU] rcp WITH (NOLOCK)
			JOIN
			(
				SELECT	sodd.RevenueControlDetailId,
						SUM(IIF(sodd.RecoveryFeeType = 2, sodd.SubTotalPatientSalesPrice, 0)) PAGADOCMO,
						SUM(IIF(sodd.RecoveryFeeType = 3, sodd.SubTotalPatientSalesPrice, 0)) PAGADOCOP,
						SUM(IIF(sodd.RecoveryFeeType = 5, sodd.SubTotalPatientSalesPrice, 0)) PAGADOCRE
				FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
				WHERE sodd.RevenueControlDetailId = @RevenueControlDetailId
				GROUP BY sodd.RevenueControlDetailId
			) sodd ON @RevenueControlDetailId = sodd.RevenueControlDetailId
			WHERE rcp.IPCODPACI = @PatientCode AND ANIO = YEAR(Common.[GETDATE]())
		END
		ELSE
		BEGIN
			INSERT INTO [dbo].[INPACIENTTOPANU]
			(
				ANIO,IPCODPACI,PAGADOCMO, PAGADOCOP, PAGADOCRE
			)
			SELECT YEAR(Common.[GETDATE]()),@PatientCode,PAGADOCMO, PAGADOCOP, PAGADOCRE
			FROM
			(
				SELECT	sodd.RevenueControlDetailId,
						SUM(IIF(sodd.RecoveryFeeType = 2, sodd.SubTotalPatientSalesPrice, 0)) PAGADOCMO,
						SUM(IIF(sodd.RecoveryFeeType = 3, sodd.SubTotalPatientSalesPrice, 0)) PAGADOCOP,
						SUM(IIF(sodd.RecoveryFeeType = 5, sodd.SubTotalPatientSalesPrice, 0)) PAGADOCRE
				FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
				WHERE sodd.RevenueControlDetailId = @RevenueControlDetailId
				GROUP BY sodd.RevenueControlDetailId
			) sodd
		END
		
		---------------------------------------------------------------------------------------------------------------

		--Se actualiza el estado de las autorizaciones a Facturado(10) 
		UPDATE tp 
			SET tp.PreviousStatus = tp.Status, 
				tp.Status = 10
		FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = sodd.ServiceOrderDetailId
		JOIN [Authorization].TraceabilityPaperworkEvents tpe WITH (NOLOCK) on tpe.Id = sod.TraceabilityPaperworkEventsId
		JOIN [Authorization].TraceabilityPaperwork tp WITH (NOLOCK) on tp.Id = tpe.TraceabilityPaperworkId
		WHERE RevenueControlDetailId = @RevenueControlDetailId AND sod.IsDelete = 0

		UPDATE rcd
			SET rcd.BillingAuthorizationId = @BillingAuthorizationId,
				rcd.OutputDate = @OutputDate,
				rcd.IsCutAccount = @IsCutAccount,
				rcd.OutputDiagnosis = @OutputDiagnosis,
				rcd.[Status] = 2
		FROM Billing.RevenueControlDetail rcd
		WHERE rcd.Id = @RevenueControlDetailId

		-------------------generación de documento contable para paquetes------------------------		
		if @ValidatePackaging = 1 and @AccountingPackage = 1 
		begin
			exec [Billing].[SP_CreatePackageJournalVoucher_Output] @InvoiceId, @UserCode, 
				@StatusResult output, 
				@Message output 

			IF @StatusResult = 0
			BEGIN
				SELECT	@StatusResult = CONVERT(BIT, 0), 
						@MessageResult = '', 
						@MessageOutput = ISNULL(@Message, 'Error generando el comprobante contable de paquetes'), 
						@InvoiceId = 0, 
						@InvoiceNumber = '' 
				RETURN
			END
		end
		---------------------------------------------------------------------------------------------------------------

		SELECT	@StatusResult = CONVERT(BIT, 1),
				@MessageResult = ISNULL(@MessageResult, ''),
				@MessageOutput = ISNULL(@MessageOutput, ''),
				@InvoiceId = @InvoiceId, 
				@InvoiceNumber = @InvoiceNumber
	END TRY
	BEGIN CATCH
		SELECT	@StatusResult = CONVERT(BIT, 0),
				@MessageResult = '',
				@MessageOutput = CONCAT('Error creando la factura: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()),
				@InvoiceId = 0,
				@InvoiceNumber = ''
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la factura de salida (egreso) y las cuentas por cobrar correspondientes a un folio de liquidación de un control de ingresos. Toma los parámetros del paciente, la admisión, la fecha de corte y los cruces de anticipos para calcular los valores a facturar al tercero pagador (EPS, aseguradora) y al paciente (cuotas moderadoras, copagos, descuentos). Consulta la configuración del módulo de facturación por unidad operativa (SettingsBilling), los datos del contrato y grupo de atención (CareGroup, Contract, ContractDetail) y el detalle del control de ingresos (RevenueControlDetail) para determinar el tipo de folio, tipo de liquidación, plazos de pago y observaciones permanentes de la factura. Retorna el identificador y número de la factura generada, así como mensajes de resultado para ser consumido por el proceso de facturación electrónica o de cartera.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreateInvoice_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CreateInvoice_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la factura de salida de un folio (RevenueControlDetail), su detalle, documentos electrónicos asociados, retenciones, devoluciones de IVA y dispara la creación de cuentas por cobrar y cruces de anticipos, dejando el folio en estado facturado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateInvoice_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El RevenueControlDetail debe tener InvoiceCategoryId asignado (no NULL).; Las cantidades del SODD deben ser > 0 y la InvoicedQuantity del SOD no puede ser negativa.; Para folios con DistributionType=1, sin IncludeServiceOrderDetailId, no paquete y SettlementType=1, las cantidades del folio (SODD.Quantity) y de la orden (SOD.InvoicedQuantity) deben coincidir.; El número de líneas del folio no debe superar SettingsBilling.MaxInvoiceItems cuando éste sea > 0.; Si LiquidationType=1, el consecutivo de la BillingAuthorization tras incrementar debe quedar dentro del rango [InitialInvoice, FinalInvoice] y la fecha de factura debe estar entre InitialDate y FinalDate de la autorización.; La función Billing.RecalculateFolioDetailsByCurrency debe retornar registros y todos con StatusResult=1 para la moneda solicitada.; Para FolioType=1 con TerminationControl IN (3,4), el ExecuteValue acumulado del contrato no debe superar el ContractValue.; Existe configuración Billing.SettingsBilling para la unidad operativa y CompanySettings con OfficialCurrencyId.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateInvoice_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CreateInvoice_Output';
-- GO
