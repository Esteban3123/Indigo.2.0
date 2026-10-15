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
		
		IF @DocumentType IN (1,2,3) --Factura prestación de servicios salud
		BEGIN
		INSERT INTO #Table_Result
					SELECT 0 AS StateResult, 'OK' AS MessageResult,
				id.Id, i.Id DocumentId, ips.Code, ips.Name, ce.RIPSCode CabysCode, 1 TypeItem, 'Sp' MeasureUnit,
				NULL CodeAlternative, NULL CodeAlternativeTwo, bg.Code + ' - ' + bg.Name BillingGroup,
			'' MedicationRegistration, '' DosageForm, '01' TransactionType,
			sod.InvoicedQuantity, CAST(ROUND((id.NetWorth + id.GrandTotalDiscount)/id.InvoicedQuantity,5) AS DECIMAL(20,5)) UnitValue, id.NetWorth SalesPrice, id.GrandTotalSalesPrice,
			-- TaxValue: Usar el valor real almacenado, se calcula si es 0 para los terceros exentos
			IIF(id.GrandTotalTaxes = 0, ROUND(id.NetWorth * ISNULL(iva.Percentage, 0) / 100, 5), id.GrandTotalTaxes) TaxValue,
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
				id.GrandTotalDiscount DiscountValue, NULL ReferenceDocument, NULL ShippingDateReferenceDocument,
				ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
			FROM Billing.Invoice i WITH(NOLOCK)
				JOIN Billing.InvoiceDetail id WITH(NOLOCK) ON i.Id = id.InvoiceId
				JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
				JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
				JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId
				JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = ce.BillingGroupId
				JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) ON 1=1
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON iva.Id = ISNULL(id.TaxId,sod.IvaId)
			WHERE i.Id = @DocumentId
				AND sod.SettlementType <> 3
				AND sod.IsDelete = 0
				AND (id.GrandTotalSalesPrice > 0 OR id.GrandTotalDiscount > 0)
		UNION ALL
			SELECT 0 AS StateResult, 'OK' AS MessageResult,
				id.Id, i.Id DocumentId, p.Code, p.Name, IIF(p.ATCId is null, isnull(p.CodeAlternative,p.CodeAlternativeTwo), ISNULL(p.CodeAlternativeTwo, p.CodeAlternative)) CabysCode, 2 TypeItem, 'Unid' MeasureUnit,
				p.CodeAlternative, p.CodeAlternativeTwo, bg.Code + ' - ' + bg.Name BillingGroup,
			ISNULL(IIF(atc.Id IS NULL, '', p.HealthRegistration), '') MedicationRegistration, ISNULL(pfg.Code, '') DosageForm, '01' TransactionType,
			sod.InvoicedQuantity, CAST(ROUND((id.NetWorth + id.GrandTotalDiscount)/id.InvoicedQuantity,5) AS DECIMAL(20,5)) UnitValue, id.NetWorth SalesPrice, id.GrandTotalSalesPrice,
			-- TaxValue: Usar el valor real almacenado, se calcula si es 0 para los terceros exentos
			IIF(id.GrandTotalTaxes = 0, ROUND(id.NetWorth * ISNULL(iva.Percentage, 0) / 100, 5), id.GrandTotalTaxes) TaxValue,
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
				id.GrandTotalDiscount DiscountValue, NULL ReferenceDocument, NULL ShippingDateReferenceDocument,
				ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
			FROM Billing.Invoice i WITH(NOLOCK)
				JOIN Billing.InvoiceDetail id WITH(NOLOCK) ON i.Id = id.InvoiceId
				JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
				JOIN Inventory.InventoryProduct p WITH(NOLOCK) ON p.Id = sod.ProductId
				JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) ON 1=1
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON iva.Id = ISNULL(id.TaxId,sod.IvaId)
				LEFT JOIN Billing.ProductServiceDetail psd WITH(NOLOCK) ON sod.Id = psd.ServiceOrderDetailId
				LEFT JOIN Contract.CUPSEntity cups WITH(NOLOCK) ON psd.CUPSEntityId=cups.Id
				LEFT JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(psd.CUPSEntityId IS NULL, p.BillingGroupId, cups.BillingGroupId)
				LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON p.ATCId = atc.Id
				LEFT JOIN Inventory.PharmaceuticalForm pf WITH (NOLOCK) ON atc.PharmaceuticalFormId = pf.Id
				LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg WITH (NOLOCK) ON pf.PharmaceuticalFormGroupingId = pfg.Id
			WHERE i.Id = @DocumentId
				AND sod.SettlementType <> 3
				AND sod.IsDelete = 0
				AND (id.GrandTotalSalesPrice > 0 OR id.GrandTotalDiscount > 0)

	END 
		ELSE IF @DocumentType = 6 --Factura básica
		BEGIN
		INSERT INTO #Table_Result
							SELECT 0 AS StateResult, 'OK' AS MessageResult,
				bbd.Id, i.Id DocumentId, ip.Code, ip.Name, ISNULL(ip.CodeAlternativeTwo, ip.CodeAlternative) CabysCode, 2 TypeItem, 'Unid' MeasureUnit,
				ip.CodeAlternative, ip.CodeAlternativeTwo, NULL BillingGroup,
			ISNULL(IIF(atc.Id IS NULL, '', ip.HealthRegistration), '') MedicationRegistration, ISNULL(pfg.Code, '') DosageForm, '01' TransactionType,
			bbd.Quantity InvoicedQuantity, bbd.Price UnitValue, ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) SalesPrice, ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) GrandTotalSalesPrice,
			ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) TaxValue, bbd.PercentageIVA IvaPercentage,
			CASE
				WHEN bbd.PercentageIVA = 0  THEN '11'
				WHEN bbd.PercentageIVA = 1  THEN '02'
				WHEN bbd.PercentageIVA = 2  THEN '03'
				WHEN bbd.PercentageIVA = 4  THEN '04'
				WHEN bbd.PercentageIVA = 8  THEN '07'
				WHEN bbd.PercentageIVA = 13 THEN '08'
				ELSE '08'
			END RateCode,
				bbd.ValueDiscount DiscountValue, NULL ReferenceDocument, NULL ShippingDateReferenceDocument,
				ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
			FROM Billing.Invoice i WITH(NOLOCK)
				JOIN Billing.BasicBilling bb WITH(NOLOCK) ON i.Id = bb.InvoiceId
				JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) ON bb.Id = bbd.BasicBillingId
				JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON bbd.ProductId = ip.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON iva.Id = ip.IVAId
				LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
				LEFT JOIN Inventory.PharmaceuticalForm pf WITH (NOLOCK) ON atc.PharmaceuticalFormId = pf.Id
				LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg WITH (NOLOCK) ON pf.PharmaceuticalFormGroupingId = pfg.Id
			WHERE i.Id = @DocumentId
				AND bbd.DetailType = 1
			-- Productos
		UNION ALL
			SELECT 0 AS StateResult, 'OK' AS MessageResult,
			bbd.Id,
			i.Id DocumentId,
			bc.Code,
			bc.Name,
			cast(bc.AlternativeCode as VARCHAR(50)) CabysCode,
			1 TypeItem,
			'Sp' MeasureUnit,
			NULL CodeAlternative, NULL CodeAlternativeTwo, NULL BillingGroup,
			'' MedicationRegistration, '' DosageForm, '01' TransactionType,
			bbd.Quantity InvoicedQuantity, bbd.Price UnitValue, ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) SalesPrice, ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) GrandTotalSalesPrice,
			ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) TaxValue, bbd.PercentageIVA IvaPercentage,
			CASE
				WHEN bbd.PercentageIVA = 0  THEN '11'
				WHEN bbd.PercentageIVA = 1  THEN '02'
				WHEN bbd.PercentageIVA = 2  THEN '03'
				WHEN bbd.PercentageIVA = 4  THEN '04'
				WHEN bbd.PercentageIVA = 8  THEN '07'
				WHEN bbd.PercentageIVA = 13 THEN '08'
				ELSE '08'
			END RateCode,
				bbd.ValueDiscount DiscountValue, NULL ReferenceDocument, NULL ShippingDateReferenceDocument,
				ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
			FROM Billing.Invoice i WITH(NOLOCK)
				JOIN Billing.BasicBilling bb WITH(NOLOCK) ON i.Id = bb.InvoiceId
				JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) ON bb.Id = bbd.BasicBillingId
				JOIN Billing.BillingConcept bc WITH(NOLOCK) ON bbd.BillingConceptId = bc.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON iva.Id = bc.IVAId
			WHERE i.Id = @DocumentId
				AND bbd.DetailType = 2
		-- Servicios
		UNION ALL
			SELECT 0 AS StateResult, 'OK' AS MessageResult,
			bbd.Id, i.Id DocumentId, fai.Code, fai.Description Name, NULL CabysCode, 2 TypeItem, 'Unid' MeasureUnit,
			NULL CodeAlternative, NULL CodeAlternativeTwo, NULL BillingGroup,
			'' MedicationRegistration, '' DosageForm, '01' TransactionType,
			bbd.Quantity InvoicedQuantity, bbd.Price UnitValue, ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) SalesPrice, ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) GrandTotalSalesPrice,
			ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) TaxValue, bbd.PercentageIVA IvaPercentage,
			CASE
				WHEN bbd.PercentageIVA = 0  THEN '11'
				WHEN bbd.PercentageIVA = 1  THEN '02'
				WHEN bbd.PercentageIVA = 2  THEN '03'
				WHEN bbd.PercentageIVA = 4  THEN '04'
				WHEN bbd.PercentageIVA = 8  THEN '07'
				WHEN bbd.PercentageIVA = 13 THEN '08'
				ELSE '08'
			END RateCode,
				bbd.ValueDiscount DiscountValue, NULL ReferenceDocument, NULL ShippingDateReferenceDocument,
				ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
			FROM Billing.Invoice i WITH(NOLOCK)
				JOIN Billing.BasicBilling bb WITH(NOLOCK) ON i.Id = bb.InvoiceId
				JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) ON bb.Id = bbd.BasicBillingId
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH(NOLOCK) ON bbd.PhysicalAssetId = fapa.Id
				JOIN FixedAsset.FixedAssetItem fai WITH(NOLOCK) ON fapa.ItemId = fai.Id
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON iva.Id = fai.IVAId
			WHERE i.Id = @DocumentId
				AND bbd.DetailType = 3
	-- Activos fijos
	END
		ELSE IF @DocumentType = 7 -- Factura de productos
		BEGIN
		INSERT INTO #Table_Result
		SELECT 0 AS StateResult, 'OK' AS MessageResult,
			dipsd.Id, i.Id DocumentId, ip.Code, ip.Name, ISNULL(ip.CodeAlternativeTwo, ip.CodeAlternative) CabysCode, 2 TypeItem, 'Unid' MeasureUnit,
			ip.CodeAlternative, ip.CodeAlternativeTwo, NULL BillingGroup,
			ISNULL(IIF(atc.Id IS NULL, '', ip.HealthRegistration), '') MedicationRegistration, ISNULL(pfg.Code, '') DosageForm, '01' TransactionType,
			dipsd.Quantity InvoicedQuantity, dipsd.SalePrice UnitValue, ROUND(dipsd.Quantity * dipsd.SalePrice, 2) SalesPrice, ROUND(dipsd.Quantity * dipsd.SalePrice, 2) GrandTotalSalesPrice,
			ISNULL(dipsd.IvaValue, 0) TaxValue, ISNULL(dipsd.IvaPercentage, 0) IvaPercentage,
		CASE
			WHEN ISNULL(dipsd.IvaPercentage, 0) = 0  THEN '11'
			WHEN ISNULL(dipsd.IvaPercentage, 0) = 1  THEN '02'
			WHEN ISNULL(dipsd.IvaPercentage, 0) = 2  THEN '03'
			WHEN ISNULL(dipsd.IvaPercentage, 0) = 4  THEN '04'
			WHEN ISNULL(dipsd.IvaPercentage, 0) = 8  THEN '07'
			WHEN ISNULL(dipsd.IvaPercentage, 0) = 13 THEN '08'
			ELSE '08'
		END RateCode,
			dipsd.DiscountValue DiscountValue, NULL ReferenceDocument, NULL ShippingDateReferenceDocument,
			ISNULL(iva.ApplyTaxDevolution, 0) ApplyTaxDevolution
		FROM Billing.Invoice i WITH(NOLOCK)
			JOIN Inventory.DocumentInvoiceProductSales dips WITH(NOLOCK) ON i.Id = dips.InvoiceId
			JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH(NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
			JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON dipsd.ProductId = ip.Id
			LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON iva.Id = ip.IVAId
			LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
			LEFT JOIN Inventory.PharmaceuticalForm pf WITH (NOLOCK) ON atc.PharmaceuticalFormId = pf.Id
			LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg WITH (NOLOCK) ON pf.PharmaceuticalFormGroupingId = pfg.Id
		WHERE i.Id = @DocumentId
	END
		ELSE BEGIN
		--Notas
		DECLARE @EntityOrigin VARCHAR(250),
					@EntityOriginId INT

		SELECT @EntityOrigin = EntityName,
			@EntityOriginId = EntityId
		FROM Billing.BillingNote WITH(NOLOCK)
		WHERE Id = @DocumentId


		IF @EntityOrigin = 'Invoice'
			BEGIN
			INSERT INTO #Table_Result
							SELECT 0 AS StateResult,
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
				ISNULL(IIF(atc.Id IS NULL, '', p.HealthRegistration), '') MedicationRegistration, ISNULL(pfg.Code, '') DosageForm, '01' TransactionType,
				sod.InvoicedQuantity,
				CAST(ROUND((id.NetWorth + id.GrandTotalDiscount)/id.InvoicedQuantity,5) AS DECIMAL(20,5)) UnitValue,
				id.NetWorth SalesPrice,
				id.GrandTotalSalesPrice,
				-- TaxValue: Usar el valor real almacenado, se calcula si es 0 para los terceros exentos
				IIF(id.GrandTotalTaxes = 0, ROUND(id.NetWorth * ISNULL(iva.Percentage, 0) / 100, 5), id.GrandTotalTaxes) TaxValue,
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
				FROM Billing.BillingNote bn WITH (NOLOCK)
					JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
					JOIN Billing.Invoice i WITH (NOLOCK) ON i.Id = bnd.InvoiceId
					JOIN Billing.InvoiceDetail id WITH(NOLOCK) on I.Id=id.InvoiceId
					JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
					JOIN Inventory.InventoryProduct p WITH(NOLOCK) ON sod.ProductId = p.Id
					JOIN Billing.ElectronicDocument ed WITH (NOLOCK) ON i.Id = ed.EntityId AND 'Invoice' = ed.EntityName
					LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON iva.Id = ISNULL(id.TaxId,sod.IvaId)
					JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) ON 1=1
					LEFT JOIN Billing.ProductServiceDetail psd WITH(NOLOCK) ON sod.Id = psd.ServiceOrderDetailId
					LEFT JOIN Contract.CUPSEntity cups WITH(NOLOCK) ON psd.CUPSEntityId=cups.Id
					LEFT JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(psd.CUPSEntityId IS NULL, p.BillingGroupId, cups.BillingGroupId)
					LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON p.ATCId = atc.Id
					LEFT JOIN Inventory.PharmaceuticalForm pf WITH (NOLOCK) ON atc.PharmaceuticalFormId = pf.Id
					LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg WITH (NOLOCK) ON pf.PharmaceuticalFormGroupingId = pfg.Id
				WHERE bn.Id = @DocumentId
					AND sod.SettlementType <> 3
					AND sod.IsDelete = 0
					AND (id.GrandTotalSalesPrice > 0 OR id.GrandTotalDiscount > 0)

			UNION ALL

				SELECT 0 AS StateResult,
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
				'' MedicationRegistration, '' DosageForm, '01' TransactionType,
				sod.InvoicedQuantity,
				CAST(ROUND((id.NetWorth + id.GrandTotalDiscount)/id.InvoicedQuantity,5) AS DECIMAL(20,5)) UnitValue,
				id.NetWorth SalesPrice,
				id.GrandTotalSalesPrice,
				-- TaxValue: Usar el valor real almacenado, se calcula si es 0 para los terceros exentos
				IIF(id.GrandTotalTaxes = 0, ROUND(id.NetWorth * ISNULL(iva.Percentage, 0) / 100, 5), id.GrandTotalTaxes) TaxValue,
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
				FROM Billing.BillingNote bn WITH (NOLOCK)
					JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
					join Billing.Invoice i WITH(NOLOCK) ON i.Id = bnd.InvoiceId
					JOIN Billing.ElectronicDocument ed WITH (NOLOCK) ON i.Id = ed.EntityId AND 'Invoice' = ed.EntityName
					JOIN Billing.InvoiceDetail id WITH(NOLOCK) ON i.Id = id.InvoiceId
					JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
					JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
					JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId
					JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = ce.BillingGroupId
					JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) ON 1=1
					LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON iva.Id = ISNULL(id.TaxId,sod.IvaId)
				WHERE bn.Id = @DocumentId
					AND sod.SettlementType <> 3
					AND sod.IsDelete = 0
					AND (id.GrandTotalSalesPrice > 0 OR id.GrandTotalDiscount > 0)
		END
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
			select bn.Id DocumentId,
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
			FROM Billing.BillingNote bn WITH (NOLOCK)
				JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
				JOIN Billing.Invoice i WITH (NOLOCK) ON bnd.InvoiceId = i.Id
				JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
					OUTER APPLY (
						SELECT TOP 1
					ShippingDate
				FROM Billing.ElectronicDocument ed WITH (NOLOCK)
				WHERE ed.EntityId = bn.Id
					AND ed.EntityName = 'BillingNote'
				ORDER BY ed.Id DESC
					) ed
			WHERE bn.Id = @DocumentId

			/*Se valida si la nota asociada es de tipo detalle ya que se tiene informacion especifica de la factura*/
			IF EXISTS (SELECT 1
			FROM @TempNote
			WHERE NoteType =6) BEGIN
				INSERT INTO #Table_Result
									SELECT
						0 AS StateResult, 'OK' AS MessageResult,
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
						ISNULL(IIF(atc.Id IS NULL, '', p.HealthRegistration), '') MedicationRegistration, ISNULL(pfg.Code, '') DosageForm, '01' TransactionType,
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
					JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on bnCte.PortfolioNoteId = pnara.PortfolioNoteId
					JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
					JOIN Billing.InvoiceDetail id WITH(NOLOCK) on pnarad.EntityName ='InvoiceDetail' and pnarad.EntityId = id.Id
					JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
					JOIN Inventory.InventoryProduct p WITH(NOLOCK) ON sod.ProductId = p.Id
						JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =id.InvoiceId
						LEFT JOIN Billing.ProductServiceDetail psd WITH(NOLOCK) ON sod.Id = psd.ServiceOrderDetailId
						LEFT JOIN Contract.CUPSEntity cups WITH(NOLOCK) ON psd.CUPSEntityId=cups.Id
						LEFT JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(psd.CUPSEntityId IS NULL, p.BillingGroupId, cups.BillingGroupId)
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
						LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON p.ATCId = atc.Id
						LEFT JOIN Inventory.PharmaceuticalForm pf WITH (NOLOCK) ON atc.PharmaceuticalFormId = pf.Id
						LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg WITH (NOLOCK) ON pf.PharmaceuticalFormGroupingId = pfg.Id
					WHERE bnCte.NoteType = 6 and sod.RecordType =2

				UNION ALL

					SELECT
						0 AS StateResult, 'OK' AS MessageResult,
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
						'' MedicationRegistration, '' DosageForm, '01' TransactionType,
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
					JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on bnCte.PortfolioNoteId = pnara.PortfolioNoteId
					JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
					JOIN Billing.InvoiceDetail id WITH(NOLOCK) on pnarad.EntityName ='InvoiceDetail' and pnarad.EntityId = id.Id
					JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
					JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =id.InvoiceId
					JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
						JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId
						JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = ce.BillingGroupId
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
					WHERE bnCte.NoteType = 6 and sod.RecordType =1
			END
				ELSE BEGIN
				UPDATE bnCte
						SET bnCte.OriginId = bbd.BasicBillingId
					FROM @TempNote bnCte
					JOIN Billing.InvoiceDetailBasicInvoice idbi ON bnCte.InvoiceId = idbi.InvoiceId
					JOIN Billing.BasicBillingDetail bbd ON idbi.BasicBillingDetailId = bbd.Id
					WHERE bnCte.NoteType = 1 AND bnCte.InvoiceType = 6

				--============ INSERT DETAILS ============--

				INSERT INTO #Table_Result
																	SELECT
						0 AS StateResult, 'OK' AS MessageResult,
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
						'' MedicationRegistration, '' DosageForm, '01' TransactionType,
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
						JOIN Portfolio.PortfolioNoteDetail pnd WITH (NOLOCK) ON bnCte.PortfolioNoteId = pnd.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteConcept pnc WITH (NOLOCK) ON pnc.Id = pnd.PortfolioNoteConceptId
					WHERE  bnCte.NoteType <> 6 AND NOT (bnCte.InvoiceType = 6 AND bnCte.NoteType = 1)

					--=================== REVERSION DE FACTURA BASICA
				UNION ALL
					SELECT
						0 AS StateResult, 'OK' AS MessageResult,
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
						JOIN Billing.BasicBilling bb WITH(NOLOCK) ON bnCte.OriginId = bb.Id
						JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) ON bb.Id = bbd.BasicBillingId
						JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON bbd.ProductId = ip.Id
						LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON iva.Id = ip.IVAId
						LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
						LEFT JOIN Inventory.PharmaceuticalForm pf WITH (NOLOCK) ON atc.PharmaceuticalFormId = pf.Id
						LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg WITH (NOLOCK) ON pf.PharmaceuticalFormGroupingId = pfg.Id
					WHERE bbd.DetailType = 1
				-- Productos
				UNION ALL
					SELECT
						0 AS StateResult, 'OK' AS MessageResult,
					bbd.Id,
					bnCte.InvoiceId DocumentId,
					bc.Code,
					bc.Name,
					cast(bc.AlternativeCode as VARCHAR(50)) CabysCode,
					1 TypeItem,
					'Sp' MeasureUnit,
					NULL CodeAlternative, NULL CodeAlternativeTwo, NULL BillingGroup,
					'' MedicationRegistration, '' DosageForm, '01' TransactionType,
					bbd.Quantity InvoicedQuantity, bbd.Price UnitValue, ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) SalesPrice, ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) GrandTotalSalesPrice,
					ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) TaxValue, bbd.PercentageIVA IvaPercentage,
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
						JOIN Billing.BasicBilling bb WITH(NOLOCK) ON bnCte.OriginId = bb.Id
						JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) ON bb.Id = bbd.BasicBillingId
						JOIN Billing.BillingConcept bc WITH(NOLOCK) ON bbd.BillingConceptId = bc.Id
						LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON iva.Id = bc.IVAId
					WHERE bbd.DetailType = 2
				-- Servicios
				UNION ALL
					SELECT
						0 AS StateResult, 'OK' AS MessageResult,
						bbd.Id,
						bnCte.InvoiceId DocumentId,
					fai.Code, fai.Description Name, NULL CabysCode, 2 TypeItem, 'Unid' MeasureUnit,
					NULL CodeAlternative, NULL CodeAlternativeTwo, NULL BillingGroup,
					'' MedicationRegistration, '' DosageForm, '01' TransactionType,
					bbd.Quantity InvoicedQuantity, bbd.Price UnitValue, ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) SalesPrice, ROUND(bbd.Quantity * bbd.Price, bb.RoundLevel) GrandTotalSalesPrice,
					ROUND((bbd.Quantity * bbd.Price - bbd.ValueDiscount) * bbd.PercentageIVA / 100, bb.RoundLevel) TaxValue, bbd.PercentageIVA IvaPercentage,
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
						JOIN Billing.BasicBilling bb WITH(NOLOCK) ON bnCte.OriginId = bb.Id
						JOIN Billing.BasicBillingDetail bbd WITH(NOLOCK) ON bb.Id = bbd.BasicBillingId
						JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH(NOLOCK) ON bbd.PhysicalAssetId = fapa.Id
						JOIN FixedAsset.FixedAssetItem fai WITH(NOLOCK) ON fapa.ItemId = fai.Id
						LEFT JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) ON iva.Id = fai.IVAId
					WHERE bbd.DetailType = 3
			-- Activos fijos
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

	SELECT *,
		IIF(TaxValue = 0, '00', @DocumentTypeExemption) DocumentTypeExemption,
		IIF(TaxValue = 0, '', @DocumentTypeExemptionOther) DocumentTypeExemptionOther,
		IIF(TaxValue = 0, '', @DocumentNumberExemption) DocumentNumberExemption,
		IIF(TaxValue = 0, '', @InstitutionCodeExemption) InstitutionCodeExemption,
		IIF(TaxValue = 0, '', @InstitutionNameExemption) InstitutionNameExemption,
		IIF(TaxValue = 0, 0, @ArticleNumberExemption) ArticleNumberExemption,
		IIF(TaxValue = 0, 0, @ClauseNumberExemption) ClauseNumberExemption,
		IIF(TaxValue = 0, DATEFROMPARTS(1900,1,1), @EmissionDateExemption) EmissionDateExemption,
		CASE
			WHEN TaxValue = 0 THEN 0
			WHEN IvaPercentage > @PercentageExemption THEN ROUND(SalesPrice * @PercentageExemption / 100, 2)
			ELSE IIF(@PercentageExemption > 0, TaxValue, 0)
		END AmountExemption,
		CASE
			WHEN TaxValue = 0 THEN 0
			WHEN @PercentageExemption < IvaPercentage THEN CAST(@PercentageExemption AS INT)
			ELSE CAST(IvaPercentage AS INT)
		END PercentageExemption
	FROM #Table_Result
END
