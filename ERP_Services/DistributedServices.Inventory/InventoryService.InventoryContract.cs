using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.InventoryContract;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryContract
    {
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryContract> SaveInventoryContract(Domain.Entities.InventoryContract InventoryContract, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractAdminService>())
            {               
                return service.SaveInventoryContract(InventoryContract, audit, idSequense);
            }
            //return _inventoryContractAdminService.SaveInventoryContract(InventoryContract, audit, idSequense);
        }

        public Domain.Base.Entities.ActionResult DeleteInventoryContract(Domain.Entities.InventoryContract InventoryContract, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractAdminService>())
            {               
                return service.DeleteInventoryContract(InventoryContract, audit);
            }
            //return _inventoryContractAdminService.DeleteInventoryContract(InventoryContract, audit);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryContract> ChangeStateInventoryContract(string code, byte state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractAdminService>())
            {               
                return service.ChangeStateInventoryContract(code, state, audit);
            }
            //return _inventoryContractAdminService.ChangeStateInventoryContract(code, state, audit);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryContract> GetInventoryContract(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryContractAdminService>())
            {                
                return service.GetInventoryContract(code, audit);
            }
            //return _inventoryContractAdminService.GetInventoryContract(code, audit);
        }

        public Domain.Entities.InventoryContract GetInventoryContractById(int idInventoryContract)
        {
            using (var service = Container.Current.Resolve<IInventoryContractAdminService>())
            {
                return service.GetInventoryContractById(idInventoryContract);
            }
            //return _inventoryContractAdminService.GetInventoryContractById(idInventoryContract);
        }
    }
}
