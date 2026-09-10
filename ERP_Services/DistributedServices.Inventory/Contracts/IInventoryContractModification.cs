using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryContractModification
    {

        /// <summary>
        /// Consulta un otro si de contrato por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryContractModification> GetInventoryContractModification(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un otro si de contrato por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryContractModification> GetInventoryContractModificationById(int id);

        /// <summary>
        /// Guarda o actualiza un otro si de contrato
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryContractModification> SaveInventoryContractModification(Domain.Entities.InventoryContractModification InventoryContractModification, AuditMessage audit);

    }
}
