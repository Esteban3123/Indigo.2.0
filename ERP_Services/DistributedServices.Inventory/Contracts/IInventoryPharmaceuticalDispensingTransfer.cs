using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
     public interface IInventoryPharmaceuticalDispensingTransfer
    {

        /// <summary>
        /// obtiene una traslado de dispensacion por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.PharmaceuticalDispensingTransfer GetPharmaceuticalDispensingTransferById(int id);

        /// <summary>
        /// obtiene un traslado de dispensacion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.PharmaceuticalDispensingTransfer GetPharmaceuticalDispensingTransferByCode(string code, AuditMessage audit);

        /// <summary>
        /// guarda, actualiza y confirma un traslado de dispensacion
        /// </summary>
        /// <param name="pharmaceuticalDispensingTransfer"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.PharmaceuticalDispensingTransfer> SavePharmaceuticalDispensingTransfer(Domain.Entities.PharmaceuticalDispensingTransfer pharmaceuticalDispensingTransfer, AuditMessage audit);

    }
}
