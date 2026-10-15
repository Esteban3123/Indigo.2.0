






CREATE VIEW [FixedAsset].[VReportPurchaseOrder]
AS
SELECT 
    pod.Id AS idDetailPurchaseOrder, 
    po.Id AS idPurchaseOrder, 
    po.Code AS codePurchaseOrder, 
    po.PurchaseOrderDate AS datePurchaseOrder, 
    po.CreationUser AS CreationUser,
    po.[Status] AS statusPurchaseOrder, 
    po.Detail AS detailPurchaseOrder, 
    fe.Code AS codeEquipment, 
    fe.[Description] AS descriptionEquipment,
    pod.Quantity AS Quantity, 
    pod.UnitValue, 
    pod.TotalValue, 
    pod.IvaValue, 
    po.DeliverDate AS dateDeliver, 
    c.Code + ' - ' + c.Name AS citySupplier,
    tp.Nit + ' - ' + tp.Name AS descriptionThirdParty, 
    (SELECT TOP 1 Addresss 
     FROM Common.Address AS Ad 
     WHERE Ad.IdPerson = tp.PersonId) AS ThirdPartyAddress,
    (SELECT TOP 1 Phone 
     FROM Common.Phone AS Ph 
     WHERE Ph.IdPerson = tp.PersonId) AS ThirdPartyPhone,
    po.CurrencyId
FROM FixedAsset.FixedAssetPurchaseOrder po
INNER JOIN FixedAsset.FixedAssetPurchaseOrderItem pod ON pod.PurchaseOrderId = po.Id
INNER JOIN FixedAsset.FixedAssetItem fe ON fe.Id = pod.ItemId
INNER JOIN Common.SuppliersDistributionLines sdl ON sdl.Id = po.SupplierDistributionLineId
INNER JOIN Common.Supplier s ON s.Id = sdl.IdSupplier
INNER JOIN Common.ThirdParty tp ON tp.Id = s.IdThirdParty
INNER JOIN Common.Person p ON p.Id = tp.PersonId
INNER JOIN Common.City c ON c.Id = s.IdCity
INNER JOIN Common.Currency cu ON cu.Id = po.CurrencyId;