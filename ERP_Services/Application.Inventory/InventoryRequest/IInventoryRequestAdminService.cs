//'************************************************************
//' Assembly         : Domain.Inventory.EntranceVoucherRepository
//' Author           : Juan Carlos Bermudez Gutierrez
//' Created          : 30/04/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.InventoryRequest
{
     public interface IInventoryRequestAdminService : IDisposable
    {

        /// <summary>
        /// Guarda o actualiza una solicitud de inventario
        /// </summary>
        /// <param name="inventoryRequest"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
         ActionResult<Domain.Entities.InventoryRequest> SaveInventoryRequest(Domain.Entities.InventoryRequest inventoryRequest, AuditMessage audit, Int64 idSecuence = 0, InventorySequence sequenceC = null);

        /// <summary>
        /// Elimina una solicitud de inventario
        /// </summary>
        /// <param name="inventoryRequest"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteInventoryRequest(Domain.Entities.InventoryRequest inventoryRequest, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryRequest> ChangeStateInventoryRequest(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta una solicitud de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryRequest> GetInventoryRequestByCode(string code, AuditMessage audit);

        /// <summary>
        /// Consulta una solicitud de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.InventoryRequest GetInventoryRequestById(int id);

        /// <summary>
        /// CopyPaste/Import solicitudes
        /// </summary>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.InventoryRequestDetail>> SP_CopyPasteAndImportRequests(List<List<string>> data);

        /// <summary>
        /// CopyPaste/Import medicamentos,insumos o otros
        /// </summary>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.InventoryRequestDetailOther>> SP_CopyPasteAndImportRequestsOtherDetail(List<List<string>> data);

    }
}
