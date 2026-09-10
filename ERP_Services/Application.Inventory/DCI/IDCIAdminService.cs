using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.DCI
{
    public interface IDCIAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un DCI
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.DCI> SaveDCI(Domain.Entities.DCI DCI, List<DrugInteraction> ListDeleteDrugInteraction, List<DrugActive> ListDeleteDrugActive, List<LethalDoseLimits> ListDeleteLethalDoseLimits, List<RisksDescription> ListDeleteRisksDescription, List<DCIRiskFactors> ListDeleteDCIRiskFactors, SessionValues audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un DCI
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteDCI(Domain.Entities.DCI DCI, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de DCI
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.DCI> ChangeStateDCI(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Obtiene una interacción de medicamento por el Id del DCI padre
        /// </summary>
        /// <param name="ParentDCIid"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        List<Domain.Entities.DrugInteraction> GetDrugInteractionByDCIParentId(int ParentDCIid);

        /// <summary>
        /// Consulta el DCI por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.DCI> GetDCI(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un DCI por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.DCI> GetDCIById(int idDCI, AuditMessage audit);

        /// <summary>
        /// Función para Actualizar el estado del DCI
        /// </summary>
        /// <param name="DCI"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.DCI> SaveStateDCI(Domain.Entities.DCI DCI, AuditMessage audit, long idSecuence = 0);
        IEnumerable<WarningHighRiskDrugModel> GetHighRiskDrugsByAtcCode(List<string> atcCodes);
    }
}
