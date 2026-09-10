using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;

namespace Application.Inventory.ConsignmentTransfer
{
    /// <summary>
    /// Traslado en consignación
    /// </summary>
    public interface IConsignmentTransferAdminService : IDisposable
    {
        /// <summary>
        /// Obtener por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.ConsignmentTransfer GetConsignmentTransferById(int id);
        /// <summary>
        /// Obtener por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Domain.Entities.ConsignmentTransfer GetConsignmentTransferByCode(string code);
        /// <summary>
        /// Guardar
        /// </summary>
        /// <param name="consignmentTransfer"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ConsignmentTransfer> SaveConsignmentTransfer(
            Domain.Entities.ConsignmentTransfer consignmentTransfer,
            AuditMessage audit,
            long idSequence = 0
        );
        /// <summary>
        /// Guardar y confirmar
        /// </summary>
        /// <param name="consignmentTransfer"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ConsignmentTransfer> SaveAndConfirmConsignmentTransfer(
           Domain.Entities.ConsignmentTransfer consignmentTransfer,
           AuditMessage audit,
           long idSequence = 0
        );
        /// <summary>
        /// Confirmar
        /// </summary>
        /// <param name="consignmentTransfer"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ConsignmentTransfer> ConfirmConsignmentTransfer(
            Domain.Entities.ConsignmentTransfer consignmentTransfer,
            AuditMessage audit
        );
        /// <summary>
        /// Consulta las cantidades en el almacén de consignación
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        ConsignmentMovementInventory GetConsignmentInventoryQuantities(int warehouseId, int productId);
    }
}
