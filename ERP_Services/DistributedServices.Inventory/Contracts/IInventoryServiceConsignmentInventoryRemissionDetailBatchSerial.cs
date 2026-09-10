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
    public interface IInventoryServiceConsignmentInventoryRemissionDetailBatchSerial
    {
        /// <summary>
        /// Obtiene los detalles de la remision desde el Lote
        /// </summary>
        /// <param name="idSupplier"></param>
        /// <param name="idSupplierDistributionLine"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial> ListConsignmentInventoryRemissionDetailBatchSerialBySupplierAndSupplierDistributionLine(int idSupplier, int idSupplierDistributionLine);

        /// <summary>
        /// lista los detalles del detalle de la remision de entrada
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.ConsignmentInventoryRemissionDetailBatchSerial> ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(int ConsignmentInventoryRemissionId);

        /// <summary>
        /// lista los detalles de la remision de consignacion sin legalizar
        /// </summary>
        /// <param name="code"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.ViewConsignmentInventoryRemissionWithoutLegalize> ListConsignmentInventoryRemissionWithoutLegalize(string code, int warehouseId);
    }
}