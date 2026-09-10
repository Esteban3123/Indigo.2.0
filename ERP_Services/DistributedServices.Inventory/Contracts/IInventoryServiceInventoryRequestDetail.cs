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
    public interface IInventoryServiceInventoryRequestDetail
    {

        /// <summary>
        /// obtiene una lista de detalles de solicitud de inventario por almacen o unidad funcional de destino
        /// </summary>
        /// <param name="idFilter"></param>
        /// <param name="orderType"></param>
        /// <param name="dispatchTo"></param>
        /// <returns></returns>
        [OperationContract]
         List<Domain.Entities.InventoryRequestDetail> ListInventoryRequestDetailByTarget(int idFilter, int orderType, int dispatchTo);

        /// <summary>
        /// Consulta un detalle de solicitud de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryRequestDetail GetInventoryRequestDetailById(int id);

        /// <summary>
        /// Cambia La cantidad pendiente del item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="QuantityExported"></param>
        /// <returns></returns>
        [OperationContract]
        ActionMessageResult ChangeQuantityInventoryRequestDetail(int id, int QuantityExported);

        /// <summary>
        /// Actualiza la cantidad de la solicitud seleccionada
        /// </summary>
        /// <param name="id"></param>
        /// <param name="QuantityExported"></param>
        /// <returns></returns>
        [OperationContract]
        ActionMessageResult UpdateQuantityAuthorizedInventoryRequestDetail(int id, int Quantity, string UserCode);

    }
}
