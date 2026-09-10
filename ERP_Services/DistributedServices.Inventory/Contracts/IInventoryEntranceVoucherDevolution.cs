///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Henry Alejandro Vargas Polania
/// Created          : 09-03-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

#region Imported Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel; 
#endregion

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryEntranceVoucherDevolution
    {
        /// <summary>
        /// Guarda o actualiza un EntranceVoucherDevolution
        /// </summary>
        /// <param name="EntranceVoucherDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.EntranceVoucherDevolution> SaveEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution EntranceVoucherDevolution, long idSequense, AuditMessage audit, Domain.Entities.InventorySequence sequenceC);

        /// <summary>
        /// Elimina un EntranceVoucherDevolution
        /// </summary>
        /// <param name="EntranceVoucherDevolution"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution EntranceVoucherDevolution, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de EntranceVoucherDevolution
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.EntranceVoucherDevolution> ChangeStateEntranceVoucherDevolution(string code, byte state, AuditMessage audit);

        /// <summary>
        /// Consulta el EntranceVoucherDevolution por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.EntranceVoucherDevolution> GetEntranceVoucherDevolution(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un EntranceVoucherDevolution por id
        /// </summary>
        /// <param name="idEntranceVoucherDevolution"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.EntranceVoucherDevolution GetEntranceVoucherDevolutionById(int idEntranceVoucherDevolution);

        /// <summary>
        /// Guardar y confirma la devolucion del comprobante de entrada
        /// </summary>
        /// <param name="entranceDevolution"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.EntranceVoucherDevolution> SaveAndConfirmbEntranceVoucherDevolution(Domain.Entities.EntranceVoucherDevolution entranceDevolution, AuditMessage audit, long idSequense, Domain.Entities.InventorySequence sequenceC, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert);
    }
}
