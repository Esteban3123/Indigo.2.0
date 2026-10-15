CREATE VIEW [Inventory].[InvoicesForSISMED]
AS
SELECT 
	YEAR(i.InvoiceDate) Year
	,MONTH(i.InvoiceDate) Month
	,ISNULL(ip.CodeCUM, '') CodeCUM
	,ISNULL(sod.SubTotalSalesPrice, dipsd.SalePrice) Price
	,MIN(i.InvoiceNumber) InvoiceNumber
FROM Billing.Invoice i
LEFT JOIN Billing.InvoiceDetail AS id WITH (NOLOCK) ON i.Id = id.InvoiceId
LEFT JOIN Billing.ServiceOrderDetail AS sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id AND sod.GrandTotalSalesPrice > 0
LEFT JOIN Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK) ON i.Id = dips.InvoiceId
LEFT JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
LEFT JOIN Inventory.InventoryProduct ip ON ISNULL(sod.ProductId, dipsd.ProductId) = ip.Id
WHERE ip.ATCId IS NOT NULL --AND ip.CodeCUM IS NOT NULL
GROUP BY YEAR(i.InvoiceDate), MONTH(i.InvoiceDate), ip.CodeCUM, ISNULL(sod.SubTotalSalesPrice, dipsd.SalePrice)