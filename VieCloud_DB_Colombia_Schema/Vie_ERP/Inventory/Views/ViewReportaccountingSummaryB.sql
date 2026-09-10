
CREATE VIEW [Inventory].[ViewReportaccountingSummaryB]
AS
select ROW_NUMBER() OVER(ORDER BY DocumentDate ASC) as Row, cast(sum(ROUND(quantity * Value,0)) as numeric(18,0)) as total , entityname, MovementType, DocumentDate from Inventory.Kardex 
where AffectInventory = 1 and EntityName in ('PharmaceuticalDispensing','PharmaceuticalDispensingDevolution','InventoryAdjustment','LoanMerchandise','LoanMerchandiseDevolution', 'EntranceVoucher', 'EntranceVoucherDevolution','TransferOrder','TransferOrderDevolution') 
group by entityname, MovementType, DocumentDate
--order by EntityName
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resumen contable de movimientos de inventario farmacéutico y de almacén que afectan el stock real. Consolida del kardex el valor total (cantidad × precio) agrupado por tipo de entidad de negocio, tipo de movimiento y fecha del documento, filtrando únicamente los movimientos que impactan inventario (dispensación de medicamentos, devoluciones, ajustes, préstamos de mercancía, entradas de almacén, órdenes de traslado y sus devoluciones). Sirve como base para reportes contables y de conciliación de inventario, permitiendo auditar los montos totales movidos por cada tipo de transacción en un período dado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportaccountingSummaryB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportaccountingSummaryB';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resume valorizadamente los movimientos de inventario que afectan stock, agrupados por entidad origen, tipo de movimiento y fecha, para reportes contables.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en el kardex con AffectInventory = 1; Los movimientos pertenecen a alguna de las entidades contables consideradas (dispensación farmacéutica y su devolución, ajuste de inventario, préstamo de mercancía y su devolución, entrada por vale y su devolución, orden de traslado y su devolución)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen movimientos que afectan inventario (AffectInventory = 1); Solo se consideran 9 tipos de entidades: PharmaceuticalDispensing, PharmaceuticalDispensingDevolution, InventoryAdjustment, LoanMerchandise, LoanMerchandiseDevolution, EntranceVoucher, EntranceVoucherDevolution, TransferOrder, TransferOrderDevolution; El total se calcula como suma de quantity*Value redondeado a entero y casteado a numeric(18,0); Se asigna un número de fila secuencial ordenado ascendentemente por DocumentDate', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex; Dispensación farmacéutica; Devolución de dispensación; Ajuste de inventario; Préstamo de mercancía; Devolución de préstamo; Vale de entrada; Devolución de vale de entrada; Orden de traslado; Devolución de orden de traslado; Valorización de inventario; Resumen contable', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.Kardex: Cuando AffectInventory = 1 y EntityName está en la lista de entidades contables permitidas, suma quantity*Value redondeado y lo agrupa por entityname, MovementType y DocumentDate', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryB';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportaccountingSummaryB';
GO
