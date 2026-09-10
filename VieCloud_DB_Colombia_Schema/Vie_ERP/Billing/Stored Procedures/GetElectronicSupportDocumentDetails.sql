-- ===============================================================================================================
-- Author:		Andrea Pahola Coqueco Cuellar
-- Create date: 2025-09-24
-- Description:	Procedimiento para obtener los detalles relacionados a un documento de soporte electrónico
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[GetElectronicSupportDocumentDetails]
		@EntityId INT,
		@EntityName VARCHAR(250)
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
			@PercentageExemption NUMERIC(3, 0),
			@Exempt BIT -- 0 Si paga impuestos 1 No paga impuestos

	IF  OBJECT_ID('tempdb..#Table_Result') IS NOT NULL DROP TABLE #TableResult

	CREATE TABLE #Table_Result
	(
		StateResult BIT,
		MessageResult VARCHAR(500),
		--------------------------------------
		EntityName VARCHAR(250),
		EntityCode VARCHAR(20),
	    --------------------------------------
		TariffHeading VARCHAR(12),
		CabysCode VARCHAR(50),
		MedicationRegistration VARCHAR(100),
		DosageForm VARCHAR(20),
		TransactionType VARCHAR(20),
		Quantity INT NOT NULL,
		MeasureUnit VARCHAR(20) NOT NULL,
		Name VARCHAR(500) NOT NULL,
		UnitValue DECIMAL(18,2) NOT NULL,
		SalesPrice DECIMAL(18,2) NOT NULL,
		-------------------------------------
		DiscountValue DECIMAL(18,2) NOT NULL,
		DiscountNature VARCHAR(80) NOT NULL,
		DiscountCode VARCHAR(20) NOT NULL,
		-------------------------------------
		Subtotal DECIMAL(18,2) NOT NULL,
		TaxBase DECIMAL(18,2) NOT NULL,
		-------------------------------------
		TaxCode VARCHAR(2),
		RateCode VARCHAR(20),
		IvaPercentage DECIMAL(4,2) NOT NULL, --Rate
		TaxValue DECIMAL(18,2) NOT NULL, --Amount
		IVAFactor decimal(4,2),
		ExportAmount INT,
		-------------------------------------
		FactoryChargedIva VARCHAR(2),
		AssumedTax DECIMAL(18,2),
		NetTax DECIMAL(18,2),
		LineTotalAmount DECIMAL(18,2), --0
		Service BIT,  -- 1 Servicio 0 Producto
		Bonus BIT, -- 1 Bonificación 0 -No bonificación
		Comment VARCHAR(500),
		ConceptName VARCHAR(500)
	)

	SELECT TOP 1
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
		@PercentageExemption = ISNULL(gliva.Percentage, 0),
		@Exempt = IIF(tp.ContributionType = 5, 1, 0)
	FROM Billing.ElectronicSupportDocument esd
	JOIN Common.ThirdParty tp ON esd.SupplierThirdPartyId = tp.Id
	LEFT JOIN Common.ThirdPartyTaxExemptions tpte ON esd.SupplierThirdPartyId = tpte.ThirdPartyId
	LEFT JOIN [Common].[TaxExemptions] te ON tpte.DocumentTypeId = te.Id
	LEFT JOIN [Common].[TaxExemptions] tei ON tpte.InstitutionId = tei.Id
	LEFT JOIN [GeneralLedger].[GeneralLedgerIVA] gliva ON tpte.ExemptFeeId = gliva.Id
	WHERE esd.EntityId = @EntityId AND esd.EntityName = @EntityName

	BEGIN TRY

		IF @EntityName = 'AccountPayable'
		BEGIN
			INSERT INTO #Table_Result
			-- Cuentas por pagar con origen Comprobante de Entrada
			SELECT	
					0 AS StateResult, 
					'OK' AS MessageResult,
					-----------------------
				   'AccountPayable'AS EntityName, 
				   ap.Code AS EntityCode, 
					--------------------------------------
					'' AS TariffHeading,
					ISNULL(apcc.CabysCode, '') CabysCode,
					IIF(a.Id IS NOT NULL, ISNULL(ipr.HealthRegistration, ''), '') AS MedicationRegistration,
					IIF(a.Id IS NOT NULL, ISNULL(pfg.Code, ''), '') AS DosageForm,
					'01' AS TransactionType,
					evd.Quantity,
					'Unid' MeasureUnit,
					ISNULL(apcc.CabysDescription, CONCAT(ipr.Code, ' - ' , ipr.Name)) AS Name,
					evd.UnitValue,
					evd.SubTotalValue SalesPrice,
					-------------------------------------
					evd.DiscountValue,
					IIF(evd.DiscountValue > 0, 'Descuento comercial', '') DiscountNature,
					IIF(evd.DiscountValue > 0, '07', '') DiscountCode,
					-------------------------------------
					evd.SubTotalValue AS Subtotal,
					evd.SubTotalValue AS TaxBase,
					-------------------------------------
					'07' AS TaxCode,
					ISNULL(iva.Code, '01') AS RateCode,
					evd.IvaPercentage,
					evd.IvaValue TaxValue, 
					0 IVAFactor,
					0 ExportAmount,
					-------------------------------------
					'' FactoryChargedIva,
					0 AssumedTax,
					0 NetTax,
					0 LineTotalAmount, --0
					0 Service,  -- 1 Servicio 0 Producto
					0 Bonus, -- 1 Bonificación 0 -No bonificación
					'' Comment,
					'' AS ConceptName 
			FROM Payments.AccountPayable ap
			JOIN Inventory.EntranceVoucher ev ON ap.Id = ev.AccountPayableId
			JOIN Inventory.EntranceVoucherDetail evd ON ev.Id = evd.EntranceVoucherId 
			JOIN Inventory.InventoryProduct ipr ON evd.ProductId = ipr.Id
			JOIN Inventory.ProductGroup pg ON ipr.ProductGroupId = pg.Id
			LEFT JOIN Inventory.ATC a ON ipr.ATCId = a.Id
			LEFT JOIN Inventory.PharmaceuticalForm pf ON a.PharmaceuticalFormId = pf.Id
			LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg ON pf.PharmaceuticalFormGroupingId = pfg.Id
			LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON ipr.IVAId = iva.Id
			LEFT JOIN Payments.AccountPayableConceptsCabys apcc ON pg.InventoryAccountPayableConceptId = apcc.AccountPayableConceptId
			WHERE ap.Id = @EntityId	
				AND ap.EntityName = 'EntranceVoucher'

			UNION ALL

			-- Cuentas por pagar con origen Activos fijos
			SELECT	
					0 AS StateResult, 
					'OK' AS MessageResult,
					-----------------------
				   'AccountPayable'AS EntityName, 
				   ap.Code AS EntityCode, 
					--------------------------------------
					'' TariffHeading,
					ISNULL(apcc.CabysCode, '') CabysCode,
					'' MedicationRegistration,
					'' DosageForm,
					'01' TransactionType,
					faei.Quantity,
					'Unid' MeasureUnit,
					ISNULL(apcc.CabysDescription, CONCAT(fai.Code, ' - ' , fai.Description)) AS  Name,
					faei.UnitValue,
					faei.SubTotalValue SalesPrice,
					-------------------------------------
					faei.DiscountValue,
					IIF(faei.DiscountValue > 0, 'Descuento comercial', '') DiscountNature,
					IIF(faei.DiscountValue > 0, '07', '') DiscountCode,
					-------------------------------------
					faei.SubTotalValue AS Subtotal,
					faei.SubTotalValue AS TaxBase,
					-------------------------------------
					'07' AS TaxCode,
					ISNULL(iva.Code, '01') AS RateCode,
					faei.IvaPercentage AS IvaPercentage,
					faei.IvaValue AS TaxValue, 
					0 AS IVAFactor,
					0 AS ExportAmount,
					-------------------------------------
					'' AS FactoryChargedIva,
					0 AS AssumedTax,
					0 AS NetTax,
					0 AS LineTotalAmount, --0
					0 AS Service,  -- 1 Servicio 0 Producto
					0 AS Bonus, -- 1 Bonificación 0 -No bonificación
					'' AS Comment,
					'' AS ConceptName 
			FROM Payments.AccountPayable ap
			JOIN FixedAsset.FixedAssetEntry fae ON ap.EntityId = fae.Id
			JOIN FixedAsset.FixedAssetEntryItem faei ON fae.Id = faei.FixedAssetEntryId
			JOIN FixedAsset.FixedAssetEntryItemDetail faeid	 ON faei.Id = faeid.FixedAssetEntryItemId
			JOIN FixedAsset.FixedAssetItem fai ON faei.ItemId = fai.Id
			JOIN FixedAsset.FixedAssetItemCatalog faic ON fai.ItemCatalogId = faic.Id
			JOIN GeneralLedger.GeneralLedgerIVA iva ON faei.IVAId = iva.Id
			LEFT JOIN Payments.AccountPayableConceptsCabys apcc ON faic.IncomeAccountPayableConceptId = apcc.AccountPayableConceptId
			WHERE ap.Id = @EntityId	
				AND ap.EntityName = 'FixedAssetEntry'

			UNION ALL

			-- Cuentas por pagar con otros origenes
			SELECT	
					0 AS StateResult, 
					'OK' AS MessageResult,
					-----------------------
				   'AccountPayable'AS EntityName, 
				   ap.Code AS EntityCode, 
					--------------------------------------
					'' TariffHeading,
					apdc.CabysCode CabysCode,
					'' MedicationRegistration,
					'' DosageForm,
					'01' TransactionType,
					1 Quantity,
					'Unid' MeasureUnit,
					apdc.CabysDescription Name,
					(apdc.DebitBaseValue - apdc.CreditBaseValue) UnitValue,
					(apdc.DebitValue - apdc.CreditValue) SalesPrice,
					-------------------------------------
					0 DiscountValue,
					'' DiscountNature,
					'' DiscountCode,
					-------------------------------------
					(apdc.DebitBaseValue - apdc.CreditBaseValue) Subtotal,
					(apdc.DebitBaseValue - apdc.CreditBaseValue) TaxBase,
					-------------------------------------
					IIF(apdc.HandleTaxes = 1, '07', '') TaxCode,
					IIF(apdc.HandleTaxes = 1,
						CASE
							WHEN apdc.IvaPercentage = 0  THEN '11'
							WHEN apdc.IvaPercentage = 1  THEN '02'
							WHEN apdc.IvaPercentage = 2  THEN '03'
							WHEN apdc.IvaPercentage = 4  THEN '04'

							WHEN apdc.IvaPercentage = 8  THEN '07'
							WHEN apdc.IvaPercentage = 13 THEN '08'
							ELSE '08'
						END, '') RateCode,
					IIF(apdc.HandleTaxes = 1, apdc.IvaPercentage, 0) IvaPercentage,
					IIF(apdc.HandleTaxes = 1, apdc.TaxValue, 0) TaxValue, 
					0 IVAFactor,
					0 ExportAmount,
					-------------------------------------
					'' FactoryChargedIva,
					0 AssumedTax,
					0 NetTax,
					0 LineTotalAmount, --0
					0 Service,  -- 1 Servicio 0 Producto
					0 Bonus, -- 1 Bonificación 0 -No bonificación
					'' Comment,
					apdc.ConceptName ConceptName 
			FROM Payments.AccountPayable ap
			JOIN 
			(
				SELECT 
					apdc.IdAccountPayable,
					ISNULL(apcc.CabysCode, '') CabysCode,
					ISNULL(apcc.CabysDescription, '') CabysDescription,
					SUM(IIF(apdc.Nature = 1, apdc.Value, 0)) DebitValue,
					SUM(IIF(apdc.Nature = 1, 0, apdc.Value)) CreditValue,
					SUM(IIF(apdc.Nature = 1, apdc.BaseValue, 0)) DebitBaseValue,
					SUM(IIF(apdc.Nature = 1, 0, apdc.BaseValue)) CreditBaseValue,
					SUM(ISNULL(apdc.IvaValue, 0)) TaxValue,
					ISNULL(apc.HandleTaxes, 0) HandleTaxes,
					ISNULL(iva.Percentage, 0) IvaPercentage,
					MAX(apc.Name) ConceptName
				FROM Payments.AccountPayableDetailConcept apdc
				JOIN Payments.AccountPayableConcepts apc ON apc.Id = apdc.IdConceptAccountPayable
				LEFT JOIN GeneralLedger.GeneralLedgerIVA iva ON iva.Id = apdc.RateIva
				LEFT JOIN Payments.AccountPayableConceptsCabys apcc ON apdc.IdConceptAccountPayable = apcc.AccountPayableConceptId
				GROUP BY apdc.IdAccountPayable, ISNULL(apcc.CabysCode, ''), ISNULL(apcc.CabysDescription, ''),
						 ISNULL(apc.HandleTaxes, 0), ISNULL(iva.Percentage, 0)
			) apdc ON ap.Id = apdc.IdAccountPayable
			WHERE ap.Id = @EntityId
				AND ISNULL(ap.EntityName, '') NOT IN ('EntranceVoucher', 'FixedAssetEntry') 

		END 

		ELSE IF @EntityName = 'VoucherTransaction'
		BEGIN

			INSERT INTO #Table_Result
			SELECT
					0 AS StateResult, 
					'OK' AS MessageResult,
					-----------------------
				   'VoucherTransaction'AS EntityName, 
					vt.Code AS EntityCode, 
					--------------------------------------
					'' TariffHeading, --Partida arancelaria
					'' CabysCode,
					'' MedicationRegistration,
					'' DosageForm,
					'01' TransactionType,
					0 Quantity,
					'Unid' MeasureUnit,
					CONCAT(vt.Code, ' - ', vt.Detail) Name,
					1 UnitValue,
					(vtd.DebitValue - vtd.CreditValue) SalesPrice,
					-------------------------------------
					0 DiscountValue,
					'' DiscountNature,
					'' DiscountCode,
					-------------------------------------
					(vtd.DebitValue - vtd.CreditValue) Subtotal,
					(vtd.DebitValue - vtd.CreditValue) TaxBase,
					-------------------------------------
					'' TaxCode,
					'' RateCode,
					0 IvaPercentage,
					0 TaxValue, 
					0 IVAFactor,
					0 ExportAmount,
					-------------------------------------
					'' FactoryChargedIva,
					0 AssumedTax,
					0 NetTax,
					0 LineTotalAmount, --0
					0 Service,  -- 1 Servicio 0 Producto
					0 Bonus, -- 1 Bonificación 0 -No bonificación
					'' Comment,
					'' AS ConceptName 
			FROM Treasury.VoucherTransaction vt
			JOIN 
			(
				SELECT 
					IdVoucherTransaction,
					SUM(IIF(vtd.Nature = 1, vtd.Value, 0)) DebitValue,
					SUM(IIF(vtd.Nature = 1, 0, vtd.Value)) CreditValue,
					0 TaxValue
				FROM Treasury.VoucherTransactionDetails vtd
				JOIN Treasury.ExpenseConcepts ec WITH(NOLOCK) ON vtd.IdExpenseConcept = ec.Id
				WHERE ec.Behavior = 6
				GROUP BY IdVoucherTransaction
			) vtd ON vt.Id = vtd.IdVoucherTransaction
			WHERE vt.Id = @EntityId
				AND vt.VoucherClass = 1

		END
		ELSE 
		BEGIN

			INSERT INTO #Table_Result
			SELECT 0 AS StateResult, 
					'OK' AS MessageResult,
					-----------------------
					NULL AS EntityName, 
					NULL AS EntityCode, 
					--------------------------------------
					'' TariffHeading, --Partida arancelaria
					'' CabysCode,
					'' MedicationRegistration,
					'' DosageForm,
					'01' TransactionType,
					1 Quantity,
					'Unid' MeasureUnit,
					'' Name,
					esd.SubTotalValue UnitValue,
					esd.SubTotalValue SalesPrice,
					-------------------------------------
					0 DiscountValue,
					'' DiscountNature,
					'' DiscountCode,
					-------------------------------------
					esd.SubTotalValue Subtotal,
					esd.SubTotalValue TaxBase,
					-------------------------------------
					'' TaxCode,
					'' RateCode,
					0 IvaPercentage,
					0 TaxValue, 
					0 IVAFactor,
					0 ExportAmount,
					-------------------------------------
					'' FactoryChargedIva,
					0 AssumedTax,
					0 NetTax,
					0 LineTotalAmount, --0
					0 Service,  -- 1 Servicio 0 Producto
					0 Bonus, -- 1 Bonificación 0 -No bonificación
					'' Comment,
					'' AS ConceptName 
			FROM Billing.ElectronicSupportDocument esd
			WHERE esd.Id = @EntityId

		END
	END TRY
	BEGIN CATCH
		DELETE FROM #Table_Result

		INSERT INTO #Table_Result(StateResult, MessageResult)
		SELECT 1, CONCAT('Se presento un error al intentar obtener los detalles del documento soporte: ',ERROR_MESSAGE(), ' Línea: ',ERROR_LINE())
	END CATCH 

	SELECT *,
		@DocumentTypeExemption DocumentTypeExemption,
		@DocumentTypeExemptionOther DocumentTypeExemptionOther,
		@DocumentNumberExemption DocumentNumberExemption,
		@InstitutionCodeExemption InstitutionCodeExemption,
		@InstitutionNameExemption InstitutionNameExemption,
		@ArticleNumberExemption ArticleNumberExemption,
		@ClauseNumberExemption ClauseNumberExemption,
		@EmissionDateExemption EmissionDateExemption,
		IIF(@PercentageExemption > 0, TaxValue, CAST(0 AS DECIMAL(18,2))) AmountExemption,
		CAST(@PercentageExemption AS INT) PercentageExemption,
		@Exempt Exempt
	FROM #Table_Result
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle completo de las líneas de un documento soporte electrónico de compras para su transmisión ante la DIAN. Recibe el identificador y el tipo de entidad origen (cuenta por pagar, comprobante de entrada de inventario u otro), y consolida la información de cada línea: descripción del bien o servicio, código CABYS, registro sanitario y forma farmacéutica (para medicamentos), cantidades, valores unitarios, descuentos, base gravable e impuestos (IVA). Para cuentas por pagar de origen genérico expone además ConceptName (nombre del concepto de Payments.AccountPayableConcepts asociado a cada línea agrupada), usado junto con el código CABYS para construir el nodo de referencia del documento electrónico; en las demás ramas (comprobante de entrada, activo fijo, comprobante de tesorería, caso por defecto) ConceptName se devuelve vacío. Además determina si el proveedor/tercero tiene exenciones tributarias vigentes, consultando el tipo y número del documento de exención, la institución que la ampara, el artículo o resolución legal, la tarifa aplicable y si el tercero está exento de IVA según su tipo de contribución. El resultado se usa para alimentar el XML o la estructura del documento soporte electrónico que se reporta a la autoridad tributaria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'GetElectronicSupportDocumentDetails';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'GetElectronicSupportDocumentDetails';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La línea de resultado siempre se devuelve con MeasureUnit=''Unid'' y TransactionType=''01''.; En orígenes distintos a ''EntranceVoucher'' y ''FixedAssetEntry'' (otros AccountPayable, VoucherTransaction y default) no se calcula IVA: TaxCode, RateCode quedan vacíos y IvaPercentage/TaxValue=0.; Para AccountPayable con EntranceVoucher o FixedAssetEntry, TaxCode siempre se fija en ''07'' y RateCode toma iva.Code (o ''01'' si es nulo).; Subtotal y TaxBase se igualan al SubTotalValue de la línea correspondiente (entrada, activo fijo o ESD).; Para VoucherTransaction solo se consideran detalles cuyo ExpenseConcepts.Behavior = 6 y vt.VoucherClass = 1.; Para AccountPayable con ''otros orígenes'' el valor unitario y subtotal se calcula como (DebitValue - CreditValue) agregado por concepto CABYS.; Service y Bonus siempre se devuelven en 0 (todas las líneas se reportan como producto y no bonificación).; Los datos de exención (tipo, número, institución, artículo, fecha, porcentaje) se calculan una sola vez para el ESD y se anexan a cada fila del resultado.; Si no existe registro en ThirdPartyTaxExemptions/TaxExemptions/GeneralLedgerIVA, se aplican defaults: códigos ''00'', textos vacíos, fecha 1900-01-01 y porcentaje 0.; EntityCode y EntityName del resultado se sobrescriben según el origen real (AccountPayable, VoucherTransaction) o quedan NULL en el caso por defecto.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetElectronicSupportDocumentDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento de soporte electrónico; Exención tributaria / IVA; Cuenta por pagar (AccountPayable); Comprobante de entrada de inventario; Activo fijo; Comprobante de tesorería (VoucherTransaction); Código CABYS (Catálogo de Bienes y Servicios de Costa Rica); Registro sanitario de medicamento; Forma farmacéutica / clasificación ATC; Descuento comercial; Tarifa y porcentaje de IVA; Tipo de contribución del tercero (exento); Bonificación vs servicio vs producto', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetElectronicSupportDocumentDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EntityName = ''AccountPayable'' → Construye el detalle desde Payments.AccountPayable, diferenciando tres orígenes: ap.EntityName=''EntranceVoucher'' (líneas desde Inventory.EntranceVoucherDetail con producto, ATC y forma farmacéutica), ap.EntityName=''FixedAssetEntry'' (líneas desde FixedAsset.FixedAssetEntryItem) y otros orígenes (agregando débito-crédito desde Payments.AccountPayableDetailConcept). else Si @EntityName=''VoucherTransaction'' arma el detalle desde Treasury.VoucherTransaction; en cualquier otro caso devuelve una línea genérica con SubTotalValue del propio Billing.ElectronicSupportDocument.; si te.InternalCode = ''99'' al obtener la exención del tercero → Asigna @DocumentTypeExemptionOther = tpte.Detail (descripción del ''otro'' tipo de documento de exención). else Asigna cadena vacía a @DocumentTypeExemptionOther.; si tp.ContributionType = 5 (proveedor exento) → Marca @Exempt = 1 (no paga impuestos). else Marca @Exempt = 0 (paga impuestos).; si @PercentageExemption > 0 al construir el resultado final → Devuelve AmountExemption = TaxValue de la línea (monto exento igual al IVA calculado). else Devuelve AmountExemption = 0.; si evd.DiscountValue > 0 (o faei.DiscountValue > 0) → Marca DiscountNature=''Descuento comercial'' y DiscountCode=''07'' en la línea. else Deja DiscountNature y DiscountCode como cadena vacía.; si Origen es comprobante de entrada y a.Id IS NOT NULL (producto con clasificación ATC) → Reporta MedicationRegistration=ipr.HealthRegistration y DosageForm=pfg.Code. else Reporta MedicationRegistration y DosageForm como cadena vacía.; si BEGIN CATCH activado por error en la construcción del detalle → Vacía #Table_Result e inserta una sola fila con StateResult=1 y MessageResult con el ERROR_MESSAGE() y ERROR_LINE() del fallo.; si Origen es Cuentas por pagar con ''otros orígenes'' (no EntranceVoucher ni FixedAssetEntry) → Reporta ConceptName = MAX(apc.Name) agrupado junto con CabysCode/CabysDescription, para poblar el nombre del concepto usado en el nodo de referencia del documento electrónico. else (EntranceVoucher, FixedAssetEntry, VoucherTransaction, caso por defecto) ConceptName se devuelve como cadena vacía.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetElectronicSupportDocumentDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ElectronicSupportDocument; Common.ThirdParty; Common.ThirdPartyTaxExemptions; Common.TaxExemptions; GeneralLedger.GeneralLedgerIVA; Payments.AccountPayable; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ATC; Inventory.PharmaceuticalForm; Inventory.PharmaceuticalFormGrouping; Payments.AccountPayableConceptsCabys; Payments.AccountPayableConcepts; FixedAsset.FixedAssetEntry; FixedAsset.FixedAssetEntryItem; FixedAsset.FixedAssetEntryItemDetail; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemCatalog; Payments.AccountPayableDetailConcept; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.ExpenseConcepts', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetElectronicSupportDocumentDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'GetElectronicSupportDocumentDetails';
-- GO
