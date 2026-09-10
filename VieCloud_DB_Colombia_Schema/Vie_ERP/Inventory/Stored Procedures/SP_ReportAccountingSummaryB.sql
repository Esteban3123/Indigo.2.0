CREATE PROCEDURE[Inventory].[SP_ReportAccountingSummaryB]
@DateStart Date,
@DateEnd Date
AS
BEGIN

if @DateEnd = '01/01/0001 0:00:00'
                Begin
                               set @DateEnd = [Common].[GETDATE]()
                End


select ROW_NUMBER() OVER(ORDER BY MovementType ASC) as Row, cast(sum(ROUND(quantity * Value,0)) as numeric(18,0)) as total , entityname, MovementType from Inventory.Kardex 
where AffectInventory = 1 and EntityName in ('PharmaceuticalDispensing','PharmaceuticalDispensingDevolution','InventoryAdjustment','LoanMerchandise','LoanMerchandiseDevolution', 'EntranceVoucher', 'EntranceVoucherDevolution','TransferOrder','TransferOrderDevolution') 
and CAST(DocumentDate as date) >= @DateStart AND CAST(DocumentDate as date) <= @DateEnd
group by entityname, MovementType
--order by EntityName
END