using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System.Collections.Generic;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryRequestParam
    {
        /// <summary>
        /// Consulta RequestParam por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.RequestParam GetRequestParamByCode(string code);

        /// <summary>
        /// Guarda un RequestParam
        /// </summary>
        [OperationContract]
        ActionResult<Domain.Entities.RequestParam> SaveRequestParam(Domain.Entities.RequestParam requestParam, AuditMessage audit, long idSecuence = 0);

        /// <summary>
        /// Elimina un RequestParam
        /// </summary>
        [OperationContract]
        ActionResult DeleteRequestParam(Domain.Entities.RequestParam requestParam, AuditMessage audit);

        /// <summary>
        /// Actualiza el estado del RequestParam
        /// </summary>
        [OperationContract]
        ActionResult<Domain.Entities.RequestParam> UpdateStateRequestParam(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Obtiene RequestParam por id
        /// </summary>
        [OperationContract]
        Domain.Entities.RequestParam GetRequestParamById(int id);

        /// <summary>
        /// Carga los detalles de productos
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<RequestParamProduct>> LoadRequestParamProductByImportData(List<List<object>> data);
    }
}
