CREATE VIEW [FixedAsset].[ViewListFixedAssetForReclassification]
AS
	SELECT DISTINCT
		CONCAT(fapa.Id, lb.Id) AS Id
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
		, IIF(fapa.Depreciate = 1, fapadb.DepreciatedValue, 0) DepreciatedValue
		, IIF(fapa.Depreciate = 1, fapadb.ResidualValue, 0) ResidualValue
		, IIF(fapa.Depreciate = 1, fapadb.HistoricalValue, fapa.HistoricalValue) HistoricalValue
		, fapa.AdquisitionTypeReal
		, fapa.HasReclassified
		, fapa.HasOutput
		, fapa.OutputRefund
	FROM FixedAsset.FixedAssetItem fai
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fai.Id = fapa.ItemId
	LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId
	LEFT JOIN GeneralLedger.LegalBook lb ON (fapa.Depreciate = 1 AND fapadb.LegalBookId = lb.Id) OR (fapa.Depreciate = 0 AND lb.OfficialBook = 1)