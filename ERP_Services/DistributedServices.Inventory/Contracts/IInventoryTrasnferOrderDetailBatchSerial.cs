///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Juan Carlos Bermudez
/// Created          : 03-06-2015
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
    public interface IInventoryTrasnferOrderDetailBatchSerial
    {

        /// <summary>
        /// lista los detalles del detalle de la orden de traslado por id de la orden de traslado
        /// </summary>
        /// <param name="transferOrderId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.TransferOrderDetailBatchSerial> ListTransferOrderDetailBatchSerialByTransferOrderId(int transferOrderId, bool flagQuantiyZero);

    }
}
