CREATE FUNCTION [Inventory].[NITThirdPartySISMED] 
(
	@Type varchar(2),
	@Year INT, 
	@Month INT, 
	@ProductId INT, 	
	@Value DECIMAL(18,2)
)
RETURNS VARCHAR(20)
AS
BEGIN
	DECLARE @NIT VARCHAR(20)

	IF @Type = 'VN' 
	BEGIN
		SELECT TOP 1 @NIT = t.Nit
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
		JOIN Common.ThirdParty t WITH(NOLOCK) ON t.Id = i.ThirdPartyId
		WHERE i.Status = 1 
			AND YEAR(i.InvoiceDate) = @Year
			AND MONTH(i.InvoiceDate) = @Month
	END
	ElSE
	BEGIN

		SELECT TOP 1 @NIT = t.Nit
				FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
				JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
				JOIN Common.Supplier s WITH(NOLOCK) ON s.Id = ev.SupplierId
				JOIN Common.ThirdParty t WITH(NOLOCK) ON t.Id = s.IdThirdParty
				WHERE ev.Status = 2 
					AND YEAR(ev.DocumentDate) = @Year
					AND MONTH(ev.DocumentDate) = @Month
					AND evd.ProductId = @ProductId 
					AND evd.UnitValue = @Value
	END
 
	RETURN @NIT
END