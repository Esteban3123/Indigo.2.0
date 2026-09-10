using Application.Inventory.RemissionEntranceDetail;
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
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <returns></returns>
        public List<Domain.Entities.RemissionEntranceDetail> GetRemissionEntranceDetailByRemissionEntranceId(int RemissionEntranceId)
        {
            using (var service = Container.Current.Resolve<IRemissionEntranceDetailAdminService>())
            {
                return service.GetRemissionEntranceDetailByRemissionEntranceId(RemissionEntranceId);
            }
            //return _remissionEntranceDetailAdminService.GetRemissionEntranceDetailByRemissionEntranceId(RemissionEntranceId);
        }

        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <param name="SupplierDistributionLineId"></param>
        /// <returns></returns>
        public List<Domain.Entities.RemissionEntranceDetail> ListRemissionEntranceDetailBySupplierAndSupplierDistributionLine(int SupplierId, int SupplierDistributionLineId)
        {
            using (var service = Container.Current.Resolve<IRemissionEntranceDetailAdminService>())
            {
                return service.ListRemissionEntranceDetailBySupplierAndSupplierDistributionLine(SupplierId, SupplierDistributionLineId);
            }
            //return _remissionEntranceDetailAdminService.ListRemissionEntranceDetailBySupplierAndSupplierDistributionLine(SupplierId, SupplierDistributionLineId);
        }
    }
}
