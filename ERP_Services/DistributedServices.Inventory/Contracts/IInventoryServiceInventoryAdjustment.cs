///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Henry Alejandro Vargas Polania
/// Created          : 15-01-2015
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
    public interface IInventoryServiceInventoryAdjustment
    {
        /// <summary>
        /// Guarda o actualiza un InventoryAdjustment
        /// </summary>
        /// <param name="InventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        Task <ActionResult<Domain.Entities.InventoryAdjustment>> SaveInventoryAdjustment(Domain.Entities.InventoryAdjustment InventoryAdjustment, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC);

        /// <summary>
        /// Elimina un InventoryAdjustment
        /// </summary>
        /// <param name="InventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteInventoryAdjustment(Domain.Entities.InventoryAdjustment InventoryAdjustment, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de InventoryAdjustment
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryAdjustment> ChangeStateInventoryAdjustment(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta el InventoryAdjustment por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryAdjustment> GetInventoryAdjustment(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un InventoryAdjustment por id
        /// </summary>
        /// <param name="idInventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.InventoryAdjustment GetInventoryAdjustmentById(int idInventoryAdjustment);

        /// <summary>
        /// guardar y confirmar un InventoryAdjustment
        /// </summary>
        /// <param name="InventoryAdjustment"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        [OperationContract]
        Task <ActionResult<Domain.Entities.InventoryAdjustment>> SaveAndConfirmbInventoryAdjustment(Domain.Entities.InventoryAdjustment InventoryAdjustment, int OperatingUnitId, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert);
    }
}
