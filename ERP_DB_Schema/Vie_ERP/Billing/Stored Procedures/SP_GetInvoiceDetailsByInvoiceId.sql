-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-11-24
-- Description:	Procedimiento que se encarga de obtener los detalles de una factura
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[SP_GetInvoiceDetailsByInvoiceId]
	@InvoiceId INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DocumentType TINYINT,
			@RevenueControlDetailId INT,
			@InvoiceStatus TINYINT,
			@InvoiceValue DECIMAL(18, 2)

	DECLARE @Table_Result AS TABLE
	(
		BillingGroupCode VARCHAR(20),
		BillingGroupName VARCHAR(500),
		----------------------------------
		Code VARCHAR(50), 
		Name VARCHAR(500),
		CodeAlternative VARCHAR(50), 
		NameAlternative VARCHAR(500),
		CodeAlternativeTwo VARCHAR(50), 
		NameAlternativeTwo VARCHAR(500),
		----------------------------------
		InvoiceQuantity INT,
		Price DECIMAL(18,2),
		Value DECIMAL(18,2),
		----------------------------------
		DiscountValue DECIMAL(18,2),
		PatientValue DECIMAL(18,2),
		PatientDiscountValue DECIMAL(18,2),
		DistributedValue DECIMAL(18,2),
		BaseDistributeValue DECIMAL(18,2),
		----------------------------------
		IVAPercentage DECIMAL(5,2),
		IVAValue DECIMAL(18,2),
		ICAPercentage DECIMAL(5,2),
		ICAValue DECIMAL(18,2),
		----------------------------------
		WithholdingPercentage DECIMAL(6,3),
		WithholdingValue DECIMAL(18,2),
		WithholdingIVAPercentage DECIMAL(6,3),
		WithholdingIVAValue DECIMAL(18,2),
		WithholdingICAPercentage DECIMAL(6,3),
		WithholdingICAValue DECIMAL(18,2)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@DocumentType = i.DocumentType,
				@RevenueControlDetailId = i.RevenueControlDetailId,
				@InvoiceStatus = Status,
				@InvoiceValue = ThirdPartySalesValue
		FROM Billing.Invoice i WITH(NOLOCK)
		WHERE i.Id = @InvoiceId

		/********************************** OBTENCION DE DATOS **********************************/		

		IF @DocumentType = 1 OR @DocumentType = 2 OR @DocumentType = 3 --OR @DocumentType = 5
		BEGIN
			INSERT INTO @Table_Result
				SELECT	bg.Code BillingGroupCode, bg.Name BillingGroupName,
						----------------------------------
						ips.Code, ips.Name,
						ce.Code CodeAlternative, ce.Description NameAlternative,
						ce.RIPSCode CodeAlternativeTwo, ce.RIPSDescription NameAlternativeTwo,
						----------------------------------
						id.InvoicedQuantity,
						ROUND(((id.InvoicedQuantity * id.TotalSalesPrice) + id.GrandTotalDiscount) / id.InvoicedQuantity, 2) Price,
						id.GrandTotalSalesPrice + id.GrandTotalDiscount Value,
						----------------------------------
						id.GrandTotalDiscount DiscountValue,
						id.SubTotalPatientSalesPrice PatientValue,
						0 PatientDiscountValue,
						(
							IIF
							(
								id.DistributionType > 1,
								(id.ThirdPartySalesPrice - id.SubTotalPatientSalesPrice),
								0
							)
						) DistributedValue,
						IIF(id.DistributionType > 1,sod.GrandTotalSalesPrice,0) BaseDistributeValue,
						----------------------------------
						0 IVAPercentage, 0 IVAValue,
						0 ICAPercentage, 0 ICAValue,
						----------------------------------
						0 WithholdingPercentage, 0 WithholdingValue,
						0 WithholdingIVAPercentage, 0 WithholdingIVAValue,
						0 WithholdingICAPercentage, 0 WithholdingICAValue
				FROM Billing.InvoiceDetail id WITH(NOLOCK)
				JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
				JOIN Contract.IPSService ips WITH(NOLOCK) ON sod.IPSServiceId = ips.Id
				JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON sod.CUPSEntityId = ce.Id
				JOIN Billing.BillingGroup bg WITH(NOLOCK) ON ce.BillingGroupId = bg.Id
				WHERE id.InvoiceId = @InvoiceId
					AND ((sod.SettlementType <> 3 AND sod.IsDelete = 0) OR (@InvoiceStatus = 2))
					AND ((sod.IsDelete = 0) OR (@InvoiceStatus = 2))
					AND (@InvoiceValue = 0 OR id.GrandTotalSalesPrice > 0)
			UNION ALL
				SELECT	bg.Code BillingGroupCode, bg.Name BillingGroupName,
						----------------------------------
						ip.Code, ip.Name,
						ip.CodeAlternative, NULL NameAlternative,
						ip.CodeAlternativeTwo, NULL NameAlternativeTwo,
						----------------------------------
						id.InvoicedQuantity,
						id.TotalSalesPrice Price,
						ROUND
						(
							(id.InvoicedQuantity * id.TotalSalesPrice),
							2
						) + id.GrandTotalDiscount Value,
						----------------------------------
						id.GrandTotalDiscount DiscountValue,
						id.SubTotalPatientSalesPrice PatientValue,
						0 PatientDiscountValue,
						(
							IIF
							(
								id.DistributionType > 1,
								(id.ThirdPartySalesPrice - id.SubTotalPatientSalesPrice),
								0
							)
						) DistributedValue,
						IIF(id.DistributionType > 1,sod.GrandTotalSalesPrice,0) BaseDistributeValue,
						----------------------------------
						0 IVAPercentage, 0 IVAValue,
						0 ICAPercentage, 0 ICAValue,
						----------------------------------
						0 WithholdingPercentage, 0 WithholdingValue,
						0 WithholdingIVAPercentage, 0 WithholdingIVAValue,
						0 WithholdingICAPercentage, 0 WithholdingICAValue
				FROM Billing.InvoiceDetail id WITH(NOLOCK)
				JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
				JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON sod.ProductId = ip.Id
				LEFT JOIN Billing.BillingGroup bg WITH(NOLOCK) ON ip.BillingGroupId = bg.Id
				WHERE id.InvoiceId = @InvoiceId
					AND ((sod.SettlementType <> 3 AND sod.IsDelete = 0) OR (@InvoiceStatus = 2))
					AND ((sod.IsDelete = 0) OR (@InvoiceStatus = 2))
					AND (@InvoiceValue = 0 OR id.GrandTotalSalesPrice > 0)
		END
		ELSE IF @DocumentType = 4 -- Factura capita
		BEGIN
			INSERT INTO @Table_Result
				SELECT	NULL BillingGroupCode, NULL BillingGroupName,
						----------------------------------
						NULL Code, 'Factura Monto Fijo' Name,
						NULL CodeAlternative, NULL NameAlternative,
						NULL CodeAlternativeTwo, NULL NameAlternativeTwo,
						----------------------------------
						IIF(i.CapitationPatientValue = 0, 1, i.CapitationlPatientsAmount) InvoiceQuantity,
						IIF(i.CapitationPatientValue = 0, i.InvoiceValue, i.CapitationPatientValue) Price,
						ROUND
						(
							IIF(i.CapitationPatientValue = 0, 1, i.CapitationlPatientsAmount) * IIF(i.CapitationPatientValue = 0, i.InvoiceValue, iec.UserValue)
							+ IIF(i.CapitationPatientValue = 0, iec.DiscountValue, 0),
							2
						) Value,
						----------------------------------
						i.ThirdPartyDiscountValue DiscountValue,
						i.TotalPatientSalesPrice PatientValue,
						i.PatientDiscount PatientDiscountValue,
						0 DistributedValue,
						0 BaseDistributeValue,
						----------------------------------
						0 IVAPercentage, 0 IVAValue,
						0 ICAPercentage, 0 ICAValue,
						----------------------------------
						0 WithholdingPercentage, 0 WithholdingValue,
						0 WithholdingIVAPercentage, 0 WithholdingIVAValue,
						0 WithholdingICAPercentage, 0 WithholdingICAValue
				FROM Billing.Invoice i WITH(NOLOCK)
				JOIN Billing.InvoiceEntityCapitated iec WITH (NOLOCK) ON iec.InvoiceId = i.Id
				WHERE i.Id = @InvoiceId
		END
		ELSE IF @DocumentType = 6 -- Factura Basica
		BEGIN
			INSERT INTO @Table_Result
				SELECT	NULL BillingGroupCode, NULL BillingGroupName,
						----------------------------------
						ip.Code, ip.Name,
						ip.CodeAlternative, NULL NameAlternative,
						ip.CodeAlternativeTwo, NULL NameAlternativeTwo,
						----------------------------------
						bbd.Quantity,
						bbd.Price,
						ROUND
						(
							bbd.Quantity * bbd.Price,
							bb.RoundLevel
						) Value,
						----------------------------------
						bbd.ValueDiscount DiscountValue,
						0 PatientValue,
						0 PatientDiscountValue,
						0 DistributedValue,
						0 BaseDistributeValue,
						----------------------------------
						bbd.PercentageIVA, ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) IvaValue,
						0 PercentageICA, 0 ICAValue,
						----------------------------------
						bbd.RetentionPercentageTax WithholdingPercentage,
						bbd.WithholdingTax WithholdingValue,
						bb.RetentionPercentageIVA WithholdingIVAPercentage,
						IIF(bb.WithholdingIVA <> 0, ROUND(ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) * ISNULL(rc.Rate, 0) / 100, 2), 0) WithholdingIVAValue,
						bbd.RetentionPercentageICA WithholdingICAPercentage,
						bbd.WithholdingICA WithholdingICAValue
				FROM Billing.BasicBilling bb WITH(NOLOCK)
				JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) ON bb.Id = bbd.BasicBillingId
				JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON bbd.ProductId = ip.Id
				LEFT JOIN GeneralLedger.RetentionConcepts rc WITH(NOLOCK) ON bb.RetentionIdIVA = rc.Id
				WHERE bb.InvoiceId = @InvoiceId AND bbd.DetailType = 1
			UNION ALL
				SELECT	NULL BillingGroupCode, NULL BillingGroupName,
						----------------------------------
						bc.Code, bc.Name,
						NULL CodeAlternative, NULL NameAlternative,
						NULL CodeAlternativeTwo, NULL NameAlternativeTwo,
						----------------------------------
						bbd.Quantity,
						bbd.Price,
						ROUND
						(
							bbd.Quantity * bbd.Price,
							bb.RoundLevel
						) Value,
						----------------------------------
						bbd.ValueDiscount DiscountValue,
						0 PatientValue,
						0 PatientDiscountValue,
						0 DistributedValue,
						0 BaseDistributeValue,
						----------------------------------
						bbd.PercentageIVA, ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) IvaValue,
						0 PercentageICA, 0 ICAValue,
						----------------------------------
						bbd.RetentionPercentageTax WithholdingPercentage,
						bbd.WithholdingTax WithholdingValue,
						bb.RetentionPercentageIVA WithholdingIVAPercentage, 
						IIF(bb.WithholdingIVA <> 0, ROUND(ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) * ISNULL(rc.Rate, 0) / 100, 2), 0) WithholdingIVAValue,
						bbd.RetentionPercentageICA WithholdingICAPercentage,
						bbd.WithholdingICA WithholdingICAValue
				FROM Billing.BasicBilling bb WITH(NOLOCK)
				JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) ON bb.Id = bbd.BasicBillingId
				JOIN Billing.BillingConcept bc WITH(NOLOCK) ON bbd.BillingConceptId = bc.Id
				LEFT JOIN GeneralLedger.RetentionConcepts rc WITH(NOLOCK) ON bb.RetentionIdIVA = rc.Id
				WHERE bb.InvoiceId = @InvoiceId AND bbd.DetailType = 2
			UNION ALL
				SELECT	NULL BillingGroupCode, NULL BillingGroupName,
						----------------------------------
						fai.Code, fai.Description Name,
						fapa.Plate CodeAlternative, NULL NameAlternative,
						NULL CodeAlternativeTwo, NULL NameAlternativeTwo,
						----------------------------------
						bbd.Quantity,
						bbd.Price,
						ROUND
						(
							bbd.Quantity * bbd.Price,
							bb.RoundLevel
						) Value,
						----------------------------------
						bbd.ValueDiscount DiscountValue,
						0 PatientValue,
						0 PatientDiscountValue,
						0 DistributedValue,
						0 BaseDistributeValue,
						----------------------------------
						bbd.PercentageIVA, ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) IvaValue,
						0 PercentageICA, 0 ICAValue,
						----------------------------------
						bbd.RetentionPercentageTax WithholdingPercentage, 
						bbd.WithholdingTax WithholdingValue,
						bb.RetentionPercentageIVA WithholdingIVAPercentage,
						IIF(bb.WithholdingIVA <> 0, ROUND(ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) * ISNULL(rc.Rate, 0) / 100, 2), 0) WithholdingIVAValue,
						bbd.RetentionPercentageICA WithholdingICAPercentage,
						bbd.WithholdingICA WithholdingICAValue
				FROM Billing.BasicBilling bb WITH(NOLOCK)
				JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) ON bb.Id = bbd.BasicBillingId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH(NOLOCK) ON bbd.PhysicalAssetId = fapa.Id
				JOIN FixedAsset.FixedAssetItem fai WITH(NOLOCK) ON fapa.ItemId = fai.Id
				LEFT JOIN GeneralLedger.RetentionConcepts rc WITH(NOLOCK) ON bb.RetentionIdIVA = rc.Id
				WHERE bb.InvoiceId = @InvoiceId AND bbd.DetailType = 3
			UNION ALL
				SELECT	NULL BillingGroupCode, NULL BillingGroupName,
						----------------------------------
						fapac.Code, fapac.Name,
						NULL CodeAlternative, NULL NameAlternative,
						NULL CodeAlternativeTwo, NULL NameAlternativeTwo,
						----------------------------------
						bbd.Quantity,
						bbd.Price,
						ROUND
						(
							bbd.Quantity * bbd.Price,
							bb.RoundLevel
						) Value,
						----------------------------------
						bbd.ValueDiscount DiscountValue,
						0 PatientValue,
						0 PatientDiscountValue,
						0 DistributedValue,
						0 BaseDistributeValue,
						----------------------------------
						bbd.PercentageIVA, ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) IvaValue,
						0 PercentageICA, 0 ICAValue,
						----------------------------------
						bbd.RetentionPercentageTax WithholdingPercentage,
						bbd.WithholdingTax WithholdingValue,
						bb.RetentionPercentageIVA WithholdingIVAPercentage,
						IIF(bb.WithholdingIVA <> 0, ROUND(ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) * ISNULL(rc.Rate, 0) / 100, 2), 0) WithholdingIVAValue,
						bbd.RetentionPercentageICA WithholdingICAPercentage,
						bbd.WithholdingICA WithholdingICAValue
				FROM Billing.BasicBilling bb WITH(NOLOCK)
				JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) ON bb.Id = bbd.BasicBillingId
				JOIN FixedAsset.FixedAssetPhysicalAssetParts fapap WITH(NOLOCK) ON bbd.PhysicalAssetPartId = fapap.Id
				JOIN FixedAsset.FixedAssetPartsAccesoriesConsumables fapac WITH(NOLOCK) ON fapap.PartAccesoriesConsumiblesId = fapac.Id
				LEFT JOIN GeneralLedger.RetentionConcepts rc WITH(NOLOCK) ON bb.RetentionIdIVA = rc.Id
				WHERE bb.InvoiceId = @InvoiceId AND bbd.DetailType = 4
		END
		ELSE IF @DocumentType = 7 -- Factura de Productos
		BEGIN
			INSERT INTO @Table_Result
				SELECT	NULL BillingGroupCode, NULL BillingGroupName,
						----------------------------------
						ip.Code, ip.Name,
						ip.CodeAlternative, NULL NameAlternative,
						ip.CodeAlternativeTwo, NULL NameAlternativeTwo,
						----------------------------------
						dipsd.Quantity,
						dipsd.SalePrice,
						ROUND
						(
							dipsd.Quantity * dipsd.SalePrice,
							2
						) Value,
						----------------------------------
						dipsd.DiscountValue DiscountValue,
						0 PatientValue,
						0 PatientDiscountValue,
						0 DistributedValue,
						0 BaseDistributeValue,
						----------------------------------
						dipsd.IvaPercentage, dipsd.IvaValue,
						0 PercentageICA, 0 ICAValue,
						----------------------------------
						dipsd.RTFPercentage WithholdingPercentage, dipsd.RTFValue WithholdingValue,
						ROUND((dipsd.WithholdingTax / dipsd.SubTotalValue) * 100, 3) WithholdingIVAPercentage, (ROUND(dipsd.WithholdingTax, 2) + ISNULL(dipsda.AdjustmentValue, 0)) WithholdingIVAValue,
						ROUND((dipsd.WithholdingICA / dipsd.SubTotalValue) * 100, 3) WithholdingICAPercentage, dipsd.WithholdingICA WithholdingICAValue
				FROM Inventory.DocumentInvoiceProductSales dips WITH(NOLOCK)
				JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH(NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
				JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON dipsd.ProductId = ip.Id
				LEFT JOIN
				(
					SELECT	MAX(IIF(dipsd.WithholdingTax <> ROUND(dipsd.WithholdingTax, 2), dipsd.Id, 0)) Id,
							ROUND(SUM(dipsd.WithholdingTax), 2) - SUM(ROUND(dipsd.WithholdingTax, 2)) AdjustmentValue
					FROM Inventory.DocumentInvoiceProductSales dips WITH(NOLOCK)
					JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH(NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
					WHERE dips.InvoiceId = @InvoiceId
				) dipsda ON dipsd.Id = dipsda.Id
				WHERE dips.InvoiceId = @InvoiceId
		END
	END TRY
	BEGIN CATCH	
		PRINT 'Error: ' + CAST(ERROR_MESSAGE() AS VARCHAR(MAX))
		PRINT 'Error Line: ' + CAST(ERROR_LINE() AS VARCHAR(MAX))

		DELETE FROM @Table_Result		
	END CATCH

	SELECT	tr.BillingGroupCode, tr.BillingGroupName,
			----------------------------------
			tr.Code, tr.Name,
			tr.CodeAlternative, tr.NameAlternative,
			tr.CodeAlternativeTwo, tr.NameAlternativeTwo,
			----------------------------------
			tr.InvoiceQuantity, tr.Price, tr.Value,
			----------------------------------
			CAST(ROUND(IIF(tr.DiscountValue = 0, 0, (tr.DiscountValue / tr.Value) * 100), 2) AS DECIMAL(5,2)) DiscountPercentage, 
			tr.DiscountValue,
			CAST(ROUND(IIF(tr.PatientValue = 0, 0, (tr.PatientValue / tr.Value) * 100), 2) AS DECIMAL(5,2)) PatientPercentage, 
			tr.PatientValue,
			CAST(ROUND(IIF(tr.PatientDiscountValue = 0, 0, (tr.PatientDiscountValue / tr.PatientValue) * 100), 2) AS DECIMAL(5,2)) PatientDiscountPercentage, 
			tr.PatientDiscountValue,
			CAST(IIF(tr.DistributedValue = 0, 0, ROUND((tr.Value * 100) / tr.BaseDistributeValue,2)) AS DECIMAL(5,2)) DistributedPercentage,
			tr.DistributedValue,
			tr.BaseDistributeValue,
			----------------------------------
			tr.IVAPercentage, tr.IVAValue,
			tr.ICAPercentage, tr.ICAValue,
			----------------------------------
			(tr.Value - tr.DiscountValue - tr.PatientValue) LineExtensionAmount,
			----------------------------------
			IIF(tr.WithholdingValue = 0, 0, tr.WithholdingPercentage) WithholdingPercentage, tr.WithholdingValue, 
			IIF(tr.WithholdingIVAValue = 0, 0, tr.WithholdingIVAPercentage) WithholdingIVAPercentage, tr.WithholdingIVAValue, 
			IIF(tr.WithholdingICAValue = 0, 0, tr.WithholdingICAPercentage) WithholdingICAPercentage, tr.WithholdingICAValue
	FROM @Table_Result tr
	ORDER BY tr.BillingGroupCode, tr.Code
END