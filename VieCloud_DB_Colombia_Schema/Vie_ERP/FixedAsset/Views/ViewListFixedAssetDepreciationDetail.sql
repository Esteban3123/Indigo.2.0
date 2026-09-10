

CREATE VIEW [FixedAsset].[ViewListFixedAssetDepreciationDetail]
AS

select fadd.Id,
CONCAT(fai.Code, ' - ', fai.Description) as CodeDescriptionItem,
fai.Code as CodeItem,
fai.[Description] as DescriptionItem,
fad.ClosingDate,
fadd.FixedAssetPhysicalAssetId,
fadd.DepreciationValue,
fad.ClosingYear,
fad.ClosingMonth,
fad.[Status],
fadd.LegalBookId
from FixedAsset.FixedAssetDepreciationDetail fadd
inner join GeneralLedger.LegalBook lb on lb.Id = fadd.LegalBookId
inner join FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = fadd.FixedAssetPhysicalAssetId
inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
inner join FixedAsset.FixedAssetDepreciation fad on fad.Id = fadd.FixedAssetDepreciationId
where fad.Status = 2 and fadd.DepreciationValue > 0 and lb.OfficialBook = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de depreciación de activos fijos aplicada: muestra el desglose por activo físico de las depreciaciones que ya fueron cerradas (estado 2) con valor de depreciación mayor a cero, registradas únicamente en el libro oficial contable. Integra el ítem del activo fijo (código y descripción), el activo físico asociado, el proceso de depreciación al que pertenece (con su fecha, año y mes de cierre) y el libro contable legal. Sirve para reportería contable y auditoría de la depreciación real aplicada a cada activo en cada período de cierre.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListFixedAssetDepreciationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListFixedAssetDepreciationDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de depreciaciones de activos fijos físicos cerradas y con valor positivo, restringido al libro contable oficial, enriquecido con la identificación del ítem.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en FixedAssetDepreciationDetail vinculados a un FixedAssetPhysicalAsset, su FixedAssetItem y un proceso de FixedAssetDepreciation; El LegalBook asociado al detalle debe estar marcado como libro oficial (OfficialBook = 1)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen depreciaciones cuyo proceso de cierre tiene Status = 2 (cerrado/oficial); Solo se exponen detalles con DepreciationValue estrictamente mayor a 0; Solo se consideran movimientos asociados al libro contable oficial (OfficialBook = 1); Todo detalle expuesto está asociado a un activo físico con su ítem maestro existente (joins INNER); La descripción concatenada del ítem siempre tiene el formato ''Code - Description''', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Activo físico; Depreciación; Detalle de depreciación; Cierre contable (año/mes); Libro contable oficial; Ítem de activo fijo', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.ViewListFixedAssetDepreciationDetail: Devuelve filas solo cuando fad.Status = 2 AND fadd.DepreciationValue > 0 AND lb.OfficialBook = 1', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetDepreciationDetail; GeneralLedger.LegalBook; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetDepreciation', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetDepreciationDetail';
GO
