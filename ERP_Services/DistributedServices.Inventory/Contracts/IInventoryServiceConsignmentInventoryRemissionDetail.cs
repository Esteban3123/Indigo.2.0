///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Miguel Angel Fonseca
/// Created          : 2017-12-12
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

#region Imports

using System.Collections.Generic;
using System.ServiceModel;

#endregion Imports

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceConsignmentInventoryRemissionDetail
    {
        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.ConsignmentInventoryRemissionDetail> GetConsignmentInventoryRemissionDetailByConsignmentInventoryRemissionId(int ConsignmentInventoryRemissionId);

        /// <summary>
        ///
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <param name="SupplierDistributionLineId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.ConsignmentInventoryRemissionDetail> ListConsignmentInventoryRemissionDetailBySupplierAndSupplierDistributionLine(int SupplierId, int SupplierDistributionLineId);
    }
}