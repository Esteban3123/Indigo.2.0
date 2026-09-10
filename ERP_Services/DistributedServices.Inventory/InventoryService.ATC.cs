using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Unity;
using Application.Inventory.ATC;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda un ATC
        /// </summary>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ATC> SaveATC(Domain.Entities.ATC atc, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IATCAdminService>())
            {               
                return service.SaveATC(atc, audit, idSequense);
            }
            //return _atcAdminService.SaveATC(atc, audit, idSequense);
        }

        /// <summary>
        /// Elimina un ATC
        /// </summary>
        public Domain.Base.Entities.ActionResult DeleteATC(Domain.Entities.ATC atc, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IATCAdminService>())
            {                
                return service.DeleteATC(atc, audit);
            }
            //return _atcAdminService.DeleteATC(atc, audit);
        }

        /// <summary>
        /// Actualiza el estado de un ATC
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ATC> UpdateStateATC(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IATCAdminService>())
            {               
                return service.UpdateStateATC(code, state, audit);
            }
            //return _atcAdminService.UpdateStateATC(code, state, audit);
        }

        /// <summary>
        /// Obtiene un ATC por Codigo
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ATC> GetATC(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IATCAdminService>())
            {                
                return service.GetATC(code, audit);
            }
            //return _atcAdminService.GetATC(code, audit);
        }

        /// <summary>
        /// Obtiene ATC por Id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Domain.Entities.ATC GetATCById(int id)
        {
            using (var service = Container.Current.Resolve<IATCAdminService>())
            {
                return service.GetATCById(id);
            }
            //return _atcAdminService.GetATCById(id);
        }
    }
}