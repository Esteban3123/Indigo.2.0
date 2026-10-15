


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