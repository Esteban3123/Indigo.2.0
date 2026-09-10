using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;

namespace Application.Inventory.InventoryContractModification
{
    public interface IInventoryContractModificationAdminService : IDisposable
    {

        /// <summary>
        /// Consulta la modificación de contrato por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContractModification> GetInventoryContractModification(string code, AuditMessage audit);

        /// <summary>
        /// Consulta una modificación de contrato por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContractModification> GetInventoryContractModificationById(int id);

        /// <summary>
        /// Guarda o actualiza una modificación de contrato
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContractModification> SaveInventoryContractModification(Domain.Entities.InventoryContractModification InventoryContractModification, AuditMessage audit);

    }
}
