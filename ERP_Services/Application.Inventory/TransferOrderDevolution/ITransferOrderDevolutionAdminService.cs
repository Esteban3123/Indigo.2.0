using System;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Application.Inventory.Rollback;

namespace Application.Inventory.TransferOrderDevolution
{
    public interface ITransferOrderDevolutionAdminService : IAdminServiceRollbackStrategy, IDisposable
    {

        /// <summary>
        /// obtiene una devolucion de orden de traslado por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        Domain.Entities.TransferOrderDevolution GetTransferOrderDevolutionById(int id, AuditMessage audit);

        /// <summary>
        /// obtiene una devolucion de orden de traslado por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        Domain.Entities.TransferOrderDevolution GetTransferOrderDevolutionByCode(string code, AuditMessage audit);

        /// <summary>
        /// guarda una devolucion de orden de traslado
        /// </summary>
        /// <param name="transferOrderDevolution"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.TransferOrderDevolution> SaveTransferOrderDevolution(Domain.Entities.TransferOrderDevolution transferOrderDevolution, AuditMessage audit);

    }
}
