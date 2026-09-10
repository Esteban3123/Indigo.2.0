using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.ProductType;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Guarda o actualiza un tipo producto
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductType> SaveProductType(Domain.Entities.ProductType productType, long idSequense, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductTypeAdminService>())
            {                
                return service.SaveProductType(productType, audit, idSequense);
            }
            //return _productTypeAdminService.SaveProductType(productType, audit, idSequense);
        }

        /// <summary>
        /// Elimina un tipo de producto
        /// </summary>
        /// <param name="productGroup"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteProductType(Domain.Entities.ProductType productType, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductTypeAdminService>())
            {                
                return service.DeleteProductType(productType, audit);
            }
            //return _productTypeAdminService.DeleteProductType(productType, audit);
        }

        /// <summary>
        /// Obtiene un tipo producto por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductType> GetProductType(string code, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductTypeAdminService>())
            {                
                return service.GetProductType(code, audit);
            }
            //return _productTypeAdminService.GetProductType(code, audit);
        }

        /// <summary>
        /// Obtiene un tipo producto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductType> GetProductTypeById(int id, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductTypeAdminService>())
            {                
                return service.GetProductTypeById(id, audit);
            }
            //return _productTypeAdminService.GetProductTypeById(id, audit);
        }

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ProductType> ChangeStateProductType(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IProductTypeAdminService>())
            {                
                return service.ChangeStateProductType(code, state, audit);
            }
            //return _productTypeAdminService.ChangeStateProductType(code, state, audit);
        }
    }
}
