using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;
using System;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceDevolutionCause
    {

        /// <summary>
        /// Consulta la causa de devolución por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.DevolutionCause> GetDevolutionCauseByCode(string code, AuditMessage audit);

        /// <summary>
        /// Consulta la causa de devolución por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.DevolutionCause> GetDevolutionCauseById(int id, AuditMessage audit);

        /// <summary>
        /// Guarda o actualiza una causa de devolución
        /// </summary>
        /// <param name="devolutionCause"></param>
        /// <param name="audit"></param>
        /// <param name="secuenceId"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.DevolutionCause> SaveDevolutionCause(Domain.Entities.DevolutionCause devolutionCause, AuditMessage audit, Int64 secuenceId = 0);

        /// <summary>
        /// Cambia el estado de una causa de devolución
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.DevolutionCause> ChangeStateDevolutionCause(int id, bool state, AuditMessage audit);

        /// <summary>
        /// Elimina una causa de devolución
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteDevolutionCause(int id, AuditMessage audit);

    }
}
