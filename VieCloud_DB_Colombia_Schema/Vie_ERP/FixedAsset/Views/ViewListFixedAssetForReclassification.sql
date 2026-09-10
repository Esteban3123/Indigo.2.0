CREATE VIEW [FixedAsset].[ViewListFixedAssetForReclassification]
AS
	SELECT DISTINCT
		CONCAT(fapa.Id, lb.Id) AS Id
		, fai.ItemCatalogId
		, fai.Id ItemId
		, fai.Code + ' - ' + fai.Description ItemCodeDescription
		, fapa.Id PhysicalAssetId
		, fapa.Serie
		, fapa.Plate
		, fapa.AdquisitionType
		, fapa.Status
		, lb.Id LegalBookId
		, lb.Code + ' - ' + lb.Name LegalBookCodeName
		, IIF(fapa.Depreciate = 1, fapadb.DepreciatedValue, 0) DepreciatedValue
		, IIF(fapa.Depreciate = 1, fapadb.ResidualValue, 0) ResidualValue
		, IIF(fapa.Depreciate = 1, fapadb.HistoricalValue, fapa.HistoricalValue) HistoricalValue
		, fapa.AdquisitionTypeReal
		, fapa.HasReclassified
		, fapa.HasOutput
		, fapa.OutputRefund
	FROM FixedAsset.FixedAssetItem fai
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fai.Id = fapa.ItemId
	LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId
	LEFT JOIN GeneralLedger.LegalBook lb ON (fapa.Depreciate = 1 AND fapadb.LegalBookId = lb.Id) OR (fapa.Depreciate = 0 AND lb.OfficialBook = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que lista los activos fijos físicos disponibles para ser reclasificados, combinando el catálogo de ítems de activos fijos con su información física (serie, placa, tipo de adquisición, estado) y los valores contables por libro legal (valor histórico, valor depreciado y valor residual). Para activos que deprecian, toma los valores del detalle contable por libro; para los que no deprecian, utiliza el libro oficial y el valor histórico del activo físico. Sirve como fuente de datos para el proceso de reclasificación contable de activos fijos, indicando además si el activo ya fue reclasificado, si tiene salida registrada o si tiene devolución de salida.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListFixedAssetForReclassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListFixedAssetForReclassification';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Provee el listado de activos fijos físicos elegibles para reclasificación contable, mostrando por cada libro contable aplicable sus valores histórico, depreciado y residual, junto con su estado de reclasificación, salida y devolución.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetForReclassification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada activo físico debe estar vinculado a un ítem del catálogo de activos fijos.; Los activos marcados como depreciables deben tener registros en el detalle por libro contable para reportar valores distintos de cero.; Debe existir al menos un libro contable marcado como oficial (OfficialBook = 1) para los activos no depreciables.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetForReclassification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Para activos no depreciables, los valores depreciado y residual siempre se reportan como 0.; Para activos no depreciables, el valor histórico proviene del activo físico, no del detalle por libro.; Para activos depreciables, los valores monetarios se obtienen del detalle por libro contable correspondiente.; Cada fila combina un activo físico con un libro contable: el de su detalle si deprecia, o el libro oficial si no deprecia.; El identificador de la fila es único por la combinación activo físico + libro (CONCAT de ambos Id).; Se incluye la trazabilidad del estado de reclasificación, salida y devolución de salida del activo.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetForReclassification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Reclasificación contable de activos fijos; Depreciación; Valor histórico; Valor residual; Valor depreciado; Libro contable / Libro oficial; Salida de activo; Devolución de salida; Tipo de adquisición; Placa y serie del activo', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetForReclassification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.FixedAssetPhysicalAsset: Devuelve una fila por combinación activo físico + libro contable; si Depreciate=1 se cruza con el libro del detalle, si Depreciate=0 se cruza únicamente con libros donde OfficialBook=1.; [RETURN_RESULT] FixedAsset.FixedAssetPhysicalAssetDetailBook: Cuando fapa.Depreciate = 1 retorna DepreciatedValue, ResidualValue e HistoricalValue desde el detalle por libro; cuando Depreciate = 0 retorna 0 en depreciado y residual, y el HistoricalValue del activo físico.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetForReclassification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si fapa.Depreciate = 1 → Une el activo físico con su detalle por libro contable (fapadb.LegalBookId = lb.Id) y expone los valores depreciado, residual e histórico desde el detalle por libro. else Cuando Depreciate = 0, asocia únicamente el libro marcado como oficial (lb.OfficialBook = 1) y reporta DepreciatedValue=0, ResidualValue=0 y HistoricalValue tomado directamente del activo físico.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetForReclassification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetItem; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetPhysicalAssetDetailBook; GeneralLedger.LegalBook', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetForReclassification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetForReclassification';
GO
