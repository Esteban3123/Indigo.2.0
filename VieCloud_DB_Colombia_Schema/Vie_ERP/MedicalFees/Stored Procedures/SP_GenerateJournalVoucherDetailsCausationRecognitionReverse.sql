-- =============================================
-- Author:		Andres Alarcon
-- Create date: 2025-10-16
-- Description:	Genera los detalles del comprobante contable de REVERSIÓN para el reconocimiento de causaciones
--              Invierte los asientos: Débitos → Créditos, Créditos → Débitos
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_GenerateJournalVoucherDetailsCausationRecognitionReverse]
	@CausationRecognitionId INT,
	@OperativeUnitId INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @TableDetail AS TABLE 
	(
		MainAccountId INT, 
		ThirdPartyId INT, 
		CostCenterId INT, 
		DebitValue DECIMAL(18,2), 
		CreditValue NUMERIC(18,2), 
		Detail VARCHAR(500)
	)
	
	BEGIN TRY
		
		-- ============================================
		-- ASIENTO 1: CRÉDITO (Inverso del reconocimiento)
		-- ============================================
		-- En el reconocimiento se DEBITABA la cuenta de gasto
		-- En la reversión se ACREDITA la misma cuenta
		
		INSERT INTO @TableDetail		
		SELECT	
			bca.FeesExpensesAccountId AS MainAccountId,
			IIF(ma.HandlesThirdParty = 1, crd.ThirdPartyId, NULL) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, crd.CostCenterId, NULL) AS CostCenterId,
			0 AS DebitValue,  -- ⚠️ INVERTIDO: Era débito, ahora es 0
			SUM(crd.TotalAmountPayable) AS CreditValue,  -- 💰 CRÉDITO: Reversión de Gasto
			'Reversión Reconocimiento de Costos - Honorarios Médicos' AS Detail
		FROM MedicalFees.CausationRecognition cr
		INNER JOIN MedicalFees.CausationRecognitionDetail crd ON cr.Id = crd.CausationRecognitionId
		INNER JOIN MedicalFees.MedicalFeesCausation mfc ON mfc.Id = crd.MedicalFeesCausationId
		INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = mfc.ServiceOrderDetailId
		INNER JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId
		INNER JOIN Payroll.FunctionalUnit fu ON fu.Id = crd.PerformsFunctionalUnitId
		INNER JOIN Billing.BillingConceptAccount bca ON bca.BillingConceptId = ce.BillingConceptId AND fu.UnitType = bca.UnitType
		INNER JOIN GeneralLedger.MainAccounts ma ON ma.Id = bca.FeesExpensesAccountId
		WHERE cr.Id = @CausationRecognitionId
		GROUP BY bca.FeesExpensesAccountId, ma.HandlesThirdParty, crd.ThirdPartyId, ma.HandlesCostCenter, crd.CostCenterId

		-- ============================================
		-- ASIENTO 2: DÉBITO (Inverso del reconocimiento)
		-- ============================================
		-- En el reconocimiento se ACREDITABA la cuenta por pagar
		-- En la reversión se DEBITA la misma cuenta
		
		INSERT INTO @TableDetail
		SELECT	
			dl.MainAccountCostProvisionId AS MainAccountId,
			IIF(ma.HandlesThirdParty = 1, crd.ThirdPartyId, NULL) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, crd.CostCenterId, NULL) AS CostCenterId,
			SUM(crd.TotalAmountPayable) AS DebitValue,  -- 💰 DÉBITO: Reversión de Cuenta por Pagar
			0 AS CreditValue,  -- ⚠️ INVERTIDO: Era crédito, ahora es 0
			'Reversión Reconocimiento de Costos - Honorarios Médicos por Pagar' AS Detail
		FROM MedicalFees.CausationRecognition cr
		INNER JOIN MedicalFees.CausationRecognitionDetail crd ON cr.Id = crd.CausationRecognitionId
		INNER JOIN INPROFSAL i ON i.CODPROSAL = crd.HealthProfessionalCode
		INNER JOIN Common.DistributionLines dl ON dl.Id = i.GENLINDIST
		INNER JOIN GeneralLedger.MainAccounts ma ON dl.MainAccountCostProvisionId = ma.Id
		WHERE cr.Id = @CausationRecognitionId
		GROUP BY dl.MainAccountCostProvisionId, ma.HandlesThirdParty, crd.ThirdPartyId, ma.HandlesCostCenter, crd.CostCenterId

		-- ============================================
		-- RETORNAR RESULTADOS
		-- ============================================
		-- Eliminar asientos con valor cero (por seguridad)
		DELETE FROM @TableDetail WHERE DebitValue = 0 AND CreditValue = 0

		-- Retornar los detalles del comprobante
		SELECT 
			MainAccountId, 
			ThirdPartyId, 
			CostCenterId, 
			DebitValue, 
			CreditValue, 
			Detail
		FROM @TableDetail
		ORDER BY MainAccountId, ThirdPartyId, CostCenterId
	END TRY
	BEGIN CATCH
		-- Manejo de errores
		SELECT 
			CONVERT(BIT, 0) AS StatusResult, 
			'Error al generar detalles del comprobante de reversión: ' + ERROR_MESSAGE() AS MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los asientos contables de reversión del comprobante de reconocimiento de costos por honorarios médicos, dado el identificador de un reconocimiento de causación. Invierte los débitos y créditos del asiento original: acredita la cuenta de gasto de honorarios (obtenida desde el catálogo CUPS, el concepto de facturación y la cuenta contable de gastos según tipo de unidad funcional) y debita la cuenta por pagar al proveedor médico (médico individual o agremiación, según el tipo de contrato de honorarios y las líneas de distribución contable del proveedor). Se utiliza para anular contablemente un reconocimiento de honorarios médicos previamente registrado, devolviendo el libro mayor al estado previo a dicho reconocimiento.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherDetailsCausationRecognitionReverse';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherDetailsCausationRecognitionReverse';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera los detalles contables de reversión del reconocimiento de causaciones de honorarios médicos, invirtiendo los asientos originales (gasto se acredita y cuenta por pagar se debita).', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognitionReverse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un reconocimiento de causación identificado y sus detalles asociados; Cada detalle debe tener una causación médica con orden de servicio, CUPS y unidad funcional válidas; Debe existir configuración en BillingConceptAccount para la combinación BillingConceptId/UnitType con FeesExpensesAccountId definido; El código de profesional de salud (HealthProfessionalCode) debe existir en INPROFSAL y tener una línea de distribución (GENLINDIST) con MainAccountCostProvisionId configurado; Las cuentas principales referenciadas deben existir en GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognitionReverse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La reversión invierte la naturaleza original: lo que fue débito (cuenta de gasto de honorarios) se acredita; lo que fue crédito (cuenta por pagar) se debita; El monto reversado por línea corresponde a la suma de TotalAmountPayable de los detalles del reconocimiento; Solo se asigna tercero cuando la cuenta principal lo maneja (HandlesThirdParty=1); lo mismo aplica para centro de costo (HandlesCostCenter=1); Se eliminan del resultado las líneas con DebitValue=0 y CreditValue=0; El resultado se ordena por MainAccountId, ThirdPartyId, CostCenterId; La cuenta de gasto se determina por la combinación BillingConceptId del CUPS y UnitType de la unidad funcional; La cuenta por pagar (provisión) se obtiene a través del profesional de salud (INPROFSAL) y su línea de distribución asociada', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognitionReverse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reconocimiento de causaciones; Honorarios médicos; Reversión contable; Comprobante contable; Cuenta de gastos de honorarios; Cuenta por pagar (provisión de costo); Tercero; Centro de costo; Unidad funcional; Concepto de facturación; CUPS; Línea de distribución contable', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognitionReverse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableDetail: Por cada agrupación de cuenta de gasto de honorarios (BillingConceptAccount.FeesExpensesAccountId) asociada al reconocimiento, se inserta una línea con CreditValue = SUM(TotalAmountPayable) y DebitValue=0 (reversión del gasto); [INSERT] @TableDetail: Por cada agrupación de cuenta de provisión de costo (DistributionLines.MainAccountCostProvisionId) obtenida vía INPROFSAL del profesional, se inserta una línea con DebitValue = SUM(TotalAmountPayable) y CreditValue=0 (reversión de la cuenta por pagar); [DELETE] @TableDetail: Se eliminan todas las filas donde DebitValue=0 y CreditValue=0; [RETURN_RESULT] @TableDetail: Se retornan los detalles contables (MainAccountId, ThirdPartyId, CostCenterId, DebitValue, CreditValue, Detail) ordenados por cuenta, tercero y centro de costo; [RETURN_RESULT] (resultset error): Si ocurre un error en TRY, se retorna StatusResult=0 con mensaje ''Error al generar detalles del comprobante de reversión: '' + ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognitionReverse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MainAccounts.HandlesThirdParty = 1 → Se asigna el ThirdPartyId del detalle de la causación a la línea contable else ThirdPartyId queda NULL; si MainAccounts.HandlesCostCenter = 1 → Se asigna el CostCenterId del detalle de la causación a la línea contable else CostCenterId queda NULL; si Ocurre cualquier error en la generación (BEGIN CATCH) → Se retorna un resultset con StatusResult=0 y mensaje de error en lugar del detalle contable', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognitionReverse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalFees.CausationRecognition; MedicalFees.CausationRecognitionDetail; MedicalFees.MedicalFeesCausation; Billing.ServiceOrderDetail; Contract.CUPSEntity; Payroll.FunctionalUnit; Billing.BillingConceptAccount; GeneralLedger.MainAccounts; Common.DistributionLines; INPROFSAL', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognitionReverse';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognitionReverse';
-- GO
