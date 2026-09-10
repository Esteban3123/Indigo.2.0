-- =============================================
-- Author:pablo alexander salazar sanchez
-- Create Date: 29/09/2023
-- Description: se obtiene el proveedor del producto para la generacion de los furips2 
-- =============================================
CREATE FUNCTION [Billing].[GetSupplierSod]
(
    @ServiceOrderDetailId int
)
RETURNS varchar(100)
AS
BEGIN

    DECLARE @ResultVar  varchar(100)

	SELECT TOP 1
			
		@ResultVar = iif(w.WarehouseConsignment = 1, sr.Name, sr2.Name)
			
	from Billing.ServiceOrderDetail sod
		JOIN Billing.ServiceOrder so WITH(NOLOCK) ON so.id = sod.ServiceOrderId
		JOIN Inventory.PharmaceuticalDispensing pd WITH(NOLOCK) on pd.id = so.EntityId	
		JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.PharmaceuticalDispensingId = pd.id and pdd.ProductId = sod.ProductId
		JOIN Inventory.Warehouse w WITH(NOLOCK) on w.Id = pdd.WarehouseId
		JOIN Common.Supplier sr WITH(NOLOCK) on sr.id = w.SupplierId 
		JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH(NOLOCK) ON pddbs.PharmaceuticalDispensingDetailId = pdd.id
		JOIN Inventory.PhysicalInventory pci WITH(NOLOCK) on pci.id = pddbs.PhysicalInventoryId
		JOIN Inventory.Kardex k ON k.ProductId = pci.ProductId and k.BatchSerialId = pci.BatchSerialId and k.EntityName in ('EntranceVoucher', 'RemissionEntrance')
		LEFT JOIN Inventory.EntranceVoucher ev WITH(NOLOCK) on ev.id = k.EntityId
		LEFT JOIN Inventory.RemissionEntrance re WITH(NOLOCK) ON re.id = k.EntityId
		JOIN Common.Supplier sr2 WITH(NOLOCK) ON sr2.id = ISNULL(ev.SupplierId,re.SupplierId)
	where sod.id = @ServiceOrderDetailId 
	ORDER by k.DocumentDate DESC

    RETURN @ResultVar
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado el identificador de un ítem de orden de servicio (detalle de facturación), retorna el nombre del proveedor asociado al medicamento dispensado. Si la bodega de despacho es de consignación, devuelve el proveedor dueño de la bodega; en caso contrario, devuelve el proveedor del comprobante de entrada o remisión más reciente registrado en el kardex de inventario para el lote o serial dispensado. Se utiliza en la generación del archivo FURIPS 2 (reporte de suministros a entidades pagadoras), recorriendo la cadena dispensación farmacéutica → bodega → lote/serial → movimiento de inventario para identificar con exactitud el proveedor/fabricante del producto facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetSupplierSod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'GetSupplierSod';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el proveedor asociado al producto dispensado en un detalle de orden de servicio, para alimentar el reporte FURIPS 2.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetSupplierSod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de orden de servicio debe existir y estar enlazado a una dispensación farmacéutica con detalle por producto.; Debe existir al menos un movimiento de Kardex para el producto y lote/serial cuyo EntityName sea ''EntranceVoucher'' o ''RemissionEntrance''.; La bodega referenciada debe tener proveedor asociado (w.SupplierId) cuando se trate de bodega en consignación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetSupplierSod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el movimiento de kardex de entrada más reciente por DocumentDate (TOP 1 + ORDER BY DESC).; El proveedor del kardex se resuelve priorizando EntranceVoucher.SupplierId y, si es nulo, RemissionEntrance.SupplierId (ISNULL).; La trazabilidad del proveedor sigue la cadena: ServiceOrderDetail → ServiceOrder → PharmaceuticalDispensing → PharmaceuticalDispensingDetail (mismo ProductId) → Warehouse → BatchSerial → PhysicalInventory → Kardex → EntranceVoucher/RemissionEntrance.; El emparejamiento producto-bodega exige coincidencia de ProductId entre el detalle de la orden de servicio y el detalle de dispensación farmacéutica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetSupplierSod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Proveedor; Bodega en consignación; Dispensación farmacéutica; Kardex de inventario; Comprobante de entrada; Remisión de entrada; Lote/Serial; FURIPS 2; Orden de servicio (facturación)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetSupplierSod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna varchar(100) con el nombre del proveedor: si la bodega es de consignación (w.WarehouseConsignment = 1) devuelve el proveedor de la bodega (Common.Supplier vía w.SupplierId); en caso contrario devuelve el proveedor del comprobante de entrada o remisión más reciente (ORDER BY k.DocumentDate DESC TOP 1).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetSupplierSod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si w.WarehouseConsignment = 1 (bodega en consignación) → Retorna el nombre del proveedor asociado a la bodega (sr.Name vía Warehouse.SupplierId). else Retorna el nombre del proveedor del último movimiento de entrada en kardex (sr2.Name vía ISNULL(ev.SupplierId, re.SupplierId)).; si k.EntityName IN (''EntranceVoucher'',''RemissionEntrance'') → Filtra solo movimientos de kardex que correspondan a entradas por comprobante o remisión, ignorando otros tipos de movimiento.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetSupplierSod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetail; Billing.ServiceOrder; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.Warehouse; Common.Supplier; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PhysicalInventory; Inventory.Kardex; Inventory.EntranceVoucher; Inventory.RemissionEntrance', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetSupplierSod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'GetSupplierSod';
GO
