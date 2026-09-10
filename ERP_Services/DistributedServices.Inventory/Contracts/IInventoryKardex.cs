using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryKardex
    {
        /// <summary>
        /// Obtiene la cantidad total de productos en el kardex de una bodega
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        [OperationContract]
        int GetQuantityKardex(int warehouseId);
    }
}
