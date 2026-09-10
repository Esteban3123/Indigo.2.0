using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System.Collections.Generic;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
     public interface IInventoryTrasnferOrder
    {

        /// <summary>
        /// obtiene una orden de traslado por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.TransferOrder GetTransferOrderById(int id);

        /// <summary>
        /// obtiene una orden de traslado por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.TransferOrder GetTranferOrderByCode(string code, AuditMessage audit);

        /// <summary>
        /// guarda, actualiza y confirma una orden de traslado
        /// </summary>
        /// <param name="transferOrder"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.TransferOrder> SaveTrasnferOrder(Domain.Entities.TransferOrder transferOrder, AuditMessage audit);

        /// <summary>
        /// actualiza el estado de los item de la solicitud
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.ViewListRequestDetailImport>> ChangeStateInventoryRequestDetail(List<ViewListRequestDetailImport> data);
    }
}
