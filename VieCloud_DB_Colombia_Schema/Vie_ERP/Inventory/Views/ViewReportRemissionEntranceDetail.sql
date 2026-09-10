

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida el detalle de las remisiones de entrada de inventario, combinando información del proveedor (NIT y nombre), datos de la remisión (código, fecha, descripción, bodega destino), producto recibido (código y nombre), cantidades iniciales recibidas por lote o serial, cantidades pendientes por legalizar y sus valores estimados con IVA incluido. Integra las tablas de remisiones de entrada, sus detalles por ítem, los lotes y seriales asociados, bodegas, proveedores, terceros y catálogo de productos; adicionalmente cruza con el comprobante de entrada (vale de ingreso) cuando el lote ya fue legalizado, mostrando su estado y código. Sirve para reportería de seguimiento y control de recepciones de mercancía pendientes de legalizar o ya formalizadas en el inventario, permitiendo identificar qué remisiones de proveedor tienen ítems sin comprobante de entrada definitivo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportRemissionEntranceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportRemissionEntranceDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reportería que consolida las remisiones de entrada de mercancía con sus lotes/series, mostrando cantidades pendientes de legalizar y su valor, e indicando si ya existe un comprobante de entrada (vale) aprobado asociado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportRemissionEntranceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las remisiones deben tener detalle con lotes/series en RemissionEntranceDetailBatchSerial.; El producto debe estar asociado a un ProductGroup con concepto contable (AccountPayableConcepts) y cuenta principal (MainAccounts) configurados, ya que son JOIN obligatorios.; El proveedor debe tener un tercero (ThirdParty) asociado para obtener su NIT.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportRemissionEntranceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El cruce con el comprobante de entrada se restringe a la fuente de entrada tipo 4 (remisión) y a comprobantes en estado 2 (aprobado/legalizado).; El valor pendiente por ítem se calcula incluyendo el IVA sobre el valor unitario y la cantidad pendiente.; Cada fila representa una agrupación a nivel de detalle de remisión (red.Id) combinada con el estado/código del comprobante asociado.; Solo se incluyen remisiones cuyo proveedor tiene tercero, y cuyos productos pertenecen a un grupo con concepto contable de cuentas por pagar y cuenta principal definidos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportRemissionEntranceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Remisión de entrada de inventario; Proveedor; Tercero (NIT); Bodega/almacén; Producto de inventario; Lote y número de serie; Comprobante/vale de entrada; Cantidad pendiente por legalizar; IVA; Concepto contable de cuentas por pagar; Moneda', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportRemissionEntranceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.RemissionEntrance: Devuelve una fila por combinación de remisión/producto/bodega/lote-serie con cantidades agregadas (SUM de Quantity y OutstandingQuantity) y el valor pendiente calculado como ROUND(OutstandingQuantity * UnitValue * (100 + IvaPercentage)/100, 2).; [RETURN_RESULT] Inventory.EntranceVoucher: Solo se cruza el comprobante de entrada cuando EntranceVoucherDetail.EntranceSource = 4 y EntranceVoucher.Status = 2; en caso contrario las columnas StatusEntranceVoucher y CodeEntranceVoucher quedan NULL (LEFT JOIN al CTE).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportRemissionEntranceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntranceVoucherDetail.EntranceSource = 4 AND EntranceVoucher.Status = 2 → Se considera que el lote/serie ya fue legalizado en un comprobante de entrada y se exponen su código y estado. else El lote/serie aparece sin comprobante asociado (NULL), indicando recepción pendiente de legalizar.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportRemissionEntranceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.RemissionEntranceDetailBatchSerial; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucher; Inventory.RemissionEntrance; Inventory.RemissionEntranceDetail; Inventory.Warehouse; Common.Supplier; Common.ThirdParty; Inventory.InventoryProduct; Inventory.ProductGroup; Payments.AccountPayableConcepts; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportRemissionEntranceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportRemissionEntranceDetail';
GO
