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
		WithholdingICAValue DECIMAL(18,2),
		TaxClassificationType tinyint
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@DocumentType = i.DocumentType,
				@RevenueControlDetailId = i.RevenueControlDetailId,
				@InvoiceStatus = Status,
				@InvoiceValue = ThirdPartySalesValue
		FROM Billing.Invoice i
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
						0 WithholdingICAPercentage, 0 WithholdingICAValue,
						2 as TaxClassificationType
				FROM Billing.InvoiceDetail id
				JOIN Billing.ServiceOrderDetail sod ON id.ServiceOrderDetailId = sod.Id
				JOIN Contract.IPSService ips ON sod.IPSServiceId = ips.Id
				JOIN Contract.CUPSEntity ce ON sod.CUPSEntityId = ce.Id
				JOIN Billing.BillingGroup bg ON ce.BillingGroupId = bg.Id
				WHERE id.InvoiceId = @InvoiceId
					AND ((sod.SettlementType <> 3 AND sod.IsDelete = 0) OR (@InvoiceStatus = 2))
					AND ((sod.IsDelete = 0) OR (@InvoiceStatus = 2))
					AND (@InvoiceValue = 0 OR id.GrandTotalSalesPrice > 0)
			UNION ALL
				SELECT	bg.Code BillingGroupCode, bg.Name BillingGroupName,
						----------------------------------
						CASE
							WHEN pt.Name = 'ALIMENTO' AND pt.Class = 2 THEN ip.IUM
							WHEN ip.CodeCUM IS NULL THEN ip.Code
							ELSE ip.CodeCUM
						END AS Code,		 
						ip.Name,
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
						0 WithholdingICAPercentage, 0 WithholdingICAValue,
						2 as TaxClassificationType
				FROM Billing.InvoiceDetail id
				JOIN Billing.ServiceOrderDetail sod ON id.ServiceOrderDetailId = sod.Id
				JOIN Inventory.InventoryProduct ip ON sod.ProductId = ip.Id
				JOIN Inventory.ProductType pt ON ip.ProductTypeId = pt.Id
				LEFT JOIN Billing.BillingGroup bg ON ip.BillingGroupId = bg.Id
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
							IIF(i.CapitationPatientValue = 0, 1, i.CapitationlPatientsAmount) * IIF(i.CapitationPatientValue = 0, i.InvoiceValue, iec.UserValue),
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
						0 WithholdingICAPercentage, 0 WithholdingICAValue,
						2 as TaxClassificationType
				FROM Billing.Invoice i
				JOIN Billing.InvoiceEntityCapitated iec ON iec.InvoiceId = i.Id
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
							rl.Decimals
						) Value,
						----------------------------------
						bbd.ValueDiscount DiscountValue,
						0 PatientValue,
						0 PatientDiscountValue,
						0 DistributedValue,
						0 BaseDistributeValue,
						----------------------------------
						bbd.PercentageIVA, ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, rl.Decimals) IvaValue,
						0 PercentageICA, 0 ICAValue,
						----------------------------------
						bbd.RetentionPercentageTax WithholdingPercentage,
						bbd.WithholdingTax WithholdingValue,
						bb.RetentionPercentageIVA WithholdingIVAPercentage,
						IIF(bb.WithholdingIVA <> 0, ROUND(ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, rl.Decimals) * ISNULL(rc.Rate, 0) / 100, rl.Decimals), 0) WithholdingIVAValue,
						bbd.RetentionPercentageICA WithholdingICAPercentage,
						bbd.WithholdingICA WithholdingICAValue,
						COALESCE(i.TaxClassificationType, 2) AS TaxClassificationType
				FROM Billing.BasicBilling bb
				JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
				JOIN Inventory.InventoryProduct ip ON bbd.ProductId = ip.Id
				LEFT JOIN GeneralLedger.RetentionConcepts rc ON bb.RetentionIdIVA = rc.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA i ON ip.IvaId = i.Id
				CROSS APPLY (VALUES (CASE bb.RoundLevel WHEN 0 THEN 2 WHEN 5 THEN 1 WHEN 1 THEN 0 WHEN 2 THEN -1 WHEN 3 THEN -2 WHEN 4 THEN -3 ELSE 0 END)) rl(Decimals)
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
							rl.Decimals
						) Value,
						----------------------------------
						bbd.ValueDiscount DiscountValue,
						0 PatientValue,
						0 PatientDiscountValue,
						0 DistributedValue,
						0 BaseDistributeValue,
						----------------------------------
						bbd.PercentageIVA, ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, rl.Decimals) IvaValue,
						0 PercentageICA, 0 ICAValue,
						----------------------------------
						bbd.RetentionPercentageTax WithholdingPercentage,
						bbd.WithholdingTax WithholdingValue,
						bb.RetentionPercentageIVA WithholdingIVAPercentage,
						IIF(bb.WithholdingIVA <> 0, ROUND(ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, rl.Decimals) * ISNULL(rc.Rate, 0) / 100, rl.Decimals), 0) WithholdingIVAValue,
						bbd.RetentionPercentageICA WithholdingICAPercentage,
						bbd.WithholdingICA WithholdingICAValue,
						COALESCE(i.TaxClassificationType, 2) AS TaxClassificationType
				FROM Billing.BasicBilling bb
				JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
				JOIN Billing.BillingConcept bc ON bbd.BillingConceptId = bc.Id
				LEFT JOIN GeneralLedger.RetentionConcepts rc ON bb.RetentionIdIVA = rc.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA i ON bc.IvaId = i.Id
				CROSS APPLY (VALUES (CASE bb.RoundLevel WHEN 0 THEN 2 WHEN 5 THEN 1 WHEN 1 THEN 0 WHEN 2 THEN -1 WHEN 3 THEN -2 WHEN 4 THEN -3 ELSE 0 END)) rl(Decimals)
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
							rl.Decimals
						) Value,
						----------------------------------
						bbd.ValueDiscount DiscountValue,
						0 PatientValue,
						0 PatientDiscountValue,
						0 DistributedValue,
						0 BaseDistributeValue,
						----------------------------------
						bbd.PercentageIVA, ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, rl.Decimals) IvaValue,
						0 PercentageICA, 0 ICAValue,
						----------------------------------
						bbd.RetentionPercentageTax WithholdingPercentage,
						bbd.WithholdingTax WithholdingValue,
						bb.RetentionPercentageIVA WithholdingIVAPercentage,
						IIF(bb.WithholdingIVA <> 0, ROUND(ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, rl.Decimals) * ISNULL(rc.Rate, 0) / 100, rl.Decimals), 0) WithholdingIVAValue,
						bbd.RetentionPercentageICA WithholdingICAPercentage,
						bbd.WithholdingICA WithholdingICAValue,
						COALESCE(bi.TaxClassificationType, i.TaxClassificationType, 2) AS TaxClassificationType
				FROM Billing.BasicBilling bb
				JOIN  Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
				JOIN  FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.PhysicalAssetId = fapa.Id
				JOIN  FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
				LEFT JOIN GeneralLedger.RetentionConcepts rc ON bb.RetentionIdIVA = rc.Id
				LEFT JOIN inventory.inventoryproduct p                             ON bbd.ProductId = p.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA i                        ON p.IvaId = i.Id
				LEFT JOIN Billing.BillingConcept bc                               ON bbd.BillingConceptId = bc.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA bi                       ON bc.IvaId = bi.Id
				CROSS APPLY (VALUES (CASE bb.RoundLevel WHEN 0 THEN 2 WHEN 5 THEN 1 WHEN 1 THEN 0 WHEN 2 THEN -1 WHEN 3 THEN -2 WHEN 4 THEN -3 ELSE 0 END)) rl(Decimals)
				WHERE bb.InvoiceId = @InvoiceId
					AND bbd.DetailType = 3
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
							rl.Decimals
						) Value,
						----------------------------------
						bbd.ValueDiscount DiscountValue,
						0 PatientValue,
						0 PatientDiscountValue,
						0 DistributedValue,
						0 BaseDistributeValue,
						----------------------------------
						bbd.PercentageIVA, ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, rl.Decimals) IvaValue,
						0 PercentageICA, 0 ICAValue,
						----------------------------------
						bbd.RetentionPercentageTax WithholdingPercentage,
						bbd.WithholdingTax WithholdingValue,
						bb.RetentionPercentageIVA WithholdingIVAPercentage,
						IIF(bb.WithholdingIVA <> 0, ROUND(ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, rl.Decimals) * ISNULL(rc.Rate, 0) / 100, rl.Decimals), 0) WithholdingIVAValue,
						bbd.RetentionPercentageICA WithholdingICAPercentage,
						bbd.WithholdingICA WithholdingICAValue,
						COALESCE(bi.TaxClassificationType, i.TaxClassificationType, 2) AS TaxClassificationType
				FROM Billing.BasicBilling bb
				JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
				JOIN FixedAsset.FixedAssetPhysicalAssetParts fapap ON bbd.PhysicalAssetPartId = fapap.Id
				JOIN FixedAsset.FixedAssetPartsAccesoriesConsumables fapac ON fapap.PartAccesoriesConsumiblesId = fapac.Id
				LEFT JOIN GeneralLedger.RetentionConcepts rc ON bb.RetentionIdIVA = rc.Id
				LEFT JOIN inventory.inventoryproduct p                             ON bbd.ProductId = p.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA i                        ON p.IvaId = i.Id
				LEFT JOIN Billing.BillingConcept bc                               ON bbd.BillingConceptId = bc.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA bi                       ON bc.IvaId = bi.Id
				CROSS APPLY (VALUES (CASE bb.RoundLevel WHEN 0 THEN 2 WHEN 5 THEN 1 WHEN 1 THEN 0 WHEN 2 THEN -1 WHEN 3 THEN -2 WHEN 4 THEN -3 ELSE 0 END)) rl(Decimals)
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
						ROUND((dipsd.WithholdingICA / dipsd.SubTotalValue) * 100, 3) WithholdingICAPercentage, dipsd.WithholdingICA WithholdingICAValue,
						COALESCE(i.TaxClassificationType, 2) AS TaxClassificationType
				FROM Inventory.DocumentInvoiceProductSales dips
				JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd ON dips.Id = dipsd.DocumentInvoiceProductSalesId
				JOIN Inventory.InventoryProduct ip ON dipsd.ProductId = ip.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA i ON ip.IvaId = i.Id
				LEFT JOIN
				(
					SELECT	MAX(IIF(dipsd.WithholdingTax <> ROUND(dipsd.WithholdingTax, 2), dipsd.Id, 0)) Id,
							ROUND(SUM(dipsd.WithholdingTax), 2) - SUM(ROUND(dipsd.WithholdingTax, 2)) AdjustmentValue
					FROM Inventory.DocumentInvoiceProductSales dips
				JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd ON dips.Id = dipsd.DocumentInvoiceProductSalesId
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
			IIF(tr.WithholdingICAValue = 0, 0, tr.WithholdingICAPercentage) WithholdingICAPercentage, tr.WithholdingICAValue,
			tr.TaxClassificationType
	FROM @Table_Result tr
	ORDER BY tr.BillingGroupCode, tr.Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle línea a línea de una factura de cobro dado su identificador, consolidando los servicios y procedimientos (CUPS), medicamentos e insumos facturados, junto con sus cantidades, precios, descuentos, cuotas moderadoras, valores del paciente y del tercero pagador (EPS/aseguradora), impuestos (IVA, ICA) y retenciones. Soporta distintos tipos de documento: facturas de venta estándar (tipos 1, 2, 3), facturas de capita (monto fijo periódico) y facturas de servicios complementarios, adaptando el cálculo de valores según el tipo. Compone la información uniendo el encabezado de la factura (Billing.Invoice), las líneas de detalle (Billing.InvoiceDetail), el detalle de órdenes de servicio (Billing.ServiceOrderDetail), el catálogo de servicios propios de la IPS (Contract.IPSService), el catálogo CUPS (Contract.CUPSEntity), productos de inventario (medicamentos e insumos) y los grupos de facturación (Billing.BillingGroup). Se utiliza principalmente para renderizar el cuerpo de la factura en pantalla, generar reportes de cobro, verificar la distribución de valores entre pagadores y apoyar la facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInvoiceDetailsByInvoiceId';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInvoiceDetailsByInvoiceId';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle consolidado de líneas de una factura, ramificando la consulta según el tipo de documento (asistencial, capita, básica o de productos) y calculando porcentajes y valores fiscales por línea.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir en Billing.Invoice (se obtienen DocumentType, RevenueControlDetailId, Status y ThirdPartySalesValue por Id); El DocumentType debe estar dentro de los valores soportados (1, 2, 3, 4, 6 o 7); otros tipos no producen filas', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Table_Result: Cuando DocumentType ∈ {1,2,3} se insertan dos UNION: (a) líneas de servicios asistenciales uniendo InvoiceDetail-ServiceOrderDetail-IPSService-CUPSEntity-BillingGroup y (b) líneas de productos uniendo InvoiceDetail-ServiceOrderDetail-InventoryProduct-BillingGroup, filtrando líneas no eliminadas, SettlementType<>3 (excepto si el estado de la factura es 2) y excluyendo líneas con GrandTotalSalesPrice=0 cuando la factura tiene ThirdPartySalesValue>0; [INSERT] @Table_Result: Cuando DocumentType=4 (Factura Cápita) se inserta una sola línea ''Factura Monto Fijo'' usando Invoice e InvoiceEntityCapitated; si CapitationPatientValue=0 se usa InvoiceValue como precio y cantidad 1, en otro caso se usa CapitationPatientValue × CapitationlPatientsAmount; [INSERT] @Table_Result: Cuando DocumentType=6 (Factura Básica) se insertan 4 UNION clasificadas por BasicBillingDetail.DetailType: 1=productos de inventario, 2=conceptos de facturación (BillingConcept), 3=activos físicos (FixedAssetPhysicalAsset+Item), 4=partes/accesorios/consumibles de activos; calculando IVA, retención en fuente, retención IVA (con Rate de RetentionConcepts) y retención ICA por línea; [INSERT] @Table_Result: Cuando DocumentType=7 (Factura de Productos) se insertan líneas desde DocumentInvoiceProductSales/Detail e InventoryProduct, calculando porcentajes de retención IVA e ICA como (valor/SubTotalValue)*100 y aplicando un AdjustmentValue por diferencias de redondeo en WithholdingTax al detalle con mayor variación; [DELETE] @Table_Result: Si ocurre cualquier error en el bloque TRY, se vacía @Table_Result en CATCH para no devolver datos parciales y se imprime el mensaje y línea del error; [RETURN_RESULT] RESULT: Se devuelve el contenido de @Table_Result ordenado por BillingGroupCode y Code, agregando columnas calculadas: porcentaje de descuento, paciente, descuento de paciente y distribuido sobre Value/BaseDistributeValue (con guarda de división por cero vía IIF=0→0), LineExtensionAmount=Value-DiscountValue-PatientValue, y forzando porcentajes de retención a 0 cuando su valor de retención asociado es 0', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @DocumentType IN (1,2,3) → Consultar InvoiceDetail+ServiceOrderDetail uniendo a IPSService/CUPSEntity (servicios) y a InventoryProduct (productos), aplicando filtros de SettlementType, IsDelete y GrandTotalSalesPrice según el estado de la factura; si @DocumentType = 4 → Generar una única línea ''Factura Monto Fijo'' a partir de Invoice e InvoiceEntityCapitated, eligiendo cantidad/precio según si CapitationPatientValue es cero; si @DocumentType = 6 → Construir 4 UNION desde BasicBilling/BasicBillingDetail según DetailType (1 producto, 2 concepto, 3 activo físico, 4 parte de activo), calculando IVA y retenciones con RoundLevel y RetentionConcepts.Rate; si @DocumentType = 7 → Consultar DocumentInvoiceProductSales y aplicar ajuste por redondeo (AdjustmentValue) sobre la línea con mayor diferencia entre SUM(WithholdingTax) y SUM(ROUND(WithholdingTax,2)); si sod.SettlementType <> 3 AND sod.IsDelete = 0 → Se incluye la línea en el resultado else Solo se incluye si @InvoiceStatus = 2 (factura anulada/especial); si @InvoiceValue = 0 OR id.GrandTotalSalesPrice > 0 → Se incluye la línea (si la factura tiene valor a tercero, se excluyen líneas sin valor de venta); si i.CapitationPatientValue = 0 (en cápita) → InvoiceQuantity=1 y Price=InvoiceValue else InvoiceQuantity=CapitationlPatientsAmount y Price=CapitationPatientValue; si bb.WithholdingIVA <> 0 (en básica) → Se calcula WithholdingIVAValue como ROUND(IVA*Rate/100,2) usando RetentionConcepts.Rate else WithholdingIVAValue = 0; si id.DistributionType > 1 → DistributedValue = ThirdPartySalesPrice - SubTotalPatientSalesPrice y BaseDistributeValue = sod.GrandTotalSalesPrice else DistributedValue = 0 y BaseDistributeValue = 0', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByInvoiceId';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceDetailsByInvoiceId';
-- GO
