using Application.Inventory.SurgicalPackageProcess;
using DistributedServices.Inventory.Contracts;
using DistributedServices.Inventory.Unity;
using Domain.Base.Entities;
using Domain.Inventory.POCO;
using Infrastructure.CrossCutting.Base;
using System;
using Microsoft.Practices.Unity;


namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        public ActionResult<string> AllSurgicalPackageTransaction(AllSurgicalPackageProcessWrapper objParams, AuditMessage audit, DateTime serverDate, string container)
        {
            ActionResult<string> result = null;

            using (var service = Container.Current.Resolve<ISurgicalPackageProcessAdminService>())
            {
                result = service.AllSurgicalPackageTransaction(objParams, audit, serverDate, container);
            }

            return result;
        }
    }
}
