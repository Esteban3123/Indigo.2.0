///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Hector Rodriguez Rubiano
/// Created          : 04-10-2019
/// 
/// Copyright        : (c) . All rights reserved.
/// About            : PBI3499
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
    public interface IInventoryServicePurchaseRequestDetail
    {

        /// <summary>
        /// obtiene una lista de detalles de solicitud de compra de inventario por unidad funcional
        /// </summary>
        /// <param name="idFunctionalUnit"></param>
        /// <returns></returns>
        [OperationContract]
         List<Domain.Entities.PurchaseRequestDetail> ListPurchaseRequestDetailByFunctionalUnit(int idFunctionalUnit);

        /// <summary>
        /// Consulta un detalle de solicitud de compra de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.PurchaseRequestDetail GetPurchaseRequestDetailById(int id);

        /// <summary>
        /// Cambia La cantidad pendiente del item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="QuantityExported"></param>
        /// <returns></returns>
        [OperationContract]
        ActionMessageResult ChangeQuantityPurchaseRequestDetail(int id, int QuantityExported);

    }
}
