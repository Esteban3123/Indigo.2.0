using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryTransferOrderDevolution
    {

        /// <summary>
        /// obtiene una devolucion de orden de traslado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.TransferOrderDevolution GetTransferOrderDevolutionById(int id, AuditMessage audit);

        /// <summary>
        /// obtiene una devolucion de orden de traslado
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.TransferOrderDevolution GetTransferOrderDevolutionByCode(string code, AuditMessage audit);

        /// <summary>
        /// guarda, actualiza y confirma una devolucion de orden de traslado
        /// </summary>
        /// <param name="transferOrderDevolution"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.TransferOrderDevolution> SaveTransferOrderDevolution(Domain.Entities.TransferOrderDevolution transferOrderDevolution, AuditMessage audit);

    }
}
