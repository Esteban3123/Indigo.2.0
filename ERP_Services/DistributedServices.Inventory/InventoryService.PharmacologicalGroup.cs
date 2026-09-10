using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.PharmacologicalGroup;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza un grupo farmacologico
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmacologicalGroup> SavePharmacologicalGroup(Domain.Entities.PharmacologicalGroup pharmacologicalGroup, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmacologicalGroupAdminService>())
            {                
                return service.SavePharmacologicalGroup(pharmacologicalGroup, audit, idSequense);
            }
            //return _pharmacologicalGroupAdminService.SavePharmacologicalGroup(pharmacologicalGroup, audit, idSequense);
        }

        /// <summary>
        /// Elimina un grupo farmacologico
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeletePharmacologicalGroup(Domain.Entities.PharmacologicalGroup pharmacologicalGroup, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmacologicalGroupAdminService>())
            {                
                return service.DeletePharmacologicalGroup(pharmacologicalGroup, audit);
            }
            //return _pharmacologicalGroupAdminService.DeletePharmacologicalGroup(pharmacologicalGroup, audit);
        }

        /// <summary>
        /// Obtiene un grupo farmacologico por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmacologicalGroup> GetPharmacologicalGroup(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmacologicalGroupAdminService>())
            {                
                return service.GetPharmacologicalGroup(code, audit);
            }
            //return _pharmacologicalGroupAdminService.GetPharmacologicalGroup(code, audit);
        }

        /// <summary>
        /// Obtiene un grupo farmacologico por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmacologicalGroup> GetPharmacologicalGroupById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmacologicalGroupAdminService>())
            {                
                return service.GetPharmacologicalGroupById(id, audit);
            }
            //return _pharmacologicalGroupAdminService.GetPharmacologicalGroupById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmacologicalGroup> ChangeStatePharmacologicalGroup(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmacologicalGroupAdminService>())
            {                
                return service.ChangeStatePharmacologicalGroup(code, state, audit);
            }
            //return _pharmacologicalGroupAdminService.ChangeStatePharmacologicalGroup(code, state, audit);
        }
    }
}
