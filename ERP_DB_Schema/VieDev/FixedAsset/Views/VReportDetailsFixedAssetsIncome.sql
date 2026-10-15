

CREATE VIEW [FixedAsset].[VReportDetailsFixedAssetsIncome]
AS
	SELECT	CONCAT('EI', eid.Id) Row,
			'EI' as Type, 
			e.EntryDate AS 'Fecha',
			e.Code AS 'Documento',
			po.Code AS 'OrdenDeCompra',
			s.Name AS 'Origen',
			l.Name AS 'Destino',
			eid.Plate AS 'Placa',
			eid.Serie,
			ei.Model,
			ei.Quantity AS 'cant',
			i.[Description] AS 'Description',
			fat.Name Trademark,
			(ei.UnitValue - ei.DiscountValue) As 'SubTotalValue',
			(ei.IvaValue/ei.Quantity) As 'IvaValue',
			(ei.UnitValue - ei.DiscountValue) + (ei.IvaValue/ei.Quantity) As 'TotalValue',
			(ei.RTFValue / ei.Quantity) AS RTFValue,
			(e.WithholdingTax / fae.Quantity) AS WithholdingTax,
			(e.WithholdingICA / fae.Quantity) AS WithholdingICA,
			e.Code,
			e.[Status],
			s.code AS 'Proveedor',
			e.AdquisitionType,
			c.Abbreviation AS CurrencyAbbreviation,
				iso.CodeAbbreviation AS CodeAbbreviationISO
	FROM FixedAsset.FixedAssetEntryItemDetail AS eid 
	JOIN FixedAsset.FixedAssetEntryItem AS ei ON ei.Id = eid.FixedAssetEntryItemId
	JOIN FixedAsset.FixedAssetEntry AS e ON e.Id = ei.FixedAssetEntryId
	JOIN 
	(
		SELECT faei.FixedAssetEntryId, SUM(faei.Quantity) Quantity 
		FROM FixedAsset.FixedAssetEntryItem faei 
		GROUP BY faei.FixedAssetEntryId
	) fae ON e.Id = fae.FixedAssetEntryId
	JOIN Common.SuppliersDistributionLines AS sdl ON sdl.Id = e.SupplierDistributionLineId
	JOIN Common.Supplier AS s ON s.Id = sdl.IdSupplier
	JOIN FixedAsset.FixedAssetLocation AS l ON l.Id = e.LocationId
	JOIN FixedAsset.FixedAssetItem AS i ON i.Id = ei.ItemId 
	JOIN FixedAsset.FixedAssetTrademark fat ON ei.TrademarkId = fat.Id
	LEFT JOIN FixedAsset.FixedAssetPurchaseOrderItem AS oi ON oi.Id = ei.PurchaseOrderItemId
	LEFT JOIN FixedAsset.FixedAssetPurchaseOrder AS po ON po.Id = oi.PurchaseOrderId
	JOIN Common.Currency c ON c.Id = e.CurrencyId
	JOIN  Common.ISO4217 AS iso ON iso.CodeAbbreviation = c.Abbreviation

UNION ALL
	SELECT	CONCAT('RE', reid.Id) Row,
			'RE' as Type,
			re.RemisionDate AS 'Fecha',
			re.Code AS 'Documento',
			po.Code AS 'OrdenDeCompra',
			s.Name AS 'Origen',
			l.Name AS 'Destino', 
			reid.Plate AS 'Placa',
			reid.Serie,
			rei.Model,
			rei.Quantity AS 'cant',
			i.[Description] AS 'Description',
			fat.Name Trademark,
			(rei.UnitValue - rei.DiscountValue) As 'SubTotalValue',
			(rei.IvaValue/rei.Quantity) As 'IvaValue',
			(rei.UnitValue - rei.DiscountValue) + (rei.IvaValue/rei.Quantity) As 'TotalValue',
			0 AS RTFValue,
			0 AS WithholdingTax,
			0 AS WithholdingICA,
			re.Code,
			re.[Status],
			s.code As 'Proveedor',
			re.AdquisitionType,
			c.Abbreviation AS CurrencyAbbreviation,
			iso.CodeAbbreviation AS CodeAbbreviationISO

	FROM FixedAsset.FixedAssetRemissionEntranceItemDetail AS reid
	JOIN FixedAsset.FixedAssetRemissionEntranceItem AS rei ON rei.Id = reid.RemissionEntranceItemId
	JOIN FixedAsset.FixedAssetRemissionEntrance AS re ON re.Id = rei.RemissionEntranceId
	JOIN Common.SuppliersDistributionLines AS sdl ON sdl.Id = re.SupplierDistributionLineId
	JOIN Common.Supplier AS s ON s.Id = sdl.IdSupplier
	JOIN FixedAsset.FixedAssetLocation AS l ON l.Id = re.LocationId
	JOIN FixedAsset.FixedAssetItem AS i ON i.Id = rei.ItemId
	JOIN FixedAsset.FixedAssetTrademark fat ON rei.TrademarkId = fat.Id
	LEFT JOIN FixedAsset.FixedAssetPurchaseOrderItem AS oi ON oi.Id = rei.PurchaseOrderItemId
	LEFT JOIN FixedAsset.FixedAssetPurchaseOrder AS po ON po.Id = oi.PurchaseOrderId
	JOIN FixedAsset.SettingFixedAsset AS sfa on sfa.OperatingUnitId = re.OperatingUnitId
	JOIN Common.Currency c ON c.Id = sfa.CurrencyId
	JOIN  Common.ISO4217 AS iso ON iso.CodeAbbreviation = c.Abbreviation