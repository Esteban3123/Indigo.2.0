-- =============================================
-- Author:		Juan David Capera
-- Create date: 2023-04-13
-- Description:	procedimiento encargado de obtener los detalles del documento electrónico para enviar al API de integración CIMA
-- =============================================
CREATE PROCEDURE [Billing].[GetElectronicDocumentDetailsInformation]
	@DocumentId INT,
	@DocumentName VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	-- ============================================================
	-- Variables de exención tributaria del tercero pagador
	-- ============================================================
	DECLARE
		@DocumentType TINYINT,
		@DocumentTypeExemption VARCHAR(2),
		@DocumentTypeExemptionOther VARCHAR(100),
		@DocumentNumberExemption VARCHAR(40),
		@InstitutionCodeExemption VARCHAR(2),
		@InstitutionNameExemption VARCHAR(160),
		@ArticleNumberExemption INT,
		@ClauseNumberExemption INT,
		@EmissionDateExemption DATETIME,
		@PercentageExemption NUMERIC(3, 0)

	-- ============================================================
	-- Tabla temporal de resultados
	-- ============================================================
	IF OBJECT_ID('tempdb..#Table_Result') IS NOT NULL
		DROP TABLE #Table_Result;

	CREATE TABLE #Table_Result
	(
		StateResult BIT NOT NULL,
		MessageResult VARCHAR(500),
		------------------------
		Id INT,
		DocumentId INT NOT NULL,
		Code VARCHAR(50) NOT NULL,
		Name VARCHAR(500) NOT NULL,
		CabysCode VARCHAR(50),
		TypeItem TINYINT NOT NULL,
		MeasureUnit VARCHAR(20) NOT NULL,
		CodeAlternative VARCHAR(50),
		CodeAlternativeTwo VARCHAR(50),
		BillingGroup VARCHAR(500),
		MedicationRegistration VARCHAR(100),
		DosageForm VARCHAR(20),
		TransactionType VARCHAR(2),
		InvoicedQuantity INT NOT NULL,
		UnitValue DECIMAL(18,5) NOT NULL,
		SalesPrice DECIMAL(18,5) NOT NULL,
		GrandTotalSalesPrice DECIMAL(18,5) NOT NULL,
		TaxValue DECIMAL(18,5) NOT NULL,
		IvaPercentage DECIMAL(5,2) NOT NULL,
		RateCode VARCHAR(20),
		DiscountValue DECIMAL(18,5) NOT NULL,
		ReferenceDocument VARCHAR(500),
		ShippingDateReferenceDocument DATETIME,
		ApplyTaxDevolution BIT NOT NULL DEFAULT(0)
		-- Indica si el IVA aplica para devolución de impuestos médicos
	)

	-- ============================================================
	-- Resuelve tipo de documento y datos de exención tributaria
	-- ============================================================
	SELECT TOP 1
		@DocumentType = ed.DocumentType,
		@DocumentTypeExemption = ISNULL(te.InternalCode, '00'),
		@DocumentTypeExemptionOther = CASE
			WHEN te.InternalCode = '99' THEN tpte.Detail
			ELSE ''
		END,
		@DocumentNumberExemption = ISNULL(tpte.DocumentIdentification, ''),
		@InstitutionCodeExemption = ISNULL(tei.InternalCode, '00'),
		@InstitutionNameExemption = ISNULL(tei.Description, ''),
		@ArticleNumberExemption = ISNULL(TRY_PARSE(tpte.[ART/RESNumber] AS INT), 0),
		@ClauseNumberExemption = 0,
		@EmissionDateExemption = ISNULL(tpte.DocumentDate, DATEFROMPARTS(1900,1,1)),
		@PercentageExemption = ISNULL(gliva.Percentage, 0)
	FROM Billing.ElectronicDocument ed
		LEFT JOIN Common.ThirdPartyTaxExemptions tpte ON ed.CustomerPartyId = tpte.ThirdPartyId
		LEFT JOIN [Common].[TaxExemptions] te ON tpte.DocumentTypeId = te.Id
		LEFT JOIN [Common].[TaxExemptions] tei ON tpte.InstitutionId = tei.Id
		LEFT JOIN [GeneralLedger].[GeneralLedgerIVA] gliva ON tpte.ExemptFeeId = gliva.Id
	WHERE ed.EntityId = @DocumentId
		AND ed.EntityName = @DocumentName

	BEGIN TRY

		-- ============================================================
		-- FACTURA PRESTACIÓN DE SERVICIOS SALUD (DocumentType 1, 2, 3)
		-- ============================================================
		IF @DocumentType IN (1,2,3)
		BEGIN
			INSERT INTO #Table_Result
			-- Servicios IPS
			SELECT
				0 AS StateResult,
				'OK' AS MessageResult,
				id.Id,
				i.Id DocumentId,
				ips.Code,
				ips.Name,
				ce.RIPSCode CabysCode,
				1 TypeItem,
				'Sp' MeasureUnit,
				NULL CodeAlternative,
				NULL CodeAlternativeTwo,
				bg.Code + ' - ' + bg.Name BillingGroup,
				'' MedicationRegistration,
				'' DosageForm,
				'01' TransactionType,
				sod.InvoicedQuantity,
				CAST(ROUND((id.NetWorth + id.GrandTotalDiscount)/NULLIF(id.InvoicedQuantity,0),5) AS DECIMAL(20,5)) UnitValue,
				CAST(ROUND(id.GrandTotalSalesPrice - id.GrandTotalTaxes,5) AS DECIMAL(20,5)) SalesPrice,
				id.GrandTotalSalesPrice,
				-- TaxValue: Usar el valor real almacenado; solo se recalcula el nominal cuando el tercero
				-- tiene una exención tributaria real (@PercentageExemption > 0) y el ítem es gravado.
				-- Para terceros sin exención, el comportamiento es idéntico al actual (sin cambios).
				IIF(@PercentageExemption > 0 AND ISNULL(iva.Percentage, 0) > 0 AND id.GrandTotalTaxes = 0,
					ROUND(id.NetWorth * ISNULL(iva.Percentage, 0) / 100, 5), id.GrandTotalTaxes) TaxValue,
				ISNULL(iva.Percentage, 0) IvaPercentage,
				CASE
					WHEN ISNULL(iva.Percentage, 0) = 0  THEN '11'
					WHEN ISNULL(iva.Percentage, 0) = 1  THEN '02'
					WHEN ISNULL(iva.Percentage, 0) = 2  THEN '03'
					WHEN ISNULL(iva.Percentage, 0) = 4  THEN '04'
					WHEN ISNULL(iva.Percentage, 0) = 8  THEN '07'
					WHEN ISNULL(iva.Percentage, 0) = 13 THEN '08'
					ELSE '08'
				END RateCode,
				id.GrandTotalDiscount DiscountValue,
				NULL ReferenceDocument,
				NULL ShippingDateReferenceDocument,
				ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
			FROM Billing.Invoice i
				JOIN Billing.InvoiceDetail id ON i.Id = id.InvoiceId
				JOIN Billing.ServiceOrderDetail sod ON sod.Id = id.ServiceOrderDetailId
				JOIN Contract.IPSService ips ON ips.Id = sod.IPSServiceId
				JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId
				JOIN Billing.BillingGroup bg ON bg.Id = ce.BillingGroupId
				JOIN GeneralLedger.CompanySettings cs ON 1=1
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = ISNULL(id.TaxId,sod.IvaId)
			WHERE i.Id = @DocumentId
				AND sod.SettlementType <> 3
				AND sod.IsDelete = 0
				AND (id.GrandTotalSalesPrice > 0 OR id.GrandTotalDiscount > 0)

			UNION ALL

			-- Productos de inventario
			SELECT
				0 AS StateResult,
				'OK' AS MessageResult,
				id.Id,
				i.Id DocumentId,
				p.Code,
				p.Name,
				IIF(p.ATCId is null, isnull(p.CodeAlternative,p.CodeAlternativeTwo), ISNULL(p.CodeAlternativeTwo, p.CodeAlternative)) CabysCode,
				2 TypeItem,
				'Unid' MeasureUnit,
				p.CodeAlternative,
				p.CodeAlternativeTwo,
				bg.Code + ' - ' + bg.Name BillingGroup,
				ISNULL(IIF(atc.Id IS NULL, '', p.HealthRegistration), '') MedicationRegistration,
				ISNULL(pfg.Code, '') DosageForm,
				'01' TransactionType,
				sod.InvoicedQuantity,
				CAST(ROUND((id.NetWorth + id.GrandTotalDiscount)/NULLIF(id.InvoicedQuantity,0),5) AS DECIMAL(20,5)) UnitValue,
				CAST(ROUND(id.GrandTotalSalesPrice - id.GrandTotalTaxes,5) AS DECIMAL(20,5)) SalesPrice,
				id.GrandTotalSalesPrice,
				-- TaxValue: Usar el valor real almacenado; solo se recalcula el nominal cuando el tercero
				-- tiene una exención tributaria real (@PercentageExemption > 0) y el ítem es gravado.
				-- Para terceros sin exención, el comportamiento es idéntico al actual (sin cambios).
				IIF(@PercentageExemption > 0 AND ISNULL(iva.Percentage, 0) > 0 AND id.GrandTotalTaxes = 0,
					ROUND(id.NetWorth * ISNULL(iva.Percentage, 0) / 100, 5), id.GrandTotalTaxes) TaxValue,
				ISNULL(iva.Percentage, 0) IvaPercentage,
				CASE
					WHEN ISNULL(iva.Percentage, 0) = 0  THEN '11'
					WHEN ISNULL(iva.Percentage, 0) = 1  THEN '02'
					WHEN ISNULL(iva.Percentage, 0) = 2  THEN '03'
					WHEN ISNULL(iva.Percentage, 0) = 4  THEN '04'
					WHEN ISNULL(iva.Percentage, 0) = 8  THEN '07'
					WHEN ISNULL(iva.Percentage, 0) = 13 THEN '08'
					ELSE '08'
				END RateCode,
				id.GrandTotalDiscount DiscountValue,
				NULL ReferenceDocument,
				NULL ShippingDateReferenceDocument,
				ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
			FROM Billing.Invoice i
				JOIN Billing.InvoiceDetail id ON i.Id = id.InvoiceId
				JOIN Billing.ServiceOrderDetail sod ON sod.Id = id.ServiceOrderDetailId
				JOIN Inventory.InventoryProduct p ON p.Id = sod.ProductId
				JOIN GeneralLedger.CompanySettings cs ON 1=1
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = ISNULL(id.TaxId,sod.IvaId)
				LEFT JOIN Billing.ProductServiceDetail psd ON sod.Id = psd.ServiceOrderDetailId
				LEFT JOIN Contract.CUPSEntity cups ON psd.CUPSEntityId=cups.Id
				LEFT JOIN Billing.BillingGroup bg ON bg.Id = IIF(psd.CUPSEntityId IS NULL, p.BillingGroupId, cups.BillingGroupId)
				LEFT JOIN Inventory.ATC atc ON p.ATCId = atc.Id
				LEFT JOIN Inventory.PharmaceuticalForm pf ON atc.PharmaceuticalFormId = pf.Id
				LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg ON pf.PharmaceuticalFormGroupingId = pfg.Id
			WHERE i.Id = @DocumentId
				AND sod.SettlementType <> 3
				AND sod.IsDelete = 0
				AND (id.GrandTotalSalesPrice > 0 OR id.GrandTotalDiscount > 0)
		END

		-- ============================================================
		-- FACTURA BÁSICA (DocumentType 6)
		-- ============================================================
		ELSE IF @DocumentType = 6
		BEGIN
			INSERT INTO #Table_Result
			-- Productos
			SELECT
				0 AS StateResult,
				'OK' AS MessageResult,
				bbd.Id,
				i.Id DocumentId,
				ip.Code,
				ip.Name,
				ISNULL(ip.CodeAlternativeTwo, ip.CodeAlternative) CabysCode,
				2 TypeItem,
				'Unid' MeasureUnit,
				ip.CodeAlternative,
				ip.CodeAlternativeTwo,
				NULL BillingGroup,
				ISNULL(IIF(atc.Id IS NULL, '', ip.HealthRegistration), '') MedicationRegistration,
				ISNULL(pfg.Code, '') DosageForm,
				'01' TransactionType,
				bbd.Quantity InvoicedQuantity,
				bbd.Price UnitValue,
				ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) SalesPrice,
				ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) GrandTotalSalesPrice,
				ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) TaxValue,
				bbd.PercentageIVA IvaPercentage,
				CASE
					WHEN bbd.PercentageIVA = 0  THEN '11'
					WHEN bbd.PercentageIVA = 1  THEN '02'
					WHEN bbd.PercentageIVA = 2  THEN '03'
					WHEN bbd.PercentageIVA = 4  THEN '04'
					WHEN bbd.PercentageIVA = 8  THEN '07'
					WHEN bbd.PercentageIVA = 13 THEN '08'
					ELSE '08'
				END RateCode,
				bbd.ValueDiscount DiscountValue,
				NULL ReferenceDocument,
				NULL ShippingDateReferenceDocument,
				ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
			FROM Billing.Invoice i
				JOIN Billing.BasicBilling bb ON i.Id = bb.InvoiceId
				JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
				JOIN Inventory.InventoryProduct ip ON bbd.ProductId = ip.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = ip.IVAId
				LEFT JOIN Inventory.ATC atc ON ip.ATCId = atc.Id
				LEFT JOIN Inventory.PharmaceuticalForm pf ON atc.PharmaceuticalFormId = pf.Id
				LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg ON pf.PharmaceuticalFormGroupingId = pfg.Id
			WHERE i.Id = @DocumentId
				AND bbd.DetailType = 1

			UNION ALL

			-- Servicios
			SELECT
				0 AS StateResult,
				'OK' AS MessageResult,
				bbd.Id,
				i.Id DocumentId,
				bc.Code,
				bc.Name,
				cast(bc.AlternativeCode as VARCHAR(50)) CabysCode,
				1 TypeItem,
				'Sp' MeasureUnit,
				NULL CodeAlternative,
				NULL CodeAlternativeTwo,
				NULL BillingGroup,
				'' MedicationRegistration,
				'' DosageForm,
				'01' TransactionType,
				bbd.Quantity InvoicedQuantity,
				bbd.Price UnitValue,
				ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) SalesPrice,
				ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) GrandTotalSalesPrice,
				ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) TaxValue,
				bbd.PercentageIVA IvaPercentage,
				CASE
					WHEN bbd.PercentageIVA = 0  THEN '11'
					WHEN bbd.PercentageIVA = 1  THEN '02'
					WHEN bbd.PercentageIVA = 2  THEN '03'
					WHEN bbd.PercentageIVA = 4  THEN '04'
					WHEN bbd.PercentageIVA = 8  THEN '07'
					WHEN bbd.PercentageIVA = 13 THEN '08'
					ELSE '08'
				END RateCode,
				bbd.ValueDiscount DiscountValue,
				NULL ReferenceDocument,
				NULL ShippingDateReferenceDocument,
				ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
			FROM Billing.Invoice i
				JOIN Billing.BasicBilling bb ON i.Id = bb.InvoiceId
				JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
				JOIN Billing.BillingConcept bc ON bbd.BillingConceptId = bc.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = bc.IVAId
			WHERE i.Id = @DocumentId
				AND bbd.DetailType = 2

			UNION ALL

			-- Activos fijos
			SELECT
				0 AS StateResult,
				'OK' AS MessageResult,
				bbd.Id,
				i.Id DocumentId,
				fai.Code,
				fai.Description Name,
				NULL CabysCode,
				2 TypeItem,
				'Unid' MeasureUnit,
				NULL CodeAlternative,
				NULL CodeAlternativeTwo,
				NULL BillingGroup,
				'' MedicationRegistration,
				'' DosageForm,
				'01' TransactionType,
				bbd.Quantity InvoicedQuantity,
				bbd.Price UnitValue,
				ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) SalesPrice,
				ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) GrandTotalSalesPrice,
				ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) TaxValue,
				bbd.PercentageIVA IvaPercentage,
				CASE
					WHEN bbd.PercentageIVA = 0  THEN '11'
					WHEN bbd.PercentageIVA = 1  THEN '02'
					WHEN bbd.PercentageIVA = 2  THEN '03'
					WHEN bbd.PercentageIVA = 4  THEN '04'
					WHEN bbd.PercentageIVA = 8  THEN '07'
					WHEN bbd.PercentageIVA = 13 THEN '08'
					ELSE '08'
				END RateCode,
				bbd.ValueDiscount DiscountValue,
				NULL ReferenceDocument,
				NULL ShippingDateReferenceDocument,
				ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
			FROM Billing.Invoice i
				JOIN Billing.BasicBilling bb ON i.Id = bb.InvoiceId
				JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.PhysicalAssetId = fapa.Id
				JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = fai.IVAId
			WHERE i.Id = @DocumentId
				AND bbd.DetailType = 3
		END

		-- ============================================================
		-- FACTURA DE PRODUCTOS (DocumentType 7)
		-- ============================================================
		ELSE IF @DocumentType = 7
		BEGIN
			INSERT INTO #Table_Result
			SELECT
				0 AS StateResult,
				'OK' AS MessageResult,
				dipsd.Id,
				i.Id DocumentId,
				ip.Code,
				ip.Name,
				ISNULL(ip.CodeAlternativeTwo, ip.CodeAlternative) CabysCode,
				2 TypeItem,
				'Unid' MeasureUnit,
				ip.CodeAlternative,
				ip.CodeAlternativeTwo,
				NULL BillingGroup,
				ISNULL(IIF(atc.Id IS NULL, '', ip.HealthRegistration), '') MedicationRegistration,
				ISNULL(pfg.Code, '') DosageForm,
				'01' TransactionType,
				dipsd.Quantity InvoicedQuantity,
				dipsd.SalePrice UnitValue,
				ROUND(dipsd.Quantity * dipsd.SalePrice, 2) SalesPrice,
				ROUND(dipsd.Quantity * dipsd.SalePrice, 2) GrandTotalSalesPrice,
				ISNULL(dipsd.IvaValue, 0) TaxValue,
				ISNULL(dipsd.IvaPercentage, 0) IvaPercentage,
				CASE
					WHEN ISNULL(dipsd.IvaPercentage, 0) = 0  THEN '11'
					WHEN ISNULL(dipsd.IvaPercentage, 0) = 1  THEN '02'
					WHEN ISNULL(dipsd.IvaPercentage, 0) = 2  THEN '03'
					WHEN ISNULL(dipsd.IvaPercentage, 0) = 4  THEN '04'
					WHEN ISNULL(dipsd.IvaPercentage, 0) = 8  THEN '07'
					WHEN ISNULL(dipsd.IvaPercentage, 0) = 13 THEN '08'
					ELSE '08'
				END RateCode,
				dipsd.DiscountValue DiscountValue,
				NULL ReferenceDocument,
				NULL ShippingDateReferenceDocument,
				ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
			FROM Billing.Invoice i
				JOIN Inventory.DocumentInvoiceProductSales dips ON i.Id = dips.InvoiceId
				JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd ON dips.Id = dipsd.DocumentInvoiceProductSalesId
				JOIN Inventory.InventoryProduct ip ON dipsd.ProductId = ip.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = ip.IVAId
				LEFT JOIN Inventory.ATC atc ON ip.ATCId = atc.Id
				LEFT JOIN Inventory.PharmaceuticalForm pf ON atc.PharmaceuticalFormId = pf.Id
				LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg ON pf.PharmaceuticalFormGroupingId = pfg.Id
			WHERE i.Id = @DocumentId
		END

		-- ============================================================
		-- NOTAS (cualquier otro DocumentType)
		-- ============================================================
		ELSE
		BEGIN
			DECLARE @EntityOrigin VARCHAR(250),
					@EntityOriginId INT

			SELECT @EntityOrigin = EntityName,
				@EntityOriginId = EntityId
			FROM Billing.BillingNote
			WHERE Id = @DocumentId

			-- --------------------------------------------------------
			-- Nota asociada a una Factura (Invoice)
			-- --------------------------------------------------------
			IF @EntityOrigin = 'Invoice'
			BEGIN
				INSERT INTO #Table_Result
				-- Productos de inventario
				SELECT
					0 AS StateResult,
					'OK' AS MessageResult,
					id.Id,
					i.Id DocumentId,
					p.Code,
					p.Name,
					IIF(p.ATCId is null, isnull(p.CodeAlternative,p.CodeAlternativeTwo),
							ISNULL(p.CodeAlternativeTwo, p.CodeAlternative)) CabysCode,
					2 TypeItem,
					'Unid' MeasureUnit,
					p.CodeAlternative,
					p.CodeAlternativeTwo,
					bg.Code + ' - ' + bg.Name BillingGroup,
					ISNULL(IIF(atc.Id IS NULL, '', p.HealthRegistration), '') MedicationRegistration,
					ISNULL(pfg.Code, '') DosageForm,
					'01' TransactionType,
					sod.InvoicedQuantity,
					CAST(ROUND((id.GrandTotalSalesPrice + id.GrandTotalDiscount)/NULLIF(id.InvoicedQuantity,0),5) AS DECIMAL(20,5)) UnitValue,
					id.GrandTotalSalesPrice SalesPrice,
					id.GrandTotalSalesPrice,
					-- TaxValue: Usar el valor real almacenado; solo se recalcula el nominal cuando el tercero
					-- tiene una exención tributaria real (@PercentageExemption > 0) y el ítem es gravado.
					-- Para terceros sin exención, el comportamiento es idéntico al actual (sin cambios).
					IIF(@PercentageExemption > 0 AND ISNULL(iva.Percentage, 0) > 0 AND id.GrandTotalTaxes = 0,
						ROUND(id.NetWorth * ISNULL(iva.Percentage, 0) / 100, 5), id.GrandTotalTaxes) TaxValue,
					ISNULL(iva.Percentage, 0) IvaPercentage,
					CASE
						WHEN ISNULL(iva.Percentage, 0) = 0  THEN '11'
						WHEN ISNULL(iva.Percentage, 0) = 1  THEN '02'
						WHEN ISNULL(iva.Percentage, 0) = 2  THEN '03'
						WHEN ISNULL(iva.Percentage, 0) = 4  THEN '04'
						WHEN ISNULL(iva.Percentage, 0) = 8  THEN '07'
						WHEN ISNULL(iva.Percentage, 0) = 13 THEN '08'
						ELSE '08'
					END RateCode,
					id.GrandTotalDiscount DiscountValue,
					bnd.CUFE ReferenceDocument,
					ed.ShippingDate ShippingDateReferenceDocument,
					ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
				FROM Billing.BillingNote bn
					JOIN Billing.BillingNoteDetail bnd ON bn.Id = bnd.BillingNoteId
					JOIN Billing.Invoice i ON i.Id = bnd.InvoiceId
					JOIN Billing.InvoiceDetail id on I.Id=id.InvoiceId
					JOIN Billing.ServiceOrderDetail sod on sod.Id=id.ServiceOrderDetailId
					JOIN Inventory.InventoryProduct p ON sod.ProductId = p.Id
					JOIN Billing.ElectronicDocument ed ON i.Id = ed.EntityId AND 'Invoice' = ed.EntityName
					LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = ISNULL(id.TaxId,sod.IvaId)
					JOIN GeneralLedger.CompanySettings cs ON 1=1
					LEFT JOIN Billing.ProductServiceDetail psd ON sod.Id = psd.ServiceOrderDetailId
					LEFT JOIN Contract.CUPSEntity cups ON psd.CUPSEntityId=cups.Id
					LEFT JOIN Billing.BillingGroup bg ON bg.Id = IIF(psd.CUPSEntityId IS NULL, p.BillingGroupId, cups.BillingGroupId)
					LEFT JOIN Inventory.ATC atc ON p.ATCId = atc.Id
					LEFT JOIN Inventory.PharmaceuticalForm pf ON atc.PharmaceuticalFormId = pf.Id
					LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg ON pf.PharmaceuticalFormGroupingId = pfg.Id
				WHERE bn.Id = @DocumentId
					AND sod.SettlementType <> 3
					AND sod.IsDelete = 0
					AND (id.GrandTotalSalesPrice > 0 OR id.GrandTotalDiscount > 0)

				UNION ALL

				-- Servicios IPS
				SELECT
					0 AS StateResult,
					'OK' AS MessageResult,
					id.Id,
					i.Id DocumentId,
					ips.Code,
					ips.Name,
					ce.RIPSCode CabysCode,
					1 TypeItem,
					'Sp' MeasureUnit,
					NULL CodeAlternative,
					NULL CodeAlternativeTwo,
					bg.Code + ' - ' + bg.Name BillingGroup,
					'' MedicationRegistration,
					'' DosageForm,
					'01' TransactionType,
					sod.InvoicedQuantity,
					CAST(ROUND((id.GrandTotalSalesPrice + id.GrandTotalDiscount)/NULLIF(id.InvoicedQuantity,0),5) AS DECIMAL(20,5)) UnitValue,
					id.GrandTotalSalesPrice SalesPrice,
					id.GrandTotalSalesPrice,
					-- TaxValue: Usar el valor real almacenado; solo se recalcula el nominal cuando el tercero
					-- tiene una exención tributaria real (@PercentageExemption > 0) y el ítem es gravado.
					-- Para terceros sin exención, el comportamiento es idéntico al actual (sin cambios).
					IIF(@PercentageExemption > 0 AND ISNULL(iva.Percentage, 0) > 0 AND id.GrandTotalTaxes = 0,
						ROUND(id.NetWorth * ISNULL(iva.Percentage, 0) / 100, 5), id.GrandTotalTaxes) TaxValue,
					ISNULL(iva.Percentage, 0) IvaPercentage,
					CASE
						WHEN ISNULL(iva.Percentage, 0) = 0  THEN '11'
						WHEN ISNULL(iva.Percentage, 0) = 1  THEN '02'
						WHEN ISNULL(iva.Percentage, 0) = 2  THEN '03'
						WHEN ISNULL(iva.Percentage, 0) = 4  THEN '04'
						WHEN ISNULL(iva.Percentage, 0) = 8  THEN '07'
						WHEN ISNULL(iva.Percentage, 0) = 13 THEN '08'
						ELSE '08'
					END RateCode,
					id.GrandTotalDiscount DiscountValue,
					bnd.CUFE ReferenceDocument,
					ed.ShippingDate ShippingDateReferenceDocument,
					ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
				FROM Billing.BillingNote bn
					JOIN Billing.BillingNoteDetail bnd ON bn.Id = bnd.BillingNoteId
					join Billing.Invoice i ON i.Id = bnd.InvoiceId
					JOIN Billing.ElectronicDocument ed ON i.Id = ed.EntityId AND 'Invoice' = ed.EntityName
					JOIN Billing.InvoiceDetail id ON i.Id = id.InvoiceId
					JOIN Billing.ServiceOrderDetail sod ON sod.Id = id.ServiceOrderDetailId
					JOIN Contract.IPSService ips ON ips.Id = sod.IPSServiceId
					JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId
					JOIN Billing.BillingGroup bg ON bg.Id = ce.BillingGroupId
					JOIN GeneralLedger.CompanySettings cs ON 1=1
					LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = ISNULL(id.TaxId,sod.IvaId)
				WHERE bn.Id = @DocumentId
					AND sod.SettlementType <> 3
					AND sod.IsDelete = 0
					AND (id.GrandTotalSalesPrice > 0 OR id.GrandTotalDiscount > 0)
			END

			-- --------------------------------------------------------
			-- Nota asociada a PortfolioNote
			-- --------------------------------------------------------
			ELSE
			BEGIN
				/*Se construye tabla variable para almacenar los datos de la Nota electronica minimos*/
				DECLARE @TempNote as TABLE (
					DocumentId INT NOT NULL,
					EntityId INT NOT NULL,
					EntityName VARCHAR(250) NOT NULL,
					NoteType TINYINT NOT NULL,
					PortfolioNoteId INT NOT NULL,
					ShippingDate DATETIME,
					Id INT NOT NULL,
					InvoiceId INT,
					InvoiceType TINYINT,
					CUFE VARCHAR(250) NOT NULL,
					BillingValue NUMERIC(20,2) NOT NULL,
					OriginId INT
				)

				INSERT INTO @TempNote
					(
					DocumentId,EntityId,EntityName,
					NoteType,PortfolioNoteId,ShippingDate,
					Id,InvoiceId,InvoiceType,CUFE,BillingValue
					)
				select
					bn.Id DocumentId,
					bn.EntityId,
					bn.EntityName,
					pn.NoteType,
					pn.Id PortfolioNoteId,
					ed.ShippingDate,
					bnd.Id,
					i.Id,
					i.DocumentType,
					bnd.CUFE,
					bnd.BillingValue
				FROM Billing.BillingNote bn
					JOIN Billing.BillingNoteDetail bnd ON bn.Id = bnd.BillingNoteId
					JOIN Billing.Invoice i ON bnd.InvoiceId = i.Id
					JOIN Portfolio.PortfolioNote pn ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
					OUTER APPLY (
						SELECT TOP 1
							ShippingDate
						FROM Billing.ElectronicDocument ed
						WHERE ed.EntityId = bn.Id
							AND ed.EntityName = 'BillingNote'
						ORDER BY ed.Id DESC
					) ed
				WHERE bn.Id = @DocumentId

				/*Se valida si la nota asociada es de tipo detalle ya que se tiene informacion especifica de la factura*/
				IF EXISTS (SELECT 1
				FROM @TempNote
				WHERE NoteType =6)
				BEGIN
					INSERT INTO #Table_Result
					-- Productos (nota detalle, RecordType 2)
					SELECT
						0 AS StateResult,
						'OK' AS MessageResult,
						id.Id,
						i.Id DocumentId,
						p.Code,
						p.Name,
						CASE
						WHEN p.ATCId is null THEN COALESCE(p.CodeAlternative,p.CodeAlternativeTwo)
						ELSE  COALESCE(p.CodeAlternativeTwo, p.CodeAlternative)
						END CabysCode,
						2 TypeItem,
						'Unid' MeasureUnit,
						p.CodeAlternative,
						p.CodeAlternativeTwo,
						bg.Code + ' - ' + bg.Name BillingGroup,
						ISNULL(IIF(atc.Id IS NULL, '', p.HealthRegistration), '') MedicationRegistration,
						ISNULL(pfg.Code, '') DosageForm,
						'01' TransactionType,
						id.InvoicedQuantity,
						ROUND(pnarad.BaseValue/id.InvoicedQuantity,5) AS UnitValue,
						pnarad.BaseValue SalesPrice,
						pnarad.Value GrandTotalSalesPrice,
						-- Recalcular TaxValue: BaseValue * Porcentaje / 100 con 5 decimales para evitar errores de redondeo
						ROUND(pnarad.BaseValue * COALESCE(pnarad.TaxPercentage,0) / 100, 5) TaxValue,
						COALESCE(pnarad.TaxPercentage,0) IvaPercentage,
						CASE
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 0  THEN '11'
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 1  THEN '02'
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 2  THEN '03'
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 4  THEN '04'
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 8  THEN '07'
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 13 THEN '08'
							ELSE '08'
						END RateCode,
						0 DiscountValue,
						bnCte.CUFE ReferenceDocument,
						bnCte.ShippingDate ShippingDateReferenceDocument,
						ISNULL(tax.ApplyTaxDevolution, 0) ApplyTaxDevolution
					FROM @TempNote bnCte
						JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara on bnCte.PortfolioNoteId = pnara.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetail id on pnarad.EntityName ='InvoiceDetail' and pnarad.EntityId = id.Id
						JOIN Billing.ServiceOrderDetail sod on sod.Id=id.ServiceOrderDetailId
						JOIN Inventory.InventoryProduct p ON sod.ProductId = p.Id
						JOIN Billing.Invoice i on i.Id =id.InvoiceId
						LEFT JOIN Billing.ProductServiceDetail psd ON sod.Id = psd.ServiceOrderDetailId
						LEFT JOIN Contract.CUPSEntity cups ON psd.CUPSEntityId=cups.Id
						LEFT JOIN Billing.BillingGroup bg ON bg.Id = IIF(psd.CUPSEntityId IS NULL, p.BillingGroupId, cups.BillingGroupId)
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax ON tax.Id = pnarad.TaxId
						LEFT JOIN Inventory.ATC atc ON p.ATCId = atc.Id
						LEFT JOIN Inventory.PharmaceuticalForm pf ON atc.PharmaceuticalFormId = pf.Id
						LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg ON pf.PharmaceuticalFormGroupingId = pfg.Id
					WHERE bnCte.NoteType = 6 and sod.RecordType =2

					UNION ALL

					-- Servicios IPS (nota detalle, RecordType 1)
					SELECT
						0 AS StateResult,
						'OK' AS MessageResult,
						id.Id,
						i.Id DocumentId,
						ips.Code,
						ips.Name,
						ce.RIPSCode CabysCode,
						1 TypeItem,
						'Sp' MeasureUnit,
						ce.Code CodeAlternative,
						ce.RIPSCode CodeAlternativeTwo,
						bg.Code + ' - ' + bg.Name BillingGroup,
						'' MedicationRegistration,
						'' DosageForm,
						'01' TransactionType,
						id.InvoicedQuantity,
						ROUND(pnarad.BaseValue/id.InvoicedQuantity,5) AS UnitValue,
						pnarad.BaseValue SalesPrice,
						pnarad.Value GrandTotalSalesPrice,
						-- Recalcular TaxValue: BaseValue * Porcentaje / 100 con 5 decimales para evitar errores de redondeo
						ROUND(pnarad.BaseValue * COALESCE(pnarad.TaxPercentage,0) / 100, 5) TaxValue,
						COALESCE(pnarad.TaxPercentage,0) IvaPercentage,
						CASE
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 0  THEN '11'
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 1  THEN '02'
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 2  THEN '03'
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 4  THEN '04'
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 8  THEN '07'
							WHEN COALESCE(pnarad.TaxPercentage, 0) = 13 THEN '08'
							ELSE '08'
						END RateCode,
						0 DiscountValue,
						bnCte.CUFE ReferenceDocument,
						bnCte.ShippingDate ShippingDateReferenceDocument,
						ISNULL(tax.ApplyTaxDevolution, 0) ApplyTaxDevolution
					FROM @TempNote bnCte
						JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara on bnCte.PortfolioNoteId = pnara.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetail id on pnarad.EntityName ='InvoiceDetail' and pnarad.EntityId = id.Id
						JOIN Billing.ServiceOrderDetail sod on sod.Id=id.ServiceOrderDetailId
						JOIN Billing.Invoice i on i.Id =id.InvoiceId
						JOIN Contract.IPSService ips ON ips.Id = sod.IPSServiceId
						JOIN Contract.CUPSEntity ce ON ce.Id = sod.CUPSEntityId
						JOIN Billing.BillingGroup bg ON bg.Id = ce.BillingGroupId
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax ON tax.Id = pnarad.TaxId
					WHERE bnCte.NoteType = 6 and sod.RecordType =1
				END

				-- --------------------------------------------------------
				-- Nota genérica (NoteType <> 6) + reversión factura básica
				-- --------------------------------------------------------
				ELSE
				BEGIN
					UPDATE bnCte
						SET bnCte.OriginId = bbd.BasicBillingId
					FROM @TempNote bnCte
					JOIN Billing.InvoiceDetailBasicInvoice idbi ON bnCte.InvoiceId = idbi.InvoiceId
					JOIN Billing.BasicBillingDetail bbd ON idbi.BasicBillingDetailId = bbd.Id
					WHERE bnCte.NoteType = 1 AND bnCte.InvoiceType = 6

					--============ INSERT DETAILS ============--

					INSERT INTO #Table_Result
					-- Concepto de nota de portfolio
					SELECT
						0 AS StateResult,
						'OK' AS MessageResult,
						bnCte.Id,
						bnCte.DocumentId,
						pnc.Code,
						pnc.Name,
						pnc.AlternateCode CabysCode,
						1 TypeItem,
						'Os' MeasureUnit,
						pnc.AlternateCode CodeAlternative,
						pnc.AlternateCode CodeAlternativeTwo,
						NULL BillingGroup,
						'' MedicationRegistration,
						'' DosageForm,
						'01' TransactionType,
						1 InvoicedQuantity,
						bnCte.BillingValue UnitValue,
						bnCte.BillingValue SalesPrice,
						bnCte.BillingValue GrandTotalSalesPrice,
						ISNULL(pnd.IvaRate,0) TaxValue,
						ISNULL(((pnd.IvaRate / pnd.Value) * 100), 0) IvaPercentage,
						NULL RateCode,
						0 DiscountValue,
						bnCte.CUFE ReferenceDocument,
						bnCte.ShippingDate ShippingDateReferenceDocument,
						0 ApplyTaxDevolution
					-- Notas de portfolio no aplican devolución de IVA
					FROM @TempNote bnCte
						JOIN Portfolio.PortfolioNoteDetail pnd ON bnCte.PortfolioNoteId = pnd.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteConcept pnc ON pnc.Id = pnd.PortfolioNoteConceptId
					WHERE  bnCte.NoteType <> 6 AND NOT (bnCte.InvoiceType = 6 AND bnCte.NoteType = 1)

					--=================== REVERSION DE FACTURA BASICA
					UNION ALL

					-- Reversión: Productos
					SELECT
						0 AS StateResult,
						'OK' AS MessageResult,
						bbd.Id,
						bnCte.InvoiceId DocumentId,
						ip.Code,
						ip.Name,
						ISNULL(ip.CodeAlternativeTwo, ip.CodeAlternative) CabysCode,
						2 TypeItem,
						'Unid' MeasureUnit,
						ip.CodeAlternative,
						ip.CodeAlternativeTwo,
						NULL BillingGroup,
						ISNULL(IIF(atc.Id IS NULL, '', ip.HealthRegistration), '') MedicationRegistration,
						ISNULL(pfg.Code, '') DosageForm,
						'01' TransactionType,
						bbd.Quantity InvoicedQuantity,
						bbd.Price UnitValue,
						ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) SalesPrice,
						ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) GrandTotalSalesPrice,
						ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) TaxValue,
						bbd.PercentageIVA IvaPercentage,
						CASE
							WHEN bbd.PercentageIVA = 0  THEN '11'
							WHEN bbd.PercentageIVA = 1  THEN '02'
							WHEN bbd.PercentageIVA = 2  THEN '03'
							WHEN bbd.PercentageIVA = 4  THEN '04'
							WHEN bbd.PercentageIVA = 8  THEN '07'
							WHEN bbd.PercentageIVA = 13 THEN '08'
							ELSE '08'
						END RateCode,
						bbd.ValueDiscount DiscountValue,
						bnCte.CUFE ReferenceDocument,
						bnCte.ShippingDate ShippingDateReferenceDocument,
						ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
					FROM @TempNote bnCte
						JOIN Billing.BasicBilling bb ON bnCte.OriginId = bb.Id
						JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
						JOIN Inventory.InventoryProduct ip ON bbd.ProductId = ip.Id
						LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = ip.IVAId
						LEFT JOIN Inventory.ATC atc ON ip.ATCId = atc.Id
						LEFT JOIN Inventory.PharmaceuticalForm pf ON atc.PharmaceuticalFormId = pf.Id
						LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg ON pf.PharmaceuticalFormGroupingId = pfg.Id
					WHERE bbd.DetailType = 1

					UNION ALL

					-- Reversión: Servicios
					SELECT
						0 AS StateResult,
						'OK' AS MessageResult,
						bbd.Id,
						bnCte.InvoiceId DocumentId,
						bc.Code,
						bc.Name,
						cast(bc.AlternativeCode as VARCHAR(50)) CabysCode,
						1 TypeItem,
						'Sp' MeasureUnit,
						NULL CodeAlternative,
						NULL CodeAlternativeTwo,
						NULL BillingGroup,
						'' MedicationRegistration,
						'' DosageForm,
						'01' TransactionType,
						bbd.Quantity InvoicedQuantity,
						bbd.Price UnitValue,
						ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) SalesPrice,
						ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) GrandTotalSalesPrice,
						ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) TaxValue,
						bbd.PercentageIVA IvaPercentage,
						CASE
							WHEN bbd.PercentageIVA = 0  THEN '11'
							WHEN bbd.PercentageIVA = 1  THEN '02'
							WHEN bbd.PercentageIVA = 2  THEN '03'
							WHEN bbd.PercentageIVA = 4  THEN '04'
							WHEN bbd.PercentageIVA = 8  THEN '07'
							WHEN bbd.PercentageIVA = 13 THEN '08'
							ELSE '08'
						END RateCode,
						bbd.ValueDiscount DiscountValue,
						bnCte.CUFE ReferenceDocument,
						bnCte.ShippingDate ShippingDateReferenceDocument,
						ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
					FROM @TempNote bnCte
						JOIN Billing.BasicBilling bb ON bnCte.OriginId = bb.Id
						JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
						JOIN Billing.BillingConcept bc ON bbd.BillingConceptId = bc.Id
						LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = bc.IVAId
					WHERE bbd.DetailType = 2

					UNION ALL

					-- Reversión: Activos fijos
					SELECT
						0 AS StateResult,
						'OK' AS MessageResult,
						bbd.Id,
						bnCte.InvoiceId DocumentId,
						fai.Code,
						fai.Description Name,
						NULL CabysCode,
						2 TypeItem,
						'Unid' MeasureUnit,
						NULL CodeAlternative,
						NULL CodeAlternativeTwo,
						NULL BillingGroup,
						'' MedicationRegistration,
						'' DosageForm,
						'01' TransactionType,
						bbd.Quantity InvoicedQuantity,
						bbd.Price UnitValue,
						ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) SalesPrice,
						ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) GrandTotalSalesPrice,
						ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) TaxValue,
						bbd.PercentageIVA IvaPercentage,
						CASE
							WHEN bbd.PercentageIVA = 0  THEN '11'
							WHEN bbd.PercentageIVA = 1  THEN '02'
							WHEN bbd.PercentageIVA = 2  THEN '03'
							WHEN bbd.PercentageIVA = 4  THEN '04'
							WHEN bbd.PercentageIVA = 8  THEN '07'
							WHEN bbd.PercentageIVA = 13 THEN '08'
							ELSE '08'
						END RateCode,
						bbd.ValueDiscount DiscountValue,
						bnCte.CUFE ReferenceDocument,
						bnCte.ShippingDate ShippingDateReferenceDocument,
						ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
					FROM @TempNote bnCte
						JOIN Billing.BasicBilling bb ON bnCte.OriginId = bb.Id
						JOIN Billing.BasicBillingDetail bbd ON bb.Id = bbd.BasicBillingId
						JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.PhysicalAssetId = fapa.Id
						JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
						LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = fai.IVAId
					WHERE bbd.DetailType = 3
				END
			END
		END

	END TRY
	BEGIN CATCH
		DELETE FROM #Table_Result
		INSERT INTO #Table_Result
		SELECT 1, CONCAT('Se presento un error al intentar obtener los detalles del documento: ',ERROR_MESSAGE(), ' Línea: ',ERROR_LINE()),
			0, @DocumentId, '', '', '', 0, '', '', '', '', '', '', '', 0, 0, 0, 0, 0, 0, '', 0, '', '', 0
	END CATCH

	-- ============================================================
	-- Resultado final + columnas de exención tributaria
	-- ============================================================
	-- La condición para exponer la exención pasa de "TaxValue = 0" (ambiguo: puede ser por descuento,
	-- ítem no gravado, etc.) a "el tercero tiene exención real (@PercentageExemption > 0) y el ítem es gravado".
	-- Para terceros sin exención real, el resultado es idéntico al actual (sin cambios).
	SELECT *,
		IIF(@PercentageExemption > 0 AND IvaPercentage > 0, @DocumentTypeExemption, '00') DocumentTypeExemption,
		IIF(@PercentageExemption > 0 AND IvaPercentage > 0, @DocumentTypeExemptionOther, '') DocumentTypeExemptionOther,
		IIF(@PercentageExemption > 0 AND IvaPercentage > 0, @DocumentNumberExemption, '') DocumentNumberExemption,
		IIF(@PercentageExemption > 0 AND IvaPercentage > 0, @InstitutionCodeExemption, '') InstitutionCodeExemption,
		IIF(@PercentageExemption > 0 AND IvaPercentage > 0, @InstitutionNameExemption, '') InstitutionNameExemption,
		IIF(@PercentageExemption > 0 AND IvaPercentage > 0, @ArticleNumberExemption, 0) ArticleNumberExemption,
		IIF(@PercentageExemption > 0 AND IvaPercentage > 0, @ClauseNumberExemption, 0) ClauseNumberExemption,
		IIF(@PercentageExemption > 0 AND IvaPercentage > 0, @EmissionDateExemption, DATEFROMPARTS(1900,1,1)) EmissionDateExemption,
		CASE
			WHEN @PercentageExemption = 0 OR IvaPercentage = 0 THEN 0
			WHEN IvaPercentage > @PercentageExemption THEN ROUND(SalesPrice * @PercentageExemption / 100, 2)
			ELSE TaxValue
		END AmountExemption,
		CASE
			WHEN @PercentageExemption = 0 OR IvaPercentage = 0 THEN 0
			WHEN @PercentageExemption < IvaPercentage THEN CAST(@PercentageExemption AS INT)
			ELSE CAST(IvaPercentage AS INT)
		END PercentageExemption
	FROM #Table_Result
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle completo de un documento electrónico de facturación (factura, nota crédito o nota débito) para enviarlo al API de integración CIMA ante la DIAN. Consolida las líneas facturadas —servicios de salud (procedimientos CUPS), medicamentos e insumos— con sus valores netos, descuentos, impuestos (IVA) y precios de venta, tomando la información desde las facturas, el detalle de facturación y las órdenes de servicio. Adicionalmente, resuelve las exenciones tributarias del tercero pagador (EPS, aseguradora o empresa) consultando el tipo de exención, la resolución o artículo legal habilitante, la institución que la otorga y el porcentaje de IVA exento, para que el documento electrónico se emita con el tratamiento fiscal correcto. Se invoca durante el proceso de generación y envío de facturas electrónicas, garantizando que cada ítem del documento cumpla con la estructura requerida por la facturación electrónica en salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'GetElectronicDocumentDetailsInformation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'GetElectronicDocumentDetailsInformation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye y devuelve el detalle (líneas) de un documento electrónico (factura de servicios, factura básica, factura de productos o nota crédito/débito) consolidado para envío a la API de integración CIMA, incluyendo datos de exención tributaria.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetElectronicDocumentDetailsInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Billing.ElectronicDocument cuyo EntityId/EntityName coincida con los parámetros de entrada para resolver el tipo de documento y los datos de exención.; Para notas (DocumentType no ∈ {1,2,3,6,7}), el Id debe existir en Billing.BillingNote y se asume EntityName = ''Invoice'' o ''PortfolioNote''.; Las notas asociadas a PortfolioNote requieren existencia del registro en Portfolio.PortfolioNote vinculado por EntityId.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetElectronicDocumentDetailsInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #Table_Result: Cuando @DocumentType ∈ (1,2,3) (factura de prestación de servicios de salud) inserta dos UNION: servicios IPS (TypeItem=1, MeasureUnit=''Sp'') y productos de inventario (TypeItem=2, MeasureUnit=''Unid''), filtrando sod.SettlementType<>3, sod.IsDelete=0 y (id.GrandTotalSalesPrice>0 OR id.GrandTotalDiscount>0).; [INSERT] #Table_Result: Cuando @DocumentType=6 (factura básica) inserta tres UNION según bbd.DetailType: 1=Productos (InventoryProduct), 2=Servicios (BillingConcept), 3=Activos fijos (FixedAssetItem).; [INSERT] #Table_Result: Cuando @DocumentType=7 (factura de productos) inserta detalles desde Inventory.DocumentInvoiceProductSalesDetail con TypeItem=2 y MeasureUnit=''Unid''.; [INSERT] #Table_Result: Cuando @DocumentType no es 1,2,3,6 ni 7 (notas) y BillingNote.EntityName=''Invoice'', inserta el detalle desde InvoiceDetail/ServiceOrderDetail filtrando sod.SettlementType<>3, sod.IsDelete=0 y (id.GrandTotalSalesPrice>0 OR id.GrandTotalDiscount>0), con ReferenceDocument=bnd.CUFE y ShippingDateReferenceDocument=ed.ShippingDate.; [INSERT] #Table_Result: Cuando la nota está asociada a PortfolioNote y existe NoteType=6 (nota de detalle), inserta líneas desde Portfolio.PortfolioNoteAccountReceivableDetail filtrando sod.RecordType=2 (productos) y sod.RecordType=1 (servicios IPS).; [INSERT] #Table_Result: Cuando la nota está asociada a PortfolioNote y NoteType<>6, inserta una línea genérica desde PortfolioNoteDetail/PortfolioNoteConcept con TypeItem=1, MeasureUnit=''Os'' e InvoicedQuantity=1; si además NoteType=1 e InvoiceType=6 (reversión de factura básica) agrega tres UNION desde BasicBillingDetail por DetailType 1, 2 y 3.; [UPDATE] @TempNote: Cuando NoteType=1 e InvoiceType=6 actualiza OriginId con el BasicBillingId resuelto vía Billing.InvoiceDetailBasicInvoice para enlazar la nota con la factura básica original.; [DELETE] #Table_Result: En el bloque CATCH se borra el contenido previo de #Table_Result antes de insertar la fila de error.; [INSERT] #Table_Result: En CATCH inserta una fila con StateResult=1 y MessageResult concatenando ERROR_MESSAGE() y ERROR_LINE() para reportar el fallo al consumidor.; [RETURN_RESULT] #Table_Result: Al final retorna SELECT * de #Table_Result añadiendo columnas de exención (DocumentTypeExemption, DocumentNumberExemption, InstitutionCodeExemption, etc.) que se anulan o ponen por defecto cuando TaxValue=0; si IvaPercentage>@PercentageExemption, AmountExemption=ROUND(SalesPrice*@PercentageExemption/100,2); en caso contrario AmountExemption=TaxValue solo si @PercentageExemption>0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetElectronicDocumentDetailsInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverterWithDate', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetElectronicDocumentDetailsInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ElectronicDocument; Common.ThirdPartyTaxExemptions; Common.TaxExemptions; GeneralLedger.GeneralLedgerIVA; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Contract.IPSService; Contract.CUPSEntity; Billing.BillingGroup; GeneralLedger.CompanySettings; Inventory.InventoryProduct; Billing.ProductServiceDetail; Inventory.ATC; Inventory.PharmaceuticalForm; Inventory.PharmaceuticalFormGrouping; Billing.BasicBilling; Billing.BasicBillingDetail; Billing.BillingConcept; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail; Billing.BillingNote; Billing.BillingNoteDetail; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.PortfolioNoteAccountReceivableDetail; Portfolio.PortfolioNoteDetail; Portfolio.PortfolioNoteConcept (+1 adicionales)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetElectronicDocumentDetailsInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetElectronicDocumentDetailsInformation';
-- GO
