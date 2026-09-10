
using Application.Inventory.TransferOrderDetailBatchSerial;
using DistributedServices.Inventory.Unity;
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
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {

        /// <summary>
        /// lista los detalles del detalle de la orden de traslado por id de la orden de traslado
        /// </summary>
        /// <param name="transferOrderId"></param>
        /// <returns></returns>
        public List<Domain.Entities.TransferOrderDetailBatchSerial> ListTransferOrderDetailBatchSerialByTransferOrderId(int transferOrderId, bool flagQuantiyZero)
        {
            using (var service = Container.Current.Resolve<ITransferOrderDetailBatchSerialAdminService>())
            {
                return service.ListTransferOrderDetailBatchSerialByTransferOrderId(transferOrderId, flagQuantiyZero);
            }
            //return _transferOrderDetailBatchSerialAdminService.ListTransferOrderDetailBatchSerialByTransferOrderId(transferOrderId, flagQuantiyZero);
        }

    }
}
