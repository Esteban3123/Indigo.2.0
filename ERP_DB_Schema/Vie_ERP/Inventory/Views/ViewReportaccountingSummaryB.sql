
CREATE VIEW [Inventory].[ViewReportaccountingSummaryB]
AS
select ROW_NUMBER() OVER(ORDER BY DocumentDate ASC) as Row, cast(sum(ROUND(quantity * Value,0)) as numeric(18,0)) as total , entityname, MovementType, DocumentDate from Inventory.Kardex 
where AffectInventory = 1 and EntityName in ('PharmaceuticalDispensing','PharmaceuticalDispensingDevolution','InventoryAdjustment','LoanMerchandise','LoanMerchandiseDevolution', 'EntranceVoucher', 'EntranceVoucherDevolution','TransferOrder','TransferOrderDevolution') 
group by entityname, MovementType, DocumentDate
--order by EntityName