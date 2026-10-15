-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-08-03
-- Description:	Genera el informe de Deterioro de Inventarios
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportDeterioration]
	@Year INT,
	@Month INT
AS
BEGIN
	SELECT 
		i.Code InventoryProductCode,
		i.Name InventoryProductName,
		i.Quantity,
		i.AverageCost,
		i.Quantity * i.AverageCost CostTotal,
		i.AverageValue,
		i.Quantity * i.AverageValue ValueTotal,
		i.AverageValue - i.AverageCost DiffAverage,
		i.Quantity * (i.AverageValue - i.AverageCost) DiffTotal,
		((i.AverageValue - i.AverageCost) / (i.AverageValue)) Utility,
		IIF((i.AverageValue - i.AverageCost) < 0, i.Quantity * (i.AverageValue - i.AverageCost), 0) Deterioration
	FROM
	(
		SELECT 
			ip.Code,
			ip.Name,
			ISNULL(so.Quantity, 0) + ISNULL(dips.Quantity, 0) Quantity,
			CASE 
				WHEN ISNULL(so.Quantity, 0) + ISNULL(dips.Quantity, 0) = 0 THEN 0
				ELSE ((ISNULL(so.Quantity, 0) * ISNULL(so.AverageCost, 0)) + (ISNULL(dips.Quantity, 0) * ISNULL(dips.AverageCost, 0))) / (ISNULL(so.Quantity, 0) + ISNULL(dips.Quantity, 0))
			END AverageCost,
			CASE 
				WHEN ISNULL(so.Quantity, 0) + ISNULL(dips.Quantity, 0) = 0 THEN 0
				ELSE ((ISNULL(so.Quantity, 0) * ISNULL(so.AverageValue, 0)) + (ISNULL(dips.Quantity, 0) * ISNULL(dips.AverageValue, 0))) / (ISNULL(so.Quantity, 0) + ISNULL(dips.Quantity, 0))
			END AverageValue
		FROM Inventory.InventoryProduct ip
		LEFT JOIN
		(
			SELECT 
				sod.ProductId, 
				SUM(sod.InvoicedQuantity) Quantity, 
				SUM(sod.InvoicedQuantity * sod.CostValue) / SUM(sod.InvoicedQuantity) AverageCost,
				SUM(sod.InvoicedQuantity * sod.RateManualSalePrice) / SUM(sod.InvoicedQuantity) AverageValue
			FROM Billing.Invoice i
			JOIN Billing.InvoiceDetail id ON i.Id = id.InvoiceId
			JOIN Billing.ServiceOrderDetail sod ON id.ServiceOrderDetailId = sod.Id
			WHERE i.Status = 1 AND YEAR(i.InvoiceDate) = @Year AND MONTH(i.InvoiceDate) = @Month
			GROUP BY sod.ProductId
		) so ON ip.Id = so.ProductId
		LEFT JOIN 
		(
			SELECT 
				dips.ProductId, 
				SUM(k.Quantity) Quantity, 
				SUM(k.Quantity * k.AverageCost) / SUM(k.Quantity) AverageCost,
				SUM(k.Quantity * dips.AverageValue) / SUM(k.Quantity) AverageValue
			FROM 
			(
				SELECT 
					dips.Id, 
					dipsd.ProductId,
					SUM(dipsd.Quantity * dipsd.SalePrice) / SUM(dipsd.Quantity) AverageValue
				FROM Billing.Invoice i
				JOIN Inventory.DocumentInvoiceProductSales dips ON i.Id = dips.InvoiceId
				JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd ON dips.Id = dipsd.DocumentInvoiceProductSalesId
				WHERE i.Status = 1 AND YEAR(i.InvoiceDate) = @Year AND MONTH(i.InvoiceDate) = @Month
				GROUP BY dips.Id, dipsd.ProductId
			) dips
			JOIN
			(
				SELECT 
					k.EntityId, 
					k.ProductId, 
					SUM(k.Quantity) Quantity,
					SUM(k.Quantity * k.AverageCost) / SUM(k.Quantity) AverageCost
				FROM Inventory.Kardex k
				WHERE k.MovementType = 2 AND k.EntityName = 'DocumentInvoiceProductSales'
				GROUP BY k.EntityId, k.ProductId
			) k ON dips.Id = k.EntityId AND dips.ProductId = k.ProductId
			GROUP BY dips.ProductId
		) dips ON ip.Id = dips.ProductId
	) i
	WHERE i.Quantity <> 0
END