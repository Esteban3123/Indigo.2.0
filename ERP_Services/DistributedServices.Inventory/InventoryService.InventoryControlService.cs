using Application.Inventory.InventoryControl;
using DistributedServices.Inventory.Contracts;
using DistributedServices.Inventory.Unity;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;
using System.ServiceModel;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryInventoryControlService
    {
        public ActionResult<InventoryControlService> GetInventoryControlServiceByEntityIdAndEntityName(int entityId, string entityName)
        {
            using (var service = Container.Current.Resolve<IInventoryControlServiceAdminService>())
            {
                return service.GetInventoryControlServiceByEntityIdAndEntityName(entityId, entityName);
            }
        }

        public ActionResult<InventoryControlService> GetInventoryControlServiceByEntityCodeAndEntityName(string entityCode, string entityName)
        {
            using (var service = Container.Current.Resolve<IInventoryControlServiceAdminService>())
            {
                return service.GetInventoryControlServiceByEntityCodeAndEntityName(entityCode, entityName);
            }
        }

        public ActionResult<InventoryControlService> SaveInventoryControlService(InventoryControlService inventoryControlService)
        {
            using (var service = Container.Current.Resolve<IInventoryControlServiceAdminService>())
            {
                return service.SaveInventoryControlService(inventoryControlService);
            }
        }
    }
}