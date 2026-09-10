//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Juan Carlos Bermudez Gutierrez
//' Created          : 09/04/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using Infrastructure.CrossCutting.Base;
using Application.Inventory.PackagingUnit;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza una unidad de paquete
        /// </summary>
        /// <param name="measureUnit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PackagingUnit> SavePackagingUnit(Domain.Entities.PackagingUnit packagingUnit, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPackagingUnitAdminService>())
            {               
                return service.SavePackagingUnit(packagingUnit, audit, idSequense);
            }
            //return _packagingUnitAdminService.SavePackagingUnit(packagingUnit, audit, idSequense);
        }

        /// <summary>
        /// Elimina una unidad de paquete
        /// </summary>
        /// <param name="measureUnit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeletePackagingUnit(Domain.Entities.PackagingUnit packagingUnit, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPackagingUnitAdminService>())
            {               
                return service.DeletePackagingUnit(packagingUnit, audit);
            }
            //return _packagingUnitAdminService.DeletePackagingUnit(packagingUnit, audit);
        }

        /// <summary>
        /// Obtiene una unidad de paquete por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PackagingUnit> GetPackagingUnitByCode(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPackagingUnitAdminService>())
            {                
                return service.GetPackagingUnitByCode(code, audit);
            }
            //return _packagingUnitAdminService.GetPackagingUnitByCode(code, audit);
        }

        /// <summary>
        /// Obtiene una unidad de paquete por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PackagingUnit> GetPackagingUnitById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPackagingUnitAdminService>())
            {                
                return service.GetPackagingUnitById(id, audit);
            }
            //return _packagingUnitAdminService.GetPackagingUnitById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PackagingUnit> ChangeStatePackagingUnit(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPackagingUnitAdminService>())
            {                
                return service.ChangeStatePackagingUnit(code, state, audit);
            }
            //return _packagingUnitAdminService.ChangeStatePackagingUnit(code, state, audit);
        }

    }
}
