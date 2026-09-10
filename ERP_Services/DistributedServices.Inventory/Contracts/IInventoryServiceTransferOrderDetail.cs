///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Juan Carlos Bermudez
/// Created          : 25-05-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceTransferOrderDetail
    {

        /// <summary>
        /// obtiene una lista de detalles de orden de traslado por almacen o unidad funcional de destino
        /// </summary>
        /// <param name="idFilter"></param>
        /// <param name="orderType"></param>
        /// <param name="dispatchTo"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.TransferOrderDetail> ListTransferOrderDetailByTarget(int idFilter, int orderType, int dispatchTo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        System.Data.DataSet ListAverageConsumptionTransfer(string parameters, Infrastructure.CrossCutting.Base.SessionValues session);

        /// <summary>
        /// Metodo que lista el reporte de entradas de inventario.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        [OperationContract]
        System.Data.DataSet ListAverageEntranceOrder(string parameters, Infrastructure.CrossCutting.Base.SessionValues session);

    }
}
