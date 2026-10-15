CREATE VIEW [FixedAsset].[ViewListFixedAssetEntryItemDetail]
AS
	SELECT
		ROW_NUMBER() OVER (ORDER BY FixedAssetEntryId DESC) AS Id
	   ,*
	FROM (SELECT
			CONCAT(fai.Code, ' - ', fai.Description) AS CodeNameItem
		   ,faeid.Plate
		   ,faeid.Serie
		   ,fae.Id AS FixedAssetEntryId
		   ,faei.Id AS FixedAssetEntryItemId
		   ,faeid.Id AS FixedAssetEntryItemDetailId
		   ,0 AS FixedAssetEntryDevolutionDetailId
		   ,SelectOption = 1
		   ,faei.Quantity
		   ,CAST(ROUND(faei.UnitValue, 2) AS DECIMAL(18, 2)) AS UnitValue
		   ,CAST(ROUND((faei.SubTotalValue / faei.Quantity), 2) AS DECIMAL(18, 2)) AS SubTotalValue
		   ,faei.IvaPercentage
		   ,CAST(ROUND((faei.IvaValue / faei.Quantity), 2) AS DECIMAL(18, 2)) AS IvaValue
		   ,faei.DiscountPercentage
		   ,CAST(ROUND((faei.DiscountValue / faei.Quantity), 2) AS DECIMAL(18, 2)) AS DiscountValue
		   ,CAST(ROUND((faei.TotalValue / faei.Quantity), 2) AS DECIMAL(18, 2)) AS TotalValue
		   ,faei.RTFPercentage
		   ,CAST(ROUND((faei.RTFValue / faei.Quantity), 2) + ISNULL(adjustment.diffRTFValue, 0) AS DECIMAL(18, 2)) AS RTFValue
		   ,CAST(ROUND((fae.WithholdingTax / fat.Quantity), 2) + ISNULL(adjustment.diffWithholdingTax, 0) AS DECIMAL(18, 2)) AS WithholdingTax
		   ,CAST(ROUND((fae.WithholdingICA / fat.Quantity), 2) AS DECIMAL(18, 2)) AS WithholdingICA
		   --,CAST(ROUND((fae.RetentionSource / fat.Quantity), 0) + ISNULL(adjustment.diffRetentionSource, 0) AS DECIMAL(18, 2)) AS RetentionSource
		   ,CAST(adjustment2.sumRTFValue + ISNULL(adjustment.diffRTFValue, 2) AS DECIMAL(18, 2)) AS RetentionSource
		   ,CAST(ROUND(faei.SubTotalValue, 2) AS DECIMAL(18, 2)) AS SubTotalValueSource
		   ,CAST(ROUND(faei.IvaValue, 2) AS DECIMAL(18, 2)) AS IvaValueSource
		   ,CAST(ROUND(faei.DiscountValue, 2) AS DECIMAL(18, 2)) AS DiscountValueSource
		   ,CAST(ROUND(faei.TotalValue, 2) AS DECIMAL(18, 2)) AS TotalValueSource
		FROM FixedAsset.FixedAssetEntryItemDetail faeid
		INNER JOIN fixedasset.FixedAssetEntryItem faei ON faeid.FixedAssetEntryItemId = faei.Id
		INNER JOIN FixedAsset.FixedAssetItem fai ON faei.ItemId = fai.Id
		INNER JOIN FixedAsset.FixedAssetEntry fae ON faei.FixedAssetEntryId = fae.Id
		INNER JOIN 
		(
			SELECT faei.FixedAssetEntryId, SUM(faei.Quantity) Quantity 
			FROM FixedAsset.FixedAssetEntryItem faei 
			GROUP BY faei.FixedAssetEntryId
		) fat ON fae.Id = fat.FixedAssetEntryId
		JOIN
		(
			SELECT faei.Id,
				SUM(ROUND((faei.RTFValue / faei.Quantity), 2)) sumRTFValue
			FROM FixedAsset.FixedAssetEntryItemDetail faeid
			INNER JOIN fixedasset.FixedAssetEntryItem faei ON faeid.FixedAssetEntryItemId = faei.Id
			GROUP BY faei.Id
		) adjustment2 ON faei.Id = adjustment2.Id
		LEFT JOIN
		(
			SELECT MIN(faeid.Id) Id,
				fae.RetentionSource - SUM(ROUND((faei.RTFValue / faei.Quantity), 2)) diffRTFValue,
				fae.WithholdingTax - SUM(ROUND((fae.WithholdingTax / fat.Quantity), 2)) diffWithholdingTax
			FROM FixedAsset.FixedAssetEntryItemDetail faeid
			INNER JOIN fixedasset.FixedAssetEntryItem faei ON faeid.FixedAssetEntryItemId = faei.Id
			INNER JOIN FixedAsset.FixedAssetEntry fae ON faei.FixedAssetEntryId = fae.Id
			INNER JOIN 
			(
				SELECT faei.FixedAssetEntryId, SUM(faei.Quantity) Quantity 
				FROM FixedAsset.FixedAssetEntryItem faei 
				GROUP BY faei.FixedAssetEntryId
			) fat ON fae.Id = fat.FixedAssetEntryId
			GROUP BY fae.Id, fae.RetentionSource, fae.WithholdingTax
		) adjustment ON faeid.Id = adjustment.Id		
		WHERE fae.Status = 2 --and faeid.Refund = 0
	) AS datos