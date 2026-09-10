using Application.Inventory.PurchaseOrderDevolution;
using DistributedServices.Inventory.Unity;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        public Domain.Entities.PurchaseOrderDevolution GetPurchaseOrderDevolutionByCode(string Code)
        {
            using (var service = Container.Current.Resolve<IPurchaseOrderDevolutionAdminService>())
            {
                return service.GetPurchaseOrderDevolutionByCode(Code);
            }
            //return _purchaseOrderDevolutionAdminService.GetPurchaseOrderDevolutionByCode(Code);
        }

        public Domain.Entities.PurchaseOrderDevolution GetPurchaseOrderDevolutionById(int Id)
        {
            using (var service = Container.Current.Resolve<IPurchaseOrderDevolutionAdminService>())
            {
                return service.GetPurchaseOrderDevolutionById(Id);
            }
            //return _purchaseOrderDevolutionAdminService.GetPurchaseOrderDevolutionById(Id);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.PurchaseOrderDevolution> SavePurchaseOrderDevolution(Domain.Entities.PurchaseOrderDevolution PurchaseOrderDevolution, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPurchaseOrderDevolutionAdminService>())
            {               
                return service.SavePurchaseOrderDevolution(PurchaseOrderDevolution, audit, idSequense);
            }
            //return _purchaseOrderDevolutionAdminService.SavePurchaseOrderDevolution(PurchaseOrderDevolution, audit, idSequense);
        }

        public List<Domain.Entities.PurchaseOrderDevolutionDetail> GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId(int PurchaseOrderDevolutionId)
        {
            using (var service = Container.Current.Resolve<IPurchaseOrderDevolutionAdminService>())
            {
                return service.GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId(PurchaseOrderDevolutionId);
            }
            //return _purchaseOrderDevolutionAdminService.GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId(PurchaseOrderDevolutionId);
        }
    }
}
