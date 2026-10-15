-- =============================================
-- Author:		Andres Alarcon
-- CREATE date: 2025-10-16
-- Description:	Genera Reconocimiento de Causaciones de Honorarios Médicos por Proveedor
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_GenerateCausationRecognition]
	@SupplierId AS INT,             
	@OperatingUnitId INT,
	@RecognitionDate AS DATETIME,
	@UserCode AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;
	
	-- Variables necesarias
	DECLARE @CostRecognitionVoucherId INT
	DECLARE @SupplierName VARCHAR(200) = ''
	DECLARE @SupplierCode VARCHAR(20) = ''
	DECLARE @ThirdPartyId INT
	DECLARE @MessageReturn VARCHAR(MAX) = ''
	DECLARE @LegalBookId INT
	DECLARE @JournalVoucherId INT
	DECLARE @JournalVoucherConsecutive BIGINT
	DECLARE @CausationRecognitionId INT
	DECLARE @TotalSupplierValue DECIMAL(18,2) = 0
	DECLARE @ServiceCount INT = 0
	
	-- Variables para manejo de resultado
	DECLARE @StatusResult BIT = 0
	DECLARE @MessageResult VARCHAR(MAX) = ''
	
	-- Tablas temporales para el comprobante contable
	DECLARE @tmpTableDetail AS TABLE (
		MainAccountId INT, 
		ThirdPartyId INT, 
		CostCenterId INT, 
		DebitValue NUMERIC(18,2), 
		CreditValue NUMERIC(18,2), 
		Detail VARCHAR(300)
	)
	
	DECLARE @TableJournalVoucher TABLE(
		IdJournalVoucher INT NOT NULL, 
		LegalBookId INT NOT NULL, 
		VoucherDate DATETIME NOT NULL, 
		Imported BIT NOT NULL, 
		[Status] TINYINT NOT NULL, 
		Detail VARCHAR(500) NULL, 
		EntityCode VARCHAR(20) NULL, 
		EntityId INT NULL, 
		EntityName VARCHAR(250) NULL, 
		IsClosedYear BIT NOT NULL
	)
	
	DECLARE @TableJournalVoucherDetail TABLE(
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

	DECLARE @TableResultJournal TABLE(
		CodeMessage VARCHAR(20), 
		[Message] VARCHAR(MAX), 
		IdJournalVoucher INT
	)
	
	BEGIN TRY
		
		-- ============================================
		-- PASO 1: Obtener información del proveedor y validaciones iniciales
		-- ============================================
		
		SELECT TOP 1 
			@SupplierName = s.Name,
			@SupplierCode = s.Code,
			@ThirdPartyId = t.Id
		FROM Common.Supplier s
		JOIN Common.ThirdParty t ON t.Id = s.IdThirdParty
		WHERE s.Id = @SupplierId 

		-- Validar que el proveedor existe
		IF @ThirdPartyId IS NULL
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = 'El proveedor especificado no existe'
			GOTO FIN;
		END

		-- Obtener el tipo de comprobante de reconocimiento de costos
		SELECT @CostRecognitionVoucherId = CostRecognitionVoucherId 
		FROM MedicalFees.SettingMedicalFees
		
		IF @CostRecognitionVoucherId IS NULL
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = 'No se ha configurado el tipo de comprobante de reconocimiento de costos en la configuración de Honorarios Médicos'
			GOTO FIN;
		END
		
		-- Obtener el libro oficial
		SELECT @LegalBookId = Id FROM GeneralLedger.LegalBook WHERE OfficialBook = 1

		IF @LegalBookId IS NULL
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = 'No se encontró un libro oficial configurado'
			GOTO FIN;
		END

		-- ============================================
		-- PASO 2: Verificar que no existan reconocimientos activos
		-- ============================================
		IF EXISTS(
			SELECT 1 
			FROM MedicalFees.CausationRecognition 
			WHERE SupplierId = @SupplierId 
			  AND OperativeUnitId = @OperatingUnitId
			  AND State = 1
		)
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = CONCAT('Existe un reconocimiento de causación activo para el proveedor: ', @SupplierCode, ' - ', @SupplierName)
			GOTO FIN;
		END

		-- ============================================
		-- PASO 3: Validar que existan causaciones pendientes
		-- ============================================
		SELECT 
			@TotalSupplierValue = SUM(CausationValue),
			@ServiceCount = COUNT(*)
		FROM MedicalFees.ViewListCausationwithoutRecognition
		WHERE SupplierId = @SupplierId 
		  AND OperatingUnitId = @OperatingUnitId
		
		IF @TotalSupplierValue = 0 OR @ServiceCount = 0
		BEGIN
			-- No es error, simplemente no hay nada que procesar
			SET @StatusResult = 1
			SET @MessageResult = CONCAT('El proveedor ', @SupplierCode, ' - ', @SupplierName, ' no tiene causaciones pendientes de reconocer.')
			GOTO FIN;
		END

		-- ============================================
		-- PASO 4: Crear el registro de reconocimiento de causación
		-- ============================================
		INSERT INTO MedicalFees.CausationRecognition (
			SupplierId, 
			ThirdPartyId,
			JournalVoucherTypeId, 
			OperativeUnitId, 
			TotalSupplier, 
			VoucherDate, 
			State, 
			CreationUser, 
			CreationDate
		)
		VALUES(
			@SupplierId, 
			@ThirdPartyId,
			@CostRecognitionVoucherId, 
			@OperatingUnitId, 
			@TotalSupplierValue, 
			@RecognitionDate, 
			1, -- Estado: Reconocido
			@UserCode, 
			[Common].[GETDATE]()
		)

		SET @CausationRecognitionId = SCOPE_IDENTITY()

		-- ============================================
		-- PASO 5: Insertar el detalle del reconocimiento
		-- ============================================
		INSERT INTO MedicalFees.CausationRecognitionDetail (
			CausationRecognitionId,
			MedicalFeesCausationId,
			HealthProfessionalCode,
			ThirdPartyId,
			MedicalFeesContractId,
			PerformsFunctionalUnitId,
			CostCenterId,
			InvoiceQuantity,
			AmountPayable,
			TotalAmountPayable,
			AdmissionNumber
		)
		SELECT	
			@CausationRecognitionId,
			mfc.Id,
			mfc.HealthProfessionalCode,
			mfc.ThirdPartyId,
			mfc.MedicalFeesContractId,
			sod.PerformsFunctionalUnitId,
			sod.CostCenterId,
			mfc.InvoiceQuantity,
			mfc.AmountPayable,
			mfc.TotalAmountPayable,
			mfc.AdmissionNumber
		FROM MedicalFees.MedicalFeesCausation mfc
		INNER JOIN Billing.ServiceOrderDetail sod ON mfc.ServiceOrderDetailId = sod.Id
		WHERE mfc.Id IN (
			SELECT Id 
			FROM MedicalFees.ViewListCausationwithoutRecognition 
			WHERE SupplierId = @SupplierId 
			  AND OperatingUnitId = @OperatingUnitId
		)
		AND mfc.Status = 1

		-- Validar que se insertaron detalles
		IF @@ROWCOUNT = 0
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = CONCAT('Proveedor ', @SupplierCode, ' - ', @SupplierName, ': No se pudieron insertar los detalles del reconocimiento')
			GOTO FIN;
		END

		-- ============================================
		-- PASO 6: Actualizar las causaciones con el ID del reconocimiento
		-- ============================================
		UPDATE MedicalFees.MedicalFeesCausation
		SET CausationRecognitionId = @CausationRecognitionId
		WHERE Id IN (
			SELECT MedicalFeesCausationId 
			FROM MedicalFees.CausationRecognitionDetail 
			WHERE CausationRecognitionId = @CausationRecognitionId
		)

		-- ============================================
		-- PASO 7: Generar el comprobante contable
		-- ============================================
		
		-- Insertar la cabecera del comprobante
		INSERT INTO @TableJournalVoucher 
		VALUES (
			@CostRecognitionVoucherId, 
			@LegalBookId, 
			@RecognitionDate, 
			0,
			2,
			CONCAT('Reconocimiento de Causación Proveedor ', @SupplierCode, ' - ', @SupplierName), 
			@SupplierCode, 
			@CausationRecognitionId, 
			'CausationRecognition', 
			0
		)

		-- Generar los detalles del comprobante contable
		INSERT INTO @tmpTableDetail
		EXEC [MedicalFees].[SP_GenerateJournalVoucherDetailsCausationRecognition] 
			@CausationRecognitionId

		-- Validar que se generaron detalles
		IF NOT EXISTS (SELECT 1 FROM @tmpTableDetail)
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = CONCAT('Proveedor ', @SupplierCode, ' - ', @SupplierName, ': No se pudieron generar los detalles del comprobante contable')
			GOTO FIN;
		END

		-- Insertar los detalles del comprobante
		INSERT INTO @TableJournalVoucherDetail
		SELECT 
			tdt.MainAccountId, 
			tdt.ThirdPartyId, 
			tdt.CostCenterId, 
			tdt.DebitValue, 
			tdt.CreditValue, 
			tdt.Detail, 
			NULL, NULL, NULL, NULL
		FROM @tmpTableDetail tdt

		-- Eliminar registros sin valor
		DELETE FROM @TableJournalVoucherDetail WHERE DebitValue = 0 AND CreditValue = 0
		
		-- Validar balance del comprobante
		DECLARE @TotalDebits DECIMAL(18,2) = (SELECT SUM(DebitValue) FROM @TableJournalVoucherDetail)
		DECLARE @TotalCredits DECIMAL(18,2) = (SELECT SUM(CreditValue) FROM @TableJournalVoucherDetail)
		DECLARE @Difference DECIMAL(18,2) = @TotalDebits - @TotalCredits

		IF ABS(@Difference) > 0.01
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = CONCAT(
				'Proveedor ', @SupplierCode, ' - ', @SupplierName, 
				': El comprobante está desbalanceado. ',
				'Total Débitos: ', CAST(@TotalDebits AS VARCHAR), ', ',
				'Total Créditos: ', CAST(@TotalCredits AS VARCHAR)
			)
			GOTO FIN;
		END
		
		-- Generar el XML para la interfaz con contabilidad
		DECLARE @JournalXml XML = (
			SELECT *
			FROM @TableJournalVoucher AS JournalVoucher
			CROSS APPLY @TableJournalVoucherDetail AS JournalVoucherDetail
			FOR XML AUTO, ELEMENTS
		)
		
		-- ============================================
		-- PASO 8: Llamar al SP de creación de comprobante contable
		-- ============================================
		INSERT INTO @TableResultJournal
		EXEC [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement] @JournalXml, @UserCode
					
		-- VALIDACIÓN CRÍTICA: Si falla la creación del comprobante
		IF EXISTS (SELECT 1 FROM @TableResultJournal WHERE CodeMessage <> '0')
		BEGIN
			SELECT @MessageResult = CONCAT('Proveedor ', @SupplierCode, ' - ', @SupplierName, ': ', [Message])
			FROM @TableResultJournal
			WHERE CodeMessage <> '0'

			SET @StatusResult = 0
			GOTO FIN; -- El TransactionScope de C# hará el ROLLBACK
		END

		-- Obtener mensaje de éxito del comprobante
		SELECT @MessageReturn = STRING_AGG([Message], '; ')
		FROM @TableResultJournal

		-- ============================================
		-- PASO 9: Actualizar el reconocimiento con los datos del comprobante
		-- ============================================
		
		SELECT	
			@JournalVoucherId = jv.Id,
			@JournalVoucherConsecutive = Consecutive
		FROM [GeneralLedger].[JournalVouchers] jv
		INNER JOIN GeneralLedger.LegalBook lb ON lb.Id = jv.LegalBookId
		WHERE jv.EntityId = @CausationRecognitionId 
		  AND jv.EntityName = 'CausationRecognition' 
		  AND lb.OfficialBook = 1

		UPDATE MedicalFees.CausationRecognition
		SET JournalVoucherId = @JournalVoucherId,
			JournalVoucherConsecutive = @JournalVoucherConsecutive
		WHERE Id = @CausationRecognitionId		

		-- Construir mensaje de éxito completo
		SELECT @MessageReturn += CONCAT(
			' | ', lb.Code, ' - ', lb.Name, 
			': Comprobante ', jv.Consecutive, 
			' de tipo ', jvt.Code, ' - ', jvt.Name,
			' para el proveedor ', @SupplierName,
			' (', @ServiceCount, ' servicios, Total: $', CONVERT(VARCHAR, @TotalSupplierValue), ')'
		)
		FROM [GeneralLedger].[JournalVouchers] jv
		JOIN GeneralLedger.LegalBook lb ON jv.LegalBookId = lb.Id
		JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.Id
		WHERE jv.EntityId = @CausationRecognitionId 
		  AND jv.EntityName = 'CausationRecognition'
		
		-- Todo exitoso
		SET @StatusResult = 1
		SET @MessageResult = @MessageReturn
		
	END TRY
	BEGIN CATCH
		
		SET @StatusResult = 0
		SET @MessageResult = CONCAT(
			'Proveedor ', ISNULL(@SupplierCode, ''), ' - ', ISNULL(@SupplierName, ''), 
			': Error crítico: ', ERROR_MESSAGE(), 
			' (Línea: ', CAST(ERROR_LINE() AS VARCHAR), ')'
		)
		
	END CATCH

FIN:

	SELECT 
		@StatusResult AS StatusResult, 
		@MessageResult AS MessageResult, 
		ISNULL(CAST(@JournalVoucherConsecutive AS VARCHAR(20)), '') AS JournalVoucherConsecutive, 
		'' AS JournalVoucherCodeName, 
		ISNULL(@CausationRecognitionId, 0) AS CausationRecognitionId
	
END