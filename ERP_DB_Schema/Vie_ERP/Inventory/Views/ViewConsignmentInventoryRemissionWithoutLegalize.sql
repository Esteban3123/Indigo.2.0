


CREATE VIEW [Inventory].[ViewConsignmentInventoryRemissionWithoutLegalize]
AS
with cte_ccld as (	select ccl.SupplierId,ccld.ProductId,ccld.CostNew,ccl.OperatingUnitId
					from Inventory.ConsignmentCostList as ccl with(nolock)
					inner join Inventory.ConsignmentCostListDetail as ccld with(nolock) on ccl.Id = ccld.ConsignmentCostListId
					inner join Common.Supplier as s on s.Id = ccl.SupplierId
					--Solo los proveedores que tengan marcado costo de listas y la lista este activa
					where  s.ConsignmentInventoryCosting = 1 and ccl.Status = 1 
					group by ccl.SupplierId,ccld.ProductId,ccld.CostNew,ccl.OperatingUnitId) 

	select
	cirdbs.Id,
	cirdbs.BatchSerialId,
	cirdbs.Quantity,
	cirdbs.UsedQuantity,
	cirdbs.LegalizedQuantity,
	cirdbs.UsedQuantity - cirdbs.LegalizedQuantity - ISNULL(ev.Quantity, 0) AS OutstandingQuantity,
	------------------------------
	cird.ProductId,
	coalesce(ccld.CostNew,cird.UnitValue,0) UnitValue	,	
	cird.IvaValue,
	cird.LastValue,
	------------------------------
	cir.Code,
	cir.RemissionDate,
	cir.SupplierId,
	cir.SupplierDistributionLineId,
	cir.WarehouseId,
	------------------------------
	CONCAT(pg.Code, ' - ', pg.Name) GroupCodeName,
	CONCAT(ip.Code, ' - ', ip.Name) CodeNameProduct,
	bs.BatchCode,
	c.Id AS CurrencyId,
	c.Abbreviation AS CurrencyAbbreviation
FROM Inventory.ConsignmentInventoryRemission as cir
JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cir.Id = cird.ConsignmentInventoryRemissionId
JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
------------------------------------------------------------------------------------------------------------------------------
JOIN Inventory.InventoryProduct ip ON cird.ProductId = ip.Id
JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
JOIN Common.Currency c WITH(NOLOCK) ON c.id = cir.CurrencyId
LEFT JOIN Inventory.BatchSerial bs ON cirdbs.BatchSerialId = bs.Id
------------------------------------------------------------------------------------------------------------------------------
LEFT JOIN 
(
	SELECT
		evd.ConsignmentInventoryRemissionDetailBatchSerialId,
		SUM(evd.Quantity) Quantity
	FROM Inventory.EntranceVoucher ev
	JOIN Inventory.EntranceVoucherDetail evd ON ev.Id = evd.EntranceVoucherId
	WHERE ev.Status IN (1)
		AND evd.ConsignmentInventoryRemissionDetailBatchSerialId IS NOT NULL
	GROUP BY evd.ConsignmentInventoryRemissionDetailBatchSerialId
) ev ON ev.ConsignmentInventoryRemissionDetailBatchSerialId = cirdbs.Id
LEFT join cte_ccld ccld on ccld.ProductId = cird.ProductId and ccld.SupplierId= cir.SupplierId  AND ccld.OperatingUnitId = cir.OperatingUnitId