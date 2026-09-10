

CREATE view [ViewInternal].[VResumidoDepreciacionXCuenta]
as (

select d.ClosingMonth, map.Number + ' - ' + map.Name as Cuenta,ma.Number + ' - ' + ma.Name as Cuenta2, sum(ddc.DepreciationValue) as Value
from FixedAsset.FixedAssetDepreciation d
inner join FixedAsset.FixedAssetDepreciationDetail dd on d.Id = dd.FixedAssetDepreciationId
inner join FixedAsset.FixedAssetDepreciationDetailCost ddc on ddc.FixedAssetDepreciationDetailId = dd.Id
inner join FixedAsset.FixedAssetPhysicalAsset ph on ph.Id = dd.FixedAssetPhysicalAssetId
inner join FixedAsset.FixedAssetItem i on i.Id = ph.ItemId
inner join FixedAsset.FixedAssetItemCatalog c on c.Id = i.ItemCatalogId
inner join GeneralLedger.MainAccounts ma on ma.Id = c.DepreciationAccountId 
inner join GeneralLedger.MainAccounts map on map.Id = ph.MainAccountId
where dd.LegalBookId = 1
group by d.ClosingMonth, map.Number, map.Name, ma.Number, ma.Name
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que resume el valor total depreciado por período de cierre (mes) agrupado por dos cuentas contables del libro mayor: la cuenta del activo físico y la cuenta de depreciación definida en el catálogo del ítem. Filtra exclusivamente el libro legal (LegalBookId = 1) y consolida los costos de depreciación distribuidos, mostrando número y nombre de cada cuenta para facilitar análisis contables del gasto por depreciación de activos fijos.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VResumidoDepreciacionXCuenta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VResumidoDepreciacionXCuenta';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resume el valor depreciado de activos fijos por mes de cierre, agrupando por cuenta contable del activo y cuenta contable de depreciación, restringido al libro legal principal.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VResumidoDepreciacionXCuenta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros de depreciación con detalle y costos asociados; Cada activo físico debe tener Item, ItemCatalog y MainAccount válidos; El ItemCatalog debe tener configurada una cuenta de depreciación (DepreciationAccountId)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VResumidoDepreciacionXCuenta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran detalles del libro legal con Id = 1; Solo aparecen activos cuyo catálogo tiene cuenta de depreciación definida y cuyo activo físico tiene cuenta principal asignada (INNER JOIN); Las cuentas se exponen en formato ''Número - Nombre''', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VResumidoDepreciacionXCuenta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Depreciación de activos fijos; Cuenta contable; Cuenta de depreciación; Libro legal; Mes de cierre contable; Activo fijo físico; Catálogo de activos', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VResumidoDepreciacionXCuenta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultado consultado: Filtra dd.LegalBookId = 1 y devuelve la suma de DepreciationValue agrupada por mes de cierre, cuenta contable del activo y cuenta de depreciación', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VResumidoDepreciacionXCuenta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetDepreciationDetail; FixedAsset.FixedAssetDepreciationDetailCost; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemCatalog; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VResumidoDepreciacionXCuenta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VResumidoDepreciacionXCuenta';
GO
