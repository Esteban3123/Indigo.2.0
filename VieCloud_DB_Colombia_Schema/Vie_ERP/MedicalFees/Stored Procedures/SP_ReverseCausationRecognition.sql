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
	
	BEGIN TRY			
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

		-- ============================================
		-- PASO 1: Validaciones iniciales
		-- ============================================
		
		-- Obtener el usuario
		SELECT @UserId = Id FROM [Security].[User] WHERE UserCode = @UserCode

		-- Verificar que el reconocimiento exista y esté activo
		IF NOT EXISTS(
			SELECT 1 
			FROM MedicalFees.CausationRecognition 
			WHERE Id = @CausationRecognitionId 
			  AND State = 1
		)
		BEGIN
			SELECT 
				CONVERT(BIT, 0) AS StatusResult, 
				'El reconocimiento de causación no existe o ya fue reversado.' AS MessageResult, 
				'' AS JournalVoucherConsecutive, 
				'' AS JournalVoucherCodeName, 
				0 AS CausationRecognitionId
			RETURN
		END
		
		-- Obtener información del reconocimiento
		SELECT 
			@SupplierId = SupplierId,
			@ThirdPartyId = ThirdPartyId,
			@OperatingUnitId = OperativeUnitId,
			@TotalSupplierValue = TotalSupplier,
			@OriginalJournalVoucherId = JournalVoucherId,
			@OriginalJournalVoucherConsecutive = JournalVoucherConsecutive,
			@RecognitionDate = VoucherDate
		FROM MedicalFees.CausationRecognition
		WHERE Id = @CausationRecognitionId
		
		-- Obtener nombre del proveedor
		SELECT 
			@SupplierCode = Code,
			@SupplierName = CONCAT(Code, ' - ', [Name])
		FROM Common.Supplier
		WHERE Id = @SupplierId
		
		-- Contar servicios incluidos
		SELECT @ServiceCount = COUNT(*)
		FROM MedicalFees.CausationRecognitionDetail
		WHERE CausationRecognitionId = @CausationRecognitionId

		-- TODO: Validar que el reconocimiento no tenga liquidación asociada
		-- IF EXISTS (
		--     SELECT 1 
		--     FROM MedicalFees.MedicalFeesLiquidation mfl
		--     INNER JOIN MedicalFees.MedicalFeesLiquidationDetail mfld ON mfl.Id = mfld.MedicalFeesLiquidationId
		--     INNER JOIN MedicalFees.CausationRecognitionDetail crd ON mfld.MedicalFeesCausationId = crd.MedicalFeesCausationId
		--     WHERE crd.CausationRecognitionId = @CausationRecognitionId
		-- )
		-- BEGIN
		--     SELECT 
		--         CONVERT(BIT, 0) AS StatusResult, 
		--         'No se puede reversar el reconocimiento porque tiene liquidación asociada.' AS MessageResult, 
		--         '' AS JournalVoucherConsecutive, 
		--         '' AS JournalVoucherCodeName, 
		--         0 AS CausationRecognitionId
		--     RETURN
		-- END

		-- Obtener el tipo de comprobante de reversión desde la configuración
		SELECT @CostRecognitionReversalVoucherId = CostRecognitionReversalVoucherId 
		FROM MedicalFees.SettingMedicalFees
		
		IF @CostRecognitionReversalVoucherId IS NULL
		BEGIN
			SELECT 
				CONVERT(BIT, 0) AS StatusResult, 
				'No se ha configurado el tipo de comprobante de reversión de reconocimiento de costos en la configuración de Honorarios Médicos' AS MessageResult, 
				'' AS JournalVoucherConsecutive, 
				'' AS JournalVoucherCodeName, 
				0 AS CausationRecognitionId
			RETURN
		END
		
		-- Obtener el libro oficial
		SELECT @LegalBookId = Id FROM GeneralLedger.LegalBook WHERE OfficialBook = 1

		-- ============================================
		-- PASO 2: Generar el comprobante contable de reversión
		-- ============================================
		
		-- Insertar la cabecera del comprobante de reversión
		INSERT INTO @TableJournalVoucher 
		VALUES (
			@CostRecognitionReversalVoucherId, 
			@LegalBookId, 
			[Common].[GETDATE](), -- Fecha actual para la reversión
			0, -- No importado
			2, -- Estado: Contabilizado
			CONCAT('Reversión Reconocimiento Causación Proveedor ', @SupplierCode, ' - ', @SupplierName), 
			@SupplierCode, 
			@CausationRecognitionId, 
			'CausationRecognitionReverse', 
			0 -- Año no cerrado
		)

		-- Generar los detalles del comprobante de reversión usando el SP dedicado
		-- Este SP genera los asientos inversos basándose en los datos del reconocimiento original
		INSERT INTO @tmpTableDetail
		EXEC [MedicalFees].[SP_GenerateJournalVoucherDetailsCausationRecognitionReverse] 
			@CausationRecognitionId, 
			@OperatingUnitId

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
		
		-- Generar el XML para la interfaz con contabilidad
		DECLARE @JournalXml XML = (
			SELECT *
			FROM @TableJournalVoucher AS JournalVoucher
			CROSS APPLY @TableJournalVoucherDetail AS JournalVoucherDetail
			FOR XML AUTO, ELEMENTS
		)

		DECLARE @TableResultJournal TABLE(
			CodeMessage VARCHAR(20), 
			[Message] VARCHAR(MAX), 
			IdJournalVoucher INT
		)
		
		-- Llamar al procedimiento de generación de comprobantes contables
		INSERT INTO @TableResultJournal
		EXEC [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement] @JournalXml, @UserCode
					
		-- Validar el resultado de la generación contable
		IF (SELECT COUNT(*) FROM @TableResultJournal WHERE CodeMessage <> '0') > 0 
		BEGIN
			SELECT 
				CONVERT(BIT, 0) AS StatusResult, 
				CONCAT('Proveedor ', @SupplierCode, ' - ', @SupplierName, ': Error al reversar - ', [Message]) AS MessageResult, 
				'' AS JournalVoucherConsecutive, 
				'' AS JournalVoucherCodeName, 
				0 AS CausationRecognitionId 
			FROM @TableResultJournal
			RETURN
		END

		-- ============================================
		-- PASO 3: Actualizar el reconocimiento con los datos de la reversión
		-- ============================================
		
		-- Obtener el Id y consecutivo del comprobante de reversión generado
		SELECT	
			@JournalVoucherId = jv.Id,
			@JournalVoucherConsecutive = Consecutive
		FROM [GeneralLedger].[JournalVouchers] jv
		INNER JOIN GeneralLedger.LegalBook lb ON lb.Id = jv.LegalBookId
		WHERE jv.EntityId = @CausationRecognitionId 
		  AND jv.EntityName = 'CausationRecognitionReverse' 
		  AND lb.OfficialBook = 1

		-- Actualizar el registro de reconocimiento como reversado
		UPDATE MedicalFees.CausationRecognition
		SET State = 2,  -- Estado: Reversado
			JournalVoucherReverseId = @JournalVoucherId,
			JournalVoucherTypeReverseId = @CostRecognitionReversalVoucherId,
			JournalVoucherReverseConsecutive = @JournalVoucherConsecutive,
			VoucherDateReverse = [Common].[GETDATE]()
		WHERE Id = @CausationRecognitionId		

		-- ============================================
		-- PASO 4: Limpiar las causaciones (volver a estado "no reconocidas")
		-- ============================================
		UPDATE MedicalFees.MedicalFeesCausation
		SET CausationRecognitionId = NULL
		WHERE Id IN (
		    SELECT MedicalFeesCausationId 
		    FROM MedicalFees.CausationRecognitionDetail 
		    WHERE CausationRecognitionId = @CausationRecognitionId
		)

		-- Construir mensaje de éxito
		SELECT @MessageReturn += CONCAT(
			lb.Code, ' - ', lb.Name,
			': Se generó el comprobante de reversión de tipo ', jvt.Code, ' - ', jvt.Name,
			' para el proveedor ', @SupplierName,
			' (', @ServiceCount, ' servicios reversados)'
		)
		FROM GeneralLedger.LegalBook lb
		JOIN GeneralLedger.JournalVoucherTypes jvt ON jvt.Id = @CostRecognitionReversalVoucherId
		WHERE lb.Id = @LegalBookId
			
		-- Retornar resultado exitoso
		SELECT 
			CONVERT(BIT, 1) AS StatusResult, 
			@MessageReturn AS MessageResult, 
			CAST(@JournalVoucherConsecutive AS VARCHAR(20)) AS JournalVoucherConsecutive, 
			'' AS JournalVoucherCodeName, 
			@CausationRecognitionId AS CausationRecognitionId
			
	END TRY
	BEGIN CATCH
		-- Manejo de errores
		SELECT 
			CONVERT(BIT, 0) AS StatusResult, 
			CONCAT('Error al reversar reconocimiento: ', ERROR_MESSAGE()) AS MessageResult, 
			'' AS JournalVoucherConsecutive, 
			'' AS JournalVoucherCodeName, 
			0 AS CausationRecognitionId
	END CATCH	
END
GO
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reversa un reconocimiento de causación de honorarios médicos previamente contabilizado. El procedimiento valida que el reconocimiento exista y esté en estado activo (no reversado), obtiene el comprobante contable original asociado y genera un comprobante de reversión en el libro oficial contable, invirtiendo los débitos y créditos del reconocimiento original del proveedor (médico o agremiación). Actualiza el estado del reconocimiento en la tabla CausationRecognition, marca el detalle de causaciones en CausationRecognitionDetail como reversadas y registra el nuevo comprobante en el libro mayor (GeneralLedger), dejando trazabilidad completa del usuario que ejecutó la reversión y el consecutivo del comprobante generado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseCausationRecognition';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseCausationRecognition';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa un reconocimiento de causación de honorarios médicos generando un comprobante contable inverso, marcando el reconocimiento como reversado y liberando las causaciones asociadas para que vuelvan a estar disponibles.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El reconocimiento de causación debe existir y tener State = 1 (activo/no reversado).; Debe existir configuración del tipo de comprobante de reversión (CostRecognitionReversalVoucherId) en SettingMedicalFees.; Debe existir un libro legal oficial (OfficialBook = 1) en GeneralLedger.LegalBook.; El usuario identificado por UserCode debe existir en Security.User.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reversan reconocimientos en estado activo (State=1); un reconocimiento ya reversado no puede reversarse nuevamente.; El comprobante de reversión siempre se asocia al libro oficial (OfficialBook=1) y se identifica con EntityName=''CausationRecognitionReverse''.; Las líneas de detalle contable con DebitValue=0 y CreditValue=0 nunca se envían al comprobante.; La actualización del reconocimiento y la liberación de causaciones solo ocurren si la creación contable fue exitosa.; Al reversar, las causaciones médicas asociadas siempre quedan con CausationRecognitionId=NULL, volviendo a estar disponibles para nuevo reconocimiento.; La fecha de reversión se toma de Common.GETDATE() (fecha actual del sistema), no de la fecha del reconocimiento original.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reconocimiento de causación de honorarios médicos; Reversión contable; Comprobante contable (Journal Voucher); Proveedor (médico/profesional); Causación médica; Libro oficial contable; Tercero; Centro de costo; Unidad operativa', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] MedicalFees.CausationRecognition: Tras generar exitosamente el comprobante contable de reversión, marca el reconocimiento con State=2 (Reversado), guarda JournalVoucherReverseId, JournalVoucherTypeReverseId, JournalVoucherReverseConsecutive y VoucherDateReverse=GETDATE().; [UPDATE] MedicalFees.MedicalFeesCausation: Después de reversar, libera todas las causaciones cuyo Id esté en CausationRecognitionDetail del reconocimiento, poniendo CausationRecognitionId = NULL para que vuelvan a estado ''no reconocidas''.; [INSERT] GeneralLedger.JournalVouchers: Vía SP_CreateAndValidateJournalVoucherMovement se crea un comprobante contable de reversión con EntityName=''CausationRecognitionReverse'', EntityId=CausationRecognitionId, estado 2 (Contabilizado), fecha actual y detalles inversos generados por SP_GenerateJournalVoucherDetailsCausationRecognitionReverse.; [RETURN_RESULT] RESULT_SET: Devuelve un conjunto con StatusResult (BIT), MessageResult, JournalVoucherConsecutive, JournalVoucherCodeName y CausationRecognitionId; en error o validación fallida retorna StatusResult=0 con mensaje específico.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe reconocimiento con Id dado y State=1 → Retorna StatusResult=0 con mensaje ''El reconocimiento de causación no existe o ya fue reversado'' y termina.; si CostRecognitionReversalVoucherId IS NULL en SettingMedicalFees → Retorna StatusResult=0 indicando que no se ha configurado el tipo de comprobante de reversión y termina.; si El SP de creación de comprobante retorna alguna fila con CodeMessage <> ''0'' → Retorna StatusResult=0 con el mensaje de error del proceso contable y no actualiza el reconocimiento ni libera las causaciones.; si Se captura excepción en CATCH → Retorna StatusResult=0 con el ERROR_MESSAGE() prefijado por ''Error al reversar reconocimiento:''.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'MedicalFees.SP_GenerateJournalVoucherDetailsCausationRecognitionReverse; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.User; MedicalFees.CausationRecognition; Common.Supplier; MedicalFees.CausationRecognitionDetail; MedicalFees.SettingMedicalFees; GeneralLedger.LegalBook; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCausationRecognition';
-- GO
