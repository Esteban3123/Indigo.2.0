



CREATE VIEW [Billing].[ViewDocumentInvoiceProductSalesDevolutionDetails]
AS

select ROW_NUMBER() OVER (ORDER BY d.Id) RowId,
pro.Id ProductId, pro.Code ProductCode, pro.Name ProductName, pro.Code + ' - ' + pro.Name ProductDescription,
phy.BatchSerialId, bs.BatchCode, 
psdbs.Quantity, dd.Quantity QuantityDevolution,
d.Id DocumentInvoiceProductSalesDevolutionId, psdbs.Id DocumentInvoiceProductSalesDetailBatchSerialId,
psd.SalePrice, psd.IvaPercentage, psd.DiscountPercentage, psd.RTFPercentage,
dd.Id DocumentInvoiceProductSalesDevolutionDetailId,psd.ImportSource,psd.SourceCode
from Inventory.DocumentInvoiceProductSalesDevolution d
inner join Inventory.DocumentInvoiceProductSalesDevolutionDetail dd on dd.DocumentInvoiceProductSalesDevolutionId = d.Id
inner join Inventory.DocumentInvoiceProductSalesDetailBatchSerial psdbs on psdbs.Id = dd.DocumentInvoiceProductSalesDetailBatchSerialId
inner join Inventory.DocumentInvoiceProductSalesDetail psd on psd.Id = psdbs.DocumentInvoiceProductSalesDetailId
inner join Inventory.PhysicalInventory phy on phy.Id = psdbs.PhysicalInventoryId
inner join Inventory.InventoryProduct pro on pro.Id = psd.ProductId
left join Inventory.BatchSerial bs on bs.Id = phy.BatchSerialId