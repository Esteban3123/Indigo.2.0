CREATE FUNCTION [Inventory].[InvoiceSISMEDProductSales] (@ProductID as int, @Value as decimal(18,2), @month as int)
RETURNS varchar(100)
AS
BEGIN
	declare @invoiceNumber varchar(100)

select top 1 @invoiceNumber = i.InvoiceNumber 
from Billing.Invoice as i WITH (NOLOCK) 
inner join Inventory.DocumentInvoiceProductSales dips with(nolock) on dips.InvoiceId = i.Id
inner join Inventory.DocumentInvoiceProductSalesDetail dipsd with(nolock) on dipsd.DocumentInvoiceProductSalesId = dips.Id
where i.Status = 1 and dipsd.ProductId = @ProductID and dipsd.SubTotalValue = @value and MONTH(i.InvoiceDate) = @month
 
RETURN @invoiceNumber

END