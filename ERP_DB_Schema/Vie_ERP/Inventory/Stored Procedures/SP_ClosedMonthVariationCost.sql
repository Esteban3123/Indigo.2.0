-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2017-10-19
-- Description:	Variacion mensual del costo de los productos
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ClosedMonthVariationCost]
	@MonthClosed AS INT,
	@YearClosed AS INT
AS
BEGIN
	DECLARE @Date DATE = '01/' + RIGHT( '0' + CAST(@MonthClosed AS VARCHAR(2)), 2) + '/'+ CAST(@YearClosed AS VARCHAR(4))
	DECLARE @DatePrevious DATE = DATEADD(MONTH, -1, @Date)
	DECLARE @MonthClosedPrevious INT = MONTH(@DatePrevious)
	DECLARE @YearClosedPrevious INT = YEAR(@DatePrevious)
	
	SELECT
		i.Code, i.Name,
		i.ProductCost, i.ProductCostPrevious,
		IIF( 
			i.ProductCostPrevious = 0, 
			1,
			((i.ProductCost-i.ProductCostPrevious)/i.ProductCostPrevious)
		) AS Variation
	FROM
	(
		SELECT 
			ip.Code, ip.Name,
			ip.ProductCost,
			ISNULL( cmi.ProductCost, 0 ) ProductCostPrevious
		FROM Inventory.PhysicalInventory [pi]
		INNER JOIN Inventory.Warehouse w
				ON [pi].WarehouseId = w.Id AND w.VirtualStore = 0
		INNER JOIN Inventory.InventoryProduct ip 
			ON [pi].ProductId = ip.Id
		LEFT JOIN Inventory.ClosedMonthInventory cmi
			ON ip.Id = cmi.ProductId
		LEFT JOIN Inventory.ClosedMonth cm
			ON cmi.ClosedMonthId = cm.Id
				AND cm.Month = @MonthClosedPrevious
				AND cm.Year = @YearClosedPrevious
		GROUP BY ip.Code, ip.Name, ip.ProductCost, cmi.ProductCost
	) as i
END