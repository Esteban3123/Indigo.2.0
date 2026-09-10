using Application.Inventory.InventoryContractModification;
using DistributedServices.Inventory.Contracts;
using DistributedServices.Inventory.Unity;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryContractModification
    {
        public ActionResult<InventoryContractModification> GetInventoryContractModification(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractModificationAdminService>())
            {
                return service.GetInventoryContractModification(code, audit);
            }
        }

        public ActionResult<InventoryContractModification> GetInventoryContractModificationById(int id)
        {
            using (var service = Container.Current.Resolve<IInventoryContractModificationAdminService>())
            {
                return service.GetInventoryContractModificationById(id);
            }
        }

        public ActionResult<InventoryContractModification> SaveInventoryContractModification(InventoryContractModification InventoryContractModification, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractModificationAdminService>())
            {
                return service.SaveInventoryContractModification(InventoryContractModification, audit);
            }
        }
    }
}
