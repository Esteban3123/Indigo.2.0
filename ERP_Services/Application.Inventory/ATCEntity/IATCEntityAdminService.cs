using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.ATCEntity
{
    public interface IATCEntityAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un ATC
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ATCEntity> SaveATCEntity(Domain.Entities.ATCEntity ATCEntity, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina una via de adminisatracion
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteATCEntity(Domain.Entities.ATCEntity ATCEntity, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ATCEntity> ChangeStateATCEntity(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta una via de administracion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ATCEntity> GetATCEntity(string code, AuditMessage audit);

        /// <summary>
        /// Consulta una via de administracion por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ATCEntity> GetATCEntityById(int idATCEntity, AuditMessage audit);
    }
}
