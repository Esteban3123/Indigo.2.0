-- =============================================
-- Author:		Diego Andrés Roldán Lozano
-- Create date: 29-09-2015
-- Description:	Procedimiento para la anulación de un folioPEND
-- =============================================
CREATE PROCEDURE [Billing].[SP_AnulateInvoice]
	@OperativeUnitId INT,
	@RevenueControlDetailId INT,
	@ReversalReasonId INT,
	@ReversalReasonDescription VARCHAR(300),
	@CodUser VARCHAR(20),
	@ContainerNameCrystal VARCHAR(20),
	@PatientCode VARCHAR(20),
	------------------------------------------------------
	@CompanyType TINYINT
AS
BEGIN
	SET NOCOUNT ON

	/****************************************************** DECLARACION DE VARIABLES ******************************************************/

	DECLARE @FolioOrder TINYINT,
			@TotalFolio DECIMAL(20,2),
			@CareGroupId INT,
			@FolioLiquidationType TINYINT,
			-------------------------------------------------
			@InvoiceId INT,
			@InvoiceNumber VARCHAR(20),
			@AdmissionNumber VARCHAR(50),
			@DiscountApply DECIMAL(20,2),
			@ThirdPartySalesValue DECIMAL(20,2),
			@AnnulmentDate DATETIME,
			-------------------------------------------------
			@ContractId INT,
			@ContractExecuteValue DECIMAL(18,0),
			@ContractValue DECIMAL(18,0),
			@TerminationControl TINYINT,
			@ContractCodeName VARCHAR(50),
			@ContractNotificationValueType TINYINT,
			@ContractNotificationValue DECIMAL(18,0),
			@ContractEndDate DATE,
			@ContractNotificationTimeType TINYINT,
			@ContractNotificationDays INT,			
			@ContractPercentageNotification DECIMAL(5,2),
			@CareGroupType TINYINT,
			@StatusContract TINYINT,
			@IsMasterAccount TINYINT,
			@FolioType TINYINT,
			-------------------------------------------------
			@Message VARCHAR(MAX),
			@messageValidationContract VARCHAR(MAX),
			-------------------------------------------------
			@JournalVoucherTypeId INT,
			@ApplyElectronicSalesTicket BIT,
			@XmlAccounting XML,
			-------------------------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@LiquidatedPackageJournalVoucherTypeId int,
			@JournalVoucherId_Output INT,
			@ReversalPreviousYearsMainAccountId INT,
			@ReversalPreviousYearsGenericBillingMainAccountId INT,
			@InvoiceDate DATETIME,
			@LiquidateMasterAccount BIT

	-- Detalle comprobante contable
	DECLARE @JournalVourcherDetailTmp TABLE 
	(
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(21,5),
		CreditValue DECIMAL(21,5),
		Detail VARCHAR(500),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	-- Detalle Comprobante para comparar que los valores del comprobante original y el recreado coincida
	DECLARE @JournalVourcherDetailToCompare TABLE 
	(
		Id INT DEFAULT(0),
		IdAccounting INT DEFAULT(0),
		IdMainAccount INT,
		IdThirdParty INT,
		IdCostCenter INT,
		DebitValue DECIMAL(21,5),
		CreditValue DECIMAL(21,5),
		Detail VARCHAR(500),
		IdRetention INT,
		RetentionRate DECIMAL(6,3),
		BaseValue DECIMAL(18,2),
		BillingValue DECIMAL(18,2)
	)

	--Tabla variable para almacenar los detalles que estan en el Pending
	DECLARE @JournalVoucherDetailsOrigin AS TABLE
			(	IdMainAccount INT,
				IdThirdParty INT,
				IdCostCenter INT,
				DebitValue NUMERIC(21,5),
				CreditValue NUMERIC(21,5),
				Detail VARCHAR(500),
				IdRetention INT,
				RetentionRate DECIMAL(6,3),
				BaseValue DECIMAL(18,2),
				BillingValue DECIMAL(18,2),
				LegalBookId INT,
				EntityId INT,
				OfficialBook BIT)

	--tabla temporal para almacenar el resultado del movimiento contable
	DECLARE @resultJournalVoucher TABLE
		(
			code INT, 
			MessageResult VARCHAR(max), 
			IdJournalVoucher INT
		)	
	BEGIN TRY
		/********************************************** ASIGNACIONES DE VARIABLES **********************************************/
		
		SELECT	@FolioOrder = rcd.FolioOrder,
				@TotalFolio = rcd.TotalFolio,
				@CareGroupId = ISNULL(i.CareGroupId, rcd.CareGroupId),
				@FolioLiquidationType = rcd.LiquidationType,
				-------------------------------------------------
				@InvoiceId = i.Id,
				@InvoiceNumber = i.InvoiceNumber,
				@AdmissionNumber = i.AdmissionNumber,
				@DiscountApply = COALESCE(i.PatientDiscount,0),
				@ThirdPartySalesValue = COALESCE(i.ThirdPartySalesValue,0),				
				@AnnulmentDate = [Common].[GETDATE](),
				@InvoiceDate = i.InvoiceDate,
				-------------------------------------------------
				@ContractId = c.Id,
				@ContractExecuteValue = c.ExecuteValue,
				@ContractValue = c.ContractValue,
				@TerminationControl = c.TerminationControl,
				@ContractCodeName = CONCAT(c.Code,' - ',c.ContractName),
				@ContractNotificationValueType = c.NotificationValueType,
				@ContractNotificationValue = c.NotificationValue,
				@ContractEndDate = c.EndDate,
				@ContractNotificationTimeType = c.NotificationTimeType,
				@ContractNotificationDays = c.NotificationDays,
				@ContractPercentageNotification = c.PercentageNotification,
				@CareGroupType = cg.CareGroupType,
				@StatusContract = c.Status,
				@IsMasterAccount = rcd.IsMasterAccount,
				@FolioType = rcd.FolioType
		FROM Billing.RevenueControlDetail rcd
		LEFT JOIN Billing.Invoice i ON rcd.Id = i.RevenueControlDetailId AND i.Status = 1
		LEFT JOIN Contract.CareGroup cg ON ISNULL(i.CareGroupId, rcd.CareGroupId) = cg.Id
		LEFT JOIN Contract.Contract c ON ISNULL(i.ContractId, cg.ContractId) = c.Id
		WHERE rcd.Id = @RevenueControlDetailId
	
		SELECT @JournalVoucherTypeId = InvoiceAnnulmentJournalVoucherTypeId,
				@LiquidatedPackageJournalVoucherTypeId = LiquidatedPackageJournalVoucherTypeId,			
				@ReversalPreviousYearsMainAccountId  = ReversalPreviousYearsMainAccountId  ,
				@ReversalPreviousYearsGenericBillingMainAccountId = ReversalPreviousYearsGenericBillingMainAccountId,
				@LiquidateMasterAccount = LiquidateMasterAccount,
				@ApplyElectronicSalesTicket = ApplyElectronicSalesTicket
		FROM Billing.SettingsBilling WITH(NOLOCK)
		WHERE IdOperatingUnit = @OperativeUnitId

		--Si aplica la logica de tiquete electronico de venta se cambia el tipo de comprobante contable para la reversion
		IF @ApplyElectronicSalesTicket = 1 AND EXISTS(SELECT 1 
														FROM Billing.Invoice i
														JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
														WHERE i.Id = @InvoiceId AND tp.ElectronicBiller = 0) BEGIN

			SELECT @JournalVoucherTypeId = sb.AccountingVoucherReversalId
			FROM Billing.SettingsBilling sb
			WHERE sb.IdOperatingUnit = @OperativeUnitId
		END

		/***************************************************  VALIDACIONES *****************************************************/

		IF ISNULL(@FolioLiquidationType, 0) = 0 
		BEGIN
			SELECT 999 AS CodeResult, 'No se ha identificado el tipo de liquidación del folio' AS MessageResult, '' AS NotificationContract
			RETURN
		END

		--==validación de que haya una factura activa para el folio
		IF ISNULL(@InvoiceId, 0) = 0
		BEGIN 
			SELECT 999 AS CodeResult, 'No se puede liquidar debido a que no hay una factura activa para el folio ' + CAST(@FolioOrder AS VARCHAR(10)) AS MessageResult, '' AS NotificationContract
			RETURN
		END
		
		--==Si no se permite anular facturas de periodos anteriores, valido que la factura sea del periodo
		IF EXISTS
		(
			SELECT 1
			FROM Billing.Invoice i 
			JOIN Billing.SettingsBilling sb ON i.OperatingUnitId = sb.IdOperatingUnit
			WHERE i.Id = @InvoiceId AND sb.AnulateInvoicesPreviousPeriods = 0
		)
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Billing.Invoice i WHERE i.Id = @InvoiceId AND YEAR(i.InvoiceDate) = YEAR(@AnnulmentDate) AND MONTH(i.InvoiceDate) = MONTH(@AnnulmentDate))
			BEGIN
				IF [Common].[ValidatePermision](@CodUser, '756', '89') = 0
				BEGIN
					SELECT 999 AS CodeResult, 'El usuario no cuenta con permisos para anular Factura Periodo Anterior ' AS MessageResult, '' AS NotificationContract
					RETURN
				END
			END
		END

		IF @CareGroupType = 1 AND ISNULL(@ContractId, 0) = 0
		BEGIN
			SELECT 999 AS CodeResult, 'No se puede Anular el folio (' + CAST(@FolioOrder AS VARCHAR(10)) + ') debido a que no se encontro contrato relacionado' AS MessageResult, '' AS NotificationContract
			RETURN
		END

		--==Validaciones de terminación del contrato
		IF @StatusContract = 2 or @StatusContract = 3
		BEGIN
			SELECT 999 AS CodeResult, 'No se puede Anular el folio (' + CAST(@FolioOrder AS VARCHAR(10)) + ') debido a que el contrato esta (' + IIF(@StatusContract=2,'Suspendido','Terminado') + ')' AS MessageResult, '' AS NotificationContract
			RETURN
		END

		--se busca si alguna cuenta por cobrar ya tiene movimiento
		IF @FolioLiquidationType = 1
		BEGIN --Pago por servicios
			DECLARE @TotalCrossing decimal(20,2) = 0

			IF @CareGroupType = 3 OR @IsMasterAccount =4
			BEGIN
				SELECT @TotalCrossing = COALESCE(SUM(Value),0) FROM Billing.InvoicePortfolioAdvance WHERE InvoiceId = @InvoiceId
			END
			ELSE IF EXISTS (SELECT 1 FROM Portfolio.AccountReceivable WHERE InvoiceId = @InvoiceId AND PortfolioStatus <> 1 AND AccountReceivableType NOT IN (4, 6))
			BEGIN
				SELECT 999 AS CodeResult, 'No se puede anular debido a que la Factura ' + @InvoiceNumber + ' ha sido radicada' AS MessageResult, '' AS NotificationContract
				RETURN
			END

			IF EXISTS(	select 1 
						FROM Billing.InvoicePortfolioAdvance ipa 
						JOIN Portfolio.PortfolioAdvance pa ON pa.Id = ipa.PortfolioAdvanceId
						WHERE pa.ThirdPartyBeneficiaryId IS NOT NULL) 
			BEGIN
				SELECT @TotalCrossing += SUM(ipa.Value)
				FROM Billing.InvoicePortfolioAdvance ipa
				JOIN Portfolio.PortfolioAdvance pa ON pa.Id = ipa.PortfolioAdvanceId
				WHERE pa.ThirdPartyBeneficiaryId IS NOT NULL AND ipa.InvoiceId = @InvoiceId
			END

			IF EXISTS
			(
				SELECT 1 
				FROM Portfolio.AccountReceivable ar
				LEFT JOIN
				(
					SELECT InvoiceId, SUM(Value) Value
					FROM Billing.InvoiceCustomerRetention
					WHERE InvoiceId = @InvoiceId AND CalculateTaxAdvance = 2
					GROUP BY InvoiceId
				) icr ON ar.InvoiceId = icr.InvoiceId
				WHERE ar.InvoiceId = @InvoiceId AND AccountReceivableType <> 6 AND (Balance+@TotalCrossing+ISNULL(icr.Value, 0)) <> ar.Value
			)
			BEGIN
				SELECT @message = STUFF((
					SELECT N'; La factura '+@InvoiceNumber+' no se puede anular porque la cuenta por cobrar ('+ar.Code+') ya tiene movimiento'
					FROM Portfolio.AccountReceivable AS ar
					WHERE InvoiceId = @InvoiceId AND AccountReceivableType <> 6
					ORDER BY Code
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeResult, @Message AS MessageResult, '' AS NotificationContract
				RETURN
			END	
		END
		ELSE IF EXISTS
		(
			SELECT 1
			FROM Billing.InvoiceEntityCapitatedDistribution iecd
			JOIN Billing.InvoiceEntityCapitatedDistributionDetail iecdd ON iecd.Id = iecdd.InvoiceEntityCapitatedDistributionId
			WHERE iecd.Status = 2 AND iecdd.InvoiceId = @InvoiceId
		)
		BEGIN
			SELECT	999 AS CodeResult, 'No se puede anular debido a que la Factura ' + @InvoiceNumber + ' se encuentra distribuida' AS MessageResult, '' AS NotificationContract
			RETURN
		END

		/********************************************** VALIDACIONES DEL CONTRATO **********************************************/

		IF @CareGroupType = 1
		BEGIN
			--==VALIDACIONES Y NOTIFICACIONES DEL CONTRATO
			DECLARE @NewExecuteValue DECIMAL(20,2) = COALESCE((@ContractExecuteValue-@ThirdPartySalesValue), 0)

			IF @TerminationControl = 2
			BEGIN
				--Terminación del contrato por Fecha del contrato
				IF @AnnulmentDate > @ContractEndDate
					SET @messageValidationContract += 'El folio '+CONVERT(VARCHAR(10),@FolioOrder)+' no se pueden liquidar debido a ya se venció la fecha del contrato '+@ContractCodeName
				IF @ContractNotificationTimeType=2 AND DATEADD(DAY, @ContractNotificationDays, @AnnulmentDate) >= @ContractEndDate
					SET @messageValidationContract += 'Atención: La fecha del contrato vencerá en '+ CONVERT(VARCHAR(20), DATEDIFF(DD, @ContractEndDate, DATEADD(DAY, @ContractNotificationDays, @AnnulmentDate)))+' días'
			END
			ELSE IF @TerminationControl = 3
			BEGIN
				--Terminación del contrato por valor del contrato
				IF @NewExecuteValue > @ContractValue
					SET @messageValidationContract += 'El folio '+CONVERT(VARCHAR(10),@FolioOrder)+' no se pueden liquidar debido a que se superaría el valor del contrato '+@ContractCodeName
				IF @ContractNotificationValueType=2 AND @NewExecuteValue > @ContractValue*@ContractPercentageNotification/100
					SET @messageValidationContract += 'Atención: queda un saldo restante de '+CONVERT(VARCHAR(50),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
				ELSE IF @ContractNotificationValueType=3 AND @NewExecuteValue > @ContractNotificationValue
					SET @messageValidationContract += 'Atención: queda un saldo restante de '+CONVERT(VARCHAR(50),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
			END
			ELSE IF @TerminationControl = 4
			BEGIN
				--Terminación del contrato por Fecha o Valor del contrato
				IF @NewExecuteValue > @ContractValue
					SET @messageValidationContract += 'El folio '+CONVERT(VARCHAR(10),@FolioOrder)+' no se pueden liquidar debido a que se superaría el valor del contrato '+@ContractCodeName
				IF @AnnulmentDate > @ContractEndDate
					SET @messageValidationContract += 'El folio '+CONVERT(VARCHAR(10),@FolioOrder)+' no se pueden liquidar debido a ya se venció la fecha del contrato '+@ContractCodeName
				IF @ContractNotificationValueType=2 AND @NewExecuteValue > @ContractValue*@ContractPercentageNotification/100
					SET @messageValidationContract += 'Atención: queda un saldo restante de '+CONVERT(VARCHAR(50),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
				ELSE IF @ContractNotificationValueType=3 AND @NewExecuteValue > @ContractNotificationValue
					SET @messageValidationContract += 'Atención: queda un saldo restante de '+CONVERT(VARCHAR(50),(@ContractValue-@NewExecuteValue))+' para la terminación del contrato '+@ContractCodeName
				IF @ContractNotificationTimeType=2 AND DATEADD(DAY, @ContractNotificationDays, @AnnulmentDate) >= @ContractEndDate
					SET @messageValidationContract += 'Atención: La fecha del contrato vencerá en '+ CONVERT(VARCHAR(20),DATEDIFF(DD, @ContractEndDate, DATEADD(DAY, @ContractNotificationDays, @AnnulmentDate))) +' días'
			END

			--===Actualizar el contrato
			UPDATE Contract.Contract 
				SET ExecuteValue = @NewExecuteValue 
			WHERE Id = @ContractId
		END

		/********************************************** VALIDACION SALDOS PAGARES **********************************************/

		IF EXISTS (SELECT 1 FROM Portfolio.AccountReceivable WHERE InvoiceId = @InvoiceId AND AccountReceivableType = 4 AND Value <> Balance)
		BEGIN
			SELECT	999 AS CodeResult, 'No se pudo reversar el pagaré asociados al control de servicio' AS MessageResult, '' AS NotificationContract
			RETURN
		END

		--=============================================================================================================================================================================

		SET @Message = 'El folio ' + CAST(@FolioOrder AS VARCHAR(20)) + ' se anuló correctamente generando:'
		SET @Message = @Message + CHAR(13) + CHAR(10) +  CONCAT('Se reversó la Factura ', @InvoiceNumber)

		/***************************** REVERSIÓN DE LAS CUENTAS POR COBRAR Y DE CRUCE DE ANTICIPOS *****************************/
		EXEC [Billing].[SP_ReversePortfolioByInvoiceId_Output] @OperativeUnitId, @InvoiceId, @AnnulmentDate, @ReversalReasonDescription, @CodUser, @CompanyType, @Code_Output OUT, @Message_Output OUT

		IF @Code_Output <> 0 
		BEGIN
			SELECT	999 AS CodeResult, ISNULL(@Message_Output, 'No se pudo reversar los cruces de anticipo asociados a la factura') AS MessageResult, '' AS NotificationContract
			RETURN
		END

		SET @Message = @Message + IIF(ISNULL(@Message_Output, '') = '', '', CHAR(13) + CHAR(10) + @Message_Output)
		/****************************************** GENERACION DEL DOCUMENTO CONTABLE ******************************************/
		
		IF @FolioLiquidationType = 1
		BEGIN --Pago por servicios
			DECLARE @RegeneratedDetailRequiredForReversal BIT = 0,
					@DebitCompareVal DECIMAL(21, 5),
					@DebitOriginVal DECIMAL(21, 5)

			/*Detalles del Comprobante contable*/
			INSERT INTO @JournalVourcherDetailToCompare 
			(
				IdMainAccount,IdThirdParty,IdCostCenter,CreditValue,DebitValue,Detail
			)
			EXEC Billing.SP_GenerateJournalVoucherDetails @RevenueControlDetailId, @OperativeUnitId, 1, @InvoiceId

			--Funcionalidad para obtener los datos de detalles contables cuando esta activo el desacople contable.
			--se verifica que No exista un comprobante creado aun sino se toma de las tablas de journalVouchers
			IF NOT EXISTS(	SELECT 1
							FROM GeneralLedger.LegalBook lb WITH(NOLOCK)
							JOIN GeneralLedger.JournalVouchers jv WITH(NOLOCK) ON lb.Id = jv.LegalBookId
							WHERE lb.OfficialBook =1 AND jv.EntityName = 'Invoice' AND jv.EntityId = @InvoiceId )
			BEGIN
				
				SELECT top 1 @XmlAccounting = am.JournalVoucherXml
				FROM GeneralLedger.AccountingMovement AS am WITH(NOLOCK) 
				WHERE  am.EntityName = 'Invoice' AND am.EntityId = @InvoiceId 

				INSERT INTO @JournalVoucherDetailsOrigin(	IdMainAccount,IdThirdParty,
															IdCostCenter,DebitValue,
															CreditValue,Detail,
															IdRetention,RetentionRate,
															BaseValue,BillingValue,
															LegalBookId,EntityId)
				SELECT  t.x.value('(IdMainAccount/text())[1]','int') AS IdMainAccount,
						t.x.value('IdThirdParty[1]','int') AS IdThirdParty,
						t.x.value('IdCostCenter[1]','int') AS IdCostCenter,
						t.x.value('(DebitValue/text())[1]','decimal(21, 5)') AS DebitValue,
						t.x.value('(CreditValue/text())[1]','decimal(21, 5)') AS CreditValue,
						t.x.value('(Detail/text())[1]','varchar(500)') AS Detail,
						t.x.value('(IdRetention/text())[1]','int') AS IdRetention,
						t.x.value('(RetentionRate/text())[1]','decimal(6, 3)') AS RetentionRate,
						t.x.value('(BaseValue/text())[1]','decimal(18, 2)') AS BaseValue,
						t.x.value('(BillingValue/text())[1]','decimal(18, 2)') AS BillingValue,
						NULL,
						@InvoiceId
				FROM  @XmlAccounting.nodes('/JournalVoucher/JournalVoucherDetail') t(x)

			END
			ELSE BEGIN
				INSERT INTO @JournalVoucherDetailsOrigin(	IdMainAccount,IdThirdParty,
															IdCostCenter,DebitValue,
															CreditValue,Detail,
															IdRetention,RetentionRate,
															BaseValue,BillingValue,
															LegalBookId,EntityId,OfficialBook)				
				SELECT	jvd.IdMainAccount,
						jvd.IdThirdParty,
						jvd.IdCostCenter,
						jvd.DebitValue,
						jvd.CreditValue,
						jvd.Detail,
						jvd.IdRetention,
						jvd.RetentionRate,
						jvd.BaseValue,
						jvd.BillingValue,
						lb.Id,
						jv.EntityId,
						lb.OfficialBook
				FROM GeneralLedger.LegalBook lb WITH(NOLOCK)
				JOIN GeneralLedger.JournalVouchers jv WITH(NOLOCK) ON lb.Id = jv.LegalBookId
				JOIN GeneralLedger.JournalVoucherDetails jvd WITH(NOLOCK) ON jv.Id = jvd.IdAccounting
				WHERE jv.EntityName = 'Invoice' AND jv.EntityId = @InvoiceId
					AND lb.OfficialBook = 1
			END

			SET @RegeneratedDetailRequiredForReversal = 0
			IF (SELECT COUNT(*) FROM (SELECT lb.OfficialCurrencyId FROM GeneralLedger.LegalBook lb WITH(NOLOCK) GROUP BY lb.OfficialCurrencyId) f) > 1
				SET @RegeneratedDetailRequiredForReversal = 1

			-- Si el reverso usa líneas recalculadas (varias monedas oficiales entre libros), deben coincidir con lo contabilizado.
			-- Con una sola moneda oficial el reverso toma el detalle ya contabilizado; comparar con recálculo actual suele fallar por TRM, folio o parámetros.
			IF @RegeneratedDetailRequiredForReversal = 1
			BEGIN
				SELECT @DebitCompareVal = ISNULL(SUM(jvd.DebitValue), 0) FROM @JournalVourcherDetailToCompare jvd
				SELECT @DebitOriginVal = ISNULL(SUM(jvdo.DebitValue), 0)
				FROM @JournalVoucherDetailsOrigin jvdo
				WHERE jvdo.EntityId = @InvoiceId AND ISNULL(jvdo.OfficialBook, 1) = 1

				IF ROUND(@DebitCompareVal, 2) <> ROUND(@DebitOriginVal, 2)
				BEGIN
					SELECT 999 AS CodeResult,
						'Los valores a contabilizar de la reversión no corresponden con lo contabilizado por la factura. Débitos recalculados: '
						+ RTRIM(CAST(ROUND(@DebitCompareVal, 2) AS VARCHAR(42)))
						+ N'; débitos contabilizados (libro oficial): '
						+ RTRIM(CAST(ROUND(@DebitOriginVal, 2) AS VARCHAR(42))) AS MessageResult, '' AS NotificationContract
					RETURN
				END
			END

			IF @RegeneratedDetailRequiredForReversal = 1
			BEGIN
				INSERT INTO @JournalVourcherDetailTmp
				SELECT *
				FROM @JournalVourcherDetailToCompare
			END
			ELSE
			BEGIN
				INSERT INTO @JournalVourcherDetailTmp
				SELECT	0,
						0,
						jvd.IdMainAccount,
						jvd.IdThirdParty,
						jvd.IdCostCenter,
						jvd.CreditValue,
						jvd.DebitValue,
						jvd.Detail,
						jvd.IdRetention,
						jvd.RetentionRate,
						jvd.BaseValue,
						jvd.BillingValue
				FROM @JournalVoucherDetailsOrigin jvd
				WHERE ISNULL(jvd.OfficialBook, 1) = 1 AND jvd.EntityId = @InvoiceId
			END
																			-- Se valida que el cliente tenga parametrizado la Cuenta Contable para Vigencias Anteriores
		    IF DATEPART(YEAR,@AnnulmentDate) > DATEPART(YEAR,@InvoiceDate) AND @ReversalPreviousYearsMainAccountId IS NOT NULL -- Si no la tiene se omite esta lógica
			BEGIN			
				DECLARE @DestHandlesTP BIT = 0;
				DECLARE @InvoiceThirdPartyId INT;

				SELECT @DestHandlesTP = ma.HandlesThirdParty
				FROM GeneralLedger.MainAccounts ma
				WHERE ma.Id = @ReversalPreviousYearsMainAccountId;

				SELECT @InvoiceThirdPartyId = i.ThirdPartyId
				FROM Billing.Invoice i
				WHERE i.Id = @InvoiceId;

				UPDATE tmp
				SET tmp.IdMainAccount = @ReversalPreviousYearsMainAccountId,
					tmp.IdThirdParty  = CASE
										  WHEN @DestHandlesTP = 1 AND (tmp.IdThirdParty IS NULL OR tmp.IdThirdParty = 0)
											THEN @InvoiceThirdPartyId
										  ELSE tmp.IdThirdParty
										END
				FROM @JournalVourcherDetailTmp tmp
				JOIN @JournalVourcherDetailToCompare tmpc 
				  ON tmp.IdMainAccount = tmpc.IdMainAccount
				 AND (tmpc.Detail = tmp.Detail OR tmp.Detail IS NULL)
				WHERE tmpc.Detail = 'Producto/Servicio';
			END	

					
			SELECT @SubXml = CONVERT
			(
				XML,
				(
					SELECT * 
					FROM
					(
						SELECT	@JournalVoucherTypeId AS IdJournalVoucher,
								@AnnulmentDate AS VoucherDate,
								0 AS Imported,
								2 AS Status,
								'Anulación: Factura No. ' + i.InvoiceNumber + ' - Tercero (' + CONCAT(tp.Nit, ' - ', tp.Name) + ')' AS Detail,
								i.InvoiceNumber AS EntityCode,
								i.Id AS EntityId,
								'Invoice' AS EntityName,
								0 AS IsClosedYear,
								i.CurrencyId
						FROM Billing.Invoice i
						JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
						WHERE i.Id = @InvoiceId
					) JournalVoucher 
					CROSS APPLY @JournalVourcherDetailTmp As JournalVoucherDetail
					FOR XML AUTO,TYPE, ELEMENTS
				)
			)

			--Se consume el sp que guarda el comprobante contable
			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml,@CodUser 
			select 
				@Code_Output = rjv.code, 
				@Message_Output = rjv.MessageResult, 
				@JournalVoucherId_Output = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv
				
			IF ISNULL(@Code_Output, 999) <> 0 
			BEGIN
				SELECT 999 AS CodeResult, ISNULL(@Message_Output, 'Error al generar el comprobante contable') AS MessageResult, '' AS NotificationContract
				RETURN
			END

			--consultar tipo de documento
			SELECT @Message = @Message + CHAR(13) + CHAR(10) + 'Se generó el Comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name
			FROM GeneralLedger.JournalVoucherTypes jvt
			WHERE jvt.Id = @JournalVoucherTypeId
		END
		ELSE
		BEGIN
			IF EXISTS (SELECT 1 FROM Portfolio.AccountReceivable WHERE InvoiceId = @InvoiceId AND AccountReceivableType NOT IN (4, 6))
			BEGIN
				--Se valida si la factura contabilizo, la reversión tambien lo debe hacer
				IF EXISTS
				(
					SELECT 1
					FROM GeneralLedger.JournalVouchers jv
					WHERE jv.EntityName = 'Invoice' AND jv.EntityId = @InvoiceId
				)
				BEGIN
					SELECT 999 AS CodeResult, 'Los valores a contabilizar de la reversión no corresponden con lo contabilizado por la factura (La anulación no esta generando comprobante contable)' AS MessageResult, '' AS NotificationContract
					RETURN
				END
			END

			IF EXISTS (SELECT 1 FROM Portfolio.AccountReceivable WHERE InvoiceId = @InvoiceId AND AccountReceivableType = 4)
			BEGIN
				SELECT @SubXml = CONVERT
				(
					XML,
					(
						SELECT * 
						FROM
						(
							SELECT	@JournalVoucherTypeId AS IdJournalVoucher,
									@AnnulmentDate AS VoucherDate,
									0 AS Imported,
									2 AS Status,
									'Anulación: Pagaré Asociado al Control de Servicio No. ' + i.InvoiceNumber AS Detail,
									i.InvoiceNumber AS EntityCode,
									i.Id AS EntityId,
									'Invoice' AS EntityName,
									0 AS IsClosedYear
							FROM Billing.Invoice i
							WHERE i.Id = @InvoiceId
						) JournalVoucher 
						CROSS APPLY
						(
									SELECT	ma.Id IdMainAccount,
											IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL) IdThirdParty,
											IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL) IdCostCenter,
											0 DebitValue,
											ar.Value CreditValue
									FROM Portfolio.AccountReceivable ar
									JOIN GeneralLedger.MainAccounts ma ON ar.AccountWithoutRadicateId = ma.Id
									WHERE ar.InvoiceId = @InvoiceId AND ar.AccountReceivableType = 4
								UNION ALL
									SELECT	ma.Id IdMainAccount,
											IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL) IdThirdParty,
											IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL) IdCostCenter,
											ar.Value DebitValue,
											0 CreditValue
									FROM Portfolio.AccountReceivable ar
									JOIN Billing.SettingsBilling sb ON ar.OperatingUnitId = sb.IdOperatingUnit
									JOIN Treasury.CashReceiptConcepts crc ON sb.CapitedPatientAdvanceCashReceiptConceptId = crc.Id
									JOIN GeneralLedger.MainAccounts ma ON crc.IdMainAccount = ma.Id
									WHERE ar.InvoiceId = @InvoiceId AND ar.AccountReceivableType = 4
							) As JournalVoucherDetail
						FOR XML AUTO,TYPE, ELEMENTS
					)
				)

				--Se consume el sp que guarda el comprobante contable
				insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml,@CodUser 
				select 
					@Code_Output = rjv.code, 
					@Message_Output = rjv.MessageResult, 
					@JournalVoucherId_Output = rjv.IdJournalVoucher
				from @resultJournalVoucher rjv
					
				IF ISNULL(@Code_Output, 999) <> 0 
				BEGIN
					SELECT 999 AS CodeResult, ISNULL(@Message_Output, 'Error al generar el comprobante contable') AS MessageResult, '' AS NotificationContract
					RETURN
				END

				--consultar tipo de documento
				SELECT @Message = @Message + CHAR(13) + CHAR(10) + 'Se generó el Comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name
				FROM GeneralLedger.JournalVoucherTypes jvt
				WHERE jvt.Id = @JournalVoucherTypeId
			END
		END

		/*******************************ANULACIÓN DE PAQUETES*************************/
		if exists (select 1 from GeneralLedger.JournalVouchers (nolock) where IdJournalVoucher = @LiquidatedPackageJournalVoucherTypeId and EntityId = @InvoiceId and EntityName = 'Invoice')
		begin
			declare @statusAnnulmentPackage bit, @messageAnnulmentPackage varchar(max)
			exec billing.[SP_CreatePackageJournalVoucherAnnulment_Output] @InvoiceId, @CodUser, @statusAnnulmentPackage output, @messageAnnulmentPackage output

			if @statusAnnulmentPackage = 0 begin
				set @Message = @Message + char(13) + char(10) + @messageAnnulmentPackage
				select 999 as CodeResult, isnull(@Message, 'Error al generar el comprobante contable') AS MessageResult, '' AS NotificationContract
				return
			end
		end
		/*************************************************** ACTUALIZACIONES ***************************************************/

		--Inactivar asociacion a la Factura
		UPDATE rcdi
			SET Status = 0, 
				ModificationDate = @AnnulmentDate, 
				ModificationUser = @CodUser 
		FROM Billing.RevenueControlDetailInvoice rcdi
		WHERE rcdi.InvoiceId = @InvoiceId

		--==Anulando la factura y liberando el folio
		UPDATE Billing.Invoice 
			SET Status = 2, 
				RevenueControlDetailId = NULL, 
				ReversalReasonId = @ReversalReasonId, 
				DescriptionReversal = @ReversalReasonDescription, 
				AnnulmentUser = @CodUser, 
				AnnulmentDate = @AnnulmentDate
		WHERE Id = @InvoiceId

		UPDATE id
			SET id.Balance = 0
		FROM Billing.InvoiceDetail id
		WHERE id.InvoiceId = @InvoiceId

		UPDATE ids
			SET ids.Balance = 0
		FROM Billing.InvoiceDetail id
		JOIN Billing.InvoiceDetailSurgical ids ON id.Id = ids.InvoiceDetailId
		WHERE id.InvoiceId = @InvoiceId

		UPDATE sod
			SET sod.IsAnnulled = 1 
		FROM Billing.InvoiceDetail id
		JOIN Billing.ServiceOrderDetail sod ON id.ServiceOrderDetailId = sod.Id
		WHERE id.InvoiceId = @InvoiceId AND sod.CUPSAssociateService = 1

		--Se anulan los registros de causación de honorarios médicos, porque antes no se tenía en cuenta cuando la factura era por capitación
		UPDATE mfc
			SET Status = IIF(mfc.Status = 1, 4, mfc.Status), 
				InvoiceReversal = IIF(mfc.Status = 1, mfc.InvoiceReversal, 1), 
				ModificationUser = @CodUser, 
				ModificationDate = @AnnulmentDate,
				AnnulmentUser = IIF(mfc.Status = 1, @CodUser, mfc.AnnulmentUser), 
				AnnulmentDate = IIF(mfc.Status = 1, @AnnulmentDate, mfc.AnnulmentDate) 
		FROM Billing.InvoiceDetail id
		JOIN MedicalFees.MedicalFeesCausation mfc ON id.Id = mfc.InvoiceDetailId
		WHERE id.InvoiceId = @InvoiceId

		--Folio asociado a la Facturas
		UPDATE rcdi
			SET Status = 0, 
				ModificationDate = @AnnulmentDate, 
				ModificationUser = @CodUser 
		FROM Billing.RevenueControlDetailInvoice rcdi
		JOIN Billing.RevenueControlDetail rcd ON rcdi.RevenueControlDetailId = rcd.Id
		WHERE rcdi.InvoiceId = @InvoiceId

		UPDATE rcd 
			SET Status = 1,
				TotalFolio = @TotalFolio+iif(rcd.IsMasterAccount=0,isnull(@DiscountApply,0),0) ,
				PatientDiscount = iif(@LiquidateMasterAccount=1,rcd.PatientDiscount,0),
				PatientDiscountPercentage = 0,
				TotalPatientWithDiscount = rcd.TotalPatientSalesPrice,
				BillingAuthorizationId = null, 
				ModificationDate = @AnnulmentDate, 
				ModificationUser = @CodUser 
		FROM Billing.RevenueControlDetail rcd
		LEFT JOIN Billing.Invoice i ON rcd.Id = i.RevenueControlDetailId
		WHERE rcd.Id = @RevenueControlDetailId AND i.Id IS NULL

		--=============================================================================================================================================================================
		
		DECLARE @IdInpacientTopAnu int = 0
		DECLARE @PreviusPAGADOCMO numeric(18,0)
		DECLARE @PreviusPAGADOCOP numeric(18,0)
		DECLARE @PreviusPAGADOCRE numeric(18,0)

		DECLARE @PAGADOCMO numeric(18,0)
		DECLARE @PAGADOCOP numeric(18,0)
		DECLARE @PAGADOCRE numeric(18,0)

		DECLARE @sqlCrystal nvarchar(400) = 'select @IdInpacientTopAnu=Id,@PreviusPAGADOCMO=PAGADOCMO,@PreviusPAGADOCOP=PAGADOCOP,@PreviusPAGADOCRE=PAGADOCRE from '+@ContainerNameCrystal+'.dbo.INPACIENTTOPANU where IPCODPACI = '''+@PatientCode+''' and ANIO = YEAR([Common].[GETDATE]())';
		exec sp_executesql @sqlCrystal, N'@IdInpacientTopAnu int output,@PreviusPAGADOCMO numeric(18,0) output,@PreviusPAGADOCOP numeric(18,0) output,@PreviusPAGADOCRE numeric(18,0) output', @IdInpacientTopAnu output,@PreviusPAGADOCMO output,@PreviusPAGADOCOP output,@PreviusPAGADOCRE output

		select @PAGADOCMO=COALESCE(sum(SubTotalPatientSalesPrice), 0) from Billing.ServiceOrderDetailDistribution where RevenueControlDetailId = @RevenueControlDetailId and ApplyRecoveryFee = 2 and RecoveryFeeType = 2
		select @PAGADOCOP=COALESCE(sum(SubTotalPatientSalesPrice), 0) from Billing.ServiceOrderDetailDistribution where RevenueControlDetailId = @RevenueControlDetailId and ApplyRecoveryFee = 2 and RecoveryFeeType = 3
		select @PAGADOCRE=COALESCE(sum(SubTotalPatientSalesPrice), 0 ) from Billing.ServiceOrderDetailDistribution where RevenueControlDetailId = @RevenueControlDetailId and ApplyRecoveryFee = 2 and RecoveryFeeType = 5
		
		IF @IdInpacientTopAnu > 0
		BEGIN
			SET @sqlCrystal = 'update '+@ContainerNameCrystal+'.dbo.INPACIENTTOPANU SET PAGADOCMO = '+CONVERT(varchar(18), (@PreviusPAGADOCMO-@PAGADOCMO))+', PAGADOCOP = '+CONVERT(varchar(18), (@PreviusPAGADOCOP-@PAGADOCOP))+', PAGADOCRE = '+CONVERT(varchar(18), (@PreviusPAGADOCRE-@PAGADOCRE))+' where Id = '+CONVERT(varchar(18), @IdInpacientTopAnu)
			exec sp_executesql @sqlCrystal
		END
		
		--=======================VERIFICAR SI NO HAY FOLIOS LIQUIDADOS PARA DEJAR EL INGRESO SIN CONFIRMAR==========================
		DECLARE @CantidadFoliosLiquidados int
		select @CantidadFoliosLiquidados=count(*) from Billing.RevenueControl rc inner join Billing.RevenueControlDetail rcd on rcd.RevenueControlId = rc.Id
		where rc.AdmissionNumber = @AdmissionNumber and rcd.Status = 2

		IF @CantidadFoliosLiquidados = 0
			SET @sqlCrystal = 'update ['+@ContainerNameCrystal+'].[dbo].[ADINGRESO] SET IESTADOIN = '' '' where NUMINGRES = '''+@AdmissionNumber+''''
		else
			SET @sqlCrystal = 'update ['+@ContainerNameCrystal+'].[dbo].[ADINGRESO] SET IESTADOIN = ''P'' where NUMINGRES = '''+@AdmissionNumber+''''

		exec sp_executesql @sqlCrystal
		
		--===========================================================================================================================
		
		SELECT 0 AS CodeResult, ISNULL(@Message, '') AS MessageResult, @messageValidationContract AS NotificationContract
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)) AS MessageResult, '' AS NotificationContract
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la anulación (reversa) de una factura de cobro emitida en el sistema de facturación. Toma el folio de facturación del detalle de control de ingresos (RevenueControlDetail), recupera el encabezado de la factura (Invoice) junto con el grupo de atención (CareGroup) y el contrato vigente (Contract) para validar valores ejecutados, controles de terminación y parámetros de liquidación. Como parte del proceso, genera el comprobante contable de reversa (incluyendo manejo de años anteriores), actualiza el estado de la factura a anulada registrando la fecha, el motivo y el usuario responsable, y puede desencadenar ajustes sobre el valor ejecutado del contrato. Aplica SQL dinámico en tiempo de ejecución para armar la contabilización según el tipo de compañía y la configuración de facturación electrónica, por lo que las entidades contables afectadas pueden variar según los parámetros recibidos en @CompanyType y @ContainerNameCrystal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_AnulateInvoice';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_AnulateInvoice';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Anula una factura activa de un folio de facturación, reversa cuentas por cobrar y cruces de anticipos, genera el comprobante contable de reversión y libera el folio para nueva facturación, sincronizando con el sistema externo Crystal.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AnulateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El folio (RevenueControlDetail) debe tener tipo de liquidación definido (LiquidationType <> 0).; Debe existir una factura activa (Invoice.Status = 1) asociada al RevenueControlDetail.; Si SettingsBilling.AnulateInvoicesPreviousPeriods = 0 y la factura no es del año/mes actual, el usuario debe tener el permiso ''756''/''89'' validado por Common.ValidatePermision.; Si el CareGroupType = 1, debe existir un contrato relacionado (ContractId <> 0).; El contrato no debe estar Suspendido (Status=2) ni Terminado (Status=3).; Para liquidación tipo 1 (Pago por servicios) y CareGroup distinto de 3 e IsMasterAccount distinto de 4, ninguna AccountReceivable de la factura puede tener PortfolioStatus <> 1 (radicada).; Las cuentas por cobrar (excepto tipo 6) deben cumplir Balance + cruces + retenciones = Value (sin movimiento).; Los pagarés asociados (AccountReceivable.AccountReceivableType=4) deben tener Value = Balance.; La factura no debe estar incluida en una InvoiceEntityCapitatedDistribution con Status=2 (distribuida).; Si la anulación es de un año posterior al de la factura, ReversalPreviousYearsMainAccountId debe estar parametrizado en SettingsBilling.; Para liquidación tipo 1, el total débito recreado debe coincidir con el total débito del comprobante original de la factura.; Si existen AccountReceivable con tipo NOT IN (4,6) y la factura ya tiene comprobante contable, no puede anularse sin generar comprobante de reversión.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AnulateInvoice';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_AnulateInvoice';
-- GO
