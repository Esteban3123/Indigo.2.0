using Application.Inventory.RemissionEntranceDetailBatchSerial;
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
        public List<Domain.Entities.RemissionEntranceDetailBatchSerial> ListRemissionEntranceDetailBatchSerialBySupplierAndSupplierDistributionLine(int idSupplier, int idSupplierDistributionLine)
        {
            using (var service = Container.Current.Resolve<IRemissionEntranceDetailBatchSerialAdminService>())
            {
                return service.ListRemissionEntranceDetailBatchSerialBySupplierAndSupplierDistributionLine(idSupplier, idSupplierDistributionLine);
            }
            //return _remissionEntranceDetailBatchSerialAdminService .ListRemissionEntranceDetailBatchSerialBySupplierAndSupplierDistributionLine (idSupplier, idSupplierDistributionLine);   
        }

        /// <summary>
        /// Lists the remission entrance detail batch serial by remission entrance identifier.
        /// </summary>
        /// <param name="RemissionEntranceId">The remission entrance identifier.</param>
        /// <returns></returns>
        public List<Domain.Entities.RemissionEntranceDetailBatchSerial> ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(int RemissionEntranceId)
        {
            using (var service = Container.Current.Resolve<IRemissionEntranceDetailBatchSerialAdminService>())
            {
                return service.ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(RemissionEntranceId);
            }
            //return _remissionEntranceDetailBatchSerialAdminService.ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(RemissionEntranceId);
        }

        /// <summary>
        /// lista los detalles del detalle de la remision de entrada por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<Domain.Entities.RemissionEntranceDetailBatchSerial> ListRemissionEntranceDetailBatchSerialByRemissionEntranceCode(string code)
        {
            using (var service = Container.Current.Resolve<IRemissionEntranceDetailBatchSerialAdminService>())
            {
                return service.ListRemissionEntranceDetailBatchSerialByRemissionEntranceCode(code);
            }
        }

    }
}
