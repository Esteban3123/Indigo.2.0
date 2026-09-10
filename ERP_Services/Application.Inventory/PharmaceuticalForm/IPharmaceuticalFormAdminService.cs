using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.PharmaceuticalForm
{
    public interface IPharmaceuticalFormAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza una forma farmaceutica
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalForm> SavePharmaceuticalForm(Domain.Entities.PharmaceuticalForm PharmaceuticalForm, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina una forma farmaceutica
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeletePharmaceuticalForm(Domain.Entities.PharmaceuticalForm PharmaceuticalForm, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de PharmaceuticalForm
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalForm> ChangeStatePharmaceuticalForm(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta una forma farmaceutica por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalForm> GetPharmaceuticalForm(string code, AuditMessage audit);

        /// <summary>
        /// Consulta una forma farmaceutica por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmaceuticalForm> GetPharmaceuticalFormById(int idPharmaceuticalForm, AuditMessage audit);
    }
}
