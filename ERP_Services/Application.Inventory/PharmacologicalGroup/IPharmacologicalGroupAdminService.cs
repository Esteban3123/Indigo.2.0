using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;


namespace Application.Inventory.PharmacologicalGroup
{
    public interface IPharmacologicalGroupAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un grupo farmacologico
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmacologicalGroup> SavePharmacologicalGroup(Domain.Entities.PharmacologicalGroup pharmacologicalGroup, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un grupo farmacologico
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeletePharmacologicalGroup(Domain.Entities.PharmacologicalGroup pharmacologicalGroup, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de grupo farmacologico
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmacologicalGroup> ChangeStatePharmacologicalGroup(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta el grupo farmacologico por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmacologicalGroup> GetPharmacologicalGroup(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un grupo farmacologico por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.PharmacologicalGroup> GetPharmacologicalGroupById(int idProductGroup, AuditMessage audit);
    }
}
