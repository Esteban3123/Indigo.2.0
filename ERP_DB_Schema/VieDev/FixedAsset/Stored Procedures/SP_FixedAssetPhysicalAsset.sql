CREATE PROCEDURE [FixedAsset].[SP_FixedAssetPhysicalAsset]
	@year int,
	@month int,
	@PlateIdStart int,
	@PlateIdEND int,
	@ItemStart int,
	@ItemEnd int,
	@ItemTypeStart int,
	@ItemTypeEnd int,
	@LocationStart int,
	@LocationEnd int,
	@Responsible int,
	@ItemCatalog int,
	@StatusAsset int
AS
BEGIN 
	SELECT	pa.Plate, 
			i.[Description], 
			pa.Serie, 
			pa.AdquisitionDate,
			CONCAT(thi.Nit, ' - ', thi.Name) ThirdPartyNitName,
			CONCAT(i.Code, ' - ', i.Description) ItemCodeName,
			CONCAT(c.Code, ' - ', c.Description) ItemCatalogCodeName,
			CONCAT(cps.code,' - ', cps.Name) CatalogOfPropertyandServicesCodeDescription,
			CONCAT(l.Code, ' - ', l.Name) LocationCodeName,
			CASE pa.Status
				WHEN 0 THEN 'Inactivo'
				WHEN 1 THEN Iif(pa.OutputRefund = 0, 'Activo', 'Devuelto')
			END AS StatusName,
			pa.EntryNumber,
			pa.PurchaseDate,
			pa.OutputDate,
			pa.HistoricalValue AS 'valueAcquisition',
			lb.Name,
			(db.ResidualValue + db.DepreciatedValue) AS InitialBalanceDepreciation, 
			isnull((SELECT MAX(d.ClosingYear) FROM FixedAsset.FixedAssetDepreciationDetail AS dd INNER JOIN FixedAsset.FixedAssetDepreciation AS d ON d.Id = dd.FixedAssetDepreciationId WHERE dd.FixedAssetPhysicalAssetId = pa.Id), YEAR(pa.AdquisitionDate)) AS 'year',
			isnull((SELECT MAX(d.ClosingMonth) FROM FixedAsset.FixedAssetDepreciationDetail AS dd INNER JOIN FixedAsset.FixedAssetDepreciation AS d ON d.Id = dd.FixedAssetDepreciationId WHERE dd.FixedAssetPhysicalAssetId = pa.Id), MONTH(pa.AdquisitionDate)) AS 'month',
			db.InflationAdjustmentValue AS 'PAAG',
			(SELECT SUM(VALUE) FROM FixedAsset.FixedAssetTransactionDetail WHERE PhysicalAssetId = pa.Id) AS 'additions',
			(SELECT SUM(td.VALUE) FROM FixedAsset.FixedAssetTransactionDetail AS td INNER JOIN FixedAsset.FixedAssetTransaction AS t ON t.Id = td.FixedAssetTransactionId WHERE td.PhysicalAssetId = pa.Id AND MONTH(t.DocumentDate) = @month AND YEAR(t.DocumentDate) = @year) AS 'additionsMonth',
			db.AdjustedValue AS 'accumulatedValueAsset',
			db.InflationAdjustmentValue AS 'InflationAdjustmentAccumulatedAct',
			db.AdjustedValue AS 'TotalValueAsset',
			(SELECT COUNT(dd.Id) FROM FixedAsset.FixedAssetDepreciationDetail AS dd INNER JOIN FixedAsset.FixedAssetDepreciation AS d ON d.Id = dd.FixedAssetDepreciationId WHERE dd.FixedAssetPhysicalAssetId = pa.Id) AS 'period',
			db.DepreciatedValue AS 'historicalDepreciated',
			(SELECT SUM(dd.DepreciationValue) FROM FixedAsset.FixedAssetDepreciationDetail AS dd INNER JOIN FixedAsset.FixedAssetDepreciation AS d ON d.Id = dd.FixedAssetDepreciationId WHERE dd.FixedAssetPhysicalAssetId = pa.Id AND MONTH(d.ClosingMonth) = @month AND YEAR(d.ClosingYear) = @year) AS 'depreciationMonth',
			db.DepreciatedValue AS 'accumulatedDepreciationMonthToMonth',
			0 AS 'ATAccumulatedDepreciation',
			db.ResidualValue AS 'balance books'
	FROM FixedAsset.FixedAssetPhysicalAsset AS pa
	INNER JOIN FixedAsset.FixedAssetItem AS i ON i.Id = pa.ItemId
	INNER JOIN FixedAsset.FixedAssetItemType AS it ON it.Id = i.ItemTypeId
	INNER JOIN FixedAsset.FixedAssetItemCatalog AS c ON c.Id = i.ItemCatalogId
	INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook AS db ON pa.Id = db.PhysicalAssetId
	INNER JOIN FixedAsset.FixedAssetLocation AS l ON l.Id = pa.LocationId
	INNER JOIN FixedAsset.FixedAssetResponsible AS r ON r.Id = pa.ResponsibleId
	INNER JOIN FixedAsset.FixedAssetStatusAsset AS sa ON sa.id = pa.StatusAssetId
	INNER JOIN Common.ThirdParty AS thi ON thi.Id = r.ThirdPartyId
	INNER JOIN GeneralLedger.LegalBook AS lb ON lb.Id = db.LegalBookId
	LEFT JOIN FixedAsset.FixedAssetCatalogOfPropertyandServices cps ON cps.Id = i.CatalogOfPropertyandServicesId
	WHERE pa.Id BETWEEN IIF(@PlateIdStart = '', 0 ,ISNULL(@PlateIdStart,0)) AND IIF(@PlateIdEND = '', 999999999, ISNULL(@PlateIdEND, 999999999))
		And i.Id BETWEEN IIF(@ItemStart = '', 0, ISNULL(@ItemStart,0)) and IIF(@ItemEnd = '',999999999, ISNULL(@ItemEnd, 999999999))
		AND it.Id BETWEEN IIF(@ItemTypeStart = '', 0, ISNULL(@ItemTypeStart,0)) AND IIF(@ItemTypeEnd = '', 999999999, ISNULL(@ItemTypeEnd, 999999999))
		AND l.Id BETWEEN IIF(@LocationStart = '', 0, ISNULL(@LocationStart,0)) AND IIF(@LocationEnd = '', 999999999, ISNULL(@LocationEnd, 999999999))
		AND (((@Responsible IS NULL OR @Responsible = '') AND thi.Nit Like '%%') OR(thi.Nit = @Responsible))
		AND (((@ItemCatalog IS NULL OR @ItemCatalog = '') AND c.Code Like '%%') OR (c.Code = @ItemCatalog))
		AND (((@StatusAsset IS NULL OR @StatusAsset = '') AND sa.Code Like '%%') OR (sa.Code = @StatusAsset))
END