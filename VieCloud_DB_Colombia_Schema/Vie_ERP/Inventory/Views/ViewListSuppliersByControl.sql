

CREATE VIEW [Inventory].[ViewListSuppliersByControl]
AS

select evd.Id EntranceVoucherDetailId, evd.ProductId, s.Code SupplierCode, s.Name SupplierName, evd.UnitValue
from Inventory.EntranceVoucher ev
inner join Inventory.EntranceVoucherDetail evd on evd.EntranceVoucherId = ev.Id
inner join Common.Supplier s on s.Id = ev.SupplierId
where ev.Status = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de proveedores asociados a productos recibidos en inventario, obtenida a partir de los comprobantes de entrada aprobados (estado 2). Cruza los vales de ingreso de mercancía con su detalle de ítems y los datos del proveedor, exponiendo por cada línea de recepción: el identificador del detalle del comprobante, el producto recibido, el código y nombre del proveedor, y el valor unitario pagado. Sirve para controlar y consultar qué proveedor suministró cada producto y a qué precio, considerando únicamente las recepciones confirmadas o legalizadas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListSuppliersByControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListSuppliersByControl';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los proveedores asociados a cada producto recibido en bodega mediante comprobantes de entrada confirmados, junto con el valor unitario registrado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListSuppliersByControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir comprobantes de entrada con Status = 2 (estado confirmado/aprobado).; Cada detalle debe estar vinculado a un comprobante de entrada existente y este a un proveedor válido.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListSuppliersByControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen entradas de inventario en estado 2 (típicamente confirmadas/aprobadas).; Cada fila relaciona un detalle de entrada con un único proveedor a través del comprobante padre.; El valor unitario reportado proviene del detalle de la entrada, no de un maestro de precios.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListSuppliersByControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de entrada de inventario; Detalle de entrada; Proveedor; Producto; Valor unitario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListSuppliersByControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve únicamente filas cuyo comprobante de entrada cumple ev.Status = 2; los demás estados quedan excluidos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListSuppliersByControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ev.Status = 2 → Se incluye el detalle del comprobante junto con el proveedor y su valor unitario en el resultado. else Se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListSuppliersByControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Common.Supplier', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListSuppliersByControl';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListSuppliersByControl';
GO
