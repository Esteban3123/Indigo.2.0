






CREATE VIEW [FixedAsset].[VReportFixedAssetPhysicalMainAccounts]
AS
select distinct gm.id,gm.Number,gm.Name,gm.LegalBookId,gm.[Status],gm.HandlesThirdParty,gm.HandlesCostCenter,gm.RetencionType,gm.AllowsMovement from FixedAsset.FixedAssetPhysicalAsset as fa 
inner join GeneralLedger.MainAccounts as gm on gm.id = fa.MainAccountId