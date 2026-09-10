using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryRiskLevel
    {
        /// <summary>
        /// Guarda o actualiza un nivel de riesgo
        /// </summary>
        /// <param name="riskLevel">Nivel de riesgo que se va guardar</param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryRiskLevel> SaveRiskLevel(Domain.Entities.InventoryRiskLevel riskLevel, long idSequense, AuditMessage audit);

        /// <summary>
        /// Elimina un nivel de riesgo
        /// </summary>
        /// <param name="riskLevel">Nivel de riesgo que se va a eliminar</param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteRiskLevel(Domain.Entities.InventoryRiskLevel riskLevel, AuditMessage audit);

        /// <summary>
        /// Consulta el nivel de riesgo por codigo
        /// </summary>
        /// <param name="code">Codigo del nivel de riesgo que se va almacenar</param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryRiskLevel> GetRiskLevelByCode(string code, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.InventoryRiskLevel> UpdateStateInventoryRiskLevel(string code, bool state, AuditMessage audit);

    }
}
