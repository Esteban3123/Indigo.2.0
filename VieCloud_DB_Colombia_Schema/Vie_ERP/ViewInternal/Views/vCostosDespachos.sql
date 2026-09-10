

CREATE View [ViewInternal].[vCostosDespachos]
as
(
select 
tor.DocumentDate as FechaDocumento
,ip.Code as CodigoProducto
,ip.Name as Producto
,c.Code + ' - ' + c.Name as CentroCosto
,case pt.Class when 3 then 'Equipo Medico' else 'Varios' end as Tipo
,pg.Name as Grupo
,tod.Quantity as Cantidad
,ip.ProductCost as CostoPromedio
,ip.ProductCost * tod.Quantity as Total
from Inventory.TransferOrder tor
inner join Inventory.TransferOrderDetail tod on tod.TransferOrderId = tor.Id
inner join Inventory.InventoryProduct ip on ip.Id = tod.ProductId
inner join Inventory.ProductGroup pg on pg.Id = ip.ProductGroupId
inner join Inventory.ProductType pt on pt.Id = ip.ProductTypeId
inner join Payroll.FunctionalUnit fu on fu.Id = tor.TargetFunctionalUnitId
inner join Payroll.CostCenter as c on c.Id = fu.CostCenterId
where pt.Class in (3,4) and tor.Status = 2

)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida los costos de despachos de inventario (órdenes de traslado en estado 2) restringidos a tipos de producto de clase 3 (Equipo Médico) y clase 4 (Varios). Cruza productos con su costo promedio, cantidad despachada y total calculado, clasificados por grupo de producto y centro de costo de la unidad funcional destino. Permite analizar el gasto por despachos internos discriminado por fecha de documento, producto y área receptora.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vCostosDespachos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vCostosDespachos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el costo de los despachos de inventario (equipos médicos y varios) realizados mediante órdenes de transferencia confirmadas, valorizados al costo promedio del producto y agrupados por centro de costo destino.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vCostosDespachos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de transferencia deben tener Status = 2 (despachadas/confirmadas) para ser consideradas.; Los productos deben pertenecer a tipos cuya Class sea 3 (Equipo Médico) o 4 (Varios).; La unidad funcional destino debe estar asociada a un centro de costo válido.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vCostosDespachos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes con Status = 2.; Solo se incluyen productos cuyo tipo tenga Class 3 o 4.; El total monetario siempre se calcula como costo promedio del producto multiplicado por la cantidad transferida.; El centro de costo se determina a partir de la unidad funcional destino de la transferencia (TargetFunctionalUnitId).; El código del centro de costo se presenta concatenado con su nombre en formato ''Code - Name''.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vCostosDespachos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de transferencia de inventario; Despacho de productos; Equipo médico; Centro de costo; Unidad funcional; Costo promedio de producto; Grupo de productos', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vCostosDespachos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewInternal.vCostosDespachos: Devuelve una fila por detalle de orden de transferencia cuando pt.Class IN (3,4) y tor.Status = 2, con el costo total calculado como ProductCost * Quantity.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vCostosDespachos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pt.Class = 3 → Clasifica el ítem como ''Equipo Medico'' else Clasifica el ítem como ''Varios'' (aplica para Class = 4 según el filtro)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vCostosDespachos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.TransferOrder; Inventory.TransferOrderDetail; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductType; Payroll.FunctionalUnit; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vCostosDespachos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'vCostosDespachos';
GO
