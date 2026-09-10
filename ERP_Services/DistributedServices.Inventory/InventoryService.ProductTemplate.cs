using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.ProductTemplate;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Saves the product template.
        /// </summary>
        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.ProductRate>> SaveProductTemplate(Domain.Entities.ProductRate productTemplate, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductTemplateAdminService>())
            {               
                return await service.SaveProductTemplate(productTemplate, audit, idSequense);
            }
        }

        /// <summary>
        /// Deletes the product template.
        /// </summary>
        public Domain.Base.Entities.ActionResult DeleteProductTemplate(Domain.Entities.ProductRate productTemplate, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductTemplateAdminService>())
            {                
                return service.DeleteProductTemplate(productTemplate, audit);
            }
            //return _productTemplateAdminService.DeleteProductTemplate(productTemplate, audit);
        }

        /// <summary>
        /// Updates the state product template.
        /// </summary>
        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.ProductRate>> UpdateStateProductTemplate(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductTemplateAdminService>())
            {                
                return await service.UpdateStateProductTemplate(code, state, audit);
            }
            //return _productTemplateAdminService.UpdateStateProductTemplate(code, state, audit);
        }

        /// <summary>
        /// Gets the product template.
        /// </summary>
        public async Task<Domain.Base.Entities.ActionResult<Domain.Entities.ProductRate>> GetProductTemplate(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductTemplateAdminService>())
            {               
                return await service.GetProductTemplate(code, audit);
            }
            //return _productTemplateAdminService.GetProductTemplate(code, audit);
        }

        /// <summary>
        /// Gets the product template by identifier.
        /// </summary>
        public Domain.Entities.ProductRate GetProductTemplateById(int id)
        {
            using (var service = Container.Current.Resolve<IProductTemplateAdminService>())
            {
                return service.GetProductTemplateById(id);
            }
            //return _productTemplateAdminService.GetProductTemplateById(id);
        }
    }
}