

CREATE VIEW [FixedAsset].[VReportFixedAssetPhysicalMainAccounts]
AS
select distinct gm.id,gm.Number,gm.Name,gm.LegalBookId,gm.[Status],gm.HandlesThirdParty,gm.HandlesCostCenter,gm.RetencionType,gm.AllowsMovement from FixedAsset.FixedAssetPhysicalAsset as fa 
inner join GeneralLedger.MainAccounts as gm on gm.id = fa.MainAccountId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra las cuentas contables principales (Plan de Cuentas) que están asociadas a activos fijos físicos registrados en el inventario de la organización. Integra el módulo de Activos Fijos con el Libro Mayor, cruzando cada activo físico con su cuenta contable correspondiente. Devuelve datos únicos (sin duplicados) de cada cuenta: número, nombre, libro legal al que pertenece, estado, si maneja terceros, si maneja centro de costos, tipo de retención y si permite movimientos contables. Sirve para reportes de control contable-patrimonial que relacionan los activos físicos tangibles con sus cuentas de mayor en el Plan de Cuentas.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportFixedAssetPhysicalMainAccounts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportFixedAssetPhysicalMainAccounts';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las cuentas contables principales que están asociadas a activos fijos físicos registrados, para alimentar reportes de inventario físico contra el plan de cuentas.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetPhysicalMainAccounts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación entre activos fijos físicos y cuentas del libro mayor mediante el identificador de cuenta principal.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetPhysicalMainAccounts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna cuentas contables que tienen al menos un activo fijo físico asociado (INNER JOIN).; Aplica DISTINCT, garantizando que cada cuenta aparezca una sola vez aunque tenga múltiples activos asociados.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetPhysicalMainAccounts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo físico; Cuenta contable principal; Plan de cuentas; Libro legal; Manejo de terceros; Centro de costo; Tipo de retención; Movimientos contables', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetPhysicalMainAccounts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.MainAccounts: Devuelve cuentas contables distintas que estén referenciadas por al menos un activo fijo físico (INNER JOIN con FixedAssetPhysicalAsset).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetPhysicalMainAccounts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPhysicalAsset; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetPhysicalMainAccounts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetPhysicalMainAccounts';
GO
