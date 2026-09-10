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
using Application.Inventory.AdministrationRoute;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza una via de administracion
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AdministrationRoute> SaveAdministrationRoute(Domain.Entities.AdministrationRoute AdministrationRoute, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAdministrationRouteAdminService>())
            {
                return service.SaveAdministrationRoute(AdministrationRoute, audit, idSequense);
            }
            //return _administrationRouteAdminService.SaveAdministrationRoute(AdministrationRoute, audit, idSequense);
        }

        /// <summary>
        /// Elimina una via de administracion
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteAdministrationRoute(Domain.Entities.AdministrationRoute AdministrationRoute, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAdministrationRouteAdminService>())
            {               
                return service.DeleteAdministrationRoute(AdministrationRoute, audit);
            }
            //return _administrationRouteAdminService.DeleteAdministrationRoute(AdministrationRoute, audit);
        }

        /// <summary>
        /// Obtiene una via de administracion por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AdministrationRoute> GetAdministrationRoute(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAdministrationRouteAdminService>())
            {                
                return service.GetAdministrationRoute(code, audit);
            }
            //return _administrationRouteAdminService.GetAdministrationRoute(code, audit);
        }

        /// <summary>
        /// Obtiene una via de administracion por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AdministrationRoute> GetAdministrationRouteById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAdministrationRouteAdminService>())
            {                
                return service.GetAdministrationRouteById(id, audit);
            }
            //return _administrationRouteAdminService.GetAdministrationRouteById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AdministrationRoute> ChangeStateAdministrationRoute(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAdministrationRouteAdminService>())
            {                
                return service.ChangeStateAdministrationRoute(code, state, audit);
            }
            //return _administrationRouteAdminService.ChangeStateAdministrationRoute(code, state, audit);
        }


    }
}
