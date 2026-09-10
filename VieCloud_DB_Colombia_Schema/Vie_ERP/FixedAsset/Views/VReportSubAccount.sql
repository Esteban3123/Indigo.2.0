

CREATE VIEW [FixedAsset].[VReportSubAccount]
AS
--Se agrego el LegalBookId para poderlo filtrar ya que estaba quemado solo con el libro OfficialBook, por lo tanto se comenta la linea y se agregan los valores en NULL como cero..para en el reporte adiccionarlos.
select pa.id,ma.Number as NumberAccount, ma.Name as NameAccount, pa.Plate, it.Code,it.[Description], fit.Code as CodeType, fit.Name as NameType , pa.HistoricalValue, isnull(pad.Valorization,0) as Valorization
, isnull(pad.Devaluation,0) as Devaluation, mad.Number as DeprecationAccount,isnull(pad.DepreciatedValue,0) as DepreciatedValue, isnull(pad.ResidualValue, 0) as ResidualValue, pa.HasOutput, isnull(pad.LegalBookId,0) as LegalBookId
from FixedAsset.FixedAssetPhysicalAsset pa
inner join FixedAsset.FixedAssetItem it on it.Id = pa.ItemId
inner join [FixedAsset].[FixedAssetItemType] fit on fit.Id = it.ItemTypeId
inner join FixedAsset.FixedAssetItemCatalog c on c.Id = it.ItemCatalogId
inner join GeneralLedger.MainAccounts mad on mad.Id = c.DepreciationAccountId
inner join GeneralLedger.MainAccounts ma on ma.Id = pa.MainAccountId
left join FixedAsset.FixedAssetPhysicalAssetDetailBook pad on pa.Id = pad.PhysicalAssetId-- and pad.LegalBookId = (select Id from GeneralLedger.LegalBook where OfficialBook = 1)

--select * from [FixedAsset].[VReportSubAccount] where LegalBookId in(0,9)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de activos fijos por subcuenta contable. Integra cada bien físico (activo fijo) con su cuenta contable principal, su tipo de bien, su catálogo y la cuenta de depreciación, mostrando valores clave como valor histórico, valorización, desvalorización, valor depreciado y valor residual por libro contable (libro legal). Permite filtrar por libro contable sin tenerlo fijo en el código, facilitando reportes financieros y contables de activos fijos asociados a subcuentas del plan de cuentas. Es la fuente principal para informes de control patrimonial, seguimiento de placas de inventario y análisis de depreciación acumulada por tipo de activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportSubAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportSubAccount';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida información de activos fijos físicos con su cuenta contable, tipo, ítem y datos de depreciación por libro contable para reportes de submayor.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportSubAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Todo activo físico debe tener un ItemId válido en FixedAssetItem; El ítem debe estar asociado a un ItemType y a un ItemCatalog válidos; El ItemCatalog debe tener configurada una DepreciationAccountId existente en MainAccounts; El activo físico debe tener una MainAccountId válida en MainAccounts', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportSubAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen activos cuyo ítem, tipo, catálogo y cuentas contables (principal y de depreciación) existan (INNER JOIN); Los valores monetarios de valorización, devaluación, depreciación y valor residual nunca son NULL en la salida (siempre 0 como mínimo); LegalBookId nunca es NULL en la salida; se reporta 0 cuando no hay detalle por libro; Un activo físico puede aparecer múltiples veces, una por cada libro contable con detalle registrado; El filtro por libro oficial fue removido: la vista expone todos los libros para ser filtrados externamente', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportSubAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Placa de activo; Cuenta contable principal; Cuenta de depreciación; Valor histórico; Valorización; Devaluación; Valor depreciado; Valor residual; Libro contable (LegalBook); Libro oficial; Tipo de ítem; Catálogo de activos; Salida de activo (HasOutput)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportSubAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.VReportSubAccount: Devuelve un registro por activo físico con su cuenta principal, cuenta de depreciación, tipo, valor histórico y, si existe detalle por libro, su valorización/devaluación/depreciación; si no existe detalle, retorna ceros y LegalBookId=0', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportSubAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en FixedAssetPhysicalAssetDetailBook para el activo (LEFT JOIN con coincidencia) → Toma Valorization, Devaluation, DepreciatedValue, ResidualValue y LegalBookId del detalle else Sustituye estos valores por 0 mediante ISNULL para que el reporte los considere como cero', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportSubAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetItemCatalog; GeneralLedger.MainAccounts; FixedAsset.FixedAssetPhysicalAssetDetailBook', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportSubAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportSubAccount';
GO
