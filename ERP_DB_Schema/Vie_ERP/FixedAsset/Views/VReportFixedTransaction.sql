
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