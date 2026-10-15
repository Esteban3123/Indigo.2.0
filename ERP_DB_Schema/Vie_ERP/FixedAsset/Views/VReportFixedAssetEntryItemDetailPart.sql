

CREATE VIEW [FixedAsset].[VReportFixedAssetEntryItemDetailPart]
AS
select cast(CONCAT(id.Id,ISNULL(idp.Id,0)) as Integer) as Id,fe.Id as IdEntry,Iif(fe.[Status] = 1, 'Registrado' , Iif(fe.[Status] = 2, 'Confirmado' , 'Anulado')) as [Status],fe.Code as AdmisionNumber,fe.EntryDate,
cs.Name as NameSupplier,cs.Code as CodeSupplier,(select top 1 Addresss from Common.[Address] as cca where cca.IdPerson = cp.Id) as [Address],
fe.InvoiceDate,ap.Code as CodeCxP,fe.InvoiceNumber,cc.Name as CityName,fe.GetLocationResponsible,
(select top 1 Phone from Common.Phone as ccp where ccp.IdPerson = cp.Id) as Phone, fr.Code as CodeResponsible,fct.Name as NameResponsible,
lo.Code as CodeLocation,lo.Name as NameLocation,ai.Code as CodeItem,ai.[Description] as DescriptionItem,flo.code as CodeLocationDetail, flo.Name as NameLocationDetail,
id.Plate, ei.IvaPercentage,ei.UnitValue,fe.Value,fe.ValueDiscount,(fe.FreightIVAValue + fe.ValueTax) as Iva,fe.WithholdingTax,fe.RetentionOther,fe.DeductionOther,fe.WithholdingICA,
fe.RetentionSource,fe.FreightValue,fe.FreightIVAValue,(fe.TotalValue-fe.WithholdingTax-fe.WithholdingICA-fe.RetentionSource) as Total,fe.[Description],
pac.code as CodePartAccesorieConsumible,pac.Name as NamePartAccesorieConsumible,idp.DepreciatePart,idp.Value as ValuePart,su.UserCode,sp.Fullname
from FixedAsset.FixedAssetEntryItemDetail as id 
inner join FixedAsset.FixedAssetLocation as flo on flo.Id = id.LocationId
inner join FixedAsset.FixedAssetEntryItem as ei on ei.Id = id.FixedAssetEntryItemId
inner join FixedAsset.FixedAssetItem as ai on ai.Id = ei.ItemId
inner join FixedAsset.FixedAssetEntry as fe on fe.Id = ei.FixedAssetEntryId
inner join Common.Supplier as cs on cs.Id = fe.SupplierId
inner join Common.City as cc on cc.Id = cs.IdCity
inner join Common.ThirdParty as ct on ct.Id = cs.IdThirdParty
inner join Common.Person as cp on cp.Id = ct.PersonId
left join Payments.AccountPayable as ap on ap.BillNumber = fe.InvoiceNumber and ap.IdSupplier = fe.SupplierId
left join FixedAsset.FixedAssetResponsible as fr on fr.Id = fe.ResponsibleId
left join Common.ThirdParty as fct on fct.Id = fr.ThirdPartyId
left join FixedAsset.FixedAssetLocation as lo on lo.id = fe.LocationId
left join FixedAsset.FixedAssetEntryItemDetailPart as idp on idp.FixedAssetEntryItemDetailId = id.Id
left join FixedAsset.FixedAssetPartsAccesoriesConsumables as pac on pac.Id = idp.PartAccesoriesConsumiblesId
left join [Security].[User] as su on su.UserCode = fe.CreationUser
left join [Security].Person as sp on sp.Id = su.IdPerson