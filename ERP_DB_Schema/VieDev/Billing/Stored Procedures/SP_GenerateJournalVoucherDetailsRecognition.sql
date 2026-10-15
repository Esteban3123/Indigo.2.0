-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2017-07-26
-- Description:	Genera los detalles de comprobante de egreso para un grupo de atencion
-- =============================================
CREATE PROCEDURE [Billing].[SP_GenerateJournalVoucherDetailsRecognition]
	@RevenueRecognitionId INT,
	@OperativeUnitId INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @careGroupType INT,
			@contractAccountingStructureId INT,
			@careGroupCostCenter INT,
			@careGroupCostCenterId INT,
			-------------------------------------------------------------------
			@DiscountSalesMainAccountId INT,
			@handlesThirdPartyMainAccountSetting BIT,
			@handlesHandlesCostCenterMainAccountSetting BIT,
			@AccountingForSurgical TINYINT,
			@RecoveryFeeDiscountMainAccountId INT,
			@MainAccountRecoveryFee INT,
			@RecoveryFeeCostCenterDiscountId INT

	DECLARE @TableDetail AS TABLE 
	(
		MainAccountId INT, 
		ThirdPartyId INT, 
		CostCenterId INT, 
		DebitValue DECIMAL(18,2), 
		CreditValue numeric(18,2), 
		Detail VARCHAR(500)
	)
	
	BEGIN TRY
		
		--SELECT	rrd.*
		--FROM Contract.CareGroup cg
		--JOIN Billing.RevenueRecognition rr ON cg.Id = rr.CareGroupId
		--JOIN Billing.RevenueRecognitionDetail rrd ON rr.Id = rrd.RevenueRecognitionId
		--JOIN Billing.RevenueControlDetail rcd ON rrd.RevenueControlDetailId = rcd.Id
		--JOIN Contract.ContractAccountingStructure cas ON cg.ContractAccountingStructureId = cas.Id
		--JOIN GeneralLedger.MainAccounts ma ON cas.ServicesPendingBillingMainAccountId = ma.Id
		--WHERE rr.Id = @RevenueRecognitionId

		declare @LiquidateMaster bit = isnull((select LiquidateMasterAccount from Billing.SettingsBilling where IdOperatingUnit = @OperativeUnitId), 0)
	
		--==DEBITO POR EL VALOR DEL SERVICIO/PRODUCTO SIN INCLUIR EL DESCUENTO (Valor cobrado a Entidad)
		INSERT INTO @TableDetail		
			SELECT	ma.Id MainAccountId, 
					IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) ThirdPartyId,
					IIF(ma.HandlesCostCenter = 1, rrd.CostCenterId, NULL) CostCenterId,
					SUM(IIF(@LiquidateMaster = 1, rrd.ThirdPartySalesPrice, rrd.ThirdPartySalesPrice + IIF(cg.CareGroupType = 3, rrd.SubTotalPatientSalesPrice, 0))) DebitValue,
					0 CreditValue,
					'1 Generación de Reconocimiento de Ingreso módulo de facturación' Detail
			FROM Contract.CareGroup cg
			JOIN Billing.RevenueRecognition rr ON cg.Id = rr.CareGroupId
			JOIN Billing.RevenueRecognitionDetail rrd ON rr.Id = rrd.RevenueRecognitionId
			JOIN Billing.RevenueControlDetail rcd ON rrd.RevenueControlDetailId = rcd.Id
			JOIN Contract.ContractAccountingStructure cas ON cg.ContractAccountingStructureId = cas.Id
			JOIN GeneralLedger.MainAccounts ma ON cas.ServicesPendingBillingMainAccountId = ma.Id
			WHERE rr.Id = @RevenueRecognitionId
			GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, rrd.CostCenterId

		--==DESCUENTOS DEBITO - (Descuentos a la entidad)
		INSERT INTO @TableDetail
			SELECT	ma.Id MainAccountId, 
					IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) ThirdPartyId,
					IIF(ma.HandlesCostCenter = 1, rrd.CostCenterId, NULL) CostCenterId,
					SUM(rrd.GrandTotalDiscount) DebitValue,
					0 CreditValue,
					'2 Generación de Reconocimiento de Ingreso módulo de facturación' Detail
			FROM Contract.CareGroup cg
			JOIN Billing.RevenueRecognition rr ON cg.Id = rr.CareGroupId
			JOIN Billing.RevenueRecognitionDetail rrd ON rr.Id = rrd.RevenueRecognitionId
			JOIN Billing.RevenueControlDetail rcd ON rrd.RevenueControlDetailId = rcd.Id
			JOIN Contract.CUPSEntity ce ON rrd.CUPSEntityId = ce.Id
			LEFT JOIN Billing.BillingConcept bc ON ce.BillingConceptId = bc.Id
			LEFT JOIN Inventory.SettingInventory si ON @OperativeUnitId = si.OperatingUnitId
			LEFT JOIN GeneralLedger.MainAccounts ma ON ISNULL(bc.DiscountAccountId, si.DiscountSalesMainAccountId) = ma.Id
			WHERE rr.Id = @RevenueRecognitionId
			GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, rrd.CostCenterId

		INSERT INTO @TableDetail
			SELECT	ma.Id MainAccountId, 
					IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) ThirdPartyId,
					IIF(ma.HandlesCostCenter = 1, rrd.CostCenterId, NULL) CostCenterId,
					SUM(rrd.GrandTotalDiscount) DebitValue,
					0 CreditValue,
					'2 Generación de Reconocimiento de Ingreso módulo de facturación' Detail
			FROM Contract.CareGroup cg
			JOIN Billing.RevenueRecognition rr ON cg.Id = rr.CareGroupId
			JOIN Billing.RevenueRecognitionDetail rrd ON rr.Id = rrd.RevenueRecognitionId
			JOIN Billing.RevenueControlDetail rcd ON rrd.RevenueControlDetailId = rcd.Id
			JOIN Inventory.InventoryProduct ipro ON rrd.ProductId = ipro.Id
			LEFT JOIN Inventory.ProductGroup pg ON pg.Id = ipro.ProductGroupId
			LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu on pgfu.ProductGroupId= pg.Id and rrd.[PerformsFunctionalUnitId] = pgfu.FunctionalUnitId
			LEFT JOIN GeneralLedger.MainAccounts ma on ma.Id = pgfu.DiscountAccountId
			WHERE rr.Id = @RevenueRecognitionId
			GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, rrd.CostCenterId

		--==VALOR PACIENTE (Solo para colombia)
		if (@LiquidateMaster = 0) begin
			INSERT INTO @TableDetail
			SELECT	ma.Id MainAccountId, 
					IIF(ma.HandlesThirdParty = 1, tp.Id, NULL) ThirdPartyId,
					IIF(ma.HandlesCostCenter = 1, rrd.CostCenterId, NULL) CostCenterId,
					SUM(rrd.SubTotalPatientSalesPrice) DebitValue,
					0 CreditValue,
					'3 Generación de Reconocimiento de Ingreso módulo de facturación' Detail
			FROM Contract.CareGroup cg
			JOIN Billing.RevenueRecognition rr ON cg.Id = rr.CareGroupId
			JOIN Billing.RevenueRecognitionDetail rrd ON rr.Id = rrd.RevenueRecognitionId
			JOIN Billing.RevenueControlDetail rcd ON rrd.RevenueControlDetailId = rcd.Id
			JOIN Billing.RevenueControl rc ON rcd.RevenueControlId = rc.Id
			JOIN Common.ThirdParty tp ON LTRIM(RTRIM(rc.PatientCode)) = tp.Nit
			JOIN Contract.ContractAccountingStructure cas ON cg.ContractAccountingStructureId = cas.Id
			JOIN GeneralLedger.MainAccounts ma ON cas.ServicesPendingBillingMainAccountId = ma.Id
			WHERE rr.Id = @RevenueRecognitionId AND cg.CareGroupType <> 3
			GROUP BY ma.Id, ma.HandlesThirdParty, tp.Id, ma.HandlesCostCenter, rrd.CostCenterId
		end

		--==Cuando esta configurado como detallado la contabilizacion de los procedimientos qx
		INSERT INTO @TableDetail
			SELECT	ma.Id MainAccountId, 
					IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) ThirdPartyId,
					IIF(ma.HandlesCostCenter = 1, rrd.CostCenterId, NULL) CostCenterId,
					0 DebitValue,
					SUM(rrd.GrandTotalSalesPrice + rrd.GrandTotalDiscount) CreditValue,
					'4 Generación de Reconocimiento de Ingreso módulo de facturación' Detail
			FROM Contract.CareGroup cg
			JOIN Billing.RevenueRecognition rr ON cg.Id = rr.CareGroupId
			JOIN Billing.RevenueRecognitionDetail rrd ON rr.Id = rrd.RevenueRecognitionId
			JOIN Billing.RevenueControlDetail rcd ON rrd.RevenueControlDetailId = rcd.Id
			JOIN [Contract].CUPSEntity ce ON rrd.CUPSEntityId = ce.Id
			JOIN Billing.BillingConcept bc ON ce.BillingConceptId = bc.Id
			JOIN Payroll.FunctionalUnit f ON rrd.PerformsFunctionalUnitId = f.Id
			JOIN GeneralLedger.MainAccounts ma ON ma.Id = [Billing].[fnGetIncomeRecognitionPendingBillingMainAccountId](rrd.RecordType, bc.Id, bc.AccountingType, bc.IncomeRecognitionPendingBillingMainAccountId, bc.IncomeRecognitionPendingBillingMainAccountId, f.UnitType)
			WHERE rr.Id = @RevenueRecognitionId
			GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, rrd.CostCenterId
		UNION ALL
			SELECT	ma.Id MainAccountId, 
					IIF(ma.HandlesThirdParty = 1, rcd.ThirdPartyId, NULL) ThirdPartyId,
					IIF(ma.HandlesCostCenter = 1, rrd.CostCenterId, NULL) CostCenterId,
					0 DebitValue,
					SUM(rrd.GrandTotalSalesPrice + rrd.GrandTotalDiscount) CreditValue,
					'5 Generación de Reconocimiento de Ingreso módulo de facturación' Detail
			FROM Contract.CareGroup cg
			JOIN Billing.RevenueRecognition rr ON cg.Id = rr.CareGroupId
			JOIN Billing.RevenueRecognitionDetail rrd ON rr.Id = rrd.RevenueRecognitionId
			JOIN Billing.RevenueControlDetail rcd ON rrd.RevenueControlDetailId = rcd.Id
			JOIN Inventory.InventoryProduct ip on rrd.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
			JOIN GeneralLedger.MainAccounts ma on ma.Id = pg.IncomeRecognitionMainAccountId
			WHERE rr.Id = @RevenueRecognitionId
			GROUP BY ma.Id, ma.HandlesThirdParty, rcd.ThirdPartyId, ma.HandlesCostCenter, rrd.CostCenterId

		SELECT * FROM @TableDetail
	END TRY
	BEGIN CATCH
		SELECT CONVERT(BIT, 0) AS StatusResult, 'Se ha producido un error!'+ ERROR_MESSAGE() AS MessageResult
	END CATCH
END