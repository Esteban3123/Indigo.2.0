///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Juan Carlos Bermudez Gutierrez
/// Created          : 02-05-2015
///
/// Copyright        : (c) . All rights reserved.
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
    public interface IInventoryRequest
    {

        /// <summary>
        /// Guarda o Actualiza una solicitud de inventario
        /// </summary>
        /// <param name="inventoryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryRequest> SaveInventoryRequest(Domain.Entities.InventoryRequest inventoryRequest, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC);

        /// <summary>
        /// Elimina una solicitud de inventario
        /// 
        /// </summary>
        /// <param name="inventoryRequest"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteInventoryRequest(Domain.Entities.InventoryRequest inventoryRequest, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryRequest> ChangeStateInventoryRequest(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta una solicitud de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryRequest> GetInventoryRequestByCode(string code, AuditMessage audit);

        /// <summary>
        /// Consulta una solicitud de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryRequest GetInventoryRequestById(int id);

        /// <summary>
        /// CopyPaste/Import solicitudes
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.InventoryRequestDetail>> SP_CopyPasteAndImportRequests(List<List<string>> data);

        /// <summary>
        /// CopyPaste/Import medicamentos,insumos,otros
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.InventoryRequestDetailOther>> SP_CopyPasteAndImportRequestsOtherDetail(List<List<string>> data);
    }
}
