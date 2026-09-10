CREATE VIEW [FixedAsset].[ViewListFixedAssetReclassified]
AS
	SELECT
		fardb.Id
		, far.Id FixedAssetReclassificationId
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
		, fardb.DepreciatedValue
		, fardb.ResidualValue
		, fardb.HistoricalValue
		, fapa.AdquisitionTypeReal
		, fapa.HasReclassified
		, fapa.HasOutput
		, fapa.OutputRefund
	FROM FixedAsset.FixedAssetReclassification far
	JOIN FixedAsset.FixedAssetReclassificationDetail fard ON far.Id = fard.FixedAssetReclassificationId
	JOIN FixedAsset.FixedAssetReclassificationDetailBook fardb ON fard.Id = fardb.FixedAssetReclassificationDetailId
	JOIN FixedAsset.FixedAssetItem fai ON far.ItemIdPrevious = fai.Id
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fard.PhysicalAssetId = fapa.Id	
	JOIN GeneralLedger.LegalBook lb ON fardb.LegalBookId = lb.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los activos fijos que han sido reclasificados, consolidando en una sola consulta el encabezado de la reclasificación, el detalle de cada activo físico involucrado (con su serie, placa, tipo de adquisición y estado) y los valores contables por libro legal (valor depreciado, residual e histórico). Integra las tablas de reclasificación, detalle de reclasificación, activos físicos, ítems de catálogo y libros contables legales para ofrecer una vista completa del proceso de reclasificación. Sirve para reportería y auditoría contable de activos fijos reclasificados, permitiendo identificar si el activo fue dado de baja, reintegrado o tiene salida registrada, así como el ítem de catálogo anterior al cual pertenecía antes de la reclasificación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListFixedAssetReclassified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListFixedAssetReclassified';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista consolidada de reclasificaciones de activos fijos con su detalle por libro legal, mostrando valores contables (histórico, depreciado y residual) junto con datos del ítem previo y del activo físico involucrado.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetReclassified';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros vinculados en FixedAssetReclassification, su detalle, el detalle por libro legal, el ítem previo y el activo físico para que aparezcan en la vista (todos los JOIN son INNER).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetReclassified';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone reclasificaciones cuyo activo físico, ítem previo y libro legal existan (INNER JOIN en cadena).; El ítem mostrado corresponde siempre al ítem PREVIO de la reclasificación (ItemIdPrevious), no al ítem destino.; Cada fila representa la valuación contable (histórico, depreciado, residual) de un activo físico bajo un libro legal específico dentro de un proceso de reclasificación.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetReclassified';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reclasificación de activos fijos; Activo físico (placa y serie); Libro legal contable; Valor histórico; Valor depreciado; Valor residual; Tipo de adquisición; Salida de activo (HasOutput, OutputRefund); Ítem de catálogo de activos fijos', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetReclassified';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.ViewListFixedAssetReclassified: Devuelve una fila por cada combinación detalle-libro legal de una reclasificación, concatenando ''Code - Description'' del ítem y ''Code - Name'' del libro legal.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetReclassified';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetReclassification; FixedAsset.FixedAssetReclassificationDetail; FixedAsset.FixedAssetReclassificationDetailBook; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetPhysicalAsset; GeneralLedger.LegalBook', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetReclassified';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListFixedAssetReclassified';
GO
