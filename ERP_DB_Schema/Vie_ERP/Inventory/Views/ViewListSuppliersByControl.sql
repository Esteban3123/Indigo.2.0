


CREATE VIEW [Inventory].[ViewListSuppliersByControl]
AS



select evd.Id EntranceVoucherDetailId, evd.ProductId, s.Code SupplierCode, s.Name SupplierName, evd.UnitValue
from Inventory.EntranceVoucher ev
inner join Inventory.EntranceVoucherDetail evd on evd.EntranceVoucherId = ev.Id
inner join Common.Supplier s on s.Id = ev.SupplierId
where ev.Status = 2