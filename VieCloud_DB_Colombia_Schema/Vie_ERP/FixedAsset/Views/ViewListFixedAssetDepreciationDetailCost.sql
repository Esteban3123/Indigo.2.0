

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el detalle de depreciación de activos fijos distribuida por centro de costo, mostrando únicamente los registros de depreciaciones cerradas (estado 2) con valor de depreciación mayor a cero y registradas en el libro oficial contable. Combina información del ítem del activo (código y descripción), el activo físico, el proceso de depreciación (fecha de cierre, año y mes de cierre) y el valor depreciado asignado a cada centro de costo. Sirve para reportería contable y control de la depreciación mensual de activos fijos por centro de costo, garantizando que solo se consulten movimientos válidos y oficialmente reconocidos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListFixedAssetDepreciationDetailCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListFixedAssetDepreciationDetailCost';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los detalles de costos de depreciación de activos fijos por centro de costo, restringidos al libro contable oficial y a procesos de depreciación cerrados con valor depreciado positivo.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetailCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros de depreciación con Status = 2.; El libro contable asociado debe estar marcado como oficial (OfficialBook = 1).; El detalle de costo debe tener un valor de depreciación mayor a cero.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetailCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone detalles de depreciación cuyo proceso de cierre tiene Status = 2 (cerrado/oficializado).; Solo considera registros asociados al libro contable oficial (LegalBook.OfficialBook = 1).; Excluye detalles de costo cuyo valor de depreciación no sea estrictamente mayor a 0.; Cada fila se vincula obligatoriamente a un activo físico, su ítem maestro, un libro contable y un proceso de depreciación (joins INNER).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetailCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Depreciación; Centro de costo; Libro contable oficial; Cierre de depreciación; Activo físico; Período contable (mes/año de cierre)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetailCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.FixedAssetDepreciationDetailCost: Cuando fad.Status = 2 AND faddc.DepreciationValue > 0 AND lb.OfficialBook = 1, retorna el detalle de costo de depreciación con datos del activo físico, ítem y período de cierre.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetailCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetDepreciationDetailCost; FixedAsset.FixedAssetDepreciationDetail; GeneralLedger.LegalBook; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetDepreciation', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetailCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetailCost';
GO
