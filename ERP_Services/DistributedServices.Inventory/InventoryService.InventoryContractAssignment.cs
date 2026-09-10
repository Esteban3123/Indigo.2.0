using Application.Inventory.InventoryContractAssignment;
using DistributedServices.Inventory.Contracts;
using DistributedServices.Inventory.Unity;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryContractAssignment
    {
        public ActionResult<InventoryContractAssignment> GetInventoryContractAssignment(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractAssignmentAdminService>())
            {
                return service.GetInventoryContractAssignment(code, audit);
            }
        }

        public ActionResult<InventoryContractAssignment> GetInventoryContractAssignmentById(int id)
        {
            using (var service = Container.Current.Resolve<IInventoryContractAssignmentAdminService>())
            {
                return service.GetInventoryContractAssignmentById(id);
            }
        }

        public ActionResult<InventoryContractAssignment> SaveInventoryContractAssignment(InventoryContractAssignment InventoryContractAssignment, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractAssignmentAdminService>())
            {
                return service.SaveInventoryContractAssignment(InventoryContractAssignment, audit);
            }
        }
    }
}
