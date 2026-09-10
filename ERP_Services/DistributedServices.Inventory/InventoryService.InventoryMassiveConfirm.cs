using Application.Inventory.InventoryMassiveConfirm;
using DistributedServices.Inventory.Unity;
using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        public async Task<ActionResult<Tuple<string, int>>> ConfirmInventoryDocument(int processId, string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryMassiveConfirmAdminService>())
            {
                return await service.ConfirmInventoryDocument(processId, code, audit);
            }
        }

        public ActionResult<List<Tuple<string, int>>> ConfirmInventoryDocuments(int processId, List<string> listDocuments, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryMassiveConfirmAdminService>())
            {
                return service.ConfirmInventoryDocuments(processId, listDocuments, audit);
            }
        }
    }
}