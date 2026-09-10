using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Contracts;
using DistributedServices.Inventory.Unity;
using Application.Inventory.InventoryAdjustment;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryServiceInventoryAdjustment
    {
        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.InventoryAdjustment>> SaveInventoryAdjustment(Domain.Entities.InventoryAdjustment InventoryAdjustment, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC)
        {
            using (var service = Container.Current.Resolve<IInventoryAdjustmentAdminService>())
            {                
                return await service.SaveInventoryAdjustment(InventoryAdjustment, audit, idSequense, sequenceC);
            }
        }

        public Domain.Base.Entities.ActionResult DeleteInventoryAdjustment(Domain.Entities.InventoryAdjustment InventoryAdjustment, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryAdjustmentAdminService>())
            {                
                return service.DeleteInventoryAdjustment(InventoryAdjustment, audit);
            }
            //return _inventoryAdjustmentAdminService.DeleteInventoryAdjustment(InventoryAdjustment, audit);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryAdjustment> ChangeStateInventoryAdjustment(string code, byte state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryAdjustmentAdminService>())
            {                
                return service.ChangeStateInventoryAdjustment(code, state, audit);
            }
            //return _inventoryAdjustmentAdminService.ChangeStateInventoryAdjustment(code, state, audit);
        }

        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryAdjustment> GetInventoryAdjustment(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryAdjustmentAdminService>())
            {               
                return service.GetInventoryAdjustment(code, audit);
            }
            //return _inventoryAdjustmentAdminService.GetInventoryAdjustment(code, audit);
        }

        public Domain.Entities.InventoryAdjustment GetInventoryAdjustmentById(int idInventoryAdjustment)
        {
            using (var service = Container.Current.Resolve<IInventoryAdjustmentAdminService>())
            {
                return service.GetInventoryAdjustmentById(idInventoryAdjustment);
            }
            //return _inventoryAdjustmentAdminService.GetInventoryAdjustmentById(idInventoryAdjustment);
        }

        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.InventoryAdjustment>> SaveAndConfirmbInventoryAdjustment(Domain.Entities.InventoryAdjustment InventoryAdjustment, int OperatingUnitId, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert)
        {
            using (var service = Container.Current.Resolve<IInventoryAdjustmentAdminService>())
            {                
                return await service.SaveAndConfirmbInventoryAdjustment(InventoryAdjustment, audit, OperatingUnitId, idSequense, action, sequenceC);
            }
            //return _inventoryAdjustmentAdminService.SaveAndConfirmbInventoryAdjustment(InventoryAdjustment, audit, OperatingUnitId, idSequense, action, sequenceC);
        }
    }
}
