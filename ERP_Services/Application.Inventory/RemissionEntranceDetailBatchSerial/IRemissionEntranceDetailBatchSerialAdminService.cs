//***********************************************************************
// Assembly         : Aplication.Inventory
// Author           : Henry Alejandro Vargas Polania
// Created          : 28-01-2015
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

namespace Application.Inventory.RemissionEntranceDetailBatchSerial
{
    public interface IRemissionEntranceDetailBatchSerialAdminService : IDisposable
    {
        /// <summary>
        /// Obtiene los detalles de la remision desde el Lote
        /// </summary>
        /// <param name="idSupplier"></param>
        /// <param name="idSupplierDistributionLine"></param>
        /// <returns></returns>
        List<Domain.Entities.RemissionEntranceDetailBatchSerial> ListRemissionEntranceDetailBatchSerialBySupplierAndSupplierDistributionLine(int idSupplier, int idSupplierDistributionLine);
        /// <summary>
        /// lista los detalles del detalle de la remision de entrada
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <returns></returns>
        List<Domain.Entities.RemissionEntranceDetailBatchSerial> ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(int RemissionEntranceId);

        /// <summary>
        /// lista los detalles del detalle de la remision de entrada por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        List<Domain.Entities.RemissionEntranceDetailBatchSerial> ListRemissionEntranceDetailBatchSerialByRemissionEntranceCode(string code);

    }
}
