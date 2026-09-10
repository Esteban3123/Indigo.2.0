using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.PharmaceuticalForm;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza una forma farmaceutica
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalForm> SavePharmaceuticalForm(Domain.Entities.PharmaceuticalForm PharmaceuticalForm, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalFormAdminService>())
            {                
                return service.SavePharmaceuticalForm(PharmaceuticalForm, audit, idSequense);
            }
            //return _pharmaceuticalFormAdminService.SavePharmaceuticalForm(PharmaceuticalForm, audit, idSequense);
        }

        /// <summary>
        /// Elimina una forma farmaceutica
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeletePharmaceuticalForm(Domain.Entities.PharmaceuticalForm PharmaceuticalForm, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalFormAdminService>())
            {                
                return service.DeletePharmaceuticalForm(PharmaceuticalForm, audit);
            }
            //return _pharmaceuticalFormAdminService.DeletePharmaceuticalForm(PharmaceuticalForm, audit);
        }

        /// <summary>
        /// Obtiene una forma farmaceutica por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalForm> GetPharmaceuticalForm(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalFormAdminService>())
            {               
                return service.GetPharmaceuticalForm(code, audit);
            }
            //return _pharmaceuticalFormAdminService.GetPharmaceuticalForm(code, audit);
        }

        /// <summary>
        /// Obtiene una forma farmaceutica por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalForm> GetPharmaceuticalFormById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalFormAdminService>())
            {               
                return service.GetPharmaceuticalFormById(id, audit);
            }
            //return _pharmaceuticalFormAdminService.GetPharmaceuticalFormById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.PharmaceuticalForm> ChangeStatePharmaceuticalForm(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IPharmaceuticalFormAdminService>())
            {                
                return service.ChangeStatePharmaceuticalForm(code, state, audit);
            }
            //return _pharmaceuticalFormAdminService.ChangeStatePharmaceuticalForm(code, state, audit);
        }


    }
}
