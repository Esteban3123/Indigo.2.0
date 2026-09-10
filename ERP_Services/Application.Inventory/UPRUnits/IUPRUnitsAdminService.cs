using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.UPRUnits
{
    public interface IUPRUnitsAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un concepto de ajuste
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.UPRUnits> SaveUPRUnits(Domain.Entities.UPRUnits uPRUnits, AuditMessage audit, Int64 idSecuence = 0);
        
        /// <summary>
        /// Cambia el estado de la entidad de concepto de ajuste
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.UPRUnits> ChangeStateUPRUnits(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta un concepto de ajuste por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.UPRUnits> GetUPRUnits(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un concepto de ajuste por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.UPRUnits> GetUPRUnitsById(int id, AuditMessage audit);
    }
}
