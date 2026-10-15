




CREATE VIEW [Billing].[ViewDocumentInvoiceProductSalesByDevolution]
AS

select ROW_NUMBER() OVER (ORDER BY ps.Id) RowId,
pro.Id ProductId, pro.Code ProductCode, pro.Name ProductName, pro.Code + ' - ' + pro.Name ProductDescription,
phy.BatchSerialId, bs.BatchCode, 
psdbs.OutstandingQuantity Quantity, psdbs.OutstandingQuantity QuantityDevolution,
ps.Id DocumentInvoiceProductSalesId, psdbs.Id DocumentInvoiceProductSalesDetailBatchSerialId,
psd.SalePrice, psd.IvaPercentage, psd.DiscountPercentage, psd.RTFPercentage,
ps.ContractExternalClientsId,
CONCAT(cec.Code,' - ',cec.ContractNumber) CECcode,
psd.ImportSource,
psd.SourceCode
from Inventory.DocumentInvoiceProductSales ps
inner join Inventory.DocumentInvoiceProductSalesDetail psd with(NOLOCK) on psd.DocumentInvoiceProductSalesId = ps.Id
inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs with(NOLOCK) on psdbs.DocumentInvoiceProductSalesDetailId = psd.Id
inner join Inventory.PhysicalInventory phy with(NOLOCK) on phy.Id = psdbs.PhysicalInventoryId
inner join Inventory.InventoryProduct pro with(NOLOCK) on pro.Id = psd.ProductId
left join Inventory.BatchSerial bs with(NOLOCK) on bs.Id = phy.BatchSerialId
left join MixingStation.ContractExternalClients cec  with(NOLOCK) on ps.ContractExternalClientsId = cec.Id