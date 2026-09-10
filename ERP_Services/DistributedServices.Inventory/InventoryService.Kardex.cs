using Application.Inventory.InventoryKardex;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Obtiene la cantidad total de productos en el kardex de una bodega
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public int GetQuantityKardex(int warehouseId)
        {
            using (var service = Container.Current.Resolve<IInventoryKardexAdminService>())
            {
                return service.GetQuantityKardex(warehouseId);
            }
        }
    }
}
