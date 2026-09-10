#region Imports

using Application.Inventory.ConsignmentInventoryRemissionDetailBatchSerial;
using DistributedServices.Inventory.Unity;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

#endregion Imports

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        public List<Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial> ListConsignmentInventoryRemissionDetailBatchSerialBySupplierAndSupplierDistributionLine(int idSupplier, int idSupplierDistributionLine)
        {
            using (var service = Container.Current.Resolve<IConsignmentInventoryRemissionDetailBatchSerialAdminService>())
            {
                return service.ListConsignmentInventoryRemissionDetailBatchSerialBySupplierAndSupplierDistributionLine(idSupplier, idSupplierDistributionLine);
            }
            //return _consignmentInventoryRemissionDetailBatchSerialAdminService.ListConsignmentInventoryRemissionDetailBatchSerialBySupplierAndSupplierDistributionLine(idSupplier, idSupplierDistributionLine);
        }

        /// <summary>
        /// Lists the remission entrance detail batch serial by remission entrance identifier.
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId">The remission entrance identifier.</param>
        /// <returns></returns>
        public List<Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial> ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(int ConsignmentInventoryRemissionId)
        {
            using (var service = Container.Current.Resolve<IConsignmentInventoryRemissionDetailBatchSerialAdminService>())
            {
                return service.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId);
            }
            //return _consignmentInventoryRemissionDetailBatchSerialAdminService.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId);
        }

        /// <summary>
        /// lista los detalles de la remision de consignacion sin legalizar
        /// </summary>
        /// <param name="code"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public List<Domain.Entities.ViewConsignmentInventoryRemissionWithoutLegalize> ListConsignmentInventoryRemissionWithoutLegalize(string code, int warehouseId)
        {
            using (var service = Container.Current.Resolve<IConsignmentInventoryRemissionDetailBatchSerialAdminService>())
            {
                return service.ListConsignmentInventoryRemissionWithoutLegalize(code, warehouseId);
            }
            //return _consignmentInventoryRemissionDetailBatchSerialAdminService.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId);
        }
    }
}