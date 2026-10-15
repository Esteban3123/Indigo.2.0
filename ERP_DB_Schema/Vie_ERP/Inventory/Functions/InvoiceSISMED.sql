CREATE FUNCTION [Inventory].[InvoiceSISMED] 
(
	@Type INT,
	@Year INT, 
	@Month INT, 
	@ProductId INT, 	
	@Value DECIMAL(18,2)
)
RETURNS VARCHAR(100)
AS
BEGIN
	DECLARE @InvoiceNumber VARCHAR(100)

	IF @Type = 1 
	BEGIN
		SELECT TOP 1 @InvoiceNumber = i.InvoiceNumber 
		FROM Billing.Invoice i WITH (NOLOCK) 
		JOIN
		(
				SELECT id.InvoiceId
				FROM Billing.InvoiceDetail id WITH (NOLOCK)
				JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id
				WHERE sod.ProductId = @ProductId 
					AND sod.SubTotalSalesPrice = @Value 
			UNION ALL
				SELECT dips.InvoiceId
				FROM Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK)
				JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd WITH (NOLOCK) ON dips.Id = dipsd.DocumentInvoiceProductSalesId
				WHERE dipsd.ProductId = @ProductId 
					AND dipsd.SalePrice = @Value 

			UNION ALL
				SELECT bb.InvoiceId
				FROM Billing.BasicBilling bb WITH (NOLOCK)
				JOIN Billing.BasicBillingDetail bbd WITH (NOLOCK) ON bb.Id = bbd.BasicBillingId
				WHERE bbd.ProductId = @ProductId 
					AND bbd.Price = @Value

		) id ON i.Id = id.InvoiceId
		WHERE i.Status = 1 
			AND YEAR(i.InvoiceDate) = @Year
			AND MONTH(i.InvoiceDate) = @Month
	END
	ElSE
	BEGIN
		SELECT TOP 1 @InvoiceNumber = ev.InvoiceNumber 
		FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
		JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
		WHERE ev.Status = 2 
			AND YEAR(ev.DocumentDate) = @Year
			AND MONTH(ev.DocumentDate) = @Month
			AND evd.ProductId = @ProductId 
			AND evd.UnitValue = @Value
	END
 
	RETURN @InvoiceNumber
END