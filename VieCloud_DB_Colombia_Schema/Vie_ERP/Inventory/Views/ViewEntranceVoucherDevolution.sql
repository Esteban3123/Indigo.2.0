
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los valores financieros de las devoluciones a proveedor que están en estado aprobado o procesado (Status = 2), calculando por cada línea de detalle devuelta el subtotal, el descuento, el IVA y la retención en la fuente (RTF). Integra el encabezado de la devolución, el detalle de ítems devueltos, los lotes o seriales afectados y el comprobante de entrada original para obtener los valores unitarios y porcentajes aplicables. Sirve como base de reportería y conciliación contable de devoluciones de compras de inventario, agrupando las cantidades devueltas por comprobante de entrada y línea de producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewEntranceVoucherDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewEntranceVoucherDevolution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los valores económicos (subtotal, descuento, IVA y retención en la fuente) de las devoluciones confirmadas de comprobantes de entrada de inventario, agrupados por devolución y línea original.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewEntranceVoucherDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las devoluciones deben estar en estado 2 (Status = 2) para ser incluidas.; Cada detalle de devolución debe estar vinculado a un lote/serial de una línea de comprobante de entrada existente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewEntranceVoucherDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El subtotal devuelto se calcula como cantidad devuelta × valor unitario original de la línea de entrada.; El descuento se aplica sobre el subtotal usando el porcentaje de descuento de la línea original.; El IVA y la RTF (retención en la fuente) se calculan sobre la base ya descontada (subtotal × (100 - %descuento)/100).; Cada valor monetario calculado se redondea a 2 decimales antes de sumarse.; El identificador de cada fila combina el Id de la devolución, el Id del comprobante de entrada y el Id del detalle de entrada, garantizando unicidad por agrupación.; Los valores económicos de la devolución heredan los porcentajes (descuento, IVA, RTF) y el valor unitario del comprobante de entrada original, no se redefinen en la devolución.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewEntranceVoucherDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de compra; Comprobante de entrada de inventario; Lote/Serial; Descuento; IVA; Retención en la fuente (RTF); Servicio de ronda (RoundService)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewEntranceVoucherDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultado de la vista): Solo se retornan filas cuyas devoluciones cumplen evdev.Status = 2; las demás se excluyen.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewEntranceVoucherDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si evdev.Status = 2 → Se incluye la devolución en el cálculo de los totales económicos. else La devolución se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewEntranceVoucherDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucherDevolution; Inventory.EntranceVoucherDevolutionDetail; Inventory.EntranceVoucherDetailBatchSerial; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucher', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewEntranceVoucherDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewEntranceVoucherDevolution';
GO
