using Application.Inventory.PurchaseOrderDetail;
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
        public List<Domain.Entities.PurchaseOrderDetail> GetPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(int supplierId, int supplierDistributionLineId)
        {
            using (var service = Container.Current.Resolve<IPurchaseOrderDetailAdminService>())
            {
                return service.GetPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(supplierId, supplierDistributionLineId);
            }
            //return _purchaseOrderDetailAdminService.GetPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(supplierId, supplierDistributionLineId);
        }
    }
}
