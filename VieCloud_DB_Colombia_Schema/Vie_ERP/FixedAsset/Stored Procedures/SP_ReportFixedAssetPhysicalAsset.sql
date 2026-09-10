
CREATE PROCEDURE [FixedAsset].[SP_ReportFixedAssetPhysicalAsset]
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
@Responsible VARCHAR(20),
@ItemCatalog VARCHAR(20),
@StatusAsset VARCHAR(20)

AS
BEGIN 

SELECT
pa.Serie, 
pa.Plate, 
i.[Description], 
(db.ResidualValue + db.DepreciatedValue) AS InitialBalanceDepreciation, 
lb.Name,
pa.AdquisitionDate,
--isnull((SELECT d.ClosingYear FROM FixedAsset.FixedAssetDepreciationDetail AS dd INNER JOIN FixedAsset.FixedAssetDepreciation AS d ON d.Id = dd.FixedAssetDepreciationId WHERE dd.FixedAssetPhysicalAssetId = pa.Id), YEAR(pa.AdquisitionDate)) AS 'year',
--isnull((SELECT d.ClosingMonth FROM FixedAsset.FixedAssetDepreciationDetail AS dd INNER JOIN FixedAsset.FixedAssetDepreciation AS d ON d.Id = dd.FixedAssetDepreciationId WHERE dd.FixedAssetPhysicalAssetId = pa.Id), MONTH(pa.AdquisitionDate)) AS 'month',
db.InflationAdjustmentValue AS 'PAAG',
pa.HistoricalValue AS 'valueAcquisition',
(SELECT SUM(VALUE) FROM FixedAsset.FixedAssetTransactionDetail WHERE PhysicalAssetId = pa.Id) AS 'additions',
(SELECT SUM(td.VALUE) FROM FixedAsset.FixedAssetTransactionDetail AS td INNER JOIN FixedAsset.FixedAssetTransaction AS t ON t.Id = td.FixedAssetTransactionId WHERE td.PhysicalAssetId = pa.Id AND MONTH(t.DocumentDate) = @month AND YEAR(t.DocumentDate) = @year) AS 'additionsMonth',
db.AdjustedValue AS 'accumulatedValueAsset',
db.InflationAdjustmentValue AS 'InflationAdjustmentAccumulatedAct',
db.AdjustedValue AS 'TotalValueAsset',
(SELECT COUNT(dd.Id) FROM FixedAsset.FixedAssetDepreciationDetail AS dd INNER JOIN FixedAsset.FixedAssetDepreciation AS d ON d.Id = dd.FixedAssetDepreciationId WHERE dd.FixedAssetPhysicalAssetId = pa.Id) AS 'period',
db.DepreciatedValue AS 'historicalDepreciated',
(SELECT SUM(dd.DepreciationValue) FROM FixedAsset.FixedAssetDepreciationDetail AS dd INNER JOIN FixedAsset.FixedAssetDepreciation AS d ON d.Id = dd.FixedAssetDepreciationId WHERE dd.FixedAssetPhysicalAssetId = pa.Id AND MONTH(d.ClosingMonth) = @month AND YEAR(d.ClosingYear) = @year) AS 'depreciationMonth',
'' AS 'accumulatedDepreciationMonthToMonth',
'' AS 'ATAccumulatedDepreciation',
db.ResidualValue AS 'balance books'
FROM
FixedAsset.FixedAssetPhysicalAsset AS pa
INNER JOIN FixedAsset.FixedAssetItem AS i ON i.Id = pa.ItemId
INNER JOIN FixedAsset.FixedAssetItemType AS it ON it.Id = i.ItemTypeId
INNER JOIN FixedAsset.FixedAssetItemCatalog AS c ON c.Id = i.ItemCatalogId
INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook AS db ON pa.Id = db.PhysicalAssetId
INNER JOIN FixedAsset.FixedAssetLocation AS l ON l.Id = pa.LocationId
INNER JOIN FixedAsset.FixedAssetResponsible AS r ON r.Id = pa.ResponsibleId
INNER JOIN FixedAsset.FixedAssetStatusAsset AS sa ON sa.id = pa.StatusAssetId
INNER JOIN Common.ThirdParty AS thi ON thi.Id = r.ThirdPartyId
INNER JOIN GeneralLedger.LegalBook AS lb ON lb.Id = db.LegalBookId
WHERE
pa.Id BETWEEN IIF(@PlateIdStart = 0, 0 ,ISNULL(@PlateIdStart,0)) AND IIF(@PlateIdEND = 0, 999999999, ISNULL(@PlateIdEND, 999999999))
And i.Id BETWEEN IIF(@ItemStart = 0, 0, ISNULL(@ItemStart,0)) and IIF(@ItemEnd = 0,999999999, ISNULL(@ItemEnd, 999999999))
AND it.Id BETWEEN IIF(@ItemTypeStart = 0, 0, ISNULL(@ItemTypeStart,0)) AND IIF(@ItemTypeEnd = 0, 999999999, ISNULL(@ItemTypeEnd, 999999999))
AND l.Id BETWEEN IIF(@LocationStart = 0, 0, ISNULL(@LocationStart,0)) AND IIF(@LocationEnd = 0, 999999999, ISNULL(@LocationEnd, 999999999))
AND (((@Responsible = 'Nothing' OR @Responsible = '') AND thi.Nit Like '%%') OR(thi.Nit = @Responsible))
AND (((@ItemCatalog = 'Nothing' OR @ItemCatalog = '') AND c.Code Like '%%') OR (c.Code = @ItemCatalog))
AND (((@StatusAsset = 'Nothing' OR @StatusAsset = '') AND sa.Code Like '%%') OR (sa.Code = @StatusAsset))
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de activos fijos físicos con su información contable y de depreciación para un período (año y mes) determinado. Combina datos del activo físico (placa, serie, fecha de adquisición, valor histórico) con su libro contable legal, tipo de ítem, catálogo, ubicación, responsable y estado del activo. Calcula el saldo inicial de depreciación, adiciones totales y del mes, valor ajustado por inflación (PAAG), depreciación histórica acumulada, depreciación del mes y saldo en libros (valor residual). Permite filtrar por rango de placas, rango de ítems, tipo de ítem, ubicación, responsable (NIT del tercero), catálogo de ítem y estado del activo, siendo el insumo principal para informes contables de inventario de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetPhysicalAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetPhysicalAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de inventario físico de activos fijos con saldos contables, depreciación acumulada, adiciones del mes y valores ajustados por inflación, filtrado por rangos y atributos del activo.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada activo físico debe tener un detalle contable asociado por libro legal (FixedAssetPhysicalAssetDetailBook).; Cada activo debe tener ítem, tipo de ítem, catálogo, ubicación, responsable y estado asignados (los JOINs son INNER).; El responsable debe tener un tercero (ThirdParty) vinculado.; Los parámetros de rango aceptan 0 o NULL para indicar ''sin límite'' (0 inferior, 999.999.999 superior).; Los filtros de texto admiten ''Nothing'' o cadena vacía para indicar ''todos''.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El saldo inicial de depreciación se calcula como ResidualValue + DepreciatedValue del libro contable.; Si el activo no tiene registros de depreciación, el año/mes reportado se toma de la fecha de adquisición (vía ISNULL en columnas comentadas).; Las adiciones del mes solo consideran transacciones cuyo DocumentDate coincida con @month y @year.; La depreciación del mes solo suma los detalles cuyo cierre (ClosingMonth/ClosingYear) coincida con @month y @year.; El reporte solo incluye activos con detalle contable por libro legal existente.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo físico; Placa y serie; Depreciación acumulada y mensual; Ajuste por inflación (PAAG); Valor histórico de adquisición; Valor residual / saldo en libros; Libro contable legal; Responsable del activo; Catálogo y tipo de ítem; Ubicación del activo; Estado del activo; Adiciones por transacciones', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada activo físico que cumpla los rangos de placa, ítem, tipo de ítem y ubicación, y los filtros de responsable (NIT), catálogo (Code) y estado (Code).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @PlateIdStart/@PlateIdEND, @ItemStart/@ItemEnd, @ItemTypeStart/@ItemTypeEnd o @LocationStart/@LocationEnd = 0 o NULL → Se sustituye por 0 (límite inferior) o 999999999 (límite superior), abriendo el rango.; si @Responsible = ''Nothing'' o ''''  → No filtra por NIT del responsable (acepta todos). else Filtra exactamente por thi.Nit = @Responsible.; si @ItemCatalog = ''Nothing'' o ''''  → No filtra por código de catálogo. else Filtra exactamente por c.Code = @ItemCatalog.; si @StatusAsset = ''Nothing'' o ''''  → No filtra por código de estado del activo. else Filtra exactamente por sa.Code = @StatusAsset.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetItemCatalog; FixedAsset.FixedAssetPhysicalAssetDetailBook; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetResponsible; FixedAsset.FixedAssetStatusAsset; Common.ThirdParty; GeneralLedger.LegalBook; FixedAsset.FixedAssetTransactionDetail; FixedAsset.FixedAssetTransaction; FixedAsset.FixedAssetDepreciationDetail; FixedAsset.FixedAssetDepreciation', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetPhysicalAsset';
-- GO
