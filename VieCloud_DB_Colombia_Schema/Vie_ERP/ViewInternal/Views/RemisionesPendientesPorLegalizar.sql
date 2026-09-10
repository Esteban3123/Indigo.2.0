

CREATE view [ViewInternal].[RemisionesPendientesPorLegalizar]
as (
select MONTH(re.RemissionDate) as Mes
, YEAR(re.RemissionDate) as Anio
,ma.Number
, ma.Name
, sum(bs.Quantity) as CantidadInicial
, sum(bs.Quantity * ip.ProductCost) as ValorInicial
, sum(bs.OutstandingQuantity) as Cantidad
, sum(bs.OutstandingQuantity * ip.ProductCost) as Valor
from Inventory.RemissionEntrance re
inner join Inventory.RemissionEntranceDetail red on red.RemissionEntranceId = re.Id
inner join Inventory.RemissionEntranceDetailBatchSerial bs on bs.RemissionEntranceDetailId = red.Id
inner join Inventory.InventoryProduct ip on ip.Id = red.ProductId
inner join Inventory.ProductGroup pg on pg.Id = ip.ProductGroupId
inner join Payments.AccountPayableConcepts apc on apc.Id = pg.InventoryAccountPayableConceptId
inner join GeneralLedger.MainAccounts ma on ma.Id = apc.IdAccount
where re.Status = 2 and re.WarehouseId not in (select Id from Inventory.Warehouse where Code in ('103','07','06'))
group by ma.Number, ma.Name, MONTH(re.RemissionDate), YEAR(re.RemissionDate)
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consolida las remisiones de entrada de inventario en estado 2 (pendientes de legalizar) excluyendo almacenes con códigos ''103'', ''06'' y ''07'', agrupando por mes, año y cuenta contable del libro mayor. Para cada grupo expone cantidades e importes totales iniciales versus los saldos pendientes por legalizar, calculados desde los lotes/seriales, a fin de apoyar el seguimiento contable y de tesorería de mercancía recibida aún no formalizada.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesPendientesPorLegalizar';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resume mensualmente las remisiones de entrada de inventario pendientes por legalizar, agrupadas por cuenta contable, mostrando cantidades y valores iniciales y saldos pendientes.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las remisiones consideradas deben tener Status = 2 (estado que representa ''pendiente por legalizar'').; El almacén de la remisión no debe corresponder a los códigos ''103'', ''07'' ni ''06''.; Cada producto debe estar vinculado a un grupo de productos con concepto de cuenta por pagar y a una cuenta contable principal.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen remisiones en Status=2 (pendientes por legalizar).; Se excluyen sistemáticamente las bodegas con códigos ''103'',''07'' y ''06''.; El valor monetario se calcula siempre usando el costo del producto (ProductCost) del catálogo maestro, no el costo registrado en el detalle de la remisión.; La agrupación contable se hace por la cuenta principal asociada al concepto de cuentas por pagar del grupo de producto.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Remisión de entrada de inventario; Legalización de remisiones; Lotes y series; Cuenta contable / Plan de cuentas; Concepto de cuentas por pagar; Grupo de productos; Bodega/Almacén; Costo de producto', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve por mes/año y cuenta contable: cantidad inicial y valor inicial (Quantity * ProductCost) y cantidad/valor pendiente (OutstandingQuantity * ProductCost) de las remisiones con Status=2 excluyendo bodegas ''103'',''07'',''06''.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.RemissionEntrance; Inventory.RemissionEntranceDetail; Inventory.RemissionEntranceDetailBatchSerial; Inventory.InventoryProduct; Inventory.ProductGroup; Payments.AccountPayableConcepts; GeneralLedger.MainAccounts; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesPendientesPorLegalizar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'RemisionesPendientesPorLegalizar';
GO
