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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera las líneas de detalle del comprobante contable (journal voucher) para el proceso de reconocimiento de ingresos en facturación, por grupo de atención y unidad operativa. Para cada reconocimiento de ingreso, produce los movimientos débito y crédito correspondientes a: el valor del servicio o producto cobrado a la entidad pagadora (cuentas por cobrar pendientes de facturar), los descuentos otorgados a la entidad, el valor a cargo del paciente (cuotas moderadoras y copagos), y los descuentos al paciente, considerando si se liquida como cuenta maestra o no. Consulta la configuración contable del contrato (ContractAccountingStructure) para determinar las cuentas contables principales (MainAccounts), respeta si cada cuenta maneja terceros y centros de costo, y toma como base el detalle del reconocimiento de ingresos (RevenueRecognitionDetail) cruzado con los folios de control (RevenueControlDetail) y el grupo de atención (CareGroup). Es el procedimiento central que alimenta el libro mayor con la causación contable de los ingresos hospitalarios antes de la facturación formal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherDetailsRecognition';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherDetailsRecognition';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye y devuelve los renglones débito/crédito del comprobante contable de reconocimiento de ingresos para un proceso de facturación dado, separando valor cobrado a la entidad, descuentos, valor paciente e ingresos por servicios y productos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un Billing.RevenueRecognition con Id = @RevenueRecognitionId vinculado a un CareGroup con ContractAccountingStructure válida.; La estructura contable del contrato debe tener configurada ServicesPendingBillingMainAccountId.; Para la línea de valor paciente, el RevenueControl.PatientCode debe coincidir (trim) con un Common.ThirdParty.Nit.; Debe existir configuración Billing.SettingsBilling para la unidad operativa (si no existe se asume LiquidateMasterAccount = 0).; Para descuentos de productos debe existir relación ProductGroupFunctionalUnit por ProductGroupId y PerformsFunctionalUnitId con DiscountAccountId.; Para el crédito por productos, el ProductGroup debe tener IncomeRecognitionMainAccountId definido.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las líneas se generan siempre para un único RevenueRecognitionId recibido como parámetro.; Las columnas ThirdPartyId y CostCenterId sólo se llenan cuando la cuenta contable lo permite (HandlesThirdParty/HandlesCostCenter = 1); en caso contrario se fuerza NULL.; Las líneas de descuento y de valor paciente sólo aparecen como débito; las líneas de ingreso por servicio/producto del bloque final sólo aparecen como crédito.; El crédito de reconocimiento se calcula como GrandTotalSalesPrice + GrandTotalDiscount (valor bruto antes de descuento).; La línea de valor paciente excluye explícitamente los grupos de atención con CareGroupType = 3.; El procedimiento no realiza INSERT/UPDATE en tablas físicas; sólo construye y devuelve un conjunto de detalles contables en una tabla en memoria.; Si ocurre cualquier error, se devuelve un único registro con StatusResult=0 y un mensaje de error en lugar del set de detalles.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reconocimiento de ingresos; Comprobante contable / asiento; Grupo de atención (CareGroup); Estructura contable de contrato; Descuentos a la entidad; Valor paciente (copago/cuota); Procedimientos quirúrgicos contabilizados detalladamente; Tercero (entidad/paciente); Centro de costo; Cuentas PUC (MainAccounts); Productos de inventario y grupos de productos; Unidad funcional; Liquidación maestra (Colombia)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SettingsBilling.LiquidateMasterAccount = 1 para la unidad operativa → El débito por el servicio toma sólo ThirdPartySalesPrice (no se suma el valor paciente) y NO se genera la línea débito de valor paciente (#3) else Si LiquidateMaster = 0 se inserta línea débito de valor paciente (etiqueta ''3'') usando SubTotalPatientSalesPrice contra el tercero asociado al PatientCode, sólo cuando CareGroupType <> 3; si cg.CareGroupType = 3 (en el cálculo del débito principal) → Al ThirdPartySalesPrice se suma SubTotalPatientSalesPrice como parte del débito del servicio else Sólo se debita ThirdPartySalesPrice; si MainAccounts.HandlesThirdParty = 1 → Se asigna ThirdPartyId a la línea contable else ThirdPartyId queda NULL; si MainAccounts.HandlesCostCenter = 1 → Se asigna CostCenterId a la línea contable else CostCenterId queda NULL; si BillingConcept.DiscountAccountId IS NULL para el CUPS → Se utiliza SettingInventory.DiscountSalesMainAccountId de la unidad operativa como cuenta de descuento else Se utiliza la cuenta de descuento configurada en BillingConcept', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.fnGetIncomeRecognitionPendingBillingMainAccountId', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Billing.RevenueRecognition; Billing.RevenueRecognitionDetail; Billing.RevenueControlDetail; Contract.ContractAccountingStructure; GeneralLedger.MainAccounts; Billing.SettingsBilling; Contract.CUPSEntity; Billing.BillingConcept; Inventory.SettingInventory; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductGroupFunctionalUnit; Billing.RevenueControl; Common.ThirdParty; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherDetailsRecognition';
-- GO
