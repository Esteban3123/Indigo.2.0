
create VIEW [FixedAsset].[ViewListAccountsUsedInFixedAsset]
AS

/** Type: [ 1: 'MainAccount', 2: 'DepreciationAccount' ] **/

SELECT 
	DISTINCT Id, Number, Name, Status, LegalBookId, Type
FROM (
	SELECT 
		ma.Id, ma.Number, ma.Name, ma.Status, ma.LegalBookId, 1 as Type
	FROM FixedAsset.FixedAssetPhysicalAsset fapa
	INNER JOIN GeneralLedger.MainAccounts ma ON fapa.MainAccountId = ma.Id

	UNION

	SELECT 
		ma.Id, ma.Number, ma.Name, ma.Status, ma.LegalBookId, 1 as Type
	FROM FixedAsset.FixedAssetPhysicalAsset fapa
	INNER JOIN GeneralLedger.HomologationAccount ha ON fapa.MainAccountId = ha.MainAccountId
	INNER JOIN GeneralLedger.MainAccounts ma ON ha.OfficialMainAccountId = ma.Id

	UNION

	SELECT 
		ma.Id, ma.Number, ma.Name, ma.Status, ma.LegalBookId, 2 as Type
	FROM FixedAsset.FixedAssetItemCatalog faic
	INNER JOIN GeneralLedger.MainAccounts ma ON faic.DepreciationAccountId = ma.Id OR faic.DepreciationLeasingAccountId = ma.Id

	UNION

	SELECT 
		ma.Id, ma.Number, ma.Name, ma.Status, ma.LegalBookId, 2 as Type
	FROM FixedAsset.FixedAssetItemCatalog faic
	INNER JOIN GeneralLedger.HomologationAccount ha ON faic.DepreciationAccountId = ha.MainAccountId OR faic.DepreciationLeasingAccountId = ha.MainAccountId
	INNER JOIN GeneralLedger.MainAccounts ma ON ha.OfficialMainAccountId = ma.Id
) AS d
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada y sin duplicados de todas las cuentas contables del plan de cuentas utilizadas en el módulo de activos fijos, ya sea como cuenta principal de un activo físico o como cuenta de depreciación (incluyendo depreciación por leasing) definida en el catálogo de ítems de activos. Incluye tanto las cuentas contables directas como sus equivalentes oficiales obtenidas mediante la tabla de homologación de cuentas, garantizando que se cubran mappings entre planes de cuentas locales y oficiales. Cada registro indica el número, nombre, estado y libro legal de la cuenta, junto con un tipo que distingue si es cuenta principal del activo (Tipo 1) o cuenta de depreciación (Tipo 2). Sirve para reportería contable, auditoría de parametrización y validación de que los activos fijos tienen correctamente asignadas sus cuentas en el libro mayor.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListAccountsUsedInFixedAsset';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'ViewListAccountsUsedInFixedAsset';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las cuentas contables del libro mayor que están asociadas a activos fijos, distinguiendo entre cuentas principales (Type=1) y cuentas de depreciación (Type=2), incluyendo sus equivalencias por homologación.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListAccountsUsedInFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas referenciadas (MainAccountId, DepreciationAccountId, DepreciationLeasingAccountId, OfficialMainAccountId) deben existir en GeneralLedger.MainAccounts; Para resolver equivalencias debe existir registro en GeneralLedger.HomologationAccount que vincule la cuenta interna con la cuenta oficial', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListAccountsUsedInFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Type solo puede tomar valores 1 (MainAccount) o 2 (DepreciationAccount); Solo se exponen cuentas efectivamente referenciadas por algún activo físico o por algún catálogo de ítem de activo fijo; Las cuentas oficiales homologadas se exponen como equivalentes de las internas, no como registros adicionales independientes; No se filtra por Status: se devuelven cuentas activas e inactivas indistintamente', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListAccountsUsedInFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Cuenta contable principal; Cuenta de depreciación; Cuenta de depreciación de leasing; Homologación de cuentas; Libro legal; Plan de cuentas', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListAccountsUsedInFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve cuentas con Type=1 cuando provienen de la cuenta principal del activo físico (FixedAssetPhysicalAsset.MainAccountId), directa o vía homologación a OfficialMainAccountId; [RETURN_RESULT] resultset: Devuelve cuentas con Type=2 cuando provienen de la cuenta de depreciación o de depreciación de leasing del catálogo de ítems (FixedAssetItemCatalog.DepreciationAccountId o DepreciationLeasingAccountId), directa o vía homologación; [RETURN_RESULT] resultset: Aplica DISTINCT y UNION para eliminar duplicados entre cuentas directas y homologadas', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListAccountsUsedInFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si La cuenta del activo físico coincide directamente con MainAccounts.Id → Se incluye la cuenta directa con Type=1; si La cuenta del activo físico coincide con HomologationAccount.MainAccountId → Se incluye la cuenta oficial homologada (OfficialMainAccountId) con Type=1; si La cuenta del catálogo coincide en DepreciationAccountId o DepreciationLeasingAccountId con MainAccounts.Id → Se incluye la cuenta directa con Type=2; si La cuenta de depreciación del catálogo coincide con HomologationAccount.MainAccountId → Se incluye la cuenta oficial homologada con Type=2', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListAccountsUsedInFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItemCatalog; GeneralLedger.MainAccounts; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListAccountsUsedInFixedAsset';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'ViewListAccountsUsedInFixedAsset';
GO
