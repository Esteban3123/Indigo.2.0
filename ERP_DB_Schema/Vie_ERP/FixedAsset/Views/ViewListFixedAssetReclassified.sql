CREATE VIEW [FixedAsset].[ViewListFixedAssetReclassified]
AS
	SELECT
		fardb.Id
		, far.Id FixedAssetReclassificationId
		, fai.ItemCatalogId
		, fai.Id ItemId
		, fai.Code + ' - ' + fai.Description ItemCodeDescription
		, fapa.Id PhysicalAssetId
		, fapa.Serie
		, fapa.Plate
		, fapa.AdquisitionType
		, fapa.Status
		, lb.Id LegalBookId
		, lb.Code + ' - ' + lb.Name LegalBookCodeName
		, fardb.DepreciatedValue
		, fardb.ResidualValue
		, fardb.HistoricalValue
		, fapa.AdquisitionTypeReal
		, fapa.HasReclassified
		, fapa.HasOutput
		, fapa.OutputRefund
	FROM FixedAsset.FixedAssetReclassification far
	JOIN FixedAsset.FixedAssetReclassificationDetail fard ON far.Id = fard.FixedAssetReclassificationId
	JOIN FixedAsset.FixedAssetReclassificationDetailBook fardb ON fard.Id = fardb.FixedAssetReclassificationDetailId
	JOIN FixedAsset.FixedAssetItem fai ON far.ItemIdPrevious = fai.Id
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fard.PhysicalAssetId = fapa.Id	
	JOIN GeneralLedger.LegalBook lb ON fardb.LegalBookId = lb.Id