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
using System.ServiceModel;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServicePurchaseRequest
    {

        /// <summary>
        /// Guarda o Actualiza una solicitud de compra de inventario
        /// </summary>
        /// <param name="oPurchaseRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PurchaseRequest> SavePurchaseRequest(Domain.Entities.PurchaseRequest oPurchaseRequest, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC);

        /// <summary>
        /// Elimina una solicitud de compra de inventario
        /// 
        /// </summary>
        /// <param name="oPurchaseRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeletePurchaseRequest(Domain.Entities.PurchaseRequest oPurchaseRequest, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PurchaseRequest> ChangeStatePurchaseRequest(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta una solicitud de compra de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PurchaseRequest> GetPurchaseRequestByCode(string code, AuditMessage audit);

        /// <summary>
        /// Consulta una solicitud de compra de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.PurchaseRequest GetPurchaseRequestById(int id);

    }
}
