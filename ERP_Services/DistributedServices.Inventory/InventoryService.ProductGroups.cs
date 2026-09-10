using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.ProductGroup;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza un grupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductGroup> SaveProductGroup(Domain.Entities.ProductGroup productGroup, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductGroupsAdminService>())
            {                
                return service.SaveProductGroup(productGroup, audit, idSequense);
            }
            //return _productGroupAdminService.SaveProductGroup(productGroup, audit, idSequense);
        }

        /// <summary>
        /// Elimina un grupo
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteProductGroup(Domain.Entities.ProductGroup productGroup, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductGroupsAdminService>())
            {               
                return service.DeleteProductGroup(productGroup, audit);
            }
            //return _productGroupAdminService.DeleteProductGroup(productGroup, audit);
        }

        /// <summary>
        /// Obtiene un grupo por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductGroup> GetProductGroup(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductGroupsAdminService>())
            {                
                return service.GetProductGroup(code, audit);
            }
            //return _productGroupAdminService.GetProductGroup(code, audit);
        }

        /// <summary>
        /// Obtiene un grupo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductGroup> GetProductGroupById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductGroupsAdminService>())
            {                
                return service.GetProductGroupById(id, audit);
            }
            //return _productGroupAdminService.GetProductGroupById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductGroup> ChangeState(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductGroupsAdminService>())
            {               
                return service.ChangeState(code, state, audit);
            }
            //return _productGroupAdminService.ChangeState(code, state, audit);
        }

    }
}
