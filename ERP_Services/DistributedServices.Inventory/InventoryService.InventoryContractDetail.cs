using Application.Inventory.InventoryContractDetail;
using DistributedServices.Inventory.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        public List<Domain.Entities.InventoryContractDetail> GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(int supplierId, int supplierDistributionLineId, int contractType)
        {
            using (var service = Container.Current.Resolve<IInventoryContractDetailAdminService>())
            {
                return service.GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(supplierId, supplierDistributionLineId, contractType);
            }
            //return _inventoryContractDetailAdminService.GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(supplierId, supplierDistributionLineId,  contractType);
        }
    }
}
