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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de activos fijos físicos con su información de valoración contable para un año y mes determinados. Consolida datos del inventario de bienes (placa, tipo, catálogo, ubicación, responsable y estado del activo) con los valores de depreciación acumulada, ajuste por inflación, valor ajustado y valor residual registrados en el libro legal contable. Permite filtrar por rango de placa, ítem, tipo de ítem, ubicación, responsable, catálogo y estado del activo, facilitando la generación de informes de control patrimonial y conciliación contable de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_FixedAssetPhysicalAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_FixedAssetPhysicalAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de inventario físico de activos fijos con sus valores históricos, depreciación acumulada, depreciación del mes, adiciones y saldo en libros, filtrado por rangos y atributos del activo.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada activo físico debe tener su detalle de libro contable (FixedAssetPhysicalAssetDetailBook) asociado, ya que se une por INNER JOIN.; El activo debe tener Item, ItemType, ItemCatalog, Location, Responsible, StatusAsset y ThirdParty del responsable existentes (INNER JOIN).; El LegalBook referenciado en el detalle contable debe existir.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los rangos vacíos o nulos se normalizan a 0 (límite inferior) y 999999999 (límite superior).; La depreciación del mes se calcula solo con detalles cuyo ClosingMonth y ClosingYear coincidan con el período (@month/@year) solicitado.; Las adiciones del mes se calculan sobre transacciones cuyo DocumentDate corresponda al mes/año solicitado.; Solo se incluyen activos que tengan al menos un detalle de libro contable asociado.; El valor ''ATAccumulatedDepreciation'' siempre se reporta como 0 (placeholder).; El balance inicial de depreciación se calcula como ResidualValue + DepreciatedValue del libro contable.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Placa de activo; Depreciación acumulada; Depreciación mensual; Valor histórico de adquisición; Ajuste por inflación (PAAG); Valor residual / Saldo en libros; Libro contable (fiscal/NIIF); Catálogo de bienes y servicios; Tercero responsable (Nit); Ubicación del activo; Estado del activo (Activo/Inactivo/Devuelto); Adiciones por transacciones', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve una fila por activo físico que cumpla los rangos/filtros, con datos descriptivos, estado, y métricas de depreciación, adiciones y valores acumulados.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pa.Status = 0 → El estado del activo se reporta como ''Inactivo''.; si pa.Status = 1 AND pa.OutputRefund = 0 → El estado se reporta como ''Activo''. else Si Status=1 y OutputRefund<>0, se reporta como ''Devuelto''.; si No existe depreciación registrada para el activo → Se toma como año/mes de referencia el año/mes de la fecha de adquisición (AdquisitionDate). else Se toma el máximo ClosingYear/ClosingMonth de su historial de depreciación.; si @Responsible es NULL o vacío → No se filtra por responsable (cualquier Nit). else Se filtra por thi.Nit igual al parámetro.; si @ItemCatalog es NULL o vacío → No se filtra por catálogo de ítem. else Se filtra por c.Code igual al parámetro.; si @StatusAsset es NULL o vacío → No se filtra por estado del activo. else Se filtra por sa.Code igual al parámetro.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetItemCatalog; FixedAsset.FixedAssetPhysicalAssetDetailBook; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetResponsible; FixedAsset.FixedAssetStatusAsset; Common.ThirdParty; GeneralLedger.LegalBook; FixedAsset.FixedAssetCatalogOfPropertyandServices; FixedAsset.FixedAssetDepreciationDetail; FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetTransactionDetail; FixedAsset.FixedAssetTransaction', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_FixedAssetPhysicalAsset';
-- GO
