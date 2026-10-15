CREATE VIEW [FixedAsset].[VReportDepreciation]
AS
SELECT dd.Id,
		fi.code AS CodeItem,
		fi.[Description],
		ma.Number,
		ma.[Name] AS NameAccount,
		pa.Serie,
		pa.Plate,
		pa.Model,
		ft.Code,
		ft.[Name],
		(pa.HistoricalValue - pa.FinancialDiscount) HistoricalValue,
		dd.ResidualValue,
		(dd.DepreciationValue - dd.FinantialDiscountAdjusment) DepreciationValue,
		dd.DepreciatedDays,
		d.ClosingYear,
		d.ClosingMonth,
		gb.Code AS CodeLegalBook,
		gb.Name AS NameLegalBook
FROM FixedAsset.FixedAssetDepreciationDetail dd WITH(NOLOCK)
JOIN FixedAsset.FixedAssetDepreciation d WITH(NOLOCK) ON d.id = dd.FixedAssetDepreciationId
JOIN GeneralLedger.LegalBook gb WITH(NOLOCK) ON gb.Id = dd.LegalBookId
JOIN FixedAsset.FixedAssetPhysicalAsset pa WITH(NOLOCK) ON pa.Id = dd.FixedAssetPhysicalAssetId
JOIN FixedAsset.FixedAssetItem fi WITH(NOLOCK) ON fi.Id = pa.ItemId
JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pa.MainAccountId
JOIN FixedAsset.FixedAssetTrademark ft WITH(NOLOCK) ON ft.Id = pa.TrademarkId