-- =============================================
-- Author:		Andres Alarcon
-- Create date: 2025-10-16
-- Description:	Genera los detalles del comprobante contable para el reconocimiento de causaciones de honorarios médicos
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_GenerateJournalVoucherDetailsCausationRecognition]
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
		-- ASIENTO 1: DÉBITO
		-- ============================================
		INSERT INTO @TableDetail		
		SELECT	
			ma.Id AS MainAccountId,
			IIF(ma.HandlesThirdParty = 1, crd.ThirdPartyId, NULL) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, crd.CostCenterId, NULL) AS CostCenterId,
			SUM(crd.TotalAmountPayable) AS DebitValue,  --DÉBITO: Gasto de Honorarios
			0 AS CreditValue,
			'Reconocimiento de Costos - Honorarios Médicos' AS Detail
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
		-- ASIENTO 2: CRÉDITO
		-- ============================================
		INSERT INTO @TableDetail
		SELECT	
			dl.MainAccountCostProvisionId AS MainAccountId,
			IIF(ma.HandlesThirdParty = 1, crd.ThirdPartyId, NULL) AS ThirdPartyId,
			IIF(ma.HandlesCostCenter = 1, crd.CostCenterId, NULL) AS CostCenterId,
			0 AS DebitValue,
			SUM(crd.TotalAmountPayable) AS CreditValue,  --CRÉDITO: Cuenta por Pagar
			'Reconocimiento de Costos - Honorarios Médicos por Pagar' AS Detail
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
			0 AS DebitValue,
			SUM(crd.TotalAmountPayable) AS CreditValue,  --CRÉDITO: Cuenta por Pagar
			'Reconocimiento de Costos - Honorarios Médicos por Pagar' AS Detail
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
			'Error al generar detalles del comprobante: ' + ERROR_MESSAGE() AS MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los renglones de débito y crédito del comprobante contable (voucher de diario) asociado al reconocimiento de causaciones de honorarios médicos. Para el asiento de débito registra el gasto de honorarios tomando la cuenta contable de gastos/honorarios configurada en el concepto de facturación del servicio CUPS realizado, considerando la unidad funcional que ejecutó el procedimiento. Para el asiento de crédito registra la cuenta por pagar al proveedor (médico o agremiación), resolviendo la cuenta contable a partir de las líneas de distribución del proveedor según el tipo de contrato (individual o colectivo). Existe para automatizar la contabilización del reconocimiento de costos de honorarios médicos dentro del módulo de honorarios, garantizando que cada causación aprobada genere correctamente su movimiento contable débito-crédito listo para el libro mayor.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherDetailsCausationRecognition';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherDetailsCausationRecognition';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye las líneas débito/crédito del comprobante contable para reconocer la causación de honorarios médicos, separando gasto de honorarios y cuenta por pagar según tipo de contrato.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un reconocimiento de causación con el Id recibido y sus detalles asociados; Los detalles deben enlazar con MedicalFeesCausation, ServiceOrderDetail, CUPSEntity y unidad funcional para el asiento débito; Para créditos con ContractType=1, el código del profesional (CODPROSAL) debe existir en INPROFSAL y tener proveedor (GENPROVEE) con líneas de distribución configuradas; Para créditos con ContractType=2, el contrato debe tener configurada SupplierDistributionLineId; Las cuentas contables (gasto y provisión de costo) deben estar parametrizadas en BillingConcept/BillingConceptAccount y DistributionLines', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los asientos débito siempre corresponden a gasto de honorarios médicos y los crédito a la cuenta por pagar (provisión de costo); ThirdPartyId y CostCenterId solo se diligencian si la cuenta contable está parametrizada para manejarlos; Los montos se agrupan (SUM) por cuenta y dimensiones contables relevantes; Se descartan líneas con débito y crédito en cero; Los errores no propagan excepción: se devuelven como resultset con StatusResult=0', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reconocimiento de causación; Honorarios médicos; Comprobante contable; Débito/Crédito; Cuenta de gasto de honorarios; Cuenta por pagar (provisión de costo); Tercero contable; Centro de costo; Tipo de contrato de honorarios; Concepto de facturación; Unidad funcional; CUPS; Línea de distribución de proveedor', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableDetail: Por cada combinación de cuenta contable de gasto de honorarios (resuelta por BillingConceptAccount según tipo de unidad funcional, o en su defecto BillingConcept.FeesExpensesAccountId), se inserta una línea DÉBITO con la suma de TotalAmountPayable y detalle ''Reconocimiento de Costos - Honorarios Médicos''; [INSERT] @TableDetail: Cuando mfc.ContractType=1, se inserta línea CRÉDITO con la cuenta MainAccountCostProvisionId obtenida vía INPROFSAL → SuppliersDistributionLines → DistributionLines (por código del profesional), sumando TotalAmountPayable; [INSERT] @TableDetail: Cuando mfc.ContractType=2, se inserta línea CRÉDITO con la cuenta MainAccountCostProvisionId obtenida directamente de DistributionLines vía MedicalFeesContract.SupplierDistributionLineId, sumando TotalAmountPayable; [DELETE] @TableDetail: Se eliminan registros donde DebitValue=0 y CreditValue=0 antes de retornar; [RETURN_RESULT] @TableDetail: Se retornan las líneas del comprobante ordenadas por MainAccountId, ThirdPartyId, CostCenterId; [RETURN_RESULT] (resultset error): Si ocurre excepción, retorna StatusResult=0 y MessageResult con el mensaje del error en lugar de los detalles', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ma.HandlesThirdParty = 1 → Asigna crd.ThirdPartyId a la línea contable else Deja ThirdPartyId en NULL; si ma.HandlesCostCenter = 1 → Asigna crd.CostCenterId a la línea contable else Deja CostCenterId en NULL; si mfc.ContractType = 1 → Resuelve cuenta por pagar a través del proveedor del profesional (INPROFSAL/SuppliersDistributionLines); si mfc.ContractType = 2 → Resuelve cuenta por pagar a través de la línea de distribución configurada en el contrato (SupplierDistributionLineId); si bca.FeesExpensesAccountId IS NOT NULL → Usa la cuenta de gasto definida en BillingConceptAccount para el tipo de unidad funcional else Usa bc.FeesExpensesAccountId del BillingConcept como cuenta de gasto', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.fnGetUnitType', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalFees.CausationRecognition; MedicalFees.CausationRecognitionDetail; MedicalFees.MedicalFeesCausation; Billing.ServiceOrderDetail; Contract.CUPSEntity; Payroll.FunctionalUnit; Billing.BillingConcept; Billing.BillingConceptAccount; GeneralLedger.MainAccounts; MedicalFees.MedicalFeesContract; INPROFSAL; Common.SuppliersDistributionLines; Common.DistributionLines', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsCausationRecognition';
-- GO
