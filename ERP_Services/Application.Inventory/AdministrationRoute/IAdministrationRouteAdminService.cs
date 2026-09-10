using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.AdministrationRoute
{
    public interface IAdministrationRouteAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza una via de administracion
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AdministrationRoute> SaveAdministrationRoute(Domain.Entities.AdministrationRoute AdministrationRoute, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina una via de adminisatracion
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteAdministrationRoute(Domain.Entities.AdministrationRoute AdministrationRoute, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AdministrationRoute> ChangeStateAdministrationRoute(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta una via de administracion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AdministrationRoute> GetAdministrationRoute(string code, AuditMessage audit);

        /// <summary>
        /// Consulta una via de administracion por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AdministrationRoute> GetAdministrationRouteById(int idAdministrationRoute, AuditMessage audit);
    }
}
