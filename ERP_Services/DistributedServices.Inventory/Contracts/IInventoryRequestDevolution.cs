///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Faiber Julian Mora D.
/// Created          : 28/09/2016
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
    public interface IInventoryRequestDevolution
    {
        /// <summary>
        /// Guarda o actualiza una devolución de solicitud de inventario
        /// </summary>
        /// <param name="inventoryRequestdevolution"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryRequestDevolution> SaveInventoryRequestDevolution(Domain.Entities.InventoryRequestDevolution inventoryRequestdevolution, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC);

        /// <summary>
        /// Elimina una devolución de solicitud de inventario
        /// </summary>
        /// <param name="inventoryRequestdevolution"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteInventoryrequestdevolution(Domain.Entities.InventoryRequestDevolution inventoryRequestdevolution, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryRequestDevolution> ChangeStatusInventoryRequest(string code, byte status, AuditMessage audit);

        /// <summary>
        /// Consulta una devolución de solicitud por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryRequestDevolution> GetRequestDevolutionByCode(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene una solicitud de devolución de inventario po id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryRequestDevolution> GetRequestdevolutionById(int id, AuditMessage audit);

    }
}
