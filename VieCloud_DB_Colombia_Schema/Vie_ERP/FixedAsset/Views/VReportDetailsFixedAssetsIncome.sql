

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida el detalle de todos los ingresos de activos fijos a la organización, integrando dos fuentes: entradas directas de activos (compras con factura) y entradas por remisión (recepción sin factura inmediata). Para cada activo ingresado muestra la fecha del movimiento, número de documento, orden de compra asociada, proveedor de origen, ubicación o sede de destino, placa y serie del bien, modelo, marca, descripción del artículo, cantidad, subtotal, IVA, valor total, retención en la fuente (RTF), retención de renta (withholding tax), retención de ICA, estado del documento, tipo de adquisición y moneda (con su abreviatura ISO 4217). Sirve como base para reportes gerenciales y contables de incorporación de bienes de capital, permitiendo rastrear cada activo fijo desde su ingreso hasta su ubicación y el proveedor que lo suministró.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportDetailsFixedAssetsIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportDetailsFixedAssetsIncome';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un solo conjunto los detalles de ingresos de activos fijos provenientes tanto de entradas formales como de remisiones, para alimentar reportes con datos de documento, proveedor, ubicación, valores y tributos.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDetailsFixedAssetsIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada entrada (FixedAssetEntry) debe tener proveedor vía SuppliersDistributionLines, ubicación, ítem, marca y moneda válidos, y la moneda debe existir en ISO4217.; Cada remisión (FixedAssetRemissionEntrance) debe tener proveedor, ubicación, ítem, marca y una configuración SettingFixedAsset asociada a su OperatingUnit con moneda ISO4217 válida.; Quantity de los ítems de entrada y remisión debe ser distinto de cero (se usa como divisor para IVA, RTF y retenciones).; El total agregado de Quantity por FixedAssetEntry debe ser distinto de cero (se usa como divisor de WithholdingTax e ICA).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDetailsFixedAssetsIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo Row siempre es único por origen al concatenar el prefijo (''EI'' o ''RE'') con el Id del detalle.; El campo Type sólo toma los valores ''EI'' o ''RE''.; TotalValue siempre equivale a SubTotalValue + IvaValue unitario.; Los registros de remisión nunca aportan valores de retenciones (siempre 0).; La moneda reportada siempre debe existir en el catálogo ISO4217 (JOIN obligatorio).; Sólo se incluyen entradas/remisiones cuyo proveedor esté ligado a una línea de distribución de proveedores.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDetailsFixedAssetsIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Entrada de activo fijo; Remisión de entrada; Orden de compra; Proveedor; Línea de distribución de proveedores; Ubicación/Destino; Marca (Trademark); Placa y serie del bien; IVA; Retención en la fuente (WithholdingTax); Retención de ICA; Retención RTF; Moneda y código ISO 4217; Tipo de adquisición', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDetailsFixedAssetsIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.VReportDetailsFixedAssetsIncome: Cada fila de FixedAssetEntryItemDetail produce una fila tipo ''EI'' con prefijo ''EI'' en Row; cada fila de FixedAssetRemissionEntranceItemDetail produce una fila tipo ''RE'' con prefijo ''RE'' en Row.; [RETURN_RESULT] FixedAsset.VReportDetailsFixedAssetsIncome: Para registros tipo ''RE'' (remisiones) los valores RTFValue, WithholdingTax y WithholdingICA siempre se devuelven en 0, ya que estas retenciones solo aplican al flujo de entrada formal ''EI''.; [RETURN_RESULT] FixedAsset.VReportDetailsFixedAssetsIncome: SubTotalValue se calcula como UnitValue - DiscountValue; IvaValue como IvaValue/Quantity; TotalValue como (UnitValue - DiscountValue) + (IvaValue/Quantity).; [RETURN_RESULT] FixedAsset.VReportDetailsFixedAssetsIncome: Para ''EI'', WithholdingTax e WithholdingICA del documento se prorratean dividiendo entre la suma total de Quantity de todos los ítems de la misma entrada; RTFValue se prorratea dividiendo entre la Quantity del ítem.; [RETURN_RESULT] FixedAsset.VReportDetailsFixedAssetsIncome: Para ''EI'' la moneda proviene directamente de FixedAssetEntry.CurrencyId; para ''RE'' la moneda proviene de SettingFixedAsset según la OperatingUnit de la remisión.; [RETURN_RESULT] FixedAsset.VReportDetailsFixedAssetsIncome: La orden de compra (OrdenDeCompra) se incluye sólo si el ítem tiene PurchaseOrderItemId asociado (LEFT JOIN); de lo contrario será NULL.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDetailsFixedAssetsIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen de los datos: detalle de entrada de activo fijo (FixedAssetEntryItemDetail) → Se etiqueta Type=''EI'', se usa EntryDate como Fecha y se calculan retenciones (RTF, WithholdingTax, WithholdingICA) prorrateadas. else Si proviene de FixedAssetRemissionEntranceItemDetail, se etiqueta Type=''RE'', se usa RemisionDate como Fecha y las retenciones se fijan en 0.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDetailsFixedAssetsIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetEntryItemDetail; FixedAsset.FixedAssetEntryItem; FixedAsset.FixedAssetEntry; Common.SuppliersDistributionLines; Common.Supplier; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetTrademark; FixedAsset.FixedAssetPurchaseOrderItem; FixedAsset.FixedAssetPurchaseOrder; Common.Currency; Common.ISO4217; FixedAsset.FixedAssetRemissionEntranceItemDetail; FixedAsset.FixedAssetRemissionEntranceItem; FixedAsset.FixedAssetRemissionEntrance; FixedAsset.SettingFixedAsset', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDetailsFixedAssetsIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportDetailsFixedAssetsIncome';
GO
