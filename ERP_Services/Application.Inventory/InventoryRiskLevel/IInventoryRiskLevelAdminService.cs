//'************************************************************
//' Assembly         : Domain.Inventory.IInventoryRiskLevel
//' Author           : John Ortiz
//' Created          : 30/10/2014
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.InventoryRiskLevel
{
    public interface IInventoryRiskLevelAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un nivel de riesgo
        /// </summary>
        /// <param name="riskLevel">Nivel de riesgo que se va guardar</param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryRiskLevel> SaveRiskLevel(Domain.Entities.InventoryRiskLevel riskLevel, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un nivel de riesgo
        /// </summary>
        /// <param name="riskLevel">Nivel de riesgo que se va a eliminar</param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteRiskLevel(Domain.Entities.InventoryRiskLevel riskLevel, AuditMessage audit);

        /// <summary>
        /// Consulta el nivel de riesgo por codigo
        /// </summary>
        /// <param name="code">Codigo del nivel de riesgo que se va almacenar</param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.InventoryRiskLevel> GetRiskLevelByCode(string code, AuditMessage audit);

        /// <summary>
        /// Actualiza el estado de un nivel de riesgo
        /// </summary>
        /// <param name="code">Codigo</param>
        /// <param name="audit"></param>
        /// <returns></returns>

        ActionResult<Domain.Entities.InventoryRiskLevel> UpdateStateInventoryRiskLevel(string code, bool state, AuditMessage audit);
    }
}
