
CREATE VIEW [Inventory].[ViewEntranceVoucherDevolution]
AS
SELECT	CONCAT(evdev.Id,'-',evd.EntranceVoucherId,'-',evdbs.EntranceVoucherDetailId) Id,
		evdev.Id EntranceVoucherDevolutionId,
		evd.EntranceVoucherId,
		evdbs.EntranceVoucherDetailId,
		SUM(ROUND(evdevd.Quantity * evd.UnitValue, 2)) SubTotalValue,
		SUM(ROUND(evdevd.Quantity * evd.UnitValue * evd.DiscountPercentage / 100, 2)) DiscountValue,
		SUM(ROUND(evdevd.Quantity * evd.UnitValue * (100 - evd.DiscountPercentage) / 100 * evd.IvaPercentage / 100, 2)) IvaValue,
		SUM(ROUND(evdevd.Quantity * evd.UnitValue * (100 - evd.DiscountPercentage) / 100 * evd.RTFPercentage / 100, 2)) RTFValue
FROM Inventory.EntranceVoucherDevolution evdev
JOIN Inventory.EntranceVoucherDevolutionDetail evdevd ON evdev.Id = evdevd.EntranceVoucherDevolutionId
JOIN Inventory.EntranceVoucherDetailBatchSerial evdbs ON evdevd.EntranceVoucherDetailBatchSerialId = evdbs.Id
JOIN Inventory.EntranceVoucherDetail evd ON evdbs.EntranceVoucherDetailId = evd.Id
JOIN Inventory.EntranceVoucher ev ON evd.EntranceVoucherId = ev.Id
WHERE evdev.Status = 2
GROUP BY evdev.Id, evd.EntranceVoucherId, evdbs.EntranceVoucherDetailId, ev.RoundService