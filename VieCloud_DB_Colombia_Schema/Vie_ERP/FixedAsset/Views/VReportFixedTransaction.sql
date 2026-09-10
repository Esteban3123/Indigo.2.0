
CREATE VIEW [FixedAsset].[VReportFixedTransaction]
AS
	SELECT
		ROW_NUMBER() OVER(ORDER BY pa.Plate ASC) Row,
		t.Id,
		thi.Nit,
		thi.[Name],
		CASE WHEN thi.ContributionType = 0 THEN 'Simplificado' WHEN thi.ContributionType = 1 THEN 'Común' WHEN thi.ContributionType = 2 THEN 'Empresa estatal' WHEN thi.ContributionType = 3 THEN 'Gran Contribuyente' END ContributionType,
		ISNULL((SELECT TOP 1 aTemp.Addresss FROM Common.[Address] aTemp WHERE aTemp.IdPerson = thi.PersonId ORDER BY aTemp.Id DESC), '') Adress,
		(SELECT TOP 1 pTemp.Phone FROM Common.Phone pTemp WHERE pTemp.IdPerson = thi.PersonId ORDER BY pTemp.Id DESC) Phone,
		t.GenerateAccountPayable,
		t.Code,
		t.DocumentDate,
		t.InvoiceNumber,
		t.[Status],
		pa.Plate,
		IIF(TransactionClass = 1, i.[Description], ac.Name) 'Description',
		td.[LifeTime],
		IIF(td.TransactionType = 2,'Desvalorizacion', CASE WHEN td.ValorizationType = 0 THEN 'No aplica' WHEN td.ValorizationType = 1 THEN 'Adicion' WHEN td.ValorizationType = 2 THEN 'Mantenimiento' WHEN td.ValorizationType = 3 THEN 'Mejora' WHEN td.ValorizationType = 4 THEN 'Reparacion' end) 'Type',
		td.Value 'Neto',
		ma.Number 'Acount',
		td.IvaPercentage,
		td.DiscountPercentage,
		td.Detail,
		t.Value,
		t.ValueDiscount,
		t.ValueTax 'IVA',
		t.RetentionSource,
		t.WithholdingICA,
		t.WithholdingTax,
		t.RetentionOther,
		t.DeductionOther,
		rcRetefte.MinBase 'RetefteMinBase',
		rcReteica.MinBase 'ReteicaMinBase',
		rcReteiva.MinBase 'ReteivaMinBase',
		rcRetefte.Rate 'RetefteRate',
		rcReteica.Rate 'ReteicaRate',
		rcReteiva.Rate 'ReteivaRate',
		t.CurrencyId,
		t.OperatingUnitId
	FROM FixedAsset.FixedAssetTransaction t
	INNER JOIN Common.ThirdParty thi ON thi.Id = t.ThirdPartyId
	INNER JOIN Common.Person p ON p.Id = thi.PersonId
	INNER JOIN FixedAsset.FixedAssetTransactionDetail td ON t.Id = td.FixedAssetTransactionId
	INNER JOIN GeneralLedger.MainAccounts ma ON ma.Id = td.AssetMainAccountId
	LEFT JOIN Common.Supplier s ON s.IdThirdParty = t.ThirdPartyId
	LEFT JOIN Common.SuppliersDistributionLines sdl ON sdl.Id = t.SupplierDistributionLineId
	LEFT JOIN Common.DistributionLines dl ON dl.Id = sdl.Id
	LEFT JOIN Common.DistributionLinesICARetention dlirReteica ON dlirReteica.OperatingUnitId = t.OperatingUnitId and dlirReteica.DistributionLineId = dl.Id
	LEFT JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = td.PhysicalAssetId
	LEFT JOIN FixedAsset.FixedAssetPhysicalAssetParts pab ON pab.Id = td.PhysicalAssetPartsId
	LEFT JOIN FixedAsset.FixedAssetPartsAccesoriesConsumables ac ON ac.Id = pab.PartAccesoriesConsumiblesId
	LEFT JOIN FixedAsset.FixedAssetItem i ON i.Id = pa.ItemId
	---------Retención en la fuente(Retefuente)----
	LEFT JOIN Payments.AccountPayableConcepts apcRetefte ON apcRetefte.Id = thi.IVARetentionAccountPayableConceptId
	LEFT JOIN GeneralLedger.RetentionConcepts rcRetefte ON rcRetefte.Id = apcRetefte.RetentionConceptId
	----------------------------------------------
	-------------Retención ICA(ReteICA)-----------
	LEFT JOIN Payments.AccountPayableConcepts apcReteica ON apcReteica.Id = dlirReteica.AccountPayableConceptId
	LEFT JOIN GeneralLedger.RetentionConcepts rcReteica ON rcReteica.Id = apcReteica.RetentionConceptId
	----------------------------------------------
	-------------Retención IVA(ReteIVA)-----------
	LEFT JOIN Payments.AccountPayableConcepts apcReteiva ON apcReteiva.Id = thi.IVARetentionAccountPayableConceptId
	LEFT JOIN GeneralLedger.RetentionConcepts rcReteiva ON rcReteiva.Id = apcReteiva.RetentionConceptId
	--------------------------------------------
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte detallado de transacciones de activos fijos que consolida, por cada movimiento (compra, valorización, desvalorización, mantenimiento, mejora o reparación), los datos del tercero/proveedor involucrado (NIT, nombre, tipo de contribuyente, dirección, teléfono), el activo físico identificado por placa, la cuenta contable principal afectada, los valores netos, descuentos e impuestos (IVA, Retefuente, ReteICA, ReteIVA) junto con sus bases mínimas y tarifas. Integra las tablas de transacciones de activos fijos, terceros, personas, proveedores, líneas de distribución contable y retenciones para ofrecer una vista completa del ciclo contable y tributario de cada operación sobre activos fijos. Sirve como base para reportes de auditoría, contabilización de activos, control de retenciones y generación de cuentas por pagar a proveedores de bienes de capital.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportFixedTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportFixedTransaction';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una vista de reporte las transacciones de activos fijos con sus detalles, datos del tercero/proveedor y bases y tarifas de retenciones (Retefuente, ReteICA, ReteIVA) para fines de informe contable y tributario.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada transacción debe tener un tercero (ThirdParty) y una persona asociada (Person) existentes; Cada transacción debe tener al menos un detalle en FixedAssetTransactionDetail con cuenta principal (MainAccounts) válida', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La dirección reportada es la última registrada (TOP 1 ORDER BY Id DESC) en Common.Address para la persona del tercero; si no hay, retorna cadena vacía; El teléfono reportado es el último registrado (TOP 1 ORDER BY Id DESC) en Common.Phone para la persona del tercero; La Retefuente y la ReteIVA se derivan del mismo concepto de cuenta por pagar del tercero (IVARetentionAccountPayableConceptId); La ReteICA se obtiene a través de la línea de distribución del proveedor filtrada por la unidad operativa de la transacción (OperatingUnitId); Solo se incluyen transacciones con tercero, persona, detalle y cuenta contable principal existentes (INNER JOIN); el resto de relaciones (proveedor, activo físico, retenciones) son opcionales', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Transacción de activo fijo; Tercero / Proveedor; Tipo de contribuyente (Simplificado, Común, Empresa estatal, Gran Contribuyente); Retención en la fuente (Retefuente); Retención ICA (ReteICA); Retención IVA (ReteIVA); IVA; Descuento; Valorización (Adición, Mantenimiento, Mejora, Reparación); Desvalorización; Vida útil; Cuenta contable (PUC); Línea de distribución; Unidad operativa; Placa de activo; Factura', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.VReportFixedTransaction: Devuelve una fila por cada detalle de transacción de activo fijo numerada con ROW_NUMBER ordenado por placa (Plate) ascendente', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si thi.ContributionType ∈ {0,1,2,3} → Traduce el tipo de contribuyente a etiqueta: 0=Simplificado, 1=Común, 2=Empresa estatal, 3=Gran Contribuyente else NULL; si TransactionClass = 1 → La descripción se toma de FixedAssetItem (i.Description, activo principal) else La descripción se toma de FixedAssetPartsAccesoriesConsumables (ac.Name, parte/accesorio); si td.TransactionType = 2 → El tipo se etiqueta como ''Desvalorizacion'' else Se mapea según ValorizationType: 0=No aplica, 1=Adicion, 2=Mantenimiento, 3=Mejora, 4=Reparacion', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetTransaction; FixedAsset.FixedAssetTransactionDetail; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetPhysicalAssetParts; FixedAsset.FixedAssetPartsAccesoriesConsumables; FixedAsset.FixedAssetItem; Common.ThirdParty; Common.Person; Common.Address; Common.Phone; Common.Supplier; Common.SuppliersDistributionLines; Common.DistributionLines; Common.DistributionLinesICARetention; GeneralLedger.MainAccounts; GeneralLedger.RetentionConcepts; Payments.AccountPayableConcepts', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedTransaction';
GO
