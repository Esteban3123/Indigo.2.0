-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-11-19
-- Description:	Procedimiento que se encarga de generar el comprobante contable para la facturacion basica
-- ==============================================================================================================
CREATE  PROCEDURE [Billing].[SP_GenerateJournalVoucherByBasicBilling]
	@LegalBookId INT,
	@Id As INT,
	@InvoiceNumber AS VARCHAR(15),
	@CodeUser as VARCHAR(20),
	@IsFixedAsset BIT = 0,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN	
	BEGIN TRY
		--Se declara una tabla con los datos para la cabecera del comprobante contable 
		DECLARE @JournalVourcherTmp TABLE 
		(
			Id INT DEFAULT(0),
			Consecutive BIGINT DEFAULT(0),
			LegalBookId INT,
			IdJournalVoucher INT,
			VoucherDate VARCHAR(30),
			Imported VARCHAR(5) DEFAULT('False'),
			Status TINYINT,
			Detail VARCHAR(500),
			EntityCode VARCHAR(20),
			EntityId INT,
			EntityName VARCHAR(250),
			IsClosedYear TINYINT DEFAULT(0),
			CurrencyId INT
		)

		--Se declara una tabla temporal para los detalles del comprobante
		DECLARE @JournalVourcherDetailTmp TABLE 
		(
			Id INT DEFAULT(0),
			IdAccounting INT DEFAULT(0),
			IdMainAccount INT,
			IdThirdParty INT,
			IdCostCenter INT,
			DebitValue DECIMAL(18,2),
			CreditValue DECIMAL(18,2),
			Detail VARCHAR(500),
			IdRetention INT,
			RetentionRate DECIMAL(6,3),
			BaseValue DECIMAL(18,2),
			BillingValue DECIMAL(18,2)
		)

		--tabla temporal para almacenar el resultado del movimiento contable
		DECLARE @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

		--Variable para obtener el xml
		DECLARE @JournalVoucherXML as XML

		--Variables
		DECLARE @Code VARCHAR(20),
				@OperatingUnitId INT,
				---------------------
				@ThirdPartyId INT,
				@ThirdPartyNitName VARCHAR(200),
				@CurrencyId INT,
				@OfficialCurrencyId INT,
				@ThirdPartyEntityCopayId int,
				@StructureAccountRecoveryFeeId int,
                @InvoiceEntityCaregroupId int,
                @InvoiceEntityCaregroupLiquidationType tinyint,
                @ThirdPartyInvoiceCopayFixedAmount tinyint,
				---------------------
				-- Variables para precisión de redondeo basada en la moneda (coherente con SP_SaveBasicBilling)
				@RoundPrecision INT = 2

		--Se obtiene el codigo y la unidad operativa
		SELECT @Code = bb.Code, 
			@OperatingUnitId = bb.OperatingUnitId,
			@ThirdPartyId = c.ThirdPartyId,
			@ThirdPartyNitName = c.Nit + ' - ' + c.Name,
			@CurrencyId = bb.CurrencyId,
			@ThirdPartyEntityCopayId = bb.ThirdPartyEntityCopayId,
			@StructureAccountRecoveryFeeId = IIF(bb.ThirdPartyEntityCopayId IS NULL, NULL,  st.AccountRecoveryFeeId), -- se establece el valor solo si la factura basica fue creada como factura copago
            @InvoiceEntityCaregroupId = i.CareGroupId,
            @InvoiceEntityCaregroupLiquidationType = cg.LiquidationType, -- 2: capitacion, 5: PGP
            @ThirdPartyInvoiceCopayFixedAmount = cg.ThirdPartyInvoiceCopayFixedAmount -- 0: tercero del grupo de atencion, 1: tercero responsable del pago
		FROM Billing.BasicBilling  bb
		JOIN Common.Customer c ON bb.CustomerId = c.Id
        LEFT JOIN Billing.Invoice i ON bb.InvoiceId = i.Id
		LEFT JOIN Contract.CareGroup cg ON i.CareGroupId = cg.Id
		LEFT JOIN Contract.ContractAccountingStructure st ON cg.ContractAccountingStructureId = st.Id
		WHERE bb.Id = @Id
		
		-- Obtener precisión de redondeo basada en la moneda (coherente con SP_SaveBasicBilling)
		IF @CurrencyId IS NOT NULL AND @CurrencyId > 0
		BEGIN
			SELECT @RoundPrecision = Common.GetRoundPrecision(c.RoundingType)
			FROM Common.Currency c WITH(NOLOCK)
			WHERE c.Id = @CurrencyId
		END
		SET @RoundPrecision = ISNULL(@RoundPrecision, 2)
		
		SET @OfficialCurrencyId = (SELECT top 1 cs.OfficialCurrencyId from GeneralLedger.CompanySettings cs)

		--Id del tipo de comprobante contable
		DECLARE @JournalVoucherTypeId INT,
				@ApplyElectronicSalesTicket BIT

		--Se obtiene el tipo de comprobante contable por unidad operativa
		SELECT 
			@JournalVoucherTypeId = sb.BasicBillingJournalVoucherTypeId,
			@ApplyElectronicSalesTicket = sb.ApplyElectronicSalesTicket
		FROM Billing.SettingsBilling sb 
		WHERE sb.IdOperatingUnit = @OperatingUnitId

		--Si aplica la logica de tiquete electronico de venta se cambia el tipo de comprobante contable
		IF @ApplyElectronicSalesTicket = 1 AND EXISTS(SELECT 1 
															FROM Billing.BasicBilling bb
															JOIN Common.Customer c ON c.Id = bb.CustomerId
															JOIN Common.ThirdParty th ON th.Id = c.ThirdPartyId
															WHERE th.ElectronicBiller = 0 AND bb.Id = @Id) BEGIN

			SELECT @JournalVoucherTypeId = sb.AccountingVoucherGenerationId
			FROM Billing.SettingsBilling sb
			WHERE sb.IdOperatingUnit = @OperatingUnitId
		END

		--Inserto la cabecera del comprobante contable
		INSERT INTO @JournalVourcherTmp
		(
			LegalBookId, 
			IdJournalVoucher, 
			VoucherDate, 
			Status, 
			Detail, 
			EntityCode, 
			EntityId, 
			EntityName,
			CurrencyId
		)
		VALUES
		(
			@LegalBookId, 
			@JournalVoucherTypeId, 
			[Common].[GETDATE](), 
			2, 
			'Factura No. ' +  @InvoiceNumber + '- Tercero: (' + @ThirdPartyNitName + ')', 
			@Code, 
			@Id,
			IIF(@IsFixedAsset = 1, 'BasicBillingFixedAsset', 'BasicBilling'),
			@CurrencyId
		)

        Declare @ThirdPartyIncomeId int = ISNULL(@ThirdPartyEntityCopayId, @ThirdPartyId)
        if @InvoiceEntityCaregroupLiquidationType IS NOT NULL AND @InvoiceEntityCaregroupLiquidationType IN (2, 5)
        begin
            if @ThirdPartyInvoiceCopayFixedAmount = 1 begin
                -- si el valor del parametro "tercero copago monto fijo" está en "tercero responsable del pago" se toma el tercero: @ThirdPartyId
                set @ThirdPartyIncomeId = @ThirdPartyId
            end
        end

	    --Se insertan los detalles del comprobante contable crédito
		INSERT INTO @JournalVourcherDetailTmp
		(
			IdMainAccount, 
			IdThirdParty, 
			IdCostCenter, 
			Detail, 
			DebitValue, 
			CreditValue, 
			IdRetention, RetentionRate, BaseValue, BillingValue
		)
		--Cliente
		SELECT 
			ISNULL(@StructureAccountRecoveryFeeId, ma.Id), -- @StructureAccountRecoveryFeeId solo tiene valor si es una factura copago
			CASE ma.HandlesThirdParty 
				WHEN 1 THEN @ThirdPartyId
				ELSE NULL 
			END as ThirdPartyId,
			CASE ma.HandlesCostCenter 
				WHEN 1 THEN (SELECT TOP 1 fu.CostCenterId FROM Payroll.FunctionalUnit fu LEFT JOIN Billing.BasicBillingDetail bbd ON bbd.BasicBillingId = bb.Id WHERE bbd.BasicBillingId = bb.Id) 
				ELSE NULL 
			END as CostCenterId,
			'Detalle cuenta cliente de la Factura Basica',
			bb.TotalValue, 
			0, 
			NULL, 0, 0, 0 
		FROM Billing.BasicBilling bb
		JOIN Common.Customer c ON bb.CustomerId = c.Id
		JOIN GeneralLedger.MainAccounts ma ON c.MainAccountReceivableId = ma.Id

		WHERE bb.Id = @Id		
	--Retencion Fuente
		UNION ALL
		SELECT 
			ma.Id,
			CASE ma.HandlesThirdParty WHEN 1 THEN @ThirdPartyId ELSE NULL END as ThirdPartyId,
			CASE ma.HandlesCostCenter WHEN 1 THEN (SELECT TOP 1 fu.CostCenterId FROM Payroll.FunctionalUnit fu WHERE bbd.FunctionalUnitId = fu.Id) ELSE NULL END as CostCenterId,
			'Detalle cuenta retencion fuente de la Factura Basica',
			ROUND(SUM((bbd.Value - bbd.ValueDiscount) * bbd.RetentionPercentageTax / 100), @RoundPrecision),
			0, 			
			bbd.RetentionIdTax, bbd.RetentionPercentageTax, SUM(bbd.Value - bbd.ValueDiscount), (bb.Value - bb.ValueDiscount)
		FROM Billing.BasicBilling bb
		JOIN Billing.BasicBillingDetail bbd	ON bb.Id = bbd.BasicBillingId
		LEFT JOIN Inventory.InventoryProduct ip ON bbd.DetailType = 1 AND bbd.ProductId = ip.Id
		LEFT JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
		LEFT JOIN Billing.BillingConcept bc ON bbd.DetailType = 2 AND bbd.BillingConceptId = bc.Id
		LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.DetailType = 3 AND bbd.PhysicalAssetId = fapa.Id
		LEFT JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
		LEFT JOIN FixedAsset.FixedAssetItemCatalog faic ON fai.ItemCatalogId = faic.Id
		LEFT JOIN GeneralLedger.MainAccounts ma ON 
			(bbd.DetailType = 1 AND ma.Id = pg.WithholdingTaxAccountId) OR
			(bbd.DetailType = 2 AND ma.Id = bc.WithholdingTaxAccountId) OR
			(bbd.DetailType = 3 AND ma.Id = faic.WithholdingTaxAccountId)
		WHERE bb.Id = @Id AND bbd.WithholdingTax > 0
		GROUP BY ma.Id, ma.HandlesThirdParty, ma.HandlesCostCenter, bbd.RetentionIdTax, bbd.RetentionPercentageTax, (bb.Value - bb.ValueDiscount), bbd.FunctionalUnitId
	--Retencion IVA
		UNION ALL
		SELECT 
			ma.Id,
			CASE ma.HandlesThirdParty WHEN 1 THEN @ThirdPartyId ELSE NULL END as ThirdPartyId,
			CASE ma.HandlesCostCenter WHEN 1 THEN (SELECT TOP 1 fu.CostCenterId FROM Payroll.FunctionalUnit fu LEFT JOIN Billing.BasicBillingDetail bbd ON bbd.BasicBillingId = bb.Id WHERE bbd.BasicBillingId = bb.Id) ELSE NULL END as CostCenterId,
			'Detalle cuenta retencion IVA de la Factura Basica', 
			bb.WithholdingIVA, 
			0, 
			bb.RetentionIdIVA, bb.RetentionPercentageIVA, bb.ValueIVA, (bb.Value -bb.ValueDiscount)
		FROM Billing.BasicBilling bb
		JOIN Billing.SettingsBilling sb ON bb.OperatingUnitId = sb.IdOperatingUnit
		JOIN GeneralLedger.MainAccounts ma ON sb.ReteIVAMainAccountId = ma.Id
		WHERE bb.Id = @Id AND bb.WithholdingIVA > 0		
	--Retencion ICA
		UNION ALL
		SELECT 
			ma.Id,
			CASE ma.HandlesThirdParty WHEN 1 THEN @ThirdPartyId ELSE NULL END as ThirdPartyId,
			CASE ma.HandlesCostCenter WHEN 1 THEN (SELECT TOP 1 fu.CostCenterId FROM Payroll.FunctionalUnit fu WHERE fu.Id = bbd.FunctionalUnitId) ELSE NULL END as CostCenterId,
			'Detalle cuenta retencion ICA de la Factura Basica',
			ROUND(SUM((bbd.Value - bbd.ValueDiscount) * bbd.RetentionPercentageICA / 100), @RoundPrecision),
			0, 			
			bbd.RetentionIdICA, bbd.RetentionPercentageICA, SUM(bbd.Value - bbd.ValueDiscount), (bb.Value -bb.ValueDiscount)
		FROM Billing.BasicBilling bb
		JOIN Billing.BasicBillingDetail bbd	ON bb.Id = bbd.BasicBillingId
		LEFT JOIN Inventory.InventoryProduct ip ON bbd.DetailType = 1 AND bbd.ProductId = ip.Id
		LEFT JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
		LEFT JOIN Billing.BillingConcept bc ON bbd.DetailType = 2 AND bbd.BillingConceptId = bc.Id
		LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.DetailType = 3 AND bbd.PhysicalAssetId = fapa.Id
		LEFT JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
		LEFT JOIN FixedAsset.FixedAssetItemCatalog faic ON fai.ItemCatalogId = faic.Id
		LEFT JOIN GeneralLedger.MainAccounts ma ON 
			(bbd.DetailType = 1 AND ma.Id = pg.WithholdingICAAccountId) OR
			(bbd.DetailType = 2 AND ma.Id = bc.WithholdingICAAccountId) OR
			(bbd.DetailType = 3 AND ma.Id = faic.WithholdingICAAccountId)
		WHERE bb.Id = @Id AND bbd.WithholdingICA > 0
		GROUP BY ma.Id, ma.HandlesThirdParty, ma.HandlesCostCenter, bbd.RetentionIdICA, bbd.RetentionPercentageICA, (bb.Value -bb.ValueDiscount),bbd.FunctionalUnitId
	--Ingreso
		UNION ALL
		SELECT 
			ma.Id,
			CASE ma.HandlesThirdParty WHEN 1 THEN @ThirdPartyIncomeId ELSE NULL END as ThirdPartyId,
			CASE ma.HandlesCostCenter WHEN 1 THEN (SELECT TOP 1 fu.CostCenterId FROM Payroll.FunctionalUnit fu WHERE fu.Id = bbd.FunctionalUnitId) ELSE NULL END as CostCenterId,
			'Detalle cuenta ingreso de la Factura Basica: ' + CASE bbd.DetailType WHEN 1 THEN 'Productos' WHEN 2 THEN 'Servicios' WHEN 3 THEN 'Activos Fijos' WHEN 4 THEN 'Partes de Activos Fijos' END, 
			SUM
			(
				IIF
				(
					Common.CurrencyConverter(ISNULL(ISNULL(fapadb.ResidualValue, (fapa.HistoricalValue - fapa.FinancialDiscount)), 0),IIF(bbd.DetailType = 3, lb.OfficialCurrencyId, @OfficialCurrencyId),@CurrencyId)> (bbd.Value - bbd.ValueDiscount), 
					common.CurrencyConverter(ISNULL(ISNULL(fapadb.ResidualValue, (fapa.HistoricalValue - fapa.FinancialDiscount)), 0),IIF(bbd.DetailType = 3, lb.OfficialCurrencyId, @OfficialCurrencyId),@CurrencyId) - (bbd.Value - bbd.ValueDiscount),
					0					
				)
			) DebitValue, 
			SUM
			(
				IIF
				(
					IIF(SB.AccountsConditionalCommercialDiscount = 1, bbd.Value, bbd.Value - bbd.ValueDiscount) > common.CurrencyConverter(ISNULL(ISNULL(fapadb.ResidualValue, (fapa.HistoricalValue - fapa.FinancialDiscount)), 0),IIF(bbd.DetailType = 3, lb.OfficialCurrencyId, @OfficialCurrencyId),@CurrencyId), 
					IIF(SB.AccountsConditionalCommercialDiscount = 1, bbd.Value, bbd.Value - bbd.ValueDiscount) - common.CurrencyConverter(ISNULL(ISNULL(fapadb.ResidualValue, (fapa.HistoricalValue - fapa.FinancialDiscount)), 0),IIF(bbd.DetailType = 3, lb.OfficialCurrencyId, @OfficialCurrencyId),@CurrencyId), 
					0
				)
			) CreditValue, 
			NULL, 0, 0, 0 
		FROM Billing.BasicBillingDetail bbd		
		JOIN Billing.BasicBilling bb on bbd.BasicBillingId = bb.Id
		JOIN Common.OperatingUnit op ON op.id = bb.OperatingUnitId
		JOIN Billing.SettingsBilling sb ON sb.IdOperatingUnit = op.Id
		LEFT JOIN FixedAsset.SettingFixedAsset sfa ON sfa.OperatingUnitId = op.Id
		LEFT JOIN Inventory.InventoryProduct ip ON bbd.DetailType = 1 AND bbd.ProductId = ip.Id
		LEFT JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
		LEFT JOIN Billing.BillingConcept bc ON bbd.DetailType = 2 AND bbd.BillingConceptId = bc.Id
		LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.DetailType = 3 AND bbd.PhysicalAssetId = fapa.Id
		LEFT JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
		LEFT JOIN FixedAsset.FixedAssetItemCatalog faic ON fai.ItemCatalogId = faic.Id
		LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId AND fapadb.LegalBookId = @LegalBookId
		LEFT JOIN GeneralLedger.LegalBook lb ON lb.Id = @LegalBookId
		LEFT JOIN GeneralLedger.MainAccounts ma ON 
			(bbd.DetailType = 1 AND ma.Id = pg.IncomeAccountId) OR
			(bbd.DetailType = 2 AND ma.Id = IIF(
												bbd.ServicesProvidedId IS NOT NULL,
												(SELECT TOP 1 bc.EntityIncomeAccountId from Billing.BillingConcept bc WHERE bc.Id = bbd.ServicesProvidedId),
												iif (bc.ConceptType = 3, iif((select top 1 cg.liquidationType from Billing.Invoice iv join contract.caregroup cg on iv.caregroupid = cg.id where iv.id = bb.InvoiceId) = 1, bc.CopayMainAccountId, bc.RecoveryFixedAmountMainAccountId), bc.EntityIncomeAccountId))) OR
												-- bc.EntityIncomeAccountId)) OR
		(bbd.DetailType = 3 AND ma.Id = IIF
										(
											(bbd.Value - bbd.ValueDiscount) > common.CurrencyConverter(ISNULL(ISNULL(fapadb.ResidualValue, (fapa.HistoricalValue - fapa.FinancialDiscount)), 0),lb.OfficialCurrencyId,@CurrencyId), 
											faic.NetIncomeAccountId, 
											faic.LossMainAccountId
										))
		WHERE bbd.BasicBillingId = @Id 
			AND (bbd.Value - bbd.ValueDiscount) > 0 
			AND (bbd.Value - bbd.ValueDiscount) <> common.CurrencyConverter(ISNULL(ISNULL(fapadb.ResidualValue, (fapa.HistoricalValue - fapa.FinancialDiscount)), 0),IIF(bbd.DetailType = 3, lb.OfficialCurrencyId, @OfficialCurrencyId),@CurrencyId)
		GROUP BY ma.Id, ma.HandlesThirdParty, ma.HandlesCostCenter, bbd.DetailType,bbd.FunctionalUnitId
	--IVA
		UNION ALL

		SELECT 
			ma.Id,
			CASE ma.HandlesThirdParty WHEN 1 THEN @ThirdPartyId ELSE NULL END as ThirdPartyId,
			CASE ma.HandlesCostCenter WHEN 1 THEN (SELECT TOP 1 fu.CostCenterId FROM Payroll.FunctionalUnit fu WHERE bbd.FunctionalUnitId = fu.Id) ELSE NULL END as CostCenterId,
			CONCAT('Detalle cuenta IVA (',iva.Name,') de la Factura Basica: ',CASE bbd.DetailType
																						WHEN 1 THEN 'Productos' 
																						WHEN 2 THEN 'Servicios' 
																						WHEN 3 THEN 'Activos Fijos' 
																						WHEN 4 THEN 'Partes de Activos Fijos' END),
			0 DebitValue,
			-- IVA: redondear por ítem antes de sumar (coherente con SP_SaveBasicBilling y presentación)
			SUM(ROUND((bbd.Value - bbd.ValueDiscount) * bbd.PercentageIVA / 100, @RoundPrecision)) CreditValue,
			NULL, 0, 0, 0
		FROM Billing.BasicBilling bb
		LEFT JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
		LEFT JOIN Inventory.InventoryProduct ip ON bbd.DetailType = 1 AND bbd.ProductId = ip.Id
		LEFT JOIN Billing.BillingConcept bc ON bbd.DetailType = 2 AND bbd.BillingConceptId = bc.Id
		LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.DetailType = 3 AND bbd.PhysicalAssetId = fapa.Id
		LEFT JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
		JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON (bbd.DetailType = 1 AND ip.TaxedProduct = 1 AND ip.IVAId=iva.Id) OR
																(bbd.DetailType=2 AND bc.IVAId =iva.Id) OR
																(bbd.DetailType=3 AND iva.Id=fai.IVAId)
		JOIN GeneralLedger.MainAccounts ma ON ma.Id = iva.IdAccountSale
		WHERE bb.Id = @Id AND bb.ValueIVA > 0 AND bbd.PercentageIVA > 0
		GROUP BY ma.Id, ma.HandlesThirdParty, ma.HandlesCostCenter, bbd.DetailType,iva.Id,iva.Name,bbd.FunctionalUnitId
		UNION ALL
	--DESCUENTO
		SELECT
			ma.id,
			CASE ma.HandlesThirdParty 
			WHEN 1 THEN 10000 
			ELSE NULL END as ThirdPartyId,
			CASE ma.HandlesCostCenter 
			WHEN 1 THEN fu.CostCenterId 
			ELSE NULL 
			END as CostCenterId,
			'Detalle cuenta Descuento de la Factura Basica: ' + CASE bbd.DetailType 
														WHEN 1 THEN 'Productos' 
														WHEN 2 THEN 'Servicios' 
														WHEN 3 THEN 'Activos Fijos' 
														WHEN 4 THEN 'Partes de Activos Fijos' 
														END,
			bbd.ValueDiscount debitValue, 
			0 creditValue,  
			NULL,
			0,
			0, 
			0 
		FROM Billing.BasicBilling bb
			JOIN Billing.BasicBillingDetail bbd on bb.id = bbd.BasicBillingId
			JOIN Payroll.FunctionalUnit fu on fu.id = bbd.FunctionalUnitId
			JOIN Common.Customer c ON bb.CustomerId = c.Id
			JOIN Common.OperatingUnit op ON op.id = bb.OperatingUnitId
			JOIN Billing.SettingsBilling sb ON sb.IdOperatingUnit = op.Id
			LEFT JOIN Inventory.InventoryProduct ip ON bbd.DetailType = 1 AND bbd.ProductId = ip.Id
			LEFT JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
			LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu on pgfu.ProductGroupId = pg.Id and pgfu.FunctionalUnitId = bbd.FunctionalUnitId
			LEFT JOIN Billing.BillingConcept bc ON bbd.DetailType = 2 AND bbd.BillingConceptId = bc.Id
			LEFT JOIN GeneralLedger.MainAccounts ma ON ma.Id = isnull(bc.DiscountAccountId,pgfu.DiscountAccountId)
		WHERE sb.AccountsConditionalCommercialDiscount = 1 and bbd.ValueDiscount > 0 -- si esta activa esta bandera se muestra el descuento y hay valor en el descuento
			and bb.Id = @Id
	--COSTO PRODUCTOS (debito al costo: siempre la cuenta de costo del grupo)
		UNION ALL
		SELECT 
			ma.Id,
			CASE ma.HandlesThirdParty WHEN 1 THEN @ThirdPartyId ELSE NULL END,
			CASE ma.HandlesCostCenter WHEN 1 THEN (SELECT TOP 1 fu.CostCenterId FROM Payroll.FunctionalUnit fu WHERE fu.Id = lc.FunctionalUnitId) ELSE NULL END,
			'Detalle cuenta costo inventario de la Factura Basica',
			common.CurrencyConverter(SUM(lc.LineCost),@OfficialCurrencyId,@CurrencyId),
			0,
			NULL, 0, 0, 0
		FROM (
			SELECT bbd.FunctionalUnitId,
				   pg.InventoryCostMainAccountId,
				   pg.CounterpartCostConsignedInventoryId,
				   apc.IdAccount AS OwnedInventoryAccountId,
				   CASE WHEN wh.WarehouseConsignment = 1 THEN 1 ELSE 0 END AS IsConsignment,
				   sup.IdThirdParty AS SupplierThirdPartyId,
				   bbd.Quantity * ISNULL(clp.CostNew, ip.ProductCost) AS LineCost
			FROM Billing.BasicBillingDetail bbd
			JOIN Billing.BasicBilling bb ON bb.Id = bbd.BasicBillingId
			JOIN Inventory.InventoryProduct ip ON bbd.DetailType = 1 AND bbd.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc ON pg.InventoryAccountPayableConceptId = apc.Id
			LEFT JOIN Inventory.Warehouse wh ON wh.Id = bbd.WarehouseId
			LEFT JOIN Common.Supplier sup ON sup.Id = wh.SupplierId
			OUTER APPLY (
				SELECT TOP 1 ccld.CostNew
				FROM Inventory.ConsignmentCostList ccl
				JOIN Inventory.ConsignmentCostListDetail ccld ON ccld.ConsignmentCostListId = ccl.Id
				WHERE wh.WarehouseConsignment = 1 AND sup.ConsignmentInventoryCosting = 1
				  AND ccl.SupplierId = wh.SupplierId AND ccld.ProductId = bbd.ProductId
				  AND ccl.OperatingUnitId = @OperatingUnitId
				  AND CAST(bb.DocumentDate AS DATE) <= CAST(ccl.EffectiveDate AS DATE)
				ORDER BY ccl.EffectiveDate ASC
			) clp
			WHERE bbd.BasicBillingId = @Id
		) lc
		JOIN GeneralLedger.MainAccounts ma ON ma.Id = lc.InventoryCostMainAccountId
		GROUP BY ma.Id, ma.HandlesThirdParty, ma.HandlesCostCenter, lc.FunctionalUnitId

	--CONTRAPARTIDA DEL COSTO (consignacion: cuenta contrapartida + tercero proveedor; propio: cuenta inventario + cliente)
		UNION ALL
		SELECT 
			ma.Id,
			CASE WHEN ma.HandlesThirdParty = 1 THEN IIF(lc.IsConsignment = 1, lc.SupplierThirdPartyId, @ThirdPartyId) ELSE NULL END,
			CASE ma.HandlesCostCenter WHEN 1 THEN (SELECT TOP 1 fu.CostCenterId FROM Payroll.FunctionalUnit fu WHERE fu.Id = lc.FunctionalUnitId) ELSE NULL END,
			IIF(lc.IsConsignment = 1, 'Detalle contrapartida costo inventario en consignacion de la Factura Basica', 'Detalle cuenta inventario de la Factura Basica'),
			0,
			common.CurrencyConverter(SUM(lc.LineCost),@OfficialCurrencyId,@CurrencyId),
			NULL, 0, 0, 0
		FROM (
			SELECT bbd.FunctionalUnitId,
				   pg.InventoryCostMainAccountId,
				   pg.CounterpartCostConsignedInventoryId,
				   apc.IdAccount AS OwnedInventoryAccountId,
				   CASE WHEN wh.WarehouseConsignment = 1 THEN 1 ELSE 0 END AS IsConsignment,
				   sup.IdThirdParty AS SupplierThirdPartyId,
				   bbd.Quantity * ISNULL(clp.CostNew, ip.ProductCost) AS LineCost
			FROM Billing.BasicBillingDetail bbd
			JOIN Billing.BasicBilling bb ON bb.Id = bbd.BasicBillingId
			JOIN Inventory.InventoryProduct ip ON bbd.DetailType = 1 AND bbd.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
			JOIN Payments.AccountPayableConcepts apc ON pg.InventoryAccountPayableConceptId = apc.Id
			LEFT JOIN Inventory.Warehouse wh ON wh.Id = bbd.WarehouseId
			LEFT JOIN Common.Supplier sup ON sup.Id = wh.SupplierId
			OUTER APPLY (
				SELECT TOP 1 ccld.CostNew
				FROM Inventory.ConsignmentCostList ccl
				JOIN Inventory.ConsignmentCostListDetail ccld ON ccld.ConsignmentCostListId = ccl.Id
				WHERE wh.WarehouseConsignment = 1 AND sup.ConsignmentInventoryCosting = 1
				  AND ccl.SupplierId = wh.SupplierId AND ccld.ProductId = bbd.ProductId
				  AND ccl.OperatingUnitId = @OperatingUnitId
				  AND CAST(bb.DocumentDate AS DATE) <= CAST(ccl.EffectiveDate AS DATE)
				ORDER BY ccl.EffectiveDate ASC
			) clp
			WHERE bbd.BasicBillingId = @Id
		) lc
		JOIN GeneralLedger.MainAccounts ma ON ma.Id = IIF(lc.IsConsignment = 1, lc.CounterpartCostConsignedInventoryId, lc.OwnedInventoryAccountId)
		GROUP BY ma.Id, ma.HandlesThirdParty, ma.HandlesCostCenter, lc.FunctionalUnitId, lc.IsConsignment, lc.SupplierThirdPartyId

	--COSTO ACTIVOS
		UNION ALL
		SELECT 
			ma.Id,
			CASE ma.HandlesThirdParty WHEN 1 THEN @ThirdPartyId ELSE NULL END as ThirdPartyId,
			CASE ma.HandlesCostCenter WHEN 1 THEN (SELECT TOP 1 fu.CostCenterId FROM Payroll.FunctionalUnit fu WHERE fu.Id = bbd.FunctionalUnitId) ELSE NULL END as CostCenterId,
			IIF(ma.Id = fapa.MainAccountId, 'Detalle cuenta activo de la Factura Basica', 'Detalle cuenta depreciacion del activo de la Factura Basica'), 
			IIF(ma.Id = fapa.MainAccountId, 0, common.CurrencyConverter( SUM(ISNULL(fapadb.DepreciatedValue,0)),lb.OfficialCurrencyId,@CurrencyId)), 
			IIF(ma.Id = fapa.MainAccountId, common.CurrencyConverter(SUM(ISNULL((fapadb.DepreciatedValue + fapadb.ResidualValue), (fapa.HistoricalValue - fapa.FinancialDiscount))),lb.OfficialCurrencyId,@CurrencyId), 0), 
			NULL, 0, 0, 0 
		FROM Billing.BasicBillingDetail bbd
		JOIN Billing.BasicBilling bb on bb.Id = bbd.BasicBillingId
		JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.DetailType = 3 AND bbd.PhysicalAssetId = fapa.Id
		JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
		JOIN FixedAsset.FixedAssetItemCatalog faic ON fai.ItemCatalogId = faic.Id
		JOIN FixedAsset.SettingFixedAsset sfa ON sfa.OperatingUnitId = bb.OperatingUnitId
		LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId AND fapadb.LegalBookId = @LegalBookId
		JOIN GeneralLedger.LegalBook lb ON lb.Id = @LegalBookId
		JOIN GeneralLedger.MainAccounts ma ON bbd.DetailType = 3 AND 
			(ma.Id = fapa.MainAccountId OR ma.Id = IIF(fapadb.Id IS NULL, 0,
														CASE fapa.AdquisitionType															
															WHEN 7 THEN faic.DepreciationLeasingAccountId
															WHEN 9 THEN faic.FinancialRentingAccountId
															ELSE faic.DepreciationAccountId
														END
													)
			)
		WHERE bbd.BasicBillingId = @Id
		GROUP BY ma.Id, ma.HandlesThirdParty, ma.HandlesCostCenter, fapa.MainAccountId,bbd.FunctionalUnitId, lb.OfficialCurrencyId

	--Eliminamos cuentas en 0
		DELETE jv FROM @JournalVourcherDetailTmp jv WHERE jv.DebitValue = 0 AND jv.CreditValue = 0
		
	--Obtengo el xml para poder consumir el sp que guarda el comprobante contable

		SELECT @JournalVoucherXML = CONVERT(xml, 
			(
				SELECT * FROM @JournalVourcherTmp JournalVoucher 
				JOIN @JournalVourcherDetailTmp JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting 
				For xml AUTO,TYPE, ELEMENTS
			)
		)

	--Se consume el sp que guarda el comprobante contable
		Declare @CodeMessage Int,
			@Message Varchar(Max),
			@IdJournalVoucherResult Int
			
		insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML,@CodeUser 
		select 
				@CodeMessage = rjv.code, 
				@Message = rjv.MessageResult, 
				@IdJournalVoucherResult = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv

	--Se valida que no hayan errores en el guardado del comprobante contable
		IF @CodeMessage = '999' 
		BEGIN
			SELECT	@Message = CONCAT(lb.Code, ' - ', lb.Name, ': ', @Message)
			FROM GeneralLedger.LegalBook lb
			WHERE lb.Id = @LegalBookId

			SELECT	@CodeResult = 999, 
					@MessageResult = @Message
			RETURN
		END

	--Se obtiene el consecutivo que generó el comprobante contable
	IF @Message is NULL OR @Message='' BEGIN
		SELECT
			@Message = lb.Code + ' - ' + lb.Name + ': Se generó el comprobante contable de tipo ' + jvt.Code + ' - ' + jvt.Name
		from GeneralLedger.JournalVoucherTypes jvt
		JOIN GeneralLedger.LegalBook lb ON lb.Id = @LegalBookId
		where jvt.Id = @JournalVoucherTypeId
	END
		

		SELECT @CodeResult = 0, 
			   @MessageResult = @Message
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el comprobante contable (voucher de diario) correspondiente a una factura de facturación básica. Consolida la información de la prefactura o factura en borrador (BasicBilling), el cliente o entidad pagadora (EPS, aseguradora, empresa), la estructura contable del contrato y el grupo de atención para determinar las cuentas contables correctas de ingresos, copagos, impuestos (IVA, retenciones, ICA) y tarifas de recuperación. Tiene en cuenta el tipo de liquidación del grupo de atención (capitación, PGP), si existe un tercero de copago, y la moneda de la transacción para aplicar el redondeo adecuado, generando así los movimientos débito/crédito que alimentan el libro contable oficial de la institución.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByBasicBilling';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateJournalVoucherByBasicBilling';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera y registra el comprobante contable de una factura básica armando cabecera y detalles (cliente, retenciones, ingreso, IVA, descuento y costos) y delegando su persistencia al SP de comprobantes.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el registro en Billing.BasicBilling con el Id recibido y su Customer asociado en Common.Customer.; Debe existir configuración en Billing.SettingsBilling para la unidad operativa de la factura (BasicBillingJournalVoucherTypeId).; Debe existir un OfficialCurrencyId configurado en GeneralLedger.CompanySettings.; El cliente debe tener MainAccountReceivableId configurado en GeneralLedger.MainAccounts.; Para tiquete electrónico, el ThirdParty del cliente debe estar marcado como ElectronicBiller=0 para aplicar el tipo de comprobante alterno.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.JournalVouchers: A través de EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement con el XML armado, se persiste la cabecera del comprobante (LegalBookId, tipo, fecha, detalle ''Factura No. ... - Tercero: (NIT - Nombre)'', EntityCode, EntityId=@Id, EntityName=''BasicBilling'', moneda).; [INSERT] @JournalVourcherDetailTmp: Cuenta cliente (débito): usa ISNULL(StructureAccountRecoveryFeeId, MainAccountReceivableId) — si la factura básica es de copago (ThirdPartyEntityCopayId no nulo) se usa AccountRecoveryFeeId del ContractAccountingStructure; débito = bb.TotalValue.; [INSERT] @JournalVourcherDetailTmp: Retención en la fuente (débito SUM(WithholdingTax)): solo cuando bbd.WithholdingTax>0; cuenta tomada de ProductGroup.WithholdingTaxAccountId / BillingConcept.WithholdingTaxAccountId / FixedAssetItemCatalog.WithholdingTaxAccountId según DetailType (1/2/3).; [INSERT] @JournalVourcherDetailTmp: Retención IVA (débito bb.WithholdingIVA): solo cuando bb.WithholdingIVA>0; cuenta = SettingsBilling.ReteIVAMainAccountId.; [INSERT] @JournalVourcherDetailTmp: Retención ICA (débito SUM(WithholdingICA)): solo cuando bbd.WithholdingICA>0; cuenta tomada de WithholdingICAAccountId del ProductGroup/BillingConcept/FixedAssetItemCatalog según DetailType.; [INSERT] @JournalVourcherDetailTmp: Ingreso por línea: si DetailType=2 y BillingConcept.ConceptType=3 y la liquidación del CareGroup de la Invoice es 1, usa bc.CopayMainAccountId, si no usa bc.RecoveryFixedAmountMainAccountId; en otro caso usa EntityIncomeAccountId. Para servicios con ServicesProvidedId se toma EntityIncomeAccountId del concepto referenciado.; [INSERT] @JournalVourcherDetailTmp: Ingreso de activos fijos (DetailType=3): la cuenta es NetIncomeAccountId si (Value-ValueDiscount) > valor en libros convertido (ResidualValue/HistoricalValue), de lo contrario LossMainAccountId; los valores se distribuyen entre débito y crédito según comparación con el valor en libros.; [INSERT] @JournalVourcherDetailTmp: IVA crédito por línea: solo cuando bb.ValueIVA>0 y bbd.PercentageIVA>0; valor = ROUND(SUM((Value-ValueDiscount)*PercentageIVA/100), bb.RoundLevel); cuenta = GeneralLedgerIVA.IdAccountSale.; [INSERT] @JournalVourcherDetailTmp: Descuento (débito bbd.ValueDiscount): solo si SettingsBilling.AccountsConditionalCommercialDiscount=1 y ValueDiscount>0; cuenta = ISNULL(BillingConcept.DiscountAccountId, ProductGroupFunctionalUnit.DiscountAccountId); ThirdPartyId fijo = 10000 cuando la cuenta maneja tercero.; [INSERT] @JournalVourcherDetailTmp: Costo de productos (DetailType=1): registra dos líneas usando InventoryAccountPayableConceptId (apc.IdAccount) y InventoryCostMainAccountId del ProductGroup; valor = SUM(Quantity*ProductCost) convertido de moneda oficial a moneda de la factura.; [INSERT] @JournalVourcherDetailTmp: Costo de activos fijos (DetailType=3): registra cuenta del activo (fapa.MainAccountId) por HistoricalValue/(Depreciated+Residual) y cuenta de depreciación según AdquisitionType (7=DepreciationLeasing, 9=FinancialRenting, otros=Depreciation) por DepreciatedValue, convertidos a la moneda de la factura.; [DELETE] @JournalVourcherDetailTmp: Se eliminan filas con DebitValue=0 y CreditValue=0 antes de armar el XML.; [RETURN_RESULT] RESULT: Si SP_CreateAndValidateJournalVoucherMovement retorna code=''999'', devuelve @CodeResult=999 y @MessageResult prefijado con ''Code - Name del LegalBook''; en éxito devuelve @CodeResult=0 con mensaje ''Se generó el comprobante contable {Consecutive} de tipo {jvt.Code} - {jvt.Name}''.; [RETURN_RESULT] RESULT: En caso de excepción (CATCH) devuelve @CodeResult=999 y @MessageResult con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByBasicBilling';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateJournalVoucherByBasicBilling';
-- GO

