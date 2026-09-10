CREATE VIEW [Cost].[ViewCostDistributionFixedAsset]
AS
	SELECT
		fadd.FixedAssetPhysicalAssetId,
		fad.ClosingYear Year, fad.ClosingMonth Month,
		fapa.Plate,
		CONCAT(fai.Code, ' - ', fai.Description) FixedAssetItemCodeDescription,
		CONCAT(fal.Code, ' - ', fal.Name) FixedAssetLocationCodeName,
		CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName
	FROM FixedAsset.FixedAssetDepreciation fad
	JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
	JOIN GeneralLedger.LegalBook lb ON lb.Id = fadd.LegalBookId AND lb.OfficialBook = 1
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fadd.FixedAssetPhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetLocation fal ON fapa.LocationId = fal.Id
	JOIN FixedAsset.FixedAssetResponsible far ON fapa.ResponsibleId = far.Id
	JOIN Common.ThirdParty tp ON far.ThirdPartyId = tp.Id
	WHERE fad.Status = 2 AND fadd.DepreciationValue > 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de costos por depreciación de activos fijos para el libro oficial contable. Consolida, por año y mes de cierre, los activos físicos que registraron valor de depreciación positivo en procesos de depreciación completados (estado 2), mostrando la placa del bien, el tipo de ítem con su código y descripción, la ubicación o sede donde se encuentra el activo, y el responsable identificado por su NIT y nombre. Sirve para reportería de costos y contabilidad de activos fijos, permitiendo analizar cómo se distribuyen los gastos de depreciación por ubicación, tipo de bien y tercero responsable.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostDistributionFixedAsset';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostDistributionFixedAsset';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la distribución de costos por depreciación de activos fijos físicos cerrados, asociando placa, ítem, ubicación y tercero responsable, restringido al libro oficial.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir depreciaciones con Status = 2 (cerrada/aplicada); El detalle de depreciación debe estar asociado a un LegalBook con OfficialBook = 1; El activo físico debe tener ítem, ubicación y responsable asignados; El responsable debe estar vinculado a un tercero en Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen depreciaciones en estado 2 (cerradas); Solo se considera el libro contable oficial (OfficialBook = 1); Se excluyen detalles con valor de depreciación menor o igual a cero; Cada fila representa un activo físico depreciado en un período (año/mes de cierre)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Depreciación de activos fijos; Activo fijo físico; Libro contable oficial; Ubicación de activo fijo; Responsable de activo fijo; Tercero; Período de cierre contable', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.ViewCostDistributionFixedAsset: Devuelve filas solo cuando fad.Status = 2 AND fadd.DepreciationValue > 0 y el LegalBook es OfficialBook = 1', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetDepreciationDetail; GeneralLedger.LegalBook; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetResponsible; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionFixedAsset';
GO
