-- =============================================
-- Author:		Andres Alarcon
-- Create date: 2025-10-16
-- Description:	Genera los detalles del comprobante contable de REVERSIÓN para el reconocimiento de causaciones
--              Invierte los asientos: Débitos → Créditos, Créditos → Débitos
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_GenerateJournalVoucherDetailsCausationRecognitionReverse]
	@CausationRecognitionId INT
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
			ma.Id AS MainAccountId,
			IIF(ma.HandlesThirdParty = 1, crd.ThirdPartyId, NULL) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, crd.CostCenterId, NULL) AS CostCenterId,
			0 AS DebitValue,  --INVERTIDO: Era débito, ahora es 0
			SUM(crd.TotalAmountPayable) AS CreditValue,  --CRÉDITO: Reversión de Gasto
			'Reversión Reconocimiento de Costos - Honorarios Médicos' AS Detail
		FROM MedicalFees.CausationRecognition cr
		INNER JOIN MedicalFees.CausationRecognitionDetail crd ON cr.Id = crd.CausationRecognitionId
		INNER JOIN MedicalFees.MedicalFeesCausation mfc ON mfc.Id = crd.MedicalFeesCausationId
		INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = mfc.ServiceOrderDetailId
		INNER JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId
		INNER JOIN Payroll.FunctionalUnit fu ON fu.Id = sod.PerformsFunctionalUnitId
		INNER JOIN Billing.BillingConcept bc ON bc.Id = ce.BillingConceptId
		LEFT JOIN Billing.BillingConceptAccount bca ON bca.BillingConceptId = ce.BillingConceptId 
														AND bca.UnitType = [Billing].[fnGetUnitType](fu.UnitType) 
														AND ConceptType = 2 
														AND AccountingType = 2
		LEFT JOIN GeneralLedger.MainAccounts ma ON ma.Id = COALESCE(bca.FeesExpensesAccountId, bc.FeesExpensesAccountId)
		WHERE cr.Id = @CausationRecognitionId
		GROUP BY ma.HandlesThirdParty, crd.ThirdPartyId, ma.HandlesCostCenter, crd.CostCenterId, ma.Id

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
			SUM(crd.TotalAmountPayable) AS DebitValue,  --DÉBITO: Reversión de Cuenta por Pagar
			0 AS CreditValue,  --INVERTIDO: Era crédito, ahora es 0
			'Reversión Reconocimiento de Costos - Honorarios Médicos por Pagar' AS Detail
		FROM MedicalFees.CausationRecognition cr
		INNER JOIN MedicalFees.CausationRecognitionDetail crd ON cr.Id = crd.CausationRecognitionId
		INNER JOIN MedicalFees.MedicalFeesContract mfc ON mfc.Id = crd.MedicalFeesContractId
		INNER JOIN INPROFSAL i ON i.CODPROSAL = crd.HealthProfessionalCode
		INNER JOIN Common.SuppliersDistributionLines sdl on sdl.IdSupplier = i.GENPROVEE
		INNER JOIN Common.DistributionLines dl ON dl.Id = sdl.IdDistributionLine
		INNER JOIN GeneralLedger.MainAccounts ma ON dl.MainAccountCostProvisionId = ma.Id
		WHERE cr.Id = @CausationRecognitionId AND mfc.ContractType = 1
		GROUP BY dl.MainAccountCostProvisionId, ma.HandlesThirdParty, crd.ThirdPartyId, ma.HandlesCostCenter, crd.CostCenterId

		UNION ALL

		SELECT	
			dl.MainAccountCostProvisionId AS MainAccountId,
			IIF(ma.HandlesThirdParty = 1, crd.ThirdPartyId, NULL) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, crd.CostCenterId, NULL) AS CostCenterId,
			SUM(crd.TotalAmountPayable) AS DebitValue,  --DÉBITO: Reversión de Cuenta por Pagar
			0 AS CreditValue,  --INVERTIDO: Era crédito, ahora es 0
			'Reversión Reconocimiento de Costos - Honorarios Médicos por Pagar' AS Detail
		FROM MedicalFees.CausationRecognition cr
		INNER JOIN MedicalFees.CausationRecognitionDetail crd ON cr.Id = crd.CausationRecognitionId
		INNER JOIN MedicalFees.MedicalFeesContract mfc ON mfc.Id = crd.MedicalFeesContractId
		INNER JOIN Common.DistributionLines dl ON dl.Id = mfc.SupplierDistributionLineId
		INNER JOIN GeneralLedger.MainAccounts ma ON dl.MainAccountCostProvisionId = ma.Id
		WHERE cr.Id = @CausationRecognitionId AND mfc.ContractType = 2
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
