using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using DistributedServices.Inventory.Unity;
using Application.Inventory.AttributeProductType;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza un atributo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AttributeProductType> SaveAttributeProductType(Domain.Entities.AttributeProductType attributeProductType, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAttributeProductTypeAdminService>())
            {                
                return service.SaveAttributeProductType(attributeProductType, audit, idSequense);
            }
            //return _attributeProductTypeAdminService.SaveAttributeProductType(attributeProductType, audit, idSequense);
        }

        /// <summary>
        /// Elimina un atributo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteAttributeProductType(Domain.Entities.AttributeProductType attributeProductType, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAttributeProductTypeAdminService>())
            {                
                return service.DeleteAttributeProductType(attributeProductType, audit);
            }
            //return _attributeProductTypeAdminService.DeleteAttributeProductType(attributeProductType, audit);
        }

        /// <summary>
        /// Obtiene un atributo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AttributeProductType> GetAttributeProductType(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAttributeProductTypeAdminService>())
            {               
                return service.GetAttributeProductType(code, audit);
            }
            //return _attributeProductTypeAdminService.GetAttributeProductType(code, audit);
        }

        /// <summary>
        /// Obtiene un atributo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AttributeProductType> GetAttributeProductTypeById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAttributeProductTypeAdminService>())
            {              
                return service.GetAttributeProductTypeById(id, audit);
            }
            //return _attributeProductTypeAdminService.GetAttributeProductTypeById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AttributeProductType> ChangeStateAttributeProductType(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAttributeProductTypeAdminService>())
            {               
                return service.ChangeStateAttributeProductType(code, state, audit);
            }
            //return _attributeProductTypeAdminService.ChangeStateAttributeProductType(code, state, audit);
        }

    }
}
