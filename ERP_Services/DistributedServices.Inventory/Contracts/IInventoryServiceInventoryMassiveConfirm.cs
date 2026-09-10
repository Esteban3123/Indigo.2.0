using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceInventoryMassiveConfirm
    {
        /// <summary>
        /// Metodo para confirmar un documentos del módulo de inventario
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        Task<ActionResult<Tuple<string, int>>> ConfirmInventoryDocument(int processId, string code, AuditMessage audit);

        /// <summary>
        /// metodo para confirmar masivamente los documentos de inventarios
        /// </summary>
        /// <param name="listDocuments"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Tuple<string, int>>> ConfirmInventoryDocuments(int processId, List<string> listDocuments, AuditMessage audit);
    }
}