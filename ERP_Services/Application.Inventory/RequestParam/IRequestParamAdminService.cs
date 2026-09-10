using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;

namespace Application.Inventory.RequestParam
{
    public interface IRequestParamAdminService : IDisposable
    {
        /// <summary>
        /// Consulta RequestParam por código
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Domain.Entities.RequestParam GetRequestParamByCode(string code);

        /// <summary>
        /// Guarda un RequestParam
        /// </summary>
        ActionResult<Domain.Entities.RequestParam> SaveRequestParam(Domain.Entities.RequestParam requestParam, AuditMessage audit, long idSecuence = 0);

        /// <summary>
        /// Elimina un RequestParam
        /// </summary>
        ActionResult DeleteRequestParam(Domain.Entities.RequestParam requestParam, AuditMessage audit);

        /// <summary>
        /// Actualiza el estado del RequestParam
        /// </summary>
        ActionResult<Domain.Entities.RequestParam> UpdateStateRequestParam(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Obtiene RequestParam por id
        /// </summary>
        Domain.Entities.RequestParam GetRequestParamById(int id);

        /// <summary>
        /// Crea detalles de parametros de solicitud por imformación a importar
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        ActionResult<List<RequestParamProduct>> LoadRequestParamProductByImportData(List<List<object>> data);
    }
}
