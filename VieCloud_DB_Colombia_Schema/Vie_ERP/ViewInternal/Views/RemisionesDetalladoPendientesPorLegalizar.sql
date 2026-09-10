

CREATE view [ViewInternal].[RemisionesDetalladoPendientesPorLegalizar]
as (
select s.Code + ' - ' + s.Name as Proveedor
, re.Code as CodigoRemision
, re.RemissionDate as FechaRemision
, re.Description as Descripcion
, re.CreationUser
, w.Code + ' - ' + w.Name as Almacen
, ip.Code + ' - ' + ip.Name as Producto
, sum(bs.Quantity) as CantidadInicial
, sum(bs.Quantity * ip.ProductCost) as ValorInicial
, sum(bs.OutstandingQuantity) as CantidadPendiente
, sum(bs.OutstandingQuantity * ip.ProductCost) as ValorPendiente
from Inventory.RemissionEntrance re
inner join Inventory.Warehouse w on w.Id = re.WarehouseId
inner join Common.Supplier s on s.Id = re.SupplierId
inner join Inventory.RemissionEntranceDetail red on red.RemissionEntranceId = re.Id
inner join Inventory.RemissionEntranceDetailBatchSerial bs on bs.RemissionEntranceDetailId = red.Id
inner join Inventory.InventoryProduct ip on ip.Id = red.ProductId
inner join Inventory.ProductGroup pg on pg.Id = ip.ProductGroupId
inner join Payments.AccountPayableConcepts apc on apc.Id = pg.InventoryAccountPayableConceptId
inner join GeneralLedger.MainAccounts ma on ma.Id = apc.IdAccount
where re.Status = 2 and re.WarehouseId not in (select Id from Inventory.Warehouse where Code in ('103','07','06'))
group by s.Code, s.Name, re.Code, re.CreationUser, ip.Code, ip.Name, re.RemissionDate, re.Description, w.Code, w.Name
having sum(bs.OutstandingQuantity) > 0
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida las remisiones de entrada de inventario en estado 2 (confirmadas/pendientes de legalizar) que aún tienen cantidades pendientes por legalizar, excluyendo almacenes con códigos ''103'', ''07'' y ''06''. Agrupa por proveedor, remisión, producto y almacén, calculando cantidades y valores iniciales versus pendientes usando el costo del producto. Sirve para el seguimiento contable-operativo de recepciones de mercancía no completamente legalizadas ante cuentas por pagar.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesDetalladoPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesDetalladoPendientesPorLegalizar';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las remisiones de entrada de inventario activas con cantidades aún pendientes por legalizar, mostrando totales iniciales y pendientes valorizados al costo del producto, agrupadas por proveedor, almacén y producto.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesDetalladoPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las remisiones deben tener Status = 2 (estado que representa pendientes/activas para legalización).; El almacén de la remisión no debe estar entre los códigos excluidos ''103'', ''07'' y ''06''.; Cada remisión debe tener detalle, lotes/series, producto, grupo de producto, concepto de cuenta por pagar y cuenta contable principal asociados (todas las uniones son INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesDetalladoPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan remisiones con estado 2 (pendientes).; Se excluyen sistemáticamente los almacenes con códigos ''103'', ''07'' y ''06''.; La valorización (ValorInicial y ValorPendiente) siempre se calcula multiplicando cantidades por el ProductCost del producto.; Solo aparecen combinaciones proveedor/remisión/almacén/producto con saldo pendiente estrictamente positivo.; Se requiere que el producto tenga grupo con concepto de cuenta por pagar y cuenta contable mayor configurados (de lo contrario el INNER JOIN excluye la fila).', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesDetalladoPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Remisión de entrada de inventario; Proveedor; Almacén/Bodega; Producto de inventario; Lote y serie; Cantidad pendiente por legalizar; Costo del producto; Concepto de cuenta por pagar; Cuenta contable (plan de cuentas)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesDetalladoPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve solo agrupaciones cuya suma de OutstandingQuantity sea mayor a 0 (HAVING sum(bs.OutstandingQuantity) > 0), es decir, con cantidad pendiente real por legalizar.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesDetalladoPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si re.Status = 2 y WarehouseId no pertenece a almacenes con Code en (''103'',''07'',''06'') → La remisión se incluye en el resultado else Se excluye de la vista; si sum(bs.OutstandingQuantity) > 0 tras el agrupamiento → La fila agrupada se retorna else Se descarta del resultado final', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesDetalladoPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.RemissionEntrance; Inventory.Warehouse; Common.Supplier; Inventory.RemissionEntranceDetail; Inventory.RemissionEntranceDetailBatchSerial; Inventory.InventoryProduct; Inventory.ProductGroup; Payments.AccountPayableConcepts; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesDetalladoPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesDetalladoPendientesPorLegalizar';
GO
