//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Juan Carlos Bermudez
// Created          : 25-05-2015
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

namespace Application.Inventory.TransferOrderDetail
{
    public interface ITransferOrderDetailAdminService : IDisposable
    {

        /// <summary>
        /// Obtiene los detalles de una orden de traslado por almacen o unidad funcional de destino
        /// </summary>
        /// <param name="idFilter"></param>
        /// <param name="orderType"></param>
        /// <param name="dispatchTo"></param>
        /// <returns></returns>
        List<Domain.Entities.TransferOrderDetail> ListTrasnferOrderDetailByTarget(int idFilter, int orderType, int dispatchTo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        System.Data.DataSet ListAverageConsumptionTransfer(string parameters, SessionValues session);

        /// <summary>
        /// Metodo que lista el reporte de entradas de inventario.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        System.Data.DataSet ListAverageEntranceOrder(string parameters, SessionValues session);

    }
}
