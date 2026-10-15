
CREATE VIEW [Inventory].[ViewReportaccountingSummaryA]
AS
select ROW_NUMBER() OVER(ORDER BY VoucherDate ASC) as Row, cast(sum(jvd.DebitValue) as numeric(18,0)) as Debito , cast(sum(jvd.CreditValue) as numeric(18,0)) as Credito , EntityName, VoucherDate from GeneralLedger.JournalVouchers as jv inner join
GeneralLedger.JournalVoucherDetails as jvd on jv.Id = jvd.IdAccounting
where jv.id in (
select id from GeneralLedger.JournalVouchers   as jv 
where jv.EntityName in(
'PharmaceuticalDispensing','PharmaceuticalDispensingDevolution','InventoryAdjustment','LoanMerchandise','LoanMerchandiseDevolution', 'EntranceVoucher', 'EntranceVoucherDevolution','TransferOrder','TransferOrderDevolution') and jv.Status = 2
) group by EntityName, VoucherDate
--order by EntityName