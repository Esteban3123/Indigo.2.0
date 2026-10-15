
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