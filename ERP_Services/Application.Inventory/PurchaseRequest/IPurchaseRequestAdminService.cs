//'************************************************************
//' Assembly         : Application.Inventory.PurchaseRequest
//' Author           : Hector Rodriguez Rubiano
//' Created          : 10/04/2019
//'
//' Copyright        : (c) . All rights reserved.
//' About            : PBI3499
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.PurchaseRequest
{
     public interface IPurchaseRequestAdminService : IDisposable
    {

        /// <summary>
        /// Guarda o actualiza una solicitud de compra de inventario
        /// </summary>
        /// <param name="oPurchaseRequest"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <param name="sequenceC"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PurchaseRequest> SavePurchaseRequest(Domain.Entities.PurchaseRequest oPurchaseRequest, AuditMessage audit, Int64 idSecuence = 0, InventorySequence sequenceC = null);

        /// <summary>
        /// Elimina una solicitud de compra de inventario
        /// </summary>
        /// <param name="oPurchaseRequest"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeletePurchaseRequest(Domain.Entities.PurchaseRequest oPurchaseRequest, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PurchaseRequest> ChangeStatePurchaseRequest(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta una solicitud de compra de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PurchaseRequest> GetPurchaseRequestByCode(string code, AuditMessage audit);

        /// <summary>
        /// Consulta una solicitud de compra de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.PurchaseRequest GetPurchaseRequestById(int id);

    }
}
