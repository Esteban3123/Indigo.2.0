using Application.Inventory.RemissionOutputDetail;
using DistributedServices.Inventory.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        /// <summary>
        /// Gets the remission output detail by remission output identifier.
        /// </summary>
        /// <param name="RemissionOutputId">The remission output identifier.</param>
        /// <returns></returns>
        public List<Domain.Entities.RemissionOutputDetail> GetRemissionOutputDetailByRemissionOutputId(int RemissionOutputId)
        {
            using (var service = Container.Current.Resolve<IRemissionOutputDetailAdminService>())
            {
                return service.GetRemissionOutputDetailByRemissionOutputId(RemissionOutputId);
            }
            //return _remissionOutputDetailAdminService.GetRemissionOutputDetailByRemissionOutputId(RemissionOutputId);
        }
    }
}
