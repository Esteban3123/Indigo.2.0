CREATE VIEW [Cost].[InventoryConsumedByCostCenter]
AS
SELECT 
	k.Year, 
	k.Month, 
	k.ProductId, 
	k.CostCenterId
FROM
(
	SELECT 
		YEAR(k.DocumentDate) Year, 
		MONTH(k.DocumentDate) Month, 
		k.ProductId, 
		fu.CostCenterId
	FROM Inventory.Kardex k WITH (NOLOCK)
	JOIN Inventory.PharmaceuticalDispensingDetail pdd 
		ON k.EntityName = 'PharmaceuticalDispensing' 
			AND k.EntityId = pdd.PharmaceuticalDispensingId
			AND k.ProductId = pdd.ProductId
	JOIN Payroll.FunctionalUnit fu ON pdd.FunctionalUnitId = fu.Id
	WHERE pdd.Quantity > pdd.ReturnedQuantity
	GROUP BY YEAR(k.DocumentDate), MONTH(k.DocumentDate), k.ProductId, fu.CostCenterId

	UNION ALL

	SELECT 
		YEAR([to].DocumentDate) Year, 
		MONTH([to].DocumentDate) Month, 
		k.ProductId, 
		IIF([to].DispatchTo = 1, w.CostCenterId, fu.CostCenterId) CostCenterId
	FROM Inventory.Kardex k WITH (NOLOCK)
	JOIN Inventory.TransferOrder [to] 
		ON k.EntityName = 'TransferOrder' 
			AND k.EntityId = [to].Id
	JOIN Inventory.TransferOrderDetail tod 
		ON [to].Id = tod.TransferOrderId 
			AND k.ProductId = tod.ProductId
	JOIN Inventory.TransferOrderDetailBatchSerial todbs 
		ON tod.Id = todbs.TransferOrderDetailId
	LEFT JOIN Inventory.Warehouse w ON [to].TargetWarehouseId = w.Id
	LEFT JOIN Payroll.FunctionalUnit fu ON [to].TargetFunctionalUnitId = fu.Id
	WHERE [to].OrderType = 2
		AND todbs.OutstandingQuantity > 0
	GROUP BY YEAR([to].DocumentDate), MONTH([to].DocumentDate), k.ProductId, IIF([to].DispatchTo = 1, w.CostCenterId, fu.CostCenterId)
) k
GROUP BY k.Year, k.Month, k.ProductId, k.CostCenterId