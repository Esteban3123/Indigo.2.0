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

namespace Application.Inventory.InventoryRequestDetail
{
    public interface IInventoryRequestDetailAdminService : IDisposable
    {

        /// <summary>
        /// Obtiene los detalles de una solicitud de inventario por almacen o unidad funcional de destino
        /// </summary>
        /// <param name="idFilter"></param>
        /// <param name="orderType"></param>
        /// <param name="dispatchTo"></param>
        /// <returns></returns>
        List<Domain.Entities.InventoryRequestDetail> ListInventoryRequestDetailByTarget(int idFilter, int orderType, int dispatchTo);

        /// <summary>
        /// Obtiene un detalle de solicitud de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.InventoryRequestDetail GetInventoryRequestDetailById(int id);

       /// <summary>
       /// Cambia La cantidad pendiente del item
       /// </summary>
       /// <param name="id"></param>
       /// <param name="QuantityExport"></param>
       /// <param name="audit"></param>
       /// <returns></returns>
        ActionMessageResult ChangeQuantityInventoryRequestDetail(int id, int QuantityExported);

        /// <summary>
        /// Actualiza la cantidad de la solicitud seleccionada
        /// </summary>
        /// <param name="id"></param>
        /// <param name="QuantityExport"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionMessageResult UpdateQuantityAuthorizedInventoryRequestDetail(int id, int Quantity, string User);

    }
}
