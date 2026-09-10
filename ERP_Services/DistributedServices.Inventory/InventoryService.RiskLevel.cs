using Application.Inventory.InventoryRiskLevel;
using DistributedServices.Inventory.Unity;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza un nivel de riesgo
        /// </summary>
        /// <param name="riskLevel">Nivel de riesgo que se va guardar</param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryRiskLevel> SaveRiskLevel(Domain.Entities.InventoryRiskLevel riskLevel, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryRiskLevelAdminService>())
            {                
                return service.SaveRiskLevel(riskLevel, audit, idSequense);
            }
            //return _inventoryRiskLevelAdminService.SaveRiskLevel(riskLevel, audit, idSequense);
        }

        /// <summary>
        /// Elimina un nivel de riesgo
        /// </summary>
        /// <param name="riskLevel">Nivel de riesgo que se va a eliminar</param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteRiskLevel(Domain.Entities.InventoryRiskLevel riskLevel, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryRiskLevelAdminService>())
            {                
                return service.DeleteRiskLevel(riskLevel, audit);
            }
            //return _inventoryRiskLevelAdminService.DeleteRiskLevel(riskLevel, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryRiskLevel> UpdateStateInventoryRiskLevel(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryRiskLevelAdminService>())
            {                
                return service.UpdateStateInventoryRiskLevel(code, state, audit);
            }
            //return _inventoryRiskLevelAdminService.UpdateStateInventoryRiskLevel(code, state, audit);
        }

        /// <summary>
        /// Consulta el nivel de riesgo por codigo
        /// </summary>
        /// <param name="code">Codigo del nivel de riesgo que se va almacenar</param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.InventoryRiskLevel> GetRiskLevelByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IInventoryRiskLevelAdminService>())
            {               
                return service.GetRiskLevelByCode(code, audit);
            }
            //return _inventoryRiskLevelAdminService.GetRiskLevelByCode(code, audit);
        }
    }
}
