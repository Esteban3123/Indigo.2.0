using Application.Inventory.RemissionOutputDetailPhysical;
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
    partial class InventoryService
    {
        /// <summary>
        /// Lists the remission output detail physical by remission output identifier.
        /// </summary>
        /// <param name="RemissionOutputId">The remission output identifier.</param>
        /// <returns></returns>
        public List<Domain.Entities.RemissionOutputDetailPhysical> ListRemissionOutputDetailPhysicalByRemissionOutputId(int RemissionOutputId)
        {
            using (var service = Container.Current.Resolve<IRemissionOutputDetailPhysicalAdminService>())
            {
                return service.ListRemissionOutputDetailPhysicalByRemissionOutputId(RemissionOutputId);
            }
            //return _remissionOutputDetailPhysicalAdminService.ListRemissionOutputDetailPhysicalByRemissionOutputId(RemissionOutputId);
        }
    }
}
