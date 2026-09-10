using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory
{
    public interface IAdjustmentConceptAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un concepto de ajuste
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AdjustmentConcept> SaveAdjustmentConcept(Domain.Entities.AdjustmentConcept adjustmentConcept, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un concepto de ajuste
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteAdjustmentConcept(Domain.Entities.AdjustmentConcept adjustmentConcept, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de concepto de ajuste
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AdjustmentConcept> ChangeStateAdjustmentConcept(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta un concepto de ajuste por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AdjustmentConcept> GetAdjustmentConcept(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un concepto de ajuste por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.AdjustmentConcept> GetAdjustmentConceptById(int idAdjustmentConcept, AuditMessage audit);

        /// <summary>
        /// Obtiene un concepto de ajuste por cuenta contable y centro de costo.
        /// </summary>
        /// <param name="conceptType"></param>
        /// <param name="adjustmentAccountId"></param>
        /// <param name="costCenterId"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.AdjustmentConcept>> GetListByAdjustmentAccountCostCenterId(Byte conceptType, int adjustmentAccountId, int costCenterId, AuditMessage audit);
    }
}
