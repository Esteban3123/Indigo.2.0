CREATE VIEW [Cost].[ViewCostDistributionFixedAsset]
AS
	SELECT
		fadd.FixedAssetPhysicalAssetId,
		fad.ClosingYear Year, fad.ClosingMonth Month,
		fapa.Plate,
		CONCAT(fai.Code, ' - ', fai.Description) FixedAssetItemCodeDescription,
		CONCAT(fal.Code, ' - ', fal.Name) FixedAssetLocationCodeName,
		CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName
	FROM FixedAsset.FixedAssetDepreciation fad
	JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
	JOIN GeneralLedger.LegalBook lb ON lb.Id = fadd.LegalBookId AND lb.OfficialBook = 1
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fadd.FixedAssetPhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetLocation fal ON fapa.LocationId = fal.Id
	JOIN FixedAsset.FixedAssetResponsible far ON fapa.ResponsibleId = far.Id
	JOIN Common.ThirdParty tp ON far.ThirdPartyId = tp.Id
	WHERE fad.Status = 2 AND fadd.DepreciationValue > 0