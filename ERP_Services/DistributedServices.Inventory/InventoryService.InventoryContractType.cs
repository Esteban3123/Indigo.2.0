using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.InventoryContractType;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryContractType
    {
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryContractType> SaveInventoryContractType(Domain.Entities.InventoryContractType inventoryContractType, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractTypeAdminService>())
            {                
                return service.SaveInventoryContractType(inventoryContractType, audit, idSequense);
            }
            //return _inventoryContracTypeAdminService.SaveInventoryContractType(inventoryContractType, audit, idSequense);
        }

        public Domain.Base.Entities.ActionResult DeleteInventoryContractType(Domain.Entities.InventoryContractType inventoryContractType, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractTypeAdminService>())
            {               
                return service.DeleteInventoryContractType(inventoryContractType, audit);
            }
            //return _inventoryContracTypeAdminService.DeleteInventoryContractType(inventoryContractType, audit);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryContractType> ChangeStateInventoryContractType(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractTypeAdminService>())
            {               
                return service.ChangeStateInventoryContractType(code, state, audit);
            }
            //return _inventoryContracTypeAdminService.ChangeStateInventoryContractType(code, state, audit);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryContractType> GetInventoryContractType(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractTypeAdminService>())
            {                
                return service.GetInventoryContractType(code, audit);
            }
            //return _inventoryContracTypeAdminService.GetInventoryContractType(code, audit);
        }

        public Domain.Entities.InventoryContractType GetInventoryContractTypeById(int idInventoryContractType)
        {
            using (var service = Container.Current.Resolve<IInventoryContractTypeAdminService>())
            {
                return service.GetInventoryContractTypeById(idInventoryContractType);
            }
            //return _inventoryContracTypeAdminService.GetInventoryContractTypeById(idInventoryContractType);
        }
    }
}
