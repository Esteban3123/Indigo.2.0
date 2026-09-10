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
GO
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reconocimiento contable de causaciones de honorarios médicos pendientes para un proveedor (médico o agremiación) en una unidad operativa específica. Valida que el proveedor exista en Common.Supplier y Common.ThirdParty, que esté configurado el tipo de comprobante de reconocimiento de costos en la configuración de honorarios médicos, que haya un libro contable oficial activo, y que no exista ya un reconocimiento activo para ese proveedor. Consulta la vista ViewListCausationwithoutRecognition para obtener el total de causaciones pendientes y, si las hay, crea el encabezado en CausationRecognition y el detalle en CausationRecognitionDetail, generando además el comprobante contable (asiento de diario) con sus débitos y créditos correspondientes al valor total del proveedor. Se usa en el módulo de honorarios médicos para cerrar el ciclo de causación y registrar contablemente el costo de los servicios prestados por profesionales de la salud.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateCausationRecognition';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateCausationRecognition';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reconocimiento contable de las causaciones pendientes de honorarios médicos para un proveedor en una unidad operativa, creando el encabezado y detalle de reconocimiento, vinculando las causaciones y produciendo el comprobante contable balanceado.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El proveedor debe existir en Common.Supplier con su ThirdParty asociado; Debe estar configurado el tipo de comprobante de reconocimiento de costos en MedicalFees.SettingMedicalFees; Debe existir un libro oficial (OfficialBook=1) en GeneralLedger.LegalBook; No debe existir otro reconocimiento activo (State=1) para el mismo proveedor y unidad operativa; Deben existir causaciones pendientes (sin reconocer) con valor y conteo > 0 para el proveedor y unidad operativa', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se crea reconocimiento si no existe otro activo (State=1) para la combinación proveedor/unidad operativa; Sólo se incluyen en el detalle causaciones con Status=1 listadas en la vista de causaciones sin reconocer; El reconocimiento siempre se crea con State=1 (Reconocido) en este flujo; Cada causación reconocida queda vinculada al CausationRecognitionId generado; El comprobante contable sólo se persiste si está balanceado (diferencia débitos-créditos ≤ 0.01); Las líneas con DebitValue=0 y CreditValue=0 se eliminan antes de validar balance y enviar a contabilidad; El comprobante contable se asocia mediante EntityName=''CausationRecognition'' y EntityId=CausationRecognitionId; El JournalVoucherConsecutive guardado proviene exclusivamente del libro oficial (OfficialBook=1)', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] MedicalFees.CausationRecognition: Cuando hay causaciones pendientes con valor y conteo mayores a cero, se inserta el encabezado del reconocimiento con State=1 (Reconocido) y el total acumulado del proveedor; [INSERT] MedicalFees.CausationRecognitionDetail: Se insertan los detalles del reconocimiento tomando las causaciones de la vista de pendientes filtradas por proveedor y unidad operativa, sólo aquellas con Status=1 en MedicalFeesCausation, enriquecidas con datos de Billing.ServiceOrderDetail; [UPDATE] MedicalFees.MedicalFeesCausation: Tras insertar el detalle, se marca cada causación incluida con el CausationRecognitionId generado; [UPDATE] MedicalFees.CausationRecognition: Cuando el comprobante contable se crea exitosamente, se actualiza el reconocimiento con el JournalVoucherId y su Consecutive del libro oficial; [INSERT] GeneralLedger.JournalVouchers: Vía EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement con XML que contiene cabecera (Imported=0, Status=2, EntityName=''CausationRecognition'') y detalle débito/crédito generado por SP_GenerateJournalVoucherDetailsCausationRecognition; [RETURN_RESULT] RESULT: Devuelve un resultset con StatusResult, MessageResult, JournalVoucherConsecutive y CausationRecognitionId; StatusResult=0 ante validaciones fallidas o excepciones, =1 cuando no hay pendientes o el proceso fue exitoso', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El ThirdPartyId asociado al proveedor es NULL → Retorna StatusResult=0 con mensaje ''El proveedor especificado no existe'' y termina; si No existe CostRecognitionVoucherId en MedicalFees.SettingMedicalFees → Retorna StatusResult=0 indicando que no se ha configurado el tipo de comprobante de reconocimiento de costos; si No existe libro con OfficialBook=1 → Retorna StatusResult=0 indicando que no se encontró un libro oficial configurado; si Ya existe un CausationRecognition con State=1 para el mismo SupplierId y OperativeUnitId → Retorna StatusResult=0 indicando que existe un reconocimiento activo para el proveedor; si El total o el conteo de causaciones pendientes es cero → Retorna StatusResult=1 con mensaje informativo de que el proveedor no tiene causaciones pendientes y termina sin crear nada; si La inserción de detalles del reconocimiento no afectó filas (@@ROWCOUNT=0) → Retorna StatusResult=0 indicando que no se pudieron insertar los detalles; si El SP de detalles contables no produce filas en la tabla temporal → Retorna StatusResult=0 indicando que no se pudieron generar los detalles del comprobante contable; si ABS(SUM(Debitos) - SUM(Creditos)) > 0.01 → Retorna StatusResult=0 indicando que el comprobante está desbalanceado, mostrando totales; si El SP de creación de comprobante retorna alguna fila con CodeMessage <> ''0'' → Retorna StatusResult=0 con el mensaje de error del comprobante (se espera ROLLBACK externo del TransactionScope); si Ocurre una excepción en el TRY → El CATCH establece StatusResult=0 y construye un mensaje con ERROR_MESSAGE() y ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'MedicalFees.SP_GenerateJournalVoucherDetailsCausationRecognition; GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCausationRecognition';
-- GO
