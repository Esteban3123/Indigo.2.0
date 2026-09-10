using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;
using Application.Inventory.ATCEntity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza una via de administracion
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ATCEntity> SaveATCEntity(Domain.Entities.ATCEntity ATCEntity, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IATCEntityAdminService>())
            {
                return service.SaveATCEntity(ATCEntity, audit, idSequense);
            }
            //return _administrationRouteAdminService.SaveAdministrationRoute(AdministrationRoute, audit, idSequense);
        }

        /// <summary>
        /// Elimina una via de administracion
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteATCEntity(Domain.Entities.ATCEntity ATCEntity, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IATCEntityAdminService>())
            {
                return service.DeleteATCEntity(ATCEntity, audit);
            }
            //return _administrationRouteAdminService.DeleteAdministrationRoute(AdministrationRoute, audit);
        }

        /// <summary>
        /// Obtiene una via de administracion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ATCEntity> GetATCEntity(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IATCEntityAdminService>())
            {
                return service.GetATCEntity(code, audit);
            }
            //return _administrationRouteAdminService.GetAdministrationRoute(code, audit);
        }

        /// <summary>
        /// Obtiene una via de administracion por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ATCEntity> GetATCEntityById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IATCEntityAdminService>())
            {
                return service.GetATCEntityById(id, audit);
            }
            //return _administrationRouteAdminService.GetAdministrationRouteById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ATCEntity> ChangeStateATCEntity(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IATCEntityAdminService>())
            {
                return service.ChangeStateATCEntity(code, state, audit);
            }
            //return _administrationRouteAdminService.ChangeStateAdministrationRoute(code, state, audit);
        }
    }
}
