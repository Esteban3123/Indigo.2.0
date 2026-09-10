#region Imports

using Application.Inventory.ConsignmentInventoryRemissionDetail;
using DistributedServices.Inventory.Unity;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

#endregion Imports

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId"></param>
        /// <returns></returns>
        public List<Domain.Entities.ConsignmentInventoryRemissionDetail> GetConsignmentInventoryRemissionDetailByConsignmentInventoryRemissionId(int ConsignmentInventoryRemissionId)
        {
            using (var service = Container.Current.Resolve<IConsignmentInventoryRemissionDetailAdminService>())
            {
                return service.GetConsignmentInventoryRemissionDetailByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId);
            }
            //return _consignmentInventoryRemissionDetailAdminService.GetConsignmentInventoryRemissionDetailByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId);
        }

        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <param name="SupplierDistributionLineId"></param>
        /// <returns></returns>
        public List<Domain.Entities.ConsignmentInventoryRemissionDetail> ListConsignmentInventoryRemissionDetailBySupplierAndSupplierDistributionLine(int SupplierId, int SupplierDistributionLineId)
        {
            using (var service = Container.Current.Resolve<IConsignmentInventoryRemissionDetailAdminService>())
            {
                return service.ListConsignmentInventoryRemissionDetailBySupplierAndSupplierDistributionLine(SupplierId, SupplierDistributionLineId);
            }
            //return _consignmentInventoryRemissionDetailAdminService.ListConsignmentInventoryRemissionDetailBySupplierAndSupplierDistributionLine(SupplierId, SupplierDistributionLineId);
        }
    }
}