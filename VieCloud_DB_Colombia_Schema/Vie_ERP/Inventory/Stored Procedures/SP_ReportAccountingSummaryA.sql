
CREATE PROCEDURE[Inventory].[SP_ReportAccountingSummaryA]
@DateStart Date,
@DateEnd Date
AS
BEGIN

if @DateEnd = '01/01/0001 0:00:00'
                Begin
                               set @DateEnd = [Common].[GETDATE]()
                End



select ROW_NUMBER() OVER(ORDER BY EntityName ASC) as Row, cast(sum(jvd.DebitValue) as numeric(18,0)) as Debito , cast(sum(jvd.CreditValue) as numeric(18,0)) as Credito , EntityName from GeneralLedger.JournalVouchers as jv inner join
GeneralLedger.JournalVoucherDetails as jvd on jv.Id = jvd.IdAccounting
where jv.id in (
select id from GeneralLedger.JournalVouchers   as jv 
where jv.EntityName in(
'PharmaceuticalDispensing','PharmaceuticalDispensingDevolution','InventoryAdjustment','LoanMerchandise','LoanMerchandiseDevolution', 'EntranceVoucher', 'EntranceVoucherDevolution','TransferOrder','TransferOrderDevolution') and jv.Status = 2
and CAST(VoucherDate as date) >= @DateStart AND CAST(VoucherDate as date) <= @DateEnd
) group by EntityName
--order by EntityName
END