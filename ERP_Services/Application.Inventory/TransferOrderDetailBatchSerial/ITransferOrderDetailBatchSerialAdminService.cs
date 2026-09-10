//***********************************************************************
// Assembly         : Aplication.Inventory
// Author           : Juan Carlos Bermudez
// Created          : 03-06-2015
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;

namespace Application.Inventory.TransferOrderDetailBatchSerial
{
    public interface ITransferOrderDetailBatchSerialAdminService : IDisposable
    {

        /// <summary>
        /// Lista los detalles del detalle de la orden de traslado
        /// </summary>
        /// <param name="transferOrderId"></param>
        /// <returns></returns>
        List<Domain.Entities.TransferOrderDetailBatchSerial> ListTransferOrderDetailBatchSerialByTransferOrderId(int transferOrderId, bool flagQuantiyZero);

    }
}
