using System;

namespace Application.Inventory.InventoryKardex
{
    public interface IInventoryKardexAdminService : IDisposable
    {
        /// <summary>
        /// Obtiene la cantidad total de productos en el kardex de una bodega
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        int GetQuantityKardex(int warehouseId);
    }
}
