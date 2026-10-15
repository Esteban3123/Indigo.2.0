
create VIEW [FixedAsset].[ViewReportHistoricalDepreciation]
AS

SELECT
	fadd.Id, fadd.LegalBookId,
	lb.Code LegalBookCode, lb.Name LegalBookName,
	fad.ClosingYear, fad.ClosingMonth,	
	fapa.Plate, fapa.Serie, fapa.Model,
	malo.Number MainAccountNumber, malo.Name MainAccountName,
	dalo.Number DepreciationAccountNumber, dalo.Name DepreciationAccountName,
	faic.Code CatalogCode, faic.Description CatalogDescription,
	fai.Code ItemCode, fai.Description ItemDescription,
	fait.Code TypeCode, fait.Name TypeName,
	fapa.HistoricalValue, fapa.FinancialDiscount, fapa.HistoricalValue - fadd.ResidualValue AccumulatedDepreciation,
	fadd.ResidualValue, fadd.DepreciationValue,

	SUM(hd.DepreciatedDays) DepreciatedDays, 
	(fapadb.DepreciatedDays + fapadb.DaysPendingDepreciate - SUM(hd.DepreciatedDays)) RemainingLifeTime, 
	(fapadb.DepreciatedDays + fapadb.DaysPendingDepreciate) LifeTimeInDays


FROM FixedAsset.FixedAssetDepreciation fad
INNER JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
INNER JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fadd.FixedAssetPhysicalAssetId = fapa.Id
INNER JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
INNER JOIN FixedAsset.FixedAssetItemCatalog faic ON fai.ItemCatalogId = faic.Id
INNER JOIN FixedAsset.FixedAssetItemType fait ON fai.ItemTypeId = fait.Id

INNER JOIN GeneralLedger.LegalBook lb ON fadd.LegalBookId = lb.Id
INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId AND fadd.LegalBookId = fapadb.LegalBookId

LEFT JOIN 
(
	SELECT malo.Id, malo.Number, malo.Name, malo.LegalBookId
	FROM GeneralLedger.MainAccounts malo
	UNION
	SELECT maloha.Id, maloha.Number, maloha.Name, maloha.LegalBookId
	FROM GeneralLedger.MainAccounts malo
	INNER JOIN GeneralLedger.HomologationAccount maha ON malo.Id = maha.OfficialMainAccountId
	INNER JOIN GeneralLedger.MainAccounts maloha ON maha.MainAccountId = maloha.Id
) malo ON fadd.LegalBookId = malo.LegalBookId AND fapa.MainAccountId = malo.Id


LEFT JOIN 
(
	SELECT malo.Id, malo.Number, malo.Name, malo.LegalBookId
	FROM GeneralLedger.MainAccounts malo
	UNION
	SELECT maloha.Id, maloha.Number, maloha.Name, maloha.LegalBookId
	FROM GeneralLedger.MainAccounts malo
	INNER JOIN GeneralLedger.HomologationAccount maha ON malo.Id = maha.OfficialMainAccountId
	INNER JOIN GeneralLedger.MainAccounts maloha ON maha.MainAccountId = maloha.Id
) dalo ON  fadd.LegalBookId = dalo.LegalBookId 
		AND ( (fapa.AdquisitionType <> 7 AND faic.DepreciationAccountId = dalo.Id) OR (fapa.AdquisitionType = 7 AND faic.DepreciationLeasingAccountId = dalo.Id) )

INNER JOIN (
	SELECT d.ClosingYear, d.ClosingMonth, d.Status, dd.LegalBookId, dd.FixedAssetPhysicalAssetId, SUM(dd.DepreciatedDays) DepreciatedDays
	FROM FixedAsset.FixedAssetDepreciation d
	INNER JOIN FixedAsset.FixedAssetDepreciationDetail dd
		ON d.Id = dd.FixedAssetDepreciationId
	GROUP BY d.ClosingYear, d.ClosingMonth, d.Status, dd.LegalBookId, dd.FixedAssetPhysicalAssetId			
) hd ON fad.Status = hd.Status 
	AND ((fad.ClosingYear > hd.ClosingYear) OR (fad.ClosingYear = hd.ClosingYear AND fad.ClosingMonth >= hd.ClosingMonth))
	AND fadd.LegalBookId = hd.LegalBookId AND fapa.Id = hd.FixedAssetPhysicalAssetId

WHERE fad.Status = 2	

GROUP BY fadd.Id, fadd.LegalBookId,
	lb.Code, lb.Name,
	fad.ClosingYear, fad.ClosingMonth,
	fapa.Plate, fapa.Serie, fapa.Model,
	malo.Number, malo.Name, malo.LegalBookId,
	dalo.Number, dalo.Name, dalo.LegalBookId,
	faic.Code, faic.Description,
	fai.Code, fai.Description,
	fait.Code, fait.Name,
	fadd.DepreciationValue, fadd.ResidualValue, fapa.HistoricalValue, fapa.FinancialDiscount,
	fapadb.DepreciatedDays, fapadb.DaysPendingDepreciate