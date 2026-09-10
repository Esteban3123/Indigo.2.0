using System;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.Collections.Generic;
using Domain.Entities;
using Application.Inventory.Rollback;

namespace Application.Inventory.TransferOrder
{
    public interface ITransferOrderAdminService : IAdminServiceRollbackStrategy, IDisposable
    {
        /// <summary>
        /// Obtiene una orden de traslado por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.TransferOrder GetTransferOrderById(int id);

        /// <summary>
        /// Obtiene una orden de traslado por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        Domain.Entities.TransferOrder GetTranferOrderByCode(string code, AuditMessage audit);

        /// <summary>
        /// Guarda o actualiza una orden de traslado
        /// </summary>
        /// <param name="transferOrder"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.TransferOrder> SaveTrasnferOrder(Domain.Entities.TransferOrder transferOrder, AuditMessage audit);

        /// <summary>
        /// Guarda o actualiza el estado de una solicitud
        /// </summary>
        /// <param name="transferOrder"></param>
        /// <param name="audit"></param>
        /// <returns></returns>        
        ActionResult<List<Domain.Entities.ViewListRequestDetailImport>> ChangeStateInventoryRequestDetail(List<ViewListRequestDetailImport> data);
    }
}
