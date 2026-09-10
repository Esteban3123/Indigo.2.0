
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que genera un resumen contable de los comprobantes de diario (vouchers) relacionados con movimientos de inventario farmacéutico y de mercancías. Consolida los valores totales de débito y crédito agrupados por tipo de entidad (dispensación farmacéutica, devoluciones, ajustes de inventario, préstamos de mercancía, entradas y traslados) y por fecha del comprobante, considerando únicamente los comprobantes en estado aprobado (Status = 2). Sirve para reportería contable-financiera que cruza los movimientos de inventario con el libro diario, permitiendo verificar los cargos y abonos generados por cada tipo de operación de almacén o farmacia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportaccountingSummaryA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportaccountingSummaryA';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resume montos contables (débito y crédito) agrupados por tipo de entidad y fecha, para comprobantes de diario en estado aprobado relacionados con movimientos de inventario y dispensación farmacéutica.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir comprobantes en GeneralLedger.JournalVouchers con Status = 2; Los comprobantes deben tener detalles asociados en JournalVoucherDetails vinculados por IdAccounting; EntityName debe ser uno de los tipos de movimiento de inventario/farmacia soportados', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan comprobantes en Status = 2 (aprobado/contabilizado); Solo se consideran nueve tipos de movimientos: dispensación farmacéutica y su devolución, ajuste de inventario, préstamo de mercancía y su devolución, comprobante de entrada y su devolución, orden de transferencia y su devolución; Los montos de débito y crédito se truncan a enteros (numeric(18,0)); La numeración de fila se asigna ordenada ascendentemente por VoucherDate; El agrupamiento se realiza por la combinación EntityName + VoucherDate', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de diario contable; Débito y crédito contable; Dispensación farmacéutica; Devolución de dispensación farmacéutica; Ajuste de inventario; Préstamo de mercancía; Comprobante de entrada de inventario; Orden de transferencia de inventario; Estado de comprobante contable', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.JournalVouchers: Solo se incluyen comprobantes cuyo EntityName pertenece a la lista de movimientos de inventario/farmacia y Status = 2; [RETURN_RESULT] GeneralLedger.JournalVoucherDetails: Suma DebitValue y CreditValue agrupados por EntityName y VoucherDate, casteados a numeric(18,0)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si jv.EntityName IN (''PharmaceuticalDispensing'',''PharmaceuticalDispensingDevolution'',''InventoryAdjustment'',''LoanMerchandise'',''LoanMerchandiseDevolution'',''EntranceVoucher'',''EntranceVoucherDevolution'',''TransferOrder'',''TransferOrderDevolution'') AND jv.Status = 2 → Se incluye el comprobante en el resumen contable else Se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryA';
GO
