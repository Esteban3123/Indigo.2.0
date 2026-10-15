

CREATE VIEW [Inventory].[ViewReportRemissionEntranceDetail]
AS

WITH CTE_EntranceVoucher AS (
    SELECT 
        bs.Id AS BatchSerialId,
        ev.Id AS EntranceVoucherId,
        ev.Status AS StatusEntranceVoucher,
		ev.Code CodeEntranceVoucher
    FROM Inventory.RemissionEntranceDetailBatchSerial bs
    JOIN Inventory.EntranceVoucherDetail evd ON evd.RemissionEntranceDetailBatchSerialId = bs.Id AND evd.EntranceSource = 4
    JOIN Inventory.EntranceVoucher ev ON ev.Id = evd.EntranceVoucherId and ev.Status = 2
)

SELECT 
	ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) Id,
    ct.Nit AS CodeSupplier,
    s.Name AS NameSupplier,
    re.Code AS CodeRemision,
    re.RemissionDate,
    re.Description,
    re.CreationUser,
    w.Id AS IdWarehouse, 
    w.Code AS CodeWarehouse,
    w.Name AS NameWarehouse,
    ip.Code AS CodeProduct,
    ip.Name AS NameProduct,
    sum(bs.Quantity) AS InitialAmmount,
    red.TotalValue AS InitialValue,
    sum(bs.OutstandingQuantity) AS OutstandingQuantity,
    sum(ROUND(bs.OutstandingQuantity * red.UnitValue * (100 + red.IvaPercentage) / 100, 2)) AS TotalOutstandingQuantity,
    re.CurrencyId,
    re.Status,
    CTE_EntranceVoucher.StatusEntranceVoucher,
	CTE_EntranceVoucher.CodeEntranceVoucher
from Inventory.RemissionEntrance re
	JOIN Inventory.RemissionEntranceDetail red on red.RemissionEntranceId = re.Id
	JOIN Inventory.RemissionEntranceDetailBatchSerial bs on bs.RemissionEntranceDetailId = red.Id
	JOIN Inventory.Warehouse w on w.Id = re.WarehouseId
	JOIN Common.Supplier s on s.Id = re.SupplierId
	JOIN Common.ThirdParty as ct on ct.Id = s.IdThirdParty
	JOIN Inventory.InventoryProduct ip on ip.Id = red.ProductId
	JOIN Inventory.ProductGroup pg on pg.Id = ip.ProductGroupId
	JOIN Payments.AccountPayableConcepts apc on apc.Id = pg.InventoryAccountPayableConceptId
	JOIN GeneralLedger.MainAccounts ma on ma.Id = apc.IdAccount
	LEFT JOIN CTE_EntranceVoucher ON CTE_EntranceVoucher.BatchSerialId = bs.Id
group by 
	red.Id,
    ct.Nit,
    s.Name,
    re.Code,
    re.CreationUser,
    ip.Code,
    ip.Name,
    re.RemissionDate,
    re.Description,
    w.Id,
    w.Code,
    w.Name,
    red.TotalValue,
    re.CurrencyId,
    re.Status,
	CTE_EntranceVoucher.StatusEntranceVoucher,
	CTE_EntranceVoucher.CodeEntranceVoucher