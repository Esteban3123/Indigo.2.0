
CREATE VIEW [FixedAsset].[ViewReportHistoricalDepreciation]
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte histórico de depreciación de activos fijos: consolida, por cada activo físico registrado (identificado por placa, serie y modelo), el historial acumulado de depreciaciones cerradas, mostrando el valor histórico del bien, el descuento financiero, la depreciación acumulada, el valor residual y los días depreciados versus los días de vida útil restante. Integra los procesos de depreciación (FixedAssetDepreciation y su detalle), el catálogo y tipo de activo, y las cuentas contables principales y de depreciación según el libro legal correspondiente (incluyendo homologación de cuentas para diferentes marcos normativos como NIIF o local). Está orientada a la generación de informes contables y de control patrimonial, permitiendo auditar el estado de depreciación histórica de cada activo por libro contable, período de cierre (año y mes) y tipo de adquisición (incluyendo leasing).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewReportHistoricalDepreciation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewReportHistoricalDepreciation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el histórico de depreciación de activos fijos cerrados, mostrando valores acumulados, días depreciados y vida útil restante por libro contable.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewReportHistoricalDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen depreciaciones con Status = 2 (cerradas/aprobadas); Cada activo físico tiene un detalle por libro contable en FixedAssetPhysicalAssetDetailBook; Las cuentas contables principales y de depreciación están definidas o homologadas en GeneralLedger.MainAccounts/HomologationAccount', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewReportHistoricalDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan depreciaciones con Status = 2; El cálculo de días depreciados acumulados se restringe al mismo libro contable y al mismo activo físico; El acumulado histórico considera el período actual y todos los anteriores del mismo Status; Las cuentas se filtran por el mismo LegalBookId del detalle de depreciación; Activos en leasing (AdquisitionType = 7) usan cuenta de depreciación distinta a los demás', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewReportHistoricalDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Depreciación de activos fijos; Activo físico (placa, serie, modelo); Valor histórico; Depreciación acumulada; Valor residual; Vida útil en días; Libro contable (legal book); Cuenta contable principal; Cuenta de depreciación; Leasing (AdquisitionType=7); Homologación de cuentas contables; Cierre contable (año/mes); Catálogo de activos fijos', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewReportHistoricalDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Solo retorna filas cuando fad.Status = 2 (depreciaciones cerradas); [RETURN_RESULT] (resultset): AccumulatedDepreciation se calcula como HistoricalValue - ResidualValue; [RETURN_RESULT] (resultset): RemainingLifeTime = (DepreciatedDays + DaysPendingDepreciate) - SUM(DepreciatedDays históricos); [RETURN_RESULT] (resultset): LifeTimeInDays = DepreciatedDays + DaysPendingDepreciate del detalle por libro; [RETURN_RESULT] (resultset): Acumula DepreciatedDays de períodos anteriores o iguales: ClosingYear > hd.ClosingYear OR (ClosingYear = hd.ClosingYear AND ClosingMonth >= hd.ClosingMonth)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewReportHistoricalDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si fapa.AdquisitionType <> 7 → La cuenta de depreciación se toma de faic.DepreciationAccountId (cuenta de depreciación estándar) else Cuando AdquisitionType = 7 se toma faic.DepreciationLeasingAccountId (cuenta de depreciación de leasing); si Existe homologación en HomologationAccount para una cuenta oficial → Se incluye también la cuenta interna homologada como posible match para MainAccount/DepreciationAccount else Solo se considera la cuenta directa de MainAccounts', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewReportHistoricalDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetDepreciationDetail; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemCatalog; FixedAsset.FixedAssetItemType; GeneralLedger.LegalBook; FixedAsset.FixedAssetPhysicalAssetDetailBook; GeneralLedger.MainAccounts; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewReportHistoricalDepreciation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewReportHistoricalDepreciation';
GO
