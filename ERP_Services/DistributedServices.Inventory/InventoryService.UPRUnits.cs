using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Unity;
using Application.Inventory;
using Microsoft.Practices.Unity;
using Application.Inventory.UPRUnits;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza un concepto de ajuste
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.UPRUnits> SaveUPRUnits(Domain.Entities.UPRUnits uPRUnits, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IUPRUnitsAdminService>())
            {
                return service.SaveUPRUnits(uPRUnits, audit, idSequense);
            }
        }
        
        /// <summary>
        /// Obtiene un concepto de ajuste por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.UPRUnits> GetUPRUnits(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IUPRUnitsAdminService>())
            {
                return service.GetUPRUnits(code, audit);
            }
        }

        /// <summary>
        /// Obtiene un concepto de ajuste por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.UPRUnits> GetUPRUnitsById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IUPRUnitsAdminService>())
            {
                return service.GetUPRUnitsById(id, audit);
            }
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.UPRUnits> ChangeStateUPRUnits(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IUPRUnitsAdminService>())
            {
                return service.ChangeStateUPRUnits(code, state, audit);
            }
        }

    }
}
