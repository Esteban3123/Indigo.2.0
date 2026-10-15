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