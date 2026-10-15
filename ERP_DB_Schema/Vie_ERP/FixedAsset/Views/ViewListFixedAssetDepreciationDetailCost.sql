
















CREATE VIEW [FixedAsset].[ViewListFixedAssetDepreciationDetailCost]
AS

select faddc.Id,
CONCAT(fai.Code, ' - ', fai.Description) as CodeDescriptionItem,
fai.Code as CodeItem,
fai.[Description] as DescriptionItem,
fad.ClosingDate,
fadd.FixedAssetPhysicalAssetId,
faddc.CostCenterId,
faddc.DepreciationValue,
fad.ClosingYear,
fad.ClosingMonth,
fad.[Status],
fadd.LegalBookId
from FixedAsset.FixedAssetDepreciationDetailCost faddc
inner join FixedAsset.FixedAssetDepreciationDetail fadd on fadd.Id = faddc.FixedAssetDepreciationDetailId
inner join GeneralLedger.LegalBook lb on lb.Id = fadd.LegalBookId
inner join FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = fadd.FixedAssetPhysicalAssetId
inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
inner join FixedAsset.FixedAssetDepreciation fad on fad.Id = fadd.FixedAssetDepreciationId
where fad.Status = 2 and faddc.DepreciationValue > 0 and lb.OfficialBook = 1