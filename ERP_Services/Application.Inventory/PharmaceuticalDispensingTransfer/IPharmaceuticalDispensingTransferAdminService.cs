using System;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;

namespace Application.Inventory.PharmaceuticalDispensingTransfer
{
    public interface IPharmaceuticalDispensingTransferAdminService : IDisposable
    {
        /// <summary>
        /// Obtiene un traslado por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.PharmaceuticalDispensingTransfer GetPharmaceuticalDispensingTransferById(int id);

        /// <summary>
        /// Obtiene un traslado por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        Domain.Entities.PharmaceuticalDispensingTransfer GetPharmaceuticalDispensingTransferByCode(string code, AuditMessage audit);

        /// <summary>
        /// Guarda o actualiza un traslado
        /// </summary>
        /// <param name="pharmaceuticalDispensingTransfer"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalDispensingTransfer> SavePharmaceuticalDispensingTransfer(Domain.Entities.PharmaceuticalDispensingTransfer pharmaceuticalDispensingTransfer, AuditMessage audit);
    }
}
