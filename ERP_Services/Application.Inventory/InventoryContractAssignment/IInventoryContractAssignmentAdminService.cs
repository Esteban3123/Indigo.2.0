using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;

namespace Application.Inventory.InventoryContractAssignment
{
    public interface IInventoryContractAssignmentAdminService : IDisposable
    {

        /// <summary>
        /// Consulta la cesión de contrato por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContractAssignment> GetInventoryContractAssignment(string code, AuditMessage audit);

        /// <summary>
        /// Consulta una cesión de contrato por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContractAssignment> GetInventoryContractAssignmentById(int id);

        /// <summary>
        /// Guarda o actualiza una cesión de contrato
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryContractAssignment> SaveInventoryContractAssignment(Domain.Entities.InventoryContractAssignment InventoryContractAssignment, AuditMessage audit);

    }
}
