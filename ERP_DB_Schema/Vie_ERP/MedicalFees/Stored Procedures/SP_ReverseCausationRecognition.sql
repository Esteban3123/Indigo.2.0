-- =============================================
-- Author:		Andres Alarcon
-- CREATE date: 2025-10-16
-- Description:	Reversa un Reconocimiento de Causación de Honorarios Médicos
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_ReverseCausationRecognition]
	@CausationRecognitionId AS INT,
	@UserCode AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;
	
	-- Variables necesarias
	DECLARE @CostRecognitionReversalVoucherId INT
	DECLARE @SupplierName VARCHAR(200) = ''
	DECLARE @SupplierCode VARCHAR(20) = ''
	DECLARE @SupplierId INT
	DECLARE @ThirdPartyId INT
	DECLARE @OperatingUnitId INT
	DECLARE @MessageReturn VARCHAR(MAX) = ''
	DECLARE @LegalBookId INT
	DECLARE @JournalVoucherId INT
	DECLARE @JournalVoucherConsecutive BIGINT
	DECLARE @OriginalJournalVoucherId INT
	DECLARE @OriginalJournalVoucherConsecutive BIGINT
	DECLARE @UserId INT
	DECLARE @TotalSupplierValue DECIMAL(18,2) = 0
	DECLARE @ServiceCount INT = 0
	DECLARE @RecognitionDate DATETIME
	DECLARE @RecognitionState TINYINT
	
	-- Variables para manejo de resultado
	DECLARE @StatusResult BIT = 0
	DECLARE @MessageResult VARCHAR(MAX) = ''
	
	-- Tablas temporales
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
		-- PASO 1: Validar que el reconocimiento existe
		-- ============================================
		
		SELECT 
			@SupplierId = SupplierId,
			@ThirdPartyId = ThirdPartyId,
			@OperatingUnitId = OperativeUnitId,
			@TotalSupplierValue = TotalSupplier,
			@RecognitionDate = VoucherDate,
			@RecognitionState = State
		FROM MedicalFees.CausationRecognition
		WHERE Id = @CausationRecognitionId
		
		IF @SupplierId IS NULL
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = 'El reconocimiento de causación especificado no existe'
			GOTO FIN;
		END
		
		IF @RecognitionState <> 1
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = 'El reconocimiento de causación ya fue reversado anteriormente'
			GOTO FIN;
		END

		SELECT
			@OriginalJournalVoucherConsecutive = jv.Consecutive,
			@OriginalJournalVoucherId = jv.id
		FROM GeneralLedger.JournalVouchers jv
		JOIN MedicalFees.CausationRecognition cr ON cr.Id = jv.EntityId AND jv.EntityName = 'CausationRecognition'
		WHERE cr.Id = @CausationRecognitionId
		
		IF @OriginalJournalVoucherId IS NULL
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = 'El reconocimiento no tiene un comprobante contable asociado para reversar'
			GOTO FIN;
		END

		SELECT 
			@SupplierCode = Code,
			@SupplierName = Name
		FROM Common.Supplier
		WHERE Id = @SupplierId
		
		IF @SupplierCode IS NULL
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = 'No se pudo obtener la información del proveedor'
			GOTO FIN;
		END
		
		SELECT @ServiceCount = COUNT(*)
		FROM MedicalFees.CausationRecognitionDetail
		WHERE CausationRecognitionId = @CausationRecognitionId
		
		IF @ServiceCount = 0
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = CONCAT('Proveedor ', @SupplierCode, ' - ', @SupplierName, ': El reconocimiento no tiene detalles para reversar')
			GOTO FIN;
		END

		SELECT @UserId = Id FROM [Security].[User] WHERE UserCode = @UserCode

		-- ============================================
		-- PASO 2: Validar configuración
		-- ============================================
		
		SELECT @CostRecognitionReversalVoucherId = CostRecognitionReversalVoucherId 
		FROM MedicalFees.SettingMedicalFees
		
		IF @CostRecognitionReversalVoucherId IS NULL
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = 'No se ha configurado el tipo de comprobante de reversión de reconocimiento de costos en la configuración de Honorarios Médicos'
			GOTO FIN;
		END
		
		SELECT @LegalBookId = Id FROM GeneralLedger.LegalBook WHERE OfficialBook = 1

		IF @LegalBookId IS NULL
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = 'No se encontró un libro oficial configurado'
			GOTO FIN;
		END

		-- ============================================
		-- PASO 3: Generar el comprobante contable de reversión
		-- ============================================
		
		INSERT INTO @TableJournalVoucher 
		VALUES (
			@CostRecognitionReversalVoucherId, 
			@LegalBookId, 
			[Common].[GETDATE](),
			0,
			2,
			CONCAT('Reversión Reconocimiento Causación Proveedor ', @SupplierCode, ' - ', @SupplierName), 
			@SupplierCode, 
			@CausationRecognitionId, 
			'CausationRecognitionReverse', 
			0
		)

		INSERT INTO @tmpTableDetail
		EXEC [MedicalFees].[SP_GenerateJournalVoucherDetailsCausationRecognitionReverse] 
			@CausationRecognitionId

		IF NOT EXISTS (SELECT 1 FROM @tmpTableDetail)
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = CONCAT('Proveedor ', @SupplierCode, ' - ', @SupplierName, ': No se pudieron generar los detalles del comprobante de reversión')
			GOTO FIN;
		END

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

		DELETE FROM @TableJournalVoucherDetail WHERE DebitValue = 0 AND CreditValue = 0
		
		-- Validar balance
		DECLARE @TotalDebits DECIMAL(18,2) = (SELECT SUM(DebitValue) FROM @TableJournalVoucherDetail)
		DECLARE @TotalCredits DECIMAL(18,2) = (SELECT SUM(CreditValue) FROM @TableJournalVoucherDetail)
		DECLARE @Difference DECIMAL(18,2) = @TotalDebits - @TotalCredits

		IF ABS(@Difference) > 0.01
		BEGIN
			SET @StatusResult = 0
			SET @MessageResult = CONCAT(
				'Proveedor ', @SupplierCode, ' - ', @SupplierName, 
				': El comprobante de reversión está desbalanceado. ',
				'Total Débitos: ', CAST(@TotalDebits AS VARCHAR), ', ',
				'Total Créditos: ', CAST(@TotalCredits AS VARCHAR)
			)
			GOTO FIN;
		END
		
		DECLARE @JournalXml XML = (
			SELECT *
			FROM @TableJournalVoucher AS JournalVoucher
			CROSS APPLY @TableJournalVoucherDetail AS JournalVoucherDetail
			FOR XML AUTO, ELEMENTS
		)
		
		-- ============================================
		-- PASO 4: Llamar al SP de creación de comprobante contable
		-- ============================================
		INSERT INTO @TableResultJournal
		EXEC [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement] @JournalXml, @UserCode
					
		IF EXISTS (SELECT 1 FROM @TableResultJournal WHERE CodeMessage <> '0')
		BEGIN
			SELECT @MessageResult = CONCAT('Proveedor ', @SupplierCode, ' - ', @SupplierName, ': Error al reversar - ', [Message])
			FROM @TableResultJournal
			WHERE CodeMessage <> '0'

			SET @StatusResult = 0
			GOTO FIN; 
		END

		SELECT @MessageReturn = STRING_AGG([Message], '; ')
		FROM @TableResultJournal

		-- ============================================
		-- PASO 5: Actualizar el reconocimiento con los datos de la reversión
		-- ============================================
		
		SELECT	
			@JournalVoucherId = jv.Id,
			@JournalVoucherConsecutive = Consecutive
		FROM [GeneralLedger].[JournalVouchers] jv
		INNER JOIN GeneralLedger.LegalBook lb ON lb.Id = jv.LegalBookId
		WHERE jv.EntityId = @CausationRecognitionId 
		  AND jv.EntityName = 'CausationRecognitionReverse' 
		  AND lb.OfficialBook = 1

		UPDATE MedicalFees.CausationRecognition
		SET State = 2,
			JournalVoucherReverseId = @JournalVoucherId,
			JournalVoucherTypeReverseId = @CostRecognitionReversalVoucherId,
			JournalVoucherReverseConsecutive = @JournalVoucherConsecutive,
			VoucherDateReverse = [Common].[GETDATE]()
		WHERE Id = @CausationRecognitionId		

		-- ============================================
		-- PASO 6: Limpiar las causaciones
		-- ============================================
		UPDATE MedicalFees.MedicalFeesCausation
		SET CausationRecognitionId = NULL
		WHERE Id IN (
		    SELECT MedicalFeesCausationId 
		    FROM MedicalFees.CausationRecognitionDetail 
		    WHERE CausationRecognitionId = @CausationRecognitionId
		)

		SELECT @MessageReturn += CONCAT(
			' | ', lb.Code, ' - ', lb.Name, 
			': Comprobante de reversión ', jv.Consecutive, 
			' de tipo ', jvt.Code, ' - ', jvt.Name,
			' para el proveedor ', @SupplierName,
			' (Comprobante original: ', @OriginalJournalVoucherConsecutive, 
			', ', @ServiceCount, ' servicios reversados)'
		)
		FROM [GeneralLedger].[JournalVouchers] jv
		JOIN GeneralLedger.LegalBook lb ON jv.LegalBookId = lb.Id
		JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.Id
		WHERE jv.EntityId = @CausationRecognitionId 
		  AND jv.EntityName = 'CausationRecognitionReverse'
		
		SET @StatusResult = 1
		SET @MessageResult = @MessageReturn
		
	END TRY
	BEGIN CATCH
		
		SET @StatusResult = 0
		SET @MessageResult = CONCAT(
			'Proveedor ', ISNULL(@SupplierCode, ''), ' - ', ISNULL(@SupplierName, ''), 
			': Error crítico al reversar: ', ERROR_MESSAGE(), 
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
GO